// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.ResourceApplySuccessEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>资源应用成功事件。</summary>
  public sealed class ResourceApplySuccessEventArgs : GameFrameworkEventArgs
  {
    /// <summary>初始化资源应用成功事件的新实例。</summary>
    public ResourceApplySuccessEventArgs()
    {
      this.Name = (string) null;
      this.ApplyPath = (string) null;
      this.ResourcePackPath = (string) null;
      this.Length = 0;
      this.CompressedLength = 0;
    }

    /// <summary>获取资源名称。</summary>
    public string Name { get; private set; }

    /// <summary>获取资源应用后存放路径。</summary>
    public string ApplyPath { get; private set; }

    /// <summary>获取资源包路径。</summary>
    public string ResourcePackPath { get; private set; }

    /// <summary>获取资源大小。</summary>
    public int Length { get; private set; }

    /// <summary>获取压缩后大小。</summary>
    public int CompressedLength { get; private set; }

    /// <summary>创建资源应用成功事件。</summary>
    /// <param name="name">资源名称。</param>
    /// <param name="applyPath">资源应用后存放路径。</param>
    /// <param name="resourcePackPath">资源包路径。</param>
    /// <param name="length">资源大小。</param>
    /// <param name="compressedLength">压缩后大小。</param>
    /// <returns>创建的资源应用成功事件。</returns>
    public static ResourceApplySuccessEventArgs Create(
      string name,
      string applyPath,
      string resourcePackPath,
      int length,
      int compressedLength)
    {
      ResourceApplySuccessEventArgs successEventArgs = ReferencePool.Acquire<ResourceApplySuccessEventArgs>();
      successEventArgs.Name = name;
      successEventArgs.ApplyPath = applyPath;
      successEventArgs.ResourcePackPath = resourcePackPath;
      successEventArgs.Length = length;
      successEventArgs.CompressedLength = compressedLength;
      return successEventArgs;
    }

    /// <summary>清理资源应用成功事件。</summary>
    public override void Clear()
    {
      this.Name = (string) null;
      this.ApplyPath = (string) null;
      this.ResourcePackPath = (string) null;
      this.Length = 0;
      this.CompressedLength = 0;
    }
  }
}
