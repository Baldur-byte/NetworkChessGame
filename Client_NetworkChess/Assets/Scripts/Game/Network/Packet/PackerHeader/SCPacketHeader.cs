//@LeeTools
//------------------------
//Filename：SCPacketHeader.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 20:25:48
//Function：Nothing
//------------------------
namespace Game
{
    public class SCPacketHeader : PacketHeaderBase
    {
        public override PacketType PacketType => PacketType.ServerToClient;
    }
}