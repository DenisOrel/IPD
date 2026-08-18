
// Type: Intermech.Search.UI.TextBoxForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public sealed class TextBoxForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _okButton;
  private Label _label;
  private TextBox _textBox;

  public TextBoxForm() => this.InitializeComponent();

  public string LabelText
  {
    get => this._label.Text;
    set => this._label.Text = value;
  }

  public string TextBoxText
  {
    get => this._textBox.Text;
    set => this._textBox.Text = value;
  }

  private void TextBoxForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void TextBoxForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    this._okButton = new Button();
    this._label = new Label();
    this._textBox = new TextBox();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this._label, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._textBox, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 3;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
    this.tableLayoutPanel1.Size = new Size(374, 112 /*0x70*/);
    this.tableLayoutPanel1.TabIndex = 0;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._okButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 75);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(368, 34);
    this.flowLayoutPanel1.TabIndex = 0;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(290, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 1;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Location = new Point(209, 3);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 0;
    this._okButton.Text = "OK";
    this._okButton.UseVisualStyleBackColor = true;
    this._label.AutoSize = true;
    this._label.Dock = DockStyle.Fill;
    this._label.Location = new Point(5, 5);
    this._label.Margin = new Padding(5);
    this._label.Name = "_label";
    this._label.Size = new Size(364, 26);
    this._label.TabIndex = 1;
    this._textBox.Dock = DockStyle.Fill;
    this._textBox.Location = new Point(5, 41);
    this._textBox.Margin = new Padding(5);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(364, 20);
    this._textBox.TabIndex = 2;
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(374, 112 /*0x70*/);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TextBoxForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = nameof (TextBoxForm);
    this.FormClosing += new FormClosingEventHandler(this.TextBoxForm_FormClosing);
    this.Load += new EventHandler(this.TextBoxForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
