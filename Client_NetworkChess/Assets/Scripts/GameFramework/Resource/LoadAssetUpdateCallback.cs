// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadAssetUpdateCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源更新回调函数。</summary>
  /// <param name="assetName">要加载的资源名称。</param>
  /// <param name="progress">加载资源进度。</param>
  /// <param name="userData">用户自定义数据。</param>
  public delegate void LoadAssetUpdateCallback(string assetName, float progress, object userData);
}
