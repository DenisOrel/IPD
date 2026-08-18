// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.UI.ApplyFileNameRuleWindow
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

internal class ApplyFileNameRuleWindow : 
  MvpWindow,
  IApplyFileNameRuleView,
  IView,
  IOperationConfirmationView
{
  private FileNameRuleAction userAnswer;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button btOK;
  private Label lbDescription;
  private RadioButton rbAllowForAll;
  private RadioButton rbAllowForCurrent;
  private RadioButton rbDenyForCurrent;
  private RadioButton rbDenyForAll;
  private RadioButton rbEditRuleAndRecheck;

  public ApplyFileNameRuleWindow() => this.InitializeComponent();

  private void ApplyRuleWindow_Shown(object sender, EventArgs e)
  {
    this.rbAllowForAll.Checked = true;
  }

  private void ApplyRuleWindow_Load(object sender, EventArgs e)
  {
    this.rbAllowForAll.Tag = (object) FileNameRuleAction.AllowForAll;
    this.rbAllowForCurrent.Tag = (object) FileNameRuleAction.AllowForCurrent;
    this.rbDenyForCurrent.Tag = (object) FileNameRuleAction.DenyForCurrent;
    this.rbDenyForAll.Tag = (object) FileNameRuleAction.DenyForAll;
    this.rbEditRuleAndRecheck.Tag = (object) FileNameRuleAction.EditRuleAndRecheck;
  }

  private void RadioButton_CheckedChanged(object sender, EventArgs e)
  {
    RadioButton radioButton = (RadioButton) sender;
    if (!radioButton.Checked)
      return;
    this.userAnswer = (FileNameRuleAction) radioButton.Tag;
  }

  string IApplyFileNameRuleView.Description
  {
    get => this.lbDescription.Text;
    set => this.lbDescription.Text = value;
  }

  FileNameRuleAction IApplyFileNameRuleView.UserAnswer => this.userAnswer;

  /// <summary>
  /// Событие успешного подтвержения сделанных изменений или своего выбора пользователем.
  /// После этого события взаимодействие пользователя с видом заканчивается.
  /// </summary>
  event EventHandler IOperationConfirmationView.OperationConfirmed
  {
    add => this.btOK.Click += value;
    remove => this.btOK.Click -= value;
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.btOK = new Button();
    this.lbDescription = new Label();
    this.rbAllowForAll = new RadioButton();
    this.rbAllowForCurrent = new RadioButton();
    this.rbDenyForCurrent = new RadioButton();
    this.rbDenyForAll = new RadioButton();
    this.rbEditRuleAndRecheck = new RadioButton();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 6);
    this.tableLayoutPanel1.Controls.Add((Control) this.lbDescription, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbAllowForAll, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbAllowForCurrent, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbDenyForCurrent, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbDenyForAll, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.rbEditRuleAndRecheck, 0, 5);
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
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(584, 322);
    this.tableLayoutPanel1.TabIndex = 0;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.flowLayoutPanel1.Controls.Add((Control) this.btOK);
    this.flowLayoutPanel1.Dock = DockStyle.Bottom;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(0, 261);
    this.flowLayoutPanel1.Margin = new Padding(0);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Padding = new Padding(0, 16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/);
    this.flowLayoutPanel1.Size = new Size(584, 61);
    this.flowLayoutPanel1.TabIndex = 0;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(490, 19);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 23);
    this.btOK.TabIndex = 0;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.lbDescription.AutoSize = true;
    this.lbDescription.Dock = DockStyle.Fill;
    this.lbDescription.Location = new Point(0, 0);
    this.lbDescription.Margin = new Padding(0);
    this.lbDescription.Name = "lbDescription";
    this.lbDescription.Padding = new Padding(16 /*0x10*/, 16 /*0x10*/, 16 /*0x10*/, 32 /*0x20*/);
    this.lbDescription.Size = new Size(584, 61);
    this.lbDescription.TabIndex = 1;
    this.lbDescription.Text = "Description";
    this.lbDescription.TextAlign = ContentAlignment.BottomLeft;
    this.rbAllowForAll.AutoSize = true;
    this.rbAllowForAll.Location = new Point(0, 61);
    this.rbAllowForAll.Margin = new Padding(0);
    this.rbAllowForAll.Name = "rbAllowForAll";
    this.rbAllowForAll.Padding = new Padding(48 /*0x30*/, 8, 16 /*0x10*/, 8);
    this.rbAllowForAll.Size = new Size(226, 33);
    this.rbAllowForAll.TabIndex = 2;
    this.rbAllowForAll.TabStop = true;
    this.rbAllowForAll.Text = "Да, для всех таких файлов";
    this.rbAllowForAll.UseVisualStyleBackColor = true;
    this.rbAllowForAll.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    this.rbAllowForCurrent.AutoSize = true;
    this.rbAllowForCurrent.Location = new Point(0, 94);
    this.rbAllowForCurrent.Margin = new Padding(0);
    this.rbAllowForCurrent.Name = "rbAllowForCurrent";
    this.rbAllowForCurrent.Padding = new Padding(48 /*0x30*/, 8, 16 /*0x10*/, 8);
    this.rbAllowForCurrent.Size = new Size(232, 33);
    this.rbAllowForCurrent.TabIndex = 3;
    this.rbAllowForCurrent.TabStop = true;
    this.rbAllowForCurrent.Text = "Да, только для этого файла";
    this.rbAllowForCurrent.UseVisualStyleBackColor = true;
    this.rbAllowForCurrent.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    this.rbDenyForCurrent.AutoSize = true;
    this.rbDenyForCurrent.Location = new Point(0, (int) sbyte.MaxValue);
    this.rbDenyForCurrent.Margin = new Padding(0);
    this.rbDenyForCurrent.Name = "rbDenyForCurrent";
    this.rbDenyForCurrent.Padding = new Padding(48 /*0x30*/, 8, 16 /*0x10*/, 8);
    this.rbDenyForCurrent.Size = new Size(236, 33);
    this.rbDenyForCurrent.TabIndex = 4;
    this.rbDenyForCurrent.TabStop = true;
    this.rbDenyForCurrent.Text = "Нет, только для этого файла";
    this.rbDenyForCurrent.UseVisualStyleBackColor = true;
    this.rbDenyForCurrent.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    this.rbDenyForAll.AutoSize = true;
    this.rbDenyForAll.Location = new Point(0, 160 /*0xA0*/);
    this.rbDenyForAll.Margin = new Padding(0);
    this.rbDenyForAll.Name = "rbDenyForAll";
    this.rbDenyForAll.Padding = new Padding(48 /*0x30*/, 8, 16 /*0x10*/, 8);
    this.rbDenyForAll.Size = new Size(230, 33);
    this.rbDenyForAll.TabIndex = 5;
    this.rbDenyForAll.TabStop = true;
    this.rbDenyForAll.Text = "Нет, для всех таких файлов";
    this.rbDenyForAll.UseVisualStyleBackColor = true;
    this.rbDenyForAll.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    this.rbEditRuleAndRecheck.AutoSize = true;
    this.rbEditRuleAndRecheck.Location = new Point(0, 193);
    this.rbEditRuleAndRecheck.Margin = new Padding(0);
    this.rbEditRuleAndRecheck.Name = "rbEditRuleAndRecheck";
    this.rbEditRuleAndRecheck.Padding = new Padding(48 /*0x30*/, 8, 16 /*0x10*/, 8);
    this.rbEditRuleAndRecheck.Size = new Size(295, 33);
    this.rbEditRuleAndRecheck.TabIndex = 6;
    this.rbEditRuleAndRecheck.TabStop = true;
    this.rbEditRuleAndRecheck.Text = "Изменить правило и попробовать снова";
    this.rbEditRuleAndRecheck.UseVisualStyleBackColor = true;
    this.rbEditRuleAndRecheck.CheckedChanged += new EventHandler(this.RadioButton_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(584, 322);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(600, 360);
    this.Name = nameof (ApplyFileNameRuleWindow);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Автоматическое определение типа документа";
    this.Load += new EventHandler(this.ApplyRuleWindow_Load);
    this.Shown += new EventHandler(this.ApplyRuleWindow_Shown);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
