// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.SetupParagraphDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraEditors.Controls;
using Intermech.Controls;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary> Диалог настройки форматирования абзаца </summary>
public class SetupParagraphDlg : Form
{
  private ParagraphFormat _paragraphFormat;
  private ParagraphFormat _oldParagraphFormat;
  private LineSpacingMethod lastindex;
  private float charSize = 12f;
  private bool? isCell;
  private double lastMmValue = 4.0;
  private double lastPtValue = 12.0;
  private double lastRatioValue = 3.0;
  private int _sampleTextStartSymbolNumber;
  private int _sampleTextFinishSymbolNumber;
  private bool _controlsAreUpdating;
  private int _applyTextFormatingCounter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnCancel;
  private Button _btnOK;
  private TabControlAdvanced _tabSelector;
  private TabPage _tabFont;
  private TabPage _tabInterval;
  private Button _btnTabulation;
  private Bevel _bevelOptions;
  private Label _labelCommon;
  private Bevel _bevelInterval;
  private Label _labelInterval;
  private Bevel _bevelIdent;
  private Label _labelIdent;
  private Bevel _bevelSpacingOnPages;
  private Label _labelSpacingOnPages;
  private Bevel bevel1;
  private Panel _panelSampleBorder;
  private ImRtfEditor _ternSample;
  private Bevel _bevelSample;
  private Label _labelSample;
  private Label _labelAlign;
  private ComboBox _comboBoxAlign;
  private ComboBox _comboBoxLevel;
  private Label _labelLevel;
  private Label _labelIdentRight;
  private Label _labelIdentLeft;
  private MeasureSpinEdit _spinEditIdentFirstLine;
  private Label _labelIdentFirstLine;
  private ComboBox _comboBoxIdentType;
  private Label _labelFirstLineType;
  private MeasureSpinEdit _spinEditRightIdent;
  private MeasureSpinEdit _spinEditLeftIdent;
  private MeasureSpinEdit _spinEditLinesInterval;
  private Label _labelLinesInterval;
  private ComboBox _comboBoxLinesIntervalType;
  private Label _labelLinesIntervalType;
  private MeasureSpinEdit _spinEditIntervalAfter;
  private MeasureSpinEdit _spinEditIntervalBefore;
  private Label _labelIntervalAfter;
  private Label _labelIntervalBefore;
  private CheckBox _checkBoxDisableAutoWords;
  private CheckBox _checkBoxFromNewPage;
  private CheckBox _checkBoxNoSplitWithNext;
  private CheckBox _checkBoxNotSplitParagraph;
  private CheckBox _checkBoxDisableFloatLines;
  private ComboBox _comboBoxVertAlign;
  private Label _labelVertAlign;

  /// <summary> Конструктор </summary>
  /// <param name="paragraphFormat">Объект paragraphFormat</param>
  /// <param name="fontSize">размер шрифта</param>
  /// <param name="isCell">редактируем всю ячейку или отдельный абзац</param>
  public SetupParagraphDlg(ParagraphFormat paragraphFormat, float? fontSize, bool isCell)
    : this(paragraphFormat)
  {
    this.charSize = !fontSize.HasValue ? 12f : fontSize.Value;
    this.isCell = new bool?(isCell);
    if (!isCell)
    {
      this._labelLevel.Visible = true;
      this._comboBoxLevel.Visible = true;
      this._labelVertAlign.Visible = false;
      this._comboBoxVertAlign.Visible = false;
      this._labelAlign.Text = LocalizationHolder.rm.GetString("Document.Model_99");
      this._labelCommon.Text = LocalizationHolder.rm.GetString("Document.Model_100");
    }
    else
    {
      this._labelLevel.Visible = false;
      this._comboBoxLevel.Visible = false;
      this._labelAlign.Text = LocalizationHolder.rm.GetString("Document.Model_101");
      this._labelCommon.Text = LocalizationHolder.rm.GetString("Document.Model_102");
      this._labelVertAlign.Visible = true;
      this._comboBoxVertAlign.Visible = true;
    }
  }

  /// <summary> Конструктор </summary>
  public SetupParagraphDlg(ParagraphFormat paragraphFormat)
  {
    this.InitializeComponent();
    this._spinEditIdentFirstLine.EditValue = (object) null;
    this._spinEditLinesInterval.EditValue = (object) null;
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
    this._ternSample.SetTerParaFmt(1024 /*0x0400*/, true, false);
    this._ternSample.TerSetFlags3(true, 128 /*0x80*/);
    this._labelAlign.Text = LocalizationHolder.rm.GetString("Document.Model_103");
    this._labelCommon.Text = LocalizationHolder.rm.GetString("Document.Model_104");
    this._ternSample.TerDeleteAll(false);
    this._oldParagraphFormat = paragraphFormat;
    this._paragraphFormat = this._oldParagraphFormat.Clone();
    this.InitParams();
    this._ternSample.SetTerDefaultFont("Arial", -(int) Math.Round(100.0), 0, Color.Silver, false);
    string str1 = LocalizationHolder.rm.GetString("Document.Model_105") + " ";
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < 11; ++index)
      stringBuilder.Append(str1);
    stringBuilder.Append("\n");
    this._sampleTextStartSymbolNumber = stringBuilder.Length + 1;
    string str2 = LocalizationHolder.rm.GetString("Document.Model_106") + " ";
    for (int index = 0; index < 9; ++index)
      stringBuilder.Append(str2);
    this._sampleTextFinishSymbolNumber = stringBuilder.Length - 1;
    stringBuilder.Append("\n");
    string str3 = LocalizationHolder.rm.GetString("Document.Model_107") + " ";
    for (int index = 0; index < 11; ++index)
      stringBuilder.Append(str3);
    this._ternSample.InsertTerText(stringBuilder.ToString(), false);
    this._ternSample.SelectTerText(this._sampleTextStartSymbolNumber - 1, -1, this._sampleTextFinishSymbolNumber + 2, -1, false);
    this._ternSample.SetTerColor(Color.Black, false);
    this._ternSample.SelectAll(false);
    this._ternSample.TerSetParaIndent(340, 737, -1, false);
    this._ternSample.DeselectTerText(false);
    this.ApplyFormatingToSampleText();
    this._ternSample.SetTerCursorPos(0, 0, true);
  }

  public ParagraphFormat ParagraphFormat => this._paragraphFormat;

  private void InitParams()
  {
    this.lastindex = LineSpacingMethod.InPercents;
    this._controlsAreUpdating = true;
    try
    {
      if (!this._paragraphFormat.HorzAlignment.HasValue)
      {
        this._comboBoxAlign.SelectedIndex = -1;
      }
      else
      {
        HorzAlignment? horzAlignment = this._paragraphFormat.HorzAlignment;
        if (horzAlignment.HasValue)
        {
          switch (horzAlignment.GetValueOrDefault())
          {
            case HorzAlignment.Left:
              this._comboBoxAlign.SelectedIndex = 0;
              break;
            case HorzAlignment.Center:
              this._comboBoxAlign.SelectedIndex = 1;
              break;
            case HorzAlignment.Right:
              this._comboBoxAlign.SelectedIndex = 2;
              break;
            case HorzAlignment.Justify:
              this._comboBoxAlign.SelectedIndex = 3;
              break;
          }
        }
      }
      if (!this._paragraphFormat.VertAlignment.HasValue)
      {
        this._comboBoxVertAlign.SelectedIndex = -1;
      }
      else
      {
        VertAlignment? vertAlignment = this._paragraphFormat.VertAlignment;
        if (vertAlignment.HasValue)
        {
          switch (vertAlignment.GetValueOrDefault())
          {
            case VertAlignment.Top:
              this._comboBoxVertAlign.SelectedIndex = 0;
              break;
            case VertAlignment.Center:
              this._comboBoxVertAlign.SelectedIndex = 1;
              break;
            case VertAlignment.Bottom:
              this._comboBoxVertAlign.SelectedIndex = 2;
              break;
          }
        }
      }
      if (!this._paragraphFormat.TextLevel.HasValue)
        this._comboBoxLevel.SelectedIndex = -1;
      else
        this._comboBoxLevel.SelectedIndex = this._paragraphFormat.TextLevel.Value;
      this._spinEditLeftIdent.BaseMeasureDescriptor = this._spinEditLeftIdent.MeasureDescriptors[0];
      float? nullable1 = this._paragraphFormat.IdentLeft;
      if (!nullable1.HasValue)
      {
        this._spinEditLeftIdent.Text = "";
      }
      else
      {
        MeasureSpinEdit spinEditLeftIdent = this._spinEditLeftIdent;
        nullable1 = this._paragraphFormat.IdentLeft;
        double num = Math.Round((double) nullable1.Value, 2);
        spinEditLeftIdent.LastValue = num;
      }
      this._spinEditRightIdent.BaseMeasureDescriptor = this._spinEditRightIdent.MeasureDescriptors[0];
      nullable1 = this._paragraphFormat.IdentRight;
      if (!nullable1.HasValue)
      {
        this._spinEditRightIdent.Text = "";
      }
      else
      {
        MeasureSpinEdit spinEditRightIdent = this._spinEditRightIdent;
        nullable1 = this._paragraphFormat.IdentRight;
        double num = Math.Round((double) nullable1.Value, 2);
        spinEditRightIdent.LastValue = num;
      }
      this._spinEditIdentFirstLine.BaseMeasureDescriptor = this._spinEditIdentFirstLine.MeasureDescriptors[0];
      nullable1 = this._paragraphFormat.IdentFirstLine;
      if (!nullable1.HasValue)
      {
        this._comboBoxIdentType.SelectedIndex = -1;
        this._spinEditIdentFirstLine.Text = "";
      }
      else
      {
        nullable1 = this._paragraphFormat.IdentFirstLine;
        float num1 = 0.0f;
        if ((double) nullable1.GetValueOrDefault() == (double) num1 & nullable1.HasValue)
        {
          this._comboBoxIdentType.SelectedIndex = 0;
          this._spinEditIdentFirstLine.Text = "";
        }
        else
        {
          nullable1 = this._paragraphFormat.IdentFirstLine;
          float num2 = 0.0f;
          if ((double) nullable1.GetValueOrDefault() > (double) num2 & nullable1.HasValue)
          {
            this._comboBoxIdentType.SelectedIndex = 1;
            nullable1 = this._paragraphFormat.IdentFirstLine;
            if (!nullable1.HasValue)
            {
              this._spinEditIdentFirstLine.Text = "";
            }
            else
            {
              MeasureSpinEdit editIdentFirstLine = this._spinEditIdentFirstLine;
              nullable1 = this._paragraphFormat.IdentFirstLine;
              double num3 = Math.Round((double) nullable1.Value, 2);
              editIdentFirstLine.LastValue = num3;
            }
          }
          else
          {
            this._comboBoxIdentType.SelectedIndex = 2;
            nullable1 = this._paragraphFormat.IdentFirstLine;
            if (!nullable1.HasValue)
            {
              this._spinEditIdentFirstLine.Text = "";
            }
            else
            {
              MeasureSpinEdit editIdentFirstLine = this._spinEditIdentFirstLine;
              nullable1 = this._paragraphFormat.IdentFirstLine;
              double num4 = Math.Round(0.0 - (double) nullable1.Value, 2);
              editIdentFirstLine.LastValue = num4;
            }
          }
        }
      }
      this._spinEditIntervalBefore.BaseMeasureDescriptor = this._spinEditIntervalBefore.MeasureDescriptors[0];
      nullable1 = this._paragraphFormat.IntervalBefore;
      if (!nullable1.HasValue)
      {
        this._spinEditIntervalBefore.Text = "";
      }
      else
      {
        nullable1 = this._paragraphFormat.IntervalBefore;
        this._spinEditIntervalBefore.LastValue = 0.25 * (double) (int) Math.Round((double) nullable1.Value / 0.25);
      }
      this._spinEditIntervalAfter.BaseMeasureDescriptor = this._spinEditIntervalAfter.MeasureDescriptors[0];
      nullable1 = this._paragraphFormat.IntervalAfter;
      if (!nullable1.HasValue)
      {
        this._spinEditIntervalAfter.Text = "";
      }
      else
      {
        nullable1 = this._paragraphFormat.IntervalAfter;
        this._spinEditIntervalAfter.LastValue = 0.25 * (double) (int) Math.Round((double) nullable1.Value / 0.25);
      }
      if (!this._paragraphFormat.LineSpacingMethod.HasValue)
      {
        this._comboBoxLinesIntervalType.SelectedIndex = -1;
        this._spinEditLinesInterval.Text = "";
      }
      else
      {
        switch (this._paragraphFormat.LineSpacingMethod.Value)
        {
          case LineSpacingMethod.Ratio_1:
            this._comboBoxLinesIntervalType.SelectedIndex = 0;
            this._spinEditLinesInterval.LastValue = 1.0;
            this._spinEditLinesInterval.Text = "";
            break;
          case LineSpacingMethod.Ratio_1_5:
            this._comboBoxLinesIntervalType.SelectedIndex = 1;
            this._spinEditLinesInterval.LastValue = 1.5;
            this._spinEditLinesInterval.Text = "";
            break;
          case LineSpacingMethod.Ratio_2:
            this._comboBoxLinesIntervalType.SelectedIndex = 2;
            this._spinEditLinesInterval.LastValue = 2.0;
            this._spinEditLinesInterval.Text = "";
            break;
          case LineSpacingMethod.AtLeast:
            this._comboBoxLinesIntervalType.SelectedIndex = 3;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            if (!nullable1.HasValue)
            {
              this._spinEditLinesInterval.Text = "";
              break;
            }
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            this._spinEditLinesInterval.LastValue = 0.25 * (double) (int) Math.Round((double) nullable1.Value / 0.25);
            break;
          case LineSpacingMethod.AtLeastMM:
            this._comboBoxLinesIntervalType.SelectedIndex = 4;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            if (!nullable1.HasValue)
            {
              this._spinEditLinesInterval.Text = "";
              break;
            }
            MeasureSpinEdit editLinesInterval1 = this._spinEditLinesInterval;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            double num5 = Math.Round((double) nullable1.Value, 2);
            editLinesInterval1.LastValue = num5;
            break;
          case LineSpacingMethod.Exact:
            this._comboBoxLinesIntervalType.SelectedIndex = 5;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            if (!nullable1.HasValue)
            {
              this._spinEditLinesInterval.Text = "";
              break;
            }
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            this._spinEditLinesInterval.LastValue = 0.25 * (double) (int) Math.Round((double) nullable1.Value / 0.25);
            break;
          case LineSpacingMethod.ExactMM:
            this._comboBoxLinesIntervalType.SelectedIndex = 6;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            if (!nullable1.HasValue)
            {
              this._spinEditLinesInterval.Text = "";
              break;
            }
            MeasureSpinEdit editLinesInterval2 = this._spinEditLinesInterval;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            double num6 = Math.Round((double) nullable1.Value, 2);
            editLinesInterval2.LastValue = num6;
            break;
          case LineSpacingMethod.Ratio:
            this._comboBoxLinesIntervalType.SelectedIndex = 7;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            if (!nullable1.HasValue)
            {
              this._spinEditLinesInterval.Text = "";
              break;
            }
            MeasureSpinEdit editLinesInterval3 = this._spinEditLinesInterval;
            nullable1 = this._paragraphFormat.SpaceBetweenLines;
            double num7 = Math.Round((double) nullable1.Value, 2);
            editLinesInterval3.LastValue = num7;
            break;
        }
      }
      bool? nullable2;
      if (!this._paragraphFormat.DisableFloatLines.HasValue)
      {
        this._checkBoxDisableFloatLines.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckBox disableFloatLines = this._checkBoxDisableFloatLines;
        nullable2 = this._paragraphFormat.DisableFloatLines;
        int num8 = nullable2.Value ? 1 : 0;
        disableFloatLines.Checked = num8 != 0;
      }
      nullable2 = this._paragraphFormat.KeepTogether;
      if (!nullable2.HasValue)
      {
        this._checkBoxNotSplitParagraph.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckBox notSplitParagraph = this._checkBoxNotSplitParagraph;
        nullable2 = this._paragraphFormat.KeepTogether;
        int num9 = nullable2.Value ? 1 : 0;
        notSplitParagraph.Checked = num9 != 0;
      }
      nullable2 = this._paragraphFormat.KeepWithNext;
      if (!nullable2.HasValue)
      {
        this._checkBoxNoSplitWithNext.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckBox boxNoSplitWithNext = this._checkBoxNoSplitWithNext;
        nullable2 = this._paragraphFormat.KeepWithNext;
        int num10 = nullable2.Value ? 1 : 0;
        boxNoSplitWithNext.Checked = num10 != 0;
      }
      nullable2 = this._paragraphFormat.FromNewPage;
      if (!nullable2.HasValue)
      {
        this._checkBoxFromNewPage.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckBox checkBoxFromNewPage = this._checkBoxFromNewPage;
        nullable2 = this._paragraphFormat.FromNewPage;
        int num11 = nullable2.Value ? 1 : 0;
        checkBoxFromNewPage.Checked = num11 != 0;
      }
      nullable2 = this._paragraphFormat.DisableWordWrap;
      if (!nullable2.HasValue)
      {
        this._checkBoxDisableAutoWords.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckBox disableAutoWords = this._checkBoxDisableAutoWords;
        nullable2 = this._paragraphFormat.DisableWordWrap;
        int num12 = nullable2.Value ? 1 : 0;
        disableAutoWords.Checked = num12 != 0;
      }
      this._spinEditLinesInterval.BaseMeasureDescriptor = (MeasureDescriptor) null;
    }
    finally
    {
      this._controlsAreUpdating = false;
    }
  }

  private void BeginApplyTextFormating()
  {
    if (this._applyTextFormatingCounter == 0)
      this._ternSample.SelectTerText(this._sampleTextStartSymbolNumber, -1, this._sampleTextFinishSymbolNumber, -1, false);
    ++this._applyTextFormatingCounter;
  }

  private void FinishApplyTextFormating() => this.FinishApplyTextFormating(true);

  private void FinishApplyTextFormating(bool repaint)
  {
    if (this._applyTextFormatingCounter <= 0)
      return;
    --this._applyTextFormatingCounter;
    if (this._applyTextFormatingCounter != 0)
      return;
    this._ternSample.DeselectTerText(repaint);
  }

  private void ApplyFormatingToSampleText()
  {
    this.BeginApplyTextFormating();
    try
    {
      if (this._paragraphFormat.HorzAlignment.HasValue)
        this.ApplyHorizontalAligment();
      if (this._paragraphFormat.TextLevel.HasValue)
        this._ternSample.TerSetParaList(false, -1, 0, this._paragraphFormat.TextLevel.Value, false);
      this.ApplyTextIdent();
      this.ApplyTextSpacing();
      bool? nullable = this._paragraphFormat.DisableFloatLines;
      if (nullable.HasValue)
      {
        ImRtfEditor ternSample = this._ternSample;
        nullable = this._paragraphFormat.DisableFloatLines;
        int num = nullable.Value ? 1 : 0;
        ternSample.TerSetPflags(32 /*0x20*/, num != 0, false);
      }
      nullable = this._paragraphFormat.DisableWordWrap;
      if (nullable.HasValue)
      {
        ImRtfEditor ternSample = this._ternSample;
        nullable = this._paragraphFormat.DisableWordWrap;
        int num = nullable.Value ? 1 : 0;
        ternSample.TerSetPflags(16 /*0x10*/, num != 0, false);
      }
      nullable = this._paragraphFormat.FromNewPage;
      if (nullable.HasValue)
      {
        ImRtfEditor ternSample = this._ternSample;
        nullable = this._paragraphFormat.FromNewPage;
        int num = nullable.Value ? 1 : 0;
        ternSample.TerSetPflags(64 /*0x40*/, num != 0, false);
      }
      nullable = this._paragraphFormat.KeepTogether;
      if (nullable.HasValue)
      {
        ImRtfEditor ternSample = this._ternSample;
        nullable = this._paragraphFormat.KeepTogether;
        int num = nullable.Value ? 1 : 0;
        ternSample.SetTerParaFmt(16384 /*0x4000*/, num != 0, false);
      }
      nullable = this._paragraphFormat.KeepWithNext;
      if (!nullable.HasValue)
        return;
      ImRtfEditor ternSample1 = this._ternSample;
      nullable = this._paragraphFormat.KeepWithNext;
      int num1 = nullable.Value ? 1 : 0;
      ternSample1.SetTerParaFmt(32768 /*0x8000*/, num1 != 0, false);
    }
    finally
    {
      this.FinishApplyTextFormating();
    }
  }

  private void ApplyHorizontalAligment()
  {
    this.BeginApplyTextFormating();
    int FmtType = 0;
    HorzAlignment? horzAlignment = this._paragraphFormat.HorzAlignment;
    if (horzAlignment.HasValue)
    {
      switch (horzAlignment.GetValueOrDefault())
      {
        case HorzAlignment.Left:
          FmtType = 1024 /*0x0400*/;
          break;
        case HorzAlignment.Center:
          FmtType = 1;
          break;
        case HorzAlignment.Right:
          FmtType = 2;
          break;
        case HorzAlignment.Justify:
          FmtType = 2048 /*0x0800*/;
          break;
      }
    }
    this._ternSample.SetTerParaFmt(FmtType, true, false);
    this.FinishApplyTextFormating();
  }

  private void ApplyTextIdent()
  {
    this.BeginApplyTextFormating();
    int left = this._paragraphFormat.IdentLeft.HasValue ? (int) Math.Round((double) this._paragraphFormat.IdentLeft.Value * 1440.0 / 2.54) + 340 : -1;
    float? nullable = this._paragraphFormat.IdentRight;
    int num1;
    if (!nullable.HasValue)
    {
      num1 = -1;
    }
    else
    {
      nullable = this._paragraphFormat.IdentRight;
      num1 = (int) Math.Round((double) nullable.Value * 1440.0 / 2.54) + 737;
    }
    int right = num1;
    nullable = this._paragraphFormat.IdentFirstLine;
    int num2;
    if (!nullable.HasValue)
    {
      num2 = -1;
    }
    else
    {
      nullable = this._paragraphFormat.IdentFirstLine;
      num2 = (int) Math.Round((double) nullable.Value * 1440.0 / 2.54);
    }
    int first = num2;
    if (left != -1 || right != -1 || first != -1)
      this._ternSample.TerSetParaIndent(left, right, first, false);
    this.FinishApplyTextFormating();
  }

  private void ApplyTextSpacing()
  {
    this.BeginApplyTextFormating();
    int SpaceBefore = this._paragraphFormat.IntervalBefore.HasValue ? (int) Math.Round((double) this._paragraphFormat.IntervalBefore.Value * 20.0) : -1;
    float? nullable = this._paragraphFormat.IntervalAfter;
    int num;
    if (!nullable.HasValue)
    {
      num = -1;
    }
    else
    {
      nullable = this._paragraphFormat.IntervalAfter;
      num = (int) Math.Round((double) nullable.Value * 20.0);
    }
    int SpaceAfter = num;
    int SpaceBetween = 0;
    int LineSpacing = 0;
    LineSpacingMethod? lineSpacingMethod = this._paragraphFormat.LineSpacingMethod;
    if (lineSpacingMethod.HasValue)
    {
      lineSpacingMethod = this._paragraphFormat.LineSpacingMethod;
      if (lineSpacingMethod.HasValue)
      {
        switch (lineSpacingMethod.GetValueOrDefault())
        {
          case LineSpacingMethod.InPercents:
            nullable = this._paragraphFormat.SpaceBetweenLines;
            LineSpacing = (int) Math.Round((double) nullable.Value) - 100;
            break;
          case LineSpacingMethod.Ratio_1:
            LineSpacing = 0;
            break;
          case LineSpacingMethod.Ratio_1_5:
            LineSpacing = 50;
            break;
          case LineSpacingMethod.Ratio_2:
            LineSpacing = 100;
            break;
          case LineSpacingMethod.AtLeast:
            nullable = this._paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = this._paragraphFormat.SpaceBetweenLines;
              SpaceBetween = (int) Math.Round((double) nullable.Value * 20.0);
              break;
            }
            break;
          case LineSpacingMethod.Exact:
            nullable = this._paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = this._paragraphFormat.SpaceBetweenLines;
              SpaceBetween = -(int) Math.Round((double) nullable.Value * 20.0);
              break;
            }
            break;
          case LineSpacingMethod.ExactMM:
            nullable = this._paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = this._paragraphFormat.SpaceBetweenLines;
              SpaceBetween = -(int) Math.Truncate((double) nullable.Value * 56.692913055419922);
              break;
            }
            break;
          case LineSpacingMethod.Ratio:
            nullable = this._paragraphFormat.SpaceBetweenLines;
            if (nullable.HasValue)
            {
              nullable = this._paragraphFormat.SpaceBetweenLines;
              LineSpacing = (int) ((double) nullable.Value * 100.0 - 100.0);
              break;
            }
            break;
        }
      }
    }
    if (SpaceBefore != -1 || SpaceAfter != -1 || SpaceBetween != -9999 || LineSpacing != 0)
      this._ternSample.TerSetParaSpacing2(SpaceBefore, SpaceAfter, SpaceBetween, LineSpacing, false);
    this.FinishApplyTextFormating();
  }

  private void SetupParagraphDlg_Load(object sender, EventArgs e)
  {
    this._tabSelector.ReinitAmpersant();
  }

  private MeasureDescriptor[] _spinEditLeftIdent_OnGetMeasureDescriptors()
  {
    return new MeasureDescriptor[8]
    {
      new MeasureDescriptor()
      {
        IsDefault = true,
        K = 1.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_108"),
        MeasureID = 1L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_109"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_110")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1.0,
        LongName = "Centimeters",
        MeasureID = 2L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "cm",
        ShortNameIndex = new string[1]{ "CM" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 0.1,
        LongName = LocalizationHolder.rm.GetString("Document.Model_111"),
        MeasureID = 3L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_112"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_113")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 0.1,
        LongName = "Millimenters",
        MeasureID = 4L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "mm",
        ShortNameIndex = new string[1]{ "MM" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 100.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_114"),
        MeasureID = 5L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_115"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_116")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 100.0,
        LongName = "Meters",
        MeasureID = 6L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "m",
        ShortNameIndex = new string[1]{ "M" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 0.035277777777777776,
        LongName = LocalizationHolder.rm.GetString("Document.Model_117"),
        MeasureID = 7L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_118"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_119")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 0.035277777777777776,
        LongName = "Points",
        MeasureID = 8L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "pt",
        ShortNameIndex = new string[1]{ "PT" }
      }
    };
  }

  private MeasureDescriptor[] _spinEditIntervalBefore_OnGetMeasureDescriptors()
  {
    return new MeasureDescriptor[8]
    {
      new MeasureDescriptor()
      {
        IsDefault = true,
        K = 1.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_120"),
        MeasureID = 1L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_121"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_122")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1.0,
        LongName = "Points",
        MeasureID = 2L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "pt",
        ShortNameIndex = new string[1]{ "PT" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 360.0 / (double) sbyte.MaxValue,
        LongName = LocalizationHolder.rm.GetString("Document.Model_123"),
        MeasureID = 3L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_124"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_125")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 360.0 / (double) sbyte.MaxValue,
        LongName = "Millimenters",
        MeasureID = 4L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "mm",
        ShortNameIndex = new string[1]{ "MM" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 283.46456692913387,
        LongName = LocalizationHolder.rm.GetString("Document.Model_126"),
        MeasureID = 5L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_127"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_128")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 283.46456692913387,
        LongName = "Meters",
        MeasureID = 6L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "m",
        ShortNameIndex = new string[1]{ "M" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 3600.0 / (double) sbyte.MaxValue,
        LongName = LocalizationHolder.rm.GetString("Document.Model_129"),
        MeasureID = 8L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_130"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_131")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 3600.0 / (double) sbyte.MaxValue,
        LongName = "Centimeters",
        MeasureID = 9L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "cm",
        ShortNameIndex = new string[1]{ "CM" }
      }
    };
  }

  private void _spinEditIdentFirstLine_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    if (this._spinEditIdentFirstLine.LastValue < 0.0)
    {
      this._spinEditIdentFirstLine.Text = LocalizationHolder.rm.GetString("Document.Model_132");
    }
    else
    {
      if ((int) this._spinEditIdentFirstLine.LastValue == 0)
        this._comboBoxIdentType.SelectedIndex = 0;
      else
        this._comboBoxIdentType.SelectedIndex = 1;
      double num1 = 0.0;
      switch (this._comboBoxIdentType.SelectedIndex)
      {
        case 0:
          num1 = 0.0;
          break;
        case 1:
          num1 = this._spinEditIdentFirstLine.LastValue;
          break;
        case 2:
          num1 = 0.0 - this._spinEditIdentFirstLine.LastValue;
          break;
      }
      float? identFirstLine = this._paragraphFormat.IdentFirstLine;
      double? nullable = identFirstLine.HasValue ? new double?((double) identFirstLine.GetValueOrDefault()) : new double?();
      double num2 = num1;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        return;
      this._paragraphFormat.IdentFirstLine = new float?((float) num1);
      this.ApplyTextIdent();
    }
  }

  private void _comboBoxIdentType_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    if (this._comboBoxIdentType.SelectedIndex != 0)
      this._spinEditIdentFirstLine.LastValue = 1.0;
    if (this._comboBoxIdentType.SelectedIndex == 0)
    {
      this._spinEditIdentFirstLine.LastValue = 0.0;
      this._spinEditIdentFirstLine.Text = "0";
    }
    else if ((int) this._spinEditIdentFirstLine.LastValue == 0)
      this._spinEditIdentFirstLine.Text = LocalizationHolder.rm.GetString("Document.Model_133");
    if (this._comboBoxIdentType.SelectedIndex == -1)
      return;
    double num1 = 0.0;
    switch (this._comboBoxIdentType.SelectedIndex)
    {
      case 0:
        num1 = 0.0;
        break;
      case 1:
        num1 = this._spinEditIdentFirstLine.LastValue;
        break;
      case 2:
        num1 = 0.0 - this._spinEditIdentFirstLine.LastValue;
        break;
    }
    float? identFirstLine = this._paragraphFormat.IdentFirstLine;
    double? nullable = identFirstLine.HasValue ? new double?((double) identFirstLine.GetValueOrDefault()) : new double?();
    double num2 = num1;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    this._paragraphFormat.IdentFirstLine = new float?((float) num1);
    this.ApplyTextIdent();
  }

  private void _spinEditIdentFirstLine_BeforeDecrement(object sender, CancelEventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null || !(sender is MeasureSpinEdit measureSpinEdit) || (int) measureSpinEdit.LastValue != 0)
      return;
    e.Cancel = true;
  }

  private void _spinEditLinesInterval_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    if (this._spinEditLinesInterval.Text.Trim() != string.Empty && (this._comboBoxLinesIntervalType.SelectedIndex < 3 || this._comboBoxLinesIntervalType.SelectedIndex == 7))
    {
      switch ((int) (this._spinEditLinesInterval.Value * 2M))
      {
        case 2:
          this._comboBoxLinesIntervalType.SelectedIndex = 0;
          break;
        case 3:
          this._comboBoxLinesIntervalType.SelectedIndex = 1;
          break;
        case 4:
          this._comboBoxLinesIntervalType.SelectedIndex = 2;
          break;
        default:
          this._comboBoxLinesIntervalType.SelectedIndex = 7;
          break;
      }
      if (this._comboBoxLinesIntervalType.SelectedIndex < 3 && this._comboBoxLinesIntervalType.SelectedIndex > -1)
        this._spinEditLinesInterval.Text = string.Empty;
    }
    if (this._comboBoxLinesIntervalType.SelectedIndex == -1)
      return;
    this.ApplyTextSpacing();
  }

  private void _comboBoxLinesIntervalType_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null || this._comboBoxLinesIntervalType.SelectedIndex == -1)
      return;
    switch (this._comboBoxLinesIntervalType.SelectedIndex)
    {
      case 0:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1);
        break;
      case 1:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_1_5);
        break;
      case 2:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio_2);
        break;
      case 3:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeast);
        break;
      case 4:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.AtLeastMM);
        break;
      case 5:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Exact);
        break;
      case 6:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.ExactMM);
        break;
      case 7:
        this._paragraphFormat.LineSpacingMethod = new LineSpacingMethod?(LineSpacingMethod.Ratio);
        break;
    }
    if (this._paragraphFormat.LineSpacingMethod.HasValue)
    {
      LineSpacingMethod? lineSpacingMethod = this._paragraphFormat.LineSpacingMethod;
      if (lineSpacingMethod.HasValue)
      {
        switch (lineSpacingMethod.GetValueOrDefault())
        {
          case LineSpacingMethod.Ratio_1:
            this._spinEditLinesInterval.BaseMeasureDescriptor = (MeasureDescriptor) null;
            this._spinEditLinesInterval.AssignLastValue(1.0, true);
            break;
          case LineSpacingMethod.Ratio_1_5:
            this._spinEditLinesInterval.BaseMeasureDescriptor = (MeasureDescriptor) null;
            this._spinEditLinesInterval.AssignLastValue(1.5, true);
            break;
          case LineSpacingMethod.Ratio_2:
            this._spinEditLinesInterval.BaseMeasureDescriptor = (MeasureDescriptor) null;
            this._spinEditLinesInterval.AssignLastValue(2.0, true);
            break;
          case LineSpacingMethod.AtLeast:
            this._spinEditLinesInterval.BaseMeasureDescriptor = this._spinEditLinesInterval.MeasureDescriptors[0];
            this._spinEditLinesInterval.LastValue = this.lastindex == LineSpacingMethod.AtLeastMM || this.lastindex == LineSpacingMethod.ExactMM ? (double) UnitsConverter.MmToPoints((float) this._spinEditLinesInterval.LastValue) : this.lastPtValue;
            break;
          case LineSpacingMethod.AtLeastMM:
            this._spinEditLinesInterval.BaseMeasureDescriptor = this._spinEditLinesInterval.MeasureDescriptors[2];
            this._spinEditLinesInterval.LastValue = this.lastindex == LineSpacingMethod.AtLeast || this.lastindex == LineSpacingMethod.Exact ? Math.Round((double) UnitsConverter.PointToMm((float) this._spinEditLinesInterval.LastValue), 2) : this.lastMmValue;
            break;
          case LineSpacingMethod.Exact:
            this._spinEditLinesInterval.BaseMeasureDescriptor = this._spinEditLinesInterval.MeasureDescriptors[0];
            this._spinEditLinesInterval.LastValue = this.lastindex == LineSpacingMethod.AtLeastMM || this.lastindex == LineSpacingMethod.ExactMM ? (double) UnitsConverter.MmToPoints((float) this._spinEditLinesInterval.LastValue) : this.lastPtValue;
            break;
          case LineSpacingMethod.ExactMM:
            this._spinEditLinesInterval.BaseMeasureDescriptor = this._spinEditLinesInterval.MeasureDescriptors[2];
            this._spinEditLinesInterval.LastValue = this.lastindex == LineSpacingMethod.AtLeast || this.lastindex == LineSpacingMethod.Exact ? Math.Round((double) UnitsConverter.PointToMm((float) this._spinEditLinesInterval.LastValue), 2) : this.lastMmValue;
            break;
          case LineSpacingMethod.Ratio:
            switch ((int) this.lastRatioValue * 2)
            {
              case 2:
                this._comboBoxLinesIntervalType.SelectedIndex = 0;
                break;
              case 3:
                this._comboBoxLinesIntervalType.SelectedIndex = 1;
                break;
              case 4:
                this._comboBoxLinesIntervalType.SelectedIndex = 2;
                break;
            }
            this._spinEditLinesInterval.BaseMeasureDescriptor = (MeasureDescriptor) null;
            this._spinEditLinesInterval.AssignLastValue(this.lastRatioValue, true);
            break;
        }
      }
      this.lastindex = this._paragraphFormat.LineSpacingMethod.Value;
    }
    if (this._comboBoxLinesIntervalType.SelectedIndex < 3 && this._comboBoxLinesIntervalType.SelectedIndex > -1)
      this._spinEditLinesInterval.Text = string.Empty;
    this.ApplyTextSpacing();
  }

  private void _comboBoxAlign_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    HorzAlignment? horzAlignment1;
    switch (this._comboBoxAlign.SelectedIndex)
    {
      case -1:
        if (!this._paragraphFormat.HorzAlignment.HasValue)
          return;
        this._paragraphFormat.HorzAlignment = new HorzAlignment?();
        break;
      case 0:
        horzAlignment1 = this._paragraphFormat.HorzAlignment;
        HorzAlignment horzAlignment2 = HorzAlignment.Left;
        if (horzAlignment1.GetValueOrDefault() == horzAlignment2 & horzAlignment1.HasValue)
          return;
        this._paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Left);
        break;
      case 1:
        horzAlignment1 = this._paragraphFormat.HorzAlignment;
        HorzAlignment horzAlignment3 = HorzAlignment.Center;
        if (horzAlignment1.GetValueOrDefault() == horzAlignment3 & horzAlignment1.HasValue)
          return;
        this._paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Center);
        break;
      case 2:
        horzAlignment1 = this._paragraphFormat.HorzAlignment;
        HorzAlignment horzAlignment4 = HorzAlignment.Right;
        if (horzAlignment1.GetValueOrDefault() == horzAlignment4 & horzAlignment1.HasValue)
          return;
        this._paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Right);
        break;
      case 3:
        horzAlignment1 = this._paragraphFormat.HorzAlignment;
        HorzAlignment horzAlignment5 = HorzAlignment.Justify;
        if (horzAlignment1.GetValueOrDefault() == horzAlignment5 & horzAlignment1.HasValue)
          return;
        this._paragraphFormat.HorzAlignment = new HorzAlignment?(HorzAlignment.Justify);
        break;
    }
    horzAlignment1 = this._paragraphFormat.HorzAlignment;
    if (!horzAlignment1.HasValue)
      return;
    this.ApplyHorizontalAligment();
  }

  private void _comboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    int? textLevel1 = this._paragraphFormat.TextLevel;
    int selectedIndex = this._comboBoxLevel.SelectedIndex;
    if (textLevel1.GetValueOrDefault() == selectedIndex & textLevel1.HasValue)
      return;
    this._paragraphFormat.TextLevel = this._comboBoxLevel.SelectedIndex == -1 ? new int?() : new int?(this._comboBoxLevel.SelectedIndex);
    int? textLevel2 = this._paragraphFormat.TextLevel;
    if (!textLevel2.HasValue)
      return;
    ImRtfEditor ternSample = this._ternSample;
    textLevel2 = this._paragraphFormat.TextLevel;
    int level = textLevel2.Value;
    ternSample.TerSetParaList(false, -1, 0, level, false);
  }

  private void _spinEditLeftIdent_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    float? identLeft = this._paragraphFormat.IdentLeft;
    double? nullable = identLeft.HasValue ? new double?((double) identLeft.GetValueOrDefault()) : new double?();
    double lastValue = this._spinEditLeftIdent.LastValue;
    if (nullable.GetValueOrDefault() == lastValue & nullable.HasValue)
      return;
    this._paragraphFormat.IdentLeft = new float?((float) this._spinEditLeftIdent.LastValue);
    if (!this._paragraphFormat.IdentLeft.HasValue)
      return;
    this.ApplyTextIdent();
  }

  private void _spinEditRightIdent_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    float? identRight = this._paragraphFormat.IdentRight;
    double? nullable = identRight.HasValue ? new double?((double) identRight.GetValueOrDefault()) : new double?();
    double lastValue = this._spinEditRightIdent.LastValue;
    if (nullable.GetValueOrDefault() == lastValue & nullable.HasValue)
      return;
    this._paragraphFormat.IdentRight = new float?((float) this._spinEditRightIdent.LastValue);
    if (!this._paragraphFormat.IdentRight.HasValue)
      return;
    this.ApplyTextIdent();
  }

  private void _spinEditIdentFirstLine_EditValueChanged(object sender, EventArgs e)
  {
  }

  private void _spinEditIntervalBefore_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    float? intervalBefore = this._paragraphFormat.IntervalBefore;
    double? nullable = intervalBefore.HasValue ? new double?((double) intervalBefore.GetValueOrDefault()) : new double?();
    double lastValue = this._spinEditIntervalBefore.LastValue;
    if (nullable.GetValueOrDefault() == lastValue & nullable.HasValue)
      return;
    this._paragraphFormat.IntervalBefore = new float?((float) this._spinEditIntervalBefore.LastValue);
    if (!this._paragraphFormat.IntervalBefore.HasValue)
      return;
    this.ApplyTextSpacing();
  }

  private void _spinEditIntervalAfter_TextChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    float? intervalAfter = this._paragraphFormat.IntervalAfter;
    double? nullable = intervalAfter.HasValue ? new double?((double) intervalAfter.GetValueOrDefault()) : new double?();
    double lastValue = this._spinEditIntervalAfter.LastValue;
    if (nullable.GetValueOrDefault() == lastValue & nullable.HasValue)
      return;
    this._paragraphFormat.IntervalAfter = new float?((float) this._spinEditIntervalAfter.LastValue);
    if (!this._paragraphFormat.IntervalAfter.HasValue)
      return;
    this.ApplyTextSpacing();
  }

  private void _checkBoxDisableFloatLines_CheckedChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    this._paragraphFormat.DisableFloatLines = this._checkBoxDisableFloatLines.CheckState == CheckState.Indeterminate ? new bool?() : new bool?(this._checkBoxDisableFloatLines.CheckState == CheckState.Checked);
  }

  private void _checkBoxNotSplitParagraph_CheckedChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    this._paragraphFormat.KeepTogether = this._checkBoxNotSplitParagraph.CheckState == CheckState.Indeterminate ? new bool?() : new bool?(this._checkBoxNotSplitParagraph.CheckState == CheckState.Checked);
  }

  private void _checkBoxNoSplitWithNext_CheckedChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    this._paragraphFormat.KeepWithNext = this._checkBoxNoSplitWithNext.CheckState == CheckState.Indeterminate ? new bool?() : new bool?(this._checkBoxNoSplitWithNext.CheckState == CheckState.Checked);
  }

  private void _checkBoxFromNewPage_CheckedChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    this._paragraphFormat.FromNewPage = this._checkBoxFromNewPage.CheckState == CheckState.Indeterminate ? new bool?() : new bool?(this._checkBoxFromNewPage.CheckState == CheckState.Checked);
  }

  private void _checkBoxDisableAutoWords_CheckedChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    this._paragraphFormat.DisableWordWrap = this._checkBoxDisableAutoWords.CheckState == CheckState.Indeterminate ? new bool?() : new bool?(this._checkBoxDisableAutoWords.CheckState == CheckState.Checked);
  }

  private MeasureDescriptor[] _spinEditLinesInterval_OnGetMeasureDescriptors()
  {
    return new MeasureDescriptor[8]
    {
      new MeasureDescriptor()
      {
        IsDefault = true,
        K = 1.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_134"),
        MeasureID = 1L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_135"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_136")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1.0,
        LongName = "Points",
        MeasureID = 2L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 1L,
        ShortName = "pt",
        ShortNameIndex = new string[1]{ "PT" }
      },
      new MeasureDescriptor()
      {
        IsDefault = true,
        K = 1.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_137"),
        MeasureID = 3L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_138"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_139")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1.0,
        LongName = "Millimenters",
        MeasureID = 4L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "mm",
        ShortNameIndex = new string[1]{ "MM" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1000.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_140"),
        MeasureID = 5L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_141"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_142")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 1000.0,
        LongName = "Meters",
        MeasureID = 6L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "m",
        ShortNameIndex = new string[1]{ "M" }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 10.0,
        LongName = LocalizationHolder.rm.GetString("Document.Model_143"),
        MeasureID = 8L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = LocalizationHolder.rm.GetString("Document.Model_144"),
        ShortNameIndex = new string[1]
        {
          LocalizationHolder.rm.GetString("Document.Model_145")
        }
      },
      new MeasureDescriptor()
      {
        IsDefault = false,
        K = 10.0,
        LongName = "Centimeters",
        MeasureID = 9L,
        OperationsList = (string[]) null,
        PhysicalQuantityID = 2L,
        ShortName = "cm",
        ShortNameIndex = new string[1]{ "CM" }
      }
    };
  }

  private void _spinEditLinesInterval_BeforeChange(object sender, CancelEventArgs e)
  {
    if (!(this._spinEditLinesInterval.Text == "") || this._comboBoxLinesIntervalType.SelectedIndex >= 3)
      return;
    this._spinEditLinesInterval.AssignLastValue(3.0, true);
    e.Cancel = true;
  }

  private void _comboBoxVertAlign_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._controlsAreUpdating || this._paragraphFormat == null)
      return;
    switch (this._comboBoxVertAlign.SelectedIndex)
    {
      case -1:
        if (!this._paragraphFormat.HorzAlignment.HasValue)
          break;
        this._paragraphFormat.VertAlignment = new VertAlignment?();
        break;
      case 0:
        VertAlignment? vertAlignment1 = this._paragraphFormat.VertAlignment;
        VertAlignment vertAlignment2 = VertAlignment.Top;
        if (vertAlignment1.GetValueOrDefault() == vertAlignment2 & vertAlignment1.HasValue)
          break;
        this._paragraphFormat.VertAlignment = new VertAlignment?(VertAlignment.Top);
        break;
      case 1:
        VertAlignment? vertAlignment3 = this._paragraphFormat.VertAlignment;
        VertAlignment vertAlignment4 = VertAlignment.Center;
        if (vertAlignment3.GetValueOrDefault() == vertAlignment4 & vertAlignment3.HasValue)
          break;
        this._paragraphFormat.VertAlignment = new VertAlignment?(VertAlignment.Center);
        break;
      case 2:
        VertAlignment? vertAlignment5 = this._paragraphFormat.VertAlignment;
        VertAlignment vertAlignment6 = VertAlignment.Bottom;
        if (vertAlignment5.GetValueOrDefault() == vertAlignment6 & vertAlignment5.HasValue)
          break;
        this._paragraphFormat.VertAlignment = new VertAlignment?(VertAlignment.Bottom);
        break;
    }
  }

  private void _spinEditLinesInterval_Leave(object sender, EventArgs e)
  {
    if (this._comboBoxLinesIntervalType.SelectedIndex >= 0 && this._comboBoxLinesIntervalType.SelectedIndex <= 7)
    {
      float? intervalAfter = this._paragraphFormat.IntervalAfter;
      double? nullable = intervalAfter.HasValue ? new double?((double) intervalAfter.GetValueOrDefault()) : new double?();
      double lastValue = this._spinEditIntervalAfter.LastValue;
      if (!(nullable.GetValueOrDefault() == lastValue & nullable.HasValue))
        this._paragraphFormat.IntervalAfter = new float?((float) this._spinEditIntervalAfter.LastValue);
    }
    this._paragraphFormat.SpaceBetweenLines = new float?((float) this._spinEditLinesInterval.LastValue);
    if (this._comboBoxLinesIntervalType.SelectedIndex == 3 || this._comboBoxLinesIntervalType.SelectedIndex == 5)
      this.lastPtValue = this._spinEditLinesInterval.LastValue;
    if (this._comboBoxLinesIntervalType.SelectedIndex == 4 || this._comboBoxLinesIntervalType.SelectedIndex == 6)
      this.lastMmValue = this._spinEditLinesInterval.LastValue;
    if (this._comboBoxLinesIntervalType.SelectedIndex == 7)
      this.lastRatioValue = this._spinEditLinesInterval.LastValue;
    if (this._spinEditLinesInterval.LastMeasureDescriptor != null)
    {
      if (this._spinEditLinesInterval.LastMeasureDescriptor.PhysicalQuantityID == 1L)
      {
        if (this._comboBoxLinesIntervalType.SelectedIndex == 4)
          this._comboBoxLinesIntervalType.SelectedIndex = 3;
        if (this._comboBoxLinesIntervalType.SelectedIndex == 6)
          this._comboBoxLinesIntervalType.SelectedIndex = 5;
      }
      if (this._spinEditLinesInterval.LastMeasureDescriptor.PhysicalQuantityID == 2L)
      {
        if (this._comboBoxLinesIntervalType.SelectedIndex == 3)
          this._comboBoxLinesIntervalType.SelectedIndex = 4;
        if (this._comboBoxLinesIntervalType.SelectedIndex == 5)
          this._comboBoxLinesIntervalType.SelectedIndex = 6;
      }
    }
    this.ApplyTextSpacing();
  }

  private void _btnOK_Click(object sender, EventArgs e)
  {
    float? nullable1;
    if (this._paragraphFormat.IdentLeft.HasValue)
    {
      ParagraphFormat paragraphFormat = this._paragraphFormat;
      nullable1 = this._paragraphFormat.IdentLeft;
      float? nullable2 = new float?((float) Math.Round((double) nullable1.Value, 2));
      paragraphFormat.IdentLeft = nullable2;
    }
    nullable1 = this._paragraphFormat.IdentRight;
    if (nullable1.HasValue)
    {
      ParagraphFormat paragraphFormat = this._paragraphFormat;
      nullable1 = this._paragraphFormat.IdentRight;
      float? nullable3 = new float?((float) Math.Round((double) nullable1.Value, 2));
      paragraphFormat.IdentRight = nullable3;
    }
    nullable1 = this._paragraphFormat.IdentFirstLine;
    if (nullable1.HasValue)
    {
      ParagraphFormat paragraphFormat = this._paragraphFormat;
      nullable1 = this._paragraphFormat.IdentFirstLine;
      float? nullable4 = new float?((float) Math.Round((double) nullable1.Value, 2));
      paragraphFormat.IdentFirstLine = nullable4;
    }
    nullable1 = this._paragraphFormat.IntervalBefore;
    if (nullable1.HasValue)
    {
      nullable1 = this._paragraphFormat.IntervalBefore;
      this._paragraphFormat.IntervalBefore = new float?(0.25f * (float) (int) Math.Round((double) nullable1.Value / 0.25));
    }
    nullable1 = this._paragraphFormat.IntervalAfter;
    if (nullable1.HasValue)
    {
      nullable1 = this._paragraphFormat.IntervalAfter;
      this._paragraphFormat.IntervalAfter = new float?(0.25f * (float) (int) Math.Round((double) nullable1.Value / 0.25));
    }
    nullable1 = this._paragraphFormat.SpaceBetweenLines;
    if (!nullable1.HasValue)
      return;
    LineSpacingMethod? lineSpacingMethod = this._paragraphFormat.LineSpacingMethod;
    if (!lineSpacingMethod.HasValue)
      return;
    switch (lineSpacingMethod.GetValueOrDefault())
    {
      case LineSpacingMethod.AtLeast:
      case LineSpacingMethod.Exact:
        nullable1 = this._paragraphFormat.SpaceBetweenLines;
        this._paragraphFormat.SpaceBetweenLines = new float?(0.25f * (float) (int) Math.Round((double) nullable1.Value / 0.25));
        break;
      case LineSpacingMethod.AtLeastMM:
      case LineSpacingMethod.ExactMM:
      case LineSpacingMethod.Ratio:
        ParagraphFormat paragraphFormat1 = this._paragraphFormat;
        nullable1 = this._paragraphFormat.SpaceBetweenLines;
        float? nullable5 = new float?((float) Math.Round((double) nullable1.Value, 2));
        paragraphFormat1.SpaceBetweenLines = nullable5;
        break;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetupParagraphDlg));
    this._btnCancel = new Button();
    this._btnOK = new Button();
    this._btnTabulation = new Button();
    this._panelSampleBorder = new Panel();
    this._ternSample = new ImRtfEditor();
    this._labelSample = new Label();
    this._bevelSample = new Bevel();
    this._tabSelector = new TabControlAdvanced();
    this._tabFont = new TabPage();
    this._spinEditLinesInterval = new MeasureSpinEdit();
    this._labelLinesInterval = new Label();
    this._comboBoxLinesIntervalType = new ComboBox();
    this._labelLinesIntervalType = new Label();
    this._spinEditIntervalAfter = new MeasureSpinEdit();
    this._spinEditIntervalBefore = new MeasureSpinEdit();
    this._labelIntervalAfter = new Label();
    this._labelIntervalBefore = new Label();
    this._spinEditIdentFirstLine = new MeasureSpinEdit();
    this._labelIdentFirstLine = new Label();
    this._comboBoxIdentType = new ComboBox();
    this._labelFirstLineType = new Label();
    this._spinEditRightIdent = new MeasureSpinEdit();
    this._spinEditLeftIdent = new MeasureSpinEdit();
    this._labelIdentRight = new Label();
    this._labelIdentLeft = new Label();
    this._comboBoxLevel = new ComboBox();
    this._labelLevel = new Label();
    this._comboBoxVertAlign = new ComboBox();
    this._comboBoxAlign = new ComboBox();
    this._labelVertAlign = new Label();
    this._labelAlign = new Label();
    this._bevelInterval = new Bevel();
    this._labelInterval = new Label();
    this._bevelIdent = new Bevel();
    this._labelIdent = new Label();
    this._bevelOptions = new Bevel();
    this._labelCommon = new Label();
    this._tabInterval = new TabPage();
    this._checkBoxDisableAutoWords = new CheckBox();
    this._checkBoxFromNewPage = new CheckBox();
    this._checkBoxNoSplitWithNext = new CheckBox();
    this._checkBoxNotSplitParagraph = new CheckBox();
    this._checkBoxDisableFloatLines = new CheckBox();
    this.bevel1 = new Bevel();
    this._bevelSpacingOnPages = new Bevel();
    this._labelSpacingOnPages = new Label();
    this._panelSampleBorder.SuspendLayout();
    this._tabSelector.SuspendLayout();
    this._tabFont.SuspendLayout();
    this._spinEditLinesInterval.Properties.BeginInit();
    this._spinEditIntervalAfter.Properties.BeginInit();
    this._spinEditIntervalBefore.Properties.BeginInit();
    this._spinEditIdentFirstLine.Properties.BeginInit();
    this._spinEditRightIdent.Properties.BeginInit();
    this._spinEditLeftIdent.Properties.BeginInit();
    this._tabInterval.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.Click += new EventHandler(this._btnOK_Click);
    componentResourceManager.ApplyResources((object) this._btnTabulation, "_btnTabulation");
    this._btnTabulation.Name = "_btnTabulation";
    componentResourceManager.ApplyResources((object) this._panelSampleBorder, "_panelSampleBorder");
    this._panelSampleBorder.BorderStyle = BorderStyle.FixedSingle;
    this._panelSampleBorder.Controls.Add((Control) this._ternSample);
    this._panelSampleBorder.Name = "_panelSampleBorder";
    this._ternSample.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._ternSample, "_ternSample");
    this._ternSample.BorderMargin = false;
    this._ternSample.Command = 0;
    this._ternSample.Cursor = Cursors.WaitCursor;
    this._ternSample.Data = "";
    this._ternSample.DictPath = "";
    this._ternSample.DoClassCleanup = false;
    this._ternSample.FittedView = false;
    this._ternSample.HorzScrollBar = false;
    this._ternSample.HtmlAddOnKey = "";
    this._ternSample.InServer = false;
    this._ternSample.InWebPage = false;
    this._ternSample.Name = "_ternSample";
    this._ternSample.PageMode = true;
    this._ternSample.ReadOnlyMode = true;
    this._ternSample.RTFOutput = true;
    this._ternSample.RtfText = componentResourceManager.GetString("_ternSample.RtfText");
    this._ternSample.ShowRuler = false;
    this._ternSample.ShowStatusBar = false;
    this._ternSample.ShowToolBar = false;
    this._ternSample.SpellTimeKey = "";
    this._ternSample.TabStop = false;
    this._ternSample.TernKey = "";
    this._ternSample.UseWindow = true;
    this._ternSample.VertScrollBar = false;
    this._ternSample.WordWrap = true;
    componentResourceManager.ApplyResources((object) this._labelSample, "_labelSample");
    this._labelSample.BackColor = SystemColors.Control;
    this._labelSample.FlatStyle = FlatStyle.System;
    this._labelSample.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelSample.Name = "_labelSample";
    componentResourceManager.ApplyResources((object) this._bevelSample, "_bevelSample");
    this._bevelSample.BackColor = Color.Transparent;
    this._bevelSample.Name = "_bevelSample";
    componentResourceManager.ApplyResources((object) this._tabSelector, "_tabSelector");
    this._tabSelector.Controls.Add((Control) this._tabFont);
    this._tabSelector.Controls.Add((Control) this._tabInterval);
    this._tabSelector.Name = "_tabSelector";
    this._tabSelector.SelectedIndex = 0;
    this._tabFont.BackColor = SystemColors.Control;
    this._tabFont.Controls.Add((Control) this._spinEditLinesInterval);
    this._tabFont.Controls.Add((Control) this._labelLinesInterval);
    this._tabFont.Controls.Add((Control) this._comboBoxLinesIntervalType);
    this._tabFont.Controls.Add((Control) this._labelLinesIntervalType);
    this._tabFont.Controls.Add((Control) this._spinEditIntervalAfter);
    this._tabFont.Controls.Add((Control) this._spinEditIntervalBefore);
    this._tabFont.Controls.Add((Control) this._labelIntervalAfter);
    this._tabFont.Controls.Add((Control) this._labelIntervalBefore);
    this._tabFont.Controls.Add((Control) this._spinEditIdentFirstLine);
    this._tabFont.Controls.Add((Control) this._labelIdentFirstLine);
    this._tabFont.Controls.Add((Control) this._comboBoxIdentType);
    this._tabFont.Controls.Add((Control) this._labelFirstLineType);
    this._tabFont.Controls.Add((Control) this._spinEditRightIdent);
    this._tabFont.Controls.Add((Control) this._spinEditLeftIdent);
    this._tabFont.Controls.Add((Control) this._labelIdentRight);
    this._tabFont.Controls.Add((Control) this._labelIdentLeft);
    this._tabFont.Controls.Add((Control) this._comboBoxLevel);
    this._tabFont.Controls.Add((Control) this._labelLevel);
    this._tabFont.Controls.Add((Control) this._comboBoxVertAlign);
    this._tabFont.Controls.Add((Control) this._comboBoxAlign);
    this._tabFont.Controls.Add((Control) this._labelVertAlign);
    this._tabFont.Controls.Add((Control) this._labelAlign);
    this._tabFont.Controls.Add((Control) this._bevelInterval);
    this._tabFont.Controls.Add((Control) this._labelInterval);
    this._tabFont.Controls.Add((Control) this._bevelIdent);
    this._tabFont.Controls.Add((Control) this._labelIdent);
    this._tabFont.Controls.Add((Control) this._bevelOptions);
    this._tabFont.Controls.Add((Control) this._labelCommon);
    componentResourceManager.ApplyResources((object) this._tabFont, "_tabFont");
    this._tabFont.Name = "_tabFont";
    componentResourceManager.ApplyResources((object) this._spinEditLinesInterval, "_spinEditLinesInterval");
    this._spinEditLinesInterval.LastValue = 0.5;
    this._spinEditLinesInterval.Name = "_spinEditLinesInterval";
    this._spinEditLinesInterval.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditLinesInterval.Properties.Increment = new Decimal(new int[4]
    {
      5,
      0,
      0,
      65536 /*0x010000*/
    });
    this._spinEditLinesInterval.Properties.MaxValue = new Decimal(new int[4]
    {
      9999,
      0,
      0,
      0
    });
    this._spinEditLinesInterval.Properties.MinValue = new Decimal(new int[4]
    {
      5,
      0,
      0,
      65536 /*0x010000*/
    });
    this._spinEditLinesInterval.Properties.UseCtrlIncrement = false;
    this._spinEditLinesInterval.Properties.ValidateOnEnterKey = true;
    this._spinEditLinesInterval.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditLinesInterval_OnGetMeasureDescriptors);
    this._spinEditLinesInterval.BeforeIncrement += new CancelEventHandler(this._spinEditLinesInterval_BeforeChange);
    this._spinEditLinesInterval.BeforeDecrement += new CancelEventHandler(this._spinEditLinesInterval_BeforeChange);
    this._spinEditLinesInterval.Leave += new EventHandler(this._spinEditLinesInterval_Leave);
    this._spinEditLinesInterval.TextChanged += new EventHandler(this._spinEditLinesInterval_TextChanged);
    componentResourceManager.ApplyResources((object) this._labelLinesInterval, "_labelLinesInterval");
    this._labelLinesInterval.BackColor = SystemColors.Control;
    this._labelLinesInterval.FlatStyle = FlatStyle.System;
    this._labelLinesInterval.Name = "_labelLinesInterval";
    this._comboBoxLinesIntervalType.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxLinesIntervalType, "_comboBoxLinesIntervalType");
    this._comboBoxLinesIntervalType.FormattingEnabled = true;
    this._comboBoxLinesIntervalType.Items.AddRange(new object[8]
    {
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items1"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items2"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items3"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items4"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items5"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items6"),
      (object) componentResourceManager.GetString("_comboBoxLinesIntervalType.Items7")
    });
    this._comboBoxLinesIntervalType.Name = "_comboBoxLinesIntervalType";
    this._comboBoxLinesIntervalType.SelectedIndexChanged += new EventHandler(this._comboBoxLinesIntervalType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._labelLinesIntervalType, "_labelLinesIntervalType");
    this._labelLinesIntervalType.BackColor = SystemColors.Control;
    this._labelLinesIntervalType.FlatStyle = FlatStyle.System;
    this._labelLinesIntervalType.Name = "_labelLinesIntervalType";
    componentResourceManager.ApplyResources((object) this._spinEditIntervalAfter, "_spinEditIntervalAfter");
    this._spinEditIntervalAfter.LastValue = 0.0;
    this._spinEditIntervalAfter.Name = "_spinEditIntervalAfter";
    this._spinEditIntervalAfter.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditIntervalAfter.Properties.Increment = new Decimal(new int[4]
    {
      6,
      0,
      0,
      0
    });
    this._spinEditIntervalAfter.Properties.IsFloatValue = false;
    this._spinEditIntervalAfter.Properties.UseCtrlIncrement = false;
    this._spinEditIntervalAfter.Properties.ValidateOnEnterKey = true;
    this._spinEditIntervalAfter.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditIntervalBefore_OnGetMeasureDescriptors);
    this._spinEditIntervalAfter.TextChanged += new EventHandler(this._spinEditIntervalAfter_TextChanged);
    componentResourceManager.ApplyResources((object) this._spinEditIntervalBefore, "_spinEditIntervalBefore");
    this._spinEditIntervalBefore.LastValue = 0.0;
    this._spinEditIntervalBefore.Name = "_spinEditIntervalBefore";
    this._spinEditIntervalBefore.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditIntervalBefore.Properties.Increment = new Decimal(new int[4]
    {
      6,
      0,
      0,
      0
    });
    this._spinEditIntervalBefore.Properties.IsFloatValue = false;
    this._spinEditIntervalBefore.Properties.UseCtrlIncrement = false;
    this._spinEditIntervalBefore.Properties.ValidateOnEnterKey = true;
    this._spinEditIntervalBefore.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditIntervalBefore_OnGetMeasureDescriptors);
    this._spinEditIntervalBefore.TextChanged += new EventHandler(this._spinEditIntervalBefore_TextChanged);
    componentResourceManager.ApplyResources((object) this._labelIntervalAfter, "_labelIntervalAfter");
    this._labelIntervalAfter.BackColor = SystemColors.Control;
    this._labelIntervalAfter.FlatStyle = FlatStyle.System;
    this._labelIntervalAfter.Name = "_labelIntervalAfter";
    componentResourceManager.ApplyResources((object) this._labelIntervalBefore, "_labelIntervalBefore");
    this._labelIntervalBefore.BackColor = SystemColors.Control;
    this._labelIntervalBefore.FlatStyle = FlatStyle.System;
    this._labelIntervalBefore.Name = "_labelIntervalBefore";
    componentResourceManager.ApplyResources((object) this._spinEditIdentFirstLine, "_spinEditIdentFirstLine");
    this._spinEditIdentFirstLine.EmptyValueAsNullText = true;
    this._spinEditIdentFirstLine.LastValue = 0.0;
    this._spinEditIdentFirstLine.Name = "_spinEditIdentFirstLine";
    this._spinEditIdentFirstLine.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditIdentFirstLine.Properties.Increment = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this._spinEditIdentFirstLine.Properties.MaxValue = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._spinEditIdentFirstLine.Properties.UseCtrlIncrement = false;
    this._spinEditIdentFirstLine.Properties.ValidateOnEnterKey = true;
    this._spinEditIdentFirstLine.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditLeftIdent_OnGetMeasureDescriptors);
    this._spinEditIdentFirstLine.EditValueChanged += new EventHandler(this._spinEditIdentFirstLine_EditValueChanged);
    this._spinEditIdentFirstLine.BeforeDecrement += new CancelEventHandler(this._spinEditIdentFirstLine_BeforeDecrement);
    this._spinEditIdentFirstLine.TextChanged += new EventHandler(this._spinEditIdentFirstLine_TextChanged);
    componentResourceManager.ApplyResources((object) this._labelIdentFirstLine, "_labelIdentFirstLine");
    this._labelIdentFirstLine.BackColor = SystemColors.Control;
    this._labelIdentFirstLine.FlatStyle = FlatStyle.System;
    this._labelIdentFirstLine.Name = "_labelIdentFirstLine";
    this._comboBoxIdentType.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxIdentType, "_comboBoxIdentType");
    this._comboBoxIdentType.FormattingEnabled = true;
    this._comboBoxIdentType.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxIdentType.Items"),
      (object) componentResourceManager.GetString("_comboBoxIdentType.Items1"),
      (object) componentResourceManager.GetString("_comboBoxIdentType.Items2")
    });
    this._comboBoxIdentType.Name = "_comboBoxIdentType";
    this._comboBoxIdentType.SelectedIndexChanged += new EventHandler(this._comboBoxIdentType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._labelFirstLineType, "_labelFirstLineType");
    this._labelFirstLineType.BackColor = SystemColors.Control;
    this._labelFirstLineType.FlatStyle = FlatStyle.System;
    this._labelFirstLineType.Name = "_labelFirstLineType";
    componentResourceManager.ApplyResources((object) this._spinEditRightIdent, "_spinEditRightIdent");
    this._spinEditRightIdent.LastValue = 0.0;
    this._spinEditRightIdent.Name = "_spinEditRightIdent";
    this._spinEditRightIdent.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditRightIdent.Properties.Increment = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this._spinEditRightIdent.Properties.UseCtrlIncrement = false;
    this._spinEditRightIdent.Properties.ValidateOnEnterKey = true;
    this._spinEditRightIdent.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditLeftIdent_OnGetMeasureDescriptors);
    this._spinEditRightIdent.TextChanged += new EventHandler(this._spinEditRightIdent_TextChanged);
    componentResourceManager.ApplyResources((object) this._spinEditLeftIdent, "_spinEditLeftIdent");
    this._spinEditLeftIdent.LastValue = 0.0;
    this._spinEditLeftIdent.Name = "_spinEditLeftIdent";
    this._spinEditLeftIdent.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this._spinEditLeftIdent.Properties.Increment = new Decimal(new int[4]
    {
      1,
      0,
      0,
      65536 /*0x010000*/
    });
    this._spinEditLeftIdent.Properties.NullText = componentResourceManager.GetString("_spinEditLeftIdent.Properties.NullText");
    this._spinEditLeftIdent.Properties.UseCtrlIncrement = false;
    this._spinEditLeftIdent.Properties.ValidateOnEnterKey = true;
    this._spinEditLeftIdent.OnGetMeasureDescriptors += new GetMeasureDescriptorsDelegate(this._spinEditLeftIdent_OnGetMeasureDescriptors);
    this._spinEditLeftIdent.TextChanged += new EventHandler(this._spinEditLeftIdent_TextChanged);
    componentResourceManager.ApplyResources((object) this._labelIdentRight, "_labelIdentRight");
    this._labelIdentRight.BackColor = SystemColors.Control;
    this._labelIdentRight.FlatStyle = FlatStyle.System;
    this._labelIdentRight.Name = "_labelIdentRight";
    componentResourceManager.ApplyResources((object) this._labelIdentLeft, "_labelIdentLeft");
    this._labelIdentLeft.BackColor = SystemColors.Control;
    this._labelIdentLeft.FlatStyle = FlatStyle.System;
    this._labelIdentLeft.Name = "_labelIdentLeft";
    this._comboBoxLevel.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxLevel, "_comboBoxLevel");
    this._comboBoxLevel.FormattingEnabled = true;
    this._comboBoxLevel.Items.AddRange(new object[10]
    {
      (object) componentResourceManager.GetString("_comboBoxLevel.Items"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items1"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items2"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items3"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items4"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items5"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items6"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items7"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items8"),
      (object) componentResourceManager.GetString("_comboBoxLevel.Items9")
    });
    this._comboBoxLevel.Name = "_comboBoxLevel";
    this._comboBoxLevel.SelectedIndexChanged += new EventHandler(this._comboBoxLevel_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._labelLevel, "_labelLevel");
    this._labelLevel.BackColor = SystemColors.Control;
    this._labelLevel.FlatStyle = FlatStyle.System;
    this._labelLevel.Name = "_labelLevel";
    this._comboBoxVertAlign.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxVertAlign, "_comboBoxVertAlign");
    this._comboBoxVertAlign.FormattingEnabled = true;
    this._comboBoxVertAlign.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("_comboBoxVertAlign.Items"),
      (object) componentResourceManager.GetString("_comboBoxVertAlign.Items1"),
      (object) componentResourceManager.GetString("_comboBoxVertAlign.Items2")
    });
    this._comboBoxVertAlign.Name = "_comboBoxVertAlign";
    this._comboBoxVertAlign.SelectedIndexChanged += new EventHandler(this._comboBoxVertAlign_SelectedIndexChanged);
    this._comboBoxAlign.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this._comboBoxAlign, "_comboBoxAlign");
    this._comboBoxAlign.FormattingEnabled = true;
    this._comboBoxAlign.Items.AddRange(new object[4]
    {
      (object) componentResourceManager.GetString("_comboBoxAlign.Items"),
      (object) componentResourceManager.GetString("_comboBoxAlign.Items1"),
      (object) componentResourceManager.GetString("_comboBoxAlign.Items2"),
      (object) componentResourceManager.GetString("_comboBoxAlign.Items3")
    });
    this._comboBoxAlign.Name = "_comboBoxAlign";
    this._comboBoxAlign.SelectedIndexChanged += new EventHandler(this._comboBoxAlign_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._labelVertAlign, "_labelVertAlign");
    this._labelVertAlign.BackColor = SystemColors.Control;
    this._labelVertAlign.FlatStyle = FlatStyle.System;
    this._labelVertAlign.Name = "_labelVertAlign";
    componentResourceManager.ApplyResources((object) this._labelAlign, "_labelAlign");
    this._labelAlign.BackColor = SystemColors.Control;
    this._labelAlign.FlatStyle = FlatStyle.System;
    this._labelAlign.Name = "_labelAlign";
    componentResourceManager.ApplyResources((object) this._bevelInterval, "_bevelInterval");
    this._bevelInterval.BackColor = Color.Transparent;
    this._bevelInterval.Name = "_bevelInterval";
    componentResourceManager.ApplyResources((object) this._labelInterval, "_labelInterval");
    this._labelInterval.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelInterval.Name = "_labelInterval";
    componentResourceManager.ApplyResources((object) this._bevelIdent, "_bevelIdent");
    this._bevelIdent.BackColor = Color.Transparent;
    this._bevelIdent.Name = "_bevelIdent";
    componentResourceManager.ApplyResources((object) this._labelIdent, "_labelIdent");
    this._labelIdent.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelIdent.Name = "_labelIdent";
    componentResourceManager.ApplyResources((object) this._bevelOptions, "_bevelOptions");
    this._bevelOptions.BackColor = Color.Transparent;
    this._bevelOptions.Name = "_bevelOptions";
    componentResourceManager.ApplyResources((object) this._labelCommon, "_labelCommon");
    this._labelCommon.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelCommon.Name = "_labelCommon";
    this._tabInterval.BackColor = SystemColors.Control;
    this._tabInterval.Controls.Add((Control) this._checkBoxDisableAutoWords);
    this._tabInterval.Controls.Add((Control) this._checkBoxFromNewPage);
    this._tabInterval.Controls.Add((Control) this._checkBoxNoSplitWithNext);
    this._tabInterval.Controls.Add((Control) this._checkBoxNotSplitParagraph);
    this._tabInterval.Controls.Add((Control) this._checkBoxDisableFloatLines);
    this._tabInterval.Controls.Add((Control) this.bevel1);
    this._tabInterval.Controls.Add((Control) this._bevelSpacingOnPages);
    this._tabInterval.Controls.Add((Control) this._labelSpacingOnPages);
    componentResourceManager.ApplyResources((object) this._tabInterval, "_tabInterval");
    this._tabInterval.Name = "_tabInterval";
    componentResourceManager.ApplyResources((object) this._checkBoxDisableAutoWords, "_checkBoxDisableAutoWords");
    this._checkBoxDisableAutoWords.BackColor = SystemColors.Control;
    this._checkBoxDisableAutoWords.Name = "_checkBoxDisableAutoWords";
    this._checkBoxDisableAutoWords.UseVisualStyleBackColor = false;
    this._checkBoxDisableAutoWords.CheckedChanged += new EventHandler(this._checkBoxDisableAutoWords_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxFromNewPage, "_checkBoxFromNewPage");
    this._checkBoxFromNewPage.BackColor = SystemColors.Control;
    this._checkBoxFromNewPage.Name = "_checkBoxFromNewPage";
    this._checkBoxFromNewPage.UseVisualStyleBackColor = false;
    this._checkBoxFromNewPage.CheckedChanged += new EventHandler(this._checkBoxFromNewPage_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxNoSplitWithNext, "_checkBoxNoSplitWithNext");
    this._checkBoxNoSplitWithNext.BackColor = SystemColors.Control;
    this._checkBoxNoSplitWithNext.Name = "_checkBoxNoSplitWithNext";
    this._checkBoxNoSplitWithNext.UseVisualStyleBackColor = false;
    this._checkBoxNoSplitWithNext.CheckedChanged += new EventHandler(this._checkBoxNoSplitWithNext_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxNotSplitParagraph, "_checkBoxNotSplitParagraph");
    this._checkBoxNotSplitParagraph.BackColor = SystemColors.Control;
    this._checkBoxNotSplitParagraph.Name = "_checkBoxNotSplitParagraph";
    this._checkBoxNotSplitParagraph.UseVisualStyleBackColor = false;
    this._checkBoxNotSplitParagraph.CheckedChanged += new EventHandler(this._checkBoxNotSplitParagraph_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._checkBoxDisableFloatLines, "_checkBoxDisableFloatLines");
    this._checkBoxDisableFloatLines.BackColor = SystemColors.Control;
    this._checkBoxDisableFloatLines.Checked = true;
    this._checkBoxDisableFloatLines.CheckState = CheckState.Checked;
    this._checkBoxDisableFloatLines.Name = "_checkBoxDisableFloatLines";
    this._checkBoxDisableFloatLines.UseVisualStyleBackColor = false;
    this._checkBoxDisableFloatLines.CheckedChanged += new EventHandler(this._checkBoxDisableFloatLines_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.BackColor = Color.Transparent;
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this._bevelSpacingOnPages, "_bevelSpacingOnPages");
    this._bevelSpacingOnPages.BackColor = Color.Transparent;
    this._bevelSpacingOnPages.Name = "_bevelSpacingOnPages";
    componentResourceManager.ApplyResources((object) this._labelSpacingOnPages, "_labelSpacingOnPages");
    this._labelSpacingOnPages.Name = "_labelSpacingOnPages";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._panelSampleBorder);
    this.Controls.Add((Control) this._bevelSample);
    this.Controls.Add((Control) this._labelSample);
    this.Controls.Add((Control) this._btnTabulation);
    this.Controls.Add((Control) this._tabSelector);
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SetupParagraphDlg);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Load += new EventHandler(this.SetupParagraphDlg_Load);
    this._panelSampleBorder.ResumeLayout(false);
    this._tabSelector.ResumeLayout(false);
    this._tabFont.ResumeLayout(false);
    this._tabFont.PerformLayout();
    this._spinEditLinesInterval.Properties.EndInit();
    this._spinEditIntervalAfter.Properties.EndInit();
    this._spinEditIntervalBefore.Properties.EndInit();
    this._spinEditIdentFirstLine.Properties.EndInit();
    this._spinEditRightIdent.Properties.EndInit();
    this._spinEditLeftIdent.Properties.EndInit();
    this._tabInterval.ResumeLayout(false);
    this._tabInterval.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
