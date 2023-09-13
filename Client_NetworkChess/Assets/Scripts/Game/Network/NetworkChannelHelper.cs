//@LeeTools
//------------------------
//Filename：NetworkChannelHelper.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 15:36:40
//Function：Nothing
//------------------------
using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityGameFramework.Runtime;

namespace Game
{
    public class NetworkChannelHelper : INetworkChannelHelper
    {
        private readonly Dictionary<int, Type> m_ServerToClientPacketTypes = new Dictionary<int, Type>();

        private readonly MemoryStream m_CachedStream = new MemoryStream(1024 * 8);

        private readonly byte[] m_CachedBytes = new byte[1024 * 8];

        private INetworkChannel m_NetworkChannel = null;

        /// <summary>
        /// 消息包头长度
        /// </summary>
        public int PacketHeaderLength => throw new NotImplementedException();

        /// <summary>
        /// 初始化网络频道辅助器
        /// </summary>
        /// <param name="networkChannel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Initialize(INetworkChannel networkChannel)
        {
            m_NetworkChannel = networkChannel;

            Type packetBaseType = typeof(SCPacketBase);
            Type packetHandlerBaseType = typeof(PacketHandlerBase);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].IsClass && !types[i].IsAbstract)
                {
                    if (types[i].BaseType == packetBaseType)
                    {
                        SCPacketBase packetBase = (SCPacketBase)Activator.CreateInstance(types[i]);
                        Type packetType = GetServerToClientPacketType(packetBase.Id);
                        if(packetType != null)
                        {
                            Log.Warning("Packet '{0}' and '{1}' have the same packet id '{2}'.", packetType.FullName, types[i].FullName, ((PacketId)Enum.ToObject(typeof(PacketId), packetBase.Id)).ToString());
                            continue;
                        }
                        m_ServerToClientPacketTypes.Add(packetBase.Id, types[i]);
                    }
                    else if (types[i].BaseType == packetHandlerBaseType)
                    {
                        PacketHandlerBase packetHandlerBase = (PacketHandlerBase)Activator.CreateInstance(types[i]);
                        m_NetworkChannel.RegisterHandler(packetHandlerBase);
                    }
                }
            }

            GameRuntime.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameRuntime.Event.Subscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameRuntime.Event.Subscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameRuntime.Event.Subscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameRuntime.Event.Subscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);
        }

        public void Shutdown()
        {
            GameRuntime.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameRuntime.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameRuntime.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId, OnNetworkMissHeartBeat);
            GameRuntime.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameRuntime.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId, OnNetworkCustomError);

            m_NetworkChannel = null;
        }

        /// <summary>
        /// 准备进行连接
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void PrepareForConnecting()
        {
            m_NetworkChannel.Socket.ReceiveBufferSize = 1024 * 64;
            m_NetworkChannel.Socket.SendBufferSize = 1024 * 64;
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SendHeartBeat()
        {
            m_NetworkChannel.Send(ReferencePool.Acquire<CSHeartBeatPacket>());
            return true;
        }

        public bool Serialize<T>(T packet, Stream destination) where T : Packet
        {
            PacketBase packetImpl = packet as PacketBase;
            if(packetImpl == null)
            {
                Log.Error("Packet is invalid.");
                return false;
            }

            if(packetImpl.PacketType != PacketType.ClientToServer)
            {
                Log.Error("Send packet '{0}' is not client to server packet.", packetImpl.Id.ToString());
                return false;
            }

            m_CachedStream.SetLength(m_CachedStream.Capacity); // 此行防止 Array.Copy 的数据无法写入
            m_CachedStream.Position = 0L;

            CSPacketHeader packetHeader = ReferencePool.Acquire<CSPacketHeader>();
            packetHeader.Id = packetImpl.Id;
            packetHeader.PacketLength = packetImpl.Bytes.Length;

            PacketMessge(m_CachedStream, packetHeader, packetImpl);

            ReferencePool.Release(packetHeader);
            ReferencePool.Release(packetImpl);

            m_CachedStream.WriteTo(destination);

            return true;
        }

        /// <summary>
        /// 反序列化消息头
        /// </summary>
        /// <param name="source"></param>
        /// <param name="customErrorData"></param>
        /// <returns></returns>
        public IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData)
        {
            customErrorData = null;
            SCPacketHeader packetHeader = ReferencePool.Acquire<SCPacketHeader>();
            source.Read(m_CachedBytes, 0, 2);
            PacketType packetType = (PacketType)BitConverter.ToUInt16(m_CachedBytes, 0);
            if(packetType != PacketType.ServerToClient)
            {
                Log.Error("Receive packet '{0}' is not server to client packet.", packetType.ToString());
                return null;
            }
            m_CachedBytes.Free();
            source.Read(m_CachedBytes, 0, 2);
            packetHeader.Id = BitConverter.ToUInt16(m_CachedBytes, 0);
            m_CachedBytes.Free();
            source.Read(m_CachedBytes, 0, 4);
            packetHeader.PacketLength = BitConverter.ToInt32(m_CachedBytes, 0);
            m_CachedBytes.Free();
            return packetHeader;
        }

        /// <summary>
        /// 反序列化消息包
        /// </summary>
        /// <param name="packetHeader"></param>
        /// <param name="source"></param>
        /// <param name="customErrorData"></param>
        /// <returns></returns>
        public Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData)
        {
            customErrorData = null;

            SCPacketHeader scPacketHeader = packetHeader as SCPacketHeader;
            if(scPacketHeader == null)
            {
                Log.Error("Packet header is invalid.");
                return null;
            }

            SCPacketBase packet = null;
            if (scPacketHeader.IsValid)
            {
                Type packetType = GetServerToClientPacketType(scPacketHeader.Id);
                if (packetType == null)
                {
                    Log.Error("Can not deserialize packet for packet id '{0}'.", scPacketHeader.Id.ToString());
                    return null;
                }
                packet = (SCPacketBase)ReferencePool.Acquire(packetType);

                source.Read(m_CachedBytes, 8, scPacketHeader.PacketLength);

                packet.Message = GameRuntime.Protobuf.CreateMessage(packetType.Name, m_CachedBytes);
            }
            else
            {
                Log.Error("Packet header is invalid.");
            }

            ReferencePool.Release(scPacketHeader);
            return packet;
        }

        private Type GetServerToClientPacketType(int id)
        {
            Type type = null;
            if(m_ServerToClientPacketTypes.TryGetValue(id, out type))
            {
                return type;
            }
            return null;
        }

        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne = (UnityGameFramework.Runtime.NetworkConnectedEventArgs)e;
            Log.Info("Network channel '{0}' connected, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void OnNetworkClosed(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkClosedEventArgs ne = (UnityGameFramework.Runtime.NetworkClosedEventArgs)e;

            if(ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' closed, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void OnNetworkMissHeartBeat(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs ne = (UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs)e;

            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            if(ne.MissCount < 2)
            {
                return;
            }

            ne.NetworkChannel.Close();

            Log.Info("Network channel '{0}' miss heart beat, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void OnNetworkError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkErrorEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }
            Log.Info("Network channel '{0}' error, local address '{1}', remote address '{2}', error code '{3}', error message '{4}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString(), ne.ErrorCode.ToString(), ne.ErrorMessage);

            ne.NetworkChannel.Close();
        }

        private void OnNetworkCustomError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkCustomErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkCustomErrorEventArgs)e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }
            Log.Info("Network channel '{0}' custom error, local address '{1}', remote address '{2}'.", ne.NetworkChannel.Name, ne.NetworkChannel.Socket.LocalEndPoint.ToString(), ne.NetworkChannel.Socket.RemoteEndPoint.ToString());
        }

        private void PacketMessge(MemoryStream destination, PacketHeaderBase packetHeader, PacketBase packetBase)
        {
            byte[] data = null;
            data.AddRange(BitConverter.GetBytes((ushort)packetHeader.PacketType));
            data.AddRange(BitConverter.GetBytes((ushort)packetHeader.Id));
            data.AddRange(BitConverter.GetBytes(packetHeader.PacketLength));
            data.AddRange(packetBase.Bytes);

            destination.Write(data);
        }

        private byte[] GetBytes(PacketBase packetBase)
        {
            byte[] data = null;
            data.AddRange(BitConverter.GetBytes((ushort)packetBase.PacketType));
            data.AddRange(BitConverter.GetBytes((ushort)packetBase.Id));
            data.AddRange(packetBase.Bytes);
            return data;
        }
    }
}