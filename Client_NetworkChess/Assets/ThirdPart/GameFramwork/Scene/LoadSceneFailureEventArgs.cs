// Decompiled with JetBrains decompiler
// Type: GameFramework.Scene.LoadSceneFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Scene
{
  /// <summary>加载场景失败事件。</summary>
  public sealed class LoadSceneFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载场景失败事件的新实例。</summary>
    public LoadSceneFailureEventArgs()
    {
      this.SceneAssetName = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取场景资源名称。</summary>
    public string SceneAssetName { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建加载场景失败事件。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的加载场景失败事件。</returns>
    public static LoadSceneFailureEventArgs Create(
      string sceneAssetName,
      string errorMessage,
      object userData)
    {
      LoadSceneFailureEventArgs failureEventArgs = ReferencePool.Acquire<LoadSceneFailureEventArgs>();
      failureEventArgs.SceneAssetName = sceneAssetName;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理加载场景失败事件。</summary>
    public override void Clear()
    {
      this.SceneAssetName = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
