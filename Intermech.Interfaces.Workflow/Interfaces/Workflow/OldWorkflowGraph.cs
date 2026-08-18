// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.OldWorkflowGraph
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Kernel.Search;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public class OldWorkflowGraph : XMLGraph
{
  public List<long> LinkIDs = new List<long>();
  public List<long> NodeIDs = new List<long>();
  public Dictionary<long, List<long>> CloneIDs = new Dictionary<long, List<long>>();
  public DataTable LinksTable;

  public void Load(Stream stream, long processID, IUserSession sess)
  {
    this.Load(stream);
    this.LinkIDs.Clear();
    this.NodeIDs.Clear();
    this.CloneIDs.Clear();
    if ((this.VersionFlags & VersionFlags.IncludeObjectGuids) != VersionFlags.None)
      return;
    IDBObjectCollection objectCollection1 = sess.GetObjectCollection(wfConsts.ActivitiesTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.Less, (object) 0, LogicalOperators.AND, 0, false);
    if (processID > 0L)
      conditionStructure.RelationalOperator = RelationalOperators.Greater;
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) Math.Abs(processID), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      conditionStructure
    };
    SortOrders sort = SortOrders.ASC;
    if (processID < 0L)
      sort = SortOrders.DESC;
    ColumnDescriptor[] columns1 = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, sort, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrParentActivityID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns1);
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectRefSelector(wfConsts.AttrProcessID, Math.Abs(processID));
    DataTable dataTable = objectCollection1.Select(paramSet);
    int index1 = 0;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      long key = 0;
      if (!row[2].Equals((object) DBNull.Value))
        key = Convert.ToInt64(row[2]);
      if (key == 0L)
      {
        this.NodeIDs.Add(int64);
        NameValueCollection nameValueCollection = index1 < this.Nodes.Count ? this.Nodes[index1] : (NameValueCollection) null;
        if (nameValueCollection == null)
        {
          nameValueCollection = new NameValueCollection();
          this.Nodes.Add(nameValueCollection);
        }
        nameValueCollection["ObjectID"] = int64.ToString();
        ++index1;
      }
      else
      {
        List<long> longList = (List<long>) null;
        if (!this.CloneIDs.TryGetValue(key, out longList))
        {
          longList = new List<long>();
          this.CloneIDs.Add(key, longList);
        }
        longList.Add(int64);
      }
    }
    IDBObjectCollection objectCollection2 = sess.GetObjectCollection(wfConsts.LinksTypeID);
    ColumnDescriptor[] columns2 = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, sort, 1),
      new ColumnDescriptor((object) wfConsts.AttrFromActivityID),
      new ColumnDescriptor((object) wfConsts.AttrToActivityID)
    };
    paramSet = new DBRecordSetParams(conditions, columns2);
    this.LinksTable = objectCollection2.Select(paramSet);
    int index2 = 0;
    foreach (DataRow row in (InternalDataCollectionBase) this.LinksTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      NameValueCollection nameValueCollection = index2 < this.Links.Count ? this.Links[index2] : (NameValueCollection) null;
      if (nameValueCollection == null)
      {
        nameValueCollection = new NameValueCollection();
        this.Links.Add(nameValueCollection);
      }
      nameValueCollection["ObjectID"] = int64.ToString();
      ++index2;
    }
  }
}
