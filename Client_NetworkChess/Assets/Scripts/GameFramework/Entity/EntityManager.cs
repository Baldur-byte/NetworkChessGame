// Decompiled with JetBrains decompiler
// Type: GameFramework.Entity.EntityManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.ObjectPool;
using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Entity
{
  /// <summary>实体管理器。</summary>
  internal sealed class EntityManager : GameFrameworkModule, IEntityManager
  {
    private readonly Dictionary<int, EntityManager.EntityInfo> m_EntityInfos;
    private readonly Dictionary<string, EntityManager.EntityGroup> m_EntityGroups;
    private readonly Dictionary<int, int> m_EntitiesBeingLoaded;
    private readonly HashSet<int> m_EntitiesToReleaseOnLoad;
    private readonly Queue<EntityManager.EntityInfo> m_RecycleQueue;
    private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
    private IObjectPoolManager m_ObjectPoolManager;
    private IResourceManager m_ResourceManager;
    private IEntityHelper m_EntityHelper;
    private int m_Serial;
    private bool m_IsShutdown;
    private EventHandler<ShowEntitySuccessEventArgs> m_ShowEntitySuccessEventHandler;
    private EventHandler<ShowEntityFailureEventArgs> m_ShowEntityFailureEventHandler;
    private EventHandler<ShowEntityUpdateEventArgs> m_ShowEntityUpdateEventHandler;
    private EventHandler<ShowEntityDependencyAssetEventArgs> m_ShowEntityDependencyAssetEventHandler;
    private EventHandler<HideEntityCompleteEventArgs> m_HideEntityCompleteEventHandler;

    /// <summary>初始化实体管理器的新实例。</summary>
    public EntityManager()
    {
      this.m_EntityInfos = new Dictionary<int, EntityManager.EntityInfo>();
      this.m_EntityGroups = new Dictionary<string, EntityManager.EntityGroup>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_EntitiesBeingLoaded = new Dictionary<int, int>();
      this.m_EntitiesToReleaseOnLoad = new HashSet<int>();
      this.m_RecycleQueue = new Queue<EntityManager.EntityInfo>();
      this.m_LoadAssetCallbacks = new LoadAssetCallbacks(new GameFramework.Resource.LoadAssetSuccessCallback(this.LoadAssetSuccessCallback), new GameFramework.Resource.LoadAssetFailureCallback(this.LoadAssetFailureCallback), new GameFramework.Resource.LoadAssetUpdateCallback(this.LoadAssetUpdateCallback), new GameFramework.Resource.LoadAssetDependencyAssetCallback(this.LoadAssetDependencyAssetCallback));
      this.m_ObjectPoolManager = (IObjectPoolManager) null;
      this.m_ResourceManager = (IResourceManager) null;
      this.m_EntityHelper = (IEntityHelper) null;
      this.m_Serial = 0;
      this.m_IsShutdown = false;
      this.m_ShowEntitySuccessEventHandler = (EventHandler<ShowEntitySuccessEventArgs>) null;
      this.m_ShowEntityFailureEventHandler = (EventHandler<ShowEntityFailureEventArgs>) null;
      this.m_ShowEntityUpdateEventHandler = (EventHandler<ShowEntityUpdateEventArgs>) null;
      this.m_ShowEntityDependencyAssetEventHandler = (EventHandler<ShowEntityDependencyAssetEventArgs>) null;
      this.m_HideEntityCompleteEventHandler = (EventHandler<HideEntityCompleteEventArgs>) null;
    }

    /// <summary>获取实体数量。</summary>
    public int EntityCount => this.m_EntityInfos.Count;

    /// <summary>获取实体组数量。</summary>
    public int EntityGroupCount => this.m_EntityGroups.Count;

    /// <summary>显示实体成功事件。</summary>
    public event EventHandler<ShowEntitySuccessEventArgs> ShowEntitySuccess
    {
      add => this.m_ShowEntitySuccessEventHandler += value;
      remove => this.m_ShowEntitySuccessEventHandler -= value;
    }

    /// <summary>显示实体失败事件。</summary>
    public event EventHandler<ShowEntityFailureEventArgs> ShowEntityFailure
    {
      add => this.m_ShowEntityFailureEventHandler += value;
      remove => this.m_ShowEntityFailureEventHandler -= value;
    }

    /// <summary>显示实体更新事件。</summary>
    public event EventHandler<ShowEntityUpdateEventArgs> ShowEntityUpdate
    {
      add => this.m_ShowEntityUpdateEventHandler += value;
      remove => this.m_ShowEntityUpdateEventHandler -= value;
    }

    /// <summary>显示实体时加载依赖资源事件。</summary>
    public event EventHandler<ShowEntityDependencyAssetEventArgs> ShowEntityDependencyAsset
    {
      add => this.m_ShowEntityDependencyAssetEventHandler += value;
      remove => this.m_ShowEntityDependencyAssetEventHandler -= value;
    }

    /// <summary>隐藏实体完成事件。</summary>
    public event EventHandler<HideEntityCompleteEventArgs> HideEntityComplete
    {
      add => this.m_HideEntityCompleteEventHandler += value;
      remove => this.m_HideEntityCompleteEventHandler -= value;
    }

    /// <summary>实体管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      while (this.m_RecycleQueue.Count > 0)
      {
        EntityManager.EntityInfo entityInfo = this.m_RecycleQueue.Dequeue();
        IEntity entity = entityInfo.Entity;
        EntityManager.EntityGroup entityGroup = (EntityManager.EntityGroup) entity.EntityGroup;
        if (entityGroup == null)
          throw new GameFrameworkException("Entity group is invalid.");
        entityInfo.Status = EntityManager.EntityStatus.WillRecycle;
        entity.OnRecycle();
        entityInfo.Status = EntityManager.EntityStatus.Recycled;
        entityGroup.UnspawnEntity(entity);
        ReferencePool.Release((IReference) entityInfo);
      }
      foreach (KeyValuePair<string, EntityManager.EntityGroup> entityGroup in this.m_EntityGroups)
        entityGroup.Value.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理实体管理器。</summary>
    internal override void Shutdown()
    {
      this.m_IsShutdown = true;
      this.HideAllLoadedEntities();
      this.m_EntityGroups.Clear();
      this.m_EntitiesBeingLoaded.Clear();
      this.m_EntitiesToReleaseOnLoad.Clear();
      this.m_RecycleQueue.Clear();
    }

    /// <summary>设置对象池管理器。</summary>
    /// <param name="objectPoolManager">对象池管理器。</param>
    public void SetObjectPoolManager(IObjectPoolManager objectPoolManager) => this.m_ObjectPoolManager = objectPoolManager != null ? objectPoolManager : throw new GameFrameworkException("Object pool manager is invalid.");

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>设置实体辅助器。</summary>
    /// <param name="entityHelper">实体辅助器。</param>
    public void SetEntityHelper(IEntityHelper entityHelper) => this.m_EntityHelper = entityHelper != null ? entityHelper : throw new GameFrameworkException("Entity helper is invalid.");

    /// <summary>是否存在实体组。</summary>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <returns>是否存在实体组。</returns>
    public bool HasEntityGroup(string entityGroupName) => !string.IsNullOrEmpty(entityGroupName) ? this.m_EntityGroups.ContainsKey(entityGroupName) : throw new GameFrameworkException("Entity group name is invalid.");

    /// <summary>获取实体组。</summary>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <returns>要获取的实体组。</returns>
    public IEntityGroup GetEntityGroup(string entityGroupName)
    {
      if (string.IsNullOrEmpty(entityGroupName))
        throw new GameFrameworkException("Entity group name is invalid.");
      EntityManager.EntityGroup entityGroup = (EntityManager.EntityGroup) null;
      return this.m_EntityGroups.TryGetValue(entityGroupName, out entityGroup) ? (IEntityGroup) entityGroup : (IEntityGroup) null;
    }

    /// <summary>获取所有实体组。</summary>
    /// <returns>所有实体组。</returns>
    public IEntityGroup[] GetAllEntityGroups()
    {
      int num = 0;
      IEntityGroup[] allEntityGroups = new IEntityGroup[this.m_EntityGroups.Count];
      foreach (KeyValuePair<string, EntityManager.EntityGroup> entityGroup in this.m_EntityGroups)
        allEntityGroups[num++] = (IEntityGroup) entityGroup.Value;
      return allEntityGroups;
    }

    /// <summary>获取所有实体组。</summary>
    /// <param name="results">所有实体组。</param>
    public void GetAllEntityGroups(List<IEntityGroup> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, EntityManager.EntityGroup> entityGroup in this.m_EntityGroups)
        results.Add((IEntityGroup) entityGroup.Value);
    }

    /// <summary>增加实体组。</summary>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <param name="instanceAutoReleaseInterval">实体实例对象池自动释放可释放对象的间隔秒数。</param>
    /// <param name="instanceCapacity">实体实例对象池容量。</param>
    /// <param name="instanceExpireTime">实体实例对象池对象过期秒数。</param>
    /// <param name="instancePriority">实体实例对象池的优先级。</param>
    /// <param name="entityGroupHelper">实体组辅助器。</param>
    /// <returns>是否增加实体组成功。</returns>
    public bool AddEntityGroup(
      string entityGroupName,
      float instanceAutoReleaseInterval,
      int instanceCapacity,
      float instanceExpireTime,
      int instancePriority,
      IEntityGroupHelper entityGroupHelper)
    {
      if (string.IsNullOrEmpty(entityGroupName))
        throw new GameFrameworkException("Entity group name is invalid.");
      if (entityGroupHelper == null)
        throw new GameFrameworkException("Entity group helper is invalid.");
      if (this.m_ObjectPoolManager == null)
        throw new GameFrameworkException("You must set object pool manager first.");
      if (this.HasEntityGroup(entityGroupName))
        return false;
      this.m_EntityGroups.Add(entityGroupName, new EntityManager.EntityGroup(entityGroupName, instanceAutoReleaseInterval, instanceCapacity, instanceExpireTime, instancePriority, entityGroupHelper, this.m_ObjectPoolManager));
      return true;
    }

    /// <summary>是否存在实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <returns>是否存在实体。</returns>
    public bool HasEntity(int entityId) => this.m_EntityInfos.ContainsKey(entityId);

    /// <summary>是否存在实体。</summary>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <returns>是否存在实体。</returns>
    public bool HasEntity(string entityAssetName)
    {
      if (string.IsNullOrEmpty(entityAssetName))
        throw new GameFrameworkException("Entity asset name is invalid.");
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
      {
        if (entityInfo.Value.Entity.EntityAssetName == entityAssetName)
          return true;
      }
      return false;
    }

    /// <summary>获取实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <returns>要获取的实体。</returns>
    public IEntity GetEntity(int entityId) => this.GetEntityInfo(entityId)?.Entity;

    /// <summary>获取实体。</summary>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <returns>要获取的实体。</returns>
    public IEntity GetEntity(string entityAssetName)
    {
      if (string.IsNullOrEmpty(entityAssetName))
        throw new GameFrameworkException("Entity asset name is invalid.");
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
      {
        if (entityInfo.Value.Entity.EntityAssetName == entityAssetName)
          return entityInfo.Value.Entity;
      }
      return (IEntity) null;
    }

    /// <summary>获取实体。</summary>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <returns>要获取的实体。</returns>
    public IEntity[] GetEntities(string entityAssetName)
    {
      if (string.IsNullOrEmpty(entityAssetName))
        throw new GameFrameworkException("Entity asset name is invalid.");
      List<IEntity> entityList = new List<IEntity>();
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
      {
        if (entityInfo.Value.Entity.EntityAssetName == entityAssetName)
          entityList.Add(entityInfo.Value.Entity);
      }
      return entityList.ToArray();
    }

    /// <summary>获取实体。</summary>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="results">要获取的实体。</param>
    public void GetEntities(string entityAssetName, List<IEntity> results)
    {
      if (string.IsNullOrEmpty(entityAssetName))
        throw new GameFrameworkException("Entity asset name is invalid.");
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
      {
        if (entityInfo.Value.Entity.EntityAssetName == entityAssetName)
          results.Add(entityInfo.Value.Entity);
      }
    }

    /// <summary>获取所有已加载的实体。</summary>
    /// <returns>所有已加载的实体。</returns>
    public IEntity[] GetAllLoadedEntities()
    {
      int num = 0;
      IEntity[] allLoadedEntities = new IEntity[this.m_EntityInfos.Count];
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
        allLoadedEntities[num++] = entityInfo.Value.Entity;
      return allLoadedEntities;
    }

    /// <summary>获取所有已加载的实体。</summary>
    /// <param name="results">所有已加载的实体。</param>
    public void GetAllLoadedEntities(List<IEntity> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<int, EntityManager.EntityInfo> entityInfo in this.m_EntityInfos)
        results.Add(entityInfo.Value.Entity);
    }

    /// <summary>获取所有正在加载实体的编号。</summary>
    /// <returns>所有正在加载实体的编号。</returns>
    public int[] GetAllLoadingEntityIds()
    {
      int num = 0;
      int[] loadingEntityIds = new int[this.m_EntitiesBeingLoaded.Count];
      foreach (KeyValuePair<int, int> keyValuePair in this.m_EntitiesBeingLoaded)
        loadingEntityIds[num++] = keyValuePair.Key;
      return loadingEntityIds;
    }

    /// <summary>获取所有正在加载实体的编号。</summary>
    /// <param name="results">所有正在加载实体的编号。</param>
    public void GetAllLoadingEntityIds(List<int> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<int, int> keyValuePair in this.m_EntitiesBeingLoaded)
        results.Add(keyValuePair.Key);
    }

    /// <summary>是否正在加载实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <returns>是否正在加载实体。</returns>
    public bool IsLoadingEntity(int entityId) => this.m_EntitiesBeingLoaded.ContainsKey(entityId);

    /// <summary>是否是合法的实体。</summary>
    /// <param name="entity">实体。</param>
    /// <returns>实体是否合法。</returns>
    public bool IsValidEntity(IEntity entity) => entity != null && this.HasEntity(entity.Id);

    /// <summary>显示实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroupName">实体组名称。</param>
    public void ShowEntity(int entityId, string entityAssetName, string entityGroupName) => this.ShowEntity(entityId, entityAssetName, entityGroupName, 0, (object) null);

    /// <summary>显示实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <param name="priority">加载实体资源的优先级。</param>
    public void ShowEntity(
      int entityId,
      string entityAssetName,
      string entityGroupName,
      int priority)
    {
      this.ShowEntity(entityId, entityAssetName, entityGroupName, priority, (object) null);
    }

    /// <summary>显示实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ShowEntity(
      int entityId,
      string entityAssetName,
      string entityGroupName,
      object userData)
    {
      this.ShowEntity(entityId, entityAssetName, entityGroupName, 0, userData);
    }

    /// <summary>显示实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <param name="priority">加载实体资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ShowEntity(
      int entityId,
      string entityAssetName,
      string entityGroupName,
      int priority,
      object userData)
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_EntityHelper == null)
        throw new GameFrameworkException("You must set entity helper first.");
      if (string.IsNullOrEmpty(entityAssetName))
        throw new GameFrameworkException("Entity asset name is invalid.");
      if (string.IsNullOrEmpty(entityGroupName))
        throw new GameFrameworkException("Entity group name is invalid.");
      if (this.HasEntity(entityId))
        throw new GameFrameworkException(Utility.Text.Format<int>("Entity id '{0}' is already exist.", entityId));
      if (this.IsLoadingEntity(entityId))
        throw new GameFrameworkException(Utility.Text.Format<int>("Entity '{0}' is already being loaded.", entityId));
      EntityManager.EntityGroup entityGroup = (EntityManager.EntityGroup) this.GetEntityGroup(entityGroupName);
      EntityManager.EntityInstanceObject entityInstanceObject = entityGroup != null ? entityGroup.SpawnEntityInstanceObject(entityAssetName) : throw new GameFrameworkException(Utility.Text.Format<string>("Entity group '{0}' is not exist.", entityGroupName));
      if (entityInstanceObject == null)
      {
        int serialId = ++this.m_Serial;
        this.m_EntitiesBeingLoaded.Add(entityId, serialId);
        this.m_ResourceManager.LoadAsset(entityAssetName, priority, this.m_LoadAssetCallbacks, (object) EntityManager.ShowEntityInfo.Create(serialId, entityId, entityGroup, userData));
      }
      else
        this.InternalShowEntity(entityId, entityAssetName, entityGroup, entityInstanceObject.Target, false, 0.0f, userData);
    }

    /// <summary>隐藏实体。</summary>
    /// <param name="entityId">实体编号。</param>
    public void HideEntity(int entityId) => this.HideEntity(entityId, (object) null);

    /// <summary>隐藏实体。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void HideEntity(int entityId, object userData)
    {
      if (this.IsLoadingEntity(entityId))
      {
        this.m_EntitiesToReleaseOnLoad.Add(this.m_EntitiesBeingLoaded[entityId]);
        this.m_EntitiesBeingLoaded.Remove(entityId);
      }
      else
        this.InternalHideEntity(this.GetEntityInfo(entityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find entity '{0}'.", entityId)), userData);
    }

    /// <summary>隐藏实体。</summary>
    /// <param name="entity">实体。</param>
    public void HideEntity(IEntity entity) => this.HideEntity(entity, (object) null);

    /// <summary>隐藏实体。</summary>
    /// <param name="entity">实体。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void HideEntity(IEntity entity, object userData)
    {
      if (entity == null)
        throw new GameFrameworkException("Entity is invalid.");
      this.HideEntity(entity.Id, userData);
    }

    /// <summary>隐藏所有已加载的实体。</summary>
    public void HideAllLoadedEntities() => this.HideAllLoadedEntities((object) null);

    /// <summary>隐藏所有已加载的实体。</summary>
    /// <param name="userData">用户自定义数据。</param>
    public void HideAllLoadedEntities(object userData)
    {
      while (this.m_EntityInfos.Count > 0)
      {
        using (Dictionary<int, EntityManager.EntityInfo>.Enumerator enumerator = this.m_EntityInfos.GetEnumerator())
        {
          if (enumerator.MoveNext())
            this.InternalHideEntity(enumerator.Current.Value, userData);
        }
      }
    }

    /// <summary>隐藏所有正在加载的实体。</summary>
    public void HideAllLoadingEntities()
    {
      foreach (KeyValuePair<int, int> keyValuePair in this.m_EntitiesBeingLoaded)
        this.m_EntitiesToReleaseOnLoad.Add(keyValuePair.Value);
      this.m_EntitiesBeingLoaded.Clear();
    }

    /// <summary>获取父实体。</summary>
    /// <param name="childEntityId">要获取父实体的子实体的实体编号。</param>
    /// <returns>子实体的父实体。</returns>
    public IEntity GetParentEntity(int childEntityId) => (this.GetEntityInfo(childEntityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find child entity '{0}'.", childEntityId))).ParentEntity;

    /// <summary>获取父实体。</summary>
    /// <param name="childEntity">要获取父实体的子实体。</param>
    /// <returns>子实体的父实体。</returns>
    public IEntity GetParentEntity(IEntity childEntity) => childEntity != null ? this.GetParentEntity(childEntity.Id) : throw new GameFrameworkException("Child entity is invalid.");

    /// <summary>获取子实体数量。</summary>
    /// <param name="parentEntityId">要获取子实体数量的父实体的实体编号。</param>
    /// <returns>子实体数量。</returns>
    public int GetChildEntityCount(int parentEntityId) => (this.GetEntityInfo(parentEntityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId))).ChildEntityCount;

    /// <summary>获取子实体。</summary>
    /// <param name="parentEntityId">要获取子实体的父实体的实体编号。</param>
    /// <returns>子实体。</returns>
    public IEntity GetChildEntity(int parentEntityId) => (this.GetEntityInfo(parentEntityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId))).GetChildEntity();

    /// <summary>获取子实体。</summary>
    /// <param name="parentEntity">要获取子实体的父实体。</param>
    /// <returns>子实体。</returns>
    public IEntity GetChildEntity(IEntity parentEntity) => parentEntity != null ? this.GetChildEntity(parentEntity.Id) : throw new GameFrameworkException("Parent entity is invalid.");

    /// <summary>获取所有子实体。</summary>
    /// <param name="parentEntityId">要获取所有子实体的父实体的实体编号。</param>
    /// <returns>所有子实体。</returns>
    public IEntity[] GetChildEntities(int parentEntityId) => (this.GetEntityInfo(parentEntityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId))).GetChildEntities();

    /// <summary>获取所有子实体。</summary>
    /// <param name="parentEntityId">要获取所有子实体的父实体的实体编号。</param>
    /// <param name="results">所有子实体。</param>
    public void GetChildEntities(int parentEntityId, List<IEntity> results) => (this.GetEntityInfo(parentEntityId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId))).GetChildEntities(results);

    /// <summary>获取所有子实体。</summary>
    /// <param name="parentEntity">要获取所有子实体的父实体。</param>
    /// <returns>所有子实体。</returns>
    public IEntity[] GetChildEntities(IEntity parentEntity) => parentEntity != null ? this.GetChildEntities(parentEntity.Id) : throw new GameFrameworkException("Parent entity is invalid.");

    /// <summary>获取所有子实体。</summary>
    /// <param name="parentEntity">要获取所有子实体的父实体。</param>
    /// <param name="results">所有子实体。</param>
    public void GetChildEntities(IEntity parentEntity, List<IEntity> results)
    {
      if (parentEntity == null)
        throw new GameFrameworkException("Parent entity is invalid.");
      this.GetChildEntities(parentEntity.Id, results);
    }

    /// <summary>附加子实体。</summary>
    /// <param name="childEntityId">要附加的子实体的实体编号。</param>
    /// <param name="parentEntityId">被附加的父实体的实体编号。</param>
    public void AttachEntity(int childEntityId, int parentEntityId) => this.AttachEntity(childEntityId, parentEntityId, (object) null);

    /// <summary>附加子实体。</summary>
    /// <param name="childEntityId">要附加的子实体的实体编号。</param>
    /// <param name="parentEntityId">被附加的父实体的实体编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void AttachEntity(int childEntityId, int parentEntityId, object userData)
    {
      EntityManager.EntityInfo entityInfo1 = childEntityId != parentEntityId ? this.GetEntityInfo(childEntityId) : throw new GameFrameworkException(Utility.Text.Format<int>("Can not attach entity when child entity id equals to parent entity id '{0}'.", parentEntityId));
      if (entityInfo1 == null)
        throw new GameFrameworkException(Utility.Text.Format<int>("Can not find child entity '{0}'.", childEntityId));
      if (entityInfo1.Status >= EntityManager.EntityStatus.WillHide)
        throw new GameFrameworkException(Utility.Text.Format<EntityManager.EntityStatus>("Can not attach entity when child entity status is '{0}'.", entityInfo1.Status));
      EntityManager.EntityInfo entityInfo2 = this.GetEntityInfo(parentEntityId);
      if (entityInfo2 == null)
        throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId));
      if (entityInfo2.Status >= EntityManager.EntityStatus.WillHide)
        throw new GameFrameworkException(Utility.Text.Format<EntityManager.EntityStatus>("Can not attach entity when parent entity status is '{0}'.", entityInfo2.Status));
      IEntity entity1 = entityInfo1.Entity;
      IEntity entity2 = entityInfo2.Entity;
      this.DetachEntity(entity1.Id, userData);
      entityInfo1.ParentEntity = entity2;
      entityInfo2.AddChildEntity(entity1);
      entity2.OnAttached(entity1, userData);
      entity1.OnAttachTo(entity2, userData);
    }

    /// <summary>附加子实体。</summary>
    /// <param name="childEntityId">要附加的子实体的实体编号。</param>
    /// <param name="parentEntity">被附加的父实体。</param>
    public void AttachEntity(int childEntityId, IEntity parentEntity) => this.AttachEntity(childEntityId, parentEntity, (object) null);

    /// <summary>附加子实体。</summary>
    /// <param name="childEntityId">要附加的子实体的实体编号。</param>
    /// <param name="parentEntity">被附加的父实体。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void AttachEntity(int childEntityId, IEntity parentEntity, object userData)
    {
      if (parentEntity == null)
        throw new GameFrameworkException("Parent entity is invalid.");
      this.AttachEntity(childEntityId, parentEntity.Id, userData);
    }

    /// <summary>附加子实体。</summary>
    /// <param name="childEntity">要附加的子实体。</param>
    /// <param name="parentEntityId">被附加的父实体的实体编号。</param>
    public void AttachEntity(IEntity childEntity, int parentEntityId) => this.AttachEntity(childEntity, parentEntityId, (object) null);

    /// <summary>附加子实体。</summary>
    /// <param name="childEntity">要附加的子实体。</param>
    /// <param name="parentEntityId">被附加的父实体的实体编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void AttachEntity(IEntity childEntity, int parentEntityId, object userData)
    {
      if (childEntity == null)
        throw new GameFrameworkException("Child entity is invalid.");
      this.AttachEntity(childEntity.Id, parentEntityId, userData);
    }

    /// <summary>附加子实体。</summary>
    /// <param name="childEntity">要附加的子实体。</param>
    /// <param name="parentEntity">被附加的父实体。</param>
    public void AttachEntity(IEntity childEntity, IEntity parentEntity) => this.AttachEntity(childEntity, parentEntity, (object) null);

    /// <summary>附加子实体。</summary>
    /// <param name="childEntity">要附加的子实体。</param>
    /// <param name="parentEntity">被附加的父实体。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void AttachEntity(IEntity childEntity, IEntity parentEntity, object userData)
    {
      if (childEntity == null)
        throw new GameFrameworkException("Child entity is invalid.");
      if (parentEntity == null)
        throw new GameFrameworkException("Parent entity is invalid.");
      this.AttachEntity(childEntity.Id, parentEntity.Id, userData);
    }

    /// <summary>解除子实体。</summary>
    /// <param name="childEntityId">要解除的子实体的实体编号。</param>
    public void DetachEntity(int childEntityId) => this.DetachEntity(childEntityId, (object) null);

    /// <summary>解除子实体。</summary>
    /// <param name="childEntityId">要解除的子实体的实体编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void DetachEntity(int childEntityId, object userData)
    {
      EntityManager.EntityInfo entityInfo1 = this.GetEntityInfo(childEntityId);
      IEntity parentEntity = entityInfo1 != null ? entityInfo1.ParentEntity : throw new GameFrameworkException(Utility.Text.Format<int>("Can not find child entity '{0}'.", childEntityId));
      if (parentEntity == null)
        return;
      EntityManager.EntityInfo entityInfo2 = this.GetEntityInfo(parentEntity.Id);
      if (entityInfo2 == null)
        throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntity.Id));
      IEntity entity = entityInfo1.Entity;
      entityInfo1.ParentEntity = (IEntity) null;
      entityInfo2.RemoveChildEntity(entity);
      parentEntity.OnDetached(entity, userData);
      entity.OnDetachFrom(parentEntity, userData);
    }

    /// <summary>解除子实体。</summary>
    /// <param name="childEntity">要解除的子实体。</param>
    public void DetachEntity(IEntity childEntity) => this.DetachEntity(childEntity, (object) null);

    /// <summary>解除子实体。</summary>
    /// <param name="childEntity">要解除的子实体。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void DetachEntity(IEntity childEntity, object userData)
    {
      if (childEntity == null)
        throw new GameFrameworkException("Child entity is invalid.");
      this.DetachEntity(childEntity.Id, userData);
    }

    /// <summary>解除所有子实体。</summary>
    /// <param name="parentEntityId">被解除的父实体的实体编号。</param>
    public void DetachChildEntities(int parentEntityId) => this.DetachChildEntities(parentEntityId, (object) null);

    /// <summary>解除所有子实体。</summary>
    /// <param name="parentEntityId">被解除的父实体的实体编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void DetachChildEntities(int parentEntityId, object userData)
    {
      EntityManager.EntityInfo entityInfo = this.GetEntityInfo(parentEntityId);
      if (entityInfo == null)
        throw new GameFrameworkException(Utility.Text.Format<int>("Can not find parent entity '{0}'.", parentEntityId));
      while (entityInfo.ChildEntityCount > 0)
        this.DetachEntity(entityInfo.GetChildEntity().Id, userData);
    }

    /// <summary>解除所有子实体。</summary>
    /// <param name="parentEntity">被解除的父实体。</param>
    public void DetachChildEntities(IEntity parentEntity) => this.DetachChildEntities(parentEntity, (object) null);

    /// <summary>解除所有子实体。</summary>
    /// <param name="parentEntity">被解除的父实体。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void DetachChildEntities(IEntity parentEntity, object userData)
    {
      if (parentEntity == null)
        throw new GameFrameworkException("Parent entity is invalid.");
      this.DetachChildEntities(parentEntity.Id, userData);
    }

    /// <summary>获取实体信息。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <returns>实体信息。</returns>
    private EntityManager.EntityInfo GetEntityInfo(int entityId)
    {
      EntityManager.EntityInfo entityInfo = (EntityManager.EntityInfo) null;
      return this.m_EntityInfos.TryGetValue(entityId, out entityInfo) ? entityInfo : (EntityManager.EntityInfo) null;
    }

    private void InternalShowEntity(
      int entityId,
      string entityAssetName,
      EntityManager.EntityGroup entityGroup,
      object entityInstance,
      bool isNewInstance,
      float duration,
      object userData)
    {
      try
      {
        IEntity entity = this.m_EntityHelper.CreateEntity(entityInstance, (IEntityGroup) entityGroup, userData);
        EntityManager.EntityInfo entityInfo = entity != null ? EntityManager.EntityInfo.Create(entity) : throw new GameFrameworkException("Can not create entity in entity helper.");
        this.m_EntityInfos.Add(entityId, entityInfo);
        entityInfo.Status = EntityManager.EntityStatus.WillInit;
        entity.OnInit(entityId, entityAssetName, (IEntityGroup) entityGroup, isNewInstance, userData);
        entityInfo.Status = EntityManager.EntityStatus.Inited;
        entityGroup.AddEntity(entity);
        entityInfo.Status = EntityManager.EntityStatus.WillShow;
        entity.OnShow(userData);
        entityInfo.Status = EntityManager.EntityStatus.Showed;
        if (this.m_ShowEntitySuccessEventHandler == null)
          return;
        ShowEntitySuccessEventArgs e = ShowEntitySuccessEventArgs.Create(entity, duration, userData);
        this.m_ShowEntitySuccessEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      catch (Exception ex)
      {
        if (this.m_ShowEntityFailureEventHandler != null)
        {
          ShowEntityFailureEventArgs e = ShowEntityFailureEventArgs.Create(entityId, entityAssetName, entityGroup.Name, ex.ToString(), userData);
          this.m_ShowEntityFailureEventHandler((object) this, e);
          ReferencePool.Release((IReference) e);
        }
        else
          throw;
      }
    }

    private void InternalHideEntity(EntityManager.EntityInfo entityInfo, object userData)
    {
      while (entityInfo.ChildEntityCount > 0)
        this.HideEntity(entityInfo.GetChildEntity().Id, userData);
      if (entityInfo.Status == EntityManager.EntityStatus.Hidden)
        return;
      IEntity entity = entityInfo.Entity;
      this.DetachEntity(entity.Id, userData);
      entityInfo.Status = EntityManager.EntityStatus.WillHide;
      entity.OnHide(this.m_IsShutdown, userData);
      entityInfo.Status = EntityManager.EntityStatus.Hidden;
      EntityManager.EntityGroup entityGroup = (EntityManager.EntityGroup) entity.EntityGroup;
      if (entityGroup == null)
        throw new GameFrameworkException("Entity group is invalid.");
      entityGroup.RemoveEntity(entity);
      if (!this.m_EntityInfos.Remove(entity.Id))
        throw new GameFrameworkException("Entity info is unmanaged.");
      if (this.m_HideEntityCompleteEventHandler != null)
      {
        HideEntityCompleteEventArgs e = HideEntityCompleteEventArgs.Create(entity.Id, entity.EntityAssetName, (IEntityGroup) entityGroup, userData);
        this.m_HideEntityCompleteEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      this.m_RecycleQueue.Enqueue(entityInfo);
    }

    private void LoadAssetSuccessCallback(
      string entityAssetName,
      object entityAsset,
      float duration,
      object userData)
    {
      EntityManager.ShowEntityInfo showEntityInfo = (EntityManager.ShowEntityInfo) userData;
      if (showEntityInfo == null)
        throw new GameFrameworkException("Show entity info is invalid.");
      if (this.m_EntitiesToReleaseOnLoad.Contains(showEntityInfo.SerialId))
      {
        this.m_EntitiesToReleaseOnLoad.Remove(showEntityInfo.SerialId);
        ReferencePool.Release((IReference) showEntityInfo);
        this.m_EntityHelper.ReleaseEntity(entityAsset, (object) null);
      }
      else
      {
        this.m_EntitiesBeingLoaded.Remove(showEntityInfo.EntityId);
        EntityManager.EntityInstanceObject entityInstanceObject = EntityManager.EntityInstanceObject.Create(entityAssetName, entityAsset, this.m_EntityHelper.InstantiateEntity(entityAsset), this.m_EntityHelper);
        showEntityInfo.EntityGroup.RegisterEntityInstanceObject(entityInstanceObject, true);
        this.InternalShowEntity(showEntityInfo.EntityId, entityAssetName, showEntityInfo.EntityGroup, entityInstanceObject.Target, true, duration, showEntityInfo.UserData);
        ReferencePool.Release((IReference) showEntityInfo);
      }
    }

    private void LoadAssetFailureCallback(
      string entityAssetName,
      LoadResourceStatus status,
      string errorMessage,
      object userData)
    {
      EntityManager.ShowEntityInfo showEntityInfo = (EntityManager.ShowEntityInfo) userData;
      if (showEntityInfo == null)
        throw new GameFrameworkException("Show entity info is invalid.");
      if (this.m_EntitiesToReleaseOnLoad.Contains(showEntityInfo.SerialId))
      {
        this.m_EntitiesToReleaseOnLoad.Remove(showEntityInfo.SerialId);
      }
      else
      {
        this.m_EntitiesBeingLoaded.Remove(showEntityInfo.EntityId);
        string str = Utility.Text.Format<string, LoadResourceStatus, string>("Load entity failure, asset name '{0}', status '{1}', error message '{2}'.", entityAssetName, status, errorMessage);
        if (this.m_ShowEntityFailureEventHandler == null)
          throw new GameFrameworkException(str);
        ShowEntityFailureEventArgs e = ShowEntityFailureEventArgs.Create(showEntityInfo.EntityId, entityAssetName, showEntityInfo.EntityGroup.Name, str, showEntityInfo.UserData);
        this.m_ShowEntityFailureEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void LoadAssetUpdateCallback(string entityAssetName, float progress, object userData)
    {
      EntityManager.ShowEntityInfo showEntityInfo = (EntityManager.ShowEntityInfo) userData;
      if (showEntityInfo == null)
        throw new GameFrameworkException("Show entity info is invalid.");
      if (this.m_ShowEntityUpdateEventHandler == null)
        return;
      ShowEntityUpdateEventArgs e = ShowEntityUpdateEventArgs.Create(showEntityInfo.EntityId, entityAssetName, showEntityInfo.EntityGroup.Name, progress, showEntityInfo.UserData);
      this.m_ShowEntityUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadAssetDependencyAssetCallback(
      string entityAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      EntityManager.ShowEntityInfo showEntityInfo = (EntityManager.ShowEntityInfo) userData;
      if (showEntityInfo == null)
        throw new GameFrameworkException("Show entity info is invalid.");
      if (this.m_ShowEntityDependencyAssetEventHandler == null)
        return;
      ShowEntityDependencyAssetEventArgs e = ShowEntityDependencyAssetEventArgs.Create(showEntityInfo.EntityId, entityAssetName, showEntityInfo.EntityGroup.Name, dependencyAssetName, loadedCount, totalCount, showEntityInfo.UserData);
      this.m_ShowEntityDependencyAssetEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    /// <summary>实体组。</summary>
    private sealed class EntityGroup : IEntityGroup
    {
      private readonly string m_Name;
      private readonly IEntityGroupHelper m_EntityGroupHelper;
      private readonly IObjectPool<EntityManager.EntityInstanceObject> m_InstancePool;
      private readonly GameFrameworkLinkedList<IEntity> m_Entities;
      private LinkedListNode<IEntity> m_CachedNode;

      /// <summary>初始化实体组的新实例。</summary>
      /// <param name="name">实体组名称。</param>
      /// <param name="instanceAutoReleaseInterval">实体实例对象池自动释放可释放对象的间隔秒数。</param>
      /// <param name="instanceCapacity">实体实例对象池容量。</param>
      /// <param name="instanceExpireTime">实体实例对象池对象过期秒数。</param>
      /// <param name="instancePriority">实体实例对象池的优先级。</param>
      /// <param name="entityGroupHelper">实体组辅助器。</param>
      /// <param name="objectPoolManager">对象池管理器。</param>
      public EntityGroup(
        string name,
        float instanceAutoReleaseInterval,
        int instanceCapacity,
        float instanceExpireTime,
        int instancePriority,
        IEntityGroupHelper entityGroupHelper,
        IObjectPoolManager objectPoolManager)
      {
        if (string.IsNullOrEmpty(name))
          throw new GameFrameworkException("Entity group name is invalid.");
        if (entityGroupHelper == null)
          throw new GameFrameworkException("Entity group helper is invalid.");
        this.m_Name = name;
        this.m_EntityGroupHelper = entityGroupHelper;
        this.m_InstancePool = objectPoolManager.CreateSingleSpawnObjectPool<EntityManager.EntityInstanceObject>(Utility.Text.Format<string>("Entity Instance Pool ({0})", name), instanceCapacity, instanceExpireTime, instancePriority);
        this.m_InstancePool.AutoReleaseInterval = instanceAutoReleaseInterval;
        this.m_Entities = new GameFrameworkLinkedList<IEntity>();
        this.m_CachedNode = (LinkedListNode<IEntity>) null;
      }

      /// <summary>获取实体组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取实体组中实体数量。</summary>
      public int EntityCount => this.m_Entities.Count;

      /// <summary>获取或设置实体组实例对象池自动释放可释放对象的间隔秒数。</summary>
      public float InstanceAutoReleaseInterval
      {
        get => this.m_InstancePool.AutoReleaseInterval;
        set => this.m_InstancePool.AutoReleaseInterval = value;
      }

      /// <summary>获取或设置实体组实例对象池的容量。</summary>
      public int InstanceCapacity
      {
        get => this.m_InstancePool.Capacity;
        set => this.m_InstancePool.Capacity = value;
      }

      /// <summary>获取或设置实体组实例对象池对象过期秒数。</summary>
      public float InstanceExpireTime
      {
        get => this.m_InstancePool.ExpireTime;
        set => this.m_InstancePool.ExpireTime = value;
      }

      /// <summary>获取或设置实体组实例对象池的优先级。</summary>
      public int InstancePriority
      {
        get => this.m_InstancePool.Priority;
        set => this.m_InstancePool.Priority = value;
      }

      /// <summary>获取实体组辅助器。</summary>
      public IEntityGroupHelper Helper => this.m_EntityGroupHelper;

      /// <summary>实体组轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        LinkedListNode<IEntity> linkedListNode = this.m_Entities.First;
        while (linkedListNode != null)
        {
          this.m_CachedNode = linkedListNode.Next;
          linkedListNode.Value.OnUpdate(elapseSeconds, realElapseSeconds);
          linkedListNode = this.m_CachedNode;
          this.m_CachedNode = (LinkedListNode<IEntity>) null;
        }
      }

      /// <summary>实体组中是否存在实体。</summary>
      /// <param name="entityId">实体序列编号。</param>
      /// <returns>实体组中是否存在实体。</returns>
      public bool HasEntity(int entityId)
      {
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.Id == entityId)
            return true;
        }
        return false;
      }

      /// <summary>实体组中是否存在实体。</summary>
      /// <param name="entityAssetName">实体资源名称。</param>
      /// <returns>实体组中是否存在实体。</returns>
      public bool HasEntity(string entityAssetName)
      {
        if (string.IsNullOrEmpty(entityAssetName))
          throw new GameFrameworkException("Entity asset name is invalid.");
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.EntityAssetName == entityAssetName)
            return true;
        }
        return false;
      }

      /// <summary>从实体组中获取实体。</summary>
      /// <param name="entityId">实体序列编号。</param>
      /// <returns>要获取的实体。</returns>
      public IEntity GetEntity(int entityId)
      {
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.Id == entityId)
            return entity;
        }
        return (IEntity) null;
      }

      /// <summary>从实体组中获取实体。</summary>
      /// <param name="entityAssetName">实体资源名称。</param>
      /// <returns>要获取的实体。</returns>
      public IEntity GetEntity(string entityAssetName)
      {
        if (string.IsNullOrEmpty(entityAssetName))
          throw new GameFrameworkException("Entity asset name is invalid.");
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.EntityAssetName == entityAssetName)
            return entity;
        }
        return (IEntity) null;
      }

      /// <summary>从实体组中获取实体。</summary>
      /// <param name="entityAssetName">实体资源名称。</param>
      /// <returns>要获取的实体。</returns>
      public IEntity[] GetEntities(string entityAssetName)
      {
        if (string.IsNullOrEmpty(entityAssetName))
          throw new GameFrameworkException("Entity asset name is invalid.");
        List<IEntity> entityList = new List<IEntity>();
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.EntityAssetName == entityAssetName)
            entityList.Add(entity);
        }
        return entityList.ToArray();
      }

      /// <summary>从实体组中获取实体。</summary>
      /// <param name="entityAssetName">实体资源名称。</param>
      /// <param name="results">要获取的实体。</param>
      public void GetEntities(string entityAssetName, List<IEntity> results)
      {
        if (string.IsNullOrEmpty(entityAssetName))
          throw new GameFrameworkException("Entity asset name is invalid.");
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (IEntity entity in this.m_Entities)
        {
          if (entity.EntityAssetName == entityAssetName)
            results.Add(entity);
        }
      }

      /// <summary>从实体组中获取所有实体。</summary>
      /// <returns>实体组中的所有实体。</returns>
      public IEntity[] GetAllEntities()
      {
        List<IEntity> entityList = new List<IEntity>();
        foreach (IEntity entity in this.m_Entities)
          entityList.Add(entity);
        return entityList.ToArray();
      }

      /// <summary>从实体组中获取所有实体。</summary>
      /// <param name="results">实体组中的所有实体。</param>
      public void GetAllEntities(List<IEntity> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (IEntity entity in this.m_Entities)
          results.Add(entity);
      }

      /// <summary>往实体组增加实体。</summary>
      /// <param name="entity">要增加的实体。</param>
      public void AddEntity(IEntity entity) => this.m_Entities.AddLast(entity);

      /// <summary>从实体组移除实体。</summary>
      /// <param name="entity">要移除的实体。</param>
      public void RemoveEntity(IEntity entity)
      {
        if (this.m_CachedNode != null && this.m_CachedNode.Value == entity)
          this.m_CachedNode = this.m_CachedNode.Next;
        if (!this.m_Entities.Remove(entity))
          throw new GameFrameworkException(Utility.Text.Format<string, int, string>("Entity group '{0}' not exists specified entity '[{1}]{2}'.", this.m_Name, entity.Id, entity.EntityAssetName));
      }

      public void RegisterEntityInstanceObject(EntityManager.EntityInstanceObject obj, bool spawned) => this.m_InstancePool.Register(obj, spawned);

      public EntityManager.EntityInstanceObject SpawnEntityInstanceObject(string name) => this.m_InstancePool.Spawn(name);

      public void UnspawnEntity(IEntity entity) => this.m_InstancePool.Unspawn(entity.Handle);

      public void SetEntityInstanceLocked(object entityInstance, bool locked)
      {
        if (entityInstance == null)
          throw new GameFrameworkException("Entity instance is invalid.");
        this.m_InstancePool.SetLocked(entityInstance, locked);
      }

      public void SetEntityInstancePriority(object entityInstance, int priority)
      {
        if (entityInstance == null)
          throw new GameFrameworkException("Entity instance is invalid.");
        this.m_InstancePool.SetPriority(entityInstance, priority);
      }
    }

    /// <summary>实体信息。</summary>
    private sealed class EntityInfo : IReference
    {
      private IEntity m_Entity;
      private EntityManager.EntityStatus m_Status;
      private IEntity m_ParentEntity;
      private List<IEntity> m_ChildEntities;

      public EntityInfo()
      {
        this.m_Entity = (IEntity) null;
        this.m_Status = EntityManager.EntityStatus.Unknown;
        this.m_ParentEntity = (IEntity) null;
        this.m_ChildEntities = new List<IEntity>();
      }

      public IEntity Entity => this.m_Entity;

      public EntityManager.EntityStatus Status
      {
        get => this.m_Status;
        set => this.m_Status = value;
      }

      public IEntity ParentEntity
      {
        get => this.m_ParentEntity;
        set => this.m_ParentEntity = value;
      }

      public int ChildEntityCount => this.m_ChildEntities.Count;

      public static EntityManager.EntityInfo Create(IEntity entity)
      {
        if (entity == null)
          throw new GameFrameworkException("Entity is invalid.");
        EntityManager.EntityInfo entityInfo = ReferencePool.Acquire<EntityManager.EntityInfo>();
        entityInfo.m_Entity = entity;
        entityInfo.m_Status = EntityManager.EntityStatus.WillInit;
        return entityInfo;
      }

      public void Clear()
      {
        this.m_Entity = (IEntity) null;
        this.m_Status = EntityManager.EntityStatus.Unknown;
        this.m_ParentEntity = (IEntity) null;
        this.m_ChildEntities.Clear();
      }

      public IEntity GetChildEntity() => this.m_ChildEntities.Count <= 0 ? (IEntity) null : this.m_ChildEntities[0];

      public IEntity[] GetChildEntities() => this.m_ChildEntities.ToArray();

      public void GetChildEntities(List<IEntity> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (IEntity childEntity in this.m_ChildEntities)
          results.Add(childEntity);
      }

      public void AddChildEntity(IEntity childEntity)
      {
        if (this.m_ChildEntities.Contains(childEntity))
          throw new GameFrameworkException("Can not add child entity which is already exist.");
        this.m_ChildEntities.Add(childEntity);
      }

      public void RemoveChildEntity(IEntity childEntity)
      {
        if (!this.m_ChildEntities.Remove(childEntity))
          throw new GameFrameworkException("Can not remove child entity which is not exist.");
      }
    }

    /// <summary>实体实例对象。</summary>
    private sealed class EntityInstanceObject : ObjectBase
    {
      private object m_EntityAsset;
      private IEntityHelper m_EntityHelper;

      public EntityInstanceObject()
      {
        this.m_EntityAsset = (object) null;
        this.m_EntityHelper = (IEntityHelper) null;
      }

      public static EntityManager.EntityInstanceObject Create(
        string name,
        object entityAsset,
        object entityInstance,
        IEntityHelper entityHelper)
      {
        if (entityAsset == null)
          throw new GameFrameworkException("Entity asset is invalid.");
        if (entityHelper == null)
          throw new GameFrameworkException("Entity helper is invalid.");
        EntityManager.EntityInstanceObject entityInstanceObject = ReferencePool.Acquire<EntityManager.EntityInstanceObject>();
        entityInstanceObject.Initialize(name, entityInstance);
        entityInstanceObject.m_EntityAsset = entityAsset;
        entityInstanceObject.m_EntityHelper = entityHelper;
        return entityInstanceObject;
      }

      public override void Clear()
      {
        base.Clear();
        this.m_EntityAsset = (object) null;
        this.m_EntityHelper = (IEntityHelper) null;
      }

      protected internal override void Release(bool isShutdown) => this.m_EntityHelper.ReleaseEntity(this.m_EntityAsset, this.Target);
    }

    /// <summary>实体状态。</summary>
    private enum EntityStatus : byte
    {
      Unknown,
      WillInit,
      Inited,
      WillShow,
      Showed,
      WillHide,
      Hidden,
      WillRecycle,
      Recycled,
    }

    private sealed class ShowEntityInfo : IReference
    {
      private int m_SerialId;
      private int m_EntityId;
      private EntityManager.EntityGroup m_EntityGroup;
      private object m_UserData;

      public ShowEntityInfo()
      {
        this.m_SerialId = 0;
        this.m_EntityId = 0;
        this.m_EntityGroup = (EntityManager.EntityGroup) null;
        this.m_UserData = (object) null;
      }

      public int SerialId => this.m_SerialId;

      public int EntityId => this.m_EntityId;

      public EntityManager.EntityGroup EntityGroup => this.m_EntityGroup;

      public object UserData => this.m_UserData;

      public static EntityManager.ShowEntityInfo Create(
        int serialId,
        int entityId,
        EntityManager.EntityGroup entityGroup,
        object userData)
      {
        EntityManager.ShowEntityInfo showEntityInfo = ReferencePool.Acquire<EntityManager.ShowEntityInfo>();
        showEntityInfo.m_SerialId = serialId;
        showEntityInfo.m_EntityId = entityId;
        showEntityInfo.m_EntityGroup = entityGroup;
        showEntityInfo.m_UserData = userData;
        return showEntityInfo;
      }

      public void Clear()
      {
        this.m_SerialId = 0;
        this.m_EntityId = 0;
        this.m_EntityGroup = (EntityManager.EntityGroup) null;
        this.m_UserData = (object) null;
      }
    }
  }
}
