﻿// Decompiled with JetBrains decompiler
// Type: GameFramework.TypeNamePair
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Runtime.InteropServices;

namespace GameFramework
{
  /// <summary>类型和名称的组合值。</summary>
  [StructLayout(LayoutKind.Auto)]
  internal struct TypeNamePair : IEquatable<TypeNamePair>
  {
    private readonly Type m_Type;
    private readonly string m_Name;

    /// <summary>初始化类型和名称的组合值的新实例。</summary>
    /// <param name="type">类型。</param>
    public TypeNamePair(Type type)
      : this(type, string.Empty)
    {
    }

    /// <summary>初始化类型和名称的组合值的新实例。</summary>
    /// <param name="type">类型。</param>
    /// <param name="name">名称。</param>
    public TypeNamePair(Type type, string name)
    {
      this.m_Type = type != null ? type : throw new GameFrameworkException("Type is invalid.");
      this.m_Name = name ?? string.Empty;
    }

    /// <summary>获取类型。</summary>
    public Type Type => this.m_Type;

    /// <summary>获取名称。</summary>
    public string Name => this.m_Name;

    /// <summary>获取类型和名称的组合值字符串。</summary>
    /// <returns>类型和名称的组合值字符串。</returns>
    public override string ToString()
    {
      if (this.m_Type == null)
        throw new GameFrameworkException("Type is invalid.");
      string fullName = this.m_Type.FullName;
      return !string.IsNullOrEmpty(this.m_Name) ? Utility.Text.Format<string, string>("{0}.{1}", fullName, this.m_Name) : fullName;
    }

    /// <summary>获取对象的哈希值。</summary>
    /// <returns>对象的哈希值。</returns>
    public override int GetHashCode() => this.m_Type.GetHashCode() ^ this.m_Name.GetHashCode();

    /// <summary>比较对象是否与自身相等。</summary>
    /// <param name="obj">要比较的对象。</param>
    /// <returns>被比较的对象是否与自身相等。</returns>
    public override bool Equals(object obj) => obj is TypeNamePair typeNamePair && this.Equals(typeNamePair);

    /// <summary>比较对象是否与自身相等。</summary>
    /// <param name="value">要比较的对象。</param>
    /// <returns>被比较的对象是否与自身相等。</returns>
    public bool Equals(TypeNamePair value) => this.m_Type == value.m_Type && this.m_Name == value.m_Name;

    /// <summary>判断两个对象是否相等。</summary>
    /// <param name="a">值 a。</param>
    /// <param name="b">值 b。</param>
    /// <returns>两个对象是否相等。</returns>
    public static bool operator ==(TypeNamePair a, TypeNamePair b) => a.Equals(b);

    /// <summary>判断两个对象是否不相等。</summary>
    /// <param name="a">值 a。</param>
    /// <param name="b">值 b。</param>
    /// <returns>两个对象是否不相等。</returns>
    public static bool operator !=(TypeNamePair a, TypeNamePair b) => !(a == b);
  }
}