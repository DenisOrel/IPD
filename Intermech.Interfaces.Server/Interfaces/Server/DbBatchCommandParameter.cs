// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.DbBatchCommandParameter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public class DbBatchCommandParameter
{
  public DbBatchCommandParameter(string paramName, DbType paramType, List<object> paramValues)
  {
    if (paramName == null)
      throw new ArgumentNullException(nameof (paramName));
    if (paramValues == null)
      throw new ArgumentNullException(nameof (paramValues));
    this.ParamName = paramName;
    this.ParamType = paramType;
    this.ParamValues = paramValues;
  }

  public DbBatchCommandParameter(string paramName, DbType paramType, object initialValue)
  {
    this.ParamName = paramName != null ? paramName : throw new ArgumentNullException(nameof (paramName));
    this.ParamType = paramType;
    this.ParamValues = new List<object>();
    this.ParamValues.Add(initialValue);
  }

  public string ParamName { get; }

  public DbType ParamType { get; }

  public List<object> ParamValues { get; }
}
