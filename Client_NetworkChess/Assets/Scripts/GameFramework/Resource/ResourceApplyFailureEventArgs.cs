// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceApplyFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源应用失败事件。</summary>
  public sealed class ResourceApplyFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源应用失败事件的新实例。</summary>
    public ResourceApplyFailureEventArgs()
    {
      this.Name = (string) null;
      this.ResourcePackPath = (string) null;
      this.ErrorMessage = (string) null;
    }

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>获取资源包路径。</summary>
    public string ResourcePackPath { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>创建资源应用失败事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <param name="resourcePackPath">资源包路径。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>创建的资源应用失败事件。</returns>
    public static ResourceApplyFailureEventArgs Create(
      string name,
      string resourcePackPath,
      string errorMessage)
    {
      ResourceApplyFailureEventArgs failureEventArgs = ReferencePool.Acquire<ResourceApplyFailureEventArgs>();
      failureEventArgs.Name = name;
      failureEventArgs.ResourcePackPath = resourcePackPath;
      failureEventArgs.ErrorMessage = errorMessage;
      return failureEventArgs;
    }

    /// <summary>清理资源应用失败事件。</summary>
    public override void Clear()
    {
      this.Name = (string) null;
      this.ResourcePackPath = (string) null;
      this.ErrorMessage = (string) null;
    }
  }
}
