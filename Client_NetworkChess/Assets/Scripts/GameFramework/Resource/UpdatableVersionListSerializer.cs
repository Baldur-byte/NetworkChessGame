// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.UpdatableVersionListSerializer
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>可更新模式版本资源列表序列化器。</summary>
  public sealed class UpdatableVersionListSerializer : GameFrameworkSerializer<UpdatableVersionList>
  {
    private static readonly byte[] Header = new byte[3]
    {
      (byte) 71,
      (byte) 70,
      (byte) 85
    };

    /// <summary>获取可更新模式版本资源列表头标识。</summary>
    /// <returns>可更新模式版本资源列表头标识。</returns>
    protected override byte[] GetHeader() => UpdatableVersionListSerializer.Header;
  }
}
