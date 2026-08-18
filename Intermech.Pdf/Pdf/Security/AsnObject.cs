// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.AsnObject
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.IO;


namespace Syncfusion.Pdf.Security
{
    internal class AsnObject
    {
      private MemoryStream m_outStream;
      private ASN1Tags m_tag;

      public AsnObject()
      {
      }

      public AsnObject(ASN1Tags tag) => this.m_tag = tag;

      internal byte[] AsnEncode(byte[] value)
      {
        this.m_outStream = new MemoryStream();
        this.m_outStream.WriteByte((byte) this.m_tag);
        this.WriteCorrectLength(value.Length);
        this.m_outStream.Write(value, 0, value.Length);
        this.m_outStream.Close();
        return this.m_outStream.ToArray();
      }

      private void WriteCorrectLength(int length)
      {
        if (length > (int) sbyte.MaxValue)
        {
          int num1 = 1;
          uint num2 = (uint) length;
          while ((num2 >>= 8) != 0U)
            ++num1;
          this.m_outStream.WriteByte((byte) (num1 | 128 /*0x80*/));
          for (int index = (num1 - 1) * 8; index >= 0; index -= 8)
            this.m_outStream.WriteByte((byte) (length >> index));
        }
        else
          this.m_outStream.WriteByte((byte) length);
      }
    }
}
