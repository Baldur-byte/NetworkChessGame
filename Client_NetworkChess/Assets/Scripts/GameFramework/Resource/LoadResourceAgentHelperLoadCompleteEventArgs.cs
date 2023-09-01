// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceAgentHelperLoadCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源代理辅助器异步加载资源完成事件。</summary>
  public sealed class LoadResourceAgentHelperLoadCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载资源代理辅助器异步加载资源完成事件的新实例。</summary>
    public LoadResourceAgentHelperLoadCompleteEventArgs() => this.Asset = (object) null;

    /// <summary>获取加载的资源。</summary>
    public object Asset { get; private set; }

    /// <summary>创建加载资源代理辅助器异步加载资源完成事件。</summary>
    /// <param name="asset">加载的资源。</param>
    /// <returns>创建的加载资源代理辅助器异步加载资源完成事件。</returns>
    public static LoadResourceAgentHelperLoadCompleteEventArgs Create(object asset)
    {
      LoadResourceAgentHelperLoadCompleteEventArgs completeEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperLoadCompleteEventArgs>();
      completeEventArgs.Asset = asset;
      return completeEventArgs;
    }

    /// <summary>清理加载资源代理辅助器异步加载资源完成事件。</summary>
    public override void Clear() => this.Asset = (object) null;
  }
}
