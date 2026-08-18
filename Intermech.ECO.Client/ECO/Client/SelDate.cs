// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SelDate
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SelDate : Form
{
  public DialogResult dr;
  private DateTime hitTime;
  private IContainer components;
  private MonthCalendar cal;
  private Button btnOK;
  private Button button2;
  private Button button1;

  public SelDate()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 841);
  }

  public DateTime Execute(DateTime dt, Point loc)
  {
    this.cal.SelectionStart = dt;
    if (!loc.IsEmpty)
      this.Location = loc;
    this.dr = this.ShowDialog();
    return this.dr == DialogResult.OK ? this.cal.SelectionStart : dt;
  }

  private void cal_MouseUp(object sender, MouseEventArgs e)
  {
    MonthCalendar.HitTestInfo hitTestInfo = this.cal.HitTest(new Point(e.X, e.Y));
    if (hitTestInfo.HitArea != MonthCalendar.HitArea.Date)
      return;
    if (hitTestInfo.Time == this.hitTime)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    else
      this.hitTime = hitTestInfo.Time;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelDate));
    this.cal = new MonthCalendar();
    this.btnOK = new Button();
    this.button2 = new Button();
    this.button1 = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cal, "cal");
    this.cal.MaxSelectionCount = 1;
    this.cal.Name = "cal";
    this.cal.MouseUp += new MouseEventHandler(this.cal_MouseUp);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Yes;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.cal);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelDate);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }
}
