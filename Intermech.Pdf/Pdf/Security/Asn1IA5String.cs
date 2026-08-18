// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1IA5String
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Text;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1IA5String : AsnObject
    {
      public byte[] m_value;

      public Asn1IA5String(byte[] bytes)
        : base(ASN1Tags.IA5String)
      {
        this.m_value = bytes;
        if (Encoding.ASCII.GetString(bytes) == null)
          throw new ArgumentNullException("IA5 string cannot be null");
      }

      public byte[] AsnEncode() => this.AsnEncode(this.m_value);
    }
}
