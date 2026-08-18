// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.ApplyGroupAttributesBaseCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Передать атрибуты группового объекта в единичные. Отображает мастер из диалога выбора единичных объектов и диалогов с атрибутами
/// </summary>
internal abstract class ApplyGroupAttributesBaseCommand(string name) : TechCardSelectedItemsCommand(name)
{
  /// <summary>
  /// Информация о выделенной родительской связи группового объекта
  /// </summary>
  protected IDBRelationID _relationId;
  /// <summary>Информация о выделенном групповом объекте</summary>
  protected IDBTypedObjectID _groupObjId;
  /// <summary>Способ создания объекта</summary>
  protected ImbaseObjCreateMode _createMode = ImbaseObjCreateMode.iocmCreateNew;
  /// <summary>Коллекция единичных объектов для диалога</summary>
  protected List<ObjInfoItem> _unitInfoItems;
  protected List<CategoryValue> _modificationsList = new List<CategoryValue>();

  /// <summary>Загрузка данных для команды</summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    this._relationId = this.Items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    this._groupObjId = this.Items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    if (this._groupObjId == null || this._relationId == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      if (service != null)
      {
        ImbaseObjCreateInfo objCreateInfo;
        if (service.GetCreationMode(this._groupObjId.ObjectID, sessionKeeper.Session.SessionGUID, out objCreateInfo))
          this._createMode = objCreateInfo.CreateMode;
      }
    }
    if (this._relationId.RelationType != -1 || this._createMode == ImbaseObjCreateMode.iocmCreateNew)
      return this.LoadUnitItems();
    int num = (int) MessageBox.Show($"Не допускается передавать атрибуты группового объекта с настройкой \"{MetaDataHelper.GetAttributeTypeName(Intermech.Imbase.Consts.CreateNewObjectAttGUID)}\" = Нет", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    return false;
  }

  protected override bool ExecuteCommand()
  {
    ApplyGroupAttributesWizard attributesWizard1 = new ApplyGroupAttributesWizard();
    attributesWizard1.Text = this._groupObjId.Caption;
    ApplyGroupAttributesWizard attributesWizard2 = attributesWizard1;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      Icon icon = service.GetIcon(4, this._groupObjId.ObjectType);
      if (icon != null)
        attributesWizard2.Icon = icon;
    }
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) this._unitInfoItems);
    attributesWizard2.InitializePageCollection(objectTypeCache, this._createMode != ImbaseObjCreateMode.iocmUseExists ? this._relationId.PartID : 0L, this._relationId.Value);
    if (attributesWizard2.ShowDialog() != DialogResult.OK)
      return false;
    List<long> selectedUnitItems = attributesWizard2.SelectedUnitItems;
    Dictionary<ElementInfo, List<AttributeValues>> selectedAttributes = attributesWizard2.SelectedAttributes;
    return selectedUnitItems.Count != 0 && selectedAttributes.Count != 0 && this.ApplyGroupAttributes(selectedUnitItems, selectedAttributes);
  }

  protected override void UpdateNotificationQueue()
  {
    if (!this._modificationsList.Any<CategoryValue>())
      return;
    foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) this._modificationsList))
      this.Notifications.QueueEvent(notificationEvent);
    this._modificationsList.Clear();
  }

  /// <summary>
  /// Загрузить данные по единичным объектам для диалога выбора
  /// </summary>
  /// <returns></returns>
  protected abstract bool LoadUnitItems();

  /// <summary>Применить в отмеченных объектах отмеченные атрибуты</summary>
  /// <param name="selectedUnitList">Отмеченные единичные объекты</param>
  /// <param name="selectedAttributes">Словарь отмеченных атрибутов для элемента связь/объект</param>
  /// <returns></returns>
  public abstract bool ApplyGroupAttributes(
    List<long> selectedUnitList,
    Dictionary<ElementInfo, List<AttributeValues>> selectedAttributes);

  /// <summary>Записать атрибуты в связи и объекты</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="relInfoItem">Элементы состава (связь и дочерний объект) единичных объектов, в которые необходимо записать атрибуты</param>
  /// <param name="setAttributes">Атрибуты, которые необходимо записать</param>
  /// <returns></returns>
  public bool SetGroupAttributes(
    IUserSession session,
    List<RelObjInfoItem> relInfoItem,
    Dictionary<ElementInfo, List<AttributeValues>> setAttributes)
  {
    bool flag = true;
    List<ObjInfoItem> list = relInfoItem.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => (TypedInfoItem) a.PartInfo != (TypedInfoItem) null)).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (a => a.PartInfo)).ToList<ObjInfoItem>();
    foreach (KeyValuePair<ElementInfo, List<AttributeValues>> setAttribute in setAttributes)
    {
      if (setAttribute.Key.ElementKind == AttributableElements.Object)
        flag &= this.SetGroupAttributesInObject(session, list, setAttribute.Value);
      else if (setAttribute.Key.ElementKind == AttributableElements.Relation)
        flag &= this.SetGroupAttributesInRelation(session, relInfoItem, setAttribute.Value);
    }
    return flag;
  }

  /// <summary>Записать атрибуты в объекты</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="objItem">Объекты в которые нужно записать атрибуты</param>
  /// <param name="attributeValues">Атрибуты для записи</param>
  /// <returns></returns>
  public bool SetGroupAttributesInObject(
    IUserSession session,
    List<ObjInfoItem> objInfoItems,
    List<AttributeValues> attributeValues)
  {
    if (attributeValues == null || attributeValues.Count == 0 || objInfoItems.Count == 0)
      return false;
    int objTypeId = objInfoItems[0].ObjTypeID;
    List<long> objIdList = objInfoItems.Select<ObjInfoItem, long>((System.Func<ObjInfoItem, long>) (a => a.ObjectID)).Distinct<long>().ToList<long>();
    List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(objTypeId);
    if (attribute4ObjectTypeList == null)
      return false;
    if (!MetaDataHelper.GetObjectType(objTypeId).AnyAttributes)
    {
      IEnumerable<int> attrTypeIds = attribute4ObjectTypeList.Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (a => a.AttributeID));
      if (!attributeValues.All<AttributeValues>((System.Func<AttributeValues, bool>) (a => attrTypeIds.Contains<int>(a.AttributeID))))
        return false;
    }
    List<IMSAttribute4ObjectType> source1 = new List<IMSAttribute4ObjectType>();
    List<IMSAttributeType> source2 = new List<IMSAttributeType>();
    foreach (AttributeValues attributeValue1 in attributeValues)
    {
      AttributeValues attributeValue = attributeValue1;
      IMSAttribute4ObjectType attribute4ObjectType = attribute4ObjectTypeList.FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (a => a.AttributeID == attributeValue.AttributeID));
      if (attribute4ObjectType != null)
      {
        source1.Add(attribute4ObjectType);
      }
      else
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeValue.AttributeID);
        source2.Add(attributeType);
      }
    }
    if (source1.Any<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (a => !a.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))) || source2.Any<IMSAttributeType>((System.Func<IMSAttributeType, bool>) (a => !a.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))))
    {
      List<long> checkOutObjectList = this.GetCheckOutObjectList(session, objTypeId, objIdList);
      if (checkOutObjectList.Count == 0)
        return false;
      objIdList = checkOutObjectList;
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(objTypeId);
    if (objectCollection == null)
      return false;
    objectCollection.SetAttributesValues(objIdList.ToArray(), attributeValues.ToArray(), true, true);
    return true;
  }

  /// <summary>Записать атрибуты в связи</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="relInfoItem">Связи в которые нужно записать атрибуты</param>
  /// <param name="attributeValues">Атрибуты для записи</param>
  /// <returns></returns>
  public bool SetGroupAttributesInRelation(
    IUserSession session,
    List<RelObjInfoItem> relInfoItem,
    List<AttributeValues> attributes)
  {
    if (attributes == null || attributes.Count == 0 || relInfoItem.Count == 0)
      return false;
    int relTypeId = relInfoItem[0].RelTypeID;
    int num = (TypedInfoItem) relInfoItem[0].ProjInfo != (TypedInfoItem) null ? relInfoItem[0].ProjInfo.ObjTypeID : session.GetRelation(relInfoItem[0].RelationID).ProjObject.ObjectType;
    int objTypeId = relInfoItem[0].PartInfo.ObjTypeID;
    List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(relTypeId);
    if (!MetaDataHelper.GetRelationType(relTypeId).AnyAttributes)
    {
      IEnumerable<int> attrTypeIds = relationTypeList.Select<IMSAttribute4RelationType, int>((System.Func<IMSAttribute4RelationType, int>) (a => a.AttributeID));
      if (!attributes.All<AttributeValues>((System.Func<AttributeValues, bool>) (a => attrTypeIds.Contains<int>(a.AttributeID))))
        return false;
    }
    IDBRelationCollection relationCollection = session.GetRelationCollection(relTypeId);
    if (relationCollection == null)
      return false;
    List<long> list = relInfoItem.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (a => a.RelationID)).ToList<long>();
    if (MetaDataHelper.GetApplicability(num, objTypeId, relTypeId).IsContent)
    {
      List<IMSAttribute4RelationType> source1 = new List<IMSAttribute4RelationType>();
      List<IMSAttributeType> source2 = new List<IMSAttributeType>();
      foreach (AttributeValues attribute in attributes)
      {
        AttributeValues attributeValue = attribute;
        IMSAttribute4RelationType attribute4RelationType = relationTypeList.FirstOrDefault<IMSAttribute4RelationType>((System.Func<IMSAttribute4RelationType, bool>) (a => a.AttributeID == attributeValue.AttributeID));
        if (attribute4RelationType != null)
        {
          source1.Add(attribute4RelationType);
        }
        else
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeValue.AttributeID);
          source2.Add(attributeType);
        }
      }
      if (source1.Any<IMSAttribute4RelationType>((System.Func<IMSAttribute4RelationType, bool>) (a => !a.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))) || source2.Any<IMSAttributeType>((System.Func<IMSAttributeType, bool>) (a => !a.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))))
      {
        List<long> longList = new List<long>();
        if (relInfoItem.Any<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => (TypedInfoItem) a.ProjInfo == (TypedInfoItem) null)))
        {
          ColumnDescriptor[] columns = new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          };
          ConditionStructure[] conditions = new ConditionStructure[1]
          {
            new ConditionStructure(-20, RelationalOperators.In, (object) list.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation)
          };
          DataTable dataTable = relationCollection.Select(new DBRecordSetParams(conditions, columns));
          if (dataTable != null && dataTable.Rows.Count != 0)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64Value = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
              if (int64Value != 0L)
                longList.Add(int64Value);
            }
          }
        }
        else
          longList.AddRange(relInfoItem.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (a => a.ProjInfo.ObjectID)));
        List<long> checkOutObjectList = this.GetCheckOutObjectList(session, num, longList);
        if (checkOutObjectList.Count == 0)
          return false;
        if (!checkOutObjectList.SequenceEqual<long>((IEnumerable<long>) longList))
        {
          ColumnDescriptor[] columns = new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          };
          ConditionStructure[] conditions = new ConditionStructure[2]
          {
            new ConditionStructure(-21, RelationalOperators.In, (object) checkOutObjectList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation),
            new ConditionStructure(TechCardConsts.AttributeTypes.TechProcGroupRelAttrID, RelationalOperators.Equal, (object) this._relationId.RelGuid.ToString(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation)
          };
          relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
          {
            objTypeId
          };
          DataTable dataTable = relationCollection.Select(new DBRecordSetParams(conditions, columns));
          if (dataTable != null && dataTable.Rows.Count != 0)
          {
            list.Clear();
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64Value = DataSetProcessor.GetInt64Value(row, "F_LINK_ID", 0L);
              if (int64Value != 0L)
                list.Add(int64Value);
            }
          }
        }
      }
      relationCollection.SetAttributesValues(list.ToArray(), attributes.ToArray(), true, true);
    }
    return true;
  }

  /// <summary>
  /// Получить взятые на редактирование объекты. Если объекты не на редактировании, то предлагает пользователю взять их на редактирование
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objType"></param>
  /// <param name="objIdList"></param>
  /// <returns></returns>
  private List<long> GetCheckOutObjectList(IUserSession session, int objType, List<long> objIdList)
  {
    List<long> list1 = objIdList.Where<long>((System.Func<long, bool>) (a => a < 0L)).ToList<long>();
    List<long> list2 = objIdList.Where<long>((System.Func<long, bool>) (a => a > 0L)).ToList<long>();
    if (list2.Count == 0)
      return list1;
    string caption = "Список объектов, требующих взятие на редактирование";
    TechDictDescriptor techDictDescriptor = new TechDictDescriptor(Intermech.Navigator.Consts.CategoryVersionsObjectNode, 0, caption, new Dictionary<int, List<long>>()
    {
      {
        objType,
        list2
      }
    });
    techDictDescriptor.ExpandNodes = false;
    TechcardErrorObjForm parent = new TechcardErrorObjForm();
    string errorMsg = "Требуется взятие на редактирование следующих объектов. Продолжить?";
    parent.ShowBtn_OK = true;
    parent.ShowBtn_Cancel = true;
    parent.LoadData(errorMsg, (IDescriptor) techDictDescriptor);
    if (parent.ShowDialog() != DialogResult.OK)
      return list1;
    bool flag1 = false;
    foreach (long objectID in list2)
    {
      try
      {
        long num = session.CheckOutCommand(objectID);
        list1.Add(num);
      }
      catch (Exception ex)
      {
        if (!flag1)
        {
          if (list2.Count == 1)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
          else
          {
            bool flag2 = true;
            while (flag2)
            {
              flag2 = false;
              switch ((DialogResult) IMMessageBox.Show("Внимание", "При взятии на редактирование очередного объекта возникла ошибка. Какие действия следует выполнить?", new IMMessageBoxButton[4]
              {
                new IMMessageBoxButton("Прервать", DialogResult.Abort),
                new IMMessageBoxButton("Игнорировать ошибку", DialogResult.Ignore),
                new IMMessageBoxButton("Игнорировать все ошибки", DialogResult.Retry),
                new IMMessageBoxButton("Показать текст ошибки", DialogResult.No)
              }, IMMessageBoxImage.Question, (Form) parent))
              {
                case DialogResult.Abort:
                  return list1;
                case DialogResult.Retry:
                  flag1 = true;
                  continue;
                case DialogResult.No:
                  ExceptionHelper.ExceptionService.ShowException(ex);
                  flag2 = true;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }
    return list1;
  }
}
