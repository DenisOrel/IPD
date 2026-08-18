// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1OutputStream
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;
using System.IO;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1OutputStream
    {
      private MemoryStream m_stream = new MemoryStream();

      internal Asn1OutputStream()
      {
      }

      internal byte[] ParseTimeStamp(AsnObject encodedObject)
      {
        Asn1OutputStream asn1OutputStream = new Asn1OutputStream();
        this.m_stream.WriteByte((byte) 48 /*0x30*/);
        this.m_stream.WriteByte((byte) 128 /*0x80*/);
        if ((encodedObject as Asn1Sequence)[0] is Asn1ObjectIdentifier)
        {
          byte[] buffer = ((encodedObject as Asn1Sequence)[0] as Asn1ObjectIdentifier).AsnEncode();
          this.m_stream.Write(buffer, 0, buffer.Length);
        }
        if ((encodedObject as Asn1Sequence)[1] is Asn1TaggedObject)
        {
          AsnObject asnObject = (encodedObject as Asn1Sequence)[1];
          this.m_stream.WriteByte((byte) (160 /*0xA0*/ | (asnObject as Asn1TaggedObject).TagNumber));
          this.m_stream.WriteByte((byte) 128 /*0x80*/);
          Asn1Sequence encodedObject1 = new Asn1Sequence(new List<AsnObject>()
          {
            (asnObject as Asn1TaggedObject).Objects
          });
          byte[] timeStampToken = asn1OutputStream.ParseTimeStampToken((AsnObject) encodedObject1);
          this.m_stream.Write(timeStampToken, 0, timeStampToken.Length);
        }
        this.m_stream.WriteByte((byte) 0);
        this.m_stream.WriteByte((byte) 0);
        this.m_stream.WriteByte((byte) 0);
        this.m_stream.WriteByte((byte) 0);
        this.m_stream.Close();
        return this.m_stream.ToArray();
      }

      internal byte[] ParseTimeStampToken(AsnObject encodedObject)
      {
        Asn1Sequence asn1Sequence = (Asn1Sequence) null;
        switch (encodedObject)
        {
          case Asn1Sequence _:
            asn1Sequence = encodedObject as Asn1Sequence;
            break;
          case Asn1Set _:
            asn1Sequence = new Asn1Sequence();
            using (List<AsnObject>.Enumerator enumerator = (encodedObject as Asn1Set).Objects.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                AsnObject current = enumerator.Current;
                asn1Sequence.Objects.Add(current);
              }
              break;
            }
        }
        foreach (AsnObject encodedObject1 in asn1Sequence)
        {
          byte[] buffer = (byte[]) null;
          switch (encodedObject1)
          {
            case Asn1Integer _:
              buffer = (encodedObject1 as Asn1Integer).AsnEncode();
              break;
            case Asn1Boolean _:
              buffer = (encodedObject1 as Asn1Boolean).AsnEncode();
              break;
            case Asn1Null _:
              buffer = (encodedObject1 as Asn1Null).AsnEncode();
              break;
            case Asn1ObjectIdentifier _:
              buffer = (encodedObject1 as Asn1ObjectIdentifier).AsnEncode();
              break;
            case Asn1TaggedObject _:
              if ((encodedObject1 as Asn1TaggedObject).Objects is Asn1Sequence)
                buffer = new Asn1OutputStream().ParseTimeStampToken((AsnObject) new Asn1Sequence(new List<AsnObject>()
                {
                  (encodedObject1 as Asn1TaggedObject).Objects
                }));
              else if ((encodedObject1 as Asn1TaggedObject).Objects is Asn1TaggedObject)
                buffer = new Asn1OutputStream().ParseTimeStampToken((encodedObject1 as Asn1TaggedObject).Objects);
              else if ((encodedObject1 as Asn1TaggedObject).Objects is Asn1OctetString)
              {
                Asn1OutputStream asn1OutputStream = new Asn1OutputStream();
                buffer = ((encodedObject1 as Asn1TaggedObject).Objects as Asn1OctetString).AsnEncode();
              }
              else if ((encodedObject1 as Asn1TaggedObject).Objects is Asn1Integer)
              {
                Asn1OutputStream asn1OutputStream = new Asn1OutputStream();
                buffer = ((encodedObject1 as Asn1TaggedObject).Objects as Asn1Integer).AsnEncode();
              }
              if ((encodedObject1 as Asn1TaggedObject).IsExplicit)
              {
                this.m_stream.WriteByte((byte) (160 /*0xA0*/ | (encodedObject1 as Asn1TaggedObject).TagNumber));
                this.WriteCorrrectLength(buffer);
                break;
              }
              buffer[0] = (byte) ((uint) buffer[0] & 32U /*0x20*/);
              buffer[0] = (byte) ((uint) buffer[0] | (uint) (byte) (128 /*0x80*/ | (encodedObject1 as Asn1TaggedObject).TagNumber));
              break;
            case Asn1Set _:
              buffer = new Asn1OutputStream().ParseTimeStampToken(encodedObject1);
              this.m_stream.WriteByte((byte) 49);
              this.WriteCorrrectLength(buffer);
              break;
            case Asn1Sequence _:
              Asn1OutputStream asn1OutputStream1 = new Asn1OutputStream();
              asn1OutputStream1.ParseTimeStampToken(encodedObject1);
              buffer = asn1OutputStream1.m_stream.ToArray();
              this.m_stream.WriteByte((byte) 48 /*0x30*/);
              this.WriteCorrrectLength(buffer);
              break;
            case Asn1OctetString _:
              buffer = (encodedObject1 as Asn1OctetString).AsnEncode();
              break;
            case AlgorithmIdentifier _:
              buffer = (encodedObject1 as AlgorithmIdentifier).AsnEncode();
              break;
            case Asn1UTFTime _:
              buffer = (encodedObject1 as Asn1UTFTime).AsnEncode();
              break;
            case Asn1BitString _:
              buffer = (encodedObject1 as Asn1BitString).AsnEncode();
              break;
            case Asn1PrintableString _:
              buffer = (encodedObject1 as Asn1PrintableString).AsnEncode();
              break;
            case Asn1IA5String _:
              buffer = (encodedObject1 as Asn1IA5String).AsnEncode();
              break;
          }
          this.m_stream.Write(buffer, 0, buffer.Length);
        }
        this.m_stream.Close();
        return this.m_stream.ToArray();
      }

      private void WriteCorrrectLength(byte[] buffer)
      {
        if (buffer.Length > (int) sbyte.MaxValue)
        {
          int num = 1;
          uint length = (uint) buffer.Length;
          while ((length >>= 8) != 0U)
            ++num;
          this.m_stream.WriteByte((byte) (num | 128 /*0x80*/));
          for (int index = (num - 1) * 8; index >= 0; index -= 8)
            this.m_stream.WriteByte((byte) (buffer.Length >> index));
        }
        else
          this.m_stream.WriteByte((byte) buffer.Length);
      }
    }
}
