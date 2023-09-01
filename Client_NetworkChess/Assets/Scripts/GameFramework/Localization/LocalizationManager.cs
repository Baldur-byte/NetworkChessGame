// Decompiled with JetBrains decompiler
// Type: GameFramework.Localization.LocalizationManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Localization
{
  /// <summary>本地化管理器。</summary>
  internal sealed class LocalizationManager : 
    GameFrameworkModule,
    ILocalizationManager,
    IDataProvider<ILocalizationManager>
  {
    private readonly Dictionary<string, string> m_Dictionary;
    private readonly DataProvider<ILocalizationManager> m_DataProvider;
    private ILocalizationHelper m_LocalizationHelper;
    private Language m_Language;

    /// <summary>初始化本地化管理器的新实例。</summary>
    public LocalizationManager()
    {
      this.m_Dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_DataProvider = new DataProvider<ILocalizationManager>((ILocalizationManager) this);
      this.m_LocalizationHelper = (ILocalizationHelper) null;
      this.m_Language = Language.Unspecified;
    }

    /// <summary>获取或设置本地化语言。</summary>
    public Language Language
    {
      get => this.m_Language;
      set => this.m_Language = value != Language.Unspecified ? value : throw new GameFrameworkException("Language is invalid.");
    }

    /// <summary>获取系统语言。</summary>
    public Language SystemLanguage => this.m_LocalizationHelper != null ? this.m_LocalizationHelper.SystemLanguage : throw new GameFrameworkException("You must set localization helper first.");

    /// <summary>获取字典数量。</summary>
    public int DictionaryCount => this.m_Dictionary.Count;

    /// <summary>获取缓冲二进制流的大小。</summary>
    public int CachedBytesSize => DataProvider<ILocalizationManager>.CachedBytesSize;

    /// <summary>读取字典成功事件。</summary>
    public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
    {
      add => this.m_DataProvider.ReadDataSuccess += value;
      remove => this.m_DataProvider.ReadDataSuccess -= value;
    }

    /// <summary>读取字典失败事件。</summary>
    public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
    {
      add => this.m_DataProvider.ReadDataFailure += value;
      remove => this.m_DataProvider.ReadDataFailure -= value;
    }

    /// <summary>读取字典更新事件。</summary>
    public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
    {
      add => this.m_DataProvider.ReadDataUpdate += value;
      remove => this.m_DataProvider.ReadDataUpdate -= value;
    }

    /// <summary>读取字典时加载依赖资源事件。</summary>
    public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
    {
      add => this.m_DataProvider.ReadDataDependencyAsset += value;
      remove => this.m_DataProvider.ReadDataDependencyAsset -= value;
    }

    /// <summary>本地化管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理本地化管理器。</summary>
    internal override void Shutdown()
    {
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_DataProvider.SetResourceManager(resourceManager);

    /// <summary>设置本地化数据提供者辅助器。</summary>
    /// <param name="dataProviderHelper">本地化数据提供者辅助器。</param>
    public void SetDataProviderHelper(
      IDataProviderHelper<ILocalizationManager> dataProviderHelper)
    {
      this.m_DataProvider.SetDataProviderHelper(dataProviderHelper);
    }

    /// <summary>设置本地化辅助器。</summary>
    /// <param name="localizationHelper">本地化辅助器。</param>
    public void SetLocalizationHelper(ILocalizationHelper localizationHelper) => this.m_LocalizationHelper = localizationHelper != null ? localizationHelper : throw new GameFrameworkException("Localization helper is invalid.");

    /// <summary>确保二进制流缓存分配足够大小的内存并缓存。</summary>
    /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
    public void EnsureCachedBytesSize(int ensureSize) => DataProvider<ILocalizationManager>.EnsureCachedBytesSize(ensureSize);

    /// <summary>释放缓存的二进制流。</summary>
    public void FreeCachedBytes() => DataProvider<ILocalizationManager>.FreeCachedBytes();

    /// <summary>读取字典。</summary>
    /// <param name="dictionaryAssetName">字典资源名称。</param>
    public void ReadData(string dictionaryAssetName) => this.m_DataProvider.ReadData(dictionaryAssetName);

    /// <summary>读取字典。</summary>
    /// <param name="dictionaryAssetName">字典资源名称。</param>
    /// <param name="priority">加载字典资源的优先级。</param>
    public void ReadData(string dictionaryAssetName, int priority) => this.m_DataProvider.ReadData(dictionaryAssetName, priority);

    /// <summary>读取字典。</summary>
    /// <param name="dictionaryAssetName">字典资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dictionaryAssetName, object userData) => this.m_DataProvider.ReadData(dictionaryAssetName, userData);

    /// <summary>读取字典。</summary>
    /// <param name="dictionaryAssetName">字典资源名称。</param>
    /// <param name="priority">加载字典资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string dictionaryAssetName, int priority, object userData) => this.m_DataProvider.ReadData(dictionaryAssetName, priority, userData);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryString">要解析的字典字符串。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(string dictionaryString) => this.m_DataProvider.ParseData(dictionaryString);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryString">要解析的字典字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(string dictionaryString, object userData) => this.m_DataProvider.ParseData(dictionaryString, userData);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryBytes">要解析的字典二进制流。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(byte[] dictionaryBytes) => this.m_DataProvider.ParseData(dictionaryBytes);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryBytes">要解析的字典二进制流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(byte[] dictionaryBytes, object userData) => this.m_DataProvider.ParseData(dictionaryBytes, userData);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryBytes">要解析的字典二进制流。</param>
    /// <param name="startIndex">字典二进制流的起始位置。</param>
    /// <param name="length">字典二进制流的长度。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(byte[] dictionaryBytes, int startIndex, int length) => this.m_DataProvider.ParseData(dictionaryBytes, startIndex, length);

    /// <summary>解析字典。</summary>
    /// <param name="dictionaryBytes">要解析的字典二进制流。</param>
    /// <param name="startIndex">字典二进制流的起始位置。</param>
    /// <param name="length">字典二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析字典成功。</returns>
    public bool ParseData(byte[] dictionaryBytes, int startIndex, int length, object userData) => this.m_DataProvider.ParseData(dictionaryBytes, startIndex, length, userData);

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <param name="key">字典主键。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString(string key) => this.GetRawString(key) ?? Utility.Text.Format<string>("<NoKey>{0}", key);

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T">字典参数的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg">字典参数。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T>(string key, T arg)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T>(rawString, arg);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T, Exception>("<Error>{0},{1},{2},{3}", key, rawString, arg, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2>(string key, T1 arg1, T2 arg2)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2>(rawString, arg1, arg2);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, Exception>("<Error>{0},{1},{2},{3},{4}", key, rawString, arg1, arg2, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3>(string key, T1 arg1, T2 arg2, T3 arg3)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3>(rawString, arg1, arg2, arg3);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, Exception>("<Error>{0},{1},{2},{3},{4},{5}", key, rawString, arg1, arg2, arg3, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4>(string key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4>(rawString, arg1, arg2, arg3, arg4);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6}", key, rawString, arg1, arg2, arg3, arg4, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5>(rawString, arg1, arg2, arg3, arg4, arg5);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7}", key, rawString, arg1, arg2, arg3, arg4, arg5, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6>(rawString, arg1, arg2, arg3, arg4, arg5, arg6);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, T9, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <typeparam name="T12">字典参数 12 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <param name="arg12">字典参数 12。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11,
      T12 arg12)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <typeparam name="T12">字典参数 12 的类型。</typeparam>
    /// <typeparam name="T13">字典参数 13 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <param name="arg12">字典参数 12。</param>
    /// <param name="arg13">字典参数 13。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11,
      T12 arg12,
      T13 arg13)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
      }
      catch (Exception ex)
      {
        return Utility.Text.Format<string, string, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Exception>("<Error>{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", key, rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <typeparam name="T12">字典参数 12 的类型。</typeparam>
    /// <typeparam name="T13">字典参数 13 的类型。</typeparam>
    /// <typeparam name="T14">字典参数 14 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <param name="arg12">字典参数 12。</param>
    /// <param name="arg13">字典参数 13。</param>
    /// <param name="arg14">字典参数 14。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11,
      T12 arg12,
      T13 arg13,
      T14 arg14)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
      }
      catch (Exception ex)
      {
        string str = Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        return Utility.Text.Format<string, string, string, Exception>("<Error>{0},{1},{2},{3}", key, rawString, str, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <typeparam name="T12">字典参数 12 的类型。</typeparam>
    /// <typeparam name="T13">字典参数 13 的类型。</typeparam>
    /// <typeparam name="T14">字典参数 14 的类型。</typeparam>
    /// <typeparam name="T15">字典参数 15 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <param name="arg12">字典参数 12。</param>
    /// <param name="arg13">字典参数 13。</param>
    /// <param name="arg14">字典参数 14。</param>
    /// <param name="arg15">字典参数 15。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11,
      T12 arg12,
      T13 arg13,
      T14 arg14,
      T15 arg15)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
      }
      catch (Exception ex)
      {
        string str = Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        return Utility.Text.Format<string, string, string, Exception>("<Error>{0},{1},{2},{3}", key, rawString, str, ex);
      }
    }

    /// <summary>根据字典主键获取字典内容字符串。</summary>
    /// <typeparam name="T1">字典参数 1 的类型。</typeparam>
    /// <typeparam name="T2">字典参数 2 的类型。</typeparam>
    /// <typeparam name="T3">字典参数 3 的类型。</typeparam>
    /// <typeparam name="T4">字典参数 4 的类型。</typeparam>
    /// <typeparam name="T5">字典参数 5 的类型。</typeparam>
    /// <typeparam name="T6">字典参数 6 的类型。</typeparam>
    /// <typeparam name="T7">字典参数 7 的类型。</typeparam>
    /// <typeparam name="T8">字典参数 8 的类型。</typeparam>
    /// <typeparam name="T9">字典参数 9 的类型。</typeparam>
    /// <typeparam name="T10">字典参数 10 的类型。</typeparam>
    /// <typeparam name="T11">字典参数 11 的类型。</typeparam>
    /// <typeparam name="T12">字典参数 12 的类型。</typeparam>
    /// <typeparam name="T13">字典参数 13 的类型。</typeparam>
    /// <typeparam name="T14">字典参数 14 的类型。</typeparam>
    /// <typeparam name="T15">字典参数 15 的类型。</typeparam>
    /// <typeparam name="T16">字典参数 16 的类型。</typeparam>
    /// <param name="key">字典主键。</param>
    /// <param name="arg1">字典参数 1。</param>
    /// <param name="arg2">字典参数 2。</param>
    /// <param name="arg3">字典参数 3。</param>
    /// <param name="arg4">字典参数 4。</param>
    /// <param name="arg5">字典参数 5。</param>
    /// <param name="arg6">字典参数 6。</param>
    /// <param name="arg7">字典参数 7。</param>
    /// <param name="arg8">字典参数 8。</param>
    /// <param name="arg9">字典参数 9。</param>
    /// <param name="arg10">字典参数 10。</param>
    /// <param name="arg11">字典参数 11。</param>
    /// <param name="arg12">字典参数 12。</param>
    /// <param name="arg13">字典参数 13。</param>
    /// <param name="arg14">字典参数 14。</param>
    /// <param name="arg15">字典参数 15。</param>
    /// <param name="arg16">字典参数 16。</param>
    /// <returns>要获取的字典内容字符串。</returns>
    public string GetString<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string key,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8,
      T9 arg9,
      T10 arg10,
      T11 arg11,
      T12 arg12,
      T13 arg13,
      T14 arg14,
      T15 arg15,
      T16 arg16)
    {
      string rawString = this.GetRawString(key);
      if (rawString == null)
        return Utility.Text.Format<string>("<NoKey>{0}", key);
      try
      {
        return Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(rawString, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
      }
      catch (Exception ex)
      {
        string str = Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        return Utility.Text.Format<string, string, string, Exception>("<Error>{0},{1},{2},{3}", key, rawString, str, ex);
      }
    }

    /// <summary>是否存在字典。</summary>
    /// <param name="key">字典主键。</param>
    /// <returns>是否存在字典。</returns>
    public bool HasRawString(string key) => !string.IsNullOrEmpty(key) ? this.m_Dictionary.ContainsKey(key) : throw new GameFrameworkException("Key is invalid.");

    /// <summary>根据字典主键获取字典值。</summary>
    /// <param name="key">字典主键。</param>
    /// <returns>字典值。</returns>
    public string GetRawString(string key)
    {
      if (string.IsNullOrEmpty(key))
        throw new GameFrameworkException("Key is invalid.");
      string str = (string) null;
      return !this.m_Dictionary.TryGetValue(key, out str) ? (string) null : str;
    }

    /// <summary>增加字典。</summary>
    /// <param name="key">字典主键。</param>
    /// <param name="value">字典内容。</param>
    /// <returns>是否增加字典成功。</returns>
    public bool AddRawString(string key, string value)
    {
      if (string.IsNullOrEmpty(key))
        throw new GameFrameworkException("Key is invalid.");
      if (this.m_Dictionary.ContainsKey(key))
        return false;
      this.m_Dictionary.Add(key, value ?? string.Empty);
      return true;
    }

    /// <summary>移除字典。</summary>
    /// <param name="key">字典主键。</param>
    /// <returns>是否移除字典成功。</returns>
    public bool RemoveRawString(string key) => !string.IsNullOrEmpty(key) ? this.m_Dictionary.Remove(key) : throw new GameFrameworkException("Key is invalid.");

    /// <summary>清空所有字典。</summary>
    public void RemoveAllRawStrings() => this.m_Dictionary.Clear();
  }
}
