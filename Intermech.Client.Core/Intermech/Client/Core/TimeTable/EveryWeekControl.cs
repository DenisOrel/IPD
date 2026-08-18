
// Type: Intermech.Client.Core.TimeTable.EveryWeekControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.TimeTable;

public class EveryWeekControl : UserControl, ITimesControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListView listView1;
  private Label label1;

  public EveryWeekControl() => this.InitializeComponent();

  public void Load(ProcessTime time)
  {
    for (int index = 0; index < this.listView1.Items.Count; ++index)
      this.listView1.Items[index].Checked = false;
    if (time.DaysOfWeek == null)
      return;
    for (int index1 = 0; index1 < time.DaysOfWeek.Length; ++index1)
    {
      for (int index2 = 0; index2 < this.listView1.Items.Count; ++index2)
      {
        if (time.DaysOfWeek[index1] == Convert.ToInt32(this.listView1.Items[index2].Tag))
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
    List<int> intList = new List<int>(7);
    for (int index = 0; index < this.listView1.Items.Count; ++index)
    {
      if (this.listView1.Items[index].Checked)
        intList.Add(Convert.ToInt32(this.listView1.Items[index].Tag));
    }
    if (intList.Count == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1566"), LocalizationHolder.rm.GetString("Client.Core_1565"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    time.DaysOfWeek = intList.ToArray();
    time.DayOfMonth = 0;
    time.Months = (int[]) null;
    time.DayExecution = EveryDayExecution.None;
    return true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EveryWeekControl));
    this.listView1 = new ListView();
    this.label1 = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.BackColor = SystemColors.Control;
    this.listView1.BorderStyle = BorderStyle.None;
    this.listView1.CheckBoxes = true;
    this.listView1.Items.AddRange(new ListViewItem[7]
    {
      (ListViewItem) componentResourceManager.GetObject("listView1.Items"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items1"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items2"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items3"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items4"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items5"),
      (ListViewItem) componentResourceManager.GetObject("listView1.Items6")
    });
    this.listView1.Name = "listView1";
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.List;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.listView1);
    this.Name = nameof (EveryWeekControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
