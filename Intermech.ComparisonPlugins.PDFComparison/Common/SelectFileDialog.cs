// Decompiled with JetBrains decompiler
// Type: Intermech.ComparisonPlugins.PDFComparison.Common.SelectFileDialog
// Assembly: Intermech.ComparisonPlugins.PDFComparison, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8B4ECC9-43EB-48A8-B8E5-C6978FF09846
// Assembly location: D:\IPS\Client\Intermech.ComparisonPlugins.PDFComparison.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ComparisonPlugins.PDFComparison.Common;

public class SelectFileDialog : Form
{
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private ListView listViewFiles;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private ColumnHeader columnHeader4;

  public FileDescription SelectedFile { get; private set; }

  public SelectFileDialog() => this.InitializeComponent();

  public SelectFileDialog(List<FileDescription> files)
    : this()
  {
    foreach (FileDescription file in files)
      this.listViewFiles.Items.Add(new ListViewItem(new string[4]
      {
        file.FileName,
        this.getFileTypeName(file.FileType),
        file.ModifyDate.ToString(),
        file.RealFileSize.ToString()
      })
      {
        Tag = (object) file
      });
  }

  private void listViewFiles_DoubleClick(object sender, EventArgs e) => this.btnOK.PerformClick();

  private void listViewFiles_ItemSelectionChanged(
    object sender,
    ListViewItemSelectionChangedEventArgs e)
  {
    if (this.listViewFiles.SelectedIndices.Count > 0)
    {
      this.SelectedFile = (FileDescription) this.listViewFiles.SelectedItems[0].Tag;
      this.btnOK.Enabled = true;
    }
    else
      this.btnOK.Enabled = false;
  }

  private string getFileTypeName(FileTypes fileType)
  {
    return new string[6]
    {
      "Файл объекта",
      "Файл не влияющий на подписи",
      "Файл ОТД",
      "Файл замечаний",
      "Аутентичный файл",
      "Неизвестный тип файла"
    }[(int) fileType];
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.listViewFiles = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader4 = new ColumnHeader();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Enabled = false;
    this.btnOK.Location = new Point(555, 13);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(100, 23);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(661, 13);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(100, 23);
    this.btnCancel.TabIndex = 5;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.listViewFiles, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.Padding = new Padding(10, 10, 10, 0);
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
    this.tableLayoutPanel1.Size = new Size(784, 361);
    this.tableLayoutPanel1.TabIndex = 6;
    this.flowLayoutPanel1.Controls.Add((Control) this.btnCancel);
    this.flowLayoutPanel1.Controls.Add((Control) this.btnOK);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(10, 311);
    this.flowLayoutPanel1.Margin = new Padding(0);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Padding = new Padding(0, 10, 0, 0);
    this.flowLayoutPanel1.Size = new Size(764, 50);
    this.flowLayoutPanel1.TabIndex = 0;
    this.listViewFiles.Columns.AddRange(new ColumnHeader[4]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3,
      this.columnHeader4
    });
    this.listViewFiles.Dock = DockStyle.Fill;
    this.listViewFiles.FullRowSelect = true;
    this.listViewFiles.GridLines = true;
    this.listViewFiles.Location = new Point(13, 13);
    this.listViewFiles.MultiSelect = false;
    this.listViewFiles.Name = "listViewFiles";
    this.listViewFiles.Size = new Size(758, 295);
    this.listViewFiles.TabIndex = 1;
    this.listViewFiles.UseCompatibleStateImageBehavior = false;
    this.listViewFiles.View = View.Details;
    this.listViewFiles.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listViewFiles_ItemSelectionChanged);
    this.listViewFiles.DoubleClick += new EventHandler(this.listViewFiles_DoubleClick);
    this.columnHeader1.Text = "Имя файла";
    this.columnHeader1.Width = 300;
    this.columnHeader2.Text = "Тип файла";
    this.columnHeader2.Width = 200;
    this.columnHeader3.Text = "Дата модификации";
    this.columnHeader3.Width = 150;
    this.columnHeader4.Text = "Размер";
    this.columnHeader4.Width = 100;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(784, 361);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MinimumSize = new Size(800, 400);
    this.Name = "SelectFileForm";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выберите файл для сравнения";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
