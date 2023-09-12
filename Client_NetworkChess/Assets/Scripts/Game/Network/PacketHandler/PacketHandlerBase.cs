//@LeeTools
//------------------------
//Filename：PacketHandlerBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:40:04
//Function：Nothing
//------------------------
using GameFramework.Network;

namespace Game
{
    public abstract class PacketHandlerBase : IPacketHandler
    {
        public abstract int Id
        {
            get;
        }

        public abstract void Handle(object sender, Packet packet);
    }
}