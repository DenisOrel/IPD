
// Type: Intermech.PropertyEditors.ObjTypeApplTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjTypeApplTabPage.</summary>
public class ObjTypeApplTabPage : BaseTabPage
{
  private ObjTypeApplForm _ObjTypeApplForm;

  public ObjTypeApplTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_142"))
  {
    this._ObjTypeApplForm = PropertyFormsHolder.PropertyForms(this.instGuid).ObjTypeApplForm;
  }

  public override void DockToPanel(Panel panel) => this._ObjTypeApplForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._ObjTypeApplForm;
}
