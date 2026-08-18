// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.TextStyleDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

public class TextStyleDlg : Form
{
  private CharFormat _charFormat;
  private readonly string _testString;
  private static TextStyleDlg _instance;
  private bool _suspendEventsHandling;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _BtnCancel;
  private Button _BtnOK;
  private Bevel _bevelOptions;
  private Label _labelOptions;
  private Bevel bevel1;
  private Label label1;
  private CheckBox chDbkStrikeout;
  private CheckBox chStrikeout;
  private CheckBox chDblUnderline;
  private CheckBox chUnderline;
  private CheckBox chItalic;
  private CheckBox chBold;
  private ImRtfEditor ternSample;
  private Label _labelSample;

  public static DialogResult Execute(Form owner, CharFormat charFormat)
  {
    using (TextStyleDlg textStyleDlg = new TextStyleDlg(charFormat))
    {
      textStyleDlg.Owner = owner;
      return textStyleDlg.ShowDialog();
    }
  }

  public CharFormat CharFormat
  {
    get => this._charFormat;
    set => this.AssignCharFormat(value);
  }

  private void AssignCharFormat(CharFormat value)
  {
    if (value != null)
    {
      if (this._charFormat == null)
        this._charFormat = value;
      else
        this._charFormat.CharStyle = value.CharStyle;
    }
    this.UpdateSampleText();
  }

  private void UpdateSampleText()
  {
    string TypeFace1;
    if (!this.ternSample.GetFontInfo(-9999, out TypeFace1, out int _, out int _))
      return;
    float num = this._charFormat.FontSize.HasValue ? ((double) this._charFormat.FontSize.Value < 20.0 ? 20f : this._charFormat.FontSize.Value) : 20f;
    ImRtfEditor ternSample = this.ternSample;
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
    this.ternSample.TerDeleteAll(false);
    this.ternSample.InsertTerText(TextStyleDlg.GetStyleDescription(this._charFormat), true);
  }

  public TextStyleDlg(CharFormat charFormat, string testString = "")
  {
    this.InitializeComponent();
    this._testString = string.IsNullOrWhiteSpace(testString) ? "Строка текста" : testString.Trim();
    this.ternSample.Text = "";
    this.ternSample.BorderShowing = true;
    this.ternSample.TerSetFlags(true, 1048576 /*0x100000*/);
    this.ternSample.TerSetFlags5(true, 1073741824 /*0x40000000*/);
    this.ternSample.FittedView = false;
    this.ternSample.BorderMargin = false;
    this.ternSample.HorzScrollBar = false;
    this.ternSample.ReadOnlyMode = true;
    this.ternSample.TerSetMarginEx(-1, 0, 0, 0, 0, 0, 0, false);
    this.ternSample.TerSetFlags3(true, 1073741824 /*0x40000000*/);
    this.ternSample.TerSetCharSet((byte) 204);
    FontSetupDlg.FitPageSizeToWindow(this.ternSample, false);
    this.ternSample.SetTerParaFmt(1, true, false);
    this.ternSample.TerSetSectAlign(-1, 128 /*0x80*/, false);
    this.ternSample.TerSetFlags3(true, 128 /*0x80*/);
    this._charFormat = charFormat;
    this.ternSample.TerDeleteAll(false);
    this.ternSample.InsertTerText(this._testString, true);
    this.UpdateSampleText();
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    if (this._charFormat == null)
      return;
    this._suspendEventsHandling = true;
    try
    {
      CheckBox chBold = this.chBold;
      BoldItalicStyle? boldItalic = this._charFormat.BoldItalic;
      BoldItalicStyle? nullable = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & BoldItalicStyle.Bold) : new BoldItalicStyle?();
      BoldItalicStyle boldItalicStyle1 = BoldItalicStyle.Regular;
      int num1 = !(nullable.GetValueOrDefault() == boldItalicStyle1 & nullable.HasValue) ? 1 : 0;
      chBold.Checked = num1 != 0;
      CheckBox chItalic = this.chItalic;
      boldItalic = this._charFormat.BoldItalic;
      nullable = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & BoldItalicStyle.Italic) : new BoldItalicStyle?();
      BoldItalicStyle boldItalicStyle2 = BoldItalicStyle.Regular;
      int num2 = !(nullable.GetValueOrDefault() == boldItalicStyle2 & nullable.HasValue) ? 1 : 0;
      chItalic.Checked = num2 != 0;
      CheckBox chDbkStrikeout = this.chDbkStrikeout;
      StrikeoutLineStyle? strike = this._charFormat.Strike;
      int num3 = ((int) strike ?? 0) == 524288 /*0x080000*/ ? 1 : 0;
      chDbkStrikeout.Checked = num3 != 0;
      CheckBox chStrikeout = this.chStrikeout;
      strike = this._charFormat.Strike;
      int num4 = ((int) strike ?? 0) == 8 ? 1 : 0;
      chStrikeout.Checked = num4 != 0;
      CheckBox chUnderline = this.chUnderline;
      UnderlineStyle? underline = this._charFormat.Underline;
      int num5 = ((int) underline ?? 0) == 1 ? 1 : 0;
      chUnderline.Checked = num5 != 0;
      CheckBox chDblUnderline = this.chDblUnderline;
      underline = this._charFormat.Underline;
      int num6 = ((int) underline ?? 0) == 256 /*0x0100*/ ? 1 : 0;
      chDblUnderline.Checked = num6 != 0;
    }
    finally
    {
      this._suspendEventsHandling = false;
    }
  }

  private void chStrikeout_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chStrikeout.Checked)
    {
      this.chDbkStrikeout.Checked = false;
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.SingleLine);
    }
    else if (!this.chStrikeout.Checked && !this.chDbkStrikeout.Checked)
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.None);
    this.UpdateSampleText();
  }

  private void chDbkStrikeout_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chDbkStrikeout.Checked)
    {
      this.chStrikeout.Checked = false;
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.DoubleLine);
    }
    else if (!this.chStrikeout.Checked && !this.chDbkStrikeout.Checked)
      this._charFormat.Strike = new StrikeoutLineStyle?(StrikeoutLineStyle.None);
    this.UpdateSampleText();
  }

  private void chBold_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chBold.Checked)
    {
      CharFormat charFormat = this._charFormat;
      BoldItalicStyle? boldItalic = charFormat.BoldItalic;
      charFormat.BoldItalic = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() | BoldItalicStyle.Bold) : new BoldItalicStyle?();
    }
    else
    {
      CharFormat charFormat = this._charFormat;
      BoldItalicStyle? boldItalic = charFormat.BoldItalic;
      charFormat.BoldItalic = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & ~BoldItalicStyle.Bold) : new BoldItalicStyle?();
    }
    this.UpdateSampleText();
  }

  private void chItalic_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chItalic.Checked)
    {
      CharFormat charFormat = this._charFormat;
      BoldItalicStyle? boldItalic = charFormat.BoldItalic;
      charFormat.BoldItalic = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() | BoldItalicStyle.Italic) : new BoldItalicStyle?();
    }
    else
    {
      CharFormat charFormat = this._charFormat;
      BoldItalicStyle? boldItalic = charFormat.BoldItalic;
      charFormat.BoldItalic = boldItalic.HasValue ? new BoldItalicStyle?(boldItalic.GetValueOrDefault() & ~BoldItalicStyle.Italic) : new BoldItalicStyle?();
    }
    this.UpdateSampleText();
  }

  private void chUnderline_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chUnderline.Checked)
    {
      this.chDblUnderline.Checked = false;
      this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
    }
    else if (!this.chUnderline.Checked && !this.chDblUnderline.Checked)
      this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
    this.UpdateSampleText();
  }

  private void chDblUnderline_CheckedChanged(object sender, EventArgs e)
  {
    if (this._suspendEventsHandling)
      return;
    if (this.chDblUnderline.Checked)
    {
      this.chUnderline.Checked = false;
      this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.DoubleUnderline);
    }
    else if (!this.chUnderline.Checked && !this.chDblUnderline.Checked)
      this._charFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
    this.UpdateSampleText();
  }

  public static string GetStyleDescription(CharFormat charFormat)
  {
    if (charFormat == null)
      return "";
    BoldItalicStyle? boldItalic = charFormat.BoldItalic;
    int num1;
    if (!boldItalic.HasValue)
    {
      num1 = 0;
    }
    else
    {
      boldItalic = charFormat.BoldItalic;
      num1 = (int) boldItalic.Value;
    }
    BoldItalicStyle boldItalicStyle = (BoldItalicStyle) num1;
    if (charFormat.FontSize.HasValue)
    {
      float? fontSize = charFormat.FontSize;
    }
    UnderlineStyle? underline = charFormat.Underline;
    int num2;
    if (underline.HasValue)
    {
      underline = charFormat.Underline;
      num2 = (int) underline.Value;
    }
    else
      num2 = 0;
    UnderlineStyle underlineStyle = (UnderlineStyle) num2;
    bool flag1 = (charFormat.CharStyle & CharStyle.Strikethrough) != 0;
    bool flag2 = (charFormat.CharStyle & CharStyle.DoubleStrikethrough) != 0;
    string str = new StringBuilder().Append((boldItalicStyle & BoldItalicStyle.Bold) != BoldItalicStyle.Regular ? "полужирный " : "").Append((boldItalicStyle & BoldItalicStyle.Italic) != BoldItalicStyle.Regular ? "курсив " : "").Append(underlineStyle == UnderlineStyle.Underline ? "подч. " : "").Append(underlineStyle == UnderlineStyle.DoubleUnderline ? "дв.подч. " : "").Append(flag1 ? "зачерк. " : "").Append(flag2 ? "дв.зачерк. " : "").ToString().Trim();
    return !string.IsNullOrWhiteSpace(str) ? str : "обычный";
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TextStyleDlg));
    this._BtnCancel = new Button();
    this._BtnOK = new Button();
    this._bevelOptions = new Bevel();
    this._labelOptions = new Label();
    this.bevel1 = new Bevel();
    this.label1 = new Label();
    this.chDbkStrikeout = new CheckBox();
    this.chStrikeout = new CheckBox();
    this.chDblUnderline = new CheckBox();
    this.chUnderline = new CheckBox();
    this.chItalic = new CheckBox();
    this.chBold = new CheckBox();
    this.ternSample = new ImRtfEditor();
    this._labelSample = new Label();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._BtnCancel, "_BtnCancel");
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.Name = "_BtnCancel";
    componentResourceManager.ApplyResources((object) this._BtnOK, "_BtnOK");
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Name = "_BtnOK";
    componentResourceManager.ApplyResources((object) this._bevelOptions, "_bevelOptions");
    this._bevelOptions.BackColor = Color.Transparent;
    this._bevelOptions.Name = "_bevelOptions";
    componentResourceManager.ApplyResources((object) this._labelOptions, "_labelOptions");
    this._labelOptions.FlatStyle = FlatStyle.System;
    this._labelOptions.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelOptions.Name = "_labelOptions";
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.BackColor = Color.Transparent;
    this.bevel1.Name = "bevel1";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.FlatStyle = FlatStyle.System;
    this.label1.ForeColor = Color.FromArgb(0, 70, 213);
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.chDbkStrikeout, "chDbkStrikeout");
    this.chDbkStrikeout.BackColor = SystemColors.Control;
    this.chDbkStrikeout.Name = "chDbkStrikeout";
    this.chDbkStrikeout.UseVisualStyleBackColor = false;
    this.chDbkStrikeout.CheckedChanged += new EventHandler(this.chDbkStrikeout_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chStrikeout, "chStrikeout");
    this.chStrikeout.BackColor = SystemColors.Control;
    this.chStrikeout.Name = "chStrikeout";
    this.chStrikeout.UseVisualStyleBackColor = false;
    this.chStrikeout.CheckedChanged += new EventHandler(this.chStrikeout_CheckedChanged);
    this.chDblUnderline.AllowDrop = true;
    componentResourceManager.ApplyResources((object) this.chDblUnderline, "chDblUnderline");
    this.chDblUnderline.BackColor = SystemColors.Control;
    this.chDblUnderline.Name = "chDblUnderline";
    this.chDblUnderline.UseVisualStyleBackColor = false;
    this.chDblUnderline.CheckedChanged += new EventHandler(this.chDblUnderline_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chUnderline, "chUnderline");
    this.chUnderline.BackColor = SystemColors.Control;
    this.chUnderline.Name = "chUnderline";
    this.chUnderline.UseVisualStyleBackColor = false;
    this.chUnderline.CheckedChanged += new EventHandler(this.chUnderline_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chItalic, "chItalic");
    this.chItalic.BackColor = SystemColors.Control;
    this.chItalic.Name = "chItalic";
    this.chItalic.UseVisualStyleBackColor = false;
    this.chItalic.CheckedChanged += new EventHandler(this.chItalic_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.chBold, "chBold");
    this.chBold.BackColor = SystemColors.Control;
    this.chBold.Name = "chBold";
    this.chBold.UseVisualStyleBackColor = false;
    this.chBold.CheckedChanged += new EventHandler(this.chBold_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.ternSample, "ternSample");
    this.ternSample.Cursor = Cursors.Default;
    this.ternSample.Name = "ternSample";
    this.ternSample.ReadOnlyMode = false;
    this.ternSample.RtfText = componentResourceManager.GetString("ternSample.RtfText");
    this.ternSample.TotalLines = 1;
    componentResourceManager.ApplyResources((object) this._labelSample, "_labelSample");
    this._labelSample.BackColor = SystemColors.Control;
    this._labelSample.FlatStyle = FlatStyle.System;
    this._labelSample.ForeColor = Color.FromArgb(0, 70, 213);
    this._labelSample.Name = "_labelSample";
    this.AcceptButton = (IButtonControl) this._BtnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.Controls.Add((Control) this.ternSample);
    this.Controls.Add((Control) this._labelSample);
    this.Controls.Add((Control) this.chItalic);
    this.Controls.Add((Control) this.chBold);
    this.Controls.Add((Control) this.chDbkStrikeout);
    this.Controls.Add((Control) this.chStrikeout);
    this.Controls.Add((Control) this.chDblUnderline);
    this.Controls.Add((Control) this.chUnderline);
    this.Controls.Add((Control) this.bevel1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._bevelOptions);
    this.Controls.Add((Control) this._labelOptions);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (TextStyleDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
