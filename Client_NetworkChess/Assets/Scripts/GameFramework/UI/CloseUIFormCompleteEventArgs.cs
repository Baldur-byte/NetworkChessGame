// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.CloseUIFormCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.UI
{
  /// <summary>关闭界面完成事件。</summary>
  public sealed class CloseUIFormCompleteEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化关闭界面完成事件的新实例。</summary>
    public CloseUIFormCompleteEventArgs()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroup = (IUIGroup) null;
      this.UserData = (object) null;
    }

    /// <summary>获取界面序列编号。</summary>
    public int SerialId { get; private set; }

    /// <summary>获取界面资源名称。</summary>
    public string UIFormAssetName { get; private set; }

    /// <summary>获取界面所属的界面组。</summary>
    public IUIGroup UIGroup { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建关闭界面完成事件。</summary>
    /// <param name="serialId">界面序列编号。</param>
    /// <param name="uiFormAssetName">界面资源名称。</param>
    /// <param name="uiGroup">界面所属的界面组。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的关闭界面完成事件。</returns>
    public static CloseUIFormCompleteEventArgs Create(
      int serialId,
      string uiFormAssetName,
      IUIGroup uiGroup,
      object userData)
    {
      CloseUIFormCompleteEventArgs completeEventArgs = ReferencePool.Acquire<CloseUIFormCompleteEventArgs>();
      completeEventArgs.SerialId = serialId;
      completeEventArgs.UIFormAssetName = uiFormAssetName;
      completeEventArgs.UIGroup = uiGroup;
      completeEventArgs.UserData = userData;
      return completeEventArgs;
    }

    /// <summary>清理关闭界面完成事件。</summary>
    public override void Clear()
    {
      this.SerialId = 0;
      this.UIFormAssetName = (string) null;
      this.UIGroup = (IUIGroup) null;
      this.UserData = (object) null;
    }
  }
}
