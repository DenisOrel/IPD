// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedFieldItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedFieldItem
    {
      private int m_collectionIndex;
      private PdfDictionary m_dictionary;
      private PdfLoadedStyledField m_field;
      private PdfPageBase m_page;

      internal PdfLoadedFieldItem(PdfLoadedStyledField field, int index, PdfDictionary dictionary)
      {
        this.m_field = field;
        this.m_collectionIndex = index;
        this.m_dictionary = dictionary;
      }

      internal PdfBrush BackBrush
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfBrush backBrush = this.m_field.BackBrush;
          this.m_field.DefaultIndex = defaultIndex;
          return backBrush;
        }
      }

      internal PdfPen BorderPen
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfPen borderPen = this.m_field.BorderPen;
          this.m_field.DefaultIndex = defaultIndex;
          return borderPen;
        }
      }

      internal PdfBorderStyle BorderStyle
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          int borderStyle = (int) this.m_field.BorderStyle;
          this.m_field.DefaultIndex = defaultIndex;
          return (PdfBorderStyle) borderStyle;
        }
      }

      internal int BorderWidth
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          int borderWidth = this.m_field.BorderWidth;
          this.m_field.DefaultIndex = defaultIndex;
          return borderWidth;
        }
      }

      public RectangleF Bounds
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          RectangleF bounds = this.m_field.Bounds;
          this.m_field.DefaultIndex = defaultIndex;
          return bounds;
        }
        set
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          this.m_field.Bounds = value;
          this.m_field.DefaultIndex = defaultIndex;
        }
      }

      internal PdfCrossTable CrossTable => this.Parent.CrossTable;

      internal float[] DashPatern
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          float[] dashPatern = this.m_field.DashPatern;
          this.m_field.DefaultIndex = defaultIndex;
          return dashPatern;
        }
      }

      internal PdfDictionary Dictionary => this.m_dictionary;

      protected PdfLoadedStyledField Field => this.m_field;

      internal PdfFont Font
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfFont font = this.m_field.Font;
          this.m_field.DefaultIndex = defaultIndex;
          return font;
        }
      }

      internal PdfBrush ForeBrush
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfBrush foreBrush = this.m_field.ForeBrush;
          this.m_field.DefaultIndex = defaultIndex;
          return foreBrush;
        }
      }

      public PointF Location
      {
        get => this.Bounds.Location;
        set => this.Bounds = new RectangleF(value, this.Bounds.Size);
      }

      public PdfPageBase Page
      {
        get
        {
          if (this.m_page == null)
          {
            int defaultIndex = this.m_field.DefaultIndex;
            this.m_field.DefaultIndex = this.m_collectionIndex;
            this.m_page = this.m_field.Page;
            PdfName key = new PdfName("P");
            if (this.m_field.Kids.Count > 0 && this.m_dictionary.ContainsKey(key))
            {
              PdfLoadedDocument document = this.CrossTable.Document as PdfLoadedDocument;
              if (this.CrossTable.GetObject(this.m_dictionary["P"]) is PdfDictionary dic)
                this.m_page = document.Pages.GetPage(dic);
            }
            this.m_field.DefaultIndex = defaultIndex;
          }
          return this.m_page;
        }
        internal set => this.m_page = value;
      }

      internal PdfLoadedStyledField Parent => this.m_field;

      internal PdfBrush ShadowBrush
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfBrush shadowBrush = this.m_field.ShadowBrush;
          this.m_field.DefaultIndex = defaultIndex;
          return shadowBrush;
        }
      }

      public SizeF Size
      {
        get => this.Bounds.Size;
        set => this.Bounds = new RectangleF(this.Bounds.Location, value);
      }

      internal PdfStringFormat StringFormat
      {
        get
        {
          int defaultIndex = this.m_field.DefaultIndex;
          this.m_field.DefaultIndex = this.m_collectionIndex;
          PdfStringFormat stringFormat = this.m_field.StringFormat;
          this.m_field.DefaultIndex = defaultIndex;
          return stringFormat;
        }
      }
    }
}
