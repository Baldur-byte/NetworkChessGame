// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.IPacketHeader
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络消息包头接口。</summary>
  public interface IPacketHeader
  {
    /// <summary>获取网络消息包长度。</summary>
    int PacketLength { get; }
  }
}
