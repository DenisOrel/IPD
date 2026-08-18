
// Type: Intermech.Controls.MeasureSpinEdit
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using DevExpress.IM.XtraEditors;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary> SpinEdit допускающий редактирование значения указаные в некоторых единицах измерения  </summary>
public class MeasureSpinEdit : SpinEdit
{
  private MeasuresConvertor _measuresConvertor;
  private bool _emptyValueAsNullText;
  private MeasureDescriptor _baseMeasureDescriptor;
  private MeasureDescriptor[] _measureDescriptors;
  public GetMeasureDescriptorsDelegate _getMeasureDescriptorsEvent;
  private MeasureDescriptor _lastMeasureDescriptor;
  private MeasuredValue _lastMeasuredValue;
  private double _lastValue;
  private double _precision;
  private bool _lockOnTextChanged;

  public MeasuresConvertor MeasuresConvertor => this._measuresConvertor;

  /// <summary> Удалять ли текст если значение становиться равным нулю </summary>
  [DefaultValue(false)]
  [Description("Удалять ли текст если значение становиться равным нулю")]
  [Browsable(true)]
  public bool EmptyValueAsNullText
  {
    get => this._emptyValueAsNullText;
    set
    {
      if (this._emptyValueAsNullText == value)
        return;
      this._emptyValueAsNullText = value;
      if (!this._emptyValueAsNullText)
        return;
      if (this._measuresConvertor == null)
      {
        if (!(this.Text == "0"))
          return;
        this.Text = "";
      }
      else
        this.OnTextChanged((EventArgs) null);
    }
  }

  /// <summary> Список доступных единиц измерения </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public MeasureDescriptor[] MeasureDescriptors
  {
    get => this._measureDescriptors;
    set
    {
      this._measureDescriptors = value;
      if (this._measureDescriptors != null && this._measureDescriptors.Length != 0)
      {
        if (this._baseMeasureDescriptor != null && Array.IndexOf<MeasureDescriptor>(this._measureDescriptors, this._baseMeasureDescriptor) != -1)
          return;
        this._baseMeasureDescriptor = this._measureDescriptors[0];
      }
      else
        this._baseMeasureDescriptor = (MeasureDescriptor) null;
    }
  }

  /// <summary> Базовая единица измерения </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public MeasureDescriptor BaseMeasureDescriptor
  {
    get => this._baseMeasureDescriptor;
    set => this._baseMeasureDescriptor = value;
  }

  /// <summary> Точность округления. Если = 0, то округление не производиться </summary>
  [DefaultValue(0.0)]
  [Browsable(true)]
  [Description("Точность округления. Если = 0, то округление не производиться")]
  public double Precision
  {
    get => this._precision;
    set => this._precision = value < 0.0 ? 0.0 : value;
  }

  /// <summary></summary>
  [Browsable(false)]
  public MeasureDescriptor LastMeasureDescriptor => this._lastMeasureDescriptor;

  [Browsable(false)]
  public MeasuredValue LastMeasuredValue => this._lastMeasuredValue;

  /// <summary></summary>
  [Browsable(false)]
  public double LastValue
  {
    get => this._lastValue;
    set
    {
      this._lastValue = value;
      if (this._measuresConvertor != null && this.BaseMeasureDescriptor != null)
      {
        this._lastMeasuredValue = new MeasuredValue(this._lastValue, this.BaseMeasureDescriptor.MeasureID, (string) null);
        this._lastMeasuredValue.Caption = this._measuresConvertor.ConvertToString(this._lastValue, this._lastMeasuredValue.MeasureID, false);
        this.Text = this._lastMeasuredValue.Caption;
      }
      else
      {
        this._lastMeasuredValue = (MeasuredValue) null;
        this.Text = this._lastValue.ToString();
      }
    }
  }

  /// <summary>Назначить значение LastValue</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="baseMeasureDescriptor">Базовая физическая величина для значения</param>
  /// <param name="updateText">Обновлять текст в контроле</param>
  public void AssignLastValue(
    MeasuredValue value,
    MeasureDescriptor baseMeasureDescriptor,
    bool updateText)
  {
    this._lastMeasuredValue = value;
    if (this._lastMeasuredValue != null)
      this._lastValue = this._lastMeasuredValue.Value;
    this.BaseMeasureDescriptor = baseMeasureDescriptor;
    if (this._measuresConvertor != null && this.BaseMeasureDescriptor != null && this._lastMeasuredValue != null)
    {
      this._lastValue = this._measuresConvertor.ConvertToBaseMeasure(this._lastMeasuredValue).Value;
      this._lastMeasuredValue.Caption = this._measuresConvertor.ConvertToString(this._lastValue, this._lastMeasuredValue.MeasureID, true);
      if (!updateText)
        return;
      this.Text = this._lastMeasuredValue.Caption;
    }
    else
    {
      this._lastMeasuredValue = (MeasuredValue) null;
      if (!updateText)
        return;
      this.Text = this._lastValue.ToString();
    }
  }

  /// <summary>Назначить значение LastValue</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="baseMeasureDescriptor">Базовая физическая величина для значения</param>
  /// <param name="updateText">Обновлять текст в контроле</param>
  public void AssignLastValue(
    double value,
    MeasureDescriptor baseMeasureDescriptor,
    bool updateText)
  {
    this._lastValue = value;
    this.BaseMeasureDescriptor = baseMeasureDescriptor;
    if (this._measuresConvertor != null && this.BaseMeasureDescriptor != null)
    {
      this._lastMeasuredValue = new MeasuredValue(this._lastValue, this.BaseMeasureDescriptor.MeasureID, (string) null);
      this._lastMeasuredValue.Caption = this._measuresConvertor.ConvertToString(this._lastValue, this._lastMeasuredValue.MeasureID, false);
      if (!updateText)
        return;
      this.Text = this._lastMeasuredValue.Caption;
    }
    else
    {
      this._lastMeasuredValue = (MeasuredValue) null;
      if (!updateText)
        return;
      this.Text = this._lastValue.ToString();
    }
  }

  /// <summary>Назначить значение LastValue</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="updateText">Обновлять текст в контроле</param>
  public void AssignLastValue(double value, bool updateText)
  {
    this._lastValue = value;
    if (this._measuresConvertor != null && this.BaseMeasureDescriptor != null)
    {
      this._lastMeasuredValue = new MeasuredValue(this._lastValue, this.BaseMeasureDescriptor.MeasureID, (string) null);
      this._lastMeasuredValue.Caption = this._measuresConvertor.ConvertToString(this._lastValue, this._lastMeasuredValue.MeasureID, false);
      if (!updateText)
        return;
      this.Text = this._lastMeasuredValue.Caption;
    }
    else
    {
      this._lastMeasuredValue = (MeasuredValue) null;
      if (!updateText)
        return;
      this.Text = this._lastValue.ToString();
    }
  }

  public override string Text
  {
    get => base.Text;
    set
    {
      string str = value;
      if ((this._measuresConvertor == null || this.BaseMeasureDescriptor == null) && this.EmptyValueAsNullText && value == "0")
        str = "";
      base.Text = str;
    }
  }

  /// <summary> Событие загрузки списка допустимых единиц измерения </summary>
  public event GetMeasureDescriptorsDelegate OnGetMeasureDescriptors
  {
    add
    {
      this.MeasureDescriptors = value != null ? value() : (MeasureDescriptor[]) null;
      if (this._measureDescriptors != null)
      {
        this._measuresConvertor = new MeasuresConvertor();
        this._measuresConvertor.Init(this._measureDescriptors);
        this._measuresConvertor.NormalizeString = false;
        this._lastMeasureDescriptor = this.BaseMeasureDescriptor;
        if (this.BaseMeasureDescriptor != null)
          this.Text = $"{this._lastValue.ToString("G2")} {this.BaseMeasureDescriptor.ShortName}";
        else
          this.Text = this._lastValue.ToString("G2");
      }
      else
      {
        this._measuresConvertor = (MeasuresConvertor) null;
        this.Text = this._lastValue.ToString("G2");
      }
      this._getMeasureDescriptorsEvent += value;
    }
    remove
    {
      this._getMeasureDescriptorsEvent -= value;
      if (this._getMeasureDescriptorsEvent != null && this._getMeasureDescriptorsEvent.GetInvocationList().Length != 0)
        return;
      this._measureDescriptors = (MeasureDescriptor[]) null;
      this._measuresConvertor = (MeasuresConvertor) null;
      this._baseMeasureDescriptor = (MeasureDescriptor) null;
      this.Text = this._lastValue.ToString("G2");
    }
  }

  protected override void OnValidating(CancelEventArgs e)
  {
    if (this._measuresConvertor != null)
      return;
    e.Cancel = false;
  }

  /// <summary> Вызывается после того, как было изменено введёное значение  </summary>
  /// <param name="e"></param>
  protected override void OnTextChanged(EventArgs e)
  {
    int num = this._lockOnTextChanged ? 1 : 0;
    base.OnTextChanged(e);
  }

  public event CancelEventHandler BeforeIncrement;

  protected override void OnIncrement()
  {
    if (this.BeforeIncrement != null)
    {
      CancelEventArgs e = new CancelEventArgs(false);
      this.BeforeIncrement((object) this, e);
      if (e.Cancel)
        return;
    }
    this.TextToValue();
    double a = this._lastValue + (double) this.Properties.Increment;
    if (!this.Properties.IsFloatValue)
      a = Math.Round(a);
    this.LastValue = a;
  }

  public event CancelEventHandler BeforeDecrement;

  protected override void OnDecrement()
  {
    if (this.BeforeDecrement != null)
    {
      CancelEventArgs e = new CancelEventArgs(false);
      this.BeforeDecrement((object) this, e);
      if (e.Cancel)
        return;
    }
    this.TextToValue();
    double a = this._lastValue - (double) this.Properties.Increment;
    if (!this.Properties.IsFloatValue)
      a = Math.Round(a);
    this.LastValue = a;
  }

  private void TextToValue()
  {
    string str = this.Text.Trim();
    switch (str)
    {
      case null:
        break;
      case "":
        break;
      default:
        double result = 0.0;
        if (this._measuresConvertor != null)
        {
          if (this.BaseMeasureDescriptor != null)
          {
            MeasuredValue measuredValue;
            try
            {
              measuredValue = this._measuresConvertor.ConvertToMeasuredValue(str, this.BaseMeasureDescriptor, true);
            }
            catch
            {
              break;
            }
            if (measuredValue == null)
              break;
            double num = measuredValue.Value;
            this._lastMeasuredValue = measuredValue;
            this._lastValue = num;
            break;
          }
        }
        this._lastMeasureDescriptor = (MeasureDescriptor) null;
        this._lastMeasuredValue = (MeasuredValue) null;
        if (str != null && str != "")
        {
          if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator != ",")
            str = str.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
          if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator != ".")
            str = str.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }
        double.TryParse(str, out result);
        this._lastValue = result;
        break;
    }
  }

  protected override void OnLeave(EventArgs e)
  {
    this.TextToValue();
    if (this._measuresConvertor != null && this.BaseMeasureDescriptor != null && this._lastMeasuredValue != null)
    {
      double val = this._lastMeasuredValue.Value;
      MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
      foreach (MeasureDescriptor measure in this._measuresConvertor.Measures)
      {
        if (measure.MeasureID == this._lastMeasuredValue.MeasureID)
        {
          measureDescriptor = measure;
          break;
        }
      }
      if (measureDescriptor == null)
        return;
      this.Text = this._measuresConvertor.ConvertToString(val, this._lastMeasuredValue.MeasureID, false);
    }
    else
      this.Text = this._lastValue.ToString();
    base.OnLeave(e);
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Return)
      return base.ProcessCmdKey(ref msg, keyData);
    this.OnLeave(new EventArgs());
    return true;
  }

  /// <summary> Проверка того, что формат введённой строки распознаётся </summary>
  /// <returns></returns>
  public bool CheckIsValueParseable()
  {
    if (this._measuresConvertor == null)
      return false;
    string str = this.Text.Trim();
    if (this.BaseMeasureDescriptor != null)
    {
      MeasuredValue measuredValue;
      try
      {
        measuredValue = this._measuresConvertor.ConvertToMeasuredValue(str, this.BaseMeasureDescriptor, false);
      }
      catch
      {
        return false;
      }
      if (measuredValue == null)
        return false;
      MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
      foreach (MeasureDescriptor measure in this._measuresConvertor.Measures)
      {
        if (measure.MeasureID == measuredValue.MeasureID)
        {
          measureDescriptor = measure;
          break;
        }
      }
      return measureDescriptor != null;
    }
    float result = 0.0f;
    if (str != null && str != "")
    {
      if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator != ",")
        str = str.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
      if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator != ".")
        str = str.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
    }
    return float.TryParse(str, out result);
  }
}
