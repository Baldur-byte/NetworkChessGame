// Decompiled with JetBrains decompiler
// Type: GameFramework.BaseEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>事件基类。</summary>
  public abstract class BaseEventArgs : GameFrameworkEventArgs
  {
    /// <summary>获取类型编号。</summary>
    public abstract int Id { get; }
  }
}
