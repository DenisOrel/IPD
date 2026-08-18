// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ImportSourceWindow
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

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ImportSourceWindow : 
  MvpWindow,
  IImportSourceView,
  IView,
  IOperationConfirmationView
{
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label1;
  private RadioButton rbImportQueue;
  private RadioButton rbListFile;
  private RadioButton rbDisk;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button btClose;

  public ImportSourceWindow() => this.InitializeComponent();

  ImportSource IImportSourceView.SelectedSource
  {
    get
    {
      if (this.rbImportQueue.Checked)
        return ImportSource.ImportQueue;
      if (this.rbListFile.Checked)
        return ImportSource.ListFile;
      return this.rbDisk.Checked ? ImportSource.Disk : ImportSource.ImportQueue;
    }
  }

  event EventHandler IOperationConfirmationView.OperationConfirmed
  {
    add => this.btClose.Click += value;
    remove => this.btClose.Click -= value;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.label1 = new Label();
    this.rbImportQueue = new RadioButton();
    this.rbListFile = new RadioButton();
    this.rbDisk = new RadioButton();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.btClose = new Button();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.AutoSize = true;
    this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbImportQueue, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbListFile, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbDisk, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 4);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.MaximumSize = new Size(500, 310);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 5;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(500, 223);
    this.tableLayoutPanel1.TabIndex = 0;
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Bottom;
    this.label1.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    this.label1.Margin = new Padding(16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(468, 26);
    this.label1.TabIndex = 0;
    this.label1.Text = "Выберите источник файлов исходного архива для импорта в IPS. В большинстве случаев можно использовать предложенный вариант без изменений.";
    this.rbImportQueue.AutoSize = true;
    this.rbImportQueue.Checked = true;
    this.rbImportQueue.Location = new Point(32 /*0x20*/, 58);
    this.rbImportQueue.Margin = new Padding(32 /*0x20*/, 8, 3, 8);
    this.rbImportQueue.Name = "rbImportQueue";
    this.rbImportQueue.Size = new Size(407, 17);
    this.rbImportQueue.TabIndex = 1;
    this.rbImportQueue.TabStop = true;
    this.rbImportQueue.Text = "Автоматически взять файлы для импорта из очереди импорта документов";
    this.rbImportQueue.UseVisualStyleBackColor = true;
    this.rbListFile.AutoSize = true;
    this.rbListFile.Location = new Point(32 /*0x20*/, 91);
    this.rbListFile.Margin = new Padding(32 /*0x20*/, 8, 3, 8);
    this.rbListFile.Name = "rbListFile";
    this.rbListFile.Size = new Size(330, 17);
    this.rbListFile.TabIndex = 2;
    this.rbListFile.Text = "Взять файлы для импорта из указанного текстового файла";
    this.rbListFile.UseVisualStyleBackColor = true;
    this.rbDisk.AutoSize = true;
    this.rbDisk.Location = new Point(32 /*0x20*/, 124);
    this.rbDisk.Margin = new Padding(32 /*0x20*/, 8, 3, 8);
    this.rbDisk.Name = "rbDisk";
    this.rbDisk.Size = new Size(382, 17);
    this.rbDisk.TabIndex = 3;
    this.rbDisk.Text = "Выбрать файлы для импорта из папки исходного архива предприятия";
    this.rbDisk.UseVisualStyleBackColor = true;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.flowLayoutPanel1.Controls.Add((Control) this.btClose);
    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(16 /*0x10*/, 182);
    this.flowLayoutPanel1.Margin = new Padding(16 /*0x10*/, 32 /*0x20*/, 16 /*0x10*/, 8);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(468, 33);
    this.flowLayoutPanel1.TabIndex = 4;
    this.btClose.AutoSize = true;
    this.btClose.DialogResult = DialogResult.OK;
    this.btClose.Location = new Point(393, 3);
    this.btClose.Margin = new Padding(0, 3, 0, 3);
    this.btClose.Name = "btClose";
    this.btClose.Padding = new Padding(2);
    this.btClose.Size = new Size(75, 27);
    this.btClose.TabIndex = 0;
    this.btClose.Text = "OK";
    this.btClose.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btClose;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.ClientSize = new Size(530, 223);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ImportSourceWindow);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Импорт исходного архива";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
