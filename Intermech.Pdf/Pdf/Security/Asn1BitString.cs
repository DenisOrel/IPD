// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1BitString
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1BitString : AsnObject
    {
      private int m_padBits;
      private readonly byte[] m_value;

      public Asn1BitString(byte[] bytes, int padBit)
        : base(ASN1Tags.BitString)
      {
        this.m_value = bytes;
        this.m_padBits = padBit;
      }

      public byte[] AsnEncode()
      {
        byte[] destinationArray = new byte[this.m_value.Length + 1];
        destinationArray[0] = (byte) this.m_padBits;
        Array.Copy((Array) this.m_value, 0, (Array) destinationArray, 1, destinationArray.Length - 1);
        return this.AsnEncode(destinationArray);
      }
    }
}
