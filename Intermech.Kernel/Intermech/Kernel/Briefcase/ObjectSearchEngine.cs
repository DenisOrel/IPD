// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ObjectSearchEngine
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal static class ObjectSearchEngine
{
  private static readonly int _attrLCStepGuid = MetaDataHelper.GetAttributeTypeID("cad0014c-306c-11d8-b4e9-00304f19f545");
  private static readonly int _attrLCLevelGuid = MetaDataHelper.GetAttributeTypeID("cad0015b-306c-11d8-b4e9-00304f19f545");
  private static readonly int _attrObjTypeGuid = MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545");

  public static long FindObject(
    IUserSession session,
    IDBObjectType objType,
    ImportingObject briefObject,
    out long id,
    out ObjectSearchEngine.FoundType foundType)
  {
    id = 0L;
    long objectId = ObjectSearchEngine.FindObjectID(session as UserSession, (Guid) briefObject.Object.ObjectGuid);
    if (objectId != 0L)
    {
      foundType = ObjectSearchEngine.FoundType.ObjectID;
      return objectId;
    }
    id = ObjectSearchEngine.FindID(session as UserSession, (Guid) briefObject.Object.IdGuid);
    if (id != 0L && objType.Versionable == ObjectVersionModes.SingleVersion)
    {
      IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(id, "cad001df-306c-11d8-b4e9-00304f19f545", true);
      if (objectByVersionsRule != null)
      {
        foundType = ObjectSearchEngine.FoundType.ID;
        return objectByVersionsRule.ObjectID;
      }
    }
    long objectOnIdAttributes = ObjectSearchEngine.FindObjectOnIDAttributes(session, objType, briefObject);
    foundType = objectOnIdAttributes != 0L ? ObjectSearchEngine.FoundType.IDAttribute : ObjectSearchEngine.FoundType.None;
    return objectOnIdAttributes;
  }

  public static long FindObjectOnIDAttributes(
    IUserSession session,
    IDBObjectType objType,
    ImportingObject briefObject)
  {
    List<int> intList = new List<int>();
    if (session.GetCustomService(typeof (IContainerService)) is IContainerService customService)
    {
      IDBObject containerForObjectType = customService.GetContainerForObjectType((object) session.SessionGUID, objType.ObjectType);
      if (containerForObjectType != null)
      {
        IDBAttribute attributeByGuid = containerForObjectType.GetAttributeByGuid(new Guid("cad014ab-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && attributeByGuid.ValuesCount > 0)
        {
          for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
          {
            string str = Convert.ToString(attributeByGuid.Values[index]);
            if (GuidHelper.IsGuid(str))
            {
              IDBAttributeType attributeType = session.GetAttributeType(new Guid(str), false);
              if (attributeType != null && !intList.Contains(attributeType.AttributeID))
                intList.Add(attributeType.AttributeID);
            }
          }
        }
      }
    }
    if (intList.Count > 0)
    {
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        string conditionValue = string.Empty;
        for (int index2 = 0; index2 < briefObject.Attributes.Count; ++index2)
        {
          AttributeRecord attribute = briefObject.Attributes[index2];
          if (attribute.AttributeId == intList[index1] && attribute.StringValue != null && (string) attribute.StringValue != string.Empty)
          {
            conditionValue = (string) attribute.StringValue;
            break;
          }
        }
        if (conditionValue != string.Empty)
        {
          int objectType = ObjectSearchEngine.GetRootObjType4Attr(session, objType, intList[index1], -1);
          switch (objectType != -1 ? objType.Attributes.GetAttributeByID(intList[index1]).UniqueMode : session.GetAttributeType(intList[index1]).UniqueMode)
          {
            case UniqueValueModes.NotUnique:
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_949"), (object) intList[index1], (object) objType.ObjectTypeName));
            case UniqueValueModes.AllVerTypes:
              objectType = -1;
              break;
          }
          DataTable dataTable = session.GetObjectCollection(objectType).Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(intList[index1], RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, false)
          }, new object[3]
          {
            (object) -2,
            (object) -3,
            (object) -16
          }));
          if (dataTable.Rows.Count > 1)
          {
            long num = -1;
            long objectOnIdAttributes = -1;
            for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
            {
              if (index3 == 0)
                num = Convert.ToInt64(dataTable.Rows[index3][1]);
              else if (num != Convert.ToInt64(dataTable.Rows[index3][1]))
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_950"), (object) conditionValue, (object) intList[index1]));
              if (Convert.ToInt32(dataTable.Rows[index3][2]) == 1)
                objectOnIdAttributes = Convert.ToInt64(dataTable.Rows[index3][0]);
            }
            if (objectOnIdAttributes != -1L)
              return objectOnIdAttributes;
          }
          else if (dataTable.Rows.Count == 1)
            return Convert.ToInt64(dataTable.Rows[0][0]);
        }
      }
    }
    long container = ObjectSearchEngine.FindContainer(session, objType, briefObject);
    return container != 0L ? container : 0L;
  }

  private static long FindContainer(
    IUserSession session,
    IDBObjectType objType,
    ImportingObject briefObject)
  {
    if (objType.ObjectType == MetaDataHelper.GetObjectTypeID(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")))
    {
      IContainerService customService = session.GetCustomService(typeof (IContainerService)) as IContainerService;
      for (int index = 0; index < briefObject.Attributes.Count; ++index)
      {
        AttributeRecord attribute = briefObject.Attributes[index];
        if (attribute.AttributeId == ObjectSearchEngine._attrLCStepGuid)
        {
          IDBObject containerForLcStep = customService.GetContainerForLCStep((object) session, new Guid((string) attribute.StringValue));
          if (containerForLcStep != null)
            return containerForLcStep.ObjectID;
        }
        else if (attribute.AttributeId == ObjectSearchEngine._attrLCLevelGuid)
        {
          IDBObject containerForLcLevel = customService.GetContainerForLCLevel((object) session, new Guid((string) attribute.StringValue));
          if (containerForLcLevel != null)
            return containerForLcLevel.ObjectID;
        }
        else if (attribute.AttributeId == ObjectSearchEngine._attrObjTypeGuid)
        {
          IDBObject containerForObjectType = customService.GetContainerForObjectType((object) session, new Guid((string) attribute.StringValue));
          if (containerForObjectType != null)
            return containerForObjectType.ObjectID;
        }
      }
    }
    return 0;
  }

  private static int GetRootObjType4Attr(
    IUserSession session,
    IDBObjectType objType,
    int attrID,
    int firtsType)
  {
    if (!(objType.Attributes.GetAttributeByID(attrID, false) is IDBAttributeType4Object attributeById))
      return firtsType;
    return attributeById.InheritMode == InheritModes.Inherited && objType.ParentTypeID != -1 ? ObjectSearchEngine.GetRootObjType4Attr(session, session.GetObjectType(objType.ParentTypeID), attrID, objType.ObjectType) : objType.ObjectType;
  }

  public static long FindObjectID(UserSession session, Guid ObjectGuid)
  {
    object obj = session.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :v_guid", session.DataManager.Parameter("v_guid", (object) ObjectGuid));
    return obj == null ? 0L : Convert.ToInt64(obj);
  }

  public static long FindID(UserSession session, Guid IDGuid)
  {
    object obj = session.DataManager.ExecuteScalar("SELECT F_ID FROM IMS_GUID_RESOLVE WHERE F_GUID = :v_guid", session.DataManager.Parameter("v_guid", (object) IDGuid));
    return obj == null ? 0L : Convert.ToInt64(obj);
  }

  public enum FoundType
  {
    None,
    ObjectID,
    ID,
    IDAttribute,
  }
}
