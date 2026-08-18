// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionTableEditForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Expert;
using Intermech.Expert.Editor;
using Intermech.Expert.Table;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

internal class AutoSelectionTableEditForm : Form
{
  private readonly TableEditControl _control;
  private IContainer components;
  private MenuStrip menuStrip1;
  private ToolStripMenuItem таблицаToolStripMenuItem;
  private ToolStripMenuItem tsMenuItemTableEdit;
  private ToolStripMenuItem tsMenuItemTableDelete;
  private Panel pnlClient;

  public AutoSelectionTableEditForm(eTable[] tables)
  {
    this.InitializeComponent();
    this._control = new TableEditControl(LocalizationHolder.rm.GetString(sc_727.ssp_automatch_728()), tables, (TempFormula) null, true);
    this._control.Name = "tecExpTable";
    this._control.Parent = (Control) this.pnlClient;
    this._control.Dock = DockStyle.Fill;
    this._control.ShowFormulaEditBox = false;
    this._control.OnApplyChanges += new EventHandler(this._control_OnApplyChanges);
    this._control.OnRollbackChanges += new EventHandler(this._control_OnRollbackChanges);
  }

  public eTable[] Tables => this._control.Tables;

  private void _control_OnApplyChanges(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
  }

  private void _control_OnRollbackChanges(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
  }

  private void TableEditForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this._control.Activate();
  }

  private void TableEditForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this._control.Deactivate();
    FormStorage.SaveLayout((Control) this);
  }

  private void tsMenuItemTableEdit_Click(object sender, EventArgs e)
  {
    eTable[] tables = this._control.Tables;
    if (!AutoSelectionExpTableSetup.EditTables(ref tables))
      return;
    this._control.Tables = tables;
  }

  private void tsMenuItemTableDelete_Click(object sender, EventArgs e)
  {
    this._control.Tables = (eTable[]) null;
    this.DialogResult = DialogResult.OK;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionTableEditForm));
    this.menuStrip1 = new MenuStrip();
    this.таблицаToolStripMenuItem = new ToolStripMenuItem();
    this.tsMenuItemTableEdit = new ToolStripMenuItem();
    this.tsMenuItemTableDelete = new ToolStripMenuItem();
    this.pnlClient = new Panel();
    this.menuStrip1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.menuStrip1, "menuStrip1");
    this.menuStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.таблицаToolStripMenuItem
    });
    this.menuStrip1.Name = "menuStrip1";
    componentResourceManager.ApplyResources((object) this.таблицаToolStripMenuItem, "таблицаToolStripMenuItem");
    this.таблицаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsMenuItemTableEdit,
      (ToolStripItem) this.tsMenuItemTableDelete
    });
    this.таблицаToolStripMenuItem.Name = "таблицаToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.tsMenuItemTableEdit, "tsMenuItemTableEdit");
    this.tsMenuItemTableEdit.Name = "tsMenuItemTableEdit";
    this.tsMenuItemTableEdit.Click += new EventHandler(this.tsMenuItemTableEdit_Click);
    componentResourceManager.ApplyResources((object) this.tsMenuItemTableDelete, "tsMenuItemTableDelete");
    this.tsMenuItemTableDelete.Name = "tsMenuItemTableDelete";
    this.tsMenuItemTableDelete.Click += new EventHandler(this.tsMenuItemTableDelete_Click);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.menuStrip1);
    this.MainMenuStrip = this.menuStrip1;
    this.MinimizeBox = false;
    this.Name = nameof (AutoSelectionTableEditForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.FormClosed += new FormClosedEventHandler(this.TableEditForm_FormClosed);
    this.Load += new EventHandler(this.TableEditForm_Load);
    this.menuStrip1.ResumeLayout(false);
    this.menuStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
