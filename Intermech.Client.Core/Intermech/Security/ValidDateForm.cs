
// Type: Intermech.Security.ValidDateForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Security;

/// <summary>Summary description for ValidDateForm.</summary>
public class ValidDateForm : Form
{
  private Button btnOk;
  private Button btnCancel;
  private Label label1;
  private Label label2;
  private RadioButton permanentRB;
  private RadioButton tempRB;
  private GroupBox tempGB;
  private DateTimePicker endDTP;
  private DateTimePicker startDTP;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public ValidDateForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 710);
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ValidDateForm));
    this.permanentRB = new RadioButton();
    this.tempRB = new RadioButton();
    this.tempGB = new GroupBox();
    this.endDTP = new DateTimePicker();
    this.startDTP = new DateTimePicker();
    this.label2 = new Label();
    this.label1 = new Label();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.tempGB.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.permanentRB, "permanentRB");
    this.permanentRB.Checked = true;
    this.permanentRB.Name = "permanentRB";
    this.permanentRB.TabStop = true;
    componentResourceManager.ApplyResources((object) this.tempRB, "tempRB");
    this.tempRB.Name = "tempRB";
    this.tempRB.CheckedChanged += new EventHandler(this.tempRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.tempGB, "tempGB");
    this.tempGB.Controls.Add((Control) this.endDTP);
    this.tempGB.Controls.Add((Control) this.startDTP);
    this.tempGB.Controls.Add((Control) this.label2);
    this.tempGB.Controls.Add((Control) this.label1);
    this.tempGB.Name = "tempGB";
    this.tempGB.TabStop = false;
    componentResourceManager.ApplyResources((object) this.endDTP, "endDTP");
    this.endDTP.Format = DateTimePickerFormat.Custom;
    this.endDTP.Name = "endDTP";
    this.endDTP.Value = new DateTime(2005, 5, 3, 0, 0, 0, 0);
    componentResourceManager.ApplyResources((object) this.startDTP, "startDTP");
    this.startDTP.Format = DateTimePickerFormat.Custom;
    this.startDTP.Name = "startDTP";
    this.startDTP.Value = new DateTime(2005, 5, 3, 0, 0, 0, 0);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.tempGB);
    this.Controls.Add((Control) this.tempRB);
    this.Controls.Add((Control) this.permanentRB);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.Name = nameof (ValidDateForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.ValidDateForm_Load);
    this.Closed += new EventHandler(this.ValidDateForm_Closed);
    this.tempGB.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public DialogResult Execute(ref object startDate, ref object endDate, bool aReadonly)
  {
    this.btnOk.Enabled = !aReadonly;
    if (startDate == DBNull.Value && endDate == DBNull.Value)
      this.permanentRB.Checked = true;
    else
      this.tempRB.Checked = true;
    DateTime now = DateTime.Now;
    this.startDTP.Value = startDate == DBNull.Value ? now : (DateTime) startDate;
    this.endDTP.Value = endDate == DBNull.Value ? now.AddDays(7.0) : (DateTime) endDate;
    this.CheckControlStates();
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    if (this.permanentRB.Checked)
    {
      startDate = (object) DBNull.Value;
      endDate = (object) DBNull.Value;
      return (DialogResult) num;
    }
    startDate = (object) this.startDTP.Value;
    endDate = (object) this.endDTP.Value;
    return (DialogResult) num;
  }

  private void tempRB_CheckedChanged(object sender, EventArgs e) => this.CheckControlStates();

  private void CheckControlStates() => this.tempGB.Enabled = this.tempRB.Checked;

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (!(this.startDTP.Value >= this.endDTP.Value))
      return;
    this.DialogResult = DialogResult.None;
  }

  private void ValidDateForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ValidDateForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }
}
