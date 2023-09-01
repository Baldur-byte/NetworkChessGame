// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceAgentHelperReadFileCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源代理辅助器异步将资源文件转换为加载对象完成事件。</summary>
  public sealed class LoadResourceAgentHelperReadFileCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载资源代理辅助器异步将资源文件转换为加载对象完成事件的新实例。</summary>
    public LoadResourceAgentHelperReadFileCompleteEventArgs() => this.Resource = (object) null;

    /// <summary>获取加载对象。</summary>
    public object Resource { get; private set; }

    /// <summary>创建加载资源代理辅助器异步将资源文件转换为加载对象完成事件。</summary>
    /// <param name="resource">资源对象。</param>
    /// <returns>创建的加载资源代理辅助器异步将资源文件转换为加载对象完成事件。</returns>
    public static LoadResourceAgentHelperReadFileCompleteEventArgs Create(object resource)
    {
      LoadResourceAgentHelperReadFileCompleteEventArgs completeEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperReadFileCompleteEventArgs>();
      completeEventArgs.Resource = resource;
      return completeEventArgs;
    }

    /// <summary>清理加载资源代理辅助器异步将资源文件转换为加载对象完成事件。</summary>
    public override void Clear() => this.Resource = (object) null;
  }
}
