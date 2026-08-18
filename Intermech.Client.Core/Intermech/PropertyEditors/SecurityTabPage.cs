
// Type: Intermech.PropertyEditors.SecurityTabPage
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

/// <summary>Summary description for SecurityTabPage.</summary>
public class SecurityTabPage : BaseTabPage
{
  private SecurityForm _SecurityForm;

  public SecurityTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_154"))
  {
    this._SecurityForm = PropertyFormsHolder.PropertyForms(this.instGuid).SecurityForm;
  }

  public override void DockToPanel(Panel panel) => this._SecurityForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._SecurityForm;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityTabPage));
    this.SuspendLayout();
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.Tag = (object) "  ";
    this.ResumeLayout(false);
  }
}
