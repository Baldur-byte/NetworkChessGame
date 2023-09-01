// Decompiled with JetBrains decompiler
// Type: GameFramework.WebRequest.WebRequestStartEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.WebRequest
{
  /// <summary>Web 请求开始事件。</summary>
  public sealed class WebRequestStartEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化 Web 请求开始事件的新实例。</summary>
    public WebRequestStartEventArgs()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取 Web 请求任务的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取 Web 请求地址。</summary>
    public string WebRequestUri { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建 Web 请求开始事件。</summary>
    /// <param name="serialId">Web 请求任务的序列编号。</param>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的 Web 请求开始事件。</returns>
    public static WebRequestStartEventArgs Create(
      int serialId,
      string webRequestUri,
      object userData)
    {
      WebRequestStartEventArgs requestStartEventArgs = ReferencePool.Acquire<WebRequestStartEventArgs>();
      requestStartEventArgs.SerialId = serialId;
      requestStartEventArgs.WebRequestUri = webRequestUri;
      requestStartEventArgs.UserData = userData;
      return requestStartEventArgs;
    }

    /// <summary>清理 Web 请求开始事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.UserData = (object) null;
    }
  }
}
