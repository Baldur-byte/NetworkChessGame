// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.LoadResourceAgentHelperReadBytesCompleteEventArgs
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>加载资源代理辅助器异步读取资源二进制流完成事件。</summary>
  public sealed class LoadResourceAgentHelperReadBytesCompleteEventArgs : GameFrameworkEventArgs
  {
    private byte[] m_Bytes;

    /// <summary>初始化加载资源代理辅助器异步读取资源二进制流完成事件的新实例。</summary>
    public LoadResourceAgentHelperReadBytesCompleteEventArgs() => this.m_Bytes = (byte[]) null;

    /// <summary>创建加载资源代理辅助器异步读取资源二进制流完成事件。</summary>
    /// <param name="bytes">资源的二进制流。</param>
    /// <returns>创建的加载资源代理辅助器异步读取资源二进制流完成事件。</returns>
    public static LoadResourceAgentHelperReadBytesCompleteEventArgs Create(byte[] bytes)
    {
      LoadResourceAgentHelperReadBytesCompleteEventArgs completeEventArgs = ReferencePool.Acquire<LoadResourceAgentHelperReadBytesCompleteEventArgs>();
      completeEventArgs.m_Bytes = bytes;
      return completeEventArgs;
    }

    /// <summary>清理加载资源代理辅助器异步读取资源二进制流完成事件。</summary>
    public override void Clear() => this.m_Bytes = (byte[]) null;

    /// <summary>获取资源的二进制流。</summary>
    /// <returns>资源的二进制流。</returns>
    public byte[] GetBytes() => this.m_Bytes;
  }
}
