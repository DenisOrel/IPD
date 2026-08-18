// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.Client.EventsWrapper
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces.Client;

public class EventsWrapper : MarshalByRefObject
{
  public event EventHandler IndexCompletedEvent;

  public event EventHandler ItemMoveEvent;

  public event EventHandler MoveErrorEvent;

  public event EventHandler MoveCompleteEvent;

  public void LocallyEventHandler(object sender, EventArgs e)
  {
    if (!(e is FilesCopierEventArgs filesCopierEventArgs))
      return;
    switch (filesCopierEventArgs.EventName)
    {
      case "IndexComplete":
        if (this.IndexCompletedEvent == null)
          break;
        this.IndexCompletedEvent(sender, e);
        break;
      case "ItemMoved":
        if (this.ItemMoveEvent == null)
          break;
        this.ItemMoveEvent(sender, e);
        break;
      case "MoveError":
        if (this.MoveErrorEvent == null)
          break;
        this.MoveErrorEvent(sender, e);
        break;
      case "MoveComplete":
        if (this.MoveCompleteEvent == null)
          break;
        this.MoveCompleteEvent(sender, e);
        break;
    }
  }

  public override object InitializeLifetimeService() => (object) null;
}
