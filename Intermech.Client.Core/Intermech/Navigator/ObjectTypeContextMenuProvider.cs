
// Type: Intermech.Navigator.ObjectTypeContextMenuProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator;

internal class ObjectTypeContextMenuProvider : ICommandsProvider
{
  private ICurrentUserAndRole currentUserAndRole;

  public ObjectTypeContextMenuProvider(ICurrentUserAndRole currentUserAndRole)
  {
    this.currentUserAndRole = currentUserAndRole != null ? currentUserAndRole : throw new ArgumentNullException(nameof (currentUserAndRole));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    IDBTypedObjectID itemData1 = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBObjectTypeID itemData2 = items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID;
    if (itemData1 != null || itemData2 != null)
    {
      IMSObjectType imsObjectType = itemData1 != null ? MetaDataHelper.GetObjectType(itemData1.ObjectType) : MetaDataHelper.GetObjectType(itemData2.Value);
      if (imsObjectType != null)
      {
        if (this.currentUserAndRole.IsAdmin)
          groupCommands.Add("SearchSimilarObjects", new CommandInfo(2, new ClickEventHandler(ObjectTypeContextMenuProvider.SearchSimilarObjectsCommand)));
        if (imsObjectType.VersionsMode != ObjectVersionModes.Abstract && Utils.CreateFreeObject(imsObjectType.ObjectTypeID))
          groupCommands.Add("Create", new CommandInfo(2, new ClickEventHandler(ObjectTypeContextMenuProvider.CreateCommand)));
      }
    }
    return groupCommands;
  }

  private static void CreateCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    long objectByTypeDialog = (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).CreateObjectByTypeDialog(items.GetItemID(0).TypeID);
    switch (objectByTypeDialog)
    {
      case -1:
        break;
      case 0:
        break;
      default:
        DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog);
        Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        break;
    }
  }

  /// <summary>Поиск одинаковых объектов.</summary>
  /// <param name="items">The items.</param>
  /// <param name="viewservices">The viewservices.</param>
  /// <param name="additionalinfo">The additionalinfo.</param>
  private static void SearchSimilarObjectsCommand(
    ISelectedItems items,
    System.IServiceProvider viewservices,
    object additionalinfo)
  {
    int typeId = items.GetItemID(0).TypeID;
    int attributeId = ObjectTypeContextMenuProvider.GetAttributeID(typeId);
    if (attributeId == 0)
      return;
    using (IdenticalObjectsSearchingForm objectsSearchingForm = new IdenticalObjectsSearchingForm(ObjectTypeContextMenuProvider.GetDataTable(typeId, attributeId), attributeId))
    {
      if (!IdenticalObjectsSearchingForm.IsNeedToBeShown)
        return;
      int num = (int) objectsSearchingForm.ShowDialog();
    }
  }

  /// <summary>Получает таблицу с инфой.</summary>
  /// <param name="typeID">ID типа объектов.</param>
  /// <returns></returns>
  private static DataTable GetDataTable(int typeID, int selectedAttrID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(typeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(selectedAttrID, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      }, new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) selectedAttrID, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      }));
      dataTable.DefaultView.Sort = $"{selectedAttrID.ToString()} ASC, {-3.ToString()}";
      return dataTable.DefaultView.ToTable();
    }
  }

  /// <summary>
  /// Получает ИД атрибута, по которому будут группироваться объекты. Возвращает 0, если атрибут не выбран.
  /// </summary>
  /// <param name="typeID">ID типа объекта.</param>
  /// <returns></returns>
  private static int GetAttributeID(int typeID)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(new List<int>()
      {
        typeID
      }, true);
      attributesSelectDlg.SelectorFilter = (ISelectorFilter) new ForbiddenAttrs(ObjectTypeContextMenuProvider.GetMultiValueAttrsIDs(typeID));
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[17]
      {
        FieldTypes.ftAutoInc,
        FieldTypes.ftBlob,
        FieldTypes.ftBoolean,
        FieldTypes.ftDateTime,
        FieldTypes.ftDouble,
        FieldTypes.ftExternalLink,
        FieldTypes.ftFile,
        FieldTypes.ftGuid,
        FieldTypes.ftInteger,
        FieldTypes.ftMeasured,
        FieldTypes.ftMemo,
        FieldTypes.ftObjectLinkByID,
        FieldTypes.ftObjectLink,
        FieldTypes.ftPassword,
        FieldTypes.ftShortBlob,
        FieldTypes.ftSystem,
        FieldTypes.ftUnknown
      });
      attributesSelectDlg.TypeAttributesOnly = true;
      return attributesSelectDlg.ShowDialog() == DialogResult.OK ? attributesSelectDlg.SelectedAttributesID[0] : 0;
    }
  }

  /// <summary>
  /// Выдает список ID атрибутов с множественными значениями.
  /// </summary>
  /// <param>ID типа</param>
  /// <returns>Cписок ID атрибутов с множественными значениями</returns>
  private static List<int> GetMultiValueAttrsIDs(int typeID)
  {
    List<int> multiValueAttrsIds = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<int> intList = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectType(typeID).Attributes.Select("").Rows)
        intList.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      foreach (int attrTypeID in intList)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
        if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
          multiValueAttrsIds.Add(attrTypeID);
      }
    }
    return multiValueAttrsIds;
  }

  private static void Stub(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
  }
}
