// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadBytesFailureCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载数据流失败回调函数。</summary>
  /// <param name="fileUri">文件路径。</param>
  /// <param name="errorMessage">错误信息。</param>
  /// <param name="userData">用户自定义数据。</param>
  public delegate void LoadBytesFailureCallback(
    string fileUri,
    string errorMessage,
    object userData);
}
