// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载更新事件。</summary>
  public sealed class DownloadUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化下载更新事件的新实例。</summary>
    public DownloadUpdateEventArgs()
    {
      this.SerialId = 0;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.CurrentLength = 0L;
      this.UserData = (object) null;
    }

    /// <summary>获取下载任务的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取下载后存放路径。</summary>
    public string DownloadPath { get; private set; }

    /// <summary>获取下载地址。</summary>
    public string DownloadUri { get; private set; }

    /// <summary>获取当前大小。</summary>
    public long CurrentLength { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建下载更新事件。</summary>
    /// <param name="serialId">下载任务的序列编号。</param>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="currentLength">当前大小。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的下载更新事件。</returns>
    public static DownloadUpdateEventArgs Create(
      int serialId,
      string downloadPath,
      string downloadUri,
      long currentLength,
      object userData)
    {
      DownloadUpdateEventArgs downloadUpdateEventArgs = ReferencePool.Acquire<DownloadUpdateEventArgs>();
      downloadUpdateEventArgs.SerialId = serialId;
      downloadUpdateEventArgs.DownloadPath = downloadPath;
      downloadUpdateEventArgs.DownloadUri = downloadUri;
      downloadUpdateEventArgs.CurrentLength = currentLength;
      downloadUpdateEventArgs.UserData = userData;
      return downloadUpdateEventArgs;
    }

    /// <summary>清理下载更新事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.CurrentLength = 0L;
      this.UserData = (object) null;
    }
  }
}
