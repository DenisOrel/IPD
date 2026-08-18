// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.EventLogDeleteForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

public class EventLogDeleteForm : Form
{
  private Button okBtn;
  private Button cancelBtn;
  private RadioButton allRb;
  private DateTimePicker dateTimePicker;
  private RadioButton byDateRb;
  private System.ComponentModel.Container components;

  public EventLogDeleteForm() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventLogDeleteForm));
    this.okBtn = new Button();
    this.cancelBtn = new Button();
    this.byDateRb = new RadioButton();
    this.allRb = new RadioButton();
    this.dateTimePicker = new DateTimePicker();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.okBtn, "okBtn");
    this.okBtn.DialogResult = DialogResult.OK;
    this.okBtn.Name = "okBtn";
    componentResourceManager.ApplyResources((object) this.cancelBtn, "cancelBtn");
    this.cancelBtn.DialogResult = DialogResult.Cancel;
    this.cancelBtn.Name = "cancelBtn";
    this.byDateRb.Checked = true;
    componentResourceManager.ApplyResources((object) this.byDateRb, "byDateRb");
    this.byDateRb.Name = "byDateRb";
    this.byDateRb.TabStop = true;
    this.byDateRb.CheckedChanged += new EventHandler(this.byDateRb_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.allRb, "allRb");
    this.allRb.Name = "allRb";
    componentResourceManager.ApplyResources((object) this.dateTimePicker, "dateTimePicker");
    this.dateTimePicker.Name = "dateTimePicker";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.dateTimePicker);
    this.Controls.Add((Control) this.allRb);
    this.Controls.Add((Control) this.byDateRb);
    this.Controls.Add((Control) this.cancelBtn);
    this.Controls.Add((Control) this.okBtn);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EventLogDeleteForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }

  public DialogResult Execute(ref bool byDate, ref DateTime date)
  {
    if (byDate)
      this.byDateRb.Checked = true;
    else
      this.allRb.Checked = true;
    this.dateTimePicker.Value = date;
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    byDate = this.byDateRb.Checked;
    date = this.dateTimePicker.Value;
    return (DialogResult) num;
  }

  private void byDateRb_CheckedChanged(object sender, EventArgs e)
  {
    this.dateTimePicker.Enabled = this.byDateRb.Checked;
  }
}
