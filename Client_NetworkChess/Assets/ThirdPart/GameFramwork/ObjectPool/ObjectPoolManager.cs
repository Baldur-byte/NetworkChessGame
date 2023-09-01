// Decompiled with JetBrains decompiler
// Type: GameFramework.ObjectPool.ObjectPoolManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.ObjectPool
{
  /// <summary>对象池管理器。</summary>
  internal sealed class ObjectPoolManager : GameFrameworkModule, IObjectPoolManager
  {
    private const int DefaultCapacity = 2147483647;
    private const float DefaultExpireTime = 3.40282347E+38f;
    private const int DefaultPriority = 0;
    private readonly Dictionary<TypeNamePair, ObjectPoolBase> m_ObjectPools;
    private readonly List<ObjectPoolBase> m_CachedAllObjectPools;
    private readonly Comparison<ObjectPoolBase> m_ObjectPoolComparer;

    /// <summary>初始化对象池管理器的新实例。</summary>
    public ObjectPoolManager()
    {
      this.m_ObjectPools = new Dictionary<TypeNamePair, ObjectPoolBase>();
      this.m_CachedAllObjectPools = new List<ObjectPoolBase>();
      this.m_ObjectPoolComparer = new Comparison<ObjectPoolBase>(ObjectPoolManager.ObjectPoolComparer);
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 6;

    /// <summary>获取对象池数量。</summary>
    public int Count => this.m_ObjectPools.Count;

    /// <summary>对象池管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
        objectPool.Value.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理对象池管理器。</summary>
    internal override void Shutdown()
    {
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
        objectPool.Value.Shutdown();
      this.m_ObjectPools.Clear();
      this.m_CachedAllObjectPools.Clear();
    }

    /// <summary>检查是否存在对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <returns>是否存在对象池。</returns>
    public bool HasObjectPool<T>() where T : ObjectBase => this.InternalHasObjectPool(new TypeNamePair(typeof (T)));

    /// <summary>检查是否存在对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <returns>是否存在对象池。</returns>
    public bool HasObjectPool(Type objectType)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalHasObjectPool(new TypeNamePair(objectType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>检查是否存在对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <returns>是否存在对象池。</returns>
    public bool HasObjectPool<T>(string name) where T : ObjectBase => this.InternalHasObjectPool(new TypeNamePair(typeof (T), name));

    /// <summary>检查是否存在对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <returns>是否存在对象池。</returns>
    public bool HasObjectPool(Type objectType, string name)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalHasObjectPool(new TypeNamePair(objectType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>检查是否存在对象池。</summary>
    /// <param name="condition">要检查的条件。</param>
    /// <returns>是否存在对象池。</returns>
    public bool HasObjectPool(Predicate<ObjectPoolBase> condition)
    {
      if (condition == null)
        throw new GameFrameworkException("Condition is invalid.");
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
      {
        if (condition(objectPool.Value))
          return true;
      }
      return false;
    }

    /// <summary>获取对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <returns>要获取的对象池。</returns>
    public IObjectPool<T> GetObjectPool<T>() where T : ObjectBase => (IObjectPool<T>) this.InternalGetObjectPool(new TypeNamePair(typeof (T)));

    /// <summary>获取对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <returns>要获取的对象池。</returns>
    public ObjectPoolBase GetObjectPool(Type objectType)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalGetObjectPool(new TypeNamePair(objectType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>获取对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <returns>要获取的对象池。</returns>
    public IObjectPool<T> GetObjectPool<T>(string name) where T : ObjectBase => (IObjectPool<T>) this.InternalGetObjectPool(new TypeNamePair(typeof (T), name));

    /// <summary>获取对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <returns>要获取的对象池。</returns>
    public ObjectPoolBase GetObjectPool(Type objectType, string name)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalGetObjectPool(new TypeNamePair(objectType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>获取对象池。</summary>
    /// <param name="condition">要检查的条件。</param>
    /// <returns>要获取的对象池。</returns>
    public ObjectPoolBase GetObjectPool(Predicate<ObjectPoolBase> condition)
    {
      if (condition == null)
        throw new GameFrameworkException("Condition is invalid.");
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
      {
        if (condition(objectPool.Value))
          return objectPool.Value;
      }
      return (ObjectPoolBase) null;
    }

    /// <summary>获取对象池。</summary>
    /// <param name="condition">要检查的条件。</param>
    /// <returns>要获取的对象池。</returns>
    public ObjectPoolBase[] GetObjectPools(Predicate<ObjectPoolBase> condition)
    {
      if (condition == null)
        throw new GameFrameworkException("Condition is invalid.");
      List<ObjectPoolBase> objectPoolBaseList = new List<ObjectPoolBase>();
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
      {
        if (condition(objectPool.Value))
          objectPoolBaseList.Add(objectPool.Value);
      }
      return objectPoolBaseList.ToArray();
    }

    /// <summary>获取对象池。</summary>
    /// <param name="condition">要检查的条件。</param>
    /// <param name="results">要获取的对象池。</param>
    public void GetObjectPools(Predicate<ObjectPoolBase> condition, List<ObjectPoolBase> results)
    {
      if (condition == null)
        throw new GameFrameworkException("Condition is invalid.");
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
      {
        if (condition(objectPool.Value))
          results.Add(objectPool.Value);
      }
    }

    /// <summary>获取所有对象池。</summary>
    /// <returns>所有对象池。</returns>
    public ObjectPoolBase[] GetAllObjectPools() => this.GetAllObjectPools(false);

    /// <summary>获取所有对象池。</summary>
    /// <param name="results">所有对象池。</param>
    public void GetAllObjectPools(List<ObjectPoolBase> results) => this.GetAllObjectPools(false, results);

    /// <summary>获取所有对象池。</summary>
    /// <param name="sort">是否根据对象池的优先级排序。</param>
    /// <returns>所有对象池。</returns>
    public ObjectPoolBase[] GetAllObjectPools(bool sort)
    {
      if (sort)
      {
        List<ObjectPoolBase> objectPoolBaseList = new List<ObjectPoolBase>();
        foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
          objectPoolBaseList.Add(objectPool.Value);
        objectPoolBaseList.Sort(this.m_ObjectPoolComparer);
        return objectPoolBaseList.ToArray();
      }
      int num = 0;
      ObjectPoolBase[] allObjectPools = new ObjectPoolBase[this.m_ObjectPools.Count];
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
        allObjectPools[num++] = objectPool.Value;
      return allObjectPools;
    }

    /// <summary>获取所有对象池。</summary>
    /// <param name="sort">是否根据对象池的优先级排序。</param>
    /// <param name="results">所有对象池。</param>
    public void GetAllObjectPools(bool sort, List<ObjectPoolBase> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<TypeNamePair, ObjectPoolBase> objectPool in this.m_ObjectPools)
        results.Add(objectPool.Value);
      if (!sort)
        return;
      results.Sort(this.m_ObjectPoolComparer);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>() where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType) => this.InternalCreateObjectPool(objectType, string.Empty, false, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, false, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name) => this.InternalCreateObjectPool(objectType, name, false, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity) => this.InternalCreateObjectPool(objectType, string.Empty, false, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime) => this.InternalCreateObjectPool(objectType, string.Empty, false, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, false, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity) => this.InternalCreateObjectPool(objectType, name, false, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, false, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, name, false, expireTime, int.MaxValue, expireTime, 0);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, expireTime, capacity, expireTime, 0);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      int capacity,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, int priority) => this.InternalCreateObjectPool(objectType, string.Empty, false, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, false, expireTime, int.MaxValue, expireTime, priority);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, false, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(
      string name,
      int capacity,
      float expireTime)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, false, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, false, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, false, float.MaxValue, capacity, float.MaxValue, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(
      string name,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, false, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, false, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(string.Empty, false, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, false, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(
      string name,
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, false, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, false, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public IObjectPool<T> CreateSingleSpawnObjectPool<T>(
      string name,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, false, autoReleaseInterval, capacity, expireTime, priority);
    }

    /// <summary>创建允许单次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许单次获取的对象池。</returns>
    public ObjectPoolBase CreateSingleSpawnObjectPool(
      Type objectType,
      string name,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, false, autoReleaseInterval, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>() where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType) => this.InternalCreateObjectPool(objectType, string.Empty, true, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, true, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name) => this.InternalCreateObjectPool(objectType, name, true, float.MaxValue, int.MaxValue, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity) => this.InternalCreateObjectPool(objectType, string.Empty, true, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime) => this.InternalCreateObjectPool(objectType, string.Empty, true, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, true, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity) => this.InternalCreateObjectPool(objectType, name, true, float.MaxValue, capacity, float.MaxValue, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, true, expireTime, int.MaxValue, expireTime, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, name, true, expireTime, int.MaxValue, expireTime, 0);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, expireTime, capacity, expireTime, 0);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      int capacity,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, int priority) => this.InternalCreateObjectPool(objectType, string.Empty, true, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(string.Empty, true, expireTime, int.MaxValue, expireTime, priority);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, true, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(
      string name,
      int capacity,
      float expireTime)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, true, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      float expireTime)
    {
      return this.InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, 0);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase => this.InternalCreateObjectPool<T>(name, true, float.MaxValue, capacity, float.MaxValue, priority);

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, true, float.MaxValue, capacity, float.MaxValue, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(
      string name,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, true, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, true, expireTime, int.MaxValue, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(string.Empty, true, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, string.Empty, true, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(
      string name,
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, true, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, true, expireTime, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">对象池名称。</param>
    /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public IObjectPool<T> CreateMultiSpawnObjectPool<T>(
      string name,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      return this.InternalCreateObjectPool<T>(name, true, autoReleaseInterval, capacity, expireTime, priority);
    }

    /// <summary>创建允许多次获取的对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">对象池名称。</param>
    /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
    /// <param name="capacity">对象池的容量。</param>
    /// <param name="expireTime">对象池对象过期秒数。</param>
    /// <param name="priority">对象池的优先级。</param>
    /// <returns>要创建的允许多次获取的对象池。</returns>
    public ObjectPoolBase CreateMultiSpawnObjectPool(
      Type objectType,
      string name,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
    {
      return this.InternalCreateObjectPool(objectType, name, true, autoReleaseInterval, capacity, expireTime, priority);
    }

    /// <summary>销毁对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool<T>() where T : ObjectBase => this.InternalDestroyObjectPool(new TypeNamePair(typeof (T)));

    /// <summary>销毁对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool(Type objectType)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalDestroyObjectPool(new TypeNamePair(objectType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>销毁对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="name">要销毁的对象池名称。</param>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool<T>(string name) where T : ObjectBase => this.InternalDestroyObjectPool(new TypeNamePair(typeof (T), name));

    /// <summary>销毁对象池。</summary>
    /// <param name="objectType">对象类型。</param>
    /// <param name="name">要销毁的对象池名称。</param>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool(Type objectType, string name)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      return typeof (ObjectBase).IsAssignableFrom(objectType) ? this.InternalDestroyObjectPool(new TypeNamePair(objectType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
    }

    /// <summary>销毁对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <param name="objectPool">要销毁的对象池。</param>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool<T>(IObjectPool<T> objectPool) where T : ObjectBase => objectPool != null ? this.InternalDestroyObjectPool(new TypeNamePair(typeof (T), objectPool.Name)) : throw new GameFrameworkException("Object pool is invalid.");

    /// <summary>销毁对象池。</summary>
    /// <param name="objectPool">要销毁的对象池。</param>
    /// <returns>是否销毁对象池成功。</returns>
    public bool DestroyObjectPool(ObjectPoolBase objectPool)
    {
      if (objectPool == null)
        throw new GameFrameworkException("Object pool is invalid.");
      return this.InternalDestroyObjectPool(new TypeNamePair(objectPool.ObjectType, objectPool.Name));
    }

    /// <summary>释放对象池中的可释放对象。</summary>
    public void Release()
    {
      this.GetAllObjectPools(true, this.m_CachedAllObjectPools);
      foreach (ObjectPoolBase cachedAllObjectPool in this.m_CachedAllObjectPools)
        cachedAllObjectPool.Release();
    }

    /// <summary>释放对象池中的所有未使用对象。</summary>
    public void ReleaseAllUnused()
    {
      this.GetAllObjectPools(true, this.m_CachedAllObjectPools);
      foreach (ObjectPoolBase cachedAllObjectPool in this.m_CachedAllObjectPools)
        cachedAllObjectPool.ReleaseAllUnused();
    }

    private bool InternalHasObjectPool(TypeNamePair typeNamePair) => this.m_ObjectPools.ContainsKey(typeNamePair);

    private ObjectPoolBase InternalGetObjectPool(TypeNamePair typeNamePair)
    {
      ObjectPoolBase objectPoolBase = (ObjectPoolBase) null;
      return this.m_ObjectPools.TryGetValue(typeNamePair, out objectPoolBase) ? objectPoolBase : (ObjectPoolBase) null;
    }

    private IObjectPool<T> InternalCreateObjectPool<T>(
      string name,
      bool allowMultiSpawn,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
      where T : ObjectBase
    {
      TypeNamePair key = new TypeNamePair(typeof (T), name);
      if (this.HasObjectPool<T>(name))
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist object pool '{0}'.", key));
      ObjectPoolManager.ObjectPool<T> objectPool = new ObjectPoolManager.ObjectPool<T>(name, allowMultiSpawn, autoReleaseInterval, capacity, expireTime, priority);
      this.m_ObjectPools.Add(key, (ObjectPoolBase) objectPool);
      return (IObjectPool<T>) objectPool;
    }

    private ObjectPoolBase InternalCreateObjectPool(
      Type objectType,
      string name,
      bool allowMultiSpawn,
      float autoReleaseInterval,
      int capacity,
      float expireTime,
      int priority)
    {
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      TypeNamePair key = typeof (ObjectBase).IsAssignableFrom(objectType) ? new TypeNamePair(objectType, name) : throw new GameFrameworkException(Utility.Text.Format<string>("Object type '{0}' is invalid.", objectType.FullName));
      if (this.HasObjectPool(objectType, name))
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist object pool '{0}'.", key));
      ObjectPoolBase instance = (ObjectPoolBase) Activator.CreateInstance(typeof (ObjectPoolManager.ObjectPool<>).MakeGenericType(objectType), (object) name, (object) allowMultiSpawn, (object) autoReleaseInterval, (object) capacity, (object) expireTime, (object) priority);
      this.m_ObjectPools.Add(key, instance);
      return instance;
    }

    private bool InternalDestroyObjectPool(TypeNamePair typeNamePair)
    {
      ObjectPoolBase objectPoolBase = (ObjectPoolBase) null;
      if (!this.m_ObjectPools.TryGetValue(typeNamePair, out objectPoolBase))
        return false;
      objectPoolBase.Shutdown();
      return this.m_ObjectPools.Remove(typeNamePair);
    }

    private static int ObjectPoolComparer(ObjectPoolBase a, ObjectPoolBase b) => a.Priority.CompareTo(b.Priority);

    /// <summary>内部对象。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    private sealed class Object<T> : IReference where T : ObjectBase
    {
      private T m_Object;
      private int m_SpawnCount;

      /// <summary>初始化内部对象的新实例。</summary>
      public Object()
      {
        this.m_Object = default (T);
        this.m_SpawnCount = 0;
      }

      /// <summary>获取对象名称。</summary>
      public string Name => this.m_Object.Name;

      /// <summary>获取对象是否被加锁。</summary>
      public bool Locked
      {
        get => this.m_Object.Locked;
        internal set => this.m_Object.Locked = value;
      }

      /// <summary>获取对象的优先级。</summary>
      public int Priority
      {
        get => this.m_Object.Priority;
        internal set => this.m_Object.Priority = value;
      }

      /// <summary>获取自定义释放检查标记。</summary>
      public bool CustomCanReleaseFlag => this.m_Object.CustomCanReleaseFlag;

      /// <summary>获取对象上次使用时间。</summary>
      public DateTime LastUseTime => this.m_Object.LastUseTime;

      /// <summary>获取对象是否正在使用。</summary>
      public bool IsInUse => this.m_SpawnCount > 0;

      /// <summary>获取对象的获取计数。</summary>
      public int SpawnCount => this.m_SpawnCount;

      /// <summary>创建内部对象。</summary>
      /// <param name="obj">对象。</param>
      /// <param name="spawned">对象是否已被获取。</param>
      /// <returns>创建的内部对象。</returns>
      public static ObjectPoolManager.Object<T> Create(T obj, bool spawned)
      {
        if ((object) obj == null)
          throw new GameFrameworkException("Object is invalid.");
        ObjectPoolManager.Object<T> @object = ReferencePool.Acquire<ObjectPoolManager.Object<T>>();
        @object.m_Object = obj;
        @object.m_SpawnCount = spawned ? 1 : 0;
        if (!spawned)
          return @object;
        obj.OnSpawn();
        return @object;
      }

      /// <summary>清理内部对象。</summary>
      public void Clear()
      {
        this.m_Object = default (T);
        this.m_SpawnCount = 0;
      }

      /// <summary>查看对象。</summary>
      /// <returns>对象。</returns>
      public T Peek() => this.m_Object;

      /// <summary>获取对象。</summary>
      /// <returns>对象。</returns>
      public T Spawn()
      {
        ++this.m_SpawnCount;
        this.m_Object.LastUseTime = DateTime.UtcNow;
        this.m_Object.OnSpawn();
        return this.m_Object;
      }

      /// <summary>回收对象。</summary>
      public void Unspawn()
      {
        this.m_Object.OnUnspawn();
        this.m_Object.LastUseTime = DateTime.UtcNow;
        --this.m_SpawnCount;
        if (this.m_SpawnCount < 0)
          throw new GameFrameworkException(Utility.Text.Format<string>("Object '{0}' spawn count is less than 0.", this.Name));
      }

      /// <summary>释放对象。</summary>
      /// <param name="isShutdown">是否是关闭对象池时触发。</param>
      public void Release(bool isShutdown)
      {
        this.m_Object.Release(isShutdown);
        ReferencePool.Release((IReference) this.m_Object);
      }
    }

    /// <summary>对象池。</summary>
    /// <typeparam name="T">对象类型。</typeparam>
    private sealed class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
    {
      private readonly GameFrameworkMultiDictionary<string, ObjectPoolManager.Object<T>> m_Objects;
      private readonly Dictionary<object, ObjectPoolManager.Object<T>> m_ObjectMap;
      private readonly ReleaseObjectFilterCallback<T> m_DefaultReleaseObjectFilterCallback;
      private readonly List<T> m_CachedCanReleaseObjects;
      private readonly List<T> m_CachedToReleaseObjects;
      private readonly bool m_AllowMultiSpawn;
      private float m_AutoReleaseInterval;
      private int m_Capacity;
      private float m_ExpireTime;
      private int m_Priority;
      private float m_AutoReleaseTime;

      /// <summary>初始化对象池的新实例。</summary>
      /// <param name="name">对象池名称。</param>
      /// <param name="allowMultiSpawn">是否允许对象被多次获取。</param>
      /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。</param>
      /// <param name="capacity">对象池的容量。</param>
      /// <param name="expireTime">对象池对象过期秒数。</param>
      /// <param name="priority">对象池的优先级。</param>
      public ObjectPool(
        string name,
        bool allowMultiSpawn,
        float autoReleaseInterval,
        int capacity,
        float expireTime,
        int priority)
        : base(name)
      {
        this.m_Objects = new GameFrameworkMultiDictionary<string, ObjectPoolManager.Object<T>>();
        this.m_ObjectMap = new Dictionary<object, ObjectPoolManager.Object<T>>();
        this.m_DefaultReleaseObjectFilterCallback = new ReleaseObjectFilterCallback<T>(this.DefaultReleaseObjectFilterCallback);
        this.m_CachedCanReleaseObjects = new List<T>();
        this.m_CachedToReleaseObjects = new List<T>();
        this.m_AllowMultiSpawn = allowMultiSpawn;
        this.m_AutoReleaseInterval = autoReleaseInterval;
        this.Capacity = capacity;
        this.ExpireTime = expireTime;
        this.m_Priority = priority;
        this.m_AutoReleaseTime = 0.0f;
      }

      /// <summary>获取对象池对象类型。</summary>
      public override Type ObjectType => typeof (T);

      /// <summary>获取对象池中对象的数量。</summary>
      public override int Count => this.m_ObjectMap.Count;

      /// <summary>获取对象池中能被释放的对象的数量。</summary>
      public override int CanReleaseCount
      {
        get
        {
          this.GetCanReleaseObjects(this.m_CachedCanReleaseObjects);
          return this.m_CachedCanReleaseObjects.Count;
        }
      }

      /// <summary>获取是否允许对象被多次获取。</summary>
      public override bool AllowMultiSpawn => this.m_AllowMultiSpawn;

      /// <summary>获取或设置对象池自动释放可释放对象的间隔秒数。</summary>
      public override float AutoReleaseInterval
      {
        get => this.m_AutoReleaseInterval;
        set => this.m_AutoReleaseInterval = value;
      }

      /// <summary>获取或设置对象池的容量。</summary>
      public override int Capacity
      {
        get => this.m_Capacity;
        set
        {
          if (value < 0)
            throw new GameFrameworkException("Capacity is invalid.");
          if (this.m_Capacity == value)
            return;
          this.m_Capacity = value;
          this.Release();
        }
      }

      /// <summary>获取或设置对象池对象过期秒数。</summary>
      public override float ExpireTime
      {
        get => this.m_ExpireTime;
        set
        {
          if ((double) value < 0.0)
            throw new GameFrameworkException("ExpireTime is invalid.");
          if ((double) this.ExpireTime == (double) value)
            return;
          this.m_ExpireTime = value;
          this.Release();
        }
      }

      /// <summary>获取或设置对象池的优先级。</summary>
      public override int Priority
      {
        get => this.m_Priority;
        set => this.m_Priority = value;
      }

      /// <summary>创建对象。</summary>
      /// <param name="obj">对象。</param>
      /// <param name="spawned">对象是否已被获取。</param>
      public void Register(T obj, bool spawned)
      {
        ObjectPoolManager.Object<T> @object = (object) obj != null ? ObjectPoolManager.Object<T>.Create(obj, spawned) : throw new GameFrameworkException("Object is invalid.");
        this.m_Objects.Add(obj.Name, @object);
        this.m_ObjectMap.Add(obj.Target, @object);
        if (this.Count <= this.m_Capacity)
          return;
        this.Release();
      }

      /// <summary>检查对象。</summary>
      /// <returns>要检查的对象是否存在。</returns>
      public bool CanSpawn() => this.CanSpawn(string.Empty);

      /// <summary>检查对象。</summary>
      /// <param name="name">对象名称。</param>
      /// <returns>要检查的对象是否存在。</returns>
      public bool CanSpawn(string name)
      {
        if (name == null)
          throw new GameFrameworkException("Name is invalid.");
        GameFrameworkLinkedListRange<ObjectPoolManager.Object<T>> range = new GameFrameworkLinkedListRange<ObjectPoolManager.Object<T>>();
        if (this.m_Objects.TryGetValue(name, out range))
        {
          foreach (ObjectPoolManager.Object<T> @object in range)
          {
            if (this.m_AllowMultiSpawn || !@object.IsInUse)
              return true;
          }
        }
        return false;
      }

      /// <summary>获取对象。</summary>
      /// <returns>要获取的对象。</returns>
      public T Spawn() => this.Spawn(string.Empty);

      /// <summary>获取对象。</summary>
      /// <param name="name">对象名称。</param>
      /// <returns>要获取的对象。</returns>
      public T Spawn(string name)
      {
        if (name == null)
          throw new GameFrameworkException("Name is invalid.");
        GameFrameworkLinkedListRange<ObjectPoolManager.Object<T>> range = new GameFrameworkLinkedListRange<ObjectPoolManager.Object<T>>();
        if (this.m_Objects.TryGetValue(name, out range))
        {
          foreach (ObjectPoolManager.Object<T> @object in range)
          {
            if (this.m_AllowMultiSpawn || !@object.IsInUse)
              return @object.Spawn();
          }
        }
        return default (T);
      }

      /// <summary>回收对象。</summary>
      /// <param name="obj">要回收的对象。</param>
      public void Unspawn(T obj)
      {
        if ((object) obj == null)
          throw new GameFrameworkException("Object is invalid.");
        this.Unspawn(obj.Target);
      }

      /// <summary>回收对象。</summary>
      /// <param name="target">要回收的对象。</param>
      public void Unspawn(object target)
      {
        ObjectPoolManager.Object<T> @object = target != null ? this.GetObject(target) : throw new GameFrameworkException("Target is invalid.");
        if (@object == null)
          throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string, object>("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof (T), this.Name), target.GetType().FullName, target));
        @object.Unspawn();
        if (this.Count <= this.m_Capacity || @object.SpawnCount > 0)
          return;
        this.Release();
      }

      /// <summary>设置对象是否被加锁。</summary>
      /// <param name="obj">要设置被加锁的对象。</param>
      /// <param name="locked">是否被加锁。</param>
      public void SetLocked(T obj, bool locked)
      {
        if ((object) obj == null)
          throw new GameFrameworkException("Object is invalid.");
        this.SetLocked(obj.Target, locked);
      }

      /// <summary>设置对象是否被加锁。</summary>
      /// <param name="target">要设置被加锁的对象。</param>
      /// <param name="locked">是否被加锁。</param>
      public void SetLocked(object target, bool locked)
      {
        ObjectPoolManager.Object<T> @object = target != null ? this.GetObject(target) : throw new GameFrameworkException("Target is invalid.");
        if (@object == null)
          throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string, object>("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof (T), this.Name), target.GetType().FullName, target));
        @object.Locked = locked;
      }

      /// <summary>设置对象的优先级。</summary>
      /// <param name="obj">要设置优先级的对象。</param>
      /// <param name="priority">优先级。</param>
      public void SetPriority(T obj, int priority)
      {
        if ((object) obj == null)
          throw new GameFrameworkException("Object is invalid.");
        this.SetPriority(obj.Target, priority);
      }

      /// <summary>设置对象的优先级。</summary>
      /// <param name="target">要设置优先级的对象。</param>
      /// <param name="priority">优先级。</param>
      public void SetPriority(object target, int priority)
      {
        ObjectPoolManager.Object<T> @object = target != null ? this.GetObject(target) : throw new GameFrameworkException("Target is invalid.");
        if (@object == null)
          throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string, object>("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof (T), this.Name), target.GetType().FullName, target));
        @object.Priority = priority;
      }

      /// <summary>释放对象。</summary>
      /// <param name="obj">要释放的对象。</param>
      /// <returns>释放对象是否成功。</returns>
      public bool ReleaseObject(T obj) => (object) obj != null ? this.ReleaseObject(obj.Target) : throw new GameFrameworkException("Object is invalid.");

      /// <summary>释放对象。</summary>
      /// <param name="target">要释放的对象。</param>
      /// <returns>释放对象是否成功。</returns>
      public bool ReleaseObject(object target)
      {
        ObjectPoolManager.Object<T> @object = target != null ? this.GetObject(target) : throw new GameFrameworkException("Target is invalid.");
        if (@object == null || @object.IsInUse || @object.Locked || !@object.CustomCanReleaseFlag)
          return false;
        this.m_Objects.Remove(@object.Name, @object);
        this.m_ObjectMap.Remove(@object.Peek().Target);
        @object.Release(false);
        ReferencePool.Release((IReference) @object);
        return true;
      }

      /// <summary>释放对象池中的可释放对象。</summary>
      public override void Release() => this.Release(this.Count - this.m_Capacity, this.m_DefaultReleaseObjectFilterCallback);

      /// <summary>释放对象池中的可释放对象。</summary>
      /// <param name="toReleaseCount">尝试释放对象数量。</param>
      public override void Release(int toReleaseCount) => this.Release(toReleaseCount, this.m_DefaultReleaseObjectFilterCallback);

      /// <summary>释放对象池中的可释放对象。</summary>
      /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
      public void Release(
        ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
      {
        this.Release(this.Count - this.m_Capacity, releaseObjectFilterCallback);
      }

      /// <summary>释放对象池中的可释放对象。</summary>
      /// <param name="toReleaseCount">尝试释放对象数量。</param>
      /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
      public void Release(
        int toReleaseCount,
        ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
      {
        if (releaseObjectFilterCallback == null)
          throw new GameFrameworkException("Release object filter callback is invalid.");
        if (toReleaseCount < 0)
          toReleaseCount = 0;
        DateTime expireTime = DateTime.MinValue;
        if ((double) this.m_ExpireTime < 3.4028234663852886E+38)
          expireTime = DateTime.UtcNow.AddSeconds(-(double) this.m_ExpireTime);
        this.m_AutoReleaseTime = 0.0f;
        this.GetCanReleaseObjects(this.m_CachedCanReleaseObjects);
        List<T> objList = releaseObjectFilterCallback(this.m_CachedCanReleaseObjects, toReleaseCount, expireTime);
        if (objList == null || objList.Count <= 0)
          return;
        foreach (T obj in objList)
          this.ReleaseObject(obj);
      }

      /// <summary>释放对象池中的所有未使用对象。</summary>
      public override void ReleaseAllUnused()
      {
        this.m_AutoReleaseTime = 0.0f;
        this.GetCanReleaseObjects(this.m_CachedCanReleaseObjects);
        foreach (T canReleaseObject in this.m_CachedCanReleaseObjects)
          this.ReleaseObject(canReleaseObject);
      }

      /// <summary>获取所有对象信息。</summary>
      /// <returns>所有对象信息。</returns>
      public override ObjectInfo[] GetAllObjectInfos()
      {
        List<ObjectInfo> objectInfoList = new List<ObjectInfo>();
        foreach (KeyValuePair<string, GameFrameworkLinkedListRange<ObjectPoolManager.Object<T>>> keyValuePair in this.m_Objects)
        {
          foreach (ObjectPoolManager.Object<T> @object in keyValuePair.Value)
            objectInfoList.Add(new ObjectInfo(@object.Name, @object.Locked, @object.CustomCanReleaseFlag, @object.Priority, @object.LastUseTime, @object.SpawnCount));
        }
        return objectInfoList.ToArray();
      }

      internal override void Update(float elapseSeconds, float realElapseSeconds)
      {
        this.m_AutoReleaseTime += realElapseSeconds;
        if ((double) this.m_AutoReleaseTime < (double) this.m_AutoReleaseInterval)
          return;
        this.Release();
      }

      internal override void Shutdown()
      {
        foreach (KeyValuePair<object, ObjectPoolManager.Object<T>> keyValuePair in this.m_ObjectMap)
        {
          keyValuePair.Value.Release(true);
          ReferencePool.Release((IReference) keyValuePair.Value);
        }
        this.m_Objects.Clear();
        this.m_ObjectMap.Clear();
        this.m_CachedCanReleaseObjects.Clear();
        this.m_CachedToReleaseObjects.Clear();
      }

      private ObjectPoolManager.Object<T> GetObject(object target)
      {
        if (target == null)
          throw new GameFrameworkException("Target is invalid.");
        ObjectPoolManager.Object<T> @object = (ObjectPoolManager.Object<T>) null;
        return this.m_ObjectMap.TryGetValue(target, out @object) ? @object : (ObjectPoolManager.Object<T>) null;
      }

      private void GetCanReleaseObjects(List<T> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (KeyValuePair<object, ObjectPoolManager.Object<T>> keyValuePair in this.m_ObjectMap)
        {
          ObjectPoolManager.Object<T> @object = keyValuePair.Value;
          if (!@object.IsInUse && !@object.Locked && @object.CustomCanReleaseFlag)
            results.Add(@object.Peek());
        }
      }

      private List<T> DefaultReleaseObjectFilterCallback(
        List<T> candidateObjects,
        int toReleaseCount,
        DateTime expireTime)
      {
        this.m_CachedToReleaseObjects.Clear();
        if (expireTime > DateTime.MinValue)
        {
          for (int index = candidateObjects.Count - 1; index >= 0; --index)
          {
            if (candidateObjects[index].LastUseTime <= expireTime)
            {
              this.m_CachedToReleaseObjects.Add(candidateObjects[index]);
              candidateObjects.RemoveAt(index);
            }
          }
          toReleaseCount -= this.m_CachedToReleaseObjects.Count;
        }
        for (int index1 = 0; toReleaseCount > 0 && index1 < candidateObjects.Count; ++index1)
        {
          for (int index2 = index1 + 1; index2 < candidateObjects.Count; ++index2)
          {
            if (candidateObjects[index1].Priority > candidateObjects[index2].Priority || candidateObjects[index1].Priority == candidateObjects[index2].Priority && candidateObjects[index1].LastUseTime > candidateObjects[index2].LastUseTime)
            {
              T candidateObject = candidateObjects[index1];
              candidateObjects[index1] = candidateObjects[index2];
              candidateObjects[index2] = candidateObject;
            }
          }
          this.m_CachedToReleaseObjects.Add(candidateObjects[index1]);
          --toReleaseCount;
        }
        return this.m_CachedToReleaseObjects;
      }
    }
  }
}
