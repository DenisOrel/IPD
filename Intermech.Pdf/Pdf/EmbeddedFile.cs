// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.EmbeddedFile
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;


namespace Syncfusion.Pdf
{
    internal class EmbeddedFile : IPdfWrapper
    {
      private byte[] m_data;
      private string m_fileName;
      private string m_filePath;
      private string m_mimeType;
      private EmbeddedFileParams m_params;
      private PdfStream m_stream;

      public EmbeddedFile(string fileName)
      {
        this.m_fileName = string.Empty;
        this.m_filePath = string.Empty;
        this.m_mimeType = string.Empty;
        this.m_params = new EmbeddedFileParams();
        this.m_stream = new PdfStream();
        if (fileName == null)
          throw new ArgumentNullException(nameof (fileName));
        this.Initialize();
        this.FileName = fileName;
        this.FilePath = fileName;
      }

      public EmbeddedFile(string fileName, byte[] data)
        : this(fileName)
      {
        this.Data = data != null ? data : throw new ArgumentNullException(nameof (data));
      }

      public EmbeddedFile(string fileName, Stream stream)
        : this(fileName)
      {
        int count = stream != null ? (int) stream.Length : throw new ArgumentNullException(nameof (stream));
        int offset = 0;
        this.m_data = new byte[stream.Length];
        int num;
        for (; count > 0; count -= num)
        {
          num = stream.Read(this.m_data, offset, count);
          offset += num;
        }
        this.m_stream.InternalStream.Write(this.m_data, 0, this.m_data.Length);
      }

      private string GetFileName(string attachmentName)
      {
        char[] chArray = new char[2]{ '\\', '/' };
        string[] strArray = attachmentName.Split(chArray);
        return strArray[strArray.Length - 1];
      }

      protected void Initialize()
      {
        this.m_stream.SetProperty("Type", (IPdfPrimitive) new PdfName(nameof (EmbeddedFile)));
        this.m_stream.SetProperty("Params", (IPdfWrapper) this.m_params);
        this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
      }

      protected void Save()
      {
        if (this.m_data == null)
        {
          using (FileStream fileStream = new FileStream(this.m_filePath, FileMode.Open, FileAccess.Read))
            this.m_data = PdfStream.StreamToBytes((Stream) fileStream);
        }
        this.m_stream.Clear();
        this.m_stream.InternalStream.Write(this.m_data, 0, this.m_data.Length);
        this.m_params.Size = this.m_data.Length;
      }

      private void Stream_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      public byte[] Data
      {
        get => this.m_data;
        set => this.m_data = value;
      }

      public string FileName
      {
        get => this.m_fileName;
        set
        {
          if (!(this.m_fileName != value))
            return;
          this.m_fileName = this.GetFileName(value);
        }
      }

      internal string FilePath
      {
        get => this.m_filePath;
        set
        {
          if (!(this.m_filePath != value))
            return;
          this.m_filePath = value;
        }
      }

      public string MimeType
      {
        get => this.m_mimeType;
        set
        {
          if (!(this.m_mimeType != value))
            return;
          this.m_mimeType = value;
          this.m_stream.SetName("Subtype", this.m_mimeType, true);
        }
      }

      internal EmbeddedFileParams Params => this.m_params;

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stream;
    }
}
