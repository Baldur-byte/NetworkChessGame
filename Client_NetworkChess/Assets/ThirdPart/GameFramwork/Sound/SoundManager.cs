// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.SoundManager
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using GameFramework.Resource;
using System;
using System.Collections.Generic;

namespace GameFramework.Sound
{
  /// <summary>声音管理器。</summary>
  internal sealed class SoundManager : GameFrameworkModule, ISoundManager
  {
    private readonly Dictionary<string, SoundManager.SoundGroup> m_SoundGroups;
    private readonly List<int> m_SoundsBeingLoaded;
    private readonly HashSet<int> m_SoundsToReleaseOnLoad;
    private readonly LoadAssetCallbacks m_LoadAssetCallbacks;
    private IResourceManager m_ResourceManager;
    private ISoundHelper m_SoundHelper;
    private int m_Serial;
    private EventHandler<PlaySoundSuccessEventArgs> m_PlaySoundSuccessEventHandler;
    private EventHandler<PlaySoundFailureEventArgs> m_PlaySoundFailureEventHandler;
    private EventHandler<PlaySoundUpdateEventArgs> m_PlaySoundUpdateEventHandler;
    private EventHandler<PlaySoundDependencyAssetEventArgs> m_PlaySoundDependencyAssetEventHandler;

    /// <summary>初始化声音管理器的新实例。</summary>
    public SoundManager()
    {
      this.m_SoundGroups = new Dictionary<string, SoundManager.SoundGroup>((IEqualityComparer<string>) StringComparer.Ordinal);
      this.m_SoundsBeingLoaded = new List<int>();
      this.m_SoundsToReleaseOnLoad = new HashSet<int>();
      this.m_LoadAssetCallbacks = new LoadAssetCallbacks(new GameFramework.Resource.LoadAssetSuccessCallback(this.LoadAssetSuccessCallback), new GameFramework.Resource.LoadAssetFailureCallback(this.LoadAssetFailureCallback), new GameFramework.Resource.LoadAssetUpdateCallback(this.LoadAssetUpdateCallback), new GameFramework.Resource.LoadAssetDependencyAssetCallback(this.LoadAssetDependencyAssetCallback));
      this.m_ResourceManager = (IResourceManager) null;
      this.m_SoundHelper = (ISoundHelper) null;
      this.m_Serial = 0;
      this.m_PlaySoundSuccessEventHandler = (EventHandler<PlaySoundSuccessEventArgs>) null;
      this.m_PlaySoundFailureEventHandler = (EventHandler<PlaySoundFailureEventArgs>) null;
      this.m_PlaySoundUpdateEventHandler = (EventHandler<PlaySoundUpdateEventArgs>) null;
      this.m_PlaySoundDependencyAssetEventHandler = (EventHandler<PlaySoundDependencyAssetEventArgs>) null;
    }

    /// <summary>获取声音组数量。</summary>
    public int SoundGroupCount => this.m_SoundGroups.Count;

    /// <summary>播放声音成功事件。</summary>
    public event EventHandler<PlaySoundSuccessEventArgs> PlaySoundSuccess
    {
      add => this.m_PlaySoundSuccessEventHandler += value;
      remove => this.m_PlaySoundSuccessEventHandler -= value;
    }

    /// <summary>播放声音失败事件。</summary>
    public event EventHandler<PlaySoundFailureEventArgs> PlaySoundFailure
    {
      add => this.m_PlaySoundFailureEventHandler += value;
      remove => this.m_PlaySoundFailureEventHandler -= value;
    }

    /// <summary>播放声音更新事件。</summary>
    public event EventHandler<PlaySoundUpdateEventArgs> PlaySoundUpdate
    {
      add => this.m_PlaySoundUpdateEventHandler += value;
      remove => this.m_PlaySoundUpdateEventHandler -= value;
    }

    /// <summary>播放声音时加载依赖资源事件。</summary>
    public event EventHandler<PlaySoundDependencyAssetEventArgs> PlaySoundDependencyAsset
    {
      add => this.m_PlaySoundDependencyAssetEventHandler += value;
      remove => this.m_PlaySoundDependencyAssetEventHandler -= value;
    }

    /// <summary>声音管理器轮询。</summary>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    internal override void Update(float elapseSeconds, float realElapseSeconds)
    {
    }

    /// <summary>关闭并清理声音管理器。</summary>
    internal override void Shutdown()
    {
      this.StopAllLoadedSounds();
      this.m_SoundGroups.Clear();
      this.m_SoundsBeingLoaded.Clear();
      this.m_SoundsToReleaseOnLoad.Clear();
    }

    /// <summary>设置资源管理器。</summary>
    /// <param name="resourceManager">资源管理器。</param>
    public void SetResourceManager(IResourceManager resourceManager) => this.m_ResourceManager = resourceManager != null ? resourceManager : throw new GameFrameworkException("Resource manager is invalid.");

    /// <summary>设置声音辅助器。</summary>
    /// <param name="soundHelper">声音辅助器。</param>
    public void SetSoundHelper(ISoundHelper soundHelper) => this.m_SoundHelper = soundHelper != null ? soundHelper : throw new GameFrameworkException("Sound helper is invalid.");

    /// <summary>是否存在指定声音组。</summary>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <returns>指定声音组是否存在。</returns>
    public bool HasSoundGroup(string soundGroupName) => !string.IsNullOrEmpty(soundGroupName) ? this.m_SoundGroups.ContainsKey(soundGroupName) : throw new GameFrameworkException("Sound group name is invalid.");

    /// <summary>获取指定声音组。</summary>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <returns>要获取的声音组。</returns>
    public ISoundGroup GetSoundGroup(string soundGroupName)
    {
      if (string.IsNullOrEmpty(soundGroupName))
        throw new GameFrameworkException("Sound group name is invalid.");
      SoundManager.SoundGroup soundGroup = (SoundManager.SoundGroup) null;
      return this.m_SoundGroups.TryGetValue(soundGroupName, out soundGroup) ? (ISoundGroup) soundGroup : (ISoundGroup) null;
    }

    /// <summary>获取所有声音组。</summary>
    /// <returns>所有声音组。</returns>
    public ISoundGroup[] GetAllSoundGroups()
    {
      int num = 0;
      ISoundGroup[] allSoundGroups = new ISoundGroup[this.m_SoundGroups.Count];
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
        allSoundGroups[num++] = (ISoundGroup) soundGroup.Value;
      return allSoundGroups;
    }

    /// <summary>获取所有声音组。</summary>
    /// <param name="results">所有声音组。</param>
    public void GetAllSoundGroups(List<ISoundGroup> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
        results.Add((ISoundGroup) soundGroup.Value);
    }

    /// <summary>增加声音组。</summary>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="soundGroupHelper">声音组辅助器。</param>
    /// <returns>是否增加声音组成功。</returns>
    public bool AddSoundGroup(string soundGroupName, ISoundGroupHelper soundGroupHelper) => this.AddSoundGroup(soundGroupName, false, false, 1f, soundGroupHelper);

    /// <summary>增加声音组。</summary>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="soundGroupAvoidBeingReplacedBySamePriority">声音组中的声音是否避免被同优先级声音替换。</param>
    /// <param name="soundGroupMute">声音组是否静音。</param>
    /// <param name="soundGroupVolume">声音组音量。</param>
    /// <param name="soundGroupHelper">声音组辅助器。</param>
    /// <returns>是否增加声音组成功。</returns>
    public bool AddSoundGroup(
      string soundGroupName,
      bool soundGroupAvoidBeingReplacedBySamePriority,
      bool soundGroupMute,
      float soundGroupVolume,
      ISoundGroupHelper soundGroupHelper)
    {
      if (string.IsNullOrEmpty(soundGroupName))
        throw new GameFrameworkException("Sound group name is invalid.");
      if (soundGroupHelper == null)
        throw new GameFrameworkException("Sound group helper is invalid.");
      if (this.HasSoundGroup(soundGroupName))
        return false;
      SoundManager.SoundGroup soundGroup = new SoundManager.SoundGroup(soundGroupName, soundGroupHelper)
      {
        AvoidBeingReplacedBySamePriority = soundGroupAvoidBeingReplacedBySamePriority,
        Mute = soundGroupMute,
        Volume = soundGroupVolume
      };
      this.m_SoundGroups.Add(soundGroupName, soundGroup);
      return true;
    }

    /// <summary>增加声音代理辅助器。</summary>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="soundAgentHelper">要增加的声音代理辅助器。</param>
    public void AddSoundAgentHelper(string soundGroupName, ISoundAgentHelper soundAgentHelper)
    {
      if (this.m_SoundHelper == null)
        throw new GameFrameworkException("You must set sound helper first.");
      ((SoundManager.SoundGroup) this.GetSoundGroup(soundGroupName) ?? throw new GameFrameworkException(Utility.Text.Format<string>("Sound group '{0}' is not exist.", soundGroupName))).AddSoundAgentHelper(this.m_SoundHelper, soundAgentHelper);
    }

    /// <summary>获取所有正在加载声音的序列编号。</summary>
    /// <returns>所有正在加载声音的序列编号。</returns>
    public int[] GetAllLoadingSoundSerialIds() => this.m_SoundsBeingLoaded.ToArray();

    /// <summary>获取所有正在加载声音的序列编号。</summary>
    /// <param name="results">所有正在加载声音的序列编号。</param>
    public void GetAllLoadingSoundSerialIds(List<int> results)
    {
      if (results == null)
        throw new GameFrameworkException("Results is invalid.");
      results.Clear();
      results.AddRange((IEnumerable<int>) this.m_SoundsBeingLoaded);
    }

    /// <summary>是否正在加载声音。</summary>
    /// <param name="serialId">声音序列编号。</param>
    /// <returns>是否正在加载声音。</returns>
    public bool IsLoadingSound(int serialId) => this.m_SoundsBeingLoaded.Contains(serialId);

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(string soundAssetName, string soundGroupName) => this.PlaySound(soundAssetName, soundGroupName, 0, (PlaySoundParams) null, (object) null);

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="priority">加载声音资源的优先级。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(string soundAssetName, string soundGroupName, int priority) => this.PlaySound(soundAssetName, soundGroupName, priority, (PlaySoundParams) null, (object) null);

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(
      string soundAssetName,
      string soundGroupName,
      PlaySoundParams playSoundParams)
    {
      return this.PlaySound(soundAssetName, soundGroupName, 0, playSoundParams, (object) null);
    }

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(string soundAssetName, string soundGroupName, object userData) => this.PlaySound(soundAssetName, soundGroupName, 0, (PlaySoundParams) null, userData);

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="priority">加载声音资源的优先级。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(
      string soundAssetName,
      string soundGroupName,
      int priority,
      PlaySoundParams playSoundParams)
    {
      return this.PlaySound(soundAssetName, soundGroupName, priority, playSoundParams, (object) null);
    }

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="priority">加载声音资源的优先级。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(
      string soundAssetName,
      string soundGroupName,
      int priority,
      object userData)
    {
      return this.PlaySound(soundAssetName, soundGroupName, priority, (PlaySoundParams) null, userData);
    }

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(
      string soundAssetName,
      string soundGroupName,
      PlaySoundParams playSoundParams,
      object userData)
    {
      return this.PlaySound(soundAssetName, soundGroupName, 0, playSoundParams, userData);
    }

    /// <summary>播放声音。</summary>
    /// <param name="soundAssetName">声音资源名称。</param>
    /// <param name="soundGroupName">声音组名称。</param>
    /// <param name="priority">加载声音资源的优先级。</param>
    /// <param name="playSoundParams">播放声音参数。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>声音的序列编号。</returns>
    public int PlaySound(
      string soundAssetName,
      string soundGroupName,
      int priority,
      PlaySoundParams playSoundParams,
      object userData)
    {
      if (this.m_ResourceManager == null)
        throw new GameFrameworkException("You must set resource manager first.");
      if (this.m_SoundHelper == null)
        throw new GameFrameworkException("You must set sound helper first.");
      if (playSoundParams == null)
        playSoundParams = PlaySoundParams.Create();
      int serialId = ++this.m_Serial;
      PlaySoundErrorCode? nullable = new PlaySoundErrorCode?();
      string str = (string) null;
      SoundManager.SoundGroup soundGroup = (SoundManager.SoundGroup) this.GetSoundGroup(soundGroupName);
      if (soundGroup == null)
      {
        nullable = new PlaySoundErrorCode?(PlaySoundErrorCode.SoundGroupNotExist);
        str = Utility.Text.Format<string>("Sound group '{0}' is not exist.", soundGroupName);
      }
      else if (soundGroup.SoundAgentCount <= 0)
      {
        nullable = new PlaySoundErrorCode?(PlaySoundErrorCode.SoundGroupHasNoAgent);
        str = Utility.Text.Format<string>("Sound group '{0}' is have no sound agent.", soundGroupName);
      }
      if (nullable.HasValue)
      {
        if (this.m_PlaySoundFailureEventHandler == null)
          throw new GameFrameworkException(str);
        PlaySoundFailureEventArgs e = PlaySoundFailureEventArgs.Create(serialId, soundAssetName, soundGroupName, playSoundParams, nullable.Value, str, userData);
        this.m_PlaySoundFailureEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
        if (playSoundParams.Referenced)
          ReferencePool.Release((IReference) playSoundParams);
        return serialId;
      }
      this.m_SoundsBeingLoaded.Add(serialId);
      this.m_ResourceManager.LoadAsset(soundAssetName, priority, this.m_LoadAssetCallbacks, (object) SoundManager.PlaySoundInfo.Create(serialId, soundGroup, playSoundParams, userData));
      return serialId;
    }

    /// <summary>停止播放声音。</summary>
    /// <param name="serialId">要停止播放声音的序列编号。</param>
    /// <returns>是否停止播放声音成功。</returns>
    public bool StopSound(int serialId) => this.StopSound(serialId, 0.0f);

    /// <summary>停止播放声音。</summary>
    /// <param name="serialId">要停止播放声音的序列编号。</param>
    /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
    /// <returns>是否停止播放声音成功。</returns>
    public bool StopSound(int serialId, float fadeOutSeconds)
    {
      if (this.IsLoadingSound(serialId))
      {
        this.m_SoundsToReleaseOnLoad.Add(serialId);
        this.m_SoundsBeingLoaded.Remove(serialId);
        return true;
      }
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
      {
        if (soundGroup.Value.StopSound(serialId, fadeOutSeconds))
          return true;
      }
      return false;
    }

    /// <summary>停止所有已加载的声音。</summary>
    public void StopAllLoadedSounds() => this.StopAllLoadedSounds(0.0f);

    /// <summary>停止所有已加载的声音。</summary>
    /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
    public void StopAllLoadedSounds(float fadeOutSeconds)
    {
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
        soundGroup.Value.StopAllLoadedSounds(fadeOutSeconds);
    }

    /// <summary>停止所有正在加载的声音。</summary>
    public void StopAllLoadingSounds()
    {
      foreach (int num in this.m_SoundsBeingLoaded)
        this.m_SoundsToReleaseOnLoad.Add(num);
    }

    /// <summary>暂停播放声音。</summary>
    /// <param name="serialId">要暂停播放声音的序列编号。</param>
    public void PauseSound(int serialId) => this.PauseSound(serialId, 0.0f);

    /// <summary>暂停播放声音。</summary>
    /// <param name="serialId">要暂停播放声音的序列编号。</param>
    /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
    public void PauseSound(int serialId, float fadeOutSeconds)
    {
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
      {
        if (soundGroup.Value.PauseSound(serialId, fadeOutSeconds))
          return;
      }
      throw new GameFrameworkException(Utility.Text.Format<int>("Can not find sound '{0}'.", serialId));
    }

    /// <summary>恢复播放声音。</summary>
    /// <param name="serialId">要恢复播放声音的序列编号。</param>
    public void ResumeSound(int serialId) => this.ResumeSound(serialId, 0.0f);

    /// <summary>恢复播放声音。</summary>
    /// <param name="serialId">要恢复播放声音的序列编号。</param>
    /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
    public void ResumeSound(int serialId, float fadeInSeconds)
    {
      foreach (KeyValuePair<string, SoundManager.SoundGroup> soundGroup in this.m_SoundGroups)
      {
        if (soundGroup.Value.ResumeSound(serialId, fadeInSeconds))
          return;
      }
      throw new GameFrameworkException(Utility.Text.Format<int>("Can not find sound '{0}'.", serialId));
    }

    private void LoadAssetSuccessCallback(
      string soundAssetName,
      object soundAsset,
      float duration,
      object userData)
    {
      SoundManager.PlaySoundInfo playSoundInfo = (SoundManager.PlaySoundInfo) userData;
      if (playSoundInfo == null)
        throw new GameFrameworkException("Play sound info is invalid.");
      if (this.m_SoundsToReleaseOnLoad.Contains(playSoundInfo.SerialId))
      {
        this.m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
        if (playSoundInfo.PlaySoundParams.Referenced)
          ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
        ReferencePool.Release((IReference) playSoundInfo);
        this.m_SoundHelper.ReleaseSoundAsset(soundAsset);
      }
      else
      {
        this.m_SoundsBeingLoaded.Remove(playSoundInfo.SerialId);
        PlaySoundErrorCode? errorCode = new PlaySoundErrorCode?();
        ISoundAgent soundAgent = playSoundInfo.SoundGroup.PlaySound(playSoundInfo.SerialId, soundAsset, playSoundInfo.PlaySoundParams, out errorCode);
        if (soundAgent != null)
        {
          if (this.m_PlaySoundSuccessEventHandler != null)
          {
            PlaySoundSuccessEventArgs e = PlaySoundSuccessEventArgs.Create(playSoundInfo.SerialId, soundAssetName, soundAgent, duration, playSoundInfo.UserData);
            this.m_PlaySoundSuccessEventHandler((object) this, e);
            ReferencePool.Release((IReference) e);
          }
          if (playSoundInfo.PlaySoundParams.Referenced)
            ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
          ReferencePool.Release((IReference) playSoundInfo);
        }
        else
        {
          this.m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
          this.m_SoundHelper.ReleaseSoundAsset(soundAsset);
          string str = Utility.Text.Format<string, string>("Sound group '{0}' play sound '{1}' failure.", playSoundInfo.SoundGroup.Name, soundAssetName);
          if (this.m_PlaySoundFailureEventHandler != null)
          {
            PlaySoundFailureEventArgs e = PlaySoundFailureEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name, playSoundInfo.PlaySoundParams, errorCode.Value, str, playSoundInfo.UserData);
            this.m_PlaySoundFailureEventHandler((object) this, e);
            ReferencePool.Release((IReference) e);
            if (playSoundInfo.PlaySoundParams.Referenced)
              ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
            ReferencePool.Release((IReference) playSoundInfo);
          }
          else
          {
            if (playSoundInfo.PlaySoundParams.Referenced)
              ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
            ReferencePool.Release((IReference) playSoundInfo);
            throw new GameFrameworkException(str);
          }
        }
      }
    }

    private void LoadAssetFailureCallback(
      string soundAssetName,
      LoadResourceStatus status,
      string errorMessage,
      object userData)
    {
      SoundManager.PlaySoundInfo playSoundInfo = (SoundManager.PlaySoundInfo) userData;
      if (playSoundInfo == null)
        throw new GameFrameworkException("Play sound info is invalid.");
      if (this.m_SoundsToReleaseOnLoad.Contains(playSoundInfo.SerialId))
      {
        this.m_SoundsToReleaseOnLoad.Remove(playSoundInfo.SerialId);
        if (!playSoundInfo.PlaySoundParams.Referenced)
          return;
        ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
      }
      else
      {
        this.m_SoundsBeingLoaded.Remove(playSoundInfo.SerialId);
        string str = Utility.Text.Format<string, LoadResourceStatus, string>("Load sound failure, asset name '{0}', status '{1}', error message '{2}'.", soundAssetName, status, errorMessage);
        if (this.m_PlaySoundFailureEventHandler == null)
          throw new GameFrameworkException(str);
        PlaySoundFailureEventArgs e = PlaySoundFailureEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name, playSoundInfo.PlaySoundParams, PlaySoundErrorCode.LoadAssetFailure, str, playSoundInfo.UserData);
        this.m_PlaySoundFailureEventHandler((object) this, e);
        ReferencePool.Release((IReference) e);
        if (!playSoundInfo.PlaySoundParams.Referenced)
          return;
        ReferencePool.Release((IReference) playSoundInfo.PlaySoundParams);
      }
    }

    private void LoadAssetUpdateCallback(string soundAssetName, float progress, object userData)
    {
      SoundManager.PlaySoundInfo playSoundInfo = (SoundManager.PlaySoundInfo) userData;
      if (playSoundInfo == null)
        throw new GameFrameworkException("Play sound info is invalid.");
      if (this.m_PlaySoundUpdateEventHandler == null)
        return;
      PlaySoundUpdateEventArgs e = PlaySoundUpdateEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name, playSoundInfo.PlaySoundParams, progress, playSoundInfo.UserData);
      this.m_PlaySoundUpdateEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private void LoadAssetDependencyAssetCallback(
      string soundAssetName,
      string dependencyAssetName,
      int loadedCount,
      int totalCount,
      object userData)
    {
      SoundManager.PlaySoundInfo playSoundInfo = (SoundManager.PlaySoundInfo) userData;
      if (playSoundInfo == null)
        throw new GameFrameworkException("Play sound info is invalid.");
      if (this.m_PlaySoundDependencyAssetEventHandler == null)
        return;
      PlaySoundDependencyAssetEventArgs e = PlaySoundDependencyAssetEventArgs.Create(playSoundInfo.SerialId, soundAssetName, playSoundInfo.SoundGroup.Name, playSoundInfo.PlaySoundParams, dependencyAssetName, loadedCount, totalCount, playSoundInfo.UserData);
      this.m_PlaySoundDependencyAssetEventHandler((object) this, e);
      ReferencePool.Release((IReference) e);
    }

    private sealed class PlaySoundInfo : IReference
    {
      private int m_SerialId;
      private SoundManager.SoundGroup m_SoundGroup;
      private PlaySoundParams m_PlaySoundParams;
      private object m_UserData;

      public PlaySoundInfo()
      {
        this.m_SerialId = 0;
        this.m_SoundGroup = (SoundManager.SoundGroup) null;
        this.m_PlaySoundParams = (PlaySoundParams) null;
        this.m_UserData = (object) null;
      }

      public int SerialId => this.m_SerialId;

      public SoundManager.SoundGroup SoundGroup => this.m_SoundGroup;

      public PlaySoundParams PlaySoundParams => this.m_PlaySoundParams;

      public object UserData => this.m_UserData;

      public static SoundManager.PlaySoundInfo Create(
        int serialId,
        SoundManager.SoundGroup soundGroup,
        PlaySoundParams playSoundParams,
        object userData)
      {
        SoundManager.PlaySoundInfo playSoundInfo = ReferencePool.Acquire<SoundManager.PlaySoundInfo>();
        playSoundInfo.m_SerialId = serialId;
        playSoundInfo.m_SoundGroup = soundGroup;
        playSoundInfo.m_PlaySoundParams = playSoundParams;
        playSoundInfo.m_UserData = userData;
        return playSoundInfo;
      }

      public void Clear()
      {
        this.m_SerialId = 0;
        this.m_SoundGroup = (SoundManager.SoundGroup) null;
        this.m_PlaySoundParams = (PlaySoundParams) null;
        this.m_UserData = (object) null;
      }
    }

    /// <summary>声音代理。</summary>
    private sealed class SoundAgent : ISoundAgent
    {
      private readonly SoundManager.SoundGroup m_SoundGroup;
      private readonly ISoundHelper m_SoundHelper;
      private readonly ISoundAgentHelper m_SoundAgentHelper;
      private int m_SerialId;
      private object m_SoundAsset;
      private DateTime m_SetSoundAssetTime;
      private bool m_MuteInSoundGroup;
      private float m_VolumeInSoundGroup;

      /// <summary>初始化声音代理的新实例。</summary>
      /// <param name="soundGroup">所在的声音组。</param>
      /// <param name="soundHelper">声音辅助器接口。</param>
      /// <param name="soundAgentHelper">声音代理辅助器接口。</param>
      public SoundAgent(
        SoundManager.SoundGroup soundGroup,
        ISoundHelper soundHelper,
        ISoundAgentHelper soundAgentHelper)
      {
        if (soundGroup == null)
          throw new GameFrameworkException("Sound group is invalid.");
        if (soundHelper == null)
          throw new GameFrameworkException("Sound helper is invalid.");
        if (soundAgentHelper == null)
          throw new GameFrameworkException("Sound agent helper is invalid.");
        this.m_SoundGroup = soundGroup;
        this.m_SoundHelper = soundHelper;
        this.m_SoundAgentHelper = soundAgentHelper;
        this.m_SoundAgentHelper.ResetSoundAgent += new EventHandler<ResetSoundAgentEventArgs>(this.OnResetSoundAgent);
        this.m_SerialId = 0;
        this.m_SoundAsset = (object) null;
        this.Reset();
      }

      /// <summary>获取所在的声音组。</summary>
      public ISoundGroup SoundGroup => (ISoundGroup) this.m_SoundGroup;

      /// <summary>获取或设置声音的序列编号。</summary>
      public int SerialId
      {
        get => this.m_SerialId;
        set => this.m_SerialId = value;
      }

      /// <summary>获取当前是否正在播放。</summary>
      public bool IsPlaying => this.m_SoundAgentHelper.IsPlaying;

      /// <summary>获取声音长度。</summary>
      public float Length => this.m_SoundAgentHelper.Length;

      /// <summary>获取或设置播放位置。</summary>
      public float Time
      {
        get => this.m_SoundAgentHelper.Time;
        set => this.m_SoundAgentHelper.Time = value;
      }

      /// <summary>获取是否静音。</summary>
      public bool Mute => this.m_SoundAgentHelper.Mute;

      /// <summary>获取或设置在声音组内是否静音。</summary>
      public bool MuteInSoundGroup
      {
        get => this.m_MuteInSoundGroup;
        set
        {
          this.m_MuteInSoundGroup = value;
          this.RefreshMute();
        }
      }

      /// <summary>获取或设置是否循环播放。</summary>
      public bool Loop
      {
        get => this.m_SoundAgentHelper.Loop;
        set => this.m_SoundAgentHelper.Loop = value;
      }

      /// <summary>获取或设置声音优先级。</summary>
      public int Priority
      {
        get => this.m_SoundAgentHelper.Priority;
        set => this.m_SoundAgentHelper.Priority = value;
      }

      /// <summary>获取音量大小。</summary>
      public float Volume => this.m_SoundAgentHelper.Volume;

      /// <summary>获取或设置在声音组内音量大小。</summary>
      public float VolumeInSoundGroup
      {
        get => this.m_VolumeInSoundGroup;
        set
        {
          this.m_VolumeInSoundGroup = value;
          this.RefreshVolume();
        }
      }

      /// <summary>获取或设置声音音调。</summary>
      public float Pitch
      {
        get => this.m_SoundAgentHelper.Pitch;
        set => this.m_SoundAgentHelper.Pitch = value;
      }

      /// <summary>获取或设置声音立体声声相。</summary>
      public float PanStereo
      {
        get => this.m_SoundAgentHelper.PanStereo;
        set => this.m_SoundAgentHelper.PanStereo = value;
      }

      /// <summary>获取或设置声音空间混合量。</summary>
      public float SpatialBlend
      {
        get => this.m_SoundAgentHelper.SpatialBlend;
        set => this.m_SoundAgentHelper.SpatialBlend = value;
      }

      /// <summary>获取或设置声音最大距离。</summary>
      public float MaxDistance
      {
        get => this.m_SoundAgentHelper.MaxDistance;
        set => this.m_SoundAgentHelper.MaxDistance = value;
      }

      /// <summary>获取或设置声音多普勒等级。</summary>
      public float DopplerLevel
      {
        get => this.m_SoundAgentHelper.DopplerLevel;
        set => this.m_SoundAgentHelper.DopplerLevel = value;
      }

      /// <summary>获取声音代理辅助器。</summary>
      public ISoundAgentHelper Helper => this.m_SoundAgentHelper;

      /// <summary>获取声音创建时间。</summary>
      internal DateTime SetSoundAssetTime => this.m_SetSoundAssetTime;

      /// <summary>播放声音。</summary>
      public void Play() => this.m_SoundAgentHelper.Play(0.0f);

      /// <summary>播放声音。</summary>
      /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
      public void Play(float fadeInSeconds) => this.m_SoundAgentHelper.Play(fadeInSeconds);

      /// <summary>停止播放声音。</summary>
      public void Stop() => this.m_SoundAgentHelper.Stop(0.0f);

      /// <summary>停止播放声音。</summary>
      /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
      public void Stop(float fadeOutSeconds) => this.m_SoundAgentHelper.Stop(fadeOutSeconds);

      /// <summary>暂停播放声音。</summary>
      public void Pause() => this.m_SoundAgentHelper.Pause(0.0f);

      /// <summary>暂停播放声音。</summary>
      /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
      public void Pause(float fadeOutSeconds) => this.m_SoundAgentHelper.Pause(fadeOutSeconds);

      /// <summary>恢复播放声音。</summary>
      public void Resume() => this.m_SoundAgentHelper.Resume(0.0f);

      /// <summary>恢复播放声音。</summary>
      /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
      public void Resume(float fadeInSeconds) => this.m_SoundAgentHelper.Resume(fadeInSeconds);

      /// <summary>重置声音代理。</summary>
      public void Reset()
      {
        if (this.m_SoundAsset != null)
        {
          this.m_SoundHelper.ReleaseSoundAsset(this.m_SoundAsset);
          this.m_SoundAsset = (object) null;
        }
        this.m_SetSoundAssetTime = DateTime.MinValue;
        this.Time = 0.0f;
        this.MuteInSoundGroup = false;
        this.Loop = false;
        this.Priority = 0;
        this.VolumeInSoundGroup = 1f;
        this.Pitch = 1f;
        this.PanStereo = 0.0f;
        this.SpatialBlend = 0.0f;
        this.MaxDistance = 100f;
        this.DopplerLevel = 1f;
        this.m_SoundAgentHelper.Reset();
      }

      internal bool SetSoundAsset(object soundAsset)
      {
        this.Reset();
        this.m_SoundAsset = soundAsset;
        this.m_SetSoundAssetTime = DateTime.UtcNow;
        return this.m_SoundAgentHelper.SetSoundAsset(soundAsset);
      }

      internal void RefreshMute() => this.m_SoundAgentHelper.Mute = this.m_SoundGroup.Mute || this.m_MuteInSoundGroup;

      internal void RefreshVolume() => this.m_SoundAgentHelper.Volume = this.m_SoundGroup.Volume * this.m_VolumeInSoundGroup;

      private void OnResetSoundAgent(object sender, ResetSoundAgentEventArgs e) => this.Reset();
    }

    /// <summary>声音组。</summary>
    private sealed class SoundGroup : ISoundGroup
    {
      private readonly string m_Name;
      private readonly ISoundGroupHelper m_SoundGroupHelper;
      private readonly List<SoundManager.SoundAgent> m_SoundAgents;
      private bool m_AvoidBeingReplacedBySamePriority;
      private bool m_Mute;
      private float m_Volume;

      /// <summary>初始化声音组的新实例。</summary>
      /// <param name="name">声音组名称。</param>
      /// <param name="soundGroupHelper">声音组辅助器。</param>
      public SoundGroup(string name, ISoundGroupHelper soundGroupHelper)
      {
        if (string.IsNullOrEmpty(name))
          throw new GameFrameworkException("Sound group name is invalid.");
        if (soundGroupHelper == null)
          throw new GameFrameworkException("Sound group helper is invalid.");
        this.m_Name = name;
        this.m_SoundGroupHelper = soundGroupHelper;
        this.m_SoundAgents = new List<SoundManager.SoundAgent>();
      }

      /// <summary>获取声音组名称。</summary>
      public string Name => this.m_Name;

      /// <summary>获取声音代理数。</summary>
      public int SoundAgentCount => this.m_SoundAgents.Count;

      /// <summary>获取或设置声音组中的声音是否避免被同优先级声音替换。</summary>
      public bool AvoidBeingReplacedBySamePriority
      {
        get => this.m_AvoidBeingReplacedBySamePriority;
        set => this.m_AvoidBeingReplacedBySamePriority = value;
      }

      /// <summary>获取或设置声音组静音。</summary>
      public bool Mute
      {
        get => this.m_Mute;
        set
        {
          this.m_Mute = value;
          foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
            soundAgent.RefreshMute();
        }
      }

      /// <summary>获取或设置声音组音量。</summary>
      public float Volume
      {
        get => this.m_Volume;
        set
        {
          this.m_Volume = value;
          foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
            soundAgent.RefreshVolume();
        }
      }

      /// <summary>获取声音组辅助器。</summary>
      public ISoundGroupHelper Helper => this.m_SoundGroupHelper;

      /// <summary>增加声音代理辅助器。</summary>
      /// <param name="soundHelper">声音辅助器接口。</param>
      /// <param name="soundAgentHelper">要增加的声音代理辅助器。</param>
      public void AddSoundAgentHelper(ISoundHelper soundHelper, ISoundAgentHelper soundAgentHelper) => this.m_SoundAgents.Add(new SoundManager.SoundAgent(this, soundHelper, soundAgentHelper));

      /// <summary>播放声音。</summary>
      /// <param name="serialId">声音的序列编号。</param>
      /// <param name="soundAsset">声音资源。</param>
      /// <param name="playSoundParams">播放声音参数。</param>
      /// <param name="errorCode">错误码。</param>
      /// <returns>用于播放的声音代理。</returns>
      public ISoundAgent PlaySound(
        int serialId,
        object soundAsset,
        PlaySoundParams playSoundParams,
        out PlaySoundErrorCode? errorCode)
      {
        errorCode = new PlaySoundErrorCode?();
        SoundManager.SoundAgent soundAgent1 = (SoundManager.SoundAgent) null;
        foreach (SoundManager.SoundAgent soundAgent2 in this.m_SoundAgents)
        {
          if (!soundAgent2.IsPlaying)
          {
            soundAgent1 = soundAgent2;
            break;
          }
          if (soundAgent2.Priority < playSoundParams.Priority)
          {
            if (soundAgent1 == null || soundAgent2.Priority < soundAgent1.Priority)
              soundAgent1 = soundAgent2;
          }
          else if (!this.m_AvoidBeingReplacedBySamePriority && soundAgent2.Priority == playSoundParams.Priority && (soundAgent1 == null || soundAgent2.SetSoundAssetTime < soundAgent1.SetSoundAssetTime))
            soundAgent1 = soundAgent2;
        }
        if (soundAgent1 == null)
        {
          errorCode = new PlaySoundErrorCode?(PlaySoundErrorCode.IgnoredDueToLowPriority);
          return (ISoundAgent) null;
        }
        if (!soundAgent1.SetSoundAsset(soundAsset))
        {
          errorCode = new PlaySoundErrorCode?(PlaySoundErrorCode.SetSoundAssetFailure);
          return (ISoundAgent) null;
        }
        soundAgent1.SerialId = serialId;
        soundAgent1.Time = playSoundParams.Time;
        soundAgent1.MuteInSoundGroup = playSoundParams.MuteInSoundGroup;
        soundAgent1.Loop = playSoundParams.Loop;
        soundAgent1.Priority = playSoundParams.Priority;
        soundAgent1.VolumeInSoundGroup = playSoundParams.VolumeInSoundGroup;
        soundAgent1.Pitch = playSoundParams.Pitch;
        soundAgent1.PanStereo = playSoundParams.PanStereo;
        soundAgent1.SpatialBlend = playSoundParams.SpatialBlend;
        soundAgent1.MaxDistance = playSoundParams.MaxDistance;
        soundAgent1.DopplerLevel = playSoundParams.DopplerLevel;
        soundAgent1.Play(playSoundParams.FadeInSeconds);
        return (ISoundAgent) soundAgent1;
      }

      /// <summary>停止播放声音。</summary>
      /// <param name="serialId">要停止播放声音的序列编号。</param>
      /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
      /// <returns>是否停止播放声音成功。</returns>
      public bool StopSound(int serialId, float fadeOutSeconds)
      {
        foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
        {
          if (soundAgent.SerialId == serialId)
          {
            soundAgent.Stop(fadeOutSeconds);
            return true;
          }
        }
        return false;
      }

      /// <summary>暂停播放声音。</summary>
      /// <param name="serialId">要暂停播放声音的序列编号。</param>
      /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
      /// <returns>是否暂停播放声音成功。</returns>
      public bool PauseSound(int serialId, float fadeOutSeconds)
      {
        foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
        {
          if (soundAgent.SerialId == serialId)
          {
            soundAgent.Pause(fadeOutSeconds);
            return true;
          }
        }
        return false;
      }

      /// <summary>恢复播放声音。</summary>
      /// <param name="serialId">要恢复播放声音的序列编号。</param>
      /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
      /// <returns>是否恢复播放声音成功。</returns>
      public bool ResumeSound(int serialId, float fadeInSeconds)
      {
        foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
        {
          if (soundAgent.SerialId == serialId)
          {
            soundAgent.Resume(fadeInSeconds);
            return true;
          }
        }
        return false;
      }

      /// <summary>停止所有已加载的声音。</summary>
      public void StopAllLoadedSounds()
      {
        foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
        {
          if (soundAgent.IsPlaying)
            soundAgent.Stop();
        }
      }

      /// <summary>停止所有已加载的声音。</summary>
      /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
      public void StopAllLoadedSounds(float fadeOutSeconds)
      {
        foreach (SoundManager.SoundAgent soundAgent in this.m_SoundAgents)
        {
          if (soundAgent.IsPlaying)
            soundAgent.Stop(fadeOutSeconds);
        }
      }
    }
  }
}
