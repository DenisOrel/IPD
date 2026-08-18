// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ImagePreRenderEventArgs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;


namespace Syncfusion.Pdf
{
    public class ImagePreRenderEventArgs : EventArgs
    {
      internal string[] m_filter;
      internal float m_height;
      internal Stream m_imageStream;
      internal float m_width;

      internal ImagePreRenderEventArgs(ImageStructure structure)
      {
        this.m_imageStream = structure.ImageStream;
        this.m_height = structure.Height;
        this.m_width = structure.Width;
        this.m_filter = structure.ImageFilter;
      }

      public string[] Filter => this.m_filter;

      public float Height
      {
        get => this.m_height;
        set => this.m_height = value;
      }

      public Stream ImageStream
      {
        get => this.m_imageStream;
        set => this.m_imageStream = value;
      }

      public float Width
      {
        get => this.m_width;
        set => this.m_width = value;
      }
    }
}
