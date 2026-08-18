// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSubmitAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfSubmitAction : PdfFormAction
    {
      private bool m_canonicalDateTimeFormat;
      private SubmitDataFormat m_dataFormat = SubmitDataFormat.Fdf;
      private bool m_embedForm;
      private bool m_excludeNonUserAnnotations;
      private string m_fileName = string.Empty;
      private PdfSubmitFormFlags m_flags;
      private HttpMethod m_httpMethod = HttpMethod.Post;
      private bool m_includeAnnotations;
      private bool m_includeIncrementalUpdates;
      private bool m_includeNoValueFields;
      private bool m_submitCoordinates;

      public PdfSubmitAction(string url)
      {
        if (url == null)
          throw new ArgumentNullException(nameof (url));
        this.m_fileName = url.Length > 0 ? url : throw new ArgumentException("The URL can't be an empty string.", nameof (url));
        this.Dictionary.SetProperty("F", (IPdfPrimitive) new PdfString(this.m_fileName));
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
      {
        this.Dictionary.SetProperty("Flags", (IPdfPrimitive) new PdfNumber((int) this.m_flags));
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
        this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("SubmitForm"));
      }

      public bool CanonicalDateTimeFormat
      {
        get => this.m_canonicalDateTimeFormat;
        set
        {
          if (this.m_canonicalDateTimeFormat == value)
            return;
          this.m_canonicalDateTimeFormat = value;
          if (this.m_canonicalDateTimeFormat)
            this.m_flags |= PdfSubmitFormFlags.CanonicalFormat;
          else
            this.m_flags &= ~PdfSubmitFormFlags.CanonicalFormat;
        }
      }

      public SubmitDataFormat DataFormat
      {
        get => this.m_dataFormat;
        set
        {
          if (this.m_dataFormat == value)
            return;
          this.m_dataFormat = value;
          switch (this.m_dataFormat)
          {
            case SubmitDataFormat.Html:
              this.m_flags |= PdfSubmitFormFlags.ExportFormat;
              break;
            case SubmitDataFormat.Pdf:
              this.m_flags |= PdfSubmitFormFlags.SubmitPdf;
              break;
            case SubmitDataFormat.Xfdf:
              this.m_flags |= PdfSubmitFormFlags.Xfdf;
              break;
          }
        }
      }

      public bool EmbedForm
      {
        get => this.m_embedForm;
        set
        {
          if (this.m_embedForm == value)
            return;
          this.m_embedForm = value;
          if (this.m_embedForm)
            this.m_flags |= PdfSubmitFormFlags.EmbedForm;
          else
            this.m_flags &= ~PdfSubmitFormFlags.EmbedForm;
        }
      }

      public bool ExcludeNonUserAnnotations
      {
        get => this.m_excludeNonUserAnnotations;
        set
        {
          if (this.m_excludeNonUserAnnotations == value)
            return;
          this.m_excludeNonUserAnnotations = value;
          if (this.m_excludeNonUserAnnotations)
            this.m_flags |= PdfSubmitFormFlags.ExclNonUserAnnots;
          else
            this.m_flags &= ~PdfSubmitFormFlags.ExclNonUserAnnots;
        }
      }

      public HttpMethod HttpMethod
      {
        get => this.m_httpMethod;
        set
        {
          if (this.m_httpMethod == value)
            return;
          this.m_httpMethod = value;
          if (this.m_httpMethod == HttpMethod.Get)
            this.m_flags |= PdfSubmitFormFlags.GetMethod;
          else
            this.m_flags &= ~PdfSubmitFormFlags.GetMethod;
        }
      }

      public override bool Include
      {
        get => base.Include;
        set
        {
          if (base.Include == value)
            return;
          base.Include = value;
          if (base.Include)
            this.m_flags &= ~PdfSubmitFormFlags.IncludeExclude;
          else
            this.m_flags |= PdfSubmitFormFlags.IncludeExclude;
        }
      }

      public bool IncludeAnnotations
      {
        get => this.m_includeAnnotations;
        set
        {
          if (this.m_includeAnnotations == value)
            return;
          this.m_includeAnnotations = value;
          if (this.m_includeAnnotations)
            this.m_flags |= PdfSubmitFormFlags.IncludeAnnotations;
          else
            this.m_flags &= ~PdfSubmitFormFlags.IncludeAnnotations;
        }
      }

      public bool IncludeIncrementalUpdates
      {
        get => this.m_includeIncrementalUpdates;
        set
        {
          if (this.m_includeIncrementalUpdates == value)
            return;
          this.m_includeIncrementalUpdates = value;
          if (this.m_includeIncrementalUpdates)
            this.m_flags |= PdfSubmitFormFlags.IncludeAppendSaves;
          else
            this.m_flags &= ~PdfSubmitFormFlags.IncludeAppendSaves;
        }
      }

      public bool IncludeNoValueFields
      {
        get => this.m_includeNoValueFields;
        set
        {
          if (this.m_includeNoValueFields == value)
            return;
          this.m_includeNoValueFields = value;
          if (this.m_includeNoValueFields)
            this.m_flags |= PdfSubmitFormFlags.IncludeNoValueFields;
          else
            this.m_flags &= ~PdfSubmitFormFlags.IncludeNoValueFields;
        }
      }

      public bool SubmitCoordinates
      {
        get => this.m_submitCoordinates;
        set
        {
          if (this.m_submitCoordinates == value)
            return;
          this.m_submitCoordinates = value;
          if (this.m_submitCoordinates)
            this.m_flags |= PdfSubmitFormFlags.SubmitCoordinates;
          else
            this.m_flags &= ~PdfSubmitFormFlags.SubmitCoordinates;
        }
      }

      public string Url => this.m_fileName;
    }
}
