// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.FORMATRANGE
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Runtime.InteropServices;


namespace Syncfusion.Pdf.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal class FORMATRANGE
    {
      public IntPtr hdc = IntPtr.Zero;
      public IntPtr hdcTarget = IntPtr.Zero;
      public RECT rc;
      public RECT rcPage;
      public CHARRANGE chrg;
    }
}
