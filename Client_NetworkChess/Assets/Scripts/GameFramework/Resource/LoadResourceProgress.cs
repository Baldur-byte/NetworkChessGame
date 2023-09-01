// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceProgress
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源进度类型。</summary>
  public enum LoadResourceProgress : byte
  {
    /// <summary>未知类型。</summary>
    Unknown,
    /// <summary>读取资源包。</summary>
    ReadResource,
    /// <summary>加载资源包。</summary>
    LoadResource,
    /// <summary>加载资源。</summary>
    LoadAsset,
    /// <summary>加载场景。</summary>
    LoadScene,
  }
}
