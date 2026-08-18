
// Type: Intermech.Client.Core.AlphaNumericUpDown
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Инициализация изменения прозрачность= 0-255(0 - нет заливки)</summary>
public class AlphaNumericUpDown : IDisposable
{
  /// <summary>ссылка на NumericUpDown</summary>
  protected NumericUpDown _numericUpDown;
  /// <summary>ссылка на переменную прозрачность= 0-255(0 - нет заливки)</summary>
  protected Rclass<int> _alpha;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._numericUpDown != null)
    {
      this._numericUpDown.ValueChanged -= new EventHandler(this.NumericUpDown_ValueChanged);
      this._numericUpDown.KeyUp -= new KeyEventHandler(this.NumericUpDown_KeyUp);
      this._numericUpDown = (NumericUpDown) null;
    }
    if (this._alpha == null)
      return;
    this._alpha.ValueChanged -= new EventHandler<EventArgs<int>>(this.Alpha_ValueChanged);
    this._alpha = (Rclass<int>) null;
  }

  /// <summary>Инициализация изменения прозрачности</summary>
  /// <param name="varnumericUpDown"></param>
  /// <param name="varAlpha"></param>
  public void Initialize(NumericUpDown varnumericUpDown, Rclass<int> varAlpha)
  {
    if (varnumericUpDown == null)
      throw new ArgumentNullException(nameof (varnumericUpDown));
    if (varAlpha == null)
      throw new ArgumentNullException(nameof (varAlpha));
    this._numericUpDown = varnumericUpDown;
    this._alpha = varAlpha;
    this._numericUpDown.Value = (Decimal) this._alpha.Value;
    this._numericUpDown.ValueChanged += new EventHandler(this.NumericUpDown_ValueChanged);
    this._numericUpDown.KeyUp += new KeyEventHandler(this.NumericUpDown_KeyUp);
    this._alpha.ValueChanged += new EventHandler<EventArgs<int>>(this.Alpha_ValueChanged);
  }

  private void NumericUpDown_KeyUp(object sender, KeyEventArgs e)
  {
    this._alpha.Value = this.UpdateBox(Convert.ToInt32((sender as NumericUpDown).Value));
    (sender as NumericUpDown).Focus();
  }

  private void NumericUpDown_ValueChanged(object sender, EventArgs e)
  {
    this._alpha.Value = this.UpdateBox(Convert.ToInt32((sender as NumericUpDown).Value));
    (sender as NumericUpDown).Focus();
  }

  private void Alpha_ValueChanged(object sender, EventArgs<int> e)
  {
    int num = this.UpdateBox(e.Value);
    if (e.Value == Convert.ToInt32(this._numericUpDown.Value))
      return;
    this._numericUpDown.Value = (Decimal) num;
  }

  private int UpdateBox(int alpha)
  {
    alpha = alpha < 0 ? 0 : (alpha > (int) byte.MaxValue ? (int) byte.MaxValue : alpha);
    this._numericUpDown.Text = alpha.ToString();
    return alpha;
  }
}
