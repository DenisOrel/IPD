
// Type: Intermech.Client.Core.AlphaTrackBar
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Инициализация изменения прозрачность= 1-255</summary>
public class AlphaTrackBar : IDisposable
{
  /// <summary>ссылка на NumericUpDown</summary>
  protected TrackBar _trackBar;
  /// <summary>ссылка на переменную прозрачность= 1-255</summary>
  protected Rclass<int> _alpha;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._trackBar != null)
    {
      this._trackBar.ValueChanged -= new EventHandler(this.TrackBar_ValueChanged);
      this._trackBar.KeyUp -= new KeyEventHandler(this.TrackBar_KeyUp);
      this._trackBar = (TrackBar) null;
    }
    if (this._alpha == null)
      return;
    this._alpha.ValueChanged -= new EventHandler<EventArgs<int>>(this.Alpha_ValueChanged);
    this._alpha = (Rclass<int>) null;
  }

  /// <summary>Инициализация изменения прозрачности</summary>
  /// <param name="vartrackBar"></param>
  /// <param name="varAlpha"></param>
  public void Initialize(TrackBar vartrackBar, Rclass<int> varAlpha)
  {
    if (vartrackBar == null)
      throw new ArgumentNullException(nameof (vartrackBar));
    if (varAlpha == null)
      throw new ArgumentNullException(nameof (varAlpha));
    this._trackBar = vartrackBar;
    this._alpha = varAlpha;
    this._trackBar.Value = this._alpha.Value;
    this._trackBar.ValueChanged += new EventHandler(this.TrackBar_ValueChanged);
    this._trackBar.KeyUp += new KeyEventHandler(this.TrackBar_KeyUp);
    this._alpha.ValueChanged += new EventHandler<EventArgs<int>>(this.Alpha_ValueChanged);
  }

  private void TrackBar_KeyUp(object sender, KeyEventArgs e)
  {
    this._alpha.Value = this.UpdateBox(Convert.ToInt32((sender as TrackBar).Value));
    (sender as NumericUpDown).Focus();
  }

  private void TrackBar_ValueChanged(object sender, EventArgs e)
  {
    this._alpha.Value = this.UpdateBox(Convert.ToInt32((sender as TrackBar).Value));
    (sender as NumericUpDown).Focus();
  }

  private void Alpha_ValueChanged(object sender, EventArgs<int> e)
  {
    int num = this.UpdateBox(e.Value);
    if (e.Value == Convert.ToInt32(this._trackBar.Value))
      return;
    this._trackBar.Value = num;
  }

  private int UpdateBox(int alpha)
  {
    alpha = alpha < 0 ? 0 : (alpha > (int) byte.MaxValue ? (int) byte.MaxValue : alpha);
    this._trackBar.Text = alpha.ToString();
    return alpha;
  }
}
