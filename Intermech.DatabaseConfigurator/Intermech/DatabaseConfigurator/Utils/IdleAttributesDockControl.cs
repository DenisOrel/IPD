// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.IdleAttributesDockControl
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class IdleAttributesDockControl : DockControl
{
  private static readonly string sFound = "Найдено: {0}";
  private static readonly string sSelected = "Выбрано: {0}";
  private static readonly string sAttribute = "Атрибут: {0}";
  private bool isInitialized;
  private bool taskWasStarted;
  private DataTable dataTable;
  private bool controlClosed;
  private IContainer components;
  private BackgroundWorker backgroundWorker;
  private Button btnStart;
  private Panel panel4List;
  private Panel panel4Button;
  private iGrid iaGrid;
  private Button btnDelete;
  private TextBox textBox;
  private StatusStrip statusStrip;
  private ToolStripStatusLabel FoundLabel;
  private ToolStripStatusLabel SelectedLabel;
  private ToolStripStatusLabel AttrNameLabel;
  private ToolStripProgressBar progressBar;

  public IdleAttributesDockControl() => this.InitializeComponent();

  private void IdleAttributesDockControl_Load(object sender, EventArgs e)
  {
    this.ClearGrid();
    this.UpdateControlsEx();
  }

  private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
      try
      {
        this.dataTable = customService.GetIdleAttributes(sessionKeeper.Session.SessionGUID);
        if (this.backgroundWorker.CancellationPending)
          e.Cancel = true;
        else
          e.Result = (object) this.dataTable;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }

  private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    if (this.controlClosed)
      return;
    this.ClearGrid();
    this.taskWasStarted = false;
    if (e.Error != null)
    {
      this.progressBar.Style = ProgressBarStyle.Blocks;
      ExceptionHelper.ExceptionService.ShowException(e.Error);
    }
    else if (e.Result is DataTable)
    {
      this.FillGrid((DataTable) e.Result);
      this.isInitialized = true;
    }
    this.progressBar.Style = ProgressBarStyle.Blocks;
    this.UpdateControlsEx();
  }

  private void UpdateControls()
  {
    this.btnStart.Enabled = !this.taskWasStarted;
    this.btnDelete.Enabled = this.isInitialized && !this.taskWasStarted && this.iaGrid.SelectedCells != null && this.iaGrid.SelectedCells.Count > 0;
  }

  private void btnStart_Click(object sender, EventArgs e)
  {
    this.taskWasStarted = true;
    this.UpdateControlsEx();
    this.progressBar.Style = ProgressBarStyle.Marquee;
    this.backgroundWorker.RunWorkerAsync();
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (!this.isInitialized)
      return;
    iGSelectedCellsCollection selectedCells = this.iaGrid.SelectedCells;
    if (selectedCells == null || selectedCells.Count == 0 || MessageBox.Show("Подтвердите удаление выбранного атрибута(-ов): " + selectedCells.Count.ToString(), "Подтверждение", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    List<int> removed = new List<int>();
    this.RunRemoveProcess(removed);
    int num = (int) MessageBox.Show($"Удалено атрибутов: {removed.Count}", "Информация", MessageBoxButtons.OK);
    if (removed.Count > 0)
    {
      List<DataRow> dataRowList = new List<DataRow>();
      foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
      {
        int index = removed.IndexOf(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        if (index != -1)
        {
          dataRowList.Add(row);
          removed.RemoveAt(index);
        }
      }
      for (int index = 0; index < dataRowList.Count; ++index)
        this.dataTable.Rows.Remove(dataRowList[index]);
      this.FillGrid(this.dataTable);
    }
    this.UpdateControlsEx();
  }

  private void RunRemoveProcess(List<int> removed)
  {
    bool flag = false;
    int index = this.iaGrid.Cols["F_NAME"].Index;
    this.progressBar.Minimum = 0;
    this.progressBar.Maximum = this.iaGrid.SelectedCells.Count;
    this.progressBar.Step = 1;
    this.progressBar.Value = 0;
    removed.Clear();
    try
    {
      foreach (iGCell selectedCell in this.iaGrid.SelectedCells)
      {
        this.progressBar.PerformStep();
        int tag = (int) selectedCell.Row.Tag;
        string text = this.iaGrid.Cells[selectedCell.RowIndex, index].Text;
        this.AttrNameLabel.Text = string.Format(IdleAttributesDockControl.sAttribute, (object) text);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(tag, false);
          if (attributeType == null)
          {
            removed.Add(tag);
          }
          else
          {
            try
            {
              attributeType.Delete(0L);
              removed.Add(tag);
            }
            catch (Exception ex)
            {
              if (!flag)
              {
                switch (IMMessageBox.Show("Ошибка", $"\nОшибка удаления атрибута \"{text}\", id={tag}:\n\n\"{ex.Message}\"\n", new IMMessageBoxButton[3]
                {
                  new IMMessageBoxButton("Продолжить", DialogResult.Yes),
                  new IMMessageBoxButton("Продолжить для всех", DialogResult.Retry),
                  new IMMessageBoxButton("Прервать", DialogResult.Abort)
                }))
                {
                  case DialogResult.Abort:
                    return;
                  case DialogResult.Retry:
                    flag = true;
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
        }
      }
    }
    finally
    {
      this.AttrNameLabel.Text = string.Empty;
      this.progressBar.Value = 0;
    }
  }

  private void IdleAttributesDockControl_Closing(object sender, CancelEventArgs e)
  {
    if (!this.backgroundWorker.IsBusy || MessageBox.Show("В настоящий момент производится операция по поиску неиспользуемых атрибутов. Прервать?", "Запрос", MessageBoxButtons.YesNo) != DialogResult.No)
      return;
    e.Cancel = true;
  }

  private void IdleAttributesDockControl_Closed(object sender, EventArgs e)
  {
    this.controlClosed = true;
  }

  private void ClearGrid()
  {
    this.iaGrid.Rows.Clear();
    this.iaGrid.Cols.Clear();
    this.iaGrid.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this.FoundLabel.Text = string.Format(IdleAttributesDockControl.sFound, (object) 0);
    this.SelectedLabel.Text = string.Format(IdleAttributesDockControl.sSelected, (object) 0);
    this.AttrNameLabel.Text = string.Empty;
  }

  private void FillGrid(DataTable dt)
  {
    string str = "attr_type_name";
    if (dt.Columns.IndexOf(str) == -1)
    {
      dt.Columns.Add(str, typeof (string));
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
        row[str] = (object) AttributesTypeHelper.GetCaption((FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]));
    }
    this.iaGrid.FillWithData(dt);
    for (int index = 0; index < this.iaGrid.Cols.Count; ++index)
      this.iaGrid.Cols[index].Visible = false;
    int index1 = this.iaGrid.Cols["F_ATTRIBUTE_ID"].Index;
    int index2 = this.iaGrid.Cols[str].Index;
    int index3 = this.iaGrid.Cols["F_ATTRIBUTE_TYPE"].Index;
    for (int index4 = 0; index4 < this.iaGrid.Rows.Count; ++index4)
    {
      this.iaGrid.Rows[index4].Tag = (object) Convert.ToInt32(this.iaGrid.Cells[index4, index1].Text);
      if (Statics.IconSrv != null)
      {
        int num = Statics.IconSrv.IndexOf(3, -1, (object) (FieldTypes) Convert.ToInt32(this.iaGrid.Cells[index4, index3].Value));
        this.iaGrid.Cells[index4, index2].ImageIndex = num;
      }
    }
    this.iaGrid.Cols["F_ATTRIBUTE_ID"].Text = DataSetProcessor.ColumnCaptions[(object) "F_ATTRIBUTE_ID"];
    this.iaGrid.Cols["F_ATTRIBUTE_ID"].Order = 0;
    this.iaGrid.Cols["F_ATTRIBUTE_ID"].Visible = true;
    this.iaGrid.Cols["F_NAME"].Text = DataSetProcessor.ColumnCaptions[(object) "F_NAME"];
    this.iaGrid.Cols["F_NAME"].Order = 1;
    this.iaGrid.Cols["F_NAME"].Visible = true;
    this.iaGrid.Cols[str].Text = DataSetProcessor.ColumnCaptions[(object) "F_ATTRIBUTE_TYPE"];
    this.iaGrid.Cols[str].Order = 2;
    this.iaGrid.Cols[str].Visible = true;
    this.iaGrid.Cols["F_NOTE"].Text = DataSetProcessor.ColumnCaptions[(object) "F_NOTE"];
    this.iaGrid.Cols["F_NOTE"].Order = 3;
    this.iaGrid.Cols["F_NOTE"].Visible = true;
    this.iaGrid.Cols["F_GUID"].Text = DataSetProcessor.ColumnCaptions[(object) "F_GUID"];
    this.iaGrid.Cols["F_GUID"].Order = 4;
    this.iaGrid.Cols["F_GUID"].Visible = true;
    this.iaGrid.Cols["F_ALIAS"].Text = DataSetProcessor.ColumnCaptions[(object) "F_ALIAS"];
    this.iaGrid.Cols["F_ALIAS"].Order = 5;
    this.iaGrid.Cols["F_ALIAS"].Visible = true;
    this.iaGrid.Cols["F_SHORT_NAME"].Text = DataSetProcessor.ColumnCaptions[(object) "F_SHORT_NAME"];
    this.iaGrid.Cols["F_SHORT_NAME"].Order = 6;
    this.iaGrid.Cols["F_SHORT_NAME"].Visible = true;
    this.iaGrid.AutoWidthColMode = iGAutoWidthColMode.HeaderAndCells;
    for (int index5 = 0; index5 < this.iaGrid.Cols.Count; ++index5)
      this.iaGrid.Cols[index5].AutoWidth(true);
    this.FoundLabel.Text = string.Format(IdleAttributesDockControl.sFound, (object) dt.Rows.Count);
  }

  private void iaGrid_SelectionChanged(object sender, EventArgs e) => this.UpdateControlsEx();

  private void UpdateControlsEx()
  {
    int count = this.iaGrid.SelectedCells != null ? this.iaGrid.SelectedCells.Count : 0;
    this.SelectedLabel.Text = string.Format(IdleAttributesDockControl.sSelected, (object) count);
    this.UpdateControls();
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
    iGColPattern iGcolPattern3 = new iGColPattern();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IdleAttributesDockControl));
    this.backgroundWorker = new BackgroundWorker();
    this.btnStart = new Button();
    this.panel4List = new Panel();
    this.statusStrip = new StatusStrip();
    this.FoundLabel = new ToolStripStatusLabel();
    this.SelectedLabel = new ToolStripStatusLabel();
    this.progressBar = new ToolStripProgressBar();
    this.AttrNameLabel = new ToolStripStatusLabel();
    this.iaGrid = new iGrid();
    this.panel4Button = new Panel();
    this.textBox = new TextBox();
    this.btnDelete = new Button();
    this.panel4List.SuspendLayout();
    this.statusStrip.SuspendLayout();
    ((ISupportInitialize) this.iaGrid).BeginInit();
    this.panel4Button.SuspendLayout();
    this.SuspendLayout();
    this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
    this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
    this.btnStart.Location = new Point(13, 12);
    this.btnStart.Name = "btnStart";
    this.btnStart.Size = new Size(122, 23);
    this.btnStart.TabIndex = 0;
    this.btnStart.Text = "Начать поиск";
    this.btnStart.UseVisualStyleBackColor = true;
    this.btnStart.Click += new EventHandler(this.btnStart_Click);
    this.panel4List.Controls.Add((Control) this.iaGrid);
    this.panel4List.Controls.Add((Control) this.statusStrip);
    this.panel4List.Dock = DockStyle.Fill;
    this.panel4List.Location = new Point(0, 82);
    this.panel4List.Name = "panel4List";
    this.panel4List.Size = new Size(976, 281);
    this.panel4List.TabIndex = 2;
    this.statusStrip.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.FoundLabel,
      (ToolStripItem) this.SelectedLabel,
      (ToolStripItem) this.progressBar,
      (ToolStripItem) this.AttrNameLabel
    });
    this.statusStrip.Location = new Point(0, (int) byte.MaxValue);
    this.statusStrip.Name = "statusStrip";
    this.statusStrip.Size = new Size(976, 26);
    this.statusStrip.TabIndex = 6;
    this.statusStrip.Text = "statusStrip1";
    this.FoundLabel.AutoSize = false;
    this.FoundLabel.Name = "FoundLabel";
    this.FoundLabel.Size = new Size(130, 21);
    this.FoundLabel.Text = "Найдено:";
    this.FoundLabel.TextAlign = ContentAlignment.MiddleLeft;
    this.SelectedLabel.AutoSize = false;
    this.SelectedLabel.Name = "SelectedLabel";
    this.SelectedLabel.Size = new Size(130, 21);
    this.SelectedLabel.Text = "Выбрано:";
    this.SelectedLabel.TextAlign = ContentAlignment.MiddleLeft;
    this.progressBar.AutoSize = false;
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(150, 20);
    this.AttrNameLabel.Name = "AttrNameLabel";
    this.AttrNameLabel.Size = new Size(72, 21);
    this.AttrNameLabel.Text = "Атрибут:";
    this.AttrNameLabel.TextAlign = ContentAlignment.MiddleLeft;
    this.iaGrid.AutoResizeCols = true;
    iGcolPattern1.SortOrder = iGSortOrder.Descending;
    iGcolPattern1.Width = 326;
    iGcolPattern2.Width = 323;
    iGcolPattern3.Width = 323;
    this.iaGrid.Cols.AddRange(new iGColPattern[3]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3
    });
    this.iaGrid.Dock = DockStyle.Fill;
    this.iaGrid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this.iaGrid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this.iaGrid.GroupBox.HintForeColor = SystemColors.ControlText;
    this.iaGrid.GroupBox.Text = "Перетащите заголовок колонки в эту область для группировки по значениям этой колонки";
    this.iaGrid.GroupBox.Visible = true;
    this.iaGrid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this.iaGrid.Header.Height = 19;
    this.iaGrid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this.iaGrid.Location = new Point(0, 0);
    this.iaGrid.Name = "iaGrid";
    this.iaGrid.ReadOnly = true;
    this.iaGrid.RowMode = true;
    this.iaGrid.SelectionMode = iGSelectionMode.MultiExtended;
    this.iaGrid.Size = new Size(976, (int) byte.MaxValue);
    this.iaGrid.TabIndex = 5;
    this.iaGrid.Tag = (object) "     ";
    this.iaGrid.SelectionChanged += new EventHandler(this.iaGrid_SelectionChanged);
    this.panel4Button.Controls.Add((Control) this.textBox);
    this.panel4Button.Controls.Add((Control) this.btnDelete);
    this.panel4Button.Controls.Add((Control) this.btnStart);
    this.panel4Button.Dock = DockStyle.Top;
    this.panel4Button.Location = new Point(0, 0);
    this.panel4Button.Name = "panel4Button";
    this.panel4Button.Size = new Size(976, 82);
    this.panel4Button.TabIndex = 3;
    this.textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textBox.BorderStyle = BorderStyle.None;
    this.textBox.ForeColor = SystemColors.WindowText;
    this.textBox.Location = new Point(13, 41);
    this.textBox.Multiline = true;
    this.textBox.Name = "textBox";
    this.textBox.ReadOnly = true;
    this.textBox.Size = new Size(950, 35);
    this.textBox.TabIndex = 5;
    this.textBox.TabStop = false;
    this.textBox.Text = componentResourceManager.GetString("textBox.Text");
    this.btnDelete.Location = new Point(141, 12);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(122, 23);
    this.btnDelete.TabIndex = 2;
    this.btnDelete.Text = "Удалить атрибуты";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel4List);
    this.Controls.Add((Control) this.panel4Button);
    this.Name = nameof (IdleAttributesDockControl);
    this.Size = new Size(976, 363);
    this.TabText = "Неиспользуемые атрибуты";
    this.Closed += new EventHandler(this.IdleAttributesDockControl_Closed);
    this.Closing += new CancelEventHandler(this.IdleAttributesDockControl_Closing);
    this.Load += new EventHandler(this.IdleAttributesDockControl_Load);
    this.panel4List.ResumeLayout(false);
    this.panel4List.PerformLayout();
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    ((ISupportInitialize) this.iaGrid).EndInit();
    this.panel4Button.ResumeLayout(false);
    this.panel4Button.PerformLayout();
    this.ResumeLayout(false);
  }
}
