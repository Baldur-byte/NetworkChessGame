// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdatableVersionList
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
  /// <summary>可更新模式版本资源列表。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct UpdatableVersionList
  {
    private static readonly UpdatableVersionList.Asset[] EmptyAssetArray = new UpdatableVersionList.Asset[0];
    private static readonly UpdatableVersionList.Resource[] EmptyResourceArray = new UpdatableVersionList.Resource[0];
    private static readonly UpdatableVersionList.FileSystem[] EmptyFileSystemArray = new UpdatableVersionList.FileSystem[0];
    private static readonly UpdatableVersionList.ResourceGroup[] EmptyResourceGroupArray = new UpdatableVersionList.ResourceGroup[0];
    private readonly bool m_IsValid;
    private readonly string m_ApplicableGameVersion;
    private readonly int m_InternalResourceVersion;
    private readonly UpdatableVersionList.Asset[] m_Assets;
    private readonly UpdatableVersionList.Resource[] m_Resources;
    private readonly UpdatableVersionList.FileSystem[] m_FileSystems;
    private readonly UpdatableVersionList.ResourceGroup[] m_ResourceGroups;

    /// <summary>初始化可更新模式版本资源列表的新实例。</summary>
    /// <param name="applicableGameVersion">适配的游戏版本号。</param>
    /// <param name="internalResourceVersion">内部资源版本号。</param>
    /// <param name="assets">包含的资源集合。</param>
    /// <param name="resources">包含的资源集合。</param>
    /// <param name="fileSystems">包含的文件系统集合。</param>
    /// <param name="resourceGroups">包含的资源组集合。</param>
    public UpdatableVersionList(
      string applicableGameVersion,
      int internalResourceVersion,
      UpdatableVersionList.Asset[] assets,
      UpdatableVersionList.Resource[] resources,
      UpdatableVersionList.FileSystem[] fileSystems,
      UpdatableVersionList.ResourceGroup[] resourceGroups)
    {
      this.m_IsValid = true;
      this.m_ApplicableGameVersion = applicableGameVersion;
      this.m_InternalResourceVersion = internalResourceVersion;
      this.m_Assets = assets ?? UpdatableVersionList.EmptyAssetArray;
      this.m_Resources = resources ?? UpdatableVersionList.EmptyResourceArray;
      this.m_FileSystems = fileSystems ?? UpdatableVersionList.EmptyFileSystemArray;
      this.m_ResourceGroups = resourceGroups ?? UpdatableVersionList.EmptyResourceGroupArray;
    }

    /// <summary>获取可更新模式版本资源列表是否有效。</summary>
    public bool IsValid => this.m_IsValid;

    /// <summary>获取适配的游戏版本号。</summary>
    public string ApplicableGameVersion
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_ApplicableGameVersion;
      }
    }

    /// <summary>获取内部资源版本号。</summary>
    public int InternalResourceVersion
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_InternalResourceVersion;
      }
    }

    /// <summary>获取包含的资源集合。</summary>
    /// <returns>包含的资源集合。</returns>
    public UpdatableVersionList.Asset[] GetAssets()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Assets;
    }

    /// <summary>获取包含的资源集合。</summary>
    /// <returns>包含的资源集合。</returns>
    public UpdatableVersionList.Resource[] GetResources()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Resources;
    }

    /// <summary>获取包含的文件系统集合。</summary>
    /// <returns>包含的文件系统集合。</returns>
    public UpdatableVersionList.FileSystem[] GetFileSystems()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_FileSystems;
    }

    /// <summary>获取包含的资源组集合。</summary>
    /// <returns>包含的资源组集合。</returns>
    public UpdatableVersionList.ResourceGroup[] GetResourceGroups()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_ResourceGroups;
    }

    /// <summary>资源。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Asset
    {
      private static readonly int[] EmptyIntArray = new int[0];
      private readonly string m_Name;
      private readonly int[] m_DependencyAssetIndexes;

      /// <summary>初始化资源的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="dependencyAssetIndexes">资源包含的依赖资源索引集合。</param>
      public Asset(string name, int[] dependencyAssetIndexes)
      {
        this.m_Name = !string.IsNullOrEmpty(name) ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_DependencyAssetIndexes = dependencyAssetIndexes ?? UpdatableVersionList.Asset.EmptyIntArray;
      }

      /// <summary>获取资源名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源包含的依赖资源索引集合。</summary>
      /// <returns>资源包含的依赖资源索引集合。</returns>
      public int[] GetDependencyAssetIndexes() => this.m_DependencyAssetIndexes;
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
        this.m_ResourceIndexes = resourceIndexes ?? UpdatableVersionList.FileSystem.EmptyIntArray;
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
      private static readonly int[] EmptyIntArray = new int[0];
      private readonly string m_Name;
      private readonly string m_Variant;
      private readonly string m_Extension;
      private readonly byte m_LoadType;
      private readonly int m_Length;
      private readonly int m_HashCode;
      private readonly int m_CompressedLength;
      private readonly int m_CompressedHashCode;
      private readonly int[] m_AssetIndexes;

      /// <summary>初始化资源的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="variant">资源变体名称。</param>
      /// <param name="extension">资源扩展名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="length">资源长度。</param>
      /// <param name="hashCode">资源哈希值。</param>
      /// <param name="compressedLength">资源压缩后长度。</param>
      /// <param name="compressedHashCode">资源压缩后哈希值。</param>
      /// <param name="assetIndexes">资源包含的资源索引集合。</param>
      public Resource(
        string name,
        string variant,
        string extension,
        byte loadType,
        int length,
        int hashCode,
        int compressedLength,
        int compressedHashCode,
        int[] assetIndexes)
      {
        this.m_Name = !string.IsNullOrEmpty(name) ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_Variant = variant;
        this.m_Extension = extension;
        this.m_LoadType = loadType;
        this.m_Length = length;
        this.m_HashCode = hashCode;
        this.m_CompressedLength = compressedLength;
        this.m_CompressedHashCode = compressedHashCode;
        this.m_AssetIndexes = assetIndexes ?? UpdatableVersionList.Resource.EmptyIntArray;
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

      /// <summary>获取资源压缩后长度。</summary>
      public int CompressedLength => this.m_CompressedLength;

      /// <summary>获取资源压缩后哈希值。</summary>
      public int CompressedHashCode => this.m_CompressedHashCode;

      /// <summary>获取资源包含的资源索引集合。</summary>
      /// <returns>资源包含的资源索引集合。</returns>
      public int[] GetAssetIndexes() => this.m_AssetIndexes;
    }

    /// <summary>资源组。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct ResourceGroup
    {
      private static readonly int[] EmptyIntArray = new int[0];
      private readonly string m_Name;
      private readonly int[] m_ResourceIndexes;

      /// <summary>初始化资源组的新实例。</summary>
      /// <param name="name">资源组名称。</param>
      /// <param name="resourceIndexes">资源组包含的资源索引集合。</param>
      public ResourceGroup(string name, int[] resourceIndexes)
      {
        this.m_Name = name != null ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_ResourceIndexes = resourceIndexes ?? UpdatableVersionList.ResourceGroup.EmptyIntArray;
      }

      /// <summary>获取资源组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源组包含的资源索引集合。</summary>
      /// <returns>资源组包含的资源索引集合。</returns>
      public int[] GetResourceIndexes() => this.m_ResourceIndexes;
    }
  }
}
