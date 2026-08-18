// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailDescriptor
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailDescriptor : HiveDescriptor
{
  public MailDescriptor()
    : base(Intermech.Navigator.Consts.CategoryMail, 0, LocalizationHolder.rm.GetString("Workflow.Client_10"))
  {
  }

  protected MailDescriptor(PersistentState state)
    : this()
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
