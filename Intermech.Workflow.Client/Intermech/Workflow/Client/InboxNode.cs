// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.InboxNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core.Organizer;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

public class InboxNode : MailBoxNode, INodeNotifications, IOrganizerConditionNode
{
  private ConditionStructure[] extConditions;

  public static ConditionStructure[] StaticConditions
  {
    get
    {
      return new ConditionStructure[5]
      {
        new ConditionStructure(wfConsts.AttrRecipID, RelationalOperators.Equal, (object) wfConsts.UserID, (object) null, LogicalOperators.AND, 1, false, AttributeSourceTypes.Auto, ColumnContents.ID),
        new ConditionStructure(wfConsts.AttrRecipDeletionID, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.OR, 1, false),
        new ConditionStructure(wfConsts.AttrRecipDeletionID, RelationalOperators.Equal, (object) 0, LogicalOperators.AND, -1, false),
        new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.Equal, (object) 4, LogicalOperators.OR, 1, false),
        new ConditionStructure(-7, RelationalOperators.In, (object) wfConsts.MessageTypeIDs.ToArray(), LogicalOperators.AND, -2, false)
      };
    }
  }

  public override ConditionStructure[] Conditions
  {
    get => this.extConditions != null ? this.extConditions : InboxNode.StaticConditions;
  }

  public override INodePart GetPart(IConditionsProvider conditionProvider)
  {
    return (INodePart) new MailObjectsPart(this.ElementsTypeID, this.Conditions, Intermech.Navigator.Consts.CategoryMailInbox, conditionProvider, this.Services);
  }

  public InboxNode()
    : base(MailType.Inbox)
  {
  }

  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    return e.EventName == "UnreadCountChanged" ? ProcessResult.RefreshNodeFields : ProcessResult.None;
  }

  public void SetCondition(ConditionStructure[] conditions)
  {
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>((IEnumerable<ConditionStructure>) InboxNode.StaticConditions);
    conditionStructureList.AddRange((IEnumerable<ConditionStructure>) conditions);
    this.extConditions = conditionStructureList.ToArray();
  }
}
