// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.TtfTableInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal struct TtfTableInfo
    {
      public int Offset;
      public int Length;
      public int Checksum;

      public bool Empty
      {
        get => this.Offset == this.Length && this.Length == this.Checksum && this.Checksum == 0;
      }
    }
}
