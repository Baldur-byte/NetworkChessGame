// Decompiled with JetBrains decompiler
// Type: GameFramework.Sound.PlaySoundParams
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.Sound
{
  /// <summary>播放声音参数。</summary>
  public sealed class PlaySoundParams : IReference
  {
    private bool m_Referenced;
    private float m_Time;
    private bool m_MuteInSoundGroup;
    private bool m_Loop;
    private int m_Priority;
    private float m_VolumeInSoundGroup;
    private float m_FadeInSeconds;
    private float m_Pitch;
    private float m_PanStereo;
    private float m_SpatialBlend;
    private float m_MaxDistance;
    private float m_DopplerLevel;

    /// <summary>初始化播放声音参数的新实例。</summary>
    public PlaySoundParams()
    {
      this.m_Referenced = false;
      this.m_Time = 0.0f;
      this.m_MuteInSoundGroup = false;
      this.m_Loop = false;
      this.m_Priority = 0;
      this.m_VolumeInSoundGroup = 1f;
      this.m_FadeInSeconds = 0.0f;
      this.m_Pitch = 1f;
      this.m_PanStereo = 0.0f;
      this.m_SpatialBlend = 0.0f;
      this.m_MaxDistance = 100f;
      this.m_DopplerLevel = 1f;
    }

    /// <summary>获取或设置播放位置。</summary>
    public float Time
    {
      get => this.m_Time;
      set => this.m_Time = value;
    }

    /// <summary>获取或设置在声音组内是否静音。</summary>
    public bool MuteInSoundGroup
    {
      get => this.m_MuteInSoundGroup;
      set => this.m_MuteInSoundGroup = value;
    }

    /// <summary>获取或设置是否循环播放。</summary>
    public bool Loop
    {
      get => this.m_Loop;
      set => this.m_Loop = value;
    }

    /// <summary>获取或设置声音优先级。</summary>
    public int Priority
    {
      get => this.m_Priority;
      set => this.m_Priority = value;
    }

    /// <summary>获取或设置在声音组内音量大小。</summary>
    public float VolumeInSoundGroup
    {
      get => this.m_VolumeInSoundGroup;
      set => this.m_VolumeInSoundGroup = value;
    }

    /// <summary>获取或设置声音淡入时间，以秒为单位。</summary>
    public float FadeInSeconds
    {
      get => this.m_FadeInSeconds;
      set => this.m_FadeInSeconds = value;
    }

    /// <summary>获取或设置声音音调。</summary>
    public float Pitch
    {
      get => this.m_Pitch;
      set => this.m_Pitch = value;
    }

    /// <summary>获取或设置声音立体声声相。</summary>
    public float PanStereo
    {
      get => this.m_PanStereo;
      set => this.m_PanStereo = value;
    }

    /// <summary>获取或设置声音空间混合量。</summary>
    public float SpatialBlend
    {
      get => this.m_SpatialBlend;
      set => this.m_SpatialBlend = value;
    }

    /// <summary>获取或设置声音最大距离。</summary>
    public float MaxDistance
    {
      get => this.m_MaxDistance;
      set => this.m_MaxDistance = value;
    }

    /// <summary>获取或设置声音多普勒等级。</summary>
    public float DopplerLevel
    {
      get => this.m_DopplerLevel;
      set => this.m_DopplerLevel = value;
    }

    internal bool Referenced => this.m_Referenced;

    /// <summary>创建播放声音参数。</summary>
    /// <returns>创建的播放声音参数。</returns>
    public static PlaySoundParams Create()
    {
      PlaySoundParams playSoundParams = ReferencePool.Acquire<PlaySoundParams>();
      playSoundParams.m_Referenced = true;
      return playSoundParams;
    }

    /// <summary>清理播放声音参数。</summary>
    public void Clear()
    {
      this.m_Time = 0.0f;
      this.m_MuteInSoundGroup = false;
      this.m_Loop = false;
      this.m_Priority = 0;
      this.m_VolumeInSoundGroup = 1f;
      this.m_FadeInSeconds = 0.0f;
      this.m_Pitch = 1f;
      this.m_PanStereo = 0.0f;
      this.m_SpatialBlend = 0.0f;
      this.m_MaxDistance = 100f;
      this.m_DopplerLevel = 1f;
    }
  }
}
