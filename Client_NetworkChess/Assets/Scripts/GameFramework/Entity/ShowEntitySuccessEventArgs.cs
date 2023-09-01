// Decompiled with JetBrains decompiler
// Type: GameFramework.Entity.ShowEntitySuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Entity
{
  /// <summary>显示实体成功事件。</summary>
  public sealed class ShowEntitySuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化显示实体成功事件的新实例。</summary>
    public ShowEntitySuccessEventArgs()
    {
      this.Entity = (IEntity) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取显示成功的实体。</summary>
    public IEntity Entity { get; private set; }

    /// <summary>获取加载持续时间。</summary>
    public float Duration { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建显示实体成功事件。</summary>
    /// <param name="entity">加载成功的实体。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的显示实体成功事件。</returns>
    public static ShowEntitySuccessEventArgs Create(
      IEntity entity,
      float duration,
      object userData)
    {
      ShowEntitySuccessEventArgs successEventArgs = ReferencePool.Acquire<ShowEntitySuccessEventArgs>();
      successEventArgs.Entity = entity;
      successEventArgs.Duration = duration;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理显示实体成功事件。</summary>
    public override void Clear()
    {
      this.Entity = (IEntity) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }
  }
}
