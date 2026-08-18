
// Type: Intermech.Expressions.ParserTexBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Expressions.Exceptions;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Expressions;

public class ParserTexBox : TextBox
{
  private ParseException _exception;
  private Rectangle _errorRect;
  private ToolTip _toolTip;
  private const int WM_PAINT = 15;
  private const int EM_POSFROMCHAR = 214;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 15)
    {
      base.WndProc(ref m);
      this.OnWmPaint();
      m.Result = IntPtr.Zero;
    }
    else
      base.WndProc(ref m);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._toolTip != null)
    {
      this._toolTip.Dispose();
      this._toolTip = (ToolTip) null;
    }
    base.Dispose(disposing);
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    this._toolTip = new ToolTip();
  }

  public ParseException Exception
  {
    get => this._exception;
    set
    {
      this._exception = value;
      if (this._exception == null)
        this._errorRect = Rectangle.Empty;
      this.Invalidate();
    }
  }

  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    string caption = string.Empty;
    if (this._exception != null && this._errorRect.Contains(e.X, e.Y))
      caption = this._exception.Message;
    if (!(this._toolTip.GetToolTip((Control) this) != caption))
      return;
    this._toolTip.SetToolTip((Control) this, caption);
  }

  private void OnWmPaint()
  {
    if (this._exception != null)
    {
      if (this._exception.InvalidCharacterPosition == -1)
        return;
      int num1 = this.Text.Length - 1;
      int wParam = this._exception.InvalidCharacterPosition;
      if (wParam > num1)
        wParam = num1;
      int num2 = ParserTexBox.SendMessage(this.Handle, 214, wParam, 0);
      if (num2 == -1)
        return;
      int num3 = num2 & (int) ushort.MaxValue;
      int y = num2 >> 16 /*0x10*/ & (int) ushort.MaxValue;
      using (Graphics graphics = Graphics.FromHwnd(this.Handle))
      {
        using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent60, this.BackColor, Color.Red))
        {
          using (Pen pen = new Pen((Brush) hatchBrush))
          {
            string text = this._exception.Token;
            if (text.Length == 0)
              text = "*";
            SizeF sizeF = graphics.MeasureString(text, this.Font);
            Rectangle displayRectangle = this.DisplayRectangle;
            pen.Width = 3f;
            this._errorRect = new Rectangle(num3, y, (int) sizeF.Width, (int) sizeF.Height);
            int num4 = this._errorRect.Bottom - 2;
            graphics.DrawLine(pen, num3, num4, num3 + (int) sizeF.Width, num4);
          }
        }
      }
    }
    else
      this._errorRect = Rectangle.Empty;
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
}
