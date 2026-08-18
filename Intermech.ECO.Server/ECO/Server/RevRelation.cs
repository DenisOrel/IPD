// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevRelation
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class RevRelation(UserSession uSession, DataTable relTable) : DBRelation(uSession, relTable)
{
  public static int ecoRelType;

  protected override int DoDelete(long DeleteMode)
  {
    long num1 = 0;
    IDBAttribute attributeByGuid1 = this.GetAttributeByGuid(new Guid(LinkIzvObject.guidAttrDelWhenExcluded));
    if (attributeByGuid1 != null && attributeByGuid1.AsBoolean)
    {
      IDBAttribute attributeById = this.GetAttributeByID(LinkIzvObject.attrId);
      if (attributeById != null && attributeById.AsBoolean)
      {
        IDBAttribute attributeByGuid2 = this.GetAttributeByGuid(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"), false);
        if (attributeByGuid2 != null && attributeByGuid2.Value != DBNull.Value)
          num1 = Convert.ToInt64(attributeByGuid2.Value);
      }
    }
    int num2 = base.DoDelete(DeleteMode);
    IDBObject objectActualCopy1 = this.UserSession.GetObjectActualCopy(this.PartObjectID, false);
    if (objectActualCopy1 != null)
    {
      IDBAttribute byId = objectActualCopy1.Attributes.FindByID(ECOObject.Attr_EcoObject);
      if (byId != null)
        byId.Value = (object) DBNull.Value;
    }
    if (num1 == 0L)
      return num2;
    List<long> parentRevisions = ECOServer.GetParentRevisions((IUserSession) this.UserSession, num1);
    if (parentRevisions != null)
    {
      foreach (long num3 in parentRevisions)
      {
        if (Math.Abs(num3) != Math.Abs(this.ObjectID))
          return num2;
      }
    }
    if (RevRelation.ecoRelType == 0)
      RevRelation.ecoRelType = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
    if (num1 != 0L)
    {
      if (this.ProjID < 0L && this.UserSession.GetRelation(-this.ProjID, this.PartID, RevRelation.ecoRelType) != null)
      {
        IDBAttribute attributeByGuid3 = this.UserSession.GetObject(this.ProjID).GetAttributeByGuid(ECOObject.guidAttrDelVersionsList, false);
        List<string> stringList = new List<string>();
        for (int index = 0; index < attributeByGuid3.ValuesCount; ++index)
          stringList.Add(Convert.ToString(attributeByGuid3.Values[index]));
        string lower = this.UserSession.GetObjectInfo(num1).VersionGuid.ToString().ToLower();
        if (!stringList.Contains(lower))
          attributeByGuid3.AddValue((object) lower);
        return num2;
      }
      IDBObject objectActualCopy2 = this.UserSession.GetObjectActualCopy(num1, false);
      if (objectActualCopy2 != null && objectActualCopy2.ObjectID < 0L)
        ECOServer.DeleteObject(objectActualCopy2, 0L);
      if (this.SenderObject == null || num1 != this.SenderObject.ObjectID)
      {
        IDBObject dBObject = this.UserSession.GetObject(num1, false);
        if (!this.DontDeleteChildObjectMode && LinkIzvObject.CanDeleteObject(dBObject))
          ECOServer.DeleteObject(dBObject, 0L);
      }
    }
    return num2;
  }
}
