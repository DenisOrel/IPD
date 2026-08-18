
// Type: Intermech.Controls.NativeWindowMethods
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;


namespace Intermech.Controls;

[SuppressUnmanagedCodeSecurity]
[ComVisible(false)]
public sealed class NativeWindowMethods
{
  public const int ANSI_CHARSET = 0;
  public const int ARABIC_CHARSET = 178;
  public const int BALTIC_CHARSET = 186;
  public const int CHINESEBIG5_CHARSET = 136;
  public const int DEFAULT_CHARSET = 1;
  public const int EASTEUROPE_CHARSET = 238;
  public const int GB2312_CHARSET = 134;
  public const int GREEK_CHARSET = 161;
  public const int HANGEUL_CHARSET = 129;
  public const int HANGUL_CHARSET = 129;
  public const int HEBREW_CHARSET = 177;
  public const int JOHAB_CHARSET = 130;
  public const int MAC_CHARSET = 77;
  public const int OEM_CHARSET = 255 /*0xFF*/;
  public const int RUSSIAN_CHARSET = 204;
  public const int SHIFTJIS_CHARSET = 128 /*0x80*/;
  public const int SYMBOL_CHARSET = 2;
  public const int THAI_CHARSET = 222;
  public const int TMPF_DEVICE = 8;
  public const int TMPF_FIXED_PITCH = 1;
  public const int TMPF_TRUETYPE = 4;
  public const int TMPF_VECTOR = 2;
  public const int TURKISH_CHARSET = 162;
  public const int VIETNAMESE_CHARSET = 163;

  private NativeWindowMethods()
  {
  }

  [DllImport("Gdi32", CharSet = CharSet.Auto)]
  private static extern bool DeleteObject(IntPtr hObject);

  [DllImport("gdi32", CharSet = CharSet.Unicode)]
  private static extern int GetFontUnicodeRanges(IntPtr hdc, [In, Out] IntPtr gs);

  [DllImport("gdi32", CharSet = CharSet.Unicode)]
  private static extern int GetTextCharsetInfo(
    IntPtr hdc,
    [In, Out] NativeWindowMethods.FONTSIGNATURE lpSig,
    int dwFlags);

  [DllImport("Gdi32", CharSet = CharSet.Auto)]
  private static extern bool GetTextMetrics(
    IntPtr hdc,
    out NativeWindowMethods.TEXTMETRIC textmetric);

  public static bool IsFontTrueType(
    Graphics g,
    Font font,
    out NativeWindowMethods.TEXTMETRIC textmetric)
  {
    IntPtr hdc = IntPtr.Zero;
    IntPtr hObject1 = IntPtr.Zero;
    IntPtr hObject2 = IntPtr.Zero;
    bool flag = false;
    textmetric = new NativeWindowMethods.TEXTMETRIC();
    try
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      hdc = g.GetHdc();
      if (hdc != IntPtr.Zero)
      {
        hObject1 = font.ToHfont();
        if (hObject1 != IntPtr.Zero)
        {
          hObject2 = (IntPtr) NativeWindowMethods.SelectObject(hdc, hObject1);
          NativeWindowMethods.SelectObject(hdc, hObject1);
          if (NativeWindowMethods.GetTextMetrics(hdc, out textmetric) && ((int) textmetric.tmPitchAndFamily & 4) != 0)
            flag = true;
        }
      }
      CodeAccessPermission.RevertAssert();
    }
    finally
    {
      if (hdc != IntPtr.Zero)
      {
        if (hObject2 != IntPtr.Zero)
          NativeWindowMethods.SelectObject(hdc, hObject2);
        if (hObject1 != IntPtr.Zero)
          NativeWindowMethods.DeleteObject(hObject1);
        g.ReleaseHdc(hdc);
      }
    }
    return flag;
  }

  public static NativeWindowMethods.GLYPHSET NativeGetFontUnicodeRanges(Graphics g, Font font)
  {
    IntPtr hdc = IntPtr.Zero;
    IntPtr hObject1 = IntPtr.Zero;
    IntPtr hObject2 = IntPtr.Zero;
    IntPtr num1 = IntPtr.Zero;
    NativeWindowMethods.GLYPHSET fontUnicodeRanges1 = (NativeWindowMethods.GLYPHSET) null;
    try
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      hdc = g.GetHdc();
      if (hdc != IntPtr.Zero)
      {
        hObject1 = font.ToHfont();
        if (hObject1 != IntPtr.Zero)
        {
          hObject2 = (IntPtr) NativeWindowMethods.SelectObject(hdc, hObject1);
          NativeWindowMethods.SelectObject(hdc, hObject1);
          int fontUnicodeRanges2 = NativeWindowMethods.GetFontUnicodeRanges(hdc, IntPtr.Zero);
          int num2 = Marshal.SizeOf(typeof (NativeWindowMethods.GLYPHSET_HEADER));
          int num3 = Marshal.SizeOf(typeof (NativeWindowMethods.WCRANGE));
          int num4 = (fontUnicodeRanges2 - num2) / num3;
          num1 = Marshal.AllocHGlobal(fontUnicodeRanges2);
          if (num1 != IntPtr.Zero)
          {
            fontUnicodeRanges1 = new NativeWindowMethods.GLYPHSET();
            fontUnicodeRanges1.header = new NativeWindowMethods.GLYPHSET_HEADER(fontUnicodeRanges2);
            Marshal.StructureToPtr<NativeWindowMethods.GLYPHSET_HEADER>(fontUnicodeRanges1.header, num1, false);
            int fontUnicodeRanges3 = NativeWindowMethods.GetFontUnicodeRanges(hdc, num1);
            if (fontUnicodeRanges3 != 0)
            {
              int length = (fontUnicodeRanges3 - num2) / num3;
              fontUnicodeRanges1.header = (NativeWindowMethods.GLYPHSET_HEADER) Marshal.PtrToStructure(num1, typeof (NativeWindowMethods.GLYPHSET_HEADER));
              int src = (int) num1 + num2;
              fontUnicodeRanges1.ranges = new NativeWindowMethods.WCRANGE[length];
              NativeWindowMethods.RtlMoveMemory(fontUnicodeRanges1.ranges, (IntPtr) src, length * num3);
            }
          }
        }
      }
      CodeAccessPermission.RevertAssert();
    }
    finally
    {
      if (num1 != IntPtr.Zero)
        Marshal.FreeHGlobal(num1);
      if (hdc != IntPtr.Zero)
      {
        if (hObject2 != IntPtr.Zero)
          NativeWindowMethods.SelectObject(hdc, hObject2);
        if (hObject1 != IntPtr.Zero)
          NativeWindowMethods.DeleteObject(hObject1);
        g.ReleaseHdc(hdc);
      }
    }
    return fontUnicodeRanges1;
  }

  public static int NativeGetTextCharsetInfo(
    Graphics g,
    Font font,
    NativeWindowMethods.FONTSIGNATURE fs)
  {
    IntPtr hdc = IntPtr.Zero;
    IntPtr hObject1 = IntPtr.Zero;
    IntPtr hObject2 = IntPtr.Zero;
    int textCharsetInfo = 0;
    try
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      hdc = g.GetHdc();
      if (hdc != IntPtr.Zero)
      {
        hObject1 = font.ToHfont();
        if (hObject1 != IntPtr.Zero)
        {
          hObject2 = (IntPtr) NativeWindowMethods.SelectObject(hdc, hObject1);
          NativeWindowMethods.SelectObject(hdc, hObject1);
          textCharsetInfo = NativeWindowMethods.GetTextCharsetInfo(hdc, fs, 0);
        }
      }
      CodeAccessPermission.RevertAssert();
    }
    finally
    {
      if (hdc != IntPtr.Zero)
      {
        if (hObject2 != IntPtr.Zero)
          NativeWindowMethods.SelectObject(hdc, hObject2);
        if (hObject1 != IntPtr.Zero)
          NativeWindowMethods.DeleteObject(hObject1);
        g.ReleaseHdc(hdc);
      }
    }
    return textCharsetInfo;
  }

  public static bool NativeGetTextMetrics(Graphics g, out NativeWindowMethods.TEXTMETRIC textmetric)
  {
    bool textMetrics = false;
    IntPtr hdc = IntPtr.Zero;
    textmetric = new NativeWindowMethods.TEXTMETRIC();
    try
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      hdc = g.GetHdc();
      if (hdc != IntPtr.Zero)
        textMetrics = NativeWindowMethods.GetTextMetrics(hdc, out textmetric);
      CodeAccessPermission.RevertAssert();
    }
    finally
    {
      if (hdc != IntPtr.Zero)
        g.ReleaseHdc(hdc);
    }
    return textMetrics;
  }

  [DllImport("kernel32.dll")]
  private static extern void RtlMoveMemory([Out] NativeWindowMethods.WCRANGE[] dest, [In] IntPtr src, int cb);

  [DllImport("gdi32")]
  private static extern int SelectObject(IntPtr hdc, IntPtr hObject);

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public sealed class FONTSIGNATURE
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] fsUsb;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[] fsCsb;

    public FONTSIGNATURE()
    {
      this.fsUsb = new int[4];
      this.fsCsb = new int[2];
    }
  }

  public sealed class GLYPHSET
  {
    public NativeWindowMethods.GLYPHSET_HEADER header;
    public NativeWindowMethods.WCRANGE[] ranges;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct GLYPHSET_HEADER(int size)
  {
    public int cbThis = size;
    public int flAccel = 0;
    public int cGlyphsSupported = 0;
    public int cRanges = 0;
  }

  [StructLayout(LayoutKind.Sequential)]
  public sealed class LOGFONT
  {
    public int lfHeight;
    public int lfWidth;
    public int lfEscapement;
    public int lfOrientation;
    public int lfWeight;
    public byte lfItalic;
    public byte lfUnderline;
    public byte lfStrikeOut;
    public byte lfCharSet;
    public byte lfOutPrecision;
    public byte lfClipPrecision;
    public byte lfQuality;
    public byte lfPitchAndFamily;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string lfFaceName;

    public LOGFONT(bool dummy)
    {
      this.lfHeight = 0;
      this.lfWidth = 0;
      this.lfEscapement = 0;
      this.lfOrientation = 0;
      this.lfWeight = 0;
      this.lfItalic = (byte) 0;
      this.lfUnderline = (byte) 0;
      this.lfStrikeOut = (byte) 0;
      this.lfCharSet = (byte) 0;
      this.lfOutPrecision = (byte) 0;
      this.lfClipPrecision = (byte) 0;
      this.lfQuality = (byte) 0;
      this.lfPitchAndFamily = (byte) 0;
      this.lfFaceName = new string(' ', 32 /*0x20*/);
    }
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public struct TEXTMETRIC
  {
    public int tmHeight;
    public int tmAscent;
    public int tmDescent;
    public int tmInternalLeading;
    public int tmExternalLeading;
    public int tmAveCharWidth;
    public int tmMaxCharWidth;
    public int tmWeight;
    public int tmOverhang;
    public int tmDigitizedAspectX;
    public int tmDigitizedAspectY;
    public char tmFirstChar;
    public char tmLastChar;
    public char tmDefaultChar;
    public char tmBreakChar;
    public byte tmItalic;
    public byte tmUnderlined;
    public byte tmStruckOut;
    public byte tmPitchAndFamily;
    public byte tmCharSet;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct WCRANGE
  {
    public char wcLow;
    public short cGlyphs;
  }
}
