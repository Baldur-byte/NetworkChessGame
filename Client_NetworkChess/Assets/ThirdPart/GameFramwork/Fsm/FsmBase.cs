// Decompiled with JetBrains decompiler
// Type: GameFramework.Fsm.FsmBase
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.Fsm
{
  /// <summary>有限状态机基类。</summary>
  public abstract class FsmBase
  {
    private string m_Name;

    /// <summary>初始化有限状态机基类的新实例。</summary>
    public FsmBase() => this.m_Name = string.Empty;

    /// <summary>获取有限状态机名称。</summary>
    public string Name
    {
      get => this.m_Name;
      protected set => this.m_Name = value ?? string.Empty;
    }

    /// <summary>获取有限状态机完整名称。</summary>
    public string FullName => new TypeNamePair(this.OwnerType, this.m_Name).ToString();

    /// <summary>获取有限状态机持有者类型。</summary>
    public abstract Type OwnerType { get; }

    /// <summary>获取有限状态机中状态的数量。</summary>
    public abstract int FsmStateCount { get; }

    /// <summary>获取有限状态机是否正在运行。</summary>
    public abstract bool IsRunning { get; }

    /// <summary>获取有限状态机是否被销毁。</summary>
    public abstract bool IsDestroyed { get; }

    /// <summary>获取当前有限状态机状态名称。</summary>
    public abstract string CurrentStateName { get; }

    /// <summary>获取当前有限状态机状态持续时间。</summary>
    public abstract float CurrentStateTime { get; }

    /// <summary>有限状态机轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">当前已流逝时间，以秒为单位。</param>
    internal abstract void Update(float elapseSeconds, float realElapseSeconds);

    /// <summary>关闭并清理有限状态机。</summary>
    internal abstract void Shutdown();
  }
}
