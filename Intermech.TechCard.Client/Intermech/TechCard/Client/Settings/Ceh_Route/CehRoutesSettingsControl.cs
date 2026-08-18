// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Ceh_Route.CehRoutesSettingsControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.TechCard.Ceh_Route.Settings;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Ceh_Route;

/// <summary>Контрол редактирования параметров расцеховки</summary>
public class CehRoutesSettingsControl : UserControl
{
  /// <summary>Режим только чтения</summary>
  private bool _readOnly;
  /// <summary>Режим загрузки данных</summary>
  private bool _updateMode;
  /// <summary>Настройки расцеховки</summary>
  private ICehRouteSettings _cehRouteSettings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip cmTemplates;
  private ToolStripMenuItem tsTemplMoveFirst;
  private ToolStripMenuItem tsTemplMoveUp;
  private ToolStripMenuItem tsTemplMoveDown;
  private ToolStripMenuItem tsTemplMoveLast;
  private Panel pnlClient;
  private CheckBox chbLinkTpToRoute;
  private Panel pnlTop;
  private GroupBox grbTop;
  private Label lblCaption;

  /// <summary>Initialize custom data</summary>
  private void InitCustomData() => this.UpdateControls();

  /// <summary>Обновление контролов</summary>
  private void UpdateControls() => this.chbLinkTpToRoute.Enabled = !this._readOnly;

  /// <summary>Fill params</summary>
  private void FillRouteParams()
  {
    if (this.CehRouteSettings == null)
      return;
    this._updateMode = true;
    try
    {
      this.chbLinkTpToRoute.Checked = this.CehRouteSettings.LinkTpToCehRoute == 1;
    }
    finally
    {
      this._updateMode = false;
    }
  }

  /// <summary>Fire changing event</summary>
  private void DoChanged()
  {
    if (this._updateMode)
      return;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>Конструктор</summary>
  public CehRoutesSettingsControl()
  {
    this.InitializeComponent();
    this.InitCustomData();
  }

  /// <summary>Интерфейс - параметры расцеховки</summary>
  public ICehRouteSettings CehRouteSettings
  {
    get => this._cehRouteSettings;
    set
    {
      this._cehRouteSettings = value;
      this.FillRouteParams();
    }
  }

  /// <summary>Режим только чтения</summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateControls();
    }
  }

  /// <summary>Changed Event</summary>
  public event EventHandler Changed;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chbLinkTpToRoute_CheckedChanged(object sender, EventArgs e)
  {
    if (this._readOnly || this.CehRouteSettings == null)
      return;
    this.CehRouteSettings.LinkTpToCehRoute = Convert.ToInt32(this.chbLinkTpToRoute.Checked);
    this.DoChanged();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoutesSettingsControl));
    this.cmTemplates = new ContextMenuStrip(this.components);
    this.tsTemplMoveFirst = new ToolStripMenuItem();
    this.tsTemplMoveUp = new ToolStripMenuItem();
    this.tsTemplMoveDown = new ToolStripMenuItem();
    this.tsTemplMoveLast = new ToolStripMenuItem();
    this.pnlClient = new Panel();
    this.chbLinkTpToRoute = new CheckBox();
    this.pnlTop = new Panel();
    this.grbTop = new GroupBox();
    this.lblCaption = new Label();
    this.cmTemplates.SuspendLayout();
    this.pnlClient.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.grbTop.SuspendLayout();
    this.SuspendLayout();
    this.cmTemplates.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsTemplMoveFirst,
      (ToolStripItem) this.tsTemplMoveUp,
      (ToolStripItem) this.tsTemplMoveDown,
      (ToolStripItem) this.tsTemplMoveLast
    });
    this.cmTemplates.Name = "cmTemplates";
    componentResourceManager.ApplyResources((object) this.cmTemplates, "cmTemplates");
    this.tsTemplMoveFirst.Name = "tsTemplMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveFirst, "tsTemplMoveFirst");
    this.tsTemplMoveUp.Name = "tsTemplMoveUp";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveUp, "tsTemplMoveUp");
    this.tsTemplMoveDown.Name = "tsTemplMoveDown";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveDown, "tsTemplMoveDown");
    this.tsTemplMoveLast.Name = "tsTemplMoveLast";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveLast, "tsTemplMoveLast");
    this.pnlClient.Controls.Add((Control) this.chbLinkTpToRoute);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    componentResourceManager.ApplyResources((object) this.chbLinkTpToRoute, "chbLinkTpToRoute");
    this.chbLinkTpToRoute.Name = "chbLinkTpToRoute";
    this.chbLinkTpToRoute.UseVisualStyleBackColor = true;
    this.chbLinkTpToRoute.CheckedChanged += new EventHandler(this.chbLinkTpToRoute_CheckedChanged);
    this.pnlTop.Controls.Add((Control) this.grbTop);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    this.grbTop.Controls.Add((Control) this.lblCaption);
    componentResourceManager.ApplyResources((object) this.grbTop, "grbTop");
    this.grbTop.Name = "grbTop";
    this.grbTop.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.Name = "lblCaption";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.pnlTop);
    this.Name = nameof (CehRoutesSettingsControl);
    this.Tag = (object) "";
    this.cmTemplates.ResumeLayout(false);
    this.pnlClient.ResumeLayout(false);
    this.pnlClient.PerformLayout();
    this.pnlTop.ResumeLayout(false);
    this.grbTop.ResumeLayout(false);
    this.grbTop.PerformLayout();
    this.ResumeLayout(false);
  }
}
