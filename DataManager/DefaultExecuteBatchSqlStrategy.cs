// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DefaultExecuteBatchSqlStrategy
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

public class DefaultExecuteBatchSqlStrategy(IDbManager dbManager) : ExecuteBatchSqlStrategy(dbManager)
{
  protected override void DoExecute(
    IDbCommand batchCommand,
    DbBatchCommandParameter[] parameters,
    int batchSize)
  {
    for (int index = 0; index < parameters.Length; ++index)
    {
      IDbDataParameter parameter = batchCommand.CreateParameter();
      parameter.ParameterName = this.DBManager.DataProvider.UpdateParameterName(parameters[index].ParamName);
      parameter.Direction = ParameterDirection.Input;
      parameter.DbType = parameters[index].ParamType;
      batchCommand.Parameters.Add((object) parameter);
    }
    for (int batchRecordIndex = 0; batchRecordIndex < batchSize; ++batchRecordIndex)
    {
      this.BindParameterValues(batchCommand, parameters, batchRecordIndex);
      batchCommand.ExecuteNonQuery();
    }
  }

  private void BindParameterValues(
    IDbCommand batchCommand,
    DbBatchCommandParameter[] parameters,
    int batchRecordIndex)
  {
    for (int index = 0; index < parameters.Length; ++index)
    {
      IDbDataParameter parameter = (IDbDataParameter) batchCommand.Parameters[index];
      parameter.Value = this.DBManager.DataProvider.UpdateParameterValue(parameters[index].ParamName, parameters[index].ParamValues[batchRecordIndex]);
      if (index == 0)
        this.DBManager.DataProvider.UpdateParameterTypeByValue(parameter, parameter.Value);
    }
  }
}
