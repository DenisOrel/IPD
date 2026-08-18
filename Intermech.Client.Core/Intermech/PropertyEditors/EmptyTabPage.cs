
// Type: Intermech.PropertyEditors.EmptyTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for EmptyTabPage.</summary>
public class EmptyTabPage : BaseTabPage
{
  private EmptyForm _EmptyForm;

  public EmptyTabPage(Guid aInstGuid)
    : base(aInstGuid, "")
  {
    this._EmptyForm = PropertyFormsHolder.PropertyForms(this.instGuid).EmptyForm;
  }

  public override void DockToPanel(Panel panel) => this._EmptyForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._EmptyForm;
}
