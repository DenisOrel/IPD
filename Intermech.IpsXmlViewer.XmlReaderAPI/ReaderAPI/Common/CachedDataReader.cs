// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.ReaderAPI.Common.CachedDataReader
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.Diagnostics;
using System;
using System.Data;
using System.Runtime.CompilerServices;

#nullable disable
namespace XmlReaderAPI.ReaderAPI.Common;

/// <summary>
/// Обертка над стандартным IDataReader с поддержкой кэша полей
/// </summary>
/// <remarks>Некоторые IDataReader, например SQLiteDataReader, не кэшируют наименование полей - для каждого запроса возвращают новую строку.
/// При хранении имени поля в качестве ключа это сильно увеличивает расход памяти</remarks>
/// &gt;
public sealed class CachedDataReader : IDataReader, IDisposable, IDataRecord
{
  /// <summary>
  /// 
  /// </summary>
  private IDataReader _target;
  private SchemaCacheItem _schemaCacheItem;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="target"></param>
  /// <param name="schemaCacheItem"></param>
  public CachedDataReader([NotNull] IDataReader target, SchemaCacheItem schemaCacheItem = null)
  {
    this._target = target;
    this._schemaCacheItem = schemaCacheItem ?? new SchemaCacheItem();
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
    if (this._target != null)
    {
      this._target.Dispose();
      this._target = (IDataReader) null;
    }
    this._schemaCacheItem = (SchemaCacheItem) null;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public string GetName(int i)
  {
    if (this._schemaCacheItem.FieldNames == null)
      this._schemaCacheItem.InitFieldNames(this._target);
    return this._schemaCacheItem.FieldNames[i];
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public string GetDataTypeName(int i) => this._target.GetDataTypeName(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Type GetFieldType(int i) => this._target.GetFieldType(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public object GetValue(int i) => this._target.GetValue(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int GetValues(object[] values) => this._target.GetValues(values);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int GetOrdinal(string name) => this._target.GetOrdinal(name);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool GetBoolean(int i) => this._target.GetBoolean(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public byte GetByte(int i) => this._target.GetByte(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
  {
    return this._target.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public char GetChar(int i) => this._target.GetChar(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
  {
    return this._target.GetChars(i, fieldOffset, buffer, bufferOffset, length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Guid GetGuid(int i) => this._target.GetGuid(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public short GetInt16(int i) => this._target.GetInt16(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public int GetInt32(int i) => this._target.GetInt32(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public long GetInt64(int i) => this._target.GetInt64(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float GetFloat(int i) => this._target.GetFloat(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public double GetDouble(int i) => this._target.GetDouble(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public string GetString(int i) => this._target.GetString(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Decimal GetDecimal(int i) => this._target.GetDecimal(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public DateTime GetDateTime(int i) => this._target.GetDateTime(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IDataReader GetData(int i) => this._target.GetData(i);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool IsDBNull(int i) => this._target.IsDBNull(i);

  public int FieldCount => this._target.FieldCount;

  public object this[int i] => this._target[i];

  public object this[string name] => this._target[name];

  public void Close() => this._target.Close();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public DataTable GetSchemaTable() => this._target.GetSchemaTable();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool NextResult() => this._target.NextResult();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Read() => this._target.Read();

  public int Depth => this._target.Depth;

  public bool IsClosed => this._target.IsClosed;

  public int RecordsAffected => this._target.RecordsAffected;
}
