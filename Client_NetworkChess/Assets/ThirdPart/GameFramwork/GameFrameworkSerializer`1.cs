// Decompiled with JetBrains decompiler
// Type: GameFramework.GameFrameworkSerializer`1
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

using System.Collections.Generic;
using System.IO;

namespace GameFramework
{
  /// <summary>游戏框架序列化器基类。</summary>
  /// <typeparam name="T">要序列化的数据类型。</typeparam>
  public abstract class GameFrameworkSerializer<T>
  {
    private readonly Dictionary<byte, GameFrameworkSerializer<T>.SerializeCallback> m_SerializeCallbacks;
    private readonly Dictionary<byte, GameFrameworkSerializer<T>.DeserializeCallback> m_DeserializeCallbacks;
    private readonly Dictionary<byte, GameFrameworkSerializer<T>.TryGetValueCallback> m_TryGetValueCallbacks;
    private byte m_LatestSerializeCallbackVersion;

    /// <summary>初始化游戏框架序列化器基类的新实例。</summary>
    public GameFrameworkSerializer()
    {
      this.m_SerializeCallbacks = new Dictionary<byte, GameFrameworkSerializer<T>.SerializeCallback>();
      this.m_DeserializeCallbacks = new Dictionary<byte, GameFrameworkSerializer<T>.DeserializeCallback>();
      this.m_TryGetValueCallbacks = new Dictionary<byte, GameFrameworkSerializer<T>.TryGetValueCallback>();
      this.m_LatestSerializeCallbackVersion = (byte) 0;
    }

    /// <summary>注册序列化回调函数。</summary>
    /// <param name="version">序列化回调函数的版本。</param>
    /// <param name="callback">序列化回调函数。</param>
    public void RegisterSerializeCallback(
      byte version,
      GameFrameworkSerializer<T>.SerializeCallback callback)
    {
      this.m_SerializeCallbacks[version] = callback != null ? callback : throw new GameFrameworkException("Serialize callback is invalid.");
      if ((int) version <= (int) this.m_LatestSerializeCallbackVersion)
        return;
      this.m_LatestSerializeCallbackVersion = version;
    }

    /// <summary>注册反序列化回调函数。</summary>
    /// <param name="version">反序列化回调函数的版本。</param>
    /// <param name="callback">反序列化回调函数。</param>
    public void RegisterDeserializeCallback(
      byte version,
      GameFrameworkSerializer<T>.DeserializeCallback callback)
    {
      this.m_DeserializeCallbacks[version] = callback != null ? callback : throw new GameFrameworkException("Deserialize callback is invalid.");
    }

    /// <summary>注册尝试从指定流获取指定键的值回调函数。</summary>
    /// <param name="version">尝试从指定流获取指定键的值回调函数的版本。</param>
    /// <param name="callback">尝试从指定流获取指定键的值回调函数。</param>
    public void RegisterTryGetValueCallback(
      byte version,
      GameFrameworkSerializer<T>.TryGetValueCallback callback)
    {
      this.m_TryGetValueCallbacks[version] = callback != null ? callback : throw new GameFrameworkException("Try get value callback is invalid.");
    }

    /// <summary>序列化数据到目标流中。</summary>
    /// <param name="stream">目标流。</param>
    /// <param name="data">要序列化的数据。</param>
    /// <returns>是否序列化数据成功。</returns>
    public bool Serialize(Stream stream, T data)
    {
      if (this.m_SerializeCallbacks.Count <= 0)
        throw new GameFrameworkException("No serialize callback registered.");
      return this.Serialize(stream, data, this.m_LatestSerializeCallbackVersion);
    }

    /// <summary>序列化数据到目标流中。</summary>
    /// <param name="stream">目标流。</param>
    /// <param name="data">要序列化的数据。</param>
    /// <param name="version">序列化回调函数的版本。</param>
    /// <returns>是否序列化数据成功。</returns>
    public bool Serialize(Stream stream, T data, byte version)
    {
      byte[] header = this.GetHeader();
      stream.WriteByte(header[0]);
      stream.WriteByte(header[1]);
      stream.WriteByte(header[2]);
      stream.WriteByte(version);
      GameFrameworkSerializer<T>.SerializeCallback serializeCallback = (GameFrameworkSerializer<T>.SerializeCallback) null;
      if (!this.m_SerializeCallbacks.TryGetValue(version, out serializeCallback))
        throw new GameFrameworkException(Utility.Text.Format<byte>("Serialize callback '{0}' is not exist.", version));
      return serializeCallback(stream, data);
    }

    /// <summary>从指定流反序列化数据。</summary>
    /// <param name="stream">指定流。</param>
    /// <returns>反序列化的数据。</returns>
    public T Deserialize(Stream stream)
    {
      byte[] header = this.GetHeader();
      byte num1 = (byte) stream.ReadByte();
      byte num2 = (byte) stream.ReadByte();
      byte num3 = (byte) stream.ReadByte();
      if ((int) num1 != (int) header[0] || (int) num2 != (int) header[1] || (int) num3 != (int) header[2])
        throw new GameFrameworkException(Utility.Text.Format<char, char, char, char, char, char>("Header is invalid, need '{0}{1}{2}', current '{3}{4}{5}'.", (char) header[0], (char) header[1], (char) header[2], (char) num1, (char) num2, (char) num3));
      byte key = (byte) stream.ReadByte();
      GameFrameworkSerializer<T>.DeserializeCallback deserializeCallback = (GameFrameworkSerializer<T>.DeserializeCallback) null;
      if (!this.m_DeserializeCallbacks.TryGetValue(key, out deserializeCallback))
        throw new GameFrameworkException(Utility.Text.Format<byte>("Deserialize callback '{0}' is not exist.", key));
      return deserializeCallback(stream);
    }

    /// <summary>尝试从指定流获取指定键的值。</summary>
    /// <param name="stream">指定流。</param>
    /// <param name="key">指定键。</param>
    /// <param name="value">指定键的值。</param>
    /// <returns>是否从指定流获取指定键的值成功。</returns>
    public bool TryGetValue(Stream stream, string key, out object value)
    {
      value = (object) null;
      byte[] header = this.GetHeader();
      int num1 = (int) (byte) stream.ReadByte();
      byte num2 = (byte) stream.ReadByte();
      byte num3 = (byte) stream.ReadByte();
      int num4 = (int) header[0];
      if (num1 != num4 || (int) num2 != (int) header[1] || (int) num3 != (int) header[2])
        return false;
      byte key1 = (byte) stream.ReadByte();
      GameFrameworkSerializer<T>.TryGetValueCallback getValueCallback = (GameFrameworkSerializer<T>.TryGetValueCallback) null;
      return this.m_TryGetValueCallbacks.TryGetValue(key1, out getValueCallback) && getValueCallback(stream, key, out value);
    }

    /// <summary>获取数据头标识。</summary>
    /// <returns>数据头标识。</returns>
    protected abstract byte[] GetHeader();

    /// <summary>序列化回调函数。</summary>
    /// <param name="stream">目标流。</param>
    /// <param name="data">要序列化的数据。</param>
    /// <returns>是否序列化数据成功。</returns>
    public delegate bool SerializeCallback(Stream stream, T data);

    /// <summary>反序列化回调函数。</summary>
    /// <param name="stream">指定流。</param>
    /// <returns>反序列化的数据。</returns>
    public delegate T DeserializeCallback(Stream stream);

    /// <summary>尝试从指定流获取指定键的值回调函数。</summary>
    /// <param name="stream">指定流。</param>
    /// <param name="key">指定键。</param>
    /// <param name="value">指定键的值。</param>
    /// <returns>是否从指定流获取指定键的值成功。</returns>
    public delegate bool TryGetValueCallback(Stream stream, string key, out object value);
  }
}
