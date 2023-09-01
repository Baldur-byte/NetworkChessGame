// Decompiled with JetBrains decompiler
// Type: GameFramework.WebRequest.WebRequestSuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.WebRequest
{
  /// <summary>Web 请求成功事件。</summary>
  public sealed class WebRequestSuccessEventArgs : GameFrameworkEventArgs
  {
    private byte[] m_WebResponseBytes;

    /// <summary>初始化 Web 请求成功事件的新实例。</summary>
    public WebRequestSuccessEventArgs()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.m_WebResponseBytes = (byte[]) null;
      this.UserData = (object) null;
    }

    /// <summary>获取 Web 请求任务的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取 Web 请求地址。</summary>
    public string WebRequestUri { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建 Web 请求成功事件。</summary>
    /// <param name="serialId">Web 请求任务的序列编号。</param>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="webResponseBytes">Web 响应的数据流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的 Web 请求成功事件。</returns>
    public static WebRequestSuccessEventArgs Create(
      int serialId,
      string webRequestUri,
      byte[] webResponseBytes,
      object userData)
    {
      WebRequestSuccessEventArgs successEventArgs = ReferencePool.Acquire<WebRequestSuccessEventArgs>();
      successEventArgs.SerialId = serialId;
      successEventArgs.WebRequestUri = webRequestUri;
      successEventArgs.m_WebResponseBytes = webResponseBytes;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理 Web 请求成功事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.WebRequestUri = (string) null;
      this.m_WebResponseBytes = (byte[]) null;
      this.UserData = (object) null;
    }

    /// <summary>获取 Web 响应的数据流。</summary>
    /// <returns>Web 响应的数据流。</returns>
    public byte[] GetWebResponseBytes() => this.m_WebResponseBytes;
  }
}
