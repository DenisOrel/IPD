// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.Controls.SelectFormat
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Reports.Controls;

public class SelectFormat : Form
{
  /// <summary>хранит формат данных</summary>
  internal string format;
  private static string[] formats = new string[15]
  {
    "d",
    "D",
    "t",
    "T",
    "f",
    "F",
    "g",
    "G",
    "m",
    "M",
    "r",
    "R",
    "s",
    "y",
    "Y"
  };
  private List<string> editDate = new List<string>((IEnumerable<string>) SelectFormat.formats);
  private FieldTypes type;
  private bool _initialize = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox groupBox1;
  private Label labelCountDigit;
  private NumericUpDown numericUpDownCount;
  private Button buttonOk;
  private Button buttonCancel;
  private GroupBox groupBoxSample;
  private Label labelSample;
  private Label labelDateTime;
  private ListBox listBoxDateTime;
  private Label labelBoolean;
  private Label labelFalse;
  private Label labelTrue;
  private TextBox textBoxFalse;
  private TextBox textBoxTrue;
  private GroupBox groupBoxName;
  private RadioButton radioButtonNo;
  private RadioButton radioButtonShort;
  private RadioButton radioButtonLong;

  public SelectFormat(FieldTypes type, string oldformat)
  {
    this.InitializeComponent();
    try
    {
      this.type = type;
      this.format = oldformat;
      switch (type)
      {
        case FieldTypes.ftInteger:
        case FieldTypes.ftAutoInc:
          this.TextVisible(false);
          this.DoubleVisible(false);
          this.DateTimeVisible(false);
          this.BooleanVisible(false);
          this.labelSample.Text = 457654.ToString("D");
          this.MeasureVisible(false);
          this.SampleVisible(true);
          this.IntegerVisible(true);
          this.format = "D";
          this.buttonCancel.Visible = false;
          this.buttonOk.Location = new Point(107, 257);
          break;
        case FieldTypes.ftDouble:
          this.TextVisible(false);
          this.IntegerVisible(false);
          this.DateTimeVisible(false);
          this.BooleanVisible(false);
          if (!SelectFormat.IsValidate(FieldTypes.ftDouble, this.format))
          {
            this.numericUpDownCount.Value = 0M;
            this.format = "F0";
          }
          else
            this.numericUpDownCount.Value = Decimal.Parse(this.format.Substring(1, this.format.Length - 1));
          this.labelSample.Text = (10.0 / 9.0).ToString("F" + (object) this.numericUpDownCount.Value);
          this.MeasureVisible(false);
          this.SampleVisible(true);
          this.DoubleVisible(true);
          break;
        case FieldTypes.ftDateTime:
          this.TextVisible(false);
          this.IntegerVisible(false);
          this.DoubleVisible(false);
          this.BooleanVisible(false);
          this.SampleVisible(false);
          this.MeasureVisible(false);
          this.DateTimeVisible(true);
          DateTime dateTime = new DateTime(2007, 2, 7, 17, 5, 6);
          for (int index = 14; index >= 0; --index)
          {
            if (this.listBoxDateTime.Items.Contains((object) dateTime.ToString(SelectFormat.formats[index])))
              this.editDate.RemoveAt(index);
            else
              this.listBoxDateTime.Items.Add((object) dateTime.ToString(SelectFormat.formats[index]));
          }
          this.editDate.Reverse();
          if (!SelectFormat.IsValidate(FieldTypes.ftDateTime, this.format))
            this.format = this.editDate[0];
          this.listBoxDateTime.SetSelected(this.editDate.IndexOf(this.format), true);
          break;
        case FieldTypes.ftBoolean:
          int length1;
          if (!SelectFormat.IsValidate(FieldTypes.ftBoolean, this.format))
          {
            length1 = 2;
            this.format = $"{this.textBoxTrue.Text.Trim()};{this.textBoxFalse.Text.Trim()}";
          }
          else
            length1 = this.format.IndexOf(";");
          string str = this.format.Substring(length1 + 1, this.format.Length - length1 - 1);
          this.textBoxTrue.Text = this.format.Substring(0, length1);
          this.textBoxFalse.Text = str;
          this.TextVisible(false);
          this.IntegerVisible(false);
          this.DoubleVisible(false);
          this.DateTimeVisible(false);
          this.SampleVisible(false);
          this.MeasureVisible(false);
          this.BooleanVisible(true);
          break;
        case FieldTypes.ftMeasured:
          this.TextVisible(false);
          this.DoubleVisible(false);
          this.DateTimeVisible(false);
          this.BooleanVisible(false);
          this.IntegerVisible(false);
          this.SampleVisible(true);
          this.MeasureVisible(true);
          this.radioButtonLong.Checked = true;
          int length2;
          if (!SelectFormat.IsValidate(FieldTypes.ftMeasured, this.format))
          {
            this.numericUpDownCount.Value = 2M;
            this.format = "F2;S";
            length2 = 2;
          }
          else
          {
            length2 = this.format.IndexOf(";");
            this.numericUpDownCount.Value = Decimal.Parse(this.format.Substring(1, length2 - 1));
          }
          this.labelSample.Text = (10.0 / 9.0).ToString("F" + this.numericUpDownCount.Value.ToString());
          switch (this.format[length2 + 1])
          {
            case 'L':
              this.labelSample.Text += LocalizationHolder.rm.GetString("Document.Client_131");
              this.radioButtonLong.Checked = true;
              return;
            case 'N':
              this.radioButtonNo.Checked = true;
              return;
            case 'S':
              this.labelSample.Text += LocalizationHolder.rm.GetString("Document.Client_132");
              this.radioButtonShort.Checked = true;
              return;
            default:
              this.labelSample.Text += LocalizationHolder.rm.GetString("Document.Client_132");
              this.radioButtonShort.Checked = true;
              this.format = this.format.Substring(0, length2) + "S";
              return;
          }
        default:
          this.IntegerVisible(false);
          this.DoubleVisible(false);
          this.DateTimeVisible(false);
          this.BooleanVisible(false);
          this.labelSample.Text = LocalizationHolder.rm.GetString("Document.Client_133");
          this.MeasureVisible(false);
          this.SampleVisible(true);
          this.TextVisible(true);
          this.format = string.Empty;
          this.buttonCancel.Visible = false;
          this.buttonOk.Location = new Point(107, 257);
          break;
      }
    }
    finally
    {
      this._initialize = false;
    }
  }

  private void numericUpDownCount_ValueChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    string empty = string.Empty;
    int num = this.format.IndexOf(";");
    if (this.type == FieldTypes.ftMeasured)
      empty = this.format[num + 1].ToString();
    this.format = "F" + this.numericUpDownCount.Value.ToString();
    this.labelSample.Text = (10.0 / 9.0).ToString("F" + (object) this.numericUpDownCount.Value);
    if (this.type != FieldTypes.ftMeasured)
      return;
    this.format = $"{this.format};{empty}";
    switch (empty)
    {
      case "L":
        this.labelSample.Text += LocalizationHolder.rm.GetString("Document.Client_131");
        break;
      case "S":
        this.labelSample.Text += LocalizationHolder.rm.GetString("Document.Client_134");
        break;
    }
  }

  /// <summary>отвечает за отображение типа string</summary>
  /// <param name="visible">отображать или нет</param>
  private void TextVisible(bool visible)
  {
  }

  /// <summary>отвечает за отображение типа integer</summary>
  /// <param name="visible">отображать или нет</param>
  private void IntegerVisible(bool visible)
  {
  }

  /// <summary>отвечает за отображение типа double</summary>
  /// <param name="visible">отображать или нет</param>
  private void DoubleVisible(bool visible)
  {
    this.labelCountDigit.Visible = visible;
    this.numericUpDownCount.Visible = visible;
  }

  /// <summary>отвечает за отображение поля образец</summary>
  /// <param name="visible">отображать или нет</param>
  private void SampleVisible(bool visible)
  {
    this.groupBoxSample.Location = new Point(19, 19);
    this.labelSample.Visible = visible;
    this.groupBoxSample.Visible = visible;
  }

  /// <summary>отвечает за отображение типа DateTime</summary>
  /// <param name="visible">отображать или нет</param>
  private void DateTimeVisible(bool visible)
  {
    this.listBoxDateTime.Visible = visible;
    this.labelDateTime.Visible = visible;
  }

  /// <summary>отвечает за отображение типа boolean</summary>
  /// <param name="visible">отображать или нет</param>
  private void BooleanVisible(bool visible)
  {
    this.labelBoolean.Visible = visible;
    this.labelTrue.Visible = visible;
    this.labelFalse.Visible = visible;
    this.textBoxFalse.Visible = visible;
    this.textBoxTrue.Visible = visible;
  }

  /// <summary>отвечает за отображение ftMeasure</summary>
  /// <param name="visible">отображать или нет</param>
  private void MeasureVisible(bool visible)
  {
    this.labelCountDigit.Visible = visible;
    this.numericUpDownCount.Visible = visible;
    this.groupBoxName.Visible = visible;
  }

  private void listBoxDateTime_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.format = this.editDate[this.listBoxDateTime.SelectedIndex];
  }

  private void textBoxTrue_TextChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.format = $"{this.textBoxTrue.Text.Trim()};{this.textBoxFalse.Text.Trim()}";
  }

  private void textBoxFalse_TextChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.format = $"{this.textBoxTrue.Text.Trim()};{this.textBoxFalse.Text.Trim()}";
  }

  private void radioButtonLong_CheckedChanged(object sender, EventArgs e)
  {
    if (this._initialize || !this.radioButtonLong.Checked)
      return;
    this.format = $"F{(object) this.numericUpDownCount.Value};L";
    this.labelSample.Text = (10.0 / 9.0).ToString("F" + (object) this.numericUpDownCount.Value) + LocalizationHolder.rm.GetString("Document.Client_131");
  }

  private void radioButtonShort_CheckedChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.format = $"F{(object) this.numericUpDownCount.Value};S";
    this.labelSample.Text = (10.0 / 9.0).ToString("F" + (object) this.numericUpDownCount.Value) + LocalizationHolder.rm.GetString("Document.Client_134");
  }

  private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
  {
    if (this._initialize)
      return;
    this.format = $"F{(object) this.numericUpDownCount.Value};N";
    this.labelSample.Text = (10.0 / 9.0).ToString("F" + (object) this.numericUpDownCount.Value);
  }

  public static bool IsValidate(FieldTypes type, string formatString)
  {
    double result;
    switch (type)
    {
      case FieldTypes.ftInteger:
      case FieldTypes.ftAutoInc:
        return formatString == "D";
      case FieldTypes.ftDouble:
        if (formatString == string.Empty || formatString[0] != 'F' || !double.TryParse(formatString.Substring(1, formatString.Length - 1), out result))
          return false;
        Decimal num1 = Decimal.Parse(formatString.Substring(1, formatString.Length - 1));
        return !(num1 < 0M) && !(num1 > 30M);
      case FieldTypes.ftDateTime:
        if (formatString == string.Empty | formatString.Length > 1)
          return false;
        DateTime dateTime = new DateTime(2007, 2, 7, 17, 5, 6);
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>((IEnumerable<string>) SelectFormat.formats);
        for (int index = 14; index >= 0; --index)
        {
          if (stringList1.Contains(dateTime.ToString(SelectFormat.formats[index])))
            stringList2.RemoveAt(index);
          else
            stringList1.Add(dateTime.ToString(SelectFormat.formats[index]));
        }
        stringList2.Reverse();
        return stringList2.Contains(formatString);
      case FieldTypes.ftBoolean:
        return formatString.IndexOf(";") != -1;
      case FieldTypes.ftMeasured:
        if (string.IsNullOrEmpty(formatString) || formatString[0] != 'F')
          return false;
        int num2 = formatString.IndexOf(";", StringComparison.CurrentCulture);
        switch (num2)
        {
          case 2:
          case 3:
            if (!double.TryParse(formatString.Substring(1, num2 - 1), out result))
              return false;
            Decimal num3 = Decimal.Parse(formatString.Substring(1, num2 - 1));
            return !(num3 < 0M) && !(num3 > 30M) && num2 + 1 < formatString.Length && (formatString[num2 + 1] == 'L' || formatString[num2 + 1] == 'S' || formatString[num2 + 1] == 'N');
          default:
            return false;
        }
      default:
        return formatString == string.Empty;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectFormat));
    this.groupBox1 = new GroupBox();
    this.groupBoxName = new GroupBox();
    this.radioButtonNo = new RadioButton();
    this.radioButtonShort = new RadioButton();
    this.radioButtonLong = new RadioButton();
    this.textBoxFalse = new TextBox();
    this.textBoxTrue = new TextBox();
    this.labelFalse = new Label();
    this.labelTrue = new Label();
    this.labelBoolean = new Label();
    this.listBoxDateTime = new ListBox();
    this.labelDateTime = new Label();
    this.groupBoxSample = new GroupBox();
    this.labelSample = new Label();
    this.labelCountDigit = new Label();
    this.numericUpDownCount = new NumericUpDown();
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.groupBox1.SuspendLayout();
    this.groupBoxName.SuspendLayout();
    this.groupBoxSample.SuspendLayout();
    this.numericUpDownCount.BeginInit();
    this.SuspendLayout();
    this.groupBox1.Controls.Add((Control) this.groupBoxName);
    this.groupBox1.Controls.Add((Control) this.textBoxFalse);
    this.groupBox1.Controls.Add((Control) this.textBoxTrue);
    this.groupBox1.Controls.Add((Control) this.labelFalse);
    this.groupBox1.Controls.Add((Control) this.labelTrue);
    this.groupBox1.Controls.Add((Control) this.labelBoolean);
    this.groupBox1.Controls.Add((Control) this.listBoxDateTime);
    this.groupBox1.Controls.Add((Control) this.labelDateTime);
    this.groupBox1.Controls.Add((Control) this.groupBoxSample);
    this.groupBox1.Controls.Add((Control) this.labelCountDigit);
    this.groupBox1.Controls.Add((Control) this.numericUpDownCount);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.groupBoxName.Controls.Add((Control) this.radioButtonNo);
    this.groupBoxName.Controls.Add((Control) this.radioButtonShort);
    this.groupBoxName.Controls.Add((Control) this.radioButtonLong);
    componentResourceManager.ApplyResources((object) this.groupBoxName, "groupBoxName");
    this.groupBoxName.Name = "groupBoxName";
    this.groupBoxName.TabStop = false;
    componentResourceManager.ApplyResources((object) this.radioButtonNo, "radioButtonNo");
    this.radioButtonNo.Name = "radioButtonNo";
    this.radioButtonNo.TabStop = true;
    this.radioButtonNo.Tag = (object) "3";
    this.radioButtonNo.UseVisualStyleBackColor = true;
    this.radioButtonNo.CheckedChanged += new EventHandler(this.radioButtonNo_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButtonShort, "radioButtonShort");
    this.radioButtonShort.Name = "radioButtonShort";
    this.radioButtonShort.TabStop = true;
    this.radioButtonShort.Tag = (object) "2";
    this.radioButtonShort.UseVisualStyleBackColor = true;
    this.radioButtonShort.CheckedChanged += new EventHandler(this.radioButtonShort_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.radioButtonLong, "radioButtonLong");
    this.radioButtonLong.Name = "radioButtonLong";
    this.radioButtonLong.TabStop = true;
    this.radioButtonLong.Tag = (object) "1";
    this.radioButtonLong.UseVisualStyleBackColor = true;
    this.radioButtonLong.CheckedChanged += new EventHandler(this.radioButtonLong_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.textBoxFalse, "textBoxFalse");
    this.textBoxFalse.Name = "textBoxFalse";
    this.textBoxFalse.TextChanged += new EventHandler(this.textBoxFalse_TextChanged);
    componentResourceManager.ApplyResources((object) this.textBoxTrue, "textBoxTrue");
    this.textBoxTrue.Name = "textBoxTrue";
    this.textBoxTrue.TextChanged += new EventHandler(this.textBoxTrue_TextChanged);
    componentResourceManager.ApplyResources((object) this.labelFalse, "labelFalse");
    this.labelFalse.Name = "labelFalse";
    componentResourceManager.ApplyResources((object) this.labelTrue, "labelTrue");
    this.labelTrue.Name = "labelTrue";
    componentResourceManager.ApplyResources((object) this.labelBoolean, "labelBoolean");
    this.labelBoolean.Name = "labelBoolean";
    this.listBoxDateTime.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.listBoxDateTime, "listBoxDateTime");
    this.listBoxDateTime.Name = "listBoxDateTime";
    this.listBoxDateTime.SelectedIndexChanged += new EventHandler(this.listBoxDateTime_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.labelDateTime, "labelDateTime");
    this.labelDateTime.Name = "labelDateTime";
    this.groupBoxSample.Controls.Add((Control) this.labelSample);
    componentResourceManager.ApplyResources((object) this.groupBoxSample, "groupBoxSample");
    this.groupBoxSample.Name = "groupBoxSample";
    this.groupBoxSample.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelSample, "labelSample");
    this.labelSample.Name = "labelSample";
    componentResourceManager.ApplyResources((object) this.labelCountDigit, "labelCountDigit");
    this.labelCountDigit.Name = "labelCountDigit";
    componentResourceManager.ApplyResources((object) this.numericUpDownCount, "numericUpDownCount");
    this.numericUpDownCount.Maximum = new Decimal(new int[4]
    {
      30,
      0,
      0,
      0
    });
    this.numericUpDownCount.Name = "numericUpDownCount";
    this.numericUpDownCount.ValueChanged += new EventHandler(this.numericUpDownCount_ValueChanged);
    this.buttonOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.groupBox1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.Name = nameof (SelectFormat);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBoxName.ResumeLayout(false);
    this.groupBoxName.PerformLayout();
    this.groupBoxSample.ResumeLayout(false);
    this.groupBoxSample.PerformLayout();
    this.numericUpDownCount.EndInit();
    this.ResumeLayout(false);
  }
}
