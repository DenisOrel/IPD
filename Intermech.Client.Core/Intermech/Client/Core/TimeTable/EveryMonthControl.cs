
// Type: Intermech.Client.Core.TimeTable.EveryMonthControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Client.Core.TimeTable;

public class EveryMonthControl : UserControl, ITimesControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private ListView listView1;
  private Label label2;
  private Label label3;
  private SpinEdit seDay;

  public EveryMonthControl() => this.InitializeComponent();

  public void Load(ProcessTime time)
  {
    this.seDay.Value = (Decimal) (time.DayOfMonth == 0 ? 1 : time.DayOfMonth);
    for (int index = 0; index < this.listView1.Items.Count; ++index)
      this.listView1.Items[index].Checked = false;
    if (time.Months == null)
      return;
    for (int index1 = 0; index1 < time.Months.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.listView1.Items.Count; ++index2)
      {
        if (time.Months[index1] == Convert.ToInt32(this.listView1.Items[index2].Tag))
        {
          this.listView1.Items[index2].Checked = true;
          break;
        }
      }
    }
  }

  public bool Save(ProcessTime time)
  {
    time.BeginDateTime = DateTime.Now;
    List<int> intList = new List<int>(12);
    for (int index = 0; index < this.listView1.Items.Count; ++index)
    {
      if (this.listView1.Items[index].Checked)
        intList.Add(Convert.ToInt32(this.listView1.Items[index].Tag));
    }
    if (intList.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1564"), LocalizationHolder.rm.GetString("Client.Core_1565"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    time.Months = intList.ToArray();
    time.DayOfMonth = Convert.ToInt32(this.seDay.Value);
    time.DaysOfWeek = (int[]) null;
    time.DayExecution = EveryDayExecution.None;
    return true;
  }

  private void seDay_EditValueChanging(object sender, ChangingEventArgs e)
  {
    if (EveryMonthControl.IsItNumber(e.NewValue) && Convert.ToInt32(e.NewValue) <= 31 /*0x1F*/ && Convert.ToInt32(e.NewValue) >= 1)
      return;
    e.Cancel = true;
  }

  public static bool IsItNumber(object inputvalue)
  {
    return !new Regex("[^0-9]").IsMatch(Convert.ToString(inputvalue));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EveryMonthControl));
    this.label1 = new Label();
    this.listView1 = new ListView();
    this.label2 = new Label();
    this.label3 = new Label();
    this.seDay = new SpinEdit();
    this.seDay.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.listView1.BackColor = SystemColors.Control;
    this.listView1.BorderStyle = BorderStyle.None;
    this.listView1.CheckBoxes = true;
    this.listView1.Items.AddRange(new ListViewItem[12]
    {
      (ListViewItem) componentResourceManager.GetObject("listView1.Items"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items1"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items2"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items3"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items4"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items5"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items6"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items7"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items8"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items9"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items10"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items11")
    });
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.Name = "listView1";
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.List;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.seDay, "seDay");
    this.seDay.Name = "seDay";
    this.seDay.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.seDay.Properties.UseCtrlIncrement = false;
    this.seDay.EditValueChanging += new ChangingEventHandler(this.seDay_EditValueChanging);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.seDay);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.listView1);
    this.Name = nameof (EveryMonthControl);
    this.seDay.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
