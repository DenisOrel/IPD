// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1Boolean
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Security
{
    internal class Asn1Boolean : AsnObject
    {
      private bool m_value;

      public Asn1Boolean(bool value)
        : base(ASN1Tags.Boolean)
      {
        this.m_value = value;
      }

      public Asn1Boolean(byte[] bytes)
        : base(ASN1Tags.Boolean)
      {
        this.m_value = bytes[0] == byte.MaxValue;
      }

      public byte[] AsnEncode() => this.AsnEncode(this.ToArray());

      private byte[] ToArray()
      {
        return new byte[1]
        {
          this.m_value ? byte.MaxValue : (byte) 0
        };
      }
    }
}
