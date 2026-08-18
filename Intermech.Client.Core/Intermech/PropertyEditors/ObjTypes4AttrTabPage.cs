
// Type: Intermech.PropertyEditors.ObjTypes4AttrTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjTypes4AttrTabPage.</summary>
public class ObjTypes4AttrTabPage : BaseTabPage
{
  private ObjTypes4AttrForm _ObjTypes4AttrForm;

  public ObjTypes4AttrTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_143"))
  {
    this._ObjTypes4AttrForm = PropertyFormsHolder.PropertyForms(this.instGuid).ObjTypes4AttrForm;
  }

  public override void DockToPanel(Panel panel) => this._ObjTypes4AttrForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._ObjTypes4AttrForm;
}
