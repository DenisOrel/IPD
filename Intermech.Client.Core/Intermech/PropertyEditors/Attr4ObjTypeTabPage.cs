
// Type: Intermech.PropertyEditors.Attr4ObjTypeTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for Attr4ObjTypeTabPage.</summary>
public class Attr4ObjTypeTabPage : BaseTabPage
{
  private Attr4ObjTypeForm _Attr4ObjTypeForm;

  public Attr4ObjTypeTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_54"))
  {
    this._Attr4ObjTypeForm = PropertyFormsHolder.PropertyForms(this.instGuid).Attr4ObjTypeForm;
  }

  public override void DockToPanel(Panel panel) => this._Attr4ObjTypeForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._Attr4ObjTypeForm;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Attr4ObjTypeTabPage));
    this.SuspendLayout();
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.ResumeLayout(false);
  }
}
