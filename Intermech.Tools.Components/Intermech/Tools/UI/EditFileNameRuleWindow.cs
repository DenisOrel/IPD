// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.EditFileNameRuleWindow
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Mvp;
using Intermech.Mvp.Components;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.UI;

internal class EditFileNameRuleWindow : 
  MvpWindow,
  IEditFileNameRuleView,
  IView,
  IOperationConfirmationView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Label label1;
  private Label label2;
  private Label label3;
  private TextBox textBox1;
  private TextBox textBox2;
  private TextBox textBox3;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button btCancel;
  private Button btOK;
  private Label lbDescription;

  public EditFileNameRuleWindow() => this.InitializeComponent();

  string IEditFileNameRuleView.Description
  {
    get => this.lbDescription.Text;
    set => this.lbDescription.Text = value;
  }

  string IEditFileNameRuleView.Extension
  {
    get => this.textBox1.Text;
    set => this.textBox1.Text = value;
  }

  string IEditFileNameRuleView.NamePattern
  {
    get => this.textBox2.Text;
    set => this.textBox2.Text = value;
  }

  string IEditFileNameRuleView.Directory
  {
    get => this.textBox3.Text;
    set => this.textBox3.Text = value;
  }

  /// <summary>
  /// Событие успешного подтвержения сделанных изменений или своего выбора пользователем.
  /// После этого события взаимодействие пользователя с видом заканчивается.
  /// </summary>
  event EventHandler IOperationConfirmationView.OperationConfirmed
  {
    add => this.btOK.Click += value;
    remove => this.btOK.Click -= value;
  }

  void IEditFileNameRuleView.ResetSuccess() => this.DialogResult = DialogResult.None;

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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.label1 = new Label();
    this.label2 = new Label();
    this.label3 = new Label();
    this.textBox1 = new TextBox();
    this.textBox2 = new TextBox();
    this.textBox3 = new TextBox();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.btCancel = new Button();
    this.btOK = new Button();
    this.lbDescription = new Label();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.label1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.label3, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.textBox1, 1, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.textBox2, 1, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.textBox3, 1, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.lbDescription, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Margin = new Padding(0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 5;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(469, 277);
    this.tableLayoutPanel1.TabIndex = 0;
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Top;
    this.label1.Location = new Point(0, 61);
    this.label1.Margin = new Padding(0);
    this.label1.Name = "label1";
    this.label1.Padding = new Padding(16 /*0x10*/, 8, 4, 8);
    this.label1.Size = new Size(147, 29);
    this.label1.TabIndex = 0;
    this.label1.Text = "Расширение файлов (*):";
    this.label2.AutoSize = true;
    this.label2.Dock = DockStyle.Top;
    this.label2.Location = new Point(0, 93);
    this.label2.Margin = new Padding(0);
    this.label2.Name = "label2";
    this.label2.Padding = new Padding(16 /*0x10*/, 8, 4, 8);
    this.label2.Size = new Size(147, 29);
    this.label2.TabIndex = 1;
    this.label2.Text = "Маска имен файлов:";
    this.label3.AutoSize = true;
    this.label3.Dock = DockStyle.Top;
    this.label3.Location = new Point(0, 125);
    this.label3.Margin = new Padding(0);
    this.label3.Name = "label3";
    this.label3.Padding = new Padding(16 /*0x10*/, 8, 4, 8);
    this.label3.Size = new Size(147, 29);
    this.label3.TabIndex = 2;
    this.label3.Text = "Каталог файлов:";
    this.textBox1.Dock = DockStyle.Fill;
    this.textBox1.Location = new Point(151, 65);
    this.textBox1.Margin = new Padding(4, 4, 16 /*0x10*/, 8);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(302, 20);
    this.textBox1.TabIndex = 3;
    this.textBox2.Dock = DockStyle.Fill;
    this.textBox2.Location = new Point(151, 97);
    this.textBox2.Margin = new Padding(4, 4, 16 /*0x10*/, 8);
    this.textBox2.Name = "textBox2";
    this.textBox2.Size = new Size(302, 20);
    this.textBox2.TabIndex = 4;
    this.textBox3.Dock = DockStyle.Fill;
    this.textBox3.Location = new Point(151, 129);
    this.textBox3.Margin = new Padding(4, 4, 16 /*0x10*/, 8);
    this.textBox3.Name = "textBox3";
    this.textBox3.Size = new Size(302, 20);
    this.textBox3.TabIndex = 5;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.flowLayoutPanel1, 2);
    this.flowLayoutPanel1.Controls.Add((Control) this.btCancel);
    this.flowLayoutPanel1.Controls.Add((Control) this.btOK);
    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 197);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Padding = new Padding(0, 32 /*0x20*/, 16 /*0x10*/, 16 /*0x10*/);
    this.flowLayoutPanel1.Size = new Size(463, 77);
    this.flowLayoutPanel1.TabIndex = 6;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Dock = DockStyle.Fill;
    this.btCancel.Location = new Point(369, 35);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 1;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(288, 35);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 23);
    this.btOK.TabIndex = 0;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.lbDescription.AutoSize = true;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.lbDescription, 2);
    this.lbDescription.Dock = DockStyle.Fill;
    this.lbDescription.Location = new Point(0, 0);
    this.lbDescription.Margin = new Padding(0);
    this.lbDescription.Name = "lbDescription";
    this.lbDescription.Padding = new Padding(16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/, 32 /*0x20*/);
    this.lbDescription.Size = new Size(469, 61);
    this.lbDescription.TabIndex = 7;
    this.lbDescription.Text = "Description";
    this.lbDescription.TextAlign = ContentAlignment.BottomLeft;
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(469, 277);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(485, 315);
    this.Name = nameof (EditFileNameRuleWindow);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Редактор правил определения типа документа";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
