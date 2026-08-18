
// Type: Intermech.Search.EventLog.SetArchiveFromDateForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.EventLog;

public sealed class SetArchiveFromDateForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _acceptButton;
  private TableLayoutPanel tableLayoutPanel2;
  private Label label1;
  private DateTimePicker _dateTimePicker;

  public SetArchiveFromDateForm() => this.InitializeComponent();

  private void SetArchiveFromDateForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SetArchiveFromDateForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void AcceptButton_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.EventLog.ArchiveEvents(this._dateTimePicker.Value);
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
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.tableLayoutPanel2 = new TableLayoutPanel();
    this.label1 = new Label();
    this._dateTimePicker = new DateTimePicker();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.tableLayoutPanel2.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(453, 94);
    this.tableLayoutPanel1.TabIndex = 0;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 62);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(447, 29);
    this.flowLayoutPanel1.TabIndex = 0;
    this._cancelButton.AutoSize = true;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(369, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._acceptButton.AutoSize = true;
    this._acceptButton.DialogResult = DialogResult.OK;
    this._acceptButton.Location = new Point(288, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 0;
    this._acceptButton.Text = "OK";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    this.tableLayoutPanel2.ColumnCount = 2;
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Controls.Add((Control) this.label1, 0, 0);
    this.tableLayoutPanel2.Controls.Add((Control) this._dateTimePicker, 1, 0);
    this.tableLayoutPanel2.Dock = DockStyle.Fill;
    this.tableLayoutPanel2.Location = new Point(3, 3);
    this.tableLayoutPanel2.Name = "tableLayoutPanel2";
    this.tableLayoutPanel2.RowCount = 1;
    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel2.Size = new Size(447, 53);
    this.tableLayoutPanel2.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Dock = DockStyle.Fill;
    this.label1.Location = new Point(3, 3);
    this.label1.Margin = new Padding(3);
    this.label1.Name = "label1";
    this.label1.Size = new Size(219, 47);
    this.label1.TabIndex = 0;
    this.label1.Text = "Архивировать записи журнала начиная с:";
    this._dateTimePicker.Location = new Point(228, 3);
    this._dateTimePicker.Name = "_dateTimePicker";
    this._dateTimePicker.Size = new Size(200, 20);
    this._dateTimePicker.TabIndex = 1;
    this.AcceptButton = (IButtonControl) this._acceptButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(453, 94);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (SetArchiveFromDateForm);
    this.ShowIcon = false;
    this.Text = "Установка даты начала архивирования записей журнала событий";
    this.FormClosed += new FormClosedEventHandler(this.SetArchiveFromDateForm_FormClosed);
    this.Load += new EventHandler(this.SetArchiveFromDateForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.tableLayoutPanel2.ResumeLayout(false);
    this.tableLayoutPanel2.PerformLayout();
    this.ResumeLayout(false);
  }
}
