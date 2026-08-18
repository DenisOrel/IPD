// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfPopupAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfPopupAnnotation : PdfAnnotation
    {
      private PdfAppearance m_appearance;
      private PdfPopupIcon m_icon;
      private bool m_open;

      public PdfPopupAnnotation()
      {
      }

      public PdfPopupAnnotation(RectangleF rectangle)
        : base(rectangle)
      {
      }

      public PdfPopupAnnotation(RectangleF rectangle, string text)
        : base(rectangle)
      {
        this.Text = text != null ? text : throw new ArgumentNullException(nameof (text));
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Text"));
      }

      protected override void Save()
      {
        base.Save();
        if (this.m_appearance == null || this.m_appearance.Normal == null)
          return;
        this.Dictionary.SetProperty("AP", (IPdfWrapper) this.m_appearance);
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

      public PdfPopupIcon Icon
      {
        get => this.m_icon;
        set
        {
          if (this.m_icon == value)
            return;
          this.m_icon = value;
          this.Dictionary.SetName("Name", this.m_icon.ToString());
        }
      }

      public bool Open
      {
        get => this.m_open;
        set
        {
          if (this.m_open == value)
            return;
          this.m_open = value;
          this.Dictionary.SetBoolean(nameof (Open), this.m_open);
        }
      }
    }
}
