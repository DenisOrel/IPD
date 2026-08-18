// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.PasteCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.CompositionView;
using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechAcad.Interfaces;
using Intermech.TechCard.Client.Commands.Replace;
using Intermech.TechCard.Client.CompositionView;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services;
using Intermech.TechCard.Client.Settings.TechCardParams;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды "Вставить" для технологических объектов
/// </summary>
/// <summary>Конструктор</summary>
internal class PasteCommand(string name = "Paste") : ExtendedSelectedItemsCommand(name)
{
  /// <summary>
  /// Текущее дерево навигатора, в которое происходит вставка
  /// </summary>
  private NavigatorTreeView _navigatorTreeView;
  /// <summary>Описание копируемых объектов / связей</summary>
  protected List<ClipboardObject> _clipBoardObjects;
  /// <summary>Режим "Вырезать"</summary>
  private bool _cutMode;
  /// <summary>
  /// 
  /// </summary>
  private bool _isDragDrop;
  /// <summary>Признак выполнения команды в дереве навигатора</summary>
  private bool _commandInNavTree;
  /// <summary>Список копируемых атрибутов для "головных" объектов</summary>
  private List<int> _copyRelAttrIds;
  /// <summary>
  /// 
  /// </summary>
  private NavigatorTreeNode _navFocusedNode;
  /// <summary>Контекстное меню для режима вставки</summary>
  private ContextMenuStrip _pasteTargetMenu;
  /// <summary>Описание соотв. связей для объектов ЕТП</summary>
  private List<Gtp2EtpRefData> _etpRelInfoList;
  /// <summary>Список удаленных связей</summary>
  private readonly List<long> _removedRelationIDs = new List<long>();
  /// <summary>
  /// Список объектов, для которых возможно копирование эскизов
  /// </summary>
  private Dictionary<ObjInfoItem, ObjInfoItem> _objWithCadmDraftCache;

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  private bool ValidateCommandArgs() => this.Items != null && this.ContextServices != null;

  /// <summary>Создание контекстного меню для режимов вставки</summary>
  /// <returns></returns>
  private ContextMenuStrip GetPasteTargetMenu()
  {
    if (this._pasteTargetMenu != null)
      return this._pasteTargetMenu;
    this._pasteTargetMenu = new ContextMenuStrip();
    this._pasteTargetMenu.SuspendLayout();
    try
    {
      ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
      ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
      this._pasteTargetMenu.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) toolStripMenuItem1,
        (ToolStripItem) toolStripMenuItem2
      });
      this._pasteTargetMenu.Name = "cmsPasteTarget";
      toolStripMenuItem1.Name = "tsmiTargetBefore";
      toolStripMenuItem1.Text = LocalizationHolder.rm.GetString("TechCard.Client_511");
      toolStripMenuItem1.Click += new EventHandler(this.miTargetBefore_Click);
      toolStripMenuItem2.Name = "tsmiTargetAfter";
      toolStripMenuItem2.Text = LocalizationHolder.rm.GetString("TechCard.Client_512");
      toolStripMenuItem2.Click += new EventHandler(this.miTargetAfter_Click);
    }
    finally
    {
      this._pasteTargetMenu.ResumeLayout(false);
    }
    return this._pasteTargetMenu;
  }

  /// <summary>
  /// Получение информации об объекте, в которых будет производиться вставка
  /// скопированных объектов по данной команде
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <param name="method"></param>
  /// <returns></returns>
  private bool FindTargetObjectInfo(out ObjInfoItem targetObjInfo, out CVButtonMethod method)
  {
    targetObjInfo = (ObjInfoItem) null;
    method = CVButtonMethod.InsertInto;
    if (this._clipBoardObjects == null || this._clipBoardObjects.Count == 0)
      return false;
    for (int index = 0; index < Math.Min(1, this.Items.Count); ++index)
    {
      if (this.Items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
      {
        if (this.CheckProjectTypeApplicability(itemData.ObjectType))
        {
          targetObjInfo = new ObjInfoItem(itemData.ObjectID, itemData.ObjectType);
        }
        else
        {
          ObjInfoItem objInfoItem;
          if (this.Items.GetParentData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID parentData2)
            objInfoItem = new ObjInfoItem(parentData2.ObjectID, parentData2.ObjectType);
          else if (this.Items.GetParentData(index, typeof (IDBObjectID)) is IDBObjectID parentData1)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(parentData1.Value);
              if (!objectInfo.Empty)
                objInfoItem = new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID);
              else
                continue;
            }
          }
          else
            continue;
          if (this.CheckProjectTypeApplicability(objInfoItem.ObjTypeID))
          {
            targetObjInfo = objInfoItem;
            method = this._commandInNavTree ? CVButtonMethod.InsertBefore : CVButtonMethod.Add;
          }
        }
      }
    }
    return !ObjInfoItem.IsEmpty((ITypedInfoItem) targetObjInfo);
  }

  /// <summary>
  /// Проверка допустимости вставки скопированный объектов в родительский тип технологической связью
  /// </summary>
  /// <param name="projObjType"></param>
  /// <returns></returns>
  private bool CheckProjectTypeApplicability(int projObjType)
  {
    if (this._clipBoardObjects == null || this._clipBoardObjects.Count == 0)
      return false;
    List<int> childObjectTypesId = MetaDataHelper.GetApplicabilityChildObjectTypesID(projObjType, (IEnumerable<int>) TechCardConsts.RelTypes.TechAllRelationTypes.ToList<int>());
    childObjectTypesId.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) childObjectTypesId));
    childObjectTypesId.Sort();
    foreach (ClipboardObject clipBoardObject in this._clipBoardObjects)
    {
      if (childObjectTypesId.BinarySearch(clipBoardObject.ObjectType) < 0)
        return false;
    }
    return true;
  }

  /// <summary>
  /// Проверка возможности создания (вставки) дочерних объектов для родительского объекта,
  /// согласно его состоянию, правам доступа
  /// </summary>
  /// <returns></returns>
  protected virtual bool CheckTargetObjectAllowModification(ObjInfoItem targetObjInfo)
  {
    ITechCardObjectCreateAnalyzingService service = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    if (service == null)
      return false;
    int num = TechCardConsts.RelTypes.TechAllRelationTypes.First<int>((System.Func<int, bool>) (relTypeId => this._clipBoardObjects.All<ClipboardObject>((System.Func<ClipboardObject, bool>) (objInfo => MetaDataHelper.HasApplicability(targetObjInfo.ObjTypeID, objInfo.ObjectType, relTypeId)))));
    TechObjectCreatorArgs creatorArgs = new TechObjectCreatorArgs(this._clipBoardObjects.Select<ClipboardObject, int>((System.Func<ClipboardObject, int>) (item => item.ObjectType)).ToArray<int>(), this._clipBoardObjects.Select<ClipboardObject, long>((System.Func<ClipboardObject, long>) (item => item.ObjectID)).ToArray<long>(), (int[]) null, (long[]) null, DateTime.Now, false);
    creatorArgs.RelatedObjectIDs = new long[1]
    {
      targetObjInfo.ObjectID
    };
    creatorArgs.RelationTypeIDs = new int[1]{ num };
    if (!service.AllowObjectCreation(creatorArgs, (TechObjectCreatorParams) null))
      return false;
    this.ReloadClipBoardObjectInfo(targetObjInfo, new ObjInfoItem(creatorArgs.RelatedObjectIDs[0], targetObjInfo.ObjTypeID));
    targetObjInfo.ObjectID = creatorArgs.RelatedObjectIDs[0];
    return true;
  }

  /// <summary>Обновление информации о копируемых объектах, связях</summary>
  /// <param name="oldObjInfo"></param>
  /// <param name="newObjInfo"></param>
  private void ReloadClipBoardObjectInfo(ObjInfoItem oldObjInfo, ObjInfoItem newObjInfo)
  {
    if (oldObjInfo.ObjectID == newObjInfo.ObjectID)
      return;
    List<Guid> list = this._clipBoardObjects.Where<ClipboardObject>((System.Func<ClipboardObject, bool>) (item => item.ProjID == oldObjInfo.ObjectID)).Select<ClipboardObject, Guid>((System.Func<ClipboardObject, Guid>) (item => item.RelGuid)).ToList<Guid>();
    if (list.Count == 0)
      return;
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -20, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -26, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-26, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false)
    }, columns);
    DataTable source;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
      relationCollection.LocalTypesMode = true;
      source = relationCollection.ConsistFrom(paramSet, newObjInfo.ObjectID);
    }
    if (source == null || source.Rows.Count == 0)
      return;
    int idxFldPrjLink = source.Columns.IndexOf("F_PRJLINK_ID");
    int idxFldPrjGuid = source.Columns.IndexOf("F_PRJ_GUID");
    List<ClipboardObject> clipboardObjectList = new List<ClipboardObject>(this._clipBoardObjects.Count);
    Dictionary<Guid, long> dictionary = source.AsEnumerable().ToDictionary<DataRow, Guid, long>((System.Func<DataRow, Guid>) (row => new Guid(row[idxFldPrjGuid].ToString())), (System.Func<DataRow, long>) (row => Convert.ToInt64(row[idxFldPrjLink])));
    foreach (ClipboardObject clipBoardObject in this._clipBoardObjects)
    {
      long relationID;
      if (dictionary.TryGetValue(clipBoardObject.IDBRelationID.RelGuid, out relationID))
      {
        if (clipBoardObject.IDBRelationID.Value == relationID)
          clipboardObjectList.Add(clipBoardObject);
        else
          clipboardObjectList.Add(new ClipboardObject(clipBoardObject.IDBTypedObjectID, (IDBRelationID) new DBRelationID(relationID, clipBoardObject.IDBRelationID.PartID, clipBoardObject.IDBRelationID.RelationType, clipBoardObject.IDBRelationID.Sorting, clipBoardObject.IDBRelationID.RelGuid, clipBoardObject.IDBRelationID.ProjID)));
      }
    }
    this._clipBoardObjects = clipboardObjectList;
  }

  /// <summary>Выполнение команды вставки в дереве навигатора</summary>
  private void ProceedCommandInNavigatorTree()
  {
    ObjInfoItem targetObjInfo;
    CVButtonMethod method;
    if (this._navigatorTreeView == null || this._clipBoardObjects == null || this._clipBoardObjects.Count == 0 || !this.FindTargetObjectInfo(out targetObjInfo, out method))
      return;
    if (method == CVButtonMethod.InsertAfter || method == CVButtonMethod.InsertBefore)
    {
      switch (TechCardParamsHelper.TechParams.Common.PasteCommandMode)
      {
        case Intermech.Interfaces.TechCard.TechCardParams.NavigatorPasteMode.Before:
          method = CVButtonMethod.InsertBefore;
          break;
        case Intermech.Interfaces.TechCard.TechCardParams.NavigatorPasteMode.After:
          method = CVButtonMethod.InsertAfter;
          break;
        case Intermech.Interfaces.TechCard.TechCardParams.NavigatorPasteMode.ShowMenu:
          ContextMenuStrip pasteTargetMenu = this.GetPasteTargetMenu();
          pasteTargetMenu.Tag = (object) targetObjInfo;
          pasteTargetMenu.Show(Cursor.Position);
          return;
      }
    }
    this.ProceedCommand(targetObjInfo, method);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetObjInfo"></param>
  /// <param name="method"></param>
  private void ProceedCommand(ObjInfoItem targetObjInfo, CVButtonMethod method)
  {
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) targetObjInfo) || !this.ValidateEtpObjects() || !this.CheckTargetObjectAllowModification(targetObjInfo))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.DoBeforeProceedItems(sessionKeeper.Session);
      try
      {
        this.DoProceedItems(sessionKeeper.Session, targetObjInfo, method);
      }
      finally
      {
        this.DoAfterProceedItems(sessionKeeper.Session);
      }
    }
    this.DoProceedItems_CopyDrafts();
  }

  /// <summary>Копирование эскизов Cadmech-T</summary>
  private void DoProceedItems_CopyDrafts()
  {
    if (this._objWithCadmDraftCache == null || this._objWithCadmDraftCache.Count == 0 || MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_525"), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (KeyValuePair<ObjInfoItem, ObjInfoItem> keyValuePair in this._objWithCadmDraftCache)
      {
        IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(keyValuePair.Key.ObjectID, false);
        IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(keyValuePair.Value.ObjectID, false);
        if (objectActualCopy1 != null && objectActualCopy2 != null)
          ReplaceCommand.DoProceedItems_ReplaceDrafts(sessionKeeper.Session, objectActualCopy1, objectActualCopy2, TechAcadLoadMode.Silent);
      }
    }
  }

  /// <summary>Анализ связанных ЕТП объектов</summary>
  /// <returns></returns>
  private bool ValidateEtpObjects()
  {
    this._etpRelInfoList = (List<Gtp2EtpRefData>) null;
    if (!this._cutMode)
      return true;
    List<RelInfoItem> list = this._clipBoardObjects.Select<ClipboardObject, RelInfoItem>((System.Func<ClipboardObject, RelInfoItem>) (item => new RelInfoItem(item.Value, item.RelationType))).ToList<RelInfoItem>();
    GenericListHelper.MakeUnique<RelInfoItem>(list);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._etpRelInfoList = TechProcGroupUtils.GetEtpRelIDList(list, sessionKeeper.Session);
    return this._etpRelInfoList == null || this._etpRelInfoList.Count == 0 || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_419"), (object) this._etpRelInfoList.Count), LocalizationHolder.rm.GetString("TechCard.Client_213"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
  }

  /// <summary>Обновление данных ЕТП объектов</summary>
  /// <param name="session"></param>
  private void UpdateEtpObjects(IUserSession session)
  {
    if (!this._cutMode || this._etpRelInfoList == null || this._etpRelInfoList.Count == 0)
      return;
    Dictionary<long, IDBTypedObjectID> dictionary = new Dictionary<long, IDBTypedObjectID>();
    foreach (ClipboardObject clipBoardObject in this._clipBoardObjects)
    {
      if (!dictionary.ContainsKey(clipBoardObject.Value))
        dictionary.Add(clipBoardObject.Value, (IDBTypedObjectID) new DBTypedObjectID((IDBTypedObjectID) clipBoardObject));
    }
    List<Gtp2EtpRefObjData> gtp2etpObjList = new List<Gtp2EtpRefObjData>(this._etpRelInfoList.Count);
    foreach (Gtp2EtpRefData etpRelInfo in this._etpRelInfoList)
    {
      IDBTypedObjectID dbTypedObjectId;
      if (dictionary.TryGetValue(etpRelInfo.ItemInfo.ItemID, out dbTypedObjectId))
      {
        gtp2etpObjList.Add(new Gtp2EtpRefObjData(etpRelInfo, new TechCardUtils.SostavTreeItem(0L, dbTypedObjectId.ObjectID, 0L, -1, dbTypedObjectId.ObjectType)));
        if (etpRelInfo.ObjRefIDs != null)
          this._removedRelationIDs.AddRange((IEnumerable<long>) SomeTypedInfoHelper<TypedInfoItem>.GetItemIDs((IEnumerable<TypedInfoItem>) etpRelInfo.ObjRefIDs.Keys));
      }
    }
    TechProcGroupUtils.RemoveEtpObjects(gtp2etpObjList, session);
  }

  /// <summary>Обработка объектов</summary>
  /// <param name="session"></param>
  /// <param name="targetObjInfo"></param>
  /// <param name="method"></param>
  private void DoProceedItems(
    IUserSession session,
    ObjInfoItem targetObjInfo,
    CVButtonMethod method)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if ((TypedInfoItem) targetObjInfo == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (targetObjInfo));
    IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
    try
    {
      customService?.StartTransaction();
      bool cutMode = this._cutMode;
      if (cutMode)
      {
        if (cutMode)
          this.DoProceedItems_Cut(session, targetObjInfo, method);
      }
      else
        this.DoProceedItems_Copy(session, targetObjInfo, method);
      this.UpdateEtpObjects(session);
      customService?.Commit();
    }
    catch (Exception ex)
    {
      customService?.Rollback();
      throw;
    }
  }

  /// <summary>Обработка объектов в режиме "Копировать"</summary>
  /// <param name="session"></param>
  /// <param name="targetObjInfo"></param>
  /// <param name="method"></param>
  private void DoProceedItems_Copy(
    IUserSession session,
    ObjInfoItem targetObjInfo,
    CVButtonMethod method)
  {
    List<IDBTypedObjectID> list = this._clipBoardObjects.Select<ClipboardObject, IDBTypedObjectID>((System.Func<ClipboardObject, IDBTypedObjectID>) (item => (IDBTypedObjectID) item)).ToList<IDBTypedObjectID>();
    if (this._commandInNavTree)
    {
      CVLocalButton.CVButtonClickArgs args = new CVLocalButton.CVButtonClickArgs(method, this._navigatorTreeView, list);
      args.TargetTreeNode = this._navFocusedNode;
      args.CopyRelAttrs = this._copyRelAttrIds;
      CVLocalButton.Click((CVButtonBase) new cvTechCopyButton(), args);
    }
    else
      CVLocalButton.Click((CVButtonBase) new cvTechCopyButton(), method, this.Items, list, session);
  }

  /// <summary>Обработка объектов в режиме "Вырезать"</summary>
  /// <param name="session"></param>
  /// <param name="targetObjInfo"></param>
  /// <param name="method"></param>
  private void DoProceedItems_Cut(
    IUserSession session,
    ObjInfoItem targetObjInfo,
    CVButtonMethod method)
  {
    List<IDBTypedObjectID> list = this._clipBoardObjects.Select<ClipboardObject, IDBTypedObjectID>((System.Func<ClipboardObject, IDBTypedObjectID>) (item => item.IDBTypedObjectID)).ToList<IDBTypedObjectID>();
    List<RelObjInfoItem> relObjInfoItems = new List<RelObjInfoItem>();
    foreach (ClipboardObject clipBoardObject in this._clipBoardObjects)
      relObjInfoItems.Add(new RelObjInfoItem(clipBoardObject.Value, clipBoardObject.RelationType)
      {
        PartInfo = new ObjInfoItem(clipBoardObject.IDBTypedObjectID.ObjectID, clipBoardObject.IDBTypedObjectID.ObjectType),
        ProjInfo = new ObjInfoItem(clipBoardObject.ProjID)
      });
    if (this._commandInNavTree)
    {
      CVLocalButton.CVButtonClickArgs cvButtonClickArgs = new CVLocalButton.CVButtonClickArgs(method, this._navigatorTreeView, list);
      cvButtonClickArgs.TargetTreeNode = this._navFocusedNode;
      CVLocalButton.CVButtonClickArgs args = cvButtonClickArgs;
      CVLocalButton.Click((CVButtonBase) new CVTechCutButton(relObjInfoItems), args);
    }
    else
      CVLocalButton.Click((CVButtonBase) new CVTechCutButton(relObjInfoItems), method, this.Items, list, session);
  }

  /// <summary>Инициализация команды</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public override void Init(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    base.Init(items, viewServices, additionalInfo);
    this._commandInNavTree = (this.ContextServices != null ? this.ContextServices.GetService(typeof (INavigatorTreeViewContextMenuHelper)) as INavigatorTreeViewContextMenuHelper : (INavigatorTreeViewContextMenuHelper) null) != null;
    this._navigatorTreeView = this.ContextServices?.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
    if (this._commandInNavTree)
    {
      this._isDragDrop = this._navigatorTreeView.IsDragDrop;
      this._navFocusedNode = this._isDragDrop ? this._navigatorTreeView.DragDropLastDestNode : this._navigatorTreeView.FocusedNode;
    }
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
    if (service.GetDataObject() is IDBObjectTypedIDCollection dataObject1)
      this._clipBoardObjects = ((IEnumerable<IDBTypedObjectID>) dataObject1.GetTypedObjects()).Select<IDBTypedObjectID, ClipboardObject>((System.Func<IDBTypedObjectID, ClipboardObject>) (item => item as ClipboardObject)).ToList<ClipboardObject>();
    this._cutMode = false;
    object dataObject2 = service.GetDataObject();
    if (dataObject2 is DBObjectTypedIDCollection)
      this._cutMode = dataObject2 is ICutCopy cutCopy && cutCopy.IsCut;
    if (!(dataObject2 is CopyWithRelationAttributesCommand.CopyWithAttrClipboardObjectsList clipboardObjectsList))
      return;
    this._copyRelAttrIds = clipboardObjectsList.CopyRelAttrs;
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.ValidateCommandArgs())
      return;
    if (!this._commandInNavTree && this.Items.Count == 0)
      Intermech.Navigator.ContextCommands.ObjectCommands.PasteCommand(this.Items, this.ContextServices, this.AdditionalInfo);
    else
      this.ProceedCommandInNavigatorTree();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoBeforeProceedItems(IUserSession session)
  {
    base.DoBeforeProceedItems(session);
    if (this._isDragDrop)
    {
      DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
      if (service == null)
        return;
      foreach (DockControl dockControl in service.GetDockControls())
      {
        if (dockControl is NavWindow navWindow && navWindow.TreeView == this._navigatorTreeView)
        {
          navWindow.Activate();
          break;
        }
      }
    }
    session.RemoveSessionPluginsData((object) TechCardConsts.Params.ObjWithCadmechDraft2CopyPluginData);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
    this._objWithCadmDraftCache = session.GetSessionPluginsData((object) TechCardConsts.Params.ObjWithCadmechDraft2CopyPluginData) as Dictionary<ObjInfoItem, ObjInfoItem>;
    if (this._cutMode)
    {
      foreach (object dataObject in ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false).GetDataObjects())
      {
        if (dataObject is ICutCopy cutCopy)
          cutCopy.IsCut = false;
      }
      List<long> list = this._clipBoardObjects.Select<ClipboardObject, long>((System.Func<ClipboardObject, long>) (clipboardObj => clipboardObj.Value)).ToList<long>();
      INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        this._removedRelationIDs.AddRange((IEnumerable<long>) list);
        service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) this._removedRelationIDs));
        service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) list));
      }
    }
    else if (this._isDragDrop)
      this._navigatorTreeView.CheckedNodesClear();
    NavigatorTreeView navigatorTreeView = this._navigatorTreeView;
    if (ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false).GetDataObject() as IDBObjectTypedIDCollection is IIOSourceInfo dataObject1 && dataObject1.Source != null && dataObject1.Source is NavigatorTreeView source)
      navigatorTreeView = source;
    navigatorTreeView?.CheckedNodesClear();
  }

  /// <summary>
  /// Получение "корректного" списка элементов для вызова команды
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public static ISelectedItems GetSelectedItems(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return TechCardCommandUtils.GetFocusedItems(items, viewServices);
  }

  /// <summary>
  /// Проверка допустимости команды вставить для выбранных объектов
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public static bool AllowCommand(ISelectedItems items, System.IServiceProvider viewServices)
  {
    items = PasteCommand.GetSelectedItems(items, viewServices);
    PasteCommand pasteCommand = new PasteCommand();
    pasteCommand.Init(items, viewServices, (object) null);
    return pasteCommand.FindTargetObjectInfo(out ObjInfoItem _, out CVButtonMethod _);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miTargetAfter_Click(object sender, EventArgs e)
  {
    this.ProceedCommand(this.GetPasteTargetMenu().Tag as ObjInfoItem, CVButtonMethod.InsertAfter);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void miTargetBefore_Click(object sender, EventArgs e)
  {
    this.ProceedCommand(this.GetPasteTargetMenu().Tag as ObjInfoItem, CVButtonMethod.InsertBefore);
  }
}
