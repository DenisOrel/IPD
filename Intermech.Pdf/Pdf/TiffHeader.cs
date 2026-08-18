// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TiffHeader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal struct TiffHeader
    {
      public const int ByteOrderSize = 2;
      public const int VersionSize = 2;
      public const int DirOffsetSize = 4;
      public const int SizeInBytes = 8;
      public short m_byteOrder;
      public short m_version;
      public uint m_dirOffset;
    }
}
