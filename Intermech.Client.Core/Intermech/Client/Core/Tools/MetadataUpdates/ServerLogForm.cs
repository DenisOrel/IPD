
// Type: Intermech.Client.Core.Tools.MetadataUpdates.ServerLogForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Intermech.Client.Core.Tools.MetadataUpdates;

public class ServerLogForm : Form
{
  private bool _initialize = true;
  private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.HighlightText));
  private SolidBrush reportsForegroundBrush = new SolidBrush(Color.FromKnownColor(KnownColor.WindowText));
  private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
  private SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.FromKnownColor(KnownColor.Window));
  private SolidBrush reportsBackgroundBrush2 = new SolidBrush(Color.LightGoldenrodYellow);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TabControl tcMain;
  private TabPage tpErrorsList;
  private ListBox lbErrorLines;
  private TabPage tpFilters;
  private Button bClose;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem miAddToFilter;
  private ToolStripMenuItem miRefresh;
  private ToolStrip toolStrip1;
  private ToolStripButton tbRefresh;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton tbAddFilter;
  private ToolStripButton tbEditFilter;
  private ToolStripButton tbDeleteFilter;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton tbClean;
  private ContextMenuStrip contextMenuStrip2;
  private ToolStripMenuItem miEditFilter;
  private ToolStripMenuItem miDeleteFilter;
  private ToolStripMenuItem miClean;
  private ListBox lbFilters;
  private ToolStripButton tbFilterOn;
  private ToolStripMenuItem miAddToFilter2;
  private ToolStripMenuItem miSave;
  private SaveFileDialog saveFileDialog1;
  private ToolStripButton tbCopy;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripMenuItem miCopy;
  private ToolStripSeparator toolStripMenuItem3;
  private ToolStripButton tbSave;

  public ServerLogForm()
  {
    this.InitializeComponent();
    FormStorage.LoadLayout((Control) this);
  }

  private void RefreshLog()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.RefreshLog(sessionKeeper.Session.GetCustomService<IUpdateLogService>());
  }

  private void RefreshLog(IUpdateLogService updateLogService)
  {
    this.LoadListBox(this.lbErrorLines, updateLogService.GetLastUpdateLog(this.tbFilterOn.Checked));
  }

  private void LoadFilters(IUpdateLogService updateLogService)
  {
    this.LoadListBox(this.lbFilters, updateLogService.Filters);
  }

  private void LoadListBox(ListBox listBox, string[] data)
  {
    listBox.Items.Clear();
    if (data == null || data.Length == 0)
      return;
    foreach (string str in data)
    {
      if (!string.IsNullOrEmpty(str.Trim()))
        listBox.Items.Add((object) str);
    }
  }

  private void Refresh_Click(object sender, EventArgs e) => this.RefreshLog();

  private void ServerLogForm_Shown(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUpdateLogService customService = sessionKeeper.Session.GetCustomService<IUpdateLogService>();
      this.RefreshLog(customService);
      this.LoadFilters(customService);
    }
    this._initialize = false;
    this.RefreshToolBox((object) this, (EventArgs) null);
  }

  private void AddToFilter_Click(object sender, EventArgs e)
  {
    if (!this.EditFilterString(this.tcMain.SelectedTab == this.tpErrorsList ? Convert.ToString(this.lbErrorLines.SelectedItem) : string.Empty, FilterEditorFormMode.Add))
      return;
    this.RefreshToolBox((object) this, (EventArgs) null);
    if (!this.tbFilterOn.Checked)
      return;
    this.RefreshLog();
  }

  private void EditFilter_Click(object sender, EventArgs e)
  {
    if (!this.EditFilterString(Convert.ToString(this.lbFilters.SelectedItem), FilterEditorFormMode.Edit))
      return;
    this.RefreshToolBox((object) this, (EventArgs) null);
    if (!this.tbFilterOn.Checked)
      return;
    this.RefreshLog();
  }

  private void DeleteFilter_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show($"Удалить фильтр {this.lbFilters.SelectedItem}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!sessionKeeper.Session.GetCustomService<IUpdateLogService>().RemoveLogFilter(Convert.ToString(this.lbFilters.SelectedItem)))
        return;
      this.lbFilters.Items.Remove(this.lbFilters.SelectedItem);
      this.RefreshToolBox((object) this, (EventArgs) null);
      if (!this.tbFilterOn.Checked)
        return;
      this.RefreshLog();
    }
  }

  private void Clean_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Вы уверены что хотите удалить все фильтры?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!sessionKeeper.Session.GetCustomService<IUpdateLogService>().ClearLogFilters())
        return;
      this.lbFilters.Items.Clear();
      this.RefreshToolBox((object) this, (EventArgs) null);
      if (!this.tbFilterOn.Checked)
        return;
      this.RefreshLog();
    }
  }

  private string TrimDateTime(string filterString)
  {
    Match match = new Regex("^\\d{2,2}.\\d{2,2}.\\d{4,4} \\d{2,2}:\\d{2,2}:\\d{2,2}> ").Match(filterString);
    return !string.IsNullOrEmpty(match.Value) ? filterString.Replace(match.Value, string.Empty) : filterString;
  }

  private void ServerLogForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private bool EditFilterString(string filterString, FilterEditorFormMode mode)
  {
    using (FilterEditorForm filterEditorForm = new FilterEditorForm(mode, this.TrimDateTime(filterString)))
    {
      if (filterEditorForm.ShowDialog() == DialogResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUpdateLogService customService = sessionKeeper.Session.GetCustomService<IUpdateLogService>();
          switch (mode)
          {
            case FilterEditorFormMode.Add:
              if (customService.AddLogFilter(filterEditorForm.FilterString) == 1)
              {
                this.lbFilters.Items.Add((object) filterEditorForm.FilterString);
                return true;
              }
              break;
            case FilterEditorFormMode.Edit:
              if (customService.EditLogFilter(filterEditorForm.OldFilterString, filterEditorForm.FilterString) == 1)
              {
                int selectedIndex = this.lbFilters.SelectedIndex;
                this.lbFilters.Items.RemoveAt(selectedIndex);
                this.lbFilters.Items.Insert(selectedIndex, (object) filterEditorForm.FilterString);
                return true;
              }
              break;
          }
        }
      }
    }
    return false;
  }

  private void RefreshToolBox(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.miRefresh.Enabled = this.tbRefresh.Enabled = this.tbFilterOn.Enabled = this.tcMain.SelectedTab == this.tpErrorsList;
    this.miAddToFilter.Enabled = this.tbAddFilter.Enabled = true;
    ToolStripMenuItem miEditFilter = this.miEditFilter;
    ToolStripMenuItem miDeleteFilter = this.miDeleteFilter;
    ToolStripButton tbEditFilter = this.tbEditFilter;
    bool flag1;
    this.tbDeleteFilter.Enabled = flag1 = this.tcMain.SelectedTab == this.tpFilters && this.lbFilters.SelectedItem != null;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    tbEditFilter.Enabled = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    miDeleteFilter.Enabled = num2 != 0;
    int num3 = flag3 ? 1 : 0;
    miEditFilter.Enabled = num3 != 0;
    this.miClean.Enabled = this.tbClean.Enabled = this.tcMain.SelectedTab == this.tpFilters && this.lbFilters.Items.Count > 0;
    this.miCopy.Enabled = this.tbCopy.Enabled = this.tcMain.SelectedTab == this.tpErrorsList && this.lbErrorLines.SelectedItem != null;
    this.miSave.Enabled = this.tbSave.Enabled = this.tcMain.SelectedTab == this.tpErrorsList && this.lbErrorLines.Items.Count > 0;
  }

  private void tbFilterOn_Click(object sender, EventArgs e)
  {
    this.tbFilterOn.Checked = !this.tbFilterOn.Checked;
    this.RefreshLog();
  }

  private void miSave_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog1.ShowDialog() != DialogResult.OK || !(this.saveFileDialog1.FileName != string.Empty))
      return;
    using (StreamWriter streamWriter = new StreamWriter(this.saveFileDialog1.FileName))
    {
      foreach (string str in this.lbErrorLines.Items)
        streamWriter.WriteLine(str);
    }
    int num = (int) MessageBox.Show("Лог успешно записан в файл!", "Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void lbErrorLines_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
    int index = e.Index;
    if (index >= 0 && index < this.lbErrorLines.Items.Count)
    {
      string s = this.lbErrorLines.Items[index].ToString();
      SolidBrush solidBrush1 = !flag ? (!s.Contains("Ошибка при") ? this.reportsBackgroundBrush1 : this.reportsBackgroundBrush2) : this.reportsBackgroundBrushSelected;
      e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
      SolidBrush solidBrush2 = flag ? this.reportsForegroundBrushSelected : this.reportsForegroundBrush;
      e.Graphics.DrawString(s, e.Font, (Brush) solidBrush2, (PointF) this.lbErrorLines.GetItemRectangle(index).Location);
    }
    e.DrawFocusRectangle();
  }

  private void lbFilters_DoubleClick(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(Convert.ToString(this.lbFilters.SelectedItem)))
      return;
    this.EditFilter_Click((object) this, (EventArgs) null);
  }

  private void lbErrorLines_DoubleClick(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(Convert.ToString(this.lbErrorLines.SelectedItem)))
      return;
    this.AddToFilter_Click((object) this, (EventArgs) null);
  }

  private void Copy_Click(object sender, EventArgs e)
  {
    string text = Convert.ToString(this.lbErrorLines.SelectedItem);
    if (string.IsNullOrEmpty(text))
      return;
    Clipboard.SetText(text);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ServerLogForm));
    this.tcMain = new TabControl();
    this.tpErrorsList = new TabPage();
    this.lbErrorLines = new ListBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.miAddToFilter = new ToolStripMenuItem();
    this.miRefresh = new ToolStripMenuItem();
    this.miSave = new ToolStripMenuItem();
    this.tpFilters = new TabPage();
    this.contextMenuStrip2 = new ContextMenuStrip(this.components);
    this.miAddToFilter2 = new ToolStripMenuItem();
    this.miEditFilter = new ToolStripMenuItem();
    this.miDeleteFilter = new ToolStripMenuItem();
    this.miClean = new ToolStripMenuItem();
    this.lbFilters = new ListBox();
    this.bClose = new Button();
    this.toolStrip1 = new ToolStrip();
    this.tbRefresh = new ToolStripButton();
    this.tbFilterOn = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.tbCopy = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this.tbAddFilter = new ToolStripButton();
    this.tbEditFilter = new ToolStripButton();
    this.tbDeleteFilter = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.tbClean = new ToolStripButton();
    this.saveFileDialog1 = new SaveFileDialog();
    this.miCopy = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripSeparator();
    this.tbSave = new ToolStripButton();
    this.tcMain.SuspendLayout();
    this.tpErrorsList.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.tpFilters.SuspendLayout();
    this.contextMenuStrip2.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    this.tcMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tcMain.Controls.Add((Control) this.tpErrorsList);
    this.tcMain.Controls.Add((Control) this.tpFilters);
    this.tcMain.Location = new Point(12, 28);
    this.tcMain.Name = "tcMain";
    this.tcMain.SelectedIndex = 0;
    this.tcMain.Size = new Size(520, 422);
    this.tcMain.TabIndex = 0;
    this.tcMain.SelectedIndexChanged += new EventHandler(this.RefreshToolBox);
    this.tpErrorsList.Controls.Add((Control) this.lbErrorLines);
    this.tpErrorsList.Location = new Point(4, 22);
    this.tpErrorsList.Name = "tpErrorsList";
    this.tpErrorsList.Padding = new Padding(3);
    this.tpErrorsList.Size = new Size(512 /*0x0200*/, 396);
    this.tpErrorsList.TabIndex = 0;
    this.tpErrorsList.Text = "Ошибки автообновления";
    this.tpErrorsList.UseVisualStyleBackColor = true;
    this.lbErrorLines.ContextMenuStrip = this.contextMenuStrip1;
    this.lbErrorLines.Dock = DockStyle.Fill;
    this.lbErrorLines.DrawMode = DrawMode.OwnerDrawFixed;
    this.lbErrorLines.FormattingEnabled = true;
    this.lbErrorLines.HorizontalScrollbar = true;
    this.lbErrorLines.Location = new Point(3, 3);
    this.lbErrorLines.Name = "lbErrorLines";
    this.lbErrorLines.Size = new Size(506, 390);
    this.lbErrorLines.TabIndex = 0;
    this.lbErrorLines.DrawItem += new DrawItemEventHandler(this.lbErrorLines_DrawItem);
    this.lbErrorLines.SelectedIndexChanged += new EventHandler(this.RefreshToolBox);
    this.lbErrorLines.DoubleClick += new EventHandler(this.lbErrorLines_DoubleClick);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.miRefresh,
      (ToolStripItem) this.miAddToFilter,
      (ToolStripItem) this.miCopy,
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.miSave
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(222, 98);
    this.miAddToFilter.Image = (Image) Resources.AddStandart;
    this.miAddToFilter.Name = "miAddToFilter";
    this.miAddToFilter.ShortcutKeys = Keys.A | Keys.Control;
    this.miAddToFilter.Size = new Size(221, 22);
    this.miAddToFilter.Text = "Добавить в фильтр";
    this.miAddToFilter.Click += new EventHandler(this.AddToFilter_Click);
    this.miRefresh.Image = (Image) Resources.refresh;
    this.miRefresh.Name = "miRefresh";
    this.miRefresh.ShortcutKeys = Keys.R | Keys.Control;
    this.miRefresh.Size = new Size(221, 22);
    this.miRefresh.Text = "Обновить";
    this.miRefresh.Click += new EventHandler(this.Refresh_Click);
    this.miSave.Image = (Image) Resources.Save1;
    this.miSave.Name = "miSave";
    this.miSave.ShortcutKeys = Keys.S | Keys.Control;
    this.miSave.Size = new Size(221, 22);
    this.miSave.Text = "Сохранить в файл";
    this.miSave.Click += new EventHandler(this.miSave_Click);
    this.tpFilters.ContextMenuStrip = this.contextMenuStrip2;
    this.tpFilters.Controls.Add((Control) this.lbFilters);
    this.tpFilters.Location = new Point(4, 22);
    this.tpFilters.Name = "tpFilters";
    this.tpFilters.Padding = new Padding(3);
    this.tpFilters.Size = new Size(512 /*0x0200*/, 396);
    this.tpFilters.TabIndex = 1;
    this.tpFilters.Text = "Фильтрация ошибок";
    this.tpFilters.UseVisualStyleBackColor = true;
    this.contextMenuStrip2.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.miAddToFilter2,
      (ToolStripItem) this.miEditFilter,
      (ToolStripItem) this.miDeleteFilter,
      (ToolStripItem) this.miClean
    });
    this.contextMenuStrip2.Name = "contextMenuStrip2";
    this.contextMenuStrip2.Size = new Size(222, 114);
    this.miAddToFilter2.Image = (Image) Resources.AddStandart;
    this.miAddToFilter2.Name = "miAddToFilter2";
    this.miAddToFilter2.ShortcutKeys = Keys.A | Keys.Control;
    this.miAddToFilter2.Size = new Size(221, 22);
    this.miAddToFilter2.Text = "Добавить в фильтр";
    this.miAddToFilter2.Click += new EventHandler(this.AddToFilter_Click);
    this.miEditFilter.Image = (Image) Resources.EditStandart;
    this.miEditFilter.Name = "miEditFilter";
    this.miEditFilter.ShortcutKeys = Keys.E | Keys.Control;
    this.miEditFilter.Size = new Size(221, 22);
    this.miEditFilter.Text = "Изменить фильтр";
    this.miEditFilter.Click += new EventHandler(this.EditFilter_Click);
    this.miDeleteFilter.Image = (Image) Resources.DeleteStandart;
    this.miDeleteFilter.Name = "miDeleteFilter";
    this.miDeleteFilter.ShortcutKeys = Keys.D | Keys.Control;
    this.miDeleteFilter.Size = new Size(221, 22);
    this.miDeleteFilter.Text = "Удалить фильтр";
    this.miDeleteFilter.Click += new EventHandler(this.DeleteFilter_Click);
    this.miClean.Image = (Image) Resources.Clean;
    this.miClean.Name = "miClean";
    this.miClean.ShortcutKeys = Keys.Delete | Keys.Control;
    this.miClean.Size = new Size(221, 22);
    this.miClean.Text = "Очистить";
    this.miClean.Click += new EventHandler(this.Clean_Click);
    this.lbFilters.Dock = DockStyle.Fill;
    this.lbFilters.FormattingEnabled = true;
    this.lbFilters.Location = new Point(3, 3);
    this.lbFilters.Name = "lbFilters";
    this.lbFilters.Size = new Size(506, 390);
    this.lbFilters.TabIndex = 0;
    this.lbFilters.SelectedIndexChanged += new EventHandler(this.RefreshToolBox);
    this.lbFilters.DoubleClick += new EventHandler(this.lbFilters_DoubleClick);
    this.bClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bClose.DialogResult = DialogResult.Cancel;
    this.bClose.Location = new Point(404, 456);
    this.bClose.Name = "bClose";
    this.bClose.Size = new Size(121, 27);
    this.bClose.TabIndex = 2;
    this.bClose.Text = "Закрыть";
    this.bClose.UseVisualStyleBackColor = true;
    this.toolStrip1.Items.AddRange(new ToolStripItem[11]
    {
      (ToolStripItem) this.tbRefresh,
      (ToolStripItem) this.tbFilterOn,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.tbSave,
      (ToolStripItem) this.tbCopy,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this.tbAddFilter,
      (ToolStripItem) this.tbEditFilter,
      (ToolStripItem) this.tbDeleteFilter,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.tbClean
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(544, 25);
    this.toolStrip1.TabIndex = 3;
    this.toolStrip1.Text = "toolStrip1";
    this.tbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbRefresh.Image = (Image) Resources.Update;
    this.tbRefresh.ImageTransparentColor = Color.Magenta;
    this.tbRefresh.Name = "tbRefresh";
    this.tbRefresh.Size = new Size(23, 22);
    this.tbRefresh.Text = "Обновить список ошибок";
    this.tbRefresh.ToolTipText = "Обновить список ошибок";
    this.tbRefresh.Click += new EventHandler(this.Refresh_Click);
    this.tbFilterOn.Checked = true;
    this.tbFilterOn.CheckState = CheckState.Checked;
    this.tbFilterOn.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbFilterOn.Image = (Image) componentResourceManager.GetObject("tbFilterOn.Image");
    this.tbFilterOn.ImageTransparentColor = Color.Magenta;
    this.tbFilterOn.Name = "tbFilterOn";
    this.tbFilterOn.Size = new Size(23, 22);
    this.tbFilterOn.Text = "Фильтрация списка";
    this.tbFilterOn.Click += new EventHandler(this.tbFilterOn_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this.tbCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbCopy.Image = (Image) Resources.Copy;
    this.tbCopy.ImageTransparentColor = Color.Magenta;
    this.tbCopy.Name = "tbCopy";
    this.tbCopy.Size = new Size(23, 22);
    this.tbCopy.Text = "Копировать текст";
    this.tbCopy.Click += new EventHandler(this.Copy_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(6, 25);
    this.tbAddFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbAddFilter.Image = (Image) Resources.AddStandart;
    this.tbAddFilter.ImageTransparentColor = Color.Magenta;
    this.tbAddFilter.Name = "tbAddFilter";
    this.tbAddFilter.Size = new Size(23, 22);
    this.tbAddFilter.Text = "Добавить новый фильтр";
    this.tbAddFilter.ToolTipText = "Добавить новый фильтр";
    this.tbAddFilter.Click += new EventHandler(this.AddToFilter_Click);
    this.tbEditFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbEditFilter.Image = (Image) Resources.EditStandart;
    this.tbEditFilter.ImageTransparentColor = Color.Magenta;
    this.tbEditFilter.Name = "tbEditFilter";
    this.tbEditFilter.Size = new Size(23, 22);
    this.tbEditFilter.Text = "Редактировать выделенный фильтр";
    this.tbEditFilter.ToolTipText = "Редактировать выделенный фильтр";
    this.tbEditFilter.Click += new EventHandler(this.EditFilter_Click);
    this.tbDeleteFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbDeleteFilter.Image = (Image) Resources.DeleteStandart;
    this.tbDeleteFilter.ImageTransparentColor = Color.Magenta;
    this.tbDeleteFilter.Name = "tbDeleteFilter";
    this.tbDeleteFilter.Size = new Size(23, 22);
    this.tbDeleteFilter.Text = "Удалить выделенный фильтр";
    this.tbDeleteFilter.ToolTipText = "Удалить выделенный фильтр";
    this.tbDeleteFilter.Click += new EventHandler(this.DeleteFilter_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 25);
    this.tbClean.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbClean.Image = (Image) Resources.Clean;
    this.tbClean.ImageTransparentColor = Color.Magenta;
    this.tbClean.Name = "tbClean";
    this.tbClean.Size = new Size(23, 22);
    this.tbClean.Text = "Удалить все фильтры";
    this.tbClean.ToolTipText = "Удалить все фильтры";
    this.tbClean.Click += new EventHandler(this.Clean_Click);
    this.saveFileDialog1.CheckFileExists = true;
    this.saveFileDialog1.DefaultExt = "txt";
    this.saveFileDialog1.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
    this.saveFileDialog1.RestoreDirectory = true;
    this.saveFileDialog1.Title = "Сохранение лога в текстовый файл";
    this.miCopy.Image = (Image) Resources.Copy;
    this.miCopy.Name = "miCopy";
    this.miCopy.ShortcutKeys = Keys.C | Keys.Control;
    this.miCopy.Size = new Size(221, 22);
    this.miCopy.Text = "Копировать";
    this.miCopy.Click += new EventHandler(this.Copy_Click);
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new Size(218, 6);
    this.tbSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tbSave.Image = (Image) Resources.Save1;
    this.tbSave.ImageTransparentColor = Color.Magenta;
    this.tbSave.Name = "tbSave";
    this.tbSave.Size = new Size(23, 22);
    this.tbSave.Text = "Сохранить лог в текстовый файл";
    this.tbSave.Click += new EventHandler(this.miSave_Click);
    this.AcceptButton = (IButtonControl) this.bClose;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bClose;
    this.ClientSize = new Size(544, 493);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.bClose);
    this.Controls.Add((Control) this.tcMain);
    this.MinimumSize = new Size(560, 380);
    this.Name = nameof (ServerLogForm);
    this.Text = "Диагностика ошибок патча базы данных скриптами автообновления";
    this.FormClosing += new FormClosingEventHandler(this.ServerLogForm_FormClosing);
    this.Shown += new EventHandler(this.ServerLogForm_Shown);
    this.tcMain.ResumeLayout(false);
    this.tpErrorsList.ResumeLayout(false);
    this.contextMenuStrip1.ResumeLayout(false);
    this.tpFilters.ResumeLayout(false);
    this.contextMenuStrip2.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
