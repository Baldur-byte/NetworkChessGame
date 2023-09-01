// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadAssetSuccessCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源成功回调函数。</summary>
  /// <param name="assetName">要加载的资源名称。</param>
  /// <param name="asset">已加载的资源。</param>
  /// <param name="duration">加载持续时间。</param>
  /// <param name="userData">用户自定义数据。</param>
  public delegate void LoadAssetSuccessCallback(
    string assetName,
    object asset,
    float duration,
    object userData);
}
