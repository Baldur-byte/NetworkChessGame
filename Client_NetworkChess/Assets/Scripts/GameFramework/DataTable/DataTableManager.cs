// Decompiled with JetBrains decompiler
// Type: GameFramework.DataTable.DataTableManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework.DataTable
{
  /// <summary>数据表管理器。</summary>
  internal sealed class DataTableManager : GameFrameworkModule, IDataTableManager
  {
    private readonly Dictionary<TypeNamePair, DataTableBase> m_DataTables;
    private IResourceManager m_ResourceManager;
    private IDataProviderHelper<DataTableBase> m_DataProviderHelper;
    private IDataTableHelper m_DataTableHelper;

    /// <summary>初始化数据表管理器的新实例。</summary>
    public DataTableManager()
    {
      this.m_DataTables = new Dictionary<TypeNamePair, DataTableBase>();
      this.m_ResourceManager = (IResourceManager) null;
      this.m_DataProviderHelper = (IDataProviderHelper<DataTableBase>) null;
      this.m_DataTableHelper = (IDataTableHelper) null;
    }

    /// <summary>获取数据表数量。</summary>
    public int Count => this.m_DataTables.Count;

    /// <summary>获取缓冲二进制流的大小。</summary>
    public int CachedBytesSize => DataProvider<DataTableBase>.CachedBytesSize;

    /// <summary>数据表管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理数据表管理器。</summary>
    internal override void Shutdown()
    {
      foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in this.m_DataTables)
        dataTable.Value.Shutdown();
      this.m_DataTables.Clear();
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>设置数据表数据提供者辅助器。</summary>
    /// <param name="dataProviderHelper">数据表数据提供者辅助器。</param>
    public void SetDataProviderHelper(
      IDataProviderHelper<DataTableBase> dataProviderHelper)
    {
      this.m_DataProviderHelper = dataProviderHelper != null ? dataProviderHelper : throw new GameFrameworkException("Data provider helper is invalid.");
    }

    /// <summary>设置数据表辅助器。</summary>
    /// <param name="dataTableHelper">数据表辅助器。</param>
    public void SetDataTableHelper(IDataTableHelper dataTableHelper) => this.m_DataTableHelper = dataTableHelper != null ? dataTableHelper : throw new GameFrameworkException("Data table helper is invalid.");

    /// <summary>确保二进制流缓存分配足够大小的内存并缓存。</summary>
    /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
    public void EnsureCachedBytesSize(int ensureSize) => DataProvider<DataTableBase>.EnsureCachedBytesSize(ensureSize);

    /// <summary>释放缓存的二进制流。</summary>
    public void FreeCachedBytes() => DataProvider<DataTableBase>.FreeCachedBytes();

    /// <summary>是否存在数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <returns>是否存在数据表。</returns>
    public bool HasDataTable<T>() where T : IDataRow => this.InternalHasDataTable(new TypeNamePair(typeof (T)));

    /// <summary>是否存在数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <returns>是否存在数据表。</returns>
    public bool HasDataTable(Type dataRowType)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalHasDataTable(new TypeNamePair(dataRowType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>是否存在数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <param name="name">数据表名称。</param>
    /// <returns>是否存在数据表。</returns>
    public bool HasDataTable<T>(string name) where T : IDataRow => this.InternalHasDataTable(new TypeNamePair(typeof (T), name));

    /// <summary>是否存在数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <param name="name">数据表名称。</param>
    /// <returns>是否存在数据表。</returns>
    public bool HasDataTable(Type dataRowType, string name)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalHasDataTable(new TypeNamePair(dataRowType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>获取数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <returns>要获取的数据表。</returns>
    public IDataTable<T> GetDataTable<T>() where T : IDataRow => (IDataTable<T>) this.InternalGetDataTable(new TypeNamePair(typeof (T)));

    /// <summary>获取数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <returns>要获取的数据表。</returns>
    public DataTableBase GetDataTable(Type dataRowType)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalGetDataTable(new TypeNamePair(dataRowType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>获取数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <param name="name">数据表名称。</param>
    /// <returns>要获取的数据表。</returns>
    public IDataTable<T> GetDataTable<T>(string name) where T : IDataRow => (IDataTable<T>) this.InternalGetDataTable(new TypeNamePair(typeof (T), name));

    /// <summary>获取数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <param name="name">数据表名称。</param>
    /// <returns>要获取的数据表。</returns>
    public DataTableBase GetDataTable(Type dataRowType, string name)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalGetDataTable(new TypeNamePair(dataRowType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>获取所有数据表。</summary>
    /// <returns>所有数据表。</returns>
    public DataTableBase[] GetAllDataTables()
    {
      int num = 0;
      DataTableBase[] allDataTables = new DataTableBase[this.m_DataTables.Count];
      foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in this.m_DataTables)
        allDataTables[num++] = dataTable.Value;
      return allDataTables;
    }

    /// <summary>获取所有数据表。</summary>
    /// <param name="results">所有数据表。</param>
    public void GetAllDataTables(List<DataTableBase> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<TypeNamePair, DataTableBase> dataTable in this.m_DataTables)
        results.Add(dataTable.Value);
    }

    /// <summary>创建数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <returns>要创建的数据表。</returns>
    public IDataTable<T> CreateDataTable<T>() where T : class, IDataRow, new() => this.CreateDataTable<T>(string.Empty);

    /// <summary>创建数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <returns>要创建的数据表。</returns>
    public DataTableBase CreateDataTable(Type dataRowType) => this.CreateDataTable(dataRowType, string.Empty);

    /// <summary>创建数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <param name="name">数据表名称。</param>
    /// <returns>要创建的数据表。</returns>
    public IDataTable<T> CreateDataTable<T>(string name) where T : class, IDataRow, new()
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_DataProviderHelper == null)
        throw new GameFrameworkException("You must set data provider helper first.");
      TypeNamePair key = new TypeNamePair(typeof (T), name);
      DataTableManager.DataTable<T> dataTable = !this.HasDataTable<T>(name) ? new DataTableManager.DataTable<T>(name) : throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist data table '{0}'.", key));
      dataTable.SetResourceManager(this.m_ResourceManager);
      dataTable.SetDataProviderHelper(this.m_DataProviderHelper);
      this.m_DataTables.Add(key, (DataTableBase) dataTable);
      return (IDataTable<T>) dataTable;
    }

    /// <summary>创建数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <param name="name">数据表名称。</param>
    /// <returns>要创建的数据表。</returns>
    public DataTableBase CreateDataTable(Type dataRowType, string name)
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_DataProviderHelper == null)
        throw new GameFrameworkException("You must set data provider helper first.");
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      TypeNamePair key = typeof (IDataRow).IsAssignableFrom(dataRowType) ? new TypeNamePair(dataRowType, name) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
      DataTableBase dataTable = !this.HasDataTable(dataRowType, name) ? (DataTableBase) Activator.CreateInstance(typeof (DataTableManager.DataTable<>).MakeGenericType(dataRowType), (object) name) : throw new GameFrameworkException(Utility.Text.Format<TypeNamePair>("Already exist data table '{0}'.", key));
      dataTable.SetResourceManager(this.m_ResourceManager);
      dataTable.SetDataProviderHelper(this.m_DataProviderHelper);
      this.m_DataTables.Add(key, dataTable);
      return dataTable;
    }

    /// <summary>销毁数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    public bool DestroyDataTable<T>() where T : IDataRow => this.InternalDestroyDataTable(new TypeNamePair(typeof (T)));

    /// <summary>销毁数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <returns>是否销毁数据表成功。</returns>
    public bool DestroyDataTable(Type dataRowType)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalDestroyDataTable(new TypeNamePair(dataRowType)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>销毁数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <param name="name">数据表名称。</param>
    public bool DestroyDataTable<T>(string name) where T : IDataRow => this.InternalDestroyDataTable(new TypeNamePair(typeof (T), name));

    /// <summary>销毁数据表。</summary>
    /// <param name="dataRowType">数据表行的类型。</param>
    /// <param name="name">数据表名称。</param>
    /// <returns>是否销毁数据表成功。</returns>
    public bool DestroyDataTable(Type dataRowType, string name)
    {
      if (dataRowType == null)
        throw new GameFrameworkException("Data row type is invalid.");
      return typeof (IDataRow).IsAssignableFrom(dataRowType) ? this.InternalDestroyDataTable(new TypeNamePair(dataRowType, name)) : throw new GameFrameworkException(Utility.Text.Format<string>("Data row type '{0}' is invalid.", dataRowType.FullName));
    }

    /// <summary>销毁数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    /// <param name="dataTable">要销毁的数据表。</param>
    /// <returns>是否销毁数据表成功。</returns>
    public bool DestroyDataTable<T>(IDataTable<T> dataTable) where T : IDataRow => dataTable != null ? this.InternalDestroyDataTable(new TypeNamePair(typeof (T), dataTable.Name)) : throw new GameFrameworkException("Data table is invalid.");

    /// <summary>销毁数据表。</summary>
    /// <param name="dataTable">要销毁的数据表。</param>
    /// <returns>是否销毁数据表成功。</returns>
    public bool DestroyDataTable(DataTableBase dataTable)
    {
      if (dataTable == null)
        throw new GameFrameworkException("Data table is invalid.");
      return this.InternalDestroyDataTable(new TypeNamePair(dataTable.Type, dataTable.Name));
    }

    private bool InternalHasDataTable(TypeNamePair typeNamePair) => this.m_DataTables.ContainsKey(typeNamePair);

    private DataTableBase InternalGetDataTable(TypeNamePair typeNamePair)
    {
      DataTableBase dataTableBase = (DataTableBase) null;
      return this.m_DataTables.TryGetValue(typeNamePair, out dataTableBase) ? dataTableBase : (DataTableBase) null;
    }

    private bool InternalDestroyDataTable(TypeNamePair typeNamePair)
    {
      DataTableBase dataTableBase = (DataTableBase) null;
      if (!this.m_DataTables.TryGetValue(typeNamePair, out dataTableBase))
        return false;
      dataTableBase.Shutdown();
      return this.m_DataTables.Remove(typeNamePair);
    }

    /// <summary>数据表。</summary>
    /// <typeparam name="T">数据表行的类型。</typeparam>
    private sealed class DataTable<T> : DataTableBase, IDataTable<T>, IEnumerable<T>, IEnumerable where T : class, IDataRow, new()
    {
      private readonly Dictionary<int, T> m_DataSet;
      private T m_MinIdDataRow;
      private T m_MaxIdDataRow;

      /// <summary>初始化数据表的新实例。</summary>
      /// <param name="name">数据表名称。</param>
      public DataTable(string name)
        : base(name)
      {
        this.m_DataSet = new Dictionary<int, T>();
        this.m_MinIdDataRow = default (T);
        this.m_MaxIdDataRow = default (T);
      }

      /// <summary>获取数据表行的类型。</summary>
      public override Type Type => typeof (T);

      /// <summary>获取数据表行数。</summary>
      public override int Count => this.m_DataSet.Count;

      /// <summary>获取数据表行。</summary>
      /// <param name="id">数据表行的编号。</param>
      /// <returns>数据表行。</returns>
      public T this[int id] => this.GetDataRow(id);

      /// <summary>获取编号最小的数据表行。</summary>
      public T MinIdDataRow => this.m_MinIdDataRow;

      /// <summary>获取编号最大的数据表行。</summary>
      public T MaxIdDataRow => this.m_MaxIdDataRow;

      /// <summary>检查是否存在数据表行。</summary>
      /// <param name="id">数据表行的编号。</param>
      /// <returns>是否存在数据表行。</returns>
      public override bool HasDataRow(int id) => this.m_DataSet.ContainsKey(id);

      /// <summary>检查是否存在数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <returns>是否存在数据表行。</returns>
      public bool HasDataRow(Predicate<T> condition)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            return true;
        }
        return false;
      }

      /// <summary>获取数据表行。</summary>
      /// <param name="id">数据表行的编号。</param>
      /// <returns>数据表行。</returns>
      public T GetDataRow(int id)
      {
        T obj = default (T);
        return this.m_DataSet.TryGetValue(id, out obj) ? obj : default (T);
      }

      /// <summary>获取符合条件的数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <returns>符合条件的数据表行。</returns>
      /// <remarks>当存在多个符合条件的数据表行时，仅返回第一个符合条件的数据表行。</remarks>
      public T GetDataRow(Predicate<T> condition)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            return data.Value;
        }
        return default (T);
      }

      /// <summary>获取符合条件的数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <returns>符合条件的数据表行。</returns>
      public T[] GetDataRows(Predicate<T> condition)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        List<T> objList = new List<T>();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            objList.Add(data.Value);
        }
        return objList.ToArray();
      }

      /// <summary>获取符合条件的数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <param name="results">符合条件的数据表行。</param>
      public void GetDataRows(Predicate<T> condition, List<T> results)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            results.Add(data.Value);
        }
      }

      /// <summary>获取排序后的数据表行。</summary>
      /// <param name="comparison">要排序的条件。</param>
      /// <returns>排序后的数据表行。</returns>
      public T[] GetDataRows(Comparison<T> comparison)
      {
        if (comparison == null)
          throw new GameFrameworkException("Comparison is invalid.");
        List<T> objList = new List<T>();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
          objList.Add(data.Value);
        objList.Sort(comparison);
        return objList.ToArray();
      }

      /// <summary>获取排序后的数据表行。</summary>
      /// <param name="comparison">要排序的条件。</param>
      /// <param name="results">排序后的数据表行。</param>
      public void GetDataRows(Comparison<T> comparison, List<T> results)
      {
        if (comparison == null)
          throw new GameFrameworkException("Comparison is invalid.");
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
          results.Add(data.Value);
        results.Sort(comparison);
      }

      /// <summary>获取排序后的符合条件的数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <param name="comparison">要排序的条件。</param>
      /// <returns>排序后的符合条件的数据表行。</returns>
      public T[] GetDataRows(Predicate<T> condition, Comparison<T> comparison)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        if (comparison == null)
          throw new GameFrameworkException("Comparison is invalid.");
        List<T> objList = new List<T>();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            objList.Add(data.Value);
        }
        objList.Sort(comparison);
        return objList.ToArray();
      }

      /// <summary>获取排序后的符合条件的数据表行。</summary>
      /// <param name="condition">要检查的条件。</param>
      /// <param name="comparison">要排序的条件。</param>
      /// <param name="results">排序后的符合条件的数据表行。</param>
      public void GetDataRows(Predicate<T> condition, Comparison<T> comparison, List<T> results)
      {
        if (condition == null)
          throw new GameFrameworkException("Condition is invalid.");
        if (comparison == null)
          throw new GameFrameworkException("Comparison is invalid.");
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
        {
          if (condition(data.Value))
            results.Add(data.Value);
        }
        results.Sort(comparison);
      }

      /// <summary>获取所有数据表行。</summary>
      /// <returns>所有数据表行。</returns>
      public T[] GetAllDataRows()
      {
        int num = 0;
        T[] allDataRows = new T[this.m_DataSet.Count];
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
          allDataRows[num++] = data.Value;
        return allDataRows;
      }

      /// <summary>获取所有数据表行。</summary>
      /// <param name="results">所有数据表行。</param>
      public void GetAllDataRows(List<T> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (KeyValuePair<int, T> data in this.m_DataSet)
          results.Add(data.Value);
      }

      /// <summary>增加数据表行。</summary>
      /// <param name="dataRowString">要解析的数据表行字符串。</param>
      /// <param name="userData">用户自定义数据。</param>
      /// <returns>是否增加数据表行成功。</returns>
      public override bool AddDataRow(string dataRowString, object userData)
      {
        try
        {
          T dataRow = new T();
          if (!dataRow.ParseDataRow(dataRowString, userData))
            return false;
          this.InternalAddDataRow(dataRow);
          return true;
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, Exception>("Can not parse data row string for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof (T), this.Name), ex), ex);
          throw;
        }
      }

      /// <summary>增加数据表行。</summary>
      /// <param name="dataRowBytes">要解析的数据表行二进制流。</param>
      /// <param name="startIndex">数据表行二进制流的起始位置。</param>
      /// <param name="length">数据表行二进制流的长度。</param>
      /// <param name="userData">用户自定义数据。</param>
      /// <returns>是否增加数据表行成功。</returns>
      public override bool AddDataRow(
        byte[] dataRowBytes,
        int startIndex,
        int length,
        object userData)
      {
        try
        {
          T dataRow = new T();
          if (!dataRow.ParseDataRow(dataRowBytes, startIndex, length, userData))
            return false;
          this.InternalAddDataRow(dataRow);
          return true;
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<TypeNamePair, Exception>("Can not parse data row bytes for data table '{0}' with exception '{1}'.", new TypeNamePair(typeof (T), this.Name), ex), ex);
          throw;
        }
      }

      /// <summary>移除指定数据表行。</summary>
      /// <param name="id">要移除数据表行的编号。</param>
      /// <returns>是否移除数据表行成功。</returns>
      public override bool RemoveDataRow(int id)
      {
        if (!this.HasDataRow(id) || !this.m_DataSet.Remove(id))
          return false;
        if ((object) this.m_MinIdDataRow != null && this.m_MinIdDataRow.Id == id || (object) this.m_MaxIdDataRow != null && this.m_MaxIdDataRow.Id == id)
        {
          this.m_MinIdDataRow = default (T);
          this.m_MaxIdDataRow = default (T);
          foreach (KeyValuePair<int, T> data in this.m_DataSet)
          {
            if ((object) this.m_MinIdDataRow == null || this.m_MinIdDataRow.Id > data.Key)
              this.m_MinIdDataRow = data.Value;
            if ((object) this.m_MaxIdDataRow == null || this.m_MaxIdDataRow.Id < data.Key)
              this.m_MaxIdDataRow = data.Value;
          }
        }
        return true;
      }

      /// <summary>清空所有数据表行。</summary>
      public override void RemoveAllDataRows()
      {
        this.m_DataSet.Clear();
        this.m_MinIdDataRow = default (T);
        this.m_MaxIdDataRow = default (T);
      }

      /// <summary>返回循环访问集合的枚举数。</summary>
      /// <returns>循环访问集合的枚举数。</returns>
      public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this.m_DataSet.Values.GetEnumerator();

      /// <summary>返回循环访问集合的枚举数。</summary>
      /// <returns>循环访问集合的枚举数。</returns>
      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.m_DataSet.Values.GetEnumerator();

      /// <summary>关闭并清理数据表。</summary>
      internal override void Shutdown() => this.m_DataSet.Clear();

      private void InternalAddDataRow(T dataRow)
      {
        if (this.HasDataRow(dataRow.Id))
          throw new GameFrameworkException(Utility.Text.Format<int, TypeNamePair>("Already exist '{0}' in data table '{1}'.", dataRow.Id, new TypeNamePair(typeof (T), this.Name)));
        this.m_DataSet.Add(dataRow.Id, dataRow);
        if ((object) this.m_MinIdDataRow == null || this.m_MinIdDataRow.Id > dataRow.Id)
          this.m_MinIdDataRow = dataRow;
        if ((object) this.m_MaxIdDataRow != null && this.m_MaxIdDataRow.Id >= dataRow.Id)
          return;
        this.m_MaxIdDataRow = dataRow;
      }
    }
  }
}
