// Decompiled with JetBrains decompiler
// Type: GameFramework.Variable
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework
{
  /// <summary>变量。</summary>
  public abstract class Variable : IReference
  {
    /// <summary>获取变量类型。</summary>
    public abstract Type Type { get; }

    /// <summary>获取变量值。</summary>
    /// <returns>变量值。</returns>
    public abstract object GetValue();

    /// <summary>设置变量值。</summary>
    /// <param name="value">变量值。</param>
    public abstract void SetValue(object value);

    /// <summary>清理变量值。</summary>
    public abstract void Clear();
  }
}
