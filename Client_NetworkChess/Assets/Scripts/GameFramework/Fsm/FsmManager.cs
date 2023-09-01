// Decompiled with JetBrains decompiler
// Type: GameFramework.Fsm.FsmManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.Fsm
{
  /// <summary>有限状态机管理器。</summary>
  internal sealed class FsmManager : GameFrameworkModule, IFsmManager
  {
    private readonly Dictionary<TypeNamePair, FsmBase> m_Fsms;
    private readonly List<FsmBase> m_TempFsms;

    /// <summary>初始化有限状态机管理器的新实例。</summary>
    public FsmManager()
    {
      this.m_Fsms = new Dictionary<TypeNamePair, FsmBase>();
      this.m_TempFsms = new List<FsmBase>();
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 1;

    /// <summary>获取有限状态机数量。</summary>
    public int Count => this.m_Fsms.Count;

    /// <summary>有限状态机管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      this.m_TempFsms.Clear();
      if (this.m_Fsms.Count <= 0)
        return;
      foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in this.m_Fsms)
        this.m_TempFsms.Add(fsm.Value);
      foreach (FsmBase tempFsm in this.m_TempFsms)
      {
        if (!tempFsm.IsDestroyed)
          tempFsm.Update(elapseSeconds, realElapseSeconds);
      }
    }

    /// <summary>关闭并清理有限状态机管理器。</summary>
    internal override void Shutdown()
    {
      foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in this.m_Fsms)
        fsm.Value.Shutdown();
      this.m_Fsms.Clear();
      this.m_TempFsms.Clear();
    }

    /// <summary>检查是否存在有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <returns>是否存在有限状态机。</returns>
    public bool HasFsm<T>() where T : class => this.InternalHasFsm(new TypeNamePair(typeof (T)));

    /// <summary>检查是否存在有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <returns>是否存在有限状态机。</returns>
    public bool HasFsm(Type ownerType) => ownerType != null ? this.InternalHasFsm(new TypeNamePair(ownerType)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>检查是否存在有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="name">有限状态机名称。</param>
    /// <returns>是否存在有限状态机。</returns>
    public bool HasFsm<T>(string name) where T : class => this.InternalHasFsm(new TypeNamePair(typeof (T), name));

    /// <summary>检查是否存在有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <param name="name">有限状态机名称。</param>
    /// <returns>是否存在有限状态机。</returns>
    public bool HasFsm(Type ownerType, string name) => ownerType != null ? this.InternalHasFsm(new TypeNamePair(ownerType, name)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>获取有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <returns>要获取的有限状态机。</returns>
    public IFsm<T> GetFsm<T>() where T : class => (IFsm<T>) this.InternalGetFsm(new TypeNamePair(typeof (T)));

    /// <summary>获取有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <returns>要获取的有限状态机。</returns>
    public FsmBase GetFsm(Type ownerType) => ownerType != null ? this.InternalGetFsm(new TypeNamePair(ownerType)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>获取有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="name">有限状态机名称。</param>
    /// <returns>要获取的有限状态机。</returns>
    public IFsm<T> GetFsm<T>(string name) where T : class => (IFsm<T>) this.InternalGetFsm(new TypeNamePair(typeof (T), name));

    /// <summary>获取有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <param name="name">有限状态机名称。</param>
    /// <returns>要获取的有限状态机。</returns>
    public FsmBase GetFsm(Type ownerType, string name) => ownerType != null ? this.InternalGetFsm(new TypeNamePair(ownerType, name)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>获取所有有限状态机。</summary>
    /// <returns>所有有限状态机。</returns>
    public FsmBase[] GetAllFsms()
    {
      int num = 0;
      FsmBase[] allFsms = new FsmBase[this.m_Fsms.Count];
      foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in this.m_Fsms)
        allFsms[num++] = fsm.Value;
      return allFsms;
    }

    /// <summary>获取所有有限状态机。</summary>
    /// <param name="results">所有有限状态机。</param>
    public void GetAllFsms(List<FsmBase> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<TypeNamePair, FsmBase> fsm in this.m_Fsms)
        results.Add(fsm.Value);
    }

    /// <summary>创建有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>要创建的有限状态机。</returns>
    public IFsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class => this.CreateFsm<T>(string.Empty, owner, states);

    /// <summary>创建有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="name">有限状态机名称。</param>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>要创建的有限状态机。</returns>
    public IFsm<T> CreateFsm<T>(string name, T owner, params FsmState<T>[] states) where T : class
    {
      TypeNamePair key = new TypeNamePair(typeof (T), name);
      if (this.HasFsm<T>(name))
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist FSM '{0}'.", key));
      GameFramework.Fsm.Fsm<T> fsm = GameFramework.Fsm.Fsm<T>.Create(name, owner, states);
      this.m_Fsms.Add(key, (FsmBase) fsm);
      return (IFsm<T>) fsm;
    }

    /// <summary>创建有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>要创建的有限状态机。</returns>
    public IFsm<T> CreateFsm<T>(T owner, List<FsmState<T>> states) where T : class => this.CreateFsm<T>(string.Empty, owner, states);

    /// <summary>创建有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="name">有限状态机名称。</param>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>要创建的有限状态机。</returns>
    public IFsm<T> CreateFsm<T>(string name, T owner, List<FsmState<T>> states) where T : class
    {
      TypeNamePair key = new TypeNamePair(typeof (T), name);
      if (this.HasFsm<T>(name))
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist FSM '{0}'.", key));
      GameFramework.Fsm.Fsm<T> fsm = GameFramework.Fsm.Fsm<T>.Create(name, owner, states);
      this.m_Fsms.Add(key, (FsmBase) fsm);
      return (IFsm<T>) fsm;
    }

    /// <summary>销毁有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm<T>() where T : class => this.InternalDestroyFsm(new TypeNamePair(typeof (T)));

    /// <summary>销毁有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm(Type ownerType) => ownerType != null ? this.InternalDestroyFsm(new TypeNamePair(ownerType)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>销毁有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="name">要销毁的有限状态机名称。</param>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm<T>(string name) where T : class => this.InternalDestroyFsm(new TypeNamePair(typeof (T), name));

    /// <summary>销毁有限状态机。</summary>
    /// <param name="ownerType">有限状态机持有者类型。</param>
    /// <param name="name">要销毁的有限状态机名称。</param>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm(Type ownerType, string name) => ownerType != null ? this.InternalDestroyFsm(new TypeNamePair(ownerType, name)) : throw new GameFrameworkException("Owner type is invalid.");

    /// <summary>销毁有限状态机。</summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    /// <param name="fsm">要销毁的有限状态机。</param>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm<T>(IFsm<T> fsm) where T : class => fsm != null ? this.InternalDestroyFsm(new TypeNamePair(typeof (T), fsm.Name)) : throw new GameFrameworkException("FSM is invalid.");

    /// <summary>销毁有限状态机。</summary>
    /// <param name="fsm">要销毁的有限状态机。</param>
    /// <returns>是否销毁有限状态机成功。</returns>
    public bool DestroyFsm(FsmBase fsm)
    {
      if (fsm == null)
        throw new GameFrameworkException("FSM is invalid.");
      return this.InternalDestroyFsm(new TypeNamePair(fsm.OwnerType, fsm.Name));
    }

    private bool InternalHasFsm(TypeNamePair typeNamePair) => this.m_Fsms.ContainsKey(typeNamePair);

    private FsmBase InternalGetFsm(TypeNamePair typeNamePair)
    {
      FsmBase fsmBase = (FsmBase) null;
      return this.m_Fsms.TryGetValue(typeNamePair, out fsmBase) ? fsmBase : (FsmBase) null;
    }

    private bool InternalDestroyFsm(TypeNamePair typeNamePair)
    {
      FsmBase fsmBase = (FsmBase) null;
      if (!this.m_Fsms.TryGetValue(typeNamePair, out fsmBase))
        return false;
      fsmBase.Shutdown();
      return this.m_Fsms.Remove(typeNamePair);
    }
  }
}
