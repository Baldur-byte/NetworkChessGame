// Decompiled with JetBrains decompiler
// Type: GameFramework.TaskInfo
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Runtime.InteropServices;

namespace GameFramework
{
  /// <summary>任务信息。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct TaskInfo
  {
    private readonly bool m_IsValid;
    private readonly int m_SerialId;
    private readonly string m_Tag;
    private readonly int m_Priority;
    private readonly object m_UserData;
    private readonly TaskStatus m_Status;
    private readonly string m_Description;

    /// <summary>初始化任务信息的新实例。</summary>
    /// <param name="serialId">任务的序列编号。</param>
    /// <param name="tag">任务的标签。</param>
    /// <param name="priority">任务的优先级。</param>
    /// <param name="userData">任务的用户自定义数据。</param>
    /// <param name="status">任务状态。</param>
    /// <param name="description">任务描述。</param>
    public TaskInfo(
      int serialId,
      string tag,
      int priority,
      object userData,
      TaskStatus status,
      string description)
    {
      this.m_IsValid = true;
      this.m_SerialId = serialId;
      this.m_Tag = tag;
      this.m_Priority = priority;
      this.m_UserData = userData;
      this.m_Status = status;
      this.m_Description = description;
    }

    /// <summary>获取任务信息是否有效。</summary>
    public bool IsValid => this.m_IsValid;

    /// <summary>获取任务的序列编号。</summary>
    public int SerialId
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_SerialId;
      }
    }

    /// <summary>获取任务的标签。</summary>
    public string Tag
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Tag;
      }
    }

    /// <summary>获取任务的优先级。</summary>
    public int Priority
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Priority;
      }
    }

    /// <summary>获取任务的用户自定义数据。</summary>
    public object UserData
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_UserData;
      }
    }

    /// <summary>获取任务状态。</summary>
    public TaskStatus Status
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Status;
      }
    }

    /// <summary>获取任务描述。</summary>
    public string Description
    {
      get
      {
        if (!this.m_IsValid)
          throw new GameFrameworkException("Data is invalid.");
        return this.m_Description;
      }
    }
  }
}
