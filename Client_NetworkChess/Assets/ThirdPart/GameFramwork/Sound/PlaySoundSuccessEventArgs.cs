// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundSuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音成功事件。</summary>
  public sealed class PlaySoundSuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化播放声音成功事件的新实例。</summary>
    public PlaySoundSuccessEventArgs()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundAgent = (ISoundAgent) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取声音的序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取声音资源名称。</summary>
    public string SoundAssetName { get; private set; }

    /// <summary>获取用于播放的声音代理。</summary>
    public ISoundAgent SoundAgent { get; private set; }

    /// <summary>获取加载持续时间。</summary>
    public float Duration { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建播放声音成功事件。</summary>
    /// <param name="serialId">声音的序列编号。</param>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundAgent">用于播放的声音代理。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的播放声音成功事件。</returns>
    public static PlaySoundSuccessEventArgs Create(
      int serialId,
      string soundAssetName,
      ISoundAgent soundAgent,
      float duration,
      object userData)
    {
      PlaySoundSuccessEventArgs successEventArgs = ReferencePool.Acquire<PlaySoundSuccessEventArgs>();
      successEventArgs.SerialId = serialId;
      successEventArgs.SoundAssetName = soundAssetName;
      successEventArgs.SoundAgent = soundAgent;
      successEventArgs.Duration = duration;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理播放声音成功事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundAgent = (ISoundAgent) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }
  }
}
