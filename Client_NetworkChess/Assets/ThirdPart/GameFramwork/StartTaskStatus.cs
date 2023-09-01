// Decompiled with JetBrains decompiler
// Type: GameFramework.StartTaskStatus
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>开始处理任务的状态。</summary>
  public enum StartTaskStatus : byte
  {
    /// <summary>可以立刻处理完成此任务。</summary>
    Done,
    /// <summary>可以继续处理此任务。</summary>
    CanResume,
    /// <summary>不能继续处理此任务，需等待其它任务执行完成。</summary>
    HasToWait,
    /// <summary>不能继续处理此任务，出现未知错误。</summary>
    UnknownError,
  }
}
