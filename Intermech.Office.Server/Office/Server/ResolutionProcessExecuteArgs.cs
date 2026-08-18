// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.ResolutionProcessExecuteArgs
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class ResolutionProcessExecuteArgs
{
  public long OfficeDocID { get; }

  [NotNull]
  public IList<long> ExecutorIDs { get; }

  public DateTime PlannedDate { get; }

  public long ControlUserID { get; }

  public ResolutionProcessExecuteArgs(
    long officeDocID,
    [NotNull] IList<long> executorIDs,
    DateTime plannedDate,
    long controlUserID)
  {
    this.OfficeDocID = officeDocID;
    this.ExecutorIDs = executorIDs;
    this.PlannedDate = plannedDate;
    this.ControlUserID = controlUserID;
  }
}
