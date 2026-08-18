
// Type: Intermech.Navigator.Conditions.DBConditionDataProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Провайдер данных условия выборки с атрибутом, источником данных здесь выступает база данных
/// к которой подключен текущий сервер приложений
/// </summary>
internal sealed class DBConditionDataProvider : ConditionDataProvider
{
  public override bool AnyAttributes(AttributeSourceTypes sourceType, int[] objectTypeIDs)
  {
    if (objectTypeIDs == null || objectTypeIDs.Length == 0)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sourceType == AttributeSourceTypes.Relation ? this.ExistsAnyAttributesRelations(new List<int>((IEnumerable<int>) objectTypeIDs), sessionKeeper.Session) : this.ExistsAnyAttributesObject(new List<int>((IEnumerable<int>) objectTypeIDs), sessionKeeper.Session);
  }

  public override int UserTypeID
  {
    get => MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>
  /// Проверка на наличие у типов объектов возможности присвоения любого
  /// атрибута (включая потомков по иерархи)
  /// </summary>
  /// <param name="typeIDs">Массив идентификаторов типов объектов</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Если какой-либо из переданных типов обьектов (или какой-либо из их потомков)
  /// может содержать любые атрибуты, то true, иначе false</returns>
  private bool ExistsAnyAttributesObject(List<int> typeIDs, IUserSession userSession)
  {
    if (typeIDs == null)
      return false;
    bool flag = false;
    for (int index = 0; !flag && index < typeIDs.Count; ++index)
      flag = userSession.GetObjectType(typeIDs[index]).AnyAttributes;
    if (!flag)
    {
      for (int index = 0; !flag && index < typeIDs.Count; ++index)
        flag = this.ExistsAnyAttributesObject(MetaDataHelper.GetObjectTypeChildrenID(typeIDs[index]), userSession);
    }
    return flag;
  }

  /// <summary>
  /// Проверка наличия у связей указанных типов объектов признака "любой атрибут"
  /// </summary>
  /// <param name="typeIDs">Массив идентификаторов типов объектов</param>
  /// <param name="userSession">Пользовательская сессия</param>
  /// <returns>Если какая-либо из связей переданных типов обьектов
  /// (или какая-либо из связей потомков переданных объектов) может содержать любые атрибуты,
  /// то true, иначе false</returns>
  private bool ExistsAnyAttributesRelations(List<int> typeIDs, IUserSession userSession)
  {
    if (typeIDs == null)
      return false;
    bool flag = false;
    for (int index = 0; !flag && index < typeIDs.Count; ++index)
    {
      DataTable applicabilitiesList = userSession.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, typeIDs[index], -1);
      if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
        {
          if (!userSession.GetRelationType(Convert.ToInt32(row["F_RELATION_TYPE"])).AnyAttributes)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
    {
      for (int index = 0; !flag && index < typeIDs.Count; ++index)
        flag = this.ExistsAnyAttributesRelations(MetaDataHelper.GetObjectTypeChildrenID(typeIDs[index]), userSession);
    }
    return flag;
  }

  public override string GetObjectTypeCaption(object value)
  {
    IMSObjectType imsObjectType = (IMSObjectType) null;
    switch (value)
    {
      case int objTypeID:
        imsObjectType = MetaDataHelper.GetObjectType(objTypeID);
        break;
      case Guid objTypeGuid:
        imsObjectType = MetaDataHelper.GetObjectType(objTypeGuid);
        break;
    }
    return imsObjectType.ObjectTypeName;
  }

  private IDBAttributeTypeInfo GetAttributeType(object attributeID)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributeTypeInfo attributeType = (IDBAttributeTypeInfo) null;
    switch (attributeID)
    {
      case int attributeTypeID:
        attributeType = service.GetAttributeType(attributeTypeID, false);
        break;
      case Guid attributeGUID:
        attributeType = service.GetAttributeType(attributeGUID, false);
        break;
    }
    return attributeType;
  }

  public override FieldTypes GetFieldType(object attributeID)
  {
    IDBAttributeTypeInfo attributeType = this.GetAttributeType(attributeID);
    return attributeType == null ? FieldTypes.ftUnknown : attributeType.AttributeType;
  }

  public override string GetAttributeName(object attributeID)
  {
    IDBAttributeTypeInfo attributeType = this.GetAttributeType(attributeID);
    return attributeType == null ? "Неизвестный атрибут" : attributeType.Name;
  }

  public override MultiValueModes GetAttributeMultiValueMode(object attributeID)
  {
    IDBAttributeTypeInfo attributeType = this.GetAttributeType(attributeID);
    return attributeType == null ? MultiValueModes.SingleValue : attributeType.MultipleValued;
  }

  private string GetObjectCaption(IDBObject info, object objectID)
  {
    return info == null || string.IsNullOrEmpty(info.Caption) ? this.NotFoundObjectCaption(objectID) : CaptionTransform.GetCaption(info.Caption, (long) info.VersionID);
  }

  private string NotFoundObjectCaption(object objectID) => $"<{objectID}>";

  public override List<ConditionAttributeInfo> GetListAttributes(
    AttributeSourceTypes sourceType,
    int[] objectTypeIDs)
  {
    List<ConditionAttributeInfo> listAttributes = new List<ConditionAttributeInfo>();
    foreach (FieldInfo field in typeof (ObligatoryObjectAttributes).GetFields())
    {
      ObligatoryObjectAttributes objectAttributes = (ObligatoryObjectAttributes) field.GetValue((object) ObligatoryObjectAttributes.None);
      switch (objectAttributes)
      {
        case ObligatoryObjectAttributes.None:
        case ObligatoryObjectAttributes.Zero:
          continue;
        default:
          AttributeSourceTypes attributeSourceType = ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes);
          bool flag = true;
          switch (sourceType)
          {
            case AttributeSourceTypes.Auto:
              if (attributeSourceType != AttributeSourceTypes.Object && attributeSourceType != AttributeSourceTypes.Relation)
              {
                flag = false;
                break;
              }
              break;
            case AttributeSourceTypes.Object:
              if (attributeSourceType != AttributeSourceTypes.Object)
              {
                flag = false;
                break;
              }
              break;
            case AttributeSourceTypes.Relation:
              if (attributeSourceType != AttributeSourceTypes.Relation)
              {
                flag = false;
                break;
              }
              break;
          }
          if (flag)
          {
            listAttributes.Add(new ConditionAttributeInfo((object) Convert.ToInt32((object) objectAttributes), ObligatoryObjectAttributesHelper.GetCaption(objectAttributes), FieldTypes.ftSystem));
            continue;
          }
          continue;
      }
    }
    listAttributes.RemoveAll((Predicate<ConditionAttributeInfo>) (x => (int) x.Id == -11 || (int) x.Id == -43 || (int) x.Id == -17));
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    foreach (int objectTypeId in objectTypeIDs)
    {
      if (service.GetObjectType(objectTypeId, false) != null && sourceType != AttributeSourceTypes.Relation)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objectTypeId);
        if (attribute4ObjectTypeList != null && attribute4ObjectTypeList.Count != 0)
        {
          foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
          {
            IMSAttribute4ObjectType attr4type = attribute4ObjectType;
            if (!listAttributes.Exists((Predicate<ConditionAttributeInfo>) (x => (int) x.Id == attr4type.AttributeID)))
              listAttributes.Add(new ConditionAttributeInfo((object) attr4type.AttributeID, MetaDataHelper.GetAttributeTypeName(attr4type.AttributeID), attr4type.FieldType));
          }
        }
      }
    }
    return listAttributes;
  }

  protected override Guid GetAttributeGuidFromId(int attributeID)
  {
    return MetaDataHelper.GetAttributeTypeGuid(attributeID);
  }

  protected override int GetAttributeIdFromGuid(Guid attributeGuid)
  {
    return MetaDataHelper.GetAttributeTypeID(attributeGuid);
  }

  public override Dictionary<object, string> GetPossibleValues(object attributeID)
  {
    Dictionary<object, string> possibleValues1 = new Dictionary<object, string>();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    IDBAttributeTypeInfo attributeTypeInfo = attributeID is Guid attributeGUID ? service.GetAttributeType(attributeGUID, true) : service.GetAttributeType((int) attributeID);
    DataTable possibleValues2 = attributeTypeInfo.GetPossibleValues();
    if (possibleValues2 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues2.Rows)
      {
        object key = row[attributeTypeInfo.ValueFieldName];
        string str = Convert.ToString(row["F_DESCRIPTION"]);
        if (str == string.Empty)
          str = Convert.ToString(key);
        possibleValues1.Add(key, str);
      }
    }
    return possibleValues1;
  }

  public override bool ChoiseObjectType(ref object objectType, SelectionType selectionType)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_392"), typeof (ObjectTypeFolder), false);
    if (selectionType == SelectionType.Archiv || selectionType == SelectionType.Archives)
      selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(new List<int>()
      {
        MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"))
      }.ToArray(), true, true);
    selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
    {
      objectType != null ? Convert.ToInt32(objectType) : -1
    }), new ArrayList((ICollection) new System.Type[1]
    {
      typeof (ObjectTypeFolder)
    }));
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return false;
    objectType = selectorForm.IDList[0];
    return true;
  }

  public override bool ChoiseRelationType(ref object relationType)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("Client.Core_401"), typeof (RelationTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return false;
    relationType = selectorForm.IDList[0];
    return true;
  }

  protected override string GetInputObjectAttributeCaption(InputObjectAttribute attribute)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return SelectionParameter.ConvertToStringInputObjectAttribute(sessionKeeper.Session, attribute);
  }

  public override string GetObjectCaption(object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      switch (value)
      {
        case long objectID:
          return this.GetObjectCaption(sessionKeeper.Session.GetObject(objectID, false), value);
        case Guid objectGUID:
          return this.GetObjectCaption(sessionKeeper.Session.GetObject(objectGUID, false), value);
        case int num:
          if (num == 0 || (int) value == -1)
            return LocalizationHolder.rm.GetString("Client.Core_1506");
          break;
      }
      return this.NotFoundObjectCaption(value);
    }
  }

  public override string GetLifecycleLevelCaption(object value)
  {
    return MetaDataHelper.GetLCLevelName(Convert.ToInt32(value));
  }

  public override string GetLifecycleStepCaption(object value)
  {
    return MetaDataHelper.GetLCStepName(Convert.ToInt32(value));
  }

  public override string GetRelationTypeCaption(object value)
  {
    return MetaDataHelper.GetRelationTypeName(Convert.ToInt32(value));
  }

  public override string GetSubjectAreaCaption(object value)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (value != null)
      {
        IDBSubjectAreaType subjectAreaType = sessionKeeper.Session.GetSubjectAreaType(Convert.ToChar(value), false);
        if (subjectAreaType != null)
          return subjectAreaType.AreaName;
      }
    }
    return Convert.ToString(value);
  }

  public override bool SelectDialog(
    ref object value,
    SelectionParameterTypes type,
    object addInfo,
    int attrID,
    int[] selection4Types)
  {
    switch (type)
    {
      case SelectionParameterTypes.sptSiteID:
        return this.SelectSitesDialog(ref value);
      case SelectionParameterTypes.sptObject:
        return ValueRelationSelector.SelectObject(ref value, attrID, selection4Types, addInfo, false);
      case SelectionParameterTypes.sptCheckOutBy:
      case SelectionParameterTypes.sptUser:
        return ValueRelationSelector.SelectUser(ref value);
      case SelectionParameterTypes.sptObjectType:
        return ValueRelationSelector.SelectObjectType(ref value);
      case SelectionParameterTypes.sptLifecycleLevel:
        return ValueRelationSelector.SelectLifecycleLevel(ref value);
      case SelectionParameterTypes.sptSubjectArea:
        return ValueRelationSelector.SelectSubjectArea(ref value);
      case SelectionParameterTypes.sptLinkType:
        return ValueRelationSelector.SelectLinkType(ref value);
      case SelectionParameterTypes.sptLifecycleStep:
        return ValueRelationSelector.SelectLifeCycleStep(ref value);
      case SelectionParameterTypes.sptGlobalID:
        return ValueRelationSelector.SelectVersionsGuid(ref value);
      case SelectionParameterTypes.sptMeasured:
        return ValueRelationSelector.SelectMeasure(ref value, (object) null);
      default:
        return false;
    }
  }

  public override string GenerateConditionCaption(
    ConditionStructure conditionStructure,
    string value1,
    string value2)
  {
    int attributeId = this.GetAttributeID(conditionStructure.Attribute);
    if (attributeId != 0)
      return SelectionWrapper.FormingValueString(conditionStructure.RelationalOperator, value1, string.Empty, value2, MetaDataHelper.GetAttributeTypeName(attributeId));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return SelectionWrapper.GenerateConditionCaption(sessionKeeper.Session, conditionStructure);
  }

  public override bool IsUserObjectID(long objectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectInfo(objectID).ObjectTypeID == this.UserTypeID;
  }

  public override List<ConditionAttributeInfo> GetAttributesForObjectTypes(int[] objTypes)
  {
    if (objTypes == null || objTypes.Length == 0)
      return (List<ConditionAttributeInfo>) null;
    List<ConditionAttributeInfo> attributesForObjectTypes = new List<ConditionAttributeInfo>();
    List<int> intList = new List<int>();
    for (int index = 0; index < objTypes.Length; ++index)
    {
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objTypes[index]);
      if (attribute4ObjectTypeList != null && attribute4ObjectTypeList.Count != 0)
      {
        foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
        {
          if (!intList.Contains(attribute4ObjectType.AttributeID))
          {
            intList.Add(attribute4ObjectType.AttributeID);
            attributesForObjectTypes.Add(new ConditionAttributeInfo((object) attribute4ObjectType.AttributeID, MetaDataHelper.GetAttributeTypeName(attribute4ObjectType.AttributeID)));
          }
        }
      }
    }
    return attributesForObjectTypes;
  }

  public override List<ConditionAttributeInfo> GetObligatoryAttributes(
    AttributeSourceTypes sourceType)
  {
    List<ConditionAttributeInfo> obligatoryAttributes = new List<ConditionAttributeInfo>();
    foreach (FieldInfo field in typeof (ObligatoryObjectAttributes).GetFields())
    {
      ObligatoryObjectAttributes objectAttributes = (ObligatoryObjectAttributes) field.GetValue((object) ObligatoryObjectAttributes.None);
      switch (objectAttributes)
      {
        case ObligatoryObjectAttributes.None:
        case ObligatoryObjectAttributes.Zero:
          continue;
        default:
          AttributeSourceTypes attributeSourceType = ObligatoryObjectAttributesHelper.GetAttributeSourceType(objectAttributes);
          if (attributeSourceType != AttributeSourceTypes.Events && (sourceType == attributeSourceType || sourceType != AttributeSourceTypes.Relation && attributeSourceType != AttributeSourceTypes.Relation) && objectAttributes != ObligatoryObjectAttributes.F_AREA_ID && objectAttributes != ObligatoryObjectAttributes.F_ACTUAL_DATE && objectAttributes != ObligatoryObjectAttributes.F_SITE_ID)
          {
            obligatoryAttributes.Add(new ConditionAttributeInfo((object) (int) objectAttributes, ObligatoryObjectAttributesHelper.GetCaption(objectAttributes)));
            continue;
          }
          continue;
      }
    }
    return obligatoryAttributes;
  }

  public override int GetObjectType4ObjectLink(int attributeID)
  {
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeID, false);
    return attributeType != null ? Convert.ToInt32(attributeType.SizeType) : -1;
  }

  public override int GetObjectTypeID(Guid objectTypeGuid)
  {
    return MetaDataHelper.GetObjectTypeID(objectTypeGuid);
  }

  public override int[] UserGroupTypeIDs
  {
    get
    {
      return new List<int>()
      {
        MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545"),
        MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545")
      }.ToArray();
    }
  }

  public override void GetDateAttributeFormat(
    int attributeID,
    int[] objectTypeIDs,
    out DateTimePickerFormat format,
    out string formatString)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    string str = string.Empty;
    bool flag = false;
    if (objectTypeIDs != null && objectTypeIDs.Length == 1)
    {
      IDBAttributeTypeInfo attributeById = (IDBAttributeTypeInfo) service.GetObjectType(objectTypeIDs[0]).Attributes.GetAttributeByID(attributeID, false);
      if (attributeById != null)
      {
        str = attributeById.Mask;
        flag = true;
      }
    }
    if (!flag)
      str = service.GetAttributeType(attributeID).Mask;
    if (!string.IsNullOrEmpty(str) && str.Equals(Intermech.Consts.OnlyDateFunction))
    {
      format = DateTimePickerFormat.Short;
      formatString = string.Empty;
    }
    else
      base.GetDateAttributeFormat(attributeID, objectTypeIDs, out format, out formatString);
  }

  public override RelationalOperators[] GetEnableRelationalOperators(
    FieldTypes fieldType,
    int attributeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetAttributeType(attributeID, true).GetEnabledOperators(ColumnContents.Text);
  }
}
