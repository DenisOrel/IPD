// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfConfig
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.ComponentModel;
using System.Drawing;


namespace Syncfusion.Pdf
{
    [ToolboxBitmap(typeof (PdfConfig), "ToolBoxIcons.Pdf.bmp")]
    public class PdfConfig : Component
    {
      public PdfConfig()
      {
        try
        {
          AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(PdfBaseAssembly.AssemblyResolver);
        }
        finally
        {
          AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(PdfBaseAssembly.AssemblyResolver);
        }
      }

      public string Copyright => "Syncfusion, Inc. 2001 - 2006";
    }
}
