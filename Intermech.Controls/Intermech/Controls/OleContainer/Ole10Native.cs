
// Type: Intermech.Controls.OleContainer.Ole10Native
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.IO;
using System.Text;


namespace Intermech.Controls.OleContainer;

/// <summary>Ole10Native data stucture</summary>
internal class Ole10Native : IDisposable
{
  /// <summary>Total stream size including this field</summary>
  private readonly int _totalSize;
  /// <summary>Some flags. Mostly, 02 00</summary>
  private readonly short _flags1;
  /// <summary>ASCIIZ</summary>
  private readonly string _label;
  /// <summary>ASCIIZ</summary>
  private readonly string _fileName;
  /// <summary>Also some flags. Mostly, 00 00</summary>
  private readonly short _flags2;
  /// <summary>Unknown</summary>
  private readonly byte[] _unknown1;
  /// <summary>Unknown. Mostly, 00 00 00</summary>
  private readonly byte[] _unknown2;
  /// <summary>ASCIIZ</summary>
  private readonly string _command;
  /// <summary>It's actual data size. We need this.</summary>
  private readonly int _nativeDataSize;
  /// <summary>Actual data.</summary>
  private readonly byte[] _nativeData;
  /// <summary>
  /// 
  /// </summary>
  private readonly short _unknown3;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  private static string ReadString(BinaryReader reader)
  {
    StringBuilder stringBuilder = new StringBuilder();
    byte num;
    while ((num = reader.ReadByte()) != (byte) 0)
      stringBuilder.Append((char) num);
    return stringBuilder.ToString();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="inputStream"></param>
  public Ole10Native(Stream inputStream)
  {
    BinaryReader reader = new BinaryReader(inputStream);
    this._totalSize = reader.ReadInt32();
    if (this._totalSize < 4)
      throw new InvalidDataException($"Invalid total data size: {this._totalSize}.");
    this._flags1 = reader.ReadInt16();
    this._label = Ole10Native.ReadString(reader);
    this._fileName = Ole10Native.ReadString(reader);
    this._flags2 = reader.ReadInt16();
    byte count = reader.ReadByte();
    this._unknown1 = reader.ReadBytes((int) count);
    this._unknown2 = reader.ReadBytes(3);
    this._command = Ole10Native.ReadString(reader);
    this._nativeDataSize = reader.ReadInt32();
    this._nativeData = this._nativeDataSize <= this._totalSize && this._nativeDataSize >= 0 ? reader.ReadBytes(this._nativeDataSize) : throw new InvalidDataException($"Invalid native data size: {this._nativeDataSize}.");
    this._unknown3 = this._unknown1.Length != 0 ? reader.ReadInt16() : (short) 0;
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
  }

  /// <summary>Total stream size including this field</summary>
  public int TotalSize => this._totalSize;

  /// <summary>
  /// 
  /// </summary>
  public string Label => this._label;

  /// <summary>
  /// 
  /// </summary>
  public string FileName => this._fileName;

  /// <summary>Actual data.</summary>
  public byte[] NativeData => this._nativeData;
}
