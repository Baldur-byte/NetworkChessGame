// Decompiled with JetBrains decompiler
// Type: GameFramework.WebRequest.WebRequestAgentHelperCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.WebRequest
{
  /// <summary>Web 请求代理辅助器完成事件。</summary>
  public sealed class WebRequestAgentHelperCompleteEventArgs : GameFrameworkEventArgs
  {
    private byte[] m_WebResponseBytes;

    /// <summary>初始化 Web 请求代理辅助器完成事件的新实例。</summary>
    public WebRequestAgentHelperCompleteEventArgs() => this.m_WebResponseBytes = (byte[]) null;

    /// <summary>创建 Web 请求代理辅助器完成事件。</summary>
    /// <param name="webResponseBytes">Web 响应的数据流。</param>
    /// <returns>创建的 Web 请求代理辅助器完成事件。</returns>
    public static WebRequestAgentHelperCompleteEventArgs Create(byte[] webResponseBytes)
    {
      WebRequestAgentHelperCompleteEventArgs completeEventArgs = ReferencePool.Acquire<WebRequestAgentHelperCompleteEventArgs>();
      completeEventArgs.m_WebResponseBytes = webResponseBytes;
      return completeEventArgs;
    }

    /// <summary>清理 Web 请求代理辅助器完成事件。</summary>
    public override void Clear() => this.m_WebResponseBytes = (byte[]) null;

    /// <summary>获取 Web 响应的数据流。</summary>
    /// <returns>Web 响应的数据流。</returns>
    public byte[] GetWebResponseBytes() => this.m_WebResponseBytes;
  }
}
