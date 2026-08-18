// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;


namespace Syncfusion.Pdf.Interactive
{
    internal class Pdf3DBase : IPdfWrapper
    {
      private string m_fileName = string.Empty;
      private Pdf3DStream m_stream = new Pdf3DStream();

      public Pdf3DBase(string fileName)
      {
        if (fileName == null)
          throw new ArgumentNullException(nameof (fileName));
        Utils.CheckFilePath(fileName);
        this.FileName = fileName;
        this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
      }

      protected void Save()
      {
        using (FileStream fileStream = new FileStream(this.FileName, FileMode.Open, FileAccess.Read))
        {
          byte[] bytes = Pdf3DStream.StreamToBytes((System.IO.Stream) fileStream);
          this.m_stream.Clear();
          this.m_stream.InternalStream.Write(bytes, 0, bytes.Length);
        }
      }

      private void Stream_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      public string FileName
      {
        get => this.m_fileName;
        set
        {
          switch (value)
          {
            case null:
              throw new ArgumentNullException(nameof (FileName));
            case "":
              throw new ArithmeticException("FileName can't be empty string.");
            default:
              this.m_fileName = Path.GetFullPath(value);
              break;
          }
        }
      }

      public Pdf3DStream Stream
      {
        get => this.m_stream;
        set => this.m_stream = value;
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stream;
    }
}
