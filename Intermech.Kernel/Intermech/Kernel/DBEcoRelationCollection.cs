// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBEcoRelationCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using System;


namespace Intermech.Kernel;

internal class DBEcoRelationCollection(UserSession uSession, int relationType) : DBRelationCollection(uSession, relationType)
{
  public static readonly string guidAttrIncludeGoal = "cad007a3-306c-11d8-b4e9-00304f19f545";
  public static int idAttrIncludeGoal = -1;

  public override IDBRelation Create(
    DateTime beginDate,
    long projectID,
    long partID,
    long prjlinkID,
    long partObjectID,
    IDBRelation prototype,
    Guid relationGUID,
    AttributeValues[] vals = null)
  {
    if (partObjectID != 0L)
    {
      IDBEditingContextsObject editingContextsObject = this.Session.GetObject(projectID, false) as IDBEditingContextsObject;
      IDBObject dbObject = this.Session.GetObject(partObjectID, false);
      if (DBEcoRelationCollection.idAttrIncludeGoal == -1)
        DBEcoRelationCollection.idAttrIncludeGoal = MetaDataHelper.GetAttributeTypeID(DBEcoRelationCollection.guidAttrIncludeGoal);
      int num = -1;
      if (vals != null)
      {
        foreach (AttributeValues val in vals)
        {
          if (val.AttributeID == DBEcoRelationCollection.idAttrIncludeGoal)
          {
            num = Convert.ToInt32(val.Values[0]);
            break;
          }
        }
      }
      if (editingContextsObject != null && dbObject != null && num != 1 && dbObject.ModificationID != 0L && Math.Abs(editingContextsObject.LinkedContextNumber) != Math.Abs(dbObject.ModificationID))
        throw new KernelExceptionID(sc_13535.ssp_appserver_13536(1614645450), (object) partObjectID).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(partObjectID));
    }
    return base.Create(beginDate, projectID, partID, prjlinkID, partObjectID, prototype, relationGUID);
  }
}
