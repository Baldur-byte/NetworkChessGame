// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadAgentHelperCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载代理辅助器完成事件。</summary>
  public sealed class DownloadAgentHelperCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化下载代理辅助器完成事件的新实例。</summary>
    public DownloadAgentHelperCompleteEventArgs() => this.Length = 0L;

    /// <summary>获取下载的数据大小。</summary>
    public long Length { get; private set; }

    /// <summary>创建下载代理辅助器完成事件。</summary>
    /// <param name="length">下载的数据大小。</param>
    /// <returns>创建的下载代理辅助器完成事件。</returns>
    public static DownloadAgentHelperCompleteEventArgs Create(long length)
    {
      if (length < 0L)
        throw new GameFrameworkException("Length is invalid.");
      DownloadAgentHelperCompleteEventArgs completeEventArgs = ReferencePool.Acquire<DownloadAgentHelperCompleteEventArgs>();
      completeEventArgs.Length = length;
      return completeEventArgs;
    }

    /// <summary>清理下载代理辅助器完成事件。</summary>
    public override void Clear() => this.Length = 0L;
  }
}
