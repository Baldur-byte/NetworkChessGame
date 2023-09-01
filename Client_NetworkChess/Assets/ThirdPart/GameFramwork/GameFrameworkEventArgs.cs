// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework
{
  /// <summary>游戏框架中包含事件数据的类的基类。</summary>
  public abstract class GameFrameworkEventArgs : EventArgs, IReference
  {
    /// <summary>清理引用。</summary>
    public abstract void Clear();
  }
}
