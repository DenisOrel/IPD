// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailNode : CompositeNode
{
  private static DescriptorCollection _mailBoxes;
  private const int InboxOrderID = 10;
  private const int OutboxOrderID = 20;
  private const int CompletedOrderID = 30;
  private const int TrashOrderID = 40;
  private static InboxDescriptor _inboxDescriptor;

  internal static InboxDescriptor InboxDescriptor => MailNode._inboxDescriptor;

  public static void Init()
  {
    if (MailNode._inboxDescriptor != null)
      return;
    MailNode._inboxDescriptor = new InboxDescriptor();
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(MailNode.MailBoxes, false));
  }

  private static DescriptorCollection MailBoxes
  {
    get
    {
      if (MailNode._mailBoxes == null)
      {
        MailNode._mailBoxes = new DescriptorCollection();
        MailNode._mailBoxes.Add(Intermech.Navigator.Consts.CategoryMailInboxGuid, (IDescriptor) MailNode._inboxDescriptor);
        MailNode._mailBoxes.Add(Intermech.Navigator.Consts.CategoryMailOutboxGuid, (IDescriptor) new OutboxDescriptor());
        MailNode._mailBoxes.Add(Intermech.Navigator.Consts.CategoryMailProcessedGuid, (IDescriptor) new CompletedDescriptor());
        MailNode._mailBoxes.Add(Intermech.Navigator.Consts.CategoryMailTrashGuid, (IDescriptor) new TrashDescriptor());
      }
      return MailNode._mailBoxes;
    }
  }
}
