// Decompiled with JetBrains decompiler
// Type: GameFramework.Config.ConfigManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameFramework.Config
{
  /// <summary>全局配置管理器。</summary>
  internal sealed class ConfigManager : 
    GameFrameworkModule,
    IConfigManager,
    IDataProvider<IConfigManager>
  {
    private readonly Dictionary<string, ConfigManager.ConfigData> m_ConfigDatas;
    private readonly DataProvider<IConfigManager> m_DataProvider;
    private IConfigHelper m_ConfigHelper;

    /// <summary>初始化全局配置管理器的新实例。</summary>
    public ConfigManager()
    {
      this.m_ConfigDatas = new Dictionary<string, ConfigManager.ConfigData>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_DataProvider = new DataProvider<IConfigManager>((IConfigManager) this);
      this.m_ConfigHelper = (IConfigHelper) null;
    }

    /// <summary>获取全局配置项数量。</summary>
    public int Count => this.m_ConfigDatas.Count;

    /// <summary>获取缓冲二进制流的大小。</summary>
    public int CachedBytesSize => DataProvider<IConfigManager>.CachedBytesSize;

    /// <summary>读取全局配置成功事件。</summary>
    public event EventHandler<ReadDataSuccessEventArgs> ReadDataSuccess
    {
      add => this.m_DataProvider.ReadDataSuccess += value;
      remove => this.m_DataProvider.ReadDataSuccess -= value;
    }

    /// <summary>读取全局配置失败事件。</summary>
    public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure
    {
      add => this.m_DataProvider.ReadDataFailure += value;
      remove => this.m_DataProvider.ReadDataFailure -= value;
    }

    /// <summary>读取全局配置更新事件。</summary>
    public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate
    {
      add => this.m_DataProvider.ReadDataUpdate += value;
      remove => this.m_DataProvider.ReadDataUpdate -= value;
    }

    /// <summary>读取全局配置时加载依赖资源事件。</summary>
    public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependencyAsset
    {
      add => this.m_DataProvider.ReadDataDependencyAsset += value;
      remove => this.m_DataProvider.ReadDataDependencyAsset -= value;
    }

    /// <summary>全局配置管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理全局配置管理器。</summary>
    internal override void Shutdown()
    {
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_DataProvider.SetResourceManager(resourceManager);

    /// <summary>设置全局配置数据提供者辅助器。</summary>
    /// <param name="dataProviderHelper">全局配置数据提供者辅助器。</param>
    public void SetDataProviderHelper(
      IDataProviderHelper<IConfigManager> dataProviderHelper)
    {
      this.m_DataProvider.SetDataProviderHelper(dataProviderHelper);
    }

    /// <summary>设置全局配置辅助器。</summary>
    /// <param name="configHelper">全局配置辅助器。</param>
    public void SetConfigHelper(IConfigHelper configHelper) => this.m_ConfigHelper = configHelper != null ? configHelper : throw new GameFrameworkException("Config helper is invalid.");

    /// <summary>确保二进制流缓存分配足够大小的内存并缓存。</summary>
    /// <param name="ensureSize">要确保二进制流缓存分配内存的大小。</param>
    public void EnsureCachedBytesSize(int ensureSize) => DataProvider<IConfigManager>.EnsureCachedBytesSize(ensureSize);

    /// <summary>释放缓存的二进制流。</summary>
    public void FreeCachedBytes() => DataProvider<IConfigManager>.FreeCachedBytes();

    /// <summary>读取全局配置。</summary>
    /// <param name="configAssetName">全局配置资源名称。</param>
    public void ReadData(string configAssetName) => this.m_DataProvider.ReadData(configAssetName);

    /// <summary>读取全局配置。</summary>
    /// <param name="configAssetName">全局配置资源名称。</param>
    /// <param name="priority">加载全局配置资源的优先级。</param>
    public void ReadData(string configAssetName, int priority) => this.m_DataProvider.ReadData(configAssetName, priority);

    /// <summary>读取全局配置。</summary>
    /// <param name="configAssetName">全局配置资源名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string configAssetName, object userData) => this.m_DataProvider.ReadData(configAssetName, userData);

    /// <summary>读取全局配置。</summary>
    /// <param name="configAssetName">全局配置资源名称。</param>
    /// <param name="priority">加载全局配置资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    public void ReadData(string configAssetName, int priority, object userData) => this.m_DataProvider.ReadData(configAssetName, priority, userData);

    /// <summary>解析全局配置。</summary>
    /// <param name="configString">要解析的全局配置字符串。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(string configString) => this.m_DataProvider.ParseData(configString);

    /// <summary>解析全局配置。</summary>
    /// <param name="configString">要解析的全局配置字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(string configString, object userData) => this.m_DataProvider.ParseData(configString, userData);

    /// <summary>解析全局配置。</summary>
    /// <param name="configBytes">要解析的全局配置二进制流。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(byte[] configBytes) => this.m_DataProvider.ParseData(configBytes);

    /// <summary>解析全局配置。</summary>
    /// <param name="configBytes">要解析的全局配置二进制流。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(byte[] configBytes, object userData) => this.m_DataProvider.ParseData(configBytes, userData);

    /// <summary>解析全局配置。</summary>
    /// <param name="configBytes">要解析的全局配置二进制流。</param>
    /// <param name="startIndex">全局配置二进制流的起始位置。</param>
    /// <param name="length">全局配置二进制流的长度。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(byte[] configBytes, int startIndex, int length) => this.m_DataProvider.ParseData(configBytes, startIndex, length);

    /// <summary>解析全局配置。</summary>
    /// <param name="configBytes">要解析的全局配置二进制流。</param>
    /// <param name="startIndex">全局配置二进制流的起始位置。</param>
    /// <param name="length">全局配置二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析全局配置成功。</returns>
    public bool ParseData(byte[] configBytes, int startIndex, int length, object userData) => this.m_DataProvider.ParseData(configBytes, startIndex, length, userData);

    /// <summary>检查是否存在指定全局配置项。</summary>
    /// <param name="configName">要检查全局配置项的名称。</param>
    /// <returns>指定的全局配置项是否存在。</returns>
    public bool HasConfig(string configName) => this.GetConfigData(configName).HasValue;

    /// <summary>从指定全局配置项中读取布尔值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <returns>读取的布尔值。</returns>
    public bool GetBool(string configName) => (this.GetConfigData(configName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Config name '{0}' is not exist.", configName))).Value.BoolValue;

    /// <summary>从指定全局配置项中读取布尔值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
    /// <returns>读取的布尔值。</returns>
    public bool GetBool(string configName, bool defaultValue)
    {
      ConfigManager.ConfigData? configData = this.GetConfigData(configName);
      return !configData.HasValue ? defaultValue : configData.Value.BoolValue;
    }

    /// <summary>从指定全局配置项中读取整数值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <returns>读取的整数值。</returns>
    public int GetInt(string configName) => (this.GetConfigData(configName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Config name '{0}' is not exist.", configName))).Value.IntValue;

    /// <summary>从指定全局配置项中读取整数值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
    /// <returns>读取的整数值。</returns>
    public int GetInt(string configName, int defaultValue)
    {
      ConfigManager.ConfigData? configData = this.GetConfigData(configName);
      return !configData.HasValue ? defaultValue : configData.Value.IntValue;
    }

    /// <summary>从指定全局配置项中读取浮点数值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <returns>读取的浮点数值。</returns>
    public float GetFloat(string configName) => (this.GetConfigData(configName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Config name '{0}' is not exist.", configName))).Value.FloatValue;

    /// <summary>从指定全局配置项中读取浮点数值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
    /// <returns>读取的浮点数值。</returns>
    public float GetFloat(string configName, float defaultValue)
    {
      ConfigManager.ConfigData? configData = this.GetConfigData(configName);
      return !configData.HasValue ? defaultValue : configData.Value.FloatValue;
    }

    /// <summary>从指定全局配置项中读取字符串值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <returns>读取的字符串值。</returns>
    public string GetString(string configName) => (this.GetConfigData(configName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Config name '{0}' is not exist.", configName))).Value.StringValue;

    /// <summary>从指定全局配置项中读取字符串值。</summary>
    /// <param name="configName">要获取全局配置项的名称。</param>
    /// <param name="defaultValue">当指定的全局配置项不存在时，返回此默认值。</param>
    /// <returns>读取的字符串值。</returns>
    public string GetString(string configName, string defaultValue)
    {
      ConfigManager.ConfigData? configData = this.GetConfigData(configName);
      return !configData.HasValue ? defaultValue : configData.Value.StringValue;
    }

    /// <summary>增加指定全局配置项。</summary>
    /// <param name="configName">要增加全局配置项的名称。</param>
    /// <param name="configValue">全局配置项的值。</param>
    /// <returns>是否增加全局配置项成功。</returns>
    public bool AddConfig(string configName, string configValue)
    {
      bool result1 = false;
      bool.TryParse(configValue, out result1);
      int result2 = 0;
      int.TryParse(configValue, out result2);
      float result3 = 0.0f;
      float.TryParse(configValue, out result3);
      return this.AddConfig(configName, result1, result2, result3, configValue);
    }

    /// <summary>增加指定全局配置项。</summary>
    /// <param name="configName">要增加全局配置项的名称。</param>
    /// <param name="boolValue">全局配置项布尔值。</param>
    /// <param name="intValue">全局配置项整数值。</param>
    /// <param name="floatValue">全局配置项浮点数值。</param>
    /// <param name="stringValue">全局配置项字符串值。</param>
    /// <returns>是否增加全局配置项成功。</returns>
    public bool AddConfig(
      string configName,
      bool boolValue,
      int intValue,
      float floatValue,
      string stringValue)
    {
      if (this.HasConfig(configName))
        return false;
      this.m_ConfigDatas.Add(configName, new ConfigManager.ConfigData(boolValue, intValue, floatValue, stringValue));
      return true;
    }

    /// <summary>移除指定全局配置项。</summary>
    /// <param name="configName">要移除全局配置项的名称。</param>
    public bool RemoveConfig(string configName) => this.HasConfig(configName) && this.m_ConfigDatas.Remove(configName);

    /// <summary>清空所有全局配置项。</summary>
    public void RemoveAllConfigs() => this.m_ConfigDatas.Clear();

    private ConfigManager.ConfigData? GetConfigData(string configName)
    {
      if (string.IsNullOrEmpty(configName))
        throw new GameFrameworkException("Config name is invalid.");
      ConfigManager.ConfigData configData = new ConfigManager.ConfigData();
      return this.m_ConfigDatas.TryGetValue(configName, out configData) ? new ConfigManager.ConfigData?(configData) : new ConfigManager.ConfigData?();
    }

    [StructLayout(LayoutKind.Auto)]
    private struct ConfigData
    {
      private readonly bool m_BoolValue;
      private readonly int m_IntValue;
      private readonly float m_FloatValue;
      private readonly string m_StringValue;

      public ConfigData(bool boolValue, int intValue, float floatValue, string stringValue)
      {
        this.m_BoolValue = boolValue;
        this.m_IntValue = intValue;
        this.m_FloatValue = floatValue;
        this.m_StringValue = stringValue;
      }

      public bool BoolValue => this.m_BoolValue;

      public int IntValue => this.m_IntValue;

      public float FloatValue => this.m_FloatValue;

      public string StringValue => this.m_StringValue;
    }
  }
}
