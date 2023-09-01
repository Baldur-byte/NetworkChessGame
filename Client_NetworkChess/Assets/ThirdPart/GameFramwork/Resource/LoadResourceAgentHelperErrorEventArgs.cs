// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceAgentHelperErrorEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源代理辅助器错误事件。</summary>
  public sealed class LoadResourceAgentHelperErrorEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载资源代理辅助器错误事件的新实例。</summary>
    public LoadResourceAgentHelperErrorEventArgs()
    {
      this.Status = LoadResourceStatus.Success;
      this.ErrorMessage = (string) null;
    }

    /// <summary>获取加载资源状态。</summary>
    public LoadResourceStatus Status { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>创建加载资源代理辅助器错误事件。</summary>
    /// <param name="status">加载资源状态。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <returns>创建的加载资源代理辅助器错误事件。</returns>
    public static LoadResourceAgentHelperErrorEventArgs Create(
      LoadResourceStatus status,
      string errorMessage)
    {
      LoadResourceAgentHelperErrorEventArgs helperErrorEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperErrorEventArgs>();
      helperErrorEventArgs.Status = status;
      helperErrorEventArgs.ErrorMessage = errorMessage;
      return helperErrorEventArgs;
    }

    /// <summary>清理加载资源代理辅助器错误事件。</summary>
    public override void Clear()
    {
      this.Status = LoadResourceStatus.Success;
      this.ErrorMessage = (string) null;
    }
  }
}
