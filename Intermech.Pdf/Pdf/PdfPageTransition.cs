// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageTransition
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf
{
    public class PdfPageTransition : IPdfWrapper, ICloneable
    {
      private PdfDictionary m_dictionary = new PdfDictionary();
      private PdfTransitionDimension m_dimension;
      private PdfTransitionDirection m_direction;
      private float m_duration = 1f;
      private PdfTransitionMotion m_motion;
      private float m_pageDuration;
      private float m_scale = 1f;
      private PdfTransitionStyle m_style = PdfTransitionStyle.Replace;

      public PdfPageTransition()
      {
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("Trans"));
      }

      public object Clone() => this.MemberwiseClone();

      private string DimensionToString(PdfTransitionDimension dimension)
      {
        return dimension == PdfTransitionDimension.Vertical ? "V" : "H";
      }

      private string MotionToString(PdfTransitionMotion motion)
      {
        return motion == PdfTransitionMotion.Outward ? "O" : "I";
      }

      private string StyleToString(PdfTransitionStyle style)
      {
        return style == PdfTransitionStyle.Replace ? "R" : style.ToString();
      }

      public PdfTransitionDimension Dimension
      {
        get => this.m_dimension;
        set
        {
          this.m_dimension = value;
          this.m_dictionary.SetProperty("Dm", (IPdfPrimitive) new PdfName(this.DimensionToString(this.m_dimension)));
        }
      }

      public PdfTransitionDirection Direction
      {
        get => this.m_direction;
        set
        {
          this.m_direction = value;
          this.m_dictionary.SetProperty("Di", (IPdfPrimitive) new PdfNumber((int) this.m_direction));
        }
      }

      public float Duration
      {
        get => this.m_duration;
        set
        {
          this.m_duration = value;
          this.m_dictionary.SetProperty("D", (IPdfPrimitive) new PdfNumber(this.m_duration));
        }
      }

      public PdfTransitionMotion Motion
      {
        get => this.m_motion;
        set
        {
          this.m_motion = value;
          this.m_dictionary.SetProperty("M", (IPdfPrimitive) new PdfName(this.MotionToString(this.m_motion)));
        }
      }

      public float PageDuration
      {
        get => this.m_pageDuration;
        set => this.m_pageDuration = value;
      }

      public float Scale
      {
        get => this.m_scale;
        set
        {
          this.m_scale = value;
          this.m_dictionary.SetProperty("SS", (IPdfPrimitive) new PdfNumber(this.m_scale));
        }
      }

      public PdfTransitionStyle Style
      {
        get => this.m_style;
        set
        {
          this.m_style = value;
          this.m_dictionary.SetProperty("S", (IPdfPrimitive) new PdfName(this.StyleToString(this.m_style)));
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
