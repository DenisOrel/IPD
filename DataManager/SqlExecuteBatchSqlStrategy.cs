// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.SqlExecuteBatchSqlStrategy
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class SqlExecuteBatchSqlStrategy(IDbManager dbManager) : 
  DefaultExecuteBatchSqlStrategy(dbManager)
{
  protected override void DoExecute(
    IDbCommand batchCommand,
    DbBatchCommandParameter[] parameters,
    int batchSize)
  {
    if (batchSize > 1 && this.DoExecuteBatchInsertSQL(batchCommand, parameters, batchSize))
      return;
    base.DoExecute(batchCommand, parameters, batchSize);
  }

  private bool DoExecuteBatchInsertSQL(
    IDbCommand batchCommand,
    DbBatchCommandParameter[] parameters,
    int batchSize)
  {
    string commandText = batchCommand.CommandText;
    if (commandText.IndexOf("INSERT", 0, 10, StringComparison.OrdinalIgnoreCase) == -1 || commandText.IndexOf("VALUES", StringComparison.OrdinalIgnoreCase) == -1)
      return false;
    string[] strArray1 = commandText.Split('(', ')');
    if (strArray1.Length != 5)
      return false;
    string[] strArray2 = strArray1[0].Split(' ');
    string str = strArray2[strArray2.Length - 2];
    string[] strArray3 = strArray1[1].Replace(" ", "").Split(',');
    string[] array = strArray1[3].Replace("@", "").Replace(" ", "").Split(',');
    if (strArray3.Length != array.Length)
      return false;
    List<string> stringList = new List<string>();
    foreach (DbBatchCommandParameter parameter1 in parameters)
    {
      DbBatchCommandParameter parameter = parameter1;
      int index = Array.FindIndex<string>(array, (Predicate<string>) (item => string.Compare(item, parameter.ParamName, StringComparison.OrdinalIgnoreCase) == 0));
      if (index == -1)
        throw new Exception($"Parameter '{parameter.ParamName}' not found in SQL command");
      stringList.Add(strArray3[index]);
    }
    if (stringList.Count != strArray3.Length)
      return false;
    using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection) batchCommand.Connection, SqlBulkCopyOptions.Default, batchCommand.Transaction as SqlTransaction))
    {
      bulkCopy.BatchSize = 5000;
      bulkCopy.DestinationTableName = str;
      bulkCopy.BulkCopyTimeout = batchCommand.CommandTimeout;
      stringList.ForEach((Action<string>) (item => bulkCopy.ColumnMappings.Add(item, item)));
      using (DbParamsInfoReader reader = new DbParamsInfoReader(stringList.ToArray(), parameters, batchSize))
        bulkCopy.WriteToServer((IDataReader) reader);
    }
    return true;
  }
}
