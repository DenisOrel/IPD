// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.ExecuteBatchSqlStrategy
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System;
using System.Data;

#nullable disable
namespace Intermech.Server.Data;

public abstract class ExecuteBatchSqlStrategy(IDbManager dbManager) : DBManagerSqlStrategy(dbManager)
{
  public void Execute(IDbCommand batchCommand, DbBatchCommandParameter[] parameters, int batchSize)
  {
    if (batchCommand == null)
      throw new ArgumentNullException(nameof (batchCommand));
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    if (batchSize < 0)
      throw new ArgumentOutOfRangeException(nameof (batchSize));
    for (int index = 0; index < parameters.Length; ++index)
    {
      if (parameters[index].ParamValues.Count != batchSize)
        throw new KernelException($"В вызове ExecuteBatchSQL указано неверное количество значений для параметра {parameters[index].ParamName}.");
    }
    if (batchSize == 0)
      return;
    this.DoExecute(batchCommand, parameters, batchSize);
  }

  protected abstract void DoExecute(
    IDbCommand batchCommand,
    DbBatchCommandParameter[] parameters,
    int batchSize);
}
