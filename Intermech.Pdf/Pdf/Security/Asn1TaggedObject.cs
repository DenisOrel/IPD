// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1TaggedObject
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Security
{
    internal class Asn1TaggedObject : AsnObject
    {
      private bool m_isExplicit;
      private AsnObject m_obj;
      private int m_tagNo;

      internal Asn1TaggedObject(bool isExplicit, AsnObject obj, int tagNo)
        : base(ASN1Tags.Tagged)
      {
        this.m_isExplicit = isExplicit;
        this.m_obj = obj;
        this.m_tagNo = tagNo;
      }

      internal bool IsExplicit => this.m_isExplicit;

      internal AsnObject Objects => this.m_obj;

      internal int TagNumber => this.m_tagNo;
    }
}
