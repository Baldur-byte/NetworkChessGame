// Decompiled with JetBrains decompiler
// Type: GameFramework.Utility
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameFramework
{
  /// <summary>实用函数集。</summary>
  public static class Utility
  {
    /// <summary>程序集相关的实用函数。</summary>
    public static class Assembly
    {
      private static readonly System.Reflection.Assembly[] s_Assemblies = (System.Reflection.Assembly[]) null;
      private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>((IEqualityComparer<string>) StringComparer.Ordinal);

      static Assembly() => Utility.Assembly.s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();

      /// <summary>获取已加载的程序集。</summary>
      /// <returns>已加载的程序集。</returns>
      public static System.Reflection.Assembly[] GetAssemblies() => Utility.Assembly.s_Assemblies;

      /// <summary>获取已加载的程序集中的所有类型。</summary>
      /// <returns>已加载的程序集中的所有类型。</returns>
      public static Type[] GetTypes()
      {
        List<Type> typeList = new List<Type>();
        foreach (System.Reflection.Assembly assembly in Utility.Assembly.s_Assemblies)
          typeList.AddRange((IEnumerable<Type>) assembly.GetTypes());
        return typeList.ToArray();
      }

      /// <summary>获取已加载的程序集中的所有类型。</summary>
      /// <param name="results">已加载的程序集中的所有类型。</param>
      public static void GetTypes(List<Type> results)
      {
        if (results == null)
          throw new GameFrameworkException("Results is invalid.");
        results.Clear();
        foreach (System.Reflection.Assembly assembly in Utility.Assembly.s_Assemblies)
          results.AddRange((IEnumerable<Type>) assembly.GetTypes());
      }

      /// <summary>获取已加载的程序集中的指定类型。</summary>
      /// <param name="typeName">要获取的类型名。</param>
      /// <returns>已加载的程序集中的指定类型。</returns>
      public static Type GetType(string typeName)
      {
        if (string.IsNullOrEmpty(typeName))
          throw new GameFrameworkException("Type name is invalid.");
        Type type1 = (Type) null;
        if (Utility.Assembly.s_CachedTypes.TryGetValue(typeName, out type1))
          return type1;
        Type type2 = Type.GetType(typeName);
        if (type2 != null)
        {
          Utility.Assembly.s_CachedTypes.Add(typeName, type2);
          return type2;
        }
        foreach (System.Reflection.Assembly assembly in Utility.Assembly.s_Assemblies)
        {
          Type type3 = Type.GetType(Utility.Text.Format<string, string>("{0}, {1}", typeName, assembly.FullName));
          if (type3 != null)
          {
            Utility.Assembly.s_CachedTypes.Add(typeName, type3);
            return type3;
          }
        }
        return (Type) null;
      }
    }

    /// <summary>压缩解压缩相关的实用函数。</summary>
    public static class Compression
    {
      private static Utility.Compression.ICompressionHelper s_CompressionHelper;

      /// <summary>设置压缩解压缩辅助器。</summary>
      /// <param name="compressionHelper">要设置的压缩解压缩辅助器。</param>
      public static void SetCompressionHelper(
        Utility.Compression.ICompressionHelper compressionHelper)
      {
        Utility.Compression.s_CompressionHelper = compressionHelper;
      }

      /// <summary>压缩数据。</summary>
      /// <param name="bytes">要压缩的数据的二进制流。</param>
      /// <returns>压缩后的数据的二进制流。</returns>
      public static byte[] Compress(byte[] bytes) => bytes != null ? Utility.Compression.Compress(bytes, 0, bytes.Length) : throw new GameFrameworkException("Bytes is invalid.");

      /// <summary>压缩数据。</summary>
      /// <param name="bytes">要压缩的数据的二进制流。</param>
      /// <param name="compressedStream">压缩后的数据的二进制流。</param>
      /// <returns>是否压缩数据成功。</returns>
      public static bool Compress(byte[] bytes, Stream compressedStream)
      {
        if (bytes == null)
          throw new GameFrameworkException("Bytes is invalid.");
        return Utility.Compression.Compress(bytes, 0, bytes.Length, compressedStream);
      }

      /// <summary>压缩数据。</summary>
      /// <param name="bytes">要压缩的数据的二进制流。</param>
      /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
      /// <param name="length">要压缩的数据的二进制流的长度。</param>
      /// <returns>压缩后的数据的二进制流。</returns>
      public static byte[] Compress(byte[] bytes, int offset, int length)
      {
        using (MemoryStream compressedStream = new MemoryStream())
          return Utility.Compression.Compress(bytes, offset, length, (Stream) compressedStream) ? compressedStream.ToArray() : (byte[]) null;
      }

      /// <summary>压缩数据。</summary>
      /// <param name="bytes">要压缩的数据的二进制流。</param>
      /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
      /// <param name="length">要压缩的数据的二进制流的长度。</param>
      /// <param name="compressedStream">压缩后的数据的二进制流。</param>
      /// <returns>是否压缩数据成功。</returns>
      public static bool Compress(byte[] bytes, int offset, int length, Stream compressedStream)
      {
        if (Utility.Compression.s_CompressionHelper == null)
          throw new GameFrameworkException("Compressed helper is invalid.");
        if (bytes == null)
          throw new GameFrameworkException("Bytes is invalid.");
        if (offset < 0 || length < 0 || offset + length > bytes.Length)
          throw new GameFrameworkException("Offset or length is invalid.");
        if (compressedStream == null)
          throw new GameFrameworkException("Compressed stream is invalid.");
        try
        {
          return Utility.Compression.s_CompressionHelper.Compress(bytes, offset, length, compressedStream);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not compress with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>压缩数据。</summary>
      /// <param name="stream">要压缩的数据的二进制流。</param>
      /// <returns>压缩后的数据的二进制流。</returns>
      public static byte[] Compress(Stream stream)
      {
        using (MemoryStream compressedStream = new MemoryStream())
          return Utility.Compression.Compress(stream, (Stream) compressedStream) ? compressedStream.ToArray() : (byte[]) null;
      }

      /// <summary>压缩数据。</summary>
      /// <param name="stream">要压缩的数据的二进制流。</param>
      /// <param name="compressedStream">压缩后的数据的二进制流。</param>
      /// <returns>是否压缩数据成功。</returns>
      public static bool Compress(Stream stream, Stream compressedStream)
      {
        if (Utility.Compression.s_CompressionHelper == null)
          throw new GameFrameworkException("Compressed helper is invalid.");
        if (stream == null)
          throw new GameFrameworkException("Stream is invalid.");
        if (compressedStream == null)
          throw new GameFrameworkException("Compressed stream is invalid.");
        try
        {
          return Utility.Compression.s_CompressionHelper.Compress(stream, compressedStream);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not compress with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>解压缩数据。</summary>
      /// <param name="bytes">要解压缩的数据的二进制流。</param>
      /// <returns>解压缩后的数据的二进制流。</returns>
      public static byte[] Decompress(byte[] bytes) => bytes != null ? Utility.Compression.Decompress(bytes, 0, bytes.Length) : throw new GameFrameworkException("Bytes is invalid.");

      /// <summary>解压缩数据。</summary>
      /// <param name="bytes">要解压缩的数据的二进制流。</param>
      /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
      /// <returns>是否解压缩数据成功。</returns>
      public static bool Decompress(byte[] bytes, Stream decompressedStream)
      {
        if (bytes == null)
          throw new GameFrameworkException("Bytes is invalid.");
        return Utility.Compression.Decompress(bytes, 0, bytes.Length, decompressedStream);
      }

      /// <summary>解压缩数据。</summary>
      /// <param name="bytes">要解压缩的数据的二进制流。</param>
      /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
      /// <param name="length">要解压缩的数据的二进制流的长度。</param>
      /// <returns>解压缩后的数据的二进制流。</returns>
      public static byte[] Decompress(byte[] bytes, int offset, int length)
      {
        using (MemoryStream decompressedStream = new MemoryStream())
          return Utility.Compression.Decompress(bytes, offset, length, (Stream) decompressedStream) ? decompressedStream.ToArray() : (byte[]) null;
      }

      /// <summary>解压缩数据。</summary>
      /// <param name="bytes">要解压缩的数据的二进制流。</param>
      /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
      /// <param name="length">要解压缩的数据的二进制流的长度。</param>
      /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
      /// <returns>是否解压缩数据成功。</returns>
      public static bool Decompress(
        byte[] bytes,
        int offset,
        int length,
        Stream decompressedStream)
      {
        if (Utility.Compression.s_CompressionHelper == null)
          throw new GameFrameworkException("Compressed helper is invalid.");
        if (bytes == null)
          throw new GameFrameworkException("Bytes is invalid.");
        if (offset < 0 || length < 0 || offset + length > bytes.Length)
          throw new GameFrameworkException("Offset or length is invalid.");
        if (decompressedStream == null)
          throw new GameFrameworkException("Decompressed stream is invalid.");
        try
        {
          return Utility.Compression.s_CompressionHelper.Decompress(bytes, offset, length, decompressedStream);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not decompress with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>解压缩数据。</summary>
      /// <param name="stream">要解压缩的数据的二进制流。</param>
      /// <returns>是否解压缩数据成功。</returns>
      public static byte[] Decompress(Stream stream)
      {
        using (MemoryStream decompressedStream = new MemoryStream())
          return Utility.Compression.Decompress(stream, (Stream) decompressedStream) ? decompressedStream.ToArray() : (byte[]) null;
      }

      /// <summary>解压缩数据。</summary>
      /// <param name="stream">要解压缩的数据的二进制流。</param>
      /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
      /// <returns>是否解压缩数据成功。</returns>
      public static bool Decompress(Stream stream, Stream decompressedStream)
      {
        if (Utility.Compression.s_CompressionHelper == null)
          throw new GameFrameworkException("Compressed helper is invalid.");
        if (stream == null)
          throw new GameFrameworkException("Stream is invalid.");
        if (decompressedStream == null)
          throw new GameFrameworkException("Decompressed stream is invalid.");
        try
        {
          return Utility.Compression.s_CompressionHelper.Decompress(stream, decompressedStream);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not decompress with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>压缩解压缩辅助器接口。</summary>
      public interface ICompressionHelper
      {
        /// <summary>压缩数据。</summary>
        /// <param name="bytes">要压缩的数据的二进制流。</param>
        /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
        /// <param name="length">要压缩的数据的二进制流的长度。</param>
        /// <param name="compressedStream">压缩后的数据的二进制流。</param>
        /// <returns>是否压缩数据成功。</returns>
        bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

        /// <summary>压缩数据。</summary>
        /// <param name="stream">要压缩的数据的二进制流。</param>
        /// <param name="compressedStream">压缩后的数据的二进制流。</param>
        /// <returns>是否压缩数据成功。</returns>
        bool Compress(Stream stream, Stream compressedStream);

        /// <summary>解压缩数据。</summary>
        /// <param name="bytes">要解压缩的数据的二进制流。</param>
        /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
        /// <param name="length">要解压缩的数据的二进制流的长度。</param>
        /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
        /// <returns>是否解压缩数据成功。</returns>
        bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

        /// <summary>解压缩数据。</summary>
        /// <param name="stream">要解压缩的数据的二进制流。</param>
        /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
        /// <returns>是否解压缩数据成功。</returns>
        bool Decompress(Stream stream, Stream decompressedStream);
      }
    }

    /// <summary>类型转换相关的实用函数。</summary>
    public static class Converter
    {
      private const float InchesToCentimeters = 2.54f;
      private const float CentimetersToInches = 0.393700778f;

      /// <summary>获取数据在此计算机结构中存储时的字节顺序。</summary>
      public static bool IsLittleEndian => BitConverter.IsLittleEndian;

      /// <summary>获取或设置屏幕每英寸点数。</summary>
      public static float ScreenDpi { get; set; }

      /// <summary>将像素转换为厘米。</summary>
      /// <param name="pixels">像素。</param>
      /// <returns>厘米。</returns>
      public static float GetCentimetersFromPixels(float pixels)
      {
        if ((double) Utility.Converter.ScreenDpi <= 0.0)
          throw new GameFrameworkException("You must set screen DPI first.");
        return 2.54f * pixels / Utility.Converter.ScreenDpi;
      }

      /// <summary>将厘米转换为像素。</summary>
      /// <param name="centimeters">厘米。</param>
      /// <returns>像素。</returns>
      public static float GetPixelsFromCentimeters(float centimeters)
      {
        if ((double) Utility.Converter.ScreenDpi <= 0.0)
          throw new GameFrameworkException("You must set screen DPI first.");
        return 0.393700778f * centimeters * Utility.Converter.ScreenDpi;
      }

      /// <summary>将像素转换为英寸。</summary>
      /// <param name="pixels">像素。</param>
      /// <returns>英寸。</returns>
      public static float GetInchesFromPixels(float pixels)
      {
        if ((double) Utility.Converter.ScreenDpi <= 0.0)
          throw new GameFrameworkException("You must set screen DPI first.");
        return pixels / Utility.Converter.ScreenDpi;
      }

      /// <summary>将英寸转换为像素。</summary>
      /// <param name="inches">英寸。</param>
      /// <returns>像素。</returns>
      public static float GetPixelsFromInches(float inches)
      {
        if ((double) Utility.Converter.ScreenDpi <= 0.0)
          throw new GameFrameworkException("You must set screen DPI first.");
        return inches * Utility.Converter.ScreenDpi;
      }

      /// <summary>以字节数组的形式获取指定的布尔值。</summary>
      /// <param name="value">要转换的布尔值。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(bool value)
      {
        byte[] buffer = new byte[1];
        Utility.Converter.GetBytes(value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的布尔值。</summary>
      /// <param name="value">要转换的布尔值。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(bool value, byte[] buffer) => Utility.Converter.GetBytes(value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的布尔值。</summary>
      /// <param name="value">要转换的布尔值。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static void GetBytes(bool value, byte[] buffer, int startIndex)
      {
        if (buffer == null)
          throw new GameFrameworkException("Buffer is invalid.");
        if (startIndex < 0 || startIndex + 1 > buffer.Length)
          throw new GameFrameworkException("Start index is invalid.");
        buffer[startIndex] = value ? (byte) 1 : (byte) 0;
      }

      /// <summary>返回由字节数组中首字节转换来的布尔值。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>如果 value 中的首字节非零，则为 true，否则为 false。</returns>
      public static bool GetBoolean(byte[] value) => BitConverter.ToBoolean(value, 0);

      /// <summary>返回由字节数组中指定位置的一个字节转换来的布尔值。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>如果 value 中指定位置的字节非零，则为 true，否则为 false。</returns>
      public static bool GetBoolean(byte[] value, int startIndex) => BitConverter.ToBoolean(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 Unicode 字符值。</summary>
      /// <param name="value">要转换的字符。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(char value)
      {
        byte[] buffer = new byte[2];
        Utility.Converter.GetBytes((short) value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 Unicode 字符值。</summary>
      /// <param name="value">要转换的字符。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(char value, byte[] buffer) => Utility.Converter.GetBytes((short) value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 Unicode 字符值。</summary>
      /// <param name="value">要转换的字符。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static void GetBytes(char value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes((short) value, buffer, startIndex);

      /// <summary>返回由字节数组中前两个字节转换来的 Unicode 字符。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由两个字节构成的字符。</returns>
      public static char GetChar(byte[] value) => BitConverter.ToChar(value, 0);

      /// <summary>返回由字节数组中指定位置的两个字节转换来的 Unicode 字符。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由两个字节构成的字符。</returns>
      public static char GetChar(byte[] value, int startIndex) => BitConverter.ToChar(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 16 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(short value)
      {
        byte[] buffer = new byte[2];
        Utility.Converter.GetBytes(value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 16 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(short value, byte[] buffer) => Utility.Converter.GetBytes(value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 16 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static unsafe void GetBytes(short value, byte[] buffer, int startIndex)
      {
        if (buffer == null)
          throw new GameFrameworkException("Buffer is invalid.");
        if (startIndex < 0 || startIndex + 2 > buffer.Length)
          throw new GameFrameworkException("Start index is invalid.");
        fixed (byte* numPtr = buffer)
          *(short*) (numPtr + startIndex) = value;
      }

      /// <summary>返回由字节数组中前两个字节转换来的 16 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由两个字节构成的 16 位有符号整数。</returns>
      public static short GetInt16(byte[] value) => BitConverter.ToInt16(value, 0);

      /// <summary>返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由两个字节构成的 16 位有符号整数。</returns>
      public static short GetInt16(byte[] value, int startIndex) => BitConverter.ToInt16(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 16 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(ushort value)
      {
        byte[] buffer = new byte[2];
        Utility.Converter.GetBytes((short) value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 16 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(ushort value, byte[] buffer) => Utility.Converter.GetBytes((short) value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 16 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static void GetBytes(ushort value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes((short) value, buffer, startIndex);

      /// <summary>返回由字节数组中前两个字节转换来的 16 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由两个字节构成的 16 位无符号整数。</returns>
      public static ushort GetUInt16(byte[] value) => BitConverter.ToUInt16(value, 0);

      /// <summary>返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由两个字节构成的 16 位无符号整数。</returns>
      public static ushort GetUInt16(byte[] value, int startIndex) => BitConverter.ToUInt16(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 32 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(int value)
      {
        byte[] buffer = new byte[4];
        Utility.Converter.GetBytes(value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 32 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(int value, byte[] buffer) => Utility.Converter.GetBytes(value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 32 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static unsafe void GetBytes(int value, byte[] buffer, int startIndex)
      {
        if (buffer == null)
          throw new GameFrameworkException("Buffer is invalid.");
        if (startIndex < 0 || startIndex + 4 > buffer.Length)
          throw new GameFrameworkException("Start index is invalid.");
        fixed (byte* numPtr = buffer)
          *(int*) (numPtr + startIndex) = value;
      }

      /// <summary>返回由字节数组中前四个字节转换来的 32 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由四个字节构成的 32 位有符号整数。</returns>
      public static int GetInt32(byte[] value) => BitConverter.ToInt32(value, 0);

      /// <summary>返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由四个字节构成的 32 位有符号整数。</returns>
      public static int GetInt32(byte[] value, int startIndex) => BitConverter.ToInt32(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 32 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(uint value)
      {
        byte[] buffer = new byte[4];
        Utility.Converter.GetBytes((int) value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 32 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(uint value, byte[] buffer) => Utility.Converter.GetBytes((int) value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 32 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static void GetBytes(uint value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes((int) value, buffer, startIndex);

      /// <summary>返回由字节数组中前四个字节转换来的 32 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由四个字节构成的 32 位无符号整数。</returns>
      public static uint GetUInt32(byte[] value) => BitConverter.ToUInt32(value, 0);

      /// <summary>返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由四个字节构成的 32 位无符号整数。</returns>
      public static uint GetUInt32(byte[] value, int startIndex) => BitConverter.ToUInt32(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 64 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(long value)
      {
        byte[] buffer = new byte[8];
        Utility.Converter.GetBytes(value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 64 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(long value, byte[] buffer) => Utility.Converter.GetBytes(value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 64 位有符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static unsafe void GetBytes(long value, byte[] buffer, int startIndex)
      {
        if (buffer == null)
          throw new GameFrameworkException("Buffer is invalid.");
        if (startIndex < 0 || startIndex + 8 > buffer.Length)
          throw new GameFrameworkException("Start index is invalid.");
        fixed (byte* numPtr = buffer)
          *(long*) (numPtr + startIndex) = value;
      }

      /// <summary>返回由字节数组中前八个字节转换来的 64 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由八个字节构成的 64 位有符号整数。</returns>
      public static long GetInt64(byte[] value) => BitConverter.ToInt64(value, 0);

      /// <summary>返回由字节数组中指定位置的八个字节转换来的 64 位有符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由八个字节构成的 64 位有符号整数。</returns>
      public static long GetInt64(byte[] value, int startIndex) => BitConverter.ToInt64(value, startIndex);

      /// <summary>以字节数组的形式获取指定的 64 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(ulong value)
      {
        byte[] buffer = new byte[8];
        Utility.Converter.GetBytes((long) value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的 64 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static void GetBytes(ulong value, byte[] buffer) => Utility.Converter.GetBytes((long) value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的 64 位无符号整数值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static void GetBytes(ulong value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes((long) value, buffer, startIndex);

      /// <summary>返回由字节数组中前八个字节转换来的 64 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由八个字节构成的 64 位无符号整数。</returns>
      public static ulong GetUInt64(byte[] value) => BitConverter.ToUInt64(value, 0);

      /// <summary>返回由字节数组中指定位置的八个字节转换来的 64 位无符号整数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由八个字节构成的 64 位无符号整数。</returns>
      public static ulong GetUInt64(byte[] value, int startIndex) => BitConverter.ToUInt64(value, startIndex);

      /// <summary>以字节数组的形式获取指定的单精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static unsafe byte[] GetBytes(float value)
      {
        byte[] buffer = new byte[4];
        Utility.Converter.GetBytes(*(int*) &value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的单精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static unsafe void GetBytes(float value, byte[] buffer) => Utility.Converter.GetBytes(*(int*) &value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的单精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static unsafe void GetBytes(float value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes(*(int*) &value, buffer, startIndex);

      /// <summary>返回由字节数组中前四个字节转换来的单精度浮点数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由四个字节构成的单精度浮点数。</returns>
      public static float GetSingle(byte[] value) => BitConverter.ToSingle(value, 0);

      /// <summary>返回由字节数组中指定位置的四个字节转换来的单精度浮点数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由四个字节构成的单精度浮点数。</returns>
      public static float GetSingle(byte[] value, int startIndex) => BitConverter.ToSingle(value, startIndex);

      /// <summary>以字节数组的形式获取指定的双精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static unsafe byte[] GetBytes(double value)
      {
        byte[] buffer = new byte[8];
        Utility.Converter.GetBytes(*(long*) &value, buffer, 0);
        return buffer;
      }

      /// <summary>以字节数组的形式获取指定的双精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      public static unsafe void GetBytes(double value, byte[] buffer) => Utility.Converter.GetBytes(*(long*) &value, buffer, 0);

      /// <summary>以字节数组的形式获取指定的双精度浮点值。</summary>
      /// <param name="value">要转换的数字。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      public static unsafe void GetBytes(double value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes(*(long*) &value, buffer, startIndex);

      /// <summary>返回由字节数组中前八个字节转换来的双精度浮点数。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>由八个字节构成的双精度浮点数。</returns>
      public static double GetDouble(byte[] value) => BitConverter.ToDouble(value, 0);

      /// <summary>返回由字节数组中指定位置的八个字节转换来的双精度浮点数。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <returns>由八个字节构成的双精度浮点数。</returns>
      public static double GetDouble(byte[] value, int startIndex) => BitConverter.ToDouble(value, startIndex);

      /// <summary>以字节数组的形式获取 UTF-8 编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(string value) => Utility.Converter.GetBytes(value, Encoding.UTF8);

      /// <summary>以字节数组的形式获取 UTF-8 编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <returns>buffer 内实际填充了多少字节。</returns>
      public static int GetBytes(string value, byte[] buffer) => Utility.Converter.GetBytes(value, Encoding.UTF8, buffer, 0);

      /// <summary>以字节数组的形式获取 UTF-8 编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      /// <returns>buffer 内实际填充了多少字节。</returns>
      public static int GetBytes(string value, byte[] buffer, int startIndex) => Utility.Converter.GetBytes(value, Encoding.UTF8, buffer, startIndex);

      /// <summary>以字节数组的形式获取指定编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <param name="encoding">要使用的编码。</param>
      /// <returns>用于存放结果的字节数组。</returns>
      public static byte[] GetBytes(string value, Encoding encoding)
      {
        if (value == null)
          throw new GameFrameworkException("Value is invalid.");
        return encoding != null ? encoding.GetBytes(value) : throw new GameFrameworkException("Encoding is invalid.");
      }

      /// <summary>以字节数组的形式获取指定编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <param name="encoding">要使用的编码。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <returns>buffer 内实际填充了多少字节。</returns>
      public static int GetBytes(string value, Encoding encoding, byte[] buffer) => Utility.Converter.GetBytes(value, encoding, buffer, 0);

      /// <summary>以字节数组的形式获取指定编码的字符串。</summary>
      /// <param name="value">要转换的字符串。</param>
      /// <param name="encoding">要使用的编码。</param>
      /// <param name="buffer">用于存放结果的字节数组。</param>
      /// <param name="startIndex">buffer 内的起始位置。</param>
      /// <returns>buffer 内实际填充了多少字节。</returns>
      public static int GetBytes(string value, Encoding encoding, byte[] buffer, int startIndex)
      {
        if (value == null)
          throw new GameFrameworkException("Value is invalid.");
        if (encoding == null)
          throw new GameFrameworkException("Encoding is invalid.");
        return encoding.GetBytes(value, 0, value.Length, buffer, startIndex);
      }

      /// <summary>返回由字节数组使用 UTF-8 编码转换成的字符串。</summary>
      /// <param name="value">字节数组。</param>
      /// <returns>转换后的字符串。</returns>
      public static string GetString(byte[] value) => Utility.Converter.GetString(value, Encoding.UTF8);

      /// <summary>返回由字节数组使用指定编码转换成的字符串。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="encoding">要使用的编码。</param>
      /// <returns>转换后的字符串。</returns>
      public static string GetString(byte[] value, Encoding encoding)
      {
        if (value == null)
          throw new GameFrameworkException("Value is invalid.");
        return encoding != null ? encoding.GetString(value) : throw new GameFrameworkException("Encoding is invalid.");
      }

      /// <summary>返回由字节数组使用 UTF-8 编码转换成的字符串。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <param name="length">长度。</param>
      /// <returns>转换后的字符串。</returns>
      public static string GetString(byte[] value, int startIndex, int length) => Utility.Converter.GetString(value, startIndex, length, Encoding.UTF8);

      /// <summary>返回由字节数组使用指定编码转换成的字符串。</summary>
      /// <param name="value">字节数组。</param>
      /// <param name="startIndex">value 内的起始位置。</param>
      /// <param name="length">长度。</param>
      /// <param name="encoding">要使用的编码。</param>
      /// <returns>转换后的字符串。</returns>
      public static string GetString(byte[] value, int startIndex, int length, Encoding encoding)
      {
        if (value == null)
          throw new GameFrameworkException("Value is invalid.");
        if (encoding == null)
          throw new GameFrameworkException("Encoding is invalid.");
        return encoding.GetString(value, startIndex, length);
      }
    }

    /// <summary>加密解密相关的实用函数。</summary>
    public static class Encryption
    {
      internal const int QuickEncryptLength = 220;

      /// <summary>将 bytes 使用 code 做异或运算的快速版本。</summary>
      /// <param name="bytes">原始二进制流。</param>
      /// <param name="code">异或二进制流。</param>
      /// <returns>异或后的二进制流。</returns>
      public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code) => Utility.Encryption.GetXorBytes(bytes, 0, 220, code);

      /// <summary>
      /// 将 bytes 使用 code 做异或运算的快速版本。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
      /// </summary>
      /// <param name="bytes">原始及异或后的二进制流。</param>
      /// <param name="code">异或二进制流。</param>
      public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code) => Utility.Encryption.GetSelfXorBytes(bytes, 0, 220, code);

      /// <summary>将 bytes 使用 code 做异或运算。</summary>
      /// <param name="bytes">原始二进制流。</param>
      /// <param name="code">异或二进制流。</param>
      /// <returns>异或后的二进制流。</returns>
      public static byte[] GetXorBytes(byte[] bytes, byte[] code) => bytes == null ? (byte[]) null : Utility.Encryption.GetXorBytes(bytes, 0, bytes.Length, code);

      /// <summary>
      /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
      /// </summary>
      /// <param name="bytes">原始及异或后的二进制流。</param>
      /// <param name="code">异或二进制流。</param>
      public static void GetSelfXorBytes(byte[] bytes, byte[] code)
      {
        if (bytes == null)
          return;
        Utility.Encryption.GetSelfXorBytes(bytes, 0, bytes.Length, code);
      }

      /// <summary>将 bytes 使用 code 做异或运算。</summary>
      /// <param name="bytes">原始二进制流。</param>
      /// <param name="startIndex">异或计算的开始位置。</param>
      /// <param name="length">异或计算长度，若小于 0，则计算整个二进制流。</param>
      /// <param name="code">异或二进制流。</param>
      /// <returns>异或后的二进制流。</returns>
      public static byte[] GetXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
      {
        if (bytes == null)
          return (byte[]) null;
        int length1 = bytes.Length;
        byte[] xorBytes = new byte[length1];
        Array.Copy((Array) bytes, 0, (Array) xorBytes, 0, length1);
        Utility.Encryption.GetSelfXorBytes(xorBytes, startIndex, length, code);
        return xorBytes;
      }

      /// <summary>
      /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
      /// </summary>
      /// <param name="bytes">原始及异或后的二进制流。</param>
      /// <param name="startIndex">异或计算的开始位置。</param>
      /// <param name="length">异或计算长度。</param>
      /// <param name="code">异或二进制流。</param>
      public static void GetSelfXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
      {
        if (bytes == null)
          return;
        int num1 = code != null ? code.Length : throw new GameFrameworkException("Code is invalid.");
        if (num1 <= 0)
          throw new GameFrameworkException("Code length is invalid.");
        if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
          throw new GameFrameworkException("Start index or length is invalid.");
        int num2 = startIndex % num1;
        for (int index1 = startIndex; index1 < length; ++index1)
        {
          ref byte local = ref bytes[index1];
          int num3 = (int) local;
          byte[] numArray = code;
          int index2 = num2;
          int num4 = index2 + 1;
          int num5 = (int) numArray[index2];
          local = (byte) (num3 ^ num5);
          num2 = num4 % num1;
        }
      }
    }

    /// <summary>JSON 相关的实用函数。</summary>
    public static class Json
    {
      private static Utility.Json.IJsonHelper s_JsonHelper;

      /// <summary>设置 JSON 辅助器。</summary>
      /// <param name="jsonHelper">要设置的 JSON 辅助器。</param>
      public static void SetJsonHelper(Utility.Json.IJsonHelper jsonHelper) => Utility.Json.s_JsonHelper = jsonHelper;

      /// <summary>将对象序列化为 JSON 字符串。</summary>
      /// <param name="obj">要序列化的对象。</param>
      /// <returns>序列化后的 JSON 字符串。</returns>
      public static string ToJson(object obj)
      {
        if (Utility.Json.s_JsonHelper == null)
          throw new GameFrameworkException("JSON helper is invalid.");
        try
        {
          return Utility.Json.s_JsonHelper.ToJson(obj);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not convert to JSON with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>将 JSON 字符串反序列化为对象。</summary>
      /// <typeparam name="T">对象类型。</typeparam>
      /// <param name="json">要反序列化的 JSON 字符串。</param>
      /// <returns>反序列化后的对象。</returns>
      public static T ToObject<T>(string json)
      {
        if (Utility.Json.s_JsonHelper == null)
          throw new GameFrameworkException("JSON helper is invalid.");
        try
        {
          return Utility.Json.s_JsonHelper.ToObject<T>(json);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not convert to object with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>将 JSON 字符串反序列化为对象。</summary>
      /// <param name="objectType">对象类型。</param>
      /// <param name="json">要反序列化的 JSON 字符串。</param>
      /// <returns>反序列化后的对象。</returns>
      public static object ToObject(Type objectType, string json)
      {
        if (Utility.Json.s_JsonHelper == null)
          throw new GameFrameworkException("JSON helper is invalid.");
        if (objectType == null)
          throw new GameFrameworkException("Object type is invalid.");
        try
        {
          return Utility.Json.s_JsonHelper.ToObject(objectType, json);
        }
        catch (Exception ex)
        {
          if (!(ex is GameFrameworkException))
            throw new GameFrameworkException(Utility.Text.Format<Exception>("Can not convert to object with exception '{0}'.", ex), ex);
          throw;
        }
      }

      /// <summary>JSON 辅助器接口。</summary>
      public interface IJsonHelper
      {
        /// <summary>将对象序列化为 JSON 字符串。</summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns>序列化后的 JSON 字符串。</returns>
        string ToJson(object obj);

        /// <summary>将 JSON 字符串反序列化为对象。</summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        T ToObject<T>(string json);

        /// <summary>将 JSON 字符串反序列化为对象。</summary>
        /// <param name="objectType">对象类型。</param>
        /// <param name="json">要反序列化的 JSON 字符串。</param>
        /// <returns>反序列化后的对象。</returns>
        object ToObject(Type objectType, string json);
      }
    }

    /// <summary>Marshal 相关的实用函数。</summary>
    public static class Marshal
    {
      private const int BlockSize = 4096;
      private static IntPtr s_CachedHGlobalPtr = IntPtr.Zero;
      private static int s_CachedHGlobalSize = 0;

      /// <summary>获取缓存的从进程的非托管内存中分配的内存的大小。</summary>
      public static int CachedHGlobalSize => Utility.Marshal.s_CachedHGlobalSize;

      /// <summary>确保从进程的非托管内存中分配足够大小的内存并缓存。</summary>
      /// <param name="ensureSize">要确保从进程的非托管内存中分配内存的大小。</param>
      public static void EnsureCachedHGlobalSize(int ensureSize)
      {
        if (ensureSize < 0)
          throw new GameFrameworkException("Ensure size is invalid.");
        if (!(Utility.Marshal.s_CachedHGlobalPtr == IntPtr.Zero) && Utility.Marshal.s_CachedHGlobalSize >= ensureSize)
          return;
        Utility.Marshal.FreeCachedHGlobal();
        int cb = (ensureSize - 1 + 4096) / 4096 * 4096;
        Utility.Marshal.s_CachedHGlobalPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(cb);
        Utility.Marshal.s_CachedHGlobalSize = cb;
      }

      /// <summary>释放缓存的从进程的非托管内存中分配的内存。</summary>
      public static void FreeCachedHGlobal()
      {
        if (!(Utility.Marshal.s_CachedHGlobalPtr != IntPtr.Zero))
          return;
        System.Runtime.InteropServices.Marshal.FreeHGlobal(Utility.Marshal.s_CachedHGlobalPtr);
        Utility.Marshal.s_CachedHGlobalPtr = IntPtr.Zero;
        Utility.Marshal.s_CachedHGlobalSize = 0;
      }

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <returns>存储转换结果的二进制流。</returns>
      public static byte[] StructureToBytes<T>(T structure) => Utility.Marshal.StructureToBytes<T>(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof (T)));

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <param name="structureSize">要转换的对象的大小。</param>
      /// <returns>存储转换结果的二进制流。</returns>
      internal static byte[] StructureToBytes<T>(T structure, int structureSize)
      {
        if (structureSize < 0)
          throw new GameFrameworkException("Structure size is invalid.");
        Utility.Marshal.EnsureCachedHGlobalSize(structureSize);
        System.Runtime.InteropServices.Marshal.StructureToPtr((object) structure, Utility.Marshal.s_CachedHGlobalPtr, true);
        byte[] destination = new byte[structureSize];
        System.Runtime.InteropServices.Marshal.Copy(Utility.Marshal.s_CachedHGlobalPtr, destination, 0, structureSize);
        return destination;
      }

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <param name="result">存储转换结果的二进制流。</param>
      public static void StructureToBytes<T>(T structure, byte[] result) => Utility.Marshal.StructureToBytes<T>(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof (T)), result, 0);

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <param name="structureSize">要转换的对象的大小。</param>
      /// <param name="result">存储转换结果的二进制流。</param>
      internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result) => Utility.Marshal.StructureToBytes<T>(structure, structureSize, result, 0);

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <param name="result">存储转换结果的二进制流。</param>
      /// <param name="startIndex">写入存储转换结果的二进制流的起始位置。</param>
      public static void StructureToBytes<T>(T structure, byte[] result, int startIndex) => Utility.Marshal.StructureToBytes<T>(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof (T)), result, startIndex);

      /// <summary>将数据从对象转换为二进制流。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structure">要转换的对象。</param>
      /// <param name="structureSize">要转换的对象的大小。</param>
      /// <param name="result">存储转换结果的二进制流。</param>
      /// <param name="startIndex">写入存储转换结果的二进制流的起始位置。</param>
      internal static void StructureToBytes<T>(
        T structure,
        int structureSize,
        byte[] result,
        int startIndex)
      {
        if (structureSize < 0)
          throw new GameFrameworkException("Structure size is invalid.");
        if (result == null)
          throw new GameFrameworkException("Result is invalid.");
        if (startIndex < 0)
          throw new GameFrameworkException("Start index is invalid.");
        if (startIndex + structureSize > result.Length)
          throw new GameFrameworkException("Result length is not enough.");
        Utility.Marshal.EnsureCachedHGlobalSize(structureSize);
        System.Runtime.InteropServices.Marshal.StructureToPtr((object) structure, Utility.Marshal.s_CachedHGlobalPtr, true);
        System.Runtime.InteropServices.Marshal.Copy(Utility.Marshal.s_CachedHGlobalPtr, result, startIndex, structureSize);
      }

      /// <summary>将数据从二进制流转换为对象。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="buffer">要转换的二进制流。</param>
      /// <returns>存储转换结果的对象。</returns>
      public static T BytesToStructure<T>(byte[] buffer) => Utility.Marshal.BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof (T)), buffer, 0);

      /// <summary>将数据从二进制流转换为对象。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="buffer">要转换的二进制流。</param>
      /// <param name="startIndex">读取要转换的二进制流的起始位置。</param>
      /// <returns>存储转换结果的对象。</returns>
      public static T BytesToStructure<T>(byte[] buffer, int startIndex) => Utility.Marshal.BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof (T)), buffer, startIndex);

      /// <summary>将数据从二进制流转换为对象。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structureSize">要转换的对象的大小。</param>
      /// <param name="buffer">要转换的二进制流。</param>
      /// <returns>存储转换结果的对象。</returns>
      internal static T BytesToStructure<T>(int structureSize, byte[] buffer) => Utility.Marshal.BytesToStructure<T>(structureSize, buffer, 0);

      /// <summary>将数据从二进制流转换为对象。</summary>
      /// <typeparam name="T">要转换的对象的类型。</typeparam>
      /// <param name="structureSize">要转换的对象的大小。</param>
      /// <param name="buffer">要转换的二进制流。</param>
      /// <param name="startIndex">读取要转换的二进制流的起始位置。</param>
      /// <returns>存储转换结果的对象。</returns>
      internal static T BytesToStructure<T>(int structureSize, byte[] buffer, int startIndex)
      {
        if (structureSize < 0)
          throw new GameFrameworkException("Structure size is invalid.");
        if (buffer == null)
          throw new GameFrameworkException("Buffer is invalid.");
        if (startIndex < 0)
          throw new GameFrameworkException("Start index is invalid.");
        if (startIndex + structureSize > buffer.Length)
          throw new GameFrameworkException("Buffer length is not enough.");
        Utility.Marshal.EnsureCachedHGlobalSize(structureSize);
        System.Runtime.InteropServices.Marshal.Copy(buffer, startIndex, Utility.Marshal.s_CachedHGlobalPtr, structureSize);
        return (T) System.Runtime.InteropServices.Marshal.PtrToStructure(Utility.Marshal.s_CachedHGlobalPtr, typeof (T));
      }
    }

    /// <summary>路径相关的实用函数。</summary>
    public static class Path
    {
      /// <summary>获取规范的路径。</summary>
      /// <param name="path">要规范的路径。</param>
      /// <returns>规范的路径。</returns>
      public static string GetRegularPath(string path) => path?.Replace('\\', '/');

      /// <summary>获取远程格式的路径（带有file:// 或 http:// 前缀）。</summary>
      /// <param name="path">原始路径。</param>
      /// <returns>远程格式路径。</returns>
      public static string GetRemotePath(string path)
      {
        string regularPath = Utility.Path.GetRegularPath(path);
        if (regularPath == null)
          return (string) null;
        return !regularPath.Contains("://") ? ("file:///" + regularPath).Replace("file:////", "file:///") : regularPath;
      }

      /// <summary>移除空文件夹。</summary>
      /// <param name="directoryName">要处理的文件夹名称。</param>
      /// <returns>是否移除空文件夹成功。</returns>
      public static bool RemoveEmptyDirectory(string directoryName)
      {
        if (string.IsNullOrEmpty(directoryName))
          throw new GameFrameworkException("Directory name is invalid.");
        try
        {
          if (!Directory.Exists(directoryName))
            return false;
          string[] directories = Directory.GetDirectories(directoryName, "*");
          int length = directories.Length;
          foreach (string directoryName1 in directories)
          {
            if (Utility.Path.RemoveEmptyDirectory(directoryName1))
              --length;
          }
          if (length > 0 || Directory.GetFiles(directoryName, "*").Length != 0)
            return false;
          Directory.Delete(directoryName);
          return true;
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>随机相关的实用函数。</summary>
    public static class Random
    {
      private static System.Random s_Random = new System.Random((int) DateTime.UtcNow.Ticks);

      /// <summary>设置随机数种子。</summary>
      /// <param name="seed">随机数种子。</param>
      public static void SetSeed(int seed) => Utility.Random.s_Random = new System.Random(seed);

      /// <summary>返回非负随机数。</summary>
      /// <returns>大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数。</returns>
      public static int GetRandom() => Utility.Random.s_Random.Next();

      /// <summary>返回一个小于所指定最大值的非负随机数。</summary>
      /// <param name="maxValue">要生成的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于零。</param>
      /// <returns>大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。不过，如果 maxValue 等于零，则返回 maxValue。</returns>
      public static int GetRandom(int maxValue) => Utility.Random.s_Random.Next(maxValue);

      /// <summary>返回一个指定范围内的随机数。</summary>
      /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
      /// <param name="maxValue">返回的随机数的上界（随机数不能取该上界值）。maxValue 必须大于等于 minValue。</param>
      /// <returns>一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。如果 minValue 等于 maxValue，则返回 minValue。</returns>
      public static int GetRandom(int minValue, int maxValue) => Utility.Random.s_Random.Next(minValue, maxValue);

      /// <summary>返回一个介于 0.0 和 1.0 之间的随机数。</summary>
      /// <returns>大于等于 0.0 并且小于 1.0 的双精度浮点数。</returns>
      public static double GetRandomDouble() => Utility.Random.s_Random.NextDouble();

      /// <summary>用随机数填充指定字节数组的元素。</summary>
      /// <param name="buffer">包含随机数的字节数组。</param>
      public static void GetRandomBytes(byte[] buffer) => Utility.Random.s_Random.NextBytes(buffer);
    }

    /// <summary>字符相关的实用函数。</summary>
    public static class Text
    {
      private static Utility.Text.ITextHelper s_TextHelper;

      /// <summary>设置字符辅助器。</summary>
      /// <param name="textHelper">要设置的字符辅助器。</param>
      public static void SetTextHelper(Utility.Text.ITextHelper textHelper) => Utility.Text.s_TextHelper = textHelper;

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T">字符串参数的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg">字符串参数。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T>(string format, T arg)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        return Utility.Text.s_TextHelper == null ? string.Format(format, (object) arg) : Utility.Text.s_TextHelper.Format<T>(format, arg);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2>(string format, T1 arg1, T2 arg2)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        return Utility.Text.s_TextHelper == null ? string.Format(format, (object) arg1, (object) arg2) : Utility.Text.s_TextHelper.Format<T1, T2>(format, arg1, arg2);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        return Utility.Text.s_TextHelper == null ? string.Format(format, (object) arg1, (object) arg2, (object) arg3) : Utility.Text.s_TextHelper.Format<T1, T2, T3>(format, arg1, arg2, arg3);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4>(
        string format,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5>(
        string format,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6>(
        string format,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7>(
        string format,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7)
      {
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <param name="arg12">字符串参数 12。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11, (object) arg12);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
      /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <param name="arg12">字符串参数 12。</param>
      /// <param name="arg13">字符串参数 13。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11, (object) arg12, (object) arg13);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
      /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
      /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <param name="arg12">字符串参数 12。</param>
      /// <param name="arg13">字符串参数 13。</param>
      /// <param name="arg14">字符串参数 14。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11, (object) arg12, (object) arg13, (object) arg14);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
      /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
      /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
      /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <param name="arg12">字符串参数 12。</param>
      /// <param name="arg13">字符串参数 13。</param>
      /// <param name="arg14">字符串参数 14。</param>
      /// <param name="arg15">字符串参数 15。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11, (object) arg12, (object) arg13, (object) arg14, (object) arg15);
      }

      /// <summary>获取格式化字符串。</summary>
      /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
      /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
      /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
      /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
      /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
      /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
      /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
      /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
      /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
      /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
      /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
      /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
      /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
      /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
      /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
      /// <typeparam name="T16">字符串参数 16 的类型。</typeparam>
      /// <param name="format">字符串格式。</param>
      /// <param name="arg1">字符串参数 1。</param>
      /// <param name="arg2">字符串参数 2。</param>
      /// <param name="arg3">字符串参数 3。</param>
      /// <param name="arg4">字符串参数 4。</param>
      /// <param name="arg5">字符串参数 5。</param>
      /// <param name="arg6">字符串参数 6。</param>
      /// <param name="arg7">字符串参数 7。</param>
      /// <param name="arg8">字符串参数 8。</param>
      /// <param name="arg9">字符串参数 9。</param>
      /// <param name="arg10">字符串参数 10。</param>
      /// <param name="arg11">字符串参数 11。</param>
      /// <param name="arg12">字符串参数 12。</param>
      /// <param name="arg13">字符串参数 13。</param>
      /// <param name="arg14">字符串参数 14。</param>
      /// <param name="arg15">字符串参数 15。</param>
      /// <param name="arg16">字符串参数 16。</param>
      /// <returns>格式化后的字符串。</returns>
      public static string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
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
        if (format == null)
          throw new GameFrameworkException("Format is invalid.");
        if (Utility.Text.s_TextHelper != null)
          return Utility.Text.s_TextHelper.Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
        return string.Format(format, (object) arg1, (object) arg2, (object) arg3, (object) arg4, (object) arg5, (object) arg6, (object) arg7, (object) arg8, (object) arg9, (object) arg10, (object) arg11, (object) arg12, (object) arg13, (object) arg14, (object) arg15, (object) arg16);
      }

      /// <summary>字符辅助器接口。</summary>
      public interface ITextHelper
      {
        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T">字符串参数的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg">字符串参数。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T>(string format, T arg);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2>(string format, T1 arg1, T2 arg2);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5>(
          string format,
          T1 arg1,
          T2 arg2,
          T3 arg3,
          T4 arg4,
          T5 arg5);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6>(
          string format,
          T1 arg1,
          T2 arg2,
          T3 arg3,
          T4 arg4,
          T5 arg5,
          T6 arg6);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7>(
          string format,
          T1 arg1,
          T2 arg2,
          T3 arg3,
          T4 arg4,
          T5 arg5,
          T6 arg6,
          T7 arg7);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8>(
          string format,
          T1 arg1,
          T2 arg2,
          T3 arg3,
          T4 arg4,
          T5 arg5,
          T6 arg6,
          T7 arg7,
          T8 arg8);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
          string format,
          T1 arg1,
          T2 arg2,
          T3 arg3,
          T4 arg4,
          T5 arg5,
          T6 arg6,
          T7 arg7,
          T8 arg8,
          T9 arg9);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
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
          T10 arg10);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
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
          T11 arg11);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <param name="arg12">字符串参数 12。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
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
          T12 arg12);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
        /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <param name="arg12">字符串参数 12。</param>
        /// <param name="arg13">字符串参数 13。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
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
          T13 arg13);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
        /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
        /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <param name="arg12">字符串参数 12。</param>
        /// <param name="arg13">字符串参数 13。</param>
        /// <param name="arg14">字符串参数 14。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
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
          T14 arg14);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
        /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
        /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
        /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <param name="arg12">字符串参数 12。</param>
        /// <param name="arg13">字符串参数 13。</param>
        /// <param name="arg14">字符串参数 14。</param>
        /// <param name="arg15">字符串参数 15。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
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
          T15 arg15);

        /// <summary>获取格式化字符串。</summary>
        /// <typeparam name="T1">字符串参数 1 的类型。</typeparam>
        /// <typeparam name="T2">字符串参数 2 的类型。</typeparam>
        /// <typeparam name="T3">字符串参数 3 的类型。</typeparam>
        /// <typeparam name="T4">字符串参数 4 的类型。</typeparam>
        /// <typeparam name="T5">字符串参数 5 的类型。</typeparam>
        /// <typeparam name="T6">字符串参数 6 的类型。</typeparam>
        /// <typeparam name="T7">字符串参数 7 的类型。</typeparam>
        /// <typeparam name="T8">字符串参数 8 的类型。</typeparam>
        /// <typeparam name="T9">字符串参数 9 的类型。</typeparam>
        /// <typeparam name="T10">字符串参数 10 的类型。</typeparam>
        /// <typeparam name="T11">字符串参数 11 的类型。</typeparam>
        /// <typeparam name="T12">字符串参数 12 的类型。</typeparam>
        /// <typeparam name="T13">字符串参数 13 的类型。</typeparam>
        /// <typeparam name="T14">字符串参数 14 的类型。</typeparam>
        /// <typeparam name="T15">字符串参数 15 的类型。</typeparam>
        /// <typeparam name="T16">字符串参数 16 的类型。</typeparam>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <param name="arg3">字符串参数 3。</param>
        /// <param name="arg4">字符串参数 4。</param>
        /// <param name="arg5">字符串参数 5。</param>
        /// <param name="arg6">字符串参数 6。</param>
        /// <param name="arg7">字符串参数 7。</param>
        /// <param name="arg8">字符串参数 8。</param>
        /// <param name="arg9">字符串参数 9。</param>
        /// <param name="arg10">字符串参数 10。</param>
        /// <param name="arg11">字符串参数 11。</param>
        /// <param name="arg12">字符串参数 12。</param>
        /// <param name="arg13">字符串参数 13。</param>
        /// <param name="arg14">字符串参数 14。</param>
        /// <param name="arg15">字符串参数 15。</param>
        /// <param name="arg16">字符串参数 16。</param>
        /// <returns>格式化后的字符串。</returns>
        string Format<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
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
          T16 arg16);
      }
    }

    /// <summary>校验相关的实用函数。</summary>
    public static class Verifier
    {
      private const int CachedBytesLength = 4096;
      private static readonly byte[] s_CachedBytes = new byte[4096];
      private static readonly Utility.Verifier.Crc32 s_Algorithm = new Utility.Verifier.Crc32();

      /// <summary>计算二进制流的 CRC32。</summary>
      /// <param name="bytes">指定的二进制流。</param>
      /// <returns>计算后的 CRC32。</returns>
      public static int GetCrc32(byte[] bytes) => bytes != null ? Utility.Verifier.GetCrc32(bytes, 0, bytes.Length) : throw new GameFrameworkException("Bytes is invalid.");

      /// <summary>计算二进制流的 CRC32。</summary>
      /// <param name="bytes">指定的二进制流。</param>
      /// <param name="offset">二进制流的偏移。</param>
      /// <param name="length">二进制流的长度。</param>
      /// <returns>计算后的 CRC32。</returns>
      public static int GetCrc32(byte[] bytes, int offset, int length)
      {
        if (bytes == null)
          throw new GameFrameworkException("Bytes is invalid.");
        if (offset < 0 || length < 0 || offset + length > bytes.Length)
          throw new GameFrameworkException("Offset or length is invalid.");
        Utility.Verifier.s_Algorithm.HashCore(bytes, offset, length);
        int crc32 = (int) Utility.Verifier.s_Algorithm.HashFinal();
        Utility.Verifier.s_Algorithm.Initialize();
        return crc32;
      }

      /// <summary>计算二进制流的 CRC32。</summary>
      /// <param name="stream">指定的二进制流。</param>
      /// <returns>计算后的 CRC32。</returns>
      public static int GetCrc32(Stream stream)
      {
        if (stream == null)
          throw new GameFrameworkException("Stream is invalid.");
        while (true)
        {
          int length = stream.Read(Utility.Verifier.s_CachedBytes, 0, 4096);
          if (length > 0)
            Utility.Verifier.s_Algorithm.HashCore(Utility.Verifier.s_CachedBytes, 0, length);
          else
            break;
        }
        int crc32 = (int) Utility.Verifier.s_Algorithm.HashFinal();
        Utility.Verifier.s_Algorithm.Initialize();
        Array.Clear((Array) Utility.Verifier.s_CachedBytes, 0, 4096);
        return crc32;
      }

      /// <summary>获取 CRC32 数值的二进制数组。</summary>
      /// <param name="crc32">CRC32 数值。</param>
      /// <returns>CRC32 数值的二进制数组。</returns>
      public static byte[] GetCrc32Bytes(int crc32) => new byte[4]
      {
        (byte) (crc32 >> 24 & (int) byte.MaxValue),
        (byte) (crc32 >> 16 & (int) byte.MaxValue),
        (byte) (crc32 >> 8 & (int) byte.MaxValue),
        (byte) (crc32 & (int) byte.MaxValue)
      };

      /// <summary>获取 CRC32 数值的二进制数组。</summary>
      /// <param name="crc32">CRC32 数值。</param>
      /// <param name="bytes">要存放结果的数组。</param>
      public static void GetCrc32Bytes(int crc32, byte[] bytes) => Utility.Verifier.GetCrc32Bytes(crc32, bytes, 0);

      /// <summary>获取 CRC32 数值的二进制数组。</summary>
      /// <param name="crc32">CRC32 数值。</param>
      /// <param name="bytes">要存放结果的数组。</param>
      /// <param name="offset">CRC32 数值的二进制数组在结果数组内的起始位置。</param>
      public static void GetCrc32Bytes(int crc32, byte[] bytes, int offset)
      {
        if (bytes == null)
          throw new GameFrameworkException("Result is invalid.");
        if (offset < 0 || offset + 4 > bytes.Length)
          throw new GameFrameworkException("Offset or length is invalid.");
        bytes[offset] = (byte) (crc32 >> 24 & (int) byte.MaxValue);
        bytes[offset + 1] = (byte) (crc32 >> 16 & (int) byte.MaxValue);
        bytes[offset + 2] = (byte) (crc32 >> 8 & (int) byte.MaxValue);
        bytes[offset + 3] = (byte) (crc32 & (int) byte.MaxValue);
      }

      internal static int GetCrc32(Stream stream, byte[] code, int length)
      {
        if (stream == null)
          throw new GameFrameworkException("Stream is invalid.");
        int num1 = code != null ? code.Length : throw new GameFrameworkException("Code is invalid.");
        if (num1 <= 0)
          throw new GameFrameworkException("Code length is invalid.");
        int length1 = (int) stream.Length;
        if (length < 0 || length > length1)
          length = length1;
        int num2 = 0;
        while (true)
        {
          int length2 = stream.Read(Utility.Verifier.s_CachedBytes, 0, 4096);
          if (length2 > 0)
          {
            if (length > 0)
            {
              for (int index1 = 0; index1 < length2 && index1 < length; ++index1)
              {
                ref byte local = ref Utility.Verifier.s_CachedBytes[index1];
                int num3 = (int) local;
                byte[] numArray = code;
                int index2 = num2;
                int num4 = index2 + 1;
                int num5 = (int) numArray[index2];
                local = (byte) (num3 ^ num5);
                num2 = num4 % num1;
              }
              length -= length2;
            }
            Utility.Verifier.s_Algorithm.HashCore(Utility.Verifier.s_CachedBytes, 0, length2);
          }
          else
            break;
        }
        int crc32 = (int) Utility.Verifier.s_Algorithm.HashFinal();
        Utility.Verifier.s_Algorithm.Initialize();
        Array.Clear((Array) Utility.Verifier.s_CachedBytes, 0, 4096);
        return crc32;
      }

      /// <summary>CRC32 算法。</summary>
      private sealed class Crc32
      {
        private const int TableLength = 256;
        private const uint DefaultPolynomial = 3988292384;
        private const uint DefaultSeed = 4294967295;
        private readonly uint m_Seed;
        private readonly uint[] m_Table;
        private uint m_Hash;

        public Crc32()
          : this(3988292384U, uint.MaxValue)
        {
        }

        public Crc32(uint polynomial, uint seed)
        {
          this.m_Seed = seed;
          this.m_Table = Utility.Verifier.Crc32.InitializeTable(polynomial);
          this.m_Hash = seed;
        }

        public void Initialize() => this.m_Hash = this.m_Seed;

        public void HashCore(byte[] bytes, int offset, int length) => this.m_Hash = Utility.Verifier.Crc32.CalculateHash(this.m_Table, this.m_Hash, bytes, offset, length);

        public uint HashFinal() => ~this.m_Hash;

        private static uint CalculateHash(
          uint[] table,
          uint value,
          byte[] bytes,
          int offset,
          int length)
        {
          int num = offset + length;
          for (int index = offset; index < num; ++index)
            value = value >> 8 ^ table[(int) bytes[index] ^ (int) value & (int) byte.MaxValue];
          return value;
        }

        private static uint[] InitializeTable(uint polynomial)
        {
          uint[] numArray = new uint[256];
          for (int index1 = 0; index1 < 256; ++index1)
          {
            uint num = (uint) index1;
            for (int index2 = 0; index2 < 8; ++index2)
            {
              if (((int) num & 1) == 1)
                num = num >> 1 ^ polynomial;
              else
                num >>= 1;
            }
            numArray[index1] = num;
          }
          return numArray;
        }
      }
    }
  }
}
