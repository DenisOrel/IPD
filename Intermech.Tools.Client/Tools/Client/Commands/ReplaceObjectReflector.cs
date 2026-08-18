// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ReplaceObjectReflector
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal class ReplaceObjectReflector : ObjectCommandReflector
{
  private IReplaceFilePolicy refreshPolicy;

  public ReplaceObjectReflector(ObjectCommandEventSite eventSite, IReplaceFilePolicy refreshPolicy)
    : base(eventSite)
  {
    this.refreshPolicy = refreshPolicy != null ? refreshPolicy : throw new ArgumentNullException();
  }

  protected override void OnAfterCommand(ObjectCommand command, AfterObjectCommandArgs e)
  {
    base.OnAfterCommand(command, e);
    this.AfterReplace(command, e);
  }

  private void AfterReplace(ObjectCommand command, AfterObjectCommandArgs e)
  {
    if (!e.IsObjectCopyReplaced || !IntegratorServices.GetFileHandlingRules(DBHelper.GetObjectType(e.ObjectId)).RequireNormalEditMode)
      return;
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    if (service.WorkArea.FindPublishedObjectByVersionId(e.OldObjectId) == null)
      return;
    service.WorkArea.Publish((IList<DBObjectState>) service.DBObjectsInfo.CreateStateListForSingleObject(e.ObjectId), this.refreshPolicy);
  }
}
