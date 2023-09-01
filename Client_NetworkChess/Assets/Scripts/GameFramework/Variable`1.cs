// Decompiled with JetBrains decompiler
// Type: GameFramework.Variable`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework
{
  /// <summary>变量。</summary>
  /// <typeparam name="T">变量类型。</typeparam>
  public abstract class Variable<T> : Variable
  {
    private T m_Value;

    /// <summary>初始化变量的新实例。</summary>
    public Variable() => this.m_Value = default (T);

    /// <summary>获取变量类型。</summary>
    public override Type Type => typeof (T);

    /// <summary>获取或设置变量值。</summary>
    public T Value
    {
      get => this.m_Value;
      set => this.m_Value = value;
    }

    /// <summary>获取变量值。</summary>
    /// <returns>变量值。</returns>
    public override object GetValue() => (object) this.m_Value;

    /// <summary>设置变量值。</summary>
    /// <param name="value">变量值。</param>
    public override void SetValue(object value) => this.m_Value = (T) value;

    /// <summary>清理变量值。</summary>
    public override void Clear() => this.m_Value = default (T);

    /// <summary>获取变量字符串。</summary>
    /// <returns>变量字符串。</returns>
    public override string ToString() => (object) this.m_Value == null ? "<Null>" : this.m_Value.ToString();
  }
}
