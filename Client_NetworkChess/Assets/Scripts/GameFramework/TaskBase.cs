// Decompiled with JetBrains decompiler
// Type: GameFramework.TaskBase
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>任务基类。</summary>
  internal abstract class TaskBase : IReference
  {
    /// <summary>任务默认优先级。</summary>
    public const int DefaultPriority = 0;
    private int m_SerialId;
    private string m_Tag;
    private int m_Priority;
    private object m_UserData;
    private bool m_Done;

    /// <summary>初始化任务基类的新实例。</summary>
    public TaskBase()
    {
      this.m_SerialId = 0;
      this.m_Tag = (string) null;
      this.m_Priority = 0;
      this.m_Done = false;
      this.m_UserData = (object) null;
    }

    /// <summary>获取任务的序列编号。</summary>
    public int SerialId => this.m_SerialId;

    /// <summary>获取任务的标签。</summary>
    public string Tag => this.m_Tag;

    /// <summary>获取任务的优先级。</summary>
    public int Priority => this.m_Priority;

    /// <summary>获取任务的用户自定义数据。</summary>
    public object UserData => this.m_UserData;

    /// <summary>获取或设置任务是否完成。</summary>
    public bool Done
    {
      get => this.m_Done;
      set => this.m_Done = value;
    }

    /// <summary>获取任务描述。</summary>
    public virtual string Description => (string) null;

    /// <summary>初始化任务基类。</summary>
    /// <param name="serialId">任务的序列编号。</param>
    /// <param name="tag">任务的标签。</param>
    /// <param name="priority">任务的优先级。</param>
    /// <param name="userData">任务的用户自定义数据。</param>
    internal void Initialize(int serialId, string tag, int priority, object userData)
    {
      this.m_SerialId = serialId;
      this.m_Tag = tag;
      this.m_Priority = priority;
      this.m_UserData = userData;
      this.m_Done = false;
    }

    /// <summary>清理任务基类。</summary>
    public virtual void Clear()
    {
      this.m_SerialId = 0;
      this.m_Tag = (string) null;
      this.m_Priority = 0;
      this.m_UserData = (object) null;
      this.m_Done = false;
    }
  }
}
