//@LeeTools
//------------------------
//Filename：PacketHeaderBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 20:10:17
//Function：Nothing
//------------------------
using GameFramework;
using GameFramework.Network;

namespace Game
{
    public abstract class PacketHeaderBase : IPacketHeader, IReference
    {
        public abstract PacketType PacketType { get; }

        public int Id { get; set; }

        public int PacketLength { get; set; }

        public bool IsValid
        {
            get
            {
                return PacketType != PacketType.Undefined && Id > 0 && PacketLength > 0;
            }
        }

        public void Clear()
        {
            Id = 0;
            PacketLength = 0;
        }
    }
}