// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.MssqlBase
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Params.CommonParams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

internal class MssqlBase : Intermech.Imbase.Server.Sync.DataBase.DataBase, IDataBase
{
  private Dictionary<TypeCode, SqlDbType> SQLTypeMapper = new Dictionary<TypeCode, SqlDbType>();

  public MssqlBase(string server, string database, string user, string password)
    : base(server, database, user, password)
  {
    this.SQLTypeMapper.Add(TypeCode.Int32, SqlDbType.Int);
    this.SQLTypeMapper.Add(TypeCode.Int64, SqlDbType.BigInt);
    this.SQLTypeMapper.Add(TypeCode.String, SqlDbType.NVarChar);
    this.SQLTypeMapper.Add(TypeCode.DateTime, SqlDbType.DateTime);
  }

  public IDbConnection Connection
  {
    get
    {
      if (this.DBConnection == null)
      {
        SqlConnection sqlConnection = new SqlConnection();
        SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder($"user id={this.User};password={this.Password};server={this.Server};Integrated Security=False;database={this.Database}; connection timeout=30");
        sqlConnection.ConnectionString = connectionStringBuilder.ConnectionString;
        this.DBConnection = (IDbConnection) sqlConnection;
      }
      return this.DBConnection;
    }
  }

  public BaseType Type => BaseType.MSSQL;

  protected override IDbDataParameter CreateParameter(object value)
  {
    SqlDbType sqlDbType;
    if (!this.SQLTypeMapper.TryGetValue(System.Type.GetTypeCode(value.GetType()), out sqlDbType))
      return (IDbDataParameter) null;
    SqlParameter parameter = new SqlParameter();
    parameter.SqlDbType = sqlDbType;
    parameter.Value = value;
    return (IDbDataParameter) parameter;
  }

  protected override IDbDataAdapter GetDataAdapter(string sqlText)
  {
    return (IDbDataAdapter) new SqlDataAdapter(sqlText, this.DBConnection as SqlConnection);
  }

  protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
  {
    return (IDbDataAdapter) new SqlDataAdapter((SqlCommand) command);
  }

  public override string UpdateCommandText(string commandText)
  {
    if (commandText.IndexOf(':') == -1)
      return commandText;
    bool flag = false;
    int length = commandText.Length;
    char[] charArray = commandText.ToCharArray();
    for (int index = 0; index < length; ++index)
    {
      char ch = charArray[index];
      if (ch == '\'')
        flag = !flag;
      else if (!flag && ch == ':')
        charArray[index] = '@';
    }
    return new string(charArray);
  }

  public override string UpdateParameterName(string parameterName)
  {
    if (parameterName.Length > 0 && parameterName[0] != '@')
      parameterName = parameterName[0] != ':' ? "@" + parameterName : parameterName.Replace(':', '@');
    return parameterName;
  }

  public string Caption => $"{this.Server}.{this.Database}";
}
