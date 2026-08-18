// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.FormatMessageFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Native
{
    [Flags]
    internal enum FormatMessageFlags
    {
      AllocateBuffer = 256, // 0x00000100
      ArgumentArray = 8192, // 0x00002000
      FromHmodule = 2048, // 0x00000800
      FromString = 1024, // 0x00000400
      FromSystem = 4096, // 0x00001000
      IgnoreInserts = 512, // 0x00000200
    }
}
