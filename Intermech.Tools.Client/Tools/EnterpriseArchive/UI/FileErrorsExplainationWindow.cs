// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.FileErrorsExplainationWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using Intermech.Tools.Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal sealed class FileErrorsExplainationWindow : MvpWindow, IFileErrorsExplainationView, IView
{
  private FileErrorsExplanationViewModel viewModel;
  private int lbDescriptionSizeDelta;
  private IContainer components;
  private TableLayoutPanel tlpMainGrid;
  private PictureBox pbWarning;
  private Label lbDescription;
  private FlowLayoutPanel flpButtons;
  private Button btOK;
  private Button btToggleFileList;
  private ListView lvFileList;
  private ColumnHeader chFilePath;
  private FlowLayoutPanel flpFileListToggler;
  private Label lbFileListName;
  private ColumnHeader chError;

  public FileErrorsExplainationWindow() => this.InitializeComponent();

  FileErrorsExplanationViewModel IFileErrorsExplainationView.ViewModel
  {
    get => this.viewModel;
    set
    {
      if (object.Equals((object) this.viewModel, (object) value))
        return;
      this.viewModel = value;
      if (this.viewModel == null)
        return;
      this.SuspendLayout();
      try
      {
        this.ClearView();
        this.FillView();
      }
      finally
      {
        this.ResumeLayout(true);
      }
    }
  }

  private void ClearView()
  {
    this.Text = string.Empty;
    this.lbDescription.Text = string.Empty;
    this.lbFileListName.Text = string.Empty;
    this.lvFileList.Items.Clear();
    if (!this.lvFileList.Visible)
      return;
    this.ToggleFileList();
  }

  private void FillView()
  {
    this.Text = this.viewModel.Caption;
    this.lbDescription.Text = this.viewModel.Explanation;
    this.lbFileListName.Text = this.viewModel.FileListName;
    this.lvFileList.BeginUpdate();
    try
    {
      this.lvFileList.Items.Clear();
      foreach (FileError file in (IEnumerable<FileError>) this.viewModel.FileList)
        this.lvFileList.Items.Add(new ListViewItem(file.FileName)
        {
          SubItems = {
            file.Error
          }
        });
      if (this.lvFileList.Items.Count <= 0)
        return;
      this.lvFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      this.lvFileList.Items[0].Selected = true;
    }
    finally
    {
      this.lvFileList.EndUpdate();
    }
  }

  private void btToggleFileList_Click(object sender, EventArgs e) => this.ToggleFileList();

  private void ToggleFileList()
  {
    this.lvFileList.Visible = !this.lvFileList.Visible;
    this.btToggleFileList.Image = this.lvFileList.Visible ? (Image) Resources.IR_UpSlider : (Image) Resources.IR_DownSlider;
  }

  private void tableLayoutPanel1_Resize(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this.RestrictDescriptionWidth();
  }

  private void RestrictDescriptionWidth()
  {
    int width = this.tlpMainGrid.Width - this.lbDescriptionSizeDelta;
    if (width <= 0)
      return;
    this.lbDescription.MaximumSize = new Size(width, 0);
  }

  private void Form1_Shown(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this.SetupDescriptionAutosize();
    this.InitialFileListHiding();
  }

  private void SetupDescriptionAutosize()
  {
    this.lbDescriptionSizeDelta = this.tlpMainGrid.Width - this.lbDescription.Width;
    this.lbDescription.MaximumSize = new Size(this.lbDescription.Width, 0);
    this.lbDescription.AutoSize = true;
  }

  private void InitialFileListHiding()
  {
    this.lvFileList.Dock = DockStyle.Fill;
    int num = this.lvFileList.Height + this.lvFileList.Margin.Top + this.lvFileList.Margin.Bottom;
    this.lvFileList.Visible = false;
    this.Top += num >> 1;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tlpMainGrid = new TableLayoutPanel();
    this.pbWarning = new PictureBox();
    this.lbDescription = new Label();
    this.flpButtons = new FlowLayoutPanel();
    this.btOK = new Button();
    this.lvFileList = new ListView();
    this.chFilePath = new ColumnHeader();
    this.chError = new ColumnHeader();
    this.flpFileListToggler = new FlowLayoutPanel();
    this.btToggleFileList = new Button();
    this.lbFileListName = new Label();
    this.tlpMainGrid.SuspendLayout();
    ((ISupportInitialize) this.pbWarning).BeginInit();
    this.flpButtons.SuspendLayout();
    this.flpFileListToggler.SuspendLayout();
    this.SuspendLayout();
    this.tlpMainGrid.AutoSize = true;
    this.tlpMainGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tlpMainGrid.ColumnCount = 2;
    this.tlpMainGrid.ColumnStyles.Add(new ColumnStyle());
    this.tlpMainGrid.ColumnStyles.Add(new ColumnStyle());
    this.tlpMainGrid.Controls.Add((Control) this.pbWarning, 0, 0);
    this.tlpMainGrid.Controls.Add((Control) this.lbDescription, 1, 0);
    this.tlpMainGrid.Controls.Add((Control) this.flpButtons, 0, 1);
    this.tlpMainGrid.Controls.Add((Control) this.lvFileList, 0, 3);
    this.tlpMainGrid.Controls.Add((Control) this.flpFileListToggler, 0, 2);
    this.tlpMainGrid.Dock = DockStyle.Fill;
    this.tlpMainGrid.Location = new Point(0, 0);
    this.tlpMainGrid.Name = "tlpMainGrid";
    this.tlpMainGrid.RowCount = 4;
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.Size = new Size(570, 331);
    this.tlpMainGrid.TabIndex = 0;
    this.tlpMainGrid.Resize += new EventHandler(this.tableLayoutPanel1_Resize);
    this.pbWarning.Image = (Image) Resources.IR_Warning;
    this.pbWarning.Location = new Point(12, 12);
    this.pbWarning.Margin = new Padding(12, 12, 8, 4);
    this.pbWarning.Name = "pbWarning";
    this.pbWarning.Size = new Size(48 /*0x30*/, 48 /*0x30*/);
    this.pbWarning.TabIndex = 0;
    this.pbWarning.TabStop = false;
    this.lbDescription.Dock = DockStyle.Fill;
    this.lbDescription.Location = new Point(76, 12);
    this.lbDescription.Margin = new Padding(8, 12, 12, 12);
    this.lbDescription.Name = "lbDescription";
    this.lbDescription.Size = new Size(482, 48 /*0x30*/);
    this.lbDescription.TabIndex = 0;
    this.lbDescription.Text = "Explaination description";
    this.lbDescription.TextAlign = ContentAlignment.MiddleLeft;
    this.flpButtons.AutoSize = true;
    this.flpButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tlpMainGrid.SetColumnSpan((Control) this.flpButtons, 2);
    this.flpButtons.Controls.Add((Control) this.btOK);
    this.flpButtons.Dock = DockStyle.Top;
    this.flpButtons.FlowDirection = FlowDirection.RightToLeft;
    this.flpButtons.Location = new Point(12, 80 /*0x50*/);
    this.flpButtons.Margin = new Padding(12, 8, 12, 0);
    this.flpButtons.Name = "flpButtons";
    this.flpButtons.Size = new Size(546, 29);
    this.flpButtons.TabIndex = 1;
    this.btOK.AutoSize = true;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(474, 2);
    this.btOK.Margin = new Padding(8, 2, 0, 2);
    this.btOK.Name = "btOK";
    this.btOK.Padding = new Padding(0, 1, 0, 1);
    this.btOK.Size = new Size(72, 25);
    this.btOK.TabIndex = 0;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.lvFileList.AllowColumnReorder = true;
    this.lvFileList.Columns.AddRange(new ColumnHeader[2]
    {
      this.chFilePath,
      this.chError
    });
    this.tlpMainGrid.SetColumnSpan((Control) this.lvFileList, 2);
    this.lvFileList.FullRowSelect = true;
    this.lvFileList.GridLines = true;
    this.lvFileList.HideSelection = false;
    this.lvFileList.Location = new Point(12, 145);
    this.lvFileList.Margin = new Padding(12, 4, 12, 12);
    this.lvFileList.Name = "lvFileList";
    this.lvFileList.Size = new Size(125, 174);
    this.lvFileList.TabIndex = 2;
    this.lvFileList.UseCompatibleStateImageBehavior = false;
    this.lvFileList.View = View.Details;
    this.chFilePath.Text = "Имя файла";
    this.chError.Text = "Проблема";
    this.flpFileListToggler.AutoSize = true;
    this.flpFileListToggler.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tlpMainGrid.SetColumnSpan((Control) this.flpFileListToggler, 2);
    this.flpFileListToggler.Controls.Add((Control) this.btToggleFileList);
    this.flpFileListToggler.Controls.Add((Control) this.lbFileListName);
    this.flpFileListToggler.Dock = DockStyle.Fill;
    this.flpFileListToggler.Location = new Point(12, 109);
    this.flpFileListToggler.Margin = new Padding(12, 0, 12, 8);
    this.flpFileListToggler.Name = "flpFileListToggler";
    this.flpFileListToggler.Size = new Size(546, 24);
    this.flpFileListToggler.TabIndex = 5;
    this.btToggleFileList.AutoSize = true;
    this.btToggleFileList.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btToggleFileList.Dock = DockStyle.Fill;
    this.btToggleFileList.Image = (Image) Resources.IR_DownSlider;
    this.btToggleFileList.Location = new Point(0, 0);
    this.btToggleFileList.Margin = new Padding(0, 0, 4, 0);
    this.btToggleFileList.Name = "btToggleFileList";
    this.btToggleFileList.Padding = new Padding(4);
    this.btToggleFileList.Size = new Size(24, 24);
    this.btToggleFileList.TabIndex = 0;
    this.btToggleFileList.UseVisualStyleBackColor = true;
    this.btToggleFileList.Click += new EventHandler(this.btToggleFileList_Click);
    this.lbFileListName.AutoSize = true;
    this.lbFileListName.Dock = DockStyle.Fill;
    this.lbFileListName.Location = new Point(32 /*0x20*/, 0);
    this.lbFileListName.Margin = new Padding(4, 0, 4, 0);
    this.lbFileListName.Name = "lbFileListName";
    this.lbFileListName.Size = new Size(67, 24);
    this.lbFileListName.TabIndex = 1;
    this.lbFileListName.Text = "File list name";
    this.lbFileListName.TextAlign = ContentAlignment.MiddleLeft;
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.CancelButton = (IButtonControl) this.btOK;
    this.ClientSize = new Size(570, 331);
    this.Controls.Add((Control) this.tlpMainGrid);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FileErrorsExplainationWindow);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Explaination window";
    this.Shown += new EventHandler(this.Form1_Shown);
    this.tlpMainGrid.ResumeLayout(false);
    this.tlpMainGrid.PerformLayout();
    ((ISupportInitialize) this.pbWarning).EndInit();
    this.flpButtons.ResumeLayout(false);
    this.flpButtons.PerformLayout();
    this.flpFileListToggler.ResumeLayout(false);
    this.flpFileListToggler.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
