// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadAgentHelperUpdateBytesEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Download
{
  /// <summary>下载代理辅助器更新数据流事件。</summary>
  public sealed class DownloadAgentHelperUpdateBytesEventArgs : GameFrameworkEventArgs
  {
    private byte[] m_Bytes;

    /// <summary>初始化下载代理辅助器更新数据流事件的新实例。</summary>
    public DownloadAgentHelperUpdateBytesEventArgs()
    {
      this.m_Bytes = (byte[]) null;
      this.Offset = 0;
      this.Length = 0;
    }

    /// <summary>获取数据流的偏移。</summary>
    public int Offset { get; private set; }

    /// <summary>获取数据流的长度。</summary>
    public int Length { get; private set; }

    /// <summary>创建下载代理辅助器更新数据流事件。</summary>
    /// <param name="bytes">下载的数据流。</param>
    /// <param name="offset">数据流的偏移。</param>
    /// <param name="length">数据流的长度。</param>
    /// <returns>创建的下载代理辅助器更新数据流事件。</returns>
    public static DownloadAgentHelperUpdateBytesEventArgs Create(
      byte[] bytes,
      int offset,
      int length)
    {
      if (bytes == null)
        throw new GameFrameworkException("Bytes is invalid.");
      if (offset < 0 || offset >= bytes.Length)
        throw new GameFrameworkException("Offset is invalid.");
      if (length <= 0 || offset + length > bytes.Length)
        throw new GameFrameworkException("Length is invalid.");
      DownloadAgentHelperUpdateBytesEventArgs updateBytesEventArgs = ReferencePool.Acquire<DownloadAgentHelperUpdateBytesEventArgs>();
      updateBytesEventArgs.m_Bytes = bytes;
      updateBytesEventArgs.Offset = offset;
      updateBytesEventArgs.Length = length;
      return updateBytesEventArgs;
    }

    /// <summary>清理下载代理辅助器更新数据流事件。</summary>
    public override void Clear()
    {
      this.m_Bytes = (byte[]) null;
      this.Offset = 0;
      this.Length = 0;
    }

    /// <summary>获取下载的数据流。</summary>
    public byte[] GetBytes() => this.m_Bytes;
  }
}
