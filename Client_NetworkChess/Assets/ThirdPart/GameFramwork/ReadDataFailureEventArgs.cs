// Decompiled with JetBrains decompiler
// Type: GameFramework.ReadDataFailureEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>读取数据失败事件。</summary>
  public sealed class ReadDataFailureEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化读取数据失败事件的新实例。</summary>
    public ReadDataFailureEventArgs()
    {
      this.DataAssetName = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }

    /// <summary>获取内容资源名称。</summary>
    public string DataAssetName { get; private set; }

    /// <summary>获取错误信息。</summary>
    public string ErrorMessage { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建读取数据失败事件。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="errorMessage">错误信息。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的读取数据失败事件。</returns>
    public static ReadDataFailureEventArgs Create(
      string dataAssetName,
      string errorMessage,
      object userData)
    {
      ReadDataFailureEventArgs failureEventArgs = ReferencePool.Acquire<ReadDataFailureEventArgs>();
      failureEventArgs.DataAssetName = dataAssetName;
      failureEventArgs.ErrorMessage = errorMessage;
      failureEventArgs.UserData = userData;
      return failureEventArgs;
    }

    /// <summary>清理读取数据失败事件。</summary>
    public override void Clear()
    {
      this.DataAssetName = (string) null;
      this.ErrorMessage = (string) null;
      this.UserData = (object) null;
    }
  }
}
