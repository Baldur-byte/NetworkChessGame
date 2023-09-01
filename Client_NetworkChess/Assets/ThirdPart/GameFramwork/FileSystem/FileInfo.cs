// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.FileInfo
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework.FileSystem
{
  /// <summary>文件信息。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct FileInfo
  {
    private readonly string m_Name;
    private readonly long m_Offset;
    private readonly int m_Length;

    /// <summary>初始化文件信息的新实例。</summary>
    /// <param name="name">文件名称。</param>
    /// <param name="offset">文件偏移。</param>
    /// <param name="length">文件长度。</param>
    public FileInfo(string name, long offset, int length)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Name is invalid.");
      if (offset < 0L)
        throw new GameFrameworkException("Offset is invalid.");
      if (length < 0)
        throw new GameFrameworkException("Length is invalid.");
      this.m_Name = name;
      this.m_Offset = offset;
      this.m_Length = length;
    }

    /// <summary>获取文件信息是否有效。</summary>
    public bool IsValid => !string.IsNullOrEmpty(this.m_Name) && this.m_Offset >= 0L && this.m_Length >= 0;

    /// <summary>获取文件名称。</summary>
    public string Name => this.m_Name;

    /// <summary>获取文件偏移。</summary>
    public long Offset => this.m_Offset;

    /// <summary>获取文件长度。</summary>
    public int Length => this.m_Length;
  }
}
