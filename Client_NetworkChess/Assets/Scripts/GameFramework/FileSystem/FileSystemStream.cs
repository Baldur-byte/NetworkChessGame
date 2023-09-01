// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.FileSystemStream
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.IO;

namespace GameFramework.FileSystem
{
  /// <summary>文件系统流。</summary>
  public abstract class FileSystemStream
  {
    /// <summary>缓存二进制流的长度。</summary>
    protected const int CachedBytesLength = 4096;
    /// <summary>缓存二进制流。</summary>
    protected static readonly byte[] s_CachedBytes = new byte[4096];

    /// <summary>获取或设置文件系统流位置。</summary>
    protected internal abstract long Position { get; set; }

    /// <summary>获取文件系统流长度。</summary>
    protected internal abstract long Length { get; }

    /// <summary>设置文件系统流长度。</summary>
    /// <param name="length">要设置的文件系统流的长度。</param>
    protected internal abstract void SetLength(long length);

    /// <summary>定位文件系统流位置。</summary>
    /// <param name="offset">要定位的文件系统流位置的偏移。</param>
    /// <param name="origin">要定位的文件系统流位置的方式。</param>
    protected internal abstract void Seek(long offset, SeekOrigin origin);

    /// <summary>从文件系统流中读取一个字节。</summary>
    /// <returns>读取的字节，若已经到达文件结尾，则返回 -1。</returns>
    protected internal abstract int ReadByte();

    /// <summary>从文件系统流中读取二进制流。</summary>
    /// <param name="buffer">存储读取文件内容的二进制流。</param>
    /// <param name="startIndex">存储读取文件内容的二进制流的起始位置。</param>
    /// <param name="length">存储读取文件内容的二进制流的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    protected internal abstract int Read(byte[] buffer, int startIndex, int length);

    /// <summary>从文件系统流中读取二进制流。</summary>
    /// <param name="stream">存储读取文件内容的二进制流。</param>
    /// <param name="length">存储读取文件内容的二进制流的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    protected internal int Read(Stream stream, int length)
    {
      int num = length;
      while (true)
      {
        byte[] cachedBytes = FileSystemStream.s_CachedBytes;
        int length1 = num < 4096 ? num : 4096;
        int count;
        if ((count = this.Read(cachedBytes, 0, length1)) > 0)
        {
          num -= count;
          stream.Write(FileSystemStream.s_CachedBytes, 0, count);
        }
        else
          break;
      }
      Array.Clear((Array) FileSystemStream.s_CachedBytes, 0, 4096);
      return length - num;
    }

    /// <summary>向文件系统流中写入一个字节。</summary>
    /// <param name="value">要写入的字节。</param>
    protected internal abstract void WriteByte(byte value);

    /// <summary>向文件系统流中写入二进制流。</summary>
    /// <param name="buffer">存储写入文件内容的二进制流。</param>
    /// <param name="startIndex">存储写入文件内容的二进制流的起始位置。</param>
    /// <param name="length">存储写入文件内容的二进制流的长度。</param>
    protected internal abstract void Write(byte[] buffer, int startIndex, int length);

    /// <summary>向文件系统流中写入二进制流。</summary>
    /// <param name="stream">存储写入文件内容的二进制流。</param>
    /// <param name="length">存储写入文件内容的二进制流的长度。</param>
    protected internal void Write(Stream stream, int length)
    {
      int num = length;
      while (true)
      {
        Stream stream1 = stream;
        byte[] cachedBytes = FileSystemStream.s_CachedBytes;
        int count = num < 4096 ? num : 4096;
        int length1;
        if ((length1 = stream1.Read(cachedBytes, 0, count)) > 0)
        {
          num -= length1;
          this.Write(FileSystemStream.s_CachedBytes, 0, length1);
        }
        else
          break;
      }
      Array.Clear((Array) FileSystemStream.s_CachedBytes, 0, 4096);
    }

    /// <summary>将文件系统流立刻更新到存储介质中。</summary>
    protected internal abstract void Flush();

    /// <summary>关闭文件系统流。</summary>
    protected internal abstract void Close();
  }
}
