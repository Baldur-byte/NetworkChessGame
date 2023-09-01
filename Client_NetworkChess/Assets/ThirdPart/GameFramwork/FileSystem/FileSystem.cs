// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.FileSystem
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace GameFramework.FileSystem
{
  /// <summary>文件系统。</summary>
  internal sealed class FileSystem : IFileSystem
  {
    private const int ClusterSize = 4096;
    private const int CachedBytesLength = 4096;
    private static readonly string[] EmptyStringArray = new string[0];
    private static readonly byte[] s_CachedBytes = new byte[4096];
    private static readonly int HeaderDataSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof (GameFramework.FileSystem.FileSystem.HeaderData));
    private static readonly int BlockDataSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof (GameFramework.FileSystem.FileSystem.BlockData));
    private static readonly int StringDataSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof (GameFramework.FileSystem.FileSystem.StringData));
    private readonly string m_FullPath;
    private readonly FileSystemAccess m_Access;
    private readonly FileSystemStream m_Stream;
    private readonly Dictionary<string, int> m_FileDatas;
    private readonly List<GameFramework.FileSystem.FileSystem.BlockData> m_BlockDatas;
    private readonly GameFrameworkMultiDictionary<int, int> m_FreeBlockIndexes;
    private readonly SortedDictionary<int, GameFramework.FileSystem.FileSystem.StringData> m_StringDatas;
    private readonly Queue<int> m_FreeStringIndexes;
    private readonly Queue<GameFramework.FileSystem.FileSystem.StringData> m_FreeStringDatas;
    private GameFramework.FileSystem.FileSystem.HeaderData m_HeaderData;
    private int m_BlockDataOffset;
    private int m_StringDataOffset;
    private int m_FileDataOffset;

    /// <summary>初始化文件系统的新实例。</summary>
    /// <param name="fullPath">文件系统完整路径。</param>
    /// <param name="access">文件系统访问方式。</param>
    /// <param name="stream">文件系统流。</param>
    private FileSystem(string fullPath, FileSystemAccess access, FileSystemStream stream)
    {
      if (string.IsNullOrEmpty(fullPath))
        throw new GameFrameworkException("Full path is invalid.");
      if (access == FileSystemAccess.Unspecified)
        throw new GameFrameworkException("Access is invalid.");
      if (stream == null)
        throw new GameFrameworkException("Stream is invalid.");
      this.m_FullPath = fullPath;
      this.m_Access = access;
      this.m_Stream = stream;
      this.m_FileDatas = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_BlockDatas = new List<GameFramework.FileSystem.FileSystem.BlockData>();
      this.m_FreeBlockIndexes = new GameFrameworkMultiDictionary<int, int>();
      this.m_StringDatas = new SortedDictionary<int, GameFramework.FileSystem.FileSystem.StringData>();
      this.m_FreeStringIndexes = new Queue<int>();
      this.m_FreeStringDatas = new Queue<GameFramework.FileSystem.FileSystem.StringData>();
      this.m_HeaderData = new GameFramework.FileSystem.FileSystem.HeaderData();
      this.m_BlockDataOffset = 0;
      this.m_StringDataOffset = 0;
      this.m_FileDataOffset = 0;
      Utility.Marshal.EnsureCachedHGlobalSize(4096);
    }

    /// <summary>获取文件系统完整路径。</summary>
    public string FullPath => this.m_FullPath;

    /// <summary>获取文件系统访问方式。</summary>
    public FileSystemAccess Access => this.m_Access;

    /// <summary>获取文件数量。</summary>
    public int FileCount => this.m_FileDatas.Count;

    /// <summary>获取最大文件数量。</summary>
    public int MaxFileCount => this.m_HeaderData.MaxFileCount;

    /// <summary>创建文件系统。</summary>
    /// <param name="fullPath">要创建的文件系统的完整路径。</param>
    /// <param name="access">要创建的文件系统的访问方式。</param>
    /// <param name="stream">要创建的文件系统的文件系统流。</param>
    /// <param name="maxFileCount">要创建的文件系统的最大文件数量。</param>
    /// <param name="maxBlockCount">要创建的文件系统的最大块数据数量。</param>
    /// <returns>创建的文件系统。</returns>
    public static GameFramework.FileSystem.FileSystem Create(
      string fullPath,
      FileSystemAccess access,
      FileSystemStream stream,
      int maxFileCount,
      int maxBlockCount)
    {
      if (maxFileCount <= 0)
        throw new GameFrameworkException("Max file count is invalid.");
      if (maxBlockCount <= 0)
        throw new GameFrameworkException("Max block count is invalid.");
      if (maxFileCount > maxBlockCount)
        throw new GameFrameworkException("Max file count can not larger than max block count.");
      GameFramework.FileSystem.FileSystem fileSystem = new GameFramework.FileSystem.FileSystem(fullPath, access, stream);
      fileSystem.m_HeaderData = new GameFramework.FileSystem.FileSystem.HeaderData(maxFileCount, maxBlockCount);
      GameFramework.FileSystem.FileSystem.CalcOffsets(fileSystem);
      Utility.Marshal.StructureToBytes<GameFramework.FileSystem.FileSystem.HeaderData>(fileSystem.m_HeaderData, GameFramework.FileSystem.FileSystem.HeaderDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
      try
      {
        stream.Write(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.HeaderDataSize);
        stream.SetLength((long) fileSystem.m_FileDataOffset);
        return fileSystem;
      }
      catch
      {
        fileSystem.Shutdown();
        return (GameFramework.FileSystem.FileSystem) null;
      }
    }

    /// <summary>加载文件系统。</summary>
    /// <param name="fullPath">要加载的文件系统的完整路径。</param>
    /// <param name="access">要加载的文件系统的访问方式。</param>
    /// <param name="stream">要加载的文件系统的文件系统流。</param>
    /// <returns>加载的文件系统。</returns>
    public static GameFramework.FileSystem.FileSystem Load(
      string fullPath,
      FileSystemAccess access,
      FileSystemStream stream)
    {
      GameFramework.FileSystem.FileSystem fileSystem = new GameFramework.FileSystem.FileSystem(fullPath, access, stream);
      stream.Read(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.HeaderDataSize);
      fileSystem.m_HeaderData = Utility.Marshal.BytesToStructure<GameFramework.FileSystem.FileSystem.HeaderData>(GameFramework.FileSystem.FileSystem.HeaderDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
      if (!fileSystem.m_HeaderData.IsValid)
        throw new GameFrameworkException(Utility.Text.Format<string>("File system '{0}' is invalid.", fullPath));
      GameFramework.FileSystem.FileSystem.CalcOffsets(fileSystem);
      if (fileSystem.m_BlockDatas.Capacity < fileSystem.m_HeaderData.BlockCount)
        fileSystem.m_BlockDatas.Capacity = fileSystem.m_HeaderData.BlockCount;
      for (int index = 0; index < fileSystem.m_HeaderData.BlockCount; ++index)
      {
        stream.Read(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.BlockDataSize);
        GameFramework.FileSystem.FileSystem.BlockData structure = Utility.Marshal.BytesToStructure<GameFramework.FileSystem.FileSystem.BlockData>(GameFramework.FileSystem.FileSystem.BlockDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
        fileSystem.m_BlockDatas.Add(structure);
      }
      for (int index = 0; index < fileSystem.m_BlockDatas.Count; ++index)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData = fileSystem.m_BlockDatas[index];
        if (blockData.Using)
        {
          GameFramework.FileSystem.FileSystem.StringData stringData = fileSystem.ReadStringData(blockData.StringIndex);
          fileSystem.m_StringDatas.Add(blockData.StringIndex, stringData);
          fileSystem.m_FileDatas.Add(stringData.GetString(fileSystem.m_HeaderData.GetEncryptBytes()), index);
        }
        else
          fileSystem.m_FreeBlockIndexes.Add(blockData.Length, index);
      }
      int num = 0;
      foreach (KeyValuePair<int, GameFramework.FileSystem.FileSystem.StringData> stringData in fileSystem.m_StringDatas)
      {
        while (num < stringData.Key)
          fileSystem.m_FreeStringIndexes.Enqueue(num++);
        ++num;
      }
      return fileSystem;
    }

    /// <summary>关闭并清理文件系统。</summary>
    public void Shutdown()
    {
      this.m_Stream.Close();
      this.m_FileDatas.Clear();
      this.m_BlockDatas.Clear();
      this.m_FreeBlockIndexes.Clear();
      this.m_StringDatas.Clear();
      this.m_FreeStringIndexes.Clear();
      this.m_FreeStringDatas.Clear();
      this.m_BlockDataOffset = 0;
      this.m_StringDataOffset = 0;
      this.m_FileDataOffset = 0;
    }

    /// <summary>获取文件信息。</summary>
    /// <param name="name">要获取文件信息的文件名称。</param>
    /// <returns>获取的文件信息。</returns>
    public FileInfo GetFileInfo(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      int index = 0;
      if (!this.m_FileDatas.TryGetValue(name, out index))
        return new FileInfo();
      GameFramework.FileSystem.FileSystem.BlockData blockData = this.m_BlockDatas[index];
      return new FileInfo(name, GameFramework.FileSystem.FileSystem.GetClusterOffset(blockData.ClusterIndex), blockData.Length);
    }

    /// <summary>获取所有文件信息。</summary>
    /// <returns>获取的所有文件信息。</returns>
    public FileInfo[] GetAllFileInfos()
    {
      int num = 0;
      FileInfo[] allFileInfos = new FileInfo[this.m_FileDatas.Count];
      foreach (KeyValuePair<string, int> fileData in this.m_FileDatas)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData = this.m_BlockDatas[fileData.Value];
        allFileInfos[num++] = new FileInfo(fileData.Key, GameFramework.FileSystem.FileSystem.GetClusterOffset(blockData.ClusterIndex), blockData.Length);
      }
      return allFileInfos;
    }

    /// <summary>获取所有文件信息。</summary>
    /// <param name="results">获取的所有文件信息。</param>
    public void GetAllFileInfos(List<FileInfo> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, int> fileData in this.m_FileDatas)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData = this.m_BlockDatas[fileData.Value];
        results.Add(new FileInfo(fileData.Key, GameFramework.FileSystem.FileSystem.GetClusterOffset(blockData.ClusterIndex), blockData.Length));
      }
    }

    /// <summary>检查是否存在指定文件。</summary>
    /// <param name="name">要检查的文件名称。</param>
    /// <returns>是否存在指定文件。</returns>
    public bool HasFile(string name) => !string.IsNullOrEmpty(name) ? this.m_FileDatas.ContainsKey(name) : throw new GameFrameworkException("Name is invalid.");

    /// <summary>读取指定文件。</summary>
    /// <param name="name">要读取的文件名称。</param>
    /// <returns>存储读取文件内容的二进制流。</returns>
    public byte[] ReadFile(string name)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      FileInfo fileInfo = !string.IsNullOrEmpty(name) ? this.GetFileInfo(name) : throw new GameFrameworkException("Name is invalid.");
      if (!fileInfo.IsValid)
        return (byte[]) null;
      int length = fileInfo.Length;
      byte[] buffer = new byte[length];
      if (length > 0)
      {
        this.m_Stream.Position = fileInfo.Offset;
        this.m_Stream.Read(buffer, 0, length);
      }
      return buffer;
    }

    /// <summary>读取指定文件。</summary>
    /// <param name="name">要读取的文件名称。</param>
    /// <param name="buffer">存储读取文件内容的二进制流。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFile(string name, byte[] buffer)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.ReadFile(name, buffer, 0, buffer.Length);
    }

    /// <summary>读取指定文件。</summary>
    /// <param name="name">要读取的文件名称。</param>
    /// <param name="buffer">存储读取文件内容的二进制流。</param>
    /// <param name="startIndex">存储读取文件内容的二进制流的起始位置。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFile(string name, byte[] buffer, int startIndex)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.ReadFile(name, buffer, startIndex, buffer.Length - startIndex);
    }

    /// <summary>读取指定文件。</summary>
    /// <param name="name">要读取的文件名称。</param>
    /// <param name="buffer">存储读取文件内容的二进制流。</param>
    /// <param name="startIndex">存储读取文件内容的二进制流的起始位置。</param>
    /// <param name="length">存储读取文件内容的二进制流的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFile(string name, byte[] buffer, int startIndex, int length)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      if (startIndex < 0 || length < 0 || startIndex + length > buffer.Length)
        throw new GameFrameworkException("Start index or length is invalid.");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return 0;
      this.m_Stream.Position = fileInfo.Offset;
      if (length > fileInfo.Length)
        length = fileInfo.Length;
      return length > 0 ? this.m_Stream.Read(buffer, startIndex, length) : 0;
    }

    /// <summary>读取指定文件。</summary>
    /// <param name="name">要读取的文件名称。</param>
    /// <param name="stream">存储读取文件内容的二进制流。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFile(string name, Stream stream)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (stream == null)
        throw new GameFrameworkException("Stream is invalid.");
      if (!stream.CanWrite)
        throw new GameFrameworkException("Stream is not writable.");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return 0;
      int length = fileInfo.Length;
      if (length <= 0)
        return 0;
      this.m_Stream.Position = fileInfo.Offset;
      return this.m_Stream.Read(stream, length);
    }

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>存储读取文件片段内容的二进制流。</returns>
    public byte[] ReadFileSegment(string name, int length) => this.ReadFileSegment(name, 0, length);

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="offset">要读取片段的偏移。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>存储读取文件片段内容的二进制流。</returns>
    public byte[] ReadFileSegment(string name, int offset, int length)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (offset < 0)
        throw new GameFrameworkException("Index is invalid.");
      if (length < 0)
        throw new GameFrameworkException("Length is invalid.");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return (byte[]) null;
      if (offset > fileInfo.Length)
        offset = fileInfo.Length;
      int num = fileInfo.Length - offset;
      if (length > num)
        length = num;
      byte[] buffer = new byte[length];
      if (length > 0)
      {
        this.m_Stream.Position = fileInfo.Offset + (long) offset;
        this.m_Stream.Read(buffer, 0, length);
      }
      return buffer;
    }

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, byte[] buffer)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.ReadFileSegment(name, 0, buffer, 0, buffer.Length);
    }

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, byte[] buffer, int length) => this.ReadFileSegment(name, 0, buffer, 0, length);

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <param name="startIndex">存储读取文件片段内容的二进制流的起始位置。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, byte[] buffer, int startIndex, int length) => this.ReadFileSegment(name, 0, buffer, startIndex, length);

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="offset">要读取片段的偏移。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, int offset, byte[] buffer)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.ReadFileSegment(name, offset, buffer, 0, buffer.Length);
    }

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="offset">要读取片段的偏移。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, int offset, byte[] buffer, int length) => this.ReadFileSegment(name, offset, buffer, 0, length);

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="offset">要读取片段的偏移。</param>
    /// <param name="buffer">存储读取文件片段内容的二进制流。</param>
    /// <param name="startIndex">存储读取文件片段内容的二进制流的起始位置。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(
      string name,
      int offset,
      byte[] buffer,
      int startIndex,
      int length)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (offset < 0)
        throw new GameFrameworkException("Index is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      if (startIndex < 0 || length < 0 || startIndex + length > buffer.Length)
        throw new GameFrameworkException("Start index or length is invalid.");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return 0;
      if (offset > fileInfo.Length)
        offset = fileInfo.Length;
      int num = fileInfo.Length - offset;
      if (length > num)
        length = num;
      if (length <= 0)
        return 0;
      this.m_Stream.Position = fileInfo.Offset + (long) offset;
      return this.m_Stream.Read(buffer, startIndex, length);
    }

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="stream">存储读取文件片段内容的二进制流。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, Stream stream, int length) => this.ReadFileSegment(name, 0, stream, length);

    /// <summary>读取指定文件的指定片段。</summary>
    /// <param name="name">要读取片段的文件名称。</param>
    /// <param name="offset">要读取片段的偏移。</param>
    /// <param name="stream">存储读取文件片段内容的二进制流。</param>
    /// <param name="length">要读取片段的长度。</param>
    /// <returns>实际读取了多少字节。</returns>
    public int ReadFileSegment(string name, int offset, Stream stream, int length)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (offset < 0)
        throw new GameFrameworkException("Index is invalid.");
      if (stream == null)
        throw new GameFrameworkException("Stream is invalid.");
      if (!stream.CanWrite)
        throw new GameFrameworkException("Stream is not writable.");
      if (length < 0)
        throw new GameFrameworkException("Length is invalid.");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return 0;
      if (offset > fileInfo.Length)
        offset = fileInfo.Length;
      int num = fileInfo.Length - offset;
      if (length > num)
        length = num;
      if (length <= 0)
        return 0;
      this.m_Stream.Position = fileInfo.Offset + (long) offset;
      return this.m_Stream.Read(stream, length);
    }

    /// <summary>写入指定文件。</summary>
    /// <param name="name">要写入的文件名称。</param>
    /// <param name="buffer">存储写入文件内容的二进制流。</param>
    /// <returns>是否写入指定文件成功。</returns>
    public bool WriteFile(string name, byte[] buffer)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.WriteFile(name, buffer, 0, buffer.Length);
    }

    /// <summary>写入指定文件。</summary>
    /// <param name="name">要写入的文件名称。</param>
    /// <param name="buffer">存储写入文件内容的二进制流。</param>
    /// <param name="startIndex">存储写入文件内容的二进制流的起始位置。</param>
    /// <returns>是否写入指定文件成功。</returns>
    public bool WriteFile(string name, byte[] buffer, int startIndex)
    {
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.WriteFile(name, buffer, startIndex, buffer.Length - startIndex);
    }

    /// <summary>写入指定文件。</summary>
    /// <param name="name">要写入的文件名称。</param>
    /// <param name="buffer">存储写入文件内容的二进制流。</param>
    /// <param name="startIndex">存储写入文件内容的二进制流的起始位置。</param>
    /// <param name="length">存储写入文件内容的二进制流的长度。</param>
    /// <returns>是否写入指定文件成功。</returns>
    public bool WriteFile(string name, byte[] buffer, int startIndex, int length)
    {
      if (this.m_Access != FileSystemAccess.Write && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not writable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (name.Length > (int) byte.MaxValue)
        throw new GameFrameworkException(Utility.Text.Format<string>("Name '{0}' is too long.", name));
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      if (startIndex < 0 || length < 0 || startIndex + length > buffer.Length)
        throw new GameFrameworkException("Start index or length is invalid.");
      bool hasFile = false;
      int oldBlockIndex = -1;
      if (this.m_FileDatas.TryGetValue(name, out oldBlockIndex))
        hasFile = true;
      if (!hasFile && this.m_FileDatas.Count >= this.m_HeaderData.MaxFileCount)
        return false;
      int num = this.AllocBlock(length);
      if (num < 0)
        return false;
      if (length > 0)
      {
        this.m_Stream.Position = GameFramework.FileSystem.FileSystem.GetClusterOffset(this.m_BlockDatas[num].ClusterIndex);
        this.m_Stream.Write(buffer, startIndex, length);
      }
      this.ProcessWriteFile(name, hasFile, oldBlockIndex, num, length);
      this.m_Stream.Flush();
      return true;
    }

    /// <summary>写入指定文件。</summary>
    /// <param name="name">要写入的文件名称。</param>
    /// <param name="stream">存储写入文件内容的二进制流。</param>
    /// <returns>是否写入指定文件成功。</returns>
    public bool WriteFile(string name, Stream stream)
    {
      if (this.m_Access != FileSystemAccess.Write && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not writable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (name.Length > (int) byte.MaxValue)
        throw new GameFrameworkException(Utility.Text.Format<string>("Name '{0}' is too long.", name));
      if (stream == null)
        throw new GameFrameworkException("Stream is invalid.");
      if (!stream.CanRead)
        throw new GameFrameworkException("Stream is not readable.");
      bool hasFile = false;
      int oldBlockIndex = -1;
      if (this.m_FileDatas.TryGetValue(name, out oldBlockIndex))
        hasFile = true;
      if (!hasFile && this.m_FileDatas.Count >= this.m_HeaderData.MaxFileCount)
        return false;
      int length = (int) (stream.Length - stream.Position);
      int num = this.AllocBlock(length);
      if (num < 0)
        return false;
      if (length > 0)
      {
        this.m_Stream.Position = GameFramework.FileSystem.FileSystem.GetClusterOffset(this.m_BlockDatas[num].ClusterIndex);
        this.m_Stream.Write(stream, length);
      }
      this.ProcessWriteFile(name, hasFile, oldBlockIndex, num, length);
      this.m_Stream.Flush();
      return true;
    }

    /// <summary>写入指定文件。</summary>
    /// <param name="name">要写入的文件名称。</param>
    /// <param name="filePath">存储写入文件内容的文件路径。</param>
    /// <returns>是否写入指定文件成功。</returns>
    public bool WriteFile(string name, string filePath)
    {
      if (string.IsNullOrEmpty(filePath))
        throw new GameFrameworkException("File path is invalid");
      if (!File.Exists(filePath))
        return false;
      using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        return this.WriteFile(name, (Stream) fileStream);
    }

    /// <summary>将指定文件另存为物理文件。</summary>
    /// <param name="name">要另存为的文件名称。</param>
    /// <param name="filePath">存储写入文件内容的文件路径。</param>
    /// <returns>是否将指定文件另存为物理文件成功。</returns>
    public bool SaveAsFile(string name, string filePath)
    {
      if (this.m_Access != FileSystemAccess.Read && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not readable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (string.IsNullOrEmpty(filePath))
        throw new GameFrameworkException("File path is invalid");
      FileInfo fileInfo = this.GetFileInfo(name);
      if (!fileInfo.IsValid)
        return false;
      if (File.Exists(filePath))
        File.Delete(filePath);
      string directoryName = System.IO.Path.GetDirectoryName(filePath);
      if (!Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
      {
        int length = fileInfo.Length;
        if (length <= 0)
          return true;
        this.m_Stream.Position = fileInfo.Offset;
        return this.m_Stream.Read((Stream) fileStream, length) == length;
      }
    }

    /// <summary>重命名指定文件。</summary>
    /// <param name="oldName">要重命名的文件名称。</param>
    /// <param name="newName">重命名后的文件名称。</param>
    /// <returns>是否重命名指定文件成功。</returns>
    public bool RenameFile(string oldName, string newName)
    {
      if (this.m_Access != FileSystemAccess.Write && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not writable.");
      if (string.IsNullOrEmpty(oldName))
        throw new GameFrameworkException("Old name is invalid.");
      if (string.IsNullOrEmpty(newName))
        throw new GameFrameworkException("New name is invalid.");
      if (newName.Length > (int) byte.MaxValue)
        throw new GameFrameworkException(Utility.Text.Format<string>("New name '{0}' is too long.", newName));
      if (oldName == newName)
        return true;
      if (this.m_FileDatas.ContainsKey(newName))
        return false;
      int index = 0;
      if (!this.m_FileDatas.TryGetValue(oldName, out index))
        return false;
      int stringIndex = this.m_BlockDatas[index].StringIndex;
      GameFramework.FileSystem.FileSystem.StringData stringData = this.m_StringDatas[stringIndex].SetString(newName, this.m_HeaderData.GetEncryptBytes());
      this.m_StringDatas[stringIndex] = stringData;
      this.WriteStringData(stringIndex, stringData);
      this.m_FileDatas.Add(newName, index);
      this.m_FileDatas.Remove(oldName);
      this.m_Stream.Flush();
      return true;
    }

    /// <summary>删除指定文件。</summary>
    /// <param name="name">要删除的文件名称。</param>
    /// <returns>是否删除指定文件成功。</returns>
    public bool DeleteFile(string name)
    {
      if (this.m_Access != FileSystemAccess.Write && this.m_Access != FileSystemAccess.ReadWrite)
        throw new GameFrameworkException("File system is not writable.");
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      int num = 0;
      if (!this.m_FileDatas.TryGetValue(name, out num))
        return false;
      this.m_FileDatas.Remove(name);
      GameFramework.FileSystem.FileSystem.BlockData blockData = this.m_BlockDatas[num];
      int stringIndex = blockData.StringIndex;
      GameFramework.FileSystem.FileSystem.StringData stringData = this.m_StringDatas[stringIndex].Clear();
      this.m_FreeStringIndexes.Enqueue(stringIndex);
      this.m_FreeStringDatas.Enqueue(stringData);
      this.m_StringDatas.Remove(stringIndex);
      this.WriteStringData(stringIndex, stringData);
      blockData = blockData.Free();
      this.m_BlockDatas[num] = blockData;
      if (!this.TryCombineFreeBlocks(num))
      {
        this.m_FreeBlockIndexes.Add(blockData.Length, num);
        this.WriteBlockData(num);
      }
      this.m_Stream.Flush();
      return true;
    }

    private void ProcessWriteFile(
      string name,
      bool hasFile,
      int oldBlockIndex,
      int blockIndex,
      int length)
    {
      GameFramework.FileSystem.FileSystem.BlockData blockData1 = this.m_BlockDatas[blockIndex];
      if (hasFile)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData2 = this.m_BlockDatas[oldBlockIndex];
        blockData1 = new GameFramework.FileSystem.FileSystem.BlockData(blockData2.StringIndex, blockData1.ClusterIndex, length);
        this.m_BlockDatas[blockIndex] = blockData1;
        this.WriteBlockData(blockIndex);
        blockData2 = blockData2.Free();
        this.m_BlockDatas[oldBlockIndex] = blockData2;
        if (!this.TryCombineFreeBlocks(oldBlockIndex))
        {
          this.m_FreeBlockIndexes.Add(blockData2.Length, oldBlockIndex);
          this.WriteBlockData(oldBlockIndex);
        }
      }
      else
      {
        blockData1 = new GameFramework.FileSystem.FileSystem.BlockData(this.AllocString(name), blockData1.ClusterIndex, length);
        this.m_BlockDatas[blockIndex] = blockData1;
        this.WriteBlockData(blockIndex);
      }
      if (hasFile)
        this.m_FileDatas[name] = blockIndex;
      else
        this.m_FileDatas.Add(name, blockIndex);
    }

    private bool TryCombineFreeBlocks(int freeBlockIndex)
    {
      GameFramework.FileSystem.FileSystem.BlockData blockData1 = this.m_BlockDatas[freeBlockIndex];
      if (blockData1.Length <= 0)
        return false;
      int num1 = -1;
      int num2 = -1;
      int num3 = blockData1.ClusterIndex + GameFramework.FileSystem.FileSystem.GetUpBoundClusterCount((long) blockData1.Length);
      foreach (KeyValuePair<int, GameFrameworkLinkedListRange<int>> freeBlockIndex1 in this.m_FreeBlockIndexes)
      {
        if (freeBlockIndex1.Key > 0)
        {
          int boundClusterCount = GameFramework.FileSystem.FileSystem.GetUpBoundClusterCount((long) freeBlockIndex1.Key);
          foreach (int index in freeBlockIndex1.Value)
          {
            GameFramework.FileSystem.FileSystem.BlockData blockData2 = this.m_BlockDatas[index];
            if (blockData2.ClusterIndex + boundClusterCount == blockData1.ClusterIndex)
              num1 = index;
            else if (blockData2.ClusterIndex == num3)
              num2 = index;
          }
        }
      }
      if (num1 < 0 && num2 < 0)
        return false;
      this.m_FreeBlockIndexes.Remove(blockData1.Length, freeBlockIndex);
      if (num1 >= 0)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData3 = this.m_BlockDatas[num1];
        this.m_FreeBlockIndexes.Remove(blockData3.Length, num1);
        blockData1 = new GameFramework.FileSystem.FileSystem.BlockData(blockData3.ClusterIndex, blockData3.Length + blockData1.Length);
        this.m_BlockDatas[num1] = GameFramework.FileSystem.FileSystem.BlockData.Empty;
        this.m_FreeBlockIndexes.Add(0, num1);
        this.WriteBlockData(num1);
      }
      if (num2 >= 0)
      {
        GameFramework.FileSystem.FileSystem.BlockData blockData4 = this.m_BlockDatas[num2];
        this.m_FreeBlockIndexes.Remove(blockData4.Length, num2);
        blockData1 = new GameFramework.FileSystem.FileSystem.BlockData(blockData1.ClusterIndex, blockData1.Length + blockData4.Length);
        this.m_BlockDatas[num2] = GameFramework.FileSystem.FileSystem.BlockData.Empty;
        this.m_FreeBlockIndexes.Add(0, num2);
        this.WriteBlockData(num2);
      }
      this.m_BlockDatas[freeBlockIndex] = blockData1;
      this.m_FreeBlockIndexes.Add(blockData1.Length, freeBlockIndex);
      this.WriteBlockData(freeBlockIndex);
      return true;
    }

    private int GetEmptyBlockIndex()
    {
      GameFrameworkLinkedListRange<int> range = new GameFrameworkLinkedListRange<int>();
      if (this.m_FreeBlockIndexes.TryGetValue(0, out range))
      {
        int emptyBlockIndex = range.First.Value;
        this.m_FreeBlockIndexes.Remove(0, emptyBlockIndex);
        return emptyBlockIndex;
      }
      if (this.m_BlockDatas.Count >= this.m_HeaderData.MaxBlockCount)
        return -1;
      int count = this.m_BlockDatas.Count;
      this.m_BlockDatas.Add(GameFramework.FileSystem.FileSystem.BlockData.Empty);
      this.WriteHeaderData();
      return count;
    }

    private int AllocBlock(int length)
    {
      if (length <= 0)
        return this.GetEmptyBlockIndex();
      length = (int) GameFramework.FileSystem.FileSystem.GetUpBoundClusterOffset((long) length);
      int key = -1;
      GameFrameworkLinkedListRange<int> frameworkLinkedListRange = new GameFrameworkLinkedListRange<int>();
      foreach (KeyValuePair<int, GameFrameworkLinkedListRange<int>> freeBlockIndex in this.m_FreeBlockIndexes)
      {
        if (freeBlockIndex.Key >= length && (key < 0 || key >= freeBlockIndex.Key))
        {
          key = freeBlockIndex.Key;
          frameworkLinkedListRange = freeBlockIndex.Value;
        }
      }
      if (key >= 0)
      {
        if (key > length && this.m_BlockDatas.Count >= this.m_HeaderData.MaxBlockCount)
          return -1;
        int num1 = frameworkLinkedListRange.First.Value;
        this.m_FreeBlockIndexes.Remove(key, num1);
        if (key > length)
        {
          GameFramework.FileSystem.FileSystem.BlockData blockData = this.m_BlockDatas[num1];
          this.m_BlockDatas[num1] = new GameFramework.FileSystem.FileSystem.BlockData(blockData.ClusterIndex, length);
          this.WriteBlockData(num1);
          int num2 = key - length;
          int emptyBlockIndex = this.GetEmptyBlockIndex();
          this.m_BlockDatas[emptyBlockIndex] = new GameFramework.FileSystem.FileSystem.BlockData(blockData.ClusterIndex + GameFramework.FileSystem.FileSystem.GetUpBoundClusterCount((long) length), num2);
          this.m_FreeBlockIndexes.Add(num2, emptyBlockIndex);
          this.WriteBlockData(emptyBlockIndex);
        }
        return num1;
      }
      int emptyBlockIndex1 = this.GetEmptyBlockIndex();
      if (emptyBlockIndex1 < 0)
        return -1;
      long length1 = this.m_Stream.Length;
      try
      {
        this.m_Stream.SetLength(length1 + (long) length);
      }
      catch
      {
        return -1;
      }
      this.m_BlockDatas[emptyBlockIndex1] = new GameFramework.FileSystem.FileSystem.BlockData(GameFramework.FileSystem.FileSystem.GetUpBoundClusterCount(length1), length);
      this.WriteBlockData(emptyBlockIndex1);
      return emptyBlockIndex1;
    }

    private int AllocString(string value)
    {
      GameFramework.FileSystem.FileSystem.StringData stringData1 = new GameFramework.FileSystem.FileSystem.StringData();
      int num = this.m_FreeStringIndexes.Count <= 0 ? this.m_StringDatas.Count : this.m_FreeStringIndexes.Dequeue();
      if (this.m_FreeStringDatas.Count > 0)
      {
        stringData1 = this.m_FreeStringDatas.Dequeue();
      }
      else
      {
        byte[] numArray = new byte[(int) byte.MaxValue];
        Utility.Random.GetRandomBytes(numArray);
        stringData1 = new GameFramework.FileSystem.FileSystem.StringData((byte) 0, numArray);
      }
      GameFramework.FileSystem.FileSystem.StringData stringData2 = stringData1.SetString(value, this.m_HeaderData.GetEncryptBytes());
      this.m_StringDatas.Add(num, stringData2);
      this.WriteStringData(num, stringData2);
      return num;
    }

    private void WriteHeaderData()
    {
      this.m_HeaderData = this.m_HeaderData.SetBlockCount(this.m_BlockDatas.Count);
      Utility.Marshal.StructureToBytes<GameFramework.FileSystem.FileSystem.HeaderData>(this.m_HeaderData, GameFramework.FileSystem.FileSystem.HeaderDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
      this.m_Stream.Position = 0L;
      this.m_Stream.Write(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.HeaderDataSize);
    }

    private void WriteBlockData(int blockIndex)
    {
      Utility.Marshal.StructureToBytes<GameFramework.FileSystem.FileSystem.BlockData>(this.m_BlockDatas[blockIndex], GameFramework.FileSystem.FileSystem.BlockDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
      this.m_Stream.Position = (long) (this.m_BlockDataOffset + GameFramework.FileSystem.FileSystem.BlockDataSize * blockIndex);
      this.m_Stream.Write(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.BlockDataSize);
    }

    private GameFramework.FileSystem.FileSystem.StringData ReadStringData(int stringIndex)
    {
      this.m_Stream.Position = (long) (this.m_StringDataOffset + GameFramework.FileSystem.FileSystem.StringDataSize * stringIndex);
      this.m_Stream.Read(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.StringDataSize);
      return Utility.Marshal.BytesToStructure<GameFramework.FileSystem.FileSystem.StringData>(GameFramework.FileSystem.FileSystem.StringDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
    }

    private void WriteStringData(int stringIndex, GameFramework.FileSystem.FileSystem.StringData stringData)
    {
      Utility.Marshal.StructureToBytes<GameFramework.FileSystem.FileSystem.StringData>(stringData, GameFramework.FileSystem.FileSystem.StringDataSize, GameFramework.FileSystem.FileSystem.s_CachedBytes);
      this.m_Stream.Position = (long) (this.m_StringDataOffset + GameFramework.FileSystem.FileSystem.StringDataSize * stringIndex);
      this.m_Stream.Write(GameFramework.FileSystem.FileSystem.s_CachedBytes, 0, GameFramework.FileSystem.FileSystem.StringDataSize);
    }

    private static void CalcOffsets(GameFramework.FileSystem.FileSystem fileSystem)
    {
      fileSystem.m_BlockDataOffset = GameFramework.FileSystem.FileSystem.HeaderDataSize;
      fileSystem.m_StringDataOffset = fileSystem.m_BlockDataOffset + GameFramework.FileSystem.FileSystem.BlockDataSize * fileSystem.m_HeaderData.MaxBlockCount;
      fileSystem.m_FileDataOffset = (int) GameFramework.FileSystem.FileSystem.GetUpBoundClusterOffset((long) (fileSystem.m_StringDataOffset + GameFramework.FileSystem.FileSystem.StringDataSize * fileSystem.m_HeaderData.MaxFileCount));
    }

    private static long GetUpBoundClusterOffset(long offset) => (offset - 1L + 4096L) / 4096L * 4096L;

    private static int GetUpBoundClusterCount(long length) => (int) ((length - 1L + 4096L) / 4096L);

    private static long GetClusterOffset(int clusterIndex) => 4096L * (long) clusterIndex;

    /// <summary>块数据。</summary>
    private struct BlockData
    {
      public static readonly GameFramework.FileSystem.FileSystem.BlockData Empty = new GameFramework.FileSystem.FileSystem.BlockData(0, 0);
      private readonly int m_StringIndex;
      private readonly int m_ClusterIndex;
      private readonly int m_Length;

      public BlockData(int clusterIndex, int length)
        : this(-1, clusterIndex, length)
      {
      }

      public BlockData(int stringIndex, int clusterIndex, int length)
      {
        this.m_StringIndex = stringIndex;
        this.m_ClusterIndex = clusterIndex;
        this.m_Length = length;
      }

      public bool Using => this.m_StringIndex >= 0;

      public int StringIndex => this.m_StringIndex;

      public int ClusterIndex => this.m_ClusterIndex;

      public int Length => this.m_Length;

      public GameFramework.FileSystem.FileSystem.BlockData Free() => new GameFramework.FileSystem.FileSystem.BlockData(this.m_ClusterIndex, (int) GameFramework.FileSystem.FileSystem.GetUpBoundClusterOffset((long) this.m_Length));
    }

    /// <summary>头数据。</summary>
    private struct HeaderData
    {
      private const int HeaderLength = 3;
      private const int FileSystemVersion = 0;
      private const int EncryptBytesLength = 4;
      private static readonly byte[] Header = new byte[3]
      {
        (byte) 71,
        (byte) 70,
        (byte) 70
      };
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
      private readonly byte[] m_Header;
      private readonly byte m_Version;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
      private readonly byte[] m_EncryptBytes;
      private readonly int m_MaxFileCount;
      private readonly int m_MaxBlockCount;
      private readonly int m_BlockCount;

      public HeaderData(int maxFileCount, int maxBlockCount)
        : this((byte) 0, new byte[4], maxFileCount, maxBlockCount, 0)
      {
        Utility.Random.GetRandomBytes(this.m_EncryptBytes);
      }

      public HeaderData(
        byte version,
        byte[] encryptBytes,
        int maxFileCount,
        int maxBlockCount,
        int blockCount)
      {
        this.m_Header = GameFramework.FileSystem.FileSystem.HeaderData.Header;
        this.m_Version = version;
        this.m_EncryptBytes = encryptBytes;
        this.m_MaxFileCount = maxFileCount;
        this.m_MaxBlockCount = maxBlockCount;
        this.m_BlockCount = blockCount;
      }

      public bool IsValid => this.m_Header.Length == 3 && (int) this.m_Header[0] == (int) GameFramework.FileSystem.FileSystem.HeaderData.Header[0] && (int) this.m_Header[1] == (int) GameFramework.FileSystem.FileSystem.HeaderData.Header[1] && (int) this.m_Header[2] == (int) GameFramework.FileSystem.FileSystem.HeaderData.Header[2] && this.m_Version == (byte) 0 && this.m_EncryptBytes.Length == 4 && this.m_MaxFileCount > 0 && this.m_MaxBlockCount > 0 && this.m_MaxFileCount <= this.m_MaxBlockCount && this.m_BlockCount > 0 && this.m_BlockCount <= this.m_MaxBlockCount;

      public byte Version => this.m_Version;

      public int MaxFileCount => this.m_MaxFileCount;

      public int MaxBlockCount => this.m_MaxBlockCount;

      public int BlockCount => this.m_BlockCount;

      public byte[] GetEncryptBytes() => this.m_EncryptBytes;

      public GameFramework.FileSystem.FileSystem.HeaderData SetBlockCount(int blockCount) => new GameFramework.FileSystem.FileSystem.HeaderData(this.m_Version, this.m_EncryptBytes, this.m_MaxFileCount, this.m_MaxBlockCount, blockCount);
    }

    /// <summary>字符串数据。</summary>
    private struct StringData
    {
      private static readonly byte[] s_CachedBytes = new byte[256];
      private readonly byte m_Length;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
      private readonly byte[] m_Bytes;

      public StringData(byte length, byte[] bytes)
      {
        this.m_Length = length;
        this.m_Bytes = bytes;
      }

      public string GetString(byte[] encryptBytes)
      {
        if (this.m_Length <= (byte) 0)
          return (string) null;
        Array.Copy((Array) this.m_Bytes, 0, (Array) GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes, 0, (int) this.m_Length);
        Utility.Encryption.GetSelfXorBytes(GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes, 0, (int) this.m_Length, encryptBytes);
        return Utility.Converter.GetString(GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes, 0, (int) this.m_Length);
      }

      public GameFramework.FileSystem.FileSystem.StringData SetString(
        string value,
        byte[] encryptBytes)
      {
        if (string.IsNullOrEmpty(value))
          return this.Clear();
        int bytes = Utility.Converter.GetBytes(value, GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes);
        if (bytes > (int) byte.MaxValue)
          throw new GameFrameworkException(Utility.Text.Format<string>("String '{0}' is too long.", value));
        Utility.Encryption.GetSelfXorBytes(GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes, encryptBytes);
        Array.Copy((Array) GameFramework.FileSystem.FileSystem.StringData.s_CachedBytes, 0, (Array) this.m_Bytes, 0, bytes);
        return new GameFramework.FileSystem.FileSystem.StringData((byte) bytes, this.m_Bytes);
      }

      public GameFramework.FileSystem.FileSystem.StringData Clear() => new GameFramework.FileSystem.FileSystem.StringData((byte) 0, this.m_Bytes);
    }
  }
}
