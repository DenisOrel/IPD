// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.AlgorithmIdentifier
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class AlgorithmIdentifier : AsnObject
{
  private Asn1Sequence m_seq = new Asn1Sequence();

  public AlgorithmIdentifier(Asn1ObjectIdentifier oid, AsnObject param)
  {
    this.m_seq.Objects.Add((AsnObject) oid);
    this.m_seq.Objects.Add(param);
  }

  public byte[] AsnEncode() => this.m_seq.AsnEncode();
}
