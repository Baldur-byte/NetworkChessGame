// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadSceneCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载场景回调函数集。</summary>
  public sealed class LoadSceneCallbacks
  {
    private readonly LoadSceneSuccessCallback m_LoadSceneSuccessCallback;
    private readonly LoadSceneFailureCallback m_LoadSceneFailureCallback;
    private readonly LoadSceneUpdateCallback m_LoadSceneUpdateCallback;
    private readonly LoadSceneDependencyAssetCallback m_LoadSceneDependencyAssetCallback;

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback)
      : this(loadSceneSuccessCallback, (LoadSceneFailureCallback) null, (LoadSceneUpdateCallback) null, (LoadSceneDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneFailureCallback">加载场景失败回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneFailureCallback loadSceneFailureCallback)
      : this(loadSceneSuccessCallback, loadSceneFailureCallback, (LoadSceneUpdateCallback) null, (LoadSceneDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneUpdateCallback">加载场景更新回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneUpdateCallback loadSceneUpdateCallback)
      : this(loadSceneSuccessCallback, (LoadSceneFailureCallback) null, loadSceneUpdateCallback, (LoadSceneDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneDependencyAssetCallback">加载场景时加载依赖资源回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
      : this(loadSceneSuccessCallback, (LoadSceneFailureCallback) null, (LoadSceneUpdateCallback) null, loadSceneDependencyAssetCallback)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneFailureCallback">加载场景失败回调函数。</param>
    /// <param name="loadSceneUpdateCallback">加载场景更新回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneFailureCallback loadSceneFailureCallback,
      LoadSceneUpdateCallback loadSceneUpdateCallback)
      : this(loadSceneSuccessCallback, loadSceneFailureCallback, loadSceneUpdateCallback, (LoadSceneDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneFailureCallback">加载场景失败回调函数。</param>
    /// <param name="loadSceneDependencyAssetCallback">加载场景时加载依赖资源回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneFailureCallback loadSceneFailureCallback,
      LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
      : this(loadSceneSuccessCallback, loadSceneFailureCallback, (LoadSceneUpdateCallback) null, loadSceneDependencyAssetCallback)
    {
    }

    /// <summary>初始化加载场景回调函数集的新实例。</summary>
    /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
    /// <param name="loadSceneFailureCallback">加载场景失败回调函数。</param>
    /// <param name="loadSceneUpdateCallback">加载场景更新回调函数。</param>
    /// <param name="loadSceneDependencyAssetCallback">加载场景时加载依赖资源回调函数。</param>
    public LoadSceneCallbacks(
      LoadSceneSuccessCallback loadSceneSuccessCallback,
      LoadSceneFailureCallback loadSceneFailureCallback,
      LoadSceneUpdateCallback loadSceneUpdateCallback,
      LoadSceneDependencyAssetCallback loadSceneDependencyAssetCallback)
    {
      this.m_LoadSceneSuccessCallback = loadSceneSuccessCallback != null ? loadSceneSuccessCallback : throw new GameFrameworkException("Load scene success callback is invalid.");
      this.m_LoadSceneFailureCallback = loadSceneFailureCallback;
      this.m_LoadSceneUpdateCallback = loadSceneUpdateCallback;
      this.m_LoadSceneDependencyAssetCallback = loadSceneDependencyAssetCallback;
    }

    /// <summary>获取加载场景成功回调函数。</summary>
    public LoadSceneSuccessCallback LoadSceneSuccessCallback => this.m_LoadSceneSuccessCallback;

    /// <summary>获取加载场景失败回调函数。</summary>
    public LoadSceneFailureCallback LoadSceneFailureCallback => this.m_LoadSceneFailureCallback;

    /// <summary>获取加载场景更新回调函数。</summary>
    public LoadSceneUpdateCallback LoadSceneUpdateCallback => this.m_LoadSceneUpdateCallback;

    /// <summary>获取加载场景时加载依赖资源回调函数。</summary>
    public LoadSceneDependencyAssetCallback LoadSceneDependencyAssetCallback => this.m_LoadSceneDependencyAssetCallback;
  }
}
