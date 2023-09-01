// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.IResourceHelper
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源辅助器接口。</summary>
  public interface IResourceHelper
  {
    /// <summary>直接从指定文件路径加载数据流。</summary>
    /// <param name="fileUri">文件路径。</param>
    /// <param name="loadBytesCallbacks">加载数据流回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    void LoadBytes(string fileUri, LoadBytesCallbacks loadBytesCallbacks, object userData);

    /// <summary>卸载场景。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="unloadSceneCallbacks">卸载场景回调函数集。</param>
    /// <param name="userData">用户自定义数据。</param>
    void UnloadScene(
      string sceneAssetName,
      UnloadSceneCallbacks unloadSceneCallbacks,
      object userData);

    /// <summary>释放资源。</summary>
    /// <param name="objectToRelease">要释放的资源。</param>
    void Release(object objectToRelease);
  }
}
