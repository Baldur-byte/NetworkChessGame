// Decompiled with JetBrains decompiler
// Type: GameFramework.FileSystem.FileSystemManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.FileSystem
{
  /// <summary>文件系统管理器。</summary>
  internal sealed class FileSystemManager : GameFrameworkModule, IFileSystemManager
  {
    private readonly Dictionary<string, GameFramework.FileSystem.FileSystem> m_FileSystems;
    private IFileSystemHelper m_FileSystemHelper;

    /// <summary>初始化文件系统管理器的新实例。</summary>
    public FileSystemManager()
    {
      this.m_FileSystems = new Dictionary<string, GameFramework.FileSystem.FileSystem>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_FileSystemHelper = (IFileSystemHelper) null;
    }

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 4;

    /// <summary>获取文件系统数量。</summary>
    public int Count => this.m_FileSystems.Count;

    /// <summary>文件系统管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理文件系统管理器。</summary>
    internal override void Shutdown()
    {
      while (this.m_FileSystems.Count > 0)
      {
        using (Dictionary<string, GameFramework.FileSystem.FileSystem>.Enumerator enumerator = this.m_FileSystems.GetEnumerator())
        {
          if (enumerator.MoveNext())
            this.DestroyFileSystem((IFileSystem) enumerator.Current.Value, false);
        }
      }
    }

    /// <summary>设置文件系统辅助器。</summary>
    /// <param name="fileSystemHelper">文件系统辅助器。</param>
    public void SetFileSystemHelper(IFileSystemHelper fileSystemHelper) => this.m_FileSystemHelper = fileSystemHelper != null ? fileSystemHelper : throw new GameFrameworkException("File system helper is invalid.");

    /// <summary>检查是否存在文件系统。</summary>
    /// <param name="fullPath">要检查的文件系统的完整路径。</param>
    /// <returns>是否存在文件系统。</returns>
    public bool HasFileSystem(string fullPath) => !string.IsNullOrEmpty(fullPath) ? this.m_FileSystems.ContainsKey(Utility.Path.GetRegularPath(fullPath)) : throw new GameFrameworkException("Full path is invalid.");

    /// <summary>获取文件系统。</summary>
    /// <param name="fullPath">要获取的文件系统的完整路径。</param>
    /// <returns>获取的文件系统。</returns>
    public IFileSystem GetFileSystem(string fullPath)
    {
      if (string.IsNullOrEmpty(fullPath))
        throw new GameFrameworkException("Full path is invalid.");
      GameFramework.FileSystem.FileSystem fileSystem = (GameFramework.FileSystem.FileSystem) null;
      return this.m_FileSystems.TryGetValue(Utility.Path.GetRegularPath(fullPath), out fileSystem) ? (IFileSystem) fileSystem : (IFileSystem) null;
    }

    /// <summary>创建文件系统。</summary>
    /// <param name="fullPath">要创建的文件系统的完整路径。</param>
    /// <param name="access">要创建的文件系统的访问方式。</param>
    /// <param name="maxFileCount">要创建的文件系统的最大文件数量。</param>
    /// <param name="maxBlockCount">要创建的文件系统的最大块数据数量。</param>
    /// <returns>创建的文件系统。</returns>
    public IFileSystem CreateFileSystem(
      string fullPath,
      FileSystemAccess access,
      int maxFileCount,
      int maxBlockCount)
    {
      if (this.m_FileSystemHelper == null)
        throw new GameFrameworkException("File system helper is invalid.");
      if (string.IsNullOrEmpty(fullPath))
        throw new GameFrameworkException("Full path is invalid.");
      if (access == FileSystemAccess.Unspecified)
        throw new GameFrameworkException("Access is invalid.");
      if (access == FileSystemAccess.Read)
        throw new GameFrameworkException("Access read is invalid.");
      fullPath = Utility.Path.GetRegularPath(fullPath);
      if (this.m_FileSystems.ContainsKey(fullPath))
        throw new GameFrameworkException(Utility.Text.Format<string>("File system '{0}' is already exist.", fullPath));
      GameFramework.FileSystem.FileSystem fileSystem = GameFramework.FileSystem.FileSystem.Create(fullPath, access, this.m_FileSystemHelper.CreateFileSystemStream(fullPath, access, true) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Create file system stream for '{0}' failure.", fullPath)), maxFileCount, maxBlockCount);
      if (fileSystem == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Create file system '{0}' failure.", fullPath));
      this.m_FileSystems.Add(fullPath, fileSystem);
      return (IFileSystem) fileSystem;
    }

    /// <summary>加载文件系统。</summary>
    /// <param name="fullPath">要加载的文件系统的完整路径。</param>
    /// <param name="access">要加载的文件系统的访问方式。</param>
    /// <returns>加载的文件系统。</returns>
    public IFileSystem LoadFileSystem(string fullPath, FileSystemAccess access)
    {
      if (this.m_FileSystemHelper == null)
        throw new GameFrameworkException("File system helper is invalid.");
      if (string.IsNullOrEmpty(fullPath))
        throw new GameFrameworkException("Full path is invalid.");
      if (access == FileSystemAccess.Unspecified)
        throw new GameFrameworkException("Access is invalid.");
      fullPath = Utility.Path.GetRegularPath(fullPath);
      if (this.m_FileSystems.ContainsKey(fullPath))
        throw new GameFrameworkException(Utility.Text.Format<string>("File system '{0}' is already exist.", fullPath));
      GameFramework.FileSystem.FileSystem fileSystem = GameFramework.FileSystem.FileSystem.Load(fullPath, access, this.m_FileSystemHelper.CreateFileSystemStream(fullPath, access, false) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Create file system stream for '{0}' failure.", fullPath)));
      if (fileSystem == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Load file system '{0}' failure.", fullPath));
      this.m_FileSystems.Add(fullPath, fileSystem);
      return (IFileSystem) fileSystem;
    }

    /// <summary>销毁文件系统。</summary>
    /// <param name="fileSystem">要销毁的文件系统。</param>
    /// <param name="deletePhysicalFile">是否删除文件系统对应的物理文件。</param>
    public void DestroyFileSystem(IFileSystem fileSystem, bool deletePhysicalFile)
    {
      string str = fileSystem != null ? fileSystem.FullPath : throw new GameFrameworkException("File system is invalid.");
      ((GameFramework.FileSystem.FileSystem) fileSystem).Shutdown();
      this.m_FileSystems.Remove(str);
      if (!deletePhysicalFile || !File.Exists(str))
        return;
      File.Delete(str);
    }

    /// <summary>获取所有文件系统集合。</summary>
    /// <returns>获取的所有文件系统集合。</returns>
    public IFileSystem[] GetAllFileSystems()
    {
      int num = 0;
      IFileSystem[] allFileSystems = new IFileSystem[this.m_FileSystems.Count];
      foreach (KeyValuePair<string, GameFramework.FileSystem.FileSystem> fileSystem in this.m_FileSystems)
        allFileSystems[num++] = (IFileSystem) fileSystem.Value;
      return allFileSystems;
    }

    /// <summary>获取所有文件系统集合。</summary>
    /// <param name="results">获取的所有文件系统集合。</param>
    public void GetAllFileSystems(List<IFileSystem> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, GameFramework.FileSystem.FileSystem> fileSystem in this.m_FileSystems)
        results.Add((IFileSystem) fileSystem.Value);
    }
  }
}
