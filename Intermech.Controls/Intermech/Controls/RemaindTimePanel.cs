
// Type: Intermech.Controls.RemaindTimePanel
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Controls;

public class RemaindTimePanel : Control
{
  private string _text = string.Empty;
  private StringFormat _stringFormat;
  private ColorProgressBar _cpb;
  private ElapsedTimePanel _etp;

  public RemaindTimePanel()
  {
    this.Size = new Size(150, 15);
    this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this._stringFormat = new StringFormat();
    this._stringFormat.Alignment = StringAlignment.Center;
    this._stringFormat.LineAlignment = StringAlignment.Center;
  }

  public void Attach(ColorProgressBar progressBar, ElapsedTimePanel elapsedPanel)
  {
    this._cpb = progressBar;
    this._etp = elapsedPanel;
    if (this._cpb == null)
      return;
    this._cpb.Changed += new EventHandler(this.Data_Changed);
  }

  private void Data_Changed(object sender, EventArgs e)
  {
    if (this._cpb == null || this._etp == null)
      return;
    double num1 = (double) this._cpb.Value / (double) this._cpb.Maximum * 100.0;
    if (num1 > 2.0)
    {
      double num2 = this._etp.Elapsed.TotalMilliseconds / num1;
      int num3 = (int) ((100.0 - num1) * num2);
      StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/);
      if (num3 < 1000)
        num3 = 1000;
      TimeSpan timeSpan = TimeSpan.FromMilliseconds((double) num3);
      string str = $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
      if (!(this._text != str))
        return;
      this._text = str;
      this.Invalidate();
    }
    else
      this._text = string.Empty;
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    if (this._stringFormat != null)
    {
      this._stringFormat.Dispose();
      this._stringFormat = (StringFormat) null;
    }
    if (this._cpb != null)
      this._cpb.Changed -= new EventHandler(this.Data_Changed);
    if (this._etp == null)
      return;
    this._etp.Changed -= new EventHandler(this.Data_Changed);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (this._text.Length <= 0)
      return;
    using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
      e.Graphics.DrawString(this._text, this.Font, (Brush) solidBrush, (RectangleF) this.ClientRectangle, this._stringFormat);
  }

  [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
  public static extern int StrFromTimeInterval(
    ref StringBuilder pszOut,
    int cchMax,
    int dwTimeMS,
    int digits);
}
