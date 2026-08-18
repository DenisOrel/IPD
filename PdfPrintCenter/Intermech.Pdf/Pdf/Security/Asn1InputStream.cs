// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1InputStream
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class Asn1InputStream
{
  private MemoryStream m_stream;

  public Asn1InputStream(byte[] bytes) => this.m_stream = new MemoryStream(bytes, false);

  private List<AsnObject> BuildEncodableVector()
  {
    List<AsnObject> asnObjectList = new List<AsnObject>();
    AsnObject asnObject;
    while ((asnObject = this.ReadObject()) != null)
      asnObjectList.Add(asnObject);
    return asnObjectList;
  }

  internal AsnObject BuildObject(int tag, int tagNo, byte[] bytes)
  {
    switch (tag)
    {
      case 1:
        return (AsnObject) new Asn1Boolean(bytes);
      case 2:
        return (AsnObject) new Asn1Integer((long) bytes[0]);
      case 3:
        int padBit = (int) bytes[0];
        byte[] numArray = new byte[bytes.Length - 1];
        Array.Copy((Array) bytes, 1, (Array) numArray, 0, bytes.Length - 1);
        return (AsnObject) new Asn1BitString(numArray, padBit);
      case 4:
        return (AsnObject) new Asn1OctetString(bytes);
      case 5:
        return (AsnObject) new Asn1Null();
      case 6:
        return (AsnObject) new Asn1ObjectIdentifier(bytes);
      case 19:
        return (AsnObject) new Asn1PrintableString(bytes);
      case 22:
        return (AsnObject) new Asn1IA5String(bytes);
      case 23:
        return (AsnObject) new Asn1UTFTime(bytes);
      case 24:
        return (AsnObject) new Asn1GeneralizedTime(bytes);
      case 48 /*0x30*/:
        return (AsnObject) new Asn1Sequence(this.BuildSequence(bytes));
      case 49:
        return (AsnObject) new Asn1Set(this.BuildSequence(bytes));
      default:
        if ((tag & 128 /*0x80*/) == 0)
          return (AsnObject) new Asn1Boolean(false);
        bool flag = (tag & 32 /*0x20*/) == 0;
        if (bytes.Length == 0)
          return (AsnObject) new Asn1TaggedObject(false, !flag ? (AsnObject) new Asn1Sequence() : (AsnObject) new Asn1Null(), tagNo);
        if (flag)
          return (AsnObject) new Asn1TaggedObject(false, (AsnObject) new Asn1OctetString(bytes), tagNo);
        Asn1InputStream asn1InputStream = new Asn1InputStream(bytes);
        AsnObject asnObject = asn1InputStream.ReadObject();
        if (asn1InputStream.m_stream.Position == (long) bytes.Length)
          return (AsnObject) new Asn1TaggedObject(true, asnObject, tagNo);
        List<AsnObject> sequence = new List<AsnObject>();
        for (; asnObject != null; asnObject = asn1InputStream.ReadObject())
          sequence.Add(asnObject);
        return (AsnObject) new Asn1TaggedObject(false, (AsnObject) new Asn1Sequence(sequence), tagNo);
    }
  }

  private List<AsnObject> BuildSequence(byte[] bytes)
  {
    return new Asn1InputStream(bytes).BuildEncodableVector();
  }

  private void ReadFullStream(byte[] bytes)
  {
    int length = bytes.Length;
    if (length == 0)
      return;
    int num;
    while ((num = this.m_stream.Read(bytes, bytes.Length - length, length)) > 0)
    {
      length -= num;
      if (length == 0)
        return;
    }
    if (length != 0)
      throw new EndOfStreamException("EOF encountered in middle of object");
  }

  private int ReadLength()
  {
    int num1 = this.m_stream.ReadByte();
    if (num1 < 0)
      throw new IOException("EOF found when length expected");
    if (num1 == 128 /*0x80*/)
      return -1;
    if (num1 > (int) sbyte.MaxValue)
    {
      int num2 = num1 & (int) sbyte.MaxValue;
      if (num2 > 4)
        throw new IOException("DER length more than 4 bytes");
      num1 = 0;
      for (int index = 0; index < num2; ++index)
      {
        int num3 = this.m_stream.ReadByte();
        if (num3 < 0)
          throw new IOException("EOF found reading length");
        num1 = (num1 << 8) + num3;
      }
      if (num1 < 0)
        throw new IOException("corrupted steam - negative length found");
    }
    return num1;
  }

  internal AsnObject ReadObject()
  {
    int tag = this.m_stream.ReadByte();
    if (tag == -1)
      return (AsnObject) null;
    int tagNo = 0;
    if ((tag & 128 /*0x80*/) != 0 || (tag & 64 /*0x40*/) != 0)
      tagNo = this.ReadTagNumber(tag);
    int length = this.ReadLength();
    if (tag == 0 && length == 0)
      return (AsnObject) null;
    byte[] bytes = new byte[length];
    this.ReadFullStream(bytes);
    return this.BuildObject(tag, tagNo, bytes);
  }

  private int ReadTagNumber(int tag)
  {
    int num1 = tag & 31 /*0x1F*/;
    if (num1 != 31 /*0x1F*/)
      return num1;
    int num2 = this.m_stream.ReadByte();
    int num3 = 0;
    for (; num2 >= 0 && (num2 & 128 /*0x80*/) != 0; num2 = this.m_stream.ReadByte())
      num3 = (num3 | num2 & (int) sbyte.MaxValue) << 7;
    return num2 < 0 ? 0 : num3 | num2 & (int) sbyte.MaxValue;
  }
}
