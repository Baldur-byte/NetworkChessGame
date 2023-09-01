// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.IPacketHandler
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络消息包处理器接口。</summary>
  public interface IPacketHandler
  {
    /// <summary>获取网络消息包协议编号。</summary>
    int Id { get; }

    /// <summary>网络消息包处理函数。</summary>
    /// <param name="sender">网络消息包源。</param>
    /// <param name="packet">网络消息包内容。</param>
    void Handle(object sender, Packet packet);
  }
}
