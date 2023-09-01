// Decompiled with JetBrains decompiler
// Type: GameFramework.ReadDataDependencyAssetEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>读取数据时加载依赖资源事件。</summary>
  public sealed class ReadDataDependencyAssetEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化读取数据时加载依赖资源事件的新实例。</summary>
    public ReadDataDependencyAssetEventArgs()
    {
      this.DataAssetName = (string) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }

    /// <summary>获取内容资源名称。</summary>
    public string DataAssetName { get; private set; }

    /// <summary>获取被加载的依赖资源名称。</summary>
    public string DependencyAssetName { get; private set; }

    /// <summary>获取当前已加载依赖资源数量。</summary>
    public int LoadedCount { get; private set; }

    /// <summary>获取总共加载依赖资源数量。</summary>
    public int TotalCount { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建读取数据时加载依赖资源事件。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="dependencyAssetName">被加载的依赖资源名称。</param>
    /// <param name="loadedCount">当前已加载依赖资源数量。</param>
    /// <param name="totalCount">总共加载依赖资源数量。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的读取数据时加载依赖资源事件。</returns>
    public static ReadDataDependencyAssetEventArgs Create(
      string dataAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      ReadDataDependencyAssetEventArgs dependencyAssetEventArgs = ReferencePool.Acquire<ReadDataDependencyAssetEventArgs>();
      dependencyAssetEventArgs.DataAssetName = dataAssetName;
      dependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
      dependencyAssetEventArgs.LoadedCount = loadedCount;
      dependencyAssetEventArgs.TotalCount = totalCount;
      dependencyAssetEventArgs.UserData = userData;
      return dependencyAssetEventArgs;
    }

    /// <summary>清理读取数据时加载依赖资源事件。</summary>
    public override void Clear()
    {
      this.DataAssetName = (string) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }
  }
}
