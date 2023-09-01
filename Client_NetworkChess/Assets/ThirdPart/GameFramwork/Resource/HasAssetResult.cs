// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.HasAssetResult
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>检查资源是否存在的结果。</summary>
  public enum HasAssetResult : byte
  {
    /// <summary>资源不存在。</summary>
    NotExist,
    /// <summary>资源尚未准备完毕。</summary>
    NotReady,
    /// <summary>存在资源且存储在磁盘上。</summary>
    AssetOnDisk,
    /// <summary>存在资源且存储在文件系统里。</summary>
    AssetOnFileSystem,
    /// <summary>存在二进制资源且存储在磁盘上。</summary>
    BinaryOnDisk,
    /// <summary>存在二进制资源且存储在文件系统里。</summary>
    BinaryOnFileSystem,
  }
}
