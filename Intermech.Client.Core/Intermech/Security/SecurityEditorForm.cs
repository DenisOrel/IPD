
// Type: Intermech.Security.SecurityEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Security;

/// <summary>Summary description for SecurityForm.</summary>
public class SecurityEditorForm : Form
{
  private Button okBtn;
  private Button cancelBtn;
  private SecurityControl securityControl;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public SecurityEditorForm() => this.InitializeComponent();

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SecurityEditorForm));
    this.okBtn = new Button();
    this.cancelBtn = new Button();
    this.securityControl = new SecurityControl();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okBtn, "okBtn");
    this.okBtn.DialogResult = DialogResult.OK;
    this.okBtn.Name = "okBtn";
    this.okBtn.Click += new EventHandler(this.okBtn_Click);
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.DialogResult = DialogResult.Cancel;
    this.cancelBtn.Name = "cancelBtn";
    componentResourceManager.ApplyResources((object) this.securityControl, "securityControl");
    this.securityControl.FocusedUserId = (object) null;
    this.securityControl.Name = "securityControl";
    this.securityControl.Readonly = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.cancelBtn;
    this.Controls.Add((Control) this.cancelBtn);
    this.Controls.Add((Control) this.okBtn);
    this.Controls.Add((Control) this.securityControl);
    this.Name = nameof (SecurityEditorForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.SecurityEditorForm_Load);
    this.Closed += new EventHandler(this.SecurityEditorForm_Closed);
    this.ResumeLayout(false);
  }

  public void Execute(object[] aId, ISecurityCallback aISecurityCallback, bool aReadonly)
  {
    this.securityControl.Readonly = aReadonly;
    this.okBtn.Enabled = !aReadonly;
    this.securityControl.LoadSecurity(aId, aISecurityCallback);
    int num = (int) this.ShowDialog();
  }

  private void okBtn_Click(object sender, EventArgs e)
  {
    if (this.securityControl.SaveSecurity())
      return;
    this.DialogResult = DialogResult.None;
  }

  private void SecurityEditorForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SecurityEditorForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }
}
