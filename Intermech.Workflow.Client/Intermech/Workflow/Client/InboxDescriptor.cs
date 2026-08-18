// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.InboxDescriptor
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;

#nullable disable
namespace Intermech.Workflow.Client;

internal class InboxDescriptor : HiveDescriptor
{
  private string _initialCaption = "";
  private long _unreadCount;

  public InboxDescriptor()
    : base(Intermech.Navigator.Consts.CategoryMailInbox, 0, LocalizationHolder.rm.GetString("Workflow.Client_5"))
  {
    this._initialCaption = this._caption;
  }

  protected InboxDescriptor(PersistentState state)
    : this()
  {
  }

  public long UnreadCount
  {
    get => this._unreadCount;
    set
    {
      if (this._unreadCount == value)
        return;
      this._unreadCount = value;
      string str = "";
      if (this._unreadCount > 0L)
        str = $" ({this._unreadCount})";
      this._caption = this._initialCaption + str;
      NotificationEventArgs e = new NotificationEventArgs("UnreadCountChanged");
      BaseHolder.NotificationService.FireEvent((object) null, e);
    }
  }

  public override INodeID ParseAddress(string address)
  {
    int length = address.IndexOf('*');
    if (length != -1)
    {
      address = address.Substring(0, length);
      if (this._caption.StartsWith(address))
        return this.GetRecordNodeID();
    }
    return base.ParseAddress(address);
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
