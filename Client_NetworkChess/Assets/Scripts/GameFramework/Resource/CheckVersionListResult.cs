// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.CheckVersionListResult
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>检查版本资源列表结果。</summary>
  public enum CheckVersionListResult : byte
  {
    /// <summary>已经是最新的。</summary>
    Updated,
    /// <summary>需要更新。</summary>
    NeedUpdate,
  }
}
