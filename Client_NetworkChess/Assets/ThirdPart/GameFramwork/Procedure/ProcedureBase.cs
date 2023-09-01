// Decompiled with JetBrains decompiler
// Type: GameFramework.Procedure.ProcedureBase
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Fsm;

namespace GameFramework.Procedure
{
  /// <summary>流程基类。</summary>
  public abstract class ProcedureBase : FsmState<IProcedureManager>
  {
    /// <summary>状态初始化时调用。</summary>
    /// <param name="procedureOwner">流程持有者。</param>
    protected internal override void OnInit(IFsm<IProcedureManager> procedureOwner) => base.OnInit(procedureOwner);

    /// <summary>进入状态时调用。</summary>
    /// <param name="procedureOwner">流程持有者。</param>
    protected internal override void OnEnter(IFsm<IProcedureManager> procedureOwner) => base.OnEnter(procedureOwner);

    /// <summary>状态轮询时调用。</summary>
    /// <param name="procedureOwner">流程持有者。</param>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    protected internal override void OnUpdate(
      IFsm<IProcedureManager> procedureOwner,
      float elapseSeconds,
      float realElapseSeconds)
    {
      base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
    }

    /// <summary>离开状态时调用。</summary>
    /// <param name="procedureOwner">流程持有者。</param>
    /// <param name="isShutdown">是否是关闭状态机时触发。</param>
    protected internal override void OnLeave(
      IFsm<IProcedureManager> procedureOwner,
      bool isShutdown)
    {
      base.OnLeave(procedureOwner, isShutdown);
    }

    /// <summary>状态销毁时调用。</summary>
    /// <param name="procedureOwner">流程持有者。</param>
    protected internal override void OnDestroy(IFsm<IProcedureManager> procedureOwner) => base.OnDestroy(procedureOwner);
  }
}
