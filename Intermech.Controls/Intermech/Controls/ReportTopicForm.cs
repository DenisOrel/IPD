
// Type: Intermech.Controls.ReportTopicForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Controls;

public class ReportTopicForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Label label2;
  private TextBox tbTopic;
  private TextBox tbText;
  private Button btnOK;
  private Button btnCancel;

  /// <summary>тема</summary>
  public string ReportText => this.tbText.Text;

  /// <summary>текст</summary>
  public string ReportTopic => this.tbTopic.Text;

  public ReportTopicForm() => this.InitializeComponent();

  private void btnOK_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReportTopicForm));
    this.label1 = new Label();
    this.label2 = new Label();
    this.tbTopic = new TextBox();
    this.tbText = new TextBox();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbTopic, "tbTopic");
    this.tbTopic.Name = "tbTopic";
    componentResourceManager.ApplyResources((object) this.tbText, "tbText");
    this.tbText.Name = "tbText";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.tbText);
    this.Controls.Add((Control) this.tbTopic);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.MinimizeBox = false;
    this.Name = nameof (ReportTopicForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
