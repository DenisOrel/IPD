// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.TrashNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;

#nullable disable
namespace Intermech.Workflow.Client;

public class TrashNode : MailBoxNode
{
  public static ConditionStructure[] StaticConditions
  {
    get
    {
      return new ConditionStructure[4]
      {
        new ConditionStructure(wfConsts.AttrSenderID, RelationalOperators.Equal, (object) null, (object) wfConsts.UserID, LogicalOperators.AND, 2, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrSenderDeletionID, RelationalOperators.Equal, (object) 1, LogicalOperators.OR, -1, false),
        new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.Equal, (object) wfConsts.UserID, (object) null, LogicalOperators.AND, 1, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrRecipDeletionID, RelationalOperators.Equal, (object) 1, LogicalOperators.NONE, -2, false)
      };
    }
  }

  public TrashNode()
    : base(MailType.Trash)
  {
  }

  public override ConditionStructure[] Conditions => TrashNode.StaticConditions;

  public override INodePart GetPart(IConditionsProvider conditionProvider)
  {
    return (INodePart) new MailObjectsPart(this.ElementsTypeID, this.Conditions, Intermech.Navigator.Consts.CategoryMailTrash, conditionProvider, this.Services);
  }
}
