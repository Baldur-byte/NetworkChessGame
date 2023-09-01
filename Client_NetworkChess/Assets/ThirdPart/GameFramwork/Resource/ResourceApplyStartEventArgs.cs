// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceApplyStartEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源应用开始事件。</summary>
  public sealed class ResourceApplyStartEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源应用开始事件的新实例。</summary>
    public ResourceApplyStartEventArgs()
    {
      this.ResourcePackPath = (string) null;
      this.Count = 0;
      this.TotalLength = 0L;
    }

    /// <summary>获取资源包路径。</summary>
    public string ResourcePackPath { get; private set; }

    /// <summary>获取要应用资源的数量。</summary>
    public int Count { get; private set; }

    /// <summary>获取要应用资源的总大小。</summary>
    public long TotalLength { get; private set; }

    /// <summary>创建资源应用开始事件。</summary>
    /// <param name="resourcePackPath">资源包路径。</param>
    /// <param name="count">要应用资源的数量。</param>
    /// <param name="totalLength">要应用资源的总大小。</param>
    /// <returns>创建的资源应用开始事件。</returns>
    public static ResourceApplyStartEventArgs Create(
      string resourcePackPath,
      int count,
      long totalLength)
    {
      ResourceApplyStartEventArgs applyStartEventArgs = ReferencePool.Acquire<ResourceApplyStartEventArgs>();
      applyStartEventArgs.ResourcePackPath = resourcePackPath;
      applyStartEventArgs.Count = count;
      applyStartEventArgs.TotalLength = totalLength;
      return applyStartEventArgs;
    }

    /// <summary>清理资源应用开始事件。</summary>
    public override void Clear()
    {
      this.ResourcePackPath = (string) null;
      this.Count = 0;
      this.TotalLength = 0L;
    }
  }
}
