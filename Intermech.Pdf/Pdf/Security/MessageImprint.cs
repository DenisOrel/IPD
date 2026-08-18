// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.MessageImprint
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Security
{
    internal class MessageImprint : Asn1Sequence
    {
      public MessageImprint(string oid, byte[] hash)
      {
        this.Objects.Add((AsnObject) new AlgorithmIdentifier(new Asn1ObjectIdentifier(oid), (AsnObject) new Asn1Null()));
        this.Objects.Add((AsnObject) new Asn1OctetString(hash));
      }
    }
}
