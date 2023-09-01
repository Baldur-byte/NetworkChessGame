// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceAgentHelperUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源代理辅助器更新事件。</summary>
  public sealed class LoadResourceAgentHelperUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化加载资源代理辅助器更新事件的新实例。</summary>
    public LoadResourceAgentHelperUpdateEventArgs()
    {
      this.Type = LoadResourceProgress.Unknown;
      this.Progress = 0.0f;
    }

    /// <summary>获取进度类型。</summary>
    public LoadResourceProgress Type { get; private set; }

    /// <summary>获取进度。</summary>
    public float Progress { get; private set; }

    /// <summary>创建加载资源代理辅助器更新事件。</summary>
    /// <param name="type">进度类型。</param>
    /// <param name="progress">进度。</param>
    /// <returns>创建的加载资源代理辅助器更新事件。</returns>
    public static LoadResourceAgentHelperUpdateEventArgs Create(
      LoadResourceProgress type,
      float progress)
    {
      LoadResourceAgentHelperUpdateEventArgs helperUpdateEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperUpdateEventArgs>();
      helperUpdateEventArgs.Type = type;
      helperUpdateEventArgs.Progress = progress;
      return helperUpdateEventArgs;
    }

    /// <summary>清理加载资源代理辅助器更新事件。</summary>
    public override void Clear()
    {
      this.Type = LoadResourceProgress.Unknown;
      this.Progress = 0.0f;
    }
  }
}
