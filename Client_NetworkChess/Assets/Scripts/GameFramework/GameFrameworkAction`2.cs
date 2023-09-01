// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkAction`2
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>封装一个方法，该方法具有两个参数并且不返回值。</summary>
  /// <typeparam name="T1">此委托封装的方法的第一个参数的类型。</typeparam>
  /// <typeparam name="T2">此委托封装的方法的第二个参数的类型。</typeparam>
  /// <param name="arg1">此委托封装的方法的第一个参数。</param>
  /// <param name="arg2">此委托封装的方法的第二个参数。</param>
  public delegate void GameFrameworkAction<in T1, in T2>(T1 arg1, T2 arg2);
}
