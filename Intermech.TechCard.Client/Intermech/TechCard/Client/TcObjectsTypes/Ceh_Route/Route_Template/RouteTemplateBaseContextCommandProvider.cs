// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Template.RouteTemplateBaseContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.Route_Template;

/// <summary>
/// Провайдер контекстного меню для объектов типа "Шаблон расцеховки"
/// </summary>
internal class RouteTemplateBaseContextCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    IViewState service = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    if (((ViewStateFlags) (service != null ? (long) service.ViewState : 0L)).HasFlag((Enum) ViewStateFlags.ReadOnly) || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("Add", new CommandInfo(0, new ClickEventHandler(RouteTemplateBaseContextCommandProvider.AddCommand)));
    mergedCommands.Add("Exclude", new CommandInfo(0, new ClickEventHandler(RouteTemplateBaseContextCommandProvider.ExcludeCommand)));
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      if (itemData.ObjectID < 0L)
        mergedCommands.Add("CheckIn", new CommandInfo(0, new ClickEventHandler(RouteTemplateBaseContextCommandProvider.CheckInCommand)));
      else
        mergedCommands.Add("CheckOut", new CommandInfo(0, new ClickEventHandler(RouteTemplateBaseContextCommandProvider.CheckOutCommand)));
    }
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Add command</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (items == null || items.Count == sc_19477.ssp_techcard_19478(448453000) || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData) || !MetaDataHelper.IsObjectTypeChildOf(parentData.ObjectType, TechCardConsts.ObjectTypes.CehRouteID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID, false);
      if (dbObject == null)
        return;
      int lcLevelId = MetaDataHelper.GetLCLevelID(TechCardConsts.LcLevel.LifeCycleLevelStoring);
      IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(dbObject.LCStep, false);
      if (lifecycleStep != null && lifecycleStep.ObjectModifyMode != ObjectModifyModes.CantModify && lifecycleStep.LevelID != lcLevelId)
        return;
      int result = -1;
      object[] valuesByGuid = dbObject.GetValuesByGuid(TechCardConsts.AttributeTypes.LifeCycleStepPrevGUID, false);
      if (valuesByGuid != null && valuesByGuid.Length != 0 && valuesByGuid[0] != null && valuesByGuid[0] != DBNull.Value)
        int.TryParse(valuesByGuid[0].ToString(), out result);
      if (result == -1)
        result = sessionKeeper.Session.GetLifecycleStepCollection(dbObject.ObjectType).GetFirstStep();
      if (result == -1)
        return;
      dbObject.LCStep = result;
    }
  }

  /// <summary>Exclude command</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void ExcludeCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    if (items == null || items.Count == sc_19477.ssp_techcard_19479(620326513) || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData) || !MetaDataHelper.IsObjectTypeChildOf(parentData.ObjectType, TechCardConsts.ObjectTypes.CehRouteID))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.ObjectID);
      IDBAttribute attributeById = dbObject.GetAttributeByID(TechCardConsts.AttributeTypes.ContextAttrID);
      if (attributeById != null && attributeById.AsInteger == parentData.ObjectID)
      {
        if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
          return;
        if (!dbObject.ReadOnly)
        {
          AttributeValues[] valuesList = new AttributeValues[1]
          {
            new AttributeValues(TechCardConsts.AttributeTypes.ContextAttrID, (object) DBNull.Value)
          };
          dbObject.SetAttributesValues(valuesList);
        }
      }
      List<ColumnDescriptor> columns = new List<ColumnDescriptor>()
      {
        new ColumnDescriptor((object) -6, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID).ToArray(), LogicalOperators.NONE, 0, false)
      };
      DataTable parentSostavData = DataHelper.GetParentSostavData(new ObjInfoItem(itemData.ObjectID, itemData.ObjectType), sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
      int num2 = parentSostavData != null ? parentSostavData.AsEnumerable().Count<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt32(row["F_CHKOUT_BY"]) != 0)) : 0;
      int lcLevelTechProcElemId = MetaDataHelper.GetLCLevelID(TechCardConsts.LcLevel.LifeCycleLevelStoring);
      int lcLevelAnnulledId = MetaDataHelper.GetLCLevelID(TechCardConsts.LcLevel.LifeCycleLevelAnnulled);
      if (num2 > 1)
        return;
      DataTable table = sessionKeeper.Session.GetLifecycleStepCollection(itemData.ObjectType).GetSchema().Tables["IMS_LC_STEPS"];
      int num3 = table.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt32(row["F_LEVEL_ID"]) == lcLevelTechProcElemId)).Select<DataRow, int>((System.Func<DataRow, int>) (row => Convert.ToInt32(row["F_LC_STEP"]))).FirstOrDefault<int>(-1);
      if (num3 == -1)
        num3 = table.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt32(row["F_MODIFY_MODE"]) == Convert.ToInt32((object) ObjectModifyModes.CantModify) && Convert.ToInt32(row["F_LEVEL_ID"]) != lcLevelAnnulledId)).Select<DataRow, int>((System.Func<DataRow, int>) (row => Convert.ToInt32(row["F_LC_STEP"]))).FirstOrDefault<int>(-1);
      if (num3 == -1)
        return;
      AttributeValues[] valuesList1 = new AttributeValues[1]
      {
        new AttributeValues(TechCardConsts.AttributeTypes.LifeCycleStepPrevID, (object) dbObject.LCStep)
      };
      dbObject.SetAttributesValues(valuesList1);
      dbObject.LCStep = num3;
    }
  }

  /// <summary>CheckIn command</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CheckInCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !RouteTemplateBaseContextCommandProvider.CheckInRouteTemplate(itemData))
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", itemData.ObjectID));
  }

  /// <summary>CheckOut command</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void CheckOutCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (!(items.GetItemData(sc_19477.ssp_techcard_19480(268987540), typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !RouteTemplateBaseContextCommandProvider.CheckOutRouteTemplate(itemData, items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID))
      return;
    ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) new long[1]
    {
      itemData.ObjectID
    }, (IList<long>) new long[1]{ -itemData.ObjectID }));
  }

  /// <summary>CheckIn route template</summary>
  /// <param name="typedObjectId"></param>
  /// <returns></returns>
  private static bool CheckInRouteTemplate(IDBTypedObjectID typedObjectId)
  {
    if (typedObjectId == null || !MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, TechCardConsts.ObjectTypes.TemplRouteBaseID))
      return false;
    List<long> objectIds = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(typedObjectId.ObjectID);
      if (dbObject1.CheckoutBy != sessionKeeper.Session.UserID)
        return false;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID);
      childrenIdRecursive.Add(TechCardConsts.ObjectTypes.CehRouteID);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
      };
      DataTable parentSostavData = DataHelper.GetParentSostavData(new ObjInfoItem(dbObject1), sessionKeeper.Session, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, (IEnumerable<ConditionStructure>) conditions);
      if (parentSostavData != null)
        objectIds.AddRange((IEnumerable<long>) parentSostavData.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (dataRow => Convert.ToInt64(dataRow["F_PROJ_ID"]))));
      bool flag = true;
      if (objectIds.Count > 1)
        flag = CehRoutesListDialog.ShowDialog(string.Format(LocalizationHolder.rm.GetString(sc_19477.ssp_techcard_19481()), (object) TechCardConsts.Utils.GetObjectString(typedObjectId.ObjectID, sessionKeeper.Session)), (IEnumerable<long>) objectIds, out long _);
      IDBObject prototype = sessionKeeper.Session.GetObject(typedObjectId.ObjectID);
      if (flag)
      {
        if (prototype.CheckoutBy == 0L)
          return false;
        prototype.CheckIn();
        return true;
      }
      int objectType = typedObjectId.ObjectType;
      IDBObject dbObject2 = sessionKeeper.Session.GetObjectCollection(objectType).Create(prototype);
      dbObject2.CommitCreation(false);
      IDBAttribute attributeByGuid = prototype.GetAttributeByGuid(TechCardConsts.AttributeTypes.ContextAttrGuid);
      if (attributeByGuid != null && attributeByGuid.Value != DBNull.Value)
      {
        IDBObject dbObject3 = sessionKeeper.Session.GetObject(attributeByGuid.AsInteger);
        if (dbObject3 != null)
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
          IDBRelation relation = sessionKeeper.Session.GetRelation(dbObject3.ObjectID, prototype.ObjectID, TechCardConsts.RelTypes.TechRelationID, true);
          long objectId1 = dbObject3.ObjectID;
          long objectId2 = dbObject2.ObjectID;
          DateTime now = DateTime.Now;
          relationCollection.Create(objectId1, objectId2, now).Attributes.Assign(relation.Attributes);
          AttributeValues[] valuesList = new AttributeValues[1]
          {
            new AttributeValues(MetaDataHelper.GetAttributeID((object) "cad001c2-306c-11d8-b4e9-00304f19f545"), (object) dbObject3.ObjectID)
          };
          dbObject3.SetAttributesValues(valuesList);
        }
      }
      prototype.CancelChanges();
      return true;
    }
  }

  /// <summary>CheckOut route template</summary>
  /// <param name="typedObjectId"></param>
  /// <param name="dbRelationId"></param>
  /// <returns></returns>
  private static bool CheckOutRouteTemplate(
    IDBTypedObjectID typedObjectId,
    IDBRelationID dbRelationId)
  {
    if (typedObjectId == null || !MetaDataHelper.IsObjectTypeChildOf(typedObjectId.ObjectType, TechCardConsts.ObjectTypes.TemplRouteBaseGUID) || !MetaDataHelper.GetObjectType(typedObjectId.ObjectType).AnyAttributes && MetaDataHelper.GetAttribute4ObjectType(typedObjectId.ObjectType, TechCardConsts.AttributeTypes.ContextAttrID) == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObject(typedObjectId.ObjectID);
      if (objectActualCopy.CheckoutBy != 0L)
        return false;
      IDBObject dbObject1 = dbRelationId != null ? sessionKeeper.Session.GetObject(dbRelationId.ProjID, false) : (IDBObject) null;
      bool flag = true;
      if (dbObject1 == null)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID);
        childrenIdRecursive.Add(TechCardConsts.ObjectTypes.CehRouteID);
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), LogicalOperators.NONE, 0, false)
        };
        DataTable parentSostavData = DataHelper.GetParentSostavData(new ObjInfoItem(objectActualCopy), sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          TechCardConsts.RelTypes.TechRelationID
        }, false, (IEnumerable<ConditionStructure>) conditions);
        List<long> objectIds = new List<long>();
        if (parentSostavData != null)
          objectIds.AddRange((IEnumerable<long>) parentSostavData.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row["F_PROJ_ID"]))));
        switch (objectIds.Count)
        {
          case 0:
            break;
          case 1:
            dbObject1 = sessionKeeper.Session.GetObject(objectIds[0], true);
            break;
          default:
            long selectedObjectId;
            if (MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_144"), "", MessageBoxButtons.YesNo) != DialogResult.Yes || !CehRoutesListDialog.ShowDialog(LocalizationHolder.rm.GetString(sc_19477.ssp_techcard_19482()), (IEnumerable<long>) objectIds, out selectedObjectId) || selectedObjectId != 0L)
              return false;
            flag = false;
            dbObject1 = sessionKeeper.Session.GetObject(selectedObjectId);
            objectActualCopy = sessionKeeper.Session.GetObject(typedObjectId.ObjectID);
            break;
        }
      }
      if (dbObject1 == null)
      {
        objectActualCopy.CheckOut();
        return true;
      }
      if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject1.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      {
        string objectString1 = TechCardConsts.Utils.GetObjectString(dbObject1, false);
        if (dbObject1.CheckoutBy != 0L && dbObject1.CheckoutBy != sessionKeeper.Session.UserID)
        {
          string objectString2 = TechCardConsts.Utils.GetObjectString(sessionKeeper.Session.UserID, sessionKeeper.Session);
          int num = (int) MessageBox.Show($"{LocalizationHolder.rm.GetString(sc_19477.ssp_techcard_19483())}{LocalizationHolder.rm.GetString("TechCard.Client_135")}{objectString1}{LocalizationHolder.rm.GetString("TechCard.Client_136")}{LocalizationHolder.rm.GetString(sc_19477.ssp_techcard_19484())}{objectString2}'.", LocalizationHolder.rm.GetString("TechCard.Client_138"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
        if (flag && MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19477.ssp_techcard_19485()), (object) objectString1), LocalizationHolder.rm.GetString("TechCard.Client_142"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return false;
        dbObject1 = dbObject1.CheckOut();
        objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectActualCopy.ObjectID, false);
        TechCardClientConst.OpenObjectInNewWindow(dbObject1.ObjectID);
      }
      IDBObject dbObject2 = objectActualCopy.CheckOut();
      if (dbObject2 != null)
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(TechCardConsts.AttributeTypes.ContextAttrID, (object) dbObject1.ObjectID)
        };
        dbObject2.SetAttributesValues(valuesList);
      }
    }
    return true;
  }
}
