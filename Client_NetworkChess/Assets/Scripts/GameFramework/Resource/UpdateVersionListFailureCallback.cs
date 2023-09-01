// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdateVersionListFailureCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>版本资源列表更新失败回调函数。</summary>
  /// <param name="downloadUri">版本资源列表更新地址。</param>
  /// <param name="errorMessage">错误信息。</param>
  public delegate void UpdateVersionListFailureCallback(string downloadUri, string errorMessage);
}
