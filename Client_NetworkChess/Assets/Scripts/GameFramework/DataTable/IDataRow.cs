// Decompiled with JetBrains decompiler
// Type: GameFramework.DataTable.IDataRow
// Assembly: GameFramework, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA300501-4AB4-4423-A8EA-E080265B9678
// Assembly location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.dll
// XML documentation location: D:\ProjectWorkspace\Test\NetworkChess\Client_NetworkChess\Assets\ThirdPart\UnityGameFramework-master\Libraries\GameFramework.xml

namespace GameFramework.DataTable
{
  /// <summary>数据表行接口。</summary>
  public interface IDataRow
  {
    /// <summary>获取数据表行的编号。</summary>
    int Id { get; }

    /// <summary>解析数据表行。</summary>
    /// <param name="dataRowString">要解析的数据表行字符串。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析数据表行成功。</returns>
    bool ParseDataRow(string dataRowString, object userData);

    /// <summary>解析数据表行。</summary>
    /// <param name="dataRowBytes">要解析的数据表行二进制流。</param>
    /// <param name="startIndex">数据表行二进制流的起始位置。</param>
    /// <param name="length">数据表行二进制流的长度。</param>
    /// <param name="userData">用户自定义数据。</param>
    /// <returns>是否解析数据表行成功。</returns>
    bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData);
  }
}
