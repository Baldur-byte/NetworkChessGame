// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkClosedEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络连接关闭事件。</summary>
  public sealed class NetworkClosedEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化网络连接关闭事件的新实例。</summary>
    public NetworkClosedEventArgs() => this.NetworkChannel = (INetworkChannel) null;

    /// <summary>获取网络频道。</summary>
    public INetworkChannel NetworkChannel { get; private set; }

    /// <summary>创建网络连接关闭事件。</summary>
    /// <param name="networkChannel">网络频道。</param>
    /// <returns>创建的网络连接关闭事件。</returns>
    public static NetworkClosedEventArgs Create(INetworkChannel networkChannel)
    {
      NetworkClosedEventArgs networkClosedEventArgs = ReferencePool.Acquire<NetworkClosedEventArgs>();
      networkClosedEventArgs.NetworkChannel = networkChannel;
      return networkClosedEventArgs;
    }

    /// <summary>清理网络连接关闭事件。</summary>
    public override void Clear() => this.NetworkChannel = (INetworkChannel) null;
  }
}
