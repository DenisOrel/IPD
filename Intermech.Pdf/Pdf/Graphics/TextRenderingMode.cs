// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.TextRenderingMode
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics
{
    [Flags]
    internal enum TextRenderingMode
    {
      Clip = 7,
      ClipFill = 4,
      ClipFillStroke = 6,
      ClipFlag = ClipFill, // 0x00000004
      ClipStroke = 5,
      Fill = 0,
      FillStroke = 2,
      None = 3,
      Stroke = 1,
    }
}
