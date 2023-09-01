// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载失败事件。</summary>
  public sealed class DownloadFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化下载失败事件的新实例。</summary>
    public DownloadFailureEventArgs()
    {
      this.SerialId = 0;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取下载任务的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取下载后存放路径。</summary>
    public string DownloadPath { get; private set; }

    /// <summary>获取下载地址。</summary>
    public string DownloadUri { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建下载失败事件。</summary>
    /// <param name="serialId">下载任务的序列编号。</param>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的下载失败事件。</returns>
    public static DownloadFailureEventArgs Create(
      int serialId,
      string downloadPath,
      string downloadUri,
      string errorMessage,
      object userData)
    {
      DownloadFailureEventArgs failureEventArgs = ReferencePool.Acquire<DownloadFailureEventArgs>();
      failureEventArgs.SerialId = serialId;
      failureEventArgs.DownloadPath = downloadPath;
      failureEventArgs.DownloadUri = downloadUri;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理下载失败事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
