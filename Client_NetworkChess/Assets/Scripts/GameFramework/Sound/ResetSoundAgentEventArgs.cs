// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.ResetSoundAgentEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>重置声音代理事件。</summary>
  public sealed class ResetSoundAgentEventArgs : GameFrameworkEventArgs
  {
    /// <summary>创建重置声音代理事件。</summary>
    /// <returns>创建的重置声音代理事件。</returns>
    public static ResetSoundAgentEventArgs Create() => ReferencePool.Acquire<ResetSoundAgentEventArgs>();

    /// <summary>清理重置声音代理事件。</summary>
    public override void Clear()
    {
    }
  }
}
