// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GameFramework.Network
{
  /// <summary>网络管理器。</summary>
  internal sealed class NetworkManager : GameFrameworkModule, INetworkManager
  {
    private readonly Dictionary<string, NetworkManager.NetworkChannelBase> m_NetworkChannels;
    private EventHandler<NetworkConnectedEventArgs> m_NetworkConnectedEventHandler;
    private EventHandler<NetworkClosedEventArgs> m_NetworkClosedEventHandler;
    private EventHandler<NetworkMissHeartBeatEventArgs> m_NetworkMissHeartBeatEventHandler;
    private EventHandler<NetworkErrorEventArgs> m_NetworkErrorEventHandler;
    private EventHandler<NetworkCustomErrorEventArgs> m_NetworkCustomErrorEventHandler;

    /// <summary>初始化网络管理器的新实例。</summary>
    public NetworkManager()
    {
      this.m_NetworkChannels = new Dictionary<string, NetworkManager.NetworkChannelBase>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_NetworkConnectedEventHandler = (EventHandler<NetworkConnectedEventArgs>) null;
      this.m_NetworkClosedEventHandler = (EventHandler<NetworkClosedEventArgs>) null;
      this.m_NetworkMissHeartBeatEventHandler = (EventHandler<NetworkMissHeartBeatEventArgs>) null;
      this.m_NetworkErrorEventHandler = (EventHandler<NetworkErrorEventArgs>) null;
      this.m_NetworkCustomErrorEventHandler = (EventHandler<NetworkCustomErrorEventArgs>) null;
    }

    /// <summary>获取网络频道数量。</summary>
    public int NetworkChannelCount => this.m_NetworkChannels.Count;

    /// <summary>网络连接成功事件。</summary>
    public event EventHandler<NetworkConnectedEventArgs> NetworkConnected
    {
      add => this.m_NetworkConnectedEventHandler += value;
      remove => this.m_NetworkConnectedEventHandler -= value;
    }

    /// <summary>网络连接关闭事件。</summary>
    public event EventHandler<NetworkClosedEventArgs> NetworkClosed
    {
      add => this.m_NetworkClosedEventHandler += value;
      remove => this.m_NetworkClosedEventHandler -= value;
    }

    /// <summary>网络心跳包丢失事件。</summary>
    public event EventHandler<NetworkMissHeartBeatEventArgs> NetworkMissHeartBeat
    {
      add => this.m_NetworkMissHeartBeatEventHandler += value;
      remove => this.m_NetworkMissHeartBeatEventHandler -= value;
    }

    /// <summary>网络错误事件。</summary>
    public event EventHandler<NetworkErrorEventArgs> NetworkError
    {
      add => this.m_NetworkErrorEventHandler += value;
      remove => this.m_NetworkErrorEventHandler -= value;
    }

    /// <summary>用户自定义网络错误事件。</summary>
    public event EventHandler<NetworkCustomErrorEventArgs> NetworkCustomError
    {
      add => this.m_NetworkCustomErrorEventHandler += value;
      remove => this.m_NetworkCustomErrorEventHandler -= value;
    }

    /// <summary>网络管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      foreach (KeyValuePair<string, NetworkManager.NetworkChannelBase> networkChannel in this.m_NetworkChannels)
        networkChannel.Value.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理网络管理器。</summary>
    internal override void Shutdown()
    {
      foreach (KeyValuePair<string, NetworkManager.NetworkChannelBase> networkChannel in this.m_NetworkChannels)
      {
        NetworkManager.NetworkChannelBase networkChannelBase = networkChannel.Value;
        networkChannelBase.NetworkChannelConnected -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelConnected);
        networkChannelBase.NetworkChannelClosed -= new GameFrameworkAction<NetworkManager.NetworkChannelBase>(this.OnNetworkChannelClosed);
        networkChannelBase.NetworkChannelMissHeartBeat -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, int>(this.OnNetworkChannelMissHeartBeat);
        networkChannelBase.NetworkChannelError -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, NetworkErrorCode, SocketError, string>(this.OnNetworkChannelError);
        networkChannelBase.NetworkChannelCustomError -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelCustomError);
        networkChannelBase.Shutdown();
      }
      this.m_NetworkChannels.Clear();
    }

    /// <summary>检查是否存在网络频道。</summary>
    /// <param name="name">网络频道名称。</param>
    /// <returns>是否存在网络频道。</returns>
    public bool HasNetworkChannel(string name) => this.m_NetworkChannels.ContainsKey(name ?? string.Empty);

    /// <summary>获取网络频道。</summary>
    /// <param name="name">网络频道名称。</param>
    /// <returns>要获取的网络频道。</returns>
    public INetworkChannel GetNetworkChannel(string name)
    {
      NetworkManager.NetworkChannelBase networkChannelBase = (NetworkManager.NetworkChannelBase) null;
      return this.m_NetworkChannels.TryGetValue(name ?? string.Empty, out networkChannelBase) ? (INetworkChannel) networkChannelBase : (INetworkChannel) null;
    }

    /// <summary>获取所有网络频道。</summary>
    /// <returns>所有网络频道。</returns>
    public INetworkChannel[] GetAllNetworkChannels()
    {
      int num = 0;
      INetworkChannel[] allNetworkChannels = new INetworkChannel[this.m_NetworkChannels.Count];
      foreach (KeyValuePair<string, NetworkManager.NetworkChannelBase> networkChannel in this.m_NetworkChannels)
        allNetworkChannels[num++] = (INetworkChannel) networkChannel.Value;
      return allNetworkChannels;
    }

    /// <summary>获取所有网络频道。</summary>
    /// <param name="results">所有网络频道。</param>
    public void GetAllNetworkChannels(List<INetworkChannel> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, NetworkManager.NetworkChannelBase> networkChannel in this.m_NetworkChannels)
        results.Add((INetworkChannel) networkChannel.Value);
    }

    /// <summary>创建网络频道。</summary>
    /// <param name="name">网络频道名称。</param>
    /// <param name="serviceType">网络服务类型。</param>
    /// <param name="networkChannelHelper">网络频道辅助器。</param>
    /// <returns>要创建的网络频道。</returns>
    public INetworkChannel CreateNetworkChannel(
      string name,
      ServiceType serviceType,
      INetworkChannelHelper networkChannelHelper)
    {
      if (networkChannelHelper == null)
        throw new GameFrameworkException("Network channel helper is invalid.");
      if (networkChannelHelper.PacketHeaderLength < 0)
        throw new GameFrameworkException("Packet header length is invalid.");
      if (this.HasNetworkChannel(name))
        throw new GameFrameworkException(Utility.Text.Format<string>("Already exist network channel '{0}'.", name ?? string.Empty));
      NetworkManager.NetworkChannelBase networkChannel;
      if (serviceType != ServiceType.Tcp)
      {
        if (serviceType != ServiceType.TcpWithSyncReceive)
          throw new GameFrameworkException(Utility.Text.Format<ServiceType>("Not supported service type '{0}'.", serviceType));
        networkChannel = (NetworkManager.NetworkChannelBase) new NetworkManager.TcpWithSyncReceiveNetworkChannel(name, networkChannelHelper);
      }
      else
        networkChannel = (NetworkManager.NetworkChannelBase) new NetworkManager.TcpNetworkChannel(name, networkChannelHelper);
      networkChannel.NetworkChannelConnected += new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelConnected);
      networkChannel.NetworkChannelClosed += new GameFrameworkAction<NetworkManager.NetworkChannelBase>(this.OnNetworkChannelClosed);
      networkChannel.NetworkChannelMissHeartBeat += new GameFrameworkAction<NetworkManager.NetworkChannelBase, int>(this.OnNetworkChannelMissHeartBeat);
      networkChannel.NetworkChannelError += new GameFrameworkAction<NetworkManager.NetworkChannelBase, NetworkErrorCode, SocketError, string>(this.OnNetworkChannelError);
      networkChannel.NetworkChannelCustomError += new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelCustomError);
      this.m_NetworkChannels.Add(name, networkChannel);
      return (INetworkChannel) networkChannel;
    }

    /// <summary>销毁网络频道。</summary>
    /// <param name="name">网络频道名称。</param>
    /// <returns>是否销毁网络频道成功。</returns>
    public bool DestroyNetworkChannel(string name)
    {
      NetworkManager.NetworkChannelBase networkChannelBase = (NetworkManager.NetworkChannelBase) null;
      if (!this.m_NetworkChannels.TryGetValue(name ?? string.Empty, out networkChannelBase))
        return false;
      networkChannelBase.NetworkChannelConnected -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelConnected);
      networkChannelBase.NetworkChannelClosed -= new GameFrameworkAction<NetworkManager.NetworkChannelBase>(this.OnNetworkChannelClosed);
      networkChannelBase.NetworkChannelMissHeartBeat -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, int>(this.OnNetworkChannelMissHeartBeat);
      networkChannelBase.NetworkChannelError -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, NetworkErrorCode, SocketError, string>(this.OnNetworkChannelError);
      networkChannelBase.NetworkChannelCustomError -= new GameFrameworkAction<NetworkManager.NetworkChannelBase, object>(this.OnNetworkChannelCustomError);
      networkChannelBase.Shutdown();
      return this.m_NetworkChannels.Remove(name);
    }

    private void OnNetworkChannelConnected(
      NetworkManager.NetworkChannelBase networkChannel,
      object userData)
    {
      if (this.m_NetworkConnectedEventHandler == null)
        return;
      lock (this.m_NetworkConnectedEventHandler)
      {
        NetworkConnectedEventArgs e = NetworkConnectedEventArgs.Create((INetworkChannel) networkChannel, userData);
        this.m_NetworkConnectedEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void OnNetworkChannelClosed(NetworkManager.NetworkChannelBase networkChannel)
    {
      if (this.m_NetworkClosedEventHandler == null)
        return;
      lock (this.m_NetworkClosedEventHandler)
      {
        NetworkClosedEventArgs e = NetworkClosedEventArgs.Create((INetworkChannel) networkChannel);
        this.m_NetworkClosedEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void OnNetworkChannelMissHeartBeat(
      NetworkManager.NetworkChannelBase networkChannel,
      int missHeartBeatCount)
    {
      if (this.m_NetworkMissHeartBeatEventHandler == null)
        return;
      lock (this.m_NetworkMissHeartBeatEventHandler)
      {
        NetworkMissHeartBeatEventArgs e = NetworkMissHeartBeatEventArgs.Create((INetworkChannel) networkChannel, missHeartBeatCount);
        this.m_NetworkMissHeartBeatEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void OnNetworkChannelError(
      NetworkManager.NetworkChannelBase networkChannel,
      NetworkErrorCode errorCode,
      SocketError socketErrorCode,
      string errorMessage)
    {
      if (this.m_NetworkErrorEventHandler == null)
        return;
      lock (this.m_NetworkErrorEventHandler)
      {
        NetworkErrorEventArgs e = NetworkErrorEventArgs.Create((INetworkChannel) networkChannel, errorCode, socketErrorCode, errorMessage);
        this.m_NetworkErrorEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void OnNetworkChannelCustomError(
      NetworkManager.NetworkChannelBase networkChannel,
      object customErrorData)
    {
      if (this.m_NetworkCustomErrorEventHandler == null)
        return;
      lock (this.m_NetworkCustomErrorEventHandler)
      {
        NetworkCustomErrorEventArgs e = NetworkCustomErrorEventArgs.Create((INetworkChannel) networkChannel, customErrorData);
        this.m_NetworkCustomErrorEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private sealed class ConnectState
    {
      private readonly Socket m_Socket;
      private readonly object m_UserData;

      public ConnectState(Socket socket, object userData)
      {
        this.m_Socket = socket;
        this.m_UserData = userData;
      }

      public Socket Socket => this.m_Socket;

      public object UserData => this.m_UserData;
    }

    private sealed class HeartBeatState
    {
      private float m_HeartBeatElapseSeconds;
      private int m_MissHeartBeatCount;

      public HeartBeatState()
      {
        this.m_HeartBeatElapseSeconds = 0.0f;
        this.m_MissHeartBeatCount = 0;
      }

      public float HeartBeatElapseSeconds
      {
        get => this.m_HeartBeatElapseSeconds;
        set => this.m_HeartBeatElapseSeconds = value;
      }

      public int MissHeartBeatCount
      {
        get => this.m_MissHeartBeatCount;
        set => this.m_MissHeartBeatCount = value;
      }

      public void Reset(bool resetHeartBeatElapseSeconds)
      {
        if (resetHeartBeatElapseSeconds)
          this.m_HeartBeatElapseSeconds = 0.0f;
        this.m_MissHeartBeatCount = 0;
      }
    }

    /// <summary>网络频道基类。</summary>
    private abstract class NetworkChannelBase : INetworkChannel, IDisposable
    {
      private const float DefaultHeartBeatInterval = 30f;
      private readonly string m_Name;
      protected readonly Queue<Packet> m_SendPacketPool;
      protected readonly EventPool<Packet> m_ReceivePacketPool;
      protected readonly INetworkChannelHelper m_NetworkChannelHelper;
      protected AddressFamily m_AddressFamily;
      protected bool m_ResetHeartBeatElapseSecondsWhenReceivePacket;
      protected float m_HeartBeatInterval;
      protected Socket m_Socket;
      protected readonly NetworkManager.SendState m_SendState;
      protected readonly NetworkManager.ReceiveState m_ReceiveState;
      protected readonly NetworkManager.HeartBeatState m_HeartBeatState;
      protected int m_SentPacketCount;
      protected int m_ReceivedPacketCount;
      protected bool m_Active;
      private bool m_Disposed;
      public GameFrameworkAction<NetworkManager.NetworkChannelBase, object> NetworkChannelConnected;
      public GameFrameworkAction<NetworkManager.NetworkChannelBase> NetworkChannelClosed;
      public GameFrameworkAction<NetworkManager.NetworkChannelBase, int> NetworkChannelMissHeartBeat;
      public GameFrameworkAction<NetworkManager.NetworkChannelBase, NetworkErrorCode, SocketError, string> NetworkChannelError;
      public GameFrameworkAction<NetworkManager.NetworkChannelBase, object> NetworkChannelCustomError;

      /// <summary>初始化网络频道基类的新实例。</summary>
      /// <param name="name">网络频道名称。</param>
      /// <param name="networkChannelHelper">网络频道辅助器。</param>
      public NetworkChannelBase(string name, INetworkChannelHelper networkChannelHelper)
      {
        this.m_Name = name ?? string.Empty;
        this.m_SendPacketPool = new Queue<Packet>();
        this.m_ReceivePacketPool = new EventPool<Packet>(EventPoolMode.Default);
        this.m_NetworkChannelHelper = networkChannelHelper;
        this.m_AddressFamily = AddressFamily.Unknown;
        this.m_ResetHeartBeatElapseSecondsWhenReceivePacket = false;
        this.m_HeartBeatInterval = 30f;
        this.m_Socket = (Socket) null;
        this.m_SendState = new NetworkManager.SendState();
        this.m_ReceiveState = new NetworkManager.ReceiveState();
        this.m_HeartBeatState = new NetworkManager.HeartBeatState();
        this.m_SentPacketCount = 0;
        this.m_ReceivedPacketCount = 0;
        this.m_Active = false;
        this.m_Disposed = false;
        this.NetworkChannelConnected = (GameFrameworkAction<NetworkManager.NetworkChannelBase, object>) null;
        this.NetworkChannelClosed = (GameFrameworkAction<NetworkManager.NetworkChannelBase>) null;
        this.NetworkChannelMissHeartBeat = (GameFrameworkAction<NetworkManager.NetworkChannelBase, int>) null;
        this.NetworkChannelError = (GameFrameworkAction<NetworkManager.NetworkChannelBase, NetworkErrorCode, SocketError, string>) null;
        this.NetworkChannelCustomError = (GameFrameworkAction<NetworkManager.NetworkChannelBase, object>) null;
        networkChannelHelper.Initialize((INetworkChannel) this);
      }

      /// <summary>获取网络频道名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取网络频道所使用的 Socket。</summary>
      public Socket Socket => this.m_Socket;

      /// <summary>获取是否已连接。</summary>
      public bool Connected => this.m_Socket != null && this.m_Socket.Connected;

      /// <summary>获取网络服务类型。</summary>
      public abstract ServiceType ServiceType { get; }

      /// <summary>获取网络地址类型。</summary>
      public AddressFamily AddressFamily => this.m_AddressFamily;

      /// <summary>获取要发送的消息包数量。</summary>
      public int SendPacketCount => this.m_SendPacketPool.Count;

      /// <summary>获取累计发送的消息包数量。</summary>
      public int SentPacketCount => this.m_SentPacketCount;

      /// <summary>获取已接收未处理的消息包数量。</summary>
      public int ReceivePacketCount => this.m_ReceivePacketPool.EventCount;

      /// <summary>获取累计已接收的消息包数量。</summary>
      public int ReceivedPacketCount => this.m_ReceivedPacketCount;

      /// <summary>获取或设置当收到消息包时是否重置心跳流逝时间。</summary>
      public bool ResetHeartBeatElapseSecondsWhenReceivePacket
      {
        get => this.m_ResetHeartBeatElapseSecondsWhenReceivePacket;
        set => this.m_ResetHeartBeatElapseSecondsWhenReceivePacket = value;
      }

      /// <summary>获取丢失心跳的次数。</summary>
      public int MissHeartBeatCount => this.m_HeartBeatState.MissHeartBeatCount;

      /// <summary>获取或设置心跳间隔时长，以秒为单位。</summary>
      public float HeartBeatInterval
      {
        get => this.m_HeartBeatInterval;
        set => this.m_HeartBeatInterval = value;
      }

      /// <summary>获取心跳等待时长，以秒为单位。</summary>
      public float HeartBeatElapseSeconds => this.m_HeartBeatState.HeartBeatElapseSeconds;

      /// <summary>网络频道轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public virtual void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (this.m_Socket == null || !this.m_Active)
          return;
        this.ProcessSend();
        this.ProcessReceive();
        if (this.m_Socket == null || !this.m_Active)
          return;
        this.m_ReceivePacketPool.Update(elapseSeconds, realElapseSeconds);
        if ((double) this.m_HeartBeatInterval <= 0.0)
          return;
        bool flag = false;
        int num = 0;
        lock (this.m_HeartBeatState)
        {
          if (this.m_Socket == null || !this.m_Active)
            return;
          this.m_HeartBeatState.HeartBeatElapseSeconds += realElapseSeconds;
          if ((double) this.m_HeartBeatState.HeartBeatElapseSeconds >= (double) this.m_HeartBeatInterval)
          {
            flag = true;
            num = this.m_HeartBeatState.MissHeartBeatCount;
            this.m_HeartBeatState.HeartBeatElapseSeconds = 0.0f;
            ++this.m_HeartBeatState.MissHeartBeatCount;
          }
        }
        if (!flag || !this.m_NetworkChannelHelper.SendHeartBeat() || num <= 0 || this.NetworkChannelMissHeartBeat == null)
          return;
        this.NetworkChannelMissHeartBeat(this, num);
      }

      /// <summary>关闭网络频道。</summary>
      public virtual void Shutdown()
      {
        this.Close();
        this.m_ReceivePacketPool.Shutdown();
        this.m_NetworkChannelHelper.Shutdown();
      }

      /// <summary>注册网络消息包处理函数。</summary>
      /// <param name="handler">要注册的网络消息包处理函数。</param>
      public void RegisterHandler(IPacketHandler handler)
      {
        if (handler == null)
          throw new GameFrameworkException("Packet handler is invalid.");
        this.m_ReceivePacketPool.Subscribe(handler.Id, new EventHandler<Packet>(handler.Handle));
      }

      /// <summary>设置默认事件处理函数。</summary>
      /// <param name="handler">要设置的默认事件处理函数。</param>
      public void SetDefaultHandler(EventHandler<Packet> handler) => this.m_ReceivePacketPool.SetDefaultHandler(handler);

      /// <summary>连接到远程主机。</summary>
      /// <param name="ipAddress">远程主机的 IP 地址。</param>
      /// <param name="port">远程主机的端口号。</param>
      public void Connect(IPAddress ipAddress, int port) => this.Connect(ipAddress, port, (object) null);

      /// <summary>连接到远程主机。</summary>
      /// <param name="ipAddress">远程主机的 IP 地址。</param>
      /// <param name="port">远程主机的端口号。</param>
      /// <param name="userData">用户自定义数据。</param>
      public virtual void Connect(IPAddress ipAddress, int port, object userData)
      {
        if (this.m_Socket != null)
        {
          this.Close();
          this.m_Socket = (Socket) null;
        }
        switch (ipAddress.AddressFamily)
        {
          case System.Net.Sockets.AddressFamily.InterNetwork:
            this.m_AddressFamily = AddressFamily.IPv4;
            break;
          case System.Net.Sockets.AddressFamily.InterNetworkV6:
            this.m_AddressFamily = AddressFamily.IPv6;
            break;
          default:
            string message = Utility.Text.Format<System.Net.Sockets.AddressFamily>("Not supported address family '{0}'.", ipAddress.AddressFamily);
            if (this.NetworkChannelError == null)
              throw new GameFrameworkException(message);
            this.NetworkChannelError(this, NetworkErrorCode.AddressFamilyError, SocketError.Success, message);
            return;
        }
        this.m_SendState.Reset();
        this.m_ReceiveState.PrepareForPacketHeader(this.m_NetworkChannelHelper.PacketHeaderLength);
      }

      /// <summary>关闭连接并释放所有相关资源。</summary>
      public void Close()
      {
        lock (this)
        {
          if (this.m_Socket == null)
            return;
          this.m_Active = false;
          try
          {
            this.m_Socket.Shutdown(SocketShutdown.Both);
          }
          catch
          {
          }
          finally
          {
            this.m_Socket.Close();
            this.m_Socket = (Socket) null;
            if (this.NetworkChannelClosed != null)
              this.NetworkChannelClosed(this);
          }
          this.m_SentPacketCount = 0;
          this.m_ReceivedPacketCount = 0;
          lock (this.m_SendPacketPool)
            this.m_SendPacketPool.Clear();
          this.m_ReceivePacketPool.Clear();
          lock (this.m_HeartBeatState)
            this.m_HeartBeatState.Reset(true);
        }
      }

      /// <summary>向远程主机发送消息包。</summary>
      /// <typeparam name="T">消息包类型。</typeparam>
      /// <param name="packet">要发送的消息包。</param>
      public void Send<T>(T packet) where T : Packet
      {
        if (this.m_Socket == null)
        {
          string message = "You must connect first.";
          if (this.NetworkChannelError == null)
            throw new GameFrameworkException(message);
          this.NetworkChannelError(this, NetworkErrorCode.SendError, SocketError.Success, message);
        }
        else if (!this.m_Active)
        {
          string message = "Socket is not active.";
          if (this.NetworkChannelError == null)
            throw new GameFrameworkException(message);
          this.NetworkChannelError(this, NetworkErrorCode.SendError, SocketError.Success, message);
        }
        else if ((object) packet == null)
        {
          string message = "Packet is invalid.";
          if (this.NetworkChannelError == null)
            throw new GameFrameworkException(message);
          this.NetworkChannelError(this, NetworkErrorCode.SendError, SocketError.Success, message);
        }
        else
        {
          lock (this.m_SendPacketPool)
            this.m_SendPacketPool.Enqueue((Packet) packet);
        }
      }

      /// <summary>释放资源。</summary>
      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      /// <summary>释放资源。</summary>
      /// <param name="disposing">释放资源标记。</param>
      private void Dispose(bool disposing)
      {
        if (this.m_Disposed)
          return;
        if (disposing)
        {
          this.Close();
          this.m_SendState.Dispose();
          this.m_ReceiveState.Dispose();
        }
        this.m_Disposed = true;
      }

      protected virtual bool ProcessSend()
      {
        if (this.m_SendState.Stream.Length > 0L || this.m_SendPacketPool.Count <= 0)
          return false;
        while (this.m_SendPacketPool.Count > 0)
        {
          Packet packet = (Packet) null;
          lock (this.m_SendPacketPool)
            packet = this.m_SendPacketPool.Dequeue();
          bool flag;
          try
          {
            flag = this.m_NetworkChannelHelper.Serialize<Packet>(packet, (Stream) this.m_SendState.Stream);
          }
          catch (Exception ex)
          {
            this.m_Active = false;
            if (this.NetworkChannelError != null)
            {
              SocketException socketException = ex as SocketException;
              this.NetworkChannelError(this, NetworkErrorCode.SerializeError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
              return false;
            }
            throw;
          }
          if (!flag)
          {
            string message = "Serialized packet failure.";
            if (this.NetworkChannelError == null)
              throw new GameFrameworkException(message);
            this.NetworkChannelError(this, NetworkErrorCode.SerializeError, SocketError.Success, message);
            return false;
          }
        }
        this.m_SendState.Stream.Position = 0L;
        return true;
      }

      protected virtual void ProcessReceive()
      {
      }

      protected virtual bool ProcessPacketHeader()
      {
        try
        {
          object customErrorData = (object) null;
          IPacketHeader packetHeader = this.m_NetworkChannelHelper.DeserializePacketHeader((Stream) this.m_ReceiveState.Stream, out customErrorData);
          if (customErrorData != null && this.NetworkChannelCustomError != null)
            this.NetworkChannelCustomError(this, customErrorData);
          if (packetHeader == null)
          {
            string message = "Packet header is invalid.";
            if (this.NetworkChannelError == null)
              throw new GameFrameworkException(message);
            this.NetworkChannelError(this, NetworkErrorCode.DeserializePacketHeaderError, SocketError.Success, message);
            return false;
          }
          this.m_ReceiveState.PrepareForPacket(packetHeader);
          if (packetHeader.PacketLength <= 0)
          {
            int num = this.ProcessPacket() ? 1 : 0;
            ++this.m_ReceivedPacketCount;
            return num != 0;
          }
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError(this, NetworkErrorCode.DeserializePacketHeaderError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return false;
          }
          throw;
        }
        return true;
      }

      protected virtual bool ProcessPacket()
      {
        lock (this.m_HeartBeatState)
          this.m_HeartBeatState.Reset(this.m_ResetHeartBeatElapseSecondsWhenReceivePacket);
        try
        {
          object customErrorData = (object) null;
          Packet e = this.m_NetworkChannelHelper.DeserializePacket(this.m_ReceiveState.PacketHeader, (Stream) this.m_ReceiveState.Stream, out customErrorData);
          if (customErrorData != null && this.NetworkChannelCustomError != null)
            this.NetworkChannelCustomError(this, customErrorData);
          if (e != null)
            this.m_ReceivePacketPool.Fire((object) this, e);
          this.m_ReceiveState.PrepareForPacketHeader(this.m_NetworkChannelHelper.PacketHeaderLength);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError(this, NetworkErrorCode.DeserializePacketError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return false;
          }
          throw;
        }
        return true;
      }
    }

    private sealed class ReceiveState : IDisposable
    {
      private const int DefaultBufferLength = 65536;
      private MemoryStream m_Stream;
      private IPacketHeader m_PacketHeader;
      private bool m_Disposed;

      public ReceiveState()
      {
        this.m_Stream = new MemoryStream(65536);
        this.m_PacketHeader = (IPacketHeader) null;
        this.m_Disposed = false;
      }

      public MemoryStream Stream => this.m_Stream;

      public IPacketHeader PacketHeader => this.m_PacketHeader;

      public void PrepareForPacketHeader(int packetHeaderLength) => this.Reset(packetHeaderLength, (IPacketHeader) null);

      public void PrepareForPacket(IPacketHeader packetHeader)
      {
        if (packetHeader == null)
          throw new GameFrameworkException("Packet header is invalid.");
        this.Reset(packetHeader.PacketLength, packetHeader);
      }

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      private void Dispose(bool disposing)
      {
        if (this.m_Disposed)
          return;
        if (disposing && this.m_Stream != null)
        {
          this.m_Stream.Dispose();
          this.m_Stream = (MemoryStream) null;
        }
        this.m_Disposed = true;
      }

      private void Reset(int targetLength, IPacketHeader packetHeader)
      {
        if (targetLength < 0)
          throw new GameFrameworkException("Target length is invalid.");
        this.m_Stream.Position = 0L;
        this.m_Stream.SetLength((long) targetLength);
        this.m_PacketHeader = packetHeader;
      }
    }

    private sealed class SendState : IDisposable
    {
      private const int DefaultBufferLength = 65536;
      private MemoryStream m_Stream;
      private bool m_Disposed;

      public SendState()
      {
        this.m_Stream = new MemoryStream(65536);
        this.m_Disposed = false;
      }

      public MemoryStream Stream => this.m_Stream;

      public void Reset()
      {
        this.m_Stream.Position = 0L;
        this.m_Stream.SetLength(0L);
      }

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      private void Dispose(bool disposing)
      {
        if (this.m_Disposed)
          return;
        if (disposing && this.m_Stream != null)
        {
          this.m_Stream.Dispose();
          this.m_Stream = (MemoryStream) null;
        }
        this.m_Disposed = true;
      }
    }

    /// <summary>TCP 网络频道。</summary>
    private sealed class TcpNetworkChannel : NetworkManager.NetworkChannelBase
    {
      private readonly AsyncCallback m_ConnectCallback;
      private readonly AsyncCallback m_SendCallback;
      private readonly AsyncCallback m_ReceiveCallback;

      /// <summary>初始化网络频道的新实例。</summary>
      /// <param name="name">网络频道名称。</param>
      /// <param name="networkChannelHelper">网络频道辅助器。</param>
      public TcpNetworkChannel(string name, INetworkChannelHelper networkChannelHelper)
        : base(name, networkChannelHelper)
      {
        this.m_ConnectCallback = new AsyncCallback(this.ConnectCallback);
        this.m_SendCallback = new AsyncCallback(this.SendCallback);
        this.m_ReceiveCallback = new AsyncCallback(this.ReceiveCallback);
      }

      /// <summary>获取网络服务类型。</summary>
      public override ServiceType ServiceType => ServiceType.Tcp;

      /// <summary>连接到远程主机。</summary>
      /// <param name="ipAddress">远程主机的 IP 地址。</param>
      /// <param name="port">远程主机的端口号。</param>
      /// <param name="userData">用户自定义数据。</param>
      public override void Connect(IPAddress ipAddress, int port, object userData)
      {
        base.Connect(ipAddress, port, userData);
        this.m_Socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        if (this.m_Socket == null)
        {
          string message = "Initialize network channel failure.";
          if (this.NetworkChannelError == null)
            throw new GameFrameworkException(message);
          this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SocketError, SocketError.Success, message);
        }
        else
        {
          this.m_NetworkChannelHelper.PrepareForConnecting();
          this.ConnectAsync(ipAddress, port, userData);
        }
      }

      protected override bool ProcessSend()
      {
        if (!base.ProcessSend())
          return false;
        this.SendAsync();
        return true;
      }

      private void ConnectAsync(IPAddress ipAddress, int port, object userData)
      {
        try
        {
          this.m_Socket.BeginConnect(ipAddress, port, this.m_ConnectCallback, (object) new NetworkManager.ConnectState(this.m_Socket, userData));
        }
        catch (Exception ex)
        {
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ConnectError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
          }
          else
            throw;
        }
      }

      private void ConnectCallback(IAsyncResult ar)
      {
        NetworkManager.ConnectState asyncState = (NetworkManager.ConnectState) ar.AsyncState;
        try
        {
          asyncState.Socket.EndConnect(ar);
        }
        catch (ObjectDisposedException ex)
        {
          return;
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ConnectError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return;
          }
          throw;
        }
        this.m_SentPacketCount = 0;
        this.m_ReceivedPacketCount = 0;
        lock (this.m_SendPacketPool)
          this.m_SendPacketPool.Clear();
        this.m_ReceivePacketPool.Clear();
        lock (this.m_HeartBeatState)
          this.m_HeartBeatState.Reset(true);
        if (this.NetworkChannelConnected != null)
          this.NetworkChannelConnected((NetworkManager.NetworkChannelBase) this, asyncState.UserData);
        this.m_Active = true;
        this.ReceiveAsync();
      }

      private void SendAsync()
      {
        try
        {
          this.m_Socket.BeginSend(this.m_SendState.Stream.GetBuffer(), (int) this.m_SendState.Stream.Position, (int) (this.m_SendState.Stream.Length - this.m_SendState.Stream.Position), SocketFlags.None, this.m_SendCallback, (object) this.m_Socket);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
          }
          else
            throw;
        }
      }

      private void SendCallback(IAsyncResult ar)
      {
        Socket asyncState = (Socket) ar.AsyncState;
        if (!asyncState.Connected)
          return;
        int num;
        try
        {
          num = asyncState.EndSend(ar);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return;
          }
          throw;
        }
        this.m_SendState.Stream.Position += (long) num;
        if (this.m_SendState.Stream.Position < this.m_SendState.Stream.Length)
        {
          this.SendAsync();
        }
        else
        {
          ++this.m_SentPacketCount;
          this.m_SendState.Reset();
        }
      }

      private void ReceiveAsync()
      {
        try
        {
          this.m_Socket.BeginReceive(this.m_ReceiveState.Stream.GetBuffer(), (int) this.m_ReceiveState.Stream.Position, (int) (this.m_ReceiveState.Stream.Length - this.m_ReceiveState.Stream.Position), SocketFlags.None, this.m_ReceiveCallback, (object) this.m_Socket);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ReceiveError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
          }
          else
            throw;
        }
      }

      private void ReceiveCallback(IAsyncResult ar)
      {
        Socket asyncState = (Socket) ar.AsyncState;
        if (!asyncState.Connected)
          return;
        int num;
        try
        {
          num = asyncState.EndReceive(ar);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ReceiveError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return;
          }
          throw;
        }
        if (num <= 0)
        {
          this.Close();
        }
        else
        {
          this.m_ReceiveState.Stream.Position += (long) num;
          if (this.m_ReceiveState.Stream.Position < this.m_ReceiveState.Stream.Length)
          {
            this.ReceiveAsync();
          }
          else
          {
            this.m_ReceiveState.Stream.Position = 0L;
            bool flag;
            if (this.m_ReceiveState.PacketHeader != null)
            {
              flag = this.ProcessPacket();
              ++this.m_ReceivedPacketCount;
            }
            else
              flag = this.ProcessPacketHeader();
            if (!flag)
              return;
            this.ReceiveAsync();
          }
        }
      }
    }

    /// <summary>使用同步接收的 TCP 网络频道。</summary>
    private sealed class TcpWithSyncReceiveNetworkChannel : NetworkManager.NetworkChannelBase
    {
      private readonly AsyncCallback m_ConnectCallback;
      private readonly AsyncCallback m_SendCallback;

      /// <summary>初始化网络频道的新实例。</summary>
      /// <param name="name">网络频道名称。</param>
      /// <param name="networkChannelHelper">网络频道辅助器。</param>
      public TcpWithSyncReceiveNetworkChannel(
        string name,
        INetworkChannelHelper networkChannelHelper)
        : base(name, networkChannelHelper)
      {
        this.m_ConnectCallback = new AsyncCallback(this.ConnectCallback);
        this.m_SendCallback = new AsyncCallback(this.SendCallback);
      }

      /// <summary>获取网络服务类型。</summary>
      public override ServiceType ServiceType => ServiceType.TcpWithSyncReceive;

      /// <summary>连接到远程主机。</summary>
      /// <param name="ipAddress">远程主机的 IP 地址。</param>
      /// <param name="port">远程主机的端口号。</param>
      /// <param name="userData">用户自定义数据。</param>
      public override void Connect(IPAddress ipAddress, int port, object userData)
      {
        base.Connect(ipAddress, port, userData);
        this.m_Socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        if (this.m_Socket == null)
        {
          string message = "Initialize network channel failure.";
          if (this.NetworkChannelError == null)
            throw new GameFrameworkException(message);
          this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SocketError, SocketError.Success, message);
        }
        else
        {
          this.m_NetworkChannelHelper.PrepareForConnecting();
          this.ConnectAsync(ipAddress, port, userData);
        }
      }

      protected override bool ProcessSend()
      {
        if (!base.ProcessSend())
          return false;
        this.SendAsync();
        return true;
      }

      protected override void ProcessReceive()
      {
        base.ProcessReceive();
        do
          ;
        while (this.m_Socket.Available > 0 && this.ReceiveSync());
      }

      private void ConnectAsync(IPAddress ipAddress, int port, object userData)
      {
        try
        {
          this.m_Socket.BeginConnect(ipAddress, port, this.m_ConnectCallback, (object) new NetworkManager.ConnectState(this.m_Socket, userData));
        }
        catch (Exception ex)
        {
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ConnectError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
          }
          else
            throw;
        }
      }

      private void ConnectCallback(IAsyncResult ar)
      {
        NetworkManager.ConnectState asyncState = (NetworkManager.ConnectState) ar.AsyncState;
        try
        {
          asyncState.Socket.EndConnect(ar);
        }
        catch (ObjectDisposedException ex)
        {
          return;
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ConnectError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return;
          }
          throw;
        }
        this.m_SentPacketCount = 0;
        this.m_ReceivedPacketCount = 0;
        lock (this.m_SendPacketPool)
          this.m_SendPacketPool.Clear();
        this.m_ReceivePacketPool.Clear();
        lock (this.m_HeartBeatState)
          this.m_HeartBeatState.Reset(true);
        if (this.NetworkChannelConnected != null)
          this.NetworkChannelConnected((NetworkManager.NetworkChannelBase) this, asyncState.UserData);
        this.m_Active = true;
      }

      private void SendAsync()
      {
        try
        {
          this.m_Socket.BeginSend(this.m_SendState.Stream.GetBuffer(), (int) this.m_SendState.Stream.Position, (int) (this.m_SendState.Stream.Length - this.m_SendState.Stream.Position), SocketFlags.None, this.m_SendCallback, (object) this.m_Socket);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
          }
          else
            throw;
        }
      }

      private void SendCallback(IAsyncResult ar)
      {
        Socket asyncState = (Socket) ar.AsyncState;
        if (!asyncState.Connected)
          return;
        int num;
        try
        {
          num = asyncState.EndSend(ar);
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.SendError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return;
          }
          throw;
        }
        this.m_SendState.Stream.Position += (long) num;
        if (this.m_SendState.Stream.Position < this.m_SendState.Stream.Length)
        {
          this.SendAsync();
        }
        else
        {
          ++this.m_SentPacketCount;
          this.m_SendState.Reset();
        }
      }

      private bool ReceiveSync()
      {
        try
        {
          int num = this.m_Socket.Receive(this.m_ReceiveState.Stream.GetBuffer(), (int) this.m_ReceiveState.Stream.Position, (int) (this.m_ReceiveState.Stream.Length - this.m_ReceiveState.Stream.Position), SocketFlags.None);
          if (num <= 0)
          {
            this.Close();
            return false;
          }
          this.m_ReceiveState.Stream.Position += (long) num;
          if (this.m_ReceiveState.Stream.Position < this.m_ReceiveState.Stream.Length)
            return false;
          this.m_ReceiveState.Stream.Position = 0L;
          bool sync;
          if (this.m_ReceiveState.PacketHeader != null)
          {
            sync = this.ProcessPacket();
            ++this.m_ReceivedPacketCount;
          }
          else
            sync = this.ProcessPacketHeader();
          return sync;
        }
        catch (Exception ex)
        {
          this.m_Active = false;
          if (this.NetworkChannelError != null)
          {
            SocketException socketException = ex as SocketException;
            this.NetworkChannelError((NetworkManager.NetworkChannelBase) this, NetworkErrorCode.ReceiveError, socketException != null ? socketException.SocketErrorCode : SocketError.Success, ex.ToString());
            return false;
          }
          throw;
        }
      }
    }
  }
}
