// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.LOGFONT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Native;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal class LOGFONT
{
  public int lfHeight;
  public int lfWidth;
  public int lfEscapement;
  public int lfOrientation;
  public FW_FONT_WEIGHT lfWeight = FW_FONT_WEIGHT.FW_NORMAL;
  [MarshalAs(UnmanagedType.U1)]
  public bool lfItalic;
  [MarshalAs(UnmanagedType.U1)]
  public bool lfUnderline;
  [MarshalAs(UnmanagedType.U1)]
  public bool lfStrikeOut;
  public byte lfCharSet;
  public byte lfOutPrecision;
  public byte lfClipPrecision;
  public byte lfQuality;
  public byte lfPitchAndFamily;
  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
  public string lfFaceName;
}
