
// Type: Intermech.Navigator.Controls.VersionsSelectDateForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public class VersionsSelectDateForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal MonthCalendar mcCalendar;
  internal CheckBox cbCurrentDate;
  private Button bOK;
  private Button bCancel;
  private Panel pButtons;
  private Panel panel2;

  public VersionsSelectDateForm() => this.InitializeComponent();

  private void ReCalculateSizeAndLocation(Point location)
  {
    int x = this.pButtons.Width;
    if (this.mcCalendar.Width > x)
      x = this.mcCalendar.Width + 10;
    int y = (this.mcCalendar.Height > this.panel2.Height ? this.mcCalendar.Height + 10 : this.panel2.Height) + this.pButtons.Height;
    this.SetClientSizeCore(x, y);
    this.mcCalendar.Location = new Point(Convert.ToInt32((this.pButtons.Width - this.panel2.Width) / 2), 0);
    this.Location = location;
  }

  /// <summary>Отобразить диалог выбора даты</summary>
  /// <param name="date">Начальная дата</param>
  /// <param name="location">Точка левого верхнего угла формы на экране</param>
  /// <returns></returns>
  public static DateTime ShowDateForm(DateTime date, Point location)
  {
    VersionsSelectDateForm versionsSelectDateForm = new VersionsSelectDateForm();
    versionsSelectDateForm.ReCalculateSizeAndLocation(location);
    versionsSelectDateForm.cbCurrentDate.CheckedChanged -= new EventHandler(versionsSelectDateForm.cbCurrentDate_CheckedChanged);
    if (date == DateTime.MaxValue)
    {
      versionsSelectDateForm.cbCurrentDate.Checked = true;
      versionsSelectDateForm.mcCalendar.Enabled = false;
    }
    else
    {
      versionsSelectDateForm.cbCurrentDate.Checked = false;
      versionsSelectDateForm.mcCalendar.SelectionRange = new SelectionRange(date, date);
    }
    versionsSelectDateForm.cbCurrentDate.CheckedChanged += new EventHandler(versionsSelectDateForm.cbCurrentDate_CheckedChanged);
    if (versionsSelectDateForm.ShowDialog() == DialogResult.Cancel)
      return date;
    return versionsSelectDateForm.cbCurrentDate.Checked ? DateTime.MaxValue : versionsSelectDateForm.mcCalendar.SelectionStart;
  }

  internal void cbCurrentDate_CheckedChanged(object sender, EventArgs e)
  {
    this.mcCalendar.Enabled = !this.cbCurrentDate.Checked;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionsSelectDateForm));
    this.mcCalendar = new MonthCalendar();
    this.cbCurrentDate = new CheckBox();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.pButtons = new Panel();
    this.panel2 = new Panel();
    this.pButtons.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.mcCalendar, "mcCalendar");
    this.mcCalendar.Name = "mcCalendar";
    componentResourceManager.ApplyResources((object) this.cbCurrentDate, "cbCurrentDate");
    this.cbCurrentDate.Name = "cbCurrentDate";
    this.cbCurrentDate.UseVisualStyleBackColor = true;
    this.cbCurrentDate.CheckedChanged += new EventHandler(this.cbCurrentDate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.pButtons, "pButtons");
    this.pButtons.Controls.Add((Control) this.bCancel);
    this.pButtons.Controls.Add((Control) this.cbCurrentDate);
    this.pButtons.Controls.Add((Control) this.bOK);
    this.pButtons.Name = "pButtons";
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Controls.Add((Control) this.mcCalendar);
    this.panel2.Name = "panel2";
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.pButtons);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (VersionsSelectDateForm);
    this.ShowInTaskbar = false;
    this.pButtons.ResumeLayout(false);
    this.pButtons.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
