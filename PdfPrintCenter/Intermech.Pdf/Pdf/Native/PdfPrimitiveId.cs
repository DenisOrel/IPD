// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.PdfPrimitiveId
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Syncfusion.Pdf.Native;

[StructLayout(LayoutKind.Sequential, Size = 1)]
internal struct PdfPrimitiveId
{
  internal byte[] Null => new byte[1];

  internal byte[] Integer => new byte[1]{ (byte) 1 };

  internal byte[] Real => new byte[1]{ (byte) 2 };

  internal byte[] Boolean => new byte[1]{ (byte) 3 };

  internal byte[] Name => new byte[1]{ (byte) 4 };

  internal byte[] String => new byte[1]{ (byte) 5 };

  internal byte[] Dictionary => new byte[1]{ (byte) 6 };

  internal byte[] Array => new byte[1]{ (byte) 7 };

  internal byte[] Stream => new byte[1]{ (byte) 8 };

  internal byte[] True => new byte[1]{ (byte) 1 };

  internal byte[] False => new byte[1];

  internal byte[] Visited
  {
    get
    {
      return new byte[4]
      {
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue
      };
    }
  }
}
