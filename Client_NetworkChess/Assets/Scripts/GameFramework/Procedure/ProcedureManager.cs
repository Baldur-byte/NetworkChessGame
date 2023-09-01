// Decompiled with JetBrains decompiler
// Type: GameFramework.Procedure.ProcedureManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Fsm;
using System;

namespace GameFramework.Procedure
{
  /// <summary>流程管理器。</summary>
  internal sealed class ProcedureManager : GameFrameworkModule, IProcedureManager
  {
    private IFsmManager m_FsmManager;
    private IFsm<IProcedureManager> m_ProcedureFsm;

    /// <summary>初始化流程管理器的新实例。</summary>
    public ProcedureManager()
    {
      this.m_FsmManager = (IFsmManager) null;
      this.m_ProcedureFsm = (IFsm<IProcedureManager>) null;
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => -2;

    /// <summary>获取当前流程。</summary>
    public ProcedureBase CurrentProcedure => this.m_ProcedureFsm != null ? (ProcedureBase) this.m_ProcedureFsm.CurrentState : throw new GameFrameworkException("You must initialize procedure first.");

    /// <summary>获取当前流程持续时间。</summary>
    public float CurrentProcedureTime => this.m_ProcedureFsm != null ? this.m_ProcedureFsm.CurrentStateTime : throw new GameFrameworkException("You must initialize procedure first.");

    /// <summary>流程管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理流程管理器。</summary>
    internal override void Shutdown()
    {
      if (this.m_FsmManager == null)
        return;
      if (this.m_ProcedureFsm != null)
      {
        this.m_FsmManager.DestroyFsm<IProcedureManager>(this.m_ProcedureFsm);
        this.m_ProcedureFsm = (IFsm<IProcedureManager>) null;
      }
      this.m_FsmManager = (IFsmManager) null;
    }

    /// <summary>初始化流程管理器。</summary>
    /// <param name="fsmManager">有限状态机管理器。</param>
    /// <param name="procedures">流程管理器包含的流程。</param>
    public void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures)
    {
      this.m_FsmManager = fsmManager != null ? fsmManager : throw new GameFrameworkException("FSM manager is invalid.");
      this.m_ProcedureFsm = this.m_FsmManager.CreateFsm<IProcedureManager>((IProcedureManager) this, (FsmState<IProcedureManager>[]) procedures);
    }

    /// <summary>开始流程。</summary>
    /// <typeparam name="T">要开始的流程类型。</typeparam>
    public void StartProcedure<T>() where T : ProcedureBase
    {
      if (this.m_ProcedureFsm == null)
        throw new GameFrameworkException("You must initialize procedure first.");
      this.m_ProcedureFsm.Start<T>();
    }

    /// <summary>开始流程。</summary>
    /// <param name="procedureType">要开始的流程类型。</param>
    public void StartProcedure(Type procedureType)
    {
      if (this.m_ProcedureFsm == null)
        throw new GameFrameworkException("You must initialize procedure first.");
      this.m_ProcedureFsm.Start(procedureType);
    }

    /// <summary>是否存在流程。</summary>
    /// <typeparam name="T">要检查的流程类型。</typeparam>
    /// <returns>是否存在流程。</returns>
    public bool HasProcedure<T>() where T : ProcedureBase => this.m_ProcedureFsm != null ? this.m_ProcedureFsm.HasState<T>() : throw new GameFrameworkException("You must initialize procedure first.");

    /// <summary>是否存在流程。</summary>
    /// <param name="procedureType">要检查的流程类型。</param>
    /// <returns>是否存在流程。</returns>
    public bool HasProcedure(Type procedureType) => this.m_ProcedureFsm != null ? this.m_ProcedureFsm.HasState(procedureType) : throw new GameFrameworkException("You must initialize procedure first.");

    /// <summary>获取流程。</summary>
    /// <typeparam name="T">要获取的流程类型。</typeparam>
    /// <returns>要获取的流程。</returns>
    public ProcedureBase GetProcedure<T>() where T : ProcedureBase => this.m_ProcedureFsm != null ? (ProcedureBase) this.m_ProcedureFsm.GetState<T>() : throw new GameFrameworkException("You must initialize procedure first.");

    /// <summary>获取流程。</summary>
    /// <param name="procedureType">要获取的流程类型。</param>
    /// <returns>要获取的流程。</returns>
    public ProcedureBase GetProcedure(Type procedureType) => this.m_ProcedureFsm != null ? (ProcedureBase) this.m_ProcedureFsm.GetState(procedureType) : throw new GameFrameworkException("You must initialize procedure first.");
  }
}
