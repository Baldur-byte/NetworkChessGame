// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkMultiDictionary`2
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
  /// <summary>游戏框架多值字典类。</summary>
  /// <typeparam name="TKey">指定多值字典的主键类型。</typeparam>
  /// <typeparam name="TValue">指定多值字典的值类型。</typeparam>
  public sealed class GameFrameworkMultiDictionary<TKey, TValue> : 
    IEnumerable<KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>>>,
    IEnumerable
  {
    private readonly GameFrameworkLinkedList<TValue> m_LinkedList;
    private readonly Dictionary<TKey, GameFrameworkLinkedListRange<TValue>> m_Dictionary;

    /// <summary>初始化游戏框架多值字典类的新实例。</summary>
    public GameFrameworkMultiDictionary()
    {
      this.m_LinkedList = new GameFrameworkLinkedList<TValue>();
      this.m_Dictionary = new Dictionary<TKey, GameFrameworkLinkedListRange<TValue>>();
    }

    /// <summary>获取多值字典中实际包含的主键数量。</summary>
    public int Count => this.m_Dictionary.Count;

    /// <summary>获取多值字典中指定主键的范围。</summary>
    /// <param name="key">指定的主键。</param>
    /// <returns>指定主键的范围。</returns>
    public GameFrameworkLinkedListRange<TValue> this[TKey key]
    {
      get
      {
        GameFrameworkLinkedListRange<TValue> frameworkLinkedListRange = new GameFrameworkLinkedListRange<TValue>();
        this.m_Dictionary.TryGetValue(key, out frameworkLinkedListRange);
        return frameworkLinkedListRange;
      }
    }

    /// <summary>清理多值字典。</summary>
    public void Clear()
    {
      this.m_Dictionary.Clear();
      this.m_LinkedList.Clear();
    }

    /// <summary>检查多值字典中是否包含指定主键。</summary>
    /// <param name="key">要检查的主键。</param>
    /// <returns>多值字典中是否包含指定主键。</returns>
    public bool Contains(TKey key) => this.m_Dictionary.ContainsKey(key);

    /// <summary>检查多值字典中是否包含指定值。</summary>
    /// <param name="key">要检查的主键。</param>
    /// <param name="value">要检查的值。</param>
    /// <returns>多值字典中是否包含指定值。</returns>
    public bool Contains(TKey key, TValue value)
    {
      GameFrameworkLinkedListRange<TValue> frameworkLinkedListRange = new GameFrameworkLinkedListRange<TValue>();
      return this.m_Dictionary.TryGetValue(key, out frameworkLinkedListRange) && frameworkLinkedListRange.Contains(value);
    }

    /// <summary>尝试获取多值字典中指定主键的范围。</summary>
    /// <param name="key">指定的主键。</param>
    /// <param name="range">指定主键的范围。</param>
    /// <returns>是否获取成功。</returns>
    public bool TryGetValue(TKey key, out GameFrameworkLinkedListRange<TValue> range) => this.m_Dictionary.TryGetValue(key, out range);

    /// <summary>向指定的主键增加指定的值。</summary>
    /// <param name="key">指定的主键。</param>
    /// <param name="value">指定的值。</param>
    public void Add(TKey key, TValue value)
    {
      GameFrameworkLinkedListRange<TValue> frameworkLinkedListRange = new GameFrameworkLinkedListRange<TValue>();
      if (this.m_Dictionary.TryGetValue(key, out frameworkLinkedListRange))
      {
        this.m_LinkedList.AddBefore(frameworkLinkedListRange.Terminal, value);
      }
      else
      {
        LinkedListNode<TValue> first = this.m_LinkedList.AddLast(value);
        LinkedListNode<TValue> terminal = this.m_LinkedList.AddLast(default (TValue));
        this.m_Dictionary.Add(key, new GameFrameworkLinkedListRange<TValue>(first, terminal));
      }
    }

    /// <summary>从指定的主键中移除指定的值。</summary>
    /// <param name="key">指定的主键。</param>
    /// <param name="value">指定的值。</param>
    /// <returns>是否移除成功。</returns>
    public bool Remove(TKey key, TValue value)
    {
      GameFrameworkLinkedListRange<TValue> frameworkLinkedListRange = new GameFrameworkLinkedListRange<TValue>();
      if (this.m_Dictionary.TryGetValue(key, out frameworkLinkedListRange))
      {
        for (LinkedListNode<TValue> node = frameworkLinkedListRange.First; node != null && node != frameworkLinkedListRange.Terminal; node = node.Next)
        {
          if (node.Value.Equals((object) value))
          {
            if (node == frameworkLinkedListRange.First)
            {
              LinkedListNode<TValue> next = node.Next;
              if (next == frameworkLinkedListRange.Terminal)
              {
                this.m_LinkedList.Remove(next);
                this.m_Dictionary.Remove(key);
              }
              else
                this.m_Dictionary[key] = new GameFrameworkLinkedListRange<TValue>(next, frameworkLinkedListRange.Terminal);
            }
            this.m_LinkedList.Remove(node);
            return true;
          }
        }
      }
      return false;
    }

    /// <summary>从指定的主键中移除所有的值。</summary>
    /// <param name="key">指定的主键。</param>
    /// <returns>是否移除成功。</returns>
    public bool RemoveAll(TKey key)
    {
      GameFrameworkLinkedListRange<TValue> frameworkLinkedListRange = new GameFrameworkLinkedListRange<TValue>();
      if (!this.m_Dictionary.TryGetValue(key, out frameworkLinkedListRange))
        return false;
      this.m_Dictionary.Remove(key);
      LinkedListNode<TValue> next;
      for (LinkedListNode<TValue> node = frameworkLinkedListRange.First; node != null; node = next)
      {
        next = node != frameworkLinkedListRange.Terminal ? node.Next : (LinkedListNode<TValue>) null;
        this.m_LinkedList.Remove(node);
      }
      return true;
    }

    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>循环访问集合的枚举数。</returns>
    public GameFrameworkMultiDictionary<TKey, TValue>.Enumerator GetEnumerator() => new GameFrameworkMultiDictionary<TKey, TValue>.Enumerator(this.m_Dictionary);

    IEnumerator<KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>>> IEnumerable<KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>>>.GetEnumerator() => (IEnumerator<KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>>>) this.GetEnumerator();

    /// <summary>返回循环访问集合的枚举数。</summary>
    /// <returns>循环访问集合的枚举数。</returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>循环访问集合的枚举数。</summary>
    [StructLayout(LayoutKind.Auto)]
    public struct Enumerator : 
      IEnumerator<KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>>>,
      IDisposable,
      IEnumerator
    {
      private Dictionary<TKey, GameFrameworkLinkedListRange<TValue>>.Enumerator m_Enumerator;

      internal Enumerator(
        Dictionary<TKey, GameFrameworkLinkedListRange<TValue>> dictionary)
      {
        this.m_Enumerator = dictionary != null ? dictionary.GetEnumerator() : throw new GameFrameworkException("Dictionary is invalid.");
      }

      /// <summary>获取当前结点。</summary>
      public KeyValuePair<TKey, GameFrameworkLinkedListRange<TValue>> Current => this.m_Enumerator.Current;

      /// <summary>获取当前的枚举数。</summary>
      object IEnumerator.Current => (object) this.m_Enumerator.Current;

      /// <summary>清理枚举数。</summary>
      public void Dispose() => this.m_Enumerator.Dispose();

      /// <summary>获取下一个结点。</summary>
      /// <returns>返回下一个结点。</returns>
      public bool MoveNext() => this.m_Enumerator.MoveNext();

      /// <summary>重置枚举数。</summary>
      void IEnumerator.Reset() => ((IEnumerator) this.m_Enumerator).Reset();
    }
  }
}
