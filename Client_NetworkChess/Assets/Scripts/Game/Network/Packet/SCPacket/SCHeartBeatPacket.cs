//@LeeTools
//------------------------
//Filename：SCHeartBeatPacket.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 17:51:23
//Function：Nothing
//------------------------

using Google.Protobuf;
using Protocol;

namespace Game
{
    public class SCHeartBeatPacket : SCPacketBase
    {
        public override int Id => (int)PacketId.SCHeartBeat;

        public override IMessage Message => throw new System.NotImplementedException();

        public long ServerTime => (Message as SCHeartBeat).ServerTime;

        public override void Clear()
        {
        }
    }
}