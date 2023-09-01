// Decompiled with JetBrains decompiler
// Type: GameFramework.ObjectPool.ReleaseObjectFilterCallback`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.ObjectPool
{
  /// <summary>释放对象筛选函数。</summary>
  /// <typeparam name="T">对象类型。</typeparam>
  /// <param name="candidateObjects">要筛选的对象集合。</param>
  /// <param name="toReleaseCount">需要释放的对象数量。</param>
  /// <param name="expireTime">对象过期参考时间。</param>
  /// <returns>经筛选需要释放的对象集合。</returns>
  public delegate List<T> ReleaseObjectFilterCallback<T>(
    List<T> candidateObjects,
    int toReleaseCount,
    DateTime expireTime)
    where T : ObjectBase;
}
