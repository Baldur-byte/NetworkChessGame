// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadBytesCallbacks
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载数据流回调函数集。</summary>
  public sealed class LoadBytesCallbacks
  {
    private readonly LoadBytesSuccessCallback m_LoadBytesSuccessCallback;
    private readonly LoadBytesFailureCallback m_LoadBytesFailureCallback;

    /// <summary>初始化加载数据流回调函数集的新实例。</summary>
    /// <param name="loadBinarySuccessCallback">加载数据流成功回调函数。</param>
    public LoadBytesCallbacks(LoadBytesSuccessCallback loadBinarySuccessCallback)
      : this(loadBinarySuccessCallback, (LoadBytesFailureCallback) null)
    {
    }

    /// <summary>初始化加载数据流回调函数集的新实例。</summary>
    /// <param name="loadBytesSuccessCallback">加载数据流成功回调函数。</param>
    /// <param name="loadBytesFailureCallback">加载数据流失败回调函数。</param>
    public LoadBytesCallbacks(
      LoadBytesSuccessCallback loadBytesSuccessCallback,
      LoadBytesFailureCallback loadBytesFailureCallback)
    {
      this.m_LoadBytesSuccessCallback = loadBytesSuccessCallback != null ? loadBytesSuccessCallback : throw new GameFrameworkException("Load bytes success callback is invalid.");
      this.m_LoadBytesFailureCallback = loadBytesFailureCallback;
    }

    /// <summary>获取加载数据流成功回调函数。</summary>
    public LoadBytesSuccessCallback LoadBytesSuccessCallback => this.m_LoadBytesSuccessCallback;

    /// <summary>获取加载数据流失败回调函数。</summary>
    public LoadBytesFailureCallback LoadBytesFailureCallback => this.m_LoadBytesFailureCallback;
  }
}
