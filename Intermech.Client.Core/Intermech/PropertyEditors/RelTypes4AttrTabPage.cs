
// Type: Intermech.PropertyEditors.RelTypes4AttrTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for RelTypes4AttrTabPage.</summary>
public class RelTypes4AttrTabPage : BaseTabPage
{
  private RelTypes4AttrForm _RelTypes4AttrForm;

  public RelTypes4AttrTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_153"))
  {
    this._RelTypes4AttrForm = PropertyFormsHolder.PropertyForms(this.instGuid).RelTypes4AttrForm;
  }

  public override void DockToPanel(Panel panel) => this._RelTypes4AttrForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._RelTypes4AttrForm;
}
