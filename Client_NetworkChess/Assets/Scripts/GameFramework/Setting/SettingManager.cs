﻿// Decompiled with JetBrains decompiler
// Type: GameFramework.Setting.SettingManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;

namespace GameFramework.Setting
{
  /// <summary>游戏配置管理器。</summary>
  internal sealed class SettingManager : GameFrameworkModule, ISettingManager
  {
    private ISettingHelper m_SettingHelper;

    /// <summary>初始化游戏配置管理器的新实例。</summary>
    public SettingManager() => this.m_SettingHelper = (ISettingHelper) null;

    /// <summary>获取游戏配置项数量。</summary>
    public int Count => this.m_SettingHelper != null ? this.m_SettingHelper.Count : throw new GameFrameworkException("Setting helper is invalid.");

    /// <summary>游戏配置管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理游戏配置管理器。</summary>
    internal override void Shutdown() => this.Save();

    /// <summary>设置游戏配置辅助器。</summary>
    /// <param name="settingHelper">游戏配置辅助器。</param>
    public void SetSettingHelper(ISettingHelper settingHelper) => this.m_SettingHelper = settingHelper != null ? settingHelper : throw new GameFrameworkException("Setting helper is invalid.");

    /// <summary>加载游戏配置。</summary>
    /// <returns>是否加载游戏配置成功。</returns>
    public bool Load() => this.m_SettingHelper != null ? this.m_SettingHelper.Load() : throw new GameFrameworkException("Setting helper is invalid.");

    /// <summary>保存游戏配置。</summary>
    /// <returns>是否保存游戏配置成功。</returns>
    public bool Save() => this.m_SettingHelper != null ? this.m_SettingHelper.Save() : throw new GameFrameworkException("Setting helper is invalid.");

    /// <summary>获取所有游戏配置项的名称。</summary>
    /// <returns>所有游戏配置项的名称。</returns>
    public string[] GetAllSettingNames() => this.m_SettingHelper != null ? this.m_SettingHelper.GetAllSettingNames() : throw new GameFrameworkException("Setting helper is invalid.");

    /// <summary>获取所有游戏配置项的名称。</summary>
    /// <param name="results">所有游戏配置项的名称。</param>
    public void GetAllSettingNames(List<string> results)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      this.m_SettingHelper.GetAllSettingNames(results);
    }

    /// <summary>检查是否存在指定游戏配置项。</summary>
    /// <param name="settingName">要检查游戏配置项的名称。</param>
    /// <returns>指定的游戏配置项是否存在。</returns>
    public bool HasSetting(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.HasSetting(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>移除指定游戏配置项。</summary>
    /// <param name="settingName">要移除游戏配置项的名称。</param>
    /// <returns>是否移除指定游戏配置项成功。</returns>
    public bool RemoveSetting(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.RemoveSetting(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>清空所有游戏配置项。</summary>
    public void RemoveAllSettings()
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      this.m_SettingHelper.RemoveAllSettings();
    }

    /// <summary>从指定游戏配置项中读取布尔值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的布尔值。</returns>
    public bool GetBool(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.GetBool(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>从指定游戏配置项中读取布尔值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultValue">当指定的游戏配置项不存在时，返回此默认值。</param>
    /// <returns>读取的布尔值。</returns>
    public bool GetBool(string settingName, bool defaultValue)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetBool(settingName, defaultValue);
    }

    /// <summary>向指定游戏配置项写入布尔值。</summary>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="value">要写入的布尔值。</param>
    public void SetBool(string settingName, bool value)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetBool(settingName, value);
    }

    /// <summary>从指定游戏配置项中读取整数值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的整数值。</returns>
    public int GetInt(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.GetInt(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>从指定游戏配置项中读取整数值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultValue">当指定的游戏配置项不存在时，返回此默认值。</param>
    /// <returns>读取的整数值。</returns>
    public int GetInt(string settingName, int defaultValue)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetInt(settingName, defaultValue);
    }

    /// <summary>向指定游戏配置项写入整数值。</summary>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="value">要写入的整数值。</param>
    public void SetInt(string settingName, int value)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetInt(settingName, value);
    }

    /// <summary>从指定游戏配置项中读取浮点数值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的浮点数值。</returns>
    public float GetFloat(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.GetFloat(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>从指定游戏配置项中读取浮点数值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultValue">当指定的游戏配置项不存在时，返回此默认值。</param>
    /// <returns>读取的浮点数值。</returns>
    public float GetFloat(string settingName, float defaultValue)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetFloat(settingName, defaultValue);
    }

    /// <summary>向指定游戏配置项写入浮点数值。</summary>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="value">要写入的浮点数值。</param>
    public void SetFloat(string settingName, float value)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetFloat(settingName, value);
    }

    /// <summary>从指定游戏配置项中读取字符串值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的字符串值。</returns>
    public string GetString(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.GetString(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>从指定游戏配置项中读取字符串值。</summary>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultValue">当指定的游戏配置项不存在时，返回此默认值。</param>
    /// <returns>读取的字符串值。</returns>
    public string GetString(string settingName, string defaultValue)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetString(settingName, defaultValue);
    }

    /// <summary>向指定游戏配置项写入字符串值。</summary>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="value">要写入的字符串值。</param>
    public void SetString(string settingName, string value)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetString(settingName, value);
    }

    /// <summary>从指定游戏配置项中读取对象。</summary>
    /// <typeparam name="T">要读取对象的类型。</typeparam>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的对象。</returns>
    public T GetObject<T>(string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      return !string.IsNullOrEmpty(settingName) ? this.m_SettingHelper.GetObject<T>(settingName) : throw new GameFrameworkException("Setting name is invalid.");
    }

    /// <summary>从指定游戏配置项中读取对象。</summary>
    /// <param name="objectType">要读取对象的类型。</param>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <returns>读取的对象。</returns>
    public object GetObject(Type objectType, string settingName)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetObject(objectType, settingName);
    }

    /// <summary>从指定游戏配置项中读取对象。</summary>
    /// <typeparam name="T">要读取对象的类型。</typeparam>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultObj">当指定的游戏配置项不存在时，返回此默认对象。</param>
    /// <returns>读取的对象。</returns>
    public T GetObject<T>(string settingName, T defaultObj)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetObject<T>(settingName, defaultObj);
    }

    /// <summary>从指定游戏配置项中读取对象。</summary>
    /// <param name="objectType">要读取对象的类型。</param>
    /// <param name="settingName">要获取游戏配置项的名称。</param>
    /// <param name="defaultObj">当指定的游戏配置项不存在时，返回此默认对象。</param>
    /// <returns>读取的对象。</returns>
    public object GetObject(Type objectType, string settingName, object defaultObj)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (objectType == null)
        throw new GameFrameworkException("Object type is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      return this.m_SettingHelper.GetObject(objectType, settingName, defaultObj);
    }

    /// <summary>向指定游戏配置项写入对象。</summary>
    /// <typeparam name="T">要写入对象的类型。</typeparam>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="obj">要写入的对象。</param>
    public void SetObject<T>(string settingName, T obj)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetObject<T>(settingName, obj);
    }

    /// <summary>向指定游戏配置项写入对象。</summary>
    /// <param name="settingName">要写入游戏配置项的名称。</param>
    /// <param name="obj">要写入的对象。</param>
    public void SetObject(string settingName, object obj)
    {
      if (this.m_SettingHelper == null)
        throw new GameFrameworkException("Setting helper is invalid.");
      if (string.IsNullOrEmpty(settingName))
        throw new GameFrameworkException("Setting name is invalid.");
      this.m_SettingHelper.SetObject(settingName, obj);
    }
  }
}
