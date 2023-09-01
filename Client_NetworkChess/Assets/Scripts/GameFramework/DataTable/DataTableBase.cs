// Decompiled with JetBrains decompiler
// Type: GameFramework.DataTable.DataTableBase
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;

namespace GameFramework.DataTable
{
  /// <summary>数据表基类。</summary>
  public abstract class DataTableBase : IDataProvider<DataTableBase>
  {
    private readonly string m_Name;
    private readonly DataProvider<DataTableBase> m_DataProvider;

    /// <summary>初始化数据表基类的新实例。</summary>
    public DataTableBase()
      : this((string) null)
    {
    }

    /// <summary>初始化数据表基类的新实例。</summary>
    /// <param name="name">数据表名称。</param>
    public DataTableBase(string name)
    {
      this.m_Name = name ?? string.Empty;
      this.m_DataProvider = new DataProvider<DataTableBase>(this);
    }

    /// <summary>获取数据表名称。</summary>
    public string Name => this.m_Name;

    /// <summary>获取数据表完整名称。</summary>
    public string FullName => new TypeNamePair(this.Type, this.m_Name).ToString();

    /// <summary>获取数据表行的类型。</summary>
    public abstract Type Type { get; }

    /// <summary>获取数据表行数。</summary>
    public abstract int Count { get; }

    /// <summary>读取数据表成功事件。</summary>
    public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
    {
      add => this.m_DataProvider.ReadDataSuccess += value;
      remove => this.m_DataProvider.ReadDataSuccess -= value;
    }

    /// <summary>读取数据表失败事件。</summary>
    public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
    {
      add => this.m_DataProvider.ReadDataFailure += value;
      remove => this.m_DataProvider.ReadDataFailure -= value;
    }

    /// <summary>读取数据表更新事件。</summary>
    public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
    {
      add => this.m_DataProvider.ReadDataUpdate += value;
      remove => this.m_DataProvider.ReadDataUpdate -= value;
    }

    /// <summary>读取数据表时加载依赖资源事件。</summary>
    public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
    {
      add => this.m_DataProvider.ReadDataDependencyAsset += value;
      remove => this.m_DataProvider.ReadDataDependencyAsset -= value;
    }

    /// <summary>读取数据表。</summary>
    /// <param name="dataTableAssetName">数据表资源名称。</param>
    public void ReadData(string dataTableAssetName) => this.m_DataProvider.ReadData(dataTableAssetName);

    /// <summary>读取数据表。</summary>
    /// <param name="dataTableAssetName">数据表资源名称。</param>
    /// <param name="priority">加载数据表资源的优先级。</param>
    public void ReadData(string dataTableAssetName, int priority) => this.m_DataProvider.ReadData(dataTableAssetName, priority);

    /// <summary>读取数据表。</summary>
    /// <param name="dataTableAssetName">数据表资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dataTableAssetName, object userData) => this.m_DataProvider.ReadData(dataTableAssetName, userData);

    /// <summary>读取数据表。</summary>
    /// <param name="dataTableAssetName">数据表资源名称。</param>
    /// <param name="priority">加载数据表资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dataTableAssetName, int priority, object userData) => this.m_DataProvider.ReadData(dataTableAssetName, priority, userData);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableString">要解析的数据表字符串。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(string dataTableString) => this.m_DataProvider.ParseData(dataTableString);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableString">要解析的数据表字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(string dataTableString, object userData) => this.m_DataProvider.ParseData(dataTableString, userData);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableBytes">要解析的数据表二进制流。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(byte[] dataTableBytes) => this.m_DataProvider.ParseData(dataTableBytes);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableBytes">要解析的数据表二进制流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(byte[] dataTableBytes, object userData) => this.m_DataProvider.ParseData(dataTableBytes, userData);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableBytes">要解析的数据表二进制流。</param>
    /// <param name="startIndex">数据表二进制流的起始位置。</param>
    /// <param name="length">数据表二进制流的长度。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(byte[] dataTableBytes, int startIndex, int length) => this.m_DataProvider.ParseData(dataTableBytes, startIndex, length);

    /// <summary>解析数据表。</summary>
    /// <param name="dataTableBytes">要解析的数据表二进制流。</param>
    /// <param name="startIndex">数据表二进制流的起始位置。</param>
    /// <param name="length">数据表二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析数据表成功。</returns>
    public bool ParseData(byte[] dataTableBytes, int startIndex, int length, object userData) => this.m_DataProvider.ParseData(dataTableBytes, startIndex, length, userData);

    /// <summary>检查是否存在数据表行。</summary>
    /// <param name="id">数据表行的编号。</param>
    /// <returns>是否存在数据表行。</returns>
    public abstract bool HasDataRow(int id);

    /// <summary>增加数据表行。</summary>
    /// <param name="dataRowString">要解析的数据表行字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否增加数据表行成功。</returns>
    public abstract bool AddDataRow(string dataRowString, object userData);

    /// <summary>增加数据表行。</summary>
    /// <param name="dataRowBytes">要解析的数据表行二进制流。</param>
    /// <param name="startIndex">数据表行二进制流的起始位置。</param>
    /// <param name="length">数据表行二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否增加数据表行成功。</returns>
    public abstract bool AddDataRow(
      byte[] dataRowBytes,
      int startIndex,
      int length,
      object userData);

    /// <summary>移除指定数据表行。</summary>
    /// <param name="id">要移除数据表行的编号。</param>
    /// <returns>是否移除数据表行成功。</returns>
    public abstract bool RemoveDataRow(int id);

    /// <summary>清空所有数据表行。</summary>
    public abstract void RemoveAllDataRows();

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    internal void SetResourceManager(IResourceManager resourceManager) => this.m_DataProvider.SetResourceManager(resourceManager);

    /// <summary>设置数据提供者辅助器。</summary>
    /// <param name="dataProviderHelper">数据提供者辅助器。</param>
    internal void SetDataProviderHelper(
      IDataProviderHelper<DataTableBase> dataProviderHelper)
    {
      this.m_DataProvider.SetDataProviderHelper(dataProviderHelper);
    }

    /// <summary>关闭并清理数据表。</summary>
    internal abstract void Shutdown();
  }
}
