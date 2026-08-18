// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DBManagerLoggers
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Server.Data;

public sealed class DBManagerLoggers
{
  private object _syncRoot;
  private List<IDbManagerLogger> _loggers;
  private int _loggerCountNoLock;
  private int _logStringCount;
  private int _enabledFlagNoLock;

  public DBManagerLoggers()
  {
    this._syncRoot = new object();
    this._loggers = new List<IDbManagerLogger>();
    this._loggerCountNoLock = 0;
    this._logStringCount = 0;
    this._enabledFlagNoLock = 0;
  }

  public bool Enabled
  {
    [DebuggerStepThrough] get => this._enabledFlagNoLock != 0;
    [DebuggerStepThrough] set => Interlocked.Exchange(ref this._enabledFlagNoLock, value ? 1 : 0);
  }

  private int LoggerCount => this._loggerCountNoLock;

  public void Add(IDbManagerLogger logger)
  {
    if (logger == null)
      throw new ArgumentNullException(nameof (logger));
    lock (this._syncRoot)
    {
      if (this._loggers.Contains(logger))
        return;
      this._loggers.Add(logger);
      Interlocked.Increment(ref this._loggerCountNoLock);
    }
  }

  public void Remove(IDbManagerLogger logger)
  {
    if (logger == null)
      throw new ArgumentNullException(nameof (logger));
    lock (this._syncRoot)
    {
      if (!this._loggers.Remove(logger))
        return;
      Interlocked.Decrement(ref this._loggerCountNoLock);
    }
  }

  internal string CommandParamsToString(IDbCommand command)
  {
    string str1 = string.Empty;
    IDataParameterCollection parameters = command.Parameters;
    int num = 1;
    foreach (object obj1 in (IEnumerable) parameters)
    {
      if (obj1 is DbParameter)
      {
        DbParameter dbParameter = obj1 as DbParameter;
        object obj2 = dbParameter.Value;
        string str2 = "(NULL)";
        if (obj2 != null)
          str2 = obj2.ToString();
        string str3 = $" Param = {num++}, Name = {dbParameter.ParameterName}, Type = {dbParameter.DbType.ToString()}, Data = {str2}";
        str1 = str1.Length != 0 ? str1 + Environment.NewLine + str3 : str3;
      }
    }
    return str1;
  }

  internal string[] CommandBatchParamsToStrings(DbBatchCommandParameter[] batchParams)
  {
    if (batchParams == null || batchParams.Length == 0)
      return new string[0];
    List<string> stringList = new List<string>(batchParams.Length);
    for (int index1 = 0; index1 < batchParams[0].ParamValues.Count; ++index1)
    {
      for (int index2 = 0; index2 < batchParams.Length; ++index2)
      {
        DbBatchCommandParameter batchParam = batchParams[index2];
        object paramValue = batchParam.ParamValues[index1];
        string str = paramValue != null ? paramValue.ToString() : "(NULL)";
        stringList.Add(string.Format(" Param = [{0}][{4}], Name = {1}, Type = {2}, Data = {3}", (object) index2, (object) batchParam.ParamName, (object) batchParam.ParamType.ToString(), (object) str, (object) index1));
      }
    }
    return stringList.ToArray();
  }

  internal void Log(
    DbManagerLogType type,
    DbManager dbManager,
    IDbCommand command,
    TimeSpan span,
    string[] addData = null)
  {
    if (!this.Enabled || this.LoggerCount == 0)
      return;
    lock (this._syncRoot)
    {
      if (this._loggers.Count == 0)
        return;
      this.LogInternal(type, dbManager, command, span, addData);
    }
  }

  private void LogInternal(
    DbManagerLogType type,
    DbManager dbManager,
    IDbCommand command,
    TimeSpan span,
    string[] addData = null)
  {
    List<string> stringList1 = new List<string>(8);
    if (type == DbManagerLogType.ElapsedTime)
    {
      List<string> stringList2 = stringList1;
      object[] objArray = new object[7];
      int index = Thread.CurrentThread.GetHashCode();
      objArray[0] = (object) index.ToString();
      objArray[1] = (object) dbManager.TransactionDepth;
      objArray[2] = (object) DateTime.Now.ToString();
      objArray[3] = (object) span.ToString();
      objArray[4] = (object) type.ToString();
      objArray[5] = (object) "*----------";
      objArray[6] = (object) this._logStringCount;
      string str1 = string.Format("{6:0000000}:{0} {1} {2} {3} : {4} {5}", objArray);
      stringList2.Add(str1);
      if (addData != null)
      {
        string[] strArray = addData;
        for (index = 0; index < strArray.Length; ++index)
        {
          string str2 = strArray[index];
          stringList1.Add(str2);
        }
      }
    }
    else
    {
      List<string> stringList3 = stringList1;
      object[] objArray = new object[7];
      int num = Thread.CurrentThread.GetHashCode();
      objArray[0] = (object) num.ToString();
      objArray[1] = (object) dbManager.TransactionDepth;
      objArray[2] = (object) DateTime.Now.ToString();
      objArray[3] = (object) "*";
      objArray[4] = (object) type.ToString();
      objArray[5] = (object) command.CommandText;
      num = ++this._logStringCount;
      objArray[6] = (object) num;
      string str3 = string.Format("{6:0000000}:{0} {1} {2} {3} : {4} {5}", objArray);
      stringList3.Add(str3);
      string str4 = this.CommandParamsToString(command);
      if (str4.Length > 0)
        stringList1.Add(str4);
      if (addData != null)
      {
        foreach (string str5 in addData)
          stringList1.Add(str5);
      }
    }
    foreach (IDbManagerLogger logger in this._loggers)
      logger.AddToLog(stringList1.ToArray());
  }
}
