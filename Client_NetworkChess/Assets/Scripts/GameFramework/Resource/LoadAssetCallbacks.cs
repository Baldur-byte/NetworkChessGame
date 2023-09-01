// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadAssetCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源回调函数集。</summary>
  public sealed class LoadAssetCallbacks
  {
    private readonly LoadAssetSuccessCallback m_LoadAssetSuccessCallback;
    private readonly LoadAssetFailureCallback m_LoadAssetFailureCallback;
    private readonly LoadAssetUpdateCallback m_LoadAssetUpdateCallback;
    private readonly LoadAssetDependencyAssetCallback m_LoadAssetDependencyAssetCallback;

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback)
      : this(loadAssetSuccessCallback, (LoadAssetFailureCallback) null, (LoadAssetUpdateCallback) null, (LoadAssetDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetFailureCallback loadAssetFailureCallback)
      : this(loadAssetSuccessCallback, loadAssetFailureCallback, (LoadAssetUpdateCallback) null, (LoadAssetDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetUpdateCallback loadAssetUpdateCallback)
      : this(loadAssetSuccessCallback, (LoadAssetFailureCallback) null, loadAssetUpdateCallback, (LoadAssetDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
      : this(loadAssetSuccessCallback, (LoadAssetFailureCallback) null, (LoadAssetUpdateCallback) null, loadAssetDependencyAssetCallback)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
    /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetFailureCallback loadAssetFailureCallback,
      LoadAssetUpdateCallback loadAssetUpdateCallback)
      : this(loadAssetSuccessCallback, loadAssetFailureCallback, loadAssetUpdateCallback, (LoadAssetDependencyAssetCallback) null)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
    /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetFailureCallback loadAssetFailureCallback,
      LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
      : this(loadAssetSuccessCallback, loadAssetFailureCallback, (LoadAssetUpdateCallback) null, loadAssetDependencyAssetCallback)
    {
    }

    /// <summary>初始化加载资源回调函数集的新实例。</summary>
    /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
    /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
    /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
    /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
    public LoadAssetCallbacks(
      LoadAssetSuccessCallback loadAssetSuccessCallback,
      LoadAssetFailureCallback loadAssetFailureCallback,
      LoadAssetUpdateCallback loadAssetUpdateCallback,
      LoadAssetDependencyAssetCallback loadAssetDependencyAssetCallback)
    {
      this.m_LoadAssetSuccessCallback = loadAssetSuccessCallback != null ? loadAssetSuccessCallback : throw new GameFrameworkException("Load asset success callback is invalid.");
      this.m_LoadAssetFailureCallback = loadAssetFailureCallback;
      this.m_LoadAssetUpdateCallback = loadAssetUpdateCallback;
      this.m_LoadAssetDependencyAssetCallback = loadAssetDependencyAssetCallback;
    }

    /// <summary>获取加载资源成功回调函数。</summary>
    public LoadAssetSuccessCallback LoadAssetSuccessCallback => this.m_LoadAssetSuccessCallback;

    /// <summary>获取加载资源失败回调函数。</summary>
    public LoadAssetFailureCallback LoadAssetFailureCallback => this.m_LoadAssetFailureCallback;

    /// <summary>获取加载资源更新回调函数。</summary>
    public LoadAssetUpdateCallback LoadAssetUpdateCallback => this.m_LoadAssetUpdateCallback;

    /// <summary>获取加载资源时加载依赖资源回调函数。</summary>
    public LoadAssetDependencyAssetCallback LoadAssetDependencyAssetCallback => this.m_LoadAssetDependencyAssetCallback;
  }
}
