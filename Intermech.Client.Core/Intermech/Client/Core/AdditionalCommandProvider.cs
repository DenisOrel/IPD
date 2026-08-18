
// Type: Intermech.Client.Core.AdditionalCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Protection;
using Intermech.Search.Diff;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Провайдер контекстного меню для атрибутов.</summary>
public class AdditionalCommandProvider : ICommandsProvider
{
  private const int F_OWNER_ID = -8;
  private const int F_OBJECT_TYPE = -7;
  private const int F_PROJECT_ID = -14;
  private QuestionFormResult _questionFormResult;
  private int _selectedAttrIDOnStartup;

  /// <summary>Получить команды навигатора.</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации</param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("AddAttribute", new CommandInfo(0, new ClickEventHandler(this.AddAttributeCommand)));
    mergedCommands.Add("AddAttributeGroup", new CommandInfo(0, new ClickEventHandler(this.AddAttributeGroupCommand)));
    mergedCommands.Add("DeleteAttribute", new CommandInfo(0, new ClickEventHandler(this.DeleteAttributeCommand)));
    mergedCommands.Add("DeleteAttributeGroup", new CommandInfo(0, new ClickEventHandler(this.DeleteAttributeGroupCommand)));
    mergedCommands.Add("EditAttributeValue", new CommandInfo(0, new ClickEventHandler(this.EditAttributeValueCommand)));
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && itemData.RelationType != -1)
      {
        mergedCommands.Add("EditRelationAttributeValue", new CommandInfo(0, new ClickEventHandler(this.EditRelationAttributeValueCommand)));
        mergedCommands.Add("AddRelationAttribute", new CommandInfo(0, new ClickEventHandler(this.AddRelationAttributeCommand)));
        mergedCommands.Add("AddRelationAttributeGroup", new CommandInfo(0, new ClickEventHandler(this.AddRelationAttributeGroupCommand)));
        mergedCommands.Add("DeleteRelationAttribute", new CommandInfo(0, new ClickEventHandler(this.DeleteRelationAttributeCommand)));
        mergedCommands.Add("DeleteRelationAttributeGroup", new CommandInfo(0, new ClickEventHandler(this.DeleteRelationAttributeGroupCommand)));
        break;
      }
    }
    return mergedCommands;
  }

  /// <summary>Получить команды навигатора</summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (items.Count == 1)
    {
      INodeID itemId = items.GetItemID(0);
      if (itemId != null && itemId.CategoryID == 1)
        groupCommands.Add("ObjectsDiffForCompareVersionObjectsMenu", new CommandInfo(0, new ClickEventHandler(this.ObjectsVersionDifference)));
    }
    if (items.Count == 2)
    {
      if (items.GetItemID(0) is NodeID && items.GetItemID(1) is NodeID)
      {
        groupCommands.Add("ObjectsDiff", new CommandInfo(0, new ClickEventHandler(this.ObjectsDiff)));
        groupCommands.Add("ObjectsDiffForCompareObjectsMenu", new CommandInfo(0, new ClickEventHandler(this.ObjectsDiff)));
      }
      else
        groupCommands = CommandsInfo.Empty;
    }
    else
      groupCommands.Add("ShowAttributeHistory", new CommandInfo(0, new ClickEventHandler(this.ShowAttributeHistory)));
    return groupCommands;
  }

  /// <summary>
  /// Сравнение атрибутов для версий объекта
  /// В меню Сравнить версии
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void ObjectsVersionDifference(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    long versionForCompareId = VersionComparison.GetVersionForCompareId(viewServices, itemData);
    if (versionForCompareId == 0L)
      return;
    AdditionalCommandProvider.ObjectsDiff(itemData.Value, versionForCompareId);
  }

  /// <summary>Добавление указанных атрибутов выделенным объектам.</summary>
  /// <param name="selectedObjInfo">Информация о выделенных объектах рассортированных по типам</param>
  /// <param name="values">Информация о добавляемых атрибутах</param>
  /// <param name="addAttrs">Флаг о том, какая операция будет происходить, добавление или редактирование атрибутов</param>
  /// <param name="masks">Словарь идентификаторов атрибутов и соответствующих им масок</param>
  /// <returns>Результат добавления (null не возвращается)</returns>
  private SortedList<int, List<long>> AddEditAttributes(
    Dictionary<int, List<long>> selectedObjInfo,
    List<AttributeValues> values,
    bool addAttrs,
    Dictionary<int, string> masks)
  {
    SimpleEditorForm.SimpleEditorFormMode mode = addAttrs ? SimpleEditorForm.SimpleEditorFormMode.AddAttributes : SimpleEditorForm.SimpleEditorFormMode.EditAttributes;
    IElementInfo elementInfo = (IElementInfo) null;
    if (selectedObjInfo.Count == 1)
    {
      List<long> longList = selectedObjInfo.Values.ElementAt<List<long>>(0);
      if (longList != null && longList.Count > 0)
        elementInfo = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(longList[0], AttributableElements.Object);
    }
    using (SimpleEditorForm simpleEditorForm = new SimpleEditorForm(elementInfo, values.ToArray(), mode, masks))
    {
      simpleEditorForm.SelectedObjInfo = selectedObjInfo;
      values.Clear();
      if (simpleEditorForm.ShowDialog() == DialogResult.OK)
        values.AddRange((IEnumerable<AttributeValues>) simpleEditorForm.Values);
    }
    return this.AddEditAttributesSaveValues(values, selectedObjInfo, addAttrs);
  }

  /// <summary>Сохранение измененных значений</summary>
  /// <param name="values"></param>
  /// <param name="selectedObjInfo"></param>
  /// <param name="addAttrs"></param>
  /// <returns></returns>
  private SortedList<int, List<long>> AddEditAttributesSaveValues(
    List<AttributeValues> values,
    Dictionary<int, List<long>> selectedObjInfo,
    bool addAttrs)
  {
    SortedList<int, List<long>> changedObjs = new SortedList<int, List<long>>();
    bool skipAll = false;
    if (values.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string empty = string.Empty;
        foreach (KeyValuePair<int, List<long>> keyValuePair in selectedObjInfo)
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(keyValuePair.Key);
          if (objectCollection != null)
          {
            foreach (AttributeValues attributeValues in values)
            {
              int attributeId = attributeValues.AttributeID;
              if (attributeValues.AttributeType == FieldTypes.ftSystem)
              {
                try
                {
                  object[] values1 = attributeValues.Values;
                  string msg = string.Empty;
                  if (values1 == null || values1.Length == 0 || values1[0] == DBNull.Value || values1[0] == null)
                  {
                    msg = string.Format(LocalizationHolder.rm.GetString("AttrTextEdit_ErrorMessage_NullAttrsValue"), (object) attributeValues.AttributeName);
                  }
                  else
                  {
                    switch (attributeId)
                    {
                      case -14:
                        long result1 = -1;
                        if (!long.TryParse(Convert.ToString(values1[0]), out result1) || result1 < 0L)
                        {
                          msg = string.Format(LocalizationHolder.rm.GetString("Client.Core.AttributeValue.InvalidDataType.Msg"), (object) attributeValues.AttributeName);
                          break;
                        }
                        break;
                      case -8:
                        long result2 = -1;
                        if (!long.TryParse(Convert.ToString(values1[0]), out result2) || result2 == 0L)
                        {
                          msg = string.Format(LocalizationHolder.rm.GetString("Client.Core.AttributeValue.InvalidDataType.Msg"), (object) attributeValues.AttributeName);
                          break;
                        }
                        break;
                      case -7:
                        int result3 = -1;
                        if (!int.TryParse(Convert.ToString(values1[0]), out result3) || result3 == -1)
                        {
                          msg = string.Format(LocalizationHolder.rm.GetString("Client.Core.AttributeValue.InvalidDataType.Msg"), (object) attributeValues.AttributeName);
                          break;
                        }
                        break;
                    }
                  }
                  if (!string.IsNullOrEmpty(msg))
                  {
                    if (this._questionFormResult != QuestionFormResult.SkipAll)
                      throw new AdditionalCommandProviderException(msg);
                    continue;
                  }
                }
                catch (Exception ex)
                {
                  ExceptionHelper.ExceptionService.ShowException(ex);
                  if (this._questionFormResult != QuestionFormResult.Skip)
                  {
                    if (this._questionFormResult != QuestionFormResult.SkipAll)
                    {
                      if (this._questionFormResult == QuestionFormResult.Break)
                        return changedObjs;
                    }
                    else
                      continue;
                  }
                  else
                    continue;
                }
              }
              else if (attributeValues.AttributeType == FieldTypes.ftPassword)
              {
                IMServerService service = ServicesManager.GetService(typeof (IMServerService)) as IMServerService;
                for (int index = 0; index < attributeValues.Values.Length; ++index)
                {
                  if (attributeValues.Values[index] is string)
                    attributeValues.Values[index] = (object) new PswPackage(attributeValues.Values[index].ToString(), service.ServerObject.CryptMethod);
                }
              }
              List<long> objIDs = new List<long>((IEnumerable<long>) keyValuePair.Value);
              while (objIDs.Count > 0)
              {
                CommandResult commandResult = new CommandResult((long[]) null);
                commandResult = !addAttrs ? (attributeValues.AttributeType != FieldTypes.ftSystem ? objectCollection.EditAttribute(objIDs.ToArray(), (object) attributeId, attributeValues.Values, skipAll) : this.EditSystemAttribute(objIDs, attributeId, attributeValues.Values[0], skipAll)) : objectCollection.AddAttribute(objIDs.ToArray(), (object) attributeId, attributeValues.Values, skipAll);
                if (commandResult.ProcessedObjects.Length != 0)
                {
                  if (changedObjs.ContainsKey(attributeId))
                    changedObjs[attributeId].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
                  else
                    changedObjs.Add(attributeId, new List<long>((IEnumerable<long>) commandResult.ProcessedObjects));
                }
                if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
                {
                  if (!skipAll)
                  {
                    QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149"), false, false);
                    if (questionFormResult == QuestionFormResult.Break)
                      return changedObjs;
                    skipAll = questionFormResult == QuestionFormResult.SkipAll;
                  }
                  foreach (long processedObject in commandResult.ProcessedObjects)
                    objIDs.Remove(processedObject);
                  objIDs.Remove(commandResult.ErrorObjectID);
                }
                else
                  objIDs.Clear();
              }
            }
          }
        }
        if (changedObjs.Count > 0)
        {
          List<AdditionalCommandProvider.MasterInfoClass> masterInfoClassList = this.GroupMasterAttributes(selectedObjInfo, values, changedObjs);
          if (masterInfoClassList != null)
          {
            foreach (AdditionalCommandProvider.MasterInfoClass masterInfoClass in masterInfoClassList)
            {
              if (!masterInfoClass.SaveSourceAttributes(sessionKeeper.Session, ref skipAll))
                break;
            }
          }
        }
      }
    }
    return changedObjs;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedObjInfo">Список выделенных объектов, сгруппированных по типам</param>
  /// <param name="values">Все выбранные атрибуты</param>
  /// <param name="changedObjs">Список измененных объектов, сгруппированных по атрибутам</param>
  /// <returns></returns>
  private List<AdditionalCommandProvider.MasterInfoClass> GroupMasterAttributes(
    Dictionary<int, List<long>> selectedObjInfo,
    List<AttributeValues> values,
    SortedList<int, List<long>> changedObjs)
  {
    Dictionary<int, AdditionalCommandProvider.MasterInfoClass> dictionary = new Dictionary<int, AdditionalCommandProvider.MasterInfoClass>();
    List<int> changedAttrIDs = changedObjs.Keys.ToList<int>();
    changedAttrIDs.Sort();
    List<int> list1 = values.Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeType == FieldTypes.ftObjectLink && changedAttrIDs.BinarySearch(x.AttributeID) > -1)).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToList<int>();
    list1.Sort();
    if (list1.Count > 0)
    {
      foreach (KeyValuePair<int, List<long>> keyValuePair in selectedObjInfo)
      {
        List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(keyValuePair.Key);
        string empty = string.Empty;
        foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
        {
          IMSAttribute4ObjectType attr = attribute4ObjectType;
          if (changedAttrIDs.BinarySearch(attr.AttributeID) <= -1 && attr.MasterAttributeID != 0 && list1.BinarySearch(attr.MasterAttributeID) >= 0)
          {
            List<long> list2 = keyValuePair.Value.Where<long>((System.Func<long, bool>) (x => changedObjs[attr.MasterAttributeID].Contains(x))).ToList<long>();
            if (list2.Count != 0)
            {
              if (!dictionary.ContainsKey(attr.MasterAttributeID))
              {
                AttributeValues attributeValues = values.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attr.MasterAttributeID));
                long result = 0;
                string s = Convert.ToString(attributeValues.Values[0]);
                if (!string.IsNullOrEmpty(s) && !long.TryParse(s, out result))
                  list1.Remove(attr.MasterAttributeID);
                else
                  dictionary.Add(attr.MasterAttributeID, new AdditionalCommandProvider.MasterInfoClass(attr.MasterAttributeID, result, keyValuePair.Key, list2, attr.AttributeID, attr.AttributeID));
              }
              else
                dictionary[attr.MasterAttributeID].AddAttribute(keyValuePair.Key, list2, attr.AttributeID, attr.AttributeID);
            }
          }
        }
      }
    }
    return dictionary.Count <= 0 ? (List<AdditionalCommandProvider.MasterInfoClass>) null : dictionary.Values.ToList<AdditionalCommandProvider.MasterInfoClass>();
  }

  /// <summary>Изменение системных атрибутов.</summary>
  /// <param name="objIDs">Идентификаторы выбранных объектов</param>
  /// <param name="attrID">Идентификатор рассматриваемого атрибута</param>
  /// <param name="value">Новое значение атрибута</param>
  /// <param name="skipAll">Необходимость пропуска ошибки</param>
  /// <returns>Результат изменения</returns>
  private CommandResult EditSystemAttribute(
    List<long> objIDs,
    int attrID,
    object value,
    bool skipAll)
  {
    List<long> longList = new List<long>(objIDs.Count);
    string str = string.Empty;
    long num = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIDs)
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objId, false);
        if (objectActualCopy == null)
        {
          if (skipAll)
          {
            longList.Add(objId);
          }
          else
          {
            str = string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) objId);
            num = objId;
            break;
          }
        }
        else
        {
          try
          {
            switch (attrID)
            {
              case -14:
                objectActualCopy.ProjectID = Convert.ToInt64(value);
                break;
              case -8:
                objectActualCopy.OwnerID = Convert.ToInt64(value);
                break;
              case -7:
                objectActualCopy.ObjectType = Convert.ToInt32(value);
                break;
            }
            longList.Add(objId);
          }
          catch (Exception ex)
          {
            if (!skipAll)
              ExceptionHelper.ExceptionService.ShowException(ex);
            longList.Add(objId);
          }
        }
      }
    }
    return new CommandResult(longList.ToArray())
    {
      ErrorMessage = str,
      ErrorObjectID = num
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objByTypeDict"></param>
  /// <returns></returns>
  private Dictionary<int, List<int>> GetAttributeGroups(Dictionary<int, List<long>> objByTypeDict)
  {
    Dictionary<int, List<int>> attributeGroups = (Dictionary<int, List<int>>) null;
    List<int> source = new List<int>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in objByTypeDict)
    {
      List<int> attributes = this.GetAttributes(keyValuePair.Key, keyValuePair.Value);
      if (attributes != null)
        source.AddRange((IEnumerable<int>) attributes);
    }
    if (source.Count > 0)
    {
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable[] cacheTables = sessionKeeper.Session.GetCacheTables("IMS_ATTR_IN_GROUPS");
        dataTable = cacheTables == null || cacheTables.Length == 0 ? (DataTable) null : cacheTables[0];
      }
      if (dataTable != null)
      {
        List<int> list = source.Distinct<int>().ToList<int>();
        list.Sort();
        attributeGroups = new Dictionary<int, List<int>>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          if (list.BinarySearch(int32_1) >= 0)
          {
            int int32_2 = Convert.ToInt32(row["F_GROUP_ID"]);
            if (attributeGroups.ContainsKey(int32_2))
              attributeGroups[int32_2].Add(int32_1);
            else
              attributeGroups.Add(int32_2, new List<int>()
              {
                int32_1
              });
          }
        }
      }
    }
    return attributeGroups;
  }

  /// <summary>
  /// Получение списка атрибутов объектов, которые не являются обязательными атрибутами.
  /// </summary>
  /// <remarks>
  /// Получает атрибуты указанных объектов и выбирает из них необязательные.
  /// В результирующем списке соддержатся только уникальные значения.
  /// </remarks>
  /// <param name="objTypeID">Список идентификаторов объектов</param>
  /// <param name="objIDs">Тип передаваемых объектов</param>
  /// <returns>Список идентификаторов атрибутов</returns>
  private List<int> GetAttributes(int objTypeID, List<long> objIDs)
  {
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objId in objIDs)
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objId, false);
        if (objectActualCopy != null)
        {
          foreach (AttributeValues attributesValue in objectActualCopy.GetAttributesValues(GetAttributeValuesModes.None))
          {
            if (!intList.Contains(attributesValue.AttributeID))
            {
              IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objTypeID, attributesValue.AttributeID);
              if (attribute4ObjectType == null || attribute4ObjectType.Required != RequiredModes.AutoRequired)
                intList.Add(attributesValue.AttributeID);
            }
          }
        }
      }
    }
    return intList.Count <= 0 ? (List<int>) null : intList;
  }

  /// <summary>Получить описание атрибутов по их идентификаторам.</summary>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="typeID">Тип объекта</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов</param>
  /// <param name="defaultTypeID">Идентификатор типа объектов, подставляемого по умолчанию для системного атрибута "Тип объекта"</param>
  /// <returns>Список описаний атрибутов</returns>
  private List<AttributeValues> GetAttributeValuesByIDs(
    long objID,
    int typeID,
    List<int> attrIDs,
    int defaultTypeID,
    List<int> objTypes = null)
  {
    List<AttributeValues> attributeValuesByIds = new List<AttributeValues>(attrIDs.Count);
    AttributeValues[] source = (AttributeValues[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (objID != 0L)
        source = (sessionKeeper.Session.GetObjectActualCopy(objID, false) ?? throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) objID))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        }).GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions);
      IDBObjectType objectType = typeID != -1 ? sessionKeeper.Session.GetObjectType(typeID) : (IDBObjectType) null;
      foreach (int attrId in attrIDs)
      {
        int attrID = attrId;
        object obj = (object) null;
        if (source != null)
        {
          AttributeValues attributeValues = ((IEnumerable<AttributeValues>) source).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
          if (attributeValues != null)
          {
            attributeValuesByIds.Add(attributeValues);
            continue;
          }
        }
        switch (attrID)
        {
          case -14:
            obj = (object) 0;
            break;
          case -8:
            obj = (object) sessionKeeper.Session.UserID;
            break;
          case -7:
            obj = (object) defaultTypeID;
            break;
        }
        if (objectType != null)
        {
          IDBAttributeType attributeType = objectType.GetAttributeType(attrID);
          if (attributeType != null)
          {
            AttributeValues attributeValues = new AttributeValues(attrID, attributeType.AttributeType, attributeType.MultipleValued, new object[1]
            {
              obj ?? attributeType.DefaultValue
            })
            {
              AttributeName = attributeType.Name,
              AttributeGuid = attributeType.PropertiesStructure.AttributeGuid,
              ReadOnly = (attributeType.Options & AttributeOptions.DisableManualEdit) != 0
            };
            attributeValuesByIds.Add(attributeValues);
            continue;
          }
        }
        IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(attrID);
        if (attributeType1 != null)
        {
          AttributeValues attributeValues = new AttributeValues(attrID, attributeType1.AttributeType, attributeType1.MultipleValued, new object[1]
          {
            obj ?? attributeType1.DefaultValue
          })
          {
            AttributeName = attributeType1.Name,
            AttributeGuid = attributeType1.PropertiesStructure.AttributeGuid
          };
          attributeValuesByIds.Add(attributeValues);
        }
      }
      if (attributeValuesByIds.Count > 0 && objTypes != null)
      {
        foreach (int objType in objTypes)
        {
          foreach (AttributeValues attributeValues in attributeValuesByIds)
          {
            if (!attributeValues.ReadOnly)
            {
              IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objType, attributeValues.AttributeID);
              attributeValues.ReadOnly = attribute4ObjectType != null && (attribute4ObjectType.Options & AttributeOptions.DisableManualEdit) != 0;
            }
          }
        }
      }
      return attributeValuesByIds;
    }
  }

  /// <summary>Получить маску для строковых атрибутов.</summary>
  /// <param name="objTypeIDs">Список идентификаторов выбранных типов объектов</param>
  /// <param name="attrIDs">Список идентификаторов выбранных атрибутов</param>
  /// <returns>Словарь с идентификаторами атрибутов и соответствующих им масок</returns>
  private Dictionary<int, string> GetAttributesMasks(List<int> objTypeIDs, List<int> attrIDs)
  {
    Dictionary<int, string> dictionary = new Dictionary<int, string>(attrIDs.Count);
    if (objTypeIDs != null && objTypeIDs.Count == 1)
    {
      string empty = string.Empty;
      foreach (int attrId in attrIDs)
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objTypeIDs[0], attrId);
        string mask;
        if (attribute4ObjectType == null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
          if (attributeType != null && attributeType.FieldType == FieldTypes.ftString)
            mask = attributeType.Mask;
          else
            continue;
        }
        else if (attribute4ObjectType.FieldType == FieldTypes.ftString)
          mask = attribute4ObjectType.Mask;
        else
          continue;
        if (!string.IsNullOrEmpty(mask) && !dictionary.ContainsKey(attrId))
          dictionary.Add(attrId, mask);
      }
    }
    else
    {
      foreach (int attrId in attrIDs)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
        if (attributeType != null && attributeType.FieldType == FieldTypes.ftString && !string.IsNullOrEmpty(attributeType.Mask) && !dictionary.ContainsKey(attrId))
          dictionary.Add(attrId, attributeType.Mask);
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, string>) null : dictionary;
  }

  /// <summary>Получить общие атрибуты для списка типов объектов.</summary>
  /// <param name="typeIDs">Список идентификаторов типов объектов</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты
  /// При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <returns>Общий список идентификаторов атрибутов (атрибутов, которыеприсутствуют у всех указанных типов объектов)</returns>
  private List<int> GetCommonAttrsForTypes(List<int> typeIDs, bool showAutoRequiredAttrs)
  {
    List<int> commonAttrsForTypes = (List<int>) null;
    if (typeIDs != null && typeIDs.Count > 0)
    {
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList1 = MetaDataHelper.GetAttribute4ObjectTypeList(typeIDs[0]);
      if (attribute4ObjectTypeList1 != null)
      {
        if (!showAutoRequiredAttrs)
          attribute4ObjectTypeList1 = attribute4ObjectTypeList1.Where<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.Required != RequiredModes.AutoRequired)).ToList<IMSAttribute4ObjectType>();
        for (int index = 1; index < typeIDs.Count; ++index)
        {
          List<IMSAttribute4ObjectType> attribute4ObjectTypeList2 = MetaDataHelper.GetAttribute4ObjectTypeList(typeIDs[index]);
          if (attribute4ObjectTypeList2 == null)
          {
            attribute4ObjectTypeList1.Clear();
            break;
          }
          if (!showAutoRequiredAttrs)
            attribute4ObjectTypeList2 = attribute4ObjectTypeList2.Where<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.Required != RequiredModes.AutoRequired)).ToList<IMSAttribute4ObjectType>();
          attribute4ObjectTypeList1 = attribute4ObjectTypeList1.Intersect<IMSAttribute4ObjectType>((IEnumerable<IMSAttribute4ObjectType>) attribute4ObjectTypeList2).ToList<IMSAttribute4ObjectType>();
          if (attribute4ObjectTypeList1.Count == 0)
            break;
        }
        if (attribute4ObjectTypeList1.Count > 0)
          commonAttrsForTypes = attribute4ObjectTypeList1.Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (x => x.AttributeID)).ToList<int>().Distinct<int>().ToList<int>();
      }
    }
    return commonAttrsForTypes;
  }

  /// <summary>
  /// Получение идентификатора, выделенного в гриде навигатора атрибута.
  /// </summary>
  /// <param name="services">Список сервисов</param>
  private void GetSelectedAttrIDOnStartup(System.IServiceProvider services)
  {
    if (services == null || !(services.GetService(typeof (IFocusedItem)) is IFocusedItem service))
      return;
    NodeColumn focusedColumn = service.FocusedColumn;
    if (focusedColumn == null)
      return;
    this._selectedAttrIDOnStartup = focusedColumn.Attribute.AttributeID;
  }

  /// <summary>Получить информацию о выделенных объектах.</summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="anyAttrs">
  /// Разрешение на добавления любого атрибута.
  /// (флаг устанавливается в true только если все типы выделенных объектов разрешают добавление любого атрибута)
  /// </param>
  /// <returns>Словарь со списком идентификаторов выделенных объектов сгруппированных по типам объектов</returns>
  private Dictionary<int, List<long>> GroupSelectedObjectsByType(
    ISelectedItems items,
    out bool anyAttrs)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>(items.Count);
    anyAttrs = true;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.ObjectID != 0L && itemData.ObjectType != -1)
      {
        if (dictionary.ContainsKey(itemData.ObjectType))
        {
          dictionary[itemData.ObjectType].Add(itemData.ObjectID);
        }
        else
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(itemData.ObjectType);
          if (objectType != null)
          {
            if (anyAttrs && !objectType.AnyAttributes)
              anyAttrs = false;
            dictionary.Add(itemData.ObjectType, new List<long>((IEnumerable<long>) new long[1]
            {
              itemData.ObjectID
            }));
          }
        }
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, List<long>>) null : dictionary;
  }

  /// <summary>Сгруппировать выделенные объекты по типам.</summary>
  /// <param name="items">Выделенные объекты</param>
  /// <returns>Словарь со списком идентификаторов выделенных объектов сгруппированных по типам объектов</returns>
  private Dictionary<int, List<long>> GroupSelectedObjectsByType(ISelectedItems items)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        int objectType = itemData.ObjectType;
        if (objectType != -1)
        {
          if (dictionary.ContainsKey(objectType))
            dictionary[objectType].Add(itemData.ObjectID);
          else
            dictionary.Add(objectType, new List<long>()
            {
              itemData.ObjectID
            });
        }
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, List<long>>) null : dictionary;
  }

  /// <summary>Обработка ошибок.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnProcessException(object sender, ExceptionEventArgs e)
  {
    if (e == null || !(e.Exception is AdditionalCommandProviderException exception))
      return;
    this._questionFormResult = AdditionalCommandProviderExceptionForm.Show(exception);
    e.Handled = true;
  }

  /// <summary>Удаление атрибутов.</summary>
  /// <param name="selectedObjInfo">Информация о выделенных объектах рассортированных по типам</param>
  /// <param name="attrIDs">Список идентификаторов втрибутов</param>
  /// <returns>Результат удаления</returns>
  private SortedList<int, List<long>> RemoveObjectAttributes(
    Dictionary<int, List<long>> selectedObjInfo,
    List<int> attrIDs)
  {
    SortedList<int, List<long>> sortedList = new SortedList<int, List<long>>();
    using (ValidationForDeleteAttributes deleteAttributes = new ValidationForDeleteAttributes(attrIDs))
    {
      if (deleteAttributes.ShowDialog() == DialogResult.No)
        return sortedList;
    }
    bool ignoreExceptions = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (KeyValuePair<int, List<long>> keyValuePair in selectedObjInfo)
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(keyValuePair.Key);
        if (objectCollection != null)
        {
          foreach (int attrId in attrIDs)
          {
            List<long> longList = new List<long>((IEnumerable<long>) keyValuePair.Value);
            while (longList.Count > 0)
            {
              CommandResult commandResult = objectCollection.DeleteAttribute(longList.ToArray(), (object) attrId, ignoreExceptions);
              if (sortedList.ContainsKey(attrId))
                sortedList[attrId].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
              else
                sortedList.Add(attrId, new List<long>((IEnumerable<long>) commandResult.ProcessedObjects));
              foreach (long processedObject in commandResult.ProcessedObjects)
                longList.Remove(processedObject);
              if (!string.IsNullOrEmpty(commandResult.ErrorMessage) && !ignoreExceptions)
              {
                switch (AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149")))
                {
                  case QuestionFormResult.Skip:
                    longList.Remove(commandResult.ErrorObjectID);
                    continue;
                  case QuestionFormResult.SkipAll:
                    longList.Remove(commandResult.ErrorObjectID);
                    ignoreExceptions = true;
                    continue;
                  case QuestionFormResult.Break:
                    return sortedList;
                  default:
                    continue;
                }
              }
              else
                longList.Clear();
            }
          }
        }
      }
    }
    return sortedList;
  }

  /// <summary>Выбор атрибутов в диалоге.</summary>
  /// <remarks>В диалоге будут отображаться только те атрибуты, которые добавлены объекту</remarks>
  /// <param name="objID">Идентификатор выделенного объекта</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты
  /// При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <param name="obligatoryAttrs">Список системных атрибутов, которые необходимо добавить в список выбираемых атрибутов</param>
  /// <returns>Список идентификаторов выбранных атрибутов</returns>
  private List<int> SelectAttributes(
    long objID,
    bool showAutoRequiredAttrs,
    List<int> obligatoryAttrs)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      if (obligatoryAttrs != null)
      {
        attributesSelectDlg.ObligatoryAttrsList = obligatoryAttrs;
        attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[3]
        {
          FieldTypes.ftBlob,
          FieldTypes.ftShortBlob,
          FieldTypes.ftFile
        });
      }
      else
        attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
        {
          FieldTypes.ftBlob,
          FieldTypes.ftShortBlob,
          FieldTypes.ftFile,
          FieldTypes.ftSystem
        });
      attributesSelectDlg.SelectedAttributeIDOnStartup(this._selectedAttrIDOnStartup);
      attributesSelectDlg.LoadAttrDialogForObject(objID, showAutoRequiredAttrs);
      return attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0 ? (List<int>) null : attributesSelectDlg.SelectedAttributesID;
    }
  }

  /// <summary>Выбор атрибутов в диалоге.</summary>
  /// <param name="dict">Словарь для фильтра. В нем содержатся идентификаторы атрибутов, присутствующих у объекта.
  /// Словарь заполнен только когда рассматривается один объект.</param>
  /// <param name="anyAttrs">Признак возможности добавления любого атрибута объекту</param>
  /// <param name="objTypes">Рассматриваемые типы объектов</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты
  /// При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <param name="obligatoryAttrs">Список системных атрибутов, которые необходимо добавить в список выбираемых атрибутов</param>
  /// <returns>Список идентификаторов выбранных атрибутов</returns>
  private List<int> SelectAttributes(
    Dictionary<int, List<int>> dict,
    bool anyAttrs,
    List<int> objTypes,
    bool showAutoRequiredAttrs,
    List<int> obligatoryAttrs)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.SelectorFilter = dict == null || dict.Count <= 0 ? (ISelectorFilter) null : (ISelectorFilter) new AdditionalCommandProvider.AttributeTypeFilter(dict);
      if (obligatoryAttrs != null)
      {
        attributesSelectDlg.ObligatoryAttrsList = obligatoryAttrs;
        attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[3]
        {
          FieldTypes.ftBlob,
          FieldTypes.ftShortBlob,
          FieldTypes.ftFile
        });
      }
      else
        attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
        {
          FieldTypes.ftBlob,
          FieldTypes.ftShortBlob,
          FieldTypes.ftFile,
          FieldTypes.ftSystem
        });
      if (!anyAttrs)
      {
        if (objTypes.Count > 1)
        {
          attributesSelectDlg.LoadAttrDialogForCommonAttrs(objTypes, this.GetCommonAttrsForTypes(objTypes, showAutoRequiredAttrs) ?? throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.CanNotFindCommonAttributes"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          }, AttributableElements.Object);
        }
        else
        {
          attributesSelectDlg.LoadAttrDialogForObjectsTypes(new List<int>((IEnumerable<int>) objTypes.ToArray()), showAutoRequiredAttrs);
          attributesSelectDlg.TypeAttributesOnly = true;
        }
      }
      else if (objTypes.Count == 1)
      {
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objTypes[0]);
        if (objectTypeGuid == Guid.Empty)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.CanNotFindObjectTypeGuid"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        attributesSelectDlg.LoadAttrDialogForObjectsTypes(objectTypeGuid);
      }
      attributesSelectDlg.RelationGroupEnable = false;
      attributesSelectDlg.SelectedAttributeIDOnStartup(this._selectedAttrIDOnStartup);
      return attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0 ? (List<int>) null : attributesSelectDlg.SelectedAttributesID;
    }
  }

  /// <summary>Добавление атрибута к объектам.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void AddAttributeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    bool anyAttrs = true;
    Dictionary<int, List<int>> dict = (Dictionary<int, List<int>>) null;
    this.GetSelectedAttrIDOnStartup(viewServices);
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      Dictionary<int, List<long>> selectedObjInfo;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1506"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        IMSObjectType imsObjectType = itemData.ObjectID != 0L ? MetaDataHelper.GetObjectType(itemData.ObjectType) : throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) itemData.ObjectID))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
        if (imsObjectType == null)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.CanNotDefineObjectType"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        selectedObjInfo = new Dictionary<int, List<long>>()
        {
          {
            itemData.ObjectType,
            new List<long>() { itemData.ObjectID }
          }
        };
        anyAttrs = imsObjectType.AnyAttributes;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          AttributeValues[] attributesValues = (sessionKeeper.Session.GetObjectActualCopy(itemData.ObjectID, false) ?? throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) itemData.ObjectID))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          }).GetAttributesValues(GetAttributeValuesModes.None);
          if (attributesValues != null)
            dict = ((IEnumerable<AttributeValues>) attributesValues).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToDictionary<int, int, List<int>>((System.Func<int, int>) (x => x), (System.Func<int, List<int>>) (y => new List<int>(0)));
        }
      }
      else
        selectedObjInfo = this.GroupSelectedObjectsByType(items, out anyAttrs);
      if (selectedObjInfo == null)
        return;
      List<int> list = selectedObjInfo.Keys.ToList<int>();
      List<int> attrIDs = this.SelectAttributes(dict, anyAttrs, list, true, (List<int>) null);
      if (attrIDs == null)
        return;
      List<AttributeValues> attributeValuesByIds = this.GetAttributeValuesByIDs(0L, list.Count == 1 ? list[0] : -1, attrIDs, list[0], list);
      if (attributeValuesByIds.Count == 0)
        throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.SelectedAttributesInfo.CanNotFind.Msg"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
      Dictionary<int, string> attributesMasks = this.GetAttributesMasks(list, attrIDs);
      this.FireEvent(this.AddEditAttributes(selectedObjInfo, attributeValuesByIds, true, attributesMasks));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Добавление группы атрибутов.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void AddAttributeGroupCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      Dictionary<int, List<long>> selectedObjInfo;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1506"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        if (itemData.ObjectID == 0L)
          throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) itemData.ObjectID))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        if (MetaDataHelper.GetObjectType(itemData.ObjectType) == null)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.CanNotDefineObjectType"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        selectedObjInfo = new Dictionary<int, List<long>>()
        {
          {
            itemData.ObjectType,
            new List<long>() { itemData.ObjectID }
          }
        };
      }
      else
      {
        bool anyAttrs = true;
        selectedObjInfo = this.GroupSelectedObjectsByType(items, out anyAttrs);
      }
      List<int> groupIDs = (List<int>) null;
      using (TreeViewForm treeViewForm = new TreeViewForm(1, (List<int>) null))
      {
        if (treeViewForm.ShowDialog() == DialogResult.OK)
        {
          if (treeViewForm.SelectedNodeID.Count > 0)
            groupIDs = treeViewForm.SelectedNodeID;
        }
      }
      if (groupIDs == null || groupIDs.Count <= 0)
        return;
      List<int> attributesFromGroups = this.GetAttributesFromGroups(groupIDs);
      if (attributesFromGroups == null)
        return;
      List<AttributeValues> attributeValuesByIds = this.GetAttributeValuesByIDs(0L, selectedObjInfo.Count == 1 ? selectedObjInfo.Keys.ElementAt<int>(0) : -1, attributesFromGroups, -1, selectedObjInfo.Keys.ToList<int>());
      Dictionary<int, string> attributesMasks = this.GetAttributesMasks((List<int>) null, attributesFromGroups);
      this.FireEvent(this.AddEditAttributes(selectedObjInfo, attributeValuesByIds, true, attributesMasks));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>
  /// Получить список уникальных идентификаторов атрибутов из указанных групп.
  /// </summary>
  /// <param name="groupIDs">Список идентификаторов групп атрибутов</param>
  /// <returns>Список идентификаторов атрибутов</returns>
  private List<int> GetAttributesFromGroups(List<int> groupIDs)
  {
    List<int> source = new List<int>();
    foreach (int groupId in groupIDs)
    {
      List<int> attributesInGroup = MetaDataHelper.GetAttributesInGroup(groupId);
      if (attributesInGroup != null && attributesInGroup.Count != 0)
      {
        source.AddRange((IEnumerable<int>) attributesInGroup);
        source = source.Distinct<int>().ToList<int>();
        if (source.Count > 500)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client_Core_AttributeGroup_LargeQuantity"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
      }
    }
    return source.Count <= 0 ? (List<int>) null : source;
  }

  /// <summary>Удаление атрибута у объектов.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void DeleteAttributeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    bool anyAttrs = true;
    int count = items != null ? items.Count : 0;
    List<int> intList = new List<int>();
    Dictionary<int, List<long>> selectedObjInfo = new Dictionary<int, List<long>>();
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    this.GetSelectedAttrIDOnStartup(viewServices);
    try
    {
      List<int> attrIDs;
      if (count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectID == 0L)
        {
          AdditionalCommandProviderException providerException = new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1059"));
          int num;
          bool flag = (num = 0) != 0;
          providerException.BtnSkipAllVisible = num != 0;
          providerException.BtnSkipVisible = flag;
          throw providerException;
        }
        intList.Add(itemData.ObjectType);
        selectedObjInfo.Add(itemData.ObjectType, new List<long>((IEnumerable<long>) new long[1]
        {
          itemData.ObjectID
        }));
        attrIDs = this.SelectAttributes(itemData.ObjectID, false, (List<int>) null);
      }
      else
      {
        selectedObjInfo = this.GroupSelectedObjectsByType(items, out anyAttrs);
        List<int> list = selectedObjInfo.Keys.ToList<int>();
        attrIDs = this.SelectAttributes(new Dictionary<int, List<int>>(), anyAttrs, list, false, (List<int>) null);
      }
      if (attrIDs == null)
        return;
      this.FireEvent(this.RemoveObjectAttributes(selectedObjInfo, attrIDs));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Удаление группы атрибутов у объектов.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void DeleteAttributeGroupCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count <= 0)
      return;
    Dictionary<int, List<long>> objByTypeDict = this.GroupSelectedObjectsByType(items);
    if (objByTypeDict == null)
      return;
    Dictionary<int, List<int>> groupIDs = this.GetAttributeGroups(objByTypeDict);
    if (groupIDs != null)
    {
      List<int> attrIDs = (List<int>) null;
      using (TreeViewForm treeViewForm = new TreeViewForm(1, (List<int>) null, groupIDs.Keys.ToList<int>()))
      {
        if (treeViewForm.ShowDialog() == DialogResult.OK)
        {
          List<int> selectedNodeId = treeViewForm.SelectedNodeID;
          if (selectedNodeId != null)
          {
            if (selectedNodeId.Count > 0)
            {
              attrIDs = new List<int>();
              selectedNodeId.ForEach((Action<int>) (x => attrIDs.AddRange((IEnumerable<int>) groupIDs[x])));
              attrIDs = attrIDs.Distinct<int>().ToList<int>();
            }
          }
        }
      }
      if (attrIDs == null)
        return;
      if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
        service.HandleException += new ExceptionHandler(this.OnProcessException);
      bool ignoreExceptions = false;
      SortedList<int, List<long>> Result = new SortedList<int, List<long>>();
      List<long> collection = new List<long>();
      foreach (KeyValuePair<int, List<long>> keyValuePair in objByTypeDict)
        collection.AddRange((IEnumerable<long>) keyValuePair.Value);
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
          foreach (int num in attrIDs)
          {
            List<long> objects = new List<long>((IEnumerable<long>) collection);
            while (objects.Count > 0)
            {
              CommandResult commandResult = objectCollection.DeleteAttribute(objects.ToArray(), (object) num, ignoreExceptions);
              if (commandResult.ProcessedObjects.Length != 0)
              {
                if (Result.ContainsKey(num))
                  Result[num].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
                else
                  Result[num] = new List<long>((IEnumerable<long>) commandResult.ProcessedObjects);
                ((IEnumerable<long>) commandResult.ProcessedObjects).ToList<long>().ForEach((Action<long>) (x => objects.Remove(x)));
              }
              if (!string.IsNullOrEmpty(commandResult.ErrorMessage) && !ignoreExceptions)
              {
                switch (AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1031")))
                {
                  case QuestionFormResult.SkipAll:
                    ignoreExceptions = true;
                    break;
                  case QuestionFormResult.Break:
                    return;
                }
                objects.Remove(commandResult.ErrorObjectID);
              }
              else
                objects.Clear();
            }
          }
        }
        this.FireEvent(Result);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
      finally
      {
        if (service != null)
          service.HandleException -= new ExceptionHandler(this.OnProcessException);
      }
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("ClientCore_MessageBox_Caption_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ClientCore_DeleteAttributeGroup_CanNotAttributesForDeleting"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>Редактирование значений атрибутов у объектов.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void EditAttributeValueCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    bool anyAttrs = true;
    List<int> attrIDs = (List<int>) null;
    List<int> obligatoryAttrs = new List<int>()
    {
      -8,
      -14,
      -7
    };
    this.GetSelectedAttrIDOnStartup(viewServices);
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      Dictionary<int, List<long>> dictionary;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1506"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        if (itemData.ObjectID == 0L)
          throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Client.Core.ObjectNotFind.Msg"), (object) itemData.ObjectID))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        dictionary = new Dictionary<int, List<long>>()
        {
          {
            itemData.ObjectType,
            new List<long>() { itemData.ObjectID }
          }
        };
        attrIDs = this.SelectAttributes(itemData.ObjectID, true, obligatoryAttrs);
      }
      else
      {
        dictionary = this.GroupSelectedObjectsByType(items, out anyAttrs);
        if (dictionary != null)
          attrIDs = this.SelectAttributes(new Dictionary<int, List<int>>(), anyAttrs, dictionary.Keys.ToList<int>(), true, obligatoryAttrs);
      }
      if (attrIDs == null)
        return;
      List<int> list = dictionary.Keys.ToList<int>();
      List<AttributeValues> attributeValuesByIds;
      if (dictionary.Count == 1)
      {
        KeyValuePair<int, List<long>> keyValuePair = dictionary.First<KeyValuePair<int, List<long>>>();
        attributeValuesByIds = this.GetAttributeValuesByIDs(keyValuePair.Value.Count == 1 ? keyValuePair.Value[0] : 0L, keyValuePair.Key, attrIDs, keyValuePair.Key, list);
      }
      else
        attributeValuesByIds = this.GetAttributeValuesByIDs(0L, -1, attrIDs, list[0], list);
      if (attributeValuesByIds.Count == 0)
        throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.SelectedAttributesInfo.CanNotFind.Msg"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
      Dictionary<int, string> attributesMasks = this.GetAttributesMasks(list, attrIDs);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrIDs[0]);
      this.FireEvent(items.Count <= 1 || attrIDs.Count != 1 || attributeType.MultiValueMode != MultiValueModes.MultiValues && attributeType.MultiValueMode != MultiValueModes.MultiValuesFromList ? this.AddEditAttributes(dictionary, attributeValuesByIds, false, attributesMasks) : this.EditAttributeMultiValue(attrIDs[0], dictionary, attributeValuesByIds, attributesMasks));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  private SortedList<int, List<long>> EditAttributeMultiValue(
    int attrId,
    Dictionary<int, List<long>> selectedObjInfo,
    List<AttributeValues> values,
    Dictionary<int, string> masks)
  {
    string empty = string.Empty;
    masks?.TryGetValue(attrId, out empty);
    using (MultiValueEditorForm multiValueEditorForm = new MultiValueEditorForm(attrId, values[0], empty))
    {
      SortedList<int, List<long>> sortedList = new SortedList<int, List<long>>();
      if (multiValueEditorForm.ShowDialog() != DialogResult.OK)
        return sortedList;
      if (multiValueEditorForm.EditorMode == MultiValueEditorMode.SetValue)
        return this.AddEditAttributesSaveValues(new List<AttributeValues>()
        {
          multiValueEditorForm.Values
        }, selectedObjInfo, false);
      AttributeValues values1 = multiValueEditorForm.Values;
      AttributeValues replaceValues = multiValueEditorForm.ReplaceValues;
      MultiValueEditorMode editorMode = multiValueEditorForm.EditorMode;
      if (values1 == null || values1.Values[0] == DBNull.Value || editorMode == MultiValueEditorMode.ReplaceValue && (replaceValues?.Values == null || replaceValues.Values[0] == DBNull.Value))
        return sortedList;
      bool flag = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        foreach (KeyValuePair<int, List<long>> keyValuePair1 in selectedObjInfo)
        {
          IDBObjectCollection objectCollection = session.GetObjectCollection(keyValuePair1.Key);
          if (objectCollection != null)
          {
            int attributeId = values1.AttributeID;
            if (attributeId == 0)
              return sortedList;
            Guid attributeGUID = values1.AttributeGuid != Guid.Empty ? values1.AttributeGuid : MetaDataHelper.GetAttributeType(attributeId).AttributeGuid;
            Dictionary<long, object[]> dictionary = new Dictionary<long, object[]>();
            foreach (long num in keyValuePair1.Value)
            {
              object[] objArray = session.GetObjectAttributeValuesByGuid(num, attributeGUID);
              if (objArray == null)
              {
                if (editorMode == MultiValueEditorMode.AddValue)
                  objArray = new object[0];
                else
                  continue;
              }
              dictionary.Add(num, objArray);
            }
            foreach (KeyValuePair<long, object[]> keyValuePair2 in dictionary)
            {
              List<object> list = ((IEnumerable<object>) keyValuePair2.Value).ToList<object>();
              foreach (object obj in values1.Values)
              {
                switch (editorMode)
                {
                  case MultiValueEditorMode.AddValue:
                    if (!list.Contains(obj))
                    {
                      list.Add(obj);
                      break;
                    }
                    break;
                  case MultiValueEditorMode.DelValue:
                    if (list.Contains(obj))
                    {
                      list.Remove(obj);
                      break;
                    }
                    break;
                  case MultiValueEditorMode.ReplaceValue:
                    int index = list.IndexOf(obj);
                    if (index >= 0)
                    {
                      list.Remove(obj);
                      list.Insert(index, replaceValues.Values[0]);
                      break;
                    }
                    break;
                }
              }
              if (list.Count == 0)
                list.Add((object) DBNull.Value);
              object[] array = list.ToArray();
              if (!AttributeValues.ValuesEquals(keyValuePair2.Value, array))
              {
                CommandResult commandResult1;
                if (editorMode != MultiValueEditorMode.AddValue)
                  commandResult1 = objectCollection.EditAttribute(new long[1]
                  {
                    keyValuePair2.Key
                  }, (object) attributeId, array, (flag ? 1 : 0) != 0);
                else
                  commandResult1 = objectCollection.AddAttribute(new long[1]
                  {
                    keyValuePair2.Key
                  }, (object) attributeId, array, (flag ? 1 : 0) != 0);
                CommandResult commandResult2 = commandResult1;
                if (commandResult2.ProcessedObjects.Length != 0)
                {
                  if (sortedList.ContainsKey(attributeId))
                    sortedList[attributeId].AddRange((IEnumerable<long>) commandResult2.ProcessedObjects);
                  else
                    sortedList.Add(attributeId, new List<long>((IEnumerable<long>) commandResult2.ProcessedObjects));
                }
                if (!string.IsNullOrEmpty(commandResult2.ErrorMessage) && !flag)
                {
                  QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult2.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149"), false, false);
                  if (questionFormResult == QuestionFormResult.Break)
                    return sortedList;
                  flag = questionFormResult == QuestionFormResult.SkipAll;
                }
              }
            }
          }
        }
      }
      return sortedList;
    }
  }

  /// <summary>Сравнение атрибутов объектов</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void ObjectsDiff(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    AdditionalCommandProvider.ObjectsDiff((items.GetItemID(0) as NodeID).ObjectID, (items.GetItemID(1) as NodeID).ObjectID);
  }

  private static void ObjectsDiff(long leftObjectID, long rightObjectID)
  {
    using (ObjectDiffDialog objectDiffDialog = new ObjectDiffDialog())
    {
      objectDiffDialog.SetObjectVersionIds(leftObjectID, rightObjectID);
      int num = (int) objectDiffDialog.ShowDialog();
    }
  }

  /// <summary>Рассылка сообщения об изменении объектов.</summary>
  /// <param name="Result">Список измененных объектов</param>
  public void FireEvent(SortedList<int, List<long>> Result)
  {
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in Result)
    {
      foreach (long num in keyValuePair.Value)
      {
        if (!objectIDs.Contains(num))
          objectIDs.Add(num);
      }
    }
    if (objectIDs.Count <= 0)
      return;
    (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs));
  }

  /// <summary>
  /// Показать диалог с историей изменения атрибутов выбранного объекта.
  /// </summary>
  /// <param name="items">Выбранные пользователем объекты</param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void ShowAttributeHistory(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetSystemSecurity().CheckAccess(ActionType.ShowHistory);
    using (AttributesHistory attributesHistory = new AttributesHistory(items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID, items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID))
    {
      int num = (int) attributesHistory.ShowDialog();
    }
  }

  /// <summary>Добавление атрибута к связям.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void AddRelationAttributeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.GetSelectedAttrIDOnStartup(viewServices);
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      Dictionary<int, List<int>> dict = (Dictionary<int, List<int>>) null;
      bool anyAttrs = true;
      Dictionary<int, List<long>> selectedRelInfo;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Attributes_RealtionUndefined"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        anyAttrs = (MetaDataHelper.GetRelationType(itemData.RelationType) ?? throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1060"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        }).AnyAttributes;
        selectedRelInfo = new Dictionary<int, List<long>>()
        {
          {
            itemData.RelationType,
            new List<long>() { itemData.Value }
          }
        };
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          AttributeValues[] attributesValues = (sessionKeeper.Session.GetRelation(itemData.Value, false) ?? throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Relation_CanNotFindByID"), (object) itemData.Value))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          }).GetAttributesValues(GetAttributeValuesModes.None);
          if (attributesValues != null)
            dict = ((IEnumerable<AttributeValues>) attributesValues).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToDictionary<int, int, List<int>>((System.Func<int, int>) (x => x), (System.Func<int, List<int>>) (y => new List<int>(0)));
        }
      }
      else
        selectedRelInfo = this.GroupRelationsByType(items, out anyAttrs);
      if (selectedRelInfo == null)
        return;
      List<int> list = selectedRelInfo.Keys.ToList<int>();
      List<int> attrIDs = this.SelectAttributesForRelations(dict, anyAttrs, list, true);
      if (attrIDs == null)
        return;
      List<AttributeValues> valuesByRelationIds = this.GetAttributeValuesByRelationIDs(0L, list.Count == 1 ? list[0] : -1, attrIDs, list);
      if (valuesByRelationIds == null)
        throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.SelectedAttributesInfo.CanNotFind.Msg"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
      Dictionary<int, string> forRelationTypes = this.GetAttributesMasksForRelationTypes(list, attrIDs);
      this.FireEventForRelation(this.AddEditAttributesForRelations(selectedRelInfo, valuesByRelationIds, true, forRelationTypes));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Добавление группы атрибутов.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void AddRelationAttributeGroupCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      Dictionary<int, List<long>> selectedRelInfo;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Attributes_RealtionUndefined"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        if (MetaDataHelper.GetRelationType(itemData.RelationType) == null)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core_1060"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        selectedRelInfo = new Dictionary<int, List<long>>()
        {
          {
            itemData.RelationType,
            new List<long>() { itemData.Value }
          }
        };
      }
      else
      {
        bool anyAttrs = true;
        selectedRelInfo = this.GroupRelationsByType(items, out anyAttrs);
      }
      List<int> groupIDs = (List<int>) null;
      using (TreeViewForm treeViewForm = new TreeViewForm(1, (List<int>) null))
      {
        if (treeViewForm.ShowDialog() == DialogResult.OK)
        {
          if (treeViewForm.SelectedNodeID.Count > 0)
            groupIDs = treeViewForm.SelectedNodeID;
        }
      }
      if (groupIDs == null || groupIDs.Count <= 0)
        return;
      List<int> attributesFromGroups = this.GetAttributesFromGroups(groupIDs);
      if (attributesFromGroups == null)
        return;
      List<int> list = selectedRelInfo.Keys.ToList<int>();
      List<AttributeValues> valuesByRelationIds = this.GetAttributeValuesByRelationIDs(0L, list.Count == 1 ? list[0] : -1, attributesFromGroups, list);
      if (valuesByRelationIds == null)
        throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.SelectedAttributesInfo.CanNotFind.Msg"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
      Dictionary<int, string> forRelationTypes = this.GetAttributesMasksForRelationTypes((List<int>) null, attributesFromGroups);
      this.FireEventForRelation(this.AddEditAttributesForRelations(selectedRelInfo, valuesByRelationIds, true, forRelationTypes));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Редактирование значений атрибутов у связей.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void EditRelationAttributeValueCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.GetSelectedAttrIDOnStartup(viewServices);
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      List<int> attrIDs = (List<int>) null;
      bool anyAttrs = true;
      Dictionary<int, List<long>> dictionary;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Attributes_RealtionUndefined"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        dictionary = new Dictionary<int, List<long>>()
        {
          {
            itemData.RelationType,
            new List<long>() { itemData.Value }
          }
        };
        attrIDs = this.SelectAttributesForRelation(itemData.Value, true);
      }
      else
      {
        dictionary = this.GroupRelationsByType(items, out anyAttrs);
        if (dictionary != null)
          attrIDs = this.SelectAttributesForRelations((Dictionary<int, List<int>>) null, anyAttrs, dictionary.Keys.ToList<int>(), true);
      }
      if (attrIDs == null)
        return;
      List<int> list = dictionary.Keys.ToList<int>();
      List<AttributeValues> valuesByRelationIds;
      if (dictionary.Count == 1)
      {
        KeyValuePair<int, List<long>> keyValuePair = dictionary.ElementAt<KeyValuePair<int, List<long>>>(0);
        valuesByRelationIds = this.GetAttributeValuesByRelationIDs(keyValuePair.Value.Count == 1 ? keyValuePair.Value[0] : 0L, keyValuePair.Key, attrIDs, list);
      }
      else
        valuesByRelationIds = this.GetAttributeValuesByRelationIDs(0L, -1, attrIDs, list);
      if (valuesByRelationIds.Count == 0)
        throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Client.Core.SelectedAttributesInfo.CanNotFind.Msg"))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        };
      Dictionary<int, string> forRelationTypes = this.GetAttributesMasksForRelationTypes(list, attrIDs);
      this.FireEventForRelation(this.AddEditAttributesForRelations(dictionary, valuesByRelationIds, false, forRelationTypes));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Удаление атрибута у связей.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void DeleteRelationAttributeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.GetSelectedAttrIDOnStartup(viewServices);
    if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
      service.HandleException += new ExceptionHandler(this.OnProcessException);
    try
    {
      List<int> attrIDs = (List<int>) null;
      bool anyAttrs = true;
      Dictionary<int, List<long>> selectedRelInfo;
      if (items.Count == 1)
      {
        if (!(items.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Attributes_RealtionUndefined"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        selectedRelInfo = new Dictionary<int, List<long>>()
        {
          {
            itemData.RelationType,
            new List<long>() { itemData.Value }
          }
        };
        attrIDs = this.SelectAttributesForRelation(itemData.Value, false);
      }
      else
      {
        selectedRelInfo = this.GroupRelationsByType(items, out anyAttrs);
        if (selectedRelInfo != null)
          attrIDs = this.SelectAttributesForRelations((Dictionary<int, List<int>>) null, anyAttrs, selectedRelInfo.Keys.ToList<int>(), false);
      }
      if (attrIDs == null)
        return;
      this.FireEventForRelation(this.RemoveRelationAttributes(selectedRelInfo, attrIDs));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    finally
    {
      if (service != null)
        service.HandleException -= new ExceptionHandler(this.OnProcessException);
    }
  }

  /// <summary>Удаление группы атрибутов у связей.</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public void DeleteRelationAttributeGroupCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count <= 0)
      return;
    Dictionary<int, List<long>> dictionary = this.GroupSelectedRelationsByType(items);
    if (dictionary == null)
      return;
    Dictionary<int, List<int>> groupIDs = this.GetAttributeGroupsForRelations(dictionary);
    if (groupIDs != null)
    {
      List<int> attrIDs = (List<int>) null;
      using (TreeViewForm treeViewForm = new TreeViewForm(1, (List<int>) null, groupIDs.Keys.ToList<int>()))
      {
        if (treeViewForm.ShowDialog() == DialogResult.OK)
        {
          List<int> selectedNodeId = treeViewForm.SelectedNodeID;
          if (selectedNodeId != null)
          {
            if (selectedNodeId.Count > 0)
            {
              attrIDs = new List<int>();
              selectedNodeId.ForEach((Action<int>) (x => attrIDs.AddRange((IEnumerable<int>) groupIDs[x])));
              attrIDs = attrIDs.Distinct<int>().ToList<int>();
            }
          }
        }
      }
      if (attrIDs == null)
        return;
      if (ServicesManager.GetService(typeof (IExceptionHandlerService)) is IExceptionHandlerService service)
        service.HandleException += new ExceptionHandler(this.OnProcessException);
      bool ignoreExceptions = false;
      SortedList<int, List<long>> result = new SortedList<int, List<long>>();
      List<long> list = dictionary.SelectMany<KeyValuePair<int, List<long>>, long>((System.Func<KeyValuePair<int, List<long>>, IEnumerable<long>>) (x => x.Value.Select<long, long>((System.Func<long, long>) (y => y)))).ToList<long>();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
          foreach (int num in attrIDs)
          {
            List<long> longList = new List<long>((IEnumerable<long>) list);
            while (longList.Count > 0)
            {
              CommandResult commandResult = relationCollection.DeleteAttribute(longList.ToArray(), (object) num, ignoreExceptions);
              if (commandResult.ProcessedObjects.Length != 0)
              {
                if (result.ContainsKey(num))
                  result[num].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
                else
                  result[num] = new List<long>((IEnumerable<long>) commandResult.ProcessedObjects);
              }
              if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
              {
                if (!ignoreExceptions)
                {
                  QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1031"), false, false);
                  if (questionFormResult == QuestionFormResult.Break)
                    return;
                  ignoreExceptions = questionFormResult == QuestionFormResult.SkipAll;
                }
                foreach (long processedObject in commandResult.ProcessedObjects)
                  longList.Remove(processedObject);
                longList.Remove(commandResult.ErrorObjectID);
              }
              else
                longList.Clear();
            }
          }
        }
        this.FireEventForRelation(result);
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
      }
      finally
      {
        if (service != null)
          service.HandleException -= new ExceptionHandler(this.OnProcessException);
      }
    }
    else
    {
      string caption = LocalizationHolder.rm.GetString("ClientCore_MessageBox_Caption_Message");
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ClientCore_DeleteAttributeGroup_CanNotAttributesForDeleting"), caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>Сгруппировать связи по типам.</summary>
  /// <param name="items">Выделенные элементы</param>
  /// <param name="anyAttrs">Разрешение на добавления любого атрибута (флаг устанавливается в true только если все типы выделенных объектов разрешают добавление любого атрибута)</param>
  /// <returns>Словарь связей, сгруппированных по типам</returns>
  private Dictionary<int, List<long>> GroupRelationsByType(ISelectedItems items, out bool anyAttrs)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>(items.Count);
    anyAttrs = true;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && itemData.Value != 0L && itemData.RelationType != -1)
      {
        if (dictionary.ContainsKey(itemData.RelationType))
        {
          dictionary[itemData.RelationType].Add(itemData.Value);
        }
        else
        {
          IMSRelationType relationType = MetaDataHelper.GetRelationType(itemData.RelationType);
          if (relationType != null)
          {
            if (anyAttrs && !relationType.AnyAttributes)
              anyAttrs = false;
            dictionary.Add(itemData.RelationType, new List<long>((IEnumerable<long>) new long[1]
            {
              itemData.Value
            }));
          }
        }
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, List<long>>) null : dictionary;
  }

  /// <summary>Сгруппировать связи по типам.</summary>
  /// <param name="items">Выделенные связи</param>
  /// <returns>Словарь связей, сгруппированных по типам</returns>
  private Dictionary<int, List<long>> GroupSelectedRelationsByType(ISelectedItems items)
  {
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData && itemData.Value != 0L)
      {
        int relationType = itemData.RelationType;
        if (relationType != -1)
        {
          if (dictionary.ContainsKey(relationType))
            dictionary[relationType].Add(itemData.Value);
          else
            dictionary.Add(relationType, new List<long>()
            {
              itemData.Value
            });
        }
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, List<long>>) null : dictionary;
  }

  /// <summary>Выбор атрибутов в диалоге.</summary>
  /// <param name="dict">Словарь для фильтра. В нем содержатся идентификаторы атрибутов, присутствующих у связей.
  /// Словарь заполнен только когда рассматривается один объект.</param>
  /// <param name="anyAttrs">Признак возможности добавления любого атрибута</param>
  /// <param name="relTypes">Рассматриваемые типы связей</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты. При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <returns>Список идентификаторов выбранных атрибутов</returns>
  private List<int> SelectAttributesForRelations(
    Dictionary<int, List<int>> dict,
    bool anyAttrs,
    List<int> relTypes,
    bool showAutoRequiredAttrs)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.SelectorFilter = dict == null || dict.Count <= 0 ? (ISelectorFilter) null : (ISelectorFilter) new AdditionalCommandProvider.AttributeTypeFilter(dict);
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftShortBlob,
        FieldTypes.ftFile,
        FieldTypes.ftSystem
      });
      if (!anyAttrs)
      {
        if (relTypes.Count > 1)
        {
          attributesSelectDlg.LoadAttrDialogForCommonAttrs(relTypes, this.GetCommonAttrsForRelationTypes(relTypes, showAutoRequiredAttrs) ?? throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("CommonRelationAttributes_Absent"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          }, AttributableElements.Relation);
        }
        else
        {
          attributesSelectDlg.LoadAttrDialogForRelationsTypes(new List<int>((IEnumerable<int>) relTypes.ToArray()));
          attributesSelectDlg.TypeAttributesOnly = true;
        }
      }
      else if (relTypes.Count == 1)
      {
        Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(relTypes[0]);
        if (relationTypeGuid == Guid.Empty)
          throw new AdditionalCommandProviderException(LocalizationHolder.rm.GetString("Relation_Guid_CanNotFind"))
          {
            BtnSkipVisible = false,
            BtnSkipAllVisible = false
          };
        attributesSelectDlg.LoadAttrDialogForRelationsTypes(relationTypeGuid);
      }
      attributesSelectDlg.RelationGroupEnable = true;
      attributesSelectDlg.ObjectGroupEnable = false;
      attributesSelectDlg.SelectedAttributeIDOnStartup(this._selectedAttrIDOnStartup);
      return attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0 ? (List<int>) null : attributesSelectDlg.SelectedAttributesID;
    }
  }

  /// <summary>Получить общие атрибуты для списка типов вязей.</summary>
  /// <param name="typeIDs">Список идентификаторов типов связей</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты. При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <returns>Общий список идентификаторов атрибутов (атрибутов, которые присутствуют у всех указанных типов связей)</returns>
  private List<int> GetCommonAttrsForRelationTypes(List<int> typeIDs, bool showAutoRequiredAttrs)
  {
    List<int> forRelationTypes = (List<int>) null;
    List<IMSAttribute4RelationType> attribute4RelationTypeList1 = MetaDataHelper.GetAttribute4RelationTypeList(typeIDs[0]);
    if (attribute4RelationTypeList1 != null)
    {
      if (!showAutoRequiredAttrs)
        attribute4RelationTypeList1 = attribute4RelationTypeList1.Where<IMSAttribute4RelationType>((System.Func<IMSAttribute4RelationType, bool>) (x => x.Required != RequiredModes.AutoRequired)).ToList<IMSAttribute4RelationType>();
      for (int index = 1; index < typeIDs.Count; ++index)
      {
        List<IMSAttribute4RelationType> attribute4RelationTypeList2 = MetaDataHelper.GetAttribute4RelationTypeList(typeIDs[index]);
        if (attribute4RelationTypeList2 == null)
        {
          attribute4RelationTypeList1.Clear();
          break;
        }
        if (!showAutoRequiredAttrs)
          attribute4RelationTypeList2 = attribute4RelationTypeList2.Where<IMSAttribute4RelationType>((System.Func<IMSAttribute4RelationType, bool>) (x => x.Required != RequiredModes.AutoRequired)).ToList<IMSAttribute4RelationType>();
        attribute4RelationTypeList1 = attribute4RelationTypeList1.Intersect<IMSAttribute4RelationType>((IEnumerable<IMSAttribute4RelationType>) attribute4RelationTypeList2).ToList<IMSAttribute4RelationType>();
        if (attribute4RelationTypeList1.Count == 0)
          break;
      }
      if (attribute4RelationTypeList1.Count > 0)
        forRelationTypes = attribute4RelationTypeList1.Select<IMSAttribute4RelationType, int>((System.Func<IMSAttribute4RelationType, int>) (x => x.AttributeID)).ToList<int>().Distinct<int>().ToList<int>();
    }
    return forRelationTypes;
  }

  /// <summary>Выбор атрибутов в диалоге.</summary>
  /// <remarks>В диалоге будут отображаться только те атрибуты, которые добавлены связи</remarks>
  /// <param name="relID">Идентификатор выделенной связи</param>
  /// <param name="showAutoRequiredAttrs">Нужно ли отображать обязательные атрибуты. При удалении отображать ненужно, т.к. удалить такие атрибуты всеравно нельзя</param>
  /// <returns>Список идентификаторов выбранных атрибутов</returns>
  private List<int> SelectAttributesForRelation(long relID, bool showAutoRequiredAttrs)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(true))
    {
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftShortBlob,
        FieldTypes.ftFile,
        FieldTypes.ftSystem
      });
      attributesSelectDlg.SelectedAttributeIDOnStartup(this._selectedAttrIDOnStartup);
      attributesSelectDlg.LoadAttrDialogForRelation(relID, showAutoRequiredAttrs);
      return attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count == 0 ? (List<int>) null : attributesSelectDlg.SelectedAttributesID;
    }
  }

  /// <summary>Получить описание атрибутов по их идентификаторам.</summary>
  /// <param name="relID">Идентификатор связи</param>
  /// <param name="typeID">Тип связи</param>
  /// <param name="attrIDs">Список идентификаторов атрибутов</param>
  /// <param name="relTypes"></param>
  /// <returns>Список описаний атрибутов</returns>
  private List<AttributeValues> GetAttributeValuesByRelationIDs(
    long relID,
    int typeID,
    List<int> attrIDs,
    List<int> relTypes)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>(attrIDs.Count);
    AttributeValues[] source = (AttributeValues[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (relID != 0L)
        source = (sessionKeeper.Session.GetRelation(relID, false) ?? throw new AdditionalCommandProviderException(string.Format(LocalizationHolder.rm.GetString("Relation_CanNotFindByID"), (object) relID))
        {
          BtnSkipVisible = false,
          BtnSkipAllVisible = false
        }).GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions);
      IDBRelationType relationType = typeID != -1 ? sessionKeeper.Session.GetRelationType(typeID) : (IDBRelationType) null;
      foreach (int attrId in attrIDs)
      {
        int attrID = attrId;
        if (source != null)
        {
          AttributeValues attributeValues = ((IEnumerable<AttributeValues>) source).FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attrID));
          if (attributeValues != null)
          {
            attributeValuesList.Add(attributeValues);
            continue;
          }
        }
        if (relationType != null)
        {
          IDBAttributeType attributeType = relationType.GetAttributeType(attrID);
          if (attributeType != null)
          {
            AttributeValues attributeValues = new AttributeValues(attrID, attributeType.AttributeType, attributeType.MultipleValued, new object[1]
            {
              attributeType.DefaultValue
            })
            {
              AttributeName = attributeType.Name,
              AttributeGuid = attributeType.PropertiesStructure.AttributeGuid,
              ReadOnly = (attributeType.Options & AttributeOptions.DisableManualEdit) != 0
            };
            attributeValuesList.Add(attributeValues);
            continue;
          }
        }
        IDBAttributeType attributeType1 = sessionKeeper.Session.GetAttributeType(attrID);
        if (attributeType1 != null)
        {
          AttributeValues attributeValues = new AttributeValues(attrID, attributeType1.AttributeType, attributeType1.MultipleValued, new object[1]
          {
            attributeType1.DefaultValue
          })
          {
            AttributeName = attributeType1.Name,
            AttributeGuid = attributeType1.PropertiesStructure.AttributeGuid
          };
          attributeValuesList.Add(attributeValues);
        }
      }
      if (attributeValuesList.Count > 0 && relTypes != null)
      {
        foreach (int relType in relTypes)
        {
          foreach (AttributeValues attributeValues in attributeValuesList)
          {
            if (!attributeValues.ReadOnly)
            {
              IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relType, attributeValues.AttributeID);
              attributeValues.ReadOnly = attribute4RelationType != null && (attribute4RelationType.Options & AttributeOptions.DisableManualEdit) != 0;
            }
          }
        }
      }
      return attributeValuesList.Count > 0 ? attributeValuesList : (List<AttributeValues>) null;
    }
  }

  /// <summary>Получить маску для строковых атрибутов.</summary>
  /// <param name="relTypeIDs">Список идентификаторов выбранных типов связей</param>
  /// <param name="attrIDs">Список идентификаторов выбранных атрибутов</param>
  /// <returns>Словарь с идентификаторами атрибутов и соответствующих им масок</returns>
  private Dictionary<int, string> GetAttributesMasksForRelationTypes(
    List<int> relTypeIDs,
    List<int> attrIDs)
  {
    Dictionary<int, string> dictionary = new Dictionary<int, string>(attrIDs.Count);
    if (relTypeIDs != null && relTypeIDs.Count == 1)
    {
      string empty = string.Empty;
      foreach (int attrId in attrIDs)
      {
        IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relTypeIDs[0], attrId);
        string mask;
        if (attribute4RelationType == null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
          if (attributeType != null && attributeType.FieldType == FieldTypes.ftString)
            mask = attributeType.Mask;
          else
            continue;
        }
        else if (attribute4RelationType.FieldType == FieldTypes.ftString)
          mask = attribute4RelationType.Mask;
        else
          continue;
        if (!string.IsNullOrEmpty(mask) && !dictionary.ContainsKey(attrId))
          dictionary.Add(attrId, mask);
      }
    }
    else
    {
      foreach (int attrId in attrIDs)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
        if (attributeType != null && attributeType.FieldType == FieldTypes.ftString && !string.IsNullOrEmpty(attributeType.Mask) && !dictionary.ContainsKey(attrId))
          dictionary.Add(attrId, attributeType.Mask);
      }
    }
    return dictionary.Count <= 0 ? (Dictionary<int, string>) null : dictionary;
  }

  /// <summary>Рассылка сообщения об изменении связей.</summary>
  /// <param name="result">Список измененных связей</param>
  public void FireEventForRelation(SortedList<int, List<long>> result)
  {
    List<long> list = result.SelectMany<KeyValuePair<int, List<long>>, long>((System.Func<KeyValuePair<int, List<long>>, IEnumerable<long>>) (x => x.Value.Select<long, long>((System.Func<long, long>) (y => y)))).Distinct<long>().ToList<long>();
    if (list.Count <= 0 || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", (IList<long>) list));
  }

  /// <summary>Добавление указанных атрибутов выделенным связям.</summary>
  /// <param name="selectedRelInfo">Информация о выделенных связях отсортированных по типам</param>
  /// <param name="values">Информация о добавляемых атрибутах</param>
  /// <param name="addAttrs">Флаг о том, какая операция будет происходить, добавление или редактирование атрибутов</param>
  /// <param name="masks">Словарь идентификаторов атрибутов и соответствующих им масок</param>
  /// <returns>Результат добавления (null не возвращается)</returns>
  private SortedList<int, List<long>> AddEditAttributesForRelations(
    Dictionary<int, List<long>> selectedRelInfo,
    List<AttributeValues> values,
    bool addAttrs,
    Dictionary<int, string> masks)
  {
    SortedList<int, List<long>> changedRels = new SortedList<int, List<long>>();
    bool skipAll = false;
    IElementInfo elementInfo = (IElementInfo) null;
    if (selectedRelInfo.Count == 1)
    {
      List<long> longList = selectedRelInfo.ElementAt<KeyValuePair<int, List<long>>>(0).Value;
      if (longList != null && longList.Count > 0)
        elementInfo = (IElementInfo) new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(longList[0], AttributableElements.Relation);
    }
    using (SimpleEditorForm simpleEditorForm = new SimpleEditorForm(elementInfo, values.ToArray(), addAttrs ? SimpleEditorForm.SimpleEditorFormMode.AddAttributes : SimpleEditorForm.SimpleEditorFormMode.EditAttributes, masks))
    {
      values.Clear();
      if (simpleEditorForm.ShowDialog() == DialogResult.OK)
        values.AddRange((IEnumerable<AttributeValues>) simpleEditorForm.Values);
    }
    if (values.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string empty = string.Empty;
        foreach (KeyValuePair<int, List<long>> keyValuePair in selectedRelInfo)
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(keyValuePair.Key);
          if (relationCollection != null)
          {
            foreach (AttributeValues attributeValues in values)
            {
              int attributeId = attributeValues.AttributeID;
              List<long> longList = new List<long>((IEnumerable<long>) keyValuePair.Value);
              while (longList.Count > 0)
              {
                CommandResult commandResult = new CommandResult((long[]) null);
                commandResult = !addAttrs ? relationCollection.EditAttribute(longList.ToArray(), (object) attributeId, attributeValues.Values, skipAll) : relationCollection.AddAttribute(longList.ToArray(), (object) attributeId, attributeValues.Values, skipAll);
                if (commandResult.ProcessedObjects.Length != 0)
                {
                  if (changedRels.ContainsKey(attributeId))
                    changedRels[attributeId].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
                  else
                    changedRels.Add(attributeId, new List<long>((IEnumerable<long>) commandResult.ProcessedObjects));
                }
                if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
                {
                  if (!skipAll)
                  {
                    QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149"), false, false);
                    if (questionFormResult == QuestionFormResult.Break)
                      return changedRels;
                    skipAll = questionFormResult == QuestionFormResult.SkipAll;
                  }
                  foreach (long processedObject in commandResult.ProcessedObjects)
                    longList.Remove(processedObject);
                  longList.Remove(commandResult.ErrorObjectID);
                }
                else
                  longList.Clear();
              }
            }
          }
        }
        if (changedRels.Count > 0)
        {
          List<AdditionalCommandProvider.MasterInfoClass> masterInfoClassList = this.GroupRelationMasterAttributes(selectedRelInfo, values, changedRels);
          if (masterInfoClassList != null)
          {
            foreach (AdditionalCommandProvider.MasterInfoClass masterInfoClass in masterInfoClassList)
            {
              if (!masterInfoClass.SaveSourceAttributes(sessionKeeper.Session, ref skipAll))
                break;
            }
          }
        }
      }
    }
    return changedRels;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedRelInfo">Список выделенных связей, сгруппированных по типам</param>
  /// <param name="values">Все выбранные атрибуты</param>
  /// <param name="changedRels">Список измененных связей, сгруппированных по атрибутам</param>
  /// <returns></returns>
  private List<AdditionalCommandProvider.MasterInfoClass> GroupRelationMasterAttributes(
    Dictionary<int, List<long>> selectedRelInfo,
    List<AttributeValues> values,
    SortedList<int, List<long>> changedRels)
  {
    Dictionary<int, AdditionalCommandProvider.MasterInfoClass> dictionary = new Dictionary<int, AdditionalCommandProvider.MasterInfoClass>();
    List<int> changedAttrIDs = changedRels.Keys.ToList<int>();
    changedAttrIDs.Sort();
    List<int> list1 = values.Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeType == FieldTypes.ftObjectLink && changedAttrIDs.BinarySearch(x.AttributeID) > -1)).Select<AttributeValues, int>((System.Func<AttributeValues, int>) (x => x.AttributeID)).ToList<int>();
    list1.Sort();
    if (list1.Count > 0)
    {
      foreach (KeyValuePair<int, List<long>> keyValuePair in selectedRelInfo)
      {
        List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(keyValuePair.Key);
        string empty = string.Empty;
        foreach (IMSAttribute4RelationType attribute4RelationType in relationTypeList)
        {
          IMSAttribute4RelationType attr = attribute4RelationType;
          if (changedAttrIDs.BinarySearch(attr.AttributeID) <= -1 && attr.MasterAttributeID != 0 && list1.BinarySearch(attr.MasterAttributeID) >= 0)
          {
            List<long> list2 = keyValuePair.Value.Where<long>((System.Func<long, bool>) (x => changedRels[attr.MasterAttributeID].Contains(x))).ToList<long>();
            if (list2.Count != 0)
            {
              if (!dictionary.ContainsKey(attr.MasterAttributeID))
              {
                AttributeValues attributeValues = values.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == attr.MasterAttributeID));
                long result = 0;
                string s = Convert.ToString(attributeValues.Values[0]);
                if (!string.IsNullOrEmpty(s) && !long.TryParse(s, out result))
                  list1.Remove(attr.MasterAttributeID);
                else
                  dictionary.Add(attr.MasterAttributeID, new AdditionalCommandProvider.MasterInfoClass(attr.MasterAttributeID, result, keyValuePair.Key, list2, attr.AttributeID, attr.SourceAttributeID, false));
              }
              else
                dictionary[attr.MasterAttributeID].AddAttribute(keyValuePair.Key, list2, attr.AttributeID, attr.SourceAttributeID);
            }
          }
        }
      }
    }
    return dictionary.Count <= 0 ? (List<AdditionalCommandProvider.MasterInfoClass>) null : dictionary.Values.ToList<AdditionalCommandProvider.MasterInfoClass>();
  }

  /// <summary>Удаление атрибутов.</summary>
  /// <param name="selectedRelInfo">Информация о выделенных связях отсортированных по типам</param>
  /// <param name="attrIDs">Список идентификаторов втрибутов</param>
  /// <returns>Результат удаления</returns>
  private SortedList<int, List<long>> RemoveRelationAttributes(
    Dictionary<int, List<long>> selectedRelInfo,
    List<int> attrIDs)
  {
    SortedList<int, List<long>> sortedList = new SortedList<int, List<long>>();
    DialogResult dialogResult = DialogResult.None;
    using (ValidationForDeleteAttributes deleteAttributes = new ValidationForDeleteAttributes(attrIDs))
      dialogResult = deleteAttributes.ShowDialog();
    if (dialogResult == DialogResult.Yes)
    {
      bool ignoreExceptions = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (KeyValuePair<int, List<long>> keyValuePair in selectedRelInfo)
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(keyValuePair.Key);
          if (relationCollection != null)
          {
            foreach (int attrId in attrIDs)
            {
              List<long> longList = new List<long>((IEnumerable<long>) keyValuePair.Value);
              while (longList.Count > 0)
              {
                CommandResult commandResult = relationCollection.DeleteAttribute(longList.ToArray(), (object) attrId, ignoreExceptions);
                if (commandResult.ProcessedObjects.Length != 0)
                {
                  if (sortedList.ContainsKey(attrId))
                    sortedList[attrId].AddRange((IEnumerable<long>) commandResult.ProcessedObjects);
                  else
                    sortedList.Add(attrId, new List<long>((IEnumerable<long>) commandResult.ProcessedObjects));
                }
                if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
                {
                  if (!ignoreExceptions)
                  {
                    QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149"), false, false);
                    if (questionFormResult == QuestionFormResult.Break)
                      return sortedList;
                    ignoreExceptions = questionFormResult == QuestionFormResult.SkipAll;
                  }
                  foreach (long processedObject in commandResult.ProcessedObjects)
                    longList.Remove(processedObject);
                  longList.Remove(commandResult.ErrorObjectID);
                }
                else
                  longList.Clear();
              }
            }
          }
        }
      }
    }
    return sortedList;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relByTypeDict"></param>
  /// <returns></returns>
  private Dictionary<int, List<int>> GetAttributeGroupsForRelations(
    Dictionary<int, List<long>> relByTypeDict)
  {
    Dictionary<int, List<int>> groupsForRelations = (Dictionary<int, List<int>>) null;
    List<int> source = new List<int>();
    foreach (KeyValuePair<int, List<long>> keyValuePair in relByTypeDict)
    {
      List<int> attributesForRelations = this.GetAttributesForRelations(keyValuePair.Key, keyValuePair.Value);
      if (attributesForRelations != null)
        source.AddRange((IEnumerable<int>) attributesForRelations);
    }
    if (source.Count > 0)
    {
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable[] cacheTables = sessionKeeper.Session.GetCacheTables("IMS_ATTR_IN_GROUPS");
        dataTable = cacheTables == null || cacheTables.Length == 0 ? (DataTable) null : cacheTables[0];
      }
      if (dataTable != null)
      {
        List<int> list = source.Distinct<int>().ToList<int>();
        list.Sort();
        groupsForRelations = new Dictionary<int, List<int>>();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          if (list.BinarySearch(int32_1) >= 0)
          {
            int int32_2 = Convert.ToInt32(row["F_GROUP_ID"]);
            if (groupsForRelations.ContainsKey(int32_2))
              groupsForRelations[int32_2].Add(int32_1);
            else
              groupsForRelations.Add(int32_2, new List<int>()
              {
                int32_1
              });
          }
        }
      }
    }
    return groupsForRelations;
  }

  /// <summary>
  /// Получение списка атрибутов связей, которые не являются обязательными атрибутами.
  /// </summary>
  /// <remarks>
  /// Получает атрибуты указанных связей и выбирает из них необязательные.
  /// В результирующем списке соддержатся только уникальные значения.
  /// </remarks>
  /// <param name="relTypeID">Тип связей</param>
  /// <param name="relIDs">Список идентификаторов связей</param>
  /// <returns>Список идентификаторов атрибутов</returns>
  private List<int> GetAttributesForRelations(int relTypeID, List<long> relIDs)
  {
    List<int> intList = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long relId in relIDs)
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(relId, false);
        if (relation != null)
        {
          foreach (AttributeValues attributesValue in relation.GetAttributesValues(GetAttributeValuesModes.None))
          {
            if (!intList.Contains(attributesValue.AttributeID))
            {
              IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relTypeID, attributesValue.AttributeID);
              if (attribute4RelationType == null || attribute4RelationType.Required != RequiredModes.AutoRequired)
                intList.Add(attributesValue.AttributeID);
            }
          }
        }
      }
    }
    return intList.Count <= 0 ? (List<int>) null : intList;
  }

  /// <summary>
  /// SortedList( тип объекта, List ( Tuple( id объекта, список атрибутов до изменения, список атрибутов после изменения )))
  /// </summary>
  private class AttributesOperationList : 
    SortedList<int, List<Tuple<long, List<AttributeValues>, List<AttributeValues>>>>
  {
  }

  /// <summary>
  /// 
  /// </summary>
  private class MasterInfoClass
  {
    private int _masterID;
    internal long _masterAttrValue;
    /// <summary>
    /// Идентификатор типа объектов/связи - список "идентификатор изменяемого атрибута - идентификатор атрибута описателя"
    /// </summary>
    private Dictionary<int, List<Tuple<int, int>>> _typeAttrs = new Dictionary<int, List<Tuple<int, int>>>();
    /// <summary>
    /// Идентификатор типа объектов/связи - список идентификаторов объектов/связей, у которых был изменен этот мастер-атрибут
    /// </summary>
    private Dictionary<int, List<long>> _types = new Dictionary<int, List<long>>();
    private bool _isObj = true;

    /// <summary>Конструктор.</summary>
    /// <param name="masterAttrID">Идентификатор мастер-атрибута</param>
    /// <param name="masterAttrValue">Значение мастер-атрибута</param>
    /// <param name="typeID">Идентификатор типа объектов</param>
    /// <param name="ableIDs">Список идентификаторов объектов</param>
    /// <param name="attrID">Идентификатор изменяемого атрибута</param>
    /// <param name="sourceAttrID">Идентификатор атрибута-источника</param>
    /// <param name="isObj">Передаваемая сущность - объект/связь</param>
    internal MasterInfoClass(
      int masterAttrID,
      long masterAttrValue,
      int typeID,
      List<long> ableIDs,
      int attrID,
      int sourceAttrID,
      bool isObj = true)
    {
      this._masterID = masterAttrID;
      this._masterAttrValue = masterAttrValue;
      this._types.Add(typeID, ableIDs);
      this._typeAttrs.Add(typeID, new List<Tuple<int, int>>()
      {
        new Tuple<int, int>(attrID, sourceAttrID)
      });
      this._isObj = isObj;
    }

    /// <summary>Добавить информацию об изменяемом атрибуте.</summary>
    /// <param name="typeID">Тип объектов</param>
    /// <param name="ableIDs">Список идентификаторов объектов</param>
    /// <param name="attrID">Идентификатор изменяемого атрибута</param>
    /// <param name="sourceAttrID">Идентификатор атрибута-источника</param>
    internal void AddAttribute(int typeID, List<long> ableIDs, int attrID, int sourceAttrID)
    {
      if (!this._types.ContainsKey(typeID))
      {
        this._types.Add(typeID, ableIDs);
        this._typeAttrs.Add(typeID, new List<Tuple<int, int>>()
        {
          new Tuple<int, int>(attrID, sourceAttrID)
        });
      }
      else
        this._typeAttrs[typeID].Add(new Tuple<int, int>(attrID, sourceAttrID));
    }

    /// <summary>Сохранить значения измененных атрибутов.</summary>
    /// <param name="session">Сессия пользователя</param>
    /// <param name="skipAll">Флаг пропуска ошибок</param>
    /// <returns>Флаг для возможности остановить выполнение</returns>
    internal bool SaveSourceAttributes(IUserSession session, ref bool skipAll)
    {
      if (this._masterAttrValue == 0L)
      {
        foreach (KeyValuePair<int, List<Tuple<int, int>>> typeAttr in this._typeAttrs)
        {
          IDBAttributableCollection coll = !this._isObj ? (IDBAttributableCollection) session.GetRelationCollection(typeAttr.Key) : (IDBAttributableCollection) session.GetObjectCollection(typeAttr.Key);
          if (coll != null)
          {
            foreach (Tuple<int, int> tuple in typeAttr.Value)
            {
              if (!this.SaveValues(coll, new List<long>((IEnumerable<long>) this._types[typeAttr.Key]), tuple.Item1, new object[1]
              {
                (object) DBNull.Value
              }, ref skipAll))
                return false;
            }
          }
        }
      }
      else
      {
        IDBObject objectActualCopy = session.GetObjectActualCopy(this._masterAttrValue, false);
        if (objectActualCopy != null)
        {
          Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
          List<int> intList = new List<int>();
          foreach (KeyValuePair<int, List<Tuple<int, int>>> typeAttr in this._typeAttrs)
          {
            IDBAttributableCollection coll = !this._isObj ? (IDBAttributableCollection) session.GetRelationCollection(typeAttr.Key) : (IDBAttributableCollection) session.GetObjectCollection(typeAttr.Key);
            if (coll != null)
            {
              foreach (Tuple<int, int> tuple in typeAttr.Value)
              {
                if (!intList.Contains(tuple.Item2))
                {
                  if (!dictionary.ContainsKey(tuple.Item2))
                  {
                    IDBAttribute attributeById = objectActualCopy.GetAttributeByID(tuple.Item2);
                    if (attributeById == null)
                    {
                      intList.Add(tuple.Item2);
                      continue;
                    }
                    dictionary.Add(tuple.Item2, attributeById.Values);
                  }
                  if (!this.SaveValues(coll, new List<long>((IEnumerable<long>) this._types[typeAttr.Key]), tuple.Item1, dictionary[tuple.Item2], ref skipAll))
                    return false;
                }
              }
            }
          }
        }
      }
      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coll"></param>
    /// <param name="ableIDs"></param>
    /// <param name="attrID"></param>
    /// <param name="values"></param>
    /// <param name="skipAll"></param>
    /// <returns></returns>
    private bool SaveValues(
      IDBAttributableCollection coll,
      List<long> ableIDs,
      int attrID,
      object[] values,
      ref bool skipAll)
    {
      bool flag = true;
      while (ableIDs.Count > 0)
      {
        CommandResult commandResult = new CommandResult((long[]) null);
        commandResult = coll.AddAttribute(ableIDs.ToArray(), (object) attrID, values, skipAll);
        if (!string.IsNullOrEmpty(commandResult.ErrorMessage))
        {
          if (!skipAll)
          {
            QuestionFormResult questionFormResult = AdditionalCommandProviderExceptionForm.Show(commandResult.ErrorMessage, LocalizationHolder.rm.GetString("Client.Core_1149"), false, false);
            if (questionFormResult == QuestionFormResult.Break)
            {
              flag = false;
              break;
            }
            skipAll = questionFormResult == QuestionFormResult.SkipAll;
          }
          foreach (long processedObject in commandResult.ProcessedObjects)
            ableIDs.Remove(processedObject);
          ableIDs.Remove(commandResult.ErrorObjectID);
        }
        else
          break;
      }
      return flag;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal class AttributeTypeFilter : ISelectorFilter
  {
    private List<int> _attributeTypeIDs;
    private List<int> _groupsIDs;
    private bool _allowAllAttributes;

    /// <summary>Конструктор.</summary>
    /// <param name="attributeTypeIDs"></param>
    public AttributeTypeFilter(Dictionary<int, List<int>> attributeTypeIDs)
    {
      this._attributeTypeIDs = new List<int>();
      this._groupsIDs = new List<int>();
      foreach (KeyValuePair<int, List<int>> attributeTypeId in attributeTypeIDs)
      {
        this._attributeTypeIDs.Add(attributeTypeId.Key);
        if (attributeTypeId.Value.Count > 0)
        {
          foreach (int num in attributeTypeId.Value)
          {
            if (!this._groupsIDs.Contains(num))
              this._groupsIDs.Add(num);
          }
        }
        else
          this._allowAllAttributes = true;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="category"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsInFilter(int category, object id)
    {
      if (category.Equals(12))
      {
        if (this._groupsIDs.Contains(Convert.ToInt32(id)))
          return true;
        return id.Equals((object) -1) && this._allowAllAttributes;
      }
      return category.Equals(3) && this._attributeTypeIDs.Contains(Convert.ToInt32(id));
    }
  }
}
