// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.ExtendedSaveCommand
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.AVS;
using Intermech.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.UI;
using System;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class ExtendedSaveCommand : ObjectCommand
{
  private int objectTypeId;
  private string objectCaption;

  public ExtendedSaveCommand()
    : base(MenuConsts.ExtendedSaveCommandName)
  {
    this.ObjectTypeId = -1;
    this.DisplayName = LocalizationHolder.rm.GetString("Tools.Client_112");
  }

  public int ObjectTypeId
  {
    get => this.objectTypeId;
    set => this.objectTypeId = value;
  }

  public string ObjectCaption
  {
    get => this.objectCaption;
    set => this.objectCaption = value;
  }

  private void ValidateProperties()
  {
    if (this.ObjectTypeId == -1)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ObjectTypeId");
    if (string.IsNullOrEmpty(this.ObjectCaption))
      throw PropertyExceptions.PropertyNotSetException((object) this, "ObjectCaption");
  }

  protected override void DoExecute()
  {
    this.ValidateProperties();
    IntegratorObject integrator = IntegratorServices.Find(this.ObjectTypeId);
    if (integrator == null)
      throw new InvalidOperationException($"Объект IPS '{this.ObjectCaption}' (ид. версии {this.ObjectId}) не поддерживает команду '{this.DisplayName}'.");
    ExtendedSaveResult res = (ExtendedSaveResult) null;
    IExtendedSaveSupport saveSvc = IntegratorServices.GetService<IExtendedSaveSupport>(integrator, true);
    ProgressSinks.DialogService.Invoke($"Расширенное сохранение в {this.ObjectCaption}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => res = saveSvc.CaptureChanges(this.ObjectId, new ExtendedSaveOptions(SaveChangesMode.Default)
    {
      ProgressSink = progressSink
    })));
    if (res != null && res.IsSuccessful && res.AffectedObjectIds != null && res.OpenObjects)
    {
      foreach (long affectedObjectId in res.AffectedObjectIds)
        AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(affectedObjectId));
    }
    if (res?.Errors == null || res.Errors.Count <= 0)
      return;
    string category = "Пересоздание ПЭ";
    IOutputView service = (IOutputView) ServicesManager.GetService(typeof (IOutputView));
    if (service == null)
      return;
    foreach (string error in res.Errors)
      service.WriteString(category, error);
    service.ShowView();
    service.Activate(category);
  }
}
