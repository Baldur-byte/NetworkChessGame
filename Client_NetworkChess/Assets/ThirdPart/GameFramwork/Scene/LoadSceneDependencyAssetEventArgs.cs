// Decompiled with JetBrains decompiler
// Type: GameFramework.Scene.LoadSceneDependencyAssetEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Scene
{
  /// <summary>加载场景时加载依赖资源事件。</summary>
  public sealed class LoadSceneDependencyAssetEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载场景时加载依赖资源事件的新实例。</summary>
    public LoadSceneDependencyAssetEventArgs()
    {
      this.SceneAssetName = (string) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }

    /// <summary>获取场景资源名称。</summary>
    public string SceneAssetName { get; private set; }

    /// <summary>获取被加载的依赖资源名称。</summary>
    public string DependencyAssetName { get; private set; }

    /// <summary>获取当前已加载依赖资源数量。</summary>
    public int LoadedCount { get; private set; }

    /// <summary>获取总共加载依赖资源数量。</summary>
    public int TotalCount { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建场景时加载依赖资源事件。</summary>
    /// <param name="sceneAssetName">场景资源名称。</param>
    /// <param name="dependencyAssetName">被加载的依赖资源名称。</param>
    /// <param name="loadedCount">当前已加载依赖资源数量。</param>
    /// <param name="totalCount">总共加载依赖资源数量。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的场景时加载依赖资源事件。</returns>
    public static LoadSceneDependencyAssetEventArgs Create(
      string sceneAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      LoadSceneDependencyAssetEventArgs dependencyAssetEventArgs = ReferencePool.Acquire<LoadSceneDependencyAssetEventArgs>();
      dependencyAssetEventArgs.SceneAssetName = sceneAssetName;
      dependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
      dependencyAssetEventArgs.LoadedCount = loadedCount;
      dependencyAssetEventArgs.TotalCount = totalCount;
      dependencyAssetEventArgs.UserData = userData;
      return dependencyAssetEventArgs;
    }

    /// <summary>清理场景时加载依赖资源事件。</summary>
    public override void Clear()
    {
      this.SceneAssetName = (string) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }
  }
}
