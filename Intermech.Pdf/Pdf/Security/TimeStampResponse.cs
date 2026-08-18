// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.TimeStampResponse
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Security
{
    internal class TimeStampResponse
    {
      private Asn1ObjectIdentifier m_contentType;
      private AsnObject m_encodedObject;
      private Asn1Integer m_pkiStatusInfo;
      private AsnObject m_timeStampToken;

      internal TimeStampResponse(byte[] bytes)
      {
        this.ReadTimeStampResponse(new Asn1InputStream(bytes));
      }

      internal byte[] GetEncoded(AsnObject encodedObject) => this.ReadTimeStampToken(encodedObject);

      private byte[] ReadContentInfo()
      {
        this.m_contentType = (this.m_timeStampToken as Asn1Sequence)[0] as Asn1ObjectIdentifier;
        return this.ReadTimeStampContent();
      }

      private byte[] ReadTimeStampContent()
      {
        return new Asn1OutputStream().ParseTimeStamp(this.m_timeStampToken);
      }

      private void ReadTimeStampResponse(Asn1InputStream stream)
      {
        this.m_encodedObject = stream.ReadObject();
      }

      private byte[] ReadTimeStampToken(AsnObject encodedObject)
      {
        if (encodedObject is Asn1Sequence)
        {
          this.m_pkiStatusInfo = ((encodedObject as Asn1Sequence)[0] as Asn1Sequence)[0] as Asn1Integer;
          this.m_timeStampToken = (encodedObject as Asn1Sequence)[1];
        }
        return this.ReadContentInfo();
      }

      internal AsnObject Object => this.m_encodedObject;
    }
}
