// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceUpdateStartEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源更新开始事件。</summary>
  public sealed class ResourceUpdateStartEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源更新开始事件的新实例。</summary>
    public ResourceUpdateStartEventArgs()
    {
      this.Name = (string) null;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.CurrentLength = 0;
      this.CompressedLength = 0;
      this.RetryCount = 0;
    }

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>获取资源下载后存放路径。</summary>
    public string DownloadPath { get; private set; }

    /// <summary>获取下载地址。</summary>
    public string DownloadUri { get; private set; }

    /// <summary>获取当前下载大小。</summary>
    public int CurrentLength { get; private set; }

    /// <summary>获取压缩后大小。</summary>
    public int CompressedLength { get; private set; }

    /// <summary>获取已重试下载次数。</summary>
    public int RetryCount { get; private set; }

    /// <summary>创建资源更新开始事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <param name="downloadPath">资源下载后存放路径。</param>
    /// <param name="downloadUri">资源下载地址。</param>
    /// <param name="currentLength">当前下载大小。</param>
    /// <param name="compressedLength">压缩后大小。</param>
    /// <param name="retryCount">已重试下载次数。</param>
    /// <returns>创建的资源更新开始事件。</returns>
    public static ResourceUpdateStartEventArgs Create(
      string name,
      string downloadPath,
      string downloadUri,
      int currentLength,
      int compressedLength,
      int retryCount)
    {
      ResourceUpdateStartEventArgs updateStartEventArgs = ReferencePool.Acquire<ResourceUpdateStartEventArgs>();
      updateStartEventArgs.Name = name;
      updateStartEventArgs.DownloadPath = downloadPath;
      updateStartEventArgs.DownloadUri = downloadUri;
      updateStartEventArgs.CurrentLength = currentLength;
      updateStartEventArgs.CompressedLength = compressedLength;
      updateStartEventArgs.RetryCount = retryCount;
      return updateStartEventArgs;
    }

    /// <summary>清理资源更新开始事件。</summary>
    public override void Clear()
    {
      this.Name = (string) null;
      this.DownloadPath = (string) null;
      this.DownloadUri = (string) null;
      this.CurrentLength = 0;
      this.CompressedLength = 0;
      this.RetryCount = 0;
    }
  }
}
