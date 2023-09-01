// Decompiled with JetBrains decompiler
// Type: GameFramework.Download.DownloadManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Download
{
  /// <summary>下载管理器。</summary>
  internal sealed class DownloadManager : GameFrameworkModule, IDownloadManager
  {
    private const int OneMegaBytes = 1048576;
    private readonly TaskPool<DownloadManager.DownloadTask> m_TaskPool;
    private readonly DownloadManager.DownloadCounter m_DownloadCounter;
    private int m_FlushSize;
    private float m_Timeout;
    private EventHandler<DownloadStartEventArgs> m_DownloadStartEventHandler;
    private EventHandler<DownloadUpdateEventArgs> m_DownloadUpdateEventHandler;
    private EventHandler<DownloadSuccessEventArgs> m_DownloadSuccessEventHandler;
    private EventHandler<DownloadFailureEventArgs> m_DownloadFailureEventHandler;

    /// <summary>初始化下载管理器的新实例。</summary>
    public DownloadManager()
    {
      this.m_TaskPool = new TaskPool<DownloadManager.DownloadTask>();
      this.m_DownloadCounter = new DownloadManager.DownloadCounter(1f, 10f);
      this.m_FlushSize = 1048576;
      this.m_Timeout = 30f;
      this.m_DownloadStartEventHandler = (EventHandler<DownloadStartEventArgs>) null;
      this.m_DownloadUpdateEventHandler = (EventHandler<DownloadUpdateEventArgs>) null;
      this.m_DownloadSuccessEventHandler = (EventHandler<DownloadSuccessEventArgs>) null;
      this.m_DownloadFailureEventHandler = (EventHandler<DownloadFailureEventArgs>) null;
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 5;

    /// <summary>获取或设置下载是否被暂停。</summary>
    public bool Paused
    {
      get => this.m_TaskPool.Paused;
      set => this.m_TaskPool.Paused = value;
    }

    /// <summary>获取下载代理总数量。</summary>
    public int TotalAgentCount => this.m_TaskPool.TotalAgentCount;

    /// <summary>获取可用下载代理数量。</summary>
    public int FreeAgentCount => this.m_TaskPool.FreeAgentCount;

    /// <summary>获取工作中下载代理数量。</summary>
    public int WorkingAgentCount => this.m_TaskPool.WorkingAgentCount;

    /// <summary>获取等待下载任务数量。</summary>
    public int WaitingTaskCount => this.m_TaskPool.WaitingTaskCount;

    /// <summary>获取或设置将缓冲区写入磁盘的临界大小。</summary>
    public int FlushSize
    {
      get => this.m_FlushSize;
      set => this.m_FlushSize = value;
    }

    /// <summary>获取或设置下载超时时长，以秒为单位。</summary>
    public float Timeout
    {
      get => this.m_Timeout;
      set => this.m_Timeout = value;
    }

    /// <summary>获取当前下载速度。</summary>
    public float CurrentSpeed => this.m_DownloadCounter.CurrentSpeed;

    /// <summary>下载开始事件。</summary>
    public event EventHandler<DownloadStartEventArgs> DownloadStart
    {
      add => this.m_DownloadStartEventHandler += value;
      remove => this.m_DownloadStartEventHandler -= value;
    }

    /// <summary>下载更新事件。</summary>
    public event EventHandler<DownloadUpdateEventArgs> DownloadUpdate
    {
      add => this.m_DownloadUpdateEventHandler += value;
      remove => this.m_DownloadUpdateEventHandler -= value;
    }

    /// <summary>下载成功事件。</summary>
    public event EventHandler<DownloadSuccessEventArgs> DownloadSuccess
    {
      add => this.m_DownloadSuccessEventHandler += value;
      remove => this.m_DownloadSuccessEventHandler -= value;
    }

    /// <summary>下载失败事件。</summary>
    public event EventHandler<DownloadFailureEventArgs> DownloadFailure
    {
      add => this.m_DownloadFailureEventHandler += value;
      remove => this.m_DownloadFailureEventHandler -= value;
    }

    /// <summary>下载管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      this.m_TaskPool.Update(elapseSeconds, realElapseSeconds);
      this.m_DownloadCounter.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理下载管理器。</summary>
    internal override void Shutdown()
    {
      this.m_TaskPool.Shutdown();
      this.m_DownloadCounter.Shutdown();
    }

    /// <summary>增加下载代理辅助器。</summary>
    /// <param name="downloadAgentHelper">要增加的下载代理辅助器。</param>
    public void AddDownloadAgentHelper(IDownloadAgentHelper downloadAgentHelper)
    {
      DownloadManager.DownloadAgent agent = new DownloadManager.DownloadAgent(downloadAgentHelper);
      agent.DownloadAgentStart += new GameFrameworkAction<DownloadManager.DownloadAgent>(this.OnDownloadAgentStart);
      agent.DownloadAgentUpdate += new GameFrameworkAction<DownloadManager.DownloadAgent, int>(this.OnDownloadAgentUpdate);
      agent.DownloadAgentSuccess += new GameFrameworkAction<DownloadManager.DownloadAgent, long>(this.OnDownloadAgentSuccess);
      agent.DownloadAgentFailure += new GameFrameworkAction<DownloadManager.DownloadAgent, string>(this.OnDownloadAgentFailure);
      this.m_TaskPool.AddAgent((ITaskAgent<DownloadManager.DownloadTask>) agent);
    }

    /// <summary>根据下载任务的序列编号获取下载任务的信息。</summary>
    /// <param name="serialId">要获取信息的下载任务的序列编号。</param>
    /// <returns>下载任务的信息。</returns>
    public TaskInfo GetDownloadInfo(int serialId) => this.m_TaskPool.GetTaskInfo(serialId);

    /// <summary>根据下载任务的标签获取下载任务的信息。</summary>
    /// <param name="tag">要获取信息的下载任务的标签。</param>
    /// <returns>下载任务的信息。</returns>
    public TaskInfo[] GetDownloadInfos(string tag) => this.m_TaskPool.GetTaskInfos(tag);

    /// <summary>根据下载任务的标签获取下载任务的信息。</summary>
    /// <param name="tag">要获取信息的下载任务的标签。</param>
    /// <param name="results">下载任务的信息。</param>
    public void GetDownloadInfos(string tag, List<TaskInfo> results) => this.m_TaskPool.GetTaskInfos(tag, results);

    /// <summary>获取所有下载任务的信息。</summary>
    /// <returns>所有下载任务的信息。</returns>
    public TaskInfo[] GetAllDownloadInfos() => this.m_TaskPool.GetAllTaskInfos();

    /// <summary>获取所有下载任务的信息。</summary>
    /// <param name="results">所有下载任务的信息。</param>
    public void GetAllDownloadInfos(List<TaskInfo> results) => this.m_TaskPool.GetAllTaskInfos(results);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri) => this.AddDownload(downloadPath, downloadUri, (string) null, 0, (object) null);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="tag">下载任务的标签。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, string tag) => this.AddDownload(downloadPath, downloadUri, tag, 0, (object) null);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="priority">下载任务的优先级。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, int priority) => this.AddDownload(downloadPath, downloadUri, (string) null, priority, (object) null);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, object userData) => this.AddDownload(downloadPath, downloadUri, (string) null, 0, userData);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="tag">下载任务的标签。</param>
    /// <param name="priority">下载任务的优先级。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, string tag, int priority) => this.AddDownload(downloadPath, downloadUri, tag, priority, (object) null);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="tag">下载任务的标签。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, string tag, object userData) => this.AddDownload(downloadPath, downloadUri, tag, 0, userData);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="priority">下载任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(string downloadPath, string downloadUri, int priority, object userData) => this.AddDownload(downloadPath, downloadUri, (string) null, priority, userData);

    /// <summary>增加下载任务。</summary>
    /// <param name="downloadPath">下载后存放路径。</param>
    /// <param name="downloadUri">原始下载地址。</param>
    /// <param name="tag">下载任务的标签。</param>
    /// <param name="priority">下载任务的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>新增下载任务的序列编号。</returns>
    public int AddDownload(
      string downloadPath,
      string downloadUri,
      string tag,
      int priority,
      object userData)
    {
      if (string.IsNullOrEmpty(downloadPath))
        throw new GameFrameworkException("Download path is invalid.");
      if (string.IsNullOrEmpty(downloadUri))
        throw new GameFrameworkException("Download uri is invalid.");
      if (this.TotalAgentCount <= 0)
        throw new GameFrameworkException("You must add download agent first.");
      DownloadManager.DownloadTask task = DownloadManager.DownloadTask.Create(downloadPath, downloadUri, tag, priority, this.m_FlushSize, this.m_Timeout, userData);
      this.m_TaskPool.AddTask(task);
      return task.SerialId;
    }

    /// <summary>根据下载任务的序列编号移除下载任务。</summary>
    /// <param name="serialId">要移除下载任务的序列编号。</param>
    /// <returns>是否移除下载任务成功。</returns>
    public bool RemoveDownload(int serialId) => this.m_TaskPool.RemoveTask(serialId);

    /// <summary>根据下载任务的标签移除下载任务。</summary>
    /// <param name="tag">要移除下载任务的标签。</param>
    /// <returns>移除下载任务的数量。</returns>
    public int RemoveDownloads(string tag) => this.m_TaskPool.RemoveTasks(tag);

    /// <summary>移除所有下载任务。</summary>
    /// <returns>移除下载任务的数量。</returns>
    public int RemoveAllDownloads() => this.m_TaskPool.RemoveAllTasks();

    private void OnDownloadAgentStart(DownloadManager.DownloadAgent sender)
    {
      if (this.m_DownloadStartEventHandler == null)
        return;
      DownloadStartEventArgs e = DownloadStartEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
      this.m_DownloadStartEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnDownloadAgentUpdate(DownloadManager.DownloadAgent sender, int deltaLength)
    {
      this.m_DownloadCounter.RecordDeltaLength(deltaLength);
      if (this.m_DownloadUpdateEventHandler == null)
        return;
      DownloadUpdateEventArgs e = DownloadUpdateEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
      this.m_DownloadUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnDownloadAgentSuccess(DownloadManager.DownloadAgent sender, long length)
    {
      if (this.m_DownloadSuccessEventHandler == null)
        return;
      DownloadSuccessEventArgs e = DownloadSuccessEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, sender.CurrentLength, sender.Task.UserData);
      this.m_DownloadSuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void OnDownloadAgentFailure(DownloadManager.DownloadAgent sender, string errorMessage)
    {
      if (this.m_DownloadFailureEventHandler == null)
        return;
      DownloadFailureEventArgs e = DownloadFailureEventArgs.Create(sender.Task.SerialId, sender.Task.DownloadPath, sender.Task.DownloadUri, errorMessage, sender.Task.UserData);
      this.m_DownloadFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    /// <summary>下载代理。</summary>
    private sealed class DownloadAgent : ITaskAgent<DownloadManager.DownloadTask>, IDisposable
    {
      private readonly IDownloadAgentHelper m_Helper;
      private DownloadManager.DownloadTask m_Task;
      private FileStream m_FileStream;
      private int m_WaitFlushSize;
      private float m_WaitTime;
      private long m_StartLength;
      private long m_DownloadedLength;
      private long m_SavedLength;
      private bool m_Disposed;
      public GameFrameworkAction<DownloadManager.DownloadAgent> DownloadAgentStart;
      public GameFrameworkAction<DownloadManager.DownloadAgent, int> DownloadAgentUpdate;
      public GameFrameworkAction<DownloadManager.DownloadAgent, long> DownloadAgentSuccess;
      public GameFrameworkAction<DownloadManager.DownloadAgent, string> DownloadAgentFailure;

      /// <summary>初始化下载代理的新实例。</summary>
      /// <param name="downloadAgentHelper">下载代理辅助器。</param>
      public DownloadAgent(IDownloadAgentHelper downloadAgentHelper)
      {
        this.m_Helper = downloadAgentHelper != null ? downloadAgentHelper : throw new GameFrameworkException("Download agent helper is invalid.");
        this.m_Task = (DownloadManager.DownloadTask) null;
        this.m_FileStream = (FileStream) null;
        this.m_WaitFlushSize = 0;
        this.m_WaitTime = 0.0f;
        this.m_StartLength = 0L;
        this.m_DownloadedLength = 0L;
        this.m_SavedLength = 0L;
        this.m_Disposed = false;
        this.DownloadAgentStart = (GameFrameworkAction<DownloadManager.DownloadAgent>) null;
        this.DownloadAgentUpdate = (GameFrameworkAction<DownloadManager.DownloadAgent, int>) null;
        this.DownloadAgentSuccess = (GameFrameworkAction<DownloadManager.DownloadAgent, long>) null;
        this.DownloadAgentFailure = (GameFrameworkAction<DownloadManager.DownloadAgent, string>) null;
      }

      /// <summary>获取下载任务。</summary>
      public DownloadManager.DownloadTask Task => this.m_Task;

      /// <summary>获取已经等待时间。</summary>
      public float WaitTime => this.m_WaitTime;

      /// <summary>获取开始下载时已经存在的大小。</summary>
      public long StartLength => this.m_StartLength;

      /// <summary>获取本次已经下载的大小。</summary>
      public long DownloadedLength => this.m_DownloadedLength;

      /// <summary>获取当前的大小。</summary>
      public long CurrentLength => this.m_StartLength + this.m_DownloadedLength;

      /// <summary>获取已经存盘的大小。</summary>
      public long SavedLength => this.m_SavedLength;

      /// <summary>初始化下载代理。</summary>
      public void Initialize()
      {
        this.m_Helper.DownloadAgentHelperUpdateBytes += new EventHandler<DownloadAgentHelperUpdateBytesEventArgs>(this.OnDownloadAgentHelperUpdateBytes);
        this.m_Helper.DownloadAgentHelperUpdateLength += new EventHandler<DownloadAgentHelperUpdateLengthEventArgs>(this.OnDownloadAgentHelperUpdateLength);
        this.m_Helper.DownloadAgentHelperComplete += new EventHandler<DownloadAgentHelperCompleteEventArgs>(this.OnDownloadAgentHelperComplete);
        this.m_Helper.DownloadAgentHelperError += new EventHandler<DownloadAgentHelperErrorEventArgs>(this.OnDownloadAgentHelperError);
      }

      /// <summary>下载代理轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (this.m_Task.Status != DownloadManager.DownloadTaskStatus.Doing)
          return;
        this.m_WaitTime += realElapseSeconds;
        if ((double) this.m_WaitTime < (double) this.m_Task.Timeout)
          return;
        DownloadAgentHelperErrorEventArgs e = DownloadAgentHelperErrorEventArgs.Create(false, "Timeout");
        this.OnDownloadAgentHelperError((object) this, e);
        ReferencePool.Release((IReference) e);
      }

      /// <summary>关闭并清理下载代理。</summary>
      public void Shutdown()
      {
        this.Dispose();
        this.m_Helper.DownloadAgentHelperUpdateBytes -= new EventHandler<DownloadAgentHelperUpdateBytesEventArgs>(this.OnDownloadAgentHelperUpdateBytes);
        this.m_Helper.DownloadAgentHelperUpdateLength -= new EventHandler<DownloadAgentHelperUpdateLengthEventArgs>(this.OnDownloadAgentHelperUpdateLength);
        this.m_Helper.DownloadAgentHelperComplete -= new EventHandler<DownloadAgentHelperCompleteEventArgs>(this.OnDownloadAgentHelperComplete);
        this.m_Helper.DownloadAgentHelperError -= new EventHandler<DownloadAgentHelperErrorEventArgs>(this.OnDownloadAgentHelperError);
      }

      /// <summary>开始处理下载任务。</summary>
      /// <param name="task">要处理的下载任务。</param>
      /// <returns>开始处理任务的状态。</returns>
      public StartTaskStatus Start(DownloadManager.DownloadTask task)
      {
        this.m_Task = task != null ? task : throw new GameFrameworkException("Task is invalid.");
        this.m_Task.Status = DownloadManager.DownloadTaskStatus.Doing;
        string path = Utility.Text.Format<string>("{0}.download", this.m_Task.DownloadPath);
        try
        {
          if (File.Exists(path))
          {
            this.m_FileStream = File.OpenWrite(path);
            this.m_FileStream.Seek(0L, SeekOrigin.End);
            this.m_StartLength = this.m_SavedLength = this.m_FileStream.Length;
            this.m_DownloadedLength = 0L;
          }
          else
          {
            string directoryName = System.IO.Path.GetDirectoryName(this.m_Task.DownloadPath);
            if (!Directory.Exists(directoryName))
              Directory.CreateDirectory(directoryName);
            this.m_FileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            this.m_StartLength = this.m_SavedLength = this.m_DownloadedLength = 0L;
          }
          if (this.DownloadAgentStart != null)
            this.DownloadAgentStart(this);
          if (this.m_StartLength > 0L)
            this.m_Helper.Download(this.m_Task.DownloadUri, this.m_StartLength, this.m_Task.UserData);
          else
            this.m_Helper.Download(this.m_Task.DownloadUri, this.m_Task.UserData);
          return StartTaskStatus.CanResume;
        }
        catch (Exception ex)
        {
          DownloadAgentHelperErrorEventArgs e = DownloadAgentHelperErrorEventArgs.Create(false, ex.ToString());
          this.OnDownloadAgentHelperError((object) this, e);
          ReferencePool.Release((IReference) e);
          return StartTaskStatus.UnknownError;
        }
      }

      /// <summary>重置下载代理。</summary>
      public void Reset()
      {
        this.m_Helper.Reset();
        if (this.m_FileStream != null)
        {
          this.m_FileStream.Close();
          this.m_FileStream = (FileStream) null;
        }
        this.m_Task = (DownloadManager.DownloadTask) null;
        this.m_WaitFlushSize = 0;
        this.m_WaitTime = 0.0f;
        this.m_StartLength = 0L;
        this.m_DownloadedLength = 0L;
        this.m_SavedLength = 0L;
      }

      /// <summary>释放资源。</summary>
      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      /// <summary>释放资源。</summary>
      /// <param name="disposing">释放资源标记。</param>
      private void Dispose(bool disposing)
      {
        if (this.m_Disposed)
          return;
        if (disposing && this.m_FileStream != null)
        {
          this.m_FileStream.Dispose();
          this.m_FileStream = (FileStream) null;
        }
        this.m_Disposed = true;
      }

      private void OnDownloadAgentHelperUpdateBytes(
        object sender,
        DownloadAgentHelperUpdateBytesEventArgs e)
      {
        this.m_WaitTime = 0.0f;
        try
        {
          this.m_FileStream.Write(e.GetBytes(), e.Offset, e.Length);
          this.m_WaitFlushSize += e.Length;
          this.m_SavedLength += (long) e.Length;
          if (this.m_WaitFlushSize < this.m_Task.FlushSize)
            return;
          this.m_FileStream.Flush();
          this.m_WaitFlushSize = 0;
        }
        catch (Exception ex)
        {
          DownloadAgentHelperErrorEventArgs e1 = DownloadAgentHelperErrorEventArgs.Create(false, ex.ToString());
          this.OnDownloadAgentHelperError((object) this, e1);
          ReferencePool.Release((IReference) e1);
        }
      }

      private void OnDownloadAgentHelperUpdateLength(
        object sender,
        DownloadAgentHelperUpdateLengthEventArgs e)
      {
        this.m_WaitTime = 0.0f;
        this.m_DownloadedLength += (long) e.DeltaLength;
        if (this.DownloadAgentUpdate == null)
          return;
        this.DownloadAgentUpdate(this, e.DeltaLength);
      }

      private void OnDownloadAgentHelperComplete(
        object sender,
        DownloadAgentHelperCompleteEventArgs e)
      {
        this.m_WaitTime = 0.0f;
        this.m_DownloadedLength = e.Length;
        if (this.m_SavedLength != this.CurrentLength)
          throw new GameFrameworkException("Internal download error.");
        this.m_Helper.Reset();
        this.m_FileStream.Close();
        this.m_FileStream = (FileStream) null;
        if (File.Exists(this.m_Task.DownloadPath))
          File.Delete(this.m_Task.DownloadPath);
        File.Move(Utility.Text.Format<string>("{0}.download", this.m_Task.DownloadPath), this.m_Task.DownloadPath);
        this.m_Task.Status = DownloadManager.DownloadTaskStatus.Done;
        if (this.DownloadAgentSuccess != null)
          this.DownloadAgentSuccess(this, e.Length);
        this.m_Task.Done = true;
      }

      private void OnDownloadAgentHelperError(object sender, DownloadAgentHelperErrorEventArgs e)
      {
        this.m_Helper.Reset();
        if (this.m_FileStream != null)
        {
          this.m_FileStream.Close();
          this.m_FileStream = (FileStream) null;
        }
        if (e.DeleteDownloading)
          File.Delete(Utility.Text.Format<string>("{0}.download", this.m_Task.DownloadPath));
        this.m_Task.Status = DownloadManager.DownloadTaskStatus.Error;
        if (this.DownloadAgentFailure != null)
          this.DownloadAgentFailure(this, e.ErrorMessage);
        this.m_Task.Done = true;
      }
    }

    private sealed class DownloadCounter
    {
      private readonly GameFrameworkLinkedList<DownloadManager.DownloadCounter.DownloadCounterNode> m_DownloadCounterNodes;
      private float m_UpdateInterval;
      private float m_RecordInterval;
      private float m_CurrentSpeed;
      private float m_Accumulator;
      private float m_TimeLeft;

      public DownloadCounter(float updateInterval, float recordInterval)
      {
        if ((double) updateInterval <= 0.0)
          throw new GameFrameworkException("Update interval is invalid.");
        if ((double) recordInterval <= 0.0)
          throw new GameFrameworkException("Record interval is invalid.");
        this.m_DownloadCounterNodes = new GameFrameworkLinkedList<DownloadManager.DownloadCounter.DownloadCounterNode>();
        this.m_UpdateInterval = updateInterval;
        this.m_RecordInterval = recordInterval;
        this.Reset();
      }

      public float UpdateInterval
      {
        get => this.m_UpdateInterval;
        set
        {
          this.m_UpdateInterval = (double) value > 0.0 ? value : throw new GameFrameworkException("Update interval is invalid.");
          this.Reset();
        }
      }

      public float RecordInterval
      {
        get => this.m_RecordInterval;
        set
        {
          this.m_RecordInterval = (double) value > 0.0 ? value : throw new GameFrameworkException("Record interval is invalid.");
          this.Reset();
        }
      }

      public float CurrentSpeed => this.m_CurrentSpeed;

      public void Shutdown() => this.Reset();

      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        if (this.m_DownloadCounterNodes.Count <= 0)
          return;
        this.m_Accumulator += realElapseSeconds;
        if ((double) this.m_Accumulator > (double) this.m_RecordInterval)
          this.m_Accumulator = this.m_RecordInterval;
        this.m_TimeLeft -= realElapseSeconds;
        foreach (DownloadManager.DownloadCounter.DownloadCounterNode downloadCounterNode in this.m_DownloadCounterNodes)
          downloadCounterNode.Update(elapseSeconds, realElapseSeconds);
        while (this.m_DownloadCounterNodes.Count > 0)
        {
          DownloadManager.DownloadCounter.DownloadCounterNode downloadCounterNode = this.m_DownloadCounterNodes.First.Value;
          if ((double) downloadCounterNode.ElapseSeconds >= (double) this.m_RecordInterval)
          {
            ReferencePool.Release((IReference) downloadCounterNode);
            this.m_DownloadCounterNodes.RemoveFirst();
          }
          else
            break;
        }
        if (this.m_DownloadCounterNodes.Count <= 0)
        {
          this.Reset();
        }
        else
        {
          if ((double) this.m_TimeLeft > 0.0)
            return;
          long num = 0;
          foreach (DownloadManager.DownloadCounter.DownloadCounterNode downloadCounterNode in this.m_DownloadCounterNodes)
            num += downloadCounterNode.DeltaLength;
          this.m_CurrentSpeed = (double) this.m_Accumulator > 0.0 ? (float) num / this.m_Accumulator : 0.0f;
          this.m_TimeLeft += this.m_UpdateInterval;
        }
      }

      public void RecordDeltaLength(int deltaLength)
      {
        if (deltaLength <= 0)
          return;
        if (this.m_DownloadCounterNodes.Count > 0)
        {
          DownloadManager.DownloadCounter.DownloadCounterNode downloadCounterNode = this.m_DownloadCounterNodes.Last.Value;
          if ((double) downloadCounterNode.ElapseSeconds < (double) this.m_UpdateInterval)
          {
            downloadCounterNode.AddDeltaLength(deltaLength);
            return;
          }
        }
        DownloadManager.DownloadCounter.DownloadCounterNode downloadCounterNode1 = DownloadManager.DownloadCounter.DownloadCounterNode.Create();
        downloadCounterNode1.AddDeltaLength(deltaLength);
        this.m_DownloadCounterNodes.AddLast(downloadCounterNode1);
      }

      private void Reset()
      {
        this.m_DownloadCounterNodes.Clear();
        this.m_CurrentSpeed = 0.0f;
        this.m_Accumulator = 0.0f;
        this.m_TimeLeft = 0.0f;
      }

      private sealed class DownloadCounterNode : IReference
      {
        private long m_DeltaLength;
        private float m_ElapseSeconds;

        public DownloadCounterNode()
        {
          this.m_DeltaLength = 0L;
          this.m_ElapseSeconds = 0.0f;
        }

        public long DeltaLength => this.m_DeltaLength;

        public float ElapseSeconds => this.m_ElapseSeconds;

        public static DownloadManager.DownloadCounter.DownloadCounterNode Create() => ReferencePool.Acquire<DownloadManager.DownloadCounter.DownloadCounterNode>();

        public void Update(float elapseSeconds, float realElapseSeconds) => this.m_ElapseSeconds += realElapseSeconds;

        public void AddDeltaLength(int deltaLength) => this.m_DeltaLength += (long) deltaLength;

        public void Clear()
        {
          this.m_DeltaLength = 0L;
          this.m_ElapseSeconds = 0.0f;
        }
      }
    }

    /// <summary>下载任务。</summary>
    private sealed class DownloadTask : TaskBase
    {
      private static int s_Serial;
      private DownloadManager.DownloadTaskStatus m_Status;
      private string m_DownloadPath;
      private string m_DownloadUri;
      private int m_FlushSize;
      private float m_Timeout;

      /// <summary>初始化下载任务的新实例。</summary>
      public DownloadTask()
      {
        this.m_Status = DownloadManager.DownloadTaskStatus.Todo;
        this.m_DownloadPath = (string) null;
        this.m_DownloadUri = (string) null;
        this.m_FlushSize = 0;
        this.m_Timeout = 0.0f;
      }

      /// <summary>获取或设置下载任务的状态。</summary>
      public DownloadManager.DownloadTaskStatus Status
      {
        get => this.m_Status;
        set => this.m_Status = value;
      }

      /// <summary>获取下载后存放路径。</summary>
      public string DownloadPath => this.m_DownloadPath;

      /// <summary>获取原始下载地址。</summary>
      public string DownloadUri => this.m_DownloadUri;

      /// <summary>获取将缓冲区写入磁盘的临界大小。</summary>
      public int FlushSize => this.m_FlushSize;

      /// <summary>获取下载超时时长，以秒为单位。</summary>
      public float Timeout => this.m_Timeout;

      /// <summary>获取下载任务的描述。</summary>
      public override string Description => this.m_DownloadPath;

      /// <summary>创建下载任务。</summary>
      /// <param name="downloadPath">下载后存放路径。</param>
      /// <param name="downloadUri">原始下载地址。</param>
      /// <param name="tag">下载任务的标签。</param>
      /// <param name="priority">下载任务的优先级。</param>
      /// <param name="flushSize">将缓冲区写入磁盘的临界大小。</param>
      /// <param name="timeout">下载超时时长，以秒为单位。</param>
      /// <param name="userData">用户自定义数据。</param>
      /// <returns>创建的下载任务。</returns>
      public static DownloadManager.DownloadTask Create(
        string downloadPath,
        string downloadUri,
        string tag,
        int priority,
        int flushSize,
        float timeout,
        object userData)
      {
        DownloadManager.DownloadTask downloadTask = ReferencePool.Acquire<DownloadManager.DownloadTask>();
        int serialId;
        DownloadManager.DownloadTask.s_Serial = serialId = DownloadManager.DownloadTask.s_Serial + 1;
        string tag1 = tag;
        int priority1 = priority;
        object userData1 = userData;
        downloadTask.Initialize(serialId, tag1, priority1, userData1);
        downloadTask.m_DownloadPath = downloadPath;
        downloadTask.m_DownloadUri = downloadUri;
        downloadTask.m_FlushSize = flushSize;
        downloadTask.m_Timeout = timeout;
        return downloadTask;
      }

      /// <summary>清理下载任务。</summary>
      public override void Clear()
      {
        base.Clear();
        this.m_Status = DownloadManager.DownloadTaskStatus.Todo;
        this.m_DownloadPath = (string) null;
        this.m_DownloadUri = (string) null;
        this.m_FlushSize = 0;
        this.m_Timeout = 0.0f;
      }
    }

    /// <summary>下载任务的状态。</summary>
    private enum DownloadTaskStatus : byte
    {
      /// <summary>准备下载。</summary>
      Todo,
      /// <summary>下载中。</summary>
      Doing,
      /// <summary>下载完成。</summary>
      Done,
      /// <summary>下载错误。</summary>
      Error,
    }
  }
}
