// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.CJRecord
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class CJRecord(UserSession uSession, DataTable objectsTable) : LinkIzvObject(uSession, objectsTable)
{
  protected override void DoNextLCStep(IDBLifecycleStep nextstep)
  {
    if (nextstep.LevelID != ECOServer.ecos.lcWaitingForII || ECOServer.ecos.lockDoNextLCStep)
    {
      base.DoNextLCStep(nextstep);
    }
    else
    {
      List<ECOServer.IncludedObjInfo> objectsSteps = ECOServer.ecos.GetObjectsSteps(this.Session, this.ObjectID);
      if (this.MoveObjects(nextstep, this.Session, objectsSteps, false) || this.ObjectType != ECOServer.idII && this.ObjectType != ECOServer.idPI)
        return;
      ECOServer.ecos.MoveAnnuledPI((IDBObject) this, this.Session);
    }
  }
}
