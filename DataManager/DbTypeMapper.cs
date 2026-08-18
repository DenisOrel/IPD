// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbTypeMapper
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DbTypeMapper
{
  private readonly Type[] _typeMapper = new Type[28];

  private void Initialize()
  {
    this._typeMapper[2] = typeof (byte);
    this._typeMapper[14] = typeof (sbyte);
    this._typeMapper[10] = typeof (short);
    this._typeMapper[18] = typeof (ushort);
    this._typeMapper[11] = typeof (int);
    this._typeMapper[19] = typeof (uint);
    this._typeMapper[12] = typeof (long);
    this._typeMapper[20] = typeof (ulong);
    this._typeMapper[15] = typeof (float);
    this._typeMapper[8] = typeof (double);
    this._typeMapper[7] = typeof (Decimal);
    this._typeMapper[3] = typeof (bool);
    this._typeMapper[16 /*0x10*/] = typeof (string);
    this._typeMapper[23] = typeof (char);
    this._typeMapper[9] = typeof (Guid);
    this._typeMapper[6] = typeof (DateTime);
    this._typeMapper[27] = typeof (DateTimeOffset);
    this._typeMapper[1] = typeof (byte[]);
  }

  private DbTypeMapper() => this.Initialize();

  public Type GeType(DbType dbType)
  {
    return this._typeMapper[(int) dbType] ?? throw new Exception($"Unsupported type : {dbType}");
  }

  public static DbTypeMapper Instance { get; } = new DbTypeMapper();
}
