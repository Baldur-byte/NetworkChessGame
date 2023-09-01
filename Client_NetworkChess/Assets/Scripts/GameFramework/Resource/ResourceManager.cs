// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Download;
using GameFramework.FileSystem;
using GameFramework.ObjectPool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace GameFramework.Resource
{
  /// <summary>资源管理器。</summary>
  internal sealed class ResourceManager : GameFrameworkModule, IResourceManager
  {
    private const string RemoteVersionListFileName = "GameFrameworkVersion.dat";
    private const string LocalVersionListFileName = "GameFrameworkList.dat";
    private const string DefaultExtension = "dat";
    private const string TempExtension = "tmp";
    private const int FileSystemMaxFileCount = 16384;
    private const int FileSystemMaxBlockCount = 262144;
    private Dictionary<string, ResourceManager.AssetInfo> m_AssetInfos;
    private Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo> m_ResourceInfos;
    private SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo> m_ReadWriteResourceInfos;
    private readonly Dictionary<string, IFileSystem> m_ReadOnlyFileSystems;
    private readonly Dictionary<string, IFileSystem> m_ReadWriteFileSystems;
    private readonly Dictionary<string, ResourceManager.ResourceGroup> m_ResourceGroups;
    private PackageVersionListSerializer m_PackageVersionListSerializer;
    private UpdatableVersionListSerializer m_UpdatableVersionListSerializer;
    private ReadOnlyVersionListSerializer m_ReadOnlyVersionListSerializer;
    private ReadWriteVersionListSerializer m_ReadWriteVersionListSerializer;
    private ResourcePackVersionListSerializer m_ResourcePackVersionListSerializer;
    private IFileSystemManager m_FileSystemManager;
    private ResourceManager.ResourceIniter m_ResourceIniter;
    private ResourceManager.VersionListProcessor m_VersionListProcessor;
    private ResourceManager.ResourceVerifier m_ResourceVerifier;
    private ResourceManager.ResourceChecker m_ResourceChecker;
    private ResourceManager.ResourceUpdater m_ResourceUpdater;
    private ResourceManager.ResourceLoader m_ResourceLoader;
    private IResourceHelper m_ResourceHelper;
    private string m_ReadOnlyPath;
    private string m_ReadWritePath;
    private ResourceMode m_ResourceMode;
    private bool m_RefuseSetFlag;
    private string m_CurrentVariant;
    private string m_UpdatePrefixUri;
    private string m_ApplicableGameVersion;
    private int m_InternalResourceVersion;
    private MemoryStream m_DecompressCachedStream;
    private DecryptResourceCallback m_DecryptResourceCallback;
    private InitResourcesCompleteCallback m_InitResourcesCompleteCallback;
    private UpdateVersionListCallbacks m_UpdateVersionListCallbacks;
    private VerifyResourcesCompleteCallback m_VerifyResourcesCompleteCallback;
    private CheckResourcesCompleteCallback m_CheckResourcesCompleteCallback;
    private ApplyResourcesCompleteCallback m_ApplyResourcesCompleteCallback;
    private UpdateResourcesCompleteCallback m_UpdateResourcesCompleteCallback;
    private EventHandler<ResourceVerifyStartEventArgs> m_ResourceVerifyStartEventHandler;
    private EventHandler<ResourceVerifySuccessEventArgs> m_ResourceVerifySuccessEventHandler;
    private EventHandler<ResourceVerifyFailureEventArgs> m_ResourceVerifyFailureEventHandler;
    private EventHandler<ResourceApplyStartEventArgs> m_ResourceApplyStartEventHandler;
    private EventHandler<ResourceApplySuccessEventArgs> m_ResourceApplySuccessEventHandler;
    private EventHandler<ResourceApplyFailureEventArgs> m_ResourceApplyFailureEventHandler;
    private EventHandler<ResourceUpdateStartEventArgs> m_ResourceUpdateStartEventHandler;
    private EventHandler<ResourceUpdateChangedEventArgs> m_ResourceUpdateChangedEventHandler;
    private EventHandler<ResourceUpdateSuccessEventArgs> m_ResourceUpdateSuccessEventHandler;
    private EventHandler<ResourceUpdateFailureEventArgs> m_ResourceUpdateFailureEventHandler;
    private EventHandler<ResourceUpdateAllCompleteEventArgs> m_ResourceUpdateAllCompleteEventHandler;

    /// <summary>初始化资源管理器的新实例。</summary>
    public ResourceManager()
    {
      this.m_AssetInfos = (Dictionary<string, ResourceManager.AssetInfo>) null;
      this.m_ResourceInfos = (Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo>) null;
      this.m_ReadWriteResourceInfos = (SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo>) null;
      this.m_ReadOnlyFileSystems = new Dictionary<string, IFileSystem>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_ReadWriteFileSystems = new Dictionary<string, IFileSystem>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_ResourceGroups = new Dictionary<string, ResourceManager.ResourceGroup>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_PackageVersionListSerializer = (PackageVersionListSerializer) null;
      this.m_UpdatableVersionListSerializer = (UpdatableVersionListSerializer) null;
      this.m_ReadOnlyVersionListSerializer = (ReadOnlyVersionListSerializer) null;
      this.m_ReadWriteVersionListSerializer = (ReadWriteVersionListSerializer) null;
      this.m_ResourcePackVersionListSerializer = (ResourcePackVersionListSerializer) null;
      this.m_ResourceIniter = (ResourceManager.ResourceIniter) null;
      this.m_VersionListProcessor = (ResourceManager.VersionListProcessor) null;
      this.m_ResourceVerifier = (ResourceManager.ResourceVerifier) null;
      this.m_ResourceChecker = (ResourceManager.ResourceChecker) null;
      this.m_ResourceUpdater = (ResourceManager.ResourceUpdater) null;
      this.m_ResourceLoader = new ResourceManager.ResourceLoader(this);
      this.m_ResourceHelper = (IResourceHelper) null;
      this.m_ReadOnlyPath = (string) null;
      this.m_ReadWritePath = (string) null;
      this.m_ResourceMode = ResourceMode.Unspecified;
      this.m_RefuseSetFlag = false;
      this.m_CurrentVariant = (string) null;
      this.m_UpdatePrefixUri = (string) null;
      this.m_ApplicableGameVersion = (string) null;
      this.m_InternalResourceVersion = 0;
      this.m_DecompressCachedStream = (MemoryStream) null;
      this.m_DecryptResourceCallback = (DecryptResourceCallback) null;
      this.m_InitResourcesCompleteCallback = (InitResourcesCompleteCallback) null;
      this.m_UpdateVersionListCallbacks = (UpdateVersionListCallbacks) null;
      this.m_VerifyResourcesCompleteCallback = (VerifyResourcesCompleteCallback) null;
      this.m_CheckResourcesCompleteCallback = (CheckResourcesCompleteCallback) null;
      this.m_ApplyResourcesCompleteCallback = (ApplyResourcesCompleteCallback) null;
      this.m_UpdateResourcesCompleteCallback = (UpdateResourcesCompleteCallback) null;
      this.m_ResourceVerifySuccessEventHandler = (EventHandler<ResourceVerifySuccessEventArgs>) null;
      this.m_ResourceVerifyFailureEventHandler = (EventHandler<ResourceVerifyFailureEventArgs>) null;
      this.m_ResourceApplySuccessEventHandler = (EventHandler<ResourceApplySuccessEventArgs>) null;
      this.m_ResourceApplyFailureEventHandler = (EventHandler<ResourceApplyFailureEventArgs>) null;
      this.m_ResourceUpdateStartEventHandler = (EventHandler<ResourceUpdateStartEventArgs>) null;
      this.m_ResourceUpdateChangedEventHandler = (EventHandler<ResourceUpdateChangedEventArgs>) null;
      this.m_ResourceUpdateSuccessEventHandler = (EventHandler<ResourceUpdateSuccessEventArgs>) null;
      this.m_ResourceUpdateFailureEventHandler = (EventHandler<ResourceUpdateFailureEventArgs>) null;
      this.m_ResourceUpdateAllCompleteEventHandler = (EventHandler<ResourceUpdateAllCompleteEventArgs>) null;
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 3;

    /// <summary>获取资源只读区路径。</summary>
    public string ReadOnlyPath => this.m_ReadOnlyPath;

    /// <summary>获取资源读写区路径。</summary>
    public string ReadWritePath => this.m_ReadWritePath;

    /// <summary>获取资源模式。</summary>
    public ResourceMode ResourceMode => this.m_ResourceMode;

    /// <summary>获取当前变体。</summary>
    public string CurrentVariant => this.m_CurrentVariant;

    /// <summary>获取单机模式版本资源列表序列化器。</summary>
    public PackageVersionListSerializer PackageVersionListSerializer => this.m_PackageVersionListSerializer;

    /// <summary>获取可更新模式版本资源列表序列化器。</summary>
    public UpdatableVersionListSerializer UpdatableVersionListSerializer => this.m_UpdatableVersionListSerializer;

    /// <summary>获取本地只读区版本资源列表序列化器。</summary>
    public ReadOnlyVersionListSerializer ReadOnlyVersionListSerializer => this.m_ReadOnlyVersionListSerializer;

    /// <summary>获取本地读写区版本资源列表序列化器。</summary>
    public ReadWriteVersionListSerializer ReadWriteVersionListSerializer => this.m_ReadWriteVersionListSerializer;

    /// <summary>获取资源包版本资源列表序列化器。</summary>
    public ResourcePackVersionListSerializer ResourcePackVersionListSerializer => this.m_ResourcePackVersionListSerializer;

    /// <summary>获取当前资源适用的游戏版本号。</summary>
    public string ApplicableGameVersion => this.m_ApplicableGameVersion;

    /// <summary>获取当前内部资源版本号。</summary>
    public int InternalResourceVersion => this.m_InternalResourceVersion;

    /// <summary>获取资源数量。</summary>
    public int AssetCount => this.m_AssetInfos == null ? 0 : this.m_AssetInfos.Count;

    /// <summary>获取资源数量。</summary>
    public int ResourceCount => this.m_ResourceInfos == null ? 0 : this.m_ResourceInfos.Count;

    /// <summary>获取资源组数量。</summary>
    public int ResourceGroupCount => this.m_ResourceGroups.Count;

    /// <summary>获取或设置资源更新下载地址前缀。</summary>
    public string UpdatePrefixUri
    {
      get => this.m_UpdatePrefixUri;
      set => this.m_UpdatePrefixUri = value;
    }

    /// <summary>获取或设置每更新多少字节的资源，重新生成一次版本资源列表。</summary>
    public int GenerateReadWriteVersionListLength
    {
      get => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.GenerateReadWriteVersionListLength;
      set
      {
        if (this.m_ResourceUpdater == null)
          throw new GameFrameworkException("You can not use GenerateReadWriteVersionListLength at this time.");
        this.m_ResourceUpdater.GenerateReadWriteVersionListLength = value;
      }
    }

    /// <summary>获取正在应用的资源包路径。</summary>
    public string ApplyingResourcePackPath => this.m_ResourceUpdater == null ? (string) null : this.m_ResourceUpdater.ApplyingResourcePackPath;

    /// <summary>获取等待应用资源数量。</summary>
    public int ApplyWaitingCount => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.ApplyWaitingCount;

    /// <summary>获取或设置资源更新重试次数。</summary>
    public int UpdateRetryCount
    {
      get => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.UpdateRetryCount;
      set
      {
        if (this.m_ResourceUpdater == null)
          throw new GameFrameworkException("You can not use UpdateRetryCount at this time.");
        this.m_ResourceUpdater.UpdateRetryCount = value;
      }
    }

    /// <summary>获取正在更新的资源组。</summary>
    public IResourceGroup UpdatingResourceGroup => this.m_ResourceUpdater == null ? (IResourceGroup) null : this.m_ResourceUpdater.UpdatingResourceGroup;

    /// <summary>获取等待更新资源数量。</summary>
    public int UpdateWaitingCount => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.UpdateWaitingCount;

    /// <summary>获取使用时下载的等待更新资源数量。</summary>
    public int UpdateWaitingWhilePlayingCount => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.UpdateWaitingWhilePlayingCount;

    /// <summary>获取候选更新资源数量。</summary>
    public int UpdateCandidateCount => this.m_ResourceUpdater == null ? 0 : this.m_ResourceUpdater.UpdateCandidateCount;

    /// <summary>获取加载资源代理总数量。</summary>
    public int LoadTotalAgentCount => this.m_ResourceLoader.TotalAgentCount;

    /// <summary>获取可用加载资源代理数量。</summary>
    public int LoadFreeAgentCount => this.m_ResourceLoader.FreeAgentCount;

    /// <summary>获取工作中加载资源代理数量。</summary>
    public int LoadWorkingAgentCount => this.m_ResourceLoader.WorkingAgentCount;

    /// <summary>获取等待加载资源任务数量。</summary>
    public int LoadWaitingTaskCount => this.m_ResourceLoader.WaitingTaskCount;

    /// <summary>获取或设置资源对象池自动释放可释放对象的间隔秒数。</summary>
    public float AssetAutoReleaseInterval
    {
      get => this.m_ResourceLoader.AssetAutoReleaseInterval;
      set => this.m_ResourceLoader.AssetAutoReleaseInterval = value;
    }

    /// <summary>获取或设置资源对象池的容量。</summary>
    public int AssetCapacity
    {
      get => this.m_ResourceLoader.AssetCapacity;
      set => this.m_ResourceLoader.AssetCapacity = value;
    }

    /// <summary>获取或设置资源对象池对象过期秒数。</summary>
    public float AssetExpireTime
    {
      get => this.m_ResourceLoader.AssetExpireTime;
      set => this.m_ResourceLoader.AssetExpireTime = value;
    }

    /// <summary>获取或设置资源对象池的优先级。</summary>
    public int AssetPriority
    {
      get => this.m_ResourceLoader.AssetPriority;
      set => this.m_ResourceLoader.AssetPriority = value;
    }

    /// <summary>获取或设置资源对象池自动释放可释放对象的间隔秒数。</summary>
    public float ResourceAutoReleaseInterval
    {
      get => this.m_ResourceLoader.ResourceAutoReleaseInterval;
      set => this.m_ResourceLoader.ResourceAutoReleaseInterval = value;
    }

    /// <summary>获取或设置资源对象池的容量。</summary>
    public int ResourceCapacity
    {
      get => this.m_ResourceLoader.ResourceCapacity;
      set => this.m_ResourceLoader.ResourceCapacity = value;
    }

    /// <summary>获取或设置资源对象池对象过期秒数。</summary>
    public float ResourceExpireTime
    {
      get => this.m_ResourceLoader.ResourceExpireTime;
      set => this.m_ResourceLoader.ResourceExpireTime = value;
    }

    /// <summary>获取或设置资源对象池的优先级。</summary>
    public int ResourcePriority
    {
      get => this.m_ResourceLoader.ResourcePriority;
      set => this.m_ResourceLoader.ResourcePriority = value;
    }

    /// <summary>资源校验开始事件。</summary>
    public event EventHandler<ResourceVerifyStartEventArgs> ResourceVerifyStart
    {
      add => this.m_ResourceVerifyStartEventHandler += value;
      remove => this.m_ResourceVerifyStartEventHandler -= value;
    }

    /// <summary>资源校验成功事件。</summary>
    public event EventHandler<ResourceVerifySuccessEventArgs> ResourceVerifySuccess
    {
      add => this.m_ResourceVerifySuccessEventHandler += value;
      remove => this.m_ResourceVerifySuccessEventHandler -= value;
    }

    /// <summary>资源校验失败事件。</summary>
    public event EventHandler<ResourceVerifyFailureEventArgs> ResourceVerifyFailure
    {
      add => this.m_ResourceVerifyFailureEventHandler += value;
      remove => this.m_ResourceVerifyFailureEventHandler -= value;
    }

    /// <summary>资源应用开始事件。</summary>
    public event EventHandler<ResourceApplyStartEventArgs> ResourceApplyStart
    {
      add => this.m_ResourceApplyStartEventHandler += value;
      remove => this.m_ResourceApplyStartEventHandler -= value;
    }

    /// <summary>资源应用成功事件。</summary>
    public event EventHandler<ResourceApplySuccessEventArgs> ResourceApplySuccess
    {
      add => this.m_ResourceApplySuccessEventHandler += value;
      remove => this.m_ResourceApplySuccessEventHandler -= value;
    }

    /// <summary>资源应用失败事件。</summary>
    public event EventHandler<ResourceApplyFailureEventArgs> ResourceApplyFailure
    {
      add => this.m_ResourceApplyFailureEventHandler += value;
      remove => this.m_ResourceApplyFailureEventHandler -= value;
    }

    /// <summary>资源更新开始事件。</summary>
    public event EventHandler<ResourceUpdateStartEventArgs> ResourceUpdateStart
    {
      add => this.m_ResourceUpdateStartEventHandler += value;
      remove => this.m_ResourceUpdateStartEventHandler -= value;
    }

    /// <summary>资源更新改变事件。</summary>
    public event EventHandler<ResourceUpdateChangedEventArgs> ResourceUpdateChanged
    {
      add => this.m_ResourceUpdateChangedEventHandler += value;
      remove => this.m_ResourceUpdateChangedEventHandler -= value;
    }

    /// <summary>资源更新成功事件。</summary>
    public event EventHandler<ResourceUpdateSuccessEventArgs> ResourceUpdateSuccess
    {
      add => this.m_ResourceUpdateSuccessEventHandler += value;
      remove => this.m_ResourceUpdateSuccessEventHandler -= value;
    }

    /// <summary>资源更新失败事件。</summary>
    public event EventHandler<ResourceUpdateFailureEventArgs> ResourceUpdateFailure
    {
      add => this.m_ResourceUpdateFailureEventHandler += value;
      remove => this.m_ResourceUpdateFailureEventHandler -= value;
    }

    /// <summary>资源更新全部完成事件。</summary>
    public event EventHandler<ResourceUpdateAllCompleteEventArgs> ResourceUpdateAllComplete
    {
      add => this.m_ResourceUpdateAllCompleteEventHandler += value;
      remove => this.m_ResourceUpdateAllCompleteEventHandler -= value;
    }

    /// <summary>资源管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      if (this.m_ResourceVerifier != null)
      {
        this.m_ResourceVerifier.Update(elapseSeconds, realElapseSeconds);
      }
      else
      {
        if (this.m_ResourceUpdater != null)
          this.m_ResourceUpdater.Update(elapseSeconds, realElapseSeconds);
        this.m_ResourceLoader.Update(elapseSeconds, realElapseSeconds);
      }
    }

    /// <summary>关闭并清理资源管理器。</summary>
    internal override void Shutdown()
    {
      if (this.m_ResourceIniter != null)
      {
        this.m_ResourceIniter.Shutdown();
        this.m_ResourceIniter = (ResourceManager.ResourceIniter) null;
      }
      if (this.m_VersionListProcessor != null)
      {
        this.m_VersionListProcessor.VersionListUpdateSuccess -= new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateSuccess);
        this.m_VersionListProcessor.VersionListUpdateFailure -= new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateFailure);
        this.m_VersionListProcessor.Shutdown();
        this.m_VersionListProcessor = (ResourceManager.VersionListProcessor) null;
      }
      if (this.m_ResourceVerifier != null)
      {
        this.m_ResourceVerifier.ResourceVerifyStart -= new GameFrameworkAction<int, long>(this.OnVerifierResourceVerifyStart);
        this.m_ResourceVerifier.ResourceVerifySuccess -= new GameFrameworkAction<ResourceManager.ResourceName, int>(this.OnVerifierResourceVerifySuccess);
        this.m_ResourceVerifier.ResourceVerifyFailure -= new GameFrameworkAction<ResourceManager.ResourceName>(this.OnVerifierResourceVerifyFailure);
        this.m_ResourceVerifier.ResourceVerifyComplete -= new GameFrameworkAction<bool>(this.OnVerifierResourceVerifyComplete);
        this.m_ResourceVerifier.Shutdown();
        this.m_ResourceVerifier = (ResourceManager.ResourceVerifier) null;
      }
      if (this.m_ResourceChecker != null)
      {
        this.m_ResourceChecker.ResourceNeedUpdate -= new GameFrameworkAction<ResourceManager.ResourceName, string, ResourceManager.LoadType, int, int, int, int>(this.OnCheckerResourceNeedUpdate);
        this.m_ResourceChecker.ResourceCheckComplete -= new GameFrameworkAction<int, int, int, long, long>(this.OnCheckerResourceCheckComplete);
        this.m_ResourceChecker.Shutdown();
        this.m_ResourceChecker = (ResourceManager.ResourceChecker) null;
      }
      if (this.m_ResourceUpdater != null)
      {
        this.m_ResourceUpdater.ResourceApplyStart -= new GameFrameworkAction<string, int, long>(this.OnUpdaterResourceApplyStart);
        this.m_ResourceUpdater.ResourceApplySuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceApplySuccess);
        this.m_ResourceUpdater.ResourceApplyFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, string>(this.OnUpdaterResourceApplyFailure);
        this.m_ResourceUpdater.ResourceApplyComplete -= new GameFrameworkAction<string, bool>(this.OnUpdaterResourceApplyComplete);
        this.m_ResourceUpdater.ResourceUpdateStart -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int>(this.OnUpdaterResourceUpdateStart);
        this.m_ResourceUpdater.ResourceUpdateChanged -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateChanged);
        this.m_ResourceUpdater.ResourceUpdateSuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateSuccess);
        this.m_ResourceUpdater.ResourceUpdateFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string>(this.OnUpdaterResourceUpdateFailure);
        this.m_ResourceUpdater.ResourceUpdateComplete -= new GameFrameworkAction<ResourceManager.ResourceGroup, bool>(this.OnUpdaterResourceUpdateComplete);
        this.m_ResourceUpdater.ResourceUpdateAllComplete -= new GameFrameworkAction(this.OnUpdaterResourceUpdateAllComplete);
        this.m_ResourceUpdater.Shutdown();
        this.m_ResourceUpdater = (ResourceManager.ResourceUpdater) null;
        if (this.m_ReadWriteResourceInfos != null)
        {
          this.m_ReadWriteResourceInfos.Clear();
          this.m_ReadWriteResourceInfos = (SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo>) null;
        }
        if (this.m_DecompressCachedStream != null)
        {
          this.m_DecompressCachedStream.Dispose();
          this.m_DecompressCachedStream = (MemoryStream) null;
        }
      }
      if (this.m_ResourceLoader != null)
      {
        this.m_ResourceLoader.Shutdown();
        this.m_ResourceLoader = (ResourceManager.ResourceLoader) null;
      }
      if (this.m_AssetInfos != null)
      {
        this.m_AssetInfos.Clear();
        this.m_AssetInfos = (Dictionary<string, ResourceManager.AssetInfo>) null;
      }
      if (this.m_ResourceInfos != null)
      {
        this.m_ResourceInfos.Clear();
        this.m_ResourceInfos = (Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo>) null;
      }
      this.m_ReadOnlyFileSystems.Clear();
      this.m_ReadWriteFileSystems.Clear();
      this.m_ResourceGroups.Clear();
    }

    /// <summary>设置资源只读区路径。</summary>
    /// <param name="readOnlyPath">资源只读区路径。</param>
    public void SetReadOnlyPath(string readOnlyPath)
    {
      if (string.IsNullOrEmpty(readOnlyPath))
        throw new GameFrameworkException("Read-only path is invalid.");
      if (this.m_RefuseSetFlag)
        throw new GameFrameworkException("You can not set read-only path at this time.");
      if (this.m_ResourceLoader.TotalAgentCount > 0)
        throw new GameFrameworkException("You must set read-only path before add load resource agent helper.");
      this.m_ReadOnlyPath = readOnlyPath;
    }

    /// <summary>设置资源读写区路径。</summary>
    /// <param name="readWritePath">资源读写区路径。</param>
    public void SetReadWritePath(string readWritePath)
    {
      if (string.IsNullOrEmpty(readWritePath))
        throw new GameFrameworkException("Read-write path is invalid.");
      if (this.m_RefuseSetFlag)
        throw new GameFrameworkException("You can not set read-write path at this time.");
      if (this.m_ResourceLoader.TotalAgentCount > 0)
        throw new GameFrameworkException("You must set read-write path before add load resource agent helper.");
      this.m_ReadWritePath = readWritePath;
    }

    /// <summary>设置资源模式。</summary>
    /// <param name="resourceMode">资源模式。</param>
    public void SetResourceMode(ResourceMode resourceMode)
    {
      if (resourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("Resource mode is invalid.");
      if (this.m_RefuseSetFlag)
        throw new GameFrameworkException("You can not set resource mode at this time.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
      {
        this.m_ResourceMode = resourceMode;
        if (this.m_ResourceMode == ResourceMode.Package)
        {
          this.m_PackageVersionListSerializer = new PackageVersionListSerializer();
          this.m_ResourceIniter = new ResourceManager.ResourceIniter(this);
          this.m_ResourceIniter.ResourceInitComplete += new GameFrameworkAction(this.OnIniterResourceInitComplete);
        }
        else
        {
          if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
            return;
          this.m_UpdatableVersionListSerializer = new UpdatableVersionListSerializer();
          this.m_ReadOnlyVersionListSerializer = new ReadOnlyVersionListSerializer();
          this.m_ReadWriteVersionListSerializer = new ReadWriteVersionListSerializer();
          this.m_ResourcePackVersionListSerializer = new ResourcePackVersionListSerializer();
          this.m_VersionListProcessor = new ResourceManager.VersionListProcessor(this);
          this.m_VersionListProcessor.VersionListUpdateSuccess += new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateSuccess);
          this.m_VersionListProcessor.VersionListUpdateFailure += new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateFailure);
          this.m_ResourceChecker = new ResourceManager.ResourceChecker(this);
          this.m_ResourceChecker.ResourceNeedUpdate += new GameFrameworkAction<ResourceManager.ResourceName, string, ResourceManager.LoadType, int, int, int, int>(this.OnCheckerResourceNeedUpdate);
          this.m_ResourceChecker.ResourceCheckComplete += new GameFrameworkAction<int, int, int, long, long>(this.OnCheckerResourceCheckComplete);
          this.m_ResourceUpdater = new ResourceManager.ResourceUpdater(this);
          this.m_ResourceUpdater.ResourceApplyStart += new GameFrameworkAction<string, int, long>(this.OnUpdaterResourceApplyStart);
          this.m_ResourceUpdater.ResourceApplySuccess += new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceApplySuccess);
          this.m_ResourceUpdater.ResourceApplyFailure += new GameFrameworkAction<ResourceManager.ResourceName, string, string>(this.OnUpdaterResourceApplyFailure);
          this.m_ResourceUpdater.ResourceApplyComplete += new GameFrameworkAction<string, bool>(this.OnUpdaterResourceApplyComplete);
          this.m_ResourceUpdater.ResourceUpdateStart += new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int>(this.OnUpdaterResourceUpdateStart);
          this.m_ResourceUpdater.ResourceUpdateChanged += new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateChanged);
          this.m_ResourceUpdater.ResourceUpdateSuccess += new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateSuccess);
          this.m_ResourceUpdater.ResourceUpdateFailure += new GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string>(this.OnUpdaterResourceUpdateFailure);
          this.m_ResourceUpdater.ResourceUpdateComplete += new GameFrameworkAction<ResourceManager.ResourceGroup, bool>(this.OnUpdaterResourceUpdateComplete);
          this.m_ResourceUpdater.ResourceUpdateAllComplete += new GameFrameworkAction(this.OnUpdaterResourceUpdateAllComplete);
        }
      }
      else if (this.m_ResourceMode != resourceMode)
        throw new GameFrameworkException("You can not change resource mode at this time.");
    }

    /// <summary>设置当前变体。</summary>
    /// <param name="currentVariant">当前变体。</param>
    public void SetCurrentVariant(string currentVariant)
    {
      if (this.m_RefuseSetFlag)
        throw new GameFrameworkException("You can not set current variant at this time.");
      this.m_CurrentVariant = currentVariant;
    }

    /// <summary>设置对象池管理器。</summary>
    /// <param name="objectPoolManager">对象池管理器。</param>
    public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
    {
      if (objectPoolManager == null)
        throw new GameFrameworkException("Object pool manager is invalid.");
      this.m_ResourceLoader.SetObjectPoolManager(objectPoolManager);
    }

    /// <summary>设置文件系统管理器。</summary>
    /// <param name="fileSystemManager">文件系统管理器。</param>
    public void SetFileSystemManager(IFileSystemManager fileSystemManager) => this.m_FileSystemManager = fileSystemManager != null ? fileSystemManager : throw new GameFrameworkException("File system manager is invalid.");

    /// <summary>设置下载管理器。</summary>
    /// <param name="downloadManager">下载管理器。</param>
    public void SetDownloadManager(IDownloadManager downloadManager)
    {
      if (downloadManager == null)
        throw new GameFrameworkException("Download manager is invalid.");
      if (this.m_VersionListProcessor != null)
        this.m_VersionListProcessor.SetDownloadManager(downloadManager);
      if (this.m_ResourceUpdater == null)
        return;
      this.m_ResourceUpdater.SetDownloadManager(downloadManager);
    }

    /// <summary>设置解密资源回调函数。</summary>
    /// <param name="decryptResourceCallback">要设置的解密资源回调函数。</param>
    /// <remarks>如果不设置，将使用默认的解密资源回调函数。</remarks>
    public void SetDecryptResourceCallback(DecryptResourceCallback decryptResourceCallback)
    {
      if (this.m_ResourceLoader.TotalAgentCount > 0)
        throw new GameFrameworkException("You must set decrypt resource callback before add load resource agent helper.");
      this.m_DecryptResourceCallback = decryptResourceCallback;
    }

    /// <summary>设置资源辅助器。</summary>
    /// <param name="resourceHelper">资源辅助器。</param>
    public void SetResourceHelper(IResourceHelper resourceHelper)
    {
      if (resourceHelper == null)
        throw new GameFrameworkException("Resource helper is invalid.");
      if (this.m_ResourceLoader.TotalAgentCount > 0)
        throw new GameFrameworkException("You must set resource helper before add load resource agent helper.");
      this.m_ResourceHelper = resourceHelper;
    }

    /// <summary>增加加载资源代理辅助器。</summary>
    /// <param name="loadResourceAgentHelper">要增加的加载资源代理辅助器。</param>
    public void AddLoadResourceAgentHelper(ILoadResourceAgentHelper loadResourceAgentHelper)
    {
      if (this.m_ResourceHelper == null)
        throw new GameFrameworkException("Resource helper is invalid.");
      if (string.IsNullOrEmpty(this.m_ReadOnlyPath))
        throw new GameFrameworkException("Read-only path is invalid.");
      if (string.IsNullOrEmpty(this.m_ReadWritePath))
        throw new GameFrameworkException("Read-write path is invalid.");
      this.m_ResourceLoader.AddLoadResourceAgentHelper(loadResourceAgentHelper, this.m_ResourceHelper, this.m_ReadOnlyPath, this.m_ReadWritePath, this.m_DecryptResourceCallback);
    }

    /// <summary>使用单机模式并初始化资源。</summary>
    /// <param name="initResourcesCompleteCallback">使用单机模式并初始化资源完成时的回调函数。</param>
    public void InitResources(
      InitResourcesCompleteCallback initResourcesCompleteCallback)
    {
      if (initResourcesCompleteCallback == null)
        throw new GameFrameworkException("Init resources complete callback is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Package)
        throw new GameFrameworkException("You can not use InitResources without package resource mode.");
      if (this.m_ResourceIniter == null)
        throw new GameFrameworkException("You can not use InitResources at this time.");
      this.m_RefuseSetFlag = true;
      this.m_InitResourcesCompleteCallback = initResourcesCompleteCallback;
      this.m_ResourceIniter.InitResources(this.m_CurrentVariant);
    }

    /// <summary>使用可更新模式并检查版本资源列表。</summary>
    /// <param name="latestInternalResourceVersion">最新的内部资源版本号。</param>
    /// <returns>检查版本资源列表结果。</returns>
    public CheckVersionListResult CheckVersionList(int latestInternalResourceVersion)
    {
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use CheckVersionList without updatable resource mode.");
      return this.m_VersionListProcessor != null ? this.m_VersionListProcessor.CheckVersionList(latestInternalResourceVersion) : throw new GameFrameworkException("You can not use CheckVersionList at this time.");
    }

    /// <summary>使用可更新模式并更新版本资源列表。</summary>
    /// <param name="versionListLength">版本资源列表大小。</param>
    /// <param name="versionListHashCode">版本资源列表哈希值。</param>
    /// <param name="versionListCompressedLength">版本资源列表压缩后大小。</param>
    /// <param name="versionListCompressedHashCode">版本资源列表压缩后哈希值。</param>
    /// <param name="updateVersionListCallbacks">版本资源列表更新回调函数集。</param>
    public void UpdateVersionList(
      int versionListLength,
      int versionListHashCode,
      int versionListCompressedLength,
      int versionListCompressedHashCode,
      UpdateVersionListCallbacks updateVersionListCallbacks)
    {
      if (updateVersionListCallbacks == null)
        throw new GameFrameworkException("Update version list callbacks is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use UpdateVersionList without updatable resource mode.");
      if (this.m_VersionListProcessor == null)
        throw new GameFrameworkException("You can not use UpdateVersionList at this time.");
      this.m_UpdateVersionListCallbacks = updateVersionListCallbacks;
      this.m_VersionListProcessor.UpdateVersionList(versionListLength, versionListHashCode, versionListCompressedLength, versionListCompressedHashCode);
    }

    /// <summary>使用可更新模式并校验资源。</summary>
    /// <param name="verifyResourceLengthPerFrame">每帧至少校验资源的大小，以字节为单位。</param>
    /// <param name="verifyResourcesCompleteCallback">使用可更新模式并校验资源完成时的回调函数。</param>
    public void VerifyResources(
      int verifyResourceLengthPerFrame,
      VerifyResourcesCompleteCallback verifyResourcesCompleteCallback)
    {
      if (verifyResourcesCompleteCallback == null)
        throw new GameFrameworkException("Verify resources complete callback is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use VerifyResources without updatable resource mode.");
      if (this.m_RefuseSetFlag)
        throw new GameFrameworkException("You can not verify resources at this time.");
      this.m_ResourceVerifier = new ResourceManager.ResourceVerifier(this);
      this.m_ResourceVerifier.ResourceVerifyStart += new GameFrameworkAction<int, long>(this.OnVerifierResourceVerifyStart);
      this.m_ResourceVerifier.ResourceVerifySuccess += new GameFrameworkAction<ResourceManager.ResourceName, int>(this.OnVerifierResourceVerifySuccess);
      this.m_ResourceVerifier.ResourceVerifyFailure += new GameFrameworkAction<ResourceManager.ResourceName>(this.OnVerifierResourceVerifyFailure);
      this.m_ResourceVerifier.ResourceVerifyComplete += new GameFrameworkAction<bool>(this.OnVerifierResourceVerifyComplete);
      this.m_VerifyResourcesCompleteCallback = verifyResourcesCompleteCallback;
      this.m_ResourceVerifier.VerifyResources(verifyResourceLengthPerFrame);
    }

    /// <summary>使用可更新模式并检查资源。</summary>
    /// <param name="ignoreOtherVariant">是否忽略处理其它变体的资源，若不忽略，将会移除其它变体的资源。</param>
    /// <param name="checkResourcesCompleteCallback">使用可更新模式并检查资源完成时的回调函数。</param>
    public void CheckResources(
      bool ignoreOtherVariant,
      CheckResourcesCompleteCallback checkResourcesCompleteCallback)
    {
      if (checkResourcesCompleteCallback == null)
        throw new GameFrameworkException("Check resources complete callback is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use CheckResources without updatable resource mode.");
      if (this.m_ResourceChecker == null)
        throw new GameFrameworkException("You can not use CheckResources at this time.");
      this.m_RefuseSetFlag = true;
      this.m_CheckResourcesCompleteCallback = checkResourcesCompleteCallback;
      this.m_ResourceChecker.CheckResources(this.m_CurrentVariant, ignoreOtherVariant);
    }

    /// <summary>使用可更新模式并应用资源包资源。</summary>
    /// <param name="resourcePackPath">要应用的资源包路径。</param>
    /// <param name="applyResourcesCompleteCallback">使用可更新模式并应用资源包资源完成时的回调函数。</param>
    public void ApplyResources(
      string resourcePackPath,
      ApplyResourcesCompleteCallback applyResourcesCompleteCallback)
    {
      if (string.IsNullOrEmpty(resourcePackPath))
        throw new GameFrameworkException("Resource pack path is invalid.");
      if (!File.Exists(resourcePackPath))
        throw new GameFrameworkException(Utility.Text.Format<string>("Resource pack '{0}' is not exist.", resourcePackPath));
      if (applyResourcesCompleteCallback == null)
        throw new GameFrameworkException("Apply resources complete callback is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use ApplyResources without updatable resource mode.");
      if (this.m_ResourceUpdater == null)
        throw new GameFrameworkException("You can not use ApplyResources at this time.");
      this.m_ApplyResourcesCompleteCallback = applyResourcesCompleteCallback;
      this.m_ResourceUpdater.ApplyResources(resourcePackPath);
    }

    /// <summary>使用可更新模式并更新所有资源。</summary>
    /// <param name="updateResourcesCompleteCallback">使用可更新模式并更新默认资源组完成时的回调函数。</param>
    public void UpdateResources(
      UpdateResourcesCompleteCallback updateResourcesCompleteCallback)
    {
      this.UpdateResources(string.Empty, updateResourcesCompleteCallback);
    }

    /// <summary>使用可更新模式并更新指定资源组的资源。</summary>
    /// <param name="resourceGroupName">要更新的资源组名称。</param>
    /// <param name="updateResourcesCompleteCallback">使用可更新模式并更新指定资源组完成时的回调函数。</param>
    public void UpdateResources(
      string resourceGroupName,
      UpdateResourcesCompleteCallback updateResourcesCompleteCallback)
    {
      if (updateResourcesCompleteCallback == null)
        throw new GameFrameworkException("Update resources complete callback is invalid.");
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use UpdateResources without updatable resource mode.");
      if (this.m_ResourceUpdater == null)
        throw new GameFrameworkException("You can not use UpdateResources at this time.");
      ResourceManager.ResourceGroup resourceGroup = (ResourceManager.ResourceGroup) this.GetResourceGroup(resourceGroupName);
      if (resourceGroup == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Can not find resource group '{0}'.", resourceGroupName));
      this.m_UpdateResourcesCompleteCallback = updateResourcesCompleteCallback;
      this.m_ResourceUpdater.UpdateResources(resourceGroup);
    }

    /// <summary>停止更新资源。</summary>
    public void StopUpdateResources()
    {
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use StopUpdateResources without updatable resource mode.");
      if (this.m_ResourceUpdater == null)
        throw new GameFrameworkException("You can not use StopUpdateResources at this time.");
      this.m_ResourceUpdater.StopUpdateResources();
      this.m_UpdateResourcesCompleteCallback = (UpdateResourcesCompleteCallback) null;
    }

    /// <summary>校验资源包。</summary>
    /// <param name="resourcePackPath">要校验的资源包路径。</param>
    /// <returns>是否校验资源包成功。</returns>
    public bool VerifyResourcePack(string resourcePackPath)
    {
      if (string.IsNullOrEmpty(resourcePackPath))
        throw new GameFrameworkException("Resource pack path is invalid.");
      if (!File.Exists(resourcePackPath))
        throw new GameFrameworkException(Utility.Text.Format<string>("Resource pack '{0}' is not exist.", resourcePackPath));
      if (this.m_ResourceMode == ResourceMode.Unspecified)
        throw new GameFrameworkException("You must set resource mode first.");
      if (this.m_ResourceMode != ResourceMode.Updatable && this.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
        throw new GameFrameworkException("You can not use VerifyResourcePack without updatable resource mode.");
      if (this.m_ResourcePackVersionListSerializer == null)
        throw new GameFrameworkException("You can not use VerifyResourcePack at this time.");
      try
      {
        long num1 = 0;
        ResourcePackVersionList resourcePackVersionList = new ResourcePackVersionList();
        using (FileStream fileStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read))
        {
          num1 = fileStream.Length;
          resourcePackVersionList = this.m_ResourcePackVersionListSerializer.Deserialize((Stream) fileStream);
        }
        if (!resourcePackVersionList.IsValid || (long) resourcePackVersionList.Offset + resourcePackVersionList.Length != num1)
          return false;
        int num2 = 0;
        using (FileStream fileStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read))
        {
          fileStream.Position = (long) resourcePackVersionList.Offset;
          num2 = Utility.Verifier.GetCrc32((Stream) fileStream);
        }
        return resourcePackVersionList.HashCode == num2;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>获取所有加载资源任务的信息。</summary>
    /// <returns>所有加载资源任务的信息。</returns>
    public TaskInfo[] GetAllLoadAssetInfos() => this.m_ResourceLoader.GetAllLoadAssetInfos();

    /// <summary>获取所有加载资源任务的信息。</summary>
    /// <param name="results">所有加载资源任务的信息。</param>
    public void GetAllLoadAssetInfos(List<TaskInfo> results) => this.m_ResourceLoader.GetAllLoadAssetInfos(results);

    /// <summary>检查资源是否存在。</summary>
    /// <param name="assetName">要检查资源的名称。</param>
    /// <returns>检查资源是否存在的结果。</returns>
    public HasAssetResult HasAsset(string assetName) => !string.IsNullOrEmpty(assetName) ? this.m_ResourceLoader.HasAsset(assetName) : throw new GameFrameworkException("Asset name is invalid.");

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    public void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, (Type) null, 0, loadAssetCallbacks, (object) null);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="assetType">要加载资源的类型。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    public void LoadAsset(string assetName, Type assetType, LoadAssetCallbacks loadAssetCallbacks)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, assetType, 0, loadAssetCallbacks, (object) null);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="priority">加载资源的优先级。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    public void LoadAsset(string assetName, int priority, LoadAssetCallbacks loadAssetCallbacks)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, (Type) null, priority, loadAssetCallbacks, (object) null);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadAsset(string assetName, LoadAssetCallbacks loadAssetCallbacks, object userData)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, (Type) null, 0, loadAssetCallbacks, userData);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="assetType">要加载资源的类型。</param>
    /// <param name="priority">加载资源的优先级。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    public void LoadAsset(
      string assetName,
      Type assetType,
      int priority,
      LoadAssetCallbacks loadAssetCallbacks)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, assetType, priority, loadAssetCallbacks, (object) null);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="assetType">要加载资源的类型。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadAsset(
      string assetName,
      Type assetType,
      LoadAssetCallbacks loadAssetCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, assetType, 0, loadAssetCallbacks, userData);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="priority">加载资源的优先级。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadAsset(
      string assetName,
      int priority,
      LoadAssetCallbacks loadAssetCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, (Type) null, priority, loadAssetCallbacks, userData);
    }

    /// <summary>异步加载资源。</summary>
    /// <param name="assetName">要加载资源的名称。</param>
    /// <param name="assetType">要加载资源的类型。</param>
    /// <param name="priority">加载资源的优先级。</param>
    /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadAsset(
      string assetName,
      Type assetType,
      int priority,
      LoadAssetCallbacks loadAssetCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (loadAssetCallbacks == null)
        throw new GameFrameworkException("Load asset callbacks is invalid.");
      this.m_ResourceLoader.LoadAsset(assetName, assetType, priority, loadAssetCallbacks, userData);
    }

    /// <summary>卸载资源。</summary>
    /// <param name="asset">要卸载的资源。</param>
    public void UnloadAsset(object asset)
    {
      if (asset == null)
        throw new GameFrameworkException("Asset is invalid.");
      if (this.m_ResourceLoader == null)
        return;
      this.m_ResourceLoader.UnloadAsset(asset);
    }

    /// <summary>异步加载场景。</summary>
    /// <param name="sceneAssetName">要加载场景资源的名称。</param>
    /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
    public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (loadSceneCallbacks == null)
        throw new GameFrameworkException("Load scene callbacks is invalid.");
      this.m_ResourceLoader.LoadScene(sceneAssetName, 0, loadSceneCallbacks, (object) null);
    }

    /// <summary>异步加载场景。</summary>
    /// <param name="sceneAssetName">要加载场景资源的名称。</param>
    /// <param name="priority">加载场景资源的优先级。</param>
    /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
    public void LoadScene(
      string sceneAssetName,
      int priority,
      LoadSceneCallbacks loadSceneCallbacks)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (loadSceneCallbacks == null)
        throw new GameFrameworkException("Load scene callbacks is invalid.");
      this.m_ResourceLoader.LoadScene(sceneAssetName, priority, loadSceneCallbacks, (object) null);
    }

    /// <summary>异步加载场景。</summary>
    /// <param name="sceneAssetName">要加载场景资源的名称。</param>
    /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadScene(
      string sceneAssetName,
      LoadSceneCallbacks loadSceneCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (loadSceneCallbacks == null)
        throw new GameFrameworkException("Load scene callbacks is invalid.");
      this.m_ResourceLoader.LoadScene(sceneAssetName, 0, loadSceneCallbacks, userData);
    }

    /// <summary>异步加载场景。</summary>
    /// <param name="sceneAssetName">要加载场景资源的名称。</param>
    /// <param name="priority">加载场景资源的优先级。</param>
    /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadScene(
      string sceneAssetName,
      int priority,
      LoadSceneCallbacks loadSceneCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (loadSceneCallbacks == null)
        throw new GameFrameworkException("Load scene callbacks is invalid.");
      this.m_ResourceLoader.LoadScene(sceneAssetName, priority, loadSceneCallbacks, userData);
    }

    /// <summary>异步卸载场景。</summary>
    /// <param name="sceneAssetName">要卸载场景资源的名称。</param>
    /// <param name="unloadSceneCallbacks">卸载场景回调函数集。</param>
    public void UnloadScene(string sceneAssetName, UnloadSceneCallbacks unloadSceneCallbacks)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (unloadSceneCallbacks == null)
        throw new GameFrameworkException("Unload scene callbacks is invalid.");
      this.m_ResourceLoader.UnloadScene(sceneAssetName, unloadSceneCallbacks, (object) null);
    }

    /// <summary>异步卸载场景。</summary>
    /// <param name="sceneAssetName">要卸载场景资源的名称。</param>
    /// <param name="unloadSceneCallbacks">卸载场景回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void UnloadScene(
      string sceneAssetName,
      UnloadSceneCallbacks unloadSceneCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (unloadSceneCallbacks == null)
        throw new GameFrameworkException("Unload scene callbacks is invalid.");
      this.m_ResourceLoader.UnloadScene(sceneAssetName, unloadSceneCallbacks, userData);
    }

    /// <summary>获取二进制资源的实际路径。</summary>
    /// <param name="binaryAssetName">要获取实际路径的二进制资源的名称。</param>
    /// <returns>二进制资源的实际路径。</returns>
    /// <remarks>此方法仅适用于二进制资源存储在磁盘（而非文件系统）中的情况。若二进制资源存储在文件系统中时，返回值将始终为空。</remarks>
    public string GetBinaryPath(string binaryAssetName) => !string.IsNullOrEmpty(binaryAssetName) ? this.m_ResourceLoader.GetBinaryPath(binaryAssetName) : throw new GameFrameworkException("Binary asset name is invalid.");

    /// <summary>获取二进制资源的实际路径。</summary>
    /// <param name="binaryAssetName">要获取实际路径的二进制资源的名称。</param>
    /// <param name="storageInReadOnly">二进制资源是否存储在只读区中。</param>
    /// <param name="storageInFileSystem">二进制资源是否存储在文件系统中。</param>
    /// <param name="relativePath">二进制资源或存储二进制资源的文件系统，相对于只读区或者读写区的相对路径。</param>
    /// <param name="fileName">若二进制资源存储在文件系统中，则指示二进制资源在文件系统中的名称，否则此参数返回空。</param>
    /// <returns>是否获取二进制资源的实际路径成功。</returns>
    public bool GetBinaryPath(
      string binaryAssetName,
      out bool storageInReadOnly,
      out bool storageInFileSystem,
      out string relativePath,
      out string fileName)
    {
      return this.m_ResourceLoader.GetBinaryPath(binaryAssetName, out storageInReadOnly, out storageInFileSystem, out relativePath, out fileName);
    }

    /// <summary>获取二进制资源的长度。</summary>
    /// <param name="binaryAssetName">要获取长度的二进制资源的名称。</param>
    /// <returns>二进制资源的长度。</returns>
    public int GetBinaryLength(string binaryAssetName) => this.m_ResourceLoader.GetBinaryLength(binaryAssetName);

    /// <summary>异步加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <param name="loadBinaryCallbacks">加载二进制资源回调函数集。</param>
    public void LoadBinary(string binaryAssetName, LoadBinaryCallbacks loadBinaryCallbacks)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (loadBinaryCallbacks == null)
        throw new GameFrameworkException("Load binary callbacks is invalid.");
      this.m_ResourceLoader.LoadBinary(binaryAssetName, loadBinaryCallbacks, (object) null);
    }

    /// <summary>异步加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <param name="loadBinaryCallbacks">加载二进制资源回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadBinary(
      string binaryAssetName,
      LoadBinaryCallbacks loadBinaryCallbacks,
      object userData)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (loadBinaryCallbacks == null)
        throw new GameFrameworkException("Load binary callbacks is invalid.");
      this.m_ResourceLoader.LoadBinary(binaryAssetName, loadBinaryCallbacks, userData);
    }

    /// <summary>从文件系统中加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <returns>存储加载二进制资源的二进制流。</returns>
    public byte[] LoadBinaryFromFileSystem(string binaryAssetName) => !string.IsNullOrEmpty(binaryAssetName) ? this.m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName) : throw new GameFrameworkException("Binary asset name is invalid.");

    /// <summary>从文件系统中加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源的二进制流。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, 0, buffer.Length);
    }

    /// <summary>从文件系统中加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源的二进制流。</param>
    /// <param name="startIndex">存储加载二进制资源的二进制流的起始位置。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinaryFromFileSystem(string binaryAssetName, byte[] buffer, int startIndex)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, startIndex, buffer.Length - startIndex);
    }

    /// <summary>从文件系统中加载二进制资源。</summary>
    /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源的二进制流。</param>
    /// <param name="startIndex">存储加载二进制资源的二进制流的起始位置。</param>
    /// <param name="length">存储加载二进制资源的二进制流的长度。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinaryFromFileSystem(
      string binaryAssetName,
      byte[] buffer,
      int startIndex,
      int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinaryFromFileSystem(binaryAssetName, buffer, startIndex, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>存储加载二进制资源片段内容的二进制流。</returns>
    public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="offset">要加载片段的偏移。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>存储加载二进制资源片段内容的二进制流。</returns>
    public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, 0, buffer.Length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(string binaryAssetName, byte[] buffer, int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, 0, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <param name="startIndex">存储加载二进制资源片段内容的二进制流的起始位置。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(
      string binaryAssetName,
      byte[] buffer,
      int startIndex,
      int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, 0, buffer, startIndex, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="offset">要加载片段的偏移。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, byte[] buffer)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, 0, buffer.Length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="offset">要加载片段的偏移。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(
      string binaryAssetName,
      int offset,
      byte[] buffer,
      int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, 0, length);
    }

    /// <summary>从文件系统中加载二进制资源的片段。</summary>
    /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
    /// <param name="offset">要加载片段的偏移。</param>
    /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
    /// <param name="startIndex">存储加载二进制资源片段内容的二进制流的起始位置。</param>
    /// <param name="length">要加载片段的长度。</param>
    /// <returns>实际加载了多少字节。</returns>
    public int LoadBinarySegmentFromFileSystem(
      string binaryAssetName,
      int offset,
      byte[] buffer,
      int startIndex,
      int length)
    {
      if (string.IsNullOrEmpty(binaryAssetName))
        throw new GameFrameworkException("Binary asset name is invalid.");
      if (buffer == null)
        throw new GameFrameworkException("Buffer is invalid.");
      return this.m_ResourceLoader.LoadBinarySegmentFromFileSystem(binaryAssetName, offset, buffer, startIndex, length);
    }

    /// <summary>检查资源组是否存在。</summary>
    /// <param name="resourceGroupName">要检查资源组的名称。</param>
    /// <returns>资源组是否存在。</returns>
    public bool HasResourceGroup(string resourceGroupName) => this.m_ResourceGroups.ContainsKey(resourceGroupName ?? string.Empty);

    /// <summary>获取默认资源组。</summary>
    /// <returns>默认资源组。</returns>
    public IResourceGroup GetResourceGroup() => this.GetResourceGroup(string.Empty);

    /// <summary>获取资源组。</summary>
    /// <param name="resourceGroupName">要获取的资源组名称。</param>
    /// <returns>要获取的资源组。</returns>
    public IResourceGroup GetResourceGroup(string resourceGroupName)
    {
      ResourceManager.ResourceGroup resourceGroup = (ResourceManager.ResourceGroup) null;
      return this.m_ResourceGroups.TryGetValue(resourceGroupName ?? string.Empty, out resourceGroup) ? (IResourceGroup) resourceGroup : (IResourceGroup) null;
    }

    /// <summary>获取所有资源组。</summary>
    /// <returns>所有资源组。</returns>
    public IResourceGroup[] GetAllResourceGroups()
    {
      int num = 0;
      IResourceGroup[] allResourceGroups = new IResourceGroup[this.m_ResourceGroups.Count];
      foreach (KeyValuePair<string, ResourceManager.ResourceGroup> resourceGroup in this.m_ResourceGroups)
        allResourceGroups[num++] = (IResourceGroup) resourceGroup.Value;
      return allResourceGroups;
    }

    /// <summary>获取所有资源组。</summary>
    /// <param name="results">所有资源组。</param>
    public void GetAllResourceGroups(List<IResourceGroup> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, ResourceManager.ResourceGroup> resourceGroup in this.m_ResourceGroups)
        results.Add((IResourceGroup) resourceGroup.Value);
    }

    /// <summary>获取资源组集合。</summary>
    /// <param name="resourceGroupNames">要获取的资源组名称的集合。</param>
    /// <returns>要获取的资源组集合。</returns>
    public IResourceGroupCollection GetResourceGroupCollection(params string[] resourceGroupNames)
    {
      ResourceManager.ResourceGroup[] resourceGroups = resourceGroupNames != null && resourceGroupNames.Length >= 1 ? new ResourceManager.ResourceGroup[resourceGroupNames.Length] : throw new GameFrameworkException("Resource group names is invalid.");
      for (int index = 0; index < resourceGroupNames.Length; ++index)
      {
        resourceGroups[index] = !string.IsNullOrEmpty(resourceGroupNames[index]) ? (ResourceManager.ResourceGroup) this.GetResourceGroup(resourceGroupNames[index]) : throw new GameFrameworkException("Resource group name is invalid.");
        if (resourceGroups[index] == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Resource group '{0}' is not exist.", resourceGroupNames[index]));
      }
      return (IResourceGroupCollection) new ResourceManager.ResourceGroupCollection(resourceGroups, this.m_ResourceInfos);
    }

    /// <summary>获取资源组集合。</summary>
    /// <param name="resourceGroupNames">要获取的资源组名称的集合。</param>
    /// <returns>要获取的资源组集合。</returns>
    public IResourceGroupCollection GetResourceGroupCollection(List<string> resourceGroupNames)
    {
      ResourceManager.ResourceGroup[] resourceGroups = resourceGroupNames != null && resourceGroupNames.Count >= 1 ? new ResourceManager.ResourceGroup[resourceGroupNames.Count] : throw new GameFrameworkException("Resource group names is invalid.");
      for (int index = 0; index < resourceGroupNames.Count; ++index)
      {
        resourceGroups[index] = !string.IsNullOrEmpty(resourceGroupNames[index]) ? (ResourceManager.ResourceGroup) this.GetResourceGroup(resourceGroupNames[index]) : throw new GameFrameworkException("Resource group name is invalid.");
        if (resourceGroups[index] == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Resource group '{0}' is not exist.", resourceGroupNames[index]));
      }
      return (IResourceGroupCollection) new ResourceManager.ResourceGroupCollection(resourceGroups, this.m_ResourceInfos);
    }

    private void UpdateResource(ResourceManager.ResourceName resourceName) => this.m_ResourceUpdater.UpdateResource(resourceName);

    private ResourceManager.ResourceGroup GetOrAddResourceGroup(string resourceGroupName)
    {
      if (resourceGroupName == null)
        resourceGroupName = string.Empty;
      ResourceManager.ResourceGroup addResourceGroup = (ResourceManager.ResourceGroup) null;
      if (!this.m_ResourceGroups.TryGetValue(resourceGroupName, out addResourceGroup))
      {
        addResourceGroup = new ResourceManager.ResourceGroup(resourceGroupName, this.m_ResourceInfos);
        this.m_ResourceGroups.Add(resourceGroupName, addResourceGroup);
      }
      return addResourceGroup;
    }

    private ResourceManager.AssetInfo GetAssetInfo(string assetName)
    {
      if (string.IsNullOrEmpty(assetName))
        throw new GameFrameworkException("Asset name is invalid.");
      if (this.m_AssetInfos == null)
        return (ResourceManager.AssetInfo) null;
      ResourceManager.AssetInfo assetInfo = (ResourceManager.AssetInfo) null;
      return this.m_AssetInfos.TryGetValue(assetName, out assetInfo) ? assetInfo : (ResourceManager.AssetInfo) null;
    }

    private ResourceManager.ResourceInfo GetResourceInfo(ResourceManager.ResourceName resourceName)
    {
      if (this.m_ResourceInfos == null)
        return (ResourceManager.ResourceInfo) null;
      ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
      return this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) ? resourceInfo : (ResourceManager.ResourceInfo) null;
    }

    private IFileSystem GetFileSystem(string fileSystemName, bool storageInReadOnly)
    {
      if (string.IsNullOrEmpty(fileSystemName))
        throw new GameFrameworkException("File system name is invalid.");
      IFileSystem fileSystem = (IFileSystem) null;
      if (storageInReadOnly)
      {
        if (!this.m_ReadOnlyFileSystems.TryGetValue(fileSystemName, out fileSystem))
        {
          string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ReadOnlyPath, Utility.Text.Format<string, string>("{0}.{1}", fileSystemName, "dat")));
          fileSystem = this.m_FileSystemManager.GetFileSystem(regularPath);
          if (fileSystem == null)
          {
            fileSystem = this.m_FileSystemManager.LoadFileSystem(regularPath, FileSystemAccess.Read);
            this.m_ReadOnlyFileSystems.Add(fileSystemName, fileSystem);
          }
        }
      }
      else if (!this.m_ReadWriteFileSystems.TryGetValue(fileSystemName, out fileSystem))
      {
        string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ReadWritePath, Utility.Text.Format<string, string>("{0}.{1}", fileSystemName, "dat")));
        fileSystem = this.m_FileSystemManager.GetFileSystem(regularPath);
        if (fileSystem == null)
        {
          if (File.Exists(regularPath))
          {
            fileSystem = this.m_FileSystemManager.LoadFileSystem(regularPath, FileSystemAccess.ReadWrite);
          }
          else
          {
            string directoryName = System.IO.Path.GetDirectoryName(regularPath);
            if (!Directory.Exists(directoryName))
              Directory.CreateDirectory(directoryName);
            fileSystem = this.m_FileSystemManager.CreateFileSystem(regularPath, FileSystemAccess.ReadWrite, 16384, 262144);
          }
          this.m_ReadWriteFileSystems.Add(fileSystemName, fileSystem);
        }
      }
      return fileSystem;
    }

    private void OnIniterResourceInitComplete()
    {
      this.m_ResourceIniter.ResourceInitComplete -= new GameFrameworkAction(this.OnIniterResourceInitComplete);
      this.m_ResourceIniter.Shutdown();
      this.m_ResourceIniter = (ResourceManager.ResourceIniter) null;
      this.m_InitResourcesCompleteCallback();
      this.m_InitResourcesCompleteCallback = (InitResourcesCompleteCallback) null;
    }

    private void OnVersionListProcessorUpdateSuccess(string downloadPath, string downloadUri) => this.m_UpdateVersionListCallbacks.UpdateVersionListSuccessCallback(downloadPath, downloadUri);

    private void OnVersionListProcessorUpdateFailure(string downloadUri, string errorMessage)
    {
      if (this.m_UpdateVersionListCallbacks.UpdateVersionListFailureCallback == null)
        return;
      this.m_UpdateVersionListCallbacks.UpdateVersionListFailureCallback(downloadUri, errorMessage);
    }

    private void OnVerifierResourceVerifyStart(int count, long totalLength)
    {
      if (this.m_ResourceVerifyStartEventHandler == null)
        return;
      ResourceVerifyStartEventArgs e = ResourceVerifyStartEventArgs.Create(count, totalLength);
      this.m_ResourceVerifyStartEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnVerifierResourceVerifySuccess(
      ResourceManager.ResourceName resourceName,
      int length)
    {
      if (this.m_ResourceVerifySuccessEventHandler == null)
        return;
      ResourceVerifySuccessEventArgs e = ResourceVerifySuccessEventArgs.Create(resourceName.FullName, length);
      this.m_ResourceVerifySuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnVerifierResourceVerifyFailure(ResourceManager.ResourceName resourceName)
    {
      if (this.m_ResourceVerifyFailureEventHandler == null)
        return;
      ResourceVerifyFailureEventArgs e = ResourceVerifyFailureEventArgs.Create(resourceName.FullName);
      this.m_ResourceVerifyFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnVerifierResourceVerifyComplete(bool result)
    {
      this.m_VerifyResourcesCompleteCallback(result);
      this.m_ResourceVerifier.ResourceVerifyStart -= new GameFrameworkAction<int, long>(this.OnVerifierResourceVerifyStart);
      this.m_ResourceVerifier.ResourceVerifySuccess -= new GameFrameworkAction<ResourceManager.ResourceName, int>(this.OnVerifierResourceVerifySuccess);
      this.m_ResourceVerifier.ResourceVerifyFailure -= new GameFrameworkAction<ResourceManager.ResourceName>(this.OnVerifierResourceVerifyFailure);
      this.m_ResourceVerifier.ResourceVerifyComplete -= new GameFrameworkAction<bool>(this.OnVerifierResourceVerifyComplete);
      this.m_ResourceVerifier.Shutdown();
      this.m_ResourceVerifier = (ResourceManager.ResourceVerifier) null;
    }

    private void OnCheckerResourceNeedUpdate(
      ResourceManager.ResourceName resourceName,
      string fileSystemName,
      ResourceManager.LoadType loadType,
      int length,
      int hashCode,
      int compressedLength,
      int compressedHashCode)
    {
      this.m_ResourceUpdater.AddResourceUpdate(resourceName, fileSystemName, loadType, length, hashCode, compressedLength, compressedHashCode, Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ReadWritePath, resourceName.FullName)));
    }

    private void OnCheckerResourceCheckComplete(
      int movedCount,
      int removedCount,
      int updateCount,
      long updateTotalLength,
      long updateTotalCompressedLength)
    {
      this.m_VersionListProcessor.VersionListUpdateSuccess -= new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateSuccess);
      this.m_VersionListProcessor.VersionListUpdateFailure -= new GameFrameworkAction<string, string>(this.OnVersionListProcessorUpdateFailure);
      this.m_VersionListProcessor.Shutdown();
      this.m_VersionListProcessor = (ResourceManager.VersionListProcessor) null;
      this.m_UpdateVersionListCallbacks = (UpdateVersionListCallbacks) null;
      this.m_ResourceChecker.ResourceNeedUpdate -= new GameFrameworkAction<ResourceManager.ResourceName, string, ResourceManager.LoadType, int, int, int, int>(this.OnCheckerResourceNeedUpdate);
      this.m_ResourceChecker.ResourceCheckComplete -= new GameFrameworkAction<int, int, int, long, long>(this.OnCheckerResourceCheckComplete);
      this.m_ResourceChecker.Shutdown();
      this.m_ResourceChecker = (ResourceManager.ResourceChecker) null;
      this.m_ResourceUpdater.CheckResourceComplete(movedCount > 0 || removedCount > 0);
      if (updateCount <= 0)
      {
        this.m_ResourceUpdater.ResourceApplyStart -= new GameFrameworkAction<string, int, long>(this.OnUpdaterResourceApplyStart);
        this.m_ResourceUpdater.ResourceApplySuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceApplySuccess);
        this.m_ResourceUpdater.ResourceApplyFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, string>(this.OnUpdaterResourceApplyFailure);
        this.m_ResourceUpdater.ResourceApplyComplete -= new GameFrameworkAction<string, bool>(this.OnUpdaterResourceApplyComplete);
        this.m_ResourceUpdater.ResourceUpdateStart -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int>(this.OnUpdaterResourceUpdateStart);
        this.m_ResourceUpdater.ResourceUpdateChanged -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateChanged);
        this.m_ResourceUpdater.ResourceUpdateSuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateSuccess);
        this.m_ResourceUpdater.ResourceUpdateFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string>(this.OnUpdaterResourceUpdateFailure);
        this.m_ResourceUpdater.ResourceUpdateComplete -= new GameFrameworkAction<ResourceManager.ResourceGroup, bool>(this.OnUpdaterResourceUpdateComplete);
        this.m_ResourceUpdater.ResourceUpdateAllComplete -= new GameFrameworkAction(this.OnUpdaterResourceUpdateAllComplete);
        this.m_ResourceUpdater.Shutdown();
        this.m_ResourceUpdater = (ResourceManager.ResourceUpdater) null;
        this.m_ReadWriteResourceInfos.Clear();
        this.m_ReadWriteResourceInfos = (SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo>) null;
        if (this.m_DecompressCachedStream != null)
        {
          this.m_DecompressCachedStream.Dispose();
          this.m_DecompressCachedStream = (MemoryStream) null;
        }
      }
      this.m_CheckResourcesCompleteCallback(movedCount, removedCount, updateCount, updateTotalLength, updateTotalCompressedLength);
      this.m_CheckResourcesCompleteCallback = (CheckResourcesCompleteCallback) null;
    }

    private void OnUpdaterResourceApplyStart(string resourcePackPath, int count, long totalLength)
    {
      if (this.m_ResourceApplyStartEventHandler == null)
        return;
      ResourceApplyStartEventArgs e = ResourceApplyStartEventArgs.Create(resourcePackPath, count, totalLength);
      this.m_ResourceApplyStartEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceApplySuccess(
      ResourceManager.ResourceName resourceName,
      string applyPath,
      string resourcePackPath,
      int length,
      int compressedLength)
    {
      if (this.m_ResourceApplySuccessEventHandler == null)
        return;
      ResourceApplySuccessEventArgs e = ResourceApplySuccessEventArgs.Create(resourceName.FullName, applyPath, resourcePackPath, length, compressedLength);
      this.m_ResourceApplySuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceApplyFailure(
      ResourceManager.ResourceName resourceName,
      string resourcePackPath,
      string errorMessage)
    {
      if (this.m_ResourceApplyFailureEventHandler == null)
        return;
      ResourceApplyFailureEventArgs e = ResourceApplyFailureEventArgs.Create(resourceName.FullName, resourcePackPath, errorMessage);
      this.m_ResourceApplyFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceApplyComplete(string resourcePackPath, bool result)
    {
      ApplyResourcesCompleteCallback completeCallback = this.m_ApplyResourcesCompleteCallback;
      this.m_ApplyResourcesCompleteCallback = (ApplyResourcesCompleteCallback) null;
      string resourcePackPath1 = resourcePackPath;
      int num = result ? 1 : 0;
      completeCallback(resourcePackPath1, num != 0);
    }

    private void OnUpdaterResourceUpdateStart(
      ResourceManager.ResourceName resourceName,
      string downloadPath,
      string downloadUri,
      int currentLength,
      int compressedLength,
      int retryCount)
    {
      if (this.m_ResourceUpdateStartEventHandler == null)
        return;
      ResourceUpdateStartEventArgs e = ResourceUpdateStartEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, currentLength, compressedLength, retryCount);
      this.m_ResourceUpdateStartEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceUpdateChanged(
      ResourceManager.ResourceName resourceName,
      string downloadPath,
      string downloadUri,
      int currentLength,
      int compressedLength)
    {
      if (this.m_ResourceUpdateChangedEventHandler == null)
        return;
      ResourceUpdateChangedEventArgs e = ResourceUpdateChangedEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, currentLength, compressedLength);
      this.m_ResourceUpdateChangedEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceUpdateSuccess(
      ResourceManager.ResourceName resourceName,
      string downloadPath,
      string downloadUri,
      int length,
      int compressedLength)
    {
      if (this.m_ResourceUpdateSuccessEventHandler == null)
        return;
      ResourceUpdateSuccessEventArgs e = ResourceUpdateSuccessEventArgs.Create(resourceName.FullName, downloadPath, downloadUri, length, compressedLength);
      this.m_ResourceUpdateSuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceUpdateFailure(
      ResourceManager.ResourceName resourceName,
      string downloadUri,
      int retryCount,
      int totalRetryCount,
      string errorMessage)
    {
      if (this.m_ResourceUpdateFailureEventHandler == null)
        return;
      ResourceUpdateFailureEventArgs e = ResourceUpdateFailureEventArgs.Create(resourceName.FullName, downloadUri, retryCount, totalRetryCount, errorMessage);
      this.m_ResourceUpdateFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnUpdaterResourceUpdateComplete(
      ResourceManager.ResourceGroup resourceGroup,
      bool result)
    {
      Utility.Path.RemoveEmptyDirectory(this.m_ReadWritePath);
      UpdateResourcesCompleteCallback completeCallback = this.m_UpdateResourcesCompleteCallback;
      this.m_UpdateResourcesCompleteCallback = (UpdateResourcesCompleteCallback) null;
      ResourceManager.ResourceGroup resourceGroup1 = resourceGroup;
      int num = result ? 1 : 0;
      completeCallback((IResourceGroup) resourceGroup1, num != 0);
    }

    private void OnUpdaterResourceUpdateAllComplete()
    {
      this.m_ResourceUpdater.ResourceApplyStart -= new GameFrameworkAction<string, int, long>(this.OnUpdaterResourceApplyStart);
      this.m_ResourceUpdater.ResourceApplySuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceApplySuccess);
      this.m_ResourceUpdater.ResourceApplyFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, string>(this.OnUpdaterResourceApplyFailure);
      this.m_ResourceUpdater.ResourceApplyComplete -= new GameFrameworkAction<string, bool>(this.OnUpdaterResourceApplyComplete);
      this.m_ResourceUpdater.ResourceUpdateStart -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int>(this.OnUpdaterResourceUpdateStart);
      this.m_ResourceUpdater.ResourceUpdateChanged -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateChanged);
      this.m_ResourceUpdater.ResourceUpdateSuccess -= new GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>(this.OnUpdaterResourceUpdateSuccess);
      this.m_ResourceUpdater.ResourceUpdateFailure -= new GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string>(this.OnUpdaterResourceUpdateFailure);
      this.m_ResourceUpdater.ResourceUpdateComplete -= new GameFrameworkAction<ResourceManager.ResourceGroup, bool>(this.OnUpdaterResourceUpdateComplete);
      this.m_ResourceUpdater.ResourceUpdateAllComplete -= new GameFrameworkAction(this.OnUpdaterResourceUpdateAllComplete);
      this.m_ResourceUpdater.Shutdown();
      this.m_ResourceUpdater = (ResourceManager.ResourceUpdater) null;
      this.m_ReadWriteResourceInfos.Clear();
      this.m_ReadWriteResourceInfos = (SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo>) null;
      if (this.m_DecompressCachedStream != null)
      {
        this.m_DecompressCachedStream.Dispose();
        this.m_DecompressCachedStream = (MemoryStream) null;
      }
      Utility.Path.RemoveEmptyDirectory(this.m_ReadWritePath);
      if (this.m_ResourceUpdateAllCompleteEventHandler == null)
        return;
      ResourceUpdateAllCompleteEventArgs e = ResourceUpdateAllCompleteEventArgs.Create();
      this.m_ResourceUpdateAllCompleteEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    /// <summary>资源信息。</summary>
    private sealed class AssetInfo
    {
      private readonly string m_AssetName;
      private readonly ResourceManager.ResourceName m_ResourceName;
      private readonly string[] m_DependencyAssetNames;

      /// <summary>初始化资源信息的新实例。</summary>
      /// <param name="assetName">资源名称。</param>
      /// <param name="resourceName">所在资源名称。</param>
      /// <param name="dependencyAssetNames">依赖资源名称。</param>
      public AssetInfo(
        string assetName,
        ResourceManager.ResourceName resourceName,
        string[] dependencyAssetNames)
      {
        this.m_AssetName = assetName;
        this.m_ResourceName = resourceName;
        this.m_DependencyAssetNames = dependencyAssetNames;
      }

      /// <summary>获取资源名称。</summary>
      public string AssetName => this.m_AssetName;

      /// <summary>获取所在资源名称。</summary>
      public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

      /// <summary>获取依赖资源名称。</summary>
      /// <returns>依赖资源名称。</returns>
      public string[] GetDependencyAssetNames() => this.m_DependencyAssetNames;
    }

    /// <summary>资源加载方式类型。</summary>
    private enum LoadType : byte
    {
      /// <summary>使用文件方式加载。</summary>
      LoadFromFile,
      /// <summary>使用内存方式加载。</summary>
      LoadFromMemory,
      /// <summary>使用内存快速解密方式加载。</summary>
      LoadFromMemoryAndQuickDecrypt,
      /// <summary>使用内存解密方式加载。</summary>
      LoadFromMemoryAndDecrypt,
      /// <summary>使用二进制方式加载。</summary>
      LoadFromBinary,
      /// <summary>使用二进制快速解密方式加载。</summary>
      LoadFromBinaryAndQuickDecrypt,
      /// <summary>使用二进制解密方式加载。</summary>
      LoadFromBinaryAndDecrypt,
    }

    [StructLayout(LayoutKind.Auto)]
    private struct ReadWriteResourceInfo
    {
      private readonly string m_FileSystemName;
      private readonly ResourceManager.LoadType m_LoadType;
      private readonly int m_Length;
      private readonly int m_HashCode;

      public ReadWriteResourceInfo(
        string fileSystemName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode)
      {
        this.m_FileSystemName = fileSystemName;
        this.m_LoadType = loadType;
        this.m_Length = length;
        this.m_HashCode = hashCode;
      }

      public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

      public string FileSystemName => this.m_FileSystemName;

      public ResourceManager.LoadType LoadType => this.m_LoadType;

      public int Length => this.m_Length;

      public int HashCode => this.m_HashCode;
    }

    /// <summary>资源检查器。</summary>
    private sealed class ResourceChecker
    {
      private readonly ResourceManager m_ResourceManager;
      private readonly Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceChecker.CheckInfo> m_CheckInfos;
      private string m_CurrentVariant;
      private bool m_IgnoreOtherVariant;
      private bool m_UpdatableVersionListReady;
      private bool m_ReadOnlyVersionListReady;
      private bool m_ReadWriteVersionListReady;
      public GameFrameworkAction<ResourceManager.ResourceName, string, ResourceManager.LoadType, int, int, int, int> ResourceNeedUpdate;
      public GameFrameworkAction<int, int, int, long, long> ResourceCheckComplete;

      /// <summary>初始化资源检查器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public ResourceChecker(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_CheckInfos = new Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceChecker.CheckInfo>();
        this.m_CurrentVariant = (string) null;
        this.m_IgnoreOtherVariant = false;
        this.m_UpdatableVersionListReady = false;
        this.m_ReadOnlyVersionListReady = false;
        this.m_ReadWriteVersionListReady = false;
        this.ResourceNeedUpdate = (GameFrameworkAction<ResourceManager.ResourceName, string, ResourceManager.LoadType, int, int, int, int>) null;
        this.ResourceCheckComplete = (GameFrameworkAction<int, int, int, long, long>) null;
      }

      /// <summary>关闭并清理资源检查器。</summary>
      public void Shutdown() => this.m_CheckInfos.Clear();

      /// <summary>检查资源。</summary>
      /// <param name="currentVariant">当前使用的变体。</param>
      /// <param name="ignoreOtherVariant">是否忽略处理其它变体的资源，若不忽略，将会移除其它变体的资源。</param>
      public void CheckResources(string currentVariant, bool ignoreOtherVariant)
      {
        if (this.m_ResourceManager.m_ResourceHelper == null)
          throw new GameFrameworkException("Resource helper is invalid.");
        if (string.IsNullOrEmpty(this.m_ResourceManager.m_ReadOnlyPath))
          throw new GameFrameworkException("Read-only path is invalid.");
        if (string.IsNullOrEmpty(this.m_ResourceManager.m_ReadWritePath))
          throw new GameFrameworkException("Read-write path is invalid.");
        this.m_CurrentVariant = currentVariant;
        this.m_IgnoreOtherVariant = ignoreOtherVariant;
        this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkVersion.dat")), new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadUpdatableVersionListSuccess), new LoadBytesFailureCallback(this.OnLoadUpdatableVersionListFailure)), (object) null);
        this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadOnlyPath, "GameFrameworkList.dat")), new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadReadOnlyVersionListSuccess), new LoadBytesFailureCallback(this.OnLoadReadOnlyVersionListFailure)), (object) null);
        this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkList.dat")), new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadReadWriteVersionListSuccess), new LoadBytesFailureCallback(this.OnLoadReadWriteVersionListFailure)), (object) null);
      }

      private void SetCachedFileSystemName(
        ResourceManager.ResourceName resourceName,
        string fileSystemName)
      {
        this.GetOrAddCheckInfo(resourceName).SetCachedFileSystemName(fileSystemName);
      }

      private void SetVersionInfo(
        ResourceManager.ResourceName resourceName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode,
        int compressedLength,
        int compressedHashCode)
      {
        this.GetOrAddCheckInfo(resourceName).SetVersionInfo(loadType, length, hashCode, compressedLength, compressedHashCode);
      }

      private void SetReadOnlyInfo(
        ResourceManager.ResourceName resourceName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode)
      {
        this.GetOrAddCheckInfo(resourceName).SetReadOnlyInfo(loadType, length, hashCode);
      }

      private void SetReadWriteInfo(
        ResourceManager.ResourceName resourceName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode)
      {
        this.GetOrAddCheckInfo(resourceName).SetReadWriteInfo(loadType, length, hashCode);
      }

      private ResourceManager.ResourceChecker.CheckInfo GetOrAddCheckInfo(
        ResourceManager.ResourceName resourceName)
      {
        ResourceManager.ResourceChecker.CheckInfo orAddCheckInfo1 = (ResourceManager.ResourceChecker.CheckInfo) null;
        if (this.m_CheckInfos.TryGetValue(resourceName, out orAddCheckInfo1))
          return orAddCheckInfo1;
        ResourceManager.ResourceChecker.CheckInfo orAddCheckInfo2 = new ResourceManager.ResourceChecker.CheckInfo(resourceName);
        this.m_CheckInfos.Add(orAddCheckInfo2.ResourceName, orAddCheckInfo2);
        return orAddCheckInfo2;
      }

      private void RefreshCheckInfoStatus()
      {
        if (!this.m_UpdatableVersionListReady || !this.m_ReadOnlyVersionListReady || !this.m_ReadWriteVersionListReady)
          return;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        long num4 = 0;
        long num5 = 0;
        foreach (KeyValuePair<ResourceManager.ResourceName, ResourceManager.ResourceChecker.CheckInfo> checkInfo1 in this.m_CheckInfos)
        {
          ResourceManager.ResourceChecker.CheckInfo checkInfo2 = checkInfo1.Value;
          checkInfo2.RefreshStatus(this.m_CurrentVariant, this.m_IgnoreOtherVariant);
          ResourceManager.ResourceName resourceName;
          if (checkInfo2.Status == ResourceManager.ResourceChecker.CheckInfo.CheckStatus.StorageInReadOnly)
            this.m_ResourceManager.m_ResourceInfos.Add(checkInfo2.ResourceName, new ResourceManager.ResourceInfo(checkInfo2.ResourceName, checkInfo2.FileSystemName, checkInfo2.LoadType, checkInfo2.Length, checkInfo2.HashCode, checkInfo2.CompressedLength, true, true));
          else if (checkInfo2.Status == ResourceManager.ResourceChecker.CheckInfo.CheckStatus.StorageInReadWrite)
          {
            if (checkInfo2.NeedMoveToDisk || checkInfo2.NeedMoveToFileSystem)
            {
              ++num1;
              resourceName = checkInfo2.ResourceName;
              string fullName = resourceName.FullName;
              string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, fullName));
              if (checkInfo2.NeedMoveToDisk)
              {
                IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(checkInfo2.ReadWriteFileSystemName, false);
                if (!fileSystem.SaveAsFile(fullName, regularPath))
                  throw new GameFrameworkException(Utility.Text.Format<string, string, string>("Save as file '{0}' to '{1}' from file system '{2}' error.", fullName, regularPath, fileSystem.FullPath));
                fileSystem.DeleteFile(fullName);
              }
              if (checkInfo2.NeedMoveToFileSystem)
              {
                IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(checkInfo2.FileSystemName, false);
                if (!fileSystem.WriteFile(fullName, regularPath))
                  throw new GameFrameworkException(Utility.Text.Format<string, string>("Write resource '{0}' to file system '{1}' error.", fullName, fileSystem.FullPath));
                if (File.Exists(regularPath))
                  File.Delete(regularPath);
              }
            }
            this.m_ResourceManager.m_ResourceInfos.Add(checkInfo2.ResourceName, new ResourceManager.ResourceInfo(checkInfo2.ResourceName, checkInfo2.FileSystemName, checkInfo2.LoadType, checkInfo2.Length, checkInfo2.HashCode, checkInfo2.CompressedLength, false, true));
            this.m_ResourceManager.m_ReadWriteResourceInfos.Add(checkInfo2.ResourceName, new ResourceManager.ReadWriteResourceInfo(checkInfo2.FileSystemName, checkInfo2.LoadType, checkInfo2.Length, checkInfo2.HashCode));
          }
          else if (checkInfo2.Status == ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Update)
          {
            this.m_ResourceManager.m_ResourceInfos.Add(checkInfo2.ResourceName, new ResourceManager.ResourceInfo(checkInfo2.ResourceName, checkInfo2.FileSystemName, checkInfo2.LoadType, checkInfo2.Length, checkInfo2.HashCode, checkInfo2.CompressedLength, false, false));
            ++num3;
            num4 += (long) checkInfo2.Length;
            num5 += (long) checkInfo2.CompressedLength;
            if (this.ResourceNeedUpdate != null)
              this.ResourceNeedUpdate(checkInfo2.ResourceName, checkInfo2.FileSystemName, checkInfo2.LoadType, checkInfo2.Length, checkInfo2.HashCode, checkInfo2.CompressedLength, checkInfo2.CompressedHashCode);
          }
          else if (checkInfo2.Status != ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Unavailable && checkInfo2.Status != ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Disuse)
          {
            resourceName = checkInfo2.ResourceName;
            throw new GameFrameworkException(Utility.Text.Format<string>("Check resources '{0}' error with unknown status.", resourceName.FullName));
          }
          if (checkInfo2.NeedRemove)
          {
            ++num2;
            if (checkInfo2.ReadWriteUseFileSystem)
            {
              IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(checkInfo2.ReadWriteFileSystemName, false);
              resourceName = checkInfo2.ResourceName;
              string fullName = resourceName.FullName;
              fileSystem.DeleteFile(fullName);
            }
            else
            {
              string readWritePath = this.m_ResourceManager.m_ReadWritePath;
              resourceName = checkInfo2.ResourceName;
              string fullName = resourceName.FullName;
              string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(readWritePath, fullName));
              if (File.Exists(regularPath))
                File.Delete(regularPath);
            }
          }
        }
        if (num1 > 0 || num2 > 0)
        {
          this.RemoveEmptyFileSystems();
          Utility.Path.RemoveEmptyDirectory(this.m_ResourceManager.m_ReadWritePath);
        }
        if (this.ResourceCheckComplete == null)
          return;
        this.ResourceCheckComplete(num1, num2, num3, num4, num5);
      }

      private void RemoveEmptyFileSystems()
      {
        List<string> stringList = (List<string>) null;
        foreach (KeyValuePair<string, IFileSystem> readWriteFileSystem in this.m_ResourceManager.m_ReadWriteFileSystems)
        {
          if (readWriteFileSystem.Value.FileCount <= 0)
          {
            if (stringList == null)
              stringList = new List<string>();
            this.m_ResourceManager.m_FileSystemManager.DestroyFileSystem(readWriteFileSystem.Value, true);
            stringList.Add(readWriteFileSystem.Key);
          }
        }
        if (stringList == null)
          return;
        foreach (string key in stringList)
          this.m_ResourceManager.m_ReadWriteFileSystems.Remove(key);
      }

      private void OnLoadUpdatableVersionListSuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        if (this.m_UpdatableVersionListReady)
          throw new GameFrameworkException("Updatable version list has been parsed.");
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          memoryStream = new MemoryStream(bytes, false);
          UpdatableVersionList updatableVersionList = this.m_ResourceManager.m_UpdatableVersionListSerializer.Deserialize((Stream) memoryStream);
          UpdatableVersionList.Asset[] assetArray = updatableVersionList.IsValid ? updatableVersionList.GetAssets() : throw new GameFrameworkException("Deserialize updatable version list failure.");
          UpdatableVersionList.Resource[] resources = updatableVersionList.GetResources();
          UpdatableVersionList.FileSystem[] fileSystems = updatableVersionList.GetFileSystems();
          UpdatableVersionList.ResourceGroup[] resourceGroups = updatableVersionList.GetResourceGroups();
          this.m_ResourceManager.m_ApplicableGameVersion = updatableVersionList.ApplicableGameVersion;
          this.m_ResourceManager.m_InternalResourceVersion = updatableVersionList.InternalResourceVersion;
          this.m_ResourceManager.m_AssetInfos = new Dictionary<string, ResourceManager.AssetInfo>(assetArray.Length, (IEqualityComparer<string>) StringComparer.Ordinal);
          this.m_ResourceManager.m_ResourceInfos = new Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo>(resources.Length, (IEqualityComparer<ResourceManager.ResourceName>) new ResourceManager.ResourceNameComparer());
          this.m_ResourceManager.m_ReadWriteResourceInfos = new SortedDictionary<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo>((IComparer<ResourceManager.ResourceName>) new ResourceManager.ResourceNameComparer());
          ResourceManager.ResourceGroup addResourceGroup1 = this.m_ResourceManager.GetOrAddResourceGroup(string.Empty);
          foreach (UpdatableVersionList.FileSystem fileSystem in fileSystems)
          {
            foreach (int resourceIndex in fileSystem.GetResourceIndexes())
            {
              UpdatableVersionList.Resource resource = resources[resourceIndex];
              if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
                this.SetCachedFileSystemName(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), fileSystem.Name);
            }
          }
          foreach (UpdatableVersionList.Resource resource in resources)
          {
            if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
            {
              ResourceManager.ResourceName resourceName = new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension);
              foreach (int assetIndex in resource.GetAssetIndexes())
              {
                UpdatableVersionList.Asset asset = assetArray[assetIndex];
                int[] dependencyAssetIndexes = asset.GetDependencyAssetIndexes();
                int num = 0;
                string[] dependencyAssetNames = new string[dependencyAssetIndexes.Length];
                foreach (int index in dependencyAssetIndexes)
                  dependencyAssetNames[num++] = assetArray[index].Name;
                this.m_ResourceManager.m_AssetInfos.Add(asset.Name, new ResourceManager.AssetInfo(asset.Name, resourceName, dependencyAssetNames));
              }
              this.SetVersionInfo(resourceName, (ResourceManager.LoadType) resource.LoadType, resource.Length, resource.HashCode, resource.CompressedLength, resource.CompressedHashCode);
              addResourceGroup1.AddResource(resourceName, resource.Length, resource.CompressedLength);
            }
          }
          foreach (UpdatableVersionList.ResourceGroup resourceGroup in resourceGroups)
          {
            ResourceManager.ResourceGroup addResourceGroup2 = this.m_ResourceManager.GetOrAddResourceGroup(resourceGroup.Name);
            foreach (int resourceIndex in resourceGroup.GetResourceIndexes())
            {
              UpdatableVersionList.Resource resource = resources[resourceIndex];
              if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
                addResourceGroup2.AddResource(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), resource.Length, resource.CompressedLength);
            }
          }
          this.m_UpdatableVersionListReady = true;
          this.RefreshCheckInfoStatus();
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Parse updatable version list exception '{0}'.", ex), ex);
          throw;
        }
        finally
        {
          memoryStream?.Dispose();
        }
      }

      private void OnLoadUpdatableVersionListFailure(
        string fileUri,
        string errorMessage,
        object userData)
      {
        throw new GameFrameworkException(Utility.Text.Format<string, string>("Updatable version list '{0}' is invalid, error message is '{1}'.", fileUri, string.IsNullOrEmpty(errorMessage) ? "<Empty>" : errorMessage));
      }

      private void OnLoadReadOnlyVersionListSuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        if (this.m_ReadOnlyVersionListReady)
          throw new GameFrameworkException("Read-only version list has been parsed.");
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          memoryStream = new MemoryStream(bytes, false);
          LocalVersionList localVersionList = this.m_ResourceManager.m_ReadOnlyVersionListSerializer.Deserialize((Stream) memoryStream);
          LocalVersionList.Resource[] resourceArray = localVersionList.IsValid ? localVersionList.GetResources() : throw new GameFrameworkException("Deserialize read-only version list failure.");
          foreach (LocalVersionList.FileSystem fileSystem in localVersionList.GetFileSystems())
          {
            foreach (int resourceIndex in fileSystem.GetResourceIndexes())
            {
              LocalVersionList.Resource resource = resourceArray[resourceIndex];
              this.SetCachedFileSystemName(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), fileSystem.Name);
            }
          }
          foreach (LocalVersionList.Resource resource in resourceArray)
            this.SetReadOnlyInfo(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), (ResourceManager.LoadType) resource.LoadType, resource.Length, resource.HashCode);
          this.m_ReadOnlyVersionListReady = true;
          this.RefreshCheckInfoStatus();
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Parse read-only version list exception '{0}'.", ex), ex);
          throw;
        }
        finally
        {
          memoryStream?.Dispose();
        }
      }

      private void OnLoadReadOnlyVersionListFailure(
        string fileUri,
        string errorMessage,
        object userData)
      {
        this.m_ReadOnlyVersionListReady = !this.m_ReadOnlyVersionListReady ? true : throw new GameFrameworkException("Read-only version list has been parsed.");
        this.RefreshCheckInfoStatus();
      }

      private void OnLoadReadWriteVersionListSuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        if (this.m_ReadWriteVersionListReady)
          throw new GameFrameworkException("Read-write version list has been parsed.");
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          memoryStream = new MemoryStream(bytes, false);
          LocalVersionList localVersionList = this.m_ResourceManager.m_ReadWriteVersionListSerializer.Deserialize((Stream) memoryStream);
          LocalVersionList.Resource[] resourceArray = localVersionList.IsValid ? localVersionList.GetResources() : throw new GameFrameworkException("Deserialize read-write version list failure.");
          foreach (LocalVersionList.FileSystem fileSystem in localVersionList.GetFileSystems())
          {
            foreach (int resourceIndex in fileSystem.GetResourceIndexes())
            {
              LocalVersionList.Resource resource = resourceArray[resourceIndex];
              this.SetCachedFileSystemName(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), fileSystem.Name);
            }
          }
          foreach (LocalVersionList.Resource resource in resourceArray)
            this.SetReadWriteInfo(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), (ResourceManager.LoadType) resource.LoadType, resource.Length, resource.HashCode);
          this.m_ReadWriteVersionListReady = true;
          this.RefreshCheckInfoStatus();
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Parse read-write version list exception '{0}'.", ex), ex);
          throw;
        }
        finally
        {
          memoryStream?.Dispose();
        }
      }

      private void OnLoadReadWriteVersionListFailure(
        string fileUri,
        string errorMessage,
        object userData)
      {
        this.m_ReadWriteVersionListReady = !this.m_ReadWriteVersionListReady ? true : throw new GameFrameworkException("Read-write version list has been parsed.");
        this.RefreshCheckInfoStatus();
      }

      /// <summary>资源检查信息。</summary>
      private sealed class CheckInfo
      {
        private readonly ResourceManager.ResourceName m_ResourceName;
        private ResourceManager.ResourceChecker.CheckInfo.CheckStatus m_Status;
        private bool m_NeedRemove;
        private bool m_NeedMoveToDisk;
        private bool m_NeedMoveToFileSystem;
        private ResourceManager.ResourceChecker.CheckInfo.RemoteVersionInfo m_VersionInfo;
        private ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo m_ReadOnlyInfo;
        private ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo m_ReadWriteInfo;
        private string m_CachedFileSystemName;

        /// <summary>初始化资源检查信息的新实例。</summary>
        /// <param name="resourceName">资源名称。</param>
        public CheckInfo(ResourceManager.ResourceName resourceName)
        {
          this.m_ResourceName = resourceName;
          this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Unknown;
          this.m_NeedRemove = false;
          this.m_NeedMoveToDisk = false;
          this.m_NeedMoveToFileSystem = false;
          this.m_VersionInfo = new ResourceManager.ResourceChecker.CheckInfo.RemoteVersionInfo();
          this.m_ReadOnlyInfo = new ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo();
          this.m_ReadWriteInfo = new ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo();
          this.m_CachedFileSystemName = (string) null;
        }

        /// <summary>获取资源名称。</summary>
        public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

        /// <summary>获取资源检查状态。</summary>
        public ResourceManager.ResourceChecker.CheckInfo.CheckStatus Status => this.m_Status;

        /// <summary>获取是否需要移除读写区的资源。</summary>
        public bool NeedRemove => this.m_NeedRemove;

        /// <summary>获取是否需要将读写区的资源移动到磁盘。</summary>
        public bool NeedMoveToDisk => this.m_NeedMoveToDisk;

        /// <summary>获取是否需要将读写区的资源移动到文件系统。</summary>
        public bool NeedMoveToFileSystem => this.m_NeedMoveToFileSystem;

        /// <summary>获取资源所在的文件系统名称。</summary>
        public string FileSystemName => this.m_VersionInfo.FileSystemName;

        /// <summary>获取资源是否使用文件系统。</summary>
        public bool ReadWriteUseFileSystem => this.m_ReadWriteInfo.UseFileSystem;

        /// <summary>获取读写资源所在的文件系统名称。</summary>
        public string ReadWriteFileSystemName => this.m_ReadWriteInfo.FileSystemName;

        /// <summary>获取资源加载方式。</summary>
        public ResourceManager.LoadType LoadType => this.m_VersionInfo.LoadType;

        /// <summary>获取资源大小。</summary>
        public int Length => this.m_VersionInfo.Length;

        /// <summary>获取资源哈希值。</summary>
        public int HashCode => this.m_VersionInfo.HashCode;

        /// <summary>获取压缩后大小。</summary>
        public int CompressedLength => this.m_VersionInfo.CompressedLength;

        /// <summary>获取压缩后哈希值。</summary>
        public int CompressedHashCode => this.m_VersionInfo.CompressedHashCode;

        /// <summary>临时缓存资源所在的文件系统名称。</summary>
        /// <param name="fileSystemName">资源所在的文件系统名称。</param>
        public void SetCachedFileSystemName(string fileSystemName) => this.m_CachedFileSystemName = fileSystemName;

        /// <summary>设置资源在版本中的信息。</summary>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        /// <param name="compressedLength">压缩后大小。</param>
        /// <param name="compressedHashCode">压缩后哈希值。</param>
        public void SetVersionInfo(
          ResourceManager.LoadType loadType,
          int length,
          int hashCode,
          int compressedLength,
          int compressedHashCode)
        {
          if (this.m_VersionInfo.Exist)
            throw new GameFrameworkException(Utility.Text.Format<string>("You must set version info of '{0}' only once.", this.m_ResourceName.FullName));
          this.m_VersionInfo = new ResourceManager.ResourceChecker.CheckInfo.RemoteVersionInfo(this.m_CachedFileSystemName, loadType, length, hashCode, compressedLength, compressedHashCode);
          this.m_CachedFileSystemName = (string) null;
        }

        /// <summary>设置资源在只读区中的信息。</summary>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        public void SetReadOnlyInfo(ResourceManager.LoadType loadType, int length, int hashCode)
        {
          if (this.m_ReadOnlyInfo.Exist)
            throw new GameFrameworkException(Utility.Text.Format<string>("You must set read-only info of '{0}' only once.", this.m_ResourceName.FullName));
          this.m_ReadOnlyInfo = new ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo(this.m_CachedFileSystemName, loadType, length, hashCode);
          this.m_CachedFileSystemName = (string) null;
        }

        /// <summary>设置资源在读写区中的信息。</summary>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        public void SetReadWriteInfo(ResourceManager.LoadType loadType, int length, int hashCode)
        {
          if (this.m_ReadWriteInfo.Exist)
            throw new GameFrameworkException(Utility.Text.Format<string>("You must set read-write info of '{0}' only once.", this.m_ResourceName.FullName));
          this.m_ReadWriteInfo = new ResourceManager.ResourceChecker.CheckInfo.LocalVersionInfo(this.m_CachedFileSystemName, loadType, length, hashCode);
          this.m_CachedFileSystemName = (string) null;
        }

        /// <summary>刷新资源信息状态。</summary>
        /// <param name="currentVariant">当前变体。</param>
        /// <param name="ignoreOtherVariant">是否忽略处理其它变体的资源，若不忽略则移除。</param>
        public void RefreshStatus(string currentVariant, bool ignoreOtherVariant)
        {
          if (!this.m_VersionInfo.Exist)
          {
            this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Disuse;
            this.m_NeedRemove = this.m_ReadWriteInfo.Exist;
          }
          else
          {
            ResourceManager.ResourceName resourceName = this.m_ResourceName;
            if (resourceName.Variant != null)
            {
              resourceName = this.m_ResourceName;
              if (!(resourceName.Variant == currentVariant))
              {
                this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Unavailable;
                this.m_NeedRemove = !ignoreOtherVariant && this.m_ReadWriteInfo.Exist;
                return;
              }
            }
            if (this.m_ReadOnlyInfo.Exist && this.m_ReadOnlyInfo.FileSystemName == this.m_VersionInfo.FileSystemName && this.m_ReadOnlyInfo.LoadType == this.m_VersionInfo.LoadType && this.m_ReadOnlyInfo.Length == this.m_VersionInfo.Length && this.m_ReadOnlyInfo.HashCode == this.m_VersionInfo.HashCode)
            {
              this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.StorageInReadOnly;
              this.m_NeedRemove = this.m_ReadWriteInfo.Exist;
            }
            else if (this.m_ReadWriteInfo.Exist && this.m_ReadWriteInfo.LoadType == this.m_VersionInfo.LoadType && this.m_ReadWriteInfo.Length == this.m_VersionInfo.Length && this.m_ReadWriteInfo.HashCode == this.m_VersionInfo.HashCode)
            {
              bool flag = this.m_ReadWriteInfo.FileSystemName != this.m_VersionInfo.FileSystemName;
              this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.StorageInReadWrite;
              this.m_NeedMoveToDisk = this.m_ReadWriteInfo.UseFileSystem & flag;
              this.m_NeedMoveToFileSystem = this.m_VersionInfo.UseFileSystem & flag;
            }
            else
            {
              this.m_Status = ResourceManager.ResourceChecker.CheckInfo.CheckStatus.Update;
              this.m_NeedRemove = this.m_ReadWriteInfo.Exist;
            }
          }
        }

        /// <summary>资源检查状态。</summary>
        public enum CheckStatus : byte
        {
          /// <summary>资源状态未知。</summary>
          Unknown,
          /// <summary>资源存在且已存放于只读区中。</summary>
          StorageInReadOnly,
          /// <summary>资源存在且已存放于读写区中。</summary>
          StorageInReadWrite,
          /// <summary>资源不适用于当前变体。</summary>
          Unavailable,
          /// <summary>资源需要更新。</summary>
          Update,
          /// <summary>资源已废弃。</summary>
          Disuse,
        }

        /// <summary>本地资源状态信息。</summary>
        [StructLayout(LayoutKind.Auto)]
        private struct LocalVersionInfo
        {
          private readonly bool m_Exist;
          private readonly string m_FileSystemName;
          private readonly ResourceManager.LoadType m_LoadType;
          private readonly int m_Length;
          private readonly int m_HashCode;

          public LocalVersionInfo(
            string fileSystemName,
            ResourceManager.LoadType loadType,
            int length,
            int hashCode)
          {
            this.m_Exist = true;
            this.m_FileSystemName = fileSystemName;
            this.m_LoadType = loadType;
            this.m_Length = length;
            this.m_HashCode = hashCode;
          }

          public bool Exist => this.m_Exist;

          public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

          public string FileSystemName => this.m_FileSystemName;

          public ResourceManager.LoadType LoadType => this.m_LoadType;

          public int Length => this.m_Length;

          public int HashCode => this.m_HashCode;
        }

        /// <summary>远程资源状态信息。</summary>
        [StructLayout(LayoutKind.Auto)]
        private struct RemoteVersionInfo
        {
          private readonly bool m_Exist;
          private readonly string m_FileSystemName;
          private readonly ResourceManager.LoadType m_LoadType;
          private readonly int m_Length;
          private readonly int m_HashCode;
          private readonly int m_CompressedLength;
          private readonly int m_CompressedHashCode;

          public RemoteVersionInfo(
            string fileSystemName,
            ResourceManager.LoadType loadType,
            int length,
            int hashCode,
            int compressedLength,
            int compressedHashCode)
          {
            this.m_Exist = true;
            this.m_FileSystemName = fileSystemName;
            this.m_LoadType = loadType;
            this.m_Length = length;
            this.m_HashCode = hashCode;
            this.m_CompressedLength = compressedLength;
            this.m_CompressedHashCode = compressedHashCode;
          }

          public bool Exist => this.m_Exist;

          public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

          public string FileSystemName => this.m_FileSystemName;

          public ResourceManager.LoadType LoadType => this.m_LoadType;

          public int Length => this.m_Length;

          public int HashCode => this.m_HashCode;

          public int CompressedLength => this.m_CompressedLength;

          public int CompressedHashCode => this.m_CompressedHashCode;
        }
      }
    }

    /// <summary>资源组。</summary>
    private sealed class ResourceGroup : IResourceGroup
    {
      private readonly string m_Name;
      private readonly Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo> m_ResourceInfos;
      private readonly HashSet<ResourceManager.ResourceName> m_ResourceNames;
      private long m_TotalLength;
      private long m_TotalCompressedLength;

      /// <summary>初始化资源组的新实例。</summary>
      /// <param name="name">资源组名称。</param>
      /// <param name="resourceInfos">资源信息引用。</param>
      public ResourceGroup(
        string name,
        Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo> resourceInfos)
      {
        if (name == null)
          throw new GameFrameworkException("Name is invalid.");
        if (resourceInfos == null)
          throw new GameFrameworkException("Resource infos is invalid.");
        this.m_Name = name;
        this.m_ResourceInfos = resourceInfos;
        this.m_ResourceNames = new HashSet<ResourceManager.ResourceName>();
      }

      /// <summary>获取资源组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取资源组是否准备完毕。</summary>
      public bool Ready => this.ReadyCount >= this.TotalCount;

      /// <summary>获取资源组包含资源数量。</summary>
      public int TotalCount => this.m_ResourceNames.Count;

      /// <summary>获取资源组中已准备完成资源数量。</summary>
      public int ReadyCount
      {
        get
        {
          int readyCount = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              ++readyCount;
          }
          return readyCount;
        }
      }

      /// <summary>获取资源组包含资源的总大小。</summary>
      public long TotalLength => this.m_TotalLength;

      /// <summary>获取资源组包含资源压缩后的总大小。</summary>
      public long TotalCompressedLength => this.m_TotalCompressedLength;

      /// <summary>获取资源组中已准备完成资源的总大小。</summary>
      public long ReadyLength
      {
        get
        {
          long readyLength = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              readyLength += (long) resourceInfo.Length;
          }
          return readyLength;
        }
      }

      /// <summary>获取资源组中已准备完成资源压缩后的总大小。</summary>
      public long ReadyCompressedLength
      {
        get
        {
          long compressedLength = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              compressedLength += (long) resourceInfo.CompressedLength;
          }
          return compressedLength;
        }
      }

      /// <summary>获取资源组的完成进度。</summary>
      public float Progress => this.m_TotalLength <= 0L ? 1f : (float) this.ReadyLength / (float) this.m_TotalLength;

      /// <summary>获取资源组包含的资源名称列表。</summary>
      /// <returns>资源组包含的资源名称列表。</returns>
      public string[] GetResourceNames()
      {
        int num = 0;
        string[] resourceNames = new string[this.m_ResourceNames.Count];
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          resourceNames[num++] = resourceName.FullName;
        return resourceNames;
      }

      /// <summary>获取资源组包含的资源名称列表。</summary>
      /// <param name="results">资源组包含的资源名称列表。</param>
      public void GetResourceNames(List<string> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          results.Add(resourceName.FullName);
      }

      /// <summary>获取资源组包含的资源名称列表。</summary>
      /// <returns>资源组包含的资源名称列表。</returns>
      public ResourceManager.ResourceName[] InternalGetResourceNames()
      {
        int num = 0;
        ResourceManager.ResourceName[] resourceNames = new ResourceManager.ResourceName[this.m_ResourceNames.Count];
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          resourceNames[num++] = resourceName;
        return resourceNames;
      }

      /// <summary>获取资源组包含的资源名称列表。</summary>
      /// <param name="results">资源组包含的资源名称列表。</param>
      public void InternalGetResourceNames(List<ResourceManager.ResourceName> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          results.Add(resourceName);
      }

      /// <summary>检查指定资源是否属于资源组。</summary>
      /// <param name="resourceName">要检查的资源的名称。</param>
      /// <returns>指定资源是否属于资源组。</returns>
      public bool HasResource(ResourceManager.ResourceName resourceName) => this.m_ResourceNames.Contains(resourceName);

      /// <summary>向资源组中增加资源。</summary>
      /// <param name="resourceName">资源名称。</param>
      /// <param name="length">资源大小。</param>
      /// <param name="compressedLength">资源压缩后的大小。</param>
      public void AddResource(
        ResourceManager.ResourceName resourceName,
        int length,
        int compressedLength)
      {
        this.m_ResourceNames.Add(resourceName);
        this.m_TotalLength += (long) length;
        this.m_TotalCompressedLength += (long) compressedLength;
      }
    }

    /// <summary>资源组集合。</summary>
    private sealed class ResourceGroupCollection : IResourceGroupCollection
    {
      private readonly ResourceManager.ResourceGroup[] m_ResourceGroups;
      private readonly Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo> m_ResourceInfos;
      private readonly HashSet<ResourceManager.ResourceName> m_ResourceNames;
      private long m_TotalLength;
      private long m_TotalCompressedLength;

      /// <summary>初始化资源组集合的新实例。</summary>
      /// <param name="resourceGroups">资源组集合。</param>
      /// <param name="resourceInfos">资源信息引用。</param>
      public ResourceGroupCollection(
        ResourceManager.ResourceGroup[] resourceGroups,
        Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo> resourceInfos)
      {
        if (resourceGroups == null || resourceGroups.Length < 1)
          throw new GameFrameworkException("Resource groups is invalid.");
        if (resourceInfos == null)
          throw new GameFrameworkException("Resource infos is invalid.");
        int index1 = resourceGroups.Length - 1;
        for (int index2 = 0; index2 < index1; ++index2)
        {
          if (resourceGroups[index2] == null)
            throw new GameFrameworkException(Utility.Text.Format<int>("Resource group index '{0}' is invalid.", index2));
          for (int index3 = index2 + 1; index3 < resourceGroups.Length; ++index3)
          {
            if (resourceGroups[index2] == resourceGroups[index3])
              throw new GameFrameworkException(Utility.Text.Format<string>("Resource group '{0}' duplicated.", resourceGroups[index2].Name));
          }
        }
        this.m_ResourceGroups = resourceGroups[index1] != null ? resourceGroups : throw new GameFrameworkException(Utility.Text.Format<int>("Resource group index '{0}' is invalid.", index1));
        this.m_ResourceInfos = resourceInfos;
        this.m_ResourceNames = new HashSet<ResourceManager.ResourceName>();
        this.m_TotalLength = 0L;
        this.m_TotalCompressedLength = 0L;
        List<ResourceManager.ResourceName> results = new List<ResourceManager.ResourceName>();
        foreach (ResourceManager.ResourceGroup resourceGroup in this.m_ResourceGroups)
        {
          resourceGroup.InternalGetResourceNames(results);
          foreach (ResourceManager.ResourceName key in results)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (!this.m_ResourceInfos.TryGetValue(key, out resourceInfo))
              throw new GameFrameworkException(Utility.Text.Format<string>("Resource info '{0}' is invalid.", key.FullName));
            if (this.m_ResourceNames.Add(key))
            {
              this.m_TotalLength += (long) resourceInfo.Length;
              this.m_TotalCompressedLength += (long) resourceInfo.CompressedLength;
            }
          }
        }
      }

      /// <summary>获取资源组集合是否准备完毕。</summary>
      public bool Ready => this.ReadyCount >= this.TotalCount;

      /// <summary>获取资源组集合包含资源数量。</summary>
      public int TotalCount => this.m_ResourceNames.Count;

      /// <summary>获取资源组集合中已准备完成资源数量。</summary>
      public int ReadyCount
      {
        get
        {
          int readyCount = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              ++readyCount;
          }
          return readyCount;
        }
      }

      /// <summary>获取资源组集合包含资源的总大小。</summary>
      public long TotalLength => this.m_TotalLength;

      /// <summary>获取资源组集合包含资源压缩后的总大小。</summary>
      public long TotalCompressedLength => this.m_TotalCompressedLength;

      /// <summary>获取资源组集合中已准备完成资源的总大小。</summary>
      public long ReadyLength
      {
        get
        {
          long readyLength = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              readyLength += (long) resourceInfo.Length;
          }
          return readyLength;
        }
      }

      /// <summary>获取资源组集合中已准备完成资源压缩后的总大小。</summary>
      public long ReadyCompressedLength
      {
        get
        {
          long compressedLength = 0;
          foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          {
            ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
            if (this.m_ResourceInfos.TryGetValue(resourceName, out resourceInfo) && resourceInfo.Ready)
              compressedLength += (long) resourceInfo.CompressedLength;
          }
          return compressedLength;
        }
      }

      /// <summary>获取资源组集合的完成进度。</summary>
      public float Progress => this.m_TotalLength <= 0L ? 1f : (float) this.ReadyLength / (float) this.m_TotalLength;

      /// <summary>获取资源组集合包含的资源组列表。</summary>
      /// <returns>资源组包含的资源名称列表。</returns>
      public IResourceGroup[] GetResourceGroups() => (IResourceGroup[]) this.m_ResourceGroups;

      /// <summary>获取资源组集合包含的资源名称列表。</summary>
      /// <returns>资源组包含的资源名称列表。</returns>
      public string[] GetResourceNames()
      {
        int num = 0;
        string[] resourceNames = new string[this.m_ResourceNames.Count];
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          resourceNames[num++] = resourceName.FullName;
        return resourceNames;
      }

      /// <summary>获取资源组集合包含的资源名称列表。</summary>
      /// <param name="results">资源组包含的资源名称列表。</param>
      public void GetResourceNames(List<string> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (ResourceManager.ResourceName resourceName in this.m_ResourceNames)
          results.Add(resourceName.FullName);
      }
    }

    /// <summary>资源信息。</summary>
    private sealed class ResourceInfo
    {
      private readonly ResourceManager.ResourceName m_ResourceName;
      private readonly string m_FileSystemName;
      private readonly ResourceManager.LoadType m_LoadType;
      private readonly int m_Length;
      private readonly int m_HashCode;
      private readonly int m_CompressedLength;
      private readonly bool m_StorageInReadOnly;
      private bool m_Ready;

      /// <summary>初始化资源信息的新实例。</summary>
      /// <param name="resourceName">资源名称。</param>
      /// <param name="fileSystemName">文件系统名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="length">资源大小。</param>
      /// <param name="hashCode">资源哈希值。</param>
      /// <param name="compressedLength">压缩后资源大小。</param>
      /// <param name="storageInReadOnly">资源是否在只读区。</param>
      /// <param name="ready">资源是否准备完毕。</param>
      public ResourceInfo(
        ResourceManager.ResourceName resourceName,
        string fileSystemName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode,
        int compressedLength,
        bool storageInReadOnly,
        bool ready)
      {
        this.m_ResourceName = resourceName;
        this.m_FileSystemName = fileSystemName;
        this.m_LoadType = loadType;
        this.m_Length = length;
        this.m_HashCode = hashCode;
        this.m_CompressedLength = compressedLength;
        this.m_StorageInReadOnly = storageInReadOnly;
        this.m_Ready = ready;
      }

      /// <summary>获取资源名称。</summary>
      public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

      /// <summary>获取资源是否使用文件系统。</summary>
      public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

      /// <summary>获取文件系统名称。</summary>
      public string FileSystemName => this.m_FileSystemName;

      /// <summary>获取资源是否通过二进制方式加载。</summary>
      public bool IsLoadFromBinary => this.m_LoadType == ResourceManager.LoadType.LoadFromBinary || this.m_LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || this.m_LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt;

      /// <summary>获取资源加载方式。</summary>
      public ResourceManager.LoadType LoadType => this.m_LoadType;

      /// <summary>获取资源大小。</summary>
      public int Length => this.m_Length;

      /// <summary>获取资源哈希值。</summary>
      public int HashCode => this.m_HashCode;

      /// <summary>获取压缩后资源大小。</summary>
      public int CompressedLength => this.m_CompressedLength;

      /// <summary>获取资源是否在只读区。</summary>
      public bool StorageInReadOnly => this.m_StorageInReadOnly;

      /// <summary>获取资源是否准备完毕。</summary>
      public bool Ready => this.m_Ready;

      /// <summary>标记资源准备完毕。</summary>
      public void MarkReady() => this.m_Ready = true;
    }

    /// <summary>资源初始化器。</summary>
    private sealed class ResourceIniter
    {
      private readonly ResourceManager m_ResourceManager;
      private readonly Dictionary<ResourceManager.ResourceName, string> m_CachedFileSystemNames;
      private string m_CurrentVariant;
      public GameFrameworkAction ResourceInitComplete;

      /// <summary>初始化资源初始化器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public ResourceIniter(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_CachedFileSystemNames = new Dictionary<ResourceManager.ResourceName, string>();
        this.m_CurrentVariant = (string) null;
        this.ResourceInitComplete = (GameFrameworkAction) null;
      }

      /// <summary>关闭并清理资源初始化器。</summary>
      public void Shutdown()
      {
      }

      /// <summary>初始化资源。</summary>
      public void InitResources(string currentVariant)
      {
        this.m_CurrentVariant = currentVariant;
        if (this.m_ResourceManager.m_ResourceHelper == null)
          throw new GameFrameworkException("Resource helper is invalid.");
        if (string.IsNullOrEmpty(this.m_ResourceManager.m_ReadOnlyPath))
          throw new GameFrameworkException("Read-only path is invalid.");
        this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadOnlyPath, "GameFrameworkVersion.dat")), new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadPackageVersionListSuccess), new LoadBytesFailureCallback(this.OnLoadPackageVersionListFailure)), (object) null);
      }

      private void OnLoadPackageVersionListSuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          memoryStream = new MemoryStream(bytes, false);
          PackageVersionList packageVersionList = this.m_ResourceManager.m_PackageVersionListSerializer.Deserialize((Stream) memoryStream);
          PackageVersionList.Asset[] assetArray = packageVersionList.IsValid ? packageVersionList.GetAssets() : throw new GameFrameworkException("Deserialize package version list failure.");
          PackageVersionList.Resource[] resources = packageVersionList.GetResources();
          PackageVersionList.FileSystem[] fileSystems = packageVersionList.GetFileSystems();
          PackageVersionList.ResourceGroup[] resourceGroups = packageVersionList.GetResourceGroups();
          this.m_ResourceManager.m_ApplicableGameVersion = packageVersionList.ApplicableGameVersion;
          this.m_ResourceManager.m_InternalResourceVersion = packageVersionList.InternalResourceVersion;
          this.m_ResourceManager.m_AssetInfos = new Dictionary<string, ResourceManager.AssetInfo>(assetArray.Length, (IEqualityComparer<string>) StringComparer.Ordinal);
          this.m_ResourceManager.m_ResourceInfos = new Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceInfo>(resources.Length, (IEqualityComparer<ResourceManager.ResourceName>) new ResourceManager.ResourceNameComparer());
          ResourceManager.ResourceGroup addResourceGroup1 = this.m_ResourceManager.GetOrAddResourceGroup(string.Empty);
          foreach (PackageVersionList.FileSystem fileSystem in fileSystems)
          {
            foreach (int resourceIndex in fileSystem.GetResourceIndexes())
            {
              PackageVersionList.Resource resource = resources[resourceIndex];
              if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
                this.m_CachedFileSystemNames.Add(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), fileSystem.Name);
            }
          }
          foreach (PackageVersionList.Resource resource in resources)
          {
            if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
            {
              ResourceManager.ResourceName resourceName = new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension);
              foreach (int assetIndex in resource.GetAssetIndexes())
              {
                PackageVersionList.Asset asset = assetArray[assetIndex];
                int[] dependencyAssetIndexes = asset.GetDependencyAssetIndexes();
                int num = 0;
                string[] dependencyAssetNames = new string[dependencyAssetIndexes.Length];
                foreach (int index in dependencyAssetIndexes)
                  dependencyAssetNames[num++] = assetArray[index].Name;
                this.m_ResourceManager.m_AssetInfos.Add(asset.Name, new ResourceManager.AssetInfo(asset.Name, resourceName, dependencyAssetNames));
              }
              string fileSystemName = (string) null;
              if (!this.m_CachedFileSystemNames.TryGetValue(resourceName, out fileSystemName))
                fileSystemName = (string) null;
              this.m_ResourceManager.m_ResourceInfos.Add(resourceName, new ResourceManager.ResourceInfo(resourceName, fileSystemName, (ResourceManager.LoadType) resource.LoadType, resource.Length, resource.HashCode, resource.Length, true, true));
              addResourceGroup1.AddResource(resourceName, resource.Length, resource.Length);
            }
          }
          foreach (PackageVersionList.ResourceGroup resourceGroup in resourceGroups)
          {
            ResourceManager.ResourceGroup addResourceGroup2 = this.m_ResourceManager.GetOrAddResourceGroup(resourceGroup.Name);
            foreach (int resourceIndex in resourceGroup.GetResourceIndexes())
            {
              PackageVersionList.Resource resource = resources[resourceIndex];
              if (resource.Variant == null || !(resource.Variant != this.m_CurrentVariant))
                addResourceGroup2.AddResource(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), resource.Length, resource.Length);
            }
          }
          this.ResourceInitComplete();
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Parse package version list exception '{0}'.", ex), ex);
          throw;
        }
        finally
        {
          this.m_CachedFileSystemNames.Clear();
          memoryStream?.Dispose();
        }
      }

      private void OnLoadPackageVersionListFailure(
        string fileUri,
        string errorMessage,
        object userData)
      {
        throw new GameFrameworkException(Utility.Text.Format<string, string>("Package version list '{0}' is invalid, error message is '{1}'.", fileUri, string.IsNullOrEmpty(errorMessage) ? "<Empty>" : errorMessage));
      }
    }

    /// <summary>加载资源器。</summary>
    private sealed class ResourceLoader
    {
      private const int CachedHashBytesLength = 4;
      private readonly ResourceManager m_ResourceManager;
      private readonly TaskPool<ResourceManager.ResourceLoader.LoadResourceTaskBase> m_TaskPool;
      private readonly Dictionary<object, int> m_AssetDependencyCount;
      private readonly Dictionary<object, int> m_ResourceDependencyCount;
      private readonly Dictionary<object, object> m_AssetToResourceMap;
      private readonly Dictionary<string, object> m_SceneToAssetMap;
      private readonly LoadBytesCallbacks m_LoadBytesCallbacks;
      private readonly byte[] m_CachedHashBytes;
      private IObjectPool<ResourceManager.ResourceLoader.AssetObject> m_AssetPool;
      private IObjectPool<ResourceManager.ResourceLoader.ResourceObject> m_ResourcePool;

      /// <summary>初始化加载资源器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public ResourceLoader(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_TaskPool = new TaskPool<ResourceManager.ResourceLoader.LoadResourceTaskBase>();
        this.m_AssetDependencyCount = new Dictionary<object, int>();
        this.m_ResourceDependencyCount = new Dictionary<object, int>();
        this.m_AssetToResourceMap = new Dictionary<object, object>();
        this.m_SceneToAssetMap = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.Ordinal);
        this.m_LoadBytesCallbacks = new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadBinarySuccess), new LoadBytesFailureCallback(this.OnLoadBinaryFailure));
        this.m_CachedHashBytes = new byte[4];
        this.m_AssetPool = (IObjectPool<ResourceManager.ResourceLoader.AssetObject>) null;
        this.m_ResourcePool = (IObjectPool<ResourceManager.ResourceLoader.ResourceObject>) null;
      }

      /// <summary>获取加载资源代理总数量。</summary>
      public int TotalAgentCount => this.m_TaskPool.TotalAgentCount;

      /// <summary>获取可用加载资源代理数量。</summary>
      public int FreeAgentCount => this.m_TaskPool.FreeAgentCount;

      /// <summary>获取工作中加载资源代理数量。</summary>
      public int WorkingAgentCount => this.m_TaskPool.WorkingAgentCount;

      /// <summary>获取等待加载资源任务数量。</summary>
      public int WaitingTaskCount => this.m_TaskPool.WaitingTaskCount;

      /// <summary>获取或设置资源对象池自动释放可释放对象的间隔秒数。</summary>
      public float AssetAutoReleaseInterval
      {
        get => this.m_AssetPool.AutoReleaseInterval;
        set => this.m_AssetPool.AutoReleaseInterval = value;
      }

      /// <summary>获取或设置资源对象池的容量。</summary>
      public int AssetCapacity
      {
        get => this.m_AssetPool.Capacity;
        set => this.m_AssetPool.Capacity = value;
      }

      /// <summary>获取或设置资源对象池对象过期秒数。</summary>
      public float AssetExpireTime
      {
        get => this.m_AssetPool.ExpireTime;
        set => this.m_AssetPool.ExpireTime = value;
      }

      /// <summary>获取或设置资源对象池的优先级。</summary>
      public int AssetPriority
      {
        get => this.m_AssetPool.Priority;
        set => this.m_AssetPool.Priority = value;
      }

      /// <summary>获取或设置资源对象池自动释放可释放对象的间隔秒数。</summary>
      public float ResourceAutoReleaseInterval
      {
        get => this.m_ResourcePool.AutoReleaseInterval;
        set => this.m_ResourcePool.AutoReleaseInterval = value;
      }

      /// <summary>获取或设置资源对象池的容量。</summary>
      public int ResourceCapacity
      {
        get => this.m_ResourcePool.Capacity;
        set => this.m_ResourcePool.Capacity = value;
      }

      /// <summary>获取或设置资源对象池对象过期秒数。</summary>
      public float ResourceExpireTime
      {
        get => this.m_ResourcePool.ExpireTime;
        set => this.m_ResourcePool.ExpireTime = value;
      }

      /// <summary>获取或设置资源对象池的优先级。</summary>
      public int ResourcePriority
      {
        get => this.m_ResourcePool.Priority;
        set => this.m_ResourcePool.Priority = value;
      }

      /// <summary>加载资源器轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds) => this.m_TaskPool.Update(elapseSeconds, realElapseSeconds);

      /// <summary>关闭并清理加载资源器。</summary>
      public void Shutdown()
      {
        this.m_TaskPool.Shutdown();
        this.m_AssetDependencyCount.Clear();
        this.m_ResourceDependencyCount.Clear();
        this.m_AssetToResourceMap.Clear();
        this.m_SceneToAssetMap.Clear();
        ResourceManager.ResourceLoader.LoadResourceAgent.Clear();
      }

      /// <summary>设置对象池管理器。</summary>
      /// <param name="objectPoolManager">对象池管理器。</param>
      public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
      {
        this.m_AssetPool = objectPoolManager.CreateMultiSpawnObjectPool<ResourceManager.ResourceLoader.AssetObject>("Asset Pool");
        this.m_ResourcePool = objectPoolManager.CreateMultiSpawnObjectPool<ResourceManager.ResourceLoader.ResourceObject>("Resource Pool");
      }

      /// <summary>增加加载资源代理辅助器。</summary>
      /// <param name="loadResourceAgentHelper">要增加的加载资源代理辅助器。</param>
      /// <param name="resourceHelper">资源辅助器。</param>
      /// <param name="readOnlyPath">资源只读区路径。</param>
      /// <param name="readWritePath">资源读写区路径。</param>
      /// <param name="decryptResourceCallback">要设置的解密资源回调函数。</param>
      public void AddLoadResourceAgentHelper(
        ILoadResourceAgentHelper loadResourceAgentHelper,
        IResourceHelper resourceHelper,
        string readOnlyPath,
        string readWritePath,
        DecryptResourceCallback decryptResourceCallback)
      {
        if (this.m_AssetPool == null || this.m_ResourcePool == null)
          throw new GameFrameworkException("You must set object pool manager first.");
        this.m_TaskPool.AddAgent((ITaskAgent<ResourceManager.ResourceLoader.LoadResourceTaskBase>) new ResourceManager.ResourceLoader.LoadResourceAgent(loadResourceAgentHelper, resourceHelper, this, readOnlyPath, readWritePath, decryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback)));
      }

      /// <summary>检查资源是否存在。</summary>
      /// <param name="assetName">要检查资源的名称。</param>
      /// <returns>检查资源是否存在的结果。</returns>
      public HasAssetResult HasAsset(string assetName)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(assetName);
        if (resourceInfo == null)
          return HasAssetResult.NotExist;
        if (!resourceInfo.Ready && this.m_ResourceManager.m_ResourceMode != ResourceMode.UpdatableWhilePlaying)
          return HasAssetResult.NotReady;
        return resourceInfo.UseFileSystem ? (!resourceInfo.IsLoadFromBinary ? HasAssetResult.AssetOnFileSystem : HasAssetResult.BinaryOnFileSystem) : (!resourceInfo.IsLoadFromBinary ? HasAssetResult.AssetOnDisk : HasAssetResult.BinaryOnDisk);
      }

      /// <summary>异步加载资源。</summary>
      /// <param name="assetName">要加载资源的名称。</param>
      /// <param name="assetType">要加载资源的类型。</param>
      /// <param name="priority">加载资源的优先级。</param>
      /// <param name="loadAssetCallbacks">加载资源回调函数集。</param>
      /// <param name="userData">用户自定义数据。</param>
      public void LoadAsset(
        string assetName,
        Type assetType,
        int priority,
        LoadAssetCallbacks loadAssetCallbacks,
        object userData)
      {
        ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
        string[] dependencyAssetNames = (string[]) null;
        if (!this.CheckAsset(assetName, out resourceInfo, out dependencyAssetNames))
        {
          string str = Utility.Text.Format<string>("Can not load asset '{0}'.", assetName);
          if (loadAssetCallbacks.LoadAssetFailureCallback == null)
            throw new GameFrameworkException(str);
          loadAssetCallbacks.LoadAssetFailureCallback(assetName, resourceInfo == null || resourceInfo.Ready ? LoadResourceStatus.NotExist : LoadResourceStatus.NotReady, str, userData);
        }
        else if (resourceInfo.IsLoadFromBinary)
        {
          string str = Utility.Text.Format<string>("Can not load asset '{0}' which is a binary asset.", assetName);
          if (loadAssetCallbacks.LoadAssetFailureCallback == null)
            throw new GameFrameworkException(str);
          loadAssetCallbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.TypeError, str, userData);
        }
        else
        {
          ResourceManager.ResourceLoader.LoadAssetTask loadAssetTask = ResourceManager.ResourceLoader.LoadAssetTask.Create(assetName, assetType, priority, resourceInfo, dependencyAssetNames, loadAssetCallbacks, userData);
          foreach (string assetName1 in dependencyAssetNames)
          {
            if (!this.LoadDependencyAsset(assetName1, priority, (ResourceManager.ResourceLoader.LoadResourceTaskBase) loadAssetTask, userData))
            {
              string str = Utility.Text.Format<string, string>("Can not load dependency asset '{0}' when load asset '{1}'.", assetName1, assetName);
              if (loadAssetCallbacks.LoadAssetFailureCallback == null)
                throw new GameFrameworkException(str);
              loadAssetCallbacks.LoadAssetFailureCallback(assetName, LoadResourceStatus.DependencyError, str, userData);
              return;
            }
          }
          this.m_TaskPool.AddTask((ResourceManager.ResourceLoader.LoadResourceTaskBase) loadAssetTask);
          if (resourceInfo.Ready)
            return;
          this.m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
        }
      }

      /// <summary>卸载资源。</summary>
      /// <param name="asset">要卸载的资源。</param>
      public void UnloadAsset(object asset) => this.m_AssetPool.Unspawn(asset);

      /// <summary>异步加载场景。</summary>
      /// <param name="sceneAssetName">要加载场景资源的名称。</param>
      /// <param name="priority">加载场景资源的优先级。</param>
      /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
      /// <param name="userData">用户自定义数据。</param>
      public void LoadScene(
        string sceneAssetName,
        int priority,
        LoadSceneCallbacks loadSceneCallbacks,
        object userData)
      {
        ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
        string[] dependencyAssetNames = (string[]) null;
        if (!this.CheckAsset(sceneAssetName, out resourceInfo, out dependencyAssetNames))
        {
          string str = Utility.Text.Format<string>("Can not load scene '{0}'.", sceneAssetName);
          if (loadSceneCallbacks.LoadSceneFailureCallback == null)
            throw new GameFrameworkException(str);
          loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, resourceInfo == null || resourceInfo.Ready ? LoadResourceStatus.NotExist : LoadResourceStatus.NotReady, str, userData);
        }
        else if (resourceInfo.IsLoadFromBinary)
        {
          string str = Utility.Text.Format<string>("Can not load scene asset '{0}' which is a binary asset.", sceneAssetName);
          if (loadSceneCallbacks.LoadSceneFailureCallback == null)
            throw new GameFrameworkException(str);
          loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.TypeError, str, userData);
        }
        else
        {
          ResourceManager.ResourceLoader.LoadSceneTask loadSceneTask = ResourceManager.ResourceLoader.LoadSceneTask.Create(sceneAssetName, priority, resourceInfo, dependencyAssetNames, loadSceneCallbacks, userData);
          foreach (string assetName in dependencyAssetNames)
          {
            if (!this.LoadDependencyAsset(assetName, priority, (ResourceManager.ResourceLoader.LoadResourceTaskBase) loadSceneTask, userData))
            {
              string str = Utility.Text.Format<string, string>("Can not load dependency asset '{0}' when load scene '{1}'.", assetName, sceneAssetName);
              if (loadSceneCallbacks.LoadSceneFailureCallback == null)
                throw new GameFrameworkException(str);
              loadSceneCallbacks.LoadSceneFailureCallback(sceneAssetName, LoadResourceStatus.DependencyError, str, userData);
              return;
            }
          }
          this.m_TaskPool.AddTask((ResourceManager.ResourceLoader.LoadResourceTaskBase) loadSceneTask);
          if (resourceInfo.Ready)
            return;
          this.m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
        }
      }

      /// <summary>异步卸载场景。</summary>
      /// <param name="sceneAssetName">要卸载场景资源的名称。</param>
      /// <param name="unloadSceneCallbacks">卸载场景回调函数集。</param>
      /// <param name="userData">用户自定义数据。</param>
      public void UnloadScene(
        string sceneAssetName,
        UnloadSceneCallbacks unloadSceneCallbacks,
        object userData)
      {
        if (this.m_ResourceManager.m_ResourceHelper == null)
          throw new GameFrameworkException("You must set resource helper first.");
        object target = (object) null;
        if (!this.m_SceneToAssetMap.TryGetValue(sceneAssetName, out target))
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not find asset of scene '{0}'.", sceneAssetName));
        this.m_SceneToAssetMap.Remove(sceneAssetName);
        this.m_AssetPool.Unspawn(target);
        this.m_AssetPool.ReleaseObject(target);
        this.m_ResourceManager.m_ResourceHelper.UnloadScene(sceneAssetName, unloadSceneCallbacks, userData);
      }

      /// <summary>获取二进制资源的实际路径。</summary>
      /// <param name="binaryAssetName">要获取实际路径的二进制资源的名称。</param>
      /// <returns>二进制资源的实际路径。</returns>
      /// <remarks>此方法仅适用于二进制资源存储在磁盘（而非文件系统）中的情况。若二进制资源存储在文件系统中时，返回值将始终为空。</remarks>
      public string GetBinaryPath(string binaryAssetName)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
          return (string) null;
        if (!resourceInfo.Ready)
          return (string) null;
        if (!resourceInfo.IsLoadFromBinary)
          return (string) null;
        return resourceInfo.UseFileSystem ? (string) null : Utility.Path.GetRegularPath(System.IO.Path.Combine(resourceInfo.StorageInReadOnly ? this.m_ResourceManager.m_ReadOnlyPath : this.m_ResourceManager.m_ReadWritePath, resourceInfo.ResourceName.FullName));
      }

      /// <summary>获取二进制资源的实际路径。</summary>
      /// <param name="binaryAssetName">要获取实际路径的二进制资源的名称。</param>
      /// <param name="storageInReadOnly">二进制资源是否存储在只读区中。</param>
      /// <param name="storageInFileSystem">二进制资源是否存储在文件系统中。</param>
      /// <param name="relativePath">二进制资源或存储二进制资源的文件系统，相对于只读区或者读写区的相对路径。</param>
      /// <param name="fileName">若二进制资源存储在文件系统中，则指示二进制资源在文件系统中的名称，否则此参数返回空。</param>
      /// <returns>是否获取二进制资源的实际路径成功。</returns>
      public bool GetBinaryPath(
        string binaryAssetName,
        out bool storageInReadOnly,
        out bool storageInFileSystem,
        out string relativePath,
        out string fileName)
      {
        storageInReadOnly = false;
        storageInFileSystem = false;
        relativePath = (string) null;
        fileName = (string) null;
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null || !resourceInfo.Ready || !resourceInfo.IsLoadFromBinary)
          return false;
        storageInReadOnly = resourceInfo.StorageInReadOnly;
        if (resourceInfo.UseFileSystem)
        {
          storageInFileSystem = true;
          relativePath = Utility.Text.Format<string, string>("{0}.{1}", resourceInfo.FileSystemName, "dat");
          fileName = resourceInfo.ResourceName.FullName;
        }
        else
          relativePath = resourceInfo.ResourceName.FullName;
        return true;
      }

      /// <summary>获取二进制资源的长度。</summary>
      /// <param name="binaryAssetName">要获取长度的二进制资源的名称。</param>
      /// <returns>二进制资源的长度。</returns>
      public int GetBinaryLength(string binaryAssetName)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        return resourceInfo == null || !resourceInfo.Ready || !resourceInfo.IsLoadFromBinary ? -1 : resourceInfo.Length;
      }

      /// <summary>异步加载二进制资源。</summary>
      /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
      /// <param name="loadBinaryCallbacks">加载二进制资源回调函数集。</param>
      /// <param name="userData">用户自定义数据。</param>
      public void LoadBinary(
        string binaryAssetName,
        LoadBinaryCallbacks loadBinaryCallbacks,
        object userData)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
        {
          string str = Utility.Text.Format<string>("Can not load binary '{0}' which is not exist.", binaryAssetName);
          if (loadBinaryCallbacks.LoadBinaryFailureCallback == null)
            throw new GameFrameworkException(str);
          loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotExist, str, userData);
        }
        else if (!resourceInfo.Ready)
        {
          string str = Utility.Text.Format<string>("Can not load binary '{0}' which is not ready.", binaryAssetName);
          if (loadBinaryCallbacks.LoadBinaryFailureCallback == null)
            throw new GameFrameworkException(str);
          loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.NotReady, str, userData);
        }
        else if (!resourceInfo.IsLoadFromBinary)
        {
          string str = Utility.Text.Format<string>("Can not load binary '{0}' which is not a binary asset.", binaryAssetName);
          if (loadBinaryCallbacks.LoadBinaryFailureCallback == null)
            throw new GameFrameworkException(str);
          loadBinaryCallbacks.LoadBinaryFailureCallback(binaryAssetName, LoadResourceStatus.TypeError, str, userData);
        }
        else if (resourceInfo.UseFileSystem)
          loadBinaryCallbacks.LoadBinarySuccessCallback(binaryAssetName, this.LoadBinaryFromFileSystem(binaryAssetName), 0.0f, userData);
        else
          this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(resourceInfo.StorageInReadOnly ? this.m_ResourceManager.m_ReadOnlyPath : this.m_ResourceManager.m_ReadWritePath, resourceInfo.ResourceName.FullName)), this.m_LoadBytesCallbacks, (object) ResourceManager.ResourceLoader.LoadBinaryInfo.Create(binaryAssetName, resourceInfo, loadBinaryCallbacks, userData));
      }

      /// <summary>从文件系统中加载二进制资源。</summary>
      /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
      /// <returns>存储加载二进制资源的二进制流。</returns>
      public byte[] LoadBinaryFromFileSystem(string binaryAssetName)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
        if (!resourceInfo.Ready)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
        if (!resourceInfo.IsLoadFromBinary)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
        if (!resourceInfo.UseFileSystem)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
        IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
        ResourceManager.ResourceName resourceName = resourceInfo.ResourceName;
        string fullName = resourceName.FullName;
        byte[] numArray = fileSystem.ReadFile(fullName);
        if (numArray == null)
          return (byte[]) null;
        if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
        {
          DecryptResourceCallback resourceCallback = this.m_ResourceManager.m_DecryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback);
          byte[] bytes = numArray;
          int length1 = numArray.Length;
          resourceName = resourceInfo.ResourceName;
          string name = resourceName.Name;
          resourceName = resourceInfo.ResourceName;
          string variant = resourceName.Variant;
          resourceName = resourceInfo.ResourceName;
          string extension = resourceName.Extension;
          int num = resourceInfo.StorageInReadOnly ? 1 : 0;
          string fileSystemName = resourceInfo.FileSystemName;
          int loadType = (int) resourceInfo.LoadType;
          int length2 = resourceInfo.Length;
          int hashCode = resourceInfo.HashCode;
          resourceCallback(bytes, 0, length1, name, variant, extension, num != 0, fileSystemName, (byte) loadType, length2, hashCode);
        }
        return numArray;
      }

      /// <summary>从文件系统中加载二进制资源。</summary>
      /// <param name="binaryAssetName">要加载二进制资源的名称。</param>
      /// <param name="buffer">存储加载二进制资源的二进制流。</param>
      /// <param name="startIndex">存储加载二进制资源的二进制流的起始位置。</param>
      /// <param name="length">存储加载二进制资源的二进制流的长度。</param>
      /// <returns>实际加载了多少字节。</returns>
      public int LoadBinaryFromFileSystem(
        string binaryAssetName,
        byte[] buffer,
        int startIndex,
        int length)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
        if (!resourceInfo.Ready)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
        if (!resourceInfo.IsLoadFromBinary)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
        if (!resourceInfo.UseFileSystem)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
        IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
        ResourceManager.ResourceName resourceName = resourceInfo.ResourceName;
        string fullName = resourceName.FullName;
        byte[] buffer1 = buffer;
        int startIndex1 = startIndex;
        int length1 = length;
        int num1 = fileSystem.ReadFile(fullName, buffer1, startIndex1, length1);
        if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
        {
          DecryptResourceCallback resourceCallback = this.m_ResourceManager.m_DecryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback);
          byte[] bytes = buffer;
          int startIndex2 = startIndex;
          int count = num1;
          resourceName = resourceInfo.ResourceName;
          string name = resourceName.Name;
          resourceName = resourceInfo.ResourceName;
          string variant = resourceName.Variant;
          resourceName = resourceInfo.ResourceName;
          string extension = resourceName.Extension;
          int num2 = resourceInfo.StorageInReadOnly ? 1 : 0;
          string fileSystemName = resourceInfo.FileSystemName;
          int loadType = (int) resourceInfo.LoadType;
          int length2 = resourceInfo.Length;
          int hashCode = resourceInfo.HashCode;
          resourceCallback(bytes, startIndex2, count, name, variant, extension, num2 != 0, fileSystemName, (byte) loadType, length2, hashCode);
        }
        return num1;
      }

      /// <summary>从文件系统中加载二进制资源的片段。</summary>
      /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
      /// <param name="offset">要加载片段的偏移。</param>
      /// <param name="length">要加载片段的长度。</param>
      /// <returns>存储加载二进制资源片段内容的二进制流。</returns>
      public byte[] LoadBinarySegmentFromFileSystem(string binaryAssetName, int offset, int length)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
        if (!resourceInfo.Ready)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
        if (!resourceInfo.IsLoadFromBinary)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
        if (!resourceInfo.UseFileSystem)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
        IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
        ResourceManager.ResourceName resourceName = resourceInfo.ResourceName;
        string fullName = resourceName.FullName;
        int offset1 = offset;
        int length1 = length;
        byte[] numArray = fileSystem.ReadFileSegment(fullName, offset1, length1);
        if (numArray == null)
          return (byte[]) null;
        if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
        {
          DecryptResourceCallback resourceCallback = this.m_ResourceManager.m_DecryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback);
          byte[] bytes = numArray;
          int length2 = numArray.Length;
          resourceName = resourceInfo.ResourceName;
          string name = resourceName.Name;
          resourceName = resourceInfo.ResourceName;
          string variant = resourceName.Variant;
          resourceName = resourceInfo.ResourceName;
          string extension = resourceName.Extension;
          int num = resourceInfo.StorageInReadOnly ? 1 : 0;
          string fileSystemName = resourceInfo.FileSystemName;
          int loadType = (int) resourceInfo.LoadType;
          int length3 = resourceInfo.Length;
          int hashCode = resourceInfo.HashCode;
          resourceCallback(bytes, 0, length2, name, variant, extension, num != 0, fileSystemName, (byte) loadType, length3, hashCode);
        }
        return numArray;
      }

      /// <summary>从文件系统中加载二进制资源的片段。</summary>
      /// <param name="binaryAssetName">要加载片段的二进制资源的名称。</param>
      /// <param name="offset">要加载片段的偏移。</param>
      /// <param name="buffer">存储加载二进制资源片段内容的二进制流。</param>
      /// <param name="startIndex">存储加载二进制资源片段内容的二进制流的起始位置。</param>
      /// <param name="length">要加载片段的长度。</param>
      /// <returns>实际加载了多少字节。</returns>
      public int LoadBinarySegmentFromFileSystem(
        string binaryAssetName,
        int offset,
        byte[] buffer,
        int startIndex,
        int length)
      {
        ResourceManager.ResourceInfo resourceInfo = this.GetResourceInfo(binaryAssetName);
        if (resourceInfo == null)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not exist.", binaryAssetName));
        if (!resourceInfo.Ready)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not ready.", binaryAssetName));
        if (!resourceInfo.IsLoadFromBinary)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not a binary asset.", binaryAssetName));
        if (!resourceInfo.UseFileSystem)
          throw new GameFrameworkException(Utility.Text.Format<string>("Can not load binary '{0}' from file system which is not use file system.", binaryAssetName));
        IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly);
        ResourceManager.ResourceName resourceName = resourceInfo.ResourceName;
        string fullName = resourceName.FullName;
        int offset1 = offset;
        byte[] buffer1 = buffer;
        int startIndex1 = startIndex;
        int length1 = length;
        int num1 = fileSystem.ReadFileSegment(fullName, offset1, buffer1, startIndex1, length1);
        if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
        {
          DecryptResourceCallback resourceCallback = this.m_ResourceManager.m_DecryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback);
          byte[] bytes = buffer;
          int startIndex2 = startIndex;
          int count = num1;
          resourceName = resourceInfo.ResourceName;
          string name = resourceName.Name;
          resourceName = resourceInfo.ResourceName;
          string variant = resourceName.Variant;
          resourceName = resourceInfo.ResourceName;
          string extension = resourceName.Extension;
          int num2 = resourceInfo.StorageInReadOnly ? 1 : 0;
          string fileSystemName = resourceInfo.FileSystemName;
          int loadType = (int) resourceInfo.LoadType;
          int length2 = resourceInfo.Length;
          int hashCode = resourceInfo.HashCode;
          resourceCallback(bytes, startIndex2, count, name, variant, extension, num2 != 0, fileSystemName, (byte) loadType, length2, hashCode);
        }
        return num1;
      }

      /// <summary>获取所有加载资源任务的信息。</summary>
      /// <returns>所有加载资源任务的信息。</returns>
      public TaskInfo[] GetAllLoadAssetInfos() => this.m_TaskPool.GetAllTaskInfos();

      /// <summary>获取所有加载资源任务的信息。</summary>
      /// <param name="results">所有加载资源任务的信息。</param>
      public void GetAllLoadAssetInfos(List<TaskInfo> results) => this.m_TaskPool.GetAllTaskInfos(results);

      private bool LoadDependencyAsset(
        string assetName,
        int priority,
        ResourceManager.ResourceLoader.LoadResourceTaskBase mainTask,
        object userData)
      {
        if (mainTask == null)
          throw new GameFrameworkException("Main task is invalid.");
        ResourceManager.ResourceInfo resourceInfo = (ResourceManager.ResourceInfo) null;
        string[] dependencyAssetNames = (string[]) null;
        if (!this.CheckAsset(assetName, out resourceInfo, out dependencyAssetNames) || resourceInfo.IsLoadFromBinary)
          return false;
        ResourceManager.ResourceLoader.LoadDependencyAssetTask dependencyAssetTask = ResourceManager.ResourceLoader.LoadDependencyAssetTask.Create(assetName, priority, resourceInfo, dependencyAssetNames, mainTask, userData);
        foreach (string assetName1 in dependencyAssetNames)
        {
          if (!this.LoadDependencyAsset(assetName1, priority, (ResourceManager.ResourceLoader.LoadResourceTaskBase) dependencyAssetTask, userData))
            return false;
        }
        this.m_TaskPool.AddTask((ResourceManager.ResourceLoader.LoadResourceTaskBase) dependencyAssetTask);
        if (!resourceInfo.Ready)
          this.m_ResourceManager.UpdateResource(resourceInfo.ResourceName);
        return true;
      }

      private ResourceManager.ResourceInfo GetResourceInfo(string assetName)
      {
        if (string.IsNullOrEmpty(assetName))
          return (ResourceManager.ResourceInfo) null;
        ResourceManager.AssetInfo assetInfo = this.m_ResourceManager.GetAssetInfo(assetName);
        return assetInfo == null ? (ResourceManager.ResourceInfo) null : this.m_ResourceManager.GetResourceInfo(assetInfo.ResourceName);
      }

      private bool CheckAsset(
        string assetName,
        out ResourceManager.ResourceInfo resourceInfo,
        out string[] dependencyAssetNames)
      {
        resourceInfo = (ResourceManager.ResourceInfo) null;
        dependencyAssetNames = (string[]) null;
        if (string.IsNullOrEmpty(assetName))
          return false;
        ResourceManager.AssetInfo assetInfo = this.m_ResourceManager.GetAssetInfo(assetName);
        if (assetInfo == null)
          return false;
        resourceInfo = this.m_ResourceManager.GetResourceInfo(assetInfo.ResourceName);
        if (resourceInfo == null)
          return false;
        dependencyAssetNames = assetInfo.GetDependencyAssetNames();
        return this.m_ResourceManager.m_ResourceMode == ResourceMode.UpdatableWhilePlaying || resourceInfo.Ready;
      }

      private void DefaultDecryptResourceCallback(
        byte[] bytes,
        int startIndex,
        int count,
        string name,
        string variant,
        string extension,
        bool storageInReadOnly,
        string fileSystem,
        byte loadType,
        int length,
        int hashCode)
      {
        Utility.Converter.GetBytes(hashCode, this.m_CachedHashBytes);
        switch (loadType)
        {
          case 2:
          case 5:
            Utility.Encryption.GetQuickSelfXorBytes(bytes, this.m_CachedHashBytes);
            break;
          case 3:
          case 6:
            Utility.Encryption.GetSelfXorBytes(bytes, this.m_CachedHashBytes);
            break;
          default:
            throw new GameFrameworkException("Not supported load type when decrypt resource.");
        }
        Array.Clear((Array) this.m_CachedHashBytes, 0, 4);
      }

      private void OnLoadBinarySuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        ResourceManager.ResourceLoader.LoadBinaryInfo loadBinaryInfo = (ResourceManager.ResourceLoader.LoadBinaryInfo) userData;
        ResourceManager.ResourceInfo resourceInfo = loadBinaryInfo != null ? loadBinaryInfo.ResourceInfo : throw new GameFrameworkException("Load binary info is invalid.");
        if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
          (this.m_ResourceManager.m_DecryptResourceCallback ?? new DecryptResourceCallback(this.DefaultDecryptResourceCallback))(bytes, 0, bytes.Length, resourceInfo.ResourceName.Name, resourceInfo.ResourceName.Variant, resourceInfo.ResourceName.Extension, resourceInfo.StorageInReadOnly, resourceInfo.FileSystemName, (byte) resourceInfo.LoadType, resourceInfo.Length, resourceInfo.HashCode);
        loadBinaryInfo.LoadBinaryCallbacks.LoadBinarySuccessCallback(loadBinaryInfo.BinaryAssetName, bytes, duration, loadBinaryInfo.UserData);
        ReferencePool.Release((IReference) loadBinaryInfo);
      }

      private void OnLoadBinaryFailure(string fileUri, string errorMessage, object userData)
      {
        ResourceManager.ResourceLoader.LoadBinaryInfo loadBinaryInfo = (ResourceManager.ResourceLoader.LoadBinaryInfo) userData;
        if (loadBinaryInfo == null)
          throw new GameFrameworkException("Load binary info is invalid.");
        if (loadBinaryInfo.LoadBinaryCallbacks.LoadBinaryFailureCallback != null)
          loadBinaryInfo.LoadBinaryCallbacks.LoadBinaryFailureCallback(loadBinaryInfo.BinaryAssetName, LoadResourceStatus.AssetError, errorMessage, loadBinaryInfo.UserData);
        ReferencePool.Release((IReference) loadBinaryInfo);
      }

      /// <summary>资源对象。</summary>
      private sealed class AssetObject : ObjectBase
      {
        private List<object> m_DependencyAssets;
        private object m_Resource;
        private IResourceHelper m_ResourceHelper;
        private ResourceManager.ResourceLoader m_ResourceLoader;

        public AssetObject()
        {
          this.m_DependencyAssets = new List<object>();
          this.m_Resource = (object) null;
          this.m_ResourceHelper = (IResourceHelper) null;
          this.m_ResourceLoader = (ResourceManager.ResourceLoader) null;
        }

        public override bool CustomCanReleaseFlag
        {
          get
          {
            int num = 0;
            this.m_ResourceLoader.m_AssetDependencyCount.TryGetValue(this.Target, out num);
            return base.CustomCanReleaseFlag && num <= 0;
          }
        }

        public static ResourceManager.ResourceLoader.AssetObject Create(
          string name,
          object target,
          List<object> dependencyAssets,
          object resource,
          IResourceHelper resourceHelper,
          ResourceManager.ResourceLoader resourceLoader)
        {
          if (dependencyAssets == null)
            throw new GameFrameworkException("Dependency assets is invalid.");
          if (resource == null)
            throw new GameFrameworkException("Resource is invalid.");
          if (resourceHelper == null)
            throw new GameFrameworkException("Resource helper is invalid.");
          if (resourceLoader == null)
            throw new GameFrameworkException("Resource loader is invalid.");
          ResourceManager.ResourceLoader.AssetObject assetObject = ReferencePool.Acquire<ResourceManager.ResourceLoader.AssetObject>();
          assetObject.Initialize(name, target);
          assetObject.m_DependencyAssets.AddRange((IEnumerable<object>) dependencyAssets);
          assetObject.m_Resource = resource;
          assetObject.m_ResourceHelper = resourceHelper;
          assetObject.m_ResourceLoader = resourceLoader;
          foreach (object dependencyAsset in dependencyAssets)
          {
            int num = 0;
            if (resourceLoader.m_AssetDependencyCount.TryGetValue(dependencyAsset, out num))
              resourceLoader.m_AssetDependencyCount[dependencyAsset] = num + 1;
            else
              resourceLoader.m_AssetDependencyCount.Add(dependencyAsset, 1);
          }
          return assetObject;
        }

        public override void Clear()
        {
          base.Clear();
          this.m_DependencyAssets.Clear();
          this.m_Resource = (object) null;
          this.m_ResourceHelper = (IResourceHelper) null;
          this.m_ResourceLoader = (ResourceManager.ResourceLoader) null;
        }

        protected internal override void OnUnspawn()
        {
          base.OnUnspawn();
          foreach (object dependencyAsset in this.m_DependencyAssets)
            this.m_ResourceLoader.m_AssetPool.Unspawn(dependencyAsset);
        }

        protected internal override void Release(bool isShutdown)
        {
          if (!isShutdown)
          {
            int num1 = 0;
            if (this.m_ResourceLoader.m_AssetDependencyCount.TryGetValue(this.Target, out num1) && num1 > 0)
              throw new GameFrameworkException(Utility.Text.Format<string, int>("Asset target '{0}' reference count is '{1}' larger than 0.", this.Name, num1));
            foreach (object dependencyAsset in this.m_DependencyAssets)
            {
              int num2 = 0;
              if (!this.m_ResourceLoader.m_AssetDependencyCount.TryGetValue(dependencyAsset, out num2))
                throw new GameFrameworkException(Utility.Text.Format<string>("Asset target '{0}' dependency asset reference count is invalid.", this.Name));
              this.m_ResourceLoader.m_AssetDependencyCount[dependencyAsset] = num2 - 1;
            }
            this.m_ResourceLoader.m_ResourcePool.Unspawn(this.m_Resource);
          }
          this.m_ResourceLoader.m_AssetDependencyCount.Remove(this.Target);
          this.m_ResourceLoader.m_AssetToResourceMap.Remove(this.Target);
          this.m_ResourceHelper.Release(this.Target);
        }
      }

      private sealed class LoadAssetTask : ResourceManager.ResourceLoader.LoadResourceTaskBase
      {
        private LoadAssetCallbacks m_LoadAssetCallbacks;

        public LoadAssetTask() => this.m_LoadAssetCallbacks = (LoadAssetCallbacks) null;

        public override bool IsScene => false;

        public static ResourceManager.ResourceLoader.LoadAssetTask Create(
          string assetName,
          Type assetType,
          int priority,
          ResourceManager.ResourceInfo resourceInfo,
          string[] dependencyAssetNames,
          LoadAssetCallbacks loadAssetCallbacks,
          object userData)
        {
          ResourceManager.ResourceLoader.LoadAssetTask loadAssetTask = ReferencePool.Acquire<ResourceManager.ResourceLoader.LoadAssetTask>();
          loadAssetTask.Initialize(assetName, assetType, priority, resourceInfo, dependencyAssetNames, userData);
          loadAssetTask.m_LoadAssetCallbacks = loadAssetCallbacks;
          return loadAssetTask;
        }

        public override void Clear()
        {
          base.Clear();
          this.m_LoadAssetCallbacks = (LoadAssetCallbacks) null;
        }

        public override void OnLoadAssetSuccess(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          object asset,
          float duration)
        {
          base.OnLoadAssetSuccess(agent, asset, duration);
          if (this.m_LoadAssetCallbacks.LoadAssetSuccessCallback == null)
            return;
          this.m_LoadAssetCallbacks.LoadAssetSuccessCallback(this.AssetName, asset, duration, this.UserData);
        }

        public override void OnLoadAssetFailure(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceStatus status,
          string errorMessage)
        {
          base.OnLoadAssetFailure(agent, status, errorMessage);
          if (this.m_LoadAssetCallbacks.LoadAssetFailureCallback == null)
            return;
          this.m_LoadAssetCallbacks.LoadAssetFailureCallback(this.AssetName, status, errorMessage, this.UserData);
        }

        public override void OnLoadAssetUpdate(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceProgress type,
          float progress)
        {
          base.OnLoadAssetUpdate(agent, type, progress);
          if (type != LoadResourceProgress.LoadAsset || this.m_LoadAssetCallbacks.LoadAssetUpdateCallback == null)
            return;
          this.m_LoadAssetCallbacks.LoadAssetUpdateCallback(this.AssetName, progress, this.UserData);
        }

        public override void OnLoadDependencyAsset(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          string dependencyAssetName,
          object dependencyAsset)
        {
          base.OnLoadDependencyAsset(agent, dependencyAssetName, dependencyAsset);
          if (this.m_LoadAssetCallbacks.LoadAssetDependencyAssetCallback == null)
            return;
          this.m_LoadAssetCallbacks.LoadAssetDependencyAssetCallback(this.AssetName, dependencyAssetName, this.LoadedDependencyAssetCount, this.TotalDependencyAssetCount, this.UserData);
        }
      }

      private sealed class LoadBinaryInfo : IReference
      {
        private string m_BinaryAssetName;
        private ResourceManager.ResourceInfo m_ResourceInfo;
        private LoadBinaryCallbacks m_LoadBinaryCallbacks;
        private object m_UserData;

        public LoadBinaryInfo()
        {
          this.m_BinaryAssetName = (string) null;
          this.m_ResourceInfo = (ResourceManager.ResourceInfo) null;
          this.m_LoadBinaryCallbacks = (LoadBinaryCallbacks) null;
          this.m_UserData = (object) null;
        }

        public string BinaryAssetName => this.m_BinaryAssetName;

        public ResourceManager.ResourceInfo ResourceInfo => this.m_ResourceInfo;

        public LoadBinaryCallbacks LoadBinaryCallbacks => this.m_LoadBinaryCallbacks;

        public object UserData => this.m_UserData;

        public static ResourceManager.ResourceLoader.LoadBinaryInfo Create(
          string binaryAssetName,
          ResourceManager.ResourceInfo resourceInfo,
          LoadBinaryCallbacks loadBinaryCallbacks,
          object userData)
        {
          ResourceManager.ResourceLoader.LoadBinaryInfo loadBinaryInfo = ReferencePool.Acquire<ResourceManager.ResourceLoader.LoadBinaryInfo>();
          loadBinaryInfo.m_BinaryAssetName = binaryAssetName;
          loadBinaryInfo.m_ResourceInfo = resourceInfo;
          loadBinaryInfo.m_LoadBinaryCallbacks = loadBinaryCallbacks;
          loadBinaryInfo.m_UserData = userData;
          return loadBinaryInfo;
        }

        public void Clear()
        {
          this.m_BinaryAssetName = (string) null;
          this.m_ResourceInfo = (ResourceManager.ResourceInfo) null;
          this.m_LoadBinaryCallbacks = (LoadBinaryCallbacks) null;
          this.m_UserData = (object) null;
        }
      }

      private sealed class LoadDependencyAssetTask : 
        ResourceManager.ResourceLoader.LoadResourceTaskBase
      {
        private ResourceManager.ResourceLoader.LoadResourceTaskBase m_MainTask;

        public LoadDependencyAssetTask() => this.m_MainTask = (ResourceManager.ResourceLoader.LoadResourceTaskBase) null;

        public override bool IsScene => false;

        public static ResourceManager.ResourceLoader.LoadDependencyAssetTask Create(
          string assetName,
          int priority,
          ResourceManager.ResourceInfo resourceInfo,
          string[] dependencyAssetNames,
          ResourceManager.ResourceLoader.LoadResourceTaskBase mainTask,
          object userData)
        {
          ResourceManager.ResourceLoader.LoadDependencyAssetTask dependencyAssetTask = ReferencePool.Acquire<ResourceManager.ResourceLoader.LoadDependencyAssetTask>();
          dependencyAssetTask.Initialize(assetName, (Type) null, priority, resourceInfo, dependencyAssetNames, userData);
          dependencyAssetTask.m_MainTask = mainTask;
          ++dependencyAssetTask.m_MainTask.TotalDependencyAssetCount;
          return dependencyAssetTask;
        }

        public override void Clear()
        {
          base.Clear();
          this.m_MainTask = (ResourceManager.ResourceLoader.LoadResourceTaskBase) null;
        }

        public override void OnLoadAssetSuccess(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          object asset,
          float duration)
        {
          base.OnLoadAssetSuccess(agent, asset, duration);
          this.m_MainTask.OnLoadDependencyAsset(agent, this.AssetName, asset);
        }

        public override void OnLoadAssetFailure(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceStatus status,
          string errorMessage)
        {
          base.OnLoadAssetFailure(agent, status, errorMessage);
          this.m_MainTask.OnLoadAssetFailure(agent, LoadResourceStatus.DependencyError, Utility.Text.Format<string, LoadResourceStatus, string>("Can not load dependency asset '{0}', internal status '{1}', internal error message '{2}'.", this.AssetName, status, errorMessage));
        }
      }

      /// <summary>加载资源代理。</summary>
      private sealed class LoadResourceAgent : 
        ITaskAgent<ResourceManager.ResourceLoader.LoadResourceTaskBase>
      {
        private static readonly Dictionary<string, string> s_CachedResourceNames = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.Ordinal);
        private static readonly HashSet<string> s_LoadingAssetNames = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
        private static readonly HashSet<string> s_LoadingResourceNames = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
        private readonly ILoadResourceAgentHelper m_Helper;
        private readonly IResourceHelper m_ResourceHelper;
        private readonly ResourceManager.ResourceLoader m_ResourceLoader;
        private readonly string m_ReadOnlyPath;
        private readonly string m_ReadWritePath;
        private readonly DecryptResourceCallback m_DecryptResourceCallback;
        private ResourceManager.ResourceLoader.LoadResourceTaskBase m_Task;

        /// <summary>初始化加载资源代理的新实例。</summary>
        /// <param name="loadResourceAgentHelper">加载资源代理辅助器。</param>
        /// <param name="resourceHelper">资源辅助器。</param>
        /// <param name="resourceLoader">加载资源器。</param>
        /// <param name="readOnlyPath">资源只读区路径。</param>
        /// <param name="readWritePath">资源读写区路径。</param>
        /// <param name="decryptResourceCallback">解密资源回调函数。</param>
        public LoadResourceAgent(
          ILoadResourceAgentHelper loadResourceAgentHelper,
          IResourceHelper resourceHelper,
          ResourceManager.ResourceLoader resourceLoader,
          string readOnlyPath,
          string readWritePath,
          DecryptResourceCallback decryptResourceCallback)
        {
          if (loadResourceAgentHelper == null)
            throw new GameFrameworkException("Load resource agent helper is invalid.");
          if (resourceHelper == null)
            throw new GameFrameworkException("Resource helper is invalid.");
          if (resourceLoader == null)
            throw new GameFrameworkException("Resource loader is invalid.");
          if (decryptResourceCallback == null)
            throw new GameFrameworkException("Decrypt resource callback is invalid.");
          this.m_Helper = loadResourceAgentHelper;
          this.m_ResourceHelper = resourceHelper;
          this.m_ResourceLoader = resourceLoader;
          this.m_ReadOnlyPath = readOnlyPath;
          this.m_ReadWritePath = readWritePath;
          this.m_DecryptResourceCallback = decryptResourceCallback;
          this.m_Task = (ResourceManager.ResourceLoader.LoadResourceTaskBase) null;
        }

        public ILoadResourceAgentHelper Helper => this.m_Helper;

        /// <summary>获取加载资源任务。</summary>
        public ResourceManager.ResourceLoader.LoadResourceTaskBase Task => this.m_Task;

        /// <summary>初始化加载资源代理。</summary>
        public void Initialize()
        {
          this.m_Helper.LoadResourceAgentHelperUpdate += new EventHandler<LoadResourceAgentHelperUpdateEventArgs>(this.OnLoadResourceAgentHelperUpdate);
          this.m_Helper.LoadResourceAgentHelperReadFileComplete += new EventHandler<LoadResourceAgentHelperReadFileCompleteEventArgs>(this.OnLoadResourceAgentHelperReadFileComplete);
          this.m_Helper.LoadResourceAgentHelperReadBytesComplete += new EventHandler<LoadResourceAgentHelperReadBytesCompleteEventArgs>(this.OnLoadResourceAgentHelperReadBytesComplete);
          this.m_Helper.LoadResourceAgentHelperParseBytesComplete += new EventHandler<LoadResourceAgentHelperParseBytesCompleteEventArgs>(this.OnLoadResourceAgentHelperParseBytesComplete);
          this.m_Helper.LoadResourceAgentHelperLoadComplete += new EventHandler<LoadResourceAgentHelperLoadCompleteEventArgs>(this.OnLoadResourceAgentHelperLoadComplete);
          this.m_Helper.LoadResourceAgentHelperError += new EventHandler<LoadResourceAgentHelperErrorEventArgs>(this.OnLoadResourceAgentHelperError);
        }

        /// <summary>加载资源代理轮询。</summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>关闭并清理加载资源代理。</summary>
        public void Shutdown()
        {
          this.Reset();
          this.m_Helper.LoadResourceAgentHelperUpdate -= new EventHandler<LoadResourceAgentHelperUpdateEventArgs>(this.OnLoadResourceAgentHelperUpdate);
          this.m_Helper.LoadResourceAgentHelperReadFileComplete -= new EventHandler<LoadResourceAgentHelperReadFileCompleteEventArgs>(this.OnLoadResourceAgentHelperReadFileComplete);
          this.m_Helper.LoadResourceAgentHelperReadBytesComplete -= new EventHandler<LoadResourceAgentHelperReadBytesCompleteEventArgs>(this.OnLoadResourceAgentHelperReadBytesComplete);
          this.m_Helper.LoadResourceAgentHelperParseBytesComplete -= new EventHandler<LoadResourceAgentHelperParseBytesCompleteEventArgs>(this.OnLoadResourceAgentHelperParseBytesComplete);
          this.m_Helper.LoadResourceAgentHelperLoadComplete -= new EventHandler<LoadResourceAgentHelperLoadCompleteEventArgs>(this.OnLoadResourceAgentHelperLoadComplete);
          this.m_Helper.LoadResourceAgentHelperError -= new EventHandler<LoadResourceAgentHelperErrorEventArgs>(this.OnLoadResourceAgentHelperError);
        }

        public static void Clear()
        {
          ResourceManager.ResourceLoader.LoadResourceAgent.s_CachedResourceNames.Clear();
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingAssetNames.Clear();
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Clear();
        }

        /// <summary>开始处理加载资源任务。</summary>
        /// <param name="task">要处理的加载资源任务。</param>
        /// <returns>开始处理任务的状态。</returns>
        public StartTaskStatus Start(
          ResourceManager.ResourceLoader.LoadResourceTaskBase task)
        {
          this.m_Task = task != null ? task : throw new GameFrameworkException("Task is invalid.");
          this.m_Task.StartTime = DateTime.UtcNow;
          ResourceManager.ResourceInfo resourceInfo = this.m_Task.ResourceInfo;
          if (!resourceInfo.Ready)
          {
            this.m_Task.StartTime = new DateTime();
            return StartTaskStatus.HasToWait;
          }
          if (ResourceManager.ResourceLoader.LoadResourceAgent.IsAssetLoading(this.m_Task.AssetName))
          {
            this.m_Task.StartTime = new DateTime();
            return StartTaskStatus.HasToWait;
          }
          if (!this.m_Task.IsScene)
          {
            ResourceManager.ResourceLoader.AssetObject assetObject = this.m_ResourceLoader.m_AssetPool.Spawn(this.m_Task.AssetName);
            if (assetObject != null)
            {
              this.OnAssetObjectReady(assetObject);
              return StartTaskStatus.Done;
            }
          }
          foreach (string dependencyAssetName in this.m_Task.GetDependencyAssetNames())
          {
            if (!this.m_ResourceLoader.m_AssetPool.CanSpawn(dependencyAssetName))
            {
              this.m_Task.StartTime = new DateTime();
              return StartTaskStatus.HasToWait;
            }
          }
          string name = resourceInfo.ResourceName.Name;
          if (ResourceManager.ResourceLoader.LoadResourceAgent.IsResourceLoading(name))
          {
            this.m_Task.StartTime = new DateTime();
            return StartTaskStatus.HasToWait;
          }
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingAssetNames.Add(this.m_Task.AssetName);
          ResourceManager.ResourceLoader.ResourceObject resourceObject = this.m_ResourceLoader.m_ResourcePool.Spawn(name);
          if (resourceObject != null)
          {
            this.OnResourceObjectReady(resourceObject);
            return StartTaskStatus.CanResume;
          }
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Add(name);
          string fullPath = (string) null;
          if (!ResourceManager.ResourceLoader.LoadResourceAgent.s_CachedResourceNames.TryGetValue(name, out fullPath))
          {
            fullPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(resourceInfo.StorageInReadOnly ? this.m_ReadOnlyPath : this.m_ReadWritePath, resourceInfo.UseFileSystem ? resourceInfo.FileSystemName : resourceInfo.ResourceName.FullName));
            ResourceManager.ResourceLoader.LoadResourceAgent.s_CachedResourceNames.Add(name, fullPath);
          }
          if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromFile)
          {
            if (resourceInfo.UseFileSystem)
              this.m_Helper.ReadFile(this.m_ResourceLoader.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly), resourceInfo.ResourceName.FullName);
            else
              this.m_Helper.ReadFile(fullPath);
          }
          else
          {
            if (resourceInfo.LoadType != ResourceManager.LoadType.LoadFromMemory && resourceInfo.LoadType != ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt && resourceInfo.LoadType != ResourceManager.LoadType.LoadFromMemoryAndDecrypt)
              throw new GameFrameworkException(Utility.Text.Format<ResourceManager.LoadType>("Resource load type '{0}' is not supported.", resourceInfo.LoadType));
            if (resourceInfo.UseFileSystem)
              this.m_Helper.ReadBytes(this.m_ResourceLoader.m_ResourceManager.GetFileSystem(resourceInfo.FileSystemName, resourceInfo.StorageInReadOnly), resourceInfo.ResourceName.FullName);
            else
              this.m_Helper.ReadBytes(fullPath);
          }
          return StartTaskStatus.CanResume;
        }

        /// <summary>重置加载资源代理。</summary>
        public void Reset()
        {
          this.m_Helper.Reset();
          this.m_Task = (ResourceManager.ResourceLoader.LoadResourceTaskBase) null;
        }

        private static bool IsAssetLoading(string assetName) => ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingAssetNames.Contains(assetName);

        private static bool IsResourceLoading(string resourceName) => ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Contains(resourceName);

        private void OnAssetObjectReady(
          ResourceManager.ResourceLoader.AssetObject assetObject)
        {
          this.m_Helper.Reset();
          object target = assetObject.Target;
          if (this.m_Task.IsScene)
            this.m_ResourceLoader.m_SceneToAssetMap.Add(this.m_Task.AssetName, target);
          this.m_Task.OnLoadAssetSuccess(this, target, (float) (DateTime.UtcNow - this.m_Task.StartTime).TotalSeconds);
          this.m_Task.Done = true;
        }

        private void OnResourceObjectReady(
          ResourceManager.ResourceLoader.ResourceObject resourceObject)
        {
          this.m_Task.LoadMain(this, resourceObject);
        }

        private void OnError(LoadResourceStatus status, string errorMessage)
        {
          this.m_Helper.Reset();
          this.m_Task.OnLoadAssetFailure(this, status, errorMessage);
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingAssetNames.Remove(this.m_Task.AssetName);
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Remove(this.m_Task.ResourceInfo.ResourceName.Name);
          this.m_Task.Done = true;
        }

        private void OnLoadResourceAgentHelperUpdate(
          object sender,
          LoadResourceAgentHelperUpdateEventArgs e)
        {
          this.m_Task.OnLoadAssetUpdate(this, e.Type, e.Progress);
        }

        private void OnLoadResourceAgentHelperReadFileComplete(
          object sender,
          LoadResourceAgentHelperReadFileCompleteEventArgs e)
        {
          ResourceManager.ResourceLoader.ResourceObject resourceObject = ResourceManager.ResourceLoader.ResourceObject.Create(this.m_Task.ResourceInfo.ResourceName.Name, e.Resource, this.m_ResourceHelper, this.m_ResourceLoader);
          this.m_ResourceLoader.m_ResourcePool.Register(resourceObject, true);
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Remove(this.m_Task.ResourceInfo.ResourceName.Name);
          this.OnResourceObjectReady(resourceObject);
        }

        private void OnLoadResourceAgentHelperReadBytesComplete(
          object sender,
          LoadResourceAgentHelperReadBytesCompleteEventArgs e)
        {
          byte[] bytes1 = e.GetBytes();
          ResourceManager.ResourceInfo resourceInfo = this.m_Task.ResourceInfo;
          if (resourceInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || resourceInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt)
          {
            DecryptResourceCallback resourceCallback = this.m_DecryptResourceCallback;
            byte[] bytes2 = bytes1;
            int length1 = bytes1.Length;
            ResourceManager.ResourceName resourceName = resourceInfo.ResourceName;
            string name = resourceName.Name;
            resourceName = resourceInfo.ResourceName;
            string variant = resourceName.Variant;
            resourceName = resourceInfo.ResourceName;
            string extension = resourceName.Extension;
            int num = resourceInfo.StorageInReadOnly ? 1 : 0;
            string fileSystemName = resourceInfo.FileSystemName;
            int loadType = (int) resourceInfo.LoadType;
            int length2 = resourceInfo.Length;
            int hashCode = resourceInfo.HashCode;
            resourceCallback(bytes2, 0, length1, name, variant, extension, num != 0, fileSystemName, (byte) loadType, length2, hashCode);
          }
          this.m_Helper.ParseBytes(bytes1);
        }

        private void OnLoadResourceAgentHelperParseBytesComplete(
          object sender,
          LoadResourceAgentHelperParseBytesCompleteEventArgs e)
        {
          ResourceManager.ResourceLoader.ResourceObject resourceObject = ResourceManager.ResourceLoader.ResourceObject.Create(this.m_Task.ResourceInfo.ResourceName.Name, e.Resource, this.m_ResourceHelper, this.m_ResourceLoader);
          this.m_ResourceLoader.m_ResourcePool.Register(resourceObject, true);
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingResourceNames.Remove(this.m_Task.ResourceInfo.ResourceName.Name);
          this.OnResourceObjectReady(resourceObject);
        }

        private void OnLoadResourceAgentHelperLoadComplete(
          object sender,
          LoadResourceAgentHelperLoadCompleteEventArgs e)
        {
          ResourceManager.ResourceLoader.AssetObject assetObject = (ResourceManager.ResourceLoader.AssetObject) null;
          if (this.m_Task.IsScene)
            assetObject = this.m_ResourceLoader.m_AssetPool.Spawn(this.m_Task.AssetName);
          if (assetObject == null)
          {
            List<object> dependencyAssets = this.m_Task.GetDependencyAssets();
            assetObject = ResourceManager.ResourceLoader.AssetObject.Create(this.m_Task.AssetName, e.Asset, dependencyAssets, this.m_Task.ResourceObject.Target, this.m_ResourceHelper, this.m_ResourceLoader);
            this.m_ResourceLoader.m_AssetPool.Register(assetObject, true);
            this.m_ResourceLoader.m_AssetToResourceMap.Add(e.Asset, this.m_Task.ResourceObject.Target);
            foreach (object key in dependencyAssets)
            {
              object dependencyResource = (object) null;
              if (!this.m_ResourceLoader.m_AssetToResourceMap.TryGetValue(key, out dependencyResource))
                throw new GameFrameworkException("Can not find dependency resource.");
              this.m_Task.ResourceObject.AddDependencyResource(dependencyResource);
            }
          }
          ResourceManager.ResourceLoader.LoadResourceAgent.s_LoadingAssetNames.Remove(this.m_Task.AssetName);
          this.OnAssetObjectReady(assetObject);
        }

        private void OnLoadResourceAgentHelperError(
          object sender,
          LoadResourceAgentHelperErrorEventArgs e)
        {
          this.OnError(e.Status, e.ErrorMessage);
        }
      }

      private abstract class LoadResourceTaskBase : TaskBase
      {
        private static int s_Serial;
        private string m_AssetName;
        private Type m_AssetType;
        private ResourceManager.ResourceInfo m_ResourceInfo;
        private string[] m_DependencyAssetNames;
        private readonly List<object> m_DependencyAssets;
        private ResourceManager.ResourceLoader.ResourceObject m_ResourceObject;
        private DateTime m_StartTime;
        private int m_TotalDependencyAssetCount;

        public LoadResourceTaskBase()
        {
          this.m_AssetName = (string) null;
          this.m_AssetType = (Type) null;
          this.m_ResourceInfo = (ResourceManager.ResourceInfo) null;
          this.m_DependencyAssetNames = (string[]) null;
          this.m_DependencyAssets = new List<object>();
          this.m_ResourceObject = (ResourceManager.ResourceLoader.ResourceObject) null;
          this.m_StartTime = new DateTime();
          this.m_TotalDependencyAssetCount = 0;
        }

        public string AssetName => this.m_AssetName;

        public Type AssetType => this.m_AssetType;

        public ResourceManager.ResourceInfo ResourceInfo => this.m_ResourceInfo;

        public ResourceManager.ResourceLoader.ResourceObject ResourceObject => this.m_ResourceObject;

        public abstract bool IsScene { get; }

        public DateTime StartTime
        {
          get => this.m_StartTime;
          set => this.m_StartTime = value;
        }

        public int LoadedDependencyAssetCount => this.m_DependencyAssets.Count;

        public int TotalDependencyAssetCount
        {
          get => this.m_TotalDependencyAssetCount;
          set => this.m_TotalDependencyAssetCount = value;
        }

        public override string Description => this.m_AssetName;

        public override void Clear()
        {
          base.Clear();
          this.m_AssetName = (string) null;
          this.m_AssetType = (Type) null;
          this.m_ResourceInfo = (ResourceManager.ResourceInfo) null;
          this.m_DependencyAssetNames = (string[]) null;
          this.m_DependencyAssets.Clear();
          this.m_ResourceObject = (ResourceManager.ResourceLoader.ResourceObject) null;
          this.m_StartTime = new DateTime();
          this.m_TotalDependencyAssetCount = 0;
        }

        public string[] GetDependencyAssetNames() => this.m_DependencyAssetNames;

        public List<object> GetDependencyAssets() => this.m_DependencyAssets;

        public void LoadMain(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          ResourceManager.ResourceLoader.ResourceObject resourceObject)
        {
          this.m_ResourceObject = resourceObject;
          agent.Helper.LoadAsset(resourceObject.Target, this.AssetName, this.AssetType, this.IsScene);
        }

        public virtual void OnLoadAssetSuccess(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          object asset,
          float duration)
        {
        }

        public virtual void OnLoadAssetFailure(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceStatus status,
          string errorMessage)
        {
        }

        public virtual void OnLoadAssetUpdate(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceProgress type,
          float progress)
        {
        }

        public virtual void OnLoadDependencyAsset(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          string dependencyAssetName,
          object dependencyAsset)
        {
          this.m_DependencyAssets.Add(dependencyAsset);
        }

        protected void Initialize(
          string assetName,
          Type assetType,
          int priority,
          ResourceManager.ResourceInfo resourceInfo,
          string[] dependencyAssetNames,
          object userData)
        {
          this.Initialize(++ResourceManager.ResourceLoader.LoadResourceTaskBase.s_Serial, (string) null, priority, userData);
          this.m_AssetName = assetName;
          this.m_AssetType = assetType;
          this.m_ResourceInfo = resourceInfo;
          this.m_DependencyAssetNames = dependencyAssetNames;
        }
      }

      private sealed class LoadSceneTask : ResourceManager.ResourceLoader.LoadResourceTaskBase
      {
        private LoadSceneCallbacks m_LoadSceneCallbacks;

        public LoadSceneTask() => this.m_LoadSceneCallbacks = (LoadSceneCallbacks) null;

        public override bool IsScene => true;

        public static ResourceManager.ResourceLoader.LoadSceneTask Create(
          string sceneAssetName,
          int priority,
          ResourceManager.ResourceInfo resourceInfo,
          string[] dependencyAssetNames,
          LoadSceneCallbacks loadSceneCallbacks,
          object userData)
        {
          ResourceManager.ResourceLoader.LoadSceneTask loadSceneTask = ReferencePool.Acquire<ResourceManager.ResourceLoader.LoadSceneTask>();
          loadSceneTask.Initialize(sceneAssetName, (Type) null, priority, resourceInfo, dependencyAssetNames, userData);
          loadSceneTask.m_LoadSceneCallbacks = loadSceneCallbacks;
          return loadSceneTask;
        }

        public override void Clear()
        {
          base.Clear();
          this.m_LoadSceneCallbacks = (LoadSceneCallbacks) null;
        }

        public override void OnLoadAssetSuccess(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          object asset,
          float duration)
        {
          base.OnLoadAssetSuccess(agent, asset, duration);
          if (this.m_LoadSceneCallbacks.LoadSceneSuccessCallback == null)
            return;
          this.m_LoadSceneCallbacks.LoadSceneSuccessCallback(this.AssetName, duration, this.UserData);
        }

        public override void OnLoadAssetFailure(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceStatus status,
          string errorMessage)
        {
          base.OnLoadAssetFailure(agent, status, errorMessage);
          if (this.m_LoadSceneCallbacks.LoadSceneFailureCallback == null)
            return;
          this.m_LoadSceneCallbacks.LoadSceneFailureCallback(this.AssetName, status, errorMessage, this.UserData);
        }

        public override void OnLoadAssetUpdate(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          LoadResourceProgress type,
          float progress)
        {
          base.OnLoadAssetUpdate(agent, type, progress);
          if (type != LoadResourceProgress.LoadScene || this.m_LoadSceneCallbacks.LoadSceneUpdateCallback == null)
            return;
          this.m_LoadSceneCallbacks.LoadSceneUpdateCallback(this.AssetName, progress, this.UserData);
        }

        public override void OnLoadDependencyAsset(
          ResourceManager.ResourceLoader.LoadResourceAgent agent,
          string dependencyAssetName,
          object dependencyAsset)
        {
          base.OnLoadDependencyAsset(agent, dependencyAssetName, dependencyAsset);
          if (this.m_LoadSceneCallbacks.LoadSceneDependencyAssetCallback == null)
            return;
          this.m_LoadSceneCallbacks.LoadSceneDependencyAssetCallback(this.AssetName, dependencyAssetName, this.LoadedDependencyAssetCount, this.TotalDependencyAssetCount, this.UserData);
        }
      }

      /// <summary>资源对象。</summary>
      private sealed class ResourceObject : ObjectBase
      {
        private List<object> m_DependencyResources;
        private IResourceHelper m_ResourceHelper;
        private ResourceManager.ResourceLoader m_ResourceLoader;

        public ResourceObject()
        {
          this.m_DependencyResources = new List<object>();
          this.m_ResourceHelper = (IResourceHelper) null;
          this.m_ResourceLoader = (ResourceManager.ResourceLoader) null;
        }

        public override bool CustomCanReleaseFlag
        {
          get
          {
            int num = 0;
            this.m_ResourceLoader.m_ResourceDependencyCount.TryGetValue(this.Target, out num);
            return base.CustomCanReleaseFlag && num <= 0;
          }
        }

        public static ResourceManager.ResourceLoader.ResourceObject Create(
          string name,
          object target,
          IResourceHelper resourceHelper,
          ResourceManager.ResourceLoader resourceLoader)
        {
          if (resourceHelper == null)
            throw new GameFrameworkException("Resource helper is invalid.");
          if (resourceLoader == null)
            throw new GameFrameworkException("Resource loader is invalid.");
          ResourceManager.ResourceLoader.ResourceObject resourceObject = ReferencePool.Acquire<ResourceManager.ResourceLoader.ResourceObject>();
          resourceObject.Initialize(name, target);
          resourceObject.m_ResourceHelper = resourceHelper;
          resourceObject.m_ResourceLoader = resourceLoader;
          return resourceObject;
        }

        public override void Clear()
        {
          base.Clear();
          this.m_DependencyResources.Clear();
          this.m_ResourceHelper = (IResourceHelper) null;
          this.m_ResourceLoader = (ResourceManager.ResourceLoader) null;
        }

        public void AddDependencyResource(object dependencyResource)
        {
          if (this.Target == dependencyResource || this.m_DependencyResources.Contains(dependencyResource))
            return;
          this.m_DependencyResources.Add(dependencyResource);
          int num = 0;
          if (this.m_ResourceLoader.m_ResourceDependencyCount.TryGetValue(dependencyResource, out num))
            this.m_ResourceLoader.m_ResourceDependencyCount[dependencyResource] = num + 1;
          else
            this.m_ResourceLoader.m_ResourceDependencyCount.Add(dependencyResource, 1);
        }

        protected internal override void Release(bool isShutdown)
        {
          if (!isShutdown)
          {
            int num1 = 0;
            if (this.m_ResourceLoader.m_ResourceDependencyCount.TryGetValue(this.Target, out num1) && num1 > 0)
              throw new GameFrameworkException(Utility.Text.Format<string, int>("Resource target '{0}' reference count is '{1}' larger than 0.", this.Name, num1));
            foreach (object dependencyResource in this.m_DependencyResources)
            {
              int num2 = 0;
              if (!this.m_ResourceLoader.m_ResourceDependencyCount.TryGetValue(dependencyResource, out num2))
                throw new GameFrameworkException(Utility.Text.Format<string>("Resource target '{0}' dependency asset reference count is invalid.", this.Name));
              this.m_ResourceLoader.m_ResourceDependencyCount[dependencyResource] = num2 - 1;
            }
          }
          this.m_ResourceLoader.m_ResourceDependencyCount.Remove(this.Target);
          this.m_ResourceHelper.Release(this.Target);
        }
      }
    }

    /// <summary>资源名称。</summary>
    [StructLayout(LayoutKind.Auto)]
    private struct ResourceName : 
      IComparable,
      IComparable<ResourceManager.ResourceName>,
      IEquatable<ResourceManager.ResourceName>
    {
      private readonly string m_Name;
      private readonly string m_Variant;
      private readonly string m_Extension;
      private string m_CachedFullName;

      /// <summary>初始化资源名称的新实例。</summary>
      /// <param name="name">资源名称。</param>
      /// <param name="variant">变体名称。</param>
      /// <param name="extension">扩展名称。</param>
      public ResourceName(string name, string variant, string extension)
      {
        if (string.IsNullOrEmpty(name))
          throw new GameFrameworkException("Resource name is invalid.");
        if (string.IsNullOrEmpty(extension))
          throw new GameFrameworkException("Resource extension is invalid.");
        this.m_Name = name;
        this.m_Variant = variant;
        this.m_Extension = extension;
        this.m_CachedFullName = (string) null;
      }

      /// <summary>获取资源名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取变体名称。</summary>
      public string Variant => this.m_Variant;

      /// <summary>获取扩展名称。</summary>
      public string Extension => this.m_Extension;

      public string FullName
      {
        get
        {
          if (this.m_CachedFullName == null)
            this.m_CachedFullName = this.m_Variant != null ? Utility.Text.Format<string, string, string>("{0}.{1}.{2}", this.m_Name, this.m_Variant, this.m_Extension) : Utility.Text.Format<string, string>("{0}.{1}", this.m_Name, this.m_Extension);
          return this.m_CachedFullName;
        }
      }

      public override string ToString() => this.FullName;

      public override int GetHashCode() => this.m_Variant == null ? this.m_Name.GetHashCode() ^ this.m_Extension.GetHashCode() : this.m_Name.GetHashCode() ^ this.m_Variant.GetHashCode() ^ this.m_Extension.GetHashCode();

      public override bool Equals(object obj) => obj is ResourceManager.ResourceName resourceName && this.Equals(resourceName);

      public bool Equals(ResourceManager.ResourceName value) => string.Equals(this.m_Name, value.m_Name, StringComparison.Ordinal) && string.Equals(this.m_Variant, value.m_Variant, StringComparison.Ordinal) && string.Equals(this.m_Extension, value.m_Extension, StringComparison.Ordinal);

      public static bool operator ==(ResourceManager.ResourceName a, ResourceManager.ResourceName b) => a.Equals(b);

      public static bool operator !=(ResourceManager.ResourceName a, ResourceManager.ResourceName b) => !(a == b);

      public int CompareTo(object value)
      {
        if (value == null)
          return 1;
        if (!(value is ResourceManager.ResourceName resourceName))
          throw new GameFrameworkException("Type of value is invalid.");
        return this.CompareTo(resourceName);
      }

      public int CompareTo(ResourceManager.ResourceName resourceName)
      {
        int num1 = string.CompareOrdinal(this.m_Name, resourceName.m_Name);
        if (num1 != 0)
          return num1;
        int num2 = string.CompareOrdinal(this.m_Variant, resourceName.m_Variant);
        return num2 != 0 ? num2 : string.CompareOrdinal(this.m_Extension, resourceName.m_Extension);
      }
    }

    /// <summary>资源名称比较器。</summary>
    private sealed class ResourceNameComparer : 
      IComparer<ResourceManager.ResourceName>,
      IEqualityComparer<ResourceManager.ResourceName>
    {
      public int Compare(ResourceManager.ResourceName x, ResourceManager.ResourceName y) => x.CompareTo(y);

      public bool Equals(ResourceManager.ResourceName x, ResourceManager.ResourceName y) => x.Equals(y);

      public int GetHashCode(ResourceManager.ResourceName obj) => obj.GetHashCode();
    }

    /// <summary>资源更新器。</summary>
    private sealed class ResourceUpdater
    {
      private const int CachedHashBytesLength = 4;
      private const int CachedBytesLength = 4096;
      private readonly ResourceManager m_ResourceManager;
      private readonly Queue<ResourceManager.ResourceUpdater.ApplyInfo> m_ApplyWaitingInfo;
      private readonly List<ResourceManager.ResourceUpdater.UpdateInfo> m_UpdateWaitingInfo;
      private readonly HashSet<ResourceManager.ResourceUpdater.UpdateInfo> m_UpdateWaitingInfoWhilePlaying;
      private readonly Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceUpdater.UpdateInfo> m_UpdateCandidateInfo;
      private readonly SortedDictionary<string, List<int>> m_CachedFileSystemsForGenerateReadWriteVersionList;
      private readonly List<ResourceManager.ResourceName> m_CachedResourceNames;
      private readonly byte[] m_CachedHashBytes;
      private readonly byte[] m_CachedBytes;
      private IDownloadManager m_DownloadManager;
      private bool m_CheckResourcesComplete;
      private string m_ApplyingResourcePackPath;
      private FileStream m_ApplyingResourcePackStream;
      private ResourceManager.ResourceGroup m_UpdatingResourceGroup;
      private int m_GenerateReadWriteVersionListLength;
      private int m_CurrentGenerateReadWriteVersionListLength;
      private int m_UpdateRetryCount;
      private bool m_FailureFlag;
      private string m_ReadWriteVersionListFileName;
      private string m_ReadWriteVersionListTempFileName;
      public GameFrameworkAction<string, int, long> ResourceApplyStart;
      public GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int> ResourceApplySuccess;
      public GameFrameworkAction<ResourceManager.ResourceName, string, string> ResourceApplyFailure;
      public GameFrameworkAction<string, bool> ResourceApplyComplete;
      public GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int> ResourceUpdateStart;
      public GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int> ResourceUpdateChanged;
      public GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int> ResourceUpdateSuccess;
      public GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string> ResourceUpdateFailure;
      public GameFrameworkAction<ResourceManager.ResourceGroup, bool> ResourceUpdateComplete;
      public GameFrameworkAction ResourceUpdateAllComplete;

      /// <summary>初始化资源更新器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public ResourceUpdater(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_ApplyWaitingInfo = new Queue<ResourceManager.ResourceUpdater.ApplyInfo>();
        this.m_UpdateWaitingInfo = new List<ResourceManager.ResourceUpdater.UpdateInfo>();
        this.m_UpdateWaitingInfoWhilePlaying = new HashSet<ResourceManager.ResourceUpdater.UpdateInfo>();
        this.m_UpdateCandidateInfo = new Dictionary<ResourceManager.ResourceName, ResourceManager.ResourceUpdater.UpdateInfo>();
        this.m_CachedFileSystemsForGenerateReadWriteVersionList = new SortedDictionary<string, List<int>>((IComparer<string>) StringComparer.Ordinal);
        this.m_CachedResourceNames = new List<ResourceManager.ResourceName>();
        this.m_CachedHashBytes = new byte[4];
        this.m_CachedBytes = new byte[4096];
        this.m_DownloadManager = (IDownloadManager) null;
        this.m_CheckResourcesComplete = false;
        this.m_ApplyingResourcePackPath = (string) null;
        this.m_ApplyingResourcePackStream = (FileStream) null;
        this.m_UpdatingResourceGroup = (ResourceManager.ResourceGroup) null;
        this.m_GenerateReadWriteVersionListLength = 0;
        this.m_CurrentGenerateReadWriteVersionListLength = 0;
        this.m_UpdateRetryCount = 3;
        this.m_FailureFlag = false;
        this.m_ReadWriteVersionListFileName = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkList.dat"));
        this.m_ReadWriteVersionListTempFileName = Utility.Text.Format<string, string>("{0}.{1}", this.m_ReadWriteVersionListFileName, "tmp");
        this.ResourceApplyStart = (GameFrameworkAction<string, int, long>) null;
        this.ResourceApplySuccess = (GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>) null;
        this.ResourceApplyFailure = (GameFrameworkAction<ResourceManager.ResourceName, string, string>) null;
        this.ResourceApplyComplete = (GameFrameworkAction<string, bool>) null;
        this.ResourceUpdateStart = (GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int, int>) null;
        this.ResourceUpdateChanged = (GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>) null;
        this.ResourceUpdateSuccess = (GameFrameworkAction<ResourceManager.ResourceName, string, string, int, int>) null;
        this.ResourceUpdateFailure = (GameFrameworkAction<ResourceManager.ResourceName, string, int, int, string>) null;
        this.ResourceUpdateComplete = (GameFrameworkAction<ResourceManager.ResourceGroup, bool>) null;
        this.ResourceUpdateAllComplete = (GameFrameworkAction) null;
      }

      /// <summary>获取或设置每更新多少字节的资源，重新生成一次版本资源列表。</summary>
      public int GenerateReadWriteVersionListLength
      {
        get => this.m_GenerateReadWriteVersionListLength;
        set => this.m_GenerateReadWriteVersionListLength = value;
      }

      /// <summary>获取正在应用的资源包路径。</summary>
      public string ApplyingResourcePackPath => this.m_ApplyingResourcePackPath;

      /// <summary>获取等待应用资源数量。</summary>
      public int ApplyWaitingCount => this.m_ApplyWaitingInfo.Count;

      /// <summary>获取或设置资源更新重试次数。</summary>
      public int UpdateRetryCount
      {
        get => this.m_UpdateRetryCount;
        set => this.m_UpdateRetryCount = value;
      }

      /// <summary>获取正在更新的资源组。</summary>
      public IResourceGroup UpdatingResourceGroup => (IResourceGroup) this.m_UpdatingResourceGroup;

      /// <summary>获取等待更新资源数量。</summary>
      public int UpdateWaitingCount => this.m_UpdateWaitingInfo.Count;

      /// <summary>获取使用时下载的等待更新资源数量。</summary>
      public int UpdateWaitingWhilePlayingCount => this.m_UpdateWaitingInfoWhilePlaying.Count;

      /// <summary>获取候选更新资源数量。</summary>
      public int UpdateCandidateCount => this.m_UpdateCandidateInfo.Count;

      /// <summary>资源更新器轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (this.m_ApplyingResourcePackStream != null)
        {
          while (this.m_ApplyWaitingInfo.Count > 0)
          {
            if (this.ApplyResource(this.m_ApplyWaitingInfo.Dequeue()))
              return;
          }
          Array.Clear((Array) this.m_CachedBytes, 0, 4096);
          string resourcePackPath = this.m_ApplyingResourcePackPath;
          this.m_ApplyingResourcePackPath = (string) null;
          this.m_ApplyingResourcePackStream.Dispose();
          this.m_ApplyingResourcePackStream = (FileStream) null;
          if (this.ResourceApplyComplete != null)
            this.ResourceApplyComplete(resourcePackPath, !this.m_FailureFlag);
          if (this.m_UpdateCandidateInfo.Count > 0 || this.ResourceUpdateAllComplete == null)
            return;
          this.ResourceUpdateAllComplete();
        }
        else
        {
          if (this.m_UpdateWaitingInfo.Count <= 0)
            return;
          int num = this.m_DownloadManager.FreeAgentCount - this.m_DownloadManager.WaitingTaskCount;
          if (num <= 0)
            return;
          int index1 = 0;
          for (int index2 = 0; index1 < this.m_UpdateWaitingInfo.Count && index2 < num; ++index1)
          {
            if (this.DownloadResource(this.m_UpdateWaitingInfo[index1]))
              ++index2;
          }
        }
      }

      /// <summary>关闭并清理资源更新器。</summary>
      public void Shutdown()
      {
        if (this.m_DownloadManager != null)
        {
          this.m_DownloadManager.DownloadStart -= new EventHandler<DownloadStartEventArgs>(this.OnDownloadStart);
          this.m_DownloadManager.DownloadUpdate -= new EventHandler<DownloadUpdateEventArgs>(this.OnDownloadUpdate);
          this.m_DownloadManager.DownloadSuccess -= new EventHandler<DownloadSuccessEventArgs>(this.OnDownloadSuccess);
          this.m_DownloadManager.DownloadFailure -= new EventHandler<DownloadFailureEventArgs>(this.OnDownloadFailure);
        }
        this.m_UpdateWaitingInfo.Clear();
        this.m_UpdateCandidateInfo.Clear();
        this.m_CachedFileSystemsForGenerateReadWriteVersionList.Clear();
      }

      /// <summary>设置下载管理器。</summary>
      /// <param name="downloadManager">下载管理器。</param>
      public void SetDownloadManager(IDownloadManager downloadManager)
      {
        this.m_DownloadManager = downloadManager != null ? downloadManager : throw new GameFrameworkException("Download manager is invalid.");
        this.m_DownloadManager.DownloadStart += new EventHandler<DownloadStartEventArgs>(this.OnDownloadStart);
        this.m_DownloadManager.DownloadUpdate += new EventHandler<DownloadUpdateEventArgs>(this.OnDownloadUpdate);
        this.m_DownloadManager.DownloadSuccess += new EventHandler<DownloadSuccessEventArgs>(this.OnDownloadSuccess);
        this.m_DownloadManager.DownloadFailure += new EventHandler<DownloadFailureEventArgs>(this.OnDownloadFailure);
      }

      /// <summary>增加资源更新。</summary>
      /// <param name="resourceName">资源名称。</param>
      /// <param name="fileSystemName">资源所在的文件系统名称。</param>
      /// <param name="loadType">资源加载方式。</param>
      /// <param name="length">资源大小。</param>
      /// <param name="hashCode">资源哈希值。</param>
      /// <param name="compressedLength">压缩后大小。</param>
      /// <param name="compressedHashCode">压缩后哈希值。</param>
      /// <param name="resourcePath">资源路径。</param>
      public void AddResourceUpdate(
        ResourceManager.ResourceName resourceName,
        string fileSystemName,
        ResourceManager.LoadType loadType,
        int length,
        int hashCode,
        int compressedLength,
        int compressedHashCode,
        string resourcePath)
      {
        this.m_UpdateCandidateInfo.Add(resourceName, new ResourceManager.ResourceUpdater.UpdateInfo(resourceName, fileSystemName, loadType, length, hashCode, compressedLength, compressedHashCode, resourcePath));
      }

      /// <summary>检查资源完成。</summary>
      /// <param name="needGenerateReadWriteVersionList">是否需要生成读写区版本资源列表。</param>
      public void CheckResourceComplete(bool needGenerateReadWriteVersionList)
      {
        this.m_CheckResourcesComplete = true;
        if (!needGenerateReadWriteVersionList)
          return;
        this.GenerateReadWriteVersionList();
      }

      /// <summary>应用指定资源包的资源。</summary>
      /// <param name="resourcePackPath">要应用的资源包路径。</param>
      public void ApplyResources(string resourcePackPath)
      {
        if (!this.m_CheckResourcesComplete)
          throw new GameFrameworkException("You must check resources complete first.");
        if (this.m_ApplyingResourcePackStream != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource pack '{0}' being applied.", this.m_ApplyingResourcePackPath));
        if (this.m_UpdatingResourceGroup != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource group '{0}' being updated.", this.m_UpdatingResourceGroup.Name));
        if (this.m_UpdateWaitingInfoWhilePlaying.Count > 0)
          throw new GameFrameworkException("There are already some resources being updated while playing.");
        try
        {
          long num1 = 0;
          ResourcePackVersionList resourcePackVersionList = new ResourcePackVersionList();
          using (FileStream fileStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read))
          {
            num1 = fileStream.Length;
            resourcePackVersionList = this.m_ResourceManager.m_ResourcePackVersionListSerializer.Deserialize((Stream) fileStream);
          }
          if (!resourcePackVersionList.IsValid)
            throw new GameFrameworkException("Deserialize resource pack version list failure.");
          if ((long) resourcePackVersionList.Offset + resourcePackVersionList.Length != num1)
            throw new GameFrameworkException("Resource pack length is invalid.");
          this.m_ApplyingResourcePackPath = resourcePackPath;
          this.m_ApplyingResourcePackStream = new FileStream(resourcePackPath, FileMode.Open, FileAccess.Read);
          this.m_ApplyingResourcePackStream.Position = (long) resourcePackVersionList.Offset;
          this.m_FailureFlag = false;
          long num2 = 0;
          foreach (ResourcePackVersionList.Resource resource in resourcePackVersionList.GetResources())
          {
            ResourceManager.ResourceName resourceName = new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension);
            ResourceManager.ResourceUpdater.UpdateInfo updateInfo = (ResourceManager.ResourceUpdater.UpdateInfo) null;
            if (this.m_UpdateCandidateInfo.TryGetValue(resourceName, out updateInfo) && updateInfo.LoadType == (ResourceManager.LoadType) resource.LoadType && updateInfo.Length == resource.Length && updateInfo.HashCode == resource.HashCode)
            {
              num2 += (long) resource.Length;
              this.m_ApplyWaitingInfo.Enqueue(new ResourceManager.ResourceUpdater.ApplyInfo(resourceName, updateInfo.FileSystemName, (ResourceManager.LoadType) resource.LoadType, resource.Offset, resource.Length, resource.HashCode, resource.CompressedLength, resource.CompressedHashCode, updateInfo.ResourcePath));
            }
          }
          if (this.ResourceApplyStart == null)
            return;
          this.ResourceApplyStart(this.m_ApplyingResourcePackPath, this.m_ApplyWaitingInfo.Count, num2);
        }
        catch (Exception ex)
        {
          if (this.m_ApplyingResourcePackStream != null)
          {
            this.m_ApplyingResourcePackStream.Dispose();
            this.m_ApplyingResourcePackStream = (FileStream) null;
          }
          throw new GameFrameworkException(Utility.Text.Format<string, Exception>("Apply resources '{0}' with exception '{1}'.", resourcePackPath, ex), ex);
        }
      }

      /// <summary>更新指定资源组的资源。</summary>
      /// <param name="resourceGroup">要更新的资源组。</param>
      public void UpdateResources(ResourceManager.ResourceGroup resourceGroup)
      {
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        if (!this.m_CheckResourcesComplete)
          throw new GameFrameworkException("You must check resources complete first.");
        if (this.m_ApplyingResourcePackStream != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource pack '{0}' being applied.", this.m_ApplyingResourcePackPath));
        if (this.m_UpdatingResourceGroup != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource group '{0}' being updated.", this.m_UpdatingResourceGroup.Name));
        if (string.IsNullOrEmpty(resourceGroup.Name))
        {
          foreach (KeyValuePair<ResourceManager.ResourceName, ResourceManager.ResourceUpdater.UpdateInfo> keyValuePair in this.m_UpdateCandidateInfo)
            this.m_UpdateWaitingInfo.Add(keyValuePair.Value);
        }
        else
        {
          resourceGroup.InternalGetResourceNames(this.m_CachedResourceNames);
          foreach (ResourceManager.ResourceName cachedResourceName in this.m_CachedResourceNames)
          {
            ResourceManager.ResourceUpdater.UpdateInfo updateInfo = (ResourceManager.ResourceUpdater.UpdateInfo) null;
            if (this.m_UpdateCandidateInfo.TryGetValue(cachedResourceName, out updateInfo))
              this.m_UpdateWaitingInfo.Add(updateInfo);
          }
          this.m_CachedResourceNames.Clear();
        }
        this.m_UpdatingResourceGroup = resourceGroup;
        this.m_FailureFlag = false;
      }

      /// <summary>停止更新资源。</summary>
      public void StopUpdateResources()
      {
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        if (!this.m_CheckResourcesComplete)
          throw new GameFrameworkException("You must check resources complete first.");
        if (this.m_ApplyingResourcePackStream != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource pack '{0}' being applied.", this.m_ApplyingResourcePackPath));
        if (this.m_UpdatingResourceGroup == null)
          throw new GameFrameworkException("There is no resource group being updated.");
        this.m_UpdateWaitingInfo.Clear();
        this.m_UpdatingResourceGroup = (ResourceManager.ResourceGroup) null;
      }

      /// <summary>更新指定资源。</summary>
      /// <param name="resourceName">要更新的资源名称。</param>
      public void UpdateResource(ResourceManager.ResourceName resourceName)
      {
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        if (!this.m_CheckResourcesComplete)
          throw new GameFrameworkException("You must check resources complete first.");
        if (this.m_ApplyingResourcePackStream != null)
          throw new GameFrameworkException(Utility.Text.Format<string>("There is already a resource pack '{0}' being applied.", this.m_ApplyingResourcePackPath));
        ResourceManager.ResourceUpdater.UpdateInfo updateInfo = (ResourceManager.ResourceUpdater.UpdateInfo) null;
        if (!this.m_UpdateCandidateInfo.TryGetValue(resourceName, out updateInfo) || !this.m_UpdateWaitingInfoWhilePlaying.Add(updateInfo))
          return;
        this.DownloadResource(updateInfo);
      }

      private bool ApplyResource(
        ResourceManager.ResourceUpdater.ApplyInfo applyInfo)
      {
        long position = this.m_ApplyingResourcePackStream.Position;
        try
        {
          bool flag = applyInfo.Length != applyInfo.CompressedLength || applyInfo.HashCode != applyInfo.CompressedHashCode;
          int compressedLength = applyInfo.CompressedLength;
          string directoryName = System.IO.Path.GetDirectoryName(applyInfo.ResourcePath);
          if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);
          this.m_ApplyingResourcePackStream.Position += applyInfo.Offset;
          using (FileStream fileStream = new FileStream(applyInfo.ResourcePath, FileMode.Create, FileAccess.ReadWrite))
          {
            while (true)
            {
              FileStream resourcePackStream = this.m_ApplyingResourcePackStream;
              byte[] cachedBytes = this.m_CachedBytes;
              int count1 = compressedLength < 4096 ? compressedLength : 4096;
              int count2;
              if ((count2 = resourcePackStream.Read(cachedBytes, 0, count1)) > 0)
              {
                compressedLength -= count2;
                fileStream.Write(this.m_CachedBytes, 0, count2);
              }
              else
                break;
            }
            if (flag)
            {
              fileStream.Position = 0L;
              int crc32 = Utility.Verifier.GetCrc32((Stream) fileStream);
              if (crc32 != applyInfo.CompressedHashCode)
              {
                if (this.ResourceApplyFailure != null)
                {
                  string str = Utility.Text.Format<int, int>("Resource compressed hash code error, need '{0}', applied '{1}'.", applyInfo.CompressedHashCode, crc32);
                  this.ResourceApplyFailure(applyInfo.ResourceName, this.m_ApplyingResourcePackPath, str);
                }
                this.m_FailureFlag = true;
                return false;
              }
              if (this.m_ResourceManager.m_DecompressCachedStream == null)
                this.m_ResourceManager.m_DecompressCachedStream = new MemoryStream();
              fileStream.Position = 0L;
              this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
              this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
              if (!Utility.Compression.Decompress((Stream) fileStream, (Stream) this.m_ResourceManager.m_DecompressCachedStream))
              {
                if (this.ResourceApplyFailure != null)
                {
                  string str = Utility.Text.Format<string>("Unable to decompress resource '{0}'.", applyInfo.ResourcePath);
                  this.ResourceApplyFailure(applyInfo.ResourceName, this.m_ApplyingResourcePackPath, str);
                }
                this.m_FailureFlag = true;
                return false;
              }
              fileStream.Position = 0L;
              fileStream.SetLength(0L);
              fileStream.Write(this.m_ResourceManager.m_DecompressCachedStream.GetBuffer(), 0, (int) this.m_ResourceManager.m_DecompressCachedStream.Length);
            }
            else
            {
              int num = 0;
              fileStream.Position = 0L;
              if (applyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || applyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || applyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || applyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
              {
                Utility.Converter.GetBytes(applyInfo.HashCode, this.m_CachedHashBytes);
                if (applyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || applyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt)
                  num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, 220);
                else if (applyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || applyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
                  num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, applyInfo.Length);
                Array.Clear((Array) this.m_CachedHashBytes, 0, 4);
              }
              else
                num = Utility.Verifier.GetCrc32((Stream) fileStream);
              if (num != applyInfo.HashCode)
              {
                if (this.ResourceApplyFailure != null)
                {
                  string str = Utility.Text.Format<int, int>("Resource hash code error, need '{0}', applied '{1}'.", applyInfo.HashCode, num);
                  this.ResourceApplyFailure(applyInfo.ResourceName, this.m_ApplyingResourcePackPath, str);
                }
                this.m_FailureFlag = true;
                return false;
              }
            }
          }
          if (applyInfo.UseFileSystem)
          {
            int num = this.m_ResourceManager.GetFileSystem(applyInfo.FileSystemName, false).WriteFile(applyInfo.ResourceName.FullName, applyInfo.ResourcePath) ? 1 : 0;
            if (File.Exists(applyInfo.ResourcePath))
              File.Delete(applyInfo.ResourcePath);
            if (num == 0)
            {
              if (this.ResourceApplyFailure != null)
              {
                string str = Utility.Text.Format<string, string>("Unable to write resource '{0}' to file system '{1}'.", applyInfo.ResourcePath, applyInfo.FileSystemName);
                this.ResourceApplyFailure(applyInfo.ResourceName, this.m_ApplyingResourcePackPath, str);
              }
              this.m_FailureFlag = true;
              return false;
            }
          }
          string path = Utility.Text.Format<string>("{0}.download", applyInfo.ResourcePath);
          if (File.Exists(path))
            File.Delete(path);
          this.m_UpdateCandidateInfo.Remove(applyInfo.ResourceName);
          this.m_ResourceManager.m_ResourceInfos[applyInfo.ResourceName].MarkReady();
          this.m_ResourceManager.m_ReadWriteResourceInfos.Add(applyInfo.ResourceName, new ResourceManager.ReadWriteResourceInfo(applyInfo.FileSystemName, applyInfo.LoadType, applyInfo.Length, applyInfo.HashCode));
          if (this.ResourceApplySuccess != null)
            this.ResourceApplySuccess(applyInfo.ResourceName, applyInfo.ResourcePath, this.m_ApplyingResourcePackPath, applyInfo.Length, applyInfo.CompressedLength);
          this.m_CurrentGenerateReadWriteVersionListLength += applyInfo.CompressedLength;
          if (this.m_ApplyWaitingInfo.Count > 0 && this.m_CurrentGenerateReadWriteVersionListLength < this.m_GenerateReadWriteVersionListLength)
            return false;
          this.GenerateReadWriteVersionList();
          return true;
        }
        catch (Exception ex)
        {
          if (this.ResourceApplyFailure != null)
            this.ResourceApplyFailure(applyInfo.ResourceName, this.m_ApplyingResourcePackPath, ex.ToString());
          this.m_FailureFlag = true;
          return false;
        }
        finally
        {
          this.m_ApplyingResourcePackStream.Position = position;
          if (this.m_ResourceManager.m_DecompressCachedStream != null)
          {
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
          }
        }
      }

      private bool DownloadResource(
        ResourceManager.ResourceUpdater.UpdateInfo updateInfo)
      {
        if (updateInfo.Downloading)
          return false;
        updateInfo.Downloading = true;
        string str;
        if (updateInfo.ResourceName.Variant == null)
        {
          str = Utility.Text.Format<string, int, string>("{0}.{1:x8}.{2}", updateInfo.ResourceName.Name, updateInfo.HashCode, "dat");
        }
        else
        {
          ResourceManager.ResourceName resourceName = updateInfo.ResourceName;
          string name = resourceName.Name;
          resourceName = updateInfo.ResourceName;
          string variant = resourceName.Variant;
          int hashCode = updateInfo.HashCode;
          str = Utility.Text.Format<string, string, int, string>("{0}.{1}.{2:x8}.{3}", name, variant, hashCode, "dat");
        }
        string path2 = str;
        this.m_DownloadManager.AddDownload(updateInfo.ResourcePath, Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_UpdatePrefixUri, path2)), (object) updateInfo);
        return true;
      }

      private void GenerateReadWriteVersionList()
      {
        FileStream fileStream1 = (FileStream) null;
        FileStream fileStream2;
        try
        {
          fileStream1 = new FileStream(this.m_ReadWriteVersionListTempFileName, FileMode.Create, FileAccess.Write);
          LocalVersionList.Resource[] resources = this.m_ResourceManager.m_ReadWriteResourceInfos.Count > 0 ? new LocalVersionList.Resource[this.m_ResourceManager.m_ReadWriteResourceInfos.Count] : (LocalVersionList.Resource[]) null;
          if (resources != null)
          {
            int num = 0;
            foreach (KeyValuePair<ResourceManager.ResourceName, ResourceManager.ReadWriteResourceInfo> writeResourceInfo1 in this.m_ResourceManager.m_ReadWriteResourceInfos)
            {
              LocalVersionList.Resource[] resourceArray = resources;
              int index = num;
              ResourceManager.ResourceName key = writeResourceInfo1.Key;
              string name = key.Name;
              key = writeResourceInfo1.Key;
              string variant = key.Variant;
              key = writeResourceInfo1.Key;
              string extension = key.Extension;
              ResourceManager.ReadWriteResourceInfo writeResourceInfo2 = writeResourceInfo1.Value;
              int loadType = (int) writeResourceInfo2.LoadType;
              writeResourceInfo2 = writeResourceInfo1.Value;
              int length = writeResourceInfo2.Length;
              writeResourceInfo2 = writeResourceInfo1.Value;
              int hashCode = writeResourceInfo2.HashCode;
              LocalVersionList.Resource resource = new LocalVersionList.Resource(name, variant, extension, (byte) loadType, length, hashCode);
              resourceArray[index] = resource;
              writeResourceInfo2 = writeResourceInfo1.Value;
              if (writeResourceInfo2.UseFileSystem)
              {
                List<int> intList1 = (List<int>) null;
                SortedDictionary<string, List<int>> writeVersionList1 = this.m_CachedFileSystemsForGenerateReadWriteVersionList;
                writeResourceInfo2 = writeResourceInfo1.Value;
                string fileSystemName1 = writeResourceInfo2.FileSystemName;
                ref List<int> local = ref intList1;
                if (!writeVersionList1.TryGetValue(fileSystemName1, out local))
                {
                  intList1 = new List<int>();
                  SortedDictionary<string, List<int>> writeVersionList2 = this.m_CachedFileSystemsForGenerateReadWriteVersionList;
                  writeResourceInfo2 = writeResourceInfo1.Value;
                  string fileSystemName2 = writeResourceInfo2.FileSystemName;
                  List<int> intList2 = intList1;
                  writeVersionList2.Add(fileSystemName2, intList2);
                }
                intList1.Add(num);
              }
              ++num;
            }
          }
          LocalVersionList.FileSystem[] fileSystems = this.m_CachedFileSystemsForGenerateReadWriteVersionList.Count > 0 ? new LocalVersionList.FileSystem[this.m_CachedFileSystemsForGenerateReadWriteVersionList.Count] : (LocalVersionList.FileSystem[]) null;
          if (fileSystems != null)
          {
            int num = 0;
            foreach (KeyValuePair<string, List<int>> readWriteVersion in this.m_CachedFileSystemsForGenerateReadWriteVersionList)
            {
              fileSystems[num++] = new LocalVersionList.FileSystem(readWriteVersion.Key, readWriteVersion.Value.ToArray());
              readWriteVersion.Value.Clear();
            }
          }
          LocalVersionList data = new LocalVersionList(resources, fileSystems);
          if (!this.m_ResourceManager.m_ReadWriteVersionListSerializer.Serialize((Stream) fileStream1, data))
            throw new GameFrameworkException("Serialize read-write version list failure.");
          if (fileStream1 != null)
          {
            fileStream1.Dispose();
            fileStream2 = (FileStream) null;
          }
        }
        catch (Exception ex)
        {
          if (fileStream1 != null)
          {
            fileStream1.Dispose();
            fileStream2 = (FileStream) null;
          }
          if (File.Exists(this.m_ReadWriteVersionListTempFileName))
            File.Delete(this.m_ReadWriteVersionListTempFileName);
          throw new GameFrameworkException(Utility.Text.Format<Exception>("Generate read-write version list exception '{0}'.", ex), ex);
        }
        if (File.Exists(this.m_ReadWriteVersionListFileName))
          File.Delete(this.m_ReadWriteVersionListFileName);
        File.Move(this.m_ReadWriteVersionListTempFileName, this.m_ReadWriteVersionListFileName);
        this.m_CurrentGenerateReadWriteVersionListLength = 0;
      }

      private void OnDownloadStart(object sender, DownloadStartEventArgs e)
      {
        if (!(e.UserData is ResourceManager.ResourceUpdater.UpdateInfo userData))
          return;
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        if (e.CurrentLength > (long) int.MaxValue)
          throw new GameFrameworkException(Utility.Text.Format<string>("File '{0}' is too large.", e.DownloadPath));
        if (this.ResourceUpdateStart == null)
          return;
        this.ResourceUpdateStart(userData.ResourceName, e.DownloadPath, e.DownloadUri, (int) e.CurrentLength, userData.CompressedLength, userData.RetryCount);
      }

      private void OnDownloadUpdate(object sender, DownloadUpdateEventArgs e)
      {
        if (!(e.UserData is ResourceManager.ResourceUpdater.UpdateInfo userData))
          return;
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        if (e.CurrentLength > (long) userData.CompressedLength)
        {
          this.m_DownloadManager.RemoveDownload(e.SerialId);
          string path = Utility.Text.Format<string>("{0}.download", e.DownloadPath);
          if (File.Exists(path))
            File.Delete(path);
          string errorMessage = Utility.Text.Format<int, long>("When download update, downloaded length is larger than compressed length, need '{0}', downloaded '{1}'.", userData.CompressedLength, e.CurrentLength);
          DownloadFailureEventArgs e1 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
          this.OnDownloadFailure((object) this, e1);
          ReferencePool.Release((IReference) e1);
        }
        else
        {
          if (this.ResourceUpdateChanged == null)
            return;
          this.ResourceUpdateChanged(userData.ResourceName, e.DownloadPath, e.DownloadUri, (int) e.CurrentLength, userData.CompressedLength);
        }
      }

      private void OnDownloadSuccess(object sender, DownloadSuccessEventArgs e)
      {
        if (!(e.UserData is ResourceManager.ResourceUpdater.UpdateInfo userData))
          return;
        try
        {
          using (FileStream fileStream = new FileStream(e.DownloadPath, FileMode.Open, FileAccess.ReadWrite))
          {
            bool flag = userData.Length != userData.CompressedLength || userData.HashCode != userData.CompressedHashCode;
            int length = (int) fileStream.Length;
            if (length != userData.CompressedLength)
            {
              fileStream.Close();
              string errorMessage = Utility.Text.Format<int, int>("Resource compressed length error, need '{0}', downloaded '{1}'.", userData.CompressedLength, length);
              DownloadFailureEventArgs e1 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e1);
              ReferencePool.Release((IReference) e1);
              return;
            }
            if (flag)
            {
              fileStream.Position = 0L;
              int crc32 = Utility.Verifier.GetCrc32((Stream) fileStream);
              if (crc32 != userData.CompressedHashCode)
              {
                fileStream.Close();
                string errorMessage = Utility.Text.Format<int, int>("Resource compressed hash code error, need '{0}', downloaded '{1}'.", userData.CompressedHashCode, crc32);
                DownloadFailureEventArgs e2 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                this.OnDownloadFailure((object) this, e2);
                ReferencePool.Release((IReference) e2);
                return;
              }
              if (this.m_ResourceManager.m_DecompressCachedStream == null)
                this.m_ResourceManager.m_DecompressCachedStream = new MemoryStream();
              fileStream.Position = 0L;
              this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
              this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
              if (!Utility.Compression.Decompress((Stream) fileStream, (Stream) this.m_ResourceManager.m_DecompressCachedStream))
              {
                fileStream.Close();
                string errorMessage = Utility.Text.Format<string>("Unable to decompress resource '{0}'.", e.DownloadPath);
                DownloadFailureEventArgs e3 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                this.OnDownloadFailure((object) this, e3);
                ReferencePool.Release((IReference) e3);
                return;
              }
              if (this.m_ResourceManager.m_DecompressCachedStream.Length != (long) userData.Length)
              {
                fileStream.Close();
                string errorMessage = Utility.Text.Format<int, long>("Resource length error, need '{0}', downloaded '{1}'.", userData.Length, this.m_ResourceManager.m_DecompressCachedStream.Length);
                DownloadFailureEventArgs e4 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                this.OnDownloadFailure((object) this, e4);
                ReferencePool.Release((IReference) e4);
                return;
              }
              fileStream.Position = 0L;
              fileStream.SetLength(0L);
              fileStream.Write(this.m_ResourceManager.m_DecompressCachedStream.GetBuffer(), 0, (int) this.m_ResourceManager.m_DecompressCachedStream.Length);
            }
            else
            {
              int num = 0;
              fileStream.Position = 0L;
              if (userData.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || userData.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || userData.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || userData.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
              {
                Utility.Converter.GetBytes(userData.HashCode, this.m_CachedHashBytes);
                if (userData.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || userData.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt)
                  num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, 220);
                else if (userData.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || userData.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
                  num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, length);
                Array.Clear((Array) this.m_CachedHashBytes, 0, 4);
              }
              else
                num = Utility.Verifier.GetCrc32((Stream) fileStream);
              if (num != userData.HashCode)
              {
                fileStream.Close();
                string errorMessage = Utility.Text.Format<int, int>("Resource hash code error, need '{0}', downloaded '{1}'.", userData.HashCode, num);
                DownloadFailureEventArgs e5 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
                this.OnDownloadFailure((object) this, e5);
                ReferencePool.Release((IReference) e5);
                return;
              }
            }
          }
          if (userData.UseFileSystem)
          {
            IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(userData.FileSystemName, false);
            int num = fileSystem.WriteFile(userData.ResourceName.FullName, userData.ResourcePath) ? 1 : 0;
            if (File.Exists(userData.ResourcePath))
              File.Delete(userData.ResourcePath);
            if (num == 0)
            {
              string errorMessage = Utility.Text.Format<string>("Write resource to file system '{0}' error.", fileSystem.FullPath);
              DownloadFailureEventArgs e6 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e6);
              ReferencePool.Release((IReference) e6);
              return;
            }
          }
          this.m_UpdateCandidateInfo.Remove(userData.ResourceName);
          this.m_UpdateWaitingInfo.Remove(userData);
          this.m_UpdateWaitingInfoWhilePlaying.Remove(userData);
          this.m_ResourceManager.m_ResourceInfos[userData.ResourceName].MarkReady();
          this.m_ResourceManager.m_ReadWriteResourceInfos.Add(userData.ResourceName, new ResourceManager.ReadWriteResourceInfo(userData.FileSystemName, userData.LoadType, userData.Length, userData.HashCode));
          if (this.ResourceUpdateSuccess != null)
            this.ResourceUpdateSuccess(userData.ResourceName, e.DownloadPath, e.DownloadUri, userData.Length, userData.CompressedLength);
          this.m_CurrentGenerateReadWriteVersionListLength += userData.CompressedLength;
          if (this.m_UpdateCandidateInfo.Count <= 0 || this.m_UpdateWaitingInfo.Count + this.m_UpdateWaitingInfoWhilePlaying.Count <= 0 || this.m_CurrentGenerateReadWriteVersionListLength >= this.m_GenerateReadWriteVersionListLength)
            this.GenerateReadWriteVersionList();
          if (this.m_UpdatingResourceGroup != null && this.m_UpdateWaitingInfo.Count <= 0)
          {
            ResourceManager.ResourceGroup updatingResourceGroup = this.m_UpdatingResourceGroup;
            this.m_UpdatingResourceGroup = (ResourceManager.ResourceGroup) null;
            if (this.ResourceUpdateComplete != null)
              this.ResourceUpdateComplete(updatingResourceGroup, !this.m_FailureFlag);
          }
          if (this.m_UpdateCandidateInfo.Count > 0 || this.ResourceUpdateAllComplete == null)
            return;
          this.ResourceUpdateAllComplete();
        }
        catch (Exception ex)
        {
          string errorMessage = Utility.Text.Format<string, Exception>("Update resource '{0}' with error message '{1}'.", e.DownloadPath, ex);
          DownloadFailureEventArgs e7 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
          this.OnDownloadFailure((object) this, e7);
          ReferencePool.Release((IReference) e7);
        }
        finally
        {
          if (this.m_ResourceManager.m_DecompressCachedStream != null)
          {
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
          }
        }
      }

      private void OnDownloadFailure(object sender, DownloadFailureEventArgs e)
      {
        if (!(e.UserData is ResourceManager.ResourceUpdater.UpdateInfo userData))
          return;
        if (File.Exists(e.DownloadPath))
          File.Delete(e.DownloadPath);
        if (this.ResourceUpdateFailure != null)
          this.ResourceUpdateFailure(userData.ResourceName, e.DownloadUri, userData.RetryCount, this.m_UpdateRetryCount, e.ErrorMessage);
        if (userData.RetryCount < this.m_UpdateRetryCount)
        {
          userData.Downloading = false;
          ++userData.RetryCount;
          if (!this.m_UpdateWaitingInfoWhilePlaying.Contains(userData))
            return;
          this.DownloadResource(userData);
        }
        else
        {
          this.m_FailureFlag = true;
          userData.Downloading = false;
          userData.RetryCount = 0;
          this.m_UpdateWaitingInfo.Remove(userData);
          this.m_UpdateWaitingInfoWhilePlaying.Remove(userData);
        }
      }

      /// <summary>资源应用信息。</summary>
      [StructLayout(LayoutKind.Auto)]
      private struct ApplyInfo
      {
        private readonly ResourceManager.ResourceName m_ResourceName;
        private readonly string m_FileSystemName;
        private readonly ResourceManager.LoadType m_LoadType;
        private readonly long m_Offset;
        private readonly int m_Length;
        private readonly int m_HashCode;
        private readonly int m_CompressedLength;
        private readonly int m_CompressedHashCode;
        private readonly string m_ResourcePath;

        /// <summary>初始化资源应用信息的新实例。</summary>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="fileSystemName">资源所在的文件系统名称。</param>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="offset">资源偏移。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        /// <param name="compressedLength">压缩后大小。</param>
        /// <param name="compressedHashCode">压缩后哈希值。</param>
        /// <param name="resourcePath">资源路径。</param>
        public ApplyInfo(
          ResourceManager.ResourceName resourceName,
          string fileSystemName,
          ResourceManager.LoadType loadType,
          long offset,
          int length,
          int hashCode,
          int compressedLength,
          int compressedHashCode,
          string resourcePath)
        {
          this.m_ResourceName = resourceName;
          this.m_FileSystemName = fileSystemName;
          this.m_LoadType = loadType;
          this.m_Offset = offset;
          this.m_Length = length;
          this.m_HashCode = hashCode;
          this.m_CompressedLength = compressedLength;
          this.m_CompressedHashCode = compressedHashCode;
          this.m_ResourcePath = resourcePath;
        }

        /// <summary>获取资源名称。</summary>
        public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

        /// <summary>获取资源是否使用文件系统。</summary>
        public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

        /// <summary>获取资源所在的文件系统名称。</summary>
        public string FileSystemName => this.m_FileSystemName;

        /// <summary>获取资源加载方式。</summary>
        public ResourceManager.LoadType LoadType => this.m_LoadType;

        /// <summary>获取资源偏移。</summary>
        public long Offset => this.m_Offset;

        /// <summary>获取资源大小。</summary>
        public int Length => this.m_Length;

        /// <summary>获取资源哈希值。</summary>
        public int HashCode => this.m_HashCode;

        /// <summary>获取压缩后大小。</summary>
        public int CompressedLength => this.m_CompressedLength;

        /// <summary>获取压缩后哈希值。</summary>
        public int CompressedHashCode => this.m_CompressedHashCode;

        /// <summary>获取资源路径。</summary>
        public string ResourcePath => this.m_ResourcePath;
      }

      /// <summary>资源更新信息。</summary>
      private sealed class UpdateInfo
      {
        private readonly ResourceManager.ResourceName m_ResourceName;
        private readonly string m_FileSystemName;
        private readonly ResourceManager.LoadType m_LoadType;
        private readonly int m_Length;
        private readonly int m_HashCode;
        private readonly int m_CompressedLength;
        private readonly int m_CompressedHashCode;
        private readonly string m_ResourcePath;
        private bool m_Downloading;
        private int m_RetryCount;

        /// <summary>初始化资源更新信息的新实例。</summary>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="fileSystemName">资源所在的文件系统名称。</param>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        /// <param name="compressedLength">压缩后大小。</param>
        /// <param name="compressedHashCode">压缩后哈希值。</param>
        /// <param name="resourcePath">资源路径。</param>
        public UpdateInfo(
          ResourceManager.ResourceName resourceName,
          string fileSystemName,
          ResourceManager.LoadType loadType,
          int length,
          int hashCode,
          int compressedLength,
          int compressedHashCode,
          string resourcePath)
        {
          this.m_ResourceName = resourceName;
          this.m_FileSystemName = fileSystemName;
          this.m_LoadType = loadType;
          this.m_Length = length;
          this.m_HashCode = hashCode;
          this.m_CompressedLength = compressedLength;
          this.m_CompressedHashCode = compressedHashCode;
          this.m_ResourcePath = resourcePath;
          this.m_Downloading = false;
          this.m_RetryCount = 0;
        }

        /// <summary>获取资源名称。</summary>
        public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

        /// <summary>获取资源是否使用文件系统。</summary>
        public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

        /// <summary>获取资源所在的文件系统名称。</summary>
        public string FileSystemName => this.m_FileSystemName;

        /// <summary>获取资源加载方式。</summary>
        public ResourceManager.LoadType LoadType => this.m_LoadType;

        /// <summary>获取资源大小。</summary>
        public int Length => this.m_Length;

        /// <summary>获取资源哈希值。</summary>
        public int HashCode => this.m_HashCode;

        /// <summary>获取压缩后大小。</summary>
        public int CompressedLength => this.m_CompressedLength;

        /// <summary>获取压缩后哈希值。</summary>
        public int CompressedHashCode => this.m_CompressedHashCode;

        /// <summary>获取资源路径。</summary>
        public string ResourcePath => this.m_ResourcePath;

        /// <summary>获取或设置下载状态。</summary>
        public bool Downloading
        {
          get => this.m_Downloading;
          set => this.m_Downloading = value;
        }

        /// <summary>获取或设置已重试次数。</summary>
        public int RetryCount
        {
          get => this.m_RetryCount;
          set => this.m_RetryCount = value;
        }
      }
    }

    /// <summary>资源校验器。</summary>
    private sealed class ResourceVerifier
    {
      private const int CachedHashBytesLength = 4;
      private readonly ResourceManager m_ResourceManager;
      private readonly List<ResourceManager.ResourceVerifier.VerifyInfo> m_VerifyInfos;
      private readonly byte[] m_CachedHashBytes;
      private bool m_LoadReadWriteVersionListComplete;
      private int m_VerifyResourceLengthPerFrame;
      private int m_VerifyResourceIndex;
      private bool m_FailureFlag;
      public GameFrameworkAction<int, long> ResourceVerifyStart;
      public GameFrameworkAction<ResourceManager.ResourceName, int> ResourceVerifySuccess;
      public GameFrameworkAction<ResourceManager.ResourceName> ResourceVerifyFailure;
      public GameFrameworkAction<bool> ResourceVerifyComplete;

      /// <summary>初始化资源校验器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public ResourceVerifier(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_VerifyInfos = new List<ResourceManager.ResourceVerifier.VerifyInfo>();
        this.m_CachedHashBytes = new byte[4];
        this.m_LoadReadWriteVersionListComplete = false;
        this.m_VerifyResourceLengthPerFrame = 0;
        this.m_VerifyResourceIndex = 0;
        this.m_FailureFlag = false;
        this.ResourceVerifyStart = (GameFrameworkAction<int, long>) null;
        this.ResourceVerifySuccess = (GameFrameworkAction<ResourceManager.ResourceName, int>) null;
        this.ResourceVerifyFailure = (GameFrameworkAction<ResourceManager.ResourceName>) null;
        this.ResourceVerifyComplete = (GameFrameworkAction<bool>) null;
      }

      /// <summary>资源校验器轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (!this.m_LoadReadWriteVersionListComplete)
          return;
        int num = 0;
        while (this.m_VerifyResourceIndex < this.m_VerifyInfos.Count)
        {
          ResourceManager.ResourceVerifier.VerifyInfo verifyInfo = this.m_VerifyInfos[this.m_VerifyResourceIndex];
          num += verifyInfo.Length;
          if (this.VerifyResource(verifyInfo))
          {
            ++this.m_VerifyResourceIndex;
            if (this.ResourceVerifySuccess != null)
              this.ResourceVerifySuccess(verifyInfo.ResourceName, verifyInfo.Length);
          }
          else
          {
            this.m_FailureFlag = true;
            this.m_VerifyInfos.RemoveAt(this.m_VerifyResourceIndex);
            if (this.ResourceVerifyFailure != null)
              this.ResourceVerifyFailure(verifyInfo.ResourceName);
          }
          if (num >= this.m_VerifyResourceLengthPerFrame)
            return;
        }
        this.m_LoadReadWriteVersionListComplete = false;
        if (this.m_FailureFlag)
          this.GenerateReadWriteVersionList();
        if (this.ResourceVerifyComplete == null)
          return;
        this.ResourceVerifyComplete(!this.m_FailureFlag);
      }

      /// <summary>关闭并清理资源校验器。</summary>
      public void Shutdown()
      {
        this.m_VerifyInfos.Clear();
        this.m_LoadReadWriteVersionListComplete = false;
        this.m_VerifyResourceLengthPerFrame = 0;
        this.m_VerifyResourceIndex = 0;
        this.m_FailureFlag = false;
      }

      /// <summary>校验资源。</summary>
      /// <param name="verifyResourceLengthPerFrame">每帧至少校验资源的大小，以字节为单位。</param>
      public void VerifyResources(int verifyResourceLengthPerFrame)
      {
        if (verifyResourceLengthPerFrame < 0)
          throw new GameFrameworkException("Verify resource count per frame is invalid.");
        if (this.m_ResourceManager.m_ResourceHelper == null)
          throw new GameFrameworkException("Resource helper is invalid.");
        if (string.IsNullOrEmpty(this.m_ResourceManager.m_ReadWritePath))
          throw new GameFrameworkException("Read-write path is invalid.");
        this.m_VerifyResourceLengthPerFrame = verifyResourceLengthPerFrame;
        this.m_ResourceManager.m_ResourceHelper.LoadBytes(Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkList.dat")), new LoadBytesCallbacks(new LoadBytesSuccessCallback(this.OnLoadReadWriteVersionListSuccess), new LoadBytesFailureCallback(this.OnLoadReadWriteVersionListFailure)), (object) null);
      }

      private bool VerifyResource(
        ResourceManager.ResourceVerifier.VerifyInfo verifyInfo)
      {
        if (verifyInfo.UseFileSystem)
        {
          IFileSystem fileSystem = this.m_ResourceManager.GetFileSystem(verifyInfo.FileSystemName, false);
          string fullName = verifyInfo.ResourceName.FullName;
          int length = fileSystem.GetFileInfo(fullName).Length;
          if (length == verifyInfo.Length)
          {
            if (this.m_ResourceManager.m_DecompressCachedStream == null)
              this.m_ResourceManager.m_DecompressCachedStream = new MemoryStream();
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
            fileSystem.ReadFile(fullName, (Stream) this.m_ResourceManager.m_DecompressCachedStream);
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            int num = 0;
            if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
            {
              Utility.Converter.GetBytes(verifyInfo.HashCode, this.m_CachedHashBytes);
              if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt)
                num = Utility.Verifier.GetCrc32((Stream) this.m_ResourceManager.m_DecompressCachedStream, this.m_CachedHashBytes, 220);
              else if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
                num = Utility.Verifier.GetCrc32((Stream) this.m_ResourceManager.m_DecompressCachedStream, this.m_CachedHashBytes, length);
              Array.Clear((Array) this.m_CachedHashBytes, 0, 4);
            }
            else
              num = Utility.Verifier.GetCrc32((Stream) this.m_ResourceManager.m_DecompressCachedStream);
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
            if (num == verifyInfo.HashCode)
              return true;
          }
          fileSystem.DeleteFile(fullName);
          return false;
        }
        string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.ReadWritePath, verifyInfo.ResourceName.FullName));
        using (FileStream fileStream = new FileStream(regularPath, FileMode.Open, FileAccess.Read))
        {
          int length = (int) fileStream.Length;
          if (length == verifyInfo.Length)
          {
            int num = 0;
            if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
            {
              Utility.Converter.GetBytes(verifyInfo.HashCode, this.m_CachedHashBytes);
              if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndQuickDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndQuickDecrypt)
                num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, 220);
              else if (verifyInfo.LoadType == ResourceManager.LoadType.LoadFromMemoryAndDecrypt || verifyInfo.LoadType == ResourceManager.LoadType.LoadFromBinaryAndDecrypt)
                num = Utility.Verifier.GetCrc32((Stream) fileStream, this.m_CachedHashBytes, length);
              Array.Clear((Array) this.m_CachedHashBytes, 0, 4);
            }
            else
              num = Utility.Verifier.GetCrc32((Stream) fileStream);
            if (num == verifyInfo.HashCode)
              return true;
          }
        }
        File.Delete(regularPath);
        return false;
      }

      private void GenerateReadWriteVersionList()
      {
        string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkList.dat"));
        string str = Utility.Text.Format<string, string>("{0}.{1}", regularPath, "tmp");
        SortedDictionary<string, List<int>> sortedDictionary = new SortedDictionary<string, List<int>>((IComparer<string>) StringComparer.Ordinal);
        FileStream fileStream1 = (FileStream) null;
        FileStream fileStream2;
        try
        {
          fileStream1 = new FileStream(str, FileMode.Create, FileAccess.Write);
          LocalVersionList.Resource[] resources = this.m_VerifyInfos.Count > 0 ? new LocalVersionList.Resource[this.m_VerifyInfos.Count] : (LocalVersionList.Resource[]) null;
          if (resources != null)
          {
            int index = 0;
            foreach (ResourceManager.ResourceVerifier.VerifyInfo verifyInfo in this.m_VerifyInfos)
            {
              resources[index] = new LocalVersionList.Resource(verifyInfo.ResourceName.Name, verifyInfo.ResourceName.Variant, verifyInfo.ResourceName.Extension, (byte) verifyInfo.LoadType, verifyInfo.Length, verifyInfo.HashCode);
              if (verifyInfo.UseFileSystem)
              {
                List<int> intList = (List<int>) null;
                if (!sortedDictionary.TryGetValue(verifyInfo.FileSystemName, out intList))
                {
                  intList = new List<int>();
                  sortedDictionary.Add(verifyInfo.FileSystemName, intList);
                }
                intList.Add(index);
              }
              ++index;
            }
          }
          LocalVersionList.FileSystem[] fileSystems = sortedDictionary.Count > 0 ? new LocalVersionList.FileSystem[sortedDictionary.Count] : (LocalVersionList.FileSystem[]) null;
          if (fileSystems != null)
          {
            int num = 0;
            foreach (KeyValuePair<string, List<int>> keyValuePair in sortedDictionary)
            {
              fileSystems[num++] = new LocalVersionList.FileSystem(keyValuePair.Key, keyValuePair.Value.ToArray());
              keyValuePair.Value.Clear();
            }
          }
          LocalVersionList data = new LocalVersionList(resources, fileSystems);
          if (!this.m_ResourceManager.m_ReadWriteVersionListSerializer.Serialize((Stream) fileStream1, data))
            throw new GameFrameworkException("Serialize read-write version list failure.");
          if (fileStream1 != null)
          {
            fileStream1.Dispose();
            fileStream2 = (FileStream) null;
          }
        }
        catch (Exception ex)
        {
          if (fileStream1 != null)
          {
            fileStream1.Dispose();
            fileStream2 = (FileStream) null;
          }
          if (File.Exists(str))
            File.Delete(str);
          throw new GameFrameworkException(Utility.Text.Format<Exception>("Generate read-write version list exception '{0}'.", ex), ex);
        }
        if (File.Exists(regularPath))
          File.Delete(regularPath);
        File.Move(str, regularPath);
      }

      private void OnLoadReadWriteVersionListSuccess(
        string fileUri,
        byte[] bytes,
        float duration,
        object userData)
      {
        MemoryStream memoryStream = (MemoryStream) null;
        try
        {
          memoryStream = new MemoryStream(bytes, false);
          LocalVersionList localVersionList = this.m_ResourceManager.m_ReadWriteVersionListSerializer.Deserialize((Stream) memoryStream);
          LocalVersionList.Resource[] resourceArray = localVersionList.IsValid ? localVersionList.GetResources() : throw new GameFrameworkException("Deserialize read write version list failure.");
          LocalVersionList.FileSystem[] fileSystems = localVersionList.GetFileSystems();
          Dictionary<ResourceManager.ResourceName, string> dictionary = new Dictionary<ResourceManager.ResourceName, string>();
          foreach (LocalVersionList.FileSystem fileSystem in fileSystems)
          {
            foreach (int resourceIndex in fileSystem.GetResourceIndexes())
            {
              LocalVersionList.Resource resource = resourceArray[resourceIndex];
              dictionary.Add(new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension), fileSystem.Name);
            }
          }
          long num = 0;
          foreach (LocalVersionList.Resource resource in resourceArray)
          {
            ResourceManager.ResourceName resourceName = new ResourceManager.ResourceName(resource.Name, resource.Variant, resource.Extension);
            string fileSystemName = (string) null;
            dictionary.TryGetValue(resourceName, out fileSystemName);
            num += (long) resource.Length;
            this.m_VerifyInfos.Add(new ResourceManager.ResourceVerifier.VerifyInfo(resourceName, fileSystemName, (ResourceManager.LoadType) resource.LoadType, resource.Length, resource.HashCode));
          }
          this.m_LoadReadWriteVersionListComplete = true;
          if (this.ResourceVerifyStart == null)
            return;
          this.ResourceVerifyStart(this.m_VerifyInfos.Count, num);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Parse read-write version list exception '{0}'.", ex), ex);
          throw;
        }
        finally
        {
          memoryStream?.Dispose();
        }
      }

      private void OnLoadReadWriteVersionListFailure(
        string fileUri,
        string errorMessage,
        object userData)
      {
        if (this.ResourceVerifyComplete == null)
          return;
        this.ResourceVerifyComplete(true);
      }

      /// <summary>资源校验信息。</summary>
      private struct VerifyInfo
      {
        private readonly ResourceManager.ResourceName m_ResourceName;
        private readonly string m_FileSystemName;
        private readonly ResourceManager.LoadType m_LoadType;
        private readonly int m_Length;
        private readonly int m_HashCode;

        /// <summary>初始化资源校验信息的新实例。</summary>
        /// <param name="resourceName">资源名称。</param>
        /// <param name="fileSystemName">资源所在的文件系统名称。</param>
        /// <param name="loadType">资源加载方式。</param>
        /// <param name="length">资源大小。</param>
        /// <param name="hashCode">资源哈希值。</param>
        public VerifyInfo(
          ResourceManager.ResourceName resourceName,
          string fileSystemName,
          ResourceManager.LoadType loadType,
          int length,
          int hashCode)
        {
          this.m_ResourceName = resourceName;
          this.m_FileSystemName = fileSystemName;
          this.m_LoadType = loadType;
          this.m_Length = length;
          this.m_HashCode = hashCode;
        }

        /// <summary>获取资源名称。</summary>
        public ResourceManager.ResourceName ResourceName => this.m_ResourceName;

        /// <summary>获取资源是否使用文件系统。</summary>
        public bool UseFileSystem => !string.IsNullOrEmpty(this.m_FileSystemName);

        /// <summary>获取资源所在的文件系统名称。</summary>
        public string FileSystemName => this.m_FileSystemName;

        /// <summary>获取资源加载方式。</summary>
        public ResourceManager.LoadType LoadType => this.m_LoadType;

        /// <summary>获取资源大小。</summary>
        public int Length => this.m_Length;

        /// <summary>获取资源哈希值。</summary>
        public int HashCode => this.m_HashCode;
      }
    }

    /// <summary>版本资源列表处理器。</summary>
    private sealed class VersionListProcessor
    {
      private readonly ResourceManager m_ResourceManager;
      private IDownloadManager m_DownloadManager;
      private int m_VersionListLength;
      private int m_VersionListHashCode;
      private int m_VersionListCompressedLength;
      private int m_VersionListCompressedHashCode;
      public GameFrameworkAction<string, string> VersionListUpdateSuccess;
      public GameFrameworkAction<string, string> VersionListUpdateFailure;

      /// <summary>初始化版本资源列表处理器的新实例。</summary>
      /// <param name="resourceManager">资源管理器。</param>
      public VersionListProcessor(ResourceManager resourceManager)
      {
        this.m_ResourceManager = resourceManager;
        this.m_DownloadManager = (IDownloadManager) null;
        this.m_VersionListLength = 0;
        this.m_VersionListHashCode = 0;
        this.m_VersionListCompressedLength = 0;
        this.m_VersionListCompressedHashCode = 0;
        this.VersionListUpdateSuccess = (GameFrameworkAction<string, string>) null;
        this.VersionListUpdateFailure = (GameFrameworkAction<string, string>) null;
      }

      /// <summary>关闭并清理版本资源列表处理器。</summary>
      public void Shutdown()
      {
        if (this.m_DownloadManager == null)
          return;
        this.m_DownloadManager.DownloadSuccess -= new EventHandler<DownloadSuccessEventArgs>(this.OnDownloadSuccess);
        this.m_DownloadManager.DownloadFailure -= new EventHandler<DownloadFailureEventArgs>(this.OnDownloadFailure);
      }

      /// <summary>设置下载管理器。</summary>
      /// <param name="downloadManager">下载管理器。</param>
      public void SetDownloadManager(IDownloadManager downloadManager)
      {
        this.m_DownloadManager = downloadManager != null ? downloadManager : throw new GameFrameworkException("Download manager is invalid.");
        this.m_DownloadManager.DownloadSuccess += new EventHandler<DownloadSuccessEventArgs>(this.OnDownloadSuccess);
        this.m_DownloadManager.DownloadFailure += new EventHandler<DownloadFailureEventArgs>(this.OnDownloadFailure);
      }

      /// <summary>检查版本资源列表。</summary>
      /// <param name="latestInternalResourceVersion">最新的内部资源版本号。</param>
      /// <returns>检查版本资源列表结果。</returns>
      public CheckVersionListResult CheckVersionList(int latestInternalResourceVersion)
      {
        string path = !string.IsNullOrEmpty(this.m_ResourceManager.m_ReadWritePath) ? Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkVersion.dat")) : throw new GameFrameworkException("Read-write path is invalid.");
        if (!File.Exists(path))
          return CheckVersionListResult.NeedUpdate;
        int num = 0;
        FileStream fileStream = (FileStream) null;
        try
        {
          fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
          object obj = (object) null;
          if (!this.m_ResourceManager.m_UpdatableVersionListSerializer.TryGetValue((Stream) fileStream, "InternalResourceVersion", out obj))
            return CheckVersionListResult.NeedUpdate;
          num = (int) obj;
        }
        catch
        {
          return CheckVersionListResult.NeedUpdate;
        }
        finally
        {
          fileStream?.Dispose();
        }
        return num != latestInternalResourceVersion ? CheckVersionListResult.NeedUpdate : CheckVersionListResult.Updated;
      }

      /// <summary>更新版本资源列表。</summary>
      /// <param name="versionListLength">版本资源列表大小。</param>
      /// <param name="versionListHashCode">版本资源列表哈希值。</param>
      /// <param name="versionListCompressedLength">版本资源列表压缩后大小。</param>
      /// <param name="versionListCompressedHashCode">版本资源列表压缩后哈希值。</param>
      public void UpdateVersionList(
        int versionListLength,
        int versionListHashCode,
        int versionListCompressedLength,
        int versionListCompressedHashCode)
      {
        if (this.m_DownloadManager == null)
          throw new GameFrameworkException("You must set download manager first.");
        this.m_VersionListLength = versionListLength;
        this.m_VersionListHashCode = versionListHashCode;
        this.m_VersionListCompressedLength = versionListCompressedLength;
        this.m_VersionListCompressedHashCode = versionListCompressedHashCode;
        string regularPath = Utility.Path.GetRegularPath(System.IO.Path.Combine(this.m_ResourceManager.m_ReadWritePath, "GameFrameworkVersion.dat"));
        int length = "GameFrameworkVersion.dat".LastIndexOf('.');
        string path2 = Utility.Text.Format<string, string, int>("{0}.{2:x8}.{1}", "GameFrameworkVersion.dat".Substring(0, length), "GameFrameworkVersion.dat".Substring(length + 1), this.m_VersionListHashCode);
        this.m_DownloadManager.AddDownload(regularPath, Utility.Path.GetRemotePath(System.IO.Path.Combine(this.m_ResourceManager.m_UpdatePrefixUri, path2)), (object) this);
      }

      private void OnDownloadSuccess(object sender, DownloadSuccessEventArgs e)
      {
        if (!(e.UserData is ResourceManager.VersionListProcessor userData))
          return;
        if (userData != this)
          return;
        try
        {
          using (FileStream fileStream = new FileStream(e.DownloadPath, FileMode.Open, FileAccess.ReadWrite))
          {
            int length = (int) fileStream.Length;
            if (length != this.m_VersionListCompressedLength)
            {
              fileStream.Close();
              string errorMessage = Utility.Text.Format<int, int>("Latest version list compressed length error, need '{0}', downloaded '{1}'.", this.m_VersionListCompressedLength, length);
              DownloadFailureEventArgs e1 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e1);
              ReferencePool.Release((IReference) e1);
              return;
            }
            fileStream.Position = 0L;
            int crc32 = Utility.Verifier.GetCrc32((Stream) fileStream);
            if (crc32 != this.m_VersionListCompressedHashCode)
            {
              fileStream.Close();
              string errorMessage = Utility.Text.Format<int, int>("Latest version list compressed hash code error, need '{0}', downloaded '{1}'.", this.m_VersionListCompressedHashCode, crc32);
              DownloadFailureEventArgs e2 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e2);
              ReferencePool.Release((IReference) e2);
              return;
            }
            if (this.m_ResourceManager.m_DecompressCachedStream == null)
              this.m_ResourceManager.m_DecompressCachedStream = new MemoryStream();
            fileStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
            if (!Utility.Compression.Decompress((Stream) fileStream, (Stream) this.m_ResourceManager.m_DecompressCachedStream))
            {
              fileStream.Close();
              string errorMessage = Utility.Text.Format<string>("Unable to decompress latest version list '{0}'.", e.DownloadPath);
              DownloadFailureEventArgs e3 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e3);
              ReferencePool.Release((IReference) e3);
              return;
            }
            if (this.m_ResourceManager.m_DecompressCachedStream.Length != (long) this.m_VersionListLength)
            {
              fileStream.Close();
              string errorMessage = Utility.Text.Format<int, long>("Latest version list length error, need '{0}', downloaded '{1}'.", this.m_VersionListLength, this.m_ResourceManager.m_DecompressCachedStream.Length);
              DownloadFailureEventArgs e4 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
              this.OnDownloadFailure((object) this, e4);
              ReferencePool.Release((IReference) e4);
              return;
            }
            fileStream.Position = 0L;
            fileStream.SetLength(0L);
            fileStream.Write(this.m_ResourceManager.m_DecompressCachedStream.GetBuffer(), 0, (int) this.m_ResourceManager.m_DecompressCachedStream.Length);
          }
          if (this.VersionListUpdateSuccess == null)
            return;
          this.VersionListUpdateSuccess(e.DownloadPath, e.DownloadUri);
        }
        catch (Exception ex)
        {
          string errorMessage = Utility.Text.Format<string, Exception>("Update latest version list '{0}' with error message '{1}'.", e.DownloadPath, ex);
          DownloadFailureEventArgs e5 = DownloadFailureEventArgs.Create(e.SerialId, e.DownloadPath, e.DownloadUri, errorMessage, e.UserData);
          this.OnDownloadFailure((object) this, e5);
          ReferencePool.Release((IReference) e5);
        }
        finally
        {
          if (this.m_ResourceManager.m_DecompressCachedStream != null)
          {
            this.m_ResourceManager.m_DecompressCachedStream.Position = 0L;
            this.m_ResourceManager.m_DecompressCachedStream.SetLength(0L);
          }
        }
      }

      private void OnDownloadFailure(object sender, DownloadFailureEventArgs e)
      {
        if (!(e.UserData is ResourceManager.VersionListProcessor userData) || userData != this)
          return;
        if (File.Exists(e.DownloadPath))
          File.Delete(e.DownloadPath);
        if (this.VersionListUpdateFailure == null)
          return;
        this.VersionListUpdateFailure(e.DownloadUri, e.ErrorMessage);
      }
    }
  }
}
