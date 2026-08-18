
// Type: Intermech.PropertyEditors.ObjTypeApplList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Data;


namespace Intermech.PropertyEditors;

public class ObjTypeApplList : ArrayList
{
  public RelationTypeMember relationTypeMember;

  public bool Load(RelationTypeMember rtm)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.relationTypeMember = rtm;
      this.Clear();
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      int num1 = -1;
      int num2 = -1;
      if (this.relationTypeMember.isReversed)
        num1 = this.relationTypeMember.objType;
      else
        num2 = this.relationTypeMember.objType;
      int relType = this.relationTypeMember.relType;
      int objectType = num1;
      int inObjectType = num2;
      foreach (DataRow row in (InternalDataCollectionBase) applicabilityCollection.GetApplicabilitiesList(relType, objectType, inObjectType).Rows)
        this.Add((object) new ObjTypeApplMember(this, Convert.ToInt32(row["F_APPLICABILITY_ID"]), Convert.ToInt32(row["F_INOBJECT_TYPE"]), Convert.ToInt32(row["F_OBJECT_TYPE"]), Convert.ToInt32(row["F_RELATION_TYPE"]), (InheritModes) Convert.ToInt16(row["F_PUBLIC"])));
      return true;
    }
  }
}
