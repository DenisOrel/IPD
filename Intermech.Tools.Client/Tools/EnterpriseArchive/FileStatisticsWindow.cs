// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileStatisticsWindow
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class FileStatisticsWindow : MvpWindow, IFileStatisticsView, IView
{
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label1;
  private Label label2;
  private Label label3;
  private Label label4;
  private Label label5;
  private Label label6;
  private Label label7;
  private Label label8;
  private ProgressBar progressBar1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button button1;
  private Label label9;

  public FileStatisticsWindow() => this.InitializeComponent();

  private void button1_Click(object sender, EventArgs e) => this.Close();

  void IFileStatisticsView.SetMessage(string text) => this.label9.Text = text;

  void IFileStatisticsView.ToggleProgressBar(bool toggleVisible)
  {
    if (toggleVisible)
    {
      this.progressBar1.Value = this.progressBar1.Minimum;
      this.progressBar1.Style = ProgressBarStyle.Marquee;
    }
    else
    {
      this.progressBar1.Value = this.progressBar1.Maximum;
      this.progressBar1.Style = ProgressBarStyle.Blocks;
    }
  }

  void IFileStatisticsView.SetTotalFiles(string text) => this.label5.Text = text;

  void IFileStatisticsView.SetImportedFiles(string text) => this.label6.Text = text;

  void IFileStatisticsView.SetInProgressFiles(string text) => this.label7.Text = text;

  void IFileStatisticsView.SetNotImportedFiles(string text) => this.label8.Text = text;

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
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.label5 = new Label();
    this.label6 = new Label();
    this.label7 = new Label();
    this.label8 = new Label();
    this.progressBar1 = new ProgressBar();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.button1 = new Button();
    this.label9 = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.label3, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.label4, 0, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.label5, 1, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.label6, 1, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.label7, 1, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.label8, 1, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this.progressBar1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 6);
    this.tableLayoutPanel1.Controls.Add((Control) this.label9, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Margin = new Padding(0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 7;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(463, 291);
    this.tableLayoutPanel1.TabIndex = 0;
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Left;
    this.label1.Location = new Point(16 /*0x10*/, 77);
    this.label1.Margin = new Padding(16 /*0x10*/, 4, 0, 4);
    this.label1.Name = "label1";
    this.label1.Padding = new Padding(0, 2, 0, 2);
    this.label1.Size = new Size(81, 17);
    this.label1.TabIndex = 2;
    this.label1.Text = "Всего файлов:";
    this.label2.AutoSize = true;
    this.label2.Dock = DockStyle.Left;
    this.label2.Location = new Point(16 /*0x10*/, 102);
    this.label2.Margin = new Padding(16 /*0x10*/, 4, 0, 4);
    this.label2.Name = "label2";
    this.label2.Padding = new Padding(0, 2, 0, 2);
    this.label2.Size = new Size(91, 17);
    this.label2.TabIndex = 4;
    this.label2.Text = "Импортировано:";
    this.label3.AutoSize = true;
    this.label3.Dock = DockStyle.Left;
    this.label3.Location = new Point(16 /*0x10*/, (int) sbyte.MaxValue);
    this.label3.Margin = new Padding(16 /*0x10*/, 4, 0, 4);
    this.label3.Name = "label3";
    this.label3.Padding = new Padding(0, 2, 0, 2);
    this.label3.Size = new Size(114, 17);
    this.label3.TabIndex = 6;
    this.label3.Text = "В процессе импорта:";
    this.label4.AutoSize = true;
    this.label4.Dock = DockStyle.Left;
    this.label4.Location = new Point(16 /*0x10*/, 152);
    this.label4.Margin = new Padding(16 /*0x10*/, 4, 0, 4);
    this.label4.Name = "label4";
    this.label4.Padding = new Padding(0, 2, 0, 2);
    this.label4.Size = new Size(106, 17);
    this.label4.TabIndex = 8;
    this.label4.Text = "Не импортировано:";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(185, 77);
    this.label5.Margin = new Padding(0, 4, 0, 4);
    this.label5.Name = "label5";
    this.label5.Padding = new Padding(0, 2, 0, 2);
    this.label5.Size = new Size(13, 17);
    this.label5.TabIndex = 3;
    this.label5.Text = "0";
    this.label6.AutoSize = true;
    this.label6.Location = new Point(185, 102);
    this.label6.Margin = new Padding(0, 4, 0, 4);
    this.label6.Name = "label6";
    this.label6.Padding = new Padding(0, 2, 0, 2);
    this.label6.Size = new Size(13, 17);
    this.label6.TabIndex = 5;
    this.label6.Text = "0";
    this.label7.AutoSize = true;
    this.label7.Location = new Point(185, (int) sbyte.MaxValue);
    this.label7.Margin = new Padding(0, 4, 0, 4);
    this.label7.Name = "label7";
    this.label7.Padding = new Padding(0, 2, 0, 2);
    this.label7.Size = new Size(13, 17);
    this.label7.TabIndex = 7;
    this.label7.Text = "0";
    this.label8.AutoSize = true;
    this.label8.Location = new Point(185, 152);
    this.label8.Margin = new Padding(0, 4, 0, 4);
    this.label8.Name = "label8";
    this.label8.Padding = new Padding(0, 2, 0, 2);
    this.label8.Size = new Size(13, 17);
    this.label8.TabIndex = 9;
    this.label8.Text = "0";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.progressBar1, 2);
    this.progressBar1.Dock = DockStyle.Bottom;
    this.progressBar1.Location = new Point(16 /*0x10*/, 37);
    this.progressBar1.Margin = new Padding(16 /*0x10*/, 4, 16 /*0x10*/, 24);
    this.progressBar1.Name = "progressBar1";
    this.progressBar1.Size = new Size(431, 12);
    this.progressBar1.Style = ProgressBarStyle.Marquee;
    this.progressBar1.TabIndex = 1;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.flowLayoutPanel1, 2);
    this.flowLayoutPanel1.Controls.Add((Control) this.button1);
    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(16 /*0x10*/, 252);
    this.flowLayoutPanel1.Margin = new Padding(16 /*0x10*/);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(431, 23);
    this.flowLayoutPanel1.TabIndex = 10;
    this.button1.AutoSize = true;
    this.button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.button1.DialogResult = DialogResult.Cancel;
    this.button1.Location = new Point(362, 0);
    this.button1.Margin = new Padding(0);
    this.button1.Name = "button1";
    this.button1.Padding = new Padding(4, 0, 4, 0);
    this.button1.Size = new Size(69, 23);
    this.button1.TabIndex = 0;
    this.button1.Text = "Закрыть";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    this.label9.AutoSize = true;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.label9, 2);
    this.label9.Dock = DockStyle.Fill;
    this.label9.Location = new Point(0, 0);
    this.label9.Margin = new Padding(0);
    this.label9.Name = "label9";
    this.label9.Padding = new Padding(14, 16 /*0x10*/, 14, 4);
    this.label9.Size = new Size(463, 33);
    this.label9.TabIndex = 0;
    this.label9.Text = "Message";
    this.label9.TextAlign = ContentAlignment.BottomLeft;
    this.AcceptButton = (IButtonControl) this.button1;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button1;
    this.ClientSize = new Size(463, 291);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FileStatisticsWindow);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Просмотр статистики по импорту файлов";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
