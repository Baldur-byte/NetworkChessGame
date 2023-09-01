// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.ServiceType
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络服务类型。</summary>
  public enum ServiceType : byte
  {
    /// <summary>TCP 网络服务。</summary>
    Tcp,
    /// <summary>使用同步接收的 TCP 网络服务。</summary>
    TcpWithSyncReceive,
  }
}
