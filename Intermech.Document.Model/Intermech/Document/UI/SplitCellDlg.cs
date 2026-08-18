// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.SplitCellDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог разбивки таблицы</summary>
public class SplitCellDlg : Form
{
  private Label label2;
  private Label label1;
  private Button btnCancel;
  private Button btnOk;
  private NumericUpDown nRows;
  private NumericUpDown nColumns;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Выполнить диалог</summary>
  /// <returns>Размерность таблицы</returns>
  public static TableSize Execute(bool isTableCell)
  {
    SplitCellDlg splitCellDlg = new SplitCellDlg();
    if (!isTableCell)
      splitCellDlg.Text = LocalizationHolder.rm.GetString("Document.Model_536");
    TableSize tableSize = (TableSize) null;
    if (splitCellDlg.ShowDialog() == DialogResult.OK)
    {
      int int32 = Convert.ToInt32(splitCellDlg.nColumns.Value);
      tableSize = new TableSize(Convert.ToInt32(splitCellDlg.nRows.Value), int32);
    }
    splitCellDlg.Dispose();
    return tableSize;
  }

  /// <summary>Конструктор</summary>
  public SplitCellDlg() => this.InitializeComponent();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SplitCellDlg));
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.nRows = new NumericUpDown();
    this.nColumns = new NumericUpDown();
    this.nRows.BeginInit();
    this.nColumns.BeginInit();
    this.SuspendLayout();
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.nRows, "nRows");
    this.nRows.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.nRows.Name = "nRows";
    this.nRows.Value = new Decimal(new int[4]{ 1, 0, 0, 0 });
    componentResourceManager.ApplyResources((object) this.nColumns, "nColumns");
    this.nColumns.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.nColumns.Name = "nColumns";
    this.nColumns.Value = new Decimal(new int[4]
    {
      2,
      0,
      0,
      0
    });
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.nColumns);
    this.Controls.Add((Control) this.nRows);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SplitCellDlg);
    this.ShowInTaskbar = false;
    this.nRows.EndInit();
    this.nColumns.EndInit();
    this.ResumeLayout(false);
  }
}
