// Decompiled with JetBrains decompiler
// Type: Intermech.Paint.RichTextBoxAdv
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Paint;

public class RichTextBoxAdv : RichTextBox
{
  private const int EM_FORMATRANGE = 1081;
  private const int WS_EX_TRANSPARENT = 32 /*0x20*/;
  private const double AnInch = 14.4;

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      if (Kernel32.TryLoadLibrary("msftedit.dll", out IntPtr _, out Exception _))
      {
        createParams.ExStyle |= 32 /*0x20*/;
        createParams.ClassName = "RICHEDIT50W";
      }
      return createParams;
    }
  }

  public void Draw([NotNull] Graphics graphics, Rectangle layoutArea)
  {
    Interop.RECT rect = new Interop.RECT()
    {
      Top = (int) ((double) layoutArea.Top * 14.4),
      Bottom = (int) ((double) layoutArea.Bottom * 14.4),
      Left = (int) ((double) layoutArea.Left * 14.4),
      Right = (int) ((double) layoutArea.Right * 14.4)
    };
    IntPtr hdc = graphics.GetHdc();
    RichTextBoxAdv.FORMATRANGE structure;
    structure.chrg.cpMax = -1;
    structure.chrg.cpMin = 0;
    structure.hdc = hdc;
    structure.hdcTarget = hdc;
    structure.rc = rect;
    structure.rcPage = rect;
    IntPtr wParam = new IntPtr(1);
    IntPtr num = Marshal.AllocCoTaskMem(Marshal.SizeOf<RichTextBoxAdv.FORMATRANGE>(structure));
    Marshal.StructureToPtr<RichTextBoxAdv.FORMATRANGE>(structure, num, false);
    Intermech.WindowsDll.User32.SendMessage(this.Handle, 1081, wParam, num);
    Marshal.FreeCoTaskMem(num);
    graphics.ReleaseHdc(hdc);
  }

  public struct CHARRANGE
  {
    public int cpMin;
    public int cpMax;
  }

  public struct FORMATRANGE
  {
    public IntPtr hdc;
    public IntPtr hdcTarget;
    public Interop.RECT rc;
    public Interop.RECT rcPage;
    public RichTextBoxAdv.CHARRANGE chrg;
  }
}
