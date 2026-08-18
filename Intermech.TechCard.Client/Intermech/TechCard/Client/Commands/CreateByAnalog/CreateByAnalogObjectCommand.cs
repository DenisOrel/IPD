// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CreateByAnalog.CreateByAnalogObjectCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.MRP2;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.UI.Controls;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.CreateByAnalog;

/// <summary>
/// Реализация команды "Создать объект по ПВ-аналогу (ДСЕ-аналогу)"
/// </summary>
internal class CreateByAnalogObjectCommand : TechCardSelectedItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  private readonly int _createObjectTypeId;
  /// <summary>Список объектов-ссылок типа "ДСЕ" для состава ПВ</summary>
  private readonly ICollection<ObjInfoIDItem> _articleCopyInfoItems = (ICollection<ObjInfoIDItem>) new HashSet<ObjInfoIDItem>();
  /// <summary>Описание объекта "Производственная ведомость"</summary>
  private ObjInfoIDItem _productionReportInfoItem;
  /// <summary>Данные по составу производственно ведомости</summary>
  private DataTable _productionReportData;

  /// <summary>Конструктор</summary>
  /// <param name="createObjectTypeId">Тип создаваемых объектов</param>
  public CreateByAnalogObjectCommand(int createObjectTypeId)
    : base(nameof (CreateByAnalogObjectCommand))
  {
    this._createObjectTypeId = createObjectTypeId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    if (!base.LoadCommandInfo())
      return false;
    ISet<long> longSet = (ISet<long>) null;
    if (MetaDataHelper.IsObjectTypeChildOf(this._selectedObjInfo.ObjTypeID, TechCardConsts.ObjectTypes.ArticleCopyBaseID))
    {
      IDBTypedObjectID dbTypedObjId;
      for (NavigatorTreeNode treeNode = this.Items.GetItemData<NavigatorTreeNode>(0, false); treeNode != null && TechcardClientControlsUtils.GetObjectInfo(treeNode, out dbTypedObjId); treeNode = treeNode.Parent)
      {
        if (dbTypedObjId != null && MetaDataHelper.IsObjectTypeChildOf(dbTypedObjId.ObjectType, MRP2Consts.objtypeIdProductionLists))
        {
          this._productionReportInfoItem = new ObjInfoIDItem(dbTypedObjId.ObjectID, dbTypedObjId.ObjectType, dbTypedObjId.ID);
          break;
        }
      }
      longSet = (ISet<long>) new HashSet<long>();
      IDBRelationID itemData = this.Items.GetItemData<IDBRelationID>(0, false);
      if (itemData != null)
        longSet.Add(itemData.Value);
    }
    else if (MetaDataHelper.IsObjectTypeChildOf(this._selectedObjInfo.ObjTypeID, MRP2Consts.objtypeIdProductionLists))
      this._productionReportInfoItem = new ObjInfoIDItem((TypedInfoItem) this._selectedObjInfo);
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) this._productionReportInfoItem))
    {
      int num = (int) MessageBox.Show("Внимание", $"Для выбранного объекта (ObjectId = {this._selectedObjInfo.ObjectID}) не найдены контекст ПВ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return false;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ColumnDescriptor[] extraColumns = new ColumnDescriptor[4]
      {
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ProductionObjectUIDAttrGuid, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfProductionReportVersionAttrID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfProductionReportObjectAttrID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0),
        new ColumnDescriptor((object) TechCardConsts.AttributeTypes.MemberOfExitAssemblyAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      };
      this._productionReportData = CreateByAnalogObjectWizard.LoadProductionReportData(sessionKeeper.Session, (ObjInfoItem) this._productionReportInfoItem, new int[1]
      {
        this._createObjectTypeId
      }, (IEnumerable<ColumnDescriptor>) extraColumns);
      if (this._productionReportData == null || this._productionReportData.Rows.Count == 0)
      {
        int num = (int) MessageBox.Show($"Для выбранного объекта (ObjectId = {this._selectedObjInfo.ObjectID}) не найдены производственные копии изделий", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      }
      foreach (DataRow row in (InternalDataCollectionBase) this._productionReportData.Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        if (longSet == null || longSet.Contains(int64Value1))
        {
          long int64Value2 = DataSetProcessor.GetInt64Value(row, "cadd9a8c-306c-11d8-b4e9-00304f19f545", 0L);
          if (int64Value2 != 0L)
            this._articleCopyInfoItems.Add(new ObjInfoIDItem(int64Value2));
        }
      }
      ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) this._articleCopyInfoItems, sessionKeeper.Session);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    CreateByAnalogObjectWizard analogObjectWizard = new CreateByAnalogObjectWizard(this._createObjectTypeId, (ObjInfoItem) this._productionReportInfoItem, (IEnumerable<ObjInfoIDItem>) this._articleCopyInfoItems);
    analogObjectWizard.Text = "Мастер выбора ПК-ДСЕ для создания объектов по аналогу";
    CreateByAnalogObjectWizard createByAnalogObjectWizard = analogObjectWizard;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      Icon icon = service.GetIcon(4, this._createObjectTypeId);
      if (icon != null)
        createByAnalogObjectWizard.Icon = icon;
    }
    if (createByAnalogObjectWizard.ShowDialog() != DialogResult.OK)
      return false;
    ISelectedItems selectedItems = createByAnalogObjectWizard.SelectedItems;
    if ((selectedItems != null ? (!selectedItems.Any() ? 1 : 0) : 1) != 0)
      return false;
    this.DoCreateByAnalogObjects(createByAnalogObjectWizard);
    return true;
  }

  private void DoCreateByAnalogObjects(
    [NotNull] CreateByAnalogObjectWizard createByAnalogObjectWizard)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        new CreateByAnalogCommandTask(this._productionReportInfoItem, this._createObjectTypeId, this._productionReportData, createByAnalogObjectWizard.ProductionReportAnalogData).Execute(sessionKeeper.Session, createByAnalogObjectWizard.Options, createByAnalogObjectWizard.SelectedItems);
        foreach (NotificationEventArgs notificationEvent in TechcardClientControlsUtils.GetNotificationEvents((IList<CategoryValue>) sessionKeeper.Session.GetModificationsHistoryList()))
        {
          if (!(notificationEvent.EventName == "ObjectsChanged") && !(notificationEvent.EventName == "RelationsChanged"))
            this.Notifications.QueueEvent(notificationEvent);
        }
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
      }
    }
  }
}
