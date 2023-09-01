// Decompiled with JetBrains decompiler
// Type: GameFramework.Network.NetworkErrorEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Net.Sockets;

namespace GameFramework.Network
{
  /// <summary>网络错误事件。</summary>
  public sealed class NetworkErrorEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化网络错误事件的新实例。</summary>
    public NetworkErrorEventArgs()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.ErrorCode = NetworkErrorCode.Unknown;
      this.SocketErrorCode = SocketError.Success;
      this.ErrorMessage = (string) null;
    }

    /// <summary>获取网络频道。</summary>
    public INetworkChannel NetworkChannel { get; private set; }

    /// <summary>获取错误码。</summary>
    public NetworkErrorCode ErrorCode { get; private set; }

    /// <summary>获取 Socket 错误码。</summary>
    public SocketError SocketErrorCode { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>创建网络错误事件。</summary>
    /// <param name="networkChannel">网络频道。</param>
    /// <param name="errorCode">错误码。</param>
    /// <param name="socketErrorCode">Socket 错误码。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>创建的网络错误事件。</returns>
    public static NetworkErrorEventArgs Create(
      INetworkChannel networkChannel,
      NetworkErrorCode errorCode,
      SocketError socketErrorCode,
      string errorMessage)
    {
      NetworkErrorEventArgs networkErrorEventArgs = ReferencePool.Acquire<NetworkErrorEventArgs>();
      networkErrorEventArgs.NetworkChannel = networkChannel;
      networkErrorEventArgs.ErrorCode = errorCode;
      networkErrorEventArgs.SocketErrorCode = socketErrorCode;
      networkErrorEventArgs.ErrorMessage = errorMessage;
      return networkErrorEventArgs;
    }

    /// <summary>清理网络错误事件。</summary>
    public override void Clear()
    {
      this.NetworkChannel = (INetworkChannel) null;
      this.ErrorCode = NetworkErrorCode.Unknown;
      this.SocketErrorCode = SocketError.Success;
      this.ErrorMessage = (string) null;
    }
  }
}
