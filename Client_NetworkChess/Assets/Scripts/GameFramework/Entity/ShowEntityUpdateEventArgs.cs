// Decompiled with JetBrains decompiler
// Type: GameFramework.Entity.ShowEntityUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Entity
{
  /// <summary>显示实体更新事件。</summary>
  public sealed class ShowEntityUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化显示实体更新事件的新实例。</summary>
    public ShowEntityUpdateEventArgs()
    {
      this.EntityId = 0;
      this.EntityAssetName = (string) null;
      this.EntityGroupName = (string) null;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取实体编号。</summary>
    public int EntityId { get; private set; }

    /// <summary>获取实体资源名称。</summary>
    public string EntityAssetName { get; private set; }

    /// <summary>获取实体组名称。</summary>
    public string EntityGroupName { get; private set; }

    /// <summary>获取显示实体进度。</summary>
    public float Progress { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建显示实体更新事件。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroupName">实体组名称。</param>
    /// <param name="progress">显示实体进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的显示实体更新事件。</returns>
    public static ShowEntityUpdateEventArgs Create(
      int entityId,
      string entityAssetName,
      string entityGroupName,
      float progress,
      object userData)
    {
      ShowEntityUpdateEventArgs entityUpdateEventArgs = ReferencePool.Acquire<ShowEntityUpdateEventArgs>();
      entityUpdateEventArgs.EntityId = entityId;
      entityUpdateEventArgs.EntityAssetName = entityAssetName;
      entityUpdateEventArgs.EntityGroupName = entityGroupName;
      entityUpdateEventArgs.Progress = progress;
      entityUpdateEventArgs.UserData = userData;
      return entityUpdateEventArgs;
    }

    /// <summary>清理显示实体更新事件。</summary>
    public override void Clear()
    {
      this.EntityId = 0;
      this.EntityAssetName = (string) null;
      this.EntityGroupName = (string) null;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }
  }
}
