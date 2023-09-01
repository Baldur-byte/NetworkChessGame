// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundDependencyAssetEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音时加载依赖资源事件。</summary>
  public sealed class PlaySoundDependencyAssetEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化播放声音时加载依赖资源事件的新实例。</summary>
    public PlaySoundDependencyAssetEventArgs()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }

    /// <summary>获取声音的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取声音资源名称。</summary>
    public string SoundAssetName { get; private set; }

    /// <summary>获取声音组名称。</summary>
    public string SoundGroupName { get; private set; }

    /// <summary>获取播放声音参数。</summary>
    public PlaySoundParams PlaySoundParams { get; private set; }

    /// <summary>获取被加载的依赖资源名称。</summary>
    public string DependencyAssetName { get; private set; }

    /// <summary>获取当前已加载依赖资源数量。</summary>
    public int LoadedCount { get; private set; }

    /// <summary>获取总共加载依赖资源数量。</summary>
    public int TotalCount { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建播放声音时加载依赖资源事件。</summary>
    /// <param name="serialId">声音的序列编号。</param>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <param name="dependencyAssetName">被加载的依赖资源名称。</param>
    /// <param name="loadedCount">当前已加载依赖资源数量。</param>
    /// <param name="totalCount">总共加载依赖资源数量。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的播放声音时加载依赖资源事件。</returns>
    public static PlaySoundDependencyAssetEventArgs Create(
      int serialId,
      string soundAssetName,
      string soundGroupName,
      PlaySoundParams playSoundParams,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      PlaySoundDependencyAssetEventArgs dependencyAssetEventArgs = ReferencePool.Acquire<PlaySoundDependencyAssetEventArgs>();
      dependencyAssetEventArgs.SerialId = serialId;
      dependencyAssetEventArgs.SoundAssetName = soundAssetName;
      dependencyAssetEventArgs.SoundGroupName = soundGroupName;
      dependencyAssetEventArgs.PlaySoundParams = playSoundParams;
      dependencyAssetEventArgs.DependencyAssetName = dependencyAssetName;
      dependencyAssetEventArgs.LoadedCount = loadedCount;
      dependencyAssetEventArgs.TotalCount = totalCount;
      dependencyAssetEventArgs.UserData = userData;
      return dependencyAssetEventArgs;
    }

    /// <summary>清理播放声音时加载依赖资源事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.DependencyAssetName = (string) null;
      this.LoadedCount = 0;
      this.TotalCount = 0;
      this.UserData = (object) null;
    }
  }
}
