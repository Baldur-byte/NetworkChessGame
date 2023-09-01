// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.OpenUIFormUpdateEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.UI
{
  /// <summary>打开界面更新事件。</summary>
  public sealed class OpenUIFormUpdateEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化打开界面更新事件的新实例。</summary>
    public OpenUIFormUpdateEventArgs()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroupName = (string) null;
      this.PauseCoveredUIForm = false;
      this.Progress = 0.0f;
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

    /// <summary>获取打开界面进度。</summary>
    public float Progress { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建打开界面更新事件。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroupName">界面组名称。</param>
    /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
    /// <param name="progress">打开界面进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的打开界面更新事件。</returns>
    public static OpenUIFormUpdateEventArgs Create(
      int serialId,
      string uiFormAssetName,
      string uiGroupName,
      bool pauseCoveredUIForm,
      float progress,
      object userData)
    {
      OpenUIFormUpdateEventArgs formUpdateEventArgs = ReferencePool.Acquire<OpenUIFormUpdateEventArgs>();
      formUpdateEventArgs.SerialId = serialId;
      formUpdateEventArgs.UIFormAssetName = uiFormAssetName;
      formUpdateEventArgs.UIGroupName = uiGroupName;
      formUpdateEventArgs.PauseCoveredUIForm = pauseCoveredUIForm;
      formUpdateEventArgs.Progress = progress;
      formUpdateEventArgs.UserData = userData;
      return formUpdateEventArgs;
    }

    /// <summary>清理打开界面更新事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroupName = (string) null;
      this.PauseCoveredUIForm = false;
      this.Progress = 0.0f;
      this.UserData = (object) null;
    }
  }
}
