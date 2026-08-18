// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.CompletedDescriptor
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;

#nullable disable
namespace Intermech.Workflow.Client;

internal class CompletedDescriptor : HiveDescriptor
{
  public CompletedDescriptor()
    : base(Intermech.Navigator.Consts.CategoryMailProcessed, 0, LocalizationHolder.rm.GetString("Workflow.Client_2"))
  {
  }

  protected CompletedDescriptor(PersistentState state)
    : this()
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
