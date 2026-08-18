
// Type: Intermech.Files.RecursiveTreeBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Kernel.Search;
using Intermech.Memoization;
using Intermech.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;


namespace Intermech.Files;

public sealed class RecursiveTreeBuilder : IObjectListBuilder
{
  private readonly long rootObjectId;
  private readonly VersionsRulePackage versionsRule;
  private readonly IDBObjectsInformationService dbObjectsInformation;
  private readonly IFileAttributeEditorService fileAttributeEditorService;

  public RecursiveTreeBuilder(
    long rootObjectId,
    VersionsRulePackage versionsRule,
    IDBObjectsInformationService dbObjectsInformation,
    IFileAttributeEditorService fileAttributeEditorService)
  {
    if (rootObjectId == 0L)
      throw new ArgumentException();
    if (versionsRule == null)
      throw new ArgumentNullException(nameof (versionsRule));
    if (dbObjectsInformation == null)
      throw new ArgumentNullException(nameof (dbObjectsInformation));
    if (fileAttributeEditorService == null)
      throw new ArgumentNullException(nameof (fileAttributeEditorService));
    this.rootObjectId = rootObjectId;
    this.versionsRule = versionsRule;
    this.dbObjectsInformation = dbObjectsInformation;
    this.fileAttributeEditorService = fileAttributeEditorService;
  }

  public List<DBObjectState> BuildList()
  {
    if (TraceSupport.ObjectListBuilders.Enabled)
    {
      Trace.WriteLine("File vault: object list creation");
      Trace.WriteLine($"File vault: {this.rootObjectId}");
    }
    DBObjectState objectState1;
    DataTable tbl;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.rootObjectId, true);
      objectState1 = this.dbObjectsInformation.GetObjectState(dbObject);
      int objectType = dbObject.ObjectType;
      Tuple<List<int>, List<int>> tuple = RecursiveTreeBuilder.Cache.GetRelationsAndChildTypes(objectType);
      tbl = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true).LoadComposition((object) sessionKeeper.Session.SessionGUID, this.rootObjectId, objectType, (IEnumerable<int>) tuple.Item1, (IEnumerable<int>) tuple.Item2, (IEnumerable<ColumnDescriptor>) RecursiveTreeBuilder.Consts.QueryColumns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, this.versionsRule.OwnerId, (HybridDictionary) null, -1);
    }
    if (tbl == null || tbl.Rows.Count == 0)
      return CollectionUtils.CreateList<DBObjectState>(objectState1);
    List<RecursiveTreeBuilder.PartRecord> partTable = this.ParsePartTable(tbl);
    this.FilterPartTable(partTable);
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>(partTable.Count + 1);
    dbObjectStateList.Add(objectState1);
    Dictionary<long, Tuple<RecursiveTreeBuilder.PartRecord, DBObjectState>> dictionary = new Dictionary<long, Tuple<RecursiveTreeBuilder.PartRecord, DBObjectState>>(dbObjectStateList.Capacity);
    foreach (RecursiveTreeBuilder.PartRecord partRecord in partTable)
    {
      if (partRecord.ObjectId != this.rootObjectId && partRecord.Id != objectState1.Id)
      {
        Tuple<RecursiveTreeBuilder.PartRecord, DBObjectState> tuple;
        if (dictionary.TryGetValue(partRecord.Id, out tuple))
        {
          if (partRecord.ObjectId != tuple.Item1.ObjectId)
          {
            DBObjectState objectState2 = this.dbObjectsInformation.GetObjectState(tuple.Item1.ParentId, true);
            DBObjectState dbObjectState1 = tuple.Item2;
            DBObjectState objectState3 = this.dbObjectsInformation.GetObjectState(partRecord.ParentId, true);
            DBObjectState dbObjectState2 = new DBObjectState(partRecord.Id, partRecord.ObjectId, partRecord.ModifyMode, partRecord.Caption);
            throw new KernelException($"Обнаружен конфликт подбора версий объектов на разных уровнях состава у головного объекта '{objectState1}'. Одна конфликтующая версия '{dbObjectState2}' входит в состав '{objectState3}', а другая конфликтующая версия '{dbObjectState1}' входит в состав '{objectState2}'. Исправьте ошибку и повторите операцию.");
          }
        }
        else
        {
          DBObjectState dbObjectState = new DBObjectState(partRecord.Id, partRecord.ObjectId, partRecord.ModifyMode, partRecord.Caption);
          dbObjectStateList.Add(dbObjectState);
          dictionary.Add(partRecord.Id, Tuple.Create<RecursiveTreeBuilder.PartRecord, DBObjectState>(partRecord, dbObjectState));
          if (TraceSupport.ObjectListBuilders.Enabled)
            Trace.WriteLine($"File vault: {partRecord.ObjectId}");
        }
      }
    }
    if (TraceSupport.ObjectListBuilders.Enabled)
      Trace.WriteLine($"File vault: object list complete, count = {dbObjectStateList.Count}");
    return dbObjectStateList;
  }

  private List<RecursiveTreeBuilder.PartRecord> ParsePartTable(DataTable tbl)
  {
    List<RecursiveTreeBuilder.PartRecord> partTable = new List<RecursiveTreeBuilder.PartRecord>(tbl.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      partTable.Add(new RecursiveTreeBuilder.PartRecord()
      {
        Id = Convert.ToInt64(row[0]),
        ObjectId = Convert.ToInt64(row[1]),
        ObjectTypeId = Convert.ToInt32(row[2]),
        Caption = Convert.ToString(row[3]),
        ModifyMode = RecursiveTreeBuilder.Cache.GetModifyModes(Convert.ToInt32(row[4])),
        ParentId = Convert.ToInt64(row[5])
      });
    return partTable;
  }

  private void FilterPartTable(List<RecursiveTreeBuilder.PartRecord> parts)
  {
    ICollection<int> inMemoryObjectTypes = this.fileAttributeEditorService.GetObjectTypesWithInternalEditMode();
    parts.RemoveAll((Predicate<RecursiveTreeBuilder.PartRecord>) (item => inMemoryObjectTypes.Contains(item.ObjectTypeId)));
  }

  private static class Consts
  {
    public const int AnyRelationType = -1;
    public const int AnyObjectType = -1;
    public static List<ColumnDescriptor> QueryColumns = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) new ColumnDescriptor[6]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, -1)
    });
  }

  private static class Cache
  {
    public static readonly RelationTypeResolver DocTreeType = MetadataResolvers.Factory.RelationTypeResolver(new Guid("cad0057c-306c-11d8-b4e9-00304f19f545"));
    public static readonly System.Func<int, ObjectModifyModes> GetModifyModes = TableLookupMemoizer<int, ObjectModifyModes>.Wrap(new System.Func<int, ObjectModifyModes>(RecursiveTreeBuilder.Cache.GetModifyModeBody), (IStateMonitor) MetadataResolvers.ChangeMonitor, (ISyncRoot) new RefSyncRoot());
    public static readonly System.Func<int, Tuple<List<int>, List<int>>> GetRelationsAndChildTypes = TableLookupMemoizer<int, Tuple<List<int>, List<int>>>.Wrap(new System.Func<int, Tuple<List<int>, List<int>>>(RecursiveTreeBuilder.Cache.GetRelationsAndChildTypesBody), (IStateMonitor) MetadataResolvers.ChangeMonitor, (ISyncRoot) new RefSyncRoot());

    private static ObjectModifyModes GetModifyModeBody(int lcStep)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetLifecycleStep(lcStep, true).ObjectModifyMode;
    }

    private static Tuple<List<int>, List<int>> GetRelationsAndChildTypesBody(int objectType)
    {
      List<Tuple<int, int, int>> applicabilityList = RecursiveTreeBuilder.Cache.CreateApplicabilityList(objectType);
      List<int> intList1 = new List<int>(applicabilityList.Count);
      List<int> intList2 = new List<int>(applicabilityList.Count);
      foreach (Tuple<int, int, int> tuple in applicabilityList)
      {
        if (!intList1.Contains(tuple.Item2))
          intList1.Add(tuple.Item2);
        if (!intList2.Contains(tuple.Item3))
          intList2.Add(tuple.Item3);
      }
      return new Tuple<List<int>, List<int>>(intList1, intList2);
    }

    private static List<Tuple<int, int, int>> CreateApplicabilityList(int objectType)
    {
      List<Tuple<int, int, int>> aList = new List<Tuple<int, int, int>>(32 /*0x20*/);
      RecursiveTreeBuilder.Cache.CreateApplicabilityList(objectType, aList, new List<int>(32 /*0x20*/)
      {
        objectType
      });
      return aList;
    }

    private static void CreateApplicabilityList(
      int parentObjectType,
      List<Tuple<int, int, int>> aList,
      List<int> stopList)
    {
      LinkedList<int> collection = new LinkedList<int>();
      Hashtable possibleChildren;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        possibleChildren = sessionKeeper.Session.GetObjectType(parentObjectType, true).GetPossibleChildren();
      foreach (DictionaryEntry dictionaryEntry in possibleChildren)
      {
        int relationType = (int) dictionaryEntry.Value;
        if (RecursiveTreeBuilder.Cache.IsCheckoutRelation(relationType))
        {
          Tuple<int, int, int> tuple = Tuple.Create<int, int, int>(parentObjectType, relationType, (int) dictionaryEntry.Key);
          aList.Add(tuple);
          if (!stopList.Contains(tuple.Item3))
            collection.AddLast(tuple.Item3);
        }
      }
      stopList.AddRange((IEnumerable<int>) collection);
      foreach (int parentObjectType1 in collection)
        RecursiveTreeBuilder.Cache.CreateApplicabilityList(parentObjectType1, aList, stopList);
    }

    private static bool IsCheckoutRelation(int relationType)
    {
      return relationType == RecursiveTreeBuilder.Cache.DocTreeType.Id;
    }
  }

  private sealed class PartRecord
  {
    public long Id { get; set; }

    public long ObjectId { get; set; }

    public int ObjectTypeId { get; set; }

    public string Caption { get; set; }

    public ObjectModifyModes ModifyMode { get; set; }

    public long ParentId { get; set; }
  }
}
