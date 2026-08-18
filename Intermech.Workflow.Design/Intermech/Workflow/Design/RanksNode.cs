// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.RanksNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class RanksNode : BaseNode
{
  public RanksNode()
    : base(LocalizationHolder.rm.GetString("Workflow.Design_97"), Holder.RankImageIndex, Holder.RankImageIndex)
  {
    this.Nodes.Add((TreeNode) new EmptyNode());
  }

  public override void DoExpand()
  {
    if (!(this.FirstNode is EmptyNode))
      return;
    this.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionKeeper.Session.GetObjectCollection(wfConsts.RanksTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.CAPTION
      }, (object[]) null, (SortOrders[]) null)).Rows)
        this.Nodes.Add((TreeNode) new RankNode(row[1].ToString(), Convert.ToInt64(row[0])));
    }
  }
}
