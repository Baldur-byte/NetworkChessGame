// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdateVersionListCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>版本资源列表更新回调函数集。</summary>
  public sealed class UpdateVersionListCallbacks
  {
    private readonly UpdateVersionListSuccessCallback m_UpdateVersionListSuccessCallback;
    private readonly UpdateVersionListFailureCallback m_UpdateVersionListFailureCallback;

    /// <summary>初始化版本资源列表更新回调函数集的新实例。</summary>
    /// <param name="updateVersionListSuccessCallback">版本资源列表更新成功回调函数。</param>
    public UpdateVersionListCallbacks(
      UpdateVersionListSuccessCallback updateVersionListSuccessCallback)
      : this(updateVersionListSuccessCallback, (UpdateVersionListFailureCallback) null)
    {
    }

    /// <summary>初始化版本资源列表更新回调函数集的新实例。</summary>
    /// <param name="updateVersionListSuccessCallback">版本资源列表更新成功回调函数。</param>
    /// <param name="updateVersionListFailureCallback">版本资源列表更新失败回调函数。</param>
    public UpdateVersionListCallbacks(
      UpdateVersionListSuccessCallback updateVersionListSuccessCallback,
      UpdateVersionListFailureCallback updateVersionListFailureCallback)
    {
      this.m_UpdateVersionListSuccessCallback = updateVersionListSuccessCallback != null ? updateVersionListSuccessCallback : throw new GameFrameworkException("Update version list success callback is invalid.");
      this.m_UpdateVersionListFailureCallback = updateVersionListFailureCallback;
    }

    /// <summary>获取版本资源列表更新成功回调函数。</summary>
    public UpdateVersionListSuccessCallback UpdateVersionListSuccessCallback => this.m_UpdateVersionListSuccessCallback;

    /// <summary>获取版本资源列表更新失败回调函数。</summary>
    public UpdateVersionListFailureCallback UpdateVersionListFailureCallback => this.m_UpdateVersionListFailureCallback;
  }
}
