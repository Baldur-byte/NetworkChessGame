// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkFunc`2
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>封装一个方法，该方法具有一个参数，并返回 TResult 参数所指定的类型的值。</summary>
  /// <typeparam name="T">此委托封装的方法的参数类型。</typeparam>
  /// <typeparam name="TResult">此委托封装的方法的返回值类型。</typeparam>
  /// <param name="arg">此委托封装的方法的参数。</param>
  /// <returns>此委托封装的方法的返回值。</returns>
  public delegate TResult GameFrameworkFunc<in T, out TResult>(T arg);
}
