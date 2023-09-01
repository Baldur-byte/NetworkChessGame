// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourcePackVersionList
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
  /// <summary>资源包版本资源列表。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct ResourcePackVersionList
  {
    private static readonly ResourcePackVersionList.Resource[] EmptyResourceArray = new ResourcePackVersionList.Resource[0];
    private readonly bool m_IsValid;
    private readonly int m_Offset;
    private readonly long m_Length;
    private readonly int m_HashCode;
    private readonly ResourcePackVersionList.Resource[] m_Resources;

    /// <summary>初始化资源包版本资源列表的新实例。</summary>
    /// <param name="offset">资源数据偏移。</param>
    /// <param name="length">资源数据长度。</param>
    /// <param name="hashCode">资源数据哈希值。</param>
    /// <param name="resources">包含的资源集合。</param>
    public ResourcePackVersionList(
      int offset,
      long length,
      int hashCode,
      ResourcePackVersionList.Resource[] resources)
    {
      this.m_IsValid = true;
      this.m_Offset = offset;
      this.m_Length = length;
      this.m_HashCode = hashCode;
      this.m_Resources = resources ?? ResourcePackVersionList.EmptyResourceArray;
    }

    /// <summary>获取资源包版本资源列表是否有效。</summary>
    public bool IsValid => this.m_IsValid;

    /// <summary>获取资源数据偏移。</summary>
    public int Offset
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Offset;
      }
    }

    /// <summary>获取资源数据长度。</summary>
    public long Length
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Length;
      }
    }

    /// <summary>获取资源数据哈希值。</summary>
    public int HashCode
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_HashCode;
      }
    }

    /// <summary>获取包含的资源集合。</summary>
    /// <returns>包含的资源集合。</returns>
    public ResourcePackVersionList.Resource[] GetResources()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Resources;
    }

    /// <summary>资源。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Resource
    {
      private readonly string m_Name;
      private readonly string m_Variant;
      private readonly string m_Extension;
      private readonly byte m_LoadType;
      private readonly long m_Offset;
      private readonly int m_Length;
      private readonly int m_HashCode;
      private readonly int m_CompressedLength;
      private readonly int m_CompressedHashCode;

      /// <summary>初始化资源的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="variant">资源变体名称。</param>
      /// <param name="extension">资源扩展名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="offset">资源偏移。</param>
      /// <param name="length">资源长度。</param>
      /// <param name="hashCode">资源哈希值。</param>
      /// <param name="compressedLength">资源压缩后长度。</param>
      /// <param name="compressedHashCode">资源压缩后哈希值。</param>
      public Resource(
        string name,
        string variant,
        string extension,
        byte loadType,
        long offset,
        int length,
        int hashCode,
        int compressedLength,
        int compressedHashCode)
      {
        this.m_Name = !string.IsNullOrEmpty(name) ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_Variant = variant;
        this.m_Extension = extension;
        this.m_LoadType = loadType;
        this.m_Offset = offset;
        this.m_Length = length;
        this.m_HashCode = hashCode;
        this.m_CompressedLength = compressedLength;
        this.m_CompressedHashCode = compressedHashCode;
      }

      /// <summary>获取资源名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源变体名称。</summary>
      public string Variant => this.m_Variant;

      /// <summary>获取资源扩展名称。</summary>
      public string Extension => this.m_Extension;

      /// <summary>获取资源加载方式。</summary>
      public byte LoadType => this.m_LoadType;

      /// <summary>获取资源偏移。</summary>
      public long Offset => this.m_Offset;

      /// <summary>获取资源长度。</summary>
      public int Length => this.m_Length;

      /// <summary>获取资源哈希值。</summary>
      public int HashCode => this.m_HashCode;

      /// <summary>获取资源压缩后长度。</summary>
      public int CompressedLength => this.m_CompressedLength;

      /// <summary>获取资源压缩后哈希值。</summary>
      public int CompressedHashCode => this.m_CompressedHashCode;
    }
  }
}
