// Decompiled with JetBrains decompiler
// Type: GameFramework.TaskPool`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Collections.Generic;

namespace GameFramework
{
  /// <summary>任务池。</summary>
  /// <typeparam name="T">任务类型。</typeparam>
  internal sealed class TaskPool<T> where T : TaskBase
  {
    private readonly Stack<ITaskAgent<T>> m_FreeAgents;
    private readonly GameFrameworkLinkedList<ITaskAgent<T>> m_WorkingAgents;
    private readonly GameFrameworkLinkedList<T> m_WaitingTasks;
    private bool m_Paused;

    /// <summary>初始化任务池的新实例。</summary>
    public TaskPool()
    {
      this.m_FreeAgents = new Stack<ITaskAgent<T>>();
      this.m_WorkingAgents = new GameFrameworkLinkedList<ITaskAgent<T>>();
      this.m_WaitingTasks = new GameFrameworkLinkedList<T>();
      this.m_Paused = false;
    }

    /// <summary>获取或设置任务池是否被暂停。</summary>
    public bool Paused
    {
      get => this.m_Paused;
      set => this.m_Paused = value;
    }

    /// <summary>获取任务代理总数量。</summary>
    public int TotalAgentCount => this.FreeAgentCount + this.WorkingAgentCount;

    /// <summary>获取可用任务代理数量。</summary>
    public int FreeAgentCount => this.m_FreeAgents.Count;

    /// <summary>获取工作中任务代理数量。</summary>
    public int WorkingAgentCount => this.m_WorkingAgents.Count;

    /// <summary>获取等待任务数量。</summary>
    public int WaitingTaskCount => this.m_WaitingTasks.Count;

    /// <summary>任务池轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    public void Update(float elapseSeconds, float realElapseSeconds)
    {
      if (this.m_Paused)
        return;
      this.ProcessRunningTasks(elapseSeconds, realElapseSeconds);
      this.ProcessWaitingTasks(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理任务池。</summary>
    public void Shutdown()
    {
      this.RemoveAllTasks();
      while (this.FreeAgentCount > 0)
        this.m_FreeAgents.Pop().Shutdown();
    }

    /// <summary>增加任务代理。</summary>
    /// <param name="agent">要增加的任务代理。</param>
    public void AddAgent(ITaskAgent<T> agent)
    {
      if (agent == null)
        throw new GameFrameworkException("Task agent is invalid.");
      agent.Initialize();
      this.m_FreeAgents.Push(agent);
    }

    /// <summary>根据任务的序列编号获取任务的信息。</summary>
    /// <param name="serialId">要获取信息的任务的序列编号。</param>
    /// <returns>任务的信息。</returns>
    public TaskInfo GetTaskInfo(int serialId)
    {
      foreach (ITaskAgent<T> workingAgent in this.m_WorkingAgents)
      {
        T task = workingAgent.Task;
        if (task.SerialId == serialId)
          return new TaskInfo(task.SerialId, task.Tag, task.Priority, task.UserData, task.Done ? TaskStatus.Done : TaskStatus.Doing, task.Description);
      }
      foreach (T waitingTask in this.m_WaitingTasks)
      {
        if (waitingTask.SerialId == serialId)
          return new TaskInfo(waitingTask.SerialId, waitingTask.Tag, waitingTask.Priority, waitingTask.UserData, TaskStatus.Todo, waitingTask.Description);
      }
      return new TaskInfo();
    }

    /// <summary>根据任务的标签获取任务的信息。</summary>
    /// <param name="tag">要获取信息的任务的标签。</param>
    /// <returns>任务的信息。</returns>
    public TaskInfo[] GetTaskInfos(string tag)
    {
      List<TaskInfo> results = new List<TaskInfo>();
      this.GetTaskInfos(tag, results);
      return results.ToArray();
    }

    /// <summary>根据任务的标签获取任务的信息。</summary>
    /// <param name="tag">要获取信息的任务的标签。</param>
    /// <param name="results">任务的信息。</param>
    public void GetTaskInfos(string tag, List<TaskInfo> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (ITaskAgent<T> workingAgent in this.m_WorkingAgents)
      {
        T task = workingAgent.Task;
        if (task.Tag == tag)
          results.Add(new TaskInfo(task.SerialId, task.Tag, task.Priority, task.UserData, task.Done ? TaskStatus.Done : TaskStatus.Doing, task.Description));
      }
      foreach (T waitingTask in this.m_WaitingTasks)
      {
        if (waitingTask.Tag == tag)
          results.Add(new TaskInfo(waitingTask.SerialId, waitingTask.Tag, waitingTask.Priority, waitingTask.UserData, TaskStatus.Todo, waitingTask.Description));
      }
    }

    /// <summary>获取所有任务的信息。</summary>
    /// <returns>所有任务的信息。</returns>
    public TaskInfo[] GetAllTaskInfos()
    {
      int num = 0;
      TaskInfo[] allTaskInfos = new TaskInfo[this.m_WorkingAgents.Count + this.m_WaitingTasks.Count];
      foreach (ITaskAgent<T> workingAgent in this.m_WorkingAgents)
      {
        T task = workingAgent.Task;
        allTaskInfos[num++] = new TaskInfo(task.SerialId, task.Tag, task.Priority, task.UserData, task.Done ? TaskStatus.Done : TaskStatus.Doing, task.Description);
      }
      foreach (T waitingTask in this.m_WaitingTasks)
        allTaskInfos[num++] = new TaskInfo(waitingTask.SerialId, waitingTask.Tag, waitingTask.Priority, waitingTask.UserData, TaskStatus.Todo, waitingTask.Description);
      return allTaskInfos;
    }

    /// <summary>获取所有任务的信息。</summary>
    /// <param name="results">所有任务的信息。</param>
    public void GetAllTaskInfos(List<TaskInfo> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (ITaskAgent<T> workingAgent in this.m_WorkingAgents)
      {
        T task = workingAgent.Task;
        results.Add(new TaskInfo(task.SerialId, task.Tag, task.Priority, task.UserData, task.Done ? TaskStatus.Done : TaskStatus.Doing, task.Description));
      }
      foreach (T waitingTask in this.m_WaitingTasks)
        results.Add(new TaskInfo(waitingTask.SerialId, waitingTask.Tag, waitingTask.Priority, waitingTask.UserData, TaskStatus.Todo, waitingTask.Description));
    }

    /// <summary>增加任务。</summary>
    /// <param name="task">要增加的任务。</param>
    public void AddTask(T task)
    {
      LinkedListNode<T> node = this.m_WaitingTasks.Last;
      while (node != null && task.Priority > node.Value.Priority)
        node = node.Previous;
      if (node != null)
        this.m_WaitingTasks.AddAfter(node, task);
      else
        this.m_WaitingTasks.AddFirst(task);
    }

    /// <summary>根据任务的序列编号移除任务。</summary>
    /// <param name="serialId">要移除任务的序列编号。</param>
    /// <returns>是否移除任务成功。</returns>
    public bool RemoveTask(int serialId)
    {
      foreach (T waitingTask in this.m_WaitingTasks)
      {
        if (waitingTask.SerialId == serialId)
        {
          this.m_WaitingTasks.Remove(waitingTask);
          ReferencePool.Release((IReference) waitingTask);
          return true;
        }
      }
      LinkedListNode<ITaskAgent<T>> next;
      for (LinkedListNode<ITaskAgent<T>> node = this.m_WorkingAgents.First; node != null; node = next)
      {
        next = node.Next;
        ITaskAgent<T> taskAgent = node.Value;
        T task = taskAgent.Task;
        if (task.SerialId == serialId)
        {
          taskAgent.Reset();
          this.m_FreeAgents.Push(taskAgent);
          this.m_WorkingAgents.Remove(node);
          ReferencePool.Release((IReference) task);
          return true;
        }
      }
      return false;
    }

    /// <summary>根据任务的标签移除任务。</summary>
    /// <param name="tag">要移除任务的标签。</param>
    /// <returns>移除任务的数量。</returns>
    public int RemoveTasks(string tag)
    {
      int num = 0;
      LinkedListNode<T> next1;
      for (LinkedListNode<T> node = this.m_WaitingTasks.First; node != null; node = next1)
      {
        next1 = node.Next;
        T obj = node.Value;
        if (obj.Tag == tag)
        {
          this.m_WaitingTasks.Remove(node);
          ReferencePool.Release((IReference) obj);
          ++num;
        }
      }
      LinkedListNode<ITaskAgent<T>> next2;
      for (LinkedListNode<ITaskAgent<T>> node = this.m_WorkingAgents.First; node != null; node = next2)
      {
        next2 = node.Next;
        ITaskAgent<T> taskAgent = node.Value;
        T task = taskAgent.Task;
        if (task.Tag == tag)
        {
          taskAgent.Reset();
          this.m_FreeAgents.Push(taskAgent);
          this.m_WorkingAgents.Remove(node);
          ReferencePool.Release((IReference) task);
          ++num;
        }
      }
      return num;
    }

    /// <summary>移除所有任务。</summary>
    /// <returns>移除任务的数量。</returns>
    public int RemoveAllTasks()
    {
      int num = this.m_WaitingTasks.Count + this.m_WorkingAgents.Count;
      foreach (T waitingTask in this.m_WaitingTasks)
        ReferencePool.Release((IReference) waitingTask);
      this.m_WaitingTasks.Clear();
      foreach (ITaskAgent<T> workingAgent in this.m_WorkingAgents)
      {
        T task = workingAgent.Task;
        workingAgent.Reset();
        this.m_FreeAgents.Push(workingAgent);
        ReferencePool.Release((IReference) task);
      }
      this.m_WorkingAgents.Clear();
      return num;
    }

    private void ProcessRunningTasks(float elapseSeconds, float realElapseSeconds)
    {
      LinkedListNode<ITaskAgent<T>> node = this.m_WorkingAgents.First;
      while (node != null)
      {
        T task = node.Value.Task;
        if (!task.Done)
        {
          node.Value.Update(elapseSeconds, realElapseSeconds);
          node = node.Next;
        }
        else
        {
          LinkedListNode<ITaskAgent<T>> next = node.Next;
          node.Value.Reset();
          this.m_FreeAgents.Push(node.Value);
          this.m_WorkingAgents.Remove(node);
          ReferencePool.Release((IReference) task);
          node = next;
        }
      }
    }

    private void ProcessWaitingTasks(float elapseSeconds, float realElapseSeconds)
    {
      LinkedListNode<T> next;
      for (LinkedListNode<T> node1 = this.m_WaitingTasks.First; node1 != null && this.FreeAgentCount > 0; node1 = next)
      {
        ITaskAgent<T> taskAgent = this.m_FreeAgents.Pop();
        LinkedListNode<ITaskAgent<T>> node2 = this.m_WorkingAgents.AddLast(taskAgent);
        T task = node1.Value;
        next = node1.Next;
        StartTaskStatus startTaskStatus = taskAgent.Start(task);
        if (startTaskStatus == StartTaskStatus.Done || startTaskStatus == StartTaskStatus.HasToWait || startTaskStatus == StartTaskStatus.UnknownError)
        {
          taskAgent.Reset();
          this.m_FreeAgents.Push(taskAgent);
          this.m_WorkingAgents.Remove(node2);
        }
        if (startTaskStatus == StartTaskStatus.Done || startTaskStatus == StartTaskStatus.CanResume || startTaskStatus == StartTaskStatus.UnknownError)
          this.m_WaitingTasks.Remove(node1);
        if (startTaskStatus == StartTaskStatus.Done || startTaskStatus == StartTaskStatus.UnknownError)
          ReferencePool.Release((IReference) task);
      }
    }
  }
}
