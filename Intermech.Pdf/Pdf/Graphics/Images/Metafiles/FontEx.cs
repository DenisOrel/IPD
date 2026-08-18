// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.FontEx
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    internal class FontEx : IDisposable
    {
      private Font m_font;
      private LOGFONT m_structure;

      public FontEx(Font font, LOGFONT structure)
      {
        this.m_font = font != null ? font : throw new ArgumentNullException(nameof (font));
        this.m_structure = structure;
      }

      public void Dispose()
      {
        if (this.m_font == null)
          return;
        this.m_font.Dispose();
        this.m_font = (Font) null;
      }

      public float Angle => (float) -((double) this.m_structure.lfEscapement / 10.0);

      public Font Font => this.m_font;

      public LOGFONT LogFont => this.m_structure;
    }
}
