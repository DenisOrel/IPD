// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TableBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal abstract class TableBase
    {
      private readonly FontFile2 fontSource;
      private int m_offset;

      public TableBase()
      {
      }

      public TableBase(FontFile2 fontSource) => this.fontSource = fontSource;

      public abstract void Read(ReadFontArray reader);

      protected FontFile2 FontSource => this.fontSource;

      internal abstract int Id { get; }

      public int Offset
      {
        get => this.m_offset;
        set => this.m_offset = value;
      }

      protected ReadFontArray Reader => this.fontSource.FontArrayReader;
    }
}
