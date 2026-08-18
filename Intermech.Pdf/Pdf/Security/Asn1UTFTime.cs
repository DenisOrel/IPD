// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1UTFTime
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1UTFTime : AsnObject
    {
      private byte[] m_value;

      public Asn1UTFTime(byte[] value)
        : base(ASN1Tags.UTFTime)
      {
        this.m_value = value != null ? value : throw new ArgumentNullException(nameof (value));
      }

      public byte[] AsnEncode() => this.AsnEncode(this.m_value);
    }
}
