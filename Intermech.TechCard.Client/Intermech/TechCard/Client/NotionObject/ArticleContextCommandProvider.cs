// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.NotionObject.ArticleContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.TechRelation;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Protection;
using Intermech.TechCard.Client.Common;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.NotionObject;

/// <summary>
/// Summary description for ArticleContextCommandProvider.
/// </summary>
public class ArticleContextCommandProvider : ICommandsProvider
{
  /// <summary>Конструктор</summary>
  public ArticleContextCommandProvider()
  {
    if (!(TechCardClient.ServiceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    MenuTemplate contextMenuTemplate = service.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      MenuTemplateNode orCreate = TcClientUtils.FindOrCreate(contextMenuTemplate.Nodes, "ArticleAddToDesktop", LocalizationHolder.rm.GetString("TechCard.Client_92"), -1, 13, 10);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "ArticleAddToDesktopOnly", LocalizationHolder.rm.GetString("TechCard.Client_93"), -1, 10, 10);
      TcClientUtils.FindOrCreate(orCreate.Nodes, "ArticleAddToDesktopContext", LocalizationHolder.rm.GetString("TechCard.Client_94"), -1, 10, 10);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Реализация метода интерфейса GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ArticleAddToDesktopOnly", new CommandInfo(0, new ClickEventHandler(ArticleContextCommandProvider.AddToDesktopOnlyCommand)));
    mergedCommands.Add("ArticleAddToDesktopContext", new CommandInfo(0, new ClickEventHandler(ArticleContextCommandProvider.AddToDesktopContextCommand)));
    return mergedCommands;
  }

  /// <summary>Реализация метода интерфейса GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Добавление объекта на рабочий стол</summary>
  /// <param name="folderId"> </param>
  /// <param name="globObjId"></param>
  /// <param name="parObjId"> </param>
  /// <param name="objId"></param>
  public static void AddObjectToDesktop(long folderId, long globObjId, long parObjId, long objId)
  {
    if (folderId == 0L || objId == 0L)
      return;
    List<long> relationIDs = new List<long>();
    List<int> intList = new List<int>();
    List<long> longList = new List<long>();
    List<long> objectIDs = new List<long>();
    List<int> objectTypeIDs = new List<int>();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int relationTypeId1 = MetaDataHelper.GetRelationTypeID(new Guid("cad0005e-306c-11d8-b4e9-00304f19f545"));
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) TechCardConsts.AttributeTypes.ObjectRefAttrID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
        };
        DataTable childSostavData1 = DataHelper.GetChildSostavData(new ObjInfoItem(folderId), sessionKeeper.Session, (IEnumerable<int>) new int[1]
        {
          relationTypeId1
        }, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columns);
        if (globObjId == 0L)
        {
          if (childSostavData1 != null && childSostavData1.Rows.Count != 0)
          {
            int columnIndex = childSostavData1.Columns.IndexOf("F_OBJECT_ID");
            foreach (DataRow row in (InternalDataCollectionBase) childSostavData1.Rows)
            {
              if (Convert.ToInt64(row[columnIndex]) == objId)
                return;
            }
          }
          TechcardClientUtils.StartCreateRelations(folderId, sessionKeeper.Session);
          try
          {
            List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, folderId, new int[1]
            {
              relationTypeId1
            }, new long[1]{ objId }, DateTime.Now, TechCreateRelMode.tcrmBothContainsFirst);
            if (relations == null)
              return;
            foreach (IDBRelation dbRelation in relations)
            {
              relationIDs.Add(dbRelation.RelationID);
              intList.Add(dbRelation.RelationType);
              longList.Add(dbRelation.ProjID);
            }
          }
          finally
          {
            TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
          }
        }
        else
        {
          long num = 0;
          int objectTypeId = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.NotionObjectGUID);
          int relationTypeId2 = MetaDataHelper.GetRelationTypeID(TechCardConsts.RelTypes.NotionRelationGuid);
          if (childSostavData1 != null && childSostavData1.Rows.Count != 0)
          {
            int columnIndex1 = childSostavData1.Columns.IndexOf("F_OBJECT_ID");
            int columnIndex2 = childSostavData1.Columns.IndexOf("F_OBJECT_TYPE");
            int columnIndex3 = childSostavData1.Columns.IndexOf(TechCardConsts.AttributeTypes.ObjectRefAttrGuid.ToString());
            foreach (DataRow row in (InternalDataCollectionBase) childSostavData1.Rows)
            {
              if (row[columnIndex2] != DBNull.Value && Convert.ToInt32(row[columnIndex2]) == objectTypeId && row[columnIndex3] != DBNull.Value && Convert.ToInt64(row[columnIndex3]) == globObjId)
              {
                num = Convert.ToInt64(row[columnIndex1]);
                break;
              }
            }
          }
          if (num == 0L)
          {
            IDBObject dbObject1 = sessionKeeper.Session.GetObject(globObjId);
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
            if (objectCollection != null)
            {
              IDBObject dbObject2 = objectCollection.Create();
              num = dbObject2.ObjectID;
              AttributeValues[] valuesList = new AttributeValues[3]
              {
                new AttributeValues(TechCardConsts.AttributeTypes.NameAttrTypeID, (object) dbObject1.GetAttributeByID(TechCardConsts.AttributeTypes.NameAttrTypeID)?.AsString),
                new AttributeValues(TechCardConsts.AttributeTypes.DesignationAttrTypeID, (object) dbObject1.GetAttributeByID(TechCardConsts.AttributeTypes.DesignationAttrTypeID)?.AsString),
                new AttributeValues(TechCardConsts.AttributeTypes.ObjectRefAttrID, (object) globObjId)
              };
              dbObject2.SetAttributesValues(valuesList);
              TechcardClientUtils.StartCreateRelations(folderId, sessionKeeper.Session);
              try
              {
                List<IDBRelation> relations = TechcardClientUtils.CreateRelations(sessionKeeper.Session, num, new int[1]
                {
                  relationTypeId1
                }, new long[1]{ folderId }, DateTime.Now, TechCreateRelMode.tcrmBothEnterInFirst);
                if (relations == null || relations.Count != 1)
                  return;
                if (dbObject2.IsCreationMode)
                {
                  dbObject2.CommitCreation(false);
                  num = dbObject2.ObjectID;
                }
                objectIDs.Add(num);
                objectTypeIDs.Add(objectTypeId);
                foreach (IDBRelation dbRelation in relations)
                {
                  relationIDs.Add(dbRelation.RelationID);
                  intList.Add(dbRelation.RelationType);
                  longList.Add(dbRelation.ProjID);
                }
              }
              finally
              {
                TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
              }
            }
          }
          else
          {
            ConditionStructure[] conditions = new ConditionStructure[2]
            {
              new ConditionStructure(-2, RelationalOperators.Equal, (object) objId, LogicalOperators.AND, 0, false),
              new ConditionStructure(TechCardConsts.AttributeTypes.ObjectRefAttrID, RelationalOperators.Equal, (object) parObjId, LogicalOperators.NONE, 0, false)
            };
            DataTable childSostavData2 = DataHelper.GetChildSostavData(num, sessionKeeper.Session, (IEnumerable<int>) new int[1]
            {
              relationTypeId2
            }, false, (IEnumerable<ConditionStructure>) conditions);
            if (childSostavData2 != null && childSostavData2.Rows.Count != 0)
              return;
          }
          TechcardClientUtils.StartCreateRelations(num, sessionKeeper.Session);
          List<IDBRelation> relations1;
          try
          {
            relations1 = TechcardClientUtils.CreateRelations(sessionKeeper.Session, num, new int[1]
            {
              relationTypeId2
            }, new long[1]{ objId }, DateTime.Now, TechCreateRelMode.tcrmBothContainsFirst);
          }
          finally
          {
            TechcardClientUtils.StopCreateRelations(sessionKeeper.Session);
          }
          if (relations1 == null)
            return;
          if (relations1.Count == 1)
          {
            AttributeValues[] valuesList = new AttributeValues[1]
            {
              new AttributeValues(TechCardConsts.AttributeTypes.ObjectRefAttrID, (object) parObjId)
            };
            relations1[0].SetAttributesValues(valuesList);
          }
          foreach (IDBRelation dbRelation in relations1)
          {
            relationIDs.Add(dbRelation.RelationID);
            intList.Add(dbRelation.RelationType);
            longList.Add(dbRelation.ProjID);
          }
        }
      }
    }
    finally
    {
      INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
      if (service != null)
      {
        if (objectIDs.Count != 0)
          service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) objectIDs, (IList<int>) objectTypeIDs));
        if (relationIDs.Count != 0)
          service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsManagedEventArgs("ManagedRelationsCreated", (IList<long>) relationIDs, true));
      }
    }
  }

  /// <summary>
  /// Ф-ция получения ID объекта "Рабочий стол" для текущего пользователя
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  public static long GetCurrentUserDesktopID(IUserSession session)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad0004a-306c-11d8-b4e9-00304f19f545"));
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-8, RelationalOperators.Equal, (object) session.UserID, LogicalOperators.AND, 0, false),
      new ConditionStructure(-7, RelationalOperators.Equal, (object) objectTypeId, LogicalOperators.NONE, 0, false)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    DataTable objectData = DataHelper.GetObjectData(objectTypeId, session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<long>) null);
    return objectData == null || objectData.Rows.Count == 0 ? 0L : Convert.ToInt64(objectData.Rows[0]["F_OBJECT_ID"]);
  }

  /// <summary>Реализация ф-ции добавления изделия</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddToDesktopOnlyCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray1 = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray1.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray1;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    long[] numArray2 = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString(sc_19523.ssp_techcard_19524()), LocalizationHolder.rm.GetString("TechCard.Client_96"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(TechCardConsts.ObjectTypes.ArticleBaseID), SelectionOptions.Default);
    if (numArray2 == null || numArray2.Length == 0)
      return;
    foreach (long objId in numArray2)
      ArticleContextCommandProvider.AddObjectToDesktop(itemData.ObjectID, 0L, 0L, objId);
  }

  /// <summary>Реализация ф-ции добавления изделия из заказа</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private static void AddToDesktopContextCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray1 = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray1.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray1;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num1));
    if (!(items.GetItemData(sc_19523.ssp_techcard_19525(213712183), typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00132-306c-11d8-b4e9-00304f19f545"));
    long[] numArray2 = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString(sc_19523.ssp_techcard_19526()), LocalizationHolder.rm.GetString("TechCard.Client_98"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypeId), SelectionOptions.Default);
    if (numArray2 == null || numArray2.Length == 0)
      return;
    long num2 = numArray2[0];
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString(sc_19523.ssp_techcard_19527()), LocalizationHolder.rm.GetString("TechCard.Client_100"), (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(num2), typeof (IDBRelationID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IDBRelationID dbRelationId in objArray)
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(dbRelationId.Value);
        if (relation != null)
          ArticleContextCommandProvider.AddObjectToDesktop(itemData.ObjectID, num2, relation.ProjID, dbRelationId.PartID);
      }
    }
  }
}
