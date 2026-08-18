// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry.EditCurrentAssemblyEntryCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Services.DataProviders.Composition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ProcessRoutingEntry;

/// <summary>
/// Выполнение команды Добавить/Исключить текущую входимость
/// </summary>
internal class EditCurrentAssemblyEntryCommand : TechCardSelectedItemsCommand
{
  /// <summary>Данные привязок изменены</summary>
  private bool _procRouteEntryModify;
  /// <summary>Текущая сборка</summary>
  private ObjInfoIDItem _currentAssembly;
  /// <summary>Текущий заказ</summary>
  private ObjInfoIDItem _currentOrder;
  /// <summary>Текущий объект Входимость маршрута обработки</summary>
  private ObjInfoItem _procRouteEntryItem;
  /// <summary>
  /// Режим изменения привязки сборки Добавление или Исключение
  /// </summary>
  private readonly EditCurrentAssemblyEntryMode _editEntryMode;
  /// <summary>Имя команды для вывода в сообщения</summary>
  private readonly string _commandName;

  public EditCurrentAssemblyEntryCommand(
    EditCurrentAssemblyEntryMode editEntryMode,
    string commandName)
    : base(commandName)
  {
    this._editEntryMode = editEntryMode;
    this._commandName = LocalizationHolder.rm.GetString("TechCard." + commandName);
  }

  /// <summary>Получить текущие данные для выполнения команды</summary>
  protected override bool LoadCommandInfo()
  {
    if (!base.LoadCommandInfo())
      return false;
    if (this._selectedObjInfo.ObjTypeID == TechCardConsts.ObjectTypes.ProcRoutingEntryID)
      this._procRouteEntryItem = new ObjInfoItem(this._selectedObjInfo.ObjectID, this._selectedObjInfo.ObjTypeID);
    if (!(this.ContextServices.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service1))
      return false;
    IEnumerable<RelObjInfoItem> source = new TechRelObjInfoItemsFromSelectedItemApplicabilityProvider(this.Items, service1.Services).Execute();
    if (source == null || !source.Any<RelObjInfoItem>())
      return false;
    IObjectsInfoCache service2 = ApplicationServices.Container.GetService<IObjectsInfoCache>();
    List<int> objTypesOrder = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545")
    });
    ObjInfoItem projInfo1 = source.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => objTypesOrder.Contains(a.ProjInfo.ObjTypeID)))?.ProjInfo;
    if ((TypedInfoItem) projInfo1 != (TypedInfoItem) null && projInfo1.ObjectID != 0L)
    {
      this._currentOrder = projInfo1 as ObjInfoIDItem;
      if ((TypedInfoItem) this._currentOrder == (TypedInfoItem) null)
      {
        QuickObjectInfo objectInfo = service2.GetObjectInfo(projInfo1.ObjectID);
        if (!objectInfo.Empty)
          this._currentOrder = new ObjInfoIDItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.ID);
      }
    }
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new int[2]
    {
      MRP2Consts.objtypeIdProductionObjects,
      MetaDataHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545")
    });
    ObjInfoItem childArticle = source.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => a.PartInfo.ObjTypeID == TechCardConsts.ObjectTypes.ProcRoutingID))?.ProjInfo;
    if ((TypedInfoItem) childArticle == (TypedInfoItem) null || childArticle.ObjectID == 0L || childrenIdRecursive.Contains(childArticle.ObjTypeID))
      return false;
    ObjInfoItem projInfo2 = source.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (a => (TypedInfoItem) a.PartInfo == (TypedInfoItem) childArticle))?.ProjInfo;
    if ((TypedInfoItem) projInfo2 == (TypedInfoItem) null || projInfo2.ObjectID == 0L || childrenIdRecursive.Contains(projInfo2.ObjTypeID))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_543"), this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    this._currentAssembly = projInfo2 as ObjInfoIDItem;
    if ((TypedInfoItem) this._currentAssembly == (TypedInfoItem) null)
    {
      QuickObjectInfo objectInfo = service2.GetObjectInfo(projInfo2.ObjectID);
      if (!objectInfo.Empty)
        this._currentAssembly = new ObjInfoIDItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.ID);
    }
    if ((TypedInfoItem) this._procRouteEntryItem != (TypedInfoItem) null)
      return true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
      relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
      {
        TechCardConsts.ObjectTypes.ProcRoutingEntryID
      };
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if ((TypedInfoItem) this._currentOrder == (TypedInfoItem) null)
      {
        conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID, RelationalOperators.Empty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object));
      }
      else
      {
        conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.MemberOfOrderObjectAttrID, RelationalOperators.Equal, (object) this._currentOrder.ID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.ID));
        conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID, RelationalOperators.Equal, (object) Math.Abs(this._currentOrder.ObjectID), (object) null, LogicalOperators.OR, 1, false, AttributeSourceTypes.Object, ColumnContents.ID));
        conditionStructureList.Add(new ConditionStructure(TechCardConsts.AttributeTypes.MemberOfOrderVersionAttrID, RelationalOperators.Empty, (object) null, (object) null, LogicalOperators.OR, -1, false, AttributeSourceTypes.Object));
      }
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams(conditionStructureList.ToArray(), columns), this._selectedObjInfo.ObjectID);
      if (dataTable.Rows.Count == 0)
        return true;
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[0], "F_OBJECT_ID", 0L);
      if (int64Value != 0L)
        this._procRouteEntryItem = new ObjInfoItem(int64Value, TechCardConsts.ObjectTypes.ProcRoutingEntryID);
    }
    return true;
  }

  /// <summary>Событие модификации данных привязок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void RouteEntryObject_Changed(object sender, EventArgs e)
  {
    this._procRouteEntryModify = true;
  }

  /// <summary>Заголовок объекта входимости для сообщений</summary>
  /// <param name="procRouteEntryObDbObject"></param>
  /// <returns></returns>
  private string ProcRouteEntryCaption(IDBObject procRouteEntryObDbObject)
  {
    return !(procRouteEntryObDbObject.Caption != string.Empty) ? $"{MetaDataHelper.GetObjectName(TechCardConsts.ObjectTypes.ProcRoutingEntryID)} ObjectId: {procRouteEntryObDbObject.ObjectID}" : procRouteEntryObDbObject.Caption;
  }

  /// <summary>Изменить привязку текущей сборки в объекте входимости</summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    if ((TypedInfoItem) this._currentAssembly == (TypedInfoItem) null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_543"), this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    if ((TypedInfoItem) this._procRouteEntryItem == (TypedInfoItem) null)
    {
      int num = (int) MessageBox.Show($"Не найден подходящий объект: {MetaDataHelper.GetObjectName(TechCardConsts.ObjectTypes.ProcRoutingEntryID)}", this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObject procRouteEntryObDbObject = session.GetObjectActualCopy(this._procRouteEntryItem.ObjectID, true);
      ProcRouteEntryObject routeEntryObject1 = new ProcRouteEntryObject(procRouteEntryObDbObject.ObjectID);
      routeEntryObject1.LoadData(session);
      if ((TypedInfoItem) this._currentOrder != (TypedInfoItem) null)
      {
        if (routeEntryObject1.MemberOfOrderVersion != Math.Abs(this._currentOrder.ObjectID) && routeEntryObject1.MemberOfOrderObject != this._currentOrder.ID)
        {
          int num = (int) MessageBox.Show($"Текущий заказ не найден в объекте: {this.ProcRouteEntryCaption(procRouteEntryObDbObject)}", this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return false;
        }
      }
      else if (routeEntryObject1.MemberOfOrderVersion != 0L || routeEntryObject1.MemberOfOrderObject != 0L)
      {
        int num = (int) MessageBox.Show($"Текущий заказ не совпадает в объекте: {this.ProcRouteEntryCaption(procRouteEntryObDbObject)}", this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return false;
      }
      switch (this._editEntryMode)
      {
        case EditCurrentAssemblyEntryMode.Add:
          if (routeEntryObject1.MemberOfAssemblyVersion.Contains<long>(Math.Abs(this._currentAssembly.ObjectID)) || routeEntryObject1.MemberOfAssemblyObject.Contains<long>(this._currentAssembly.ID))
          {
            int num = (int) MessageBox.Show($"Текущая сборка уже добавлена в объекте: {this.ProcRouteEntryCaption(procRouteEntryObDbObject)}", this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
          }
          break;
        case EditCurrentAssemblyEntryMode.Remove:
          if (!routeEntryObject1.MemberOfAssemblyVersion.Contains<long>(Math.Abs(this._currentAssembly.ObjectID)) && !routeEntryObject1.MemberOfAssemblyObject.Contains<long>(this._currentAssembly.ID))
          {
            int num = (int) MessageBox.Show($"Текущая сборка не найдена в объекте: {this.ProcRouteEntryCaption(procRouteEntryObDbObject)}", this._commandName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
          }
          break;
      }
      bool flag = false;
      if (procRouteEntryObDbObject.CheckoutBy != session.UserID)
      {
        IMSAttribute4ObjectType attribute4ObjectType1 = MetaDataHelper.GetAttribute4ObjectType(this._procRouteEntryItem.ObjTypeID, TechCardConsts.AttributeTypes.MemberOfAssemblyVersionAttrID);
        IMSAttribute4ObjectType attribute4ObjectType2 = MetaDataHelper.GetAttribute4ObjectType(this._procRouteEntryItem.ObjTypeID, TechCardConsts.AttributeTypes.MemberOfAssemblyObjectAttrID);
        if (attribute4ObjectType1.IsContent || !attribute4ObjectType1.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
          flag = true;
        else if (attribute4ObjectType2.IsContent || !attribute4ObjectType2.Options.HasFlag((Enum) AttributeOptions.ModifyInBase))
          flag = true;
        if (flag)
        {
          if (procRouteEntryObDbObject.ObjectModifyMode != ObjectModifyModes.InBase)
            procRouteEntryObDbObject = procRouteEntryObDbObject.CheckOut(true);
          else
            flag = false;
        }
      }
      ProcRouteEntryObject routeEntryObject2 = new ProcRouteEntryObject(procRouteEntryObDbObject.ObjectID);
      routeEntryObject2.LoadData(session);
      routeEntryObject2.Changed += new EventHandler(this.RouteEntryObject_Changed);
      routeEntryObject2.SetModifyStateAssembly(this._currentAssembly, routeEntryObject2.MemberBindingToVersions, this._editEntryMode == EditCurrentAssemblyEntryMode.Add);
      routeEntryObject2.Changed -= new EventHandler(this.RouteEntryObject_Changed);
      if (this._procRouteEntryModify)
        routeEntryObject2.SaveData(session);
      if (flag)
        procRouteEntryObDbObject.CheckIn();
    }
    return true;
  }

  protected override void UpdateNotificationQueue()
  {
    if (!this._procRouteEntryModify)
      return;
    this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", this._procRouteEntryItem.ObjectID));
  }
}
