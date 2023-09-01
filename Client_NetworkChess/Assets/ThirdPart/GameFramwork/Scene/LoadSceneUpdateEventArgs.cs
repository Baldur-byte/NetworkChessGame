// Decompiled with JetBrains decompiler
// Type: GameFramework.Scene.LoadSceneUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Scene
{
  /// <summary>加载场景更新事件。</summary>
  public sealed class LoadSceneUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载场景更新事件的新实例。</summary>
    public LoadSceneUpdateEventArgs()
    {
      this.SceneAssetName = (string) null;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取场景资源名称。</summary>
    public string SceneAssetName { get; private set; }

    /// <summary>获取加载场景进度。</summary>
    public float Progress { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建加载场景更新事件。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="progress">加载场景进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的加载场景更新事件。</returns>
    public static LoadSceneUpdateEventArgs Create(
      string sceneAssetName,
      float progress,
      object userData)
    {
      LoadSceneUpdateEventArgs sceneUpdateEventArgs = ReferencePool.Acquire<LoadSceneUpdateEventArgs>();
      sceneUpdateEventArgs.SceneAssetName = sceneAssetName;
      sceneUpdateEventArgs.Progress = progress;
      sceneUpdateEventArgs.UserData = userData;
      return sceneUpdateEventArgs;
    }

    /// <summary>清理加载场景更新事件。</summary>
    public override void Clear()
    {
      this.SceneAssetName = (string) null;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }
  }
}
