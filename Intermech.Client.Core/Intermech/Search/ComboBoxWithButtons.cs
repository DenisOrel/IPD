
// Type: Intermech.Search.ComboBoxWithButtons
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public class ComboBoxWithButtons : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private ComboBox _comboBox;
  private Button _clearButton;
  private Button _editButton;
  private ToolTip _toolTip;

  public ComboBoxWithButtons() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ComboBox ComboBox => this._comboBox;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Button EditButton => this._editButton;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Button ClearButton => this._clearButton;

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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._comboBox = new ComboBox();
    this._clearButton = new Button();
    this._editButton = new Button();
    this._toolTip = new ToolTip(this.components);
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 3;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this._editButton, 1, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._comboBox, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._clearButton, 2, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Margin = new Padding(0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(200, 21);
    this.tableLayoutPanel1.TabIndex = 0;
    this._comboBox.Dock = DockStyle.Fill;
    this._comboBox.FormattingEnabled = true;
    this._comboBox.Location = new Point(0, 0);
    this._comboBox.Margin = new Padding(0);
    this._comboBox.Name = "_comboBox";
    this._comboBox.Size = new Size(156, 21);
    this._comboBox.TabIndex = 0;
    this._clearButton.AutoSize = true;
    this._clearButton.Dock = DockStyle.Fill;
    this._clearButton.Image = (Image) Resources.Clean;
    this._clearButton.Location = new Point(178, 0);
    this._clearButton.Margin = new Padding(0);
    this._clearButton.Name = "_clearButton";
    this._clearButton.Size = new Size(22, 21);
    this._clearButton.TabIndex = 1;
    this._toolTip.SetToolTip((Control) this._clearButton, "Очистить");
    this._clearButton.UseVisualStyleBackColor = true;
    this._editButton.AutoSize = true;
    this._editButton.Dock = DockStyle.Fill;
    this._editButton.Image = (Image) Resources.EditStandart;
    this._editButton.Location = new Point(156, 0);
    this._editButton.Margin = new Padding(0);
    this._editButton.Name = "_editButton";
    this._editButton.Size = new Size(22, 21);
    this._editButton.TabIndex = 2;
    this._toolTip.SetToolTip((Control) this._editButton, "Редактировать");
    this._editButton.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (ComboBoxWithButtons);
    this.Size = new Size(200, 21);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
