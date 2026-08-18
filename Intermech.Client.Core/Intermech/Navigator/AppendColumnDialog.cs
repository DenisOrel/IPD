
// Type: Intermech.Navigator.AppendColumnDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Summary description for AppendColumnDialog.</summary>
public class AppendColumnDialog : Form
{
  private NodeColumnCollection _supportedColumns;
  private NodeColumnCollection _excludedColumns;
  private NodeColumnCollection _explicitColumns;
  private NodeColumnCollection _selectedColumns;
  private Button btOk;
  private Button btCancel;
  private TabControl tcColumns;
  private TabPage tabExplicit;
  private ListBox lbExplicit;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public AppendColumnDialog() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AppendColumnDialog));
    this.btOk = new Button();
    this.btCancel = new Button();
    this.tcColumns = new TabControl();
    this.tabExplicit = new TabPage();
    this.lbExplicit = new ListBox();
    this.tcColumns.SuspendLayout();
    this.tabExplicit.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btOk, "btOk");
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Name = "btOk";
    this.btOk.Click += new EventHandler(this.btOk_Click);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.tcColumns, "tcColumns");
    this.tcColumns.Controls.Add((Control) this.tabExplicit);
    this.tcColumns.Name = "tcColumns";
    this.tcColumns.SelectedIndex = 0;
    this.tabExplicit.Controls.Add((Control) this.lbExplicit);
    componentResourceManager.ApplyResources((object) this.tabExplicit, "tabExplicit");
    this.tabExplicit.Name = "tabExplicit";
    componentResourceManager.ApplyResources((object) this.lbExplicit, "lbExplicit");
    this.lbExplicit.Name = "lbExplicit";
    this.lbExplicit.SelectionMode = SelectionMode.MultiSimple;
    this.AcceptButton = (IButtonControl) this.btOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tcColumns);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.Name = nameof (AppendColumnDialog);
    this.ShowInTaskbar = false;
    this.Activated += new EventHandler(this.AppendColumnDialog_Activated);
    this.tcColumns.ResumeLayout(false);
    this.tabExplicit.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public static NodeColumnCollection ShowDialog(
    NodeColumnCollection supportedColumns,
    NodeColumnCollection excludedColumns)
  {
    AppendColumnDialog appendColumnDialog = new AppendColumnDialog();
    appendColumnDialog.Initialize(supportedColumns, excludedColumns);
    return appendColumnDialog.ShowDialog() == DialogResult.OK ? appendColumnDialog._selectedColumns : (NodeColumnCollection) null;
  }

  private void Initialize(
    NodeColumnCollection supportedColumns,
    NodeColumnCollection excludedColumns)
  {
    this._supportedColumns = supportedColumns;
    this._excludedColumns = excludedColumns;
    this._selectedColumns = (NodeColumnCollection) null;
  }

  private void AppendColumnDialog_Activated(object sender, EventArgs e)
  {
    this._explicitColumns = this._supportedColumns;
    for (int index1 = 0; index1 < this._excludedColumns.Count; ++index1)
    {
      int index2 = this._explicitColumns.IndexOf(this._excludedColumns[index1]);
      if (index2 >= 0)
        this._explicitColumns.RemoveAt(index2);
    }
    this._explicitColumns.Sort(true);
    for (int index = 0; index < this._explicitColumns.Count; ++index)
      this.lbExplicit.Items.Add((object) this._explicitColumns[index].Caption);
  }

  private void btOk_Click(object sender, EventArgs e)
  {
    ListBox.SelectedIndexCollection selectedIndices = this.lbExplicit.SelectedIndices;
    if (selectedIndices.Count <= 0)
      return;
    this._selectedColumns = new NodeColumnCollection();
    for (int index = 0; index < selectedIndices.Count; ++index)
      this._selectedColumns.Add(this._explicitColumns[selectedIndices[index]]);
  }
}
