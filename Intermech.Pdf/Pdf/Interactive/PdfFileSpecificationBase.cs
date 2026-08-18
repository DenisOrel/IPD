// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFileSpecificationBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfFileSpecificationBase : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();

      public PdfFileSpecificationBase(string fileName)
      {
        if (fileName == null)
          throw new ArgumentNullException(nameof (fileName));
        this.Initialize();
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      protected string FormatFileName(string fileName, bool flag)
      {
        switch (fileName)
        {
          case null:
            throw new ArgumentNullException(nameof (fileName));
          case "":
            throw new ArgumentException("fileName - string can not be empty");
          default:
            string str = fileName.Replace("\\", "/").Replace(":", string.Empty);
            if (str.Substring(0, 2) == "\\")
              str = str.Remove(1, 1);
            if (str.Substring(0, 1) != "/" && !flag)
              str = "/" + str;
            return str;
        }
      }

      protected virtual void Initialize()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Filespec"));
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
      }

      protected abstract void Save();

      internal PdfDictionary Dictionary => this.m_dictionary;

      public abstract string FileName { get; set; }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
