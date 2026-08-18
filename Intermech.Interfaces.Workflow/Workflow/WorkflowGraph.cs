// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.WorkflowGraph
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Workflow;

public class WorkflowGraph : Dictionary<long, ActivityNode>
{
  protected internal List<long> PreExecuted;
  private GraphOptions _options;
  private long _processID;
  private List<ActivityLink> _links = new List<ActivityLink>();
  private long FixedPrototypeSchemeID;
  private long _starterParentObjectID = -1;

  public WorkflowGraph(long processID, IUserSession session, GraphOptions options = GraphOptions.LoadAll)
  {
    this._processID = processID;
    this._options = options;
    this.Load(session);
  }

  public long ProcessID => this._processID;

  public Dictionary<long, ActivityNode>.ValueCollection Nodes => this.Values;

  public List<ActivityLink> Links => this._links;

  public bool HasOption(GraphOptions option) => (this._options & option) == option;

  private void Load(IUserSession session)
  {
    this.FixedPrototypeSchemeID = 0L;
    if (!this.HasOption(GraphOptions.SkipParent) && session.GetObjectInfo(this.ProcessID).ObjectTypeID == wfConsts.ProcessesTypeID)
    {
      IDBAttribute objectAttributeById1 = session.GetObjectAttributeByID(this.ProcessID, wfConsts.AttrCreateActivitiesOnDemandID);
      if (objectAttributeById1 != null && objectAttributeById1.AsBoolean)
      {
        IDBAttribute objectAttributeById2 = session.GetObjectAttributeByID(this.ProcessID, wfConsts.AttrPrototypeID);
        if (objectAttributeById2 != null)
          this.FixedPrototypeSchemeID = objectAttributeById2.AsInteger;
      }
    }
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(wfConsts.ActivitiesTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.Less, (object) 0, LogicalOperators.AND, 0, false);
    if (this.ProcessID > 0L)
      conditionStructure.RelationalOperator = RelationalOperators.Greater;
    List<long> ObjectIDs = new List<long>();
    ObjectIDs.Add(Math.Abs(this.ProcessID));
    if (this.FixedPrototypeSchemeID != 0L)
      ObjectIDs.Add(this.FixedPrototypeSchemeID);
    ConditionStructure[] array1 = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.In, (object) ObjectIDs.ToArray(), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      conditionStructure
    };
    SortOrders sort = SortOrders.ASC;
    if (this.ProcessID < 0L)
      sort = SortOrders.DESC;
    ColumnDescriptor[] array2 = new ColumnDescriptor[7]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, sort, 1),
      new ColumnDescriptor((object) wfConsts.AttrActivityStatusID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrParentActivityID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION),
      new ColumnDescriptor((object) wfConsts.AttrProcessID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, sort, 1)
    };
    int columnIndex1 = 0;
    if (this.HasOption(GraphOptions.LoadGraphData))
    {
      columnIndex1 = array2.Length;
      Array.Resize<ColumnDescriptor>(ref array2, columnIndex1 + 1);
      array2[columnIndex1] = new ColumnDescriptor((object) wfConsts.AttrGraphDataID);
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(array1, array2);
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectRefSelector(wfConsts.AttrProcessID, ObjectIDs);
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection1.Select(paramSet).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      ActivityStatus status = ActivityStatus.OnApproach;
      if (!row[1].Equals((object) DBNull.Value))
        status = (ActivityStatus) Convert.ToInt32(row[1]);
      long num1 = 0;
      if (!row[2].Equals((object) DBNull.Value))
        num1 = Convert.ToInt64(row[2]);
      if (num1 == 0L)
        num1 = int64;
      long num2 = Convert.ToInt64(row[5]);
      if (num2 == -this.ProcessID)
        num2 = this.ProcessID;
      string objectGuid = row[6].ToString();
      ActivityNode activityNode;
      if (!this.ContainsKey(num1))
      {
        activityNode = new ActivityNode(this, int64, DBNull.Value.Equals(row[4]) ? "" : row[4].ToString(), status, Convert.ToInt32(row[3]), num1, objectGuid);
        activityNode.ProcessID = num2;
        this.Add(num1, activityNode);
      }
      else
      {
        activityNode = this[num1];
        if (activityNode.ProcessID != num2)
        {
          activityNode.ObjectIDs[0] = int64;
          activityNode.Statuses[0] = status;
          activityNode.ProcessID = num2;
        }
        else
        {
          activityNode.ObjectIDs.Add(int64);
          activityNode.Statuses.Add(status);
        }
      }
      if (columnIndex1 > 0 && activityNode._graphData == null)
        activityNode._graphData = new ActivityGraphData(!row[columnIndex1].Equals((object) DBNull.Value) ? row[columnIndex1].ToString() : "");
    }
    this._links.Clear();
    if (!this.HasOption(GraphOptions.LoadLinks))
      return;
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(wfConsts.LinksTypeID);
    if (!this.HasOption(GraphOptions.LoadBackLinks))
    {
      Array.Resize<ConditionStructure>(ref array1, array1.Length + 1);
      array1[array1.Length - 1] = new ConditionStructure(wfConsts.AttrLinkKindID, RelationalOperators.NotEqual, (object) 1, LogicalOperators.AND, 0, true);
    }
    ColumnDescriptor[] array3 = new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, sort, 1),
      new ColumnDescriptor((object) wfConsts.AttrFromActivityID),
      new ColumnDescriptor((object) wfConsts.AttrToActivityID),
      new ColumnDescriptor((object) wfConsts.AttrLinkKindID)
    };
    int columnIndex2 = 0;
    if (this.HasOption(GraphOptions.LoadGraphData))
    {
      columnIndex2 = array3.Length;
      Array.Resize<ColumnDescriptor>(ref array3, columnIndex2 + 1);
      array3[columnIndex2] = new ColumnDescriptor((object) wfConsts.AttrGraphDataID);
    }
    paramSet = new DBRecordSetParams(array1, array3);
    foreach (DataRow row in (InternalDataCollectionBase) objectCollection2.Select(paramSet).Rows)
    {
      long int64_1 = Convert.ToInt64(row[1]);
      long int64_2 = Convert.ToInt64(row[2]);
      if (this.ProcessID < 0L)
      {
        int64_1 *= -1L;
        int64_2 *= -1L;
      }
      if (this.ContainsKey(int64_1) && this.ContainsKey(int64_2))
      {
        ActivityNode from = this[int64_1];
        ActivityNode activityNode = this[int64_2];
        ActivityLink l = new ActivityLink(Convert.ToInt64(row[0]), (LinkKind) Convert.ToInt32(row[3]), from, activityNode);
        if (l.Kind == LinkKind.ParallelBlock)
          activityNode.IsParallelBlockFinish = true;
        if (columnIndex2 > 0)
          l._graphData = new GraphData(!row[columnIndex2].Equals((object) DBNull.Value) ? row[columnIndex2].ToString() : "");
        from.AddLink(l, activityNode);
        this._links.Add(l);
      }
    }
  }

  protected void ClearActivityChecks()
  {
    foreach (KeyValuePair<long, ActivityNode> keyValuePair in (Dictionary<long, ActivityNode>) this)
      keyValuePair.Value.Checked = false;
  }

  protected bool IsAllPreviousCompleted(ActivityNode act, bool checkSelf)
  {
    if (act != null && !act.Checked)
    {
      act.Checked = true;
      if (checkSelf && !act.Completed)
        return false;
      if (act.Completed && act.ObjectType == wfConsts.StartTypeID || act.ParentActivityID != this._starterParentObjectID && act.CompletedWithoutApproach && act.ObjectType == wfConsts.CaseTypeID)
        return true;
      foreach (ActivityLink activityLink in act.Prev)
      {
        if (!this.IsAllPreviousCompleted(activityLink.From, true))
          return false;
      }
    }
    return true;
  }

  public bool IsAllPreviousCompleted(long objectID)
  {
    long key = Math.Abs(objectID);
    ActivityNode act = this[key];
    this._starterParentObjectID = key;
    this.ClearActivityChecks();
    return this.IsAllPreviousCompleted(act, false);
  }

  public bool IsAllCompleted(long exceptObjectID)
  {
    foreach (KeyValuePair<long, ActivityNode> keyValuePair in (Dictionary<long, ActivityNode>) this)
    {
      if (!keyValuePair.Value.ObjectIDs.Contains(exceptObjectID) && !keyValuePair.Value.Completed && keyValuePair.Value.ObjectType != wfConsts.TimerTypeID && keyValuePair.Value.ObjectType != wfConsts.StopTypeID)
        return false;
    }
    return true;
  }
}
