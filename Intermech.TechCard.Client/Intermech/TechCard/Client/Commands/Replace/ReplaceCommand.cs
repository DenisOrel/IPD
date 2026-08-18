// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.Replace.ReplaceCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechAcad.Connector;
using Intermech.TechAcad.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.Services;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands.Replace;

/// <summary>
/// Реализация команды "Заменить" для технологических объектов
/// </summary>
internal class ReplaceCommand : ExtendedSelectedItemsCommand
{
  /// <summary>
  /// Текущее дерево навигатора, в которое происходит команда
  /// </summary>
  private NavigatorTreeView _navigatorTreeView;
  /// <summary>Описание копируемых объектов / связей</summary>
  private List<ClipboardObject> _clipBoardObjects;
  /// <summary>Описание соотв. связей для объектов ЕТП</summary>
  private List<Gtp2EtpRefData> _etpRelInfoList;
  /// <summary>Список удаленных связей</summary>
  private readonly List<long> _removedRelationIds = new List<long>();
  /// <summary>Режим копирования эскизов</summary>
  private bool _copyDraft;

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  private bool ValidateCommandArgs()
  {
    return this.Items != null && this.ContextServices != null && this.Items.Count > 0;
  }

  /// <summary>Проверка допустимости команды для тек. параметров</summary>
  /// <returns></returns>
  private bool AllowCommand()
  {
    if (this._clipBoardObjects == null || this._clipBoardObjects.Count == 0 || this._clipBoardObjects.Count != 1 || this.Items.Count != 1 || !(this.Items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return false;
    ClipboardObject clipBoardObject = this._clipBoardObjects[0];
    return itemData.ObjectType == clipBoardObject.ObjectType && itemData.ObjectID != clipBoardObject.ObjectID;
  }

  /// <summary>Выполнение команды вставки в дереве навигатора</summary>
  private void ProceedCommand()
  {
    if (this.Items == null || this.Items.Count == 0)
      return;
    IDBRelationID itemData1 = this.Items.GetItemData(0, typeof (IDBRelationID)) as IDBRelationID;
    IDBTypedObjectID itemData2 = this.Items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBTypedObjectID parentData = this.Items.GetParentData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    RelObjInfoItem targetRelObjInfo = new RelObjInfoItem((IDBRelation) null)
    {
      PartInfo = itemData2 != null ? new ObjInfoItem(itemData2.ObjectID, itemData2.ObjectType) : (ObjInfoItem) null,
      ProjInfo = parentData != null ? new ObjInfoItem(parentData.ObjectID, parentData.ObjectType) : (ObjInfoItem) null
    };
    if (itemData1 != null)
    {
      targetRelObjInfo.RelationID = itemData1.Value;
      targetRelObjInfo.RelTypeID = itemData1.RelationType;
    }
    this.ProceedCommand(targetRelObjInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetRelObjInfo"></param>
  private void ProceedCommand(RelObjInfoItem targetRelObjInfo)
  {
    if (RelInfoItem.IsEmpty((RelInfoItem) targetRelObjInfo))
      return;
    ObjInfoItem partInfo = targetRelObjInfo.PartInfo;
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) partInfo) || ReplaceCommandDialog.Show(partInfo.ObjectID, out this._copyDraft) != DialogResult.OK || !this.CheckTargetObjectAllowModification(partInfo))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.DoBeforeProceedItems(sessionKeeper.Session);
      try
      {
        this.DoProceedItems(sessionKeeper.Session, targetRelObjInfo);
      }
      finally
      {
        this.DoAfterProceedItems(sessionKeeper.Session);
      }
    }
  }

  /// <summary>Обработка объектов</summary>
  /// <param name="session"></param>
  /// <param name="targetRelObjInfo"></param>
  private void DoProceedItems(IUserSession session, RelObjInfoItem targetRelObjInfo)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if ((TypedInfoItem) targetRelObjInfo == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (targetRelObjInfo));
    IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
    try
    {
      customService?.StartTransaction();
      this.DoProceedItems_Replace(session, targetRelObjInfo);
      customService?.Commit();
    }
    catch (Exception ex)
    {
      customService?.Rollback();
      throw;
    }
  }

  /// <summary>
  /// Проверка возможности создания (вставки) дочерних объектов для родительского объекта,
  /// согласно его состоянию, правам доступа
  /// </summary>
  /// <returns></returns>
  private bool CheckTargetObjectAllowModification(ObjInfoItem targetObjInfo)
  {
    ITechCardObjectCreateAnalyzingService service = ServiceUtils.GetService<ITechCardObjectCreateAnalyzingService>((object) ApplicationServices.Container, false);
    if (service == null)
      return false;
    TechObjectCreatorArgs creatorArgs = new TechObjectCreatorArgs(this._clipBoardObjects.Select<ClipboardObject, int>((System.Func<ClipboardObject, int>) (item => item.ObjectType)).ToArray<int>(), this._clipBoardObjects.Select<ClipboardObject, long>((System.Func<ClipboardObject, long>) (item => item.ObjectID)).ToArray<long>(), (int[]) null, (long[]) null, DateTime.Now, false);
    creatorArgs.RelatedObjectIDs = new long[1]
    {
      targetObjInfo.ObjectID
    };
    creatorArgs.RelationTypeIDs = new int[1]
    {
      TechCardConsts.RelTypes.TechRelationID
    };
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
    DBRecordSetParams paramSet = new DBRecordSetParams(new List<ConditionStructure>()
    {
      new ConditionStructure(-26, RelationalOperators.In, (object) list.ToArray(), LogicalOperators.NONE, 0, false)
    }.ToArray(), new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -20, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -26, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    }.ToArray());
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

  /// <summary>Обработка объектов в режиме "Заменить"</summary>
  /// <param name="session"></param>
  /// <param name="targetRelObjInfo"></param>
  private void DoProceedItems_Replace(IUserSession session, RelObjInfoItem targetRelObjInfo)
  {
    ObjInfoItem partInfo = targetRelObjInfo.PartInfo;
    if (ObjInfoItem.IsEmpty((ITypedInfoItem) targetRelObjInfo.PartInfo))
      return;
    List<IDBTypedObjectID> list1 = this._clipBoardObjects.Select<ClipboardObject, IDBTypedObjectID>((System.Func<ClipboardObject, IDBTypedObjectID>) (item => item.IDBTypedObjectID)).ToList<IDBTypedObjectID>();
    if (list1.Count == 0)
      return;
    IDBObject targetDbObject = session.GetObject(partInfo.ObjectID, false);
    if (targetDbObject == null)
      return;
    IDBTypedObjectID dbTypedObjectId = list1[0];
    IDBObject dbObject = session.GetObjectCollection(dbTypedObjectId.ObjectType).Create(dbTypedObjectId.ObjectID);
    if (dbObject == null)
      return;
    targetDbObject.Attributes.Assign(dbObject.Attributes);
    List<IMSApplicability> typeApplicabilities1 = MetaDataHelper.GetObjectTypeApplicabilities(partInfo.ObjTypeID);
    List<IMSApplicability> list2 = typeApplicabilities1 != null ? typeApplicabilities1.Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.CloneChildRelations)).ToList<IMSApplicability>() : (List<IMSApplicability>) null;
    int[] relations1 = list2 != null ? list2.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.RelationTypeID)).ToArray<int>() : new int[0];
    DataTable childSostavData1 = DataHelper.GetChildSostavData(partInfo, session, (IEnumerable<int>) relations1);
    List<IDBRelation> source1 = new List<IDBRelation>();
    if (childSostavData1 != null)
    {
      int columnIndex1 = childSostavData1.Columns.IndexOf("F_PRJLINK_ID");
      int columnIndex2 = childSostavData1.Columns.IndexOf("F_RELATION_TYPE");
      int columnIndex3 = childSostavData1.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData1.Rows)
      {
        IMSApplicability applicability = MetaDataHelper.GetApplicability(partInfo.ObjTypeID, Convert.ToInt32(row[columnIndex3]), Convert.ToInt32(row[columnIndex2]));
        if (applicability != null && applicability.CloneChildRelations)
        {
          IDBRelation relation = session.GetRelation(Convert.ToInt64(row[columnIndex1]), false);
          if (relation != null)
          {
            relation.Delete(0L);
            source1.Add(relation);
          }
        }
      }
    }
    if (dbObject.IsCreationMode)
    {
      if ((TypedInfoItem) targetRelObjInfo.ProjInfo != (TypedInfoItem) null && targetRelObjInfo.RelTypeID != -1)
      {
        IMSApplicability applicability = MetaDataHelper.GetApplicability(targetRelObjInfo.ProjInfo.ObjTypeID, dbObject.ObjectType, targetRelObjInfo.RelTypeID);
        if (applicability != null && (applicability.ApplicabilityMode == ApplicabilityModes.Required || applicability.ApplicabilityMode == ApplicabilityModes.AnyRequired))
        {
          NewRelationProperties properties = new NewRelationProperties(targetRelObjInfo.RelationID, targetRelObjInfo.ProjInfo.ObjectID, dbObject.ID)
          {
            PartObjectID = dbObject.ObjectID
          };
          session.GetRelationCollection(targetRelObjInfo.RelTypeID).Create(properties);
        }
      }
      dbObject.CommitCreation(true, true);
    }
    List<IMSApplicability> typeApplicabilities2 = MetaDataHelper.GetObjectTypeApplicabilities(dbTypedObjectId.ObjectType);
    List<IMSApplicability> list3 = typeApplicabilities2 != null ? typeApplicabilities2.Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (item => item.CloneChildRelations)).ToList<IMSApplicability>() : (List<IMSApplicability>) null;
    int[] relations2 = list3 != null ? list3.Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (item => item.RelationTypeID)).ToArray<int>() : new int[0];
    DataTable childSostavData2 = DataHelper.GetChildSostavData(new ObjInfoItem(dbObject), session, (IEnumerable<int>) relations2, false);
    List<IDBRelation> source2 = new List<IDBRelation>();
    if (childSostavData2 != null)
    {
      int columnIndex4 = childSostavData2.Columns.IndexOf("F_PRJLINK_ID");
      int columnIndex5 = childSostavData2.Columns.IndexOf("F_RELATION_TYPE");
      int columnIndex6 = childSostavData2.Columns.IndexOf("F_OBJECT_TYPE");
      foreach (DataRow row in (InternalDataCollectionBase) childSostavData2.Rows)
      {
        int int32 = Convert.ToInt32(row[columnIndex6]);
        if (!MetaDataHelper.IsObjectTypeChildOf(int32, TechCardConsts.ObjectTypes.DraftCadmechID))
        {
          IMSApplicability applicability = MetaDataHelper.GetApplicability(partInfo.ObjTypeID, int32, Convert.ToInt32(row[columnIndex5]));
          if (applicability != null && applicability.CloneChildRelations)
          {
            IDBRelation relation = session.GetRelation(Convert.ToInt64(row[columnIndex4]), false);
            if (relation != null)
            {
              relation.ProjID = partInfo.ObjectID;
              source2.Add(relation);
            }
          }
        }
      }
    }
    dbObject.Delete(0L);
    if (dbObject.CheckoutBy != 0L)
      session.GetObject(Math.Abs(dbObject.ObjectID), false)?.Delete(0L);
    if (this._copyDraft)
      ReplaceCommand.DoProceedItems_ReplaceDrafts(session, targetDbObject, session.GetObject(dbTypedObjectId.ObjectID, false));
    this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", targetDbObject.ObjectID));
    if (source1.Count != 0)
      this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) source1.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source1.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source1.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
    if (source2.Count == 0)
      return;
    this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source2.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.RelationID)).ToList<long>(), (IList<long>) source2.Select<IDBRelation, long>((System.Func<IDBRelation, long>) (item => item.ProjID)).ToList<long>(), (IList<int>) null, (IList<int>) source2.Select<IDBRelation, int>((System.Func<IDBRelation, int>) (item => item.RelationType)).ToList<int>()));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="targetDbObject"></param>
  /// <param name="sourceObject"></param>
  /// <param name="acadLoadMode"></param>
  internal static void DoProceedItems_ReplaceDrafts(
    IUserSession session,
    IDBObject targetDbObject,
    IDBObject sourceObject,
    TechAcadLoadMode acadLoadMode = TechAcadLoadMode.Normal)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (targetDbObject == null || sourceObject == null)
      return;
    DockManager service1 = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return;
    NavWindow activeDockControl = service1.ActiveDockControl as NavWindow;
    ITPObject tpObject1 = TechAcadApplication.GetTpObject(new ObjInfoItem(targetDbObject.ObjectID, targetDbObject.ObjectType), activeDockControl);
    ITPObject tpObject2 = TechAcadApplication.GetTpObject(new ObjInfoItem(sourceObject.ObjectID, sourceObject.ObjectType), activeDockControl);
    if ((tpObject1.SketchCollection == null || tpObject1.SketchCollection.Count == 0) && (tpObject2.SketchCollection == null || tpObject2.SketchCollection.Count == 0))
      return;
    IDraftObject Draft;
    if (tpObject1.DraftCollection == null)
    {
      long parentTp = TechCardUtils.GetParentTP(targetDbObject.ObjectID, session);
      if (parentTp == 0L)
        return;
      ITPObject tpObject3 = TechAcadApplication.GetTpObject(new ObjInfoItem(parentTp), activeDockControl);
      if (tpObject3?.DraftCollection == null)
        return;
      Draft = tpObject3.DraftCollection.ItemCount == 0 ? tpObject3.DraftCollection.Add() : tpObject3.DraftCollection.get_Item(0);
    }
    else
      Draft = tpObject1.DraftCollection.ItemCount == 0 ? tpObject1.DraftCollection.Add() : tpObject1.DraftCollection.get_Item(0);
    string str = Draft.Extract(1);
    ITechAcadService service2 = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false);
    if (service2 == null || !service2.LoadAcad(acadLoadMode))
      return;
    if (tpObject1.SketchCollection != null && tpObject1.SketchCollection.ReadOnly == 0)
    {
      List<string> layersList = new List<string>();
      for (int Index = tpObject1.SketchCollection.Count - 1; Index >= 0; --Index)
      {
        ISketchObject sketchObject = tpObject1.SketchCollection.get_Item(Index);
        tpObject1.SketchCollection.Remove(Index);
        layersList.Add(sketchObject.SketchID);
      }
      if (!string.IsNullOrEmpty(str))
      {
        Intermech.TechAcad.Connector.TechAcad.DeleteOper(str, layersList);
        Draft.Save();
      }
    }
    if (tpObject1.DraftCollection == null || tpObject1.SketchCollection == null || tpObject2.SketchCollection == null || tpObject2.SketchCollection.Count == 0)
      return;
    bool flag = false;
    List<Tuple<string, string>> layersList1 = new List<Tuple<string, string>>();
    for (int Index = 0; Index < tpObject2.SketchCollection.Count; ++Index)
    {
      ISketchObject sketchObject1 = tpObject2.SketchCollection.get_Item(Index);
      if (sketchObject1 != null && !string.IsNullOrEmpty(sketchObject1.SketchID))
      {
        layersList1.Clear();
        ISketchObject sketchObject2 = tpObject1.SketchCollection.Add(sketchObject1.Name, Draft, tpObject1);
        layersList1.Add(new Tuple<string, string>(sketchObject1.SketchID, sketchObject2.SketchID));
        string dwgFrom = sketchObject1.DraftObject.Extract(0);
        if (!string.IsNullOrEmpty(dwgFrom) && !string.IsNullOrEmpty(str))
        {
          Intermech.TechAcad.Connector.TechAcad.CopyOperFrom(dwgFrom, str, layersList1);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    Draft.Save();
  }

  /// <summary>Анализ связанных ЕТП объектов</summary>
  /// <returns></returns>
  private bool ValidateEtpObjects()
  {
    this._etpRelInfoList = (List<Gtp2EtpRefData>) null;
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
    if (this._etpRelInfoList == null || this._etpRelInfoList.Count == 0)
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
          this._removedRelationIds.AddRange((IEnumerable<long>) SomeTypedInfoHelper<TypedInfoItem>.GetItemIDs((IEnumerable<TypedInfoItem>) etpRelInfo.ObjRefIDs.Keys));
      }
    }
    TechProcGroupUtils.RemoveEtpObjects(gtp2etpObjList, session);
  }

  /// <summary>Конструктор</summary>
  public ReplaceCommand()
    : base("TechCard.Replace")
  {
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
    this._navigatorTreeView = this.ContextServices?.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
    if (!(ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false).GetDataObject() is IDBObjectTypedIDCollection dataObject))
      return;
    this._clipBoardObjects = ((IEnumerable<IDBTypedObjectID>) dataObject.GetTypedObjects()).Select<IDBTypedObjectID, ClipboardObject>((System.Func<IDBTypedObjectID, ClipboardObject>) (item => item as ClipboardObject)).ToList<ClipboardObject>();
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    if (!this.ValidateCommandArgs() || !this.AllowCommand())
      return;
    this.ProceedCommand();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
    NavigatorTreeView navigatorTreeView = this._navigatorTreeView;
    if (ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false).GetDataObject() as IDBObjectTypedIDCollection is IIOSourceInfo dataObject)
      navigatorTreeView = dataObject.Source as NavigatorTreeView;
    navigatorTreeView?.CheckedNodesClear();
  }

  /// <summary>
  /// Проверка допустимости команды вставить для выбранных объектов
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public static bool AllowCommand(ISelectedItems items, System.IServiceProvider viewServices)
  {
    ReplaceCommand replaceCommand = new ReplaceCommand();
    replaceCommand.Init(items, viewServices, (object) null);
    return replaceCommand.AllowCommand();
  }
}
