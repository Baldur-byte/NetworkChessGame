// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceUpdateAllCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源更新全部完成事件。</summary>
  public sealed class ResourceUpdateAllCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>创建资源更新全部完成事件。</summary>
    /// <returns>创建的资源更新全部完成事件。</returns>
    public static ResourceUpdateAllCompleteEventArgs Create() => ReferencePool.Acquire<ResourceUpdateAllCompleteEventArgs>();

    /// <summary>清理资源更新全部完成事件。</summary>
    public override void Clear()
    {
    }
  }
}
