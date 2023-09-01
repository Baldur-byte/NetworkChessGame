// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.IDownloadAgentHelper
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.Download
{
  /// <summary>下载代理辅助器接口。</summary>
  public interface IDownloadAgentHelper
  {
    /// <summary>下载代理辅助器更新数据流事件。</summary>
    event EventHandler<DownloadAgentHelperUpdateBytesEventArgs> DownloadAgentHelperUpdateBytes;

    /// <summary>下载代理辅助器更新数据大小事件。</summary>
    event EventHandler<DownloadAgentHelperUpdateLengthEventArgs> DownloadAgentHelperUpdateLength;

    /// <summary>下载代理辅助器完成事件。</summary>
    event EventHandler<DownloadAgentHelperCompleteEventArgs> DownloadAgentHelperComplete;

    /// <summary>下载代理辅助器错误事件。</summary>
    event EventHandler<DownloadAgentHelperErrorEventArgs> DownloadAgentHelperError;

    /// <summary>通过下载代理辅助器下载指定地址的数据。</summary>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    void Download(string downloadUri, object userData);

    /// <summary>通过下载代理辅助器下载指定地址的数据。</summary>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="fromPosition">下载数据起始位置。</param>
    /// <param name="userData">用户自定义数据。</param>
    void Download(string downloadUri, long fromPosition, object userData);

    /// <summary>通过下载代理辅助器下载指定地址的数据。</summary>
    /// <param name="downloadUri">下载地址。</param>
    /// <param name="fromPosition">下载数据起始位置。</param>
    /// <param name="toPosition">下载数据结束位置。</param>
    /// <param name="userData">用户自定义数据。</param>
    void Download(string downloadUri, long fromPosition, long toPosition, object userData);

    /// <summary>重置下载代理辅助器。</summary>
    void Reset();
  }
}
