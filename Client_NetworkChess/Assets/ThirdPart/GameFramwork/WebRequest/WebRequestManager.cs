// Decompiled with JetBrains decompiler
// Type: GameFramework.WebRequest.WebRequestManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.WebRequest
{
  /// <summary>Web 请求管理器。</summary>
  internal sealed class WebRequestManager : GameFrameworkModule, IWebRequestManager
  {
    private readonly TaskPool<WebRequestManager.WebRequestTask> m_TaskPool;
    private float m_Timeout;
    private EventHandler<WebRequestStartEventArgs> m_WebRequestStartEventHandler;
    private EventHandler<WebRequestSuccessEventArgs> m_WebRequestSuccessEventHandler;
    private EventHandler<WebRequestFailureEventArgs> m_WebRequestFailureEventHandler;

    /// <summary>初始化 Web 请求管理器的新实例。</summary>
    public WebRequestManager()
    {
      this.m_TaskPool = new TaskPool<WebRequestManager.WebRequestTask>();
      this.m_Timeout = 30f;
      this.m_WebRequestStartEventHandler = (EventHandler<WebRequestStartEventArgs>) null;
      this.m_WebRequestSuccessEventHandler = (EventHandler<WebRequestSuccessEventArgs>) null;
      this.m_WebRequestFailureEventHandler = (EventHandler<WebRequestFailureEventArgs>) null;
    }

    /// <summary>获取 Web 请求代理总数量。</summary>
    public int TotalAgentCount => this.m_TaskPool.TotalAgentCount;

    /// <summary>获取可用 Web 请求代理数量。</summary>
    public int FreeAgentCount => this.m_TaskPool.FreeAgentCount;

    /// <summary>获取工作中 Web 请求代理数量。</summary>
    public int WorkingAgentCount => this.m_TaskPool.WorkingAgentCount;

    /// <summary>获取等待 Web 请求数量。</summary>
    public int WaitingTaskCount => this.m_TaskPool.WaitingTaskCount;

    /// <summary>获取或设置 Web 请求超时时长，以秒为单位。</summary>
    public float Timeout
    {
      get => this.m_Timeout;
      set => this.m_Timeout = value;
    }

    /// <summary>Web 请求开始事件。</summary>
    public event EventHandler<WebRequestStartEventArgs> WebRequestStart
    {
      add => this.m_WebRequestStartEventHandler += value;
      remove => this.m_WebRequestStartEventHandler -= value;
    }

    /// <summary>Web 请求成功事件。</summary>
    public event EventHandler<WebRequestSuccessEventArgs> WebRequestSuccess
    {
      add => this.m_WebRequestSuccessEventHandler += value;
      remove => this.m_WebRequestSuccessEventHandler -= value;
    }

    /// <summary>Web 请求失败事件。</summary>
    public event EventHandler<WebRequestFailureEventArgs> WebRequestFailure
    {
      add => this.m_WebRequestFailureEventHandler += value;
      remove => this.m_WebRequestFailureEventHandler -= value;
    }

    /// <summary>Web 请求管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds) => this.m_TaskPool.Update(elapseSeconds, realElapseSeconds);

    /// <summary>关闭并清理 Web 请求管理器。</summary>
    internal override void Shutdown() => this.m_TaskPool.Shutdown();

    /// <summary>增加 Web 请求代理辅助器。</summary>
    /// <param name="webRequestAgentHelper">要增加的 Web 请求代理辅助器。</param>
    public void AddWebRequestAgentHelper(IWebRequestAgentHelper webRequestAgentHelper)
    {
      WebRequestManager.WebRequestAgent agent = new WebRequestManager.WebRequestAgent(webRequestAgentHelper);
      agent.WebRequestAgentStart += new GameFrameworkAction<WebRequestManager.WebRequestAgent>(this.OnWebRequestAgentStart);
      agent.WebRequestAgentSuccess += new GameFrameworkAction<WebRequestManager.WebRequestAgent, byte[]>(this.OnWebRequestAgentSuccess);
      agent.WebRequestAgentFailure += new GameFrameworkAction<WebRequestManager.WebRequestAgent, string>(this.OnWebRequestAgentFailure);
      this.m_TaskPool.AddAgent((ITaskAgent<WebRequestManager.WebRequestTask>) agent);
    }

    /// <summary>根据 Web 请求任务的序列编号获取 Web 请求任务的信息。</summary>
    /// <param name="serialId">要获取信息的 Web 请求任务的序列编号。</param>
    /// <returns>Web 请求任务的信息。</returns>
    public TaskInfo GetWebRequestInfo(int serialId) => this.m_TaskPool.GetTaskInfo(serialId);

    /// <summary>根据 Web 请求任务的标签获取 Web 请求任务的信息。</summary>
    /// <param name="tag">要获取信息的 Web 请求任务的标签。</param>
    /// <returns>Web 请求任务的信息。</returns>
    public TaskInfo[] GetWebRequestInfos(string tag) => this.m_TaskPool.GetTaskInfos(tag);

    /// <summary>根据 Web 请求任务的标签获取 Web 请求任务的信息。</summary>
    /// <param name="tag">要获取信息的 Web 请求任务的标签。</param>
    /// <param name="results">Web 请求任务的信息。</param>
    public void GetAllWebRequestInfos(string tag, List<TaskInfo> results) => this.m_TaskPool.GetTaskInfos(tag, results);

    /// <summary>获取所有 Web 请求任务的信息。</summary>
    /// <returns>所有 Web 请求任务的信息。</returns>
    public TaskInfo[] GetAllWebRequestInfos() => this.m_TaskPool.GetAllTaskInfos();

    /// <summary>获取所有 Web 请求任务的信息。</summary>
    /// <param name="results">所有 Web 请求任务的信息。</param>
    public void GetAllWebRequestInfos(List<TaskInfo> results) => this.m_TaskPool.GetAllTaskInfos(results);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri) => this.AddWebRequest(webRequestUri, (byte[]) null, (string) null, 0, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData) => this.AddWebRequest(webRequestUri, postData, (string) null, 0, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, string tag) => this.AddWebRequest(webRequestUri, (byte[]) null, tag, 0, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, int priority) => this.AddWebRequest(webRequestUri, (byte[]) null, (string) null, priority, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, object userData) => this.AddWebRequest(webRequestUri, (byte[]) null, (string) null, 0, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, string tag) => this.AddWebRequest(webRequestUri, postData, tag, 0, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, int priority) => this.AddWebRequest(webRequestUri, postData, (string) null, priority, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, object userData) => this.AddWebRequest(webRequestUri, postData, (string) null, 0, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, string tag, int priority) => this.AddWebRequest(webRequestUri, (byte[]) null, tag, priority, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, string tag, object userData) => this.AddWebRequest(webRequestUri, (byte[]) null, tag, 0, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, int priority, object userData) => this.AddWebRequest(webRequestUri, (byte[]) null, (string) null, priority, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, string tag, int priority) => this.AddWebRequest(webRequestUri, postData, tag, priority, (object) null);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, string tag, object userData) => this.AddWebRequest(webRequestUri, postData, tag, 0, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, byte[] postData, int priority, object userData) => this.AddWebRequest(webRequestUri, postData, (string) null, priority, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(string webRequestUri, string tag, int priority, object userData) => this.AddWebRequest(webRequestUri, (byte[]) null, tag, priority, userData);

    /// <summary>增加 Web 请求任务。</summary>
    /// <param name="webRequestUri">Web 请求地址。</param>
    /// <param name="postData">要发送的数据流。</param>
    /// <param name="tag">Web 请求任务的标签。</param>
    /// <param name="priority">Web 请求任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增 Web 请求任务的序列编号。</returns>
    public int AddWebRequest(
      string webRequestUri,
      byte[] postData,
      string tag,
      int priority,
      object userData)
    {
      if (string.IsNullOrEmpty(webRequestUri))
        throw new GameFrameworkException("Web request uri is invalid.");
      if (this.TotalAgentCount <= 0)
        throw new GameFrameworkException("You must add web request agent first.");
      WebRequestManager.WebRequestTask task = WebRequestManager.WebRequestTask.Create(webRequestUri, postData, tag, priority, this.m_Timeout, userData);
      this.m_TaskPool.AddTask(task);
      return task.SerialId;
    }

    /// <summary>根据 Web 请求任务的序列编号移除 Web 请求任务。</summary>
    /// <param name="serialId">要移除 Web 请求任务的序列编号。</param>
    /// <returns>是否移除 Web 请求任务成功。</returns>
    public bool RemoveWebRequest(int serialId) => this.m_TaskPool.RemoveTask(serialId);

    /// <summary>根据 Web 请求任务的标签移除 Web 请求任务。</summary>
    /// <param name="tag">要移除 Web 请求任务的标签。</param>
    /// <returns>移除 Web 请求任务的数量。</returns>
    public int RemoveWebRequests(string tag) => this.m_TaskPool.RemoveTasks(tag);

    /// <summary>移除所有 Web 请求任务。</summary>
    /// <returns>移除 Web 请求任务的数量。</returns>
    public int RemoveAllWebRequests() => this.m_TaskPool.RemoveAllTasks();

    private void OnWebRequestAgentStart(WebRequestManager.WebRequestAgent sender)
    {
      if (this.m_WebRequestStartEventHandler == null)
        return;
      WebRequestStartEventArgs e = WebRequestStartEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, sender.Task.UserData);
      this.m_WebRequestStartEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnWebRequestAgentSuccess(
      WebRequestManager.WebRequestAgent sender,
      byte[] webResponseBytes)
    {
      if (this.m_WebRequestSuccessEventHandler == null)
        return;
      WebRequestSuccessEventArgs e = WebRequestSuccessEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, webResponseBytes, sender.Task.UserData);
      this.m_WebRequestSuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnWebRequestAgentFailure(
      WebRequestManager.WebRequestAgent sender,
      string errorMessage)
    {
      if (this.m_WebRequestFailureEventHandler == null)
        return;
      WebRequestFailureEventArgs e = WebRequestFailureEventArgs.Create(sender.Task.SerialId, sender.Task.WebRequestUri, errorMessage, sender.Task.UserData);
      this.m_WebRequestFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    /// <summary>Web 请求代理。</summary>
    private sealed class WebRequestAgent : ITaskAgent<WebRequestManager.WebRequestTask>
    {
      private readonly IWebRequestAgentHelper m_Helper;
      private WebRequestManager.WebRequestTask m_Task;
      private float m_WaitTime;
      public GameFrameworkAction<WebRequestManager.WebRequestAgent> WebRequestAgentStart;
      public GameFrameworkAction<WebRequestManager.WebRequestAgent, byte[]> WebRequestAgentSuccess;
      public GameFrameworkAction<WebRequestManager.WebRequestAgent, string> WebRequestAgentFailure;

      /// <summary>初始化 Web 请求代理的新实例。</summary>
      /// <param name="webRequestAgentHelper">Web 请求代理辅助器。</param>
      public WebRequestAgent(IWebRequestAgentHelper webRequestAgentHelper)
      {
        this.m_Helper = webRequestAgentHelper != null ? webRequestAgentHelper : throw new GameFrameworkException("Web request agent helper is invalid.");
        this.m_Task = (WebRequestManager.WebRequestTask) null;
        this.m_WaitTime = 0.0f;
        this.WebRequestAgentStart = (GameFrameworkAction<WebRequestManager.WebRequestAgent>) null;
        this.WebRequestAgentSuccess = (GameFrameworkAction<WebRequestManager.WebRequestAgent, byte[]>) null;
        this.WebRequestAgentFailure = (GameFrameworkAction<WebRequestManager.WebRequestAgent, string>) null;
      }

      /// <summary>获取 Web 请求任务。</summary>
      public WebRequestManager.WebRequestTask Task => this.m_Task;

      /// <summary>获取已经等待时间。</summary>
      public float WaitTime => this.m_WaitTime;

      /// <summary>初始化 Web 请求代理。</summary>
      public void Initialize()
      {
        this.m_Helper.WebRequestAgentHelperComplete += new EventHandler<WebRequestAgentHelperCompleteEventArgs>(this.OnWebRequestAgentHelperComplete);
        this.m_Helper.WebRequestAgentHelperError += new EventHandler<WebRequestAgentHelperErrorEventArgs>(this.OnWebRequestAgentHelperError);
      }

      /// <summary>Web 请求代理轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (this.m_Task.Status != WebRequestManager.WebRequestTaskStatus.Doing)
          return;
        this.m_WaitTime += realElapseSeconds;
        if ((double) this.m_WaitTime < (double) this.m_Task.Timeout)
          return;
        WebRequestAgentHelperErrorEventArgs e = WebRequestAgentHelperErrorEventArgs.Create("Timeout");
        this.OnWebRequestAgentHelperError((object) this, e);
        ReferencePool.Release((IReference) e);
      }

      /// <summary>关闭并清理 Web 请求代理。</summary>
      public void Shutdown()
      {
        this.Reset();
        this.m_Helper.WebRequestAgentHelperComplete -= new EventHandler<WebRequestAgentHelperCompleteEventArgs>(this.OnWebRequestAgentHelperComplete);
        this.m_Helper.WebRequestAgentHelperError -= new EventHandler<WebRequestAgentHelperErrorEventArgs>(this.OnWebRequestAgentHelperError);
      }

      /// <summary>开始处理 Web 请求任务。</summary>
      /// <param name="task">要处理的 Web 请求任务。</param>
      /// <returns>开始处理任务的状态。</returns>
      public StartTaskStatus Start(WebRequestManager.WebRequestTask task)
      {
        this.m_Task = task != null ? task : throw new GameFrameworkException("Task is invalid.");
        this.m_Task.Status = WebRequestManager.WebRequestTaskStatus.Doing;
        if (this.WebRequestAgentStart != null)
          this.WebRequestAgentStart(this);
        byte[] postData = this.m_Task.GetPostData();
        if (postData == null)
          this.m_Helper.Request(this.m_Task.WebRequestUri, this.m_Task.UserData);
        else
          this.m_Helper.Request(this.m_Task.WebRequestUri, postData, this.m_Task.UserData);
        this.m_WaitTime = 0.0f;
        return StartTaskStatus.CanResume;
      }

      /// <summary>重置 Web 请求代理。</summary>
      public void Reset()
      {
        this.m_Helper.Reset();
        this.m_Task = (WebRequestManager.WebRequestTask) null;
        this.m_WaitTime = 0.0f;
      }

      private void OnWebRequestAgentHelperComplete(
        object sender,
        WebRequestAgentHelperCompleteEventArgs e)
      {
        this.m_Helper.Reset();
        this.m_Task.Status = WebRequestManager.WebRequestTaskStatus.Done;
        if (this.WebRequestAgentSuccess != null)
          this.WebRequestAgentSuccess(this, e.GetWebResponseBytes());
        this.m_Task.Done = true;
      }

      private void OnWebRequestAgentHelperError(
        object sender,
        WebRequestAgentHelperErrorEventArgs e)
      {
        this.m_Helper.Reset();
        this.m_Task.Status = WebRequestManager.WebRequestTaskStatus.Error;
        if (this.WebRequestAgentFailure != null)
          this.WebRequestAgentFailure(this, e.ErrorMessage);
        this.m_Task.Done = true;
      }
    }

    /// <summary>Web 请求任务。</summary>
    private sealed class WebRequestTask : TaskBase
    {
      private static int s_Serial;
      private WebRequestManager.WebRequestTaskStatus m_Status;
      private string m_WebRequestUri;
      private byte[] m_PostData;
      private float m_Timeout;

      public WebRequestTask()
      {
        this.m_Status = WebRequestManager.WebRequestTaskStatus.Todo;
        this.m_WebRequestUri = (string) null;
        this.m_PostData = (byte[]) null;
        this.m_Timeout = 0.0f;
      }

      /// <summary>获取或设置 Web 请求任务的状态。</summary>
      public WebRequestManager.WebRequestTaskStatus Status
      {
        get => this.m_Status;
        set => this.m_Status = value;
      }

      /// <summary>获取要发送的远程地址。</summary>
      public string WebRequestUri => this.m_WebRequestUri;

      /// <summary>获取 Web 请求超时时长，以秒为单位。</summary>
      public float Timeout => this.m_Timeout;

      /// <summary>获取 Web 请求任务的描述。</summary>
      public override string Description => this.m_WebRequestUri;

      /// <summary>创建 Web 请求任务。</summary>
      /// <param name="webRequestUri">要发送的远程地址。</param>
      /// <param name="postData">要发送的数据流。</param>
      /// <param name="tag">Web 请求任务的标签。</param>
      /// <param name="priority">Web 请求任务的优先级。</param>
      /// <param name="timeout">下载超时时长，以秒为单位。</param>
      /// <param name="userData">用户自定义数据。</param>
      /// <returns>创建的 Web 请求任务。</returns>
      public static WebRequestManager.WebRequestTask Create(
        string webRequestUri,
        byte[] postData,
        string tag,
        int priority,
        float timeout,
        object userData)
      {
        WebRequestManager.WebRequestTask webRequestTask = ReferencePool.Acquire<WebRequestManager.WebRequestTask>();
        int serialId;
        WebRequestManager.WebRequestTask.s_Serial = serialId = WebRequestManager.WebRequestTask.s_Serial + 1;
        string tag1 = tag;
        int priority1 = priority;
        object userData1 = userData;
        webRequestTask.Initialize(serialId, tag1, priority1, userData1);
        webRequestTask.m_WebRequestUri = webRequestUri;
        webRequestTask.m_PostData = postData;
        webRequestTask.m_Timeout = timeout;
        return webRequestTask;
      }

      /// <summary>清理 Web 请求任务。</summary>
      public override void Clear()
      {
        base.Clear();
        this.m_Status = WebRequestManager.WebRequestTaskStatus.Todo;
        this.m_WebRequestUri = (string) null;
        this.m_PostData = (byte[]) null;
        this.m_Timeout = 0.0f;
      }

      /// <summary>获取要发送的数据流。</summary>
      public byte[] GetPostData() => this.m_PostData;
    }

    /// <summary>Web 请求任务的状态。</summary>
    private enum WebRequestTaskStatus : byte
    {
      /// <summary>准备请求。</summary>
      Todo,
      /// <summary>请求中。</summary>
      Doing,
      /// <summary>请求完成。</summary>
      Done,
      /// <summary>请求错误。</summary>
      Error,
    }
  }
}
