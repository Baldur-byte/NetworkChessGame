// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdateVersionListSuccessCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>版本资源列表更新成功回调函数。</summary>
  /// <param name="downloadPath">版本资源列表更新后存放路径。</param>
  /// <param name="downloadUri">版本资源列表更新地址。</param>
  public delegate void UpdateVersionListSuccessCallback(string downloadPath, string downloadUri);
}
