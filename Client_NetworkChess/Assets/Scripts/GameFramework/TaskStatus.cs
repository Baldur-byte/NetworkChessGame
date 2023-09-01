// Decompiled with JetBrains decompiler
// Type: GameFramework.TaskStatus
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>任务状态。</summary>
  public enum TaskStatus : byte
  {
    /// <summary>未开始。</summary>
    Todo,
    /// <summary>执行中。</summary>
    Doing,
    /// <summary>完成。</summary>
    Done,
  }
}
