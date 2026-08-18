// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1ObjectIdentifier
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.Security
{
    internal class Asn1ObjectIdentifier : AsnObject
    {
      private char c_tokenSeparator;
      private string m_oid;

      public Asn1ObjectIdentifier(string oid)
        : base(ASN1Tags.ObjectIdentifier)
      {
        this.c_tokenSeparator = '.';
        this.m_oid = !string.IsNullOrEmpty(oid) ? oid : throw new ArgumentNullException("Oid");
      }

      public Asn1ObjectIdentifier(byte[] bytes)
        : base(ASN1Tags.ObjectIdentifier)
      {
        this.c_tokenSeparator = '.';
        this.m_oid = this.CreateOidString(bytes);
      }

      private void AppendField(long value, Stream outStream)
      {
        if (value >= 128L /*0x80*/)
        {
          if (value >= 16384L /*0x4000*/)
          {
            if (value >= 2097152L /*0x200000*/)
            {
              if (value >= 268435456L /*0x10000000*/)
              {
                if (value >= 34359738368L /*0x0800000000*/)
                {
                  if (value >= 4398046511104L /*0x040000000000*/)
                  {
                    if (value >= 562949953421312L /*0x02000000000000*/)
                    {
                      if (value >= 72057594037927936L /*0x0100000000000000*/)
                        outStream.WriteByte((byte) ((ulong) (value >> 56) | 128UL /*0x80*/));
                      outStream.WriteByte((byte) ((ulong) (value >> 49) | 128UL /*0x80*/));
                    }
                    outStream.WriteByte((byte) ((ulong) (value >> 42) | 128UL /*0x80*/));
                  }
                  outStream.WriteByte((byte) ((ulong) (value >> 35) | 128UL /*0x80*/));
                }
                outStream.WriteByte((byte) ((ulong) (value >> 28) | 128UL /*0x80*/));
              }
              outStream.WriteByte((byte) ((ulong) (value >> 21) | 128UL /*0x80*/));
            }
            outStream.WriteByte((byte) ((ulong) (value >> 14) | 128UL /*0x80*/));
          }
          outStream.WriteByte((byte) ((ulong) (value >> 7) | 128UL /*0x80*/));
        }
        outStream.WriteByte((byte) ((ulong) value & (ulong) sbyte.MaxValue));
      }

      public byte[] AsnEncode() => this.AsnEncode(this.ToArray());

      private string CreateOidString(byte[] bytes)
      {
        StringBuilder stringBuilder = new StringBuilder();
        long num1 = 0;
        bool flag = true;
        for (int index = 0; index != bytes.Length; ++index)
        {
          int num2 = (int) bytes[index];
          if (num1 < 36028797018963968L /*0x80000000000000*/)
          {
            num1 = num1 * 128L /*0x80*/ + (long) (num2 & (int) sbyte.MaxValue);
            if ((num2 & 128 /*0x80*/) == 0)
            {
              if (flag)
              {
                switch ((int) num1 / 40)
                {
                  case 0:
                    stringBuilder.Append('0');
                    break;
                  case 1:
                    stringBuilder.Append('1');
                    num1 -= 40L;
                    break;
                  default:
                    stringBuilder.Append('2');
                    num1 -= 80L /*0x50*/;
                    break;
                }
                flag = false;
              }
              stringBuilder.Append('.');
              stringBuilder.Append(num1);
              num1 = 0L;
            }
          }
        }
        return stringBuilder.ToString();
      }

      private byte[] ToArray()
      {
        string[] strArray = this.m_oid.Split(this.c_tokenSeparator);
        int num1 = int.Parse(strArray[0]);
        int num2 = int.Parse(strArray[1]);
        MemoryStream outStream = new MemoryStream();
        this.AppendField((long) (num1 * 40 + num2), (Stream) outStream);
        for (int index = 2; index < strArray.Length; ++index)
        {
          string s = strArray[index];
          if (s.Length < 18)
            this.AppendField((long) int.Parse(s), (Stream) outStream);
        }
        byte[] array = outStream.ToArray();
        outStream.Dispose();
        return array;
      }
    }
}
