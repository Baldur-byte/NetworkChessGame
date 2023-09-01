// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.OpenUIFormSuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.UI
{
  /// <summary>打开界面成功事件。</summary>
  public sealed class OpenUIFormSuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化打开界面成功事件的新实例。</summary>
    public OpenUIFormSuccessEventArgs()
    {
      this.UIForm = (IUIForm) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取打开成功的界面。</summary>
    public IUIForm UIForm { get; private set; }

    /// <summary>获取加载持续时间。</summary>
    public float Duration { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建打开界面成功事件。</summary>
    /// <param name="uiForm">加载成功的界面。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的打开界面成功事件。</returns>
    public static OpenUIFormSuccessEventArgs Create(
      IUIForm uiForm,
      float duration,
      object userData)
    {
      OpenUIFormSuccessEventArgs successEventArgs = ReferencePool.Acquire<OpenUIFormSuccessEventArgs>();
      successEventArgs.UIForm = uiForm;
      successEventArgs.Duration = duration;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理打开界面成功事件。</summary>
    public override void Clear()
    {
      this.UIForm = (IUIForm) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }
  }
}
