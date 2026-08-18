// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCopyCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Commands;

public abstract class ObjectCopyCommand : ObjectCommand
{
  private long newObjectId;
  private ObjectCommandEventSite eventSite;

  public ObjectCopyCommand(string name, ObjectCommandEventSite eventSite)
    : base(name)
  {
    this.eventSite = eventSite != null ? eventSite : throw new ArgumentNullException(nameof (eventSite), LocalizationHolder.rm.GetString("Interfaces.Client_137"));
  }

  public long NewObjectId
  {
    get => this.newObjectId;
    set => this.newObjectId = value;
  }

  protected override void DoExecute()
  {
    this.eventSite.RaiseBefore((Command) this, new BeforeObjectCommandArgs(this.ObjectId));
    try
    {
      this.NewObjectId = this.DoReplaceObjectCopy(this.ObjectId);
      this.eventSite.RaiseAfter((Command) this, new AfterObjectCommandArgs(this.NewObjectId, this.ObjectId));
      this.eventSite.RaiseCleanup((Command) this, CleanupCommandArgs.Empty);
    }
    catch (Exception ex)
    {
      this.eventSite.RaiseCleanup((Command) this, new CleanupCommandArgs(ex));
      throw;
    }
  }

  protected abstract long DoReplaceObjectCopy(long currObjectId);
}
