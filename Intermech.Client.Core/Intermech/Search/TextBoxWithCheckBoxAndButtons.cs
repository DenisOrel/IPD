
// Type: Intermech.Search.TextBoxWithCheckBoxAndButtons
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class TextBoxWithCheckBoxAndButtons : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _clearButton;
  private Button _editButton;
  private TextBox _textBox;
  private ToolTip _toolTip;
  private CheckBox _checkBox;
  private TableLayoutPanel tableLayoutPanel1;

  public TextBoxWithCheckBoxAndButtons() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CheckBox CheckBox => this._checkBox;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public TextBox TextBox => this._textBox;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Button EditButton => this._editButton;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Button ClearButton => this._clearButton;

  public void SuspendAllLayout()
  {
    this.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
  }

  public void ResumeAllLayout(bool performLayout)
  {
    this.tableLayoutPanel1.ResumeLayout(performLayout);
    this.ResumeLayout(performLayout);
  }

  private void TextBox_TextChanged(object sender, EventArgs e)
  {
    this._toolTip.SetToolTip((Control) this.TextBox, this.TextBox.Text);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TextBoxWithCheckBoxAndButtons));
    this._clearButton = new Button();
    this._editButton = new Button();
    this._textBox = new TextBox();
    this._toolTip = new ToolTip(this.components);
    this._checkBox = new CheckBox();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this._clearButton.Dock = DockStyle.Fill;
    this._clearButton.Image = (Image) componentResourceManager.GetObject("_clearButton.Image");
    this._clearButton.Location = new Point(176 /*0xB0*/, 0);
    this._clearButton.Margin = new Padding(0);
    this._clearButton.Name = "_clearButton";
    this._clearButton.Size = new Size(24, 20);
    this._clearButton.TabIndex = 0;
    this._toolTip.SetToolTip((Control) this._clearButton, "Очистить");
    this._clearButton.UseVisualStyleBackColor = true;
    this._clearButton.AutoSize = true;
    this._editButton.BackColor = SystemColors.Control;
    this._editButton.Dock = DockStyle.Fill;
    this._editButton.Image = (Image) componentResourceManager.GetObject("_editButton.Image");
    this._editButton.Location = new Point(152, 0);
    this._editButton.Margin = new Padding(0);
    this._editButton.Name = "_editButton";
    this._editButton.Size = new Size(24, 20);
    this._editButton.TabIndex = 0;
    this._toolTip.SetToolTip((Control) this._editButton, "Редактировать");
    this._editButton.UseVisualStyleBackColor = true;
    this._editButton.AutoSize = true;
    this._textBox.Dock = DockStyle.Fill;
    this._textBox.Location = new Point(21, 0);
    this._textBox.Margin = new Padding(0);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(131, 20);
    this._textBox.TabIndex = 1;
    this._textBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    this._checkBox.Dock = DockStyle.Fill;
    this._checkBox.Location = new Point(3, 3);
    this._checkBox.Name = "_checkBox";
    this._checkBox.Size = new Size(15, 14);
    this._checkBox.TabIndex = 2;
    this._checkBox.UseVisualStyleBackColor = true;
    this._checkBox.AutoSize = true;
    this.tableLayoutPanel1.ColumnCount = 4;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this._textBox, 1, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._clearButton, 3, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._editButton, 2, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._checkBox, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(200, 20);
    this.tableLayoutPanel1.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (TextBoxWithCheckBoxAndButtons);
    this.Size = new Size(200, 20);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
