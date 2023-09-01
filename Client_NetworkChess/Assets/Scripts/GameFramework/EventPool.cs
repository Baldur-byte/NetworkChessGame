// Decompiled with JetBrains decompiler
// Type: GameFramework.EventPool`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework
{
  /// <summary>事件池。</summary>
  /// <typeparam name="T">事件类型。</typeparam>
  internal sealed class EventPool<T> where T : BaseEventArgs
  {
    private readonly GameFrameworkMultiDictionary<int, EventHandler<T>> m_EventHandlers;
    private readonly Queue<EventPool<T>.Event> m_Events;
    private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> m_CachedNodes;
    private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> m_TempNodes;
    private readonly EventPoolMode m_EventPoolMode;
    private EventHandler<T> m_DefaultHandler;

    /// <summary>初始化事件池的新实例。</summary>
    /// <param name="mode">事件池模式。</param>
    public EventPool(EventPoolMode mode)
    {
      this.m_EventHandlers = new GameFrameworkMultiDictionary<int, EventHandler<T>>();
      this.m_Events = new Queue<EventPool<T>.Event>();
      this.m_CachedNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
      this.m_TempNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
      this.m_EventPoolMode = mode;
      this.m_DefaultHandler = (EventHandler<T>) null;
    }

    /// <summary>获取事件处理函数的数量。</summary>
    public int EventHandlerCount => this.m_EventHandlers.Count;

    /// <summary>获取事件数量。</summary>
    public int EventCount => this.m_Events.Count;

    /// <summary>事件池轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    public void Update(float elapseSeconds, float realElapseSeconds)
    {
      lock (this.m_Events)
      {
        while (this.m_Events.Count > 0)
        {
          EventPool<T>.Event @event = this.m_Events.Dequeue();
          this.HandleEvent(@event.Sender, @event.EventArgs);
          ReferencePool.Release((IReference) @event);
        }
      }
    }

    /// <summary>关闭并清理事件池。</summary>
    public void Shutdown()
    {
      this.Clear();
      this.m_EventHandlers.Clear();
      this.m_CachedNodes.Clear();
      this.m_TempNodes.Clear();
      this.m_DefaultHandler = (EventHandler<T>) null;
    }

    /// <summary>清理事件。</summary>
    public void Clear()
    {
      lock (this.m_Events)
        this.m_Events.Clear();
    }

    /// <summary>获取事件处理函数的数量。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <returns>事件处理函数的数量。</returns>
    public int Count(int id)
    {
      GameFrameworkLinkedListRange<EventHandler<T>> range = new GameFrameworkLinkedListRange<EventHandler<T>>();
      return this.m_EventHandlers.TryGetValue(id, out range) ? range.Count : 0;
    }

    /// <summary>检查是否存在事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要检查的事件处理函数。</param>
    /// <returns>是否存在事件处理函数。</returns>
    public bool Check(int id, EventHandler<T> handler)
    {
      if (handler == null)
        throw new GameFrameworkException("Event handler is invalid.");
      return this.m_EventHandlers.Contains(id, handler);
    }

    /// <summary>订阅事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要订阅的事件处理函数。</param>
    public void Subscribe(int id, EventHandler<T> handler)
    {
      if (handler == null)
        throw new GameFrameworkException("Event handler is invalid.");
      if (!this.m_EventHandlers.Contains(id))
      {
        this.m_EventHandlers.Add(id, handler);
      }
      else
      {
        if ((this.m_EventPoolMode & EventPoolMode.AllowMultiHandler) != EventPoolMode.AllowMultiHandler)
          throw new GameFrameworkException(Utility.Text.Format<int>("Event '{0}' not allow multi handler.", id));
        if ((this.m_EventPoolMode & EventPoolMode.AllowDuplicateHandler) != EventPoolMode.AllowDuplicateHandler && this.Check(id, handler))
          throw new GameFrameworkException(Utility.Text.Format<int>("Event '{0}' not allow duplicate handler.", id));
        this.m_EventHandlers.Add(id, handler);
      }
    }

    /// <summary>取消订阅事件处理函数。</summary>
    /// <param name="id">事件类型编号。</param>
    /// <param name="handler">要取消订阅的事件处理函数。</param>
    public void Unsubscribe(int id, EventHandler<T> handler)
    {
      if (handler == null)
        throw new GameFrameworkException("Event handler is invalid.");
      if (this.m_CachedNodes.Count > 0)
      {
        foreach (KeyValuePair<object, LinkedListNode<EventHandler<T>>> cachedNode in this.m_CachedNodes)
        {
          if (cachedNode.Value != null && cachedNode.Value.Value == handler)
            this.m_TempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
        }
        if (this.m_TempNodes.Count > 0)
        {
          foreach (KeyValuePair<object, LinkedListNode<EventHandler<T>>> tempNode in this.m_TempNodes)
            this.m_CachedNodes[tempNode.Key] = tempNode.Value;
          this.m_TempNodes.Clear();
        }
      }
      if (!this.m_EventHandlers.Remove(id, handler))
        throw new GameFrameworkException(Utility.Text.Format<int>("Event '{0}' not exists specified handler.", id));
    }

    /// <summary>设置默认事件处理函数。</summary>
    /// <param name="handler">要设置的默认事件处理函数。</param>
    public void SetDefaultHandler(EventHandler<T> handler) => this.m_DefaultHandler = handler;

    /// <summary>
    /// 抛出事件，这个操作是线程安全的，即使不在主线程中抛出，也可保证在主线程中回调事件处理函数，但事件会在抛出后的下一帧分发。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    public void Fire(object sender, T e)
    {
      EventPool<T>.Event @event = (object) e != null ? EventPool<T>.Event.Create(sender, e) : throw new GameFrameworkException("Event is invalid.");
      lock (this.m_Events)
        this.m_Events.Enqueue(@event);
    }

    /// <summary>抛出事件立即模式，这个操作不是线程安全的，事件会立刻分发。</summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    public void FireNow(object sender, T e)
    {
      if ((object) e == null)
        throw new GameFrameworkException("Event is invalid.");
      this.HandleEvent(sender, e);
    }

    /// <summary>处理事件结点。</summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">事件参数。</param>
    private void HandleEvent(object sender, T e)
    {
      bool flag = false;
      GameFrameworkLinkedListRange<EventHandler<T>> range = new GameFrameworkLinkedListRange<EventHandler<T>>();
      if (this.m_EventHandlers.TryGetValue(e.Id, out range))
      {
        for (LinkedListNode<EventHandler<T>> linkedListNode = range.First; linkedListNode != null && linkedListNode != range.Terminal; linkedListNode = this.m_CachedNodes[(object) e])
        {
          this.m_CachedNodes[(object) e] = linkedListNode.Next != range.Terminal ? linkedListNode.Next : (LinkedListNode<EventHandler<T>>) null;
          linkedListNode.Value(sender, e);
        }
        this.m_CachedNodes.Remove((object) e);
      }
      else if (this.m_DefaultHandler != null)
        this.m_DefaultHandler(sender, e);
      else if ((this.m_EventPoolMode & EventPoolMode.AllowNoHandler) == EventPoolMode.Default)
        flag = true;
      ReferencePool.Release((IReference) e);
      if (flag)
        throw new GameFrameworkException(Utility.Text.Format<int>("Event '{0}' not allow no handler.", e.Id));
    }

    /// <summary>事件结点。</summary>
    private sealed class Event : IReference
    {
      private object m_Sender;
      private T m_EventArgs;

      public Event()
      {
        this.m_Sender = (object) null;
        this.m_EventArgs = default (T);
      }

      public object Sender => this.m_Sender;

      public T EventArgs => this.m_EventArgs;

      public static EventPool<T>.Event Create(object sender, T e)
      {
        EventPool<T>.Event @event = ReferencePool.Acquire<EventPool<T>.Event>();
        @event.m_Sender = sender;
        @event.m_EventArgs = e;
        return @event;
      }

      public void Clear()
      {
        this.m_Sender = (object) null;
        this.m_EventArgs = default (T);
      }
    }
  }
}
