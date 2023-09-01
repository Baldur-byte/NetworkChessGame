// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkLinkedListRange`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameFramework
{
  /// <summary>游戏框架链表范围。</summary>
  /// <typeparam name="T">指定链表范围的元素类型。</typeparam>
  [StructLayout(LayoutKind.Auto)]
  public struct GameFrameworkLinkedListRange<T> : IEnumerable<T>, IEnumerable
  {
    private readonly LinkedListNode<T> m_First;
    private readonly LinkedListNode<T> m_Terminal;

    /// <summary>初始化游戏框架链表范围的新实例。</summary>
    /// <param name="first">链表范围的开始结点。</param>
    /// <param name="terminal">链表范围的终结标记结点。</param>
    public GameFrameworkLinkedListRange(LinkedListNode<T> first, LinkedListNode<T> terminal)
    {
      this.m_First = first != null && terminal != null && first != terminal ? first : throw new GameFrameworkException("Range is invalid.");
      this.m_Terminal = terminal;
    }

    /// <summary>获取链表范围是否有效。</summary>
    public bool IsValid => this.m_First != null && this.m_Terminal != null && this.m_First != this.m_Terminal;

    /// <summary>获取链表范围的开始结点。</summary>
    public LinkedListNode<T> First => this.m_First;

    /// <summary>获取链表范围的终结标记结点。</summary>
    public LinkedListNode<T> Terminal => this.m_Terminal;

    /// <summary>获取链表范围的结点数量。</summary>
    public int Count
    {
      get
      {
        if (!this.IsValid)
          return 0;
        int count = 0;
        for (LinkedListNode<T> linkedListNode = this.m_First; linkedListNode != null && linkedListNode != this.m_Terminal; linkedListNode = linkedListNode.Next)
          ++count;
        return count;
      }
    }

    /// <summary>检查是否包含指定值。</summary>
    /// <param name="value">要检查的值。</param>
    /// <returns>是否包含指定值。</returns>
    public bool Contains(T value)
    {
      for (LinkedListNode<T> linkedListNode = this.m_First; linkedListNode != null && linkedListNode != this.m_Terminal; linkedListNode = linkedListNode.Next)
      {
        if (linkedListNode.Value.Equals((object) value))
          return true;
      }
      return false;
    }

    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>循环访问集合的枚举数。</returns>
    public GameFrameworkLinkedListRange<T>.Enumerator GetEnumerator() => new GameFrameworkLinkedListRange<T>.Enumerator(this);

    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>循环访问集合的枚举数。</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>循环访问集合的枚举数。</returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>循环访问集合的枚举数。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private readonly GameFrameworkLinkedListRange<T> m_GameFrameworkLinkedListRange;
      private LinkedListNode<T> m_Current;
      private T m_CurrentValue;

      internal Enumerator(GameFrameworkLinkedListRange<T> range)
      {
        this.m_GameFrameworkLinkedListRange = range.IsValid ? range : throw new GameFrameworkException("Range is invalid.");
        this.m_Current = this.m_GameFrameworkLinkedListRange.m_First;
        this.m_CurrentValue = default (T);
      }

      /// <summary>获取当前结点。</summary>
      public T Current => this.m_CurrentValue;

      /// <summary>获取当前的枚举数。</summary>
      object IEnumerator.Current => (object) this.m_CurrentValue;

      /// <summary>清理枚举数。</summary>
      public void Dispose()
      {
      }

      /// <summary>获取下一个结点。</summary>
      /// <returns>返回下一个结点。</returns>
      public bool MoveNext()
      {
        if (this.m_Current == null || this.m_Current == this.m_GameFrameworkLinkedListRange.m_Terminal)
          return false;
        this.m_CurrentValue = this.m_Current.Value;
        this.m_Current = this.m_Current.Next;
        return true;
      }

      /// <summary>重置枚举数。</summary>
      void IEnumerator.Reset()
      {
        this.m_Current = this.m_GameFrameworkLinkedListRange.m_First;
        this.m_CurrentValue = default (T);
      }
    }
  }
}
