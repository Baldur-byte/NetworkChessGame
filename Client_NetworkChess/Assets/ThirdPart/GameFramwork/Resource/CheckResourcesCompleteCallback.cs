// Decompiled with JetBrains decompiler
// Type: GameFramework.Resource.CheckResourcesCompleteCallback
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Resource
{
  /// <summary>使用可更新模式并检查资源完成时的回调函数。</summary>
  /// <param name="movedCount">已移动的资源数量。</param>
  /// <param name="removedCount">已移除的资源数量。</param>
  /// <param name="updateCount">可更新的资源数量。</param>
  /// <param name="updateTotalLength">可更新的资源总大小。</param>
  /// <param name="updateTotalCompressedLength">可更新的压缩后总大小。</param>
  public delegate void CheckResourcesCompleteCallback(
    int movedCount,
    int removedCount,
    int updateCount,
    long updateTotalLength,
    long updateTotalCompressedLength);
}
