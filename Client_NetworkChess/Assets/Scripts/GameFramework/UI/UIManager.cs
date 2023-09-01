// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.UIManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.ObjectPool;
using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.UI
{
  /// <summary>界面管理器。</summary>
  internal sealed class UIManager : GameFrameworkModule, IUIManager
  {
    private readonly Dictionary<string, UIManager.UIGroup> m_UIGroups;
    private readonly Dictionary<int, string> m_UIFormsBeingLoaded;
    private readonly HashSet<int> m_UIFormsToReleaseOnLoad;
    private readonly Queue<IUIForm> m_RecycleQueue;
    private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
    private IObjectPoolManager m_ObjectPoolManager;
    private IResourceManager m_ResourceManager;
    private IObjectPool<UIManager.UIFormInstanceObject> m_InstancePool;
    private IUIFormHelper m_UIFormHelper;
    private int m_Serial;
    private bool m_IsShutdown;
    private EventHandler<OpenUIFormSuccessEventArgs> m_OpenUIFormSuccessEventHandler;
    private EventHandler<OpenUIFormFailureEventArgs> m_OpenUIFormFailureEventHandler;
    private EventHandler<OpenUIFormUpdateEventArgs> m_OpenUIFormUpdateEventHandler;
    private EventHandler<OpenUIFormDependencyAssetEventArgs> m_OpenUIFormDependencyAssetEventHandler;
    private EventHandler<CloseUIFormCompleteEventArgs> m_CloseUIFormCompleteEventHandler;

    /// <summary>初始化界面管理器的新实例。</summary>
    public UIManager()
    {
      this.m_UIGroups = new Dictionary<string, UIManager.UIGroup>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_UIFormsBeingLoaded = new Dictionary<int, string>();
      this.m_UIFormsToReleaseOnLoad = new HashSet<int>();
      this.m_RecycleQueue = new Queue<IUIForm>();
      this.m_LoadAssetCallbacks = new LoadAssetCallbacks(new GameFramework.Resource.LoadAssetSuccessCallback(this.LoadAssetSuccessCallback), new GameFramework.Resource.LoadAssetFailureCallback(this.LoadAssetFailureCallback), new GameFramework.Resource.LoadAssetUpdateCallback(this.LoadAssetUpdateCallback), new GameFramework.Resource.LoadAssetDependencyAssetCallback(this.LoadAssetDependencyAssetCallback));
      this.m_ObjectPoolManager = (IObjectPoolManager) null;
      this.m_ResourceManager = (IResourceManager) null;
      this.m_InstancePool = (IObjectPool<UIManager.UIFormInstanceObject>) null;
      this.m_UIFormHelper = (IUIFormHelper) null;
      this.m_Serial = 0;
      this.m_IsShutdown = false;
      this.m_OpenUIFormSuccessEventHandler = (EventHandler<OpenUIFormSuccessEventArgs>) null;
      this.m_OpenUIFormFailureEventHandler = (EventHandler<OpenUIFormFailureEventArgs>) null;
      this.m_OpenUIFormUpdateEventHandler = (EventHandler<OpenUIFormUpdateEventArgs>) null;
      this.m_OpenUIFormDependencyAssetEventHandler = (EventHandler<OpenUIFormDependencyAssetEventArgs>) null;
      this.m_CloseUIFormCompleteEventHandler = (EventHandler<CloseUIFormCompleteEventArgs>) null;
    }

    /// <summary>获取界面组数量。</summary>
    public int UIGroupCount => this.m_UIGroups.Count;

    /// <summary>获取或设置界面实例对象池自动释放可释放对象的间隔秒数。</summary>
    public float InstanceAutoReleaseInterval
    {
      get => this.m_InstancePool.AutoReleaseInterval;
      set => this.m_InstancePool.AutoReleaseInterval = value;
    }

    /// <summary>获取或设置界面实例对象池的容量。</summary>
    public int InstanceCapacity
    {
      get => this.m_InstancePool.Capacity;
      set => this.m_InstancePool.Capacity = value;
    }

    /// <summary>获取或设置界面实例对象池对象过期秒数。</summary>
    public float InstanceExpireTime
    {
      get => this.m_InstancePool.ExpireTime;
      set => this.m_InstancePool.ExpireTime = value;
    }

    /// <summary>获取或设置界面实例对象池的优先级。</summary>
    public int InstancePriority
    {
      get => this.m_InstancePool.Priority;
      set => this.m_InstancePool.Priority = value;
    }

    /// <summary>打开界面成功事件。</summary>
    public event EventHandler<OpenUIFormSuccessEventArgs> OpenUIFormSuccess
    {
      add => this.m_OpenUIFormSuccessEventHandler += value;
      remove => this.m_OpenUIFormSuccessEventHandler -= value;
    }

    /// <summary>打开界面失败事件。</summary>
    public event EventHandler<OpenUIFormFailureEventArgs> OpenUIFormFailure
    {
      add => this.m_OpenUIFormFailureEventHandler += value;
      remove => this.m_OpenUIFormFailureEventHandler -= value;
    }

    /// <summary>打开界面更新事件。</summary>
    public event EventHandler<OpenUIFormUpdateEventArgs> OpenUIFormUpdate
    {
      add => this.m_OpenUIFormUpdateEventHandler += value;
      remove => this.m_OpenUIFormUpdateEventHandler -= value;
    }

    /// <summary>打开界面时加载依赖资源事件。</summary>
    public event EventHandler<OpenUIFormDependencyAssetEventArgs> OpenUIFormDependencyAsset
    {
      add => this.m_OpenUIFormDependencyAssetEventHandler += value;
      remove => this.m_OpenUIFormDependencyAssetEventHandler -= value;
    }

    /// <summary>关闭界面完成事件。</summary>
    public event EventHandler<CloseUIFormCompleteEventArgs> CloseUIFormComplete
    {
      add => this.m_CloseUIFormCompleteEventHandler += value;
      remove => this.m_CloseUIFormCompleteEventHandler -= value;
    }

    /// <summary>界面管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
      while (this.m_RecycleQueue.Count > 0)
      {
        IUIForm uiForm = this.m_RecycleQueue.Dequeue();
        uiForm.OnRecycle();
        this.m_InstancePool.Unspawn(uiForm.Handle);
      }
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        uiGroup.Value.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理界面管理器。</summary>
    internal override void Shutdown()
    {
      this.m_IsShutdown = true;
      this.CloseAllLoadedUIForms();
      this.m_UIGroups.Clear();
      this.m_UIFormsBeingLoaded.Clear();
      this.m_UIFormsToReleaseOnLoad.Clear();
      this.m_RecycleQueue.Clear();
    }

    /// <summary>设置对象池管理器。</summary>
    /// <param name="objectPoolManager">对象池管理器。</param>
    public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
    {
      this.m_ObjectPoolManager = objectPoolManager != null ? objectPoolManager : throw new GameFrameworkException("Object pool manager is invalid.");
      this.m_InstancePool = this.m_ObjectPoolManager.CreateSingleSpawnObjectPool<UIManager.UIFormInstanceObject>("UI Instance Pool");
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>设置界面辅助器。</summary>
    /// <param name="uiFormHelper">界面辅助器。</param>
    public void SetUIFormHelper(IUIFormHelper uiFormHelper) => this.m_UIFormHelper = uiFormHelper != null ? uiFormHelper : throw new GameFrameworkException("UI form helper is invalid.");

    /// <summary>是否存在界面组。</summary>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <returns>是否存在界面组。</returns>
    public bool HasUIGroup(string uiGroupName) => !string.IsNullOrEmpty(uiGroupName) ? this.m_UIGroups.ContainsKey(uiGroupName) : throw new GameFrameworkException("UI group name is invalid.");

    /// <summary>获取界面组。</summary>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <returns>要获取的界面组。</returns>
    public IUIGroup GetUIGroup(string uiGroupName)
    {
      if (string.IsNullOrEmpty(uiGroupName))
        throw new GameFrameworkException("UI group name is invalid.");
      UIManager.UIGroup uiGroup = (UIManager.UIGroup) null;
      return this.m_UIGroups.TryGetValue(uiGroupName, out uiGroup) ? (IUIGroup) uiGroup : (IUIGroup) null;
    }

    /// <summary>获取所有界面组。</summary>
    /// <returns>所有界面组。</returns>
    public IUIGroup[] GetAllUIGroups()
    {
      int num = 0;
      IUIGroup[] allUiGroups = new IUIGroup[this.m_UIGroups.Count];
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        allUiGroups[num++] = (IUIGroup) uiGroup.Value;
      return allUiGroups;
    }

    /// <summary>获取所有界面组。</summary>
    /// <param name="results">所有界面组。</param>
    public void GetAllUIGroups(List<IUIGroup> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        results.Add((IUIGroup) uiGroup.Value);
    }

    /// <summary>增加界面组。</summary>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="uiGroupHelper">界面组辅助器。</param>
    /// <returns>是否增加界面组成功。</returns>
    public bool AddUIGroup(string uiGroupName, IUIGroupHelper uiGroupHelper) => this.AddUIGroup(uiGroupName, 0, uiGroupHelper);

    /// <summary>增加界面组。</summary>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="uiGroupDepth">界面组深度。</param>
    /// <param name="uiGroupHelper">界面组辅助器。</param>
    /// <returns>是否增加界面组成功。</returns>
    public bool AddUIGroup(string uiGroupName, int uiGroupDepth, IUIGroupHelper uiGroupHelper)
    {
      if (string.IsNullOrEmpty(uiGroupName))
        throw new GameFrameworkException("UI group name is invalid.");
      if (uiGroupHelper == null)
        throw new GameFrameworkException("UI group helper is invalid.");
      if (this.HasUIGroup(uiGroupName))
        return false;
      this.m_UIGroups.Add(uiGroupName, new UIManager.UIGroup(uiGroupName, uiGroupDepth, uiGroupHelper));
      return true;
    }

    /// <summary>是否存在界面。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <returns>是否存在界面。</returns>
    public bool HasUIForm(int serialId)
    {
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
      {
        if (uiGroup.Value.HasUIForm(serialId))
          return true;
      }
      return false;
    }

    /// <summary>是否存在界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <returns>是否存在界面。</returns>
    public bool HasUIForm(string uiFormAssetName)
    {
      if (string.IsNullOrEmpty(uiFormAssetName))
        throw new GameFrameworkException("UI form asset name is invalid.");
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
      {
        if (uiGroup.Value.HasUIForm(uiFormAssetName))
          return true;
      }
      return false;
    }

    /// <summary>获取界面。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <returns>要获取的界面。</returns>
    public IUIForm GetUIForm(int serialId)
    {
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
      {
        IUIForm uiForm = uiGroup.Value.GetUIForm(serialId);
        if (uiForm != null)
          return uiForm;
      }
      return (IUIForm) null;
    }

    /// <summary>获取界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <returns>要获取的界面。</returns>
    public IUIForm GetUIForm(string uiFormAssetName)
    {
      if (string.IsNullOrEmpty(uiFormAssetName))
        throw new GameFrameworkException("UI form asset name is invalid.");
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
      {
        IUIForm uiForm = uiGroup.Value.GetUIForm(uiFormAssetName);
        if (uiForm != null)
          return uiForm;
      }
      return (IUIForm) null;
    }

    /// <summary>获取界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <returns>要获取的界面。</returns>
    public IUIForm[] GetUIForms(string uiFormAssetName)
    {
      if (string.IsNullOrEmpty(uiFormAssetName))
        throw new GameFrameworkException("UI form asset name is invalid.");
      List<IUIForm> uiFormList = new List<IUIForm>();
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        uiFormList.AddRange((IEnumerable<IUIForm>) uiGroup.Value.GetUIForms(uiFormAssetName));
      return uiFormList.ToArray();
    }

    /// <summary>获取界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="results">要获取的界面。</param>
    public void GetUIForms(string uiFormAssetName, List<IUIForm> results)
    {
      if (string.IsNullOrEmpty(uiFormAssetName))
        throw new GameFrameworkException("UI form asset name is invalid.");
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        uiGroup.Value.InternalGetUIForms(uiFormAssetName, results);
    }

    /// <summary>获取所有已加载的界面。</summary>
    /// <returns>所有已加载的界面。</returns>
    public IUIForm[] GetAllLoadedUIForms()
    {
      List<IUIForm> uiFormList = new List<IUIForm>();
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        uiFormList.AddRange((IEnumerable<IUIForm>) uiGroup.Value.GetAllUIForms());
      return uiFormList.ToArray();
    }

    /// <summary>获取所有已加载的界面。</summary>
    /// <param name="results">所有已加载的界面。</param>
    public void GetAllLoadedUIForms(List<IUIForm> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, UIManager.UIGroup> uiGroup in this.m_UIGroups)
        uiGroup.Value.InternalGetAllUIForms(results);
    }

    /// <summary>获取所有正在加载界面的序列编号。</summary>
    /// <returns>所有正在加载界面的序列编号。</returns>
    public int[] GetAllLoadingUIFormSerialIds()
    {
      int num = 0;
      int[] loadingUiFormSerialIds = new int[this.m_UIFormsBeingLoaded.Count];
      foreach (KeyValuePair<int, string> keyValuePair in this.m_UIFormsBeingLoaded)
        loadingUiFormSerialIds[num++] = keyValuePair.Key;
      return loadingUiFormSerialIds;
    }

    /// <summary>获取所有正在加载界面的序列编号。</summary>
    /// <param name="results">所有正在加载界面的序列编号。</param>
    public void GetAllLoadingUIFormSerialIds(List<int> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<int, string> keyValuePair in this.m_UIFormsBeingLoaded)
        results.Add(keyValuePair.Key);
    }

    /// <summary>是否正在加载界面。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <returns>是否正在加载界面。</returns>
    public bool IsLoadingUIForm(int serialId) => this.m_UIFormsBeingLoaded.ContainsKey(serialId);

    /// <summary>是否正在加载界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <returns>是否正在加载界面。</returns>
    public bool IsLoadingUIForm(string uiFormAssetName) => !string.IsNullOrEmpty(uiFormAssetName) ? this.m_UIFormsBeingLoaded.ContainsValue(uiFormAssetName) : throw new GameFrameworkException("UI form asset name is invalid.");

    /// <summary>是否是合法的界面。</summary>
    /// <param name="uiForm">界面。</param>
    /// <returns>界面是否合法。</returns>
    public bool IsValidUIForm(IUIForm uiForm) => uiForm != null && this.HasUIForm(uiForm.SerialId);

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(string uiFormAssetName, string uiGroupName) => this.OpenUIForm(uiFormAssetName, uiGroupName, 0, false, (object) null);

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="priority">加载界面资源的优先级。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(string uiFormAssetName, string uiGroupName, int priority) => this.OpenUIForm(uiFormAssetName, uiGroupName, priority, false, (object) null);

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm) => this.OpenUIForm(uiFormAssetName, uiGroupName, 0, pauseCoveredUIForm, (object) null);

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(string uiFormAssetName, string uiGroupName, object userData) => this.OpenUIForm(uiFormAssetName, uiGroupName, 0, false, userData);

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="priority">加载界面资源的优先级。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(
      string uiFormAssetName,
      string uiGroupName,
      int priority,
      bool pauseCoveredUIForm)
    {
      return this.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, (object) null);
    }

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="priority">加载界面资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(
      string uiFormAssetName,
      string uiGroupName,
      int priority,
      object userData)
    {
      return this.OpenUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
    }

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(
      string uiFormAssetName,
      string uiGroupName,
      bool pauseCoveredUIForm,
      object userData)
    {
      return this.OpenUIForm(uiFormAssetName, uiGroupName, 0, pauseCoveredUIForm, userData);
    }

    /// <summary>打开界面。</summary>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="priority">加载界面资源的优先级。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>界面的序列编号。</returns>
    public int OpenUIForm(
      string uiFormAssetName,
      string uiGroupName,
      int priority,
      bool pauseCoveredUIForm,
      object userData)
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_UIFormHelper == null)
        throw new GameFrameworkException("You must set UI form helper first.");
      if (string.IsNullOrEmpty(uiFormAssetName))
        throw new GameFrameworkException("UI form asset name is invalid.");
      UIManager.UIGroup uiGroup = !string.IsNullOrEmpty(uiGroupName) ? (UIManager.UIGroup) this.GetUIGroup(uiGroupName) : throw new GameFrameworkException("UI group name is invalid.");
      if (uiGroup == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("UI group '{0}' is not exist.", uiGroupName));
      int num = ++this.m_Serial;
      UIManager.UIFormInstanceObject formInstanceObject = this.m_InstancePool.Spawn(uiFormAssetName);
      if (formInstanceObject == null)
      {
        this.m_UIFormsBeingLoaded.Add(num, uiFormAssetName);
        this.m_ResourceManager.LoadAsset(uiFormAssetName, priority, this.m_LoadAssetCallbacks, (object) UIManager.OpenUIFormInfo.Create(num, uiGroup, pauseCoveredUIForm, userData));
      }
      else
        this.InternalOpenUIForm(num, uiFormAssetName, uiGroup, formInstanceObject.Target, pauseCoveredUIForm, false, 0.0f, userData);
      return num;
    }

    /// <summary>关闭界面。</summary>
    /// <param name="serialId">要关闭界面的序列编号。</param>
    public void CloseUIForm(int serialId) => this.CloseUIForm(serialId, (object) null);

    /// <summary>关闭界面。</summary>
    /// <param name="serialId">要关闭界面的序列编号。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void CloseUIForm(int serialId, object userData)
    {
      if (this.IsLoadingUIForm(serialId))
      {
        this.m_UIFormsToReleaseOnLoad.Add(serialId);
        this.m_UIFormsBeingLoaded.Remove(serialId);
      }
      else
        this.CloseUIForm(this.GetUIForm(serialId) ?? throw new GameFrameworkException(Utility.Text.Format<int>("Can not find UI form '{0}'.", serialId)), userData);
    }

    /// <summary>关闭界面。</summary>
    /// <param name="uiForm">要关闭的界面。</param>
    public void CloseUIForm(IUIForm uiForm) => this.CloseUIForm(uiForm, (object) null);

    /// <summary>关闭界面。</summary>
    /// <param name="uiForm">要关闭的界面。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void CloseUIForm(IUIForm uiForm, object userData)
    {
      UIManager.UIGroup uiGroup = uiForm != null ? (UIManager.UIGroup) uiForm.UIGroup : throw new GameFrameworkException("UI form is invalid.");
      if (uiGroup == null)
        throw new GameFrameworkException("UI group is invalid.");
      uiGroup.RemoveUIForm(uiForm);
      uiForm.OnClose(this.m_IsShutdown, userData);
      uiGroup.Refresh();
      if (this.m_CloseUIFormCompleteEventHandler != null)
      {
        CloseUIFormCompleteEventArgs e = CloseUIFormCompleteEventArgs.Create(uiForm.SerialId, uiForm.UIFormAssetName, (IUIGroup) uiGroup, userData);
        this.m_CloseUIFormCompleteEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      this.m_RecycleQueue.Enqueue(uiForm);
    }

    /// <summary>关闭所有已加载的界面。</summary>
    public void CloseAllLoadedUIForms() => this.CloseAllLoadedUIForms((object) null);

    /// <summary>关闭所有已加载的界面。</summary>
    /// <param name="userData">用户自定义数据。</param>
    public void CloseAllLoadedUIForms(object userData)
    {
      foreach (IUIForm allLoadedUiForm in this.GetAllLoadedUIForms())
      {
        if (this.HasUIForm(allLoadedUiForm.SerialId))
          this.CloseUIForm(allLoadedUiForm, userData);
      }
    }

    /// <summary>关闭所有正在加载的界面。</summary>
    public void CloseAllLoadingUIForms()
    {
      foreach (KeyValuePair<int, string> keyValuePair in this.m_UIFormsBeingLoaded)
        this.m_UIFormsToReleaseOnLoad.Add(keyValuePair.Key);
      this.m_UIFormsBeingLoaded.Clear();
    }

    /// <summary>激活界面。</summary>
    /// <param name="uiForm">要激活的界面。</param>
    public void RefocusUIForm(IUIForm uiForm) => this.RefocusUIForm(uiForm, (object) null);

    /// <summary>激活界面。</summary>
    /// <param name="uiForm">要激活的界面。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void RefocusUIForm(IUIForm uiForm, object userData)
    {
      UIManager.UIGroup uiGroup = uiForm != null ? (UIManager.UIGroup) uiForm.UIGroup : throw new GameFrameworkException("UI form is invalid.");
      if (uiGroup == null)
        throw new GameFrameworkException("UI group is invalid.");
      uiGroup.RefocusUIForm(uiForm, userData);
      uiGroup.Refresh();
      uiForm.OnRefocus(userData);
    }

    /// <summary>设置界面实例是否被加锁。</summary>
    /// <param name="uiFormInstance">要设置是否被加锁的界面实例。</param>
    /// <param name="locked">界面实例是否被加锁。</param>
    public void SetUIFormInstanceLocked(object uiFormInstance, bool locked)
    {
      if (uiFormInstance == null)
        throw new GameFrameworkException("UI form instance is invalid.");
      this.m_InstancePool.SetLocked(uiFormInstance, locked);
    }

    /// <summary>设置界面实例的优先级。</summary>
    /// <param name="uiFormInstance">要设置优先级的界面实例。</param>
    /// <param name="priority">界面实例优先级。</param>
    public void SetUIFormInstancePriority(object uiFormInstance, int priority)
    {
      if (uiFormInstance == null)
        throw new GameFrameworkException("UI form instance is invalid.");
      this.m_InstancePool.SetPriority(uiFormInstance, priority);
    }

    private void InternalOpenUIForm(
      int serialId,
      string uiFormAssetName,
      UIManager.UIGroup uiGroup,
      object uiFormInstance,
      bool pauseCoveredUIForm,
      bool isNewInstance,
      float duration,
      object userData)
    {
      try
      {
        IUIForm uiForm = this.m_UIFormHelper.CreateUIForm(uiFormInstance, (IUIGroup) uiGroup, userData);
        if (uiForm == null)
          throw new GameFrameworkException("Can not create UI form in UI form helper.");
        uiForm.OnInit(serialId, uiFormAssetName, (IUIGroup) uiGroup, pauseCoveredUIForm, isNewInstance, userData);
        uiGroup.AddUIForm(uiForm);
        uiForm.OnOpen(userData);
        uiGroup.Refresh();
        if (this.m_OpenUIFormSuccessEventHandler == null)
          return;
        OpenUIFormSuccessEventArgs e = OpenUIFormSuccessEventArgs.Create(uiForm, duration, userData);
        this.m_OpenUIFormSuccessEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      catch (Exception ex)
      {
        if (this.m_OpenUIFormFailureEventHandler != null)
        {
          OpenUIFormFailureEventArgs e = OpenUIFormFailureEventArgs.Create(serialId, uiFormAssetName, uiGroup.Name, pauseCoveredUIForm, ex.ToString(), userData);
          this.m_OpenUIFormFailureEventHandler((object) this, e);
          ReferencePool.Release((IReference) e);
        }
        else
          throw;
      }
    }

    private void LoadAssetSuccessCallback(
      string uiFormAssetName,
      object uiFormAsset,
      float duration,
      object userData)
    {
      UIManager.OpenUIFormInfo openUiFormInfo = (UIManager.OpenUIFormInfo) userData;
      if (openUiFormInfo == null)
        throw new GameFrameworkException("Open UI form info is invalid.");
      if (this.m_UIFormsToReleaseOnLoad.Contains(openUiFormInfo.SerialId))
      {
        this.m_UIFormsToReleaseOnLoad.Remove(openUiFormInfo.SerialId);
        ReferencePool.Release((IReference) openUiFormInfo);
        this.m_UIFormHelper.ReleaseUIForm(uiFormAsset, (object) null);
      }
      else
      {
        this.m_UIFormsBeingLoaded.Remove(openUiFormInfo.SerialId);
        UIManager.UIFormInstanceObject formInstanceObject = UIManager.UIFormInstanceObject.Create(uiFormAssetName, uiFormAsset, this.m_UIFormHelper.InstantiateUIForm(uiFormAsset), this.m_UIFormHelper);
        this.m_InstancePool.Register(formInstanceObject, true);
        this.InternalOpenUIForm(openUiFormInfo.SerialId, uiFormAssetName, openUiFormInfo.UIGroup, formInstanceObject.Target, openUiFormInfo.PauseCoveredUIForm, true, duration, openUiFormInfo.UserData);
        ReferencePool.Release((IReference) openUiFormInfo);
      }
    }

    private void LoadAssetFailureCallback(
      string uiFormAssetName,
      LoadResourceStatus status,
      string errorMessage,
      object userData)
    {
      UIManager.OpenUIFormInfo openUiFormInfo = (UIManager.OpenUIFormInfo) userData;
      if (openUiFormInfo == null)
        throw new GameFrameworkException("Open UI form info is invalid.");
      if (this.m_UIFormsToReleaseOnLoad.Contains(openUiFormInfo.SerialId))
      {
        this.m_UIFormsToReleaseOnLoad.Remove(openUiFormInfo.SerialId);
      }
      else
      {
        this.m_UIFormsBeingLoaded.Remove(openUiFormInfo.SerialId);
        string str = Utility.Text.Format<string, LoadResourceStatus, string>("Load UI form failure, asset name '{0}', status '{1}', error message '{2}'.", uiFormAssetName, status, errorMessage);
        if (this.m_OpenUIFormFailureEventHandler == null)
          throw new GameFrameworkException(str);
        OpenUIFormFailureEventArgs e = OpenUIFormFailureEventArgs.Create(openUiFormInfo.SerialId, uiFormAssetName, openUiFormInfo.UIGroup.Name, openUiFormInfo.PauseCoveredUIForm, str, openUiFormInfo.UserData);
        this.m_OpenUIFormFailureEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
    }

    private void LoadAssetUpdateCallback(string uiFormAssetName, float progress, object userData)
    {
      UIManager.OpenUIFormInfo openUiFormInfo = (UIManager.OpenUIFormInfo) userData;
      if (openUiFormInfo == null)
        throw new GameFrameworkException("Open UI form info is invalid.");
      if (this.m_OpenUIFormUpdateEventHandler == null)
        return;
      OpenUIFormUpdateEventArgs e = OpenUIFormUpdateEventArgs.Create(openUiFormInfo.SerialId, uiFormAssetName, openUiFormInfo.UIGroup.Name, openUiFormInfo.PauseCoveredUIForm, progress, openUiFormInfo.UserData);
      this.m_OpenUIFormUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadAssetDependencyAssetCallback(
      string uiFormAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      UIManager.OpenUIFormInfo openUiFormInfo = (UIManager.OpenUIFormInfo) userData;
      if (openUiFormInfo == null)
        throw new GameFrameworkException("Open UI form info is invalid.");
      if (this.m_OpenUIFormDependencyAssetEventHandler == null)
        return;
      OpenUIFormDependencyAssetEventArgs e = OpenUIFormDependencyAssetEventArgs.Create(openUiFormInfo.SerialId, uiFormAssetName, openUiFormInfo.UIGroup.Name, openUiFormInfo.PauseCoveredUIForm, dependencyAssetName, loadedCount, totalCount, openUiFormInfo.UserData);
      this.m_OpenUIFormDependencyAssetEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private sealed class OpenUIFormInfo : IReference
    {
      private int m_SerialId;
      private UIManager.UIGroup m_UIGroup;
      private bool m_PauseCoveredUIForm;
      private object m_UserData;

      public OpenUIFormInfo()
      {
        this.m_SerialId = 0;
        this.m_UIGroup = (UIManager.UIGroup) null;
        this.m_PauseCoveredUIForm = false;
        this.m_UserData = (object) null;
      }

      public int SerialId => this.m_SerialId;

      public UIManager.UIGroup UIGroup => this.m_UIGroup;

      public bool PauseCoveredUIForm => this.m_PauseCoveredUIForm;

      public object UserData => this.m_UserData;

      public static UIManager.OpenUIFormInfo Create(
        int serialId,
        UIManager.UIGroup uiGroup,
        bool pauseCoveredUIForm,
        object userData)
      {
        UIManager.OpenUIFormInfo openUiFormInfo = ReferencePool.Acquire<UIManager.OpenUIFormInfo>();
        openUiFormInfo.m_SerialId = serialId;
        openUiFormInfo.m_UIGroup = uiGroup;
        openUiFormInfo.m_PauseCoveredUIForm = pauseCoveredUIForm;
        openUiFormInfo.m_UserData = userData;
        return openUiFormInfo;
      }

      public void Clear()
      {
        this.m_SerialId = 0;
        this.m_UIGroup = (UIManager.UIGroup) null;
        this.m_PauseCoveredUIForm = false;
        this.m_UserData = (object) null;
      }
    }

    /// <summary>界面实例对象。</summary>
    private sealed class UIFormInstanceObject : ObjectBase
    {
      private object m_UIFormAsset;
      private IUIFormHelper m_UIFormHelper;

      public UIFormInstanceObject()
      {
        this.m_UIFormAsset = (object) null;
        this.m_UIFormHelper = (IUIFormHelper) null;
      }

      public static UIManager.UIFormInstanceObject Create(
        string name,
        object uiFormAsset,
        object uiFormInstance,
        IUIFormHelper uiFormHelper)
      {
        if (uiFormAsset == null)
          throw new GameFrameworkException("UI form asset is invalid.");
        if (uiFormHelper == null)
          throw new GameFrameworkException("UI form helper is invalid.");
        UIManager.UIFormInstanceObject formInstanceObject = ReferencePool.Acquire<UIManager.UIFormInstanceObject>();
        formInstanceObject.Initialize(name, uiFormInstance);
        formInstanceObject.m_UIFormAsset = uiFormAsset;
        formInstanceObject.m_UIFormHelper = uiFormHelper;
        return formInstanceObject;
      }

      public override void Clear()
      {
        base.Clear();
        this.m_UIFormAsset = (object) null;
        this.m_UIFormHelper = (IUIFormHelper) null;
      }

      protected internal override void Release(bool isShutdown) => this.m_UIFormHelper.ReleaseUIForm(this.m_UIFormAsset, this.Target);
    }

    /// <summary>界面组。</summary>
    private sealed class UIGroup : IUIGroup
    {
      private readonly string m_Name;
      private int m_Depth;
      private bool m_Pause;
      private readonly IUIGroupHelper m_UIGroupHelper;
      private readonly GameFrameworkLinkedList<UIManager.UIGroup.UIFormInfo> m_UIFormInfos;
      private LinkedListNode<UIManager.UIGroup.UIFormInfo> m_CachedNode;

      /// <summary>初始化界面组的新实例。</summary>
      /// <param name="name">界面组名称。</param>
      /// <param name="depth">界面组深度。</param>
      /// <param name="uiGroupHelper">界面组辅助器。</param>
      public UIGroup(string name, int depth, IUIGroupHelper uiGroupHelper)
      {
        if (string.IsNullOrEmpty(name))
          throw new GameFrameworkException("UI group name is invalid.");
        if (uiGroupHelper == null)
          throw new GameFrameworkException("UI group helper is invalid.");
        this.m_Name = name;
        this.m_Pause = false;
        this.m_UIGroupHelper = uiGroupHelper;
        this.m_UIFormInfos = new GameFrameworkLinkedList<UIManager.UIGroup.UIFormInfo>();
        this.m_CachedNode = (LinkedListNode<UIManager.UIGroup.UIFormInfo>) null;
        this.Depth = depth;
      }

      /// <summary>获取界面组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取或设置界面组深度。</summary>
      public int Depth
      {
        get => this.m_Depth;
        set
        {
          if (this.m_Depth == value)
            return;
          this.m_Depth = value;
          this.m_UIGroupHelper.SetDepth(this.m_Depth);
          this.Refresh();
        }
      }

      /// <summary>获取或设置界面组是否暂停。</summary>
      public bool Pause
      {
        get => this.m_Pause;
        set
        {
          if (this.m_Pause == value)
            return;
          this.m_Pause = value;
          this.Refresh();
        }
      }

      /// <summary>获取界面组中界面数量。</summary>
      public int UIFormCount => this.m_UIFormInfos.Count;

      /// <summary>获取当前界面。</summary>
      public IUIForm CurrentUIForm => this.m_UIFormInfos.First == null ? (IUIForm) null : this.m_UIFormInfos.First.Value.UIForm;

      /// <summary>获取界面组辅助器。</summary>
      public IUIGroupHelper Helper => this.m_UIGroupHelper;

      /// <summary>界面组轮询。</summary>
      /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
      /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
      public void Update(float elapseSeconds, float realElapseSeconds)
      {
        LinkedListNode<UIManager.UIGroup.UIFormInfo> linkedListNode = this.m_UIFormInfos.First;
        while (linkedListNode != null && !linkedListNode.Value.Paused)
        {
          this.m_CachedNode = linkedListNode.Next;
          linkedListNode.Value.UIForm.OnUpdate(elapseSeconds, realElapseSeconds);
          linkedListNode = this.m_CachedNode;
          this.m_CachedNode = (LinkedListNode<UIManager.UIGroup.UIFormInfo>) null;
        }
      }

      /// <summary>界面组中是否存在界面。</summary>
      /// <param name="serialId">界面序列编号。</param>
      /// <returns>界面组中是否存在界面。</returns>
      public bool HasUIForm(int serialId)
      {
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.SerialId == serialId)
            return true;
        }
        return false;
      }

      /// <summary>界面组中是否存在界面。</summary>
      /// <param name="uiFormAssetName">界面资源名称。</param>
      /// <returns>界面组中是否存在界面。</returns>
      public bool HasUIForm(string uiFormAssetName)
      {
        if (string.IsNullOrEmpty(uiFormAssetName))
          throw new GameFrameworkException("UI form asset name is invalid.");
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
            return true;
        }
        return false;
      }

      /// <summary>从界面组中获取界面。</summary>
      /// <param name="serialId">界面序列编号。</param>
      /// <returns>要获取的界面。</returns>
      public IUIForm GetUIForm(int serialId)
      {
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.SerialId == serialId)
            return uiFormInfo.UIForm;
        }
        return (IUIForm) null;
      }

      /// <summary>从界面组中获取界面。</summary>
      /// <param name="uiFormAssetName">界面资源名称。</param>
      /// <returns>要获取的界面。</returns>
      public IUIForm GetUIForm(string uiFormAssetName)
      {
        if (string.IsNullOrEmpty(uiFormAssetName))
          throw new GameFrameworkException("UI form asset name is invalid.");
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
            return uiFormInfo.UIForm;
        }
        return (IUIForm) null;
      }

      /// <summary>从界面组中获取界面。</summary>
      /// <param name="uiFormAssetName">界面资源名称。</param>
      /// <returns>要获取的界面。</returns>
      public IUIForm[] GetUIForms(string uiFormAssetName)
      {
        if (string.IsNullOrEmpty(uiFormAssetName))
          throw new GameFrameworkException("UI form asset name is invalid.");
        List<IUIForm> uiFormList = new List<IUIForm>();
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
            uiFormList.Add(uiFormInfo.UIForm);
        }
        return uiFormList.ToArray();
      }

      /// <summary>从界面组中获取界面。</summary>
      /// <param name="uiFormAssetName">界面资源名称。</param>
      /// <param name="results">要获取的界面。</param>
      public void GetUIForms(string uiFormAssetName, List<IUIForm> results)
      {
        if (string.IsNullOrEmpty(uiFormAssetName))
          throw new GameFrameworkException("UI form asset name is invalid.");
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
            results.Add(uiFormInfo.UIForm);
        }
      }

      /// <summary>从界面组中获取所有界面。</summary>
      /// <returns>界面组中的所有界面。</returns>
      public IUIForm[] GetAllUIForms()
      {
        List<IUIForm> uiFormList = new List<IUIForm>();
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
          uiFormList.Add(uiFormInfo.UIForm);
        return uiFormList.ToArray();
      }

      /// <summary>从界面组中获取所有界面。</summary>
      /// <param name="results">界面组中的所有界面。</param>
      public void GetAllUIForms(List<IUIForm> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
          results.Add(uiFormInfo.UIForm);
      }

      /// <summary>往界面组增加界面。</summary>
      /// <param name="uiForm">要增加的界面。</param>
      public void AddUIForm(IUIForm uiForm) => this.m_UIFormInfos.AddFirst(UIManager.UIGroup.UIFormInfo.Create(uiForm));

      /// <summary>从界面组移除界面。</summary>
      /// <param name="uiForm">要移除的界面。</param>
      public void RemoveUIForm(IUIForm uiForm)
      {
        UIManager.UIGroup.UIFormInfo uiFormInfo = this.GetUIFormInfo(uiForm);
        if (uiFormInfo == null)
          throw new GameFrameworkException(Utility.Text.Format<int, string>("Can not find UI form info for serial id '{0}', UI form asset name is '{1}'.", uiForm.SerialId, uiForm.UIFormAssetName));
        if (!uiFormInfo.Covered)
        {
          uiFormInfo.Covered = true;
          uiForm.OnCover();
        }
        if (!uiFormInfo.Paused)
        {
          uiFormInfo.Paused = true;
          uiForm.OnPause();
        }
        if (this.m_CachedNode != null && this.m_CachedNode.Value.UIForm == uiForm)
          this.m_CachedNode = this.m_CachedNode.Next;
        if (!this.m_UIFormInfos.Remove(uiFormInfo))
          throw new GameFrameworkException(Utility.Text.Format<string, int, string>("UI group '{0}' not exists specified UI form '[{1}]{2}'.", this.m_Name, uiForm.SerialId, uiForm.UIFormAssetName));
        ReferencePool.Release((IReference) uiFormInfo);
      }

      /// <summary>激活界面。</summary>
      /// <param name="uiForm">要激活的界面。</param>
      /// <param name="userData">用户自定义数据。</param>
      public void RefocusUIForm(IUIForm uiForm, object userData)
      {
        UIManager.UIGroup.UIFormInfo uiFormInfo = this.GetUIFormInfo(uiForm);
        if (uiFormInfo == null)
          throw new GameFrameworkException("Can not find UI form info.");
        this.m_UIFormInfos.Remove(uiFormInfo);
        this.m_UIFormInfos.AddFirst(uiFormInfo);
      }

      /// <summary>刷新界面组。</summary>
      public void Refresh()
      {
        LinkedListNode<UIManager.UIGroup.UIFormInfo> linkedListNode = this.m_UIFormInfos.First;
        bool flag1 = this.m_Pause;
        bool flag2 = false;
        int uiFormCount = this.UIFormCount;
        LinkedListNode<UIManager.UIGroup.UIFormInfo> next;
        for (; linkedListNode != null && linkedListNode.Value != null; linkedListNode = next)
        {
          next = linkedListNode.Next;
          linkedListNode.Value.UIForm.OnDepthChanged(this.Depth, uiFormCount--);
          if (linkedListNode.Value == null)
            break;
          if (flag1)
          {
            if (!linkedListNode.Value.Covered)
            {
              linkedListNode.Value.Covered = true;
              linkedListNode.Value.UIForm.OnCover();
              if (linkedListNode.Value == null)
                break;
            }
            if (!linkedListNode.Value.Paused)
            {
              linkedListNode.Value.Paused = true;
              linkedListNode.Value.UIForm.OnPause();
              if (linkedListNode.Value == null)
                break;
            }
          }
          else
          {
            if (linkedListNode.Value.Paused)
            {
              linkedListNode.Value.Paused = false;
              linkedListNode.Value.UIForm.OnResume();
              if (linkedListNode.Value == null)
                break;
            }
            if (linkedListNode.Value.UIForm.PauseCoveredUIForm)
              flag1 = true;
            if (flag2)
            {
              if (!linkedListNode.Value.Covered)
              {
                linkedListNode.Value.Covered = true;
                linkedListNode.Value.UIForm.OnCover();
                if (linkedListNode.Value == null)
                  break;
              }
            }
            else
            {
              if (linkedListNode.Value.Covered)
              {
                linkedListNode.Value.Covered = false;
                linkedListNode.Value.UIForm.OnReveal();
                if (linkedListNode.Value == null)
                  break;
              }
              flag2 = true;
            }
          }
        }
      }

      internal void InternalGetUIForms(string uiFormAssetName, List<IUIForm> results)
      {
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm.UIFormAssetName == uiFormAssetName)
            results.Add(uiFormInfo.UIForm);
        }
      }

      internal void InternalGetAllUIForms(List<IUIForm> results)
      {
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
          results.Add(uiFormInfo.UIForm);
      }

      private UIManager.UIGroup.UIFormInfo GetUIFormInfo(IUIForm uiForm)
      {
        if (uiForm == null)
          throw new GameFrameworkException("UI form is invalid.");
        foreach (UIManager.UIGroup.UIFormInfo uiFormInfo in this.m_UIFormInfos)
        {
          if (uiFormInfo.UIForm == uiForm)
            return uiFormInfo;
        }
        return (UIManager.UIGroup.UIFormInfo) null;
      }

      /// <summary>界面组界面信息。</summary>
      private sealed class UIFormInfo : IReference
      {
        private IUIForm m_UIForm;
        private bool m_Paused;
        private bool m_Covered;

        public UIFormInfo()
        {
          this.m_UIForm = (IUIForm) null;
          this.m_Paused = false;
          this.m_Covered = false;
        }

        public IUIForm UIForm => this.m_UIForm;

        public bool Paused
        {
          get => this.m_Paused;
          set => this.m_Paused = value;
        }

        public bool Covered
        {
          get => this.m_Covered;
          set => this.m_Covered = value;
        }

        public static UIManager.UIGroup.UIFormInfo Create(IUIForm uiForm)
        {
          if (uiForm == null)
            throw new GameFrameworkException("UI form is invalid.");
          UIManager.UIGroup.UIFormInfo uiFormInfo = ReferencePool.Acquire<UIManager.UIGroup.UIFormInfo>();
          uiFormInfo.m_UIForm = uiForm;
          uiFormInfo.m_Paused = true;
          uiFormInfo.m_Covered = true;
          return uiFormInfo;
        }

        public void Clear()
        {
          this.m_UIForm = (IUIForm) null;
          this.m_Paused = false;
          this.m_Covered = false;
        }
      }
    }
  }
}
