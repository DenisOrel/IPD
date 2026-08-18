// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.CHARFORMAT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal struct CHARFORMAT
{
  public int cbSize;
  public uint dwMask;
  public uint dwEffects;
  public int yHeight;
  public int yOffset;
  public int crTextColor;
  public byte bCharSet;
  public byte bPitchAndFamily;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 /*0x20*/)]
  public char[] szFaceName;
  public short wWeight;
  public short sSpacing;
  public int crBackColor;
  public uint lcid;
  public uint dwReserved;
  public short sStyle;
  public short wKerning;
  public byte bUnderlineType;
  public byte bAnimation;
  public byte bRevAuthor;
  public byte bReserved1;
}
