
// Type: Intermech.Controls.SettingsBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

internal class SettingsBox : Form
{
  private Button Cancel;
  private Label CellBkGround;
  private Label CellBorderColor;
  private Label CellBorderWidth;
  private Label CellSpacing;
  private Label CellWidth;
  private Label CharMapBackGroundColor;
  private GroupBox ColorGroupBox;
  private IContainer components;
  private GroupBox FontComboStyle;
  private Label GridBackGroundColor;
  private Label GridFontColor;
  private GroupBox groupBox1;
  private Label label1;
  internal Label lCellBackGroundColor;
  internal Label lCellBorderColor;
  internal Label lCharMapBackGroundColor;
  internal Label lGridBackGroundColor;
  internal Label lGridFontColor;
  internal Label lPreviewBackGroundColor;
  internal Label lPreviewFontColor;
  private Button OK;
  private Label PreviewBackGroundColor;
  private Label PreviewCellWidth;
  internal RadioButton rMixed;
  internal RadioButton rSelected;
  internal RadioButton rStandard;
  internal NumericUpDown sCellBorderWidth;
  internal NumericUpDown sCellSpacing;
  internal NumericUpDown sCellWidth;
  internal NumericUpDown sPreviewCellWidth;
  private ToolTip ToolTip;

  internal SettingsBox()
  {
    this.InitializeComponent();
    this.ToolTip.SetToolTip((Control) this.lCellBackGroundColor, "Click here to change cell background color");
    this.ToolTip.SetToolTip((Control) this.lCellBorderColor, "Click here to change cell border color");
    this.ToolTip.SetToolTip((Control) this.lCharMapBackGroundColor, "Click here to change CharNavigator background color");
    this.ToolTip.SetToolTip((Control) this.lGridBackGroundColor, "Click here to change grid background color");
    this.ToolTip.SetToolTip((Control) this.lGridFontColor, "Click here to change grid font color");
    this.ToolTip.SetToolTip((Control) this.lPreviewBackGroundColor, "Click here to change preview background color");
    this.ToolTip.SetToolTip((Control) this.lPreviewFontColor, "Click here to change preview font color");
    this.ToolTip.SetToolTip((Control) this.rStandard, "The font names in combobox are written using a default font");
    this.ToolTip.SetToolTip((Control) this.rSelected, "All font names are written using each font itself");
    this.ToolTip.SetToolTip((Control) this.rMixed, "Font names are written using both standard and the font itself");
    this.ToolTip.SetToolTip((Control) this.sCellWidth, "Set here the grid's cell width");
    this.ToolTip.SetToolTip((Control) this.sCellSpacing, "Set here the amount of space between cells in a grid");
    this.ToolTip.SetToolTip((Control) this.sCellBorderWidth, "Set here the grid's cell border width");
    this.ToolTip.SetToolTip((Control) this.sPreviewCellWidth, "Set enlarge cell width");
  }

  private void Cancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
  }

  private void CellBackGroundColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lCellBackGroundColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lCellBackGroundColor.BackColor = colorDialog.Color;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.ColorGroupBox = new GroupBox();
    this.lPreviewFontColor = new Label();
    this.label1 = new Label();
    this.lPreviewBackGroundColor = new Label();
    this.PreviewBackGroundColor = new Label();
    this.lGridFontColor = new Label();
    this.GridFontColor = new Label();
    this.lGridBackGroundColor = new Label();
    this.GridBackGroundColor = new Label();
    this.lCharMapBackGroundColor = new Label();
    this.CharMapBackGroundColor = new Label();
    this.lCellBorderColor = new Label();
    this.lCellBackGroundColor = new Label();
    this.CellBkGround = new Label();
    this.OK = new Button();
    this.Cancel = new Button();
    this.ToolTip = new ToolTip(this.components);
    this.CellBorderColor = new Label();
    this.FontComboStyle = new GroupBox();
    this.rMixed = new RadioButton();
    this.rSelected = new RadioButton();
    this.rStandard = new RadioButton();
    this.sCellWidth = new NumericUpDown();
    this.CellWidth = new Label();
    this.CellSpacing = new Label();
    this.sCellSpacing = new NumericUpDown();
    this.CellBorderWidth = new Label();
    this.sCellBorderWidth = new NumericUpDown();
    this.PreviewCellWidth = new Label();
    this.sPreviewCellWidth = new NumericUpDown();
    this.groupBox1 = new GroupBox();
    this.ColorGroupBox.SuspendLayout();
    this.FontComboStyle.SuspendLayout();
    this.sCellWidth.BeginInit();
    this.sCellSpacing.BeginInit();
    this.sCellBorderWidth.BeginInit();
    this.sPreviewCellWidth.BeginInit();
    this.SuspendLayout();
    this.ColorGroupBox.Controls.Add((Control) this.lPreviewFontColor);
    this.ColorGroupBox.Controls.Add((Control) this.label1);
    this.ColorGroupBox.Controls.Add((Control) this.lPreviewBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.PreviewBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.lGridFontColor);
    this.ColorGroupBox.Controls.Add((Control) this.GridFontColor);
    this.ColorGroupBox.Controls.Add((Control) this.lGridBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.GridBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.lCharMapBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.CharMapBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.lCellBorderColor);
    this.ColorGroupBox.Controls.Add((Control) this.lCellBackGroundColor);
    this.ColorGroupBox.Controls.Add((Control) this.CellBkGround);
    this.ColorGroupBox.Location = new Point(8, 7);
    this.ColorGroupBox.Name = "ColorGroupBox";
    this.ColorGroupBox.Size = new Size(168, 201);
    this.ColorGroupBox.TabIndex = 0;
    this.ColorGroupBox.TabStop = false;
    this.ColorGroupBox.Text = "Colors";
    this.lPreviewFontColor.BorderStyle = BorderStyle.FixedSingle;
    this.lPreviewFontColor.Location = new Point(128 /*0x80*/, 172);
    this.lPreviewFontColor.Name = "lPreviewFontColor";
    this.lPreviewFontColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lPreviewFontColor.TabIndex = 12;
    this.lPreviewFontColor.Click += new EventHandler(this.lPreviewFontColor_Click);
    this.label1.Location = new Point(8, 173);
    this.label1.Name = "label1";
    this.label1.Size = new Size(100, 23);
    this.label1.TabIndex = 11;
    this.label1.Text = "Preview font color ";
    this.lPreviewBackGroundColor.BorderStyle = BorderStyle.FixedSingle;
    this.lPreviewBackGroundColor.Location = new Point(128 /*0x80*/, 147);
    this.lPreviewBackGroundColor.Name = "lPreviewBackGroundColor";
    this.lPreviewBackGroundColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lPreviewBackGroundColor.TabIndex = 10;
    this.lPreviewBackGroundColor.Click += new EventHandler(this.lPreviewBackGroundColor_Click);
    this.PreviewBackGroundColor.Location = new Point(8, 143);
    this.PreviewBackGroundColor.Name = "PreviewBackGroundColor";
    this.PreviewBackGroundColor.Size = new Size(112 /*0x70*/, 26);
    this.PreviewBackGroundColor.TabIndex = 9;
    this.PreviewBackGroundColor.Text = "Preview background color ";
    this.lGridFontColor.BorderStyle = BorderStyle.FixedSingle;
    this.lGridFontColor.Location = new Point(128 /*0x80*/, 124);
    this.lGridFontColor.Name = "lGridFontColor";
    this.lGridFontColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lGridFontColor.TabIndex = 8;
    this.lGridFontColor.Click += new EventHandler(this.lGridFontColor_Click);
    this.GridFontColor.Location = new Point(5, 125);
    this.GridFontColor.Name = "GridFontColor";
    this.GridFontColor.Size = new Size(100, 19);
    this.GridFontColor.TabIndex = 7;
    this.GridFontColor.Text = " Grid font color";
    this.lGridBackGroundColor.BorderStyle = BorderStyle.FixedSingle;
    this.lGridBackGroundColor.Location = new Point(128 /*0x80*/, 100);
    this.lGridBackGroundColor.Name = "lGridBackGroundColor";
    this.lGridBackGroundColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lGridBackGroundColor.TabIndex = 6;
    this.lGridBackGroundColor.Click += new EventHandler(this.lGridBackGroundColor_Click);
    this.GridBackGroundColor.Location = new Point(8, 100);
    this.GridBackGroundColor.Name = "GridBackGroundColor";
    this.GridBackGroundColor.Size = new Size(120, 23);
    this.GridBackGroundColor.TabIndex = 5;
    this.GridBackGroundColor.Text = "Grid background color ";
    this.lCharMapBackGroundColor.BorderStyle = BorderStyle.FixedSingle;
    this.lCharMapBackGroundColor.Location = new Point(128 /*0x80*/, 72);
    this.lCharMapBackGroundColor.Name = "lCharMapBackGroundColor";
    this.lCharMapBackGroundColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lCharMapBackGroundColor.TabIndex = 4;
    this.lCharMapBackGroundColor.Click += new EventHandler(this.lCharMapBackGroundColor_Click);
    this.CharMapBackGroundColor.Location = new Point(8, 68);
    this.CharMapBackGroundColor.Name = "CharMapBackGroundColor";
    this.CharMapBackGroundColor.Size = new Size(112 /*0x70*/, 32 /*0x20*/);
    this.CharMapBackGroundColor.TabIndex = 3;
    this.CharMapBackGroundColor.Text = "CharNavigator background color ";
    this.lCellBorderColor.BorderStyle = BorderStyle.FixedSingle;
    this.lCellBorderColor.Location = new Point(128 /*0x80*/, 45);
    this.lCellBorderColor.Name = "lCellBorderColor";
    this.lCellBorderColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lCellBorderColor.TabIndex = 2;
    this.lCellBorderColor.Click += new EventHandler(this.lCellBorderColor_Click);
    this.lCellBackGroundColor.BorderStyle = BorderStyle.FixedSingle;
    this.lCellBackGroundColor.Location = new Point(128 /*0x80*/, 24);
    this.lCellBackGroundColor.Name = "lCellBackGroundColor";
    this.lCellBackGroundColor.Size = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.lCellBackGroundColor.TabIndex = 1;
    this.lCellBackGroundColor.Click += new EventHandler(this.CellBackGroundColor_Click);
    this.CellBkGround.Location = new Point(8, 24);
    this.CellBkGround.Name = "CellBkGround";
    this.CellBkGround.Size = new Size(120, 16 /*0x10*/);
    this.CellBkGround.TabIndex = 0;
    this.CellBkGround.Text = "Cell background color ";
    this.OK.Location = new Point(352, 15);
    this.OK.Name = "OK";
    this.OK.Size = new Size(75, 23);
    this.OK.TabIndex = 1;
    this.OK.Text = "Ok";
    this.OK.Click += new EventHandler(this.OK_Click);
    this.Cancel.Location = new Point(352, 47);
    this.Cancel.Name = "Cancel";
    this.Cancel.Size = new Size(75, 23);
    this.Cancel.TabIndex = 2;
    this.Cancel.Text = "Cancel";
    this.Cancel.Click += new EventHandler(this.Cancel_Click);
    this.ToolTip.ShowAlways = true;
    this.CellBorderColor.Location = new Point(16 /*0x10*/, 55);
    this.CellBorderColor.Name = "CellBorderColor";
    this.CellBorderColor.Size = new Size(96 /*0x60*/, 16 /*0x10*/);
    this.CellBorderColor.TabIndex = 3;
    this.CellBorderColor.Text = "Cell border color";
    this.FontComboStyle.Controls.Add((Control) this.rMixed);
    this.FontComboStyle.Controls.Add((Control) this.rSelected);
    this.FontComboStyle.Controls.Add((Control) this.rStandard);
    this.FontComboStyle.Location = new Point(184, 7);
    this.FontComboStyle.Name = "FontComboStyle";
    this.FontComboStyle.Size = new Size(160 /*0xA0*/, 80 /*0x50*/);
    this.FontComboStyle.TabIndex = 4;
    this.FontComboStyle.TabStop = false;
    this.FontComboStyle.Text = "Font combo style";
    this.rMixed.Location = new Point(8, 57);
    this.rMixed.Name = "rMixed";
    this.rMixed.Size = new Size(104, 16 /*0x10*/);
    this.rMixed.TabIndex = 2;
    this.rMixed.Text = "Mixed";
    this.rSelected.Location = new Point(8, 38);
    this.rSelected.Name = "rSelected";
    this.rSelected.Size = new Size(104, 16 /*0x10*/);
    this.rSelected.TabIndex = 1;
    this.rSelected.Text = "Selected";
    this.rStandard.Location = new Point(8, 19);
    this.rStandard.Name = "rStandard";
    this.rStandard.Size = new Size(104, 16 /*0x10*/);
    this.rStandard.TabIndex = 0;
    this.rStandard.Text = "Standard";
    this.sCellWidth.Location = new Point(281, 103);
    this.sCellWidth.Maximum = new Decimal(new int[4]
    {
      300,
      0,
      0,
      0
    });
    this.sCellWidth.Name = "sCellWidth";
    this.sCellWidth.Size = new Size(56, 20);
    this.sCellWidth.TabIndex = 5;
    this.CellWidth.Location = new Point(185, 103);
    this.CellWidth.Name = "CellWidth";
    this.CellWidth.Size = new Size(56, 16 /*0x10*/);
    this.CellWidth.TabIndex = 6;
    this.CellWidth.Text = "Cell width";
    this.CellSpacing.Location = new Point(185, (int) sbyte.MaxValue);
    this.CellSpacing.Name = "CellSpacing";
    this.CellSpacing.Size = new Size(72, 16 /*0x10*/);
    this.CellSpacing.TabIndex = 7;
    this.CellSpacing.Text = "Cell spacing";
    this.sCellSpacing.Location = new Point(281, (int) sbyte.MaxValue);
    this.sCellSpacing.Maximum = new Decimal(new int[4]
    {
      300,
      0,
      0,
      0
    });
    this.sCellSpacing.Name = "sCellSpacing";
    this.sCellSpacing.Size = new Size(56, 20);
    this.sCellSpacing.TabIndex = 8;
    this.CellBorderWidth.Location = new Point(185, 154);
    this.CellBorderWidth.Name = "CellBorderWidth";
    this.CellBorderWidth.Size = new Size(100, 16 /*0x10*/);
    this.CellBorderWidth.TabIndex = 9;
    this.CellBorderWidth.Text = "Cell border width ";
    this.sCellBorderWidth.Location = new Point(281, 151);
    this.sCellBorderWidth.Maximum = new Decimal(new int[4]
    {
      300,
      0,
      0,
      0
    });
    this.sCellBorderWidth.Name = "sCellBorderWidth";
    this.sCellBorderWidth.Size = new Size(56, 20);
    this.sCellBorderWidth.TabIndex = 10;
    this.PreviewCellWidth.Location = new Point(185, 177);
    this.PreviewCellWidth.Name = "PreviewCellWidth";
    this.PreviewCellWidth.Size = new Size(100, 16 /*0x10*/);
    this.PreviewCellWidth.TabIndex = 11;
    this.PreviewCellWidth.Text = "Preview cell width ";
    this.sPreviewCellWidth.Location = new Point(281, 175);
    this.sPreviewCellWidth.Maximum = new Decimal(new int[4]
    {
      600,
      0,
      0,
      0
    });
    this.sPreviewCellWidth.Name = "sPreviewCellWidth";
    this.sPreviewCellWidth.Size = new Size(56, 20);
    this.sPreviewCellWidth.TabIndex = 12;
    this.groupBox1.Location = new Point(183, 87);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(160 /*0xA0*/, 120);
    this.groupBox1.TabIndex = 13;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Sizes";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(434, 215);
    this.Controls.Add((Control) this.sPreviewCellWidth);
    this.Controls.Add((Control) this.PreviewCellWidth);
    this.Controls.Add((Control) this.sCellBorderWidth);
    this.Controls.Add((Control) this.CellBorderWidth);
    this.Controls.Add((Control) this.sCellSpacing);
    this.Controls.Add((Control) this.CellSpacing);
    this.Controls.Add((Control) this.CellWidth);
    this.Controls.Add((Control) this.sCellWidth);
    this.Controls.Add((Control) this.FontComboStyle);
    this.Controls.Add((Control) this.CellBorderColor);
    this.Controls.Add((Control) this.Cancel);
    this.Controls.Add((Control) this.OK);
    this.Controls.Add((Control) this.ColorGroupBox);
    this.Controls.Add((Control) this.groupBox1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SettingsBox);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Settings";
    this.ColorGroupBox.ResumeLayout(false);
    this.FontComboStyle.ResumeLayout(false);
    this.sCellWidth.EndInit();
    this.sCellSpacing.EndInit();
    this.sCellBorderWidth.EndInit();
    this.sPreviewCellWidth.EndInit();
    this.ResumeLayout(false);
  }

  private void lCellBorderColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lCellBorderColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lCellBorderColor.BackColor = colorDialog.Color;
  }

  private void lCharMapBackGroundColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lCharMapBackGroundColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lCharMapBackGroundColor.BackColor = colorDialog.Color;
  }

  private void lGridBackGroundColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lGridBackGroundColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lGridBackGroundColor.BackColor = colorDialog.Color;
  }

  private void lGridFontColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lGridFontColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lGridFontColor.BackColor = colorDialog.Color;
  }

  private void lPreviewBackGroundColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lPreviewBackGroundColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lPreviewBackGroundColor.BackColor = colorDialog.Color;
  }

  private void lPreviewFontColor_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    colorDialog.FullOpen = true;
    colorDialog.Color = this.lPreviewFontColor.BackColor;
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    this.lPreviewFontColor.BackColor = colorDialog.Color;
  }

  private void OK_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }
}
