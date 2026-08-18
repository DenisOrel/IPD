// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.OutboxNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;

#nullable disable
namespace Intermech.Workflow.Client;

public class OutboxNode : MailBoxNode
{
  public static ConditionStructure[] StaticConditions
  {
    get
    {
      return new ConditionStructure[4]
      {
        new ConditionStructure(wfConsts.AttrSenderID, RelationalOperators.Equal, (object) wfConsts.UserID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.NotEmpty, (object) wfConsts.UserID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrSenderDeletionID, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.OR, 1, false),
        new ConditionStructure(wfConsts.AttrSenderDeletionID, RelationalOperators.Equal, (object) 0, LogicalOperators.AND, -1, false)
      };
    }
  }

  public OutboxNode()
    : base(MailType.Sent)
  {
  }

  public override ConditionStructure[] Conditions => OutboxNode.StaticConditions;

  public override INodePart GetPart(IConditionsProvider conditionProvider)
  {
    return (INodePart) new MailObjectsPart(this.ElementsTypeID, this.Conditions, Intermech.Navigator.Consts.CategoryMailOutbox, conditionProvider, this.Services);
  }
}
