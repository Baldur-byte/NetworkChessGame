// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkLog
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework
{
  /// <summary>游戏框架日志类。</summary>
  public static class GameFrameworkLog
  {
    private static GameFrameworkLog.ILogHelper s_LogHelper;

    /// <summary>设置游戏框架日志辅助器。</summary>
    /// <param name="logHelper">要设置的游戏框架日志辅助器。</param>
    public static void SetLogHelper(GameFrameworkLog.ILogHelper logHelper) => GameFrameworkLog.s_LogHelper = logHelper;

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <param name="message">日志内容。</param>
    public static void Debug(object message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, message);
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <param name="message">日志内容。</param>
    public static void Debug(string message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) message);
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T">日志参数的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg">日志参数。</param>
    public static void Debug<T>(string format, T arg)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T>(format, arg));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    public static void Debug<T1, T2>(string format, T1 arg1, T2 arg2)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2>(format, arg1, arg2));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    public static void Debug<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3>(format, arg1, arg2, arg3));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    public static void Debug<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    public static void Debug<T1, T2, T3, T4, T5>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
    }

    /// <summary>打印调试级别日志，用于记录调试类日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <typeparam name="T16">日志参数 16 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    /// <param name="arg16">日志参数 16。</param>
    public static void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Debug, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <param name="message">日志内容。</param>
    public static void Info(object message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, message);
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <param name="message">日志内容。</param>
    public static void Info(string message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) message);
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T">日志参数的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg">日志参数。</param>
    public static void Info<T>(string format, T arg)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T>(format, arg));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    public static void Info<T1, T2>(string format, T1 arg1, T2 arg2)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2>(format, arg1, arg2));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    public static void Info<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3>(format, arg1, arg2, arg3));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    public static void Info<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    public static void Info<T1, T2, T3, T4, T5>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    public static void Info<T1, T2, T3, T4, T5, T6>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
    }

    /// <summary>打印信息级别日志，用于记录程序正常运行日志信息。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <typeparam name="T16">日志参数 16 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    /// <param name="arg16">日志参数 16。</param>
    public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Info, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <param name="message">日志内容。</param>
    public static void Warning(object message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, message);
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <param name="message">日志内容。</param>
    public static void Warning(string message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) message);
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T">日志参数的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg">日志参数。</param>
    public static void Warning<T>(string format, T arg)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T>(format, arg));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    public static void Warning<T1, T2>(string format, T1 arg1, T2 arg2)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2>(format, arg1, arg2));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    public static void Warning<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3>(format, arg1, arg2, arg3));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    public static void Warning<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    public static void Warning<T1, T2, T3, T4, T5>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
    }

    /// <summary>打印警告级别日志，建议在发生局部功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <typeparam name="T16">日志参数 16 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    /// <param name="arg16">日志参数 16。</param>
    public static void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Warning, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <param name="message">日志内容。</param>
    public static void Error(object message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, message);
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <param name="message">日志内容。</param>
    public static void Error(string message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) message);
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T">日志参数的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg">日志参数。</param>
    public static void Error<T>(string format, T arg)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T>(format, arg));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    public static void Error<T1, T2>(string format, T1 arg1, T2 arg2)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2>(format, arg1, arg2));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    public static void Error<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3>(format, arg1, arg2, arg3));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    public static void Error<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    public static void Error<T1, T2, T3, T4, T5>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    public static void Error<T1, T2, T3, T4, T5, T6>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
    }

    /// <summary>打印错误级别日志，建议在发生功能逻辑错误，但尚不会导致游戏崩溃或异常时使用。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <typeparam name="T16">日志参数 16 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    /// <param name="arg16">日志参数 16。</param>
    public static void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Error, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <param name="message">日志内容。</param>
    public static void Fatal(object message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, message);
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <param name="message">日志内容。</param>
    public static void Fatal(string message)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) message);
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T">日志参数的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg">日志参数。</param>
    public static void Fatal<T>(string format, T arg)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T>(format, arg));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    public static void Fatal<T1, T2>(string format, T1 arg1, T2 arg2)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2>(format, arg1, arg2));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    public static void Fatal<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3>(format, arg1, arg2, arg3));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    public static void Fatal<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    public static void Fatal<T1, T2, T3, T4, T5>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8>(
      string format,
      T1 arg1,
      T2 arg2,
      T3 arg3,
      T4 arg4,
      T5 arg5,
      T6 arg6,
      T7 arg7,
      T8 arg8)
    {
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15));
    }

    /// <summary>打印严重错误级别日志，建议在发生严重错误，可能导致游戏崩溃或异常时使用，此时应尝试重启进程或重建游戏框架。</summary>
    /// <typeparam name="T1">日志参数 1 的类型。</typeparam>
    /// <typeparam name="T2">日志参数 2 的类型。</typeparam>
    /// <typeparam name="T3">日志参数 3 的类型。</typeparam>
    /// <typeparam name="T4">日志参数 4 的类型。</typeparam>
    /// <typeparam name="T5">日志参数 5 的类型。</typeparam>
    /// <typeparam name="T6">日志参数 6 的类型。</typeparam>
    /// <typeparam name="T7">日志参数 7 的类型。</typeparam>
    /// <typeparam name="T8">日志参数 8 的类型。</typeparam>
    /// <typeparam name="T9">日志参数 9 的类型。</typeparam>
    /// <typeparam name="T10">日志参数 10 的类型。</typeparam>
    /// <typeparam name="T11">日志参数 11 的类型。</typeparam>
    /// <typeparam name="T12">日志参数 12 的类型。</typeparam>
    /// <typeparam name="T13">日志参数 13 的类型。</typeparam>
    /// <typeparam name="T14">日志参数 14 的类型。</typeparam>
    /// <typeparam name="T15">日志参数 15 的类型。</typeparam>
    /// <typeparam name="T16">日志参数 16 的类型。</typeparam>
    /// <param name="format">日志格式。</param>
    /// <param name="arg1">日志参数 1。</param>
    /// <param name="arg2">日志参数 2。</param>
    /// <param name="arg3">日志参数 3。</param>
    /// <param name="arg4">日志参数 4。</param>
    /// <param name="arg5">日志参数 5。</param>
    /// <param name="arg6">日志参数 6。</param>
    /// <param name="arg7">日志参数 7。</param>
    /// <param name="arg8">日志参数 8。</param>
    /// <param name="arg9">日志参数 9。</param>
    /// <param name="arg10">日志参数 10。</param>
    /// <param name="arg11">日志参数 11。</param>
    /// <param name="arg12">日志参数 12。</param>
    /// <param name="arg13">日志参数 13。</param>
    /// <param name="arg14">日志参数 14。</param>
    /// <param name="arg15">日志参数 15。</param>
    /// <param name="arg16">日志参数 16。</param>
    public static void Fatal<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
      string format,
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
      if (GameFrameworkLog.s_LogHelper == null)
        return;
      GameFrameworkLog.s_LogHelper.Log(GameFrameworkLogLevel.Fatal, (object) Utility.Text.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16));
    }

    /// <summary>游戏框架日志辅助器接口。</summary>
    public interface ILogHelper
    {
      /// <summary>记录日志。</summary>
      /// <param name="level">游戏框架日志等级。</param>
      /// <param name="message">日志内容。</param>
      void Log(GameFrameworkLogLevel level, object message);
    }
  }
}
