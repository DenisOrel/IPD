// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AllUsersNode
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class AllUsersNode : GroupNode
{
  public AllUsersNode()
    : base(LocalizationHolder.rm.GetString("Workflow.Design_96"), 0L)
  {
    this.ImageIndex = Holder.GroupImageIndex;
    this.SelectedImageIndex = this.ImageIndex;
    this.Nodes.Add((TreeNode) new EmptyNode());
  }

  public override ConditionStructure[] GetConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(0, RelationalOperators.NotEntersInType, (object) wfConsts.GroupTypeID, LogicalOperators.AND, 0, false)
    };
  }
}
