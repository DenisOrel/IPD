
// Type: Intermech.Client.Core.PropertyEditors.RTFEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.PropertyEditors.RTFEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core.PropertyEditors;

public class RTFEditorForm : Form
{
  private bool _ignoreChanges;
  private int checkPrint;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private RichTextBoxPrintCtrl rtb;
  private Button btOk;
  private Button btCancel;
  private ToolStrip toolStrip1;
  private ToolStripButton btNew;
  private ToolStripButton btOpen;
  private ToolStripButton btSave;
  private ToolStripButton btPrint;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton btCut;
  private ToolStripButton btCopy;
  private ToolStripButton btPaste;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton btUndo;
  private ToolStripButton btRedo;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripComboBox cbFontFamily;
  private ToolStripComboBox cbFontSize;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripButton btBold;
  private ToolStripButton btItalic;
  private ToolStripButton btUnderline;
  private ToolStripSeparator toolStripSeparator5;
  private ToolStripButton btLeft;
  private ToolStripButton btCenter;
  private ToolStripButton btRight;
  private ToolStripSeparator toolStripSeparator6;
  private ToolStripButton btPoints;
  private ToolStripButton btNumbers;
  private ToolStripButton btStrikeout;
  private PrintDialog printDialog1;
  private PrintDocument printDocument1;

  public string RTFText
  {
    get => this.rtb.Rtf;
    set
    {
      string str = value ?? string.Empty;
      if (str.StartsWith("{\\rtf1"))
        this.rtb.Rtf = str;
      else
        this.rtb.Text = str;
    }
  }

  public RTFEditorForm()
  {
    this.InitializeComponent();
    this.PopulateFonts();
  }

  private void PopulateFonts()
  {
    try
    {
      this._ignoreChanges = true;
      List<RTFEditorForm.FontItem> fontItemList = new List<RTFEditorForm.FontItem>();
      foreach (FontFamily family in new InstalledFontCollection().Families)
      {
        if (family.IsStyleAvailable(FontStyle.Regular))
          fontItemList.Add(new RTFEditorForm.FontItem(family));
      }
      this.cbFontFamily.Items.AddRange((object[]) fontItemList.ToArray());
      this.cbFontFamily.Sorted = true;
      this.cbFontFamily.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
      this.cbFontFamily.ComboBox.DrawItem += new DrawItemEventHandler(this.ComboBox_DrawItem);
      int[] numArray = new int[16 /*0x10*/]
      {
        8,
        9,
        10,
        11,
        12,
        14,
        16 /*0x10*/,
        18,
        20,
        22,
        24,
        26,
        28,
        36,
        48 /*0x30*/,
        72
      };
      foreach (int num in numArray)
        this.cbFontSize.Items.Add((object) num);
    }
    finally
    {
      this._ignoreChanges = false;
    }
  }

  private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1)
      return;
    RTFEditorForm.FontItem fontItem = (sender as ComboBox).Items[e.Index] as RTFEditorForm.FontItem;
    Rectangle rectangle = new Rectangle(e.Bounds.Left, e.Bounds.Top, 16 /*0x10*/, 16 /*0x10*/);
    Brush brush = SystemBrushes.ControlText;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      brush = SystemBrushes.HighlightText;
    using (Font font = new Font(fontItem.Family, 12f))
      e.Graphics.DrawString(fontItem.Name, font, brush, (float) rectangle.Left, (float) (rectangle.Top + 2));
  }

  private void FontStyleButtonActivated(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    Font font = this.rtb.SelectionFont ?? this.rtb.Font;
    if (font == null)
      return;
    FontStyle newStyle = font.Style;
    if (sender == this.btBold)
      newStyle = this.btBold.Checked ? newStyle | FontStyle.Bold : newStyle & ~FontStyle.Bold;
    else if (sender == this.btItalic)
      newStyle = this.btItalic.Checked ? newStyle | FontStyle.Italic : newStyle & ~FontStyle.Italic;
    else if (sender == this.btUnderline)
      newStyle = this.btUnderline.Checked ? newStyle | FontStyle.Underline : newStyle & ~FontStyle.Underline;
    else if (sender == this.btStrikeout)
      newStyle = this.btStrikeout.Checked ? newStyle | FontStyle.Strikeout : newStyle & ~FontStyle.Strikeout;
    this.SetSelectionFont(font, new Font(font, newStyle));
    this.rtb.Select();
  }

  private void TextJustifyButtonActivated(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    if (sender == this.btCenter)
      this.rtb.SelectionAlignment = HorizontalAlignment.Center;
    else if (sender == this.btRight)
      this.rtb.SelectionAlignment = HorizontalAlignment.Right;
    else
      this.rtb.SelectionAlignment = HorizontalAlignment.Left;
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void SetSelectionFont(Font font, Font fontNew)
  {
    this.rtb.SelectionFont = fontNew;
    if (this.rtb.SelectionFont == null || this.rtb.SelectionFont.FontFamily.Equals((object) fontNew.FontFamily))
      return;
    this.rtb.SelectionFont = font;
    this.ShowSelectionFont();
  }

  private void tbFonts_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    FontFamily family = (this.cbFontFamily.SelectedItem as RTFEditorForm.FontItem).Family;
    Font font = this.rtb.SelectionFont ?? this.rtb.Font;
    if (font != null && !font.FontFamily.Equals((object) family))
      this.SetSelectionFont(font, new Font(family, font.Size, font.Style));
    this.rtb.Select();
  }

  private void btPoints_CheckedChanged(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    this.rtb.SelectionBullet = this.btPoints.Checked;
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void btCut_Click(object sender, EventArgs e)
  {
    this.rtb.Cut();
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void btCopy_Click(object sender, EventArgs e)
  {
    this.rtb.Copy();
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void btPaste_Click(object sender, EventArgs e)
  {
    this.rtb.Paste();
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void btUndo_Click(object sender, EventArgs e)
  {
    if (sender == this.btUndo)
      this.rtb.Undo();
    else if (sender == this.btRedo)
      this.rtb.Redo();
    this.ShowSelectionFont();
    this.rtb.Select();
  }

  private void tbSize_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    Font font = this.rtb.SelectionFont ?? this.rtb.Font;
    if (font == null)
      return;
    int result;
    if (int.TryParse(this.cbFontSize.SelectedItem.ToString(), out result) && (double) result != (double) font.Size)
      this.SetSelectionFont(font, new Font(font.FontFamily, (float) result, font.Style));
    this.rtb.Select();
  }

  private void ShowSelectionFont()
  {
    this.btLeft.Checked = this.rtb.SelectionAlignment == HorizontalAlignment.Left;
    this.btCenter.Checked = this.rtb.SelectionAlignment == HorizontalAlignment.Center;
    this.btRight.Checked = this.rtb.SelectionAlignment == HorizontalAlignment.Right;
    Font font = this.rtb.SelectionFont ?? this.rtb.Font;
    if (font == null)
      return;
    FontFamily fontFamily = font.FontFamily;
    foreach (RTFEditorForm.FontItem fontItem in this.cbFontFamily.Items)
    {
      if (fontItem.Family.Equals((object) fontFamily))
      {
        this.cbFontFamily.SelectedItem = (object) fontItem;
        break;
      }
    }
    this.cbFontSize.SelectedIndex = this.cbFontSize.Items.IndexOf((object) Convert.ToInt32(font.Size));
    this.btBold.Checked = font.Bold;
    this.btItalic.Checked = font.Italic;
    this.btUnderline.Checked = font.Underline;
    this.btStrikeout.Checked = font.Strikeout;
  }

  private void rtb_SelectionChanged(object sender, EventArgs e)
  {
    if (this._ignoreChanges)
      return;
    try
    {
      this._ignoreChanges = true;
      this.ShowSelectionFont();
    }
    finally
    {
      this._ignoreChanges = false;
    }
  }

  private void btNew_Click(object sender, EventArgs e) => this.rtb.Clear();

  private void btPrint_Click(object sender, EventArgs e)
  {
    if (this.printDialog1.ShowDialog() != DialogResult.OK)
      return;
    this.printDocument1.Print();
  }

  private void printDocument1_BeginPrint(object sender, PrintEventArgs e) => this.checkPrint = 0;

  private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
  {
    this.checkPrint = this.rtb.Print(this.checkPrint, this.rtb.TextLength, e);
    if (this.checkPrint < this.rtb.TextLength)
      e.HasMorePages = true;
    else
      e.HasMorePages = false;
  }

  private void btOpen_Click(object sender, EventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.DefaultExt = "*.rtf";
      openFileDialog.Filter = "Файлы RTF|*.rtf";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK || openFileDialog.FileName.Length <= 0)
        return;
      this.rtb.LoadFile(openFileDialog.FileName);
    }
  }

  private void btSave_Click(object sender, EventArgs e)
  {
    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
    {
      saveFileDialog.DefaultExt = "*.rtf";
      saveFileDialog.Filter = "Файлы RTF|*.rtf";
      saveFileDialog.RestoreDirectory = true;
      if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName.Length <= 0)
        return;
      this.rtb.SaveFile(saveFileDialog.FileName);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RTFEditorForm));
    this.rtb = new RichTextBoxPrintCtrl();
    this.btOk = new Button();
    this.btCancel = new Button();
    this.toolStrip1 = new ToolStrip();
    this.btNew = new ToolStripButton();
    this.btOpen = new ToolStripButton();
    this.btSave = new ToolStripButton();
    this.btPrint = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.btCut = new ToolStripButton();
    this.btCopy = new ToolStripButton();
    this.btPaste = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.btUndo = new ToolStripButton();
    this.btRedo = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this.cbFontFamily = new ToolStripComboBox();
    this.cbFontSize = new ToolStripComboBox();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this.btBold = new ToolStripButton();
    this.btItalic = new ToolStripButton();
    this.btUnderline = new ToolStripButton();
    this.btStrikeout = new ToolStripButton();
    this.toolStripSeparator5 = new ToolStripSeparator();
    this.btLeft = new ToolStripButton();
    this.btCenter = new ToolStripButton();
    this.btRight = new ToolStripButton();
    this.toolStripSeparator6 = new ToolStripSeparator();
    this.btPoints = new ToolStripButton();
    this.btNumbers = new ToolStripButton();
    this.printDialog1 = new PrintDialog();
    this.printDocument1 = new PrintDocument();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    this.rtb.AcceptsTab = true;
    this.rtb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.rtb.HideSelection = false;
    this.rtb.Location = new Point(12, 28);
    this.rtb.Name = "rtb";
    this.rtb.Size = new Size(836, 366);
    this.rtb.TabIndex = 0;
    this.rtb.Text = "";
    this.rtb.SelectionChanged += new EventHandler(this.rtb_SelectionChanged);
    this.btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Location = new Point(692, 400);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 1;
    this.btOk.Text = "OK";
    this.btOk.UseVisualStyleBackColor = true;
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(773, 400);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 2;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip1.Items.AddRange(new ToolStripItem[26]
    {
      (ToolStripItem) this.btNew,
      (ToolStripItem) this.btOpen,
      (ToolStripItem) this.btSave,
      (ToolStripItem) this.btPrint,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.btCut,
      (ToolStripItem) this.btCopy,
      (ToolStripItem) this.btPaste,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.btUndo,
      (ToolStripItem) this.btRedo,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this.cbFontFamily,
      (ToolStripItem) this.cbFontSize,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this.btBold,
      (ToolStripItem) this.btItalic,
      (ToolStripItem) this.btUnderline,
      (ToolStripItem) this.btStrikeout,
      (ToolStripItem) this.toolStripSeparator5,
      (ToolStripItem) this.btLeft,
      (ToolStripItem) this.btCenter,
      (ToolStripItem) this.btRight,
      (ToolStripItem) this.toolStripSeparator6,
      (ToolStripItem) this.btPoints,
      (ToolStripItem) this.btNumbers
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(860, 25);
    this.toolStrip1.TabIndex = 3;
    this.toolStrip1.Text = "toolStrip1";
    this.btNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btNew.Image = (Image) componentResourceManager.GetObject("btNew.Image");
    this.btNew.ImageTransparentColor = Color.Magenta;
    this.btNew.Name = "btNew";
    this.btNew.Size = new Size(23, 22);
    this.btNew.Text = "toolStripButton1";
    this.btNew.ToolTipText = "Новый";
    this.btNew.Click += new EventHandler(this.btNew_Click);
    this.btOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btOpen.Image = (Image) componentResourceManager.GetObject("btOpen.Image");
    this.btOpen.ImageTransparentColor = Color.Magenta;
    this.btOpen.Name = "btOpen";
    this.btOpen.Size = new Size(23, 22);
    this.btOpen.Text = "toolStripButton2";
    this.btOpen.ToolTipText = "Открыть";
    this.btOpen.Click += new EventHandler(this.btOpen_Click);
    this.btSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btSave.Image = (Image) componentResourceManager.GetObject("btSave.Image");
    this.btSave.ImageTransparentColor = Color.Magenta;
    this.btSave.Name = "btSave";
    this.btSave.Size = new Size(23, 22);
    this.btSave.Text = "toolStripButton3";
    this.btSave.ToolTipText = "Сохранить";
    this.btSave.Click += new EventHandler(this.btSave_Click);
    this.btPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btPrint.Image = (Image) componentResourceManager.GetObject("btPrint.Image");
    this.btPrint.ImageTransparentColor = Color.Magenta;
    this.btPrint.Name = "btPrint";
    this.btPrint.Size = new Size(23, 22);
    this.btPrint.Text = "toolStripButton4";
    this.btPrint.ToolTipText = "Печать";
    this.btPrint.Click += new EventHandler(this.btPrint_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this.btCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btCut.Image = (Image) componentResourceManager.GetObject("btCut.Image");
    this.btCut.ImageTransparentColor = Color.Magenta;
    this.btCut.Name = "btCut";
    this.btCut.Size = new Size(23, 22);
    this.btCut.Text = "toolStripButton5";
    this.btCut.ToolTipText = "Вырезать";
    this.btCut.Click += new EventHandler(this.btCut_Click);
    this.btCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btCopy.Image = (Image) componentResourceManager.GetObject("btCopy.Image");
    this.btCopy.ImageTransparentColor = Color.Magenta;
    this.btCopy.Name = "btCopy";
    this.btCopy.Size = new Size(23, 22);
    this.btCopy.Text = "toolStripButton6";
    this.btCopy.ToolTipText = "Копировать";
    this.btCopy.Click += new EventHandler(this.btCopy_Click);
    this.btPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btPaste.Image = (Image) componentResourceManager.GetObject("btPaste.Image");
    this.btPaste.ImageTransparentColor = Color.Magenta;
    this.btPaste.Name = "btPaste";
    this.btPaste.Size = new Size(23, 22);
    this.btPaste.Text = "toolStripButton7";
    this.btPaste.ToolTipText = "Вставить";
    this.btPaste.Click += new EventHandler(this.btPaste_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 25);
    this.btUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btUndo.Image = (Image) componentResourceManager.GetObject("btUndo.Image");
    this.btUndo.ImageTransparentColor = Color.Magenta;
    this.btUndo.Name = "btUndo";
    this.btUndo.Size = new Size(23, 22);
    this.btUndo.Text = "toolStripButton8";
    this.btUndo.ToolTipText = "Отмена последнего изменения";
    this.btUndo.Click += new EventHandler(this.btUndo_Click);
    this.btRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btRedo.Image = (Image) componentResourceManager.GetObject("btRedo.Image");
    this.btRedo.ImageTransparentColor = Color.Magenta;
    this.btRedo.Name = "btRedo";
    this.btRedo.Size = new Size(23, 22);
    this.btRedo.Text = "toolStripButton9";
    this.btRedo.ToolTipText = "Вернуть изменение";
    this.btRedo.Click += new EventHandler(this.btUndo_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(6, 25);
    this.cbFontFamily.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFontFamily.Name = "cbFontFamily";
    this.cbFontFamily.Size = new Size(221, 25);
    this.cbFontFamily.SelectedIndexChanged += new EventHandler(this.tbFonts_SelectedIndexChanged);
    this.cbFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFontSize.Name = "cbFontSize";
    this.cbFontSize.Size = new Size(75, 25);
    this.cbFontSize.SelectedIndexChanged += new EventHandler(this.tbSize_SelectedIndexChanged);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    this.toolStripSeparator4.Size = new Size(6, 25);
    this.btBold.CheckOnClick = true;
    this.btBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btBold.Image = (Image) componentResourceManager.GetObject("btBold.Image");
    this.btBold.ImageTransparentColor = Color.Magenta;
    this.btBold.Name = "btBold";
    this.btBold.Size = new Size(23, 22);
    this.btBold.Text = "toolStripButton10";
    this.btBold.ToolTipText = "Жирный";
    this.btBold.Click += new EventHandler(this.FontStyleButtonActivated);
    this.btItalic.CheckOnClick = true;
    this.btItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btItalic.Image = (Image) componentResourceManager.GetObject("btItalic.Image");
    this.btItalic.ImageTransparentColor = Color.Magenta;
    this.btItalic.Name = "btItalic";
    this.btItalic.Size = new Size(23, 22);
    this.btItalic.Text = "toolStripButton11";
    this.btItalic.ToolTipText = "Наклонный";
    this.btItalic.Click += new EventHandler(this.FontStyleButtonActivated);
    this.btUnderline.CheckOnClick = true;
    this.btUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btUnderline.Image = (Image) componentResourceManager.GetObject("btUnderline.Image");
    this.btUnderline.ImageTransparentColor = Color.Magenta;
    this.btUnderline.Name = "btUnderline";
    this.btUnderline.Size = new Size(23, 22);
    this.btUnderline.Text = "toolStripButton12";
    this.btUnderline.ToolTipText = "Подчеркнутый";
    this.btUnderline.Click += new EventHandler(this.FontStyleButtonActivated);
    this.btStrikeout.CheckOnClick = true;
    this.btStrikeout.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btStrikeout.Image = (Image) componentResourceManager.GetObject("btStrikeout.Image");
    this.btStrikeout.ImageTransparentColor = Color.Magenta;
    this.btStrikeout.Name = "btStrikeout";
    this.btStrikeout.Size = new Size(23, 22);
    this.btStrikeout.Text = "toolStripButton1";
    this.btStrikeout.ToolTipText = "Перечеркнутый";
    this.btStrikeout.Click += new EventHandler(this.FontStyleButtonActivated);
    this.toolStripSeparator5.Name = "toolStripSeparator5";
    this.toolStripSeparator5.Size = new Size(6, 25);
    this.btLeft.CheckOnClick = true;
    this.btLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btLeft.Image = (Image) componentResourceManager.GetObject("btLeft.Image");
    this.btLeft.ImageTransparentColor = Color.Magenta;
    this.btLeft.Name = "btLeft";
    this.btLeft.Size = new Size(23, 22);
    this.btLeft.Text = "toolStripButton13";
    this.btLeft.ToolTipText = "Выравнивание влево";
    this.btLeft.Click += new EventHandler(this.TextJustifyButtonActivated);
    this.btCenter.CheckOnClick = true;
    this.btCenter.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btCenter.Image = (Image) componentResourceManager.GetObject("btCenter.Image");
    this.btCenter.ImageTransparentColor = Color.Magenta;
    this.btCenter.Name = "btCenter";
    this.btCenter.Size = new Size(23, 22);
    this.btCenter.Text = "toolStripButton14";
    this.btCenter.ToolTipText = "Выравнивание по центру";
    this.btCenter.Click += new EventHandler(this.TextJustifyButtonActivated);
    this.btRight.CheckOnClick = true;
    this.btRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btRight.Image = (Image) componentResourceManager.GetObject("btRight.Image");
    this.btRight.ImageTransparentColor = Color.Magenta;
    this.btRight.Name = "btRight";
    this.btRight.Size = new Size(23, 22);
    this.btRight.Text = "toolStripButton15";
    this.btRight.ToolTipText = "Выравнивание вправо";
    this.btRight.Click += new EventHandler(this.TextJustifyButtonActivated);
    this.toolStripSeparator6.Name = "toolStripSeparator6";
    this.toolStripSeparator6.Size = new Size(6, 25);
    this.btPoints.CheckOnClick = true;
    this.btPoints.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btPoints.Image = (Image) componentResourceManager.GetObject("btPoints.Image");
    this.btPoints.ImageTransparentColor = Color.Magenta;
    this.btPoints.Name = "btPoints";
    this.btPoints.Size = new Size(23, 22);
    this.btPoints.Text = "toolStripButton16";
    this.btPoints.ToolTipText = "Список";
    this.btPoints.CheckedChanged += new EventHandler(this.btPoints_CheckedChanged);
    this.btNumbers.CheckOnClick = true;
    this.btNumbers.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btNumbers.Image = (Image) componentResourceManager.GetObject("btNumbers.Image");
    this.btNumbers.ImageTransparentColor = Color.Magenta;
    this.btNumbers.Name = "btNumbers";
    this.btNumbers.Size = new Size(23, 22);
    this.btNumbers.Text = "toolStripButton17";
    this.btNumbers.Visible = false;
    this.printDialog1.Document = this.printDocument1;
    this.printDialog1.UseEXDialog = true;
    this.printDocument1.BeginPrint += new PrintEventHandler(this.printDocument1_BeginPrint);
    this.printDocument1.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(860, 435);
    this.Controls.Add((Control) this.toolStrip1);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.Controls.Add((Control) this.rtb);
    this.DoubleBuffered = true;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(236, 155);
    this.Name = nameof (RTFEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Редактирование описания";
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>класс-обёртка для семейства шрифтов</summary>
  private class FontItem : IComparable<RTFEditorForm.FontItem>
  {
    /// <summary>семейство шрифтов</summary>
    internal FontFamily Family { get; private set; }

    /// <summary>Имя семейства шрифтов</summary>
    internal string Name { get; private set; }

    /// <summary>конструктор</summary>
    /// <param name="family">семейство шрифтов</param>
    internal FontItem(FontFamily family)
    {
      this.Family = family;
      this.Name = family.Name;
    }

    /// <summary>Преобразует семейство шрифтов</summary>
    /// <returns>Строка, представляющая семейство шрифтов</returns>
    public override string ToString() => this.Name;

    public int CompareTo(RTFEditorForm.FontItem other) => this.Name.CompareTo(other.Name);
  }
}
