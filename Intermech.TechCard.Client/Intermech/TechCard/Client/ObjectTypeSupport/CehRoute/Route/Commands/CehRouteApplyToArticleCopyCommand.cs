// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.Commands.CehRouteApplyToArticleCopyCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.MRP2;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Commands.CreateVersion;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Route.Commands;

/// <summary>
/// Реализация команды контекстного меню "Применить изменения в других ПК ДСЕ"
/// </summary>
internal class CehRouteApplyToArticleCopyCommand : TechCardSelectedItemsCommand
{
  /// <summary>
  /// 
  /// </summary>
  private ICollection<ObjInfoItem> _otherArticleCopyInfoItems;

  /// <summary>Получение родительских объектов типа "ПК ДСЕ"</summary>
  /// <param name="session"></param>
  /// <param name="articleCopyInfoItems"></param>
  /// <returns></returns>
  private bool GetParentArticleCopyObjects(
    [NotNull] IUserSession session,
    out ICollection<ObjInfoItem> articleCopyInfoItems)
  {
    articleCopyInfoItems = (ICollection<ObjInfoItem>) null;
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) session, true);
    CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      this._selectedObjInfo
    }, (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ArticleCopyBaseID).ToArray(), (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID).ToArray(), (IEnumerable<int>) new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    }, RelObjInfoDbScheme<ObjInfoItem>.GetSourceTableColumns(), (IEnumerable<ConditionStructure>) null, false, false, 3, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
    DataTable source = service.LoadComplexCompositions((object) session.SessionGUID, loadingParams);
    if (source == null)
      return false;
    ObjInfoDbScheme objInfoDbScheme = new ObjInfoDbScheme();
    articleCopyInfoItems = (ICollection<ObjInfoItem>) objInfoDbScheme.ParseItems((IEnumerable<DataRow>) source.AsEnumerable()).ToList<ObjInfoItem>();
    return articleCopyInfoItems.Any<ObjInfoItem>();
  }

  /// <summary>
  /// Получение всех ПК ДСЕ созданных на основе той же версии ДСЕ
  /// </summary>
  /// <param name="session"></param>
  /// <param name="articleCopyInfoItems"></param>
  /// <param name="otherArticleCopyInfoItems"></param>
  /// <returns></returns>
  private bool GetOtherArticleCopyObject(
    [NotNull] IUserSession session,
    ICollection<ObjInfoItem> articleCopyInfoItems,
    out ICollection<ObjInfoItem> otherArticleCopyInfoItems)
  {
    otherArticleCopyInfoItems = (ICollection<ObjInfoItem>) null;
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) MRP2Consts.attrIdArticleLink, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0)
    };
    DataTable objectDataEx1 = DataHelper.GetObjectDataEx(-1, session, new DBRecordSetParams((ConditionStructure[]) null, columns), (IEnumerable<ObjInfoItem>) articleCopyInfoItems);
    if (objectDataEx1 == null)
      return false;
    long[] array = objectDataEx1.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 0, 0L))).Where<long>((System.Func<long, bool>) (objectId => objectId != 0L)).ToArray<long>();
    if (!((IEnumerable<long>) array).Any<long>())
      return false;
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(MRP2Consts.attrIdArticleLink, RelationalOperators.In, (object) array, LogicalOperators.AND, 0, false),
      new ConditionStructure(-2, RelationalOperators.NotIn, (object) SomeTypedInfoHelper<ObjInfoItem>.GetItemIDs((IEnumerable<ObjInfoItem>) articleCopyInfoItems).ToArray(), LogicalOperators.NONE, 0, false)
    };
    DataTable objectDataEx2 = DataHelper.GetObjectDataEx((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ArticleCopyBaseID), session, new DBRecordSetParams(conditions, ObjInfoDbScheme.GetSourceTableColumns().ToArray<ColumnDescriptor>()), (IEnumerable<ObjInfoItem>) null);
    if (objectDataEx2 == null)
      return false;
    ObjInfoDbScheme objInfoDbScheme = new ObjInfoDbScheme();
    ref ICollection<ObjInfoItem> local = ref otherArticleCopyInfoItems;
    IEnumerable<ObjInfoItem> items = objInfoDbScheme.ParseItems((IEnumerable<DataRow>) objectDataEx2.AsEnumerable());
    List<ObjInfoItem> list = items != null ? items.ToList<ObjInfoItem>() : (List<ObjInfoItem>) null;
    local = (ICollection<ObjInfoItem>) list;
    ICollection<ObjInfoItem> source = otherArticleCopyInfoItems;
    return source != null && source.Any<ObjInfoItem>();
  }

  /// <summary>Выбор РМ для модификации</summary>
  /// <param name="cehRouteRelObjInfoItems"></param>
  /// <returns></returns>
  private bool SelectCehRouteObjects(
    out ICollection<RelObjInfoItem> cehRouteRelObjInfoItems)
  {
    cehRouteRelObjInfoItems = (ICollection<RelObjInfoItem>) null;
    List<ObjInfoItem> list = this._otherArticleCopyInfoItems.ToList<ObjInfoItem>();
    GenericListHelper.MakeUnique<ObjInfoItem>(list);
    HashSet<int> allowedObjectTypeIds = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehRouteID).ToHashSet<int>();
    List<int> intList = new List<int>();
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MRP2Consts.objtypeIdProductionObjects));
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.ProcRoutingID));
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID));
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object)
    };
    List<IDescriptor> descriptorList = new List<IDescriptor>();
    foreach (ObjInfoItem objInfoItem in list)
    {
      int versionsObjectNode = Intermech.Navigator.Consts.CategoryVersionsObjectNode;
      int objTypeId = objInfoItem.ObjTypeID;
      long objectId = objInfoItem.ObjectID;
      int productionObjects = MRP2Consts.objtypeIdProductionObjects;
      int[] compRelTypeIDs = new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      };
      string empty = string.Empty;
      TechCompositionConditionFilter compositionConditionFilter = new TechCompositionConditionFilter((IEnumerable<ConditionStructure>) conditions);
      compositionConditionFilter.QueryFilter = (IRelatedObjectQueryFilterMode) new RelatedObjectQueryFilterMode(filterDataByVersionRule: false);
      TechCompositionDescriptor compositionDescriptor = new TechCompositionDescriptor(versionsObjectNode, objTypeId, objectId, productionObjects, (IEnumerable<int>) compRelTypeIDs, empty, RelatedObjectsRole.Composition, (ITechCompositionFilter) compositionConditionFilter, (IEnumerable<NodeColumnID>) null);
      descriptorList.Add((IDescriptor) compositionDescriptor);
    }
    DescriptorCollection descriptors = new DescriptorCollection((IEnumerable<IDescriptor>) descriptorList);
    using (TechcardObjectForm form = new TechcardObjectForm())
    {
      form.Name = "SelectCehRouteApplyToArticleObjectDialog";
      form.EnableBtnOk = false;
      form.tolcTechObjList.CheckRootNode = true;
      form.tolcTechObjList.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
      form.Icon = Statics.IconSrv.GetIcon(4, TechCardConsts.ObjectTypes.CehRouteID);
      ICurrentUserAndRole currentUserAndRole = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, true);
      form.tolcTechObjList.AfterCreateNode += (EventHandler<NodeEventArgs>) ((sender, e) =>
      {
        TechcardNavTreeNode node = e != null ? e.Node as TechcardNavTreeNode : (TechcardNavTreeNode) null;
        NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
        if (node == null || navigatorTreeView == null)
          return;
        INode nodeHandler = navigatorTreeView.GetNodeHandler((NavigatorTreeNode) node);
        if (nodeHandler == null)
          return;
        IDBTypedObjectID data2 = node.NodeID is NodeID nodeId2 ? nodeHandler.GetData((INodeID) nodeId2, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
        if (data2 == null || !allowedObjectTypeIds.Contains(data2.ObjectType))
          node.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
        else if (nodeHandler.GetData((INodeID) nodeId2, typeof (IDBCheckedOutByID)) is IDBCheckedOutByID data3 && data3.CheckedOutBy != currentUserAndRole.UserID && data3.CheckedOutBy != 0L)
          node.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
        else
          node.SetCheckStateInternal(CheckState.Unchecked);
      });
      form.tolcTechObjList.CheckStateChanging += (EventHandler<CheckStateEventArgs>) ((sender, args) =>
      {
        if (!(args.Node is TechcardNavTreeNode node2) || args.OldValue != CheckState.Indeterminate || args.OldValue == args.NewValue)
          return;
        args.NewValue = args.OldValue;
        node2.SetCheckStateInternal(args.OldValue);
      });
      form.tolcTechObjList.CheckStateChanged += (EventHandler<NodeEventArgs>) ((sender, args) =>
      {
        if (!(args.Node is TechcardNavTreeNode node4) || !(node4.Tree?.Parent is TechcardObjectForm parent2))
          return;
        parent2.EnableBtnOk = parent2.tolcTechObjList.CheckedItems.Count > 0;
      });
      form.Load += (EventHandler) ((sender, e) =>
      {
        if (!(sender is TechcardObjectForm techcardObjectForm2) || techcardObjectForm2.tolcTechObjList.RootNode?.Children == null)
          return;
        foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) techcardObjectForm2.tolcTechObjList.RootNode.Children)
        {
          if (child is TechcardNavTreeNode techcardNavTreeNode2)
            techcardNavTreeNode2.ExpandNode(false);
        }
      });
      form.LoadData(LocalizationHolder.rm.GetString("TechCard.RouteGroupCommand_SelectCehRoutes"), (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.TechBaseObjectID, LocalizationHolder.rm.GetString("TechCard.Client_505"), descriptors));
      if (form.ShowTopDialog() != DialogResult.OK || form.tolcTechObjList.CheckedItems.Count == 0)
        return false;
      cehRouteRelObjInfoItems = (ICollection<RelObjInfoItem>) new List<RelObjInfoItem>();
      for (int index = 0; index < form.tolcTechObjList.CheckedItems.Count; ++index)
      {
        if (form.tolcTechObjList.CheckedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1 && form.tolcTechObjList.CheckedItems.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData2)
        {
          IDBTypedObjectID parentData = form.tolcTechObjList.CheckedItems.GetParentData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
          cehRouteRelObjInfoItems.Add(new RelObjInfoItem(itemData2.Value, itemData2.RelationType)
          {
            PartInfo = (ObjInfoItem) new ObjInfoIDItem(itemData1.ObjectID, itemData1.ObjectType, itemData1.ID),
            ProjInfo = parentData != null ? (ObjInfoItem) new ObjInfoIDItem(parentData.ObjectID, parentData.ObjectType, parentData.ID) : (ObjInfoItem) new ObjInfoIDItem(itemData2.ProjID)
          });
        }
      }
      return cehRouteRelObjInfoItems.Any<RelObjInfoItem>();
    }
  }

  private bool DoCheckObjectsModifications([NotNull] ICollection<RelObjInfoItem> modifyRelObjInfoItems)
  {
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      foreach (RelObjInfoItem modifyRelObjInfoItem in (IEnumerable<RelObjInfoItem>) modifyRelObjInfoItems)
      {
        IDBObject dbObject1 = session.GetObject(modifyRelObjInfoItem.PartInfo.ObjectID, true);
        switch (dbObject1.ObjectModifyMode)
        {
          case ObjectModifyModes.Checkout:
            if (dbObject1.CheckoutBy != session.UserID)
            {
              IDBObject dbObject2 = dbObject1.CheckOut();
              modifyRelObjInfoItem.PartInfo = (ObjInfoItem) new ObjInfoIDItem(dbObject2);
              continue;
            }
            continue;
          case ObjectModifyModes.CreateVersion:
            source.Add(modifyRelObjInfoItem);
            continue;
          default:
            continue;
        }
      }
    }
    if (!source.Any<RelObjInfoItem>())
      return true;
    Dictionary<long, List<long>> dictionary = source.GroupBy<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (relObjInfoItem => relObjInfoItem.ProjInfo.ObjectID)).ToDictionary<IGrouping<long, RelObjInfoItem>, long, List<long>>((System.Func<IGrouping<long, RelObjInfoItem>, long>) (group => group.Key), (System.Func<IGrouping<long, RelObjInfoItem>, List<long>>) (group => group.Select<RelObjInfoItem, long>((System.Func<RelObjInfoItem, long>) (item => item.RelationID)).ToList<long>()));
    CreateVersionCommand createVersionCommand = new CreateVersionCommand();
    createVersionCommand.Init(RelationExtensions.GetItems(dictionary), this.ContextServices, (object) null);
    createVersionCommand.Execute();
    if (createVersionCommand.CreatedRelObjInfoList == null || !createVersionCommand.CreatedRelObjInfoList.Any<RelObjInfoItem>())
      return false;
    foreach (RelObjInfoItem modifyRelObjInfoItem in (IEnumerable<RelObjInfoItem>) modifyRelObjInfoItems)
    {
      long partId = ((ObjInfoIDItem) modifyRelObjInfoItem.PartInfo).ID;
      long projId = ((ObjInfoIDItem) modifyRelObjInfoItem.ProjInfo).ID;
      RelObjInfoItem relObjInfoItem = createVersionCommand.CreatedRelObjInfoList.FirstOrDefault<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => ((ObjInfoIDItem) item.PartInfo).ID == partId && ((ObjInfoIDItem) item.ProjInfo).ID == projId));
      if (!((TypedInfoItem) relObjInfoItem == (TypedInfoItem) null))
        modifyRelObjInfoItem.CopyFrom((TypedInfoItem) relObjInfoItem);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cehRoutes2ModifyList"></param>
  private bool DoApplyChangeToObjects([NotNull] ICollection<RelObjInfoItem> cehRoutesModifyList)
  {
    if (!this.DoCheckObjectsModifications(cehRoutesModifyList))
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this._selectedObjInfo.ObjectID, false);
      ITechUtilsService service1 = ServiceUtils.GetService<ITechUtilsService>((object) sessionKeeper.Session, true);
      ICompositionLoadService service2 = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
      ColumnDescriptor[] columns = new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(TechCardConsts.ObjectTypes.CehBaseRouteID);
      CompositionLoadingParams loadingParams1 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        this._selectedObjInfo
      }, (IEnumerable<int>) childrenIdRecursive, (IEnumerable<int>) childrenIdRecursive, (IEnumerable<int>) new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, 2, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
      DataTable source = service2.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams1);
      List<long> copyRelationList = new List<long>();
      if (source != null)
        copyRelationList.AddRange((IEnumerable<long>) source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => DataSetProcessor.GetInt64Value(row, 0, 0L))));
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cadd9668-306c-11d8-b4e9-00304f19f545");
      sessionKeeper.Session.StartLogHistory();
      try
      {
        foreach (RelObjInfoItem cehRoutesModify in (IEnumerable<RelObjInfoItem>) cehRoutesModifyList)
        {
          CompositionLoadingParams loadingParams2 = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
          {
            cehRoutesModify.PartInfo
          }, (IEnumerable<int>) childrenIdRecursive, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
          {
            TechCardConsts.RelTypes.TechRelationID
          }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) null, true, false, 1, (VersionsRule) null, VersionsRuleSources.GetCurrentWindowRule().OwnerId);
          DataTable dataTable = service2.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams2);
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
              if (!Intermech.Consts.IsUndefinedRelationId(int64Value))
                sessionKeeper.Session.GetRelation(int64Value, false)?.Delete(0L);
            }
          }
          service1.CreateObjectComposition(objectActualCopy.ObjectID, cehRoutesModify.PartInfo.ObjectID, sessionKeeper.Session.SessionGUID, copyRelationList);
          foreach (long consistFromBlank in relationCollection.ConsistFromBlanks(cehRoutesModify.PartInfo.ObjectID))
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(consistFromBlank, false);
            if (dbObject != null && dbObject.IsCreationMode && childrenIdRecursive.Contains(dbObject.ObjectType))
              dbObject.CommitCreation(true, true);
          }
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(cehRoutesModify.PartInfo.ObjectID, true);
          dbObject1.Attributes.AssignPossibleAttributes(objectActualCopy.Attributes, 0);
          if (MetaDataHelper.GetAttribute4ObjectType(dbObject1.ObjectType, attributeTypeId) != null)
            dbObject1.Attributes.AddAttribute(attributeTypeId, false, new object[1]
            {
              (object) Math.Abs(objectActualCopy.ObjectID)
            });
        }
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
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  public CehRouteApplyToArticleCopyCommand()
    : base("RouteGroupCommand_ApplyToArticleCopy")
  {
  }

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  protected new virtual bool ValidateCommandArgs()
  {
    if (!base.ValidateCommandArgs())
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.EditingContextID == 0L)
      {
        int num = (int) MessageBox.Show("Контекст редактирования не выбран. Изменение РМ невозможно.", LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool LoadCommandInfo()
  {
    if (!base.LoadCommandInfo())
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICollection<ObjInfoItem> articleCopyInfoItems;
      if (!this.GetParentArticleCopyObjects(sessionKeeper.Session, out articleCopyInfoItems))
      {
        int num = (int) MessageBox.Show($"Для объекта \"{sessionKeeper.Session.GetObjectInfo(this._selectedObjInfo.ObjectID).Caption} (ObjectId = {this._selectedObjInfo.ObjectID})\" не найдены родительские объекты ПК ДСЕ.", LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      }
      if (!this.GetOtherArticleCopyObject(sessionKeeper.Session, articleCopyInfoItems, out this._otherArticleCopyInfoItems))
      {
        long objectId = articleCopyInfoItems.First<ObjInfoItem>().ObjectID;
        int num = (int) MessageBox.Show($"Для объекта \"{sessionKeeper.Session.GetObjectInfo(objectId).Caption} (ObjectId = {objectId})\" не найдены \"другие\" объекты ПК ДСЕ, " + " созданные на основе той же версии ДСЕ.", LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return false;
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    ICollection<RelObjInfoItem> cehRouteRelObjInfoItems;
    return this.SelectCehRouteObjects(out cehRouteRelObjInfoItems) && this.DoApplyChangeToObjects(cehRouteRelObjInfoItems);
  }
}
