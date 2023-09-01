// Decompiled with JetBrains decompiler
// Type: GameFramework.Scene.LoadSceneSuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Scene
{
  /// <summary>加载场景成功事件。</summary>
  public sealed class LoadSceneSuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载场景成功事件的新实例。</summary>
    public LoadSceneSuccessEventArgs()
    {
      this.SceneAssetName = (string) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取场景资源名称。</summary>
    public string SceneAssetName { get; private set; }

    /// <summary>获取加载持续时间。</summary>
    public float Duration { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建加载场景成功事件。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的加载场景成功事件。</returns>
    public static LoadSceneSuccessEventArgs Create(
      string sceneAssetName,
      float duration,
      object userData)
    {
      LoadSceneSuccessEventArgs successEventArgs = ReferencePool.Acquire<LoadSceneSuccessEventArgs>();
      successEventArgs.SceneAssetName = sceneAssetName;
      successEventArgs.Duration = duration;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理加载场景成功事件。</summary>
    public override void Clear()
    {
      this.SceneAssetName = (string) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }
  }
}
