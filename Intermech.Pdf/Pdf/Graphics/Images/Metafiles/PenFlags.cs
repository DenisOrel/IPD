// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.PenFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    [Flags]
    internal enum PenFlags
    {
      Alignment = 512, // 0x00000200
      CompoundArray = 1024, // 0x00000400
      CustomEndCap = 4096, // 0x00001000
      CustomStartCap = 2048, // 0x00000800
      DashCap = 64, // 0x00000040
      DashOffset = 128, // 0x00000080
      DashPattern = 256, // 0x00000100
      DashStyle = 32, // 0x00000020
      Default = 0,
      EndCap = 4,
      LineJoin = 8,
      MiterLimit = 16, // 0x00000010
      StartCap = 2,
      Transform = 1,
    }
}
