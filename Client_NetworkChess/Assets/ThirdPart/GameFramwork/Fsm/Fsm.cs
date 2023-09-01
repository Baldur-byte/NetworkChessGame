// Decompiled with JetBrains decompiler
// Type: GameFramework.Fsm.Fsm`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.Fsm
{
  /// <summary>有限状态机。</summary>
  /// <typeparam name="T">有限状态机持有者类型。</typeparam>
  internal sealed class Fsm<T> : FsmBase, IReference, IFsm<T> where T : class
  {
    private T m_Owner;
    private readonly Dictionary<Type, FsmState<T>> m_States;
    private Dictionary<string, Variable> m_Datas;
    private FsmState<T> m_CurrentState;
    private float m_CurrentStateTime;
    private bool m_IsDestroyed;

    /// <summary>初始化有限状态机的新实例。</summary>
    public Fsm()
    {
      this.m_Owner = default (T);
      this.m_States = new Dictionary<Type, FsmState<T>>();
      this.m_Datas = (Dictionary<string, Variable>) null;
      this.m_CurrentState = (FsmState<T>) null;
      this.m_CurrentStateTime = 0.0f;
      this.m_IsDestroyed = true;
    }

    /// <summary>获取有限状态机持有者。</summary>
    public T Owner => this.m_Owner;

    /// <summary>获取有限状态机持有者类型。</summary>
    public override Type OwnerType => typeof (T);

    /// <summary>获取有限状态机中状态的数量。</summary>
    public override int FsmStateCount => this.m_States.Count;

    /// <summary>获取有限状态机是否正在运行。</summary>
    public override bool IsRunning => this.m_CurrentState != null;

    /// <summary>获取有限状态机是否被销毁。</summary>
    public override bool IsDestroyed => this.m_IsDestroyed;

    /// <summary>获取当前有限状态机状态。</summary>
    public FsmState<T> CurrentState => this.m_CurrentState;

    /// <summary>获取当前有限状态机状态名称。</summary>
    public override string CurrentStateName => this.m_CurrentState == null ? (string) null : this.m_CurrentState.GetType().FullName;

    /// <summary>获取当前有限状态机状态持续时间。</summary>
    public override float CurrentStateTime => this.m_CurrentStateTime;

    /// <summary>创建有限状态机。</summary>
    /// <param name="name">有限状态机名称。</param>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>创建的有限状态机。</returns>
    public static GameFramework.Fsm.Fsm<T> Create(
      string name,
      T owner,
      params FsmState<T>[] states)
    {
      if ((object) owner == null)
        throw new GameFrameworkException("FSM owner is invalid.");
      if (states == null || states.Length < 1)
        throw new GameFrameworkException("FSM states is invalid.");
      GameFramework.Fsm.Fsm<T> fsm = ReferencePool.Acquire<GameFramework.Fsm.Fsm<T>>();
      fsm.Name = name;
      fsm.m_Owner = owner;
      fsm.m_IsDestroyed = false;
      foreach (FsmState<T> state in states)
      {
        Type key = state != null ? state.GetType() : throw new GameFrameworkException("FSM states is invalid.");
        if (fsm.m_States.ContainsKey(key))
          throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string>("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof (T), name), key.FullName));
        fsm.m_States.Add(key, state);
        state.OnInit((IFsm<T>) fsm);
      }
      return fsm;
    }

    /// <summary>创建有限状态机。</summary>
    /// <param name="name">有限状态机名称。</param>
    /// <param name="owner">有限状态机持有者。</param>
    /// <param name="states">有限状态机状态集合。</param>
    /// <returns>创建的有限状态机。</returns>
    public static GameFramework.Fsm.Fsm<T> Create(string name, T owner, List<FsmState<T>> states)
    {
      if ((object) owner == null)
        throw new GameFrameworkException("FSM owner is invalid.");
      if (states == null || states.Count < 1)
        throw new GameFrameworkException("FSM states is invalid.");
      GameFramework.Fsm.Fsm<T> fsm = ReferencePool.Acquire<GameFramework.Fsm.Fsm<T>>();
      fsm.Name = name;
      fsm.m_Owner = owner;
      fsm.m_IsDestroyed = false;
      foreach (FsmState<T> state in states)
      {
        Type key = state != null ? state.GetType() : throw new GameFrameworkException("FSM states is invalid.");
        if (fsm.m_States.ContainsKey(key))
          throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string>("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof (T), name), key.FullName));
        fsm.m_States.Add(key, state);
        state.OnInit((IFsm<T>) fsm);
      }
      return fsm;
    }

    /// <summary>清理有限状态机。</summary>
    public void Clear()
    {
      if (this.m_CurrentState != null)
        this.m_CurrentState.OnLeave((IFsm<T>) this, true);
      foreach (KeyValuePair<Type, FsmState<T>> state in this.m_States)
        state.Value.OnDestroy((IFsm<T>) this);
      this.Name = (string) null;
      this.m_Owner = default (T);
      this.m_States.Clear();
      if (this.m_Datas != null)
      {
        foreach (KeyValuePair<string, Variable> data in this.m_Datas)
        {
          if (data.Value != null)
            ReferencePool.Release((IReference) data.Value);
        }
        this.m_Datas.Clear();
      }
      this.m_CurrentState = (FsmState<T>) null;
      this.m_CurrentStateTime = 0.0f;
      this.m_IsDestroyed = true;
    }

    /// <summary>开始有限状态机。</summary>
    /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
    public void Start<TState>() where TState : FsmState<T>
    {
      if (this.IsRunning)
        throw new GameFrameworkException("FSM is running, can not start again.");
      FsmState<T> state = (FsmState<T>) this.GetState<TState>();
      if (state == null)
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string>("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof (T), this.Name), typeof (TState).FullName));
      this.m_CurrentStateTime = 0.0f;
      this.m_CurrentState = state;
      this.m_CurrentState.OnEnter((IFsm<T>) this);
    }

    /// <summary>开始有限状态机。</summary>
    /// <param name="stateType">要开始的有限状态机状态类型。</param>
    public void Start(Type stateType)
    {
      if (this.IsRunning)
        throw new GameFrameworkException("FSM is running, can not start again.");
      if (stateType == null)
        throw new GameFrameworkException("State type is invalid.");
      FsmState<T> fsmState = typeof (FsmState<T>).IsAssignableFrom(stateType) ? this.GetState(stateType) : throw new GameFrameworkException(Utility.Text.Format<string>("State type '{0}' is invalid.", stateType.FullName));
      if (fsmState == null)
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string>("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof (T), this.Name), stateType.FullName));
      this.m_CurrentStateTime = 0.0f;
      this.m_CurrentState = fsmState;
      this.m_CurrentState.OnEnter((IFsm<T>) this);
    }

    /// <summary>是否存在有限状态机状态。</summary>
    /// <typeparam name="TState">要检查的有限状态机状态类型。</typeparam>
    /// <returns>是否存在有限状态机状态。</returns>
    public bool HasState<TState>() where TState : FsmState<T> => this.m_States.ContainsKey(typeof (TState));

    /// <summary>是否存在有限状态机状态。</summary>
    /// <param name="stateType">要检查的有限状态机状态类型。</param>
    /// <returns>是否存在有限状态机状态。</returns>
    public bool HasState(Type stateType)
    {
      if (stateType == null)
        throw new GameFrameworkException("State type is invalid.");
      return typeof (FsmState<T>).IsAssignableFrom(stateType) ? this.m_States.ContainsKey(stateType) : throw new GameFrameworkException(Utility.Text.Format<string>("State type '{0}' is invalid.", stateType.FullName));
    }

    /// <summary>获取有限状态机状态。</summary>
    /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
    /// <returns>要获取的有限状态机状态。</returns>
    public TState GetState<TState>() where TState : FsmState<T>
    {
      FsmState<T> fsmState = (FsmState<T>) null;
      return this.m_States.TryGetValue(typeof (TState), out fsmState) ? (TState) fsmState : default (TState);
    }

    /// <summary>获取有限状态机状态。</summary>
    /// <param name="stateType">要获取的有限状态机状态类型。</param>
    /// <returns>要获取的有限状态机状态。</returns>
    public FsmState<T> GetState(Type stateType)
    {
      if (stateType == null)
        throw new GameFrameworkException("State type is invalid.");
      if (!typeof (FsmState<T>).IsAssignableFrom(stateType))
        throw new GameFrameworkException(Utility.Text.Format<string>("State type '{0}' is invalid.", stateType.FullName));
      FsmState<T> fsmState = (FsmState<T>) null;
      return this.m_States.TryGetValue(stateType, out fsmState) ? fsmState : (FsmState<T>) null;
    }

    /// <summary>获取有限状态机的所有状态。</summary>
    /// <returns>有限状态机的所有状态。</returns>
    public FsmState<T>[] GetAllStates()
    {
      int num = 0;
      FsmState<T>[] allStates = new FsmState<T>[this.m_States.Count];
      foreach (KeyValuePair<Type, FsmState<T>> state in this.m_States)
        allStates[num++] = state.Value;
      return allStates;
    }

    /// <summary>获取有限状态机的所有状态。</summary>
    /// <param name="results">有限状态机的所有状态。</param>
    public void GetAllStates(List<FsmState<T>> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<Type, FsmState<T>> state in this.m_States)
        results.Add(state.Value);
    }

    /// <summary>是否存在有限状态机数据。</summary>
    /// <param name="name">有限状态机数据名称。</param>
    /// <returns>有限状态机数据是否存在。</returns>
    public bool HasData(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Data name is invalid.");
      return this.m_Datas != null && this.m_Datas.ContainsKey(name);
    }

    /// <summary>获取有限状态机数据。</summary>
    /// <typeparam name="TData">要获取的有限状态机数据的类型。</typeparam>
    /// <param name="name">有限状态机数据名称。</param>
    /// <returns>要获取的有限状态机数据。</returns>
    public TData GetData<TData>(string name) where TData : Variable => (TData) this.GetData(name);

    /// <summary>获取有限状态机数据。</summary>
    /// <param name="name">有限状态机数据名称。</param>
    /// <returns>要获取的有限状态机数据。</returns>
    public Variable GetData(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Data name is invalid.");
      if (this.m_Datas == null)
        return (Variable) null;
      Variable variable = (Variable) null;
      return this.m_Datas.TryGetValue(name, out variable) ? variable : (Variable) null;
    }

    /// <summary>设置有限状态机数据。</summary>
    /// <typeparam name="TData">要设置的有限状态机数据的类型。</typeparam>
    /// <param name="name">有限状态机数据名称。</param>
    /// <param name="data">要设置的有限状态机数据。</param>
    public void SetData<TData>(string name, TData data) where TData : Variable => this.SetData(name, (Variable) data);

    /// <summary>设置有限状态机数据。</summary>
    /// <param name="name">有限状态机数据名称。</param>
    /// <param name="data">要设置的有限状态机数据。</param>
    public void SetData(string name, Variable data)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Data name is invalid.");
      if (this.m_Datas == null)
        this.m_Datas = new Dictionary<string, Variable>((IEqualityComparer<string>) StringComparer.Ordinal);
      Variable data1 = this.GetData(name);
      if (data1 != null)
        ReferencePool.Release((IReference) data1);
      this.m_Datas[name] = data;
    }

    /// <summary>移除有限状态机数据。</summary>
    /// <param name="name">有限状态机数据名称。</param>
    /// <returns>是否移除有限状态机数据成功。</returns>
    public bool RemoveData(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new GameFrameworkException("Data name is invalid.");
      if (this.m_Datas == null)
        return false;
      Variable data = this.GetData(name);
      if (data != null)
        ReferencePool.Release((IReference) data);
      return this.m_Datas.Remove(name);
    }

    /// <summary>有限状态机轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      if (this.m_CurrentState == null)
        return;
      this.m_CurrentStateTime += elapseSeconds;
      this.m_CurrentState.OnUpdate((IFsm<T>) this, elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理有限状态机。</summary>
    internal override void Shutdown() => ReferencePool.Release((IReference) this);

    /// <summary>切换当前有限状态机状态。</summary>
    /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
    internal void ChangeState<TState>() where TState : FsmState<T> => this.ChangeState(typeof (TState));

    /// <summary>切换当前有限状态机状态。</summary>
    /// <param name="stateType">要切换到的有限状态机状态类型。</param>
    internal void ChangeState(Type stateType)
    {
      if (this.m_CurrentState == null)
        throw new GameFrameworkException("Current state is invalid.");
      FsmState<T> state = this.GetState(stateType);
      if (state == null)
        throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, string>("FSM '{0}' can not change state to '{1}' which is not exist.", new TypeNamePair(typeof (T), this.Name), stateType.FullName));
      this.m_CurrentState.OnLeave((IFsm<T>) this, false);
      this.m_CurrentStateTime = 0.0f;
      this.m_CurrentState = state;
      this.m_CurrentState.OnEnter((IFsm<T>) this);
    }
  }
}
