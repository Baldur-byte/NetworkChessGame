// Decompiled with JetBrains decompiler
// Type: GameFramework.ReferencePoolInfo
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Runtime.InteropServices;

namespace GameFramework
{
  /// <summary>引用池信息。</summary>
  [StructLayout(LayoutKind.Auto)]
  public struct ReferencePoolInfo
  {
    private readonly Type m_Type;
    private readonly int m_UnusedReferenceCount;
    private readonly int m_UsingReferenceCount;
    private readonly int m_AcquireReferenceCount;
    private readonly int m_ReleaseReferenceCount;
    private readonly int m_AddReferenceCount;
    private readonly int m_RemoveReferenceCount;

    /// <summary>初始化引用池信息的新实例。</summary>
    /// <param name="type">引用池类型。</param>
    /// <param name="unusedReferenceCount">未使用引用数量。</param>
    /// <param name="usingReferenceCount">正在使用引用数量。</param>
    /// <param name="acquireReferenceCount">获取引用数量。</param>
    /// <param name="releaseReferenceCount">归还引用数量。</param>
    /// <param name="addReferenceCount">增加引用数量。</param>
    /// <param name="removeReferenceCount">移除引用数量。</param>
    public ReferencePoolInfo(
      Type type,
      int unusedReferenceCount,
      int usingReferenceCount,
      int acquireReferenceCount,
      int releaseReferenceCount,
      int addReferenceCount,
      int removeReferenceCount)
    {
      this.m_Type = type;
      this.m_UnusedReferenceCount = unusedReferenceCount;
      this.m_UsingReferenceCount = usingReferenceCount;
      this.m_AcquireReferenceCount = acquireReferenceCount;
      this.m_ReleaseReferenceCount = releaseReferenceCount;
      this.m_AddReferenceCount = addReferenceCount;
      this.m_RemoveReferenceCount = removeReferenceCount;
    }

    /// <summary>获取引用池类型。</summary>
    public Type Type => this.m_Type;

    /// <summary>获取未使用引用数量。</summary>
    public int UnusedReferenceCount => this.m_UnusedReferenceCount;

    /// <summary>获取正在使用引用数量。</summary>
    public int UsingReferenceCount => this.m_UsingReferenceCount;

    /// <summary>获取获取引用数量。</summary>
    public int AcquireReferenceCount => this.m_AcquireReferenceCount;

    /// <summary>获取归还引用数量。</summary>
    public int ReleaseReferenceCount => this.m_ReleaseReferenceCount;

    /// <summary>获取增加引用数量。</summary>
    public int AddReferenceCount => this.m_AddReferenceCount;

    /// <summary>获取移除引用数量。</summary>
    public int RemoveReferenceCount => this.m_RemoveReferenceCount;
  }
}
