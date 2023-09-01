// Decompiled with JetBrains decompiler
// Type: GameFramework.Version
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>版本号类。</summary>
  public static class Version
  {
    private const string GameFrameworkVersionString = "2021.05.31";
    private static Version.IVersionHelper s_VersionHelper;

    /// <summary>获取游戏框架版本号。</summary>
    public static string GameFrameworkVersion => "2021.05.31";

    /// <summary>获取游戏版本号。</summary>
    public static string GameVersion => Version.s_VersionHelper == null ? string.Empty : Version.s_VersionHelper.GameVersion;

    /// <summary>获取内部游戏版本号。</summary>
    public static int InternalGameVersion => Version.s_VersionHelper == null ? 0 : Version.s_VersionHelper.InternalGameVersion;

    /// <summary>设置版本号辅助器。</summary>
    /// <param name="versionHelper">要设置的版本号辅助器。</param>
    public static void SetVersionHelper(Version.IVersionHelper versionHelper) => Version.s_VersionHelper = versionHelper;

    /// <summary>版本号辅助器接口。</summary>
    public interface IVersionHelper
    {
      /// <summary>获取游戏版本号。</summary>
      string GameVersion { get; }

      /// <summary>获取内部游戏版本号。</summary>
      int InternalGameVersion { get; }
    }
  }
}
