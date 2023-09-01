// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceUpdateFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源更新失败事件。</summary>
  public sealed class ResourceUpdateFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源更新失败事件的新实例。</summary>
    public ResourceUpdateFailureEventArgs()
    {
      this.Name = (string) null;
      this.DownloadUri = (string) null;
      this.RetryCount = 0;
      this.TotalRetryCount = 0;
      this.ErrorMessage = (string) null;
    }

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>获取下载地址。</summary>
    public string DownloadUri { get; private set; }

    /// <summary>获取已重试次数。</summary>
    public int RetryCount { get; private set; }

    /// <summary>获取设定的重试次数。</summary>
    public int TotalRetryCount { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>创建资源更新失败事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="retryCount">已重试次数。</param>
    /// <param name="totalRetryCount">设定的重试次数。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>创建的资源更新失败事件。</returns>
    /// <remarks>当已重试次数达到设定的重试次数时，将不再重试。</remarks>
    public static ResourceUpdateFailureEventArgs Create(
      string name,
      string downloadUri,
      int retryCount,
      int totalRetryCount,
      string errorMessage)
    {
      ResourceUpdateFailureEventArgs failureEventArgs = ReferencePool.Acquire<ResourceUpdateFailureEventArgs>();
      failureEventArgs.Name = name;
      failureEventArgs.DownloadUri = downloadUri;
      failureEventArgs.RetryCount = retryCount;
      failureEventArgs.TotalRetryCount = totalRetryCount;
      failureEventArgs.ErrorMessage = errorMessage;
      return failureEventArgs;
    }

    /// <summary>清理资源更新失败事件。</summary>
    public override void Clear()
    {
      this.Name = (string) null;
      this.DownloadUri = (string) null;
      this.RetryCount = 0;
      this.TotalRetryCount = 0;
      this.ErrorMessage = (string) null;
    }
  }
}
