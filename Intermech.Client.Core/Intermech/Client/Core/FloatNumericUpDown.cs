
// Type: Intermech.Client.Core.FloatNumericUpDown
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Инициализация изменения </summary>
public class FloatNumericUpDown : IDisposable
{
  /// <summary>ссылка на NumericUpDown</summary>
  protected NumericUpDown _numericUpDown;
  /// <summary>ссылка на переменную </summary>
  protected Rclass<float> _refValue;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._numericUpDown != null)
    {
      this._numericUpDown.ValueChanged -= new EventHandler(this.NumericUpDown_ValueChanged);
      this._numericUpDown = (NumericUpDown) null;
    }
    if (this._refValue == null)
      return;
    this._refValue.ValueChanged -= new EventHandler<EventArgs<float>>(this.RefValue_ValueChanged);
    this._refValue = (Rclass<float>) null;
  }

  /// <summary>Инициализация изменения </summary>
  /// <param name="varnumericUpDown"></param>
  /// <param name="var"></param>
  public void Initialize(NumericUpDown varnumericUpDown, Rclass<float> var)
  {
    if (varnumericUpDown == null)
      throw new ArgumentNullException(nameof (varnumericUpDown));
    if (var == null)
      throw new ArgumentNullException(nameof (var));
    this._numericUpDown = varnumericUpDown;
    this._refValue = var;
    this._numericUpDown.Value = (Decimal) this._refValue.Value;
    this._numericUpDown.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this._numericUpDown.KeyUp += new KeyEventHandler(this._numericUpDown_KeyUp);
    this._refValue.ValueChanged += new EventHandler<EventArgs<float>>(this.RefValue_ValueChanged);
  }

  private void _numericUpDown_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyData != Keys.Return)
      return;
    e.SuppressKeyPress = false;
  }

  private void RefValue_ValueChanged(object sender, EventArgs<float> e)
  {
    float num = this.UpdateBox(e.Value);
    if ((double) Convert.ToSingle(e.Value) == (double) Convert.ToSingle(this._numericUpDown.Value))
      return;
    this._numericUpDown.Value = (Decimal) num;
  }

  private void NumericUpDown_ValueChanged(object sender, EventArgs e)
  {
    this._refValue.Value = this.UpdateBox(Convert.ToSingle((sender as NumericUpDown).Value));
    (sender as NumericUpDown).Focus();
  }

  private float UpdateBox(float value)
  {
    this._numericUpDown.Text = value.ToString();
    return value;
  }
}
