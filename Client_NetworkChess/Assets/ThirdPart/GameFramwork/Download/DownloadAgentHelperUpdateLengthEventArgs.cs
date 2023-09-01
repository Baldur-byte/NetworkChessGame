// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadAgentHelperUpdateLengthEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载代理辅助器更新数据大小事件。</summary>
  public sealed class DownloadAgentHelperUpdateLengthEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化下载代理辅助器更新数据大小事件的新实例。</summary>
    public DownloadAgentHelperUpdateLengthEventArgs() => this.DeltaLength = 0;

    /// <summary>获取下载的增量数据大小。</summary>
    public int DeltaLength { get; private set; }

    /// <summary>创建下载代理辅助器更新数据大小事件。</summary>
    /// <param name="deltaLength">下载的增量数据大小。</param>
    /// <returns>创建的下载代理辅助器更新数据大小事件。</returns>
    public static DownloadAgentHelperUpdateLengthEventArgs Create(int deltaLength)
    {
      if (deltaLength <= 0)
        throw new GameFrameworkException("Delta length is invalid.");
      DownloadAgentHelperUpdateLengthEventArgs updateLengthEventArgs = ReferencePool.Acquire<DownloadAgentHelperUpdateLengthEventArgs>();
      updateLengthEventArgs.DeltaLength = deltaLength;
      return updateLengthEventArgs;
    }

    /// <summary>清理下载代理辅助器更新数据大小事件。</summary>
    public override void Clear() => this.DeltaLength = 0;
  }
}
