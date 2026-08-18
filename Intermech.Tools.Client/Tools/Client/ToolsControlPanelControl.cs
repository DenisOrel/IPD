// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ToolsControlPanelControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal class ToolsControlPanelControl : UserControl
{
  private static readonly string CommonGroup = LocalizationHolder.rm.GetString("SR_294");
  private List<string> groups;
  private IComparer<string> groupComparer;
  private List<Control> groupControls;
  private int additionalScrollWidth;
  private IContainer components;
  private FlowLayoutPanel flpDock;

  public ToolsControlPanelControl()
  {
    this.groups = new List<string>(32 /*0x20*/);
    this.groupComparer = (IComparer<string>) new ToolsControlPanelControl.GroupComparer(ToolsControlPanelControl.CommonGroup);
    this.groupControls = new List<Control>(32 /*0x20*/);
    this.additionalScrollWidth = SystemInformation.HorizontalScrollBarArrowWidth * 2;
    this.InitializeComponent();
  }

  public void AddControl(string group, Control control)
  {
    if (group == null)
      throw new ArgumentNullException(nameof (group));
    if (control == null)
      throw new ArgumentNullException(nameof (control));
    if (string.IsNullOrEmpty(group))
      group = ToolsControlPanelControl.CommonGroup;
    this.SuspendLayout();
    try
    {
      int num = this.groups.BinarySearch(group, this.groupComparer);
      if (num < 0)
      {
        num = ~num;
        Control groupControl = this.CreateGroupControl(group);
        this.groups.Insert(num, group);
        this.groupControls.Insert(num, groupControl);
        this.InsertControl(num, groupControl);
        groupControl.Margin = new Padding(4, 8, 2, 8);
      }
      this.InsertControl(num, control);
    }
    finally
    {
      this.ResumeLayout(true);
    }
  }

  private Control CreateGroupControl(string group)
  {
    Label groupControl = new Label();
    groupControl.Text = group;
    groupControl.Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold | FontStyle.Underline);
    groupControl.AutoSize = true;
    return (Control) groupControl;
  }

  private void InsertControl(int groupIdx, Control control)
  {
    this.PrepareControl(control);
    this.flpDock.Controls.Add(control);
    this.flpDock.SetFlowBreak(control, true);
    int index = groupIdx + 1;
    if (index >= this.groups.Count)
      return;
    int childIndex = this.flpDock.Controls.GetChildIndex(this.groupControls[index]);
    this.flpDock.Controls.SetChildIndex(control, childIndex);
  }

  private void PrepareControl(Control control)
  {
    control.Margin = new Padding(16 /*0x10*/, 4, 2, 8);
  }

  public int CanIncreaseWidth()
  {
    int num = this.flpDock.GetPreferredSize(this.flpDock.Size).Width - this.flpDock.Size.Width;
    if (num <= 0)
      return 0;
    if (this.flpDock.HorizontalScroll.Visible)
      num += this.additionalScrollWidth;
    return num;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.flpDock = new FlowLayoutPanel();
    this.SuspendLayout();
    this.flpDock.AutoScroll = true;
    this.flpDock.Dock = DockStyle.Fill;
    this.flpDock.Location = new Point(0, 0);
    this.flpDock.Name = "flpDock";
    this.flpDock.Size = new Size(318, 460);
    this.flpDock.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.flpDock);
    this.Name = "ControlPanelForm";
    this.Size = new Size(318, 460);
    this.ResumeLayout(false);
  }

  private sealed class GroupComparer : IComparer<string>
  {
    private string commonGroup;
    private StringComparer nativeComparer;

    public GroupComparer(string commonGroup)
    {
      this.commonGroup = commonGroup;
      this.nativeComparer = StringComparer.CurrentCultureIgnoreCase;
    }

    public int Compare(string x, string y)
    {
      bool flag1 = this.nativeComparer.Compare(x, this.commonGroup) == 0;
      bool flag2 = this.nativeComparer.Compare(y, this.commonGroup) == 0;
      return flag1 ? (flag1 != flag2 ? -1 : 0) : (flag2 ? 1 : this.nativeComparer.Compare(x, y));
    }
  }
}
