// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.RefWindow
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Forums;

public class RefWindow : Form
{
  public string url = string.Empty;
  public string name = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox tbURL;
  private Label lb1;
  private Label label2;
  private TextBox tbName;
  private Button btnOK;
  private Button btnCancel;

  public RefWindow() => this.InitializeComponent();

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.url = this.tbURL.Text;
    this.name = this.tbName.Text;
  }

  private void tbName_TextChanged(object sender, EventArgs e)
  {
    if (!string.IsNullOrWhiteSpace(this.tbName.Text))
      this.btnOK.Enabled = true;
    else
      this.btnOK.Enabled = false;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RefWindow));
    this.tbURL = new TextBox();
    this.lb1 = new Label();
    this.label2 = new Label();
    this.tbName = new TextBox();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tbURL, "tbURL");
    this.tbURL.Name = "tbURL";
    componentResourceManager.ApplyResources((object) this.lb1, "lb1");
    this.lb1.Name = "lb1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbName, "tbName");
    this.tbName.Name = "tbName";
    this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.lb1);
    this.Controls.Add((Control) this.tbURL);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (RefWindow);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
