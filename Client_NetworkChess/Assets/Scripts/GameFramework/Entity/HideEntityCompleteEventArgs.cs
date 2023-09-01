// Decompiled with JetBrains decompiler
// Type: GameFramework.Entity.HideEntityCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Entity
{
  /// <summary>隐藏实体完成事件。</summary>
  public sealed class HideEntityCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化隐藏实体完成事件的新实例。</summary>
    public HideEntityCompleteEventArgs()
    {
      this.EntityId = 0;
      this.EntityAssetName = (string) null;
      this.EntityGroup = (IEntityGroup) null;
      this.UserData = (object) null;
    }

    /// <summary>获取实体编号。</summary>
    public int EntityId { get; private set; }

    /// <summary>获取实体资源名称。</summary>
    public string EntityAssetName { get; private set; }

    /// <summary>获取实体所属的实体组。</summary>
    public IEntityGroup EntityGroup { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建隐藏实体完成事件。</summary>
    /// <param name="entityId">实体编号。</param>
    /// <param name="entityAssetName">实体资源名称。</param>
    /// <param name="entityGroup">实体所属的实体组。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的隐藏实体完成事件。</returns>
    public static HideEntityCompleteEventArgs Create(
      int entityId,
      string entityAssetName,
      IEntityGroup entityGroup,
      object userData)
    {
      HideEntityCompleteEventArgs completeEventArgs = ReferencePool.Acquire<HideEntityCompleteEventArgs>();
      completeEventArgs.EntityId = entityId;
      completeEventArgs.EntityAssetName = entityAssetName;
      completeEventArgs.EntityGroup = entityGroup;
      completeEventArgs.UserData = userData;
      return completeEventArgs;
    }

    /// <summary>清理隐藏实体完成事件。</summary>
    public override void Clear()
    {
      this.EntityId = 0;
      this.EntityAssetName = (string) null;
      this.EntityGroup = (IEntityGroup) null;
      this.UserData = (object) null;
    }
  }
}
