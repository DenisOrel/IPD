// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBSchemeCategory
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBSchemeCategory(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable)
{
  public override ActionCategory GetActionCategory(ActionType actionType)
  {
    return actionType == ActionType.wfLaunchProcess ? ActionCategory.Read : base.GetActionCategory(actionType);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.wfLaunchProcess, this.GetDefaultAccess(ActionType.wfLaunchProcess));
    this.AccessActions.Add(ActionType.IncludeInComposition, false);
    this.AccessActions.Add(ActionType.ExcludeFromComposition, false);
    this.AccessActions.Remove(ActionType.View);
  }

  public override void DoBeforeCreateRelation(
    DBRelationCollection dBRelationCollection,
    long partID,
    long partObjectID,
    long prjlinkID,
    IDBRelation prototype)
  {
    this.CheckAccess(ActionType.IncludeInComposition);
    base.DoBeforeCreateRelation(dBRelationCollection, partID, partObjectID, prjlinkID, prototype);
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    this.CheckAccess(ActionType.ExcludeFromComposition);
    base.DoBeforeDeleteRelation(relation, deleteMode);
  }
}
