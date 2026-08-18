// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileStatesWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Components;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class FileStatesWindow : MvpWindow, IFileStatesView, IView
{
  private ITreeView fileTree;
  private IFileListView fileList;
  private IContainer components;
  private SplitContainer splitContainer1;
  private TreeView tvTree;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private iGCellStyle iGrid1Col2CellStyle;
  private iGColHdrStyle iGrid1Col2ColHdrStyle;
  private iGCellStyle iGrid1Col3CellStyle;
  private iGColHdrStyle iGrid1Col3ColHdrStyle;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private TableLayoutPanel tableLayoutPanel1;
  private TextBox textBox1;
  private ImageList ilTree;
  private ImageList ilGrid;
  private iGCellStyle iGrid1Col4CellStyle;
  private iGColHdrStyle iGrid1Col4ColHdrStyle;
  private FlowLayoutPanel flowLayoutPanel2;
  private Button btSaveUnimported;
  private Button btSaveAllUnimported;
  private TableLayoutPanel tableLayoutPanel2;
  private iGrid iGrid1;
  private Label lbToast;

  public FileStatesWindow()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.fileTree = (ITreeView) new TreeViewControlWrapper(this.tvTree);
    this.fileList = (IFileListView) new FileListGridWrapper(this.iGrid1);
  }

  string IFileStatesView.SelectedDir
  {
    get => this.textBox1.Text;
    set => this.textBox1.Text = value;
  }

  ITreeView IFileStatesView.FileTree => this.fileTree;

  IFileListView IFileStatesView.FileList => this.fileList;

  void IFileStatesView.ShowToast(string text) => this.ShowToastInternal(text);

  void IFileStatesView.HideToast() => this.ShowToastInternal((string) null);

  private void ShowToastInternal(string text)
  {
    this.lbToast.Text = text;
    this.lbToast.Visible = !string.IsNullOrEmpty(text);
  }

  void IFileStatesView.EnableSaveButton(bool enabled) => this.btSaveUnimported.Enabled = enabled;

  void IFileStatesView.EnableSaveAllButton(bool enabled)
  {
    this.btSaveAllUnimported.Enabled = enabled;
  }

  event EventHandler IFileStatesView.Save
  {
    add => this.btSaveUnimported.Click += value;
    remove => this.btSaveUnimported.Click -= value;
  }

  event EventHandler IFileStatesView.SaveAll
  {
    add => this.btSaveAllUnimported.Click += value;
    remove => this.btSaveAllUnimported.Click -= value;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileStatesWindow));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    iGColPattern iGcolPattern4 = new iGColPattern();
    iGColPattern iGcolPattern5 = new iGColPattern();
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.ilGrid = new ImageList(this.components);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col4CellStyle = new iGCellStyle(true);
    this.iGrid1Col4ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col2CellStyle = new iGCellStyle(true);
    this.iGrid1Col2ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col3CellStyle = new iGCellStyle(true);
    this.iGrid1Col3ColHdrStyle = new iGColHdrStyle(true);
    this.splitContainer1 = new SplitContainer();
    this.tvTree = new TreeView();
    this.ilTree = new ImageList(this.components);
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.iGrid1 = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.lbToast = new Label();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.textBox1 = new TextBox();
    this.flowLayoutPanel2 = new FlowLayoutPanel();
    this.btSaveUnimported = new Button();
    this.btSaveAllUnimported = new Button();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    ((ISupportInitialize) this.iGrid1).BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel2.SuspendLayout();
    this.SuspendLayout();
    this.iGrid1Col0CellStyle.ContentIndent = new iGIndent(8, 1, 8, 1);
    this.iGrid1Col0CellStyle.ImageAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col0CellStyle.ImageList = this.ilGrid;
    this.iGrid1Col0CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.ilGrid.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilGrid.ImageStream");
    this.ilGrid.TransparentColor = Color.Transparent;
    this.ilGrid.Images.SetKeyName(0, "Default");
    this.iGrid1Col0ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col4CellStyle.ContentIndent = new iGIndent(8, 1, 8, 1);
    this.iGrid1Col4CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col4ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col2CellStyle.ContentIndent = new iGIndent(8, 1, 8, 1);
    this.iGrid1Col2CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col2ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col1CellStyle.ContentIndent = new iGIndent(8, 1, 8, 1);
    this.iGrid1Col1CellStyle.TextAlign = iGContentAlignment.MiddleRight;
    this.iGrid1Col1ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col3CellStyle.ContentIndent = new iGIndent(8, 1, 8, 1);
    this.iGrid1Col3CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1Col3ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.FixedPanel = FixedPanel.Panel1;
    this.splitContainer1.Location = new Point(0, 69);
    this.splitContainer1.Margin = new Padding(0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.tvTree);
    this.splitContainer1.Panel1.Padding = new Padding(8, 4, 4, 8);
    this.splitContainer1.Panel2.Controls.Add((Control) this.tableLayoutPanel2);
    this.splitContainer1.Panel2.Padding = new Padding(4, 4, 8, 8);
    this.splitContainer1.Size = new Size(784, 393);
    this.splitContainer1.SplitterDistance = 240 /*0xF0*/;
    this.splitContainer1.SplitterWidth = 2;
    this.splitContainer1.TabIndex = 2;
    this.tvTree.Dock = DockStyle.Fill;
    this.tvTree.FullRowSelect = true;
    this.tvTree.HideSelection = false;
    this.tvTree.ImageKey = "Folder";
    this.tvTree.ImageList = this.ilTree;
    this.tvTree.ItemHeight = 22;
    this.tvTree.Location = new Point(8, 4);
    this.tvTree.Name = "tvTree";
    this.tvTree.SelectedImageKey = "OpenFolder";
    this.tvTree.ShowLines = false;
    this.tvTree.Size = new Size(228, 381);
    this.tvTree.TabIndex = 0;
    this.ilTree.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilTree.ImageStream");
    this.ilTree.TransparentColor = Color.Transparent;
    this.ilTree.Images.SetKeyName(0, "Folder");
    this.ilTree.Images.SetKeyName(1, "OpenFolder1");
    this.ilTree.Images.SetKeyName(2, "OpenFolder");
    this.tableLayoutPanel2.ColumnCount = 1;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Controls.Add((Control) this.iGrid1, 0, 1);
    this.tableLayoutPanel2.Controls.Add((Control) this.lbToast, 0, 0);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(4, 4);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 2;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Size = new Size(530, 381);
    this.tableLayoutPanel2.TabIndex = 2;
    this.iGrid1.BorderStyle = iGBorderStyle.Flat;
    iGcolPattern1.CellStyle = this.iGrid1Col0CellStyle;
    iGcolPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
    iGcolPattern1.DefaultCellImageIndex = 0;
    iGcolPattern1.Key = "Name";
    iGcolPattern1.Text = (object) "Имя";
    iGcolPattern1.Width = 86;
    iGcolPattern2.CellStyle = this.iGrid1Col4CellStyle;
    iGcolPattern2.ColHdrStyle = this.iGrid1Col4ColHdrStyle;
    iGcolPattern2.Key = "Type";
    iGcolPattern2.Text = (object) "Тип";
    iGcolPattern3.CellStyle = this.iGrid1Col2CellStyle;
    iGcolPattern3.ColHdrStyle = this.iGrid1Col2ColHdrStyle;
    iGcolPattern3.Key = "LastWriteTime";
    iGcolPattern3.Text = (object) "Дата изменения";
    iGcolPattern3.Width = 85;
    iGcolPattern4.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern4.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    iGcolPattern4.Key = "Length";
    iGcolPattern4.Text = (object) "Размер";
    iGcolPattern4.Width = 85;
    iGcolPattern5.CellStyle = this.iGrid1Col3CellStyle;
    iGcolPattern5.ColHdrStyle = this.iGrid1Col3ColHdrStyle;
    iGcolPattern5.Key = "State";
    iGcolPattern5.Text = (object) "Состояние";
    iGcolPattern5.Width = 85;
    this.iGrid1.Cols.AddRange(new iGColPattern[5]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3,
      iGcolPattern4,
      iGcolPattern5
    });
    this.iGrid1.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.iGrid1.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.iGrid1.DefaultRow.Height = 22;
    this.iGrid1.DefaultRow.NormalCellHeight = 22;
    this.iGrid1.Dock = DockStyle.Fill;
    this.iGrid1.GridLines.Mode = iGGridLinesMode.None;
    this.iGrid1.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnThemeChange;
    this.iGrid1.Header.Height = 22;
    this.iGrid1.HighlightBackColorNoFocus = SystemColors.ControlLight;
    this.iGrid1.Location = new Point(0, 36);
    this.iGrid1.Margin = new Padding(0);
    this.iGrid1.Name = "iGrid1";
    this.iGrid1.ReadOnly = true;
    this.iGrid1.RowMode = true;
    this.iGrid1.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.iGrid1.Size = new Size(530, 345);
    this.iGrid1.TabIndex = 2;
    this.iGrid1.UniqueKeys = true;
    this.iGrid1DefaultCellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.iGrid1DefaultColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.lbToast.BackColor = SystemColors.Info;
    this.lbToast.BorderStyle = BorderStyle.FixedSingle;
    this.lbToast.Dock = DockStyle.Fill;
    this.lbToast.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.lbToast.Location = new Point(0, 0);
    this.lbToast.Margin = new Padding(0, 0, 0, 4);
    this.lbToast.Name = "lbToast";
    this.lbToast.Padding = new Padding(4);
    this.lbToast.Size = new Size(530, 32 /*0x20*/);
    this.lbToast.TabIndex = 1;
    this.lbToast.TextAlign = ContentAlignment.MiddleLeft;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
    this.tableLayoutPanel1.Controls.Add((Control) this.splitContainer1, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.textBox1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel2, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Margin = new Padding(0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 3;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(784, 462);
    this.tableLayoutPanel1.TabIndex = 0;
    this.textBox1.Dock = DockStyle.Fill;
    this.textBox1.Location = new Point(8, 8);
    this.textBox1.Margin = new Padding(8, 8, 8, 4);
    this.textBox1.Name = "textBox1";
    this.textBox1.ReadOnly = true;
    this.textBox1.Size = new Size(768 /*0x0300*/, 20);
    this.textBox1.TabIndex = 0;
    this.flowLayoutPanel2.AutoSize = true;
    this.flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.flowLayoutPanel2.BorderStyle = BorderStyle.FixedSingle;
    this.flowLayoutPanel2.Controls.Add((Control) this.btSaveUnimported);
    this.flowLayoutPanel2.Controls.Add((Control) this.btSaveAllUnimported);
    this.flowLayoutPanel2.Dock = DockStyle.Fill;
    this.flowLayoutPanel2.Location = new Point(8, 34);
    this.flowLayoutPanel2.Margin = new Padding(8, 2, 8, 2);
    this.flowLayoutPanel2.Name = "flowLayoutPanel2";
    this.flowLayoutPanel2.Size = new Size(768 /*0x0300*/, 33);
    this.flowLayoutPanel2.TabIndex = 1;
    this.flowLayoutPanel2.WrapContents = false;
    this.btSaveUnimported.AutoSize = true;
    this.btSaveUnimported.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btSaveUnimported.FlatAppearance.BorderSize = 0;
    this.btSaveUnimported.FlatStyle = FlatStyle.Flat;
    this.btSaveUnimported.Location = new Point(3, 4);
    this.btSaveUnimported.Margin = new Padding(3, 4, 3, 4);
    this.btSaveUnimported.Name = "btSaveUnimported";
    this.btSaveUnimported.Size = new Size(178, 23);
    this.btSaveUnimported.TabIndex = 0;
    this.btSaveUnimported.Text = "Сохранить неимпортированные";
    this.btSaveUnimported.UseVisualStyleBackColor = true;
    this.btSaveAllUnimported.AutoSize = true;
    this.btSaveAllUnimported.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btSaveAllUnimported.FlatAppearance.BorderSize = 0;
    this.btSaveAllUnimported.FlatStyle = FlatStyle.Flat;
    this.btSaveAllUnimported.Location = new Point(187, 4);
    this.btSaveAllUnimported.Margin = new Padding(3, 4, 3, 4);
    this.btSaveAllUnimported.Name = "btSaveAllUnimported";
    this.btSaveAllUnimported.Size = new Size(199, 23);
    this.btSaveAllUnimported.TabIndex = 1;
    this.btSaveAllUnimported.Text = "Сохранить все неимпортированные";
    this.btSaveAllUnimported.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.ClientSize = new Size(784, 462);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (FileStatesWindow);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Просмотр состояния файлов в исходном архиве предприятия";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.tableLayoutPanel2.ResumeLayout(false);
    ((ISupportInitialize) this.iGrid1).EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel2.ResumeLayout(false);
    this.flowLayoutPanel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
