// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.EcoPropertiesEditor
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.ECO.Client;

public class EcoPropertiesEditor : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IEcoProperties _control;
  private IEcoPropertiesService _service;

  public EcoPropertiesEditor()
  {
    if (ServicesManager.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service)
    {
      service.AddPage(LocalizationHolder.rm.GetString("ECO.Client_225"), (IPropertyPage) this);
      this._service = ServicesManager.GetService(typeof (IEcoPropertiesService)) as IEcoPropertiesService;
    }
    this._control = this._service.Current;
    if (this._control == null)
      return;
    this._control.Changed += new EventHandler(this.OnChanged);
  }

  private void OnChanged(object sender, EventArgs e)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed(sender, e);
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      return this._control != null ? (object) new ClassWrapperForPropertyGrid((object) this._control) : (object) null;
    }
  }

  public string PageName => LocalizationHolder.rm.GetString("ECO.Client_226");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this._service == null)
      return;
    this._service.Current = this._control;
  }

  public void Cancel()
  {
    IEcoPropertiesService service = this._service;
  }

  public string HelpTopicID => "1627";

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
