
// Type: CharacterMap.CharacterMapForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace CharacterMap;

public class CharacterMapForm : Form
{
  private Intermech.Controls.CharacterMap characterMap;
  private System.ComponentModel.Container components;
  private Button button1;
  private SettingsBox settings;

  public CharacterMapForm()
  {
    this.components = (System.ComponentModel.Container) null;
    this.InitializeComponent();
    this.characterMap.LoadSettings();
  }

  private void About_Click(object sender, EventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void Exit_Click(object sender, EventArgs e) => Application.Exit();

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CharacterMapForm));
    this.characterMap = new Intermech.Controls.CharacterMap();
    this.button1 = new Button();
    this.SuspendLayout();
    this.characterMap.AccessibleDescription = (string) null;
    this.characterMap.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.characterMap, "characterMap");
    this.characterMap.BackColor = SystemColors.Control;
    this.characterMap.BackgroundImage = (Image) null;
    this.characterMap.CellBackGroundColor = Color.PapayaWhip;
    this.characterMap.CellBorderColor = SystemColors.ControlDarkDark;
    this.characterMap.CellBorderWidth = 0;
    this.characterMap.CellSpacing = 2;
    this.characterMap.CellWidth = 28;
    this.characterMap.CharMapBackGroundColor = SystemColors.Control;
    this.characterMap.CurrentFont = new Font("AcadEref", 21.45f, FontStyle.Regular, GraphicsUnit.Pixel);
    this.characterMap.Font = (Font) null;
    this.characterMap.GridBackGroundColor = SystemColors.Window;
    this.characterMap.GridFontColor = Color.Black;
    this.characterMap.Name = "characterMap";
    this.characterMap.PreviewBackGroundColor = Color.LightGray;
    this.characterMap.PreviewCellWidth = 56;
    this.characterMap.PreviewFontColor = Color.Black;
    this.characterMap.TabStop = false;
    this.button1.AccessibleDescription = (string) null;
    this.button1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.BackgroundImage = (Image) null;
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Font = (Font) null;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.characterMap);
    this.Font = (Font) null;
    this.HelpButton = true;
    this.MinimizeBox = false;
    this.Name = nameof (CharacterMapForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.ResumeLayout(false);
  }

  private void InitSettingsDialog()
  {
    this.settings.lCellBackGroundColor.BackColor = this.characterMap.CellBackGroundColor;
    this.settings.lCellBorderColor.BackColor = this.characterMap.CellBorderColor;
    this.settings.lCharMapBackGroundColor.BackColor = this.characterMap.CharMapBackGroundColor;
    this.settings.lGridBackGroundColor.BackColor = this.characterMap.GridBackGroundColor;
    this.settings.lGridFontColor.BackColor = this.characterMap.GridFontColor;
    this.settings.lPreviewBackGroundColor.BackColor = this.characterMap.PreviewBackGroundColor;
    this.settings.lPreviewFontColor.BackColor = this.characterMap.PreviewFontColor;
    this.settings.sCellWidth.Value = (Decimal) this.characterMap.CellWidth;
    this.settings.sCellSpacing.Value = (Decimal) this.characterMap.CellSpacing;
    this.settings.sCellBorderWidth.Value = (Decimal) this.characterMap.CellBorderWidth;
    this.settings.sPreviewCellWidth.Value = (Decimal) this.characterMap.PreviewCellWidth;
    switch (this.characterMap.FontComboStyle)
    {
      case FontComboStyle.Standard:
        this.settings.rStandard.Checked = true;
        break;
      case FontComboStyle.Selected:
        this.settings.rSelected.Checked = true;
        break;
      case FontComboStyle.Mixed:
        this.settings.rMixed.Checked = true;
        break;
    }
  }

  [STAThread]
  private static void Main() => Application.Run((Form) new CharacterMapForm());

  private void Settings_Click(object sender, EventArgs e)
  {
    this.settings = new SettingsBox();
    this.InitSettingsDialog();
    if (this.settings.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    Color color = this.characterMap.CellBackGroundColor;
    if (!color.Equals((object) this.settings.lCellBackGroundColor.BackColor))
      this.characterMap.CellBackGroundColor = this.settings.lCellBackGroundColor.BackColor;
    color = this.characterMap.CellBorderColor;
    if (!color.Equals((object) this.settings.lCellBorderColor.BackColor))
      this.characterMap.CellBorderColor = this.settings.lCellBorderColor.BackColor;
    color = this.characterMap.CharMapBackGroundColor;
    if (!color.Equals((object) this.settings.lCharMapBackGroundColor.BackColor))
      this.characterMap.CharMapBackGroundColor = this.settings.lCharMapBackGroundColor.BackColor;
    color = this.characterMap.GridBackGroundColor;
    if (!color.Equals((object) this.settings.lGridBackGroundColor.BackColor))
      this.characterMap.GridBackGroundColor = this.settings.lGridBackGroundColor.BackColor;
    color = this.characterMap.GridFontColor;
    if (!color.Equals((object) this.settings.lGridFontColor.BackColor))
      this.characterMap.GridFontColor = this.settings.lGridFontColor.BackColor;
    color = this.characterMap.PreviewBackGroundColor;
    if (!color.Equals((object) this.settings.lPreviewBackGroundColor.BackColor))
      this.characterMap.PreviewBackGroundColor = this.settings.lPreviewBackGroundColor.BackColor;
    color = this.characterMap.PreviewFontColor;
    if (!color.Equals((object) this.settings.lPreviewFontColor.BackColor))
      this.characterMap.PreviewFontColor = this.settings.lPreviewFontColor.BackColor;
    if (this.settings.rStandard.Checked && this.characterMap.FontComboStyle != FontComboStyle.Standard)
      this.characterMap.FontComboStyle = FontComboStyle.Standard;
    else if (this.settings.rSelected.Checked && this.characterMap.FontComboStyle != FontComboStyle.Selected)
      this.characterMap.FontComboStyle = FontComboStyle.Selected;
    else if (this.settings.rMixed.Checked && this.characterMap.FontComboStyle != FontComboStyle.Mixed)
      this.characterMap.FontComboStyle = FontComboStyle.Mixed;
    if ((Decimal) this.characterMap.CellWidth != this.settings.sCellWidth.Value)
      this.characterMap.CellWidth = (int) this.settings.sCellWidth.Value;
    if ((Decimal) this.characterMap.CellSpacing != this.settings.sCellSpacing.Value)
      this.characterMap.CellSpacing = (int) this.settings.sCellSpacing.Value;
    if ((Decimal) this.characterMap.CellBorderWidth != this.settings.sCellBorderWidth.Value)
      this.characterMap.CellBorderWidth = (int) this.settings.sCellBorderWidth.Value;
    if ((Decimal) this.characterMap.PreviewCellWidth != this.settings.sPreviewCellWidth.Value)
      this.characterMap.PreviewCellWidth = (int) this.settings.sPreviewCellWidth.Value;
    this.characterMap.SerializeObject("Settings.xml");
  }
}
