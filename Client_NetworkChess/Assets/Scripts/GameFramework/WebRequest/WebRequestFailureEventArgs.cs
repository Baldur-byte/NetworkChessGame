// Decompiled with JetBrains decompiler
// Type: GameFramework.WebRequest.WebRequestFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.WebRequest
{
  /// <summary>Web 请求失败事件。</summary>
  public sealed class WebRequestFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化 Web 请求失败事件的新实例。</summary>
    public WebRequestFailureEventArgs()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取 Web 请求任务的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取 Web 请求地址。</summary>
    public string WebRequestUri { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建 Web 请求失败事件。</summary>
    /// <param name="serialId">Web 请求任务的序列编号。</param>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的 Web 请求失败事件。</returns>
    public static WebRequestFailureEventArgs Create(
      int serialId,
      string webRequestUri,
      string errorMessage,
      object userData)
    {
      WebRequestFailureEventArgs failureEventArgs = ReferencePool.Acquire<WebRequestFailureEventArgs>();
      failureEventArgs.SerialId = serialId;
      failureEventArgs.WebRequestUri = webRequestUri;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理 Web 请求失败事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
