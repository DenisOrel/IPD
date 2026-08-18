// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMAttributeManager
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMAttributeManager
{
  private const GetAttributeValuesModes RetrieveNames = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.CheckVisibility;
  private const GetAttributeValuesModes RetrieveValues = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.CheckVisibility;
  private readonly IDBObjectRef pdmObject;

  public PDMAttributeManager(IDBObjectRef pdmObject)
  {
    this.pdmObject = pdmObject != null ? pdmObject : throw new ArgumentNullException(nameof (pdmObject));
  }

  public List<string> GetAttributeNames()
  {
    return DBAttributeHelper.GetAttributeLayout((IDBAttributableTypeRef) new DirectObjectAttributesRef(DBHelper.GetObjectType(this.pdmObject.GetObjectId())), RequiredModes.AutoRequired, RequiredModes.Auto, RequiredModes.Manual).ConvertAll<string>((Converter<StringKey, string>) (key => (string) key));
  }

  public List<ValueRecord> GetAttributes(List<string> attrNames)
  {
    if (attrNames == null)
      throw new ArgumentNullException(nameof (attrNames));
    int objectType;
    AttributeValues[] attributesValues;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.pdmObject.GetObjectId(), true);
      objectType = dbObject.ObjectType;
      attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess);
    }
    List<ValueRecord> attributes = DBAttributeHelper.ReadEntityValues((IDBAttributableTypeRef) new DirectObjectAttributesRef(objectType), (ICollection<AttributeValues>) attributesValues);
    List<string> lookupTable = new List<string>((IEnumerable<string>) attrNames);
    lookupTable.Sort((IComparer<string>) StringKey.Comparer);
    attributes.RemoveAll((Predicate<ValueRecord>) (item => lookupTable.BinarySearch((string) item.Key, (IComparer<string>) StringKey.Comparer) < 0));
    return attributes;
  }

  public void SetParameters(List<ValueRecord> pdmAttrs)
  {
    AttributeValues[] valuesList = pdmAttrs != null ? DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) pdmAttrs) : throw new ArgumentNullException(nameof (pdmAttrs));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(this.pdmObject.GetObjectId(), true).SetAttributesValues(valuesList);
  }

  public void DeleteParameters(List<string> attrNames)
  {
    if (attrNames == null)
      throw new ArgumentNullException(nameof (attrNames));
    using (SessionKeeper keeper = new SessionKeeper())
    {
      IDBObject dbObj = keeper.Session.GetObject(this.pdmObject.GetObjectId());
      IDBObjectType dbObjType = keeper.Session.GetObjectType(dbObj.ObjectType);
      Predicate<string> match = (Predicate<string>) (attrName =>
      {
        IDBAttributeType attributeType = keeper.Session.GetAttributeType(attrName, false);
        if (attributeType == null)
          return false;
        IDBAttributeType4 attributeById = dbObjType.Attributes.GetAttributeByID(attributeType.AttributeID, false);
        return attributeById == null || attributeById.Required != RequiredModes.AutoRequired;
      });
      attrNames = attrNames.FindAll(match);
      if (attrNames.Count <= 0)
        return;
      dbObj = keeper.Session.GetObject(this.pdmObject.GetObjectId());
      Action<string> action = (Action<string>) (attrName => dbObj.GetAttributeByName(attrName, false)?.Delete(0L));
      attrNames.ForEach(action);
    }
  }
}
