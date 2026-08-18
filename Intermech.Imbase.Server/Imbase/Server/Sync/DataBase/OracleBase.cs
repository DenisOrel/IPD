// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.OracleBase
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Params.CommonParams;
using Intermech.Interfaces.Server;
using Intermech.Server.Data;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

internal class OracleBase : Intermech.Imbase.Server.Sync.DataBase.DataBase, IDataBase
{
  private Dictionary<TypeCode, DbType> OracleTypeMapper = new Dictionary<TypeCode, DbType>();
  private IDbDataProvider _provider;
  private IDbCommand _command;

  public OracleBase(string server, string user, string password)
    : base(server, string.Empty, user, password)
  {
    this.OracleTypeMapper.Add(TypeCode.Int32, DbType.Int32);
    this.OracleTypeMapper.Add(TypeCode.Int64, DbType.Int64);
    this.OracleTypeMapper.Add(TypeCode.String, DbType.AnsiString);
    this.OracleTypeMapper.Add(TypeCode.DateTime, DbType.DateTime);
  }

  public IDbConnection Connection
  {
    get
    {
      if (this.DBConnection == null)
      {
        string connectionString = $"Data source={this.Server};User ID={this.User};Password={this.Password};Min Pool Size=5";
        this._provider = (IDbDataProvider) new OracleDataProvider();
        this.DBConnection = this._provider.CreateConnection(connectionString);
        this._command = this.DBConnection.CreateCommand();
      }
      return this.DBConnection;
    }
  }

  public BaseType Type => BaseType.Oracle;

  protected override IDbDataParameter CreateParameter(object value)
  {
    DbType dbType;
    if (!this.OracleTypeMapper.TryGetValue(System.Type.GetTypeCode(value.GetType()), out dbType))
      return (IDbDataParameter) null;
    IDbDataParameter parameter = this._command.CreateParameter();
    parameter.DbType = dbType;
    parameter.Value = value;
    return parameter;
  }

  public string Caption => this.Server;

  protected override IDbDataAdapter GetDataAdapter(string sqlText)
  {
    IDbCommand command = this.DBConnection.CreateCommand();
    command.CommandText = sqlText;
    return this.GetDataAdapter(command);
  }

  protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
  {
    IDbDataAdapter dataAdapter = this._provider.CreateDataAdapter(this.DBConnection);
    dataAdapter.SelectCommand = command;
    return dataAdapter;
  }
}
