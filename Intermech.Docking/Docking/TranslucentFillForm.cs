
// Type: Intermech.Docking.TranslucentFillForm
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class TranslucentFillForm : Form
{
  private const int _a = 2;
  private const int _b = 524288 /*0x080000*/;
  private const int _c = 16 /*0x10*/;
  private const int _d = 64 /*0x40*/;
  private const int _e = 2;
  private const int _f = 1;
  private bool _paintBorders;

  public TranslucentFillForm(bool paintBorders)
  {
    this._paintBorders = paintBorders;
    this.BackColor = SystemColors.Highlight;
    this.ShowInTaskbar = false;
  }

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.Style = int.MinValue;
      createParams.ExStyle |= 524288 /*0x080000*/;
      return createParams;
    }
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    Win32.SetLayeredWindowAttributes(this.Handle, 0, (byte) 128 /*0x80*/, 2);
  }

  protected override void OnPaint(PaintEventArgs pea)
  {
    base.OnPaint(pea);
    if (!this._paintBorders)
      return;
    Rectangle clientRectangle = this.ClientRectangle;
    --clientRectangle.Width;
    --clientRectangle.Height;
    pea.Graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
    clientRectangle.Inflate(-1, -1);
    pea.Graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
  }

  public void ShowNoActivate(Rectangle A_0, bool A_1)
  {
    Win32.SetWindowPos(this.Handle, 0, A_0.X, A_0.Y, A_0.Width, A_0.Height, 80 /*0x50*/);
  }
}
