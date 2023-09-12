//@LeeTools
//------------------------
//Filename：PacketBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:32:16
//Function：Nothing
//------------------------
using GameFramework.Network;
using Google.Protobuf;
using System.Net.Sockets;

namespace Game
{
    public abstract class PacketBase : Packet
    {
        public abstract PacketType PacketType
        {
            get;
        }

        public abstract IMessage Message
        {
            get;
        }

        public abstract byte[] Bytes
        {
            get;
        }
    }
}