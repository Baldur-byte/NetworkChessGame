// Decompiled with JetBrains decompiler
// Type: GameFramework.UI.IUIFormHelper
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.UI
{
  /// <summary>界面辅助器接口。</summary>
  public interface IUIFormHelper
  {
    /// <summary>实例化界面。</summary>
    /// <param name="uiFormAsset">要实例化的界面资源。</param>
    /// <returns>实例化后的界面。</returns>
    object InstantiateUIForm(object uiFormAsset);

    /// <summary>创建界面。</summary>
    /// <param name="uiFormInstance">界面实例。</param>
    /// <param name="uiGroup">界面所属的界面组。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>界面。</returns>
    IUIForm CreateUIForm(object uiFormInstance, IUIGroup uiGroup, object userData);

    /// <summary>释放界面。</summary>
    /// <param name="uiFormAsset">要释放的界面资源。</param>
    /// <param name="uiFormInstance">要释放的界面实例。</param>
    void ReleaseUIForm(object uiFormAsset, object uiFormInstance);
  }
}
