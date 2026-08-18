// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbParamsInfoReader
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Server;
using System;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

internal class DbParamsInfoReader : IDataReader, IDisposable, IDataRecord
{
  private int _recordIndex = -1;
  private readonly string[] _fieldNames;
  private readonly DbBatchCommandParameter[] _parameters;
  private readonly int _recordCount;

  public DbParamsInfoReader(
    [NotNull] string[] fieldNames,
    [NotNull] DbBatchCommandParameter[] parameters,
    int recCount)
  {
    if (fieldNames.Length != parameters.Length)
      throw new Exception("Число полей не соответствует числу параметров");
    this._fieldNames = fieldNames;
    this._parameters = parameters;
    this._recordCount = recCount;
  }

  public bool Read()
  {
    ++this._recordIndex;
    return this._recordIndex < this._recordCount;
  }

  public void Close() => this._recordIndex = -1;

  public bool NextResult() => throw new NotImplementedException();

  public int Depth => throw new NotImplementedException();

  public DataTable GetSchemaTable() => throw new NotImplementedException();

  public bool IsClosed => throw new NotImplementedException();

  public int RecordsAffected => throw new NotImplementedException();

  public void Dispose() => this.Close();

  public int FieldCount => this._fieldNames.Length;

  public string GetName(int i) => this._fieldNames[i];

  public object GetValue(int i) => this._parameters[i].ParamValues[this._recordIndex];

  public Type GetFieldType(int i) => DbTypeMapper.Instance.GeType(this._parameters[i].ParamType);

  public bool GetBoolean(int i) => throw new NotImplementedException();

  public byte GetByte(int i) => throw new NotImplementedException();

  public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
  {
    throw new NotImplementedException();
  }

  public char GetChar(int i) => throw new NotImplementedException();

  public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
  {
    throw new NotImplementedException();
  }

  public IDataReader GetData(int i) => throw new NotImplementedException();

  public string GetDataTypeName(int i) => throw new NotImplementedException();

  public DateTime GetDateTime(int i) => throw new NotImplementedException();

  public Decimal GetDecimal(int i) => throw new NotImplementedException();

  public double GetDouble(int i) => throw new NotImplementedException();

  public float GetFloat(int i) => throw new NotImplementedException();

  public Guid GetGuid(int i) => throw new NotImplementedException();

  public short GetInt16(int i) => throw new NotImplementedException();

  public int GetInt32(int i) => throw new NotImplementedException();

  public long GetInt64(int i) => throw new NotImplementedException();

  public int GetOrdinal(string name) => Array.IndexOf<string>(this._fieldNames, name);

  public string GetString(int i) => throw new NotImplementedException();

  public int GetValues(object[] values) => throw new NotImplementedException();

  public bool IsDBNull(int i) => throw new NotImplementedException();

  public object this[string name] => throw new NotImplementedException();

  public object this[int i] => throw new NotImplementedException();
}
