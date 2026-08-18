// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObjectContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.HelperClasses.UIHelpers;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Commands;
using Intermech.TechCard.Client.Commands.Edit;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Extensions;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcs;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// Summary description for TechCardBaseObjectContextCommandProvider.
/// </summary>
public class TechCardBaseObjectContextCommandProvider : ICommandsProvider
{
  /// <summary>Stepwise provider</summary>
  private StepwiseProviderManager _checkInOutManager;
  /// <summary>
  /// список команд которые можно отображать в режиме multi-select
  /// </summary>
  private readonly List<string> _listOfMultiSelectCommand = new List<string>((IEnumerable<string>) new string[7]
  {
    "Cut",
    "Copy",
    "Delete",
    "Paste",
    "CreateVersion",
    "techCheckAll",
    "techUncheckAll"
  });

  /// <summary>Techproc element "Add command"</summary>
  /// <param name="objectId"></param>
  private static void TechProcElemCommandAdd(long objectId)
  {
    long objectId1 = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.TechProcElemBaseGUID, LocalizationHolder.rm.GetString(sc_19601.ssp_techcard_19602()));
    if (objectId1 == 0L)
      return;
    TechProcTreeDialog techProcTreeDialog1 = new TechProcTreeDialog();
    techProcTreeDialog1.Text = LocalizationHolder.rm.GetString("TechCard.Client_374");
    TechProcTreeDialog techProcTreeDialog2 = techProcTreeDialog1;
    if (!techProcTreeDialog2.ShowDialog(objectId1) || techProcTreeDialog2.NodeList.Count == 0)
      return;
    List<long> longList = new List<long>();
    foreach (NavigatorTreeNode node1 in techProcTreeDialog2.NodeList)
    {
      if (node1.CheckState == CheckState.Checked)
      {
        NavigatorTreeNode node2 = node1;
        while (node2.Parent != null && node2.Parent.CheckState == CheckState.Checked)
          node2 = node2.Parent;
        if (node2 != null)
        {
          NavigatorTreeNode navigatorTreeNode = node2;
          if (navigatorTreeNode.NodeID.CategoryID == 1 && techProcTreeDialog2.TreeView.GetNodeHandler(node2).GetData(navigatorTreeNode.NodeID, typeof (IDBObjectID)) is IDBObjectID data && longList.IndexOf(data.Value) == -1)
            longList.Add(data.Value);
        }
      }
    }
    if (longList.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<long> relationIDs = new List<long>();
      List<long> projIDs = new List<long>();
      List<int> relTypeIDs = new List<int>();
      try
      {
        int lcLevelId = MetaDataHelper.GetLCLevelID(TechCardConsts.LcLevel.LifeCycleLevelStoring);
        int[] array = new int[2]
        {
          MetaDataHelper.GetLCLevelID(TechCardConsts.LcLevel.LifeCycleLevelAnnulled),
          MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545")
        };
        TechcardClientUtils.StartCreateRelations(objectId, sessionKeeper.Session);
        foreach (long objectID in longList)
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
          if (!objectInfo.Empty)
          {
            IDBObject dbObject1 = sessionKeeper.Session.GetObjectCollection(objectInfo.ObjectTypeID).Create(objectInfo.ObjectID);
            List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, dbObject1.ObjectID, new int[1]
            {
              TechCardConsts.RelTypes.TechRelationID
            }, new long[1]{ objectId }, DateTime.Now, TechCreateRelMode.tcrmEnterIn);
            if (relations != null && relations.Count != 0)
            {
              if (dbObject1.IsCreationMode)
                dbObject1.CommitCreation(true);
              List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(dbObject1.ObjectID, sessionKeeper.Session, (IEnumerable<int>) new int[1]
              {
                TechCardConsts.RelTypes.TechRelationID
              });
              childSostavTree.Add(new TechCardUtils.SostavSortedTreeItem(0L, dbObject1.ObjectID, -1, 0L, TechCardConsts.RelTypes.TechRelationID, 0L));
              List<Tuple<IDBObject, int>> tupleList = new List<Tuple<IDBObject, int>>();
              foreach (TechCardUtils.SostavTreeItem sostavTreeItem in childSostavTree)
              {
                IDBObject dbObject2 = sessionKeeper.Session.GetObject(sostavTreeItem.PartID);
                DataTable table = sessionKeeper.Session.GetLifecycleStepCollection(dbObject2.ObjectType).GetSchema().Tables["IMS_LC_STEPS"];
                int[] nextSteps = sessionKeeper.Session.GetLifecycleStep(dbObject2.LCStep)?.GetNextSteps();
                int num = -1;
                if (nextSteps != null)
                {
                  foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
                  {
                    int int32 = Convert.ToInt32(row["F_LC_STEP"]);
                    if (Convert.ToInt32(row["F_LEVEL_ID"]) == lcLevelId && Array.IndexOf<int>(nextSteps, int32) != -1)
                    {
                      num = int32;
                      break;
                    }
                  }
                  if (num == -1)
                  {
                    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
                    {
                      int int32 = Convert.ToInt32(row["F_LC_STEP"]);
                      if (Convert.ToInt32(row["F_MODIFY_MODE"]) == Convert.ToInt32((object) ObjectModifyModes.CantModify) && Array.IndexOf<int>(array, Convert.ToInt32(row["F_LEVEL_ID"])) == -1 && Array.IndexOf<int>(nextSteps, int32) != -1)
                      {
                        num = int32;
                        break;
                      }
                    }
                  }
                }
                if (num != -1)
                {
                  if (dbObject2.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject2.CheckoutBy == 0L)
                    dbObject2 = dbObject2.CheckOut();
                  AttributeValues[] valuesList = new AttributeValues[1];
                  int attributeID = TechCardConsts.Utils.AttributeTypeByGuid(TechCardConsts.AttributeTypes.LifeCycleStepPrevGUID, sessionKeeper.Session);
                  valuesList[0] = new AttributeValues(attributeID, (object) dbObject1.LCStep);
                  dbObject2.SetAttributesValues(valuesList);
                  if (dbObject2.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject2.CheckoutBy != 0L)
                  {
                    dbObject2.CheckIn();
                    dbObject2 = sessionKeeper.Session.GetObject(dbObject2.ObjectID);
                  }
                  tupleList.Add(new Tuple<IDBObject, int>(dbObject2, num));
                }
              }
              foreach (Tuple<IDBObject, int> tuple in tupleList)
                tuple.Item1.LCStep = tuple.Item2;
              foreach (IDBRelation dbRelation in relations)
              {
                relationIDs.Add(dbRelation.RelationID);
                projIDs.Add(dbRelation.ProjID);
                relTypeIDs.Add(dbRelation.RelationType);
              }
            }
            else
              dbObject1.Delete(0L);
          }
        }
        if (relationIDs.Count <= 0)
          return;
        ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false)?.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs));
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
    }
  }

  /// <summary>Конструктор</summary>
  public TechCardBaseObjectContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service1))
      return;
    INamedImageList service2 = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    MenuTemplate contextMenuTemplate = service1.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "editObjectNode", LocalizationHolder.rm.GetString("TechCard.Client_239"), -1, 13, 30);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "TechCard.Replace", LocalizationHolder.rm.GetString("TechCard.Client_514"), -1, 13, 30);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techCheckAll", LocalizationHolder.rm.GetString("TechCard.Client_478"), -1, 13, 1000, Keys.A | Keys.Control);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techUncheckAll", LocalizationHolder.rm.GetString("TechCard.Client_369"), -1, 13, 1100);
      MenuTemplateNode orCreate1 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "moveObjectNode", LocalizationHolder.rm.GetString("TechCard.Client_243"), service2 != null ? service2.ImageIndex("imgMoveComposition") : -1, 13, 40);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "moveTop", LocalizationHolder.rm.GetString("TechCard.Client_357"), service2 != null ? service2.ImageIndex("imgMoveFirst") : -1, 100, 100, Keys.H | Keys.Control);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "moveUp", LocalizationHolder.rm.GetString("TechCard.Client_358"), service2 != null ? service2.ImageIndex("imgMoveUp") : -1, 100, 200, Keys.U | Keys.Control);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "moveDown", LocalizationHolder.rm.GetString("TechCard.Client_359"), service2 != null ? service2.ImageIndex("imgMoveDown") : -1, 100, 300, Keys.D | Keys.Control);
      TcClientUtils.FindOrCreate(orCreate1.Nodes, "moveBottom", LocalizationHolder.rm.GetString("TechCard.Client_360"), service2 != null ? service2.ImageIndex("imgMoveLast") : -1, 100, 400, Keys.L | Keys.Control);
      TcClientUtils.FindOrCreate(TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "numTemplateNode", LocalizationHolder.rm.GetString("TechCard.Client_240"), service2 != null ? service2.ImageIndex("imgNumerate") : -1, 13, 50).Nodes, "numObjectTemplateNode", LocalizationHolder.rm.GetString("TechCard.Client_241"), service2 != null ? service2.ImageIndex("imgNumerateObject") : -1, 100, 100);
      MenuTemplateNode orCreate2 = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techProcElemTemplateNode", LocalizationHolder.rm.GetString("TechCard.Client_247"), -1, 13, 100);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techProcElemInsertNode", LocalizationHolder.rm.GetString("TechCard.Client_248"), -1, 100, 100);
      TcClientUtils.FindOrCreate(orCreate2.Nodes, "techProcElemInsertIntoNode", LocalizationHolder.rm.GetString("TechCard.Client_249"), -1, 100, 200);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "CopyWithRelAttrs", LocalizationHolder.rm.GetString("TechCard.Client_526"), -1, 13, 30);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techUnwrapComposition", LocalizationHolder.rm.GetString("TechCard.Client_33"), -1, 13, 300, Keys.Multiply);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "techReduceComposition", LocalizationHolder.rm.GetString("TechCard.Client_35"), -1, 13, 400, Keys.Multiply | Keys.Control);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (viewServices == null)
      return CommandsInfo.Empty;
    IViewState service1 = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    ViewStateFlags viewStateFlags = service1 != null ? service1.ViewState : ViewStateFlags.None;
    if (items == null || items.Count == 0)
      return CommandsInfo.Empty;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    ISelectedItems checkedItems = ContextCommandHelper.GetCheckedItems(viewServices, items);
    if (checkedItems == null || checkedItems.Count == 0)
      return CommandsInfo.Empty;
    CommandsInfo commandsInfo = new CommandsInfo();
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      commandsInfo.Add("Add", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.AddCommand)));
    if (this._checkInOutManager == null)
    {
      this._checkInOutManager = new StepwiseProviderManager();
      this._checkInOutManager.Providers.Add((IStepwiseCommandsProvider) new TechCardBaseCheckInOutCommandsProvider());
    }
    this._checkInOutManager.CollectCommands(items, viewServices, commandsInfo);
    if ((viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None)
    {
      commandsInfo.Add("Copy", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.CopyCommand)));
      if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
        commandsInfo.Add("Cut", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.CutCommand)));
    }
    if (items.AsItemsList<IDBRelationID>().Count > 0)
      commandsInfo.Add("CopyWithRelAttrs", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.CopyWithRelAttrsCommand)));
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      commandsInfo.Add("Delete", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.DeleteCommand)));
    if (items.Count == 1)
      commandsInfo.Add("SetLifecycleStep", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.SetLifecycleStepCommand)));
    if ((viewStateFlags & ViewStateFlags.InParametersCard) == ViewStateFlags.None && items.Count == 1)
      commandsInfo.Add("ParametersCard", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.ParametersCardCommand)));
    if (items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && !TechCardConsts.Utils.IsTechcardNotInheridObjType((object) itemData.ObjectType) && MetaDataHelper.GetAttribute4ObjectType(itemData.ObjectType, TechCardConsts.AttributeTypes.FileAttrTypeID) == null)
    {
      commandsInfo.Suppress("ViewDocument", 0);
      commandsInfo.Suppress("PrintDocument", 0);
      commandsInfo.Suppress("OpenWith", 0);
      commandsInfo.Suppress("OpenDocument", 0);
      commandsInfo.Suppress("EditDocument", 0);
    }
    if (items != checkedItems && checkedItems.Count > 1 && TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service3)
    {
      foreach (MenuTemplateNode node in service3.ContextMenuTemplateDefault.Nodes)
        this.SuppressNode(node, commandsInfo);
    }
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None && items.Count == 1 && items.GetParentData(0, typeof (IDBObjectID)) is IDBObjectID)
    {
      if (service2 != null)
        commandsInfo.Add("numObjectTemplateNode", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.NumObjectTemplateCommand)));
      commandsInfo.Add("editObjectNode", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.EditObjectCommand)));
      commandsInfo.Add("techProcElemInsertNode", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.TechProcElemInsertCommand)));
      TechcardBaseObjectCommandUtils.MoveCommandsValidate(commandsInfo, items, service2);
    }
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None && items.Count == 1)
      commandsInfo.Add("techProcElemInsertIntoNode", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.TechProcElemInsertIntoCommand)));
    if ((viewStateFlags & ViewStateFlags.NodeInTree) != ViewStateFlags.None && items.Count == 1)
    {
      commandsInfo.Add("techUnwrapComposition", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.UnwrapCompositionCommand)));
      commandsInfo.Add("techReduceComposition", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.ReduceCompositionCommand)));
    }
    return commandsInfo;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="commandsInfo"></param>
  private void SuppressNode(MenuTemplateNode parent, CommandsInfo commandsInfo)
  {
    if (!string.IsNullOrEmpty(parent.Name) && !this._listOfMultiSelectCommand.Contains(parent.Name))
      commandsInfo.Suppress(parent.Name, 4);
    foreach (MenuTemplateNode node in parent.Nodes)
      this.SuppressNode(node, commandsInfo);
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    NavigatorTreeView service1 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service1 == null)
      return CommandsInfo.Empty;
    CommandsInfo commandsInfo = new CommandsInfo();
    IViewState service2 = ServiceUtils.GetService<IViewState>((object) viewServices, false);
    long viewState = service2 != null ? (long) service2.ViewState : 0L;
    bool flag = (viewState & 2L) == 2L;
    if ((viewState & 128L /*0x80*/) == 128L /*0x80*/ && service1.CheckedItems.Count > 1 && TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service3)
    {
      foreach (MenuTemplateNode node in service3.ContextMenuTemplateDefault.Nodes)
        this.SuppressNode(node, commandsInfo);
    }
    if (!flag)
    {
      object dataObject = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false)?.GetDataObject();
      if (dataObject != null)
      {
        if (!(dataObject is IDBObjectTypedIDCollection))
        {
          commandsInfo.Suppress("Paste", 0);
        }
        else
        {
          this.IsPasteCommandAllow(commandsInfo, items, viewServices);
          if (Intermech.TechCard.Client.Commands.Replace.ReplaceCommand.AllowCommand(items, viewServices))
            commandsInfo.Add("TechCard.Replace", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.ReplaceCommand)));
        }
      }
    }
    return commandsInfo;
  }

  /// <summary>Реализация команды Развернуть состав</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void UnwrapCompositionCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(viewServices?.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service))
      return;
    if (service.SelectedItems.Count == 1)
    {
      int navTreeExpandLevel = TechCardParamsHelper.TechParams.Common.NavTreeExpandLevel;
      TechCardBaseObjectContextCommandProvider.ExpandedCurrentNode(service.SelectedNodes[0], navTreeExpandLevel);
    }
    NavigatorTreeNode focusedNode = service.FocusedNode;
    service.FocusedNode = (NavigatorTreeNode) null;
    service.FocusedNode = focusedNode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="currentNode"> узел, состав которого разворачиваем</param>
  /// <param name="expandedLevel">кол-во уровней на которые разворачиваем
  /// если ноль - уходим. если вдруг меньше ноля - то же</param>
  private static void ExpandedCurrentNode(NavigatorTreeNode currentNode, int expandedLevel)
  {
    if (expandedLevel <= 0)
      return;
    currentNode.Expanded = true;
    --expandedLevel;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) currentNode.Children)
      TechCardBaseObjectContextCommandProvider.ExpandedCurrentNode(child, expandedLevel);
  }

  /// <summary>Реализация команды Свернуть состав</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ReduceCompositionCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (!(viewServices?.GetService(typeof (NavigatorTreeView)) is NavigatorTreeView service))
      return;
    if (service.SelectedItems.Count == 1)
      TechCardBaseObjectContextCommandProvider.ReduceCurrentNode(service.SelectedNodes[0]);
    NavigatorTreeNode focusedNode = service.FocusedNode;
    service.FocusedNode = (NavigatorTreeNode) null;
    service.FocusedNode = focusedNode;
  }

  private static void ReduceCurrentNode(NavigatorTreeNode currentNode)
  {
    currentNode.Expanded = false;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) currentNode.Children)
      TechCardBaseObjectContextCommandProvider.ReduceCurrentNode(child);
  }

  /// <summary>Реализация команды На один уровень вверх</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void MoveUpCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || viewServices == null)
      return;
    NavigatorTreeView service1 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service1 == null || service1.SelectedItems.Count != 1)
      return;
    INavigatorTreeViewContextMenuHelper service2 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
    if (service2 != null)
      service2.CanRestoreFocusedNode = false;
    TechcardBaseObjectCommandUtils.MoveCommandsExecute(TechcardBaseObjectCommandUtils.MoveCommandMode.MoveUp, items, service1);
  }

  /// <summary>Реализация команды На один уровень вниз</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void MoveDownCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || viewServices == null)
      return;
    NavigatorTreeView service1 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service1 == null || service1.SelectedItems.Count != 1)
      return;
    INavigatorTreeViewContextMenuHelper service2 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
    if (service2 != null)
      service2.CanRestoreFocusedNode = false;
    TechcardBaseObjectCommandUtils.MoveCommandsExecute(TechcardBaseObjectCommandUtils.MoveCommandMode.Down, items, service1);
  }

  /// <summary>Реализация команды В конец</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void MoveBottomCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (items == null || viewServices == null)
      return;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service2 == null || service2.SelectedItems.Count != 1)
      return;
    INavigatorTreeViewContextMenuHelper service3 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
    if (service3 != null)
      service3.CanRestoreFocusedNode = false;
    TechcardBaseObjectCommandUtils.MoveCommandsExecute(TechcardBaseObjectCommandUtils.MoveCommandMode.Last, items, service2);
  }

  /// <summary>Реализация команды В начало</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void MoveTopCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (items == null || viewServices == null)
      return;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service2 == null || service2.SelectedItems.Count != 1)
      return;
    INavigatorTreeViewContextMenuHelper service3 = ServiceUtils.GetService<INavigatorTreeViewContextMenuHelper>((object) viewServices, false);
    if (service3 != null)
      service3.CanRestoreFocusedNode = false;
    TechcardBaseObjectCommandUtils.MoveCommandsExecute(TechcardBaseObjectCommandUtils.MoveCommandMode.First, items, service2);
  }

  /// <summary>Реализация команды Перенумеровать/Объекты</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void NumObjectTemplateCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (items == null || items.Count != 1 || viewServices == null)
      return;
    NavigatorTreeView service2 = ServiceUtils.GetService<NavigatorTreeView>((object) viewServices, false);
    if (service2 == null || service2.SelectedItems.Count != 1)
      return;
    NavigatorTreeNode selectedNode = service2.SelectedNodes[0];
    if (selectedNode?.Parent == null || !TechcardClientControlsUtils.IsSelectedItemsFromTree(items, service2))
      return;
    TechcardBaseObjectCommandUtils.NumerateCommand(selectedNode, false, service2);
  }

  /// <summary>
  /// Реализация команды "Добавить" "Типовой элемент техпроцесса"
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechProcElemInsertCommand(
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
    if (items == null || items.Count != 1 || !(items.GetParentData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData))
      return;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.CehBaseRouteGUID);
    if (MetaDataHelper.IsObjectTypeChildOf(parentData.ObjectType, objectTypeId))
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_236"));
    }
    else
      TechCardBaseObjectContextCommandProvider.TechProcElemCommandAdd(parentData.ObjectID);
  }

  /// <summary>
  /// Реализация команды "Добавить в состав" "Типовой элемент техпроцесса"
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void TechProcElemInsertIntoCommand(
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
    if (items == null || items.Count != sc_19601.ssp_techcard_19603(535382787) || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    TechCardBaseObjectContextCommandProvider.TechProcElemCommandAdd(itemData.ObjectID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void AddCommand(
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
    AddObjectCommand addObjectCommand = new AddObjectCommand();
    addObjectCommand.Init(items, viewServices, additionalInfo);
    addObjectCommand.Execute();
  }

  /// <summary>Реализация команды "Изменить объект"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void EditObjectCommand(
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
    EditCommand editCommand = new EditCommand();
    editCommand.Init(items, viewServices, additionalInfo);
    editCommand.Execute();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CutCommand(
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
    ObjectCommands.CutCommand(items, viewServices, additionalInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CopyCommand(
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
    ObjectCommands.CopyCommand(items, viewServices, additionalInfo);
    TechCardSelectedItemsCommand.ClearCheckedItems(viewServices);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void CopyWithRelAttrsCommand(
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
    CopyWithRelationAttributesCommand attributesCommand = new CopyWithRelationAttributesCommand();
    attributesCommand.Init(items, viewServices, additionalInfo);
    attributesCommand.Execute();
  }

  /// <summary>Реализация команды "Вставить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void PasteCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (viewServices?.GetService(typeof (INavigatorTreeViewContextMenuHelper)) is INavigatorTreeViewContextMenuHelper service2)
      service2.CanRestoreFocusedNode = false;
    items = Intermech.TechCard.Client.Commands.PasteCommand.GetSelectedItems(items, viewServices);
    Intermech.TechCard.Client.Commands.PasteCommand pasteCommand = new Intermech.TechCard.Client.Commands.PasteCommand();
    pasteCommand.Init(items, viewServices, additionalInfo);
    pasteCommand.Execute();
  }

  /// <summary>Реализация команды "Вставить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void ReplaceCommand(
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
    Intermech.TechCard.Client.Commands.Replace.ReplaceCommand replaceCommand = new Intermech.TechCard.Client.Commands.Replace.ReplaceCommand();
    replaceCommand.Init(items, viewServices, additionalInfo);
    replaceCommand.Execute();
  }

  /// <summary>Реализация команды "Удалить"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void DeleteCommand(
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
    if (items.Count == 0)
      return;
    Intermech.TechCard.Client.Commands.DeleteCommand deleteCommand = new Intermech.TechCard.Client.Commands.DeleteCommand();
    deleteCommand.Init(items, viewServices, additionalInfo);
    deleteCommand.Execute();
  }

  /// <summary>Вызывает диалог изменения шага ЖЦ объекта</summary>
  public static void SetLifecycleStepCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service1.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    long[] objectIDs1 = new long[items.Count];
    List<int> stepsID = new List<int>();
    ObjectSteps[] objectsSteps;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        IDBTypedObjectID itemData1 = items.GetItemData(index2, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        objectIDs1[index2] = itemData1 != null ? itemData1.ObjectID : 0L;
        if (items.GetItemData(index2, typeof (IDBLCStepID)) is IDBLCStepID itemData2 && stepsID.IndexOf(itemData2.LCStepID) < 0)
          stepsID.Add(itemData2.LCStepID);
      }
      objectsSteps = sessionKeeper.Session.GetLifecycleStepCollection(0).GetObjectsSteps(stepsID);
    }
    if (objectsSteps == null)
    {
      int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_213"), items.Count > 1 ? LocalizationHolder.rm.GetString("TechCard.Client_353") : LocalizationHolder.rm.GetString(sc_19601.ssp_techcard_19604()), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    }
    else
    {
      SetObjectsLCStep setObjectsLcStep = new SetObjectsLCStep(objectsSteps);
      if (setObjectsLcStep.ShowDialog() != DialogResult.OK || setObjectsLcStep.StepSelected == -1)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.StartLogHistory();
        try
        {
          sessionKeeper.Session.GetLifecycleStepCollection(0).SetObjectsLCStep(objectIDs1, setObjectsLcStep.StepSelected);
          List<CategoryValue> modificationsHistoryList = sessionKeeper.Session.GetModificationsHistoryList();
          INotificationService service2 = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
          if (service2 == null)
            return;
          List<long> objectIDs2 = new List<long>();
          List<long> objectIDs3 = new List<long>();
          foreach (CategoryValue categoryValue in modificationsHistoryList)
          {
            if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.Delete)
              objectIDs2.Add(categoryValue.CategoryID);
            if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.NextLCStep)
              objectIDs3.Add(categoryValue.CategoryID);
          }
          for (int index3 = objectIDs3.Count - 1; index3 >= 0; --index3)
          {
            if (objectIDs2.Contains(objectIDs3[index3]))
              objectIDs3.RemoveAt(index3);
          }
          if (objectIDs2.Count > 0)
            service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs2));
          if (objectIDs3.Count <= 0)
            return;
          service2.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", (IList<long>) objectIDs3));
        }
        finally
        {
          sessionKeeper.Session.StopLogHistory();
        }
      }
    }
  }

  /// <summary>Вызов диалога c карточкой объекта</summary>
  public static void ParametersCardCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServicesManager.GetService(typeof (IProtectionKey)) as IProtectionKey;
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    ParameterCardCommand parameterCardCommand = new ParameterCardCommand();
    parameterCardCommand.Init(items, viewServices, additionalInfo);
    parameterCardCommand.Execute();
  }

  /// <summary>Скрывать ли команду Вставить для выделенных объектов</summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  private void IsPasteCommandAllow(
    CommandsInfo commandsInfo,
    ISelectedItems items,
    System.IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0)
      return;
    if (Intermech.TechCard.Client.Commands.PasteCommand.AllowCommand(items, viewServices))
      commandsInfo.Add("Paste", new CommandInfo(3, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.PasteCommand)));
    else
      commandsInfo.Suppress("Paste", 0);
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    new TechCardBaseObjectContextCommandProvider().RegisterForAllBaseTypes(factory);
  }
}
