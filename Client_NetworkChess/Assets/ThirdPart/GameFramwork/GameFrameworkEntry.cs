// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkEntry
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework
{
  /// <summary>游戏框架入口。</summary>
  public static class GameFrameworkEntry
  {
    private static readonly GameFrameworkLinkedList<GameFrameworkModule> s_GameFrameworkModules = new GameFrameworkLinkedList<GameFrameworkModule>();

    /// <summary>所有游戏框架模块轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    public static void Update(float elapseSeconds, float realElapseSeconds)
    {
      foreach (GameFrameworkModule gameFrameworkModule in GameFrameworkEntry.s_GameFrameworkModules)
        gameFrameworkModule.Update(elapseSeconds, realElapseSeconds);
    }

    /// <summary>关闭并清理所有游戏框架模块。</summary>
    public static void Shutdown()
    {
      for (LinkedListNode<GameFrameworkModule> linkedListNode = GameFrameworkEntry.s_GameFrameworkModules.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
        linkedListNode.Value.Shutdown();
      GameFrameworkEntry.s_GameFrameworkModules.Clear();
      ReferencePool.ClearAll();
      Utility.Marshal.FreeCachedHGlobal();
      GameFrameworkLog.SetLogHelper((GameFrameworkLog.ILogHelper) null);
    }

    /// <summary>获取游戏框架模块。</summary>
    /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
    /// <returns>要获取的游戏框架模块。</returns>
    /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
    public static T GetModule<T>() where T : class
    {
      Type type = typeof (T);
      if (!type.IsInterface)
        throw new GameFrameworkException(Utility.Text.Format<string>("You must get module by interface, but '{0}' is not.", type.FullName));
      if (!type.FullName.StartsWith("GameFramework.", StringComparison.Ordinal))
        throw new GameFrameworkException(Utility.Text.Format<string>("You must get a Game Framework module, but '{0}' is not.", type.FullName));
      string typeName = Utility.Text.Format<string, string>("{0}.{1}", type.Namespace, type.Name.Substring(1));
      return GameFrameworkEntry.GetModule(Type.GetType(typeName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Can not find Game Framework module type '{0}'.", typeName))) as T;
    }

    /// <summary>获取游戏框架模块。</summary>
    /// <param name="moduleType">要获取的游戏框架模块类型。</param>
    /// <returns>要获取的游戏框架模块。</returns>
    /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
    private static GameFrameworkModule GetModule(Type moduleType)
    {
      foreach (GameFrameworkModule gameFrameworkModule in GameFrameworkEntry.s_GameFrameworkModules)
      {
        if (gameFrameworkModule.GetType() == moduleType)
          return gameFrameworkModule;
      }
      return GameFrameworkEntry.CreateModule(moduleType);
    }

    /// <summary>创建游戏框架模块。</summary>
    /// <param name="moduleType">要创建的游戏框架模块类型。</param>
    /// <returns>要创建的游戏框架模块。</returns>
    private static GameFrameworkModule CreateModule(Type moduleType)
    {
      GameFrameworkModule instance = (GameFrameworkModule) Activator.CreateInstance(moduleType);
      if (instance == null)
        throw new GameFrameworkException(Utility.Text.Format<string>("Can not create module '{0}'.", moduleType.FullName));
      LinkedListNode<GameFrameworkModule> node = GameFrameworkEntry.s_GameFrameworkModules.First;
      while (node != null && instance.Priority <= node.Value.Priority)
        node = node.Next;
      if (node != null)
        GameFrameworkEntry.s_GameFrameworkModules.AddBefore(node, instance);
      else
        GameFrameworkEntry.s_GameFrameworkModules.AddLast(instance);
      return instance;
    }
  }
}
