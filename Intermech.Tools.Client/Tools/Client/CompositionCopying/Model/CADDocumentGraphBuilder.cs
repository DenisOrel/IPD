// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADDocumentGraphBuilder
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Kernel.Search;
using Intermech.Runtime;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.CompositionCopying;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CADDocumentGraphBuilder : 
  RecursiveDBObjectGraphBuilder<CADDocumentEdgeProperties>
{
  private DBObjectRecord rootDocument;
  private ICollection<int> documentTypes;
  private ICollection<int> drawingTypes;
  private List<CADDocumentGraphBuilder.DocumentRecord> documentRecords;
  private List<CADDocumentGraphBuilder.DrawingRecord> drawingRecords;
  private List<CADDocumentGraphBuilder.ArticleRecord> articleRecords;

  public CADDocumentGraphBuilder(CopyingSession session, DBObjectRecord rootDocument)
    : base(session)
  {
    this.rootDocument = rootDocument != null ? rootDocument : throw new ArgumentNullException(nameof (rootDocument));
    this.documentTypes = (ICollection<int>) new HashSet<int>();
    this.documentTypes.Add(rootDocument.ObjectTypeId);
  }

  public DBObjectRecord RootDocument
  {
    [DebuggerStepThrough] get => this.rootDocument;
  }

  public ICollection<int> DocumentTypes => this.documentTypes;

  protected override void DoValidateConfiguration()
  {
    base.DoValidateConfiguration();
    if (!this.DocumentTypes.Contains(this.RootDocument.ObjectTypeId))
      throw PropertyExceptions.PropertyBadValueException((object) this, "DocumentTypes", "Список типов документов должен включать тип головного документа.");
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.drawingTypes = this.GetCADModelDrawingTypes();
    this.documentRecords = this.LoadDocumentComposition();
    this.drawingRecords = this.LoadDrawings();
    this.articleRecords = this.LoadArticles();
  }

  protected override void DoCleanup()
  {
    this.drawingTypes = (ICollection<int>) null;
    this.documentRecords = (List<CADDocumentGraphBuilder.DocumentRecord>) null;
    this.drawingRecords = (List<CADDocumentGraphBuilder.DrawingRecord>) null;
    this.articleRecords = (List<CADDocumentGraphBuilder.ArticleRecord>) null;
    base.DoCleanup();
  }

  protected override DBObjectGraphVertex DoBuildRootVertex()
  {
    DBObjectGraphVertex objectGraphVertex = new DBObjectGraphVertex(this.RootDocument.ObjectId, this.RootDocument.ObjectTypeId, this.RootDocument.Caption);
    objectGraphVertex.Traits.Add((DBObjectGraphTrait) new DocumentTrait());
    if (this.drawingTypes.Contains(this.RootDocument.ObjectTypeId))
      objectGraphVertex.Traits.Add((DBObjectGraphTrait) new CADModelDrawingTrait());
    return objectGraphVertex;
  }

  protected override List<(DBObjectGraphVertex, CADDocumentEdgeProperties)> DoBuildChildrenVertices(
    DBObjectGraphVertex parentVertex)
  {
    List<CADDocumentGraphBuilder.DocumentRecord> all = this.documentRecords.FindAll((Predicate<CADDocumentGraphBuilder.DocumentRecord>) (x => x.ParentObjectId == parentVertex.ObjectId));
    List<(DBObjectGraphVertex, CADDocumentEdgeProperties)> valueTupleList = new List<(DBObjectGraphVertex, CADDocumentEdgeProperties)>(all.Count);
    foreach (CADDocumentGraphBuilder.DocumentRecord documentRecord in all)
    {
      DBObjectGraphVertex objectGraphVertex = new DBObjectGraphVertex(documentRecord.ObjectId, documentRecord.ObjectTypeId, documentRecord.Caption);
      objectGraphVertex.Traits.Add((DBObjectGraphTrait) new DocumentTrait());
      valueTupleList.Add((objectGraphVertex, new CADDocumentEdgeProperties()));
    }
    return valueTupleList;
  }

  protected override DBObjectGraphEdge DoBuildChildEdge(
    DBObjectGraphVertex parentVertex,
    DBObjectGraphVertex childVertex,
    CADDocumentEdgeProperties childEdgeProperties)
  {
    return new DBObjectGraphEdge(parentVertex, childVertex);
  }

  protected override void DoBuild()
  {
    base.DoBuild();
    IEnumerable<\u003C\u003Ef__AnonymousType1<long, int, HashSet<long>, string, int>> datas = this.drawingRecords.GroupBy<CADDocumentGraphBuilder.DrawingRecord, long>((System.Func<CADDocumentGraphBuilder.DrawingRecord, long>) (x => x.ObjectId)).Select(g => new
    {
      DrawingObjectID = g.Key,
      Count = g.Count<CADDocumentGraphBuilder.DrawingRecord>(),
      ModelsIDs = g.Select<CADDocumentGraphBuilder.DrawingRecord, long>((System.Func<CADDocumentGraphBuilder.DrawingRecord, long>) (d => d.ModelID)).ToHashSet<long>(),
      DrawingCaption = g.Any<CADDocumentGraphBuilder.DrawingRecord>() ? g.First<CADDocumentGraphBuilder.DrawingRecord>().Caption : string.Empty,
      DrawingObjectTypeID = g.Any<CADDocumentGraphBuilder.DrawingRecord>() ? g.First<CADDocumentGraphBuilder.DrawingRecord>().ObjectTypeId : -1
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(this.Session.Services.IntegratorsIDCache.DocumentTree.Id);
      relationCollection.FiltrationOwnerID = this.Session.VersionsRule.OwnerId;
      DBRecordSetParams paramSet = new DBRecordSetParams();
      paramSet.AddColumnDescriptors(new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID)
      }, (List<int>) null);
      Dictionary<long, long> dictionary = new Dictionary<long, long>()
      {
        {
          sessionKeeper.Session.GetIDByObjectID(this.RootDocument.ObjectId),
          this.RootDocument.ObjectId
        }
      };
      foreach (CADDocumentGraphBuilder.DocumentRecord documentRecord in this.documentRecords)
      {
        if (!dictionary.ContainsKey(documentRecord.ID))
          dictionary.Add(documentRecord.ID, documentRecord.ObjectId);
      }
      foreach (var data in datas)
      {
        var drawingGroupRecord = data;
        DataTable dataTable = relationCollection.ConsistFrom(paramSet, drawingGroupRecord.DrawingObjectID);
        if (drawingGroupRecord.Count == 0 || drawingGroupRecord.DrawingCaption == string.Empty || drawingGroupRecord.DrawingObjectTypeID == -1)
          this.Session.DrawingWithoutAllModels.Add(new DBObjectRecord(drawingGroupRecord.DrawingObjectID, drawingGroupRecord.DrawingObjectTypeID, drawingGroupRecord.DrawingCaption));
        else if (dataTable.Rows.Count == drawingGroupRecord.Count)
        {
          HashSet<long> first = new HashSet<long>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            first.Add(Convert.ToInt64(row.ItemArray[0]));
          if (!first.Except<long>((IEnumerable<long>) drawingGroupRecord.ModelsIDs).Any<long>())
          {
            DBObjectGraphVertex objectGraphVertex = this.Session.Graph.GetFirstVertexOrDefault((Predicate<DBObjectGraphVertex>) (x => x.ObjectId == drawingGroupRecord.DrawingObjectID));
            if (objectGraphVertex == null)
            {
              objectGraphVertex = new DBObjectGraphVertex(drawingGroupRecord.DrawingObjectID, drawingGroupRecord.DrawingObjectTypeID, drawingGroupRecord.DrawingCaption);
              objectGraphVertex.Traits.Add((DBObjectGraphTrait) new DocumentTrait());
              objectGraphVertex.Traits.Add((DBObjectGraphTrait) new CADModelDrawingTrait());
              this.Session.Graph.AddVertex(objectGraphVertex);
            }
            foreach (long modelsId in drawingGroupRecord.ModelsIDs)
            {
              if (dictionary.ContainsKey(modelsId))
              {
                long compositionRecordObjectID = dictionary[modelsId];
                DBObjectGraphVertex firstVertexOrDefault = this.Session.Graph.GetFirstVertexOrDefault((Predicate<DBObjectGraphVertex>) (x => x.ObjectId == compositionRecordObjectID));
                if (firstVertexOrDefault != null && !this.Session.Graph.ContainsEdge(objectGraphVertex, firstVertexOrDefault))
                  this.Session.Graph.AddEdge(new DBObjectGraphEdge(objectGraphVertex, firstVertexOrDefault));
              }
            }
          }
          else
            this.Session.DrawingWithoutAllModels.Add(new DBObjectRecord(drawingGroupRecord.DrawingObjectID, drawingGroupRecord.DrawingObjectTypeID, drawingGroupRecord.DrawingCaption));
        }
        else
          this.Session.DrawingWithoutAllModels.Add(new DBObjectRecord(drawingGroupRecord.DrawingObjectID, drawingGroupRecord.DrawingObjectTypeID, drawingGroupRecord.DrawingCaption));
      }
      foreach (CADDocumentGraphBuilder.ArticleRecord articleRecord in this.articleRecords)
      {
        CADDocumentGraphBuilder.ArticleRecord record = articleRecord;
        if (dictionary.ContainsKey(record.ModelID))
        {
          long compositionRecordObjectID = dictionary[record.ModelID];
          DBObjectGraphVertex firstVertexOrDefault = this.Session.Graph.GetFirstVertexOrDefault((Predicate<DBObjectGraphVertex>) (x => x.ObjectId == compositionRecordObjectID));
          if (firstVertexOrDefault != null)
          {
            DBObjectGraphVertex objectGraphVertex = this.Session.Graph.GetFirstVertexOrDefault((Predicate<DBObjectGraphVertex>) (x => x.ObjectId == record.ObjectId));
            if (objectGraphVertex == null)
            {
              objectGraphVertex = new DBObjectGraphVertex(record.ObjectId, record.ObjectTypeId, record.Caption);
              objectGraphVertex.Traits.Add((DBObjectGraphTrait) new ArticleTrait());
              this.Session.Graph.AddVertex(objectGraphVertex);
            }
            DBObjectGraphEdge edge = new DBObjectGraphEdge(objectGraphVertex, firstVertexOrDefault);
            edge.Traits.Add((DBObjectGraphTrait) new ArticleDocumentationTrait()
            {
              IsBasedOnCADModel = record.IsBasedOnCADModel,
              ExternalKey = record.ExternalKey,
              CADConfigurationName = record.CADConfigurationName
            });
            this.Session.Graph.AddEdge(edge);
          }
        }
      }
    }
  }

  private List<CADDocumentGraphBuilder.DocumentRecord> LoadDocumentComposition()
  {
    CompositionLoadingParams queryParams = this.CreateQueryParams();
    long idByObjectId;
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      idByObjectId = sessionKeeper.Session.GetIDByObjectID(this.RootDocument.ObjectId);
      dataTable = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true).LoadComplexCompositions((object) sessionKeeper.Session, queryParams);
    }
    if (dataTable == null || dataTable.Rows.Count == 0)
      return new List<CADDocumentGraphBuilder.DocumentRecord>(0);
    List<CADDocumentGraphBuilder.DocumentRecord> documentRecordList1 = new List<CADDocumentGraphBuilder.DocumentRecord>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      documentRecordList1.Add(new CADDocumentGraphBuilder.DocumentRecord(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]), Convert.ToInt64(row[2]), Convert.ToString(row[3]), Convert.ToInt64(row[4])));
    List<CADDocumentGraphBuilder.DocumentRecord> documentRecordList2 = new List<CADDocumentGraphBuilder.DocumentRecord>(documentRecordList1.Count);
    Dictionary<long, CADDocumentGraphBuilder.DocumentRecord> dictionary = new Dictionary<long, CADDocumentGraphBuilder.DocumentRecord>(documentRecordList2.Capacity);
    foreach (CADDocumentGraphBuilder.DocumentRecord documentRecord1 in documentRecordList1)
    {
      if (documentRecord1.ObjectId != this.RootDocument.ObjectId && documentRecord1.ID != idByObjectId)
      {
        CADDocumentGraphBuilder.DocumentRecord documentRecord2;
        if (dictionary.TryGetValue(documentRecord1.ID, out documentRecord2))
        {
          if (documentRecord1.ObjectId != documentRecord2.ObjectId)
            throw new KernelException($"Обнаружен конфликт подбора версий объектов на разных уровнях состава у головного объекта '{this.RootDocument.Caption}'. Одна конфликтующая версия '{documentRecord1.ObjectId}' входит в состав '{documentRecord1.ParentObjectId}', а другая конфликтующая версия '{documentRecord2.ObjectId}' входит в состав '{documentRecord2.ParentObjectId}'. Исправьте ошибку и повторите операцию.").WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(documentRecord1.ObjectId), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(documentRecord1.ParentObjectId), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(documentRecord2.ObjectId), (ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(documentRecord2.ParentObjectId));
        }
        else
        {
          documentRecordList2.Add(documentRecord1);
          dictionary.Add(documentRecord1.ID, documentRecord1);
        }
      }
    }
    return documentRecordList2;
  }

  private CompositionLoadingParams CreateQueryParams()
  {
    return new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(this.RootDocument.ObjectId, this.RootDocument.ObjectTypeId)
    }, (IEnumerable<int>) null, (IEnumerable<int>) new int[1]
    {
      this.Session.Services.IntegratorsIDCache.DocumentTree.Id
    }, (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID)
    }, (IEnumerable<ConditionStructure>) new ConditionStructure[0], true, false)
    {
      LoadLevels = -1,
      FiltrationOwnerId = this.Session.VersionsRule.OwnerId
    };
  }

  private List<CADDocumentGraphBuilder.DrawingRecord> LoadDrawings()
  {
    if (this.drawingTypes.Count == 0)
      return new List<CADDocumentGraphBuilder.DrawingRecord>(0);
    Dictionary<long, long> dictionary = new Dictionary<long, long>();
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long idByObjectId = sessionKeeper.Session.GetIDByObjectID(this.RootDocument.ObjectId);
      HashSet<long> source = new HashSet<long>()
      {
        idByObjectId
      };
      dictionary.Add(idByObjectId, this.RootDocument.ObjectId);
      foreach (CADDocumentGraphBuilder.DocumentRecord documentRecord in this.documentRecords)
      {
        source.Add(documentRecord.ID);
        if (dictionary.ContainsKey(documentRecord.ID))
          dictionary[documentRecord.ID] = documentRecord.ObjectId;
        else
          dictionary.Add(documentRecord.ID, documentRecord.ObjectId);
      }
      if (source.Count == 0)
        return new List<CADDocumentGraphBuilder.DrawingRecord>(0);
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(new Guid("cad00035-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) source.ToArray<long>(), LogicalOperators.AND, 0),
        new ConditionStructure(-7, RelationalOperators.In, (object) this.drawingTypes.ToArray<int>(), LogicalOperators.NONE, 0, false)
      });
      ColumnDescriptor[] AddColumns = new ColumnDescriptor[5]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID)
      };
      dbRecordSetParams.AddColumnDescriptors(AddColumns, (List<int>) null);
      dbRecordSetParams.Tags = new HybridDictionary()
      {
        {
          (object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}",
          (object) dictionary
        }
      };
      dataTable = ServiceUtils.GetService<ICADDocumentCopyingServerService>((object) sessionKeeper.Session, true).LoadDrawingsOrArticles(sessionKeeper.Session, this.Session.Services.IntegratorsIDCache.DocumentTree.Id, this.Session.VersionsRule.OwnerId, dbRecordSetParams);
    }
    if (dataTable == null)
      return new List<CADDocumentGraphBuilder.DrawingRecord>(0);
    List<CADDocumentGraphBuilder.DrawingRecord> drawingRecordList = new List<CADDocumentGraphBuilder.DrawingRecord>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      drawingRecordList.Add(new CADDocumentGraphBuilder.DrawingRecord(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]), Convert.ToInt64(row[2]), Convert.ToString(row[3])));
    return drawingRecordList;
  }

  private List<CADDocumentGraphBuilder.ArticleRecord> LoadArticles()
  {
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<long, long> dictionary = new Dictionary<long, long>();
      long idByObjectId = sessionKeeper.Session.GetIDByObjectID(this.RootDocument.ObjectId);
      HashSet<long> source = new HashSet<long>()
      {
        idByObjectId
      };
      dictionary.Add(idByObjectId, this.RootDocument.ObjectId);
      foreach (CADDocumentGraphBuilder.DocumentRecord documentRecord in this.documentRecords)
      {
        source.Add(documentRecord.ID);
        if (dictionary.ContainsKey(documentRecord.ID))
          dictionary[documentRecord.ID] = documentRecord.ObjectId;
        else
          dictionary.Add(documentRecord.ID, documentRecord.ObjectId);
      }
      if (source.Count == 0)
        return new List<CADDocumentGraphBuilder.ArticleRecord>(0);
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.Session.Services.IntegratorsIDCache.AllArticles.Id);
      List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.Session.Services.IntegratorsIDCache.AllMaterials.Id);
      List<int> intList = new List<int>((IEnumerable<int>) childrenIdRecursive1);
      intList.AddRange((IEnumerable<int>) childrenIdRecursive2);
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(new Guid("cad00035-306c-11d8-b4e9-00304f19f545"), RelationalOperators.In, (object) source.ToArray<long>(), LogicalOperators.AND, 0),
        new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
      });
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).AddColumnDescriptors(new ColumnDescriptor[7]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION),
        new ColumnDescriptor((object) this.Session.Services.IntegratorsIDCache.BasedOnCADModel.Id)
        {
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnDescriptor((object) this.Session.Services.IntegratorsIDCache.ObjectExternalKey.Id)
        {
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnDescriptor((object) this.Session.Services.IntegratorsIDCache.CADConfigurationName.Id)
        {
          AttributeSource = AttributeSourceTypes.Relation
        }
      }, (List<int>) null);
      dbRecordSetParams.Tags = new HybridDictionary()
      {
        {
          (object) "{2C7E989F-0EAF-40CC-80FD-16EF1D9090B3}",
          (object) dictionary
        }
      };
      dataTable = ServiceUtils.GetService<ICADDocumentCopyingServerService>((object) sessionKeeper.Session, true).LoadDrawingsOrArticles(sessionKeeper.Session, this.Session.Services.IntegratorsIDCache.ArticleToDocumentTree.Id, this.Session.VersionsRule.OwnerId, dbRecordSetParams);
    }
    if (dataTable == null)
      return new List<CADDocumentGraphBuilder.ArticleRecord>(0);
    List<CADDocumentGraphBuilder.ArticleRecord> articleRecordList = new List<CADDocumentGraphBuilder.ArticleRecord>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      CADDocumentGraphBuilder.ArticleRecord articleRecord = new CADDocumentGraphBuilder.ArticleRecord(Convert.ToInt64(row[0]), Convert.ToInt32(row[1]), Convert.ToInt64(row[2]), Convert.ToString(row[3]), this.TryConvertToBoolean(row[4]), this.TryConvertToString(row[5]), this.TryConvertToString(row[6]));
      articleRecordList.Add(articleRecord);
    }
    return articleRecordList;
  }

  private string TryConvertToString(object value, string defaultValue = "")
  {
    return value == null || Convert.IsDBNull(value) ? defaultValue : Convert.ToString(value);
  }

  private bool TryConvertToBoolean(object value, bool defaultValue = false)
  {
    return value == null || Convert.IsDBNull(value) ? defaultValue : Convert.ToBoolean(value);
  }

  private ICollection<int> GetCADModelDrawingTypes()
  {
    HashSet<int> collection = new HashSet<int>();
    DocumentGroup byName1 = this.Session.IntegratorSettings.FileDocumentGroups.FindByName("AssemblyDrawing", false);
    if (byName1 != null)
      collection.AddRange<int>((IEnumerable<int>) byName1.AsIdList());
    DocumentGroup byName2 = this.Session.IntegratorSettings.FileDocumentGroups.FindByName("PartDrawing", false);
    if (byName2 != null)
      collection.AddRange<int>((IEnumerable<int>) byName2.AsIdList());
    return (ICollection<int>) collection;
  }

  private sealed class DocumentRecord
  {
    public DocumentRecord(
      long objectId,
      int objectTypeId,
      long parentObjectId,
      string caption,
      long id)
    {
      this.ObjectId = objectId;
      this.ObjectTypeId = objectTypeId;
      this.ParentObjectId = parentObjectId;
      this.Caption = caption;
      this.ID = id;
    }

    public long ID { get; }

    public long ObjectId { get; }

    public int ObjectTypeId { get; }

    public long ParentObjectId { get; }

    public string Caption { get; }
  }

  private sealed class DrawingRecord
  {
    public DrawingRecord(long objectId, int objectTypeId, long modelID, string caption)
    {
      this.ObjectId = objectId;
      this.ObjectTypeId = objectTypeId;
      this.Caption = caption;
      this.ModelID = modelID;
    }

    public long ModelID { get; }

    public long ObjectId { get; }

    public int ObjectTypeId { get; }

    public string Caption { get; }
  }

  private sealed class ArticleRecord
  {
    public ArticleRecord(
      long objectId,
      int objectTypeId,
      long modelID,
      string caption,
      bool isBasedOnCADModel,
      string externalKey,
      string cadConfigurationName)
    {
      this.ObjectId = objectId;
      this.ObjectTypeId = objectTypeId;
      this.Caption = caption;
      this.ModelID = modelID;
      this.IsBasedOnCADModel = isBasedOnCADModel;
      this.ExternalKey = externalKey;
      this.CADConfigurationName = cadConfigurationName;
    }

    public long ModelID { get; }

    public long ObjectId { get; }

    public int ObjectTypeId { get; }

    public string Caption { get; }

    public bool IsBasedOnCADModel { get; }

    public string ExternalKey { get; }

    public string CADConfigurationName { get; }
  }
}
