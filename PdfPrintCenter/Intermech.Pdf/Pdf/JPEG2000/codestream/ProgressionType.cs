// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.ProgressionType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.codestream;

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct ProgressionType
{
  public const int LY_RES_COMP_POS_PROG = 0;
  public const int RES_LY_COMP_POS_PROG = 1;
  public const int RES_POS_COMP_LY_PROG = 2;
  public const int POS_COMP_RES_LY_PROG = 3;
  public const int COMP_POS_RES_LY_PROG = 4;
}
