// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundErrorCode
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音错误码。</summary>
  public enum PlaySoundErrorCode : byte
  {
    /// <summary>未知错误。</summary>
    Unknown,
    /// <summary>声音组不存在。</summary>
    SoundGroupNotExist,
    /// <summary>声音组没有声音代理。</summary>
    SoundGroupHasNoAgent,
    /// <summary>加载资源失败。</summary>
    LoadAssetFailure,
    /// <summary>播放声音因优先级低被忽略。</summary>
    IgnoredDueToLowPriority,
    /// <summary>设置声音资源失败。</summary>
    SetSoundAssetFailure,
  }
}
