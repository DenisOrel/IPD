
// Type: Intermech.PropertyEditors.Attr4RelTypeTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for Attr4RelTypeTabPage.</summary>
public class Attr4RelTypeTabPage : BaseTabPage
{
  private Attr4RelTypeForm _Attr4RelTypeForm;

  public Attr4RelTypeTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_54"))
  {
    this._Attr4RelTypeForm = PropertyFormsHolder.PropertyForms(this.instGuid).Attr4RelTypeForm;
  }

  public override void DockToPanel(Panel panel) => this._Attr4RelTypeForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._Attr4RelTypeForm;
}
