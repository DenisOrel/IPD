// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.AccauntForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class AccauntForm : Form
{
  private IContainer components;
  private Panel panel1;
  private Button bCancel;
  private Button bOK;
  private Panel panel2;
  private Button bPassword;
  private TextBox tbPassword;
  private Label label3;
  private TextBox tbLogin;
  private Label label2;
  private TextBox tbEmail;
  private Label label1;

  public string Email
  {
    get => this.tbEmail.Text;
    set => this.tbEmail.Text = value;
  }

  public string Login
  {
    get => this.tbLogin.Text;
    set => this.tbLogin.Text = value;
  }

  public string Password
  {
    get => this.tbPassword.Text;
    set => this.tbPassword.Text = value;
  }

  public AccauntForm(string text)
  {
    this.InitializeComponent();
    this.Text = text;
  }

  private void bPassword_Click(object sender, EventArgs e)
  {
    string password;
    if (UserPasswordForm.Execute(out password, false) != DialogResult.OK)
      return;
    this.tbPassword.Text = password;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AccauntForm));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.panel2 = new Panel();
    this.bPassword = new Button();
    this.tbPassword = new TextBox();
    this.label3 = new Label();
    this.tbLogin = new TextBox();
    this.label2 = new Label();
    this.tbEmail = new TextBox();
    this.label1 = new Label();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOK);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Controls.Add((Control) this.bPassword);
    this.panel2.Controls.Add((Control) this.tbPassword);
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this.tbLogin);
    this.panel2.Controls.Add((Control) this.label2);
    this.panel2.Controls.Add((Control) this.tbEmail);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.bPassword, "bPassword");
    this.bPassword.Name = "bPassword";
    this.bPassword.UseVisualStyleBackColor = true;
    this.bPassword.Click += new EventHandler(this.bPassword_Click);
    componentResourceManager.ApplyResources((object) this.tbPassword, "tbPassword");
    this.tbPassword.BackColor = SystemColors.Window;
    this.tbPassword.Name = "tbPassword";
    this.tbPassword.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.tbLogin, "tbLogin");
    this.tbLogin.Name = "tbLogin";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.tbEmail.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.tbEmail, "tbEmail");
    this.tbEmail.Name = "tbEmail";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (AccauntForm);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
