// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.FontSetupDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Document.Model.Properties;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.UI;
using OfficePickers.ColorPicker;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary> Диалог настройки шрифта </summary>
public class FontSetupDlg : Form
{
  private CharFormat _charFormat;
  private CharFormat _oldCharFormat;
  private FontSetupDlg.InnerFontStyleClass _lastChangedFontStyle;
  private string _fontFamilysHash = string.Empty;
  private string _fontStylesHash = string.Empty;
  private string _fontSizesHash = string.Empty;
  private int _fontFamilyEditEventLockCounter;
  private int _fontStyleEditEventLockCounter;
  private int _fontSizeEditEventLockCounter;
  private HybridDictionary _allFontStyles = new HybridDictionary(4);
  private FontFamily[] _allFonts;
  private bool _loaded;
  private string _testString = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private Button _btnDefault;
  private TabControl _tabSelector;
  private TabPage _tabFont;
  private TextBox _editFontFamily;
  private TabPage _tabInterval;
  private ListBox _listBoxFontFamily;
  private ListBox _listBoxFontSize;
  private TextBox _editFontSize;
  private ListBox _listBoxFontStyle;
  private TextBox _editFontStyle;
  private Label _labelUnderLineStyle;
  private ComboBoxColorPicker _comboBoxTextColor;
  private System.Windows.Forms.ComboBox _comboBoxUnderlineStyle;
  private ComboBoxColorPicker _comboBoxUnderlineColor;
  private Label _labelOptions;
  private Bevel _bevelOptions;
  private CheckBox _checkBoxStryked;
  private CheckBox _checkBoxSubscript;
  private CheckBox _checkBoxSuperscript;
  private CheckBox _checkBoxAllCaps;
  private CheckBox _checkBoxSmallCaps;
  private CheckBox _checkBoxHidden;
  private System.Windows.Forms.ComboBox _comboBoxMovement;
  private System.Windows.Forms.ComboBox _comboBoxInterval;
  private System.Windows.Forms.ComboBox _comboBoxZoom;
  private Panel _panelSampleBorder;
  private ImRtfEditor _ternSample;
  private Bevel _bevelSample;
  private Label _labelSample;
  private Label _labelFontType;
  private SpinEdit _spinEditInterval;
  private SpinEdit _spinEditMovement;
  private Label _labelFontFamily;
  private Label _labelUnderlineColor;
  private Label _labelTextColor;
  private Label _labelFontStyle;
  private Label _labelFontSize;
  private Label _labelZoom;
  private Label _labelOnMovement;
  private Label _labelInterval;
  private Label _labelMovement;
  private Label _labelOnInterval;
  private CheckBox _checkBoxDoubleStrikout;

  /// <summary> Конструктор </summary>
  /// <param name="CharFormat"></param>
  /// <param name="testString"></param>
  public FontSetupDlg(CharFormat CharFormat, string testString)
  {
    this.InitializeComponent();
    this._tabSelector.TabPages.RemoveAt(1);
    this._testString = testString;
    this._oldCharFormat = CharFormat;
    this._charFormat = this._oldCharFormat == null ? new CharFormat() : this._oldCharFormat.Clone();
    this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Normal] = (object) new FontSetupDlg.InnerFontStyleClass(FontSetupDlg.InnerFontStyleEnum.Normal);
    this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Italic] = (object) new FontSetupDlg.InnerFontStyleClass(FontSetupDlg.InnerFontStyleEnum.Italic);
    this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Bold] = (object) new FontSetupDlg.InnerFontStyleClass(FontSetupDlg.InnerFontStyleEnum.Bold);
    this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.BoldItalic] = (object) new FontSetupDlg.InnerFontStyleClass(FontSetupDlg.InnerFontStyleEnum.BoldItalic);
    if (this._comboBoxUnderlineStyle.Items.Count > 0)
      this._comboBoxUnderlineStyle.SelectedIndex = 0;
    this._ternSample.Text = "";
    this._ternSample.BorderShowing = true;
    if (this._comboBoxInterval.Items.Count > 0)
      this._comboBoxInterval.SelectedIndex = 0;
    if (this._comboBoxMovement.Items.Count > 0)
      this._comboBoxMovement.SelectedIndex = 0;
    this._spinEditInterval.EditValue = (object) null;
    this._spinEditMovement.EditValue = (object) null;
    this._ternSample.PostPaint += new ImRtfEditor.EventPostPaint(this._ternSample_PostPaint);
    this._ternSample.TerSetFlags(true, 1048576 /*0x100000*/);
    this._ternSample.TerSetFlags5(true, 1073741824 /*0x40000000*/);
    this._ternSample.FittedView = false;
    this._ternSample.BorderMargin = false;
    this._ternSample.HorzScrollBar = false;
    this._ternSample.ReadOnlyMode = true;
    this._ternSample.TerSetMarginEx(-1, 0, 0, 0, 0, 0, 0, false);
    this._ternSample.TerSetFlags3(true, 1073741824 /*0x40000000*/);
    this._ternSample.TerSetCharSet((byte) 204);
    FontSetupDlg.FitPageSizeToWindow(this._ternSample, false);
    this._ternSample.SetTerParaFmt(1, true, false);
    this._ternSample.TerSetSectAlign(-1, 128 /*0x80*/, false);
    this._ternSample.TerSetFlags3(true, 128 /*0x80*/);
    BoldItalicStyle? boldItalic = this._charFormat.BoldItalic;
    if (boldItalic.HasValue)
    {
      boldItalic = this._charFormat.BoldItalic;
      this._lastChangedFontStyle = new FontSetupDlg.InnerFontStyleClass(boldItalic.Value);
    }
    this.LoadParams();
    this.LoadFonts();
    this.ActivateFont();
    this.LoadFontsSize();
    this.ActivateFontSize();
    this._ternSample.TerDeleteAll(false);
    if (this._testString != string.Empty)
      this._ternSample.InsertTerText(this._testString, true);
    else
      this._ternSample.InsertTerText(this._editFontFamily.Text, true);
  }

  /// <summary>Установить размер страницы соответсвующим окну редактора</summary>
  /// <param name="tern">Редактор</param>
  /// <param name="repaint">Перерисовать</param>
  public static void FitPageSizeToWindow(ImRtfEditor tern, bool repaint)
  {
    if (tern == null)
      throw new ArgumentNullException(nameof (tern));
    float dpiX;
    float dpiY;
    using (Graphics graphics = tern.CreateGraphics())
    {
      dpiX = graphics.DpiX;
      dpiY = graphics.DpiY;
    }
    Size twips = (Size) UnitsConverter.MmToTwips(UnitsConverter.PixelsToMm((Point) tern.Size, new PointF(dpiX, dpiY)));
    tern.TerSetSectPageSize(-1, PaperKind.Custom, twips.Width, twips.Height, repaint);
  }

  /// <summary> Дескриптор настраиваемого шрифта </summary>
  public CharFormat CharFormat
  {
    [DebuggerStepThrough] get => this._charFormat;
  }

  /// <summary> Установка визуальных контролов в соответствии с передаными параметрами </summary>
  /// <exception cref="T:System.NullReferenceException" />
  private void LoadParams()
  {
    this._editFontFamily.Text = this._charFormat != null ? this._charFormat.FontFamily : throw new NullReferenceException("FontSetupDlg.LoadParams () - _charFormat == null");
    TextBox editFontStyle = this._editFontStyle;
    BoldItalicStyle? boldItalic = this._charFormat.BoldItalic;
    string empty1;
    if (!boldItalic.HasValue)
    {
      empty1 = string.Empty;
    }
    else
    {
      boldItalic = this._charFormat.BoldItalic;
      empty1 = new FontSetupDlg.InnerFontStyleClass(boldItalic.Value).ToString();
    }
    editFontStyle.Text = empty1;
    TextBox editFontSize = this._editFontSize;
    float? fontSize = this._charFormat.FontSize;
    string empty2;
    if (!fontSize.HasValue)
    {
      empty2 = string.Empty;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      empty2 = fontSize.ToString();
    }
    editFontSize.Text = empty2;
    Color? nullable = this._charFormat.TextColor;
    if (!nullable.HasValue)
    {
      this._comboBoxTextColor.SelectedIndex = -1;
    }
    else
    {
      ComboBoxColorPicker comboBoxTextColor = this._comboBoxTextColor;
      nullable = this._charFormat.TextColor;
      Color color = nullable.Value;
      comboBoxTextColor.Color = color;
    }
    if (!this._charFormat.Underline.HasValue)
    {
      this._comboBoxUnderlineColor.Enabled = false;
      this._labelUnderlineColor.ForeColor = SystemColors.GrayText;
      this._comboBoxUnderlineStyle.SelectedIndex = -1;
    }
    else
    {
      UnderlineStyle? underline = this._charFormat.Underline;
      if (underline.HasValue)
      {
        switch (underline.GetValueOrDefault())
        {
          case UnderlineStyle.None:
            this._comboBoxUnderlineColor.Enabled = false;
            this._labelUnderlineColor.ForeColor = SystemColors.GrayText;
            this._comboBoxUnderlineStyle.SelectedIndex = 0;
            goto label_19;
          case UnderlineStyle.Underline:
            this._comboBoxUnderlineColor.Enabled = true;
            this._labelUnderlineColor.ForeColor = SystemColors.ControlText;
            this._comboBoxUnderlineStyle.SelectedIndex = 1;
            goto label_19;
          case UnderlineStyle.DoubleUnderline:
            this._comboBoxUnderlineColor.Enabled = true;
            this._labelUnderlineColor.ForeColor = SystemColors.ControlText;
            this._comboBoxUnderlineStyle.SelectedIndex = 2;
            goto label_19;
        }
      }
      this._comboBoxUnderlineColor.Enabled = false;
      this._labelUnderlineColor.ForeColor = SystemColors.GrayText;
      this._comboBoxUnderlineStyle.SelectedIndex = -1;
    }
label_19:
    nullable = this._charFormat.UnderlineColor;
    if (!nullable.HasValue)
    {
      this._comboBoxUnderlineColor.SelectedIndex = -1;
    }
    else
    {
      ComboBoxColorPicker boxUnderlineColor = this._comboBoxUnderlineColor;
      nullable = this._charFormat.UnderlineColor;
      Color color = nullable.Value;
      boxUnderlineColor.Color = color;
    }
    if ((this._charFormat.UndefinedCharStyles & CharStyle.Superscript) != CharStyle.Regular)
      this._checkBoxSuperscript.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxSuperscript.Checked = (this._charFormat.CharStyle & CharStyle.Superscript) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.Subscript) != CharStyle.Regular)
      this._checkBoxSubscript.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxSubscript.Checked = (this._charFormat.CharStyle & CharStyle.Subscript) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.Strikethrough) != CharStyle.Regular)
      this._checkBoxStryked.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxStryked.Checked = (this._charFormat.CharStyle & CharStyle.Strikethrough) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.DoubleStrikethrough) != CharStyle.Regular)
      this._checkBoxDoubleStrikout.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxDoubleStrikout.Checked = (this._charFormat.CharStyle & CharStyle.DoubleStrikethrough) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.HiddenText) != CharStyle.Regular)
      this._checkBoxHidden.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxHidden.Checked = (this._charFormat.CharStyle & CharStyle.HiddenText) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.AllSmallCaps) != CharStyle.Regular)
      this._checkBoxSmallCaps.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxSmallCaps.Checked = (this._charFormat.CharStyle & CharStyle.AllSmallCaps) != 0;
    if ((this._charFormat.UndefinedCharStyles & CharStyle.AllCaps) != CharStyle.Regular)
      this._checkBoxAllCaps.CheckState = CheckState.Indeterminate;
    else
      this._checkBoxAllCaps.Checked = (this._charFormat.CharStyle & CharStyle.AllCaps) != 0;
    this._loaded = true;
  }

  /// <summary> Загрузка списка шрифтов, установленных в системе </summary>
  private void LoadFonts()
  {
    using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
    {
      if (this._allFonts == null)
        this._allFonts = installedFontCollection.Families;
      string empty = string.Empty;
      this._listBoxFontFamily.BeginUpdate();
      try
      {
        this._listBoxFontFamily.Items.Clear();
        this._fontFamilysHash = string.Empty;
        if (this._allFonts.Length == 0)
          return;
        int num = 0;
        foreach (FontFamily allFont in this._allFonts)
        {
          string str = allFont.Name.Trim();
          if (!this._listBoxFontFamily.Items.Contains((object) str))
          {
            this._listBoxFontFamily.Items.Add((object) str);
            num += str.Length;
          }
        }
        StringBuilder stringBuilder = new StringBuilder(num + this._allFonts.Length);
        for (int index = 0; index < this._listBoxFontFamily.Items.Count; ++index)
        {
          stringBuilder.Append('#');
          stringBuilder.Append(((string) this._listBoxFontFamily.Items[index]).ToUpperInvariant());
        }
        this._fontFamilysHash = stringBuilder.ToString();
      }
      finally
      {
        this._listBoxFontFamily.EndUpdate();
      }
    }
  }

  /// <summary> Поиск шрифта, наиболее близко соответствующего введённому имени </summary>
  private void ActivateFont()
  {
    this._listBoxFontFamily.SelectedIndex = -1;
    int length1 = -1;
    string upperInvariant = this._editFontFamily.Text.Trim().ToUpperInvariant();
    string str = "#" + upperInvariant;
    for (int length2 = upperInvariant.Length; length2 > 0; --length2)
    {
      length1 = this._fontFamilysHash.IndexOf(str);
      if (length1 == -1)
        str = str.Remove(str.Length - 1, 1);
      else
        break;
    }
    if (length1 != -1)
    {
      string[] strArray = this._fontFamilysHash.Substring(0, length1).Split('#');
      this._listBoxFontFamily.TopIndex = strArray.Length - 1;
      if (!(this._editFontFamily.Text.Trim().ToUpperInvariant() == ((string) this._listBoxFontFamily.Items[strArray.Length - 1]).ToUpperInvariant()))
        return;
      this._listBoxFontFamily.SelectedIndex = strArray.Length - 1;
    }
    else
      this._listBoxFontFamily_SelectedIndexChanged((object) null, EventArgs.Empty);
  }

  /// <summary> Поиск стиля шрифта, наиболее близко соответствующего введённому имени </summary>
  private void ActivateFontStyle()
  {
    this._listBoxFontStyle.SelectedIndex = -1;
    int length1 = -1;
    string upperInvariant = this._editFontStyle.Text.Trim().ToUpperInvariant();
    string str = "#" + upperInvariant;
    for (int length2 = upperInvariant.Length; length2 > 0; --length2)
    {
      length1 = this._fontStylesHash.IndexOf(str);
      if (length1 == -1)
        str = str.Remove(str.Length - 1, 1);
      else
        break;
    }
    if (length1 == -1)
      return;
    string[] strArray = this._fontStylesHash.Substring(0, length1).Split('#');
    if (!(this._editFontStyle.Text.Trim().ToUpperInvariant() == this._listBoxFontStyle.Items[strArray.Length - 1].ToString().ToUpperInvariant()))
      return;
    this._listBoxFontStyle.SelectedIndex = strArray.Length - 1;
  }

  /// <summary> Поиск размера шрифта, наиболее близко соответствующего введённому рамеру </summary>
  private void ActivateFontSize()
  {
    this._listBoxFontSize.SelectedIndex = -1;
    int length1 = -1;
    string upperInvariant = this._editFontSize.Text.Trim().ToUpperInvariant();
    string str = "#" + upperInvariant;
    for (int length2 = upperInvariant.Length; length2 > 0; --length2)
    {
      length1 = this._fontSizesHash.IndexOf(str);
      if (length1 == -1)
        str = str.Remove(str.Length - 1, 1);
      else
        break;
    }
    if (length1 == -1)
      return;
    string[] strArray = this._fontSizesHash.Substring(0, length1).Split('#');
    if (!(this._editFontSize.Text.Trim().ToUpperInvariant() == this._listBoxFontSize.Items[strArray.Length - 1].ToString().ToUpperInvariant()))
      return;
    this._listBoxFontSize.SelectedIndex = strArray.Length - 1;
  }

  /// <summary> Блокирование обработки события редактирования имени шрифта </summary>
  private void LockFontFamilyEditEvent() => ++this._fontFamilyEditEventLockCounter;

  /// <summary> Разблокирование обработки события редактирования имени шрифта </summary>
  private void UnlockFontFamilyEditEvent()
  {
    if (this._fontFamilyEditEventLockCounter <= 0)
      return;
    --this._fontFamilyEditEventLockCounter;
  }

  /// <summary> Блокирование обработки события редактирования имени стиля </summary>
  private void LockFontStyleEditEvent() => ++this._fontStyleEditEventLockCounter;

  /// <summary> Раз,Разблокирование обработки события редактирования имени стиля </summary>
  private void UnlockFontStyleEditEvent()
  {
    if (this._fontStyleEditEventLockCounter <= 0)
      return;
    --this._fontStyleEditEventLockCounter;
  }

  /// <summary> Блокирование обработки события редактирования размера шрифта </summary>
  private void LockFontSizeEditEvent() => ++this._fontSizeEditEventLockCounter;

  /// <summary> Разблокирование обработки события редактирования размера шрифта </summary>
  private void UnlockFontSizeEditEvent()
  {
    if (this._fontSizeEditEventLockCounter <= 0)
      return;
    --this._fontSizeEditEventLockCounter;
  }

  /// <summary> Загрузка списка доступных стилей для выбраного шрифта </summary>
  private void LoadFontsStyles()
  {
    FontFamily fontFamily = (FontFamily) null;
    if (this._listBoxFontFamily.SelectedIndex >= 0 && this._listBoxFontFamily.SelectedIndex < this._allFonts.Length)
      fontFamily = this._allFonts[this._listBoxFontFamily.SelectedIndex];
    this._listBoxFontStyle.BeginUpdate();
    try
    {
      this._listBoxFontStyle.Items.Clear();
      this._fontStylesHash = string.Empty;
      if (fontFamily == null || fontFamily.IsStyleAvailable(FontStyle.Regular))
      {
        this._listBoxFontStyle.Items.Add(this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Normal]);
        this._fontStylesHash = "#" + this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Normal].ToString().ToUpperInvariant();
      }
      if (fontFamily == null || fontFamily.IsStyleAvailable(FontStyle.Italic))
      {
        this._listBoxFontStyle.Items.Add(this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Italic]);
        this._fontStylesHash = $"{this._fontStylesHash}#{this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Italic].ToString().ToUpperInvariant()}";
      }
      if (fontFamily == null || fontFamily.IsStyleAvailable(FontStyle.Bold))
      {
        this._listBoxFontStyle.Items.Add(this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Bold]);
        this._fontStylesHash = $"{this._fontStylesHash}#{this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.Bold].ToString().ToUpperInvariant()}";
      }
      if (fontFamily == null || fontFamily.IsStyleAvailable(FontStyle.Regular))
      {
        this._listBoxFontStyle.Items.Add(this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.BoldItalic]);
        this._fontStylesHash = $"{this._fontStylesHash}#{this._allFontStyles[(object) FontSetupDlg.InnerFontStyleEnum.BoldItalic].ToString().ToUpperInvariant()}";
      }
      this.LockFontStyleEditEvent();
      try
      {
        this._listBoxFontStyle.SelectedIndex = -1;
        if (this._lastChangedFontStyle == null)
          return;
        object[] objArray = new object[this._listBoxFontStyle.Items.Count];
        int num = -1;
        for (int index = 0; index < this._listBoxFontStyle.Items.Count; ++index)
        {
          if ((this._listBoxFontStyle.Items[index] as FontSetupDlg.InnerFontStyleClass).BoldItalicStyle == this._lastChangedFontStyle.BoldItalicStyle)
            num = index;
        }
        if (num == -1)
          return;
        this._listBoxFontStyle.SelectedIndex = num;
      }
      finally
      {
        this.UnlockFontStyleEditEvent();
      }
    }
    finally
    {
      this._listBoxFontStyle.EndUpdate();
    }
  }

  /// <summary> Загрузка списка доступных размеров для выбраного шрифта </summary>
  private void LoadFontsSize()
  {
    this._fontSizesHash = string.Empty;
    foreach (string str in this._listBoxFontSize.Items)
      this._fontSizesHash = $"{this._fontSizesHash}#{str}";
  }

  /// <summary> Обработка нажатий специальных клавиш </summary>
  /// <param name="msg"></param>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Up:
        if (this._editFontFamily.Focused)
        {
          if (this._listBoxFontFamily.SelectedIndex == -1)
            this._listBoxFontFamily.SelectedIndex = this._listBoxFontFamily.TopIndex;
          else if (this._listBoxFontFamily.SelectedIndex > 0)
            --this._listBoxFontFamily.SelectedIndex;
          else
            this._listBoxFontFamily.SelectedIndex = this._listBoxFontFamily.SelectedIndex;
          return true;
        }
        if (this._editFontStyle.Focused)
        {
          if (this._listBoxFontStyle.SelectedIndex == -1)
            this._listBoxFontStyle.SelectedIndex = 0;
          else if (this._listBoxFontStyle.SelectedIndex > 0)
            --this._listBoxFontStyle.SelectedIndex;
          else
            this._listBoxFontStyle.SelectedIndex = this._listBoxFontStyle.SelectedIndex;
          return true;
        }
        if (this._editFontSize.Focused)
        {
          if (this._listBoxFontSize.SelectedIndex == -1)
            this._listBoxFontSize.SelectedIndex = 0;
          else if (this._listBoxFontSize.SelectedIndex > 0)
            --this._listBoxFontSize.SelectedIndex;
          else
            this._listBoxFontSize.SelectedIndex = this._listBoxFontSize.SelectedIndex;
          return true;
        }
        break;
      case Keys.Down:
        if (this._editFontFamily.Focused)
        {
          if (this._listBoxFontFamily.SelectedIndex == -1)
            this._listBoxFontFamily.SelectedIndex = this._listBoxFontFamily.TopIndex;
          else if (this._listBoxFontFamily.SelectedIndex < this._listBoxFontFamily.Items.Count - 1)
            ++this._listBoxFontFamily.SelectedIndex;
          else
            this._listBoxFontFamily.SelectedIndex = this._listBoxFontFamily.SelectedIndex;
          return true;
        }
        if (this._editFontStyle.Focused)
        {
          if (this._listBoxFontStyle.SelectedIndex == -1)
            this._listBoxFontStyle.SelectedIndex = 0;
          else if (this._listBoxFontStyle.SelectedIndex < this._listBoxFontStyle.Items.Count - 1)
            ++this._listBoxFontStyle.SelectedIndex;
          else
            this._listBoxFontStyle.SelectedIndex = this._listBoxFontStyle.SelectedIndex;
          return true;
        }
        if (this._editFontSize.Focused)
        {
          if (this._listBoxFontSize.SelectedIndex == -1)
            this._listBoxFontSize.SelectedIndex = 0;
          else if (this._listBoxFontSize.SelectedIndex < this._listBoxFontSize.Items.Count - 1)
            ++this._listBoxFontSize.SelectedIndex;
          else
            this._listBoxFontSize.SelectedIndex = this._listBoxFontSize.SelectedIndex;
          return true;
        }
        break;
    }
    return base.ProcessCmdKey(ref msg, keyData);
  }

  /// <summary> Перерисовка образца текста </summary>
  private void UpdateSampleText()
  {
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float num = this._charFormat.FontSize.HasValue ? this._charFormat.FontSize.Value : 10f;
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Сохранение изменений в дексриптор шрифта, который был передан в конструктор </summary>
  public void Save()
  {
    if (this._charFormat == null)
      throw new Exception("FontSetupDlg.Save() : _charFormat == null");
    if (this._oldCharFormat == null)
      throw new Exception("FontSetupDlg.Save() : _oldCharFormat == null");
    this._oldCharFormat.CopyParamsFrom(this._charFormat);
  }

  /// <summary> Отрисовка горизонтальной линии в образце текста </summary>
  /// <param name="Sender"></param>
  /// <param name="gr"></param>
  private void _ternSample_PostPaint(object Sender, Graphics gr)
  {
    int pX = 0;
    int pY = 0;
    if (!this._ternSample.TerTextPosToPix(2, 0, -1, out pX, out pY))
      return;
    int pBaseHeight;
    int pExtLead;
    this._ternSample.pos.GetLineHeight(0, out pBaseHeight, out pExtLead);
    int y = pY + (pBaseHeight - pExtLead);
    using (Pen pen = new Pen(Color.Black))
    {
      gr.DrawLine(pen, new Point(0, y), new Point(pX - 5, y));
      gr.DrawLine(pen, new Point(this._ternSample.ClientSize.Width - (pX - 5), y), new Point(this._ternSample.ClientSize.Width, y));
    }
  }

  /// <summary> Отрисовка доступных стилий подчёркиваний в выпадающем меню </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void comboBoxUnderlineStyle_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    int num1 = e.Index == -1 ? this._comboBoxUnderlineStyle.SelectedIndex : e.Index;
    if (num1 < 0 || num1 >= this._comboBoxUnderlineStyle.Items.Count)
      return;
    switch (num1)
    {
      case 0:
        using (StringFormat format = new StringFormat())
        {
          format.Alignment = StringAlignment.Near;
          format.LineAlignment = StringAlignment.Center;
          e.Graphics.DrawString(LocalizationHolder.rm.GetString("Document.Model_60"), this._comboBoxUnderlineStyle.Font, Brushes.Black, (RectangleF) e.Bounds, format);
          break;
        }
      case 1:
        using (Pen pen1 = new Pen(Color.Black))
        {
          int top = e.Bounds.Top;
          Rectangle bounds = e.Bounds;
          int num2 = bounds.Height / 2;
          int y = top + num2;
          Graphics graphics = e.Graphics;
          Pen pen2 = pen1;
          bounds = e.Bounds;
          Point pt1 = new Point(bounds.Left + 5, y);
          bounds = e.Bounds;
          Point pt2 = new Point(bounds.Right - 5, y);
          graphics.DrawLine(pen2, pt1, pt2);
          break;
        }
      case 2:
        using (Pen pen3 = new Pen(Color.Black))
        {
          int top = e.Bounds.Top;
          Rectangle bounds = e.Bounds;
          int num3 = bounds.Height / 2;
          int num4 = top + num3;
          Graphics graphics1 = e.Graphics;
          Pen pen4 = pen3;
          bounds = e.Bounds;
          Point pt1_1 = new Point(bounds.Left + 5, num4 - 1);
          bounds = e.Bounds;
          Point pt2_1 = new Point(bounds.Right - 5, num4 - 1);
          graphics1.DrawLine(pen4, pt1_1, pt2_1);
          Graphics graphics2 = e.Graphics;
          Pen pen5 = pen3;
          bounds = e.Bounds;
          Point pt1_2 = new Point(bounds.Left + 5, num4 + 1);
          bounds = e.Bounds;
          Point pt2_2 = new Point(bounds.Right - 5, num4 + 1);
          graphics2.DrawLine(pen5, pt1_2, pt2_2);
          break;
        }
    }
  }

  /// <summary> Пользователь изменил имя шрифта </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _editFontFamily_TextChanged(object sender, EventArgs e)
  {
    if (this._fontFamilyEditEventLockCounter != 0)
      return;
    this.LockFontFamilyEditEvent();
    try
    {
      this.ActivateFont();
    }
    finally
    {
      this.UnlockFontFamilyEditEvent();
    }
  }

  /// <summary> Пользователь активировал некоторый иной шрифт </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _listBoxFontFamily_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._fontFamilyEditEventLockCounter == 0 || this._listBoxFontFamily.SelectedIndex != -1)
    {
      this.LockFontFamilyEditEvent();
      try
      {
        this._editFontFamily.Text = this._listBoxFontFamily.SelectedIndex < 0 || this._listBoxFontFamily.SelectedIndex >= this._listBoxFontFamily.Items.Count ? string.Empty : (string) this._listBoxFontFamily.Items[this._listBoxFontFamily.SelectedIndex];
        this._editFontFamily.SelectAll();
      }
      finally
      {
        this.UnlockFontFamilyEditEvent();
      }
    }
    this.LoadFontsStyles();
    if (!this._loaded || this._listBoxFontFamily.SelectedIndex == -1)
      return;
    this._charFormat.FontFamily = (string) this._listBoxFontFamily.Items[this._listBoxFontFamily.SelectedIndex];
    if (!this._ternSample.GetFontInfo(-9999, out string _, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    this._ternSample.SetTerDefaultFont(this._charFormat.FontFamily, -(int) Math.Round((double) num * 20.0), (int) this._charFormat.CharStyle, this._charFormat.TextColor.HasValue ? this._charFormat.TextColor.Value : Color.Black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Пользователь изменил имя стиля </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _editFontStyle_TextChanged(object sender, EventArgs e)
  {
    if (this._fontStyleEditEventLockCounter != 0)
      return;
    this.LockFontStyleEditEvent();
    try
    {
      this.ActivateFontStyle();
    }
    finally
    {
      this.UnlockFontStyleEditEvent();
    }
  }

  /// <summary> Пользователь активировал некоторый иной стиль шрифта </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _listBoxFontStyle_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._fontStyleEditEventLockCounter == 0 || this._listBoxFontStyle.SelectedIndex != -1)
    {
      this.LockFontStyleEditEvent();
      try
      {
        string str = this._listBoxFontStyle.SelectedIndex < 0 || this._listBoxFontStyle.SelectedIndex >= this._listBoxFontFamily.Items.Count ? string.Empty : this._listBoxFontStyle.Items[this._listBoxFontStyle.SelectedIndex].ToString();
        if (this._fontStyleEditEventLockCounter == 1 && str != string.Empty)
          this._lastChangedFontStyle = (FontSetupDlg.InnerFontStyleClass) this._listBoxFontStyle.Items[this._listBoxFontStyle.SelectedIndex];
        this._editFontStyle.Text = str;
        this._editFontStyle.SelectAll();
      }
      finally
      {
        this.UnlockFontStyleEditEvent();
      }
    }
    if (!this._loaded || this._listBoxFontStyle.SelectedIndex == -1)
      return;
    this._charFormat.BoldItalic = new BoldItalicStyle?((BoldItalicStyle) (FontSetupDlg.InnerFontStyleClass) this._listBoxFontStyle.Items[this._listBoxFontStyle.SelectedIndex]);
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Пользователь изменил имя размер шрифта </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _editFontSize_TextChanged(object sender, EventArgs e)
  {
    if (this._fontSizeEditEventLockCounter != 0)
      return;
    this.LockFontSizeEditEvent();
    try
    {
      this.ActivateFontSize();
    }
    finally
    {
      this.UnlockFontSizeEditEvent();
    }
  }

  /// <summary> Пользователь активировал некоторый иной размер шрифта </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _listBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._fontSizeEditEventLockCounter != 0 && this._listBoxFontSize.SelectedIndex == -1)
      return;
    this.LockFontSizeEditEvent();
    try
    {
      string s = this._listBoxFontSize.SelectedIndex < 0 || this._listBoxFontSize.SelectedIndex >= this._listBoxFontSize.Items.Count ? string.Empty : (string) this._listBoxFontSize.Items[this._listBoxFontSize.SelectedIndex];
      if (this._editFontSize.Text.Trim() != s)
        this._editFontSize.Text = s;
      if (this._loaded && s != string.Empty)
      {
        float result = 0.0f;
        if (float.TryParse(s, out result))
        {
          result = 0.25f * (float) (int) Math.Round((double) result / 0.25);
          this._charFormat.FontSize = new float?(result);
          this._editFontSize.Text = result.ToString();
        }
      }
      string TypeFace1;
      if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
        return;
      float? fontSize = this._charFormat.FontSize;
      float num;
      if (!fontSize.HasValue)
      {
        num = 10f;
      }
      else
      {
        fontSize = this._charFormat.FontSize;
        num = fontSize.Value;
      }
      ImRtfEditor ternSample = this._ternSample;
      string TypeFace2 = TypeFace1;
      int PointSize = -(int) Math.Round((double) num * 20.0);
      int charStyle = (int) this._charFormat.CharStyle;
      Color? textColor = this._charFormat.TextColor;
      Color black;
      if (!textColor.HasValue)
      {
        black = Color.Black;
      }
      else
      {
        textColor = this._charFormat.TextColor;
        black = textColor.Value;
      }
      ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
      this._ternSample.TerDeleteAll(false);
      this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
    }
    finally
    {
      this.UnlockFontSizeEditEvent();
    }
  }

  /// <summary> Был выбран новый цвет текста </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxTextColor_SelectedColorChanged(object sender, EventArgs e)
  {
    this._comboBoxTextColor.SelectedIndex = 0;
    if (!this._loaded)
      return;
    this._charFormat.TextColor = new Color?(this._comboBoxTextColor.Color);
    string TypeFace;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace, out int _, out int _))
      return;
    float num = this._charFormat.FontSize.HasValue ? this._charFormat.FontSize.Value : 10f;
    this._ternSample.SetTerDefaultFont(TypeFace, -(int) Math.Round((double) num * 20.0), (int) this._charFormat.CharStyle, this._charFormat.TextColor.Value, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был выбран новый стиль подчёркивания </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxUnderlineStyle_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this._loaded || this._comboBoxUnderlineStyle.SelectedIndex == -1)
      return;
    switch (this._comboBoxUnderlineStyle.SelectedIndex)
    {
      case 0:
        this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
        this._comboBoxUnderlineColor.Enabled = false;
        this._labelUnderlineColor.ForeColor = SystemColors.GrayText;
        break;
      case 1:
        this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
        this._comboBoxUnderlineColor.Enabled = true;
        this._labelUnderlineColor.ForeColor = SystemColors.ControlText;
        break;
      case 2:
        this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.DoubleUnderline);
        this._comboBoxUnderlineColor.Enabled = true;
        this._labelUnderlineColor.ForeColor = SystemColors.ControlText;
        break;
      default:
        return;
    }
    string TypeFace;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    this._ternSample.SetTerDefaultFont(TypeFace, -(int) Math.Round((double) num * 20.0), (int) this._charFormat.CharStyle, this._charFormat.TextColor.HasValue ? this._charFormat.TextColor.Value : Color.Black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был выбран новый цвет подчёркивания </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _comboBoxUnderlineColor_SelectedColorChanged(object sender, EventArgs e)
  {
    this._comboBoxUnderlineColor.SelectedIndex = 0;
    if (!this._comboBoxUnderlineColor.Enabled || !this._loaded || this._comboBoxUnderlineColor.SelectedIndex == -1)
      return;
    this._charFormat.UnderlineColor = new Color?(this._comboBoxUnderlineColor.Color);
  }

  /// <summary> Был отредактирован флажок "надстрочный" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxSuperscript_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox) || (sender as CheckBox).CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    this._charFormat.CharStyle = (sender as CheckBox).Checked ? this._charFormat.CharStyle | CharStyle.Superscript : this._charFormat.CharStyle & ~CharStyle.Superscript;
    this._charFormat.UndefinedCharStyles &= ~CharStyle.Superscript;
    if ((sender as CheckBox).Checked)
    {
      this._charFormat.UndefinedCharStyles &= ~CharStyle.Subscript;
      this._charFormat.CharStyle &= ~CharStyle.Subscript;
      this._checkBoxSubscript.Checked = false;
    }
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был отредактирован флажок "подстрочный" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxSubscript_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox) || (sender as CheckBox).CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    this._charFormat.CharStyle = (sender as CheckBox).Checked ? this._charFormat.CharStyle | CharStyle.Subscript : this._charFormat.CharStyle & ~CharStyle.Subscript;
    this._charFormat.UndefinedCharStyles &= ~CharStyle.Subscript;
    if ((sender as CheckBox).Checked)
    {
      this._charFormat.UndefinedCharStyles &= ~CharStyle.Superscript;
      this._charFormat.CharStyle &= ~CharStyle.Superscript;
      this._checkBoxSuperscript.Checked = false;
    }
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был отредактирован флажок "зачёркивание" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxStryked_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox checkBox) || checkBox.CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    if (checkBox.Checked)
    {
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.SingleLine);
      this._checkBoxDoubleStrikout.Checked = false;
    }
    else if (!this._checkBoxStryked.Checked && !this._checkBoxDoubleStrikout.Checked)
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.None);
    this.UpdateSampleText();
  }

  /// <summary> Был отредактирован флажок "двойное зачёркивание" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxDoubleStrikeout_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox checkBox) || checkBox.CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    if (checkBox.Checked)
    {
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.DoubleLine);
      this._checkBoxStryked.Checked = false;
    }
    else if (!this._checkBoxStryked.Checked && !this._checkBoxDoubleStrikout.Checked)
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.None);
    this.UpdateSampleText();
  }

  /// <summary> Был отредактирован флажок "скрытый" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxHidden_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox) || (sender as CheckBox).CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    this._charFormat.CharStyle = (sender as CheckBox).Checked ? this._charFormat.CharStyle | CharStyle.HiddenText : this._charFormat.CharStyle & ~CharStyle.HiddenText;
    this._charFormat.UndefinedCharStyles &= ~CharStyle.HiddenText;
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был отредактирован флажок "малые прописные" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxSmallCaps_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox) || (sender as CheckBox).CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    this._charFormat.AllSmallCaps = new bool?((sender as CheckBox).Checked);
    if ((sender as CheckBox).Checked)
      this._checkBoxAllCaps.Checked = false;
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  /// <summary> Был отредактирован флажок "все прописные" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _checkBoxAllCaps_CheckedChanged(object sender, EventArgs e)
  {
    if (!(sender is CheckBox) || (sender as CheckBox).CheckState == CheckState.Indeterminate || !this._loaded)
      return;
    this._charFormat.CharStyle = (sender as CheckBox).Checked ? this._charFormat.CharStyle | CharStyle.AllCaps : this._charFormat.CharStyle & ~CharStyle.AllCaps;
    this._charFormat.UndefinedCharStyles &= ~CharStyle.AllCaps;
    if ((sender as CheckBox).Checked)
    {
      this._charFormat.UndefinedCharStyles &= ~CharStyle.AllSmallCaps;
      this._charFormat.CharStyle &= ~CharStyle.AllSmallCaps;
      this._checkBoxSmallCaps.Checked = false;
    }
    string TypeFace1;
    if (!this._ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num;
    if (!fontSize.HasValue)
    {
      num = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num = fontSize.Value;
    }
    ImRtfEditor ternSample = this._ternSample;
    string TypeFace2 = TypeFace1;
    int PointSize = -(int) Math.Round((double) num * 20.0);
    int charStyle = (int) this._charFormat.CharStyle;
    Color? textColor = this._charFormat.TextColor;
    Color black;
    if (!textColor.HasValue)
    {
      black = Color.Black;
    }
    else
    {
      textColor = this._charFormat.TextColor;
      black = textColor.Value;
    }
    ternSample.SetTerDefaultFont(TypeFace2, PointSize, charStyle, black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
  }

  private void _btnOK_Click(object sender, EventArgs e)
  {
  }

  private void _editFontSize_Leave(object sender, EventArgs e)
  {
    string text = this._editFontSize.Text;
    double number;
    string textAfterNumber;
    NumberParserAdvanced.ParseNumber(this._editFontSize.Text, true, out number, out string _, out textAfterNumber);
    string str = textAfterNumber.Trim();
    if (text != null)
    {
      double num;
      switch (str)
      {
        case "":
          num = 1.0;
          break;
        case "cm":
          num = 3600.0 / (double) sbyte.MaxValue;
          break;
        case "m":
          num = 283.46456692913387;
          break;
        case "mm":
          num = 360.0 / (double) sbyte.MaxValue;
          break;
        case "pt":
          num = 1.0;
          break;
        case "м":
          num = 283.46456692913387;
          break;
        case "мм":
          num = 360.0 / (double) sbyte.MaxValue;
          break;
        case "пт":
          num = 1.0;
          break;
        case "см":
          num = 3600.0 / (double) sbyte.MaxValue;
          break;
        default:
          num = 1.0;
          break;
      }
      this._editFontSize.Text = Math.Round(number * num, 2).ToString();
    }
    string s = this._editFontSize.Text.Trim();
    bool flag = false;
    if (this._loaded && s != string.Empty)
    {
      float result = 0.0f;
      if (float.TryParse(s, out result))
      {
        result = 0.25f * (float) (int) Math.Round((double) result / 0.25);
        this._charFormat.FontSize = new float?(result);
        this._editFontSize.Text = result.ToString();
        flag = true;
      }
    }
    string TypeFace;
    if (!flag || !this._ternSample.GetFontInfo(-9999, out TypeFace, out int _, out int _))
      return;
    float? fontSize = this._charFormat.FontSize;
    float num1;
    if (!fontSize.HasValue)
    {
      num1 = 10f;
    }
    else
    {
      fontSize = this._charFormat.FontSize;
      num1 = fontSize.Value;
    }
    this._ternSample.SetTerDefaultFont(TypeFace, -(int) Math.Round((double) num1 * 20.0), (int) this._charFormat.CharStyle, this._charFormat.TextColor.HasValue ? this._charFormat.TextColor.Value : Color.Black, false);
    this._ternSample.TerDeleteAll(false);
    this._ternSample.InsertTerText(this._testString == string.Empty ? this._charFormat.FontFamily : this._testString, true);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FontSetupDlg));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._btnDefault = new Button();
    this._tabSelector = new TabControl();
    this._tabFont = new TabPage();
    this._labelFontSize = new Label();
    this._labelFontStyle = new Label();
    this._labelFontFamily = new Label();
    this._labelUnderlineColor = new Label();
    this._labelTextColor = new Label();
    this._checkBoxAllCaps = new CheckBox();
    this._checkBoxSmallCaps = new CheckBox();
    this._checkBoxHidden = new CheckBox();
    this._checkBoxStryked = new CheckBox();
    this._checkBoxSubscript = new CheckBox();
    this._checkBoxSuperscript = new CheckBox();
    this._bevelOptions = new Bevel();
    this._labelOptions = new Label();
    this._comboBoxUnderlineColor = new ComboBoxColorPicker();
    this._comboBoxUnderlineStyle = new System.Windows.Forms.ComboBox();
    this._comboBoxTextColor = new ComboBoxColorPicker();
    this._labelUnderLineStyle = new Label();
    this._listBoxFontSize = new ListBox();
    this._editFontSize = new TextBox();
    this._listBoxFontStyle = new ListBox();
    this._editFontStyle = new TextBox();
    this._listBoxFontFamily = new ListBox();
    this._editFontFamily = new TextBox();
    this._tabInterval = new TabPage();
    this._labelOnInterval = new Label();
    this._labelMovement = new Label();
    this._labelInterval = new Label();
    this._labelZoom = new Label();
    this._labelOnMovement = new Label();
    this._comboBoxZoom = new System.Windows.Forms.ComboBox();
    this._spinEditMovement = new SpinEdit();
    this._spinEditInterval = new SpinEdit();
    this._comboBoxMovement = new System.Windows.Forms.ComboBox();
    this._comboBoxInterval = new System.Windows.Forms.ComboBox();
    this._panelSampleBorder = new Panel();
    this._ternSample = new ImRtfEditor();
    this._labelSample = new Label();
    this._labelFontType = new Label();
    this._bevelSample = new Bevel();
    this._checkBoxDoubleStrikout = new CheckBox();
    this._tabSelector.SuspendLayout();
    this._tabFont.SuspendLayout();
    this._tabInterval.SuspendLayout();
    this._spinEditMovement.Properties.BeginInit();
    this._spinEditInterval.Properties.BeginInit();
    this._panelSampleBorder.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    componentResourceManager.ApplyResources((object) this._btnDefault, "_btnDefault");
    this._btnDefault.Name = "_btnDefault";
    componentResourceManager.ApplyResources((object) this._tabSelector, "_tabSelector");
    this._tabSelector.Controls.Add((Control) this._tabFont);
    this._tabSelector.Controls.Add((Control) this._tabInterval);
    this._tabSelector.DataBindings.Add(new Binding("SelectedIndex", (object) Settings.Default, "FontSetupDialogActivePage", true, DataSourceUpdateMode.OnPropertyChanged));
    this._tabSelector.Name = "_tabSelector";
    this._tabSelector.SelectedIndex = Settings.Default.FontSetupDialogActivePage;
    this._tabFont.BackColor = SystemColors.Control;
    this._tabFont.Controls.Add((Control) this._checkBoxDoubleStrikout);
    this._tabFont.Controls.Add((Control) this._labelFontSize);
    this._tabFont.Controls.Add((Control) this._labelFontStyle);
    this._tabFont.Controls.Add((Control) this._labelFontFamily);
    this._tabFont.Controls.Add((Control) this._labelUnderlineColor);
    this._tabFont.Controls.Add((Control) this._labelTextColor);
    this._tabFont.Controls.Add((Control) this._checkBoxAllCaps);
    this._tabFont.Controls.Add((Control) this._checkBoxSmallCaps);
    this._tabFont.Controls.Add((Control) this._checkBoxHidden);
    this._tabFont.Controls.Add((Control) this._checkBoxStryked);
    this._tabFont.Controls.Add((Control) this._checkBoxSubscript);
    this._tabFont.Controls.Add((Control) this._checkBoxSuperscript);
    this._tabFont.Controls.Add((Control) this._bevelOptions);
    this._tabFont.Controls.Add((Control) this._labelOptions);
    this._tabFont.Controls.Add((Control) this._comboBoxUnderlineColor);
    this._tabFont.Controls.Add((Control) this._comboBoxUnderlineStyle);
    this._tabFont.Controls.Add((Control) this._comboBoxTextColor);
    this._tabFont.Controls.Add((Control) this._labelUnderLineStyle);
    this._tabFont.Controls.Add((Control) this._listBoxFontSize);
    this._tabFont.Controls.Add((Control) this._editFontSize);
    this._tabFont.Controls.Add((Control) this._listBoxFontStyle);
    this._tabFont.Controls.Add((Control) this._editFontStyle);
    this._tabFont.Controls.Add((Control) this._listBoxFontFamily);
    this._tabFont.Controls.Add((Control) this._editFontFamily);
    componentResourceManager.ApplyResources((object) this._tabFont, "_tabFont");
    this._tabFont.Name = "_tabFont";
    componentResourceManager.ApplyResources((object) this._labelFontSize, "_labelFontSize");
    this._labelFontSize.BackColor = SystemColors.Control;
    this._labelFontSize.FlatStyle = FlatStyle.System;
    this._labelFontSize.Name = "_labelFontSize";
    componentResourceManager.ApplyResources((object) this._labelFontStyle, "_labelFontStyle");
    this._labelFontStyle.BackColor = SystemColors.Control;
    this._labelFontStyle.FlatStyle = FlatStyle.System;
    this._labelFontStyle.Name = "_labelFontStyle";
    componentResourceManager.ApplyResources((object) this._labelFontFamily, "_labelFontFamily");
    this._labelFontFamily.BackColor = SystemColors.Control;
    this._labelFontFamily.FlatStyle = FlatStyle.System;
    this._labelFontFamily.Name = "_labelFontFamily";
    componentResourceManager.ApplyResources((object) this._labelUnderlineColor, "_labelUnderlineColor");
    this._labelUnderlineColor.BackColor = SystemColors.Control;
    this._labelUnderlineColor.FlatStyle = FlatStyle.System;
    this._labelUnderlineColor.Name = "_labelUnderlineColor";
    componentResourceManager.ApplyResources((object) this._labelTextColor, "_labelTextColor");
    this._labelTextColor.BackColor = SystemColors.Control;
    this._labelTextColor.FlatStyle = FlatStyle.System;
    this._labelTextColor.Name = "_labelTextColor";
    componentResourceManager.ApplyResources((object) this._checkBoxAllCaps, "_checkBoxAllCaps");
    this._checkBoxAllCaps.BackColor = SystemColors.Control;
    this._checkBoxAllCaps.Name = "_checkBoxAllCaps";
    this._checkBoxAllCaps.UseVisualStyleBackColor = false;
    this._checkBoxAllCaps.CheckedChanged += new EventHandler(this._checkBoxAllCaps_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxSmallCaps, "_checkBoxSmallCaps");
    this._checkBoxSmallCaps.BackColor = SystemColors.Control;
    this._checkBoxSmallCaps.Name = "_checkBoxSmallCaps";
    this._checkBoxSmallCaps.UseVisualStyleBackColor = false;
    this._checkBoxSmallCaps.CheckedChanged += new EventHandler(this._checkBoxSmallCaps_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxHidden, "_checkBoxHidden");
    this._checkBoxHidden.BackColor = SystemColors.Control;
    this._checkBoxHidden.Name = "_checkBoxHidden";
    this._checkBoxHidden.UseVisualStyleBackColor = false;
    this._checkBoxHidden.CheckedChanged += new EventHandler(this._checkBoxHidden_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxStryked, "_checkBoxStryked");
    this._checkBoxStryked.BackColor = SystemColors.Control;
    this._checkBoxStryked.Name = "_checkBoxStryked";
    this._checkBoxStryked.UseVisualStyleBackColor = false;
    this._checkBoxStryked.CheckedChanged += new EventHandler(this._checkBoxStryked_CheckedChanged);
    this._checkBoxSubscript.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this._checkBoxSubscript, "_checkBoxSubscript");
    this._checkBoxSubscript.BackColor = SystemColors.Control;
    this._checkBoxSubscript.Name = "_checkBoxSubscript";
    this._checkBoxSubscript.UseVisualStyleBackColor = false;
    this._checkBoxSubscript.CheckedChanged += new EventHandler(this._checkBoxSubscript_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxSuperscript, "_checkBoxSuperscript");
    this._checkBoxSuperscript.BackColor = SystemColors.Control;
    this._checkBoxSuperscript.Name = "_checkBoxSuperscript";
    this._checkBoxSuperscript.UseVisualStyleBackColor = false;
    this._checkBoxSuperscript.CheckedChanged += new EventHandler(this._checkBoxSuperscript_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._bevelOptions, "_bevelOptions");
    this._bevelOptions.BackColor = Color.Transparent;
    this._bevelOptions.Name = "_bevelOptions";
    componentResourceManager.ApplyResources((object) this._labelOptions, "_labelOptions");
    this._labelOptions.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelOptions.Name = "_labelOptions";
    componentResourceManager.ApplyResources((object) this._comboBoxUnderlineColor, "_comboBoxUnderlineColor");
    this._comboBoxUnderlineColor.Color = Color.Black;
    this._comboBoxUnderlineColor.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboBoxUnderlineColor.DropDownHeight = 1;
    this._comboBoxUnderlineColor.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxUnderlineColor.DropDownWidth = 1;
    this._comboBoxUnderlineColor.FormattingEnabled = true;
    this._comboBoxUnderlineColor.Items.AddRange(new object[48 /*0x30*/]
    {
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items1"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items2"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items3"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items4"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items5"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items6"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items7"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items8"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items9"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items10"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items11"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items12"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items13"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items14"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items15"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items16"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items17"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items18"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items19"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items20"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items21"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items22"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items23"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items24"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items25"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items26"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items27"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items28"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items29"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items30"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items31"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items32"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items33"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items34"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items35"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items36"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items37"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items38"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items39"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items40"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items41"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items42"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items43"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items44"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items45"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items46"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineColor.Items47")
    });
    this._comboBoxUnderlineColor.Name = "_comboBoxUnderlineColor";
    this._comboBoxUnderlineColor.SelectedColorChanged += new EventHandler(this._comboBoxUnderlineColor_SelectedColorChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxUnderlineStyle, "_comboBoxUnderlineStyle");
    this._comboBoxUnderlineStyle.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboBoxUnderlineStyle.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxUnderlineStyle.FormattingEnabled = true;
    this._comboBoxUnderlineStyle.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxUnderlineStyle.Items"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineStyle.Items1"),
      (object) componentResourceManager.GetString("_comboBoxUnderlineStyle.Items2")
    });
    this._comboBoxUnderlineStyle.Name = "_comboBoxUnderlineStyle";
    this._comboBoxUnderlineStyle.DrawItem += new DrawItemEventHandler(this.comboBoxUnderlineStyle_DrawItem);
    this._comboBoxUnderlineStyle.SelectedIndexChanged += new EventHandler(this._comboBoxUnderlineStyle_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._comboBoxTextColor, "_comboBoxTextColor");
    this._comboBoxTextColor.Color = Color.Black;
    this._comboBoxTextColor.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboBoxTextColor.DropDownHeight = 1;
    this._comboBoxTextColor.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxTextColor.DropDownWidth = 1;
    this._comboBoxTextColor.FormattingEnabled = true;
    this._comboBoxTextColor.Items.AddRange(new object[47]
    {
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items1"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items2"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items3"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items4"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items5"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items6"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items7"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items8"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items9"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items10"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items11"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items12"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items13"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items14"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items15"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items16"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items17"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items18"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items19"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items20"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items21"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items22"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items23"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items24"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items25"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items26"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items27"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items28"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items29"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items30"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items31"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items32"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items33"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items34"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items35"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items36"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items37"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items38"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items39"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items40"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items41"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items42"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items43"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items44"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items45"),
      (object) componentResourceManager.GetString("_comboBoxTextColor.Items46")
    });
    this._comboBoxTextColor.Name = "_comboBoxTextColor";
    this._comboBoxTextColor.SelectedColorChanged += new EventHandler(this._comboBoxTextColor_SelectedColorChanged);
    componentResourceManager.ApplyResources((object) this._labelUnderLineStyle, "_labelUnderLineStyle");
    this._labelUnderLineStyle.BackColor = SystemColors.Control;
    this._labelUnderLineStyle.FlatStyle = FlatStyle.System;
    this._labelUnderLineStyle.Name = "_labelUnderLineStyle";
    componentResourceManager.ApplyResources((object) this._listBoxFontSize, "_listBoxFontSize");
    this._listBoxFontSize.FormattingEnabled = true;
    this._listBoxFontSize.Items.AddRange(new object[16 /*0x10*/]
    {
      (object) componentResourceManager.GetString("_listBoxFontSize.Items"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items1"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items2"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items3"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items4"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items5"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items6"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items7"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items8"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items9"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items10"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items11"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items12"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items13"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items14"),
      (object) componentResourceManager.GetString("_listBoxFontSize.Items15")
    });
    this._listBoxFontSize.Name = "_listBoxFontSize";
    this._listBoxFontSize.TabStop = false;
    this._listBoxFontSize.SelectedIndexChanged += new EventHandler(this._listBoxFontSize_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._editFontSize, "_editFontSize");
    this._editFontSize.HideSelection = false;
    this._editFontSize.Name = "_editFontSize";
    this._editFontSize.TextChanged += new EventHandler(this._editFontSize_TextChanged);
    this._editFontSize.Leave += new EventHandler(this._editFontSize_Leave);
    componentResourceManager.ApplyResources((object) this._listBoxFontStyle, "_listBoxFontStyle");
    this._listBoxFontStyle.FormattingEnabled = true;
    this._listBoxFontStyle.Items.AddRange(new object[4]
    {
      (object) componentResourceManager.GetString("_listBoxFontStyle.Items"),
      (object) componentResourceManager.GetString("_listBoxFontStyle.Items1"),
      (object) componentResourceManager.GetString("_listBoxFontStyle.Items2"),
      (object) componentResourceManager.GetString("_listBoxFontStyle.Items3")
    });
    this._listBoxFontStyle.Name = "_listBoxFontStyle";
    this._listBoxFontStyle.TabStop = false;
    this._listBoxFontStyle.SelectedIndexChanged += new EventHandler(this._listBoxFontStyle_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._editFontStyle, "_editFontStyle");
    this._editFontStyle.HideSelection = false;
    this._editFontStyle.Name = "_editFontStyle";
    this._editFontStyle.TextChanged += new EventHandler(this._editFontStyle_TextChanged);
    componentResourceManager.ApplyResources((object) this._listBoxFontFamily, "_listBoxFontFamily");
    this._listBoxFontFamily.FormattingEnabled = true;
    this._listBoxFontFamily.Items.AddRange(new object[6]
    {
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items"),
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items1"),
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items2"),
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items3"),
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items4"),
      (object) componentResourceManager.GetString("_listBoxFontFamily.Items5")
    });
    this._listBoxFontFamily.Name = "_listBoxFontFamily";
    this._listBoxFontFamily.TabStop = false;
    this._listBoxFontFamily.SelectedIndexChanged += new EventHandler(this._listBoxFontFamily_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._editFontFamily, "_editFontFamily");
    this._editFontFamily.HideSelection = false;
    this._editFontFamily.Name = "_editFontFamily";
    this._editFontFamily.TextChanged += new EventHandler(this._editFontFamily_TextChanged);
    this._tabInterval.BackColor = SystemColors.Control;
    this._tabInterval.Controls.Add((Control) this._labelOnInterval);
    this._tabInterval.Controls.Add((Control) this._labelMovement);
    this._tabInterval.Controls.Add((Control) this._labelInterval);
    this._tabInterval.Controls.Add((Control) this._labelZoom);
    this._tabInterval.Controls.Add((Control) this._labelOnMovement);
    this._tabInterval.Controls.Add((Control) this._comboBoxZoom);
    this._tabInterval.Controls.Add((Control) this._spinEditMovement);
    this._tabInterval.Controls.Add((Control) this._spinEditInterval);
    this._tabInterval.Controls.Add((Control) this._comboBoxMovement);
    this._tabInterval.Controls.Add((Control) this._comboBoxInterval);
    this._tabInterval.ForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this._tabInterval, "_tabInterval");
    this._tabInterval.Name = "_tabInterval";
    componentResourceManager.ApplyResources((object) this._labelOnInterval, "_labelOnInterval");
    this._labelOnInterval.BackColor = SystemColors.Control;
    this._labelOnInterval.FlatStyle = FlatStyle.System;
    this._labelOnInterval.ForeColor = SystemColors.GrayText;
    this._labelOnInterval.Name = "_labelOnInterval";
    componentResourceManager.ApplyResources((object) this._labelMovement, "_labelMovement");
    this._labelMovement.BackColor = SystemColors.Control;
    this._labelMovement.FlatStyle = FlatStyle.System;
    this._labelMovement.ForeColor = SystemColors.GrayText;
    this._labelMovement.Name = "_labelMovement";
    componentResourceManager.ApplyResources((object) this._labelInterval, "_labelInterval");
    this._labelInterval.BackColor = SystemColors.Control;
    this._labelInterval.FlatStyle = FlatStyle.System;
    this._labelInterval.ForeColor = SystemColors.GrayText;
    this._labelInterval.Name = "_labelInterval";
    componentResourceManager.ApplyResources((object) this._labelZoom, "_labelZoom");
    this._labelZoom.BackColor = SystemColors.Control;
    this._labelZoom.FlatStyle = FlatStyle.System;
    this._labelZoom.ForeColor = SystemColors.GrayText;
    this._labelZoom.Name = "_labelZoom";
    this._labelOnMovement.AccessibleRole = AccessibleRole.None;
    componentResourceManager.ApplyResources((object) this._labelOnMovement, "_labelOnMovement");
    this._labelOnMovement.BackColor = SystemColors.Control;
    this._labelOnMovement.FlatStyle = FlatStyle.System;
    this._labelOnMovement.ForeColor = SystemColors.GrayText;
    this._labelOnMovement.Name = "_labelOnMovement";
    componentResourceManager.ApplyResources((object) this._comboBoxZoom, "_comboBoxZoom");
    this._comboBoxZoom.FormattingEnabled = true;
    this._comboBoxZoom.Items.AddRange(new object[8]
    {
      (object) componentResourceManager.GetString("_comboBoxZoom.Items"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items1"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items2"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items3"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items4"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items5"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items6"),
      (object) componentResourceManager.GetString("_comboBoxZoom.Items7")
    });
    this._comboBoxZoom.Name = "_comboBoxZoom";
    componentResourceManager.ApplyResources((object) this._spinEditMovement, "_spinEditMovement");
    this._spinEditMovement.Name = "_spinEditMovement";
    this._spinEditMovement.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditMovement.Properties.Enabled = false;
    this._spinEditMovement.Properties.ReadOnly = true;
    this._spinEditMovement.Properties.UseCtrlIncrement = false;
    this._spinEditMovement.Properties.ValidateOnEnterKey = true;
    componentResourceManager.ApplyResources((object) this._spinEditInterval, "_spinEditInterval");
    this._spinEditInterval.Name = "_spinEditInterval";
    this._spinEditInterval.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditInterval.Properties.Enabled = false;
    this._spinEditInterval.Properties.ReadOnly = true;
    this._spinEditInterval.Properties.UseCtrlIncrement = false;
    this._spinEditInterval.Properties.ValidateOnEnterKey = true;
    this._comboBoxMovement.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxMovement, "_comboBoxMovement");
    this._comboBoxMovement.FormattingEnabled = true;
    this._comboBoxMovement.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxMovement.Items"),
      (object) componentResourceManager.GetString("_comboBoxMovement.Items1"),
      (object) componentResourceManager.GetString("_comboBoxMovement.Items2")
    });
    this._comboBoxMovement.Name = "_comboBoxMovement";
    this._comboBoxInterval.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxInterval, "_comboBoxInterval");
    this._comboBoxInterval.FormattingEnabled = true;
    this._comboBoxInterval.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxInterval.Items"),
      (object) componentResourceManager.GetString("_comboBoxInterval.Items1"),
      (object) componentResourceManager.GetString("_comboBoxInterval.Items2")
    });
    this._comboBoxInterval.Name = "_comboBoxInterval";
    componentResourceManager.ApplyResources((object) this._panelSampleBorder, "_panelSampleBorder");
    this._panelSampleBorder.BorderStyle = BorderStyle.FixedSingle;
    this._panelSampleBorder.Controls.Add((Control) this._ternSample);
    this._panelSampleBorder.Name = "_panelSampleBorder";
    componentResourceManager.ApplyResources((object) this._ternSample, "_ternSample");
    this._ternSample.Cursor = Cursors.Default;
    this._ternSample.Name = "_ternSample";
    this._ternSample.ReadOnlyMode = false;
    this._ternSample.RtfText = componentResourceManager.GetString("_ternSample.RtfText");
    this._ternSample.TotalLines = 1;
    componentResourceManager.ApplyResources((object) this._labelSample, "_labelSample");
    this._labelSample.BackColor = SystemColors.Control;
    this._labelSample.FlatStyle = FlatStyle.System;
    this._labelSample.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelSample.Name = "_labelSample";
    componentResourceManager.ApplyResources((object) this._labelFontType, "_labelFontType");
    this._labelFontType.BackColor = SystemColors.Control;
    this._labelFontType.FlatStyle = FlatStyle.System;
    this._labelFontType.ForeColor = Color.Gray;
    this._labelFontType.Name = "_labelFontType";
    componentResourceManager.ApplyResources((object) this._bevelSample, "_bevelSample");
    this._bevelSample.BackColor = SystemColors.Control;
    this._bevelSample.Name = "_bevelSample";
    componentResourceManager.ApplyResources((object) this._checkBoxDoubleStrikout, "_checkBoxDoubleStrikout");
    this._checkBoxDoubleStrikout.BackColor = SystemColors.Control;
    this._checkBoxDoubleStrikout.Name = "_checkBoxDoubleStrikout";
    this._checkBoxDoubleStrikout.UseVisualStyleBackColor = false;
    this._checkBoxDoubleStrikout.CheckedChanged += new EventHandler(this._checkBoxDoubleStrikeout_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._panelSampleBorder);
    this.Controls.Add((Control) this._bevelSample);
    this.Controls.Add((Control) this._labelSample);
    this.Controls.Add((Control) this._labelFontType);
    this.Controls.Add((Control) this._tabSelector);
    this.Controls.Add((Control) this._btnDefault);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FontSetupDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this._tabSelector.ResumeLayout(false);
    this._tabFont.ResumeLayout(false);
    this._tabFont.PerformLayout();
    this._tabInterval.ResumeLayout(false);
    this._tabInterval.PerformLayout();
    this._spinEditMovement.Properties.EndInit();
    this._spinEditInterval.Properties.EndInit();
    this._panelSampleBorder.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Стиль шрифта </summary>
  [Flags]
  private enum InnerFontStyleEnum
  {
    Normal = 0,
    Italic = 1,
    Bold = 2,
    BoldItalic = 4,
  }

  /// <summary> Стиль шрифта </summary>
  private class InnerFontStyleClass
  {
    private FontSetupDlg.InnerFontStyleEnum _fontStyleEnum;

    /// <summary> Конструктор </summary>
    /// <param name="innerFontStyleEnum"></param>
    public InnerFontStyleClass(FontSetupDlg.InnerFontStyleEnum innerFontStyleEnum)
    {
      this._fontStyleEnum = innerFontStyleEnum;
    }

    /// <summary> Конструктор </summary>
    /// <param name="boldItalic"></param>
    public InnerFontStyleClass(BoldItalicStyle boldItalic)
    {
      if ((boldItalic & BoldItalicStyle.Italic) != BoldItalicStyle.Regular)
      {
        if ((boldItalic & BoldItalicStyle.Bold) != BoldItalicStyle.Regular)
          this._fontStyleEnum = FontSetupDlg.InnerFontStyleEnum.BoldItalic;
        else
          this._fontStyleEnum = FontSetupDlg.InnerFontStyleEnum.Italic;
      }
      else if ((boldItalic & BoldItalicStyle.Bold) != BoldItalicStyle.Regular)
        this._fontStyleEnum = FontSetupDlg.InnerFontStyleEnum.Bold;
      else
        this._fontStyleEnum = FontSetupDlg.InnerFontStyleEnum.Normal;
    }

    /// <summary> Стиль шрифта </summary>
    public FontSetupDlg.InnerFontStyleEnum BoldItalicStyle
    {
      [DebuggerStepThrough] get => this._fontStyleEnum;
      set => this._fontStyleEnum = value;
    }

    /// <summary> Преобразование в строку </summary>
    /// <returns></returns>
    public override string ToString()
    {
      switch (this._fontStyleEnum)
      {
        case FontSetupDlg.InnerFontStyleEnum.Normal:
          return LocalizationHolder.rm.GetString("Document.Model_56");
        case FontSetupDlg.InnerFontStyleEnum.Italic:
          return LocalizationHolder.rm.GetString("Document.Model_57");
        case FontSetupDlg.InnerFontStyleEnum.Bold:
          return LocalizationHolder.rm.GetString("Document.Model_58");
        case FontSetupDlg.InnerFontStyleEnum.BoldItalic:
          return LocalizationHolder.rm.GetString("Document.Model_59");
        default:
          return string.Empty;
      }
    }

    /// <summary> Получение стиля в виде набора флагов используемых в ImRtfEditor-е </summary>
    /// <returns> Набор флагов используемых в ImRtfEditor-е </returns>
    public int ToTernFontStyles()
    {
      switch (this._fontStyleEnum)
      {
        case FontSetupDlg.InnerFontStyleEnum.Normal:
          return 0;
        case FontSetupDlg.InnerFontStyleEnum.Italic:
          return 4;
        case FontSetupDlg.InnerFontStyleEnum.Bold:
          return 2;
        case FontSetupDlg.InnerFontStyleEnum.BoldItalic:
          return 6;
        default:
          return 0;
      }
    }

    /// <summary> Оператор преобразования в набор флагов типа BoldItalicStyle </summary>
    /// <param name="innerFontStyleClass"></param>
    /// <returns></returns>
    public static implicit operator BoldItalicStyle(
      FontSetupDlg.InnerFontStyleClass innerFontStyleClass)
    {
      if (innerFontStyleClass == null)
        return BoldItalicStyle.Regular;
      switch (innerFontStyleClass._fontStyleEnum)
      {
        case FontSetupDlg.InnerFontStyleEnum.Normal:
          return BoldItalicStyle.Regular;
        case FontSetupDlg.InnerFontStyleEnum.Italic:
          return BoldItalicStyle.Italic;
        case FontSetupDlg.InnerFontStyleEnum.Bold:
          return BoldItalicStyle.Bold;
        case FontSetupDlg.InnerFontStyleEnum.BoldItalic:
          return BoldItalicStyle.BoldItalic;
        default:
          return BoldItalicStyle.Regular;
      }
    }
  }
}
