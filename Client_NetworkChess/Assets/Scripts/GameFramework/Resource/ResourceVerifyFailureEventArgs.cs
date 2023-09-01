// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceVerifyFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源校验失败事件。</summary>
  public sealed class ResourceVerifyFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源校验失败事件的新实例。</summary>
    public ResourceVerifyFailureEventArgs() => this.Name = (string) null;

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>创建资源校验失败事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <returns>创建的资源校验失败事件。</returns>
    public static ResourceVerifyFailureEventArgs Create(string name)
    {
      ResourceVerifyFailureEventArgs failureEventArgs = ReferencePool.Acquire<ResourceVerifyFailureEventArgs>();
      failureEventArgs.Name = name;
      return failureEventArgs;
    }

    /// <summary>清理资源校验失败事件。</summary>
    public override void Clear() => this.Name = (string) null;
  }
}
