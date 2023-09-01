// Decompiled with JetBrains decompiler
// Type: GameFramework.DataProvider`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;

namespace GameFramework
{
  /// <summary>数据提供者。</summary>
  /// <typeparam name="T">数据提供者的持有者的类型。</typeparam>
  internal sealed class DataProvider<T> : IDataProvider<T>
  {
    private const int BlockSize = 4096;
    private static byte[] s_CachedBytes;
    private readonly T m_Owner;
    private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
    private readonly LoadBinaryCallbacks m_LoadBinaryCallbacks;
    private IResourceManager m_ResourceManager;
    private IDataProviderHelper<T> m_DataProviderHelper;
    private EventHandler<ReadDataSuccessEventArgs> m_ReadDataSuccessEventHandler;
    private EventHandler<ReadDataFailureEventArgs> m_ReadDataFailureEventHandler;
    private EventHandler<ReadDataUpdateEventArgs> m_ReadDataUpdateEventHandler;
    private EventHandler<ReadDataDependencyAssetEventArgs> m_ReadDataDependencyAssetEventHandler;

    /// <summary>初始化数据提供者的新实例。</summary>
    /// <param name="owner">数据提供者的持有者。</param>
    public DataProvider(T owner)
    {
      this.m_Owner = owner;
      this.m_LoadAssetCallbacks = new LoadAssetCallbacks(new GameFramework.Resource.LoadAssetSuccessCallback(this.LoadAssetSuccessCallback), new LoadAssetFailureCallback(this.LoadAssetOrBinaryFailureCallback), new GameFramework.Resource.LoadAssetUpdateCallback(this.LoadAssetUpdateCallback), new GameFramework.Resource.LoadAssetDependencyAssetCallback(this.LoadAssetDependencyAssetCallback));
      this.m_LoadBinaryCallbacks = new LoadBinaryCallbacks(new GameFramework.Resource.LoadBinarySuccessCallback(this.LoadBinarySuccessCallback), new LoadBinaryFailureCallback(this.LoadAssetOrBinaryFailureCallback));
      this.m_ResourceManager = (IResourceManager) null;
      this.m_DataProviderHelper = (IDataProviderHelper<T>) null;
      this.m_ReadDataSuccessEventHandler = (EventHandler<ReadDataSuccessEventArgs>) null;
      this.m_ReadDataFailureEventHandler = (EventHandler<ReadDataFailureEventArgs>) null;
      this.m_ReadDataUpdateEventHandler = (EventHandler<ReadDataUpdateEventArgs>) null;
      this.m_ReadDataDependencyAssetEventHandler = (EventHandler<ReadDataDependencyAssetEventArgs>) null;
    }

    /// <summary>获取缓冲二进制流的大小。</summary>
    public static int CachedBytesSize => DataProvider<T>.s_CachedBytes == null ? 0 : DataProvider<T>.s_CachedBytes.Length;

    /// <summary>读取数据成功事件。</summary>
    public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
    {
      add => this.m_ReadDataSuccessEventHandler += value;
      remove => this.m_ReadDataSuccessEventHandler -= value;
    }

    /// <summary>读取数据失败事件。</summary>
    public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
    {
      add => this.m_ReadDataFailureEventHandler += value;
      remove => this.m_ReadDataFailureEventHandler -= value;
    }

    /// <summary>读取数据更新事件。</summary>
    public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
    {
      add => this.m_ReadDataUpdateEventHandler += value;
      remove => this.m_ReadDataUpdateEventHandler -= value;
    }

    /// <summary>读取数据时加载依赖资源事件。</summary>
    public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
    {
      add => this.m_ReadDataDependencyAssetEventHandler += value;
      remove => this.m_ReadDataDependencyAssetEventHandler -= value;
    }

    /// <summary>确保二进制流缓存分配足够大小的内存并缓存。</summary>
    /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
    public static void EnsureCachedBytesSize(int ensureSize)
    {
      if (ensureSize < 0)
        throw new GameFrameworkException("Ensure size is invalid.");
      if (DataProvider<T>.s_CachedBytes != null && DataProvider<T>.s_CachedBytes.Length >= ensureSize)
        return;
      DataProvider<T>.FreeCachedBytes();
      DataProvider<T>.s_CachedBytes = new byte[(ensureSize - 1 + 4096) / 4096 * 4096];
    }

    /// <summary>释放缓存的二进制流。</summary>
    public static void FreeCachedBytes() => DataProvider<T>.s_CachedBytes = (byte[]) null;

    /// <summary>读取数据。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    public void ReadData(string dataAssetName) => this.ReadData(dataAssetName, 0, (object) null);

    /// <summary>读取数据。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="priority">加载数据资源的优先级。</param>
    public void ReadData(string dataAssetName, int priority) => this.ReadData(dataAssetName, priority, (object) null);

    /// <summary>读取数据。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dataAssetName, object userData) => this.ReadData(dataAssetName, 0, userData);

    /// <summary>读取数据。</summary>
    /// <param name="dataAssetName">内容资源名称。</param>
    /// <param name="priority">加载数据资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dataAssetName, int priority, object userData)
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_DataProviderHelper == null)
        throw new GameFrameworkException("You must set data provider helper first.");
      HasAssetResult hasAssetResult = this.m_ResourceManager.HasAsset(dataAssetName);
      switch (hasAssetResult)
      {
        case HasAssetResult.AssetOnDisk:
        case HasAssetResult.AssetOnFileSystem:
          this.m_ResourceManager.LoadAsset(dataAssetName, priority, this.m_LoadAssetCallbacks, userData);
          break;
        case HasAssetResult.BinaryOnDisk:
          this.m_ResourceManager.LoadBinary(dataAssetName, this.m_LoadBinaryCallbacks, userData);
          break;
        case HasAssetResult.BinaryOnFileSystem:
          int binaryLength = this.m_ResourceManager.GetBinaryLength(dataAssetName);
          DataProvider<T>.EnsureCachedBytesSize(binaryLength);
          if (binaryLength != this.m_ResourceManager.LoadBinaryFromFileSystem(dataAssetName, DataProvider<T>.s_CachedBytes))
            throw new GameFrameworkException(Utility.Text.Format<string>("Load binary '{0}' from file system with internal error.", dataAssetName));
          try
          {
            if (!this.m_DataProviderHelper.ReadData(this.m_Owner, dataAssetName, DataProvider<T>.s_CachedBytes, 0, binaryLength, userData))
              throw new GameFrameworkException(Utility.Text.Format<string>("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
            if (this.m_ReadDataSuccessEventHandler == null)
              break;
            ReadDataSuccessEventArgs e = ReadDataSuccessEventArgs.Create(dataAssetName, 0.0f, userData);
            this.m_ReadDataSuccessEventHandler((object) this, e);
            ReferencePool.Release((IReference) e);
            break;
          }
          catch (Exception ex)
          {
            if (this.m_ReadDataFailureEventHandler != null)
            {
              ReadDataFailureEventArgs e = ReadDataFailureEventArgs.Create(dataAssetName, ex.ToString(), userData);
              this.m_ReadDataFailureEventHandler((object) this, e);
              ReferencePool.Release((IReference) e);
              break;
            }
            throw;
          }
        default:
          throw new GameFrameworkException(Utility.Text.Format<string, HasAssetResult>("Data asset '{0}' is '{1}'.", dataAssetName, hasAssetResult));
      }
    }

    /// <summary>解析内容。</summary>
    /// <param name="dataString">要解析的内容字符串。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(string dataString) => this.ParseData(dataString, (object) null);

    /// <summary>解析内容。</summary>
    /// <param name="dataString">要解析的内容字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(string dataString, object userData)
    {
      if (this.m_DataProviderHelper == null)
        throw new GameFrameworkException("You must set data helper first.");
      if (dataString == null)
        throw new GameFrameworkException("Data string is invalid.");
      try
      {
        return this.m_DataProviderHelper.ParseData(this.m_Owner, dataString, userData);
      }
      catch (Exception ex)
      {
        if (!(ex is GameFrameworkException))
          throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not parse data string with exception '{0}'.", ex), ex);
        throw;
      }
    }

    /// <summary>解析内容。</summary>
    /// <param name="dataBytes">要解析的内容二进制流。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(byte[] dataBytes) => dataBytes != null ? this.ParseData(dataBytes, 0, dataBytes.Length, (object) null) : throw new GameFrameworkException("Data bytes is invalid.");

    /// <summary>解析内容。</summary>
    /// <param name="dataBytes">要解析的内容二进制流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(byte[] dataBytes, object userData)
    {
      if (dataBytes == null)
        throw new GameFrameworkException("Data bytes is invalid.");
      return this.ParseData(dataBytes, 0, dataBytes.Length, userData);
    }

    /// <summary>解析内容。</summary>
    /// <param name="dataBytes">要解析的内容二进制流。</param>
    /// <param name="startIndex">内容二进制流的起始位置。</param>
    /// <param name="length">内容二进制流的长度。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(byte[] dataBytes, int startIndex, int length) => this.ParseData(dataBytes, startIndex, length, (object) null);

    /// <summary>解析内容。</summary>
    /// <param name="dataBytes">要解析的内容二进制流。</param>
    /// <param name="startIndex">内容二进制流的起始位置。</param>
    /// <param name="length">内容二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析内容成功。</returns>
    public bool ParseData(byte[] dataBytes, int startIndex, int length, object userData)
    {
      if (this.m_DataProviderHelper == null)
        throw new GameFrameworkException("You must set data helper first.");
      if (dataBytes == null)
        throw new GameFrameworkException("Data bytes is invalid.");
      if (startIndex >= 0 && length >= 0)
      {
        if (startIndex + length <= dataBytes.Length)
        {
          try
          {
            return this.m_DataProviderHelper.ParseData(this.m_Owner, dataBytes, startIndex, length, userData);
          }
          catch (Exception ex)
          {
            if (!(ex is GameFrameworkException))
              throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not parse data bytes with exception '{0}'.", ex), ex);
            throw;
          }
        }
      }
      throw new GameFrameworkException("Start index or length is invalid.");
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    internal void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>设置数据提供者辅助器。</summary>
    /// <param name="dataProviderHelper">数据提供者辅助器。</param>
    internal void SetDataProviderHelper(IDataProviderHelper<T> dataProviderHelper) => this.m_DataProviderHelper = dataProviderHelper != null ? dataProviderHelper : throw new GameFrameworkException("Data provider helper is invalid.");

    private void LoadAssetSuccessCallback(
      string dataAssetName,
      object dataAsset,
      float duration,
      object userData)
    {
      try
      {
        if (!this.m_DataProviderHelper.ReadData(this.m_Owner, dataAssetName, dataAsset, userData))
          throw new GameFrameworkException(Utility.Text.Format<string>("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
        if (this.m_ReadDataSuccessEventHandler == null)
          return;
        ReadDataSuccessEventArgs e = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
        this.m_ReadDataSuccessEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      catch (Exception ex)
      {
        if (this.m_ReadDataFailureEventHandler != null)
        {
          ReadDataFailureEventArgs e = ReadDataFailureEventArgs.Create(dataAssetName, ex.ToString(), userData);
          this.m_ReadDataFailureEventHandler((object) this, e);
          ReferencePool.Release((IReference) e);
        }
        else
          throw;
      }
      finally
      {
        this.m_DataProviderHelper.ReleaseDataAsset(this.m_Owner, dataAsset);
      }
    }

    private void LoadAssetOrBinaryFailureCallback(
      string dataAssetName,
      LoadResourceStatus status,
      string errorMessage,
      object userData)
    {
      string str = Utility.Text.Format<string, LoadResourceStatus, string>("Load data failure, data asset name '{0}', status '{1}', error message '{2}'.", dataAssetName, status, errorMessage);
      if (this.m_ReadDataFailureEventHandler == null)
        throw new GameFrameworkException(str);
      ReadDataFailureEventArgs e = ReadDataFailureEventArgs.Create(dataAssetName, str, userData);
      this.m_ReadDataFailureEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadAssetUpdateCallback(string dataAssetName, float progress, object userData)
    {
      if (this.m_ReadDataUpdateEventHandler == null)
        return;
      ReadDataUpdateEventArgs e = ReadDataUpdateEventArgs.Create(dataAssetName, progress, userData);
      this.m_ReadDataUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadAssetDependencyAssetCallback(
      string dataAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      if (this.m_ReadDataDependencyAssetEventHandler == null)
        return;
      ReadDataDependencyAssetEventArgs e = ReadDataDependencyAssetEventArgs.Create(dataAssetName, dependencyAssetName, loadedCount, totalCount, userData);
      this.m_ReadDataDependencyAssetEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadBinarySuccessCallback(
      string dataAssetName,
      byte[] dataBytes,
      float duration,
      object userData)
    {
      try
      {
        if (!this.m_DataProviderHelper.ReadData(this.m_Owner, dataAssetName, dataBytes, 0, dataBytes.Length, userData))
          throw new GameFrameworkException(Utility.Text.Format<string>("Load data failure in data provider helper, data asset name '{0}'.", dataAssetName));
        if (this.m_ReadDataSuccessEventHandler == null)
          return;
        ReadDataSuccessEventArgs e = ReadDataSuccessEventArgs.Create(dataAssetName, duration, userData);
        this.m_ReadDataSuccessEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
      }
      catch (Exception ex)
      {
        if (this.m_ReadDataFailureEventHandler != null)
        {
          ReadDataFailureEventArgs e = ReadDataFailureEventArgs.Create(dataAssetName, ex.ToString(), userData);
          this.m_ReadDataFailureEventHandler((object) this, e);
          ReferencePool.Release((IReference) e);
        }
        else
          throw;
      }
    }
  }
}
