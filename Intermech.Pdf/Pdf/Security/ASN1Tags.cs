// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.ASN1Tags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Security
{
    [Flags]
    internal enum ASN1Tags
    {
      Application = 64, // 0x00000040
      BitString = 3,
      BMPString = 30, // 0x0000001E
      Boolean = 1,
      CharacterString = 29, // 0x0000001D
      Constructed = 32, // 0x00000020
      EmbeddedPDV = 11, // 0x0000000B
      Enumerated = 10, // 0x0000000A
      External = 8,
      GeneralizedTime = 24, // 0x00000018
      GeneralString = 27, // 0x0000001B
      GraphicsString = GeneralizedTime | Boolean, // 0x00000019
      IA5String = 22, // 0x00000016
      Integer = 2,
      Null = 5,
      NumericString = 18, // 0x00000012
      ObjectDescriptor = Null | Integer, // 0x00000007
      ObjectIdentifier = 6,
      OctetString = 4,
      PrintableString = NumericString | Boolean, // 0x00000013
      Real = External | Boolean, // 0x00000009
      RelativeOid = Real | OctetString, // 0x0000000D
      ReservedBER = 0,
      Sequence = 16, // 0x00000010
      Set = Sequence | Boolean, // 0x00000011
      Tagged = 128, // 0x00000080
      TeletexString = Sequence | OctetString, // 0x00000014
      UniversalString = TeletexString | External, // 0x0000001C
      UTF8String = OctetString | External, // 0x0000000C
      UTFTime = TeletexString | Integer | Boolean, // 0x00000017
      VideotexString = TeletexString | Boolean, // 0x00000015
      VisibleString = Sequence | Integer | External, // 0x0000001A
    }
}
