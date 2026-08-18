// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.ObjectTemplater
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Helpers;

internal class ObjectTemplater : DBSessionable
{
  private Dictionary<long, long> LinkedObjectsCache = new Dictionary<long, long>();
  private ArrayList IDs = new ArrayList();
  private bool CreateAllObject;
  private DBObject _RootObject;
  private int NextNumber;
  private long LastObjectID;

  public ObjectTemplater(DBObject rootObject, UserSession session)
    : base(session)
  {
    this._RootObject = rootObject;
  }

  public override long ObjectID => this._RootObject.ObjectID;

  public Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(long templateID)
  {
    this.CreateAllObject = true;
    Dictionary<int, List<CreatedProjectData>> resultData = new Dictionary<int, List<CreatedProjectData>>();
    DataTable dataTable = this.UserSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00814-306c-11d8-b4e9-00304f19f545")).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    }, new SortOrders[1]{ SortOrders.ASC }), templateID);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[1]));
        IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
        bool create = attributeByGuid1 == null || attributeByGuid1.AsBoolean;
        IDBAttribute attributeByGuid2 = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
        string sort = attributeByGuid2 != null ? attributeByGuid2.AsString : string.Empty;
        this.CreateTemplateObjects(Convert.ToInt64(row[0]), this.ObjectID, sort, create, resultData);
      }
    }
    return resultData;
  }

  public Dictionary<int, List<CreatedProjectData>> AddTemplateObjects(
    ArrayList _IDs,
    long templateID)
  {
    string empty = string.Empty;
    Dictionary<int, List<CreatedProjectData>> resultData = new Dictionary<int, List<CreatedProjectData>>();
    this.IDs = _IDs;
    DataTable dataTable = this.UserSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00814-306c-11d8-b4e9-00304f19f545")).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    }, new SortOrders[1]{ SortOrders.ASC }), templateID);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        int int32 = Convert.ToInt32(row[2]);
        foreach (long[] id in this.IDs)
        {
          if (id[0] == int64 && id[1] == templateID)
          {
            IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row[1]));
            bool create;
            if (!MetaDataHelper.IsObjectTypeChildOf(int32, MetaDataHelper.GetObjectTypeID("cad00822-306c-11d8-b4e9-00304f19f545")))
            {
              IDBAttribute attributeByGuid = relation.GetAttributeByGuid(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
              create = attributeByGuid == null || attributeByGuid.AsBoolean;
            }
            else
              create = true;
            IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
            string sort = attributeByGuid1 != null ? attributeByGuid1.AsString : string.Empty;
            this.CreateTemplateObjects(int64, this.ObjectID, sort, create, resultData);
            break;
          }
        }
      }
    }
    return resultData;
  }

  private void CreateTemplateObjects(
    long objectID,
    long inObjectID,
    string sort,
    bool create,
    Dictionary<int, List<CreatedProjectData>> resultData)
  {
    int objectType = this.UserSession.GetObject(inObjectID).ObjectType;
    IDBObject dbObject1 = this.UserSession.GetObject(objectID);
    int num1;
    if (MetaDataHelper.IsObjectTypeChildOf(dbObject1.ObjectType, MetaDataHelper.GetObjectTypeID("cad00822-306c-11d8-b4e9-00304f19f545")))
    {
      string asString = dbObject1.GetAttributeByGuid(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545")).AsString;
      num1 = GuidHelper.IsGuid(asString) ? MetaDataHelper.GetObjectTypeID(asString) : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1008"), (object) dbObject1.Caption));
    }
    else
      num1 = dbObject1.ObjectType;
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00814-306c-11d8-b4e9-00304f19f545");
    IDBRelationsApplicability applicability = this.UserSession.GetRelationsApplicabilityCollection().GetApplicability(-1, num1, objectType);
    if (applicability == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_903"), (object) MetaDataHelper.GetObjectTypeName(num1), (object) MetaDataHelper.GetObjectTypeName(objectType)));
    if (create)
    {
      IDBObject dbObject2 = this.UserSession.GetObjectCollection(num1).Create(objectID);
      if (dbObject2 == null)
        return;
      long objectId1 = dbObject2.ObjectID;
      if (MetaDataHelper.IsObjectTypeChildOf(this._RootObject.ObjectType, new Guid("cad00812-306c-11d8-b4e9-00304f19f545")))
        dbObject2.ProjectID = this._RootObject.ObjectID;
      IDBAttribute attributeByGuid1 = this.UserSession.GetRelationCollection(applicability.RelationType).Create(inObjectID, objectId1).GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid1 != null)
      {
        if (sort != string.Empty)
          attributeByGuid1.Value = (object) sort;
        else
          attributeByGuid1.Clear();
      }
      IDBAttributeCollection attributes = dbObject2.Attributes;
      for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
      {
        IDBAttribute attr = attributes[AttrIndex];
        if (attr.AttributeType.AttributeType == FieldTypes.ftString && !attr.ReadOnly)
        {
          int num2 = 0;
          while (num2 < attr.ValuesCount)
          {
            attr.Index = num2++;
            attr.AsString = this.Select(attr, inObjectID);
          }
        }
      }
      IDBAttribute attributeByGuid2 = dbObject2.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid2 != null)
      {
        IDBAttribute attributeByGuid3 = dbObject1.GetAttributeByGuid(new Guid("cadd9392-306c-11d8-b4e9-00304f19f545"));
        long prototypeID = -1;
        if (attributeByGuid3 != null && !attributeByGuid3.IsNull)
          prototypeID = attributeByGuid3.AsInteger;
        else if (MetaDataHelper.IsObjectTypeChildOf(dbObject2.ObjectType, MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545")))
          prototypeID = 0L;
        if (prototypeID != 1L)
          (attributeByGuid2 as DBFileAttribute).SetPrototype(prototypeID);
      }
      dbObject2.CommitCreation(false);
      long objectId2 = dbObject2.ObjectID;
      List<AttributeValues> list = ((IEnumerable<AttributeValues>) dbObject2.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption)).ToList<AttributeValues>();
      if (resultData.ContainsKey(num1))
      {
        resultData[num1].Add(new CreatedProjectData(objectId2, list));
      }
      else
      {
        CreatedProjectData createdProjectData = new CreatedProjectData(objectId2, list);
        resultData.Add(num1, new List<CreatedProjectData>()
        {
          createdProjectData
        });
      }
      this.LinkedObjectsCache.Add(dbObject2.ObjectID, inObjectID);
      long objectId3 = dbObject2.ObjectID;
      DataTable dataTable = this.UserSession.GetRelationCollection(relationTypeId).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_PRJLINK_ID
      }, new SortOrders[1]{ SortOrders.ASC }), objectID);
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64_1 = Convert.ToInt64(row[0]);
        long int64_2 = Convert.ToInt64(row[1]);
        int int32 = Convert.ToInt32(row[2]);
        if (this.CreateAllObject)
        {
          this.CreateObject(int64_2, int64_1, int32, objectId3, resultData);
        }
        else
        {
          foreach (long[] id in this.IDs)
          {
            if (id[0] == int64_1 && id[1] == objectID)
            {
              this.CreateObject(int64_2, int64_1, int32, objectId3, resultData);
              break;
            }
          }
        }
      }
    }
    else
      this.UserSession.GetRelationCollection(applicability.RelationType).Create(inObjectID, objectID);
  }

  private void CreateObject(
    long relID,
    long objID,
    int objTypeID,
    long objectID,
    Dictionary<int, List<CreatedProjectData>> resultData)
  {
    bool create = true;
    IDBRelation relation = this.UserSession.GetRelation(relID);
    if (!MetaDataHelper.IsObjectTypeChildOf(objTypeID, MetaDataHelper.GetObjectTypeID("cad00822-306c-11d8-b4e9-00304f19f545")))
    {
      IDBAttribute attributeByGuid = relation.GetAttributeByGuid(Intermech.Imbase.Consts.CreateNewObjectAttGUID);
      create = attributeByGuid == null || attributeByGuid.AsBoolean;
    }
    IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"));
    string sort = attributeByGuid1 != null ? attributeByGuid1.AsString : string.Empty;
    this.CreateTemplateObjects(objID, objectID, sort, create, resultData);
  }

  private string Select(IDBAttribute attr, long inObjectID)
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder1 = objectPoolScope.Object;
      CharEnumerator enumerator = attr.AsString.GetEnumerator();
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        char current = enumerator.Current;
        if (current.ToString().Equals("%"))
        {
          current = enumerator.Current;
          string subString = "" + current.ToString();
          while (enumerator.MoveNext())
          {
            string str1 = subString;
            current = enumerator.Current;
            string str2 = current.ToString();
            subString = str1 + str2;
            current = enumerator.Current;
            if (current.ToString().Equals("%"))
              break;
          }
          if (subString.IndexOf("%9") == 0 && subString.IndexOf("9:") > 0)
            stringBuilder1.Append(subString);
          else
            stringBuilder1.Append(this.Parameters(subString, attr, inObjectID));
        }
        else
        {
          StringBuilder stringBuilder2 = stringBuilder1;
          current = enumerator.Current;
          string str = current.ToString();
          stringBuilder2.Append(str);
        }
      }
      return stringBuilder1.ToString();
    }
  }

  private string GetNextNumber(string value, long objectID)
  {
    string nextNumber;
    if (this.LastObjectID != objectID)
    {
      nextNumber = (++this.NextNumber).ToString();
      this.LastObjectID = objectID;
    }
    else
      nextNumber = this.NextNumber.ToString();
    int num = value.Length - nextNumber.Length;
    if (num > 0)
    {
      for (int index = 0; index < num; ++index)
        nextNumber = "0" + nextNumber;
    }
    return nextNumber;
  }

  private bool IsNumber9(string value)
  {
    bool flag = true;
    for (int index = 0; index < value.Length; ++index)
    {
      if (value[index] != '9')
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  public string Parameters(string subString, IDBAttribute attr, long inObjectID)
  {
    string lower = subString.Trim(' ', '%').ToLower();
    switch (lower)
    {
      case "date":
        return DateTime.Now.ToShortDateString();
      case "root":
        return this.UserSession.GetObject(this.ObjectID).GetAttributeByID(attr.AttributeID).AsString;
      default:
        if (this.IsNumber9(lower))
          return this.GetNextNumber(lower, attr.DBObjectID);
        IDBAttribute attributeByName = this.UserSession.GetObject(inObjectID).GetAttributeByName(lower, false);
        if (attributeByName == null)
          return this.LinkedObjectsCache.ContainsKey(inObjectID) ? this.Parameters(subString, (IDBAttribute) null, this.LinkedObjectsCache[inObjectID]) : string.Empty;
        if (!attributeByName.AsString.Equals(string.Empty))
          return attributeByName.AsString;
        return this.LinkedObjectsCache.ContainsKey(inObjectID) ? this.Parameters(subString, (IDBAttribute) null, this.LinkedObjectsCache[inObjectID]) : string.Empty;
    }
  }
}
