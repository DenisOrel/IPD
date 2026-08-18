// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.GradientBrushFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    [Flags]
    internal enum GradientBrushFlags
    {
      Blend = 8,
      ColorBlend = 4,
      Default = 0,
      FocusScales = 64, // 0x00000040
      GammaCorrection = 128, // 0x00000080
      Matrix = 2,
    }
}
