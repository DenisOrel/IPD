// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PARAFORMAT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal struct PARAFORMAT
{
  public int cbSize;
  public uint dwMask;
  public short wNumbering;
  public short wReserved;
  public int dxStartIndent;
  public int dxRightIndent;
  public int dxOffset;
  public short wAlignment;
  public short cTabCount;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32 /*0x20*/)]
  public int[] rgxTabs;
  public int dySpaceBefore;
  public int dySpaceAfter;
  public int dyLineSpacing;
  public short sStyle;
  public byte bLineSpacingRule;
  public byte bOutlineLevel;
  public short wShadingWeight;
  public short wShadingStyle;
  public short wNumberingStart;
  public short wNumberingStyle;
  public short wNumberingTab;
  public short wBorderSpace;
  public short wBorderWidth;
  public short wBorders;
}
