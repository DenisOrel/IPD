// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.ActivityTypeItem
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

#nullable disable
namespace Intermech.Workflow.Client;

internal sealed class ActivityTypeItem
{
  public int ID { get; private set; }

  public string Caption { get; private set; }

  public ActivityTypeItem(int activityTypeID, string caption)
  {
    this.ID = activityTypeID;
    this.Caption = caption;
  }

  public override string ToString() => this.Caption;
}
