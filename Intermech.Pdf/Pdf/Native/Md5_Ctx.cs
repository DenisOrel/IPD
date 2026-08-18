// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.Md5_Ctx
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Runtime.InteropServices;


namespace Syncfusion.Pdf.Native
{
    internal struct Md5_Ctx
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
      public uint[] i;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
      public uint[] buf;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64 /*0x40*/)]
      public byte[] input;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16 /*0x10*/)]
      public byte[] digest;
    }
}
