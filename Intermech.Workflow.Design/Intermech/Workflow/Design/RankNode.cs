// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.RankNode
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

internal class RankNode : GroupNode
{
  private DBRecordSetParams GetParams(int recordCount)
  {
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrUserRankID, RelationalOperators.Equal, (object) this.ID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false)
    };
    object[] columns;
    if (recordCount == 1)
      columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
    else
      columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      };
    return new DBRecordSetParams(conditions, columns, 0L, (object) null, recordCount);
  }

  public RankNode(string text, long id)
    : base(text, id, Holder.GroupImageIndex)
  {
    this.ImageIndex = Holder.RankImageIndex;
    this.SelectedImageIndex = this.ImageIndex;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectCollection(wfConsts.UserTypeID).Select(this.GetParams(1)).Rows.Count <= 0)
        return;
      this.Nodes.Add((TreeNode) new EmptyNode());
    }
  }

  public override void DoExpand()
  {
    if (!(this.FirstNode is EmptyNode))
      return;
    this.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectCollection(wfConsts.UserTypeID).Select(this.GetParams(-1)).Rows)
        this.Nodes.Add((TreeNode) new UserNode(row[1].ToString(), Convert.ToInt64(row[0])));
    }
  }
}
