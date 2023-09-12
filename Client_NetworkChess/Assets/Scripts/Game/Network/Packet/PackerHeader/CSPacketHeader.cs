//@LeeTools
//------------------------
//Filename：CSPacketHeader.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 20:20:49
//Function：Nothing
//------------------------
namespace Game
{
    public class CSPacketHeader : PacketHeaderBase
    {
        public override PacketType PacketType => PacketType.ClientToServer;
    }
}