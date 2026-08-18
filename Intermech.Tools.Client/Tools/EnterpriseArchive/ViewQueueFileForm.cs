// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ViewQueueFileForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class ViewQueueFileForm : MvpWindow, IQueueFileView, IView
{
  private IContainer components;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private Button btClose;
  private TableLayoutPanel tlpMainPanel;
  private Label lbToast;
  private Panel panel1;
  private iGrid iGrid1;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel toolStripStatusLabel1;
  private ToolStripStatusLabel tsslFileCount;

  public ViewQueueFileForm() => this.InitializeComponent();

  private void btClose_Click(object sender, EventArgs e) => this.Close();

  void IQueueFileView.SetFileList(ICollection<Tuple<string, int>> files)
  {
    if (files == null)
      throw new ArgumentNullException(nameof (files));
    this.CheckDisposed();
    this.iGrid1.BeginUpdate();
    try
    {
      this.iGrid1.Rows.Clear();
      foreach (Tuple<string, int> file in (IEnumerable<Tuple<string, int>>) files)
      {
        iGRow iGrow = this.iGrid1.Rows.Add();
        iGrow.Cells["Path"].Value = (object) file.Item1;
        iGrow.Cells["StageIndex"].Value = (object) file.Item2;
        iGrow.Key = file.Item1;
      }
      this.iGrid1.Cols["StageIndex"].AutoWidth();
      this.iGrid1.Cols["Path"].AutoWidth();
      this.iGrid1.GroupBox.Visible = files.Count > 0;
      this.tsslFileCount.Text = files.Count.ToString();
      if (this.iGrid1.SelectedCells.Count != 0 || files.Count <= 0)
        return;
      this.iGrid1.Rows[0].Cells[0].Selected = true;
    }
    finally
    {
      this.iGrid1.EndUpdate();
    }
  }

  void IQueueFileView.ShowToast(string text)
  {
    this.CheckDisposed();
    this.ShowToast(text);
  }

  void IQueueFileView.HideToast()
  {
    this.CheckDisposed();
    this.ShowToast((string) null);
  }

  private void CheckDisposed()
  {
    if (this.IsDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  private void ShowToast(string text)
  {
    this.lbToast.Visible = !string.IsNullOrEmpty(text);
    this.lbToast.Text = text;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.btClose = new Button();
    this.tlpMainPanel = new TableLayoutPanel();
    this.lbToast = new Label();
    this.panel1 = new Panel();
    this.iGrid1 = new iGrid();
    this.statusStrip1 = new StatusStrip();
    this.toolStripStatusLabel1 = new ToolStripStatusLabel();
    this.tsslFileCount = new ToolStripStatusLabel();
    this.tlpMainPanel.SuspendLayout();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this.iGrid1).BeginInit();
    this.statusStrip1.SuspendLayout();
    this.SuspendLayout();
    this.iGrid1Col1CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col1ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.btClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btClose.Location = new Point(697, 427);
    this.btClose.Name = "btClose";
    this.btClose.Size = new Size(75, 23);
    this.btClose.TabIndex = 1;
    this.btClose.Text = "Закрыть";
    this.btClose.UseVisualStyleBackColor = true;
    this.btClose.Click += new EventHandler(this.btClose_Click);
    this.tlpMainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tlpMainPanel.ColumnCount = 1;
    this.tlpMainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMainPanel.Controls.Add((Control) this.lbToast, 0, 0);
    this.tlpMainPanel.Controls.Add((Control) this.panel1, 0, 1);
    this.tlpMainPanel.Location = new Point(12, 12);
    this.tlpMainPanel.Name = "tlpMainPanel";
    this.tlpMainPanel.RowCount = 2;
    this.tlpMainPanel.RowStyles.Add(new RowStyle());
    this.tlpMainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tlpMainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMainPanel.Size = new Size(760, 399);
    this.tlpMainPanel.TabIndex = 2;
    this.lbToast.AutoSize = true;
    this.lbToast.BackColor = SystemColors.Info;
    this.lbToast.BorderStyle = BorderStyle.FixedSingle;
    this.lbToast.Dock = DockStyle.Fill;
    this.lbToast.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lbToast.Location = new Point(0, 0);
    this.lbToast.Margin = new Padding(0, 0, 0, 8);
    this.lbToast.Name = "lbToast";
    this.lbToast.Padding = new Padding(4, 8, 4, 8);
    this.lbToast.Size = new Size(760, 31 /*0x1F*/);
    this.lbToast.TabIndex = 1;
    this.lbToast.TextAlign = ContentAlignment.MiddleCenter;
    this.lbToast.Visible = false;
    this.panel1.BorderStyle = BorderStyle.Fixed3D;
    this.panel1.Controls.Add((Control) this.iGrid1);
    this.panel1.Controls.Add((Control) this.statusStrip1);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(3, 42);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(754, 354);
    this.panel1.TabIndex = 3;
    this.iGrid1.BorderStyle = iGBorderStyle.None;
    iGcolPattern1.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern1.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    iGcolPattern1.Key = "Path";
    iGcolPattern1.Text = (object) "Файл";
    iGcolPattern1.Width = 243;
    iGcolPattern2.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern2.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    iGcolPattern2.Key = "StageIndex";
    iGcolPattern2.Text = (object) "№ блока";
    iGcolPattern2.Width = 244;
    this.iGrid1.Cols.AddRange(new iGColPattern[2]
    {
      iGcolPattern1,
      iGcolPattern2
    });
    this.iGrid1.DefaultAutoGroupRow.Height = 23;
    this.iGrid1.DefaultRow.Height = 23;
    this.iGrid1.DefaultRow.NormalCellHeight = 23;
    this.iGrid1.Dock = DockStyle.Fill;
    this.iGrid1.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.iGrid1.GroupBox.Text = "Перетаскивайте сюда колонки для группировки их значений";
    this.iGrid1.GroupBox.Visible = true;
    this.iGrid1.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnThemeChange;
    this.iGrid1.Header.Height = 27;
    this.iGrid1.Location = new Point(0, 0);
    this.iGrid1.Margin = new Padding(0);
    this.iGrid1.Name = "iGrid1";
    this.iGrid1.ReadOnly = true;
    this.iGrid1.RowMode = true;
    this.iGrid1.Size = new Size(750, 323);
    this.iGrid1.TabIndex = 1;
    this.statusStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.toolStripStatusLabel1,
      (ToolStripItem) this.tsslFileCount
    });
    this.statusStrip1.Location = new Point(0, 323);
    this.statusStrip1.MinimumSize = new Size(0, 27);
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.Size = new Size(750, 27);
    this.statusStrip1.SizingGrip = false;
    this.statusStrip1.TabIndex = 3;
    this.statusStrip1.Text = "statusStrip1";
    this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
    this.toolStripStatusLabel1.Size = new Size(86, 22);
    this.toolStripStatusLabel1.Text = "Всего файлов:";
    this.tsslFileCount.Margin = new Padding(0, 3, 16 /*0x10*/, 2);
    this.tsslFileCount.Name = "tsslFileCount";
    this.tsslFileCount.Size = new Size(13, 22);
    this.tsslFileCount.Text = "0";
    this.AcceptButton = (IButtonControl) this.btClose;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(784, 462);
    this.Controls.Add((Control) this.tlpMainPanel);
    this.Controls.Add((Control) this.btClose);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(670, 430);
    this.Name = nameof (ViewQueueFileForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Просмотр очереди импорта файлов";
    this.tlpMainPanel.ResumeLayout(false);
    this.tlpMainPanel.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    ((ISupportInitialize) this.iGrid1).EndInit();
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.ResumeLayout(false);
  }
}
