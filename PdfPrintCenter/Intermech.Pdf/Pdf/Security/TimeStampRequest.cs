// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.TimeStampRequest
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class TimeStampRequest : AsnObject
{
  private const string c_IdSHA = "1.3.14.3.2.26";
  private const string c_IdTimeStampToken = "1.2.840.113549.1.9.16.2.14";
  private Asn1Boolean m_certReq;
  private MessageImprint m_messageImprint;
  private Asn1Integer m_version;

  public TimeStampRequest(bool certReq)
    : base(ASN1Tags.Constructed | ASN1Tags.Sequence)
  {
    this.m_certReq = new Asn1Boolean(certReq);
    this.m_version = new Asn1Integer(1L);
  }

  public byte[] GetAsnEncodedTimestampRequest(byte[] hash)
  {
    this.m_messageImprint = new MessageImprint("1.3.14.3.2.26", hash);
    byte[] sourceArray1 = this.m_version.AsnEncode();
    byte[] sourceArray2 = this.m_messageImprint.AsnEncode();
    byte[] sourceArray3 = this.m_certReq.AsnEncode();
    byte[] destinationArray = new byte[sourceArray1.Length + sourceArray2.Length + sourceArray3.Length];
    Array.Copy((Array) sourceArray1, (Array) destinationArray, sourceArray1.Length);
    Array.Copy((Array) sourceArray2, 0, (Array) destinationArray, sourceArray1.Length, sourceArray2.Length);
    Array.Copy((Array) sourceArray3, 0, (Array) destinationArray, sourceArray1.Length + sourceArray2.Length, sourceArray3.Length);
    return this.AsnEncode(destinationArray);
  }
}
