// Decompiled with JetBrains decompiler
// Type: GameFramework.Fsm.FsmState`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.Fsm
{
  /// <summary>有限状态机状态基类。</summary>
  /// <typeparam name="T">有限状态机持有者类型。</typeparam>
  public abstract class FsmState<T> where T : class
  {
    /// <summary>有限状态机状态初始化时调用。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected internal virtual void OnInit(IFsm<T> fsm)
    {
    }

    /// <summary>有限状态机状态进入时调用。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected internal virtual void OnEnter(IFsm<T> fsm)
    {
    }

    /// <summary>有限状态机状态轮询时调用。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    protected internal virtual void OnUpdate(
      IFsm<T> fsm,
      float elapseSeconds,
      float realElapseSeconds)
    {
    }

    /// <summary>有限状态机状态离开时调用。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
    protected internal virtual void OnLeave(IFsm<T> fsm, bool isShutdown)
    {
    }

    /// <summary>有限状态机状态销毁时调用。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected internal virtual void OnDestroy(IFsm<T> fsm)
    {
    }

    /// <summary>切换当前有限状态机状态。</summary>
    /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
    /// <param name="fsm">有限状态机引用。</param>
    protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T> => ((GameFramework.Fsm.Fsm<T>) fsm ?? throw new GameFrameworkException("FSM is invalid.")).ChangeState<TState>();

    /// <summary>切换当前有限状态机状态。</summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="stateType">要切换到的有限状态机状态类型。</param>
    protected void ChangeState(IFsm<T> fsm, Type stateType)
    {
      GameFramework.Fsm.Fsm<T> fsm1 = (GameFramework.Fsm.Fsm<T>) fsm;
      if (fsm1 == null)
        throw new GameFrameworkException("FSM is invalid.");
      if (stateType == null)
        throw new GameFrameworkException("State type is invalid.");
      if (!typeof (FsmState<T>).IsAssignableFrom(stateType))
        throw new GameFrameworkException(Utility.Text.Format<string>("State type '{0}' is invalid.", stateType.FullName));
      fsm1.ChangeState(stateType);
    }
  }
}
