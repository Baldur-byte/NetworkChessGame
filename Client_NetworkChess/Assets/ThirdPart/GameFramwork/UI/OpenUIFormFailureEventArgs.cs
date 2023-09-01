// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.OpenUIFormFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.UI
{
  /// <summary>打开界面失败事件。</summary>
  public sealed class OpenUIFormFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化打开界面失败事件的新实例。</summary>
    public OpenUIFormFailureEventArgs()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroupName = (string) null;
      this.PauseCoveredUIForm = false;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取界面序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取界面资源名称。</summary>
    public string UIFormAssetName { get; private set; }

    /// <summary>获取界面组名称。</summary>
    public string UIGroupName { get; private set; }

    /// <summary>获取是否暂停被覆盖的界面。</summary>
    public bool PauseCoveredUIForm { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建打开界面失败事件。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的打开界面失败事件。</returns>
    public static OpenUIFormFailureEventArgs Create(
      int serialId,
      string uiFormAssetName,
      string uiGroupName,
      bool pauseCoveredUIForm,
      string errorMessage,
      object userData)
    {
      OpenUIFormFailureEventArgs failureEventArgs = ReferencePool.Acquire<OpenUIFormFailureEventArgs>();
      failureEventArgs.SerialId = serialId;
      failureEventArgs.UIFormAssetName = uiFormAssetName;
      failureEventArgs.UIGroupName = uiGroupName;
      failureEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理打开界面失败事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroupName = (string) null;
      this.PauseCoveredUIForm = false;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
