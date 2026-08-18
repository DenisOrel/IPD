
// Type: Intermech.PropertyEditors.PasswordForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for PasswordForm.</summary>
public class PasswordForm : Form
{
  private Label label1;
  private Label label2;
  private Button btnOk;
  private Button btnCancel;
  private TextBox password1;
  private TextBox password2;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public string Password => this.password1.Text;

  public PasswordForm() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PasswordForm));
    this.label1 = new Label();
    this.label2 = new Label();
    this.password1 = new TextBox();
    this.password2 = new TextBox();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.password1, "password1");
    this.password1.Name = "password1";
    this.password1.TextChanged += new EventHandler(this.password2_TextChanged);
    componentResourceManager.ApplyResources((object) this.password2, "password2");
    this.password2.Name = "password2";
    this.password2.TextChanged += new EventHandler(this.password2_TextChanged);
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.password2);
    this.Controls.Add((Control) this.password1);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.Name = nameof (PasswordForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (!(this.password1.Text != this.password2.Text))
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_977"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK);
    this.DialogResult = DialogResult.None;
  }

  public new DialogResult ShowDialog()
  {
    this.password1.Text = ClientConsts.PasswordString;
    this.password2.Text = ClientConsts.PasswordString;
    this.btnOk.Enabled = false;
    return base.ShowDialog();
  }

  private void password2_TextChanged(object sender, EventArgs e) => this.btnOk.Enabled = true;
}
