// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Sync.DataBase.IDataBase
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces.Imbase.Params.CommonParams;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server.Sync.DataBase;

public interface IDataBase
{
  IDbConnection Connection { get; }

  void ExecuteNonQuery(string sql);

  void ExecuteNonQuery(string sql, params IDbDataParameter[] parameters);

  DataTable ExecuteDataTable(string sql);

  DataTable ExecuteDataTable(string sql, params IDbDataParameter[] parameters);

  DataTable GetSchemaTable(string tableName);

  void BeginTransaction();

  void Commit();

  void Rollback();

  bool InTransaction { get; }

  void CloseConnection();

  BaseType Type { get; }

  IDbDataParameter CreateParameter(string name, object value);

  string Caption { get; }
}
