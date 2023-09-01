// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadAgentHelperErrorEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载代理辅助器错误事件。</summary>
  public sealed class DownloadAgentHelperErrorEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化下载代理辅助器错误事件的新实例。</summary>
    public DownloadAgentHelperErrorEventArgs()
    {
      this.DeleteDownloading = false;
      this.ErrorMessage = (string) null;
    }

    /// <summary>获取是否需要删除正在下载的文件。</summary>
    public bool DeleteDownloading { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>创建下载代理辅助器错误事件。</summary>
    /// <param name="deleteDownloading">是否需要删除正在下载的文件。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>创建的下载代理辅助器错误事件。</returns>
    public static DownloadAgentHelperErrorEventArgs Create(
      bool deleteDownloading,
      string errorMessage)
    {
      DownloadAgentHelperErrorEventArgs helperErrorEventArgs = ReferencePool.Acquire<DownloadAgentHelperErrorEventArgs>();
      helperErrorEventArgs.DeleteDownloading = deleteDownloading;
      helperErrorEventArgs.ErrorMessage = errorMessage;
      return helperErrorEventArgs;
    }

    /// <summary>清理下载代理辅助器错误事件。</summary>
    public override void Clear()
    {
      this.DeleteDownloading = false;
      this.ErrorMessage = (string) null;
    }
  }
}
