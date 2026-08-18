// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.COp
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class COp : CFct
{
  internal const int ASPECTX = 40;
  internal const int ASPECTXY = 44;
  internal const int ASPECTY = 42;
  internal const byte BALTIC_CHARSET = 186;
  internal const int BITSPIXEL = 12;
  internal const byte CHINESEBIG5_CHARSET = 136;
  internal const byte CLIP_DEFAULT_PRECIS = 0;
  internal const byte DEFAULT_PITCH = 0;
  internal const int DMPAPER_10X14 = 16 /*0x10*/;
  internal const int DMPAPER_11X17 = 17;
  internal const int DMPAPER_A3 = 8;
  internal const int DMPAPER_A4 = 9;
  internal const int DMPAPER_A4SMALL = 10;
  internal const int DMPAPER_A5 = 11;
  internal const int DMPAPER_B4 = 12;
  internal const int DMPAPER_B5 = 13;
  internal const int DMPAPER_CSHEET = 24;
  internal const int DMPAPER_DSHEET = 25;
  internal const int DMPAPER_ENV_10 = 20;
  internal const int DMPAPER_ENV_11 = 21;
  internal const int DMPAPER_ENV_12 = 22;
  internal const int DMPAPER_ENV_14 = 23;
  internal const int DMPAPER_ENV_9 = 19;
  internal const int DMPAPER_ENV_B4 = 33;
  internal const int DMPAPER_ENV_B5 = 34;
  internal const int DMPAPER_ENV_B6 = 35;
  internal const int DMPAPER_ENV_C3 = 29;
  internal const int DMPAPER_ENV_C4 = 30;
  internal const int DMPAPER_ENV_C5 = 28;
  internal const int DMPAPER_ENV_C6 = 31 /*0x1F*/;
  internal const int DMPAPER_ENV_C65 = 32 /*0x20*/;
  internal const int DMPAPER_ENV_DL = 27;
  internal const int DMPAPER_ENV_ITALY = 36;
  internal const int DMPAPER_ENV_MONARCH = 37;
  internal const int DMPAPER_ENV_PERSONAL = 38;
  internal const int DMPAPER_ESHEET = 26;
  internal const int DMPAPER_EXECUTIVE = 7;
  internal const int DMPAPER_FANFOLD_LGL_GERMAN = 41;
  internal const int DMPAPER_FANFOLD_STD_GERMAN = 40;
  internal const int DMPAPER_FANFOLD_US = 39;
  internal const int DMPAPER_FOLIO = 14;
  internal const int DMPAPER_LEDGER = 4;
  internal const int DMPAPER_LEGAL = 5;
  internal const int DMPAPER_LETTER = 1;
  internal const int DMPAPER_LETTERSMALL = 2;
  internal const int DMPAPER_NOTE = 18;
  internal const int DMPAPER_QUARTO = 15;
  internal const int DMPAPER_STATEMENT = 6;
  internal const int DMPAPER_TABLOID = 3;
  internal const byte DRAFT_QUALITY = 0;
  internal const int DSTINVERT = 5570569;
  internal const int DT_METAFILE = 5;
  internal const int DT_RASDISPLAY = 1;
  internal const int DT_RASPRINTER = 2;
  internal const byte EASTEUROPE_CHARSET = 238;
  internal const int EM_REPLACESEL = 194;
  internal const int EM_SCROLLCARET = 183;
  internal const int EM_SETSEL = 177;
  internal const int ETO_CLIPPED = 4;
  internal const int ETO_GLYPH_INDEX = 16 /*0x10*/;
  internal const int ETO_OPAQUE = 2;
  internal const int ETO_RTLREADING = 128 /*0x80*/;
  internal const byte FF_DECORATIVE = 80 /*0x50*/;
  internal const byte FF_DONTCARE = 0;
  internal const byte FF_MODERN = 48 /*0x30*/;
  internal const byte FF_ROMAN = 16 /*0x10*/;
  internal const byte FF_SCRIPT = 64 /*0x40*/;
  internal const byte FF_SWISS = 32 /*0x20*/;
  internal const int FLI_GLYPHS = 262144 /*0x040000*/;
  internal const int FLI_MASK = 4155;
  internal const int FW_BOLD = 700;
  internal const int FW_REGULAR = 400;
  internal const byte GB2312_CHARSET = 134;
  internal const int GCP_CLASSIN = 524288 /*0x080000*/;
  internal const int GCP_DBCS = 1;
  internal const int GCP_DIACRITIC = 256 /*0x0100*/;
  internal const int GCP_DISPLAYZWG = 4194304 /*0x400000*/;
  internal const int GCP_ERROR = 32768 /*0x8000*/;
  internal const int GCP_GLYPHSHAPE = 16 /*0x10*/;
  internal const int GCP_JUSTIFY = 65536 /*0x010000*/;
  internal const int GCP_JUSTIFYIN = 2097152 /*0x200000*/;
  internal const int GCP_KASHIDA = 1024 /*0x0400*/;
  internal const int GCP_LIGATE = 32 /*0x20*/;
  internal const int GCP_MAXEXTENT = 1048576 /*0x100000*/;
  internal const int GCP_REORDER = 2;
  internal const int GCP_USEKERNING = 8;
  internal const int GCPCLASS_ARABIC = 2;
  internal const int GCPCLASS_HEBREW = 2;
  internal const int GCPCLASS_LATIN = 1;
  internal const int GCPCLASS_NEUTRAL = 3;
  internal const int GETPRINTINGOFFSET = 13;
  internal const byte GREEK_CHARSET = 161;
  internal const int GW_CHILD = 5;
  internal const int GWL_EXSTYLE = -20;
  internal const int GWL_STYLE = -16;
  internal const byte HANGUL_CHARSET = 129;
  internal const int HORZRES = 8;
  internal const int LOGPIXELSX = 88;
  internal const int LOGPIXELSY = 90;
  internal const byte MAC_CHARSET = 77;
  internal const int MAX_DEFAULTCHAR = 2;
  internal const int MAX_LEADBYTES = 12;
  internal const int MB_COMPOSITE = 2;
  internal const int MB_PRECOMPOSED = 1;
  internal const int MB_USEGLYPHCHARS = 4;
  internal const int MF_CHECKED = 8;
  internal const int MF_DISABLED = 2;
  internal const int MF_ENABLED = 0;
  internal const int MF_GRAYED = 1;
  internal const int MF_UNCHECKED = 0;
  internal const int MM_ANISOTROPIC = 8;
  internal const int NULL_BRUSH = 5;
  internal const int NULL_PEN = 8;
  internal const byte OEM_CHARSET = 255 /*0xFF*/;
  internal const int OPAQUE = 2;
  internal const byte OUT_DEFAULT_PRECIS = 0;
  internal const byte OUT_TT_ONLY_PRECIS = 7;
  internal const int PATINVERT = 5898313;
  internal const int PM_NOREMOVE = 0;
  internal const int PM_NOYIELD = 2;
  internal const int PM_REMOVE = 1;
  internal const int PS_DASH = 1;
  internal const int PS_DASHDOT = 3;
  internal const int PS_DASHDOTDOT = 4;
  internal const int PS_DOT = 2;
  internal const int PS_SOLID = 0;
  internal const int R2_NOTXORPEN = 10;
  internal const byte RUSSIAN_CHARSET = 204;
  internal const int SB_BOTH = 3;
  internal const int SB_BOTTOM = 7;
  internal const int SB_CTL = 2;
  internal const int SB_ENDSCROLL = 8;
  internal const int SB_HORZ = 0;
  internal const int SB_LEFT = 6;
  internal const int SB_LINEDOWN = 1;
  internal const int SB_LINELEFT = 0;
  internal const int SB_LINERIGHT = 1;
  internal const int SB_LINEUP = 0;
  internal const int SB_PAGEDOWN = 3;
  internal const int SB_PAGELEFT = 2;
  internal const int SB_PAGERIGHT = 3;
  internal const int SB_PAGEUP = 2;
  internal const int SB_RIGHT = 7;
  internal const int SB_THUMBPOSITION = 4;
  internal const int SB_THUMBTRACK = 5;
  internal const int SB_TOP = 6;
  internal const int SB_VERT = 1;
  internal const byte SHIFTJIS_CHARSET = 128 /*0x80*/;
  internal const int SIF_ALL = 23;
  internal const int SIF_DISABLENOSCROLL = 8;
  internal const int SIF_PAGE = 2;
  internal const int SIF_POS = 4;
  internal const int SIF_RANGE = 1;
  internal const int SIF_TRACKPOS = 16 /*0x10*/;
  internal const int SM_CYVSCROLL = 20;
  internal const int SM_CYVTHUMB = 9;
  internal const int SRCCOPY = 13369376;
  internal const int SWP_DRAWFRAME = 32 /*0x20*/;
  internal const int SWP_FRAMECHANGED = 32 /*0x20*/;
  internal const int SWP_HIDEWINDOW = 128 /*0x80*/;
  internal const int SWP_NOACTIVATE = 16 /*0x10*/;
  internal const int SWP_NOCOPYBITS = 256 /*0x0100*/;
  internal const int SWP_NOMOVE = 2;
  internal const int SWP_NOOWNERZORDER = 512 /*0x0200*/;
  internal const int SWP_NOREDRAW = 8;
  internal const int SWP_NOSENDCHANGING = 1024 /*0x0400*/;
  internal const int SWP_NOSIZE = 1;
  internal const int SWP_NOZORDER = 4;
  internal const int SWP_SHOWWINDOW = 64 /*0x40*/;
  internal const int TCI_SRCCHARSET = 1;
  internal const int TCI_SRCCODEPAGE = 2;
  internal const int TECHNOLOGY = 2;
  internal const int TMPF_TRUETYPE = 4;
  internal const int TRANSPARENT = 1;
  internal const byte TURKISH_CHARSET = 162;
  internal const int VERTRES = 10;
  internal const int VERTSIZE = 6;
  internal const int VK_BACK = 8;
  internal const int VK_CONTROL = 17;
  internal const int VK_ESCAPE = 27;
  internal const int VK_RETURN = 13;
  internal const int VK_SHIFT = 16 /*0x10*/;
  internal const int VK_TAB = 9;
  internal const int WM_ACTIVATE = 6;
  internal const int WM_ACTIVATEAPP = 28;
  internal const int WM_CANCELMODE = 31 /*0x1F*/;
  internal const int WM_CHAR = 258;
  internal const int WM_CHILDACTIVATE = 34;
  internal const int WM_CLOSE = 16 /*0x10*/;
  internal const int WM_COMMAND = 273;
  internal const int WM_CREATE = 1;
  internal const int WM_DEADCHAR = 259;
  internal const int WM_DESTROY = 2;
  internal const int WM_DEVMODECHANGE = 27;
  internal const int WM_DROPFILES = 563;
  internal const int WM_ENABLE = 10;
  internal const int WM_ENDSESSION = 22;
  internal const int WM_ERASEBKGND = 20;
  internal const int WM_FONTCHANGE = 29;
  internal const int WM_GETTEXT = 13;
  internal const int WM_GETTEXTLENGTH = 14;
  internal const int WM_HSCROLL = 276;
  internal const int WM_IME_COMPOSITION = 271;
  internal const int WM_IME_ENDCOMPOSITION = 270;
  internal const int WM_IME_KEYLAST = 271;
  internal const int WM_IME_STARTCOMPOSITION = 269;
  internal const int WM_INITDIALOG = 272;
  internal const int WM_INITMENU = 278;
  internal const int WM_KEYDOWN = 256 /*0x0100*/;
  internal const int WM_KEYFIRST = 256 /*0x0100*/;
  internal const int WM_KEYLAST = 264;
  internal const int WM_KEYUP = 257;
  internal const int WM_KILLFOCUS = 8;
  internal const int WM_LBUTTONDBLCLK = 515;
  internal const int WM_LBUTTONDOWN = 513;
  internal const int WM_LBUTTONUP = 514;
  internal const int WM_MBUTTONDBLCLK = 521;
  internal const int WM_MBUTTONDOWN = 519;
  internal const int WM_MBUTTONUP = 520;
  internal const int WM_MOUSEACTIVATE = 33;
  internal const int WM_MOUSEFIRST = 512 /*0x0200*/;
  internal const int WM_MOUSELAST = 522;
  internal const int WM_MOUSEMOVE = 512 /*0x0200*/;
  internal const int WM_MOUSEWHEEL = 522;
  internal const int WM_MOVE = 3;
  internal const int WM_NCHITTEST = 132;
  internal const int WM_NCMOUSEMOVE = 160 /*0xA0*/;
  internal const int WM_PAINT = 15;
  internal const int WM_QUERYENDSESSION = 17;
  internal const int WM_QUERYOPEN = 19;
  internal const int WM_QUEUESYNC = 35;
  internal const int WM_QUIT = 18;
  internal const int WM_RBUTTONDBLCLK = 518;
  internal const int WM_RBUTTONDOWN = 516;
  internal const int WM_RBUTTONUP = 517;
  internal const int WM_SETCURSOR = 32 /*0x20*/;
  internal const int WM_SETFOCUS = 7;
  internal const int WM_SETREDRAW = 11;
  internal const int WM_SETTEXT = 12;
  internal const int WM_SHOWWINDOW = 24;
  internal const int WM_SIZE = 5;
  internal const int WM_SYSCHAR = 262;
  internal const int WM_SYSCOLORCHANGE = 21;
  internal const int WM_SYSCOMMAND = 274;
  internal const int WM_SYSDEADCHAR = 263;
  internal const int WM_SYSKEYDOWN = 260;
  internal const int WM_SYSKEYUP = 261;
  internal const int WM_TIMECHANGE = 30;
  internal const int WM_TIMER = 275;
  internal const int WM_VSCROLL = 277;
  internal const int WM_WININICHANGE = 26;
  internal const int WS_BORDER = 8388608 /*0x800000*/;
  internal const int WS_CAPTION = 12582912 /*0xC00000*/;
  internal const int WS_CHILD = 1073741824 /*0x40000000*/;
  internal const int WS_CLIPCHILDREN = 33554432 /*0x02000000*/;
  internal const int WS_CLIPSIBLINGS = 67108864 /*0x04000000*/;
  internal const int WS_DISABLED = 134217728 /*0x08000000*/;
  internal const int WS_DLGFRAME = 4194304 /*0x400000*/;
  internal const int WS_EX_CLIENTEDGE = 512 /*0x0200*/;
  internal const int WS_GROUP = 131072 /*0x020000*/;
  internal const int WS_HSCROLL = 1048576 /*0x100000*/;
  internal const int WS_MAXIMIZE = 16777216 /*0x01000000*/;
  internal const int WS_MAXIMIZEBOX = 65536 /*0x010000*/;
  internal const int WS_MINIMIZE = 536870912 /*0x20000000*/;
  internal const int WS_MINIMIZEBOX = 131072 /*0x020000*/;
  internal const int WS_OVERLAPPED = 0;
  internal const uint WS_POPUP = 2147483648 /*0x80000000*/;
  internal const int WS_SYSMENU = 524288 /*0x080000*/;
  internal const int WS_TABSTOP = 65536 /*0x010000*/;
  internal const int WS_THICKFRAME = 262144 /*0x040000*/;
  internal const int WS_VISIBLE = 268435456 /*0x10000000*/;
  internal const int WS_VSCROLL = 2097152 /*0x200000*/;

  internal COp(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal bool BitBlt(
    Graphics DestGr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    Graphics SrcGr,
    int SrcX,
    int SrcY,
    int rop)
  {
    IntPtr opDc = this.GetOpDC(DestGr);
    IntPtr num1 = SrcGr != DestGr ? SrcGr.GetHdc() : opDc;
    int num2 = COp.Win32.BitBlt(opDc, DestX, DestY, DestWidth, DestHeight, num1, SrcX, SrcY, rop) ? 1 : 0;
    this.ReleaseOpDC(DestGr);
    if (DestGr == SrcGr)
      return num2 != 0;
    SrcGr.ReleaseHdc(num1);
    return num2 != 0;
  }

  internal bool BitBlt(
    Graphics DestGr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    IntPtr hSrcDC,
    int SrcX,
    int SrcY,
    int rop)
  {
    int num = COp.Win32.BitBlt(this.GetOpDC(DestGr), DestX, DestY, DestWidth, DestHeight, hSrcDC, SrcX, SrcY, rop) ? 1 : 0;
    this.ReleaseOpDC(DestGr);
    return num != 0;
  }

  internal bool BitBlt(
    Graphics DestGr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    Graphics SrcGr,
    Bitmap SrcBM,
    int SrcX,
    int SrcY,
    int rop)
  {
    if (DestWidth == 0 || DestHeight == 0)
      return true;
    this.ReleaseOpDC(DestGr, true);
    Bitmap bitmap = SrcBM.Clone(new Rectangle(DestX, DestY, DestWidth, DestHeight), SrcBM.PixelFormat);
    Graphics graphics = Graphics.FromImage((Image) bitmap);
    IntPtr opDc = this.GetOpDC(DestGr);
    IntPtr hdc = graphics.GetHdc();
    IntPtr hbitmap = bitmap.GetHbitmap();
    IntPtr hgdiObj = COp.Win32.SelectObject(hdc, hbitmap);
    COp.Win32.BitBlt(hdc, 0, 0, DestWidth, DestHeight, hdc, 0, 0, 5570569);
    int num = COp.Win32.BitBlt(opDc, DestX, DestY, DestWidth, DestHeight, hdc, 0, 0, 13369376) ? 1 : 0;
    COp.Win32.SelectObject(hdc, hgdiObj);
    COp.Win32.DeleteObject(hbitmap);
    graphics.ReleaseHdc(hdc);
    this.ReleaseOpDC(DestGr);
    return num != 0;
  }

  internal bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int width, int height)
  {
    return COp.Win32.CreateCaret(hWnd, hBitmap, width, height);
  }

  internal IntPtr CreateFontIndirect(ref COp.LOGFONT lfont)
  {
    return COp.Win32.CreateFontIndirect(ref lfont);
  }

  internal IntPtr CreatePen(Pen pen)
  {
    int width = (int) pen.Width;
    int style = 0;
    if (pen.DashStyle == DashStyle.Dot)
      style = 2;
    else if (pen.DashStyle == DashStyle.Dash)
      style = 1;
    else if (pen.DashStyle == DashStyle.DashDot)
      style = 3;
    else if (pen.DashStyle == DashStyle.DashDotDot)
      style = 4;
    int colorRef = this.ToColorRef(pen.Color);
    return COp.Win32.CreatePen(style, width, colorRef);
  }

  internal bool DeleteObject(IntPtr handle) => COp.Win32.DeleteObject(handle);

  internal void DeleteOpGr(Graphics gr)
  {
    int opGr = this.FindOpGr(gr);
    this.ReleaseOpDC(gr, true);
    this.e.OpGr[opGr] = new tc.StrOpGr();
  }

  internal bool DestroyCaret() => COp.Win32.DestroyCaret();

  internal bool DPtoLP(Graphics gr, Point[] InPt)
  {
    int length = InPt.Length;
    COp.OP_POINT[] pt = new COp.OP_POINT[length];
    for (int index = 0; index < length; ++index)
    {
      pt[index].x = InPt[index].X;
      pt[index].y = InPt[index].Y;
    }
    bool flag = COp.Win32.DPtoLP(this.GetOpDC(gr), pt, length);
    for (int index = 0; index < length; ++index)
    {
      InPt[index].X = pt[index].x;
      InPt[index].Y = pt[index].y;
    }
    this.ReleaseOpDC(gr);
    return flag;
  }

  internal bool DrawHlSegs(Graphics DestGr)
  {
    IntPtr opDc = this.GetOpDC(DestGr);
    for (int index = 0; index < this.e.TotalHlSegs; ++index)
    {
      if (this.e.HlSeg[index].width != 0 && this.e.HlSeg[index].height != 0)
        COp.Win32.BitBlt(opDc, this.e.HlSeg[index].x, this.e.HlSeg[index].y, this.e.HlSeg[index].width, this.e.HlSeg[index].height, opDc, 0, 0, 5570569);
    }
    this.ReleaseOpDC(DestGr);
    return true;
  }

  internal int Ellipse(Graphics gr, bool fill, Color BrushColor, Pen pen, COp.RECT rect)
  {
    IntPtr zero = IntPtr.Zero;
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr hgdiObj1 = !fill ? COp.Win32.GetStockObject(5) : COp.Win32.CreateSolidBrush(this.ToColorRef(BrushColor));
    IntPtr hgdiObj2 = COp.Win32.SelectObject(opDc, hgdiObj1);
    IntPtr hgdiObj3 = pen != null ? this.CreatePen(pen) : COp.Win32.GetStockObject(8);
    IntPtr hgdiObj4 = COp.Win32.SelectObject(opDc, hgdiObj3);
    int num = COp.Win32.Ellipse(opDc, rect.left, rect.top, rect.right, rect.bottom);
    COp.Win32.SelectObject(opDc, hgdiObj2);
    if (fill)
      COp.Win32.DeleteObject(hgdiObj1);
    COp.Win32.SelectObject(opDc, hgdiObj4);
    if (pen != null)
      COp.Win32.DeleteObject(hgdiObj3);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal int ExtTextOut(
    Graphics gr,
    int x,
    int y,
    int options,
    char[] txt,
    int TextLen,
    int[] dx)
  {
    int opGr = this.FindOpGr(gr);
    IntPtr handle = IntPtr.Zero;
    IntPtr opDc = this.GetOpDC(opGr);
    COp.Win32.SetBkColor(opDc, this.ToColorRef(this.e.OpGr[opGr].BkColor));
    COp.Win32.SetTextColor(opDc, this.ToColorRef(this.e.OpGr[opGr].TextColor));
    COp.Win32.SetBkMode(opDc, this.e.OpGr[opGr].BkMode);
    if (this.True(this.e.OpGr[opGr].hFont))
      handle = this.SelectObject(opDc, this.e.OpGr[opGr].hFont);
    int num = COp.Win32.ExtTextOut(opDc, x, y, options, IntPtr.Zero, txt, TextLen, dx);
    if (this.True(this.e.OpGr[opGr].hFont))
      this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal int ExtTextOut(
    Graphics gr,
    int x,
    int y,
    int options,
    COp.RECT clip,
    char[] txt,
    int TextLen,
    int[] dx)
  {
    int opGr = this.FindOpGr(gr);
    if (TextLen == 0 && (options & 2) == 0)
      return 1;
    if (this.False(this.e.OpGr[opGr].hDC) && TextLen == 0)
    {
      if ((options & 2) != 0 && clip.right - clip.left != 0 && clip.bottom - clip.top != 0)
      {
        SolidBrush solidBrush = new SolidBrush(this.e.OpGr[opGr].BkColor);
        gr.FillRectangle((Brush) solidBrush, this.ToRectangle(clip));
        solidBrush.Dispose();
      }
      return 1;
    }
    IntPtr opDc = this.GetOpDC(opGr);
    IntPtr handle = IntPtr.Zero;
    IntPtr zero = IntPtr.Zero;
    COp.Win32.SetBkColor(opDc, this.ToColorRef(this.e.OpGr[opGr].BkColor));
    COp.Win32.SetTextColor(opDc, this.ToColorRef(this.e.OpGr[opGr].TextColor));
    COp.Win32.SetBkMode(opDc, this.e.OpGr[opGr].BkMode);
    if (this.True(this.e.OpGr[opGr].hFont))
      handle = this.SelectObject(opDc, this.e.OpGr[opGr].hFont);
    int num = COp.Win32.ExtTextOut(opDc, x, y, options, ref clip, txt, TextLen, dx);
    if (this.True(this.e.OpGr[opGr].hFont))
      this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal int FillRect(Graphics gr, Color BrushColor, ref COp.RECT rect)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr solidBrush = COp.Win32.CreateSolidBrush(this.ToColorRef(BrushColor));
    ref COp.RECT local = ref rect;
    IntPtr hBr = solidBrush;
    int num = COp.Win32.FillRect(opDc, ref local, hBr);
    COp.Win32.DeleteObject(solidBrush);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal int FindOpGr(Graphics gr)
  {
    for (int opGr = 0; opGr < this.e.TotalOpGrs; ++opGr)
    {
      if (this.e.OpGr[opGr].gr == gr)
        return opGr;
    }
    int opGrSlot = this.FindOpGrSlot();
    this.e.OpGr[opGrSlot].gr = gr;
    return opGrSlot;
  }

  internal int FindOpGrSlot()
  {
    if (this.e.MaxOpGrs == 0)
    {
      this.e.MaxOpGrs = 10;
      this.e.OpGr = new tc.StrOpGr[this.e.MaxOpGrs];
      for (int index = 0; index < this.e.MaxOpGrs; ++index)
        this.e.OpGr[index] = new tc.StrOpGr();
    }
    int opGrSlot = 0;
    while (opGrSlot < this.e.TotalOpGrs && this.e.OpGr[opGrSlot].gr != null)
      ++opGrSlot;
    if (opGrSlot >= this.e.TotalOpGrs)
    {
      if (this.e.TotalOpGrs >= this.e.MaxOpGrs)
      {
        int maxOpGrs = this.e.MaxOpGrs;
        this.e.MaxOpGrs = this.e.TotalOpGrs + 10;
        tc.StrOpGr[] strOpGrArray = new tc.StrOpGr[this.e.MaxOpGrs];
        for (int index = 0; index < this.e.TotalOpGrs; ++index)
          strOpGrArray[index] = this.e.OpGr[index];
        for (int totalOpGrs = this.e.TotalOpGrs; totalOpGrs < this.e.MaxOpGrs; ++totalOpGrs)
          strOpGrArray[totalOpGrs] = new tc.StrOpGr();
        this.e.OpGr = strOpGrArray;
      }
      opGrSlot = this.e.TotalOpGrs;
      ++this.e.TotalOpGrs;
    }
    this.e.OpGr[opGrSlot].BkColor = this.e.TerFont[0].TextBkColor;
    this.e.OpGr[opGrSlot].TextColor = this.e.TerFont[0].TextColor;
    this.e.OpGr[opGrSlot].hFont = this.e.TerFont[0].hFont;
    this.e.OpGr[opGrSlot].hDC = IntPtr.Zero;
    return opGrSlot;
  }

  internal IntPtr FindWindow(string ClsName, string WinName)
  {
    return COp.Win32.FindWindow(ClsName, WinName);
  }

  internal int GetACP() => CultureInfo.CurrentCulture.TextInfo.ANSICodePage;

  internal int GetBkMode(Graphics gr)
  {
    int bkMode = COp.Win32.GetBkMode(this.GetOpDC(gr));
    this.ReleaseOpDC(gr);
    return bkMode;
  }

  internal int GetCharacterPlacement(
    Graphics gr,
    char[] lpString,
    IntPtr hFont,
    int nCount,
    int nMaxExtent,
    ref COp.GCP_RESULTS lpResults,
    int dwFlags)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr handle = this.SelectObject(opDc, hFont);
    int characterPlacement = COp.Win32.GetCharacterPlacement(opDc, lpString, nCount, nMaxExtent, ref lpResults, dwFlags);
    this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return characterPlacement;
  }

  internal bool GetCharWidth(Graphics gr, Font font, int FirstChar, int LastChar, int[] width)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr hfont;
    IntPtr handle = this.SelectObject(opDc, hfont = font.ToHfont());
    int num = COp.Win32.GetCharWidth(opDc, FirstChar, LastChar, width) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.DeleteObject(hfont);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal bool GetCharWidth(Graphics gr, IntPtr hFont, int FirstChar, int LastChar, int[] width)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr handle = this.SelectObject(opDc, hFont);
    int num = COp.Win32.GetCharWidth(opDc, FirstChar, LastChar, width) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal string GetClassName(IntPtr hWnd)
  {
    char[] ClassName = new char[300];
    int className;
    return (className = COp.Win32.GetClassName(hWnd, ClassName, ClassName.Length)) == 0 ? "" : new string(ClassName, 0, className);
  }

  internal bool GetCPInfo(int CodePage, out COp.CPINFO lpCp)
  {
    return COp.Win32.GetCPInfo(CodePage, out lpCp);
  }

  internal bool GetCursorPos(out Point pt)
  {
    COp.OP_POINT pt1 = new COp.OP_POINT();
    int num = COp.Win32.GetCursorPos(ref pt1) ? 1 : 0;
    pt = new Point(pt1.x, pt1.y);
    return num != 0;
  }

  internal IntPtr GetDC(IntPtr hWnd) => COp.Win32.GetDC(hWnd);

  internal int GetDeviceCaps(Graphics gr, int id)
  {
    int deviceCaps = this.GetDeviceCaps(this.GetOpDC(gr), id);
    this.ReleaseOpDC(gr);
    return deviceCaps;
  }

  internal int GetDeviceCaps(IntPtr hDC, int id) => COp.Win32.GetDeviceCaps(hDC, id);

  internal int GetDlgCtrlID(IntPtr hWnd) => COp.Win32.GetDlgCtrlID(hWnd);

  internal int GetFontLanguageInfo(Graphics gr, Font font)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr hfont;
    IntPtr handle = this.SelectObject(opDc, hfont = font.ToHfont());
    int fontLanguageInfo = COp.Win32.GetFontLanguageInfo(opDc);
    this.SelectObject(opDc, handle);
    this.DeleteObject(hfont);
    this.ReleaseOpDC(gr);
    return fontLanguageInfo;
  }

  internal short GetKeyState(int VirtKey) => COp.Win32.GetKeyState(VirtKey);

  internal bool GetLogFont(IntPtr hFont, out COp.LOGFONT lf)
  {
    lf = new COp.LOGFONT();
    int size = COp.Win32.GetObject(hFont, 0, IntPtr.Zero);
    return size != 0 && COp.Win32.GetObject(hFont, size, out lf) != 0;
  }

  internal IntPtr GetOpDC(Graphics gr) => this.GetOpDC(this.FindOpGr(gr));

  internal IntPtr GetOpDC(int idx)
  {
    if (this.e.OpGr[idx].gr == this.e.BufGr)
    {
      this.e.OpGr[idx].hDC = this.e.hBufDC;
      COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, 0, 0, IntPtr.Zero);
      COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
    }
    else if (this.False(this.e.OpGr[idx].hDC))
    {
      Matrix matrix = (Matrix) null;
      if (this.e.InPrinting)
      {
        if (this.e.AllTextAngle2 != 0)
        {
          Matrix transformMatrix2 = this.e.TransformMatrix2;
        }
        this.e.OpGr[idx].hDC = this.e.OpGr[idx].gr.GetHdc();
        COp.Win32.SetMapMode(this.e.OpGr[idx].hDC, 8);
        if (this.e.InPrintPreview)
        {
          this.e.PrtWinExtCX = this.e.PvExtWidth;
          this.e.PrtWinExtCY = this.e.PvExtHeight;
          this.e.PrtVwExtCX = this.e.PvVpWidth;
          this.e.PrtVwExtCY = this.e.PvVpHeight;
          this.e.PrtVwOrgX = this.e.PvX;
          this.e.PrtVwOrgY = this.e.PvY;
          this.e.PrtWinOrgX = this.e.TerWinOrgX;
          this.e.PrtWinOrgY = this.e.TerWinOrgY;
        }
        else
        {
          this.e.PrtWinExtCX = 1440;
          this.e.PrtWinExtCY = 1440;
          this.e.PrtVwExtCX = this.e.PrtResX;
          this.e.PrtVwExtCY = this.e.PrtResY;
          this.e.PrtVwOrgX = this.e.PrtVpX;
          this.e.PrtVwOrgY = this.e.PrtVpY;
          this.e.PrtWinOrgX = 0;
          this.e.PrtWinOrgY = 0;
        }
        COp.Win32.SetWindowExtEx(this.e.OpGr[idx].hDC, this.e.PrtWinExtCX, this.e.PrtWinExtCY, IntPtr.Zero);
        COp.Win32.SetViewportExtEx(this.e.OpGr[idx].hDC, this.e.PrtVwExtCX, this.e.PrtVwExtCY, IntPtr.Zero);
        COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, this.e.PrtVwOrgX, this.e.PrtVwOrgY, IntPtr.Zero);
        COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.PrtWinOrgX, this.e.PrtWinOrgY, IntPtr.Zero);
        if (this.e.TransformMatrix != null)
        {
          float[] elements = this.e.TransformMatrix.Elements;
          COp.XFORM xform = new COp.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
          COp.Win32.SetGraphicsMode(this.e.OpGr[idx].hDC, 2);
          COp.Win32.SetWorldTransform(this.e.OpGr[idx].hDC, ref xform);
        }
      }
      else if (this.e.OpGr[idx].gr == this.e.TerGr)
      {
        if (this.e.AllTextAngle2 != 0)
          matrix = this.e.TransformMatrix2;
        this.e.OpGr[idx].hDC = this.e.OpGr[idx].gr.GetHdc();
        if (this.e.UseWin)
        {
          COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinRect.left, this.e.TerWinRect.top, IntPtr.Zero);
          COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
        }
        if (this.e.TransformMatrix != null)
        {
          float[] elements = this.e.TransformMatrix.Elements;
          COp.XFORM xform = new COp.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
          COp.Win32.SetGraphicsMode(this.e.OpGr[idx].hDC, 2);
          COp.Win32.SetWorldTransform(this.e.OpGr[idx].hDC, ref xform);
        }
        if (matrix != null)
        {
          float[] elements = matrix.Elements;
          COp.XFORM xform = new COp.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
          COp.Win32.SetGraphicsMode(this.e.OpGr[idx].hDC, 2);
          COp.Win32.SetWorldTransform(this.e.OpGr[idx].hDC, ref xform);
        }
      }
      else
        this.e.OpGr[idx].hDC = this.e.OpGr[idx].gr.GetHdc();
    }
    return this.e.OpGr[idx].hDC;
  }

  internal int GetPrinterHiddenArea(Graphics gr)
  {
    COp.OP_POINT pt = new COp.OP_POINT();
    this.e.HiddenX = this.e.HiddenY = 0;
    int printerHiddenArea = COp.Win32.Escape(this.GetOpDC(gr), 13, 0, IntPtr.Zero, ref pt);
    this.e.HiddenX = pt.x;
    this.e.HiddenY = pt.y;
    this.ReleaseOpDC(gr);
    return printerHiddenArea;
  }

  internal Color GetTextColor(Graphics gr)
  {
    int textColor = COp.Win32.GetTextColor(this.GetOpDC(gr));
    this.ReleaseOpDC(gr);
    return this.ToColor(textColor);
  }

  internal bool GetTextExtentPoint(
    Graphics gr,
    Font font,
    string str,
    int len,
    out COp.SIZE size)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr hfont;
    IntPtr handle = this.SelectObject(opDc, hfont = font.ToHfont());
    int num = COp.Win32.GetTextExtentPoint(opDc, str, len, out size) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.DeleteObject(hfont);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal bool GetTextExtentPoint(
    Graphics gr,
    IntPtr hFont,
    char[] str,
    int len,
    out COp.SIZE size)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr handle = this.SelectObject(opDc, hFont);
    int num = COp.Win32.GetTextExtentPoint(opDc, str, len, out size) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal bool GetTextExtentPoint(
    Graphics gr,
    IntPtr hFont,
    string str,
    int len,
    out COp.SIZE size)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr handle = this.SelectObject(opDc, hFont);
    int num = COp.Win32.GetTextExtentPoint(opDc, str, len, out size) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal bool GetTextMetrics(Graphics gr, Font font, out COp.TEXTMETRIC tm)
  {
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr hfont;
    IntPtr handle = this.SelectObject(opDc, hfont = font.ToHfont());
    int num = COp.Win32.GetTextMetrics(opDc, out tm) ? 1 : 0;
    this.SelectObject(opDc, handle);
    this.DeleteObject(hfont);
    this.ReleaseOpDC(gr);
    return num != 0;
  }

  internal bool GetFontMetrics(Graphics gr, Font font, out COp.OUTLINETEXTMETRIC ofm)
  {
    ofm = new COp.OUTLINETEXTMETRIC();
    if (font == null)
      return false;
    bool fontMetrics = false;
    IntPtr hfont = font.ToHfont();
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr handle = this.SelectObject(opDc, hfont);
    int outlineTextMetricsEx = COp.Win32.GetOutlineTextMetricsEx(opDc, 0, IntPtr.Zero);
    if (outlineTextMetricsEx != 0)
    {
      IntPtr num = Marshal.AllocHGlobal(outlineTextMetricsEx);
      if (COp.Win32.GetOutlineTextMetricsEx(opDc, outlineTextMetricsEx, num) != 0)
      {
        ofm = (COp.OUTLINETEXTMETRIC) Marshal.PtrToStructure(num, typeof (COp.OUTLINETEXTMETRIC));
        fontMetrics = true;
      }
      Marshal.FreeHGlobal(num);
    }
    this.SelectObject(opDc, handle);
    this.DeleteObject(hfont);
    this.ReleaseOpDC(gr);
    return fontMetrics;
  }

  internal int GetUserDefaultLangID() => CultureInfo.CurrentCulture.LCID;

  internal IntPtr GetWindow(IntPtr hWnd, int cmd) => COp.Win32.GetWindow(hWnd, cmd);

  internal string hex(int val) => $"{val:x}";

  internal bool HideCaret(IntPtr hWnd) => COp.Win32.HideCaret(hWnd);

  internal static ushort HIWORD(int val) => (ushort) (val >> 16 /*0x10*/);

  internal bool IsClipboardFormatAvailable(int fmt)
  {
    string fmt1 = (string) null;
    switch (fmt)
    {
      case 1:
        fmt1 = DataFormats.Text;
        break;
      case 2:
        fmt1 = DataFormats.Bitmap;
        break;
      case 3:
        fmt1 = DataFormats.MetafilePict;
        break;
      case 8:
        fmt1 = DataFormats.Dib;
        break;
      case 13:
        fmt1 = DataFormats.UnicodeText;
        break;
      default:
        if (fmt == this.e.RtfClipFormat)
        {
          fmt1 = DataFormats.Rtf;
          break;
        }
        if (fmt == this.e.SSClipInfo)
        {
          fmt1 = "SS Object Info";
          break;
        }
        if (fmt == this.e.CfEnhMetafile)
        {
          fmt1 = DataFormats.EnhancedMetafile;
          break;
        }
        break;
    }
    return fmt1 != null && this.IsClipboardFormatAvailable(fmt1);
  }

  internal bool IsClipboardFormatAvailable(string fmt)
  {
    return Array.IndexOf<string>(Clipboard.GetDataObject().GetFormats(), fmt) != -1;
  }

  internal bool IsSameColor(Color clr1, Color clr2)
  {
    return (int) clr1.R == (int) clr2.R && (int) clr1.B == (int) clr2.B && (int) clr1.G == (int) clr2.G;
  }

  internal bool IsSameColor(Color clr1, int clr2) => this.IsSameColor(clr1, this.ToColor(clr2));

  internal bool KillTimer(IntPtr hWnd, int id) => COp.Win32.KillTimer(hWnd, (IntPtr) id);

  internal bool LineTo(IntPtr hDC, int x, int y) => COp.Win32.LineTo(hDC, x, y);

  internal Bitmap LoadBitmap(string ResFileName, string ResName)
  {
    return (Bitmap) new ResourceManager(ResFileName, Assembly.GetExecutingAssembly()).GetObject(ResName, CultureInfo.InvariantCulture);
  }

  internal Cursor LoadCursor(string ResFileName, string ResName)
  {
    return (Cursor) new ResourceManager(ResFileName, this.GetType().Assembly).GetObject(ResName, CultureInfo.InvariantCulture);
  }

  internal static ushort LOWORD(int val) => (ushort) (val & (int) ushort.MaxValue);

  internal bool LPtoDP(Graphics gr, Point[] InPt)
  {
    int length = InPt.Length;
    COp.OP_POINT[] pt = new COp.OP_POINT[length];
    for (int index = 0; index < length; ++index)
    {
      pt[index].x = InPt[index].X;
      pt[index].y = InPt[index].Y;
    }
    bool flag = COp.Win32.LPtoDP(this.GetOpDC(gr), pt, length);
    for (int index = 0; index < length; ++index)
    {
      InPt[index].X = pt[index].x;
      InPt[index].Y = pt[index].y;
    }
    this.ReleaseOpDC(gr);
    return flag;
  }

  internal int MessageBeep(int n) => COp.Win32.MessageBeep(n);

  internal bool MoveToEx(IntPtr hDC, int x, int y, IntPtr pPoint)
  {
    return COp.Win32.MoveToEx(hDC, x, y, pPoint);
  }

  internal int MultiByteToWideChar(int CodePage, byte[] InChr, out string OutStr)
  {
    int length = InChr.Length;
    char[] OutStr1 = new char[length + 1];
    int wideChar = COp.Win32.MultiByteToWideChar(CodePage, 1, InChr, length, OutStr1, length + 1);
    OutStr = new string(OutStr1, 0, wideChar);
    return wideChar;
  }

  internal Color NewColor(Color color) => color;

  internal bool OldDrawHlSegs(Graphics DestGr)
  {
    if (!this.e.UseWin)
      return true;
    bool flag = true;
    this.ReleaseOpDC(DestGr, true);
    IntPtr hgdiObj1 = IntPtr.Zero;
    if (this.e.BufBM != null)
      hgdiObj1 = this.e.BufBM.GetHbitmap();
    IntPtr opDc = this.GetOpDC(DestGr);
    Bitmap bitmap = new Bitmap(10, 10);
    Graphics graphics = Graphics.FromImage((Image) bitmap);
    IntPtr hdc = graphics.GetHdc();
    IntPtr hgdiObj2 = COp.Win32.SelectObject(hdc, hgdiObj1);
    COp.Win32.SetViewportOrgEx(hdc, 0, 0, IntPtr.Zero);
    COp.Win32.SetWindowOrgEx(hdc, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
    for (int index = 0; index < this.e.TotalHlSegs; ++index)
    {
      if (this.e.HlSeg[index].width != 0 && this.e.HlSeg[index].height != 0)
      {
        COp.Win32.BitBlt(hdc, this.e.HlSeg[index].x, this.e.HlSeg[index].y, this.e.HlSeg[index].width, this.e.HlSeg[index].height, hdc, 0, 0, 5570569);
        flag = COp.Win32.BitBlt(opDc, this.e.HlSeg[index].x, this.e.HlSeg[index].y, this.e.HlSeg[index].width, this.e.HlSeg[index].height, hdc, this.e.HlSeg[index].x, this.e.HlSeg[index].y, 13369376);
      }
    }
    COp.Win32.SelectObject(hdc, hgdiObj2);
    COp.Win32.DeleteObject(hgdiObj1);
    graphics.ReleaseHdc(hdc);
    graphics.Dispose();
    bitmap.Dispose();
    this.ReleaseOpDC(DestGr);
    return flag;
  }

  internal IntPtr OldGetOpDC(int idx)
  {
    if (this.False(this.e.OpGr[idx].hDC))
    {
      this.e.OpGr[idx].hDC = this.e.OpGr[idx].gr.GetHdc();
      if (this.e.InPrinting)
      {
        COp.Win32.SetMapMode(this.e.OpGr[idx].hDC, 8);
        if (this.e.InPrintPreview)
        {
          COp.Win32.SetWindowExtEx(this.e.OpGr[idx].hDC, this.e.PvExtWidth, this.e.PvExtHeight, IntPtr.Zero);
          COp.Win32.SetViewportExtEx(this.e.OpGr[idx].hDC, this.e.PvVpWidth, this.e.PvVpHeight, IntPtr.Zero);
          COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, this.e.PvX, this.e.PvY, IntPtr.Zero);
          COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
        }
        else
        {
          COp.Win32.SetWindowExtEx(this.e.OpGr[idx].hDC, 1440, 1440, IntPtr.Zero);
          COp.Win32.SetViewportExtEx(this.e.OpGr[idx].hDC, this.e.PrtResX, this.e.PrtResY, IntPtr.Zero);
          COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, this.e.PrtVpX, this.e.PrtVpY, IntPtr.Zero);
          COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, 0, 0, IntPtr.Zero);
        }
      }
      else if (this.e.OpGr[idx].gr == this.e.BufGr)
      {
        COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, 0, 0, IntPtr.Zero);
        COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
      }
      else if (this.e.OpGr[idx].gr == this.e.TerGr)
      {
        COp.Win32.SetViewportOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinRect.left, this.e.TerWinRect.top, IntPtr.Zero);
        COp.Win32.SetWindowOrgEx(this.e.OpGr[idx].hDC, this.e.TerWinOrgX, this.e.TerWinOrgY, IntPtr.Zero);
      }
    }
    return this.e.OpGr[idx].hDC;
  }

  internal void OldReleaseOpDC(Graphics gr, bool force)
  {
    if (!(this.e.CloseDC | force))
      return;
    int opGr = this.FindOpGr(gr);
    if (!this.True(this.e.OpGr[opGr].hDC))
      return;
    gr.ReleaseHdc(this.e.OpGr[opGr].hDC);
    this.e.OpGr[opGr].hDC = IntPtr.Zero;
  }

  internal bool OpDCIsOpen(Graphics gr) => this.True(this.e.OpGr[this.FindOpGr(gr)].hDC);

  internal IntPtr OurSetFont(Graphics gr, IntPtr hFont)
  {
    int opGr = this.FindOpGr(gr);
    ref tc.StrOpGr local = ref this.e.OpGr[opGr];
    this.e.OpGr[opGr].hFont = hFont;
    return hFont;
  }

  internal bool PeekMessage(out COp.MSG msg, IntPtr hWnd, int MinMsg, int MaxMsg, int remove)
  {
    return COp.Win32.PeekMessage(out msg, hWnd, MinMsg, MaxMsg, remove);
  }

  internal bool Polygon(Graphics gr, Color color, Point[] InPt)
  {
    int length = InPt.Length;
    COp.OP_POINT[] pt = new COp.OP_POINT[length];
    for (int index = 0; index < length; ++index)
    {
      pt[index].x = InPt[index].X;
      pt[index].y = InPt[index].Y;
    }
    IntPtr opDc = this.GetOpDC(gr);
    IntPtr solidBrush = COp.Win32.CreateSolidBrush(this.ToColorRef(color));
    IntPtr hgdiObj = COp.Win32.SelectObject(opDc, solidBrush);
    COp.Win32.Polygon(opDc, pt, length);
    COp.Win32.SelectObject(opDc, hgdiObj);
    COp.Win32.DeleteObject(solidBrush);
    this.ReleaseOpDC(gr);
    return true;
  }

  internal bool PostMessage(IntPtr hWnd, int msg, int wParam, int lParam)
  {
    return COp.Win32.PostMessage(hWnd, msg, (IntPtr) wParam, (IntPtr) lParam);
  }

  internal int RegisterClipboardFormat(string name) => COp.Win32.RegisterClipboardFormat(name);

  internal int ReleaseDC(IntPtr hWnd, IntPtr hDC) => COp.Win32.ReleaseDC(hWnd, hDC);

  internal void ReleaseOpDC(Graphics gr) => this.ReleaseOpDC(gr, false);

  internal void ReleaseOpDC(Graphics gr, bool force)
  {
    if (!(this.e.CloseDC | force))
      return;
    int opGr = this.FindOpGr(gr);
    if (gr == this.e.BufGr || !(IntPtr.Zero != this.e.OpGr[opGr].hDC))
      return;
    gr.ReleaseHdc(this.e.OpGr[opGr].hDC);
    this.e.OpGr[opGr].hDC = IntPtr.Zero;
  }

  internal Color ReverseColor(Color color)
  {
    int r = (int) color.R;
    int b = (int) color.B;
    int g = (int) color.G;
    int red = (int) (byte) ~r;
    int num = (int) (byte) ~b;
    int green = (int) (byte) ~g;
    int blue = num;
    return Color.FromArgb(red, green, blue);
  }

  internal IntPtr SelectObject(IntPtr hDC, IntPtr handle) => COp.Win32.SelectObject(hDC, handle);

  internal bool SendMessage(IntPtr hWnd, int msg, int wParam, int lParam)
  {
    return COp.Win32.SendMessage(hWnd, msg, (IntPtr) wParam, (IntPtr) lParam);
  }

  internal bool SendMessage(IntPtr hWnd, int msg, int wParam, string str)
  {
    return COp.Win32.SendMessage(hWnd, msg, (IntPtr) wParam, str);
  }

  internal Color SetBkColor(Graphics gr, Color color)
  {
    int opGr = this.FindOpGr(gr);
    Color bkColor = this.e.OpGr[opGr].BkColor;
    this.e.OpGr[opGr].BkColor = color;
    return bkColor;
  }

  internal int SetBkMode(Graphics gr, int mode)
  {
    int opGr = this.FindOpGr(gr);
    int bkMode = this.e.OpGr[opGr].BkMode;
    this.e.OpGr[opGr].BkMode = mode;
    return bkMode;
  }

  internal bool SetCaretPos(int x, int y) => COp.Win32.SetCaretPos(x, y);

  internal Color SetPixel(Graphics gr, int x, int y, Color color)
  {
    int color1 = COp.Win32.SetPixel(this.GetOpDC(gr), x, y, this.ToColorRef(color));
    this.ReleaseOpDC(gr);
    return this.ToColor(color1);
  }

  internal int SetROP2(Graphics gr, int rop)
  {
    int num = COp.Win32.SetROP2(this.GetOpDC(gr), rop);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal int SetScrollInfo(IntPtr hWnd, int fnBar, ref COp.SCROLLINFO lpsi, bool fRedraw)
  {
    lpsi.cbSize = 28;
    return COp.Win32.SetScrollInfo(hWnd, fnBar, ref lpsi, fRedraw);
  }

  internal bool SetScrollPos(IntPtr hWnd, int fnBar, int pos, bool redraw)
  {
    return COp.Win32.SetScrollPos(hWnd, fnBar, pos, redraw);
  }

  internal bool SetScrollRange(IntPtr hWnd, int fnBar, int MinPos, int MaxPos, bool redraw)
  {
    return COp.Win32.SetScrollRange(hWnd, fnBar, MinPos, MaxPos, redraw);
  }

  internal int SetTextCharacterExtra(Graphics gr, int extra)
  {
    int num = COp.Win32.SetTextCharacterExtra(this.GetOpDC(gr), extra);
    this.ReleaseOpDC(gr);
    return num;
  }

  internal Color SetTextColor(Graphics gr, Color color)
  {
    int opGr = this.FindOpGr(gr);
    Color textColor = this.e.OpGr[opGr].TextColor;
    this.e.OpGr[opGr].TextColor = color;
    return textColor;
  }

  internal int SetTimer(IntPtr hWnd, int id, int elapse)
  {
    return (int) COp.Win32.SetTimer(hWnd, (IntPtr) id, elapse, IntPtr.Zero);
  }

  internal bool ShowCaret(IntPtr hWnd) => COp.Win32.ShowCaret(hWnd);

  internal bool ShowScrollBar(IntPtr hWnd, int type, bool show)
  {
    return COp.Win32.ShowScrollBar(hWnd, type, show);
  }

  internal bool StretchBlt(
    Graphics DestGr,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    Graphics SrcGr,
    int SrcX,
    int SrcY,
    int SrcWidth,
    int SrcHeight,
    int rop)
  {
    IntPtr num = IntPtr.Zero;
    bool flag1 = false;
    IntPtr opDc = this.GetOpDC(DestGr);
    if (DestGr != SrcGr)
    {
      try
      {
        num = SrcGr.GetHdc();
        flag1 = true;
      }
      catch (Exception ex)
      {
      }
      if (!flag1)
        num = this.GetOpDC(SrcGr);
    }
    else
      num = opDc;
    bool flag2 = COp.Win32.StretchBlt(opDc, DestX, DestY, DestWidth, DestHeight, num, SrcX, SrcY, SrcWidth, SrcHeight, rop);
    this.ReleaseOpDC(DestGr);
    if (DestGr != SrcGr)
    {
      if (flag1)
      {
        SrcGr.ReleaseHdc(num);
        return flag2;
      }
      this.ReleaseOpDC(SrcGr);
    }
    return flag2;
  }

  internal bool StretchBlt(
    IntPtr hDestDC,
    int DestX,
    int DestY,
    int DestWidth,
    int DestHeight,
    IntPtr hSrcDC,
    int SrcX,
    int SrcY,
    int SrcWidth,
    int SrcHeight,
    int rop)
  {
    return COp.Win32.StretchBlt(hDestDC, DestX, DestY, DestWidth, DestHeight, hSrcDC, SrcX, SrcY, SrcWidth, SrcHeight, rop);
  }

  internal void TextOut(Graphics gr, int x, int y, string str, int TextLen)
  {
    int opGr = this.FindOpGr(gr);
    IntPtr handle = IntPtr.Zero;
    IntPtr opDc = this.GetOpDC(opGr);
    COp.Win32.SetBkColor(opDc, this.ToColorRef(this.e.OpGr[opGr].BkColor));
    COp.Win32.SetTextColor(opDc, this.ToColorRef(this.e.OpGr[opGr].TextColor));
    COp.Win32.SetBkMode(opDc, this.e.OpGr[opGr].BkMode);
    if (this.True(this.e.OpGr[opGr].hFont))
      handle = this.SelectObject(opDc, this.e.OpGr[opGr].hFont);
    COp.Win32.TextOut(opDc, x, y, str.ToCharArray(), TextLen);
    if (this.True(this.e.OpGr[opGr].hFont))
      this.SelectObject(opDc, handle);
    this.ReleaseOpDC(gr);
  }

  internal Color ToColor(int color)
  {
    return this.ToColor(color & (int) byte.MaxValue, (color & 65280) >> 8, (color & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
  }

  internal Color ToColor(int red, int green, int blue)
  {
    if (red == (int) byte.MaxValue && green == (int) byte.MaxValue && blue == (int) byte.MaxValue)
      return Color.White;
    return red == 0 && green == 0 && blue == 0 ? Color.Black : Color.FromArgb((int) byte.MaxValue, red, green, blue);
  }

  internal int ToColorRef(Color color)
  {
    int r = (int) color.R;
    return ((int) color.B << 16 /*0x10*/) + ((int) color.G << 8) + r;
  }

  internal bool TranslateCharsetInfo(int CharSetOrCodePage, out COp.CHARSETINFO lpCs, int dwFlags)
  {
    return COp.Win32.TranslateCharsetInfo((IntPtr) CharSetOrCodePage, out lpCs, dwFlags);
  }

  internal Color XOrColor(Color clr1, Color clr2)
  {
    int red = (int) clr1.R ^ (int) clr2.R;
    int num = (int) clr1.B ^ (int) clr2.B;
    int green = (int) clr1.G ^ (int) clr2.G;
    int blue = num;
    return Color.FromArgb(red, green, blue);
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct CHARSETINFO
  {
    internal int ciCharset;
    internal int ciACP;
    internal int fsUsb1;
    internal int fsUsb2;
    internal int fsUsb3;
    internal int fsUsb4;
    internal int fsCsb1;
    internal int fsCsb2;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct CPINFO
  {
    internal int MaxCharSize;
    internal byte DefaultChar1;
    internal byte DefaultChar2;
    internal byte LeadByte1;
    internal byte LeadByte2;
    internal byte LeadByte3;
    internal byte LeadByte4;
    internal byte LeadByte5;
    internal byte LeadByte6;
    internal byte LeadByte7;
    internal byte LeadByte8;
    internal byte LeadByte9;
    internal byte LeadByte10;
    internal byte LeadByte11;
    internal byte LeadByte12;
  }

  internal struct GCP_RESULTS
  {
    internal int lStructSize;
    internal IntPtr lpOutString;
    internal IntPtr lpOrder;
    internal IntPtr lpDx;
    internal IntPtr lpCaretPos;
    internal IntPtr lpClass;
    internal IntPtr lpGlyphs;
    internal int nGlyphs;
    internal int nMaxFit;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct LOGFONT
  {
    internal int lfHeight;
    internal int lfWidth;
    internal int lfEscapement;
    internal int lfOrientation;
    internal int lfWeight;
    internal byte lfItalic;
    internal byte lfUnderline;
    internal byte lfStrikeOut;
    internal byte lfCharSet;
    internal byte lfOutPrecision;
    internal byte lfClipPrecision;
    internal byte lfQuality;
    internal byte lfPitchAndFamily;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    internal string lfFaceName;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct MSG
  {
    internal IntPtr hwnd;
    internal int message;
    internal IntPtr wParam;
    internal IntPtr lParam;
    internal int time;
    internal int x;
    internal int y;
  }

  internal struct OP_POINT
  {
    internal int x;
    internal int y;
  }

  internal struct RECT
  {
    internal int left;
    internal int top;
    internal int right;
    internal int bottom;

    internal RECT(int left, int top, int right, int bottom)
    {
      this.left = left;
      this.top = top;
      this.right = right;
      this.bottom = bottom;
    }

    internal RECT(Size size)
    {
      this.left = 0;
      this.top = 0;
      this.right = size.Width;
      this.bottom = size.Height;
    }
  }

  internal struct SCROLLINFO
  {
    internal int cbSize;
    internal int fMask;
    internal int nMin;
    internal int nMax;
    internal int nPage;
    internal int nPos;
    internal int nTrackPos;
  }

  internal struct SIZE
  {
    internal int cx;
    internal int cy;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  internal struct TEXTMETRIC
  {
    internal int tmHeight;
    internal int tmAscent;
    internal int tmDescent;
    internal int tmInternalLeading;
    internal int tmExternalLeading;
    internal int tmAveCharWidth;
    internal int tmMaxCharWidth;
    internal int tmWeight;
    internal int tmOverhang;
    internal int tmDigitizedAspectX;
    internal int tmDigitizedAspectY;
    internal char tmFirstChar;
    internal char tmLastChar;
    internal char tmDefaultChar;
    internal char tmBreakChar;
    internal byte tmItalic;
    internal byte tmUnderlined;
    internal byte tmStruckOut;
    internal byte tmPitchAndFamily;
    internal byte tmCharSet;

    internal bool ContentEqual(COp.TEXTMETRIC m)
    {
      return this.tmHeight == m.tmHeight && this.tmAscent == m.tmAscent && this.tmDescent == m.tmDescent && this.tmInternalLeading == m.tmInternalLeading && this.tmExternalLeading == m.tmExternalLeading && this.tmAveCharWidth == m.tmAveCharWidth && this.tmMaxCharWidth == m.tmMaxCharWidth && this.tmWeight == m.tmWeight && this.tmOverhang == m.tmOverhang && this.tmDigitizedAspectX == m.tmDigitizedAspectX && this.tmDigitizedAspectY == m.tmDigitizedAspectY && (int) this.tmFirstChar == (int) m.tmFirstChar && (int) this.tmLastChar == (int) m.tmLastChar && (int) this.tmDefaultChar == (int) m.tmDefaultChar && (int) this.tmBreakChar == (int) m.tmBreakChar && (int) this.tmItalic == (int) m.tmItalic && (int) this.tmUnderlined == (int) m.tmUnderlined && (int) this.tmStruckOut == (int) m.tmStruckOut && (int) this.tmPitchAndFamily == (int) m.tmPitchAndFamily && (int) this.tmCharSet == (int) m.tmCharSet;
    }
  }

  internal struct OUTLINETEXTMETRIC
  {
    public uint otmSize;
    public COp.TEXTMETRIC otmTextMetrics;
    public byte otmFiller;
    public COp.PANOSE otmPanoseNumber;
    public uint otmfsSelection;
    public uint otmfsType;
    public int otmsCharSlopeRise;
    public int otmsCharSlopeRun;
    public int otmItalicAngle;
    public uint otmEMSquare;
    public int otmAscent;
    public int otmDescent;
    public uint otmLineGap;
    public uint otmsCapEmHeight;
    public uint otmsXHeight;
    public COp.RECT otmrcFontBox;
    public int otmMacAscent;
    public int otmMacDescent;
    public uint otmMacLineGap;
    public uint otmusMinimumPPEM;
    public COp.POINT otmptSubscriptSize;
    public COp.POINT otmptSubscriptOffset;
    public COp.POINT otmptSuperscriptSize;
    public COp.POINT otmptSuperscriptOffset;
    public uint otmsStrikeoutSize;
    public int otmsStrikeoutPosition;
    public int otmsUnderscoreSize;
    public int otmsUnderscorePosition;
    public IntPtr otmpFamilyName;
    public IntPtr otmpFaceName;
    public IntPtr otmpStyleName;
    public IntPtr otmpFullName;
  }

  internal struct POINT
  {
    public int x;
    public int y;

    public POINT(int X, int Y)
    {
      this.x = X;
      this.y = Y;
    }

    public POINT(int lParam)
    {
      this.x = lParam & (int) ushort.MaxValue;
      this.y = lParam >> 16 /*0x10*/;
    }

    public static implicit operator Point(COp.POINT p) => new Point(p.x, p.y);

    public static implicit operator PointF(COp.POINT p) => new PointF((float) p.x, (float) p.y);

    public static implicit operator COp.POINT(Point p) => new COp.POINT(p.X, p.Y);
  }

  internal struct PANOSE
  {
    public byte bFamilyType;
    public byte bSerifStyle;
    public byte bWeight;
    public byte bProportion;
    public byte bContrast;
    public byte bStrokeVariation;
    public byte bArmStyle;
    public byte bLetterform;
    public byte bMidline;
    public byte bXHeight;
  }

  internal struct XFORM
  {
    internal float eM11;
    internal float eM12;
    internal float eM21;
    internal float eM22;
    internal float eDx;
    internal float eDy;

    internal XFORM(float eM11, float eM12, float eM21, float eM22, float eDx, float eDy)
    {
      this.eM11 = eM11;
      this.eM12 = eM12;
      this.eM21 = eM21;
      this.eM22 = eM22;
      this.eDx = eDx;
      this.eDy = eDy;
    }
  }

  internal class Win32
  {
    internal const int GM_ADVANCED = 2;

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool BitBlt(
      IntPtr hDestDC,
      int DestX,
      int DestY,
      int DestWidth,
      int DestHeight,
      IntPtr hSrcDC,
      int SrcX,
      int SrcY,
      int rop);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int width, int height);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int width, int height);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr CreateFontIndirect(ref COp.LOGFONT lfont);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreatePen(int style, int width, int color);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateSolidBrush(int color);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool DeleteDC(IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool DeleteMetaFile(IntPtr hMeta);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool DeleteObject(IntPtr hgdiObj);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool DestroyCaret();

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool DPtoLP(IntPtr hDC, [In, Out] COp.OP_POINT[] pt, int count);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int Ellipse(IntPtr hDC, int left, int top, int right, int bottom);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool EnumThreadWindows(
      int dwThreadId,
      COp.Win32.EnumThreadWindowsCallback lpfn,
      IntPtr lParam);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int Escape(
      IntPtr hDC,
      int escape,
      int size,
      IntPtr InData,
      ref COp.OP_POINT pt);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int ExtTextOut(
      IntPtr hdc,
      int x,
      int y,
      int options,
      IntPtr rect,
      char[] txt,
      int len,
      int[] dx);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int ExtTextOut(
      IntPtr hdc,
      int x,
      int y,
      int options,
      ref COp.RECT clip,
      char[] txt,
      int len,
      int[] dx);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int FillRect(IntPtr hDC, ref COp.RECT rect, IntPtr hBr);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr FindWindow(string ClsName, string WinName);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetBkMode(IntPtr hdc);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetCharacterPlacement(
      IntPtr hdc,
      char[] lpString,
      int nCount,
      int nMaxExtend,
      [MarshalAs(UnmanagedType.Struct)] ref COp.GCP_RESULTS lpResults,
      int dwFlags);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool GetCharWidth(
      IntPtr hDC,
      int FirstChar,
      int LastChar,
      int[] lpBuffer);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetClassName(IntPtr hWnd, char[] ClassName, int NameSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool GetCPInfo(int CodePage, out COp.CPINFO lpCPInfo);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern int GetCurrentThreadId();

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool GetCursorPos(ref COp.OP_POINT pt);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetDesktopWindow();

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetDeviceCaps(IntPtr hDC, int id);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetDlgCtrlID(IntPtr hWnd);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetFontLanguageInfo(IntPtr hDC);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern short GetKeyState(int VirtKey);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int GetObject(IntPtr hDC, int size, [MarshalAs(UnmanagedType.Struct)] out COp.LOGFONT lf);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int GetObject(IntPtr hDC, int size, IntPtr obj);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetStockObject(int obj);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetTextColor(IntPtr hdc);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool GetTextExtentPoint(
      IntPtr hdc,
      string str,
      int len,
      out COp.SIZE size);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool GetTextExtentPoint(
      IntPtr hdc,
      char[] str,
      int len,
      out COp.SIZE size);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool GetTextMetrics(IntPtr hDC, [MarshalAs(UnmanagedType.Struct)] out COp.TEXTMETRIC tm);

    [DllImport("gdi32.dll")]
    internal static extern int GetOutlineTextMetrics(
      IntPtr hdc,
      int cbData,
      ref COp.OUTLINETEXTMETRIC lpOTM);

    [DllImport("gdi32.dll", EntryPoint = "GetOutlineTextMetrics")]
    internal static extern int GetOutlineTextMetricsEx(IntPtr hdc, int cbData, IntPtr lpOTM);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetWindow(IntPtr hWnd, int cmd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool HideCaret(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool KillTimer(IntPtr hWnd, IntPtr id);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool LineTo(IntPtr hDC, int x, int y);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool LPtoDP(IntPtr hDC, [In, Out] COp.OP_POINT[] pt, int count);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int MessageBeep(int n);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool MoveToEx(IntPtr hDC, int x, int y, IntPtr pPoint);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    internal static extern int MultiByteToWideChar(
      int CodePage,
      int flags,
      byte[] InStr,
      int InStrlen,
      char[] OutStr,
      int OutStrSize);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool PeekMessage(
      out COp.MSG msg,
      IntPtr hWnd,
      int MinMsg,
      int MaxMsg,
      int remove);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int PlayEnhMetaFile(IntPtr hDC, IntPtr hMeta, ref COp.RECT rect);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int PlayMetaFile(IntPtr hDC, IntPtr hMeta);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool Polygon(IntPtr hDC, COp.OP_POINT[] pt, int count);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int RegisterClipboardFormat(string name);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern bool RestoreDC(IntPtr hDC, int StateId);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern int SaveDC(IntPtr hDC);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiObj);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool SendMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool SendMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] int Msg, IntPtr wParam, string lParam);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetBkColor(IntPtr hdc, int color);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetBkMode(IntPtr hdc, int mode);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetCaretPos(int x, int y);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetMapMode(IntPtr hDC, int mode);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr SetMetaFileBitsEx(int size, byte[] data);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetPixel(IntPtr hdc, int x, int y, int color);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetROP2(IntPtr hdc, int rop);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetScrollInfo(
      IntPtr hWnd,
      int fnBar,
      [MarshalAs(UnmanagedType.Struct)] ref COp.SCROLLINFO lpsi,
      bool fRedraw);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetScrollPos(IntPtr hWnd, int fnBar, int pos, bool redraw);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetScrollRange(
      IntPtr hWnd,
      int fnBar,
      int MinPos,
      int MaxPos,
      bool redraw);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetTextCharacterExtra(IntPtr hDC, int extra);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetTextColor(IntPtr hdc, int color);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr SetTimer(IntPtr hWnd, IntPtr id, int elapse, IntPtr proc);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetViewportExtEx(IntPtr hDC, int cx, int cy, IntPtr pSize);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetViewportOrgEx(IntPtr hDC, int x, int y, IntPtr pSize);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetWindowExtEx(IntPtr hDC, int cx, int cy, IntPtr pSize);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int NewVal);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetWindowOrgEx(IntPtr hDC, int x, int y, IntPtr pSize);

    [DllImport("gdi32.dll", SetLastError = true)]
    internal static extern int SetGraphicsMode(IntPtr hdc, int iMode);

    [DllImport("gdi32.dll", SetLastError = true)]
    internal static extern bool SetWorldTransform(IntPtr hdc, ref COp.XFORM xform);

    [DllImport("gdi32.dll", SetLastError = true)]
    internal static extern bool GetWorldTransform(IntPtr hdc, ref COp.XFORM xform);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool SetWindowPos(
      IntPtr hWnd,
      IntPtr hWndAft,
      int x,
      int y,
      int cx,
      int cy,
      int flags);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool ShowCaret(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool ShowScrollBar(IntPtr hWnd, int type, bool show);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool StretchBlt(
      IntPtr hDestDC,
      int DestX,
      int DestY,
      int DestWidth,
      int DestHeight,
      IntPtr hSrcDC,
      int SrcX,
      int SrcY,
      int SrcWidth,
      int SrcHeight,
      int rop);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern int TextOut(IntPtr hdc, int x, int y, char[] txt, int len);

    [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool TranslateCharsetInfo(
      IntPtr pSrc,
      out COp.CHARSETINFO lpCs,
      int dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool ValidateRect(IntPtr hWnd, ref COp.RECT rect);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern bool ValidateRect(IntPtr hWnd, IntPtr lprect);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern int ScriptStringAnalyse(
      IntPtr hDC,
      char[] pString,
      int cString,
      int cGlyphs,
      int iCharSet,
      int dwFlags,
      int iReqWidth,
      IntPtr psControl,
      IntPtr psState,
      int[] piDX,
      IntPtr pTbdef,
      byte[] pbInClass,
      out IntPtr pssa);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern int ScriptStringCPtoX(IntPtr ssa, int icp, bool fTrailing, out int pX);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr ScriptString_pSize(IntPtr ssa);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern int ScriptStringFree(ref IntPtr pssa);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern int ScriptStringGetLogicalWidths(IntPtr ssa, int[] piDx);

    [DllImport("Usp10.dll", CharSet = CharSet.Unicode)]
    internal static extern int ScriptStringGetOrder(IntPtr ssa, int[] puOrder);

    internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);
  }
}
