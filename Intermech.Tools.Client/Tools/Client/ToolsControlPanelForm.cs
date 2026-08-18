// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.ToolsControlPanelForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Docking;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client;

internal class ToolsControlPanelForm : DockControl, IToolsControlPanel
{
  private int additionalFrameWidth;
  private IContainer components;
  private ToolsControlPanelControl pnGroupsAndControls;
  private Panel pnLegend;
  private Label lbLegend;

  public ToolsControlPanelForm()
  {
    this.InitializeComponent();
    this.additionalFrameWidth = SystemInformation.FrameBorderSize.Width * 2;
  }

  public void AddItem(string group, Control item)
  {
    if (group == null)
      throw new ArgumentNullException(nameof (group));
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    if (this.pnGroupsAndControls.InvokeRequired)
    {
      this.pnGroupsAndControls.BeginInvoke((Delegate) new Action<string, Control>(this.AddItem), (object) group, (object) item);
    }
    else
    {
      this.pnGroupsAndControls.AddControl(group, item);
      int num = this.pnGroupsAndControls.CanIncreaseWidth();
      if (num <= 0)
        return;
      this.Width += num;
      this.FloatingSize = this.FloatingSize + new Size(num + this.additionalFrameWidth, 0);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.pnGroupsAndControls = new ToolsControlPanelControl();
    this.pnLegend = new Panel();
    this.lbLegend = new Label();
    this.pnLegend.SuspendLayout();
    this.SuspendLayout();
    this.pnGroupsAndControls.Dock = DockStyle.Fill;
    this.pnGroupsAndControls.Location = new Point(1, 1);
    this.pnGroupsAndControls.Name = "pnGroupsAndControls";
    this.pnGroupsAndControls.Size = new Size(148, 503);
    this.pnGroupsAndControls.TabIndex = 0;
    this.pnLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.pnLegend.Controls.Add((Control) this.lbLegend);
    this.pnLegend.Dock = DockStyle.Bottom;
    this.pnLegend.Location = new Point(1, 504);
    this.pnLegend.Name = "pnLegend";
    this.pnLegend.Size = new Size(148, 65);
    this.pnLegend.TabIndex = 1;
    this.lbLegend.Dock = DockStyle.Fill;
    this.lbLegend.Location = new Point(0, 0);
    this.lbLegend.Name = "lbLegend";
    this.lbLegend.Padding = new Padding(4, 2, 4, 2);
    this.lbLegend.Size = new Size(146, 63 /*0x3F*/);
    this.lbLegend.TabIndex = 0;
    this.lbLegend.Text = "Настройки, отмеченные символом (*), сохраняются между сеансами работы приложения. Остальные настройки действуют только в текущем сеансе работы.";
    this.lbLegend.TextAlign = ContentAlignment.MiddleLeft;
    this.AllowedStates = DockLocation.Left | DockLocation.Right;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.Controls.Add((Control) this.pnGroupsAndControls);
    this.Controls.Add((Control) this.pnLegend);
    this.FloatingSize = new Size(150, 400);
    this.HideOnClose = true;
    this.Margin = new Padding(0);
    this.MinimumSize = new Size(150, 0);
    this.Name = nameof (ToolsControlPanelForm);
    this.PersistState = false;
    this.Size = new Size(150, 570);
    this.Text = "Управление инструментами";
    this.pnLegend.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
