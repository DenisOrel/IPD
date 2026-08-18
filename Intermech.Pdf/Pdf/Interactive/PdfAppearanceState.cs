// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAppearanceState
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfAppearanceState : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();
      private PdfTemplate m_off;
      private string m_offMappingName = nameof (Off);
      private PdfTemplate m_on;
      private string m_onMappingName = "Yes";

      public PdfAppearanceState()
      {
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
      {
        if (this.m_on != null)
          this.m_dictionary.SetProperty(this.m_onMappingName, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_on));
        if (this.m_off == null)
          return;
        this.m_dictionary.SetProperty(this.m_offMappingName, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_off));
      }

      public PdfTemplate Off
      {
        get => this.m_off;
        set
        {
          if (this.m_off == value)
            return;
          this.m_off = value;
        }
      }

      public string OffMappingName
      {
        get => this.m_offMappingName;
        set
        {
          this.m_offMappingName = value != null ? value : throw new ArgumentNullException(nameof (OffMappingName));
        }
      }

      public PdfTemplate On
      {
        get => this.m_on;
        set
        {
          if (this.m_on == value)
            return;
          this.m_on = value;
        }
      }

      public string OnMappingName
      {
        get => this.m_onMappingName;
        set
        {
          this.m_onMappingName = value != null ? value : throw new ArgumentNullException(nameof (OnMappingName));
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
