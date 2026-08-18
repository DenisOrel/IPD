// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.InterbaseBase
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Params.CommonParams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

public class InterbaseBase : Intermech.Imbase.Server.Sync.DataBase.DataBase, IDataBase
{
  private Dictionary<TypeCode, DbType> InterbaseTypeMapper = new Dictionary<TypeCode, DbType>();
  public static string DefCharset = "NONE";

  public InterbaseBase(string server, string database, string user, string password)
    : base(server, database, user, password)
  {
    this.InterbaseTypeMapper.Add(TypeCode.Int32, DbType.Int32);
    this.InterbaseTypeMapper.Add(TypeCode.Int64, DbType.Int64);
    this.InterbaseTypeMapper.Add(TypeCode.String, DbType.AnsiString);
    this.InterbaseTypeMapper.Add(TypeCode.DateTime, DbType.DateTime);
  }

  public IDbConnection Connection
  {
    get
    {
      if (this.DBConnection == null)
      {
        string str = "LCPI.IBProvider";
        DataTable elements = new OleDbEnumerator().GetElements();
        bool flag = false;
        foreach (DataRow row in (InternalDataCollectionBase) elements.Rows)
        {
          if (Convert.ToString(row["SOURCES_NAME"]).IndexOf(str) >= 0)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          throw new Exception("В системе не найден провайдер IBProvider для работы с БД Interbase. Для дальнейшей работы необходимо установить IBProvider из папки Files дистрибутива IPS.");
        OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder()
        {
          Provider = str
        };
        connectionStringBuilder.Add("Location", this.Server != string.Empty ? (object) $"{this.Server}:{this.Database}" : (object) ("localhost:" + this.Database));
        connectionStringBuilder.Add("User ID", (object) this.User);
        connectionStringBuilder.Add("Password", (object) this.Password);
        connectionStringBuilder.Add("ctype", (object) "win1251");
        this.DBConnection = (IDbConnection) new OleDbConnection(connectionStringBuilder.ToString() + ";auto_commit=true");
      }
      return this.DBConnection;
    }
  }

  public BaseType Type => BaseType.Interbase;

  protected override IDbDataParameter CreateParameter(object value)
  {
    DbType dbType;
    if (!this.InterbaseTypeMapper.TryGetValue(System.Type.GetTypeCode(value.GetType()), out dbType))
      return (IDbDataParameter) null;
    OleDbParameter parameter = new OleDbParameter();
    parameter.DbType = dbType;
    parameter.Value = value;
    parameter.SourceColumn = (string) null;
    return (IDbDataParameter) parameter;
  }

  protected override IDbDataAdapter GetDataAdapter(string sqlText)
  {
    return (IDbDataAdapter) new OleDbDataAdapter(sqlText, this.DBConnection as OleDbConnection);
  }

  protected override IDbDataAdapter GetDataAdapter(IDbCommand command)
  {
    return (IDbDataAdapter) new OleDbDataAdapter(command as OleDbCommand);
  }

  protected override IDbCommand GetCommand()
  {
    return (IDbCommand) new OleDbCommand()
    {
      Connection = (this.DBConnection as OleDbConnection)
    };
  }

  protected override IDbCommand GetCommand(string sql)
  {
    return (IDbCommand) new OleDbCommand(sql, this.DBConnection as OleDbConnection);
  }

  public override string UpdateCommandText(string commandText) => commandText;

  public override string UpdateParameterName(string parameterName) => parameterName;

  public string Caption
  {
    get => !(this.Server != string.Empty) ? this.Database : $"{this.Server}:{this.Database}";
  }
}
