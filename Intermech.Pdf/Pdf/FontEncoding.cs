// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.FontEncoding
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class FontEncoding
    {
      private ushort m_encodingId;
      private uint m_offset;
      private ushort m_platformId;

      public void ReadEncodingDeatils(ReadFontArray reader)
      {
        this.PlatformId = reader.getnextUshort();
        this.EncodingId = reader.getnextUshort();
        this.Offset = reader.getULong();
      }

      public ushort EncodingId
      {
        get => this.m_encodingId;
        set => this.m_encodingId = value;
      }

      public uint Offset
      {
        get => this.m_offset;
        set => this.m_offset = value;
      }

      public ushort PlatformId
      {
        get => this.m_platformId;
        set => this.m_platformId = value;
      }
    }
}
