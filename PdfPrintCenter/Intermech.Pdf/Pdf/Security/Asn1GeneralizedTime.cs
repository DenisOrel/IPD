// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1GeneralizedTime
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class Asn1GeneralizedTime : AsnObject
{
  private byte[] m_value;

  internal Asn1GeneralizedTime(byte[] bytes)
    : base(ASN1Tags.GeneralizedTime)
  {
    this.m_value = bytes;
  }

  public byte[] AsnEncode() => this.AsnEncode(this.m_value);
}
