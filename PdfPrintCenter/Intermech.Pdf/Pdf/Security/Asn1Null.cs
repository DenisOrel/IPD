// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1Null
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class Asn1Null : AsnObject
{
  public Asn1Null()
    : base(ASN1Tags.Null)
  {
  }

  public byte[] AsnEncode() => this.AsnEncode(this.ToArray());

  private byte[] ToArray() => new byte[0];
}
