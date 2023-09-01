// Decompiled with JetBrains decompiler
// Type: GameFramework.ReadDataSuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>读取数据成功事件。</summary>
  public sealed class ReadDataSuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化读取数据成功事件的新实例。</summary>
    public ReadDataSuccessEventArgs()
    {
      this.DataAssetName = (string) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }

    /// <summary>获取内容资源名称。</summary>
    public string DataAssetName { get; private set; }

    /// <summary>获取加载持续时间。</summary>
    public float Duration { get; private set; }

    /// <summary>获取用户自定义数据。</summary>
    public object UserData { get; private set; }

    /// <summary>创建读取数据成功事件。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="duration">加载持续时间。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>创建的读取数据成功事件。</returns>
    public static ReadDataSuccessEventArgs Create(
      string dataAssetName,
      float duration,
      object userData)
    {
      ReadDataSuccessEventArgs successEventArgs = ReferencePool.Acquire<ReadDataSuccessEventArgs>();
      successEventArgs.DataAssetName = dataAssetName;
      successEventArgs.Duration = duration;
      successEventArgs.UserData = userData;
      return successEventArgs;
    }

    /// <summary>清理读取数据成功事件。</summary>
    public override void Clear()
    {
      this.DataAssetName = (string) null;
      this.Duration = 0.0f;
      this.UserData = (object) null;
    }
  }
}
