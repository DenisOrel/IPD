// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfStringFormat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics
{
    public sealed class PdfStringFormat : ICloneable
    {
      private PdfTextAlignment m_alignment;
      private float m_characterSpacing;
      private bool m_clip;
      private float m_firstLineIndent;
      private float m_leading;
      private PdfVerticalAlignment m_lineAlignment;
      private bool m_lineLimit;
      private bool m_measureTrailingSpaces;
      private bool m_noClip;
      private float m_paragraphIndent;
      private bool m_rightToLeft;
      private float m_scalingFactor;
      private PdfSubSuperScript m_subSuperScript;
      private float m_wordSpacing;
      private PdfWordWrapType m_wrapType;

      public PdfStringFormat()
      {
        this.m_scalingFactor = 100f;
        this.m_lineLimit = true;
        this.m_wrapType = PdfWordWrapType.Word;
      }

      public PdfStringFormat(PdfTextAlignment alignment)
        : this()
      {
        this.m_alignment = alignment;
      }

      public PdfStringFormat(string columnFormat)
        : this()
      {
      }

      public PdfStringFormat(PdfTextAlignment alignment, PdfVerticalAlignment lineAlignment)
        : this(alignment)
      {
        this.m_lineAlignment = lineAlignment;
      }

      public object Clone() => (object) (PdfStringFormat) this.MemberwiseClone();

      public PdfTextAlignment Alignment
      {
        get => this.m_alignment;
        set => this.m_alignment = value;
      }

      public float CharacterSpacing
      {
        get => this.m_characterSpacing;
        set => this.m_characterSpacing = value;
      }

      public bool ClipPath
      {
        get => this.m_clip;
        set => this.m_clip = value;
      }

      internal float FirstLineIndent
      {
        get => this.m_firstLineIndent;
        set => this.m_firstLineIndent = value;
      }

      internal float HorizontalScalingFactor
      {
        get => this.m_scalingFactor;
        set
        {
          this.m_scalingFactor = (double) value > 0.0 ? value : throw new ArgumentOutOfRangeException("The scaling factor can't be less of equal to zero.", "ScalingFactor");
        }
      }

      public PdfVerticalAlignment LineAlignment
      {
        get => this.m_lineAlignment;
        set => this.m_lineAlignment = value;
      }

      public bool LineLimit
      {
        get => this.m_lineLimit;
        set => this.m_lineLimit = value;
      }

      public float LineSpacing
      {
        get => this.m_leading;
        set => this.m_leading = value;
      }

      public bool MeasureTrailingSpaces
      {
        get => this.m_measureTrailingSpaces;
        set => this.m_measureTrailingSpaces = value;
      }

      public bool NoClip
      {
        get => this.m_noClip;
        set => this.m_noClip = value;
      }

      public float ParagraphIndent
      {
        get => this.m_paragraphIndent;
        set
        {
          this.m_paragraphIndent = value;
          this.FirstLineIndent = value;
        }
      }

      public bool RightToLeft
      {
        get => this.m_rightToLeft;
        set => this.m_rightToLeft = value;
      }

      public PdfSubSuperScript SubSuperScript
      {
        get => this.m_subSuperScript;
        set => this.m_subSuperScript = value;
      }

      public float WordSpacing
      {
        get => this.m_wordSpacing;
        set => this.m_wordSpacing = value;
      }

      public PdfWordWrapType WordWrap
      {
        get => this.m_wrapType;
        set => this.m_wrapType = value;
      }
    }
}
