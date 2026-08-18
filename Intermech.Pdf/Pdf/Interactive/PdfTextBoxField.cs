// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfTextBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfTextBoxField : PdfAppearanceField
    {
      private string m_defaultValue;
      private bool m_insertSpaces;
      private int m_maxLength;
      private bool m_multiline;
      private bool m_password;
      private const string m_passwordValue = "*";
      private bool m_scrollable;
      private bool m_spellCheck;
      private string m_text;

      internal PdfTextBoxField()
      {
        this.m_text = string.Empty;
        this.m_defaultValue = string.Empty;
        this.m_scrollable = true;
      }

      public PdfTextBoxField(PdfPageBase page, string name)
        : base(page, name)
      {
        this.m_text = string.Empty;
        this.m_defaultValue = string.Empty;
        this.m_scrollable = true;
        this.Font = PdfDocument.DefaultFont;
      }

      internal override void Draw()
      {
        base.Draw();
        if (this.Widget.GetAppearance() != null)
          this.Page.Graphics.DrawPdfTemplate(this.Appearance.Normal, this.Location);
        else
          FieldPainter.DrawTextBox(this.Page.Graphics, new PaintParams(this.Bounds, this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle), this.Text, this.Font, this.StringFormat, this.Multiline, this.Scrollable);
      }

      protected override void DrawAppearance(PdfTemplate template)
      {
        base.DrawAppearance(template);
        PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, this.ForeBrush, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
        string text = this.Text;
        if (this.Password)
        {
          text = string.Empty;
          for (int index = 0; index < this.Text.Length; ++index)
            text += "*";
        }
        FieldPainter.DrawTextBox(template.Graphics, paintParams, text, this.GetFont(), this.StringFormat, this.Multiline, this.Scrollable);
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Flags |= FieldFlags.DoNotSpellCheck;
        this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Tx"));
      }

      public string DefaultValue
      {
        get => this.m_defaultValue;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (DefaultValue));
          if (!(this.m_defaultValue != value))
            return;
          this.m_defaultValue = value;
          this.Dictionary.SetString("DV", this.m_defaultValue);
        }
      }

      public bool InsertSpaces
      {
        get => this.m_insertSpaces;
        set
        {
          if (this.m_insertSpaces == value)
            return;
          this.m_insertSpaces = value;
          if (this.m_insertSpaces)
            this.Flags |= FieldFlags.Comb;
          else
            this.Flags &= ~FieldFlags.Comb;
        }
      }

      public int MaxLength
      {
        get => this.m_maxLength;
        set
        {
          if (this.m_maxLength == value)
            return;
          this.m_maxLength = value;
          this.Dictionary.SetNumber("MaxLen", this.m_maxLength);
        }
      }

      public bool Multiline
      {
        get => this.m_multiline;
        set
        {
          if (this.m_multiline == value)
            return;
          this.m_multiline = value;
          if (this.m_multiline)
          {
            this.Flags |= FieldFlags.Multiline;
            this.StringFormat.LineAlignment = PdfVerticalAlignment.Top;
          }
          else
          {
            this.Flags &= ~FieldFlags.Multiline;
            this.StringFormat.LineAlignment = PdfVerticalAlignment.Middle;
          }
        }
      }

      public bool Password
      {
        get => this.m_password;
        set
        {
          if (this.m_password == value)
            return;
          this.m_password = value;
          if (this.m_password)
            this.Flags |= FieldFlags.Password;
          else
            this.Flags &= ~FieldFlags.Password;
        }
      }

      public bool Scrollable
      {
        get => this.m_scrollable;
        set
        {
          if (this.m_scrollable == value)
            return;
          this.m_scrollable = value;
          if (this.m_scrollable)
            this.Flags &= ~FieldFlags.DoNotScroll;
          else
            this.Flags |= FieldFlags.DoNotScroll;
        }
      }

      public bool SpellCheck
      {
        get => this.m_spellCheck;
        set
        {
          if (this.m_spellCheck == value)
            return;
          this.m_spellCheck = value;
          if (this.m_spellCheck)
            this.Flags &= ~FieldFlags.DoNotSpellCheck;
          else
            this.Flags |= FieldFlags.DoNotSpellCheck;
        }
      }

      public string Text
      {
        get => this.m_text;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Text));
          if (!(this.m_text != value))
            return;
          this.m_text = value;
          this.Dictionary.SetString("V", this.m_text);
        }
      }
    }
}
