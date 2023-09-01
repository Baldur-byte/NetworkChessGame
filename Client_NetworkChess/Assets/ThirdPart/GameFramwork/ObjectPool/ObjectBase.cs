// Decompiled with JetBrains decompiler
// Type: GameFramework.ObjectPool.ObjectBase
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.ObjectPool
{
  /// <summary>对象基类。</summary>
  public abstract class ObjectBase : IReference
  {
    private string m_Name;
    private object m_Target;
    private bool m_Locked;
    private int m_Priority;
    private DateTime m_LastUseTime;

    /// <summary>初始化对象基类的新实例。</summary>
    public ObjectBase()
    {
      this.m_Name = (string) null;
      this.m_Target = (object) null;
      this.m_Locked = false;
      this.m_Priority = 0;
      this.m_LastUseTime = new DateTime();
    }

    /// <summary>获取对象名称。</summary>
    public string Name => this.m_Name;

    /// <summary>获取对象。</summary>
    public object Target => this.m_Target;

    /// <summary>获取或设置对象是否被加锁。</summary>
    public bool Locked
    {
      get => this.m_Locked;
      set => this.m_Locked = value;
    }

    /// <summary>获取或设置对象的优先级。</summary>
    public int Priority
    {
      get => this.m_Priority;
      set => this.m_Priority = value;
    }

    /// <summary>获取自定义释放检查标记。</summary>
    public virtual bool CustomCanReleaseFlag => true;

    /// <summary>获取对象上次使用时间。</summary>
    public DateTime LastUseTime
    {
      get => this.m_LastUseTime;
      internal set => this.m_LastUseTime = value;
    }

    /// <summary>初始化对象基类。</summary>
    /// <param name="target">对象。</param>
    protected void Initialize(object target) => this.Initialize((string) null, target, false, 0);

    /// <summary>初始化对象基类。</summary>
    /// <param name="name">对象名称。</param>
    /// <param name="target">对象。</param>
    protected void Initialize(string name, object target) => this.Initialize(name, target, false, 0);

    /// <summary>初始化对象基类。</summary>
    /// <param name="name">对象名称。</param>
    /// <param name="target">对象。</param>
    /// <param name="locked">对象是否被加锁。</param>
    protected void Initialize(string name, object target, bool locked) => this.Initialize(name, target, locked, 0);

    /// <summary>初始化对象基类。</summary>
    /// <param name="name">对象名称。</param>
    /// <param name="target">对象。</param>
    /// <param name="priority">对象的优先级。</param>
    protected void Initialize(string name, object target, int priority) => this.Initialize(name, target, false, priority);

    /// <summary>初始化对象基类。</summary>
    /// <param name="name">对象名称。</param>
    /// <param name="target">对象。</param>
    /// <param name="locked">对象是否被加锁。</param>
    /// <param name="priority">对象的优先级。</param>
    protected void Initialize(string name, object target, bool locked, int priority)
    {
      if (target == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Target '{0}' is invalid.", name));
      this.m_Name = name ?? string.Empty;
      this.m_Target = target;
      this.m_Locked = locked;
      this.m_Priority = priority;
      this.m_LastUseTime = DateTime.UtcNow;
    }

    /// <summary>清理对象基类。</summary>
    public virtual void Clear()
    {
      this.m_Name = (string) null;
      this.m_Target = (object) null;
      this.m_Locked = false;
      this.m_Priority = 0;
      this.m_LastUseTime = new DateTime();
    }

    /// <summary>获取对象时的事件。</summary>
    protected internal virtual void OnSpawn()
    {
    }

    /// <summary>回收对象时的事件。</summary>
    protected internal virtual void OnUnspawn()
    {
    }

    /// <summary>释放对象。</summary>
    /// <param name="isShutdown">是否是关闭对象池时触发。</param>
    protected internal abstract void Release(bool isShutdown);
  }
}
