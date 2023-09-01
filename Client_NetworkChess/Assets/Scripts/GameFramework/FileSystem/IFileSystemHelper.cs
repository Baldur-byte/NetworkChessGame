// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.IFileSystemHelper
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.FileSystem
{
  /// <summary>文件系统辅助器接口。</summary>
  public interface IFileSystemHelper
  {
    /// <summary>创建文件系统流。</summary>
    /// <param name="fullPath">要加载的文件系统的完整路径。</param>
    /// <param name="access">要加载的文件系统的访问方式。</param>
    /// <param name="createNew">是否创建新的文件系统流。</param>
    /// <returns>创建的文件系统流。</returns>
    FileSystemStream CreateFileSystemStream(
      string fullPath,
      FileSystemAccess access,
      bool createNew);
  }
}
