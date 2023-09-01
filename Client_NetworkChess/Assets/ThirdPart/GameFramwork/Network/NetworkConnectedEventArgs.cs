// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkConnectedEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>网络连接成功事件。</summary>
  public sealed class NetworkConnectedEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化网络连接成功事件的新实例。</summary>
    public NetworkConnectedEventArgs()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.UserData = (object) null;
    }

    /// <summary>获取网络频道。</summary>
    public INetworkChannel NetworkChannel { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建网络连接成功事件。</summary>
    /// <param name="networkChannel">网络频道。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的网络连接成功事件。</returns>
    public static NetworkConnectedEventArgs Create(INetworkChannel networkChannel, object userData)
    {
      NetworkConnectedEventArgs connectedEventArgs = ReferencePool.Acquire<NetworkConnectedEventArgs>();
      connectedEventArgs.NetworkChannel = networkChannel;
      connectedEventArgs.UserData = userData;
      return connectedEventArgs;
    }

    /// <summary>清理网络连接成功事件。</summary>
    public override void Clear()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.UserData = (object) null;
    }
  }
}
