// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceVerifyStartEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源校验开始事件。</summary>
  public sealed class ResourceVerifyStartEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源校验开始事件的新实例。</summary>
    public ResourceVerifyStartEventArgs()
    {
      this.Count = 0;
      this.TotalLength = 0L;
    }

    /// <summary>获取要校验资源的数量。</summary>
    public int Count { get; private set; }

    /// <summary>获取要校验资源的总大小。</summary>
    public long TotalLength { get; private set; }

    /// <summary>创建资源校验开始事件。</summary>
    /// <param name="count">要校验资源的数量。</param>
    /// <param name="totalLength">要校验资源的总大小。</param>
    /// <returns>创建的资源校验开始事件。</returns>
    public static ResourceVerifyStartEventArgs Create(int count, long totalLength)
    {
      ResourceVerifyStartEventArgs verifyStartEventArgs = ReferencePool.Acquire<ResourceVerifyStartEventArgs>();
      verifyStartEventArgs.Count = count;
      verifyStartEventArgs.TotalLength = totalLength;
      return verifyStartEventArgs;
    }

    /// <summary>清理资源校验开始事件。</summary>
    public override void Clear()
    {
      this.Count = 0;
      this.TotalLength = 0L;
    }
  }
}
