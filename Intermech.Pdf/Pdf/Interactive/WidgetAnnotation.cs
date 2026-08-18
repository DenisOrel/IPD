// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.WidgetAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    internal class WidgetAnnotation : PdfAnnotation
    {
      private PdfAnnotationActions m_actions;
      private PdfTextAlignment m_alignment;
      private PdfAppearance m_appearance;
      private string m_appearanceState;
      private WidgetBorder m_border = new WidgetBorder();
      private PdfDefaultAppearance m_defaultAppearance;
      private PdfExtendedAppearance m_extendedAppearance;
      private PdfHighlightMode m_highlightMode = PdfHighlightMode.Invert;
      private PdfField m_parent;
      private WidgetAppearance m_widgetAppearance = new WidgetAppearance();

      internal event EventHandler BeginSave;

      internal PdfAppearance GetAppearance() => this.m_appearance;

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

      protected override void Initialize()
      {
        base.Initialize();
        this.AnnotationFlags |= PdfAnnotationFlags.Print;
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Widget"));
        this.Dictionary.SetProperty("BS", (IPdfWrapper) this.m_border);
      }

      protected virtual void OnBeginSave(EventArgs args)
      {
        if (this.BeginSave == null)
          return;
        this.BeginSave((object) this, args);
      }

      protected override void Save()
      {
        base.Save();
        this.OnBeginSave(new EventArgs());
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
        if (this.m_defaultAppearance == null)
          return;
        this.Dictionary.SetProperty("DA", (IPdfPrimitive) new PdfString(this.m_defaultAppearance.ToString()));
      }

      public PdfAnnotationActions Actions
      {
        get
        {
          if (this.m_actions == null)
          {
            this.m_actions = new PdfAnnotationActions();
            this.Dictionary.SetProperty("AA", (IPdfWrapper) this.m_actions);
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

      public PdfDefaultAppearance DefaultAppearance
      {
        get
        {
          if (this.m_defaultAppearance == null)
            this.m_defaultAppearance = new PdfDefaultAppearance();
          return this.m_defaultAppearance;
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
        set => this.m_extendedAppearance = value;
      }

      public PdfHighlightMode HighlightMode
      {
        get => this.m_highlightMode;
        set
        {
          if (this.m_highlightMode == value)
            return;
          this.m_highlightMode = value;
          this.Dictionary.SetName("H", this.HighlightModeToString(this.m_highlightMode));
        }
      }

      public PdfField Parent
      {
        get => this.m_parent;
        set
        {
          if (this.m_parent == value)
            return;
          this.m_parent = value;
          if (this.m_parent != null)
            this.Dictionary.SetProperty(nameof (Parent), (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_parent));
          else
            this.Dictionary.Remove(nameof (Parent));
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
        }
      }

      public WidgetAppearance WidgetAppearance => this.m_widgetAppearance;

      public WidgetBorder WidgetBorder => this.m_border;
    }
}
