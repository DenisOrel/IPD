
// Type: Intermech.PropertyEditors.LCSchema4ObjTypeTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for LCSchema4ObjTypeTabPage.</summary>
public class LCSchema4ObjTypeTabPage : BaseTabPage
{
  private TabPageForm _LCSchema4ObjTypeForm;

  public LCSchema4ObjTypeTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_109"))
  {
    this._LCSchema4ObjTypeForm = PropertyFormsHolder.PropertyForms(this.instGuid).LCSchema4ObjTypeForm;
  }

  public override void DockToPanel(Panel panel) => this._LCSchema4ObjTypeForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._LCSchema4ObjTypeForm;
}
