//@LeeTools
//------------------------
//Filename：SCPacketBase.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:37:34
//Function：Nothing
//------------------------

namespace Game
{
    public abstract class SCPacketBase : PacketBase
    {
        public override PacketType PacketType => PacketType.ServerToClient;
    }
}