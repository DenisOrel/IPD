// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.PT_POINT_TYPE
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Native
{
    [Flags]
    internal enum PT_POINT_TYPE : byte
    {
      PT_BEZIERTO = 4,
      PT_CLOSEFIGURE = 1,
      PT_LINETO = 2,
      PT_MOVETO = PT_LINETO | PT_BEZIERTO, // 0x06
    }
}
