// Decompiled with JetBrains decompiler
// Type: GameFramework.Event.EventManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;

namespace GameFramework.Event
{
  /// <summary>事件管理器。</summary>
  internal sealed class EventManager : GameFrameworkModule, IEventManager
  {
    private readonly EventPool<GameEventArgs> m_EventPool;

    /// <summary>初始化事件管理器的新实例。</summary>
    public EventManager() => this.m_EventPool = new EventPool<GameEventArgs>(EventPoolMode.AllowNoHandler | EventPoolMode.AllowMultiHandler);

    /// <summary>获取事件处理函数的数量。</summary>
    public int EventHandlerCount => this.m_EventPool.EventHandlerCount;

    /// <summary>获取事件数量。</summary>
    public int EventCount => this.m_EventPool.EventCount;

    /// <summary>获取游戏框架模块优先级。</summary>
    /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
    internal override int Priority => 7;

    /// <summary>事件管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds) => this.m_EventPool.Update(elapseSeconds, realElapseSeconds);

    /// <summary>关闭并清理事件管理器。</summary>
    internal override void Shutdown() => this.m_EventPool.Shutdown();

    /// <summary>获取事件处理函数的数量。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <returns>事件处理函数的数量。</returns>
    public int Count(int id) => this.m_EventPool.Count(id);

    /// <summary>检查是否存在事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要检查的事件处理函数。</param>
    /// <returns>是否存在事件处理函数。</returns>
    public bool Check(int id, EventHandler<GameEventArgs> handler) => this.m_EventPool.Check(id, handler);

    /// <summary>订阅事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要订阅的事件处理函数。</param>
    public void Subscribe(int id, EventHandler<GameEventArgs> handler) => this.m_EventPool.Subscribe(id, handler);

    /// <summary>取消订阅事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要取消订阅的事件处理函数。</param>
    public void Unsubscribe(int id, EventHandler<GameEventArgs> handler) => this.m_EventPool.Unsubscribe(id, handler);

    /// <summary>设置默认事件处理函数。</summary>
    /// <param name="handler">要设置的默认事件处理函数。</param>
    public void SetDefaultHandler(EventHandler<GameEventArgs> handler) => this.m_EventPool.SetDefaultHandler(handler);

    /// <summary>
    /// 抛出事件，这个操作是线程安全的，即使不在主线程中抛出，也可保证在主线程中回调事件处理函数，但事件会在抛出后的下一帧分发。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    public void Fire(object sender, GameEventArgs e) => this.m_EventPool.Fire(sender, e);

    /// <summary>抛出事件立即模式，这个操作不是线程安全的，事件会立刻分发。</summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    public void FireNow(object sender, GameEventArgs e) => this.m_EventPool.FireNow(sender, e);
  }
}
