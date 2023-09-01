// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LocalVersionList
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
  /// <summary>本地版本资源列表。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct LocalVersionList
  {
    private static readonly LocalVersionList.Resource[] EmptyResourceArray = new LocalVersionList.Resource[0];
    private static readonly LocalVersionList.FileSystem[] EmptyFileSystemArray = new LocalVersionList.FileSystem[0];
    private readonly bool m_IsValid;
    private readonly LocalVersionList.Resource[] m_Resources;
    private readonly LocalVersionList.FileSystem[] m_FileSystems;

    /// <summary>初始化本地版本资源列表的新实例。</summary>
    /// <param name="resources">包含的资源集合。</param>
    /// <param name="fileSystems">包含的文件系统集合。</param>
    public LocalVersionList(
      LocalVersionList.Resource[] resources,
      LocalVersionList.FileSystem[] fileSystems)
    {
      this.m_IsValid = true;
      this.m_Resources = resources ?? LocalVersionList.EmptyResourceArray;
      this.m_FileSystems = fileSystems ?? LocalVersionList.EmptyFileSystemArray;
    }

    /// <summary>获取本地版本资源列表是否有效。</summary>
    public bool IsValid => this.m_IsValid;

    /// <summary>获取包含的资源集合。</summary>
    /// <returns>包含的资源集合。</returns>
    public LocalVersionList.Resource[] GetResources()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Resources;
    }

    /// <summary>获取包含的文件系统集合。</summary>
    /// <returns>包含的文件系统集合。</returns>
    public LocalVersionList.FileSystem[] GetFileSystems()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_FileSystems;
    }

    /// <summary>文件系统。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct FileSystem
    {
      private static readonly int[] EmptyIntArray = new int[0];
      private readonly string m_Name;
      private readonly int[] m_ResourceIndexes;

      /// <summary>初始化文件系统的新实例。</summary>
      /// <param name="name">文件系统名称。</param>
      /// <param name="resourceIndexes">文件系统包含的资源索引集合。</param>
      public FileSystem(string name, int[] resourceIndexes)
      {
        this.m_Name = name != null ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_ResourceIndexes = resourceIndexes ?? LocalVersionList.FileSystem.EmptyIntArray;
      }

      /// <summary>获取文件系统名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取文件系统包含的资源索引集合。</summary>
      /// <returns>文件系统包含的资源索引集合。</returns>
      public int[] GetResourceIndexes() => this.m_ResourceIndexes;
    }

    /// <summary>资源。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Resource
    {
      private readonly string m_Name;
      private readonly string m_Variant;
      private readonly string m_Extension;
      private readonly byte m_LoadType;
      private readonly int m_Length;
      private readonly int m_HashCode;

      /// <summary>初始化资源的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="variant">资源变体名称。</param>
      /// <param name="extension">资源扩展名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="length">资源长度。</param>
      /// <param name="hashCode">资源哈希值。</param>
      public Resource(
        string name,
        string variant,
        string extension,
        byte loadType,
        int length,
        int hashCode)
      {
        this.m_Name = !string.IsNullOrEmpty(name) ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_Variant = variant;
        this.m_Extension = extension;
        this.m_LoadType = loadType;
        this.m_Length = length;
        this.m_HashCode = hashCode;
      }

      /// <summary>获取资源名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源变体名称。</summary>
      public string Variant => this.m_Variant;

      /// <summary>获取资源扩展名称。</summary>
      public string Extension => this.m_Extension;

      /// <summary>获取资源加载方式。</summary>
      public byte LoadType => this.m_LoadType;

      /// <summary>获取资源长度。</summary>
      public int Length => this.m_Length;

      /// <summary>获取资源哈希值。</summary>
      public int HashCode => this.m_HashCode;
    }
  }
}
