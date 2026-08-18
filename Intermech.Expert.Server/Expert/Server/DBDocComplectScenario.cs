// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.DBDocComplectScenario
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Expert.Scenarios;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

internal class DBDocComplectScenario : DBScenario, IDBDocComplectScenario, IDBScenario
{
  public DBDocComplectScenario(UserSession session)
    : base(session)
  {
  }

  public DBDocComplectScenario(UserSession session, DataTable objectsTable)
    : base(session, objectsTable)
  {
  }

  public long ComplectTemplateID
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(ScenarioGUIDs.attributeComplectTemplate);
      return attributeByGuid == null ? 0L : attributeByGuid.AsInteger;
    }
  }

  public override bool Execute(object session, long[] objectIDs)
  {
    return new DocComplectScenario(this.ScenarioID, ScenarioHelper.ReadCodeFromAttribute((IDBObject) this), this.ComplectTemplateID, this.Language, this.ExecSide, this.ObjectGUID).Execute((object) UserSession.GetSessionByID((Guid) session), objectIDs);
  }
}
