// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.FileSystemAccess
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.FileSystem
{
  /// <summary>文件系统访问方式。</summary>
  [Flags]
  public enum FileSystemAccess : byte
  {
    /// <summary>未指定。</summary>
    Unspecified = 0,
    /// <summary>只可读。</summary>
    Read = 1,
    /// <summary>只可写。</summary>
    Write = 2,
    /// <summary>可读写。</summary>
    ReadWrite = Write | Read, // 0x03
  }
}
