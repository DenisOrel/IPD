// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public abstract class PdfAction : IPdfWrapper
    {
      private PdfAction m_action;
      private PdfDictionary m_dictionary = new PdfDictionary();

      protected PdfAction() => this.Initialize();

      protected virtual void Initialize()
      {
        this.Dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Action"));
      }

      internal PdfDictionary Dictionary => this.m_dictionary;

      public PdfAction Next
      {
        get => this.m_action;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Next));
          if (this.m_action == value)
            return;
          this.m_action = value;
          this.Dictionary.SetArray(nameof (Next), (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_action));
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
