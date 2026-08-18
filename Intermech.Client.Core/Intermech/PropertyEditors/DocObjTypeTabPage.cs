
// Type: Intermech.PropertyEditors.DocObjTypeTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class DocObjTypeTabPage : BaseTabPage
{
  private DocObjTypeForm _DocObjTypeForm;

  public DocObjTypeTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_91"))
  {
    this._DocObjTypeForm = PropertyFormsHolder.PropertyForms(this.instGuid).DocObjTypeForm;
  }

  public override void DockToPanel(Panel panel) => this._DocObjTypeForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._DocObjTypeForm;
}
