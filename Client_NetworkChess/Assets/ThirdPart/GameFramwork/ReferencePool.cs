// Decompiled with JetBrains decompiler
// Type: GameFramework.ReferencePool
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework
{
  /// <summary>引用池。</summary>
  public static class ReferencePool
  {
    private static readonly Dictionary<Type, ReferencePool.ReferenceCollection> s_ReferenceCollections = new Dictionary<Type, ReferencePool.ReferenceCollection>();
    private static bool m_EnableStrictCheck = false;

    /// <summary>获取或设置是否开启强制检查。</summary>
    public static bool EnableStrictCheck
    {
      get => ReferencePool.m_EnableStrictCheck;
      set => ReferencePool.m_EnableStrictCheck = value;
    }

    /// <summary>获取引用池的数量。</summary>
    public static int Count => ReferencePool.s_ReferenceCollections.Count;

    /// <summary>获取所有引用池的信息。</summary>
    /// <returns>所有引用池的信息。</returns>
    public static ReferencePoolInfo[] GetAllReferencePoolInfos()
    {
      int num = 0;
      ReferencePoolInfo[] referencePoolInfos = (ReferencePoolInfo[]) null;
      lock (ReferencePool.s_ReferenceCollections)
      {
        referencePoolInfos = new ReferencePoolInfo[ReferencePool.s_ReferenceCollections.Count];
        foreach (KeyValuePair<Type, ReferencePool.ReferenceCollection> referenceCollection in ReferencePool.s_ReferenceCollections)
          referencePoolInfos[num++] = new ReferencePoolInfo(referenceCollection.Key, referenceCollection.Value.UnusedReferenceCount, referenceCollection.Value.UsingReferenceCount, referenceCollection.Value.AcquireReferenceCount, referenceCollection.Value.ReleaseReferenceCount, referenceCollection.Value.AddReferenceCount, referenceCollection.Value.RemoveReferenceCount);
      }
      return referencePoolInfos;
    }

    /// <summary>清除所有引用池。</summary>
    public static void ClearAll()
    {
      lock (ReferencePool.s_ReferenceCollections)
      {
        foreach (KeyValuePair<Type, ReferencePool.ReferenceCollection> referenceCollection in ReferencePool.s_ReferenceCollections)
          referenceCollection.Value.RemoveAll();
        ReferencePool.s_ReferenceCollections.Clear();
      }
    }

    /// <summary>从引用池获取引用。</summary>
    /// <typeparam name="T">引用类型。</typeparam>
    /// <returns>引用。</returns>
    public static T Acquire<T>() where T : class, IReference, new() => ReferencePool.GetReferenceCollection(typeof (T)).Acquire<T>();

    /// <summary>从引用池获取引用。</summary>
    /// <param name="referenceType">引用类型。</param>
    /// <returns>引用。</returns>
    public static IReference Acquire(Type referenceType)
    {
      ReferencePool.InternalCheckReferenceType(referenceType);
      return ReferencePool.GetReferenceCollection(referenceType).Acquire();
    }

    /// <summary>将引用归还引用池。</summary>
    /// <param name="reference">引用。</param>
    public static void Release(IReference reference)
    {
      Type referenceType = reference != null ? reference.GetType() : throw new GameFrameworkException("Reference is invalid.");
      ReferencePool.InternalCheckReferenceType(referenceType);
      ReferencePool.GetReferenceCollection(referenceType).Release(reference);
    }

    /// <summary>向引用池中追加指定数量的引用。</summary>
    /// <typeparam name="T">引用类型。</typeparam>
    /// <param name="count">追加数量。</param>
    public static void Add<T>(int count) where T : class, IReference, new() => ReferencePool.GetReferenceCollection(typeof (T)).Add<T>(count);

    /// <summary>向引用池中追加指定数量的引用。</summary>
    /// <param name="referenceType">引用类型。</param>
    /// <param name="count">追加数量。</param>
    public static void Add(Type referenceType, int count)
    {
      ReferencePool.InternalCheckReferenceType(referenceType);
      ReferencePool.GetReferenceCollection(referenceType).Add(count);
    }

    /// <summary>从引用池中移除指定数量的引用。</summary>
    /// <typeparam name="T">引用类型。</typeparam>
    /// <param name="count">移除数量。</param>
    public static void Remove<T>(int count) where T : class, IReference => ReferencePool.GetReferenceCollection(typeof (T)).Remove(count);

    /// <summary>从引用池中移除指定数量的引用。</summary>
    /// <param name="referenceType">引用类型。</param>
    /// <param name="count">移除数量。</param>
    public static void Remove(Type referenceType, int count)
    {
      ReferencePool.InternalCheckReferenceType(referenceType);
      ReferencePool.GetReferenceCollection(referenceType).Remove(count);
    }

    /// <summary>从引用池中移除所有的引用。</summary>
    /// <typeparam name="T">引用类型。</typeparam>
    public static void RemoveAll<T>() where T : class, IReference => ReferencePool.GetReferenceCollection(typeof (T)).RemoveAll();

    /// <summary>从引用池中移除所有的引用。</summary>
    /// <param name="referenceType">引用类型。</param>
    public static void RemoveAll(Type referenceType)
    {
      ReferencePool.InternalCheckReferenceType(referenceType);
      ReferencePool.GetReferenceCollection(referenceType).RemoveAll();
    }

    private static void InternalCheckReferenceType(Type referenceType)
    {
      if (!ReferencePool.m_EnableStrictCheck)
        return;
      if (referenceType == null)
        throw new GameFrameworkException("Reference type is invalid.");
      if (!referenceType.IsClass || referenceType.IsAbstract)
        throw new GameFrameworkException("Reference type is not a non-abstract class type.");
      if (!typeof (IReference).IsAssignableFrom(referenceType))
        throw new GameFrameworkException(Utility.Text.Format<string>("Reference type '{0}' is invalid.", referenceType.FullName));
    }

    private static ReferencePool.ReferenceCollection GetReferenceCollection(Type referenceType)
    {
      if (referenceType == null)
        throw new GameFrameworkException("ReferenceType is invalid.");
      ReferencePool.ReferenceCollection referenceCollection = (ReferencePool.ReferenceCollection) null;
      lock (ReferencePool.s_ReferenceCollections)
      {
        if (!ReferencePool.s_ReferenceCollections.TryGetValue(referenceType, out referenceCollection))
        {
          referenceCollection = new ReferencePool.ReferenceCollection(referenceType);
          ReferencePool.s_ReferenceCollections.Add(referenceType, referenceCollection);
        }
      }
      return referenceCollection;
    }

    private sealed class ReferenceCollection
    {
      private readonly Queue<IReference> m_References;
      private readonly Type m_ReferenceType;
      private int m_UsingReferenceCount;
      private int m_AcquireReferenceCount;
      private int m_ReleaseReferenceCount;
      private int m_AddReferenceCount;
      private int m_RemoveReferenceCount;

      public ReferenceCollection(Type referenceType)
      {
        this.m_References = new Queue<IReference>();
        this.m_ReferenceType = referenceType;
        this.m_UsingReferenceCount = 0;
        this.m_AcquireReferenceCount = 0;
        this.m_ReleaseReferenceCount = 0;
        this.m_AddReferenceCount = 0;
        this.m_RemoveReferenceCount = 0;
      }

      public Type ReferenceType => this.m_ReferenceType;

      public int UnusedReferenceCount => this.m_References.Count;

      public int UsingReferenceCount => this.m_UsingReferenceCount;

      public int AcquireReferenceCount => this.m_AcquireReferenceCount;

      public int ReleaseReferenceCount => this.m_ReleaseReferenceCount;

      public int AddReferenceCount => this.m_AddReferenceCount;

      public int RemoveReferenceCount => this.m_RemoveReferenceCount;

      public T Acquire<T>() where T : class, IReference, new()
      {
        if (typeof (T) != this.m_ReferenceType)
          throw new GameFrameworkException("Type is invalid.");
        ++this.m_UsingReferenceCount;
        ++this.m_AcquireReferenceCount;
        lock (this.m_References)
        {
          if (this.m_References.Count > 0)
            return (T) this.m_References.Dequeue();
        }
        ++this.m_AddReferenceCount;
        return new T();
      }

      public IReference Acquire()
      {
        ++this.m_UsingReferenceCount;
        ++this.m_AcquireReferenceCount;
        lock (this.m_References)
        {
          if (this.m_References.Count > 0)
            return this.m_References.Dequeue();
        }
        ++this.m_AddReferenceCount;
        return (IReference) Activator.CreateInstance(this.m_ReferenceType);
      }

      public void Release(IReference reference)
      {
        reference.Clear();
        lock (this.m_References)
        {
          if (ReferencePool.m_EnableStrictCheck && this.m_References.Contains(reference))
            throw new GameFrameworkException("The reference has been released.");
          this.m_References.Enqueue(reference);
        }
        ++this.m_ReleaseReferenceCount;
        --this.m_UsingReferenceCount;
      }

      public void Add<T>(int count) where T : class, IReference, new()
      {
        if (typeof (T) != this.m_ReferenceType)
          throw new GameFrameworkException("Type is invalid.");
        lock (this.m_References)
        {
          this.m_AddReferenceCount += count;
          while (count-- > 0)
            this.m_References.Enqueue((IReference) new T());
        }
      }

      public void Add(int count)
      {
        lock (this.m_References)
        {
          this.m_AddReferenceCount += count;
          while (count-- > 0)
            this.m_References.Enqueue((IReference) Activator.CreateInstance(this.m_ReferenceType));
        }
      }

      public void Remove(int count)
      {
        lock (this.m_References)
        {
          if (count > this.m_References.Count)
            count = this.m_References.Count;
          this.m_RemoveReferenceCount += count;
          while (count-- > 0)
            this.m_References.Dequeue();
        }
      }

      public void RemoveAll()
      {
        lock (this.m_References)
        {
          this.m_RemoveReferenceCount += this.m_References.Count;
          this.m_References.Clear();
        }
      }
    }
  }
}
