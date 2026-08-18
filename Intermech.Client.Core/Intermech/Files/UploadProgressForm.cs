
// Type: Intermech.Files.UploadProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Files;

internal sealed class UploadProgressForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lbWorkObject;
  private TextBox tbWorkObject;
  private ProgressBar pbPercentComplete;
  private Button btCancel;
  private Label label1;
  private PictureBox pbAlert;
  private Label lbDescription;

  public UploadProgressForm() => this.InitializeComponent();

  private void OnCancelClick(object sender, EventArgs e) => this.SetCancelRequest(true);

  private void OnFormClosing(object sender, FormClosingEventArgs e)
  {
    this.SetCancelRequest(true);
    e.Cancel = true;
  }

  public void ShowWorkObject(DBObjectState workObject)
  {
    this.tbWorkObject.Text = workObject.Caption;
  }

  public void ShowProgress(double percentComplete)
  {
    this.pbPercentComplete.Value = (int) Math.Round(percentComplete);
  }

  public bool IsCancelRequested()
  {
    if (object.Equals(this.btCancel.Tag, (object) true) && MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1303"), LocalizationHolder.rm.GetString("Client.Core_1304"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      return true;
    this.SetCancelRequest(false);
    return false;
  }

  private void SetCancelRequest(bool cancel) => this.btCancel.Tag = (object) cancel;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UploadProgressForm));
    this.lbWorkObject = new Label();
    this.tbWorkObject = new TextBox();
    this.pbPercentComplete = new ProgressBar();
    this.btCancel = new Button();
    this.label1 = new Label();
    this.pbAlert = new PictureBox();
    this.lbDescription = new Label();
    ((ISupportInitialize) this.pbAlert).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbWorkObject, "lbWorkObject");
    this.lbWorkObject.Name = "lbWorkObject";
    componentResourceManager.ApplyResources((object) this.tbWorkObject, "tbWorkObject");
    this.tbWorkObject.Name = "tbWorkObject";
    this.tbWorkObject.ReadOnly = true;
    this.tbWorkObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pbPercentComplete, "pbPercentComplete");
    this.pbPercentComplete.Name = "pbPercentComplete";
    this.pbPercentComplete.Step = 1;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btCancel.Click += new EventHandler(this.OnCancelClick);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.pbAlert, "pbAlert");
    this.pbAlert.Name = "pbAlert";
    this.pbAlert.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.pbAlert);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.pbPercentComplete);
    this.Controls.Add((Control) this.tbWorkObject);
    this.Controls.Add((Control) this.lbWorkObject);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (UploadProgressForm);
    this.FormClosing += new FormClosingEventHandler(this.OnFormClosing);
    ((ISupportInitialize) this.pbAlert).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
