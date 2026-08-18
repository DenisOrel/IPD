
// Type: Intermech.Search.GroupAttributesChanging.SpecialCharacterInputControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.GroupAttributesChanging;

public sealed class SpecialCharacterInputControl : UserControl
{
  private SpecialCharacter[] _specialCharacters = new SpecialCharacter[0];
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private TextBox _textBox;
  private Button _button;
  private ContextMenuStrip _contextMenuStrip;

  public SpecialCharacterInputControl() => this.InitializeComponent();

  public event EventHandler Changed;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SpecialCharacter[] SpecialCharacters
  {
    get => this._specialCharacters;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (this._specialCharacters == value)
        return;
      this._specialCharacters = value;
      this._contextMenuStrip.Items.Clear();
      foreach (SpecialCharacter specialCharacter in this._specialCharacters)
      {
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem($"{specialCharacter.Description} {specialCharacter.Character}");
        toolStripMenuItem.Click += new EventHandler(this.ToolStripMenuItem_Click);
        toolStripMenuItem.Tag = (object) specialCharacter;
        this._contextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem);
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new string Text
  {
    get => this._textBox.Text;
    set => this._textBox.Text = value;
  }

  private void Button_Click(object sender, EventArgs e)
  {
    this._contextMenuStrip.Show((Control) this._button, new Point(this._button.Width, 0));
  }

  private void TextBox_TextChanged(object sender, EventArgs e) => this.OnChanged();

  private void ToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this._textBox.Text += ((SpecialCharacter) ((ToolStripItem) sender).Tag).Character;
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._textBox = new TextBox();
    this._button = new Button();
    this._contextMenuStrip = new ContextMenuStrip(this.components);
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this._textBox, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._button, 1, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(447, 32 /*0x20*/);
    this.tableLayoutPanel1.TabIndex = 0;
    this._textBox.Dock = DockStyle.Fill;
    this._textBox.Location = new Point(3, 3);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(355, 20);
    this._textBox.TabIndex = 0;
    this._textBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    this._button.AutoSize = true;
    this._button.Location = new Point(364, 3);
    this._button.Name = "_button";
    this._button.Size = new Size(80 /*0x50*/, 23);
    this._button.TabIndex = 1;
    this._button.Text = "Спецсимвол";
    this._button.UseVisualStyleBackColor = true;
    this._button.Click += new EventHandler(this.Button_Click);
    this._contextMenuStrip.Name = "_contextMenuStrip";
    this._contextMenuStrip.Size = new Size(61, 4);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (SpecialCharacterInputControl);
    this.Size = new Size(447, 32 /*0x20*/);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
