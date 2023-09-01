// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceVerifySuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源校验成功事件。</summary>
  public sealed class ResourceVerifySuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源校验成功事件的新实例。</summary>
    public ResourceVerifySuccessEventArgs()
    {
      this.Name = (string) null;
      this.Length = 0;
    }

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>获取资源大小。</summary>
    public int Length { get; private set; }

    /// <summary>创建资源校验成功事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <param name="length">资源大小。</param>
    /// <returns>创建的资源校验成功事件。</returns>
    public static ResourceVerifySuccessEventArgs Create(string name, int length)
    {
      ResourceVerifySuccessEventArgs successEventArgs = ReferencePool.Acquire<ResourceVerifySuccessEventArgs>();
      successEventArgs.Name = name;
      successEventArgs.Length = length;
      return successEventArgs;
    }

    /// <summary>清理资源校验成功事件。</summary>
    public override void Clear()
    {
      this.Name = (string) null;
      this.Length = 0;
    }
  }
}
