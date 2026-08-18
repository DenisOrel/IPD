// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1Sequence
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1Sequence : AsnObject, IEnumerable
    {
      private List<AsnObject> m_objects;

      public Asn1Sequence()
        : base(ASN1Tags.Constructed | ASN1Tags.Sequence)
      {
        this.m_objects = new List<AsnObject>();
      }

      public Asn1Sequence(List<AsnObject> sequence)
        : base(ASN1Tags.Constructed | ASN1Tags.Sequence)
      {
        this.m_objects = new List<AsnObject>();
        foreach (AsnObject asnObject in sequence)
          this.m_objects.Add(asnObject);
      }

      public byte[] AsnEncode() => this.AsnEncode(this.ToArray());

      public IEnumerator GetEnumerator() => (IEnumerator) this.m_objects.GetEnumerator();

      private byte[] ToArray()
      {
        MemoryStream memoryStream = new MemoryStream();
        foreach (AsnObject asnObject in this.m_objects)
        {
          byte[] buffer = (byte[]) null;
          switch (asnObject)
          {
            case Asn1Integer _:
              buffer = (asnObject as Asn1Integer).AsnEncode();
              break;
            case Asn1Boolean _:
              buffer = (asnObject as Asn1Boolean).AsnEncode();
              break;
            case Asn1Null _:
              buffer = (asnObject as Asn1Null).AsnEncode();
              break;
            case Asn1ObjectIdentifier _:
              buffer = (asnObject as Asn1ObjectIdentifier).AsnEncode();
              break;
            case Asn1OctetString _:
              buffer = (asnObject as Asn1OctetString).AsnEncode();
              break;
            case Asn1Sequence _:
              buffer = (asnObject as Asn1Sequence).AsnEncode();
              break;
            case AlgorithmIdentifier _:
              buffer = (asnObject as AlgorithmIdentifier).AsnEncode();
              break;
          }
          memoryStream.Write(buffer, 0, buffer.Length);
        }
        return memoryStream.ToArray();
      }

      public AsnObject this[int index] => this.m_objects[index];

      public List<AsnObject> Objects => this.m_objects;
    }
}
