// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfDocumentTemplate
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf
{
    public class PdfDocumentTemplate
    {
      private PdfPageTemplateElement m_bottom;
      private PdfPageTemplateElement m_evenBottom;
      private PdfPageTemplateElement m_evenLeft;
      private PdfPageTemplateElement m_evenRight;
      private PdfPageTemplateElement m_evenTop;
      private PdfPageTemplateElement m_left;
      private PdfPageTemplateElement m_oddBottom;
      private PdfPageTemplateElement m_oddLeft;
      private PdfPageTemplateElement m_oddRight;
      private PdfPageTemplateElement m_oddTop;
      private PdfPageTemplateElement m_right;
      private PdfStampCollection m_stamps;
      private PdfPageTemplateElement m_top;

      private PdfPageTemplateElement CheckElement(
        PdfPageTemplateElement templateElement,
        TemplateType type)
      {
        if (templateElement != null)
          templateElement.Type = templateElement.Type == TemplateType.None ? type : throw new NotSupportedException("Can't reassign the template element. Please, create new one.");
        return templateElement;
      }

      internal PdfPageTemplateElement GetBottom(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        return this.IsEven(page) ? (this.EvenBottom == null ? this.Bottom : this.EvenBottom) : (this.OddBottom == null ? this.Bottom : this.OddBottom);
      }

      internal PdfPageTemplateElement GetLeft(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        return this.IsEven(page) ? (this.EvenLeft == null ? this.Left : this.EvenLeft) : (this.OddLeft == null ? this.Left : this.OddLeft);
      }

      internal PdfPageTemplateElement GetRight(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        return this.IsEven(page) ? (this.EvenRight == null ? this.Right : this.EvenRight) : (this.OddRight == null ? this.Right : this.OddRight);
      }

      internal PdfPageTemplateElement GetTop(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        return this.IsEven(page) ? (this.EvenTop == null ? this.Top : this.EvenTop) : (this.OddTop == null ? this.Top : this.OddTop);
      }

      private bool IsEven(PdfPage page)
      {
        if (page == null)
          throw new ArgumentNullException(nameof (page));
        PdfDocumentPageCollection pages = page.Section.Document.Pages;
        return (!pages.PageCollectionIndex.ContainsKey(page) ? pages.IndexOf(page) + 1 : pages.PageCollectionIndex[page] + 1) % 2 == 0;
      }

      public PdfPageTemplateElement Bottom
      {
        get => this.m_bottom;
        set => this.m_bottom = this.CheckElement(value, TemplateType.Bottom);
      }

      public PdfPageTemplateElement EvenBottom
      {
        get => this.m_evenBottom;
        set => this.m_evenBottom = this.CheckElement(value, TemplateType.Bottom);
      }

      public PdfPageTemplateElement EvenLeft
      {
        get => this.m_evenLeft;
        set => this.m_evenLeft = this.CheckElement(value, TemplateType.Left);
      }

      public PdfPageTemplateElement EvenRight
      {
        get => this.m_evenRight;
        set => this.m_evenRight = this.CheckElement(value, TemplateType.Right);
      }

      public PdfPageTemplateElement EvenTop
      {
        get => this.m_evenTop;
        set => this.m_evenTop = this.CheckElement(value, TemplateType.Top);
      }

      public PdfPageTemplateElement Left
      {
        get => this.m_left;
        set => this.m_left = this.CheckElement(value, TemplateType.Left);
      }

      public PdfPageTemplateElement OddBottom
      {
        get => this.m_oddBottom;
        set => this.m_oddBottom = this.CheckElement(value, TemplateType.Bottom);
      }

      public PdfPageTemplateElement OddLeft
      {
        get => this.m_oddLeft;
        set => this.m_oddLeft = this.CheckElement(value, TemplateType.Left);
      }

      public PdfPageTemplateElement OddRight
      {
        get => this.m_oddRight;
        set => this.m_oddRight = this.CheckElement(value, TemplateType.Right);
      }

      public PdfPageTemplateElement OddTop
      {
        get => this.m_oddTop;
        set => this.m_oddTop = this.CheckElement(value, TemplateType.Top);
      }

      public PdfPageTemplateElement Right
      {
        get => this.m_right;
        set => this.m_right = this.CheckElement(value, TemplateType.Right);
      }

      public PdfStampCollection Stamps
      {
        get
        {
          if (this.m_stamps == null)
            this.m_stamps = new PdfStampCollection();
          return this.m_stamps;
        }
      }

      public PdfPageTemplateElement Top
      {
        get => this.m_top;
        set => this.m_top = this.CheckElement(value, TemplateType.Top);
      }
    }
}
