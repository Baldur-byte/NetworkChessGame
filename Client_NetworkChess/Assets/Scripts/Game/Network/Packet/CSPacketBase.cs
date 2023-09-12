//@LeeTools
//------------------------
//Filename：CSPacketBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:29:20
//Function：Nothing
//------------------------

namespace Game
{
    public abstract class CSPacketBase : PacketBase
    {
        public override PacketType PacketType => PacketType.ClientToServer;
    }
}