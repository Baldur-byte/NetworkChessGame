// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.IResourceGroup
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Collections.Generic;

namespace GameFramework.Resource
{
  /// <summary>资源组接口。</summary>
  public interface IResourceGroup
  {
    /// <summary>获取资源组名称。</summary>
    string Name { get; }

    /// <summary>获取资源组是否准备完毕。</summary>
    bool Ready { get; }

    /// <summary>获取资源组包含资源数量。</summary>
    int TotalCount { get; }

    /// <summary>获取资源组中已准备完成资源数量。</summary>
    int ReadyCount { get; }

    /// <summary>获取资源组包含资源的总大小。</summary>
    long TotalLength { get; }

    /// <summary>获取资源组包含资源压缩后的总大小。</summary>
    long TotalCompressedLength { get; }

    /// <summary>获取资源组中已准备完成资源的总大小。</summary>
    long ReadyLength { get; }

    /// <summary>获取资源组中已准备完成资源压缩后的总大小。</summary>
    long ReadyCompressedLength { get; }

    /// <summary>获取资源组的完成进度。</summary>
    float Progress { get; }

    /// <summary>获取资源组包含的资源名称列表。</summary>
    /// <returns>资源组包含的资源名称列表。</returns>
    string[] GetResourceNames();

    /// <summary>获取资源组包含的资源名称列表。</summary>
    /// <param name="results">资源组包含的资源名称列表。</param>
    void GetResourceNames(List<string> results);
  }
}
