// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.GlobalMailSettingsPropertyPage
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Workflow.Client;

public class GlobalMailSettingsPropertyPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private GlobalMailSettingsClient _settings = new GlobalMailSettingsClient();
  private ClassWrapperForPropertyGrid _wrapper;
  private bool? _isAdmin;

  public GlobalMailSettingsPropertyPage()
  {
    this._settings.Assign(GlobalMailSettings.Cfg);
    this._wrapper = new ClassWrapperForPropertyGrid((object) this._settings);
    this._wrapper.GetReadOnly += new ClassWrapperForPropertyGrid.OnGetReadOnly(this.GetReadOnly);
  }

  private bool GetReadOnly(PropertyDescriptor prop)
  {
    if (!this._isAdmin.HasValue)
      this._isAdmin = new bool?((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin);
    return !this._isAdmin.Value;
  }

  public event EventHandler Changed;

  [Browsable(false)]
  public PropertyPageType Type => PropertyPageType.Object;

  [Browsable(false)]
  public object Control => (object) this._wrapper;

  [Browsable(false)]
  public string PageName => "Workflow";

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this.GetReadOnly((PropertyDescriptor) null))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      GlobalMailSettings.Cfg.Assign((GlobalMailSettings) this._settings);
      GlobalMailSettings.Cfg.Save(sessionKeeper.Session);
      if (!(sessionKeeper.Session.GetCustomService(typeof (IRouterService)) is IRouterService customService))
        return;
      customService.ReloadSettings(SettingsGroup.Base);
    }
  }

  public void Cancel() => this._settings.Assign(GlobalMailSettings.Cfg);

  [Browsable(false)]
  public string HelpTopicID => "";

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
