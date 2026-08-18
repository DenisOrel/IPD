// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.OUTLINETEXTMETRIC
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Native
{
    internal struct OUTLINETEXTMETRIC
    {
      public uint otmSize;
      public TEXTMETRIC otmTextMetrics;
      public byte otmFiller;
      public PANOSE otmPanoseNumber;
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
      public RECT otmrcFontBox;
      public int otmMacAscent;
      public int otmMacDescent;
      public uint otmMacLineGap;
      public uint otmusMinimumPPEM;
      public POINT otmptSubscriptSize;
      public POINT otmptSubscriptOffset;
      public POINT otmptSuperscriptSize;
      public POINT otmptSuperscriptOffset;
      public uint otmsStrikeoutSize;
      public int otmsStrikeoutPosition;
      public int otmsUnderscoreSize;
      public int otmsUnderscorePosition;
      public IntPtr otmpFamilyName;
      public IntPtr otmpFaceName;
      public IntPtr otmpStyleName;
      public IntPtr otmpFullName;
    }
}
