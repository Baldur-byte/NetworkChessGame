// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音失败事件。</summary>
  public sealed class PlaySoundFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化播放声音失败事件的新实例。</summary>
    public PlaySoundFailureEventArgs()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.ErrorCode = PlaySoundErrorCode.Unknown;
      this.ErrorMessage = (string) null;
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

    /// <summary>获取错误码。</summary>
    public PlaySoundErrorCode ErrorCode { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建播放声音失败事件。</summary>
    /// <param name="serialId">声音的序列编号。</param>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <param name="errorCode">错误码。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的播放声音失败事件。</returns>
    public static PlaySoundFailureEventArgs Create(
      int serialId,
      string soundAssetName,
      string soundGroupName,
      PlaySoundParams playSoundParams,
      PlaySoundErrorCode errorCode,
      string errorMessage,
      object userData)
    {
      PlaySoundFailureEventArgs failureEventArgs = ReferencePool.Acquire<PlaySoundFailureEventArgs>();
      failureEventArgs.SerialId = serialId;
      failureEventArgs.SoundAssetName = soundAssetName;
      failureEventArgs.SoundGroupName = soundGroupName;
      failureEventArgs.PlaySoundParams = playSoundParams;
      failureEventArgs.ErrorCode = errorCode;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理播放声音失败事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.SoundAssetName = (string) null;
      this.SoundGroupName = (string) null;
      this.PlaySoundParams = (PlaySoundParams) null;
      this.ErrorCode = PlaySoundErrorCode.Unknown;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
