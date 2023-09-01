// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdateResourcesCompleteCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>使用可更新模式并更新指定资源组完成时的回调函数。</summary>
  /// <param name="resourceGroup">更新的资源组。</param>
  /// <param name="result">更新资源结果，全部成功为 true，否则为 false。</param>
  public delegate void UpdateResourcesCompleteCallback(IResourceGroup resourceGroup, bool result);
}
