// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.RelationFinder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class RelationFinder
{
  protected readonly IDBObjectType objectType;
  protected readonly IDBRelationsApplicabilityCollection aplicabilities;

  public RelationFinder(
    IDBObjectType objectType,
    IDBRelationsApplicabilityCollection aplicabilities)
  {
    this.objectType = objectType;
    this.aplicabilities = aplicabilities;
  }

  public IList<LinkedObject> Find(IUserSession session, int anotherTypeID, long objectID)
  {
    List<LinkedObject> linkedObjectList = new List<LinkedObject>();
    DataTable applicabilitiesList = this.GetApplicabilitiesList(anotherTypeID);
    if (applicabilitiesList.Rows.Count > 0)
    {
      foreach (DataRow row1 in (InternalDataCollectionBase) applicabilitiesList.Rows)
      {
        if (Convert.ToInt32(row1["F_MIN_LINKS"]) != -1)
        {
          IDBRelationCollection relationCollection = session.GetRelationCollection(Convert.ToInt32(row1["F_RELATION_TYPE"]));
          if (MetaDataHelper.IsLocalObjectType(anotherTypeID))
            relationCollection.LocalTypesMode = true;
          DataTable applicabilityTable = this.GetApplicabilityTable(relationCollection, anotherTypeID, objectID);
          if (applicabilityTable.Rows.Count != 0)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) applicabilityTable.Rows)
              linkedObjectList.Add(new LinkedObject(Convert.ToInt64(row2[0]), Convert.ToInt64(row2[1])));
          }
        }
      }
    }
    return (IList<LinkedObject>) linkedObjectList;
  }

  protected abstract int ObjectColumn { get; }

  protected DBRecordSetParams GetSelectParams(int anotherTypeID)
  {
    return new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) anotherTypeID, LogicalOperators.AND, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) this.ObjectColumn, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    });
  }

  protected abstract DataTable GetApplicabilitiesList(int anotherTypeID);

  protected abstract DataTable GetApplicabilityTable(
    IDBRelationCollection relationCollection,
    int anotherTypeID,
    long objectID);
}
