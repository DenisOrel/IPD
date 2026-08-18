// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMObjectVersionOperations
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMObjectVersionOperations
{
  public PDMObjectVersionOperations.CanCreateVersionStatus CanCreateEditableVersion(
    PDMObject pdmObject)
  {
    if (pdmObject == null)
      throw new ArgumentNullException(nameof (pdmObject));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(pdmObject.ObjectId, true);
      ObjectModifyModes objectModifyMode = dbObject.ObjectModifyMode;
      switch (objectModifyMode)
      {
        case ObjectModifyModes.InBase:
        case ObjectModifyModes.Checkout:
        case ObjectModifyModes.CreateVersion:
          return this.CanCreateEditableVersion(pdmObject, dbObject, sessionKeeper.Session);
        case ObjectModifyModes.CantModify:
          return PDMObjectVersionOperations.CanCreateVersionStatus.CantCreate;
        default:
          throw new NotSupportedEnumException((Enum) objectModifyMode);
      }
    }
  }

  private PDMObjectVersionOperations.CanCreateVersionStatus CanCreateEditableVersion(
    PDMObject pdmObject,
    IDBObject dbObject,
    IUserSession session)
  {
    IDBLifecycleStep lifecycleStep1 = session.GetLifecycleStep(dbObject.LCStep, true);
    int firstStepId = session.GetLCSchema(lifecycleStep1.SchemaID, true).GetStepsCollection().GetFirstStep();
    IDBLifecycleStep lifecycleStep2 = session.GetLifecycleStep(firstStepId, true);
    List<PDMObjectVersionOperations.DBObjectVersionInfo> dbObjectVersions = this.ConvertToDBObjectVersions(session.GetAllObjectVersions(pdmObject.ID, true, false, false, "F_OBJECT_ID", "F_LC_STEP", "F_MODIFICATION_ID"));
    if (this.IsEditContextCreated(pdmObject, session))
    {
      if (dbObjectVersions.Find((Predicate<PDMObjectVersionOperations.DBObjectVersionInfo>) (x => x.ModificationId != 0L)) != null && (lifecycleStep2.Options & LCStepOptions.DisableContextParallelVersions) != LCStepOptions.None)
        return PDMObjectVersionOperations.CanCreateVersionStatus.RequireEditContext;
    }
    else if (dbObjectVersions.Find((Predicate<PDMObjectVersionOperations.DBObjectVersionInfo>) (x => x.ModificationId == 0L && x.LCStepId == firstStepId)) != null && (lifecycleStep2.Options & LCStepOptions.DisableParallelVersions) != LCStepOptions.None)
      return PDMObjectVersionOperations.CanCreateVersionStatus.RequireEditContext;
    return PDMObjectVersionOperations.CanCreateVersionStatus.CanCreate;
  }

  private List<PDMObjectVersionOperations.DBObjectVersionInfo> ConvertToDBObjectVersions(
    DataTable dataTable)
  {
    List<PDMObjectVersionOperations.DBObjectVersionInfo> dbObjectVersions = new List<PDMObjectVersionOperations.DBObjectVersionInfo>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      dbObjectVersions.Add(new PDMObjectVersionOperations.DBObjectVersionInfo(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]), Convert.ToInt64(row[2])));
    return dbObjectVersions;
  }

  private bool IsEditContextCreated(PDMObject pdmObject, IUserSession session)
  {
    if ((session.GetObjectType(pdmObject.ObjectType).Options & ObjectTypeOptions.AutoContextEnabled) != ObjectTypeOptions.None)
    {
      ICurrentUserAndRole userAndRoleService = pdmObject.PDMSystem.PDMSystemContext.CurrentUserAndRoleService;
      if (!Consts.IsUndefinedObjectId(userAndRoleService.CachedEditingContextID) && userAndRoleService.CachedContextMode == EditingContextMode.AutoUpdate)
        return true;
    }
    return false;
  }

  public enum CanCreateVersionStatus
  {
    CanCreate,
    RequireEditContext,
    CantCreate,
  }

  private sealed class DBObjectVersionInfo
  {
    public DBObjectVersionInfo(long objectId, int lCStepId, long modificationId)
    {
      this.ObjectId = objectId;
      this.LCStepId = lCStepId;
      this.ModificationId = modificationId;
    }

    public long ObjectId { get; }

    public int LCStepId { get; }

    public long ModificationId { get; }
  }
}
