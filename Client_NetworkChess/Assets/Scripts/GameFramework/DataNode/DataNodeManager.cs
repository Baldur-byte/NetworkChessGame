// Decompiled with JetBrains decompiler
// Type: GameFramework.DataNode.DataNodeManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.DataNode
{
  /// <summary>数据结点管理器。</summary>
  internal sealed class DataNodeManager : GameFrameworkModule, IDataNodeManager
  {
    private static readonly string[] EmptyStringArray = new string[0];
    private static readonly string[] PathSplitSeparator = new string[3]
    {
      ".",
      "/",
      "\\"
    };
    private const string RootName = "<Root>";
    private DataNodeManager.DataNode m_Root;

    /// <summary>初始化数据结点管理器的新实例。</summary>
    public DataNodeManager() => this.m_Root = DataNodeManager.DataNode.Create("<Root>", (DataNodeManager.DataNode) null);

    /// <summary>获取根数据结点。</summary>
    public IDataNode Root => (IDataNode) this.m_Root;

    /// <summary>数据结点管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理数据结点管理器。</summary>
    internal override void Shutdown()
    {
      ReferencePool.Release((IReference) this.m_Root);
      this.m_Root = (DataNodeManager.DataNode) null;
    }

    /// <summary>根据类型获取数据结点的数据。</summary>
    /// <typeparam name="T">要获取的数据类型。</typeparam>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <returns>指定类型的数据。</returns>
    public T GetData<T>(string path) where T : Variable => this.GetData<T>(path, (IDataNode) null);

    /// <summary>获取数据结点的数据。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <returns>数据结点的数据。</returns>
    public Variable GetData(string path) => this.GetData(path, (IDataNode) null);

    /// <summary>根据类型获取数据结点的数据。</summary>
    /// <typeparam name="T">要获取的数据类型。</typeparam>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="node">查找起始结点。</param>
    /// <returns>指定类型的数据。</returns>
    public T GetData<T>(string path, IDataNode node) where T : Variable => (this.GetNode(path, node) ?? throw new GameFrameworkException(Utility.Text.Format<string, string>("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty))).GetData<T>();

    /// <summary>获取数据结点的数据。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="node">查找起始结点。</param>
    /// <returns>数据结点的数据。</returns>
    public Variable GetData(string path, IDataNode node) => (this.GetNode(path, node) ?? throw new GameFrameworkException(Utility.Text.Format<string, string>("Data node is not exist, path '{0}', node '{1}'.", path, node != null ? node.FullName : string.Empty))).GetData();

    /// <summary>设置数据结点的数据。</summary>
    /// <typeparam name="T">要设置的数据类型。</typeparam>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="data">要设置的数据。</param>
    public void SetData<T>(string path, T data) where T : Variable => this.SetData<T>(path, data, (IDataNode) null);

    /// <summary>设置数据结点的数据。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="data">要设置的数据。</param>
    public void SetData(string path, Variable data) => this.SetData(path, data, (IDataNode) null);

    /// <summary>设置数据结点的数据。</summary>
    /// <typeparam name="T">要设置的数据类型。</typeparam>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="data">要设置的数据。</param>
    /// <param name="node">查找起始结点。</param>
    public void SetData<T>(string path, T data, IDataNode node) where T : Variable => this.GetOrAddNode(path, node).SetData<T>(data);

    /// <summary>设置数据结点的数据。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="data">要设置的数据。</param>
    /// <param name="node">查找起始结点。</param>
    public void SetData(string path, Variable data, IDataNode node) => this.GetOrAddNode(path, node).SetData(data);

    /// <summary>获取数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
    public IDataNode GetNode(string path) => this.GetNode(path, (IDataNode) null);

    /// <summary>获取数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="node">查找起始结点。</param>
    /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
    public IDataNode GetNode(string path, IDataNode node)
    {
      IDataNode node1 = node ?? (IDataNode) this.m_Root;
      foreach (string name in DataNodeManager.GetSplitedPath(path))
      {
        node1 = node1.GetChild(name);
        if (node1 == null)
          return (IDataNode) null;
      }
      return node1;
    }

    /// <summary>获取或增加数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <returns>指定位置的数据结点，如果没有找到，则创建相应的数据结点。</returns>
    public IDataNode GetOrAddNode(string path) => this.GetOrAddNode(path, (IDataNode) null);

    /// <summary>获取或增加数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="node">查找起始结点。</param>
    /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
    public IDataNode GetOrAddNode(string path, IDataNode node)
    {
      IDataNode orAddNode = node ?? (IDataNode) this.m_Root;
      foreach (string name in DataNodeManager.GetSplitedPath(path))
        orAddNode = orAddNode.GetOrAddChild(name);
      return orAddNode;
    }

    /// <summary>移除数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    public void RemoveNode(string path) => this.RemoveNode(path, (IDataNode) null);

    /// <summary>移除数据结点。</summary>
    /// <param name="path">相对于 node 的查找路径。</param>
    /// <param name="node">查找起始结点。</param>
    public void RemoveNode(string path, IDataNode node)
    {
      IDataNode dataNode1 = node ?? (IDataNode) this.m_Root;
      IDataNode dataNode2 = dataNode1.Parent;
      foreach (string name in DataNodeManager.GetSplitedPath(path))
      {
        dataNode2 = dataNode1;
        dataNode1 = dataNode1.GetChild(name);
        if (dataNode1 == null)
          return;
      }
      dataNode2?.RemoveChild(dataNode1.Name);
    }

    /// <summary>移除所有数据结点。</summary>
    public void Clear() => this.m_Root.Clear();

    /// <summary>数据结点路径切分工具函数。</summary>
    /// <param name="path">要切分的数据结点路径。</param>
    /// <returns>切分后的字符串数组。</returns>
    private static string[] GetSplitedPath(string path) => string.IsNullOrEmpty(path) ? DataNodeManager.EmptyStringArray : path.Split(DataNodeManager.PathSplitSeparator, StringSplitOptions.RemoveEmptyEntries);

    /// <summary>数据结点。</summary>
    private sealed class DataNode : IDataNode, IReference
    {
      private static readonly DataNodeManager.DataNode[] EmptyDataNodeArray = new DataNodeManager.DataNode[0];
      private string m_Name;
      private Variable m_Data;
      private DataNodeManager.DataNode m_Parent;
      private List<DataNodeManager.DataNode> m_Childs;

      public DataNode()
      {
        this.m_Name = (string) null;
        this.m_Data = (Variable) null;
        this.m_Parent = (DataNodeManager.DataNode) null;
        this.m_Childs = (List<DataNodeManager.DataNode>) null;
      }

      /// <summary>创建数据结点。</summary>
      /// <param name="name">数据结点名称。</param>
      /// <param name="parent">父数据结点。</param>
      /// <returns>创建的数据结点。</returns>
      public static DataNodeManager.DataNode Create(string name, DataNodeManager.DataNode parent)
      {
        if (!DataNodeManager.DataNode.IsValidName(name))
          throw new GameFrameworkException("Name of data node is invalid.");
        DataNodeManager.DataNode dataNode = ReferencePool.Acquire<DataNodeManager.DataNode>();
        dataNode.m_Name = name;
        dataNode.m_Parent = parent;
        return dataNode;
      }

      /// <summary>获取数据结点的名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取数据结点的完整名称。</summary>
      public string FullName => this.m_Parent != null ? Utility.Text.Format<string, string, string>("{0}{1}{2}", this.m_Parent.FullName, DataNodeManager.PathSplitSeparator[0], this.m_Name) : this.m_Name;

      /// <summary>获取父数据结点。</summary>
      public IDataNode Parent => (IDataNode) this.m_Parent;

      /// <summary>获取子数据结点的数量。</summary>
      public int ChildCount => this.m_Childs == null ? 0 : this.m_Childs.Count;

      /// <summary>根据类型获取数据结点的数据。</summary>
      /// <typeparam name="T">要获取的数据类型。</typeparam>
      /// <returns>指定类型的数据。</returns>
      public T GetData<T>() where T : Variable => (T) this.m_Data;

      /// <summary>获取数据结点的数据。</summary>
      /// <returns>数据结点数据。</returns>
      public Variable GetData() => this.m_Data;

      /// <summary>设置数据结点的数据。</summary>
      /// <typeparam name="T">要设置的数据类型。</typeparam>
      /// <param name="data">要设置的数据。</param>
      public void SetData<T>(T data) where T : Variable => this.SetData((Variable) data);

      /// <summary>设置数据结点的数据。</summary>
      /// <param name="data">要设置的数据。</param>
      public void SetData(Variable data)
      {
        if (this.m_Data != null)
          ReferencePool.Release((IReference) this.m_Data);
        this.m_Data = data;
      }

      /// <summary>根据索引检查是否存在子数据结点。</summary>
      /// <param name="index">子数据结点的索引。</param>
      /// <returns>是否存在子数据结点。</returns>
      public bool HasChild(int index) => index >= 0 && index < this.ChildCount;

      /// <summary>根据名称检查是否存在子数据结点。</summary>
      /// <param name="name">子数据结点名称。</param>
      /// <returns>是否存在子数据结点。</returns>
      public bool HasChild(string name)
      {
        if (!DataNodeManager.DataNode.IsValidName(name))
          throw new GameFrameworkException("Name is invalid.");
        if (this.m_Childs == null)
          return false;
        foreach (DataNodeManager.DataNode child in this.m_Childs)
        {
          if (child.Name == name)
            return true;
        }
        return false;
      }

      /// <summary>根据索引获取子数据结点。</summary>
      /// <param name="index">子数据结点的索引。</param>
      /// <returns>指定索引的子数据结点，如果索引越界，则返回空。</returns>
      public IDataNode GetChild(int index) => index < 0 || index >= this.ChildCount ? (IDataNode) null : (IDataNode) this.m_Childs[index];

      /// <summary>根据名称获取子数据结点。</summary>
      /// <param name="name">子数据结点名称。</param>
      /// <returns>指定名称的子数据结点，如果没有找到，则返回空。</returns>
      public IDataNode GetChild(string name)
      {
        if (!DataNodeManager.DataNode.IsValidName(name))
          throw new GameFrameworkException("Name is invalid.");
        if (this.m_Childs == null)
          return (IDataNode) null;
        foreach (DataNodeManager.DataNode child in this.m_Childs)
        {
          if (child.Name == name)
            return (IDataNode) child;
        }
        return (IDataNode) null;
      }

      /// <summary>根据名称获取或增加子数据结点。</summary>
      /// <param name="name">子数据结点名称。</param>
      /// <returns>指定名称的子数据结点，如果对应名称的子数据结点已存在，则返回已存在的子数据结点，否则增加子数据结点。</returns>
      public IDataNode GetOrAddChild(string name)
      {
        DataNodeManager.DataNode child = (DataNodeManager.DataNode) this.GetChild(name);
        if (child != null)
          return (IDataNode) child;
        DataNodeManager.DataNode orAddChild = DataNodeManager.DataNode.Create(name, this);
        if (this.m_Childs == null)
          this.m_Childs = new List<DataNodeManager.DataNode>();
        this.m_Childs.Add(orAddChild);
        return (IDataNode) orAddChild;
      }

      /// <summary>获取所有子数据结点。</summary>
      /// <returns>所有子数据结点。</returns>
      public IDataNode[] GetAllChild() => this.m_Childs == null ? (IDataNode[]) DataNodeManager.DataNode.EmptyDataNodeArray : (IDataNode[]) this.m_Childs.ToArray();

      /// <summary>获取所有子数据结点。</summary>
      /// <param name="results">所有子数据结点。</param>
      public void GetAllChild(List<IDataNode> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        if (this.m_Childs == null)
          return;
        foreach (DataNodeManager.DataNode child in this.m_Childs)
          results.Add((IDataNode) child);
      }

      /// <summary>根据索引移除子数据结点。</summary>
      /// <param name="index">子数据结点的索引位置。</param>
      public void RemoveChild(int index)
      {
        DataNodeManager.DataNode child = (DataNodeManager.DataNode) this.GetChild(index);
        if (child == null)
          return;
        this.m_Childs.Remove(child);
        ReferencePool.Release((IReference) child);
      }

      /// <summary>根据名称移除子数据结点。</summary>
      /// <param name="name">子数据结点名称。</param>
      public void RemoveChild(string name)
      {
        DataNodeManager.DataNode child = (DataNodeManager.DataNode) this.GetChild(name);
        if (child == null)
          return;
        this.m_Childs.Remove(child);
        ReferencePool.Release((IReference) child);
      }

      public void Clear()
      {
        if (this.m_Data != null)
        {
          ReferencePool.Release((IReference) this.m_Data);
          this.m_Data = (Variable) null;
        }
        if (this.m_Childs == null)
          return;
        foreach (IReference child in this.m_Childs)
          ReferencePool.Release(child);
        this.m_Childs.Clear();
      }

      /// <summary>获取数据结点字符串。</summary>
      /// <returns>数据结点字符串。</returns>
      public override string ToString() => Utility.Text.Format<string, string>("{0}: {1}", this.FullName, this.ToDataString());

      /// <summary>获取数据字符串。</summary>
      /// <returns>数据字符串。</returns>
      public string ToDataString() => this.m_Data == null ? "<Null>" : Utility.Text.Format<string, Variable>("[{0}] {1}", this.m_Data.Type.Name, this.m_Data);

      /// <summary>检测数据结点名称是否合法。</summary>
      /// <param name="name">要检测的数据结点名称。</param>
      /// <returns>是否是合法的数据结点名称。</returns>
      private static bool IsValidName(string name)
      {
        if (string.IsNullOrEmpty(name))
          return false;
        foreach (string str in DataNodeManager.PathSplitSeparator)
        {
          if (name.Contains(str))
            return false;
        }
        return true;
      }

      void IReference.Clear()
      {
        this.m_Name = (string) null;
        this.m_Parent = (DataNodeManager.DataNode) null;
        this.Clear();
      }
    }
  }
}
