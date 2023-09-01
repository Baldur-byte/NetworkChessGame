// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.PackageVersionList
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
  /// <summary>单机模式版本资源列表。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct PackageVersionList
  {
    private static readonly PackageVersionList.Asset[] EmptyAssetArray = new PackageVersionList.Asset[0];
    private static readonly PackageVersionList.Resource[] EmptyResourceArray = new PackageVersionList.Resource[0];
    private static readonly PackageVersionList.FileSystem[] EmptyFileSystemArray = new PackageVersionList.FileSystem[0];
    private static readonly PackageVersionList.ResourceGroup[] EmptyResourceGroupArray = new PackageVersionList.ResourceGroup[0];
    private readonly bool m_IsValid;
    private readonly string m_ApplicableGameVersion;
    private readonly int m_InternalResourceVersion;
    private readonly PackageVersionList.Asset[] m_Assets;
    private readonly PackageVersionList.Resource[] m_Resources;
    private readonly PackageVersionList.FileSystem[] m_FileSystems;
    private readonly PackageVersionList.ResourceGroup[] m_ResourceGroups;

    /// <summary>初始化单机模式版本资源列表的新实例。</summary>
    /// <param name="applicableGameVersion">适配的游戏版本号。</param>
    /// <param name="internalResourceVersion">内部资源版本号。</param>
    /// <param name="assets">包含的资源集合。</param>
    /// <param name="resources">包含的资源集合。</param>
    /// <param name="fileSystems">包含的文件系统集合。</param>
    /// <param name="resourceGroups">包含的资源组集合。</param>
    public PackageVersionList(
      string applicableGameVersion,
      int internalResourceVersion,
      PackageVersionList.Asset[] assets,
      PackageVersionList.Resource[] resources,
      PackageVersionList.FileSystem[] fileSystems,
      PackageVersionList.ResourceGroup[] resourceGroups)
    {
      this.m_IsValid = true;
      this.m_ApplicableGameVersion = applicableGameVersion;
      this.m_InternalResourceVersion = internalResourceVersion;
      this.m_Assets = assets ?? PackageVersionList.EmptyAssetArray;
      this.m_Resources = resources ?? PackageVersionList.EmptyResourceArray;
      this.m_FileSystems = fileSystems ?? PackageVersionList.EmptyFileSystemArray;
      this.m_ResourceGroups = resourceGroups ?? PackageVersionList.EmptyResourceGroupArray;
    }

    /// <summary>获取单机模式版本资源列表是否有效。</summary>
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
    public PackageVersionList.Asset[] GetAssets()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Assets;
    }

    /// <summary>获取包含的资源集合。</summary>
    /// <returns>包含的资源集合。</returns>
    public PackageVersionList.Resource[] GetResources()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_Resources;
    }

    /// <summary>获取包含的文件系统集合。</summary>
    /// <returns>包含的文件系统集合。</returns>
    public PackageVersionList.FileSystem[] GetFileSystems()
    {
      if (!this.m_IsValid)
        throw new GameFrameworkException("Data is invalid.");
      return this.m_FileSystems;
    }

    /// <summary>获取包含的资源组集合。</summary>
    /// <returns>包含的资源组集合。</returns>
    public PackageVersionList.ResourceGroup[] GetResourceGroups()
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
        this.m_DependencyAssetIndexes = dependencyAssetIndexes ?? PackageVersionList.Asset.EmptyIntArray;
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
        this.m_ResourceIndexes = resourceIndexes ?? PackageVersionList.FileSystem.EmptyIntArray;
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
      private readonly int[] m_AssetIndexes;

      /// <summary>初始化资源的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="variant">资源变体名称。</param>
      /// <param name="extension">资源扩展名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="length">资源长度。</param>
      /// <param name="hashCode">资源哈希值。</param>
      /// <param name="assetIndexes">资源包含的资源索引集合。</param>
      public Resource(
        string name,
        string variant,
        string extension,
        byte loadType,
        int length,
        int hashCode,
        int[] assetIndexes)
      {
        this.m_Name = !string.IsNullOrEmpty(name) ? name : throw new GameFrameworkException("Name is invalid.");
        this.m_Variant = variant;
        this.m_Extension = extension;
        this.m_LoadType = loadType;
        this.m_Length = length;
        this.m_HashCode = hashCode;
        this.m_AssetIndexes = assetIndexes ?? PackageVersionList.Resource.EmptyIntArray;
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
        this.m_ResourceIndexes = resourceIndexes ?? PackageVersionList.ResourceGroup.EmptyIntArray;
      }

      /// <summary>获取资源组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源组包含的资源索引集合。</summary>
      /// <returns>资源组包含的资源索引集合。</returns>
      public int[] GetResourceIndexes() => this.m_ResourceIndexes;
    }
  }
}
