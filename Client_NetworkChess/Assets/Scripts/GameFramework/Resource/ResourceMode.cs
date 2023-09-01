// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceMode
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源模式。</summary>
  public enum ResourceMode : byte
  {
    /// <summary>未指定。</summary>
    Unspecified,
    /// <summary>单机模式。</summary>
    Package,
    /// <summary>预下载的可更新模式。</summary>
    Updatable,
    /// <summary>使用时下载的可更新模式。</summary>
    UpdatableWhilePlaying,
  }
}
