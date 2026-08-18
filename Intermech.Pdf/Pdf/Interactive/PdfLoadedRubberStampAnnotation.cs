// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedRubberStampAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedRubberStampAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfCrossTable m_crossTable;
      private PdfRubberStampAnnotationIcon m_name;

      internal PdfLoadedRubberStampAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle,
        string text)
        : base(dictionary, crossTable)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
        this.Text = text;
      }

      private PdfRubberStampAnnotationIcon GetIcon()
      {
        PdfRubberStampAnnotationIcon icon = PdfRubberStampAnnotationIcon.Draft;
        if (this.Dictionary.ContainsKey("Name"))
          icon = this.GetIconName((this.Dictionary["Name"] as PdfName).Value.ToString());
        return icon;
      }

      private PdfRubberStampAnnotationIcon GetIconName(string name)
      {
        PdfRubberStampAnnotationIcon iconName = PdfRubberStampAnnotationIcon.Draft;
        switch (name)
        {
          case "Approved":
            return PdfRubberStampAnnotationIcon.Approved;
          case "AsIs":
            return PdfRubberStampAnnotationIcon.AsIs;
          case "Confidential":
            return PdfRubberStampAnnotationIcon.Confidential;
          case "Departmental":
            return PdfRubberStampAnnotationIcon.Departmental;
          case "Draft":
            return PdfRubberStampAnnotationIcon.Draft;
          case "Experimental":
            return PdfRubberStampAnnotationIcon.Experimental;
          case "Expired":
            return PdfRubberStampAnnotationIcon.Expired;
          case "Final":
            return PdfRubberStampAnnotationIcon.Final;
          case "ForComment":
            return PdfRubberStampAnnotationIcon.ForComment;
          case "ForPublicRelease":
            return PdfRubberStampAnnotationIcon.ForPublicRelease;
          case "NotApproved":
            return PdfRubberStampAnnotationIcon.NotApproved;
          case "NotForPublicRelease":
            return PdfRubberStampAnnotationIcon.NotForPublicRelease;
          case "Sold":
            return PdfRubberStampAnnotationIcon.Sold;
          case "TopSecret":
            return PdfRubberStampAnnotationIcon.TopSecret;
          default:
            return iconName;
        }
      }

      public PdfRubberStampAnnotationIcon Icon
      {
        get => this.GetIcon();
        set
        {
          this.m_name = value;
          this.Dictionary.SetName("Name", this.m_name.ToString());
        }
      }
    }
}
