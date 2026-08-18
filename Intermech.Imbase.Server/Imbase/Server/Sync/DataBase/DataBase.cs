// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.DataBase
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

public class DataBase
{
  protected string Server;
  protected string Database;
  protected string User;
  protected string Password;
  protected IDbConnection DBConnection;
  protected IDbTransaction Transaction;
  private bool _inTransaction;

  public DataBase(string server, string database, string user, string password)
  {
    this.Database = database;
    this.Server = server;
    this.User = user;
    this.Password = password;
  }

  public void BeginTransaction()
  {
    if (this._inTransaction)
      throw new Exception("Transaction now is begining");
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    this.Transaction = this.DBConnection.BeginTransaction();
    this._inTransaction = true;
  }

  public void Commit()
  {
    if (!this._inTransaction)
      return;
    this.Transaction.Commit();
    this._inTransaction = false;
  }

  public void Rollback()
  {
    if (!this._inTransaction)
      return;
    this.Transaction.Rollback();
    this._inTransaction = false;
  }

  public void ExecuteNonQuery(string sql)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    IDbCommand command = this.GetCommand(sql);
    if (this._inTransaction)
      command.Transaction = this.Transaction;
    command.ExecuteNonQuery();
  }

  public void ExecuteNonQuery(string sql, params IDbDataParameter[] parameters)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    IDbCommand command = this.GetCommand(sql);
    for (int index = 0; index < parameters.Length; ++index)
      command.Parameters.Add((object) parameters[index]);
    if (this._inTransaction)
      command.Transaction = this.Transaction;
    command.ExecuteNonQuery();
  }

  public DataTable GetSchemaTable(string tableName)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    IDbCommand command = this.GetCommand($"SELECT * FROM {tableName}");
    if (this._inTransaction)
      command.Transaction = this.Transaction;
    using (IDataReader dataReader = command.ExecuteReader(CommandBehavior.SchemaOnly))
      return dataReader.GetSchemaTable();
  }

  public DataTable ExecuteDataTable(string sql)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    DataSet dataSet = new DataSet();
    IDataAdapter dataAdapter = (IDataAdapter) this.GetDataAdapter(sql);
    if (dataAdapter == null)
      return (DataTable) null;
    dataAdapter.Fill(dataSet);
    return dataSet.Tables[0];
  }

  public virtual DataTable ExecuteDataTable(string sql, params IDbDataParameter[] parameters)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    IDbCommand command = this.GetCommand();
    command.CommandText = this.UpdateCommandText(sql);
    for (int index = 0; index < parameters.Length; ++index)
      command.Parameters.Add((object) parameters[index]);
    DataSet dataSet = new DataSet();
    IDataAdapter dataAdapter = (IDataAdapter) this.GetDataAdapter(command);
    if (dataAdapter == null)
      return (DataTable) null;
    dataAdapter.Fill(dataSet);
    return dataSet.Tables[0];
  }

  public virtual string UpdateCommandText(string commandText) => commandText;

  public virtual string UpdateParameterName(string parameterName) => parameterName;

  public IDataReader ExecuteDataReader(string sql)
  {
    if (this.DBConnection.State != ConnectionState.Open)
      this.DBConnection.Open();
    IDbCommand command = this.GetCommand(sql);
    if (this._inTransaction)
      command.Transaction = this.Transaction;
    return command.ExecuteReader();
  }

  public bool InTransaction => this._inTransaction;

  public void CloseConnection() => this.DBConnection?.Close();

  protected virtual IDbDataAdapter GetDataAdapter(string sqlText) => (IDbDataAdapter) null;

  protected virtual IDbDataAdapter GetDataAdapter(IDbCommand command) => (IDbDataAdapter) null;

  protected virtual IDbCommand GetCommand() => this.DBConnection.CreateCommand();

  public IDbDataParameter CreateParameter(string name, object value)
  {
    IDbDataParameter parameter = this.CreateParameter(value);
    parameter.ParameterName = this.UpdateParameterName(name);
    return parameter;
  }

  protected virtual IDbDataParameter CreateParameter(object value) => (IDbDataParameter) null;

  protected virtual IDbCommand GetCommand(string sql)
  {
    IDbCommand command = this.DBConnection.CreateCommand();
    command.CommandText = sql;
    return command;
  }
}
