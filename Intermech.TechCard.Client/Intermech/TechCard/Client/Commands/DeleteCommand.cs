// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.DeleteCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Commands;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Interfaces.TechCard.TechNumeration;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// Реализация команды "Удалить" контекстного меню навигатора для технологических объектов
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="name">Имя команды</param>
internal class DeleteCommand(string name = "Delete") : DeleteItemsCommand(name)
{
  /// <summary>Информация о контексте удаляемых объектов</summary>
  private readonly List<SortedRelObjInfoItem> _relationInfo2Delete = new List<SortedRelObjInfoItem>();

  /// <summary>
  /// 
  /// </summary>
  private void ExcludeUniqueObjects()
  {
    if (this._deletingObjects == null || !this._deletingObjects.Any<DeletingObject>((System.Func<DeletingObject, bool>) (item => item.PrjLinkIDs.Count > 0)))
      return;
    DeletingObjects deletingObjects1 = new DeletingObjects();
    deletingObjects1.AddRange((IEnumerable<DeletingObject>) this._deletingObjects);
    DeletingObjects deletingObjects2 = new DeletingObjects();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<long, int> objects = new Dictionary<long, int>();
      Dictionary<long, ImbaseObjCreateInfo> objCreateInfo = new Dictionary<long, ImbaseObjCreateInfo>();
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      if (service != null)
      {
        foreach (DeletingObject deletingObject in (List<DeletingObject>) deletingObjects1)
        {
          long objectId = deletingObject.ObjectID;
          int objectType = deletingObject.ObjectType;
          if (objectId != 0L && !objects.ContainsKey(objectId))
            objects.Add(objectId, objectType);
        }
        if (objects.Count > 0)
          service.GetCreationMode((IDictionary<long, int>) objects, sessionKeeper.Session.SessionGUID, out objCreateInfo);
      }
      foreach (DeletingObject deletingObject in (List<DeletingObject>) deletingObjects1)
      {
        ImbaseObjCreateInfo imbaseObjCreateInfo;
        objCreateInfo.TryGetValue(deletingObject.ObjectID, out imbaseObjCreateInfo);
        if (imbaseObjCreateInfo.CreateMode == ImbaseObjCreateMode.iocmUseExists)
          deletingObjects2.Add(deletingObject);
      }
      foreach (DeletingObject deletingObject in (List<DeletingObject>) deletingObjects2)
        deletingObject.ObjectID = 0L;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ReNumerateObjects(DeleteObjectsJobStatus jobStatus)
  {
    if (jobStatus == null || jobStatus.Objects == 0 || jobStatus.RelationsCount == 0)
      return;
    List<SortedRelObjInfoItem> list = this._relationInfo2Delete.Where<SortedRelObjInfoItem>((System.Func<SortedRelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo != (TypedInfoItem) null && !jobStatus.Items.Contains(item.ProjInfo.ObjectID) && jobStatus.Relations.Contains(item.RelationID))).ToList<SortedRelObjInfoItem>();
    if (list.Count == 0)
      return;
    IEnumerable<Tuple<int, int>> tuples1 = list.Select<SortedRelObjInfoItem, Tuple<int, int>>((System.Func<SortedRelObjInfoItem, Tuple<int, int>>) (item => new Tuple<int, int>(item.ProjInfo.ObjTypeID, item.PartInfo.ObjTypeID))).Distinct<Tuple<int, int>>();
    List<Tuple<int, int>> source1 = new List<Tuple<int, int>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechNumerationService service = ServiceUtils.GetService<ITechNumerationService>((object) sessionKeeper.Session, true);
      foreach (Tuple<int, int> tuple in tuples1)
      {
        ITechNumerationRule numRule;
        if (service.GetNumerationRule(tuple.Item2, tuple.Item1, sessionKeeper.Session.SessionGUID, out numRule, out ITechNumerationNode _) && numRule.NumerationMethod == TechNumerationMethods.Auto && numRule.RenumOnDelete)
          source1.Add(tuple);
      }
    }
    if (source1.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechNumerationService service1 = ServiceUtils.GetService<ITechNumerationService>((object) sessionKeeper.Session, true);
      ICompositionLoadService service2 = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
      ColumnDescriptor[] columns = new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) "cad00202-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
      };
      foreach (int num in source1.Select<Tuple<int, int>, int>((System.Func<Tuple<int, int>, int>) (item => item.Item1)).Distinct<int>())
      {
        int projTypeId = num;
        SortedRelObjInfoItem[] array = list.Where<SortedRelObjInfoItem>((System.Func<SortedRelObjInfoItem, bool>) (item => item.ProjInfo.ObjTypeID == projTypeId)).ToArray<SortedRelObjInfoItem>();
        DataTable source2 = service2.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, (IEnumerable<ObjInfoItem>) ((IEnumerable<SortedRelObjInfoItem>) array).Select<SortedRelObjInfoItem, ObjInfoItem>((System.Func<SortedRelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).ToArray<ObjInfoItem>(), (IEnumerable<int>) ((IEnumerable<SortedRelObjInfoItem>) array).Select<SortedRelObjInfoItem, int>((System.Func<SortedRelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>(), (IEnumerable<int>) ((IEnumerable<SortedRelObjInfoItem>) array).Select<SortedRelObjInfoItem, int>((System.Func<SortedRelObjInfoItem, int>) (item => item.PartInfo.ObjTypeID)).ToArray<int>(), (IEnumerable<ColumnDescriptor>) columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, "cad001e2-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
        if (source2 != null)
        {
          foreach (ObjInfoItem objInfoItem in ((IEnumerable<SortedRelObjInfoItem>) array).Select<SortedRelObjInfoItem, ObjInfoItem>((System.Func<SortedRelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).Distinct<ObjInfoItem>())
          {
            ObjInfoItem projObjItem = objInfoItem;
            IEnumerable<Tuple<int, long>> tuples2 = list.Where<SortedRelObjInfoItem>((System.Func<SortedRelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo == (TypedInfoItem) projObjItem)).GroupBy<SortedRelObjInfoItem, int>((System.Func<SortedRelObjInfoItem, int>) (item => item.PartInfo.ObjTypeID)).Select<IGrouping<int, SortedRelObjInfoItem>, Tuple<int, long>>((System.Func<IGrouping<int, SortedRelObjInfoItem>, Tuple<int, long>>) (group => new Tuple<int, long>(group.Key, group.Min<SortedRelObjInfoItem>((System.Func<SortedRelObjInfoItem, long>) (item => item.Sorting)))));
            ITechNumerationSession session = service1.CreateSession(sessionKeeper.Session.SessionGUID);
            session.BeginLogging();
            try
            {
              foreach (Tuple<int, long> tuple in tuples2)
              {
                Tuple<int, long> partTypeWithSortItem = tuple;
                DataRow dataRow = (DataRow) null;
                IEnumerable<DataRow> source3 = (IEnumerable<DataRow>) source2.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt64(row[0]) == projObjItem.ObjectID && Convert.ToInt32(row[2]) == partTypeWithSortItem.Item1 && Convert.ToInt64(row[4]) > partTypeWithSortItem.Item2));
                if (source3.Any<DataRow>())
                {
                  dataRow = source3.Aggregate<DataRow>((Func<DataRow, DataRow, DataRow>) ((row1, row2) => Convert.ToInt64(row1[4]) <= Convert.ToInt64(row2[4]) ? row1 : row2));
                }
                else
                {
                  IEnumerable<DataRow> source4 = (IEnumerable<DataRow>) source2.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt64(row[0]) == projObjItem.ObjectID && Convert.ToInt32(row[2]) == partTypeWithSortItem.Item1 && Convert.ToInt64(row[4]) < partTypeWithSortItem.Item2));
                  if (source4.Any<DataRow>())
                    dataRow = source4.Aggregate<DataRow>((Func<DataRow, DataRow, DataRow>) ((row1, row2) => Convert.ToInt64(row1[4]) <= Convert.ToInt64(row2[4]) ? row2 : row1));
                }
                if (dataRow != null)
                  session.NumerateObject(Convert.ToInt64(dataRow[3]), TechNumerationObjectModes.CurrentObj, sessionKeeper.Session.SessionGUID);
              }
            }
            finally
            {
              ITechNumerationLog numerationLog = session.GetNumerationLog();
              if (numerationLog != null)
              {
                if (numerationLog.ObjectsLog != null && numerationLog.ObjectsLog.Count != 0)
                  this.Notifications.QueueEvent((NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", numerationLog.ObjectsLog, true));
                if (numerationLog.RelationsLog != null && numerationLog.RelationsLog.Count != 0)
                  this.Notifications.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsChanged", numerationLog.RelationsLog));
              }
              service1.DisposeSession(sessionKeeper.Session.SessionGUID);
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoExecute()
  {
    ISelectedItems items = this.Items;
    IServiceProvider contextServices = this.ContextServices;
    if (items == null || contextServices == null)
      return;
    this.DoBeforeProceedItems((IUserSession) null);
    try
    {
      base.DoExecute();
    }
    finally
    {
      this.DoAfterProceedItems((IUserSession) null);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoBeforeProceedItems(IUserSession session)
  {
    base.DoBeforeProceedItems(session);
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.Items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1 && this.Items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData2)
      {
        IDBTypedObjectID parentData = this.Items.GetParentData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        SortedRelObjInfoItem sortedRelObjInfoItem = new SortedRelObjInfoItem(itemData2.Value, itemData2.RelationType);
        sortedRelObjInfoItem.PartInfo = new ObjInfoItem(itemData1.ObjectID, itemData1.ObjectType);
        sortedRelObjInfoItem.ProjInfo = parentData != null ? new ObjInfoItem(parentData.ObjectID, parentData.ObjectType) : (ObjInfoItem) null;
        sortedRelObjInfoItem.Sorting = itemData2.Sorting;
        this._relationInfo2Delete.Add(sortedRelObjInfoItem);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="itemIndex"></param>
  /// <returns></returns>
  protected override bool CouldDeleteItemRelation(int itemIndex)
  {
    return this.Items.GetItemData(itemIndex, typeof (IDBRelationID)) is IDBRelationID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool GetDeletingObjects()
  {
    if (!base.GetDeletingObjects())
      return false;
    if (this.UpdateObjectsInfo)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IObjectsDeleteAnalyzerService service = ServiceUtils.GetService<IObjectsDeleteAnalyzerService>((object) sessionKeeper.Session, false);
        if (service == null)
          return false;
        this._deletingObjects = service.LoadDescriptions(sessionKeeper.Session.SessionGUID, this._deletingObjects);
      }
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="jobStatus"></param>
  /// <returns></returns>
  protected override bool PurgeDeletingObjects(out DeleteObjectsJobStatus jobStatus)
  {
    jobStatus = (DeleteObjectsJobStatus) null;
    if (this._deletingObjects == null)
      return false;
    this.ExcludeUniqueObjects();
    if (!base.PurgeDeletingObjects(out jobStatus))
      return false;
    this.ReNumerateObjects(jobStatus);
    return true;
  }

  /// <summary>
  /// Флаг принудительного обновления параметров удаляемых объектов
  /// </summary>
  internal bool UpdateObjectsInfo { get; set; }
}
