// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音更新事件。</summary>
  public sealed class PlaySoundUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化播放声音更新事件的新实例。</summary>
    public PlaySoundUpdateEventArgs()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.Progress = 0.0f;
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

    /// <summary>获取加载声音进度。</summary>
    public float Progress { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建播放声音更新事件。</summary>
    /// <param name="serialId">声音的序列编号。</param>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <param name="progress">加载声音进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的播放声音更新事件。</returns>
    public static PlaySoundUpdateEventArgs Create(
      int serialId,
      string soundAssetName,
      string soundGroupName,
      PlaySoundParams playSoundParams,
      float progress,
      object userData)
    {
      PlaySoundUpdateEventArgs soundUpdateEventArgs = ReferencePool.Acquire<PlaySoundUpdateEventArgs>();
      soundUpdateEventArgs.SerialId = serialId;
      soundUpdateEventArgs.SoundAssetName = soundAssetName;
      soundUpdateEventArgs.SoundGroupName = soundGroupName;
      soundUpdateEventArgs.PlaySoundParams = playSoundParams;
      soundUpdateEventArgs.Progress = progress;
      soundUpdateEventArgs.UserData = userData;
      return soundUpdateEventArgs;
    }

    /// <summary>清理播放声音更新事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }
  }
}
