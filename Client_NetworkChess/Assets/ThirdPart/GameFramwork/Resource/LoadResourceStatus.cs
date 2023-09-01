// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceStatus
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源状态。</summary>
  public enum LoadResourceStatus : byte
  {
    /// <summary>加载资源完成。</summary>
    Success,
    /// <summary>资源不存在。</summary>
    NotExist,
    /// <summary>资源尚未准备完毕。</summary>
    NotReady,
    /// <summary>依赖资源错误。</summary>
    DependencyError,
    /// <summary>资源类型错误。</summary>
    TypeError,
    /// <summary>加载资源错误。</summary>
    AssetError,
  }
}
