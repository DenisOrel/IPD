// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.ElementListVersionHandle
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class ElementListVersionHandle : DocumentVersionHandle
{
  public ElementListVersionHandle()
  {
    this.revisionInstantiationMode = RevisionInstantiationMode.Hard;
  }

  protected override List<Tuple<long, int>> FindDocuments(IUserSession session, long articleID)
  {
    DataTable dataTable = session.GetRelationCollection(session.IdentHelper.DocRelationTypeID).ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }), articleID);
    if (dataTable.Rows.Count <= 0)
      return (List<Tuple<long, int>>) null;
    List<Tuple<long, int>> documents = new List<Tuple<long, int>>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int typeID = Convert.ToInt32(row[1]);
      if (Array.Exists<int>(ElectricalGuids.ElementListTypes, (Predicate<int>) (x => x == typeID)))
        documents.Add(new Tuple<long, int>(Convert.ToInt64(row[0]), typeID));
    }
    return documents;
  }

  protected override bool NeedCreateVersion(
    IUserSession session,
    long parentArticleID,
    long parentVersionDocumentID)
  {
    IDBAttribute attributeById = session.GetRelation(parentArticleID, parentVersionDocumentID, true).GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545"));
    return attributeById != null && attributeById.AsInteger != 0L;
  }
}
