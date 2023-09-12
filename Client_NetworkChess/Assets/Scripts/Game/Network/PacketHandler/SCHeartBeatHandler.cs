//@LeeTools
//------------------------
//Filename：SCHeartBeatHandler.cs
//Auther：auus
//Device：DESKTOP-DFRI604
//Email：346679447@qq.com
//CreateDate：2023/09/12 16:41:57
//Function：Nothing
//------------------------
using GameFramework.Network;
using Protocol;
using UnityGameFramework.Runtime;

namespace Game
{
    public class SCHeartBeatHandler : PacketHandlerBase
    {
        public override int Id => (int)PacketId.SCHeartBeat;

        public override void Handle(object sender, Packet packet)
        {
            SCHeartBeatPacket packetImpl = (SCHeartBeatPacket)packet;
            Log.Info("Receive packet '{0}', ServerTime:{1}", packetImpl.Id.ToString(), packetImpl.ServerTime);
        }
    }
}