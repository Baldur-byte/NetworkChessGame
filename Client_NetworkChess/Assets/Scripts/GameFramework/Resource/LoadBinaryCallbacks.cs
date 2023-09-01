// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadBinaryCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载二进制资源回调函数集。</summary>
  public sealed class LoadBinaryCallbacks
  {
    private readonly LoadBinarySuccessCallback m_LoadBinarySuccessCallback;
    private readonly LoadBinaryFailureCallback m_LoadBinaryFailureCallback;

    /// <summary>初始化加载二进制资源回调函数集的新实例。</summary>
    /// <param name="loadBinarySuccessCallback">加载二进制资源成功回调函数。</param>
    public LoadBinaryCallbacks(
      LoadBinarySuccessCallback loadBinarySuccessCallback)
      : this(loadBinarySuccessCallback, (LoadBinaryFailureCallback) null)
    {
    }

    /// <summary>初始化加载二进制资源回调函数集的新实例。</summary>
    /// <param name="loadBinarySuccessCallback">加载二进制资源成功回调函数。</param>
    /// <param name="loadBinaryFailureCallback">加载二进制资源失败回调函数。</param>
    public LoadBinaryCallbacks(
      LoadBinarySuccessCallback loadBinarySuccessCallback,
      LoadBinaryFailureCallback loadBinaryFailureCallback)
    {
      this.m_LoadBinarySuccessCallback = loadBinarySuccessCallback != null ? loadBinarySuccessCallback : throw new GameFrameworkException("Load binary success callback is invalid.");
      this.m_LoadBinaryFailureCallback = loadBinaryFailureCallback;
    }

    /// <summary>获取加载二进制资源成功回调函数。</summary>
    public LoadBinarySuccessCallback LoadBinarySuccessCallback => this.m_LoadBinarySuccessCallback;

    /// <summary>获取加载二进制资源失败回调函数。</summary>
    public LoadBinaryFailureCallback LoadBinaryFailureCallback => this.m_LoadBinaryFailureCallback;
  }
}
