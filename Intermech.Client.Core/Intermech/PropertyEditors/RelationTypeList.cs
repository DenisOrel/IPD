
// Type: Intermech.PropertyEditors.RelationTypeList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.Data;


namespace Intermech.PropertyEditors;

public class RelationTypeList : ArrayList
{
  private int objType;

  public int ObjType => this.objType;

  public RelationTypeList(int aObjType) => this.objType = aObjType;

  public void Fill()
  {
    this.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) DataHolders.RelationTypesHolder.DataTable.Rows)
    {
      this.Add((object) new RelationTypeMember(this.objType, Convert.ToInt32(row["F_RELATION_TYPE"]), row["F_DESCRIPTION"].ToString(), row["F_TYPE_NAME"].ToString(), (RelationKinds) Convert.ToInt32(row["F_RELATION_KIND"]), false));
      if (Convert.ToInt32(row["F_RELATION_KIND"]) == 0)
        this.Add((object) new RelationTypeMember(this.objType, Convert.ToInt32(row["F_RELATION_TYPE"]), row["F_DESCRIPTION"].ToString(), row["F_REVERSE_NAME"].ToString(), (RelationKinds) Convert.ToInt32(row["F_RELATION_KIND"]), true));
    }
  }

  public RelationTypeMember GetMemberByRel(int aRelType, bool aIsReversed)
  {
    RelationTypeMember memberByRel = (RelationTypeMember) null;
    for (int index = 0; index < this.Count; ++index)
    {
      if (((RelationTypeMember) this[index]).relType == aRelType && ((RelationTypeMember) this[index]).isReversed == aIsReversed)
      {
        memberByRel = (RelationTypeMember) this[index];
        break;
      }
    }
    return memberByRel;
  }

  public bool CheckRelationInfo(RelationTypeMember rtm, bool reread)
  {
    if (this.IndexOf((object) rtm) == -1)
      return false;
    if (!rtm.isLoaded | reread)
      rtm.Load();
    return true;
  }
}
