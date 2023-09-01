// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UnloadSceneCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>卸载场景回调函数集。</summary>
  public sealed class UnloadSceneCallbacks
  {
    private readonly UnloadSceneSuccessCallback m_UnloadSceneSuccessCallback;
    private readonly UnloadSceneFailureCallback m_UnloadSceneFailureCallback;

    /// <summary>初始化卸载场景回调函数集的新实例。</summary>
    /// <param name="unloadSceneSuccessCallback">卸载场景成功回调函数。</param>
    public UnloadSceneCallbacks(
      UnloadSceneSuccessCallback unloadSceneSuccessCallback)
      : this(unloadSceneSuccessCallback, (UnloadSceneFailureCallback) null)
    {
    }

    /// <summary>初始化卸载场景回调函数集的新实例。</summary>
    /// <param name="unloadSceneSuccessCallback">卸载场景成功回调函数。</param>
    /// <param name="unloadSceneFailureCallback">卸载场景失败回调函数。</param>
    public UnloadSceneCallbacks(
      UnloadSceneSuccessCallback unloadSceneSuccessCallback,
      UnloadSceneFailureCallback unloadSceneFailureCallback)
    {
      this.m_UnloadSceneSuccessCallback = unloadSceneSuccessCallback != null ? unloadSceneSuccessCallback : throw new GameFrameworkException("Unload scene success callback is invalid.");
      this.m_UnloadSceneFailureCallback = unloadSceneFailureCallback;
    }

    /// <summary>获取卸载场景成功回调函数。</summary>
    public UnloadSceneSuccessCallback UnloadSceneSuccessCallback => this.m_UnloadSceneSuccessCallback;

    /// <summary>获取卸载场景失败回调函数。</summary>
    public UnloadSceneFailureCallback UnloadSceneFailureCallback => this.m_UnloadSceneFailureCallback;
  }
}
