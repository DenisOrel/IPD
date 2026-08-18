// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfStream
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Compression;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Security;
using System;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.Primitives
{
    internal class PdfStream : PdfDictionary, IPdfDecryptable
    {
      private bool m_bCompress;
      private bool m_bDecrypted;
      private bool m_blockEncryption;
      private PdfStream m_clonedObject;
      private MemoryStream m_dataStream;
      private const string Prefix = "stream";
      private const string Suffix = "endstream";

      internal PdfStream()
      {
        this.m_dataStream = new MemoryStream(100);
        this.m_bCompress = true;
      }

      internal PdfStream(PdfDictionary dictionary, byte[] data)
        : base(dictionary)
      {
        this.m_dataStream = new MemoryStream(data.Length);
        this.Data = data;
        this.m_bCompress = false;
        this["Length"] = (IPdfPrimitive) new PdfNumber(data.Length);
        if (this.ContainsKey("Length3") && this.ContainsKey("Filter") && (object) (this["Length3"] as PdfReferenceHolder) != null)
        {
          byte[] bytes = this.Decompress(data, "FlateDecode");
          string[] strArray1 = Encoding.UTF8.GetString(bytes, 0, bytes.Length).Split(new string[1]
          {
            "eexec"
          }, StringSplitOptions.None);
          string empty = string.Empty;
          for (int index = 0; index < 32 /*0x20*/; ++index)
            empty += "0";
          int num1 = strArray1[0].Length + 7;
          if (strArray1.Length > 1)
          {
            string[] strArray2 = strArray1[1].Split(new string[1]
            {
              empty
            }, StringSplitOptions.None);
            int num2 = 0;
            for (int index = 1; index < strArray2.Length; ++index)
              num2 += strArray2[index].Length;
            int num3 = 32 /*0x20*/ * (strArray2.Length - 1) + num2;
            Encoding.UTF8.GetBytes(strArray2[0]);
            this["Length1"] = (IPdfPrimitive) new PdfNumber(num1);
            this["Length2"] = (IPdfPrimitive) new PdfNumber(bytes.Length - num1 - num3);
            this["Length3"] = (IPdfPrimitive) new PdfNumber(num3);
          }
        }
        if (!this.ContainsKey("Length1") || !this.ContainsKey("Filter") || (object) (this["Length1"] as PdfReferenceHolder) == null || !(this["Filter"] is PdfArray))
          return;
        this["Length1"] = (IPdfPrimitive) new PdfNumber(data.Length);
      }

      private void AddFilter(string filterName)
      {
        IPdfPrimitive pdfPrimitive = this["Filter"];
        if ((object) (pdfPrimitive as PdfReferenceHolder) != null)
          pdfPrimitive = (pdfPrimitive as PdfReferenceHolder).Object;
        PdfArray pdfArray = pdfPrimitive as PdfArray;
        PdfName element1 = pdfPrimitive as PdfName;
        if (element1 != (PdfName) null)
        {
          pdfArray = new PdfArray();
          pdfArray.Insert(0, (IPdfPrimitive) element1);
          this["Filter"] = (IPdfPrimitive) pdfArray;
        }
        PdfName element2 = new PdfName(filterName);
        if (pdfArray == null)
          this["Filter"] = (IPdfPrimitive) element2;
        else
          pdfArray.Insert(0, (IPdfPrimitive) element2);
      }

      internal void BlockEncryption() => this.m_blockEncryption = true;

      internal new void Clear()
      {
        this.InternalStream.SetLength(0L);
        this.InternalStream.Position = 0L;
        this.Remove("Filter");
        this.m_bCompress = true;
        this.Modify();
      }

      public override IPdfPrimitive Clone(PdfCrossTable crossTable)
      {
        if (this.m_clonedObject != null && this.m_clonedObject.CrossTable == crossTable)
          return (IPdfPrimitive) this.m_clonedObject;
        this.m_clonedObject = (PdfStream) null;
        PdfStream pdfStream = new PdfStream(base.Clone(crossTable) as PdfDictionary, this.m_dataStream.ToArray());
        pdfStream.Compress = this.m_bCompress;
        pdfStream.m_bDecrypted = this.m_bDecrypted;
        this.m_clonedObject = pdfStream;
        return (IPdfPrimitive) pdfStream;
      }

      private byte[] CompressContent(IPdfWriter writer)
      {
        PdfCompressionLevel compression = writer.Document.Compression;
        int num = compression != 0 ? 1 : 0;
        byte[] data = this.Data;
        if (num != 0 && this.m_bCompress)
        {
          data = new PdfZlibCompressor(compression).Compress(data);
          this.AddFilter("FlateDecode");
        }
        return data;
      }

      internal void Decompress()
      {
        IPdfPrimitive pdfPrimitive1 = this["Filter"];
        if ((object) (pdfPrimitive1 as PdfReferenceHolder) != null)
          pdfPrimitive1 = (pdfPrimitive1 as PdfReferenceHolder).Object;
        if (pdfPrimitive1 != null)
        {
          if ((object) (pdfPrimitive1 as PdfName) == null)
          {
            if (!(pdfPrimitive1 is PdfArray))
              throw new PdfDocumentException("Invalid/Unknown/Unsupported formatUnexpected object for filter.");
            foreach (IPdfPrimitive pdfPrimitive2 in pdfPrimitive1 as PdfArray)
              this.Data = this.Decompress(this.Data, (pdfPrimitive2 as PdfName).Value ?? throw new PdfDocumentException("Invalid/Unknown/Unsupported format"));
          }
          else
            this.Data = this.Decompress(this.Data, (pdfPrimitive1 as PdfName).Value);
        }
        this.Remove("Filter");
        this.m_bCompress = true;
      }

      private byte[] Decompress(byte[] data, string filter)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data));
        if (filter == null)
          throw new ArgumentNullException(nameof (filter));
        if (data.Length == 0 || !(filter != "Crypt"))
          return data;
        if (!filter.Equals("RunLengthDecode"))
          return this.PostProcess(this.DetermineCompressor(filter).Decompress(data), filter);
        Stream stream = (Stream) new MemoryStream(data);
        MemoryStream memoryStream = new MemoryStream();
        byte[] buffer = new byte[128 /*0x80*/];
        int num1;
        while ((num1 = stream.ReadByte()) != -1 && num1 != 128 /*0x80*/)
        {
          if (num1 <= (int) sbyte.MaxValue)
          {
            int count1 = num1 + 1;
            int count2;
            for (; count1 > 0; count1 -= count2)
            {
              count2 = stream.Read(buffer, 0, count1);
              memoryStream.Write(buffer, 0, count2);
            }
          }
          else
          {
            int num2 = stream.ReadByte();
            for (int index = 0; index < 257 - num1; ++index)
              memoryStream.WriteByte((byte) num2);
          }
        }
        memoryStream.Position = 0L;
        return memoryStream.ToArray();
      }

      public void Decrypt(PdfEncryptor encryptor, long currObjNumber)
      {
        if (encryptor == null || this.m_bDecrypted)
          return;
        this.m_bDecrypted = true;
        this.Data = encryptor.EncryptData(currObjNumber, this.Data, false);
      }

      private IPdfCompressor DetermineCompressor(string filter)
      {
        switch (filter)
        {
          case null:
            throw new ArgumentNullException(nameof (filter));
          case "A85":
          case "ASCII85Decode":
            return (IPdfCompressor) new PdfASCII85Compressor();
          case "Fl":
          case "FlateDecode":
            return (IPdfCompressor) new PdfZlibCompressor();
          case "LZW":
          case "LZWDecode":
            return (IPdfCompressor) new PdfLzwCompressor();
          default:
            throw new PdfDocumentException($"Invalid/Unknown/Unsupported format Unsupported compressor ({filter}).");
        }
      }

      private byte[] EncryptContent(byte[] data, IPdfWriter writer)
      {
        PdfDocumentBase document = writer.Document;
        PdfEncryptor encryptor = document.Security.Encryptor;
        if (encryptor.Encrypt && !this.m_blockEncryption)
          data = encryptor.EncryptData(document.CurrentSavingObj.ObjNum, data, true);
        return data;
      }

      private void NormalizeFilter()
      {
        if (!(this["Filter"] is PdfArray pdfArray) || pdfArray.Count != 1)
          return;
        this["Filter"] = pdfArray[0];
      }

      private byte[] PostProcess(byte[] data, string filter)
      {
        if (!(filter == "FlateDecode"))
          return data;
        IPdfPrimitive pdfPrimitive1 = this["DecodeParms"];
        if (pdfPrimitive1 == null)
          return data;
        PdfDictionary pdfDictionary = pdfPrimitive1 as PdfDictionary;
        PdfArray pdfArray = pdfPrimitive1 as PdfArray;
        if (pdfDictionary == null && pdfArray == null)
          throw new PdfDocumentException("Invalid/Unknown/Unsupported format");
        int num1 = pdfDictionary != null ? (pdfDictionary["Predictor"] as PdfNumber).IntValue : (pdfArray[0] is PdfDictionary ? (pdfArray[0] as PdfDictionary).GetInt("Predictor") : 1);
        switch (num1)
        {
          case 1:
            return data;
          case 2:
            throw new PdfDocumentException("Unsupported predictor: TIFF 2.");
          default:
            if (num1 >= 16 /*0x10*/ || num1 <= 2)
              throw new PdfDocumentException("Invalid/Unknown/Unsupported format Unknown predictor code: " + num1.ToString());
            int num2 = 1;
            int num3 = 1;
            IPdfPrimitive pdfPrimitive2 = pdfDictionary["Colors"];
            if (pdfPrimitive2 != null)
              num2 = (pdfPrimitive2 as PdfNumber).IntValue;
            IPdfPrimitive pdfPrimitive3 = pdfDictionary["Columns"];
            if (pdfPrimitive3 != null)
              num3 = (pdfPrimitive3 as PdfNumber).IntValue;
            IPdfPrimitive pdfPrimitive4 = pdfDictionary["BitsPerComponent"];
            if (pdfPrimitive4 != null)
            {
              int intValue = (pdfPrimitive4 as PdfNumber).IntValue;
            }
            return PdfPngFilter.Decompress(data, num2 * num3);
        }
      }

      public override void Save(IPdfWriter writer)
      {
        this.OnBeginSave(new SavePdfPrimitiveEventArgs(writer));
        byte[] data = this.EncryptContent(this.CompressContent(writer), writer);
        this["Length"] = (IPdfPrimitive) new PdfNumber(data.Length);
        if (this.ContainsKey("Length1") && this.ContainsKey("Filter") && (object) (this["Length1"] as PdfReferenceHolder) != null && this["Filter"] is PdfArray)
          this["Length1"] = (IPdfPrimitive) new PdfNumber(data.Length);
        if (this.ContainsKey("Length3") && this.ContainsKey("Filter") && (object) (this["Length3"] as PdfReferenceHolder) != null)
        {
          byte[] bytes = this.Decompress(data, "FlateDecode");
          string[] strArray1 = Encoding.UTF8.GetString(bytes, 0, bytes.Length).Split(new string[1]
          {
            "eexec"
          }, StringSplitOptions.None);
          string empty = string.Empty;
          for (int index = 0; index < 32 /*0x20*/; ++index)
            empty += "0";
          int num1 = strArray1[0].Length + 7;
          if (strArray1.Length > 1)
          {
            string[] strArray2 = strArray1[1].Split(new string[1]
            {
              empty
            }, StringSplitOptions.None);
            int num2 = 0;
            for (int index = 1; index < strArray2.Length; ++index)
              num2 += strArray2[index].Length;
            int num3 = 32 /*0x20*/ * (strArray2.Length - 1) + num2;
            Encoding.UTF8.GetBytes(strArray2[0]);
            this["Length1"] = (IPdfPrimitive) new PdfNumber(num1);
            this["Length2"] = (IPdfPrimitive) new PdfNumber(bytes.Length - num1 - num3);
            this["Length3"] = (IPdfPrimitive) new PdfNumber(num3);
          }
        }
        this.Save(writer, false);
        writer.Write("stream");
        writer.Write("\r\n");
        if (data.Length != 0)
        {
          writer.Write(data);
          writer.Write("\r\n");
        }
        writer.Write("endstream");
        writer.Write("\r\n");
        this.OnEndSave(new SavePdfPrimitiveEventArgs(writer));
        if (!this.m_bCompress)
          return;
        this.Remove("Filter");
      }

      public static byte[] StreamToBigEndian(Stream stream)
      {
        return Encoding.Convert(Encoding.Unicode, Encoding.BigEndianUnicode, PdfStream.StreamToBytes(stream));
      }

      public static byte[] StreamToBytes(Stream stream)
      {
        return stream != null ? PdfStream.StreamToBytes(stream, false) : throw new ArgumentNullException(nameof (stream));
      }

      public static byte[] StreamToBytes(Stream stream, bool writeWholeStream)
      {
        long num1 = stream != null ? stream.Position : throw new ArgumentNullException(nameof (stream));
        long num2 = stream.Position != 0L ? stream.Position : stream.Length;
        long count = writeWholeStream ? stream.Length : num2;
        byte[] buffer = new byte[count];
        stream.Position = 0L;
        stream.Read(buffer, 0, (int) count);
        stream.Position = num1;
        return buffer;
      }

      internal void Write(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (text.Length <= 0)
          throw new ArgumentException("Can't write an empty string.", nameof (text));
        this.Write(Encoding.UTF8.GetBytes(text));
      }

      internal void Write(byte[] data)
      {
        if (data == null)
          throw new ArgumentNullException(nameof (data));
        if (data.Length == 0)
          throw new ArgumentException("Can't write an empty array.", nameof (data));
        this.m_dataStream.Write(data, 0, data.Length);
        this.Modify();
      }

      internal void Write(char symbol) => this.Write(symbol.ToString());

      public override IPdfPrimitive ClonedObject => (IPdfPrimitive) this.m_clonedObject;

      internal bool Compress
      {
        get => this.m_bCompress;
        set
        {
          this.m_bCompress = value;
          this.Modify();
        }
      }

      internal byte[] Data
      {
        get => this.m_dataStream.ToArray();
        set
        {
          this.m_dataStream.SetLength(0L);
          this.m_dataStream.Write(value, 0, value.Length);
          this.Modify();
        }
      }

      public bool Decrypted => this.m_bDecrypted;

      internal MemoryStream InternalStream
      {
        get => this.m_dataStream;
        set => this.m_dataStream = value;
      }

      public bool WasEncrypted => throw new Exception("The method or operation is not implemented.");
    }
}
