// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkAction`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>封装一个方法，该方法只有一个参数并且不返回值。</summary>
  /// <typeparam name="T">此委托封装的方法的参数类型。</typeparam>
  /// <param name="obj">此委托封装的方法的参数。</param>
  public delegate void GameFrameworkAction<in T>(T obj);
}
