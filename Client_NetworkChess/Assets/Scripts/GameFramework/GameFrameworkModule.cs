// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkModule
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>游戏框架模块抽象类。</summary>
  internal abstract class GameFrameworkModule
  {
    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal virtual int Priority => 0;

    /// <summary>游戏框架模块轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal abstract void Update(float elapseSeconds, float realElapseSeconds);

    /// <summary>关闭并清理游戏框架模块。</summary>
    internal abstract void Shutdown();
  }
}
