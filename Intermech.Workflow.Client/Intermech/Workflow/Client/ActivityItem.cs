// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.ActivityItem
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

internal sealed class ActivityItem
{
  public List<long> ObjectIDs { get; private set; }

  public int TypeID { get; private set; }

  public string Caption { get; private set; }

  public ActivityItem(long objectID, int typeID, string caption)
  {
    this.ObjectIDs = new List<long>((IEnumerable<long>) new long[1]
    {
      objectID
    });
    this.TypeID = typeID;
    this.Caption = caption;
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ActivityItem activityItem))
      return base.Equals(obj);
    return this.TypeID == activityItem.TypeID && this.Caption.Equals(activityItem.Caption);
  }

  public override string ToString() => this.Caption;
}
