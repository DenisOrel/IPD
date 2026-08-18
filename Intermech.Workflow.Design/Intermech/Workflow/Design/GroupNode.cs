// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.GroupNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class GroupNode : BaseNode
{
  protected long _id;

  public long ID => this._id;

  private bool GroupsOnly => this.TreeView is UsersTreeView treeView && treeView.GroupsOnly;

  public GroupNode(string text, long id, int imageIndex)
    : base(text, imageIndex, imageIndex)
  {
    this._id = id;
  }

  public GroupNode(string text, long id)
    : this(text, id, Holder.GroupImageIndex)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams paramSet1 = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(0, RelationalOperators.EntersIn, (object) this.ID, LogicalOperators.AND, 0, false),
        new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false)
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      }, 0L, (object) null, 1);
      if (!this.GroupsOnly && sessionKeeper.Session.GetObjectCollection(wfConsts.UserTypeID).Select(paramSet1).Rows.Count > 0)
        this.Nodes.Add((TreeNode) new EmptyNode());
      if (this.Nodes.Count != 0)
        return;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(wfConsts.GroupTypeID);
      Array.Resize<ConditionStructure>(ref paramSet1.Conditions, paramSet1.Conditions.Length - 1);
      DBRecordSetParams paramSet2 = paramSet1;
      if (objectCollection.Select(paramSet2).Rows.Count <= 0)
        return;
      this.Nodes.Add((TreeNode) new EmptyNode());
    }
  }

  public long ParentID => this.Parent is GroupNode ? ((GroupNode) this.Parent).ID : 0L;

  public virtual ConditionStructure[] GetConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.EntersIn, (object) this.ID, LogicalOperators.AND, 0, false)
    };
  }

  public override void DoExpand()
  {
    if (!(this.FirstNode is EmptyNode))
      return;
    this.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection1 = sessionKeeper.Session.GetObjectCollection(wfConsts.GroupTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(this.GetConditions(), new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.CAPTION
      }, new SortOrders[1]{ SortOrders.ASC });
      paramSet.RecordCount = -1;
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection1.Select(paramSet).Rows)
        this.Nodes.Add((TreeNode) new GroupNode(row[1].ToString(), Convert.ToInt64(row[0])));
      if (this.GroupsOnly)
        return;
      IDBObjectCollection objectCollection2 = sessionKeeper.Session.GetObjectCollection(wfConsts.UserTypeID);
      Array.Resize<ConditionStructure>(ref paramSet.Conditions, paramSet.Conditions.Length + 1);
      paramSet.Conditions[paramSet.Conditions.Length - 1] = new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false);
      DataTable dataTable = objectCollection2.Select(paramSet);
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        this.Nodes.Add((TreeNode) new UserNode(row[1].ToString(), Convert.ToInt64(row[0])));
        ++num;
      }
    }
  }
}
