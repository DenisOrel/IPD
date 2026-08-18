// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.CheckinCommandOptionsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class CheckinCommandOptionsProvider : ServiceExtender
{
  private IWorkCopyCommandOptions commandOptionsService;
  private ExtendedSaveHelper extendedSaveHelper;

  public CheckinCommandOptionsProvider(
    IWorkCopyCommandOptions commandOptionsService,
    ExtendedSaveHelper extendedSaveHelper)
  {
    this.commandOptionsService = commandOptionsService;
    this.extendedSaveHelper = extendedSaveHelper;
  }

  protected override void DoEnable()
  {
    base.DoEnable();
    this.commandOptionsService.CollectCheckinOptions += new EventHandler<WorkCopyCommandOptionsEventArgs>(this.OnCollectCheckinOptions);
  }

  protected override void DoDisable()
  {
    base.DoDisable();
    this.commandOptionsService.CollectCheckinOptions -= new EventHandler<WorkCopyCommandOptionsEventArgs>(this.OnCollectCheckinOptions);
  }

  private void OnCollectCheckinOptions(object sender, WorkCopyCommandOptionsEventArgs e)
  {
    ICollection<int> supportedObjectTypes = this.extendedSaveHelper.SupportedObjectTypes;
    bool flag = false;
    for (int index = 0; index < e.Items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) e.Items.GetItemData(index, typeof (IDBTypedObjectID));
      if (supportedObjectTypes.Contains(itemData.ObjectType))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      return;
    ExtendedSaveOptions extendedSaveOptions = new ExtendedSaveOptions(SaveChangesMode.Checkin);
    ExtendedSaveOptionsEditor saveOptionsEditor = new ExtendedSaveOptionsEditor();
    saveOptionsEditor.BindToOptions(extendedSaveOptions);
    e.ContextServices.AddService(typeof (ExtendedSaveOptions), (object) extendedSaveOptions);
    e.ContextServicesEditors.Add((WorkCopyCommandOptionsEditor) saveOptionsEditor);
  }
}
