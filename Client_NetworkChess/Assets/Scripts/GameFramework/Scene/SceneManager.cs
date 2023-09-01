// Decompiled with JetBrains decompiler
// Type: GameFramework.Scene.SceneManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Scene
{
  /// <summary>场景管理器。</summary>
  internal sealed class SceneManager : GameFrameworkModule, ISceneManager
  {
    private readonly List<string> m_LoadedSceneAssetNames;
    private readonly List<string> m_LoadingSceneAssetNames;
    private readonly List<string> m_UnloadingSceneAssetNames;
    private readonly LoadSceneCallbacks m_LoadSceneCallbacks;
    private readonly UnloadSceneCallbacks m_UnloadSceneCallbacks;
    private IResourceManager m_ResourceManager;
    private EventHandler<LoadSceneSuccessEventArgs> m_LoadSceneSuccessEventHandler;
    private EventHandler<LoadSceneFailureEventArgs> m_LoadSceneFailureEventHandler;
    private EventHandler<LoadSceneUpdateEventArgs> m_LoadSceneUpdateEventHandler;
    private EventHandler<LoadSceneDependencyAssetEventArgs> m_LoadSceneDependencyAssetEventHandler;
    private EventHandler<UnloadSceneSuccessEventArgs> m_UnloadSceneSuccessEventHandler;
    private EventHandler<UnloadSceneFailureEventArgs> m_UnloadSceneFailureEventHandler;

    /// <summary>初始化场景管理器的新实例。</summary>
    public SceneManager()
    {
      this.m_LoadedSceneAssetNames = new List<string>();
      this.m_LoadingSceneAssetNames = new List<string>();
      this.m_UnloadingSceneAssetNames = new List<string>();
      this.m_LoadSceneCallbacks = new LoadSceneCallbacks(new GameFramework.Resource.LoadSceneSuccessCallback(this.LoadSceneSuccessCallback), new GameFramework.Resource.LoadSceneFailureCallback(this.LoadSceneFailureCallback), new GameFramework.Resource.LoadSceneUpdateCallback(this.LoadSceneUpdateCallback), new GameFramework.Resource.LoadSceneDependencyAssetCallback(this.LoadSceneDependencyAssetCallback));
      this.m_UnloadSceneCallbacks = new UnloadSceneCallbacks(new GameFramework.Resource.UnloadSceneSuccessCallback(this.UnloadSceneSuccessCallback), new GameFramework.Resource.UnloadSceneFailureCallback(this.UnloadSceneFailureCallback));
      this.m_ResourceManager = (IResourceManager) null;
      this.m_LoadSceneSuccessEventHandler = (EventHandler<LoadSceneSuccessEventArgs>) null;
      this.m_LoadSceneFailureEventHandler = (EventHandler<LoadSceneFailureEventArgs>) null;
      this.m_LoadSceneUpdateEventHandler = (EventHandler<LoadSceneUpdateEventArgs>) null;
      this.m_LoadSceneDependencyAssetEventHandler = (EventHandler<LoadSceneDependencyAssetEventArgs>) null;
      this.m_UnloadSceneSuccessEventHandler = (EventHandler<UnloadSceneSuccessEventArgs>) null;
      this.m_UnloadSceneFailureEventHandler = (EventHandler<UnloadSceneFailureEventArgs>) null;
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 2;

    /// <summary>加载场景成功事件。</summary>
    public event EventHandler<LoadSceneSuccessEventArgs> LoadSceneSuccess
    {
      add => this.m_LoadSceneSuccessEventHandler += value;
      remove => this.m_LoadSceneSuccessEventHandler -= value;
    }

    /// <summary>加载场景失败事件。</summary>
    public event EventHandler<LoadSceneFailureEventArgs> LoadSceneFailure
    {
      add => this.m_LoadSceneFailureEventHandler += value;
      remove => this.m_LoadSceneFailureEventHandler -= value;
    }

    /// <summary>加载场景更新事件。</summary>
    public event EventHandler<LoadSceneUpdateEventArgs> LoadSceneUpdate
    {
      add => this.m_LoadSceneUpdateEventHandler += value;
      remove => this.m_LoadSceneUpdateEventHandler -= value;
    }

    /// <summary>加载场景时加载依赖资源事件。</summary>
    public event EventHandler<LoadSceneDependencyAssetEventArgs> LoadSceneDependencyAsset
    {
      add => this.m_LoadSceneDependencyAssetEventHandler += value;
      remove => this.m_LoadSceneDependencyAssetEventHandler -= value;
    }

    /// <summary>卸载场景成功事件。</summary>
    public event EventHandler<UnloadSceneSuccessEventArgs> UnloadSceneSuccess
    {
      add => this.m_UnloadSceneSuccessEventHandler += value;
      remove => this.m_UnloadSceneSuccessEventHandler -= value;
    }

    /// <summary>卸载场景失败事件。</summary>
    public event EventHandler<UnloadSceneFailureEventArgs> UnloadSceneFailure
    {
      add => this.m_UnloadSceneFailureEventHandler += value;
      remove => this.m_UnloadSceneFailureEventHandler -= value;
    }

    /// <summary>场景管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理场景管理器。</summary>
    internal override void Shutdown()
    {
      foreach (string sceneAssetName in this.m_LoadedSceneAssetNames.ToArray())
      {
        if (!this.SceneIsUnloading(sceneAssetName))
          this.UnloadScene(sceneAssetName);
      }
      this.m_LoadedSceneAssetNames.Clear();
      this.m_LoadingSceneAssetNames.Clear();
      this.m_UnloadingSceneAssetNames.Clear();
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>获取场景是否已加载。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <returns>场景是否已加载。</returns>
    public bool SceneIsLoaded(string sceneAssetName) => !string.IsNullOrEmpty(sceneAssetName) ? this.m_LoadedSceneAssetNames.Contains(sceneAssetName) : throw new GameFrameworkException("Scene asset name is invalid.");

    /// <summary>获取已加载场景的资源名称。</summary>
    /// <returns>已加载场景的资源名称。</returns>
    public string[] GetLoadedSceneAssetNames() => this.m_LoadedSceneAssetNames.ToArray();

    /// <summary>获取已加载场景的资源名称。</summary>
    /// <param name="results">已加载场景的资源名称。</param>
    public void GetLoadedSceneAssetNames(List<string> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      results.AddRange((IEnumerable<string>) this.m_LoadedSceneAssetNames);
    }

    /// <summary>获取场景是否正在加载。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <returns>场景是否正在加载。</returns>
    public bool SceneIsLoading(string sceneAssetName) => !string.IsNullOrEmpty(sceneAssetName) ? this.m_LoadingSceneAssetNames.Contains(sceneAssetName) : throw new GameFrameworkException("Scene asset name is invalid.");

    /// <summary>获取正在加载场景的资源名称。</summary>
    /// <returns>正在加载场景的资源名称。</returns>
    public string[] GetLoadingSceneAssetNames() => this.m_LoadingSceneAssetNames.ToArray();

    /// <summary>获取正在加载场景的资源名称。</summary>
    /// <param name="results">正在加载场景的资源名称。</param>
    public void GetLoadingSceneAssetNames(List<string> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      results.AddRange((IEnumerable<string>) this.m_LoadingSceneAssetNames);
    }

    /// <summary>获取场景是否正在卸载。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <returns>场景是否正在卸载。</returns>
    public bool SceneIsUnloading(string sceneAssetName) => !string.IsNullOrEmpty(sceneAssetName) ? this.m_UnloadingSceneAssetNames.Contains(sceneAssetName) : throw new GameFrameworkException("Scene asset name is invalid.");

    /// <summary>获取正在卸载场景的资源名称。</summary>
    /// <returns>正在卸载场景的资源名称。</returns>
    public string[] GetUnloadingSceneAssetNames() => this.m_UnloadingSceneAssetNames.ToArray();

    /// <summary>获取正在卸载场景的资源名称。</summary>
    /// <param name="results">正在卸载场景的资源名称。</param>
    public void GetUnloadingSceneAssetNames(List<string> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      results.AddRange((IEnumerable<string>) this.m_UnloadingSceneAssetNames);
    }

    /// <summary>检查场景资源是否存在。</summary>
    /// <param name="sceneAssetName">要检查场景资源的名称。</param>
    /// <returns>场景资源是否存在。</returns>
    public bool HasScene(string sceneAssetName) => this.m_ResourceManager.HasAsset(sceneAssetName) != 0;

    /// <summary>加载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    public void LoadScene(string sceneAssetName) => this.LoadScene(sceneAssetName, 0, (object) null);

    /// <summary>加载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="priority">加载场景资源的优先级。</param>
    public void LoadScene(string sceneAssetName, int priority) => this.LoadScene(sceneAssetName, priority, (object) null);

    /// <summary>加载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadScene(string sceneAssetName, object userData) => this.LoadScene(sceneAssetName, 0, userData);

    /// <summary>加载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="priority">加载场景资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void LoadScene(string sceneAssetName, int priority, object userData)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.SceneIsUnloading(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is being unloaded.", sceneAssetName));
      if (this.SceneIsLoading(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is being loaded.", sceneAssetName));
      if (this.SceneIsLoaded(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is already loaded.", sceneAssetName));
      this.m_LoadingSceneAssetNames.Add(sceneAssetName);
      this.m_ResourceManager.LoadScene(sceneAssetName, priority, this.m_LoadSceneCallbacks, userData);
    }

    /// <summary>卸载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    public void UnloadScene(string sceneAssetName) => this.UnloadScene(sceneAssetName, (object) null);

    /// <summary>卸载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void UnloadScene(string sceneAssetName, object userData)
    {
      if (string.IsNullOrEmpty(sceneAssetName))
        throw new GameFrameworkException("Scene asset name is invalid.");
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.SceneIsUnloading(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is being unloaded.", sceneAssetName));
      if (this.SceneIsLoading(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is being loaded.", sceneAssetName));
      if (!this.SceneIsLoaded(sceneAssetName))
        throw new GameFrameworkException(Utility.Text.Format<string>("Scene asset '{0}' is not loaded yet.", sceneAssetName));
      this.m_UnloadingSceneAssetNames.Add(sceneAssetName);
      this.m_ResourceManager.UnloadScene(sceneAssetName, this.m_UnloadSceneCallbacks, userData);
    }

    private void LoadSceneSuccessCallback(string sceneAssetName, float duration, object userData)
    {
      this.m_LoadingSceneAssetNames.Remove(sceneAssetName);
      this.m_LoadedSceneAssetNames.Add(sceneAssetName);
      if (this.m_LoadSceneSuccessEventHandler == null)
        return;
      LoadSceneSuccessEventArgs e = LoadSceneSuccessEventArgs.Create(sceneAssetName, duration, userData);
      this.m_LoadSceneSuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadSceneFailureCallback(
      string sceneAssetName,
      LoadResourceStatus status,
      string errorMessage,
      object userData)
    {
      this.m_LoadingSceneAssetNames.Remove(sceneAssetName);
      string str = Utility.Text.Format<string, LoadResourceStatus, string>("Load scene failure, scene asset name '{0}', status '{1}', error message '{2}'.", sceneAssetName, status, errorMessage);
      if (this.m_LoadSceneFailureEventHandler == null)
        throw new GameFrameworkException(str);
      LoadSceneFailureEventArgs e = LoadSceneFailureEventArgs.Create(sceneAssetName, str, userData);
      this.m_LoadSceneFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadSceneUpdateCallback(string sceneAssetName, float progress, object userData)
    {
      if (this.m_LoadSceneUpdateEventHandler == null)
        return;
      LoadSceneUpdateEventArgs e = LoadSceneUpdateEventArgs.Create(sceneAssetName, progress, userData);
      this.m_LoadSceneUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadSceneDependencyAssetCallback(
      string sceneAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      if (this.m_LoadSceneDependencyAssetEventHandler == null)
        return;
      LoadSceneDependencyAssetEventArgs e = LoadSceneDependencyAssetEventArgs.Create(sceneAssetName, dependencyAssetName, loadedCount, totalCount, userData);
      this.m_LoadSceneDependencyAssetEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void UnloadSceneSuccessCallback(string sceneAssetName, object userData)
    {
      this.m_UnloadingSceneAssetNames.Remove(sceneAssetName);
      this.m_LoadedSceneAssetNames.Remove(sceneAssetName);
      if (this.m_UnloadSceneSuccessEventHandler == null)
        return;
      UnloadSceneSuccessEventArgs e = UnloadSceneSuccessEventArgs.Create(sceneAssetName, userData);
      this.m_UnloadSceneSuccessEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void UnloadSceneFailureCallback(string sceneAssetName, object userData)
    {
      this.m_UnloadingSceneAssetNames.Remove(sceneAssetName);
      if (this.m_UnloadSceneFailureEventHandler == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Unload scene failure, scene asset name '{0}'.", sceneAssetName));
      UnloadSceneFailureEventArgs e = UnloadSceneFailureEventArgs.Create(sceneAssetName, userData);
      this.m_UnloadSceneFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }
  }
}
