// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.JTDocumentFromDerviedDocumentLocator
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

internal sealed class JTDocumentFromDerviedDocumentLocator : IObjectLocator
{
  private readonly IDBObjectRef derivedDocumentRef;

  public JTDocumentFromDerviedDocumentLocator(IDBObjectRef derivedDocumentRef)
  {
    this.derivedDocumentRef = derivedDocumentRef != null ? derivedDocumentRef : throw new ArgumentNullException(nameof (derivedDocumentRef));
  }

  public ObjectLocatorResult LocateObject()
  {
    long objectId = this.derivedDocumentRef.GetObjectId();
    int objectType = DBHelper.GetObjectType(objectId);
    return !DBHelper.IsBasedOnType(objectType, IDCache.Default.AlternativeRepresenations.Id) || objectType == IDCache.Default.JTDocuments.Id ? (ObjectLocatorResult) null : this.FindJTDocument(objectId);
  }

  private ObjectLocatorResult FindJTDocument(long docId)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    paramSet.ColumnsInfo = new ColumnInfo[2]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, (object) null)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.DocumentTree.Id);
      relationCollection.ObjectTypeID = IDCache.Default.JTDocuments.Id;
      dataTable = relationCollection.ConsistFrom(paramSet, docId);
    }
    return dataTable.Rows.Count == 0 ? (ObjectLocatorResult) null : new ObjectLocatorResult(Convert.ToInt64(dataTable.Rows[0][0]), Convert.ToInt32(dataTable.Rows[0][1]));
  }
}
