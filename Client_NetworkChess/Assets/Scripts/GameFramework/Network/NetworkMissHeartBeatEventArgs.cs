// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkMissHeartBeatEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络心跳包丢失事件。</summary>
  public sealed class NetworkMissHeartBeatEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化网络心跳包丢失事件的新实例。</summary>
    public NetworkMissHeartBeatEventArgs()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.MissCount = 0;
    }

    /// <summary>获取网络频道。</summary>
    public INetworkChannel NetworkChannel { get; private set; }

    /// <summary>获取心跳包已丢失次数。</summary>
    public int MissCount { get; private set; }

    /// <summary>创建网络心跳包丢失事件。</summary>
    /// <param name="networkChannel">网络频道。</param>
    /// <param name="missCount">心跳包已丢失次数。</param>
    /// <returns>创建的网络心跳包丢失事件。</returns>
    public static NetworkMissHeartBeatEventArgs Create(
      INetworkChannel networkChannel,
      int missCount)
    {
      NetworkMissHeartBeatEventArgs heartBeatEventArgs = ReferencePool.Acquire<NetworkMissHeartBeatEventArgs>();
      heartBeatEventArgs.NetworkChannel = networkChannel;
      heartBeatEventArgs.MissCount = missCount;
      return heartBeatEventArgs;
    }

    /// <summary>清理网络心跳包丢失事件。</summary>
    public override void Clear()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.MissCount = 0;
    }
  }
}
