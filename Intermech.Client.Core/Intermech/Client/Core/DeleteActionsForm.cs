
// Type: Intermech.Client.Core.DeleteActionsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class DeleteActionsForm : Form
{
  private Exception exception;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnAbort;
  private Button btnIgnore;
  private Button btnIgnoreAll;
  private Button btnShow;
  private Label label1;

  public DeleteActionsForm() => this.InitializeComponent();

  private void btnAbort_Click(object sender, EventArgs e)
  {
    if (this.DialogResult != DialogResult.No)
      return;
    ExceptionHelper.ExceptionService.ShowException(this.exception);
    this.DialogResult = DialogResult.None;
  }

  public DialogResult ShowDialog(Exception ex)
  {
    this.exception = ex;
    return this.ShowDialog();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteActionsForm));
    this.btnAbort = new Button();
    this.btnIgnore = new Button();
    this.btnIgnoreAll = new Button();
    this.btnShow = new Button();
    this.label1 = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnAbort, "btnAbort");
    this.btnAbort.DialogResult = DialogResult.Abort;
    this.btnAbort.Name = "btnAbort";
    this.btnAbort.UseVisualStyleBackColor = true;
    this.btnAbort.Click += new EventHandler(this.btnAbort_Click);
    componentResourceManager.ApplyResources((object) this.btnIgnore, "btnIgnore");
    this.btnIgnore.DialogResult = DialogResult.Ignore;
    this.btnIgnore.Name = "btnIgnore";
    this.btnIgnore.UseVisualStyleBackColor = true;
    this.btnIgnore.Click += new EventHandler(this.btnAbort_Click);
    componentResourceManager.ApplyResources((object) this.btnIgnoreAll, "btnIgnoreAll");
    this.btnIgnoreAll.DialogResult = DialogResult.Retry;
    this.btnIgnoreAll.Name = "btnIgnoreAll";
    this.btnIgnoreAll.UseVisualStyleBackColor = true;
    this.btnIgnoreAll.Click += new EventHandler(this.btnAbort_Click);
    componentResourceManager.ApplyResources((object) this.btnShow, "btnShow");
    this.btnShow.DialogResult = DialogResult.No;
    this.btnShow.Name = "btnShow";
    this.btnShow.UseVisualStyleBackColor = true;
    this.btnShow.Click += new EventHandler(this.btnAbort_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnShow);
    this.Controls.Add((Control) this.btnIgnoreAll);
    this.Controls.Add((Control) this.btnIgnore);
    this.Controls.Add((Control) this.btnAbort);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DeleteActionsForm);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
