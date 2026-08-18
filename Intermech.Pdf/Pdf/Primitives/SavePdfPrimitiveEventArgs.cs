// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.SavePdfPrimitiveEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using System;


namespace Syncfusion.Pdf.Primitives
{
    internal class SavePdfPrimitiveEventArgs : EventArgs
    {
      private IPdfWriter m_writer;

      public SavePdfPrimitiveEventArgs(IPdfWriter writer)
      {
        this.m_writer = writer != null ? writer : throw new ArgumentNullException(nameof (writer));
      }

      public IPdfWriter Writer => this.m_writer;
    }
}
