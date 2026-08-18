// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1Integer
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Security
{
    internal class Asn1Integer : AsnObject
    {
      private long m_value;

      public Asn1Integer(long value)
        : base(ASN1Tags.Integer)
      {
        this.m_value = value;
      }

      public byte[] AsnEncode() => this.AsnEncode(this.ToArray());

      private byte[] ToArray() => (byte[]) null;
    }
}
