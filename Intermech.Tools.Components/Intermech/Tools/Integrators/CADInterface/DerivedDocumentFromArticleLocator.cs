// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DerivedDocumentFromArticleLocator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class DerivedDocumentFromArticleLocator : IObjectLocator
{
  private readonly IDBObjectRef articleRef;
  private readonly int derivedDocumentType;

  public DerivedDocumentFromArticleLocator(IDBObjectRef articleRef, int derivedDocumentType)
  {
    if (articleRef == null)
      throw new ArgumentNullException(nameof (articleRef));
    if (derivedDocumentType == -1)
      throw new ArgumentException();
    this.articleRef = articleRef;
    this.derivedDocumentType = derivedDocumentType;
  }

  public ObjectLocatorResult LocateObject()
  {
    long objectId = this.articleRef.GetObjectId();
    if (!DBHelper.IsBasedOnType(DBHelper.GetObjectType(objectId), IDCache.Default.AllArticles.Id))
      return (ObjectLocatorResult) null;
    ConditionStructure conditionStructure1 = new ConditionStructure(IDCache.Default.ObjectExternalKey.Id, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, true);
    conditionStructure1.AttributeSource = AttributeSourceTypes.Relation;
    ConditionStructure conditionStructure2 = new ConditionStructure(IDCache.Default.JTSourceDocumentMarker.Id, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, true);
    conditionStructure2.AttributeSource = AttributeSourceTypes.Object;
    DBRecordSetParams paramSet1 = new DBRecordSetParams();
    paramSet1.RecordCount = -1;
    paramSet1.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) IDCache.Default.ObjectExternalKey.Id
    };
    paramSet1.ColumnsInfo = new ColumnInfo[2]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) IDCache.Default.ObjectExternalKey.Id, AttributeSourceTypes.Relation, (object) null)
    };
    paramSet1.Conditions = new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    };
    DBRecordSetParams paramSet2 = new DBRecordSetParams();
    paramSet2.RecordCount = 1;
    paramSet2.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable1 = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id).ConsistFrom(paramSet1, objectId);
      if (dataTable1.Rows.Count != 0)
      {
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.DocumentTree.Id);
        relationCollection.ObjectTypeID = this.derivedDocumentType;
        foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
        {
          long jtDocument = JTLinkManager.FindJTDocument(Convert.ToInt64(row1[0]), Convert.ToString(row1[1]));
          if (jtDocument == 0L)
            return (ObjectLocatorResult) null;
          DataTable dataTable2 = relationCollection.EntersInVersion(paramSet2, jtDocument);
          if (dataTable2.Rows.Count != 0)
          {
            DataRow row2 = dataTable2.Rows[0];
            return new ObjectLocatorResult(Convert.ToInt64(row2[0]), Convert.ToInt32(row2[1]));
          }
        }
      }
    }
    return (ObjectLocatorResult) null;
  }
}
