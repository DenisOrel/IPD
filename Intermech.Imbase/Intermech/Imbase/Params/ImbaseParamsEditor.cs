// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ImbaseParamsEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Params;

public class ImbaseParamsEditor : IPropertyPage, ISortedPropertyGrid, IPropertyPageSearchOptionEvents
{
  private ImbaseParamsDescriptor _objectDescriptor;

  public event EventHandler Changed;

  public ImbaseParamsEditor()
  {
    this.PageName = LocalizationHolder.rm.GetString("Imbase_Common");
    this.HeaderText = this.PageName;
  }

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      return (object) this._objectDescriptor ?? (object) (this._objectDescriptor = new ImbaseParamsDescriptor(new ImbaseParamsContainer(ImbaseParamsHelper.CommonParams, ImbaseParamsHelper.UserParams)));
    }
  }

  public string PageName { get; }

  public void Apply()
  {
    ImbaseParamsHelper.SaveParams();
    this._objectDescriptor?.ResetOldValues();
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  public void Cancel()
  {
    ImbaseParamsHelper.LoadParams();
    this._objectDescriptor = new ImbaseParamsDescriptor(new ImbaseParamsContainer(ImbaseParamsHelper.CommonParams, ImbaseParamsHelper.UserParams));
  }

  public string HelpTopicID { get; } = string.Empty;

  public string HeaderText { get; }

  public PropertySort Sort => PropertySort.Categorized;

  public List<string> GetOptionNames()
  {
    return !(this.Control is ImbaseParamsDescriptor control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  internal static void RegisterSettingsPage()
  {
    ServiceUtils.GetService<IPropertyPagesService>((object) ApplicationServices.Container, false)?.AddPage(LocalizationHolder.rm.GetString("Imbase_Common_Settings"), (IPropertyPage) new ImbaseParamsEditor());
  }
}
