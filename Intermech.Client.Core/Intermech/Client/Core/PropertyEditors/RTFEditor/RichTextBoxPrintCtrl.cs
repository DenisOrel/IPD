
// Type: Intermech.Client.Core.PropertyEditors.RTFEditor.RichTextBoxPrintCtrl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.PropertyEditors.RTFEditor;

public class RichTextBoxPrintCtrl : RichTextBox
{
  private const double anInch = 14.4;
  private const int WM_USER = 1024 /*0x0400*/;
  private const int EM_FORMATRANGE = 1081;

  [DllImport("USER32.dll")]
  private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

  public int Print(int charFrom, int charTo, PrintPageEventArgs e)
  {
    RichTextBoxPrintCtrl.RECT rect1;
    rect1.Top = (int) ((double) e.MarginBounds.Top * 14.4);
    rect1.Bottom = (int) ((double) e.MarginBounds.Bottom * 14.4);
    rect1.Left = (int) ((double) e.MarginBounds.Left * 14.4);
    rect1.Right = (int) ((double) e.MarginBounds.Right * 14.4);
    RichTextBoxPrintCtrl.RECT rect2;
    rect2.Top = (int) ((double) e.PageBounds.Top * 14.4);
    rect2.Bottom = (int) ((double) e.PageBounds.Bottom * 14.4);
    rect2.Left = (int) ((double) e.PageBounds.Left * 14.4);
    rect2.Right = (int) ((double) e.PageBounds.Right * 14.4);
    IntPtr hdc = e.Graphics.GetHdc();
    RichTextBoxPrintCtrl.FORMATRANGE structure;
    structure.chrg.cpMax = charTo;
    structure.chrg.cpMin = charFrom;
    structure.hdc = hdc;
    structure.hdcTarget = hdc;
    structure.rc = rect1;
    structure.rcPage = rect2;
    IntPtr zero1 = IntPtr.Zero;
    IntPtr wp = IntPtr.Zero;
    wp = new IntPtr(1);
    IntPtr zero2 = IntPtr.Zero;
    IntPtr num1 = Marshal.AllocCoTaskMem(Marshal.SizeOf<RichTextBoxPrintCtrl.FORMATRANGE>(structure));
    Marshal.StructureToPtr<RichTextBoxPrintCtrl.FORMATRANGE>(structure, num1, false);
    IntPtr num2 = RichTextBoxPrintCtrl.SendMessage(this.Handle, 1081, wp, num1);
    Marshal.FreeCoTaskMem(num1);
    e.Graphics.ReleaseHdc(hdc);
    return num2.ToInt32();
  }

  private struct RECT
  {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
  }

  private struct CHARRANGE
  {
    public int cpMin;
    public int cpMax;
  }

  private struct FORMATRANGE
  {
    public IntPtr hdc;
    public IntPtr hdcTarget;
    public RichTextBoxPrintCtrl.RECT rc;
    public RichTextBoxPrintCtrl.RECT rcPage;
    public RichTextBoxPrintCtrl.CHARRANGE chrg;
  }
}
