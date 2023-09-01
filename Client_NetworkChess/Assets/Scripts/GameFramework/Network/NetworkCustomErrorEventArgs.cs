// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkCustomErrorEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Network
{
  /// <summary>用户自定义网络错误事件。</summary>
  public sealed class NetworkCustomErrorEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化用户自定义网络错误事件的新实例。</summary>
    public NetworkCustomErrorEventArgs()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.CustomErrorData = (object) null;
    }

    /// <summary>获取网络频道。</summary>
    public INetworkChannel NetworkChannel { get; private set; }

    /// <summary>获取用户自定义错误数据。</summary>
    public object CustomErrorData { get; private set; }

    /// <summary>创建用户自定义网络错误事件。</summary>
    /// <param name="networkChannel">网络频道。</param>
    /// <param name="customErrorData">用户自定义错误数据。</param>
    /// <returns>创建的用户自定义网络错误事件。</returns>
    public static NetworkCustomErrorEventArgs Create(
      INetworkChannel networkChannel,
      object customErrorData)
    {
      NetworkCustomErrorEventArgs customErrorEventArgs = ReferencePool.Acquire<NetworkCustomErrorEventArgs>();
      customErrorEventArgs.NetworkChannel = networkChannel;
      customErrorEventArgs.CustomErrorData = customErrorData;
      return customErrorEventArgs;
    }

    /// <summary>清理用户自定义网络错误事件。</summary>
    public override void Clear()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.CustomErrorData = (object) null;
    }
  }
}
