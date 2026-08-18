// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpObjInfo
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

[Serializable]
public class ExpObjInfo
{
  public long objID = -1;
  public long templateID = -1;
  public string scriptName = "";
  public string templateName = "";
  public byte[] zippedScript;
  public Dictionary<int, GuidAndName> attrTypes = new Dictionary<int, GuidAndName>();
  public Dictionary<int, GuidAndName> objTypes = new Dictionary<int, GuidAndName>();
  public Dictionary<int, GuidAndName> relTypes = new Dictionary<int, GuidAndName>();
  public Dictionary<long, GuidAndName> objIdents = new Dictionary<long, GuidAndName>();

  public bool AddAttrType(int attrTypeId, Guid g)
  {
    if (attrTypeId == 0 || attrTypeId == -1 || attrTypeId == -10000 || g.Equals(Guid.Empty))
      return false;
    if (!this.attrTypes.ContainsKey(attrTypeId))
    {
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(attrTypeId);
      this.attrTypes.Add(attrTypeId, new GuidAndName(g, attributeTypeName));
    }
    return true;
  }

  public bool AddAttrType(int attrTypeId)
  {
    if (attrTypeId == -1 || attrTypeId == 0 || attrTypeId == -10000)
      return false;
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
    if (attributeTypeGuid.Equals(Guid.Empty))
      return false;
    if (!this.attrTypes.ContainsKey(attrTypeId))
    {
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(attrTypeId);
      this.attrTypes.Add(attrTypeId, new GuidAndName(attributeTypeGuid, attributeTypeName));
    }
    return true;
  }

  public bool AddAttrType(Guid g)
  {
    if (g.Equals(Guid.Empty))
      return false;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(g);
    switch (attributeTypeId)
    {
      case -10000:
      case -1:
      case 0:
        return false;
      default:
        if (!this.attrTypes.ContainsKey(attributeTypeId))
        {
          string attributeTypeName = MetaDataHelper.GetAttributeTypeName(attributeTypeId);
          this.attrTypes.Add(attributeTypeId, new GuidAndName(g, attributeTypeName));
        }
        return true;
    }
  }

  public bool AddAttrType(string s)
  {
    Guid result = Guid.Empty;
    return !Guid.TryParse(s, out result) || this.AddAttrType(result);
  }

  public bool AddObjType(int objTypeId, Guid g)
  {
    if (objTypeId == 0 || objTypeId == -1 || g.Equals(Guid.Empty))
      return false;
    if (!this.objTypes.ContainsKey(objTypeId))
    {
      string objectTypeName = MetaDataHelper.GetObjectTypeName(objTypeId);
      this.objTypes.Add(objTypeId, new GuidAndName(g, objectTypeName));
    }
    return true;
  }

  public bool AddObjType(int objTypeId)
  {
    if (objTypeId == -1 || objTypeId == 0)
      return false;
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objTypeId);
    if (objectTypeGuid.Equals(Guid.Empty))
      return false;
    if (!this.objTypes.ContainsKey(objTypeId))
    {
      string objectTypeName = MetaDataHelper.GetObjectTypeName(objTypeId);
      this.objTypes.Add(objTypeId, new GuidAndName(objectTypeGuid, objectTypeName));
    }
    return true;
  }

  public bool AddObjType(Guid g)
  {
    if (g.Equals(Guid.Empty))
      return false;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(g);
    switch (objectTypeId)
    {
      case -1:
      case 0:
        return false;
      default:
        if (!this.objTypes.ContainsKey(objectTypeId))
        {
          string objectTypeName = MetaDataHelper.GetObjectTypeName(objectTypeId);
          this.objTypes.Add(objectTypeId, new GuidAndName(g, objectTypeName));
        }
        return true;
    }
  }

  public bool AddObjType(string s)
  {
    Guid result = Guid.Empty;
    return !Guid.TryParse(s, out result) || this.AddObjType(result);
  }

  public bool AddObjLink(long objId, Guid g, string Name)
  {
    if (objId == 0L || objId == -1L || g.Equals(Guid.Empty))
      return false;
    if (!this.objIdents.ContainsKey(Math.Abs(objId)))
      this.objIdents.Add(Math.Abs(objId), new GuidAndName(g, Name));
    return true;
  }

  public bool AddObjLink(long objId, IUserSession ius)
  {
    if (objId == 0L || objId == -1L)
      return false;
    QuickObjectInfo objectInfo = ius.GetObjectInfo(objId);
    Guid empty = Guid.Empty;
    string caption = objectInfo.Caption;
    long num;
    Guid g1;
    if (objectInfo.Empty)
    {
      IDBObject objectById = ius.GetObjectByID(objId, false);
      if (objectById == null)
        return false;
      num = objectById.ID;
      g1 = objectById.GUID;
      caption = objectById.Caption;
    }
    else
    {
      num = Math.Abs(objectInfo.ObjectID);
      g1 = objectInfo.VersionGuid;
    }
    if (!this.objIdents.ContainsKey(Math.Abs(num)))
      this.objIdents.Add(Math.Abs(num), new GuidAndName(g1, caption));
    return true;
  }

  public bool AddObjLink(Guid objGuid, IUserSession ius)
  {
    if (objGuid.Equals(Guid.Empty))
      return false;
    QuickObjectInfo objectInfo = ius.GetObjectInfo(objGuid);
    Guid empty = Guid.Empty;
    string caption = objectInfo.Caption;
    long key;
    Guid g1;
    if (objectInfo.Empty)
    {
      IDBObject objectById = ius.GetObjectByID(objGuid, false);
      if (objectById == null)
        return false;
      key = objectById.ID;
      g1 = objectById.GUID;
      caption = objectById.Caption;
    }
    else
    {
      key = Math.Abs(objectInfo.ObjectID);
      g1 = objectInfo.VersionGuid;
    }
    if (!this.objIdents.ContainsKey(key))
      this.objIdents.Add(key, new GuidAndName(g1, caption));
    return true;
  }

  public bool AddRelType(int relTypeId, Guid g)
  {
    if (relTypeId == 0 || relTypeId == -1 || g.Equals(Guid.Empty))
      return false;
    if (!this.relTypes.ContainsKey(relTypeId))
    {
      string relationTypeName = MetaDataHelper.GetRelationTypeName(relTypeId);
      this.relTypes.Add(relTypeId, new GuidAndName(g, relationTypeName));
    }
    return true;
  }

  public bool AddRelType(int relTypeId)
  {
    if (relTypeId == -1 || relTypeId == 0)
      return false;
    Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(relTypeId);
    if (relationTypeGuid.Equals(Guid.Empty))
      return false;
    if (!this.relTypes.ContainsKey(relTypeId))
    {
      string relationTypeName = MetaDataHelper.GetRelationTypeName(relTypeId);
      this.relTypes.Add(relTypeId, new GuidAndName(relationTypeGuid, relationTypeName));
    }
    return true;
  }

  public bool AddRelType(Guid g)
  {
    if (g.Equals(Guid.Empty))
      return false;
    int relationTypeId = MetaDataHelper.GetRelationTypeID(g);
    switch (relationTypeId)
    {
      case -1:
      case 0:
        return false;
      default:
        if (!this.relTypes.ContainsKey(relationTypeId))
        {
          string relationTypeName = MetaDataHelper.GetRelationTypeName(relationTypeId);
          this.relTypes.Add(relationTypeId, new GuidAndName(g, relationTypeName));
        }
        return true;
    }
  }

  public ExpObjInfo(long aID) => this.objID = aID;
}
