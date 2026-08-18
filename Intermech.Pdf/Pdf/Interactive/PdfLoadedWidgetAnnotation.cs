// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedWidgetAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfLoadedWidgetAnnotation : PdfLoadedStyledAnnotation
    {
      private PdfAnnotationActions m_actions;
      private PdfTextAlignment m_alignment;
      private PdfAppearance m_appearance;
      private string m_appearanceState;
      private WidgetBorder m_border;
      private PdfCrossTable m_crossTable;
      private PdfDefaultAppearance m_defaultAppearance;
      private PdfExtendedAppearance m_extendedAppearance;
      private PdfAnnotationFlags m_flags;
      private PdfHighlightMode m_highlightMode;
      private WidgetAppearance m_widgetAppearance;

      internal PdfLoadedWidgetAnnotation(
        PdfDictionary dictionary,
        PdfCrossTable crossTable,
        RectangleF rectangle)
        : base(dictionary, crossTable)
      {
        this.m_border = new WidgetBorder();
        this.m_widgetAppearance = new WidgetAppearance();
        this.m_highlightMode = PdfHighlightMode.Invert;
        this.Dictionary = dictionary;
        this.m_crossTable = crossTable;
      }

      private string HighlightModeToString(PdfHighlightMode m_highlightingMode)
      {
        switch (m_highlightingMode)
        {
          case PdfHighlightMode.NoHighlighting:
            return "N";
          case PdfHighlightMode.Outline:
            return "O";
          case PdfHighlightMode.Push:
            return "P";
          default:
            return "I";
        }
      }

      public PdfAnnotationActions Actions
      {
        get
        {
          if (this.m_actions == null)
          {
            this.m_actions = new PdfAnnotationActions();
            this.Dictionary.Remove("AA");
            this.Dictionary.SetProperty("AA", (IPdfWrapper) this.m_actions);
            this.Dictionary.Modify();
          }
          return this.m_actions;
        }
      }

      public PdfAppearance Appearance
      {
        get
        {
          if (this.m_appearance == null)
            this.m_appearance = new PdfAppearance((PdfAnnotation) this);
          return this.m_appearance;
        }
        set
        {
          if (this.m_appearance == value)
            return;
          this.m_appearance = value;
        }
      }

      internal string AppearanceState
      {
        get => this.m_appearanceState;
        set
        {
          if (!(this.m_appearanceState != value))
            return;
          this.m_appearanceState = value;
          this.Dictionary.SetName("AS", value);
        }
      }

      public PdfExtendedAppearance ExtendedAppearance
      {
        get
        {
          if (this.m_extendedAppearance == null)
            this.m_extendedAppearance = new PdfExtendedAppearance();
          return this.m_extendedAppearance;
        }
        set
        {
          this.m_extendedAppearance = value;
          if (this.m_extendedAppearance != null)
          {
            this.Dictionary.SetProperty("AP", (IPdfWrapper) this.m_extendedAppearance);
            this.Dictionary.SetProperty("MK", (IPdfPrimitive) null);
          }
          else
          {
            if (this.m_appearance != null && this.m_appearance.GetNormalTemplate() != null)
              this.Dictionary.SetProperty("AP", (IPdfWrapper) this.m_appearance);
            else
              this.Dictionary.SetProperty("AP", (IPdfPrimitive) null);
            this.Dictionary.SetProperty("MK", (IPdfWrapper) this.m_widgetAppearance);
            this.Dictionary.SetProperty("AS", (IPdfPrimitive) null);
          }
        }
      }

      public PdfHighlightMode HighlightMode
      {
        get => this.m_highlightMode;
        set
        {
          this.Dictionary.SetName("H", this.HighlightModeToString(this.m_highlightMode));
          this.Dictionary.Modify();
        }
      }

      public PdfTextAlignment TextAlignment
      {
        get => this.m_alignment;
        set
        {
          if (this.m_alignment == value)
            return;
          this.m_alignment = value;
          this.Dictionary.SetProperty("Q", (IPdfPrimitive) new PdfNumber((int) this.m_alignment));
          this.Dictionary.Modify();
        }
      }
    }
}
