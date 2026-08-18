// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbManagerCommandResult
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Server.Data;

public class DbManagerCommandResult
{
  private object returnValue;
  private List<IDbDataParameter> extraOutputParameters;
  private static readonly List<IDbDataParameter> emptyOutputParameters = new List<IDbDataParameter>(0);

  public DbManagerCommandResult(object returnValue)
  {
    this.returnValue = returnValue;
    this.extraOutputParameters = DbManagerCommandResult.emptyOutputParameters;
  }

  public object ReturnValue
  {
    [DebuggerStepThrough] get => this.returnValue;
    [DebuggerStepThrough] set => this.returnValue = value;
  }

  public IReadOnlyList<IDbDataParameter> ExtraOutputParameters
  {
    [DebuggerStepThrough] get => (IReadOnlyList<IDbDataParameter>) this.extraOutputParameters;
  }

  public void AddExtraOutputParameter(IDbDataParameter outputParameter)
  {
    if (outputParameter == null)
      throw new ArgumentNullException(nameof (outputParameter));
    if (outputParameter.Direction != ParameterDirection.Output)
      throw new ArgumentException("Invalid parameter direction.", nameof (outputParameter));
    if (this.extraOutputParameters == DbManagerCommandResult.emptyOutputParameters)
      this.extraOutputParameters = new List<IDbDataParameter>();
    this.extraOutputParameters.Add(outputParameter);
  }
}
