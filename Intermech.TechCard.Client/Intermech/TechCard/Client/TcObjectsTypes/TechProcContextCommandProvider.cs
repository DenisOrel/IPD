// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>
/// Класс реализующий команды контекстного меню для объектов типа "Техпроцесс"
/// </summary>
public class TechProcContextCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public TechProcContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "AddCehFromRouteToTechProc", Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_268"), -1, 13, 92);
      TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "LinkCehRouteToTechProc", Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_269"), -1, 13, 96 /*0x60*/);
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
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || (viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("AddCehFromRouteToTechProc", new CommandInfo(0, new ClickEventHandler(TechProcContextCommandProvider.AddCehFromRouteToTechProcCommand)));
    mergedCommands.Add("LinkCehRouteToTechProc", new CommandInfo(0, new ClickEventHandler(TechProcContextCommandProvider.LinkCehRouteToTechProcCommand)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Реализация команды "Добавить цехозаход"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void AddCehFromRouteToTechProcCommand(
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
      throw new ProtectionException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    CehRouteElementList routeElemList = new CehRouteElementList((CustomTechClass) null);
    TechProcClass techProcessObj;
    CehRouteClass cehRoutesObj;
    if (!TechProcElemRouteDlg.ShowDialog(itemData.ObjectID, out techProcessObj, out cehRoutesObj, ref routeElemList) || routeElemList == null || routeElemList.Count == 0)
      return;
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      TechObjectCreationMode objectCreationMode = TechObjectCreationMode.Default;
      IImbaseTechObjInfoService service2 = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      if (service2 == null)
      {
        string caption = Intermech.Localization.LocalizationHolder.rm.GetString(sc_19665.ssp_techcard_19666());
        int num2 = (int) MessageBox.Show(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString(sc_19665.ssp_techcard_19667()), (object) typeof (IImbaseObjInfoService)), caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      List<int> objTypeIds;
      service2.GetCreationTypes(sessionKeeper.Session.SessionGUID, out objTypeIds);
      if (objTypeIds != null && objTypeIds.Contains(TechCardConsts.ObjectTypes.CehZahodObjectID))
        objectCreationMode = TechObjectCreationMode.Imbase;
      List<CehRouteElementClass> routeElementClassList = new List<CehRouteElementClass>();
      foreach (CehRouteElementContainer template in cehRoutesObj.TemplateList)
      {
        foreach (CehRouteElementClass routeElement in (CustomTechClassList<CehRouteElementClass>) template.RouteElementList)
          routeElementClassList.Add(routeElement);
      }
      Dictionary<IDBObject, CehRouteElementClass> dictionary = new Dictionary<IDBObject, CehRouteElementClass>();
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(TechCardConsts.ObjectTypes.CehZahodObjectID);
      List<int> attrIds;
      TechCardConsts.Utils.GetCommonObjTypeAttrs(TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.ObjectTypes.CehZahodObjectID, false, out attrIds);
      if (attrIds == null)
        attrIds = new List<int>();
      if (!attrIds.Contains(TechCardConsts.AttributeTypes.CehRouteAttrID))
        attrIds.Add(TechCardConsts.AttributeTypes.CehRouteAttrID);
      foreach (CehRouteElementClass routeElementClass in (CustomTechClassList<CehRouteElementClass>) routeElemList)
      {
        if (routeElementClass.CehAttrID != 0L)
        {
          IDBObject dbObject = (IDBObject) null;
          switch (objectCreationMode)
          {
            case TechObjectCreationMode.Imbase:
              long objectID = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true).CreateObject(sessionKeeper.Session.SessionGUID, 0L, routeElementClass.CehAttrID, 0L, false, TechCardConsts.ObjectTypes.CehZahodObjectID);
              switch (objectID)
              {
                case -1:
                case 0:
                  continue;
                default:
                  dbObject = sessionKeeper.Session.GetObject(objectID);
                  break;
              }
              break;
            case TechObjectCreationMode.Default:
              if (sessionKeeper.Session.GetObject(routeElementClass.CehAttrID, false) != null)
              {
                dbObject = objectCollection.Create();
                if (dbObject == null)
                  continue;
                break;
              }
              continue;
          }
          TechCardUtils.CopyObjectAttributes(session.GetObject(routeElementClass.ObjectId, false), dbObject, attrIds.ToArray(), false);
          if (dbObject != null)
            dictionary.Add(dbObject, routeElementClass);
        }
      }
      List<long> longList = new List<long>(routeElemList.Count);
      foreach (CehRouteElementClass routeElementClass in (CustomTechClassList<CehRouteElementClass>) routeElemList)
        longList.Add(routeElementClass.ObjectId);
      TechcardClientUtils.StartCreateRelations((IEnumerable<long>) longList.ToArray(), sessionKeeper.Session);
      try
      {
        foreach (KeyValuePair<IDBObject, CehRouteElementClass> keyValuePair in dictionary)
        {
          IDBObject key = keyValuePair.Key;
          CehRouteElementClass routeElementClass = keyValuePair.Value;
          List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, key.ObjectID, new int[1]
          {
            TechCardConsts.RelTypes.TechRouteRelationID
          }, new long[1]{ routeElementClass.ObjectId }, DateTime.Now, TechCreateRelMode.tcrmBothEnterInFirst);
          if (relations != null && relations.Count > 0)
          {
            AttributeValues[] valuesList = new AttributeValues[1];
            IDBRelation relation = sessionKeeper.Session.GetRelation(routeElementClass.LinkID);
            int attributeID = TechCardConsts.Utils.AttributeTypeByGuid(TechCardConsts.AttributeTypes.ElemRouteLinkAttrGuid, sessionKeeper.Session);
            valuesList[0] = new AttributeValues(attributeID, (object) ((IDBGuid) relation).GUID);
            relations[0].SetAttributesValues(valuesList);
          }
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
      List<IDBRelation> dbRelationList = new List<IDBRelation>();
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID);
      TechcardClientUtils.StartCreateRelations(itemData.ObjectID, sessionKeeper.Session);
      try
      {
        foreach (KeyValuePair<IDBObject, CehRouteElementClass> keyValuePair in dictionary)
        {
          IDBObject dbObject = keyValuePair.Key;
          NewRelationProperties empty = NewRelationProperties.Empty;
          object initValue = (object) null;
          int num3 = routeElementClassList.IndexOf(keyValuePair.Value);
          if (num3 != -1)
          {
            int index2 = -1;
            CehTechClass cehTechClass = new CehTechClass(dbObject.ObjectID, 0L);
            cehTechClass.AttrLinkGuid = keyValuePair.Value.LinkGuid;
            for (int index3 = num3 + 1; index3 < routeElementClassList.Count; ++index3)
            {
              index2 = techProcessObj.CehTechList.GetIndexByAttrLink(routeElementClassList[index3].LinkGuid);
              if (index2 != -1)
                break;
            }
            if (index2 != -1)
            {
              initValue = (object) (index2 > 0 ? (techProcessObj.CehTechList[index2 - 1].OrderID + techProcessObj.CehTechList[index2].OrderID) / 2L : techProcessObj.CehTechList[index2].OrderID - 1000000L);
              techProcessObj.CehTechList.Insert(index2, cehTechClass);
            }
            else
            {
              initValue = (object) (techProcessObj.CehTechList.Count > 0 ? techProcessObj.CehTechList[techProcessObj.CehTechList.Count - 1].OrderID + 1000000L : 500000000L);
              techProcessObj.CehTechList.Add(cehTechClass);
            }
            cehTechClass.OrderID = Convert.ToInt64(initValue);
          }
          if (initValue != null)
            empty.ValuesList = new AttributeValues[1]
            {
              new AttributeValues(TechCardConsts.AttributeTypes.SortAttrTypeID, initValue)
            };
          IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) TechcardClientUtils.GetDBTypedObjectID(dbObject);
          dbRelationList.Clear();
          dbRelationList.Add(TechcardClientUtils.CreateRelation(relationCollection, itemData, dbTypedObjectId, empty));
          if (dbRelationList.Count != 0)
          {
            if (dbObject.IsCreationMode)
            {
              dbObject.CommitCreation(false);
              if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout || dbObject.CheckoutBy == 0L)
                dbObject = dbObject.CheckOut(true);
            }
            foreach (IDBRelation dbRel in dbRelationList)
              source.Add(new RelObjInfoItem(dbRel)
              {
                ProjInfo = new ObjInfoItem(itemData.ObjectID, itemData.ObjectType),
                PartInfo = new ObjInfoItem(dbObject)
              });
          }
        }
      }
      finally
      {
        TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
      }
      if (source.Count != 0)
      {
        IAutoSelectionService service3 = ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false);
        if (service3 != null)
        {
          List<RelObjInfoItem> collection = new List<RelObjInfoItem>();
          foreach (RelObjInfoItem relObjInfoItem in source)
            collection.AddRange((IEnumerable<RelObjInfoItem>) service3.ExecuteSelection(new AutoSelectionParams(relObjInfoItem.PartInfo.ObjectID, relObjInfoItem.RelationID, AutoSelectionMode.AutoObject)));
          source.AddRange((IEnumerable<RelObjInfoItem>) collection);
        }
      }
    }
    if (!(TechCardClient.ServiceProvider.GetService(typeof (INotificationService)) is INotificationService service4))
      return;
    List<ObjInfoItem> list = source.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.PartInfo != (TypedInfoItem) null)).Select<RelObjInfoItem, ObjInfoItem>((Func<RelObjInfoItem, ObjInfoItem>) (item => item.PartInfo)).ToList<ObjInfoItem>();
    service4.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) list.Select<ObjInfoItem, long>((Func<ObjInfoItem, long>) (item => item.ObjectID)).ToArray<long>(), (IList<int>) list.Select<ObjInfoItem, int>((Func<ObjInfoItem, int>) (item => item.ObjTypeID)).ToArray<int>()));
    service4.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => !((TypedInfoItem) item.ProjInfo != (TypedInfoItem) null) ? 0L : item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => !((TypedInfoItem) item.ProjInfo != (TypedInfoItem) null) ? -1 : item.ProjInfo.ObjTypeID)).ToArray<int>(), (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
  }

  /// <summary>Реализация команды "Привязка к расцеховке"</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void LinkCehRouteToTechProcCommand(
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
      throw new ProtectionException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.ConsistFromType, (object) TechCardConsts.ObjectTypes.ProcRoutingID, (object) null, LogicalOperators.NONE, 0, false)
      };
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(itemData.ObjectID, sessionKeeper.Session, new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, conditions);
      long moObjId = 0;
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
      {
        if (sostavTreeItem != null && MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem.ObjectTypeID, TechCardConsts.ObjectTypes.ProcRoutingID))
        {
          moObjId = sostavTreeItem.ProjID;
          break;
        }
      }
      if (moObjId == 0L)
        return;
      CehRoute2TpObjDlg.ShowDialog(moObjId);
    }
  }
}
