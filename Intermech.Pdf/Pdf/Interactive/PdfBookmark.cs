// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfBookmark
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfBookmark : PdfBookmarkBase
    {
      private PdfAction m_action;
      private PdfColor m_color;
      private PdfDestination m_destination;
      private PdfBookmark m_next;
      private PdfBookmarkBase m_parent;
      private PdfBookmark m_previous;
      private PdfTextStyle m_textStyle;

      internal PdfBookmark(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
      }

      internal PdfBookmark(
        string title,
        PdfBookmarkBase parent,
        PdfBookmark previous,
        PdfBookmark next)
      {
        if (parent == null)
          throw new ArgumentNullException(nameof (parent));
        if (title == null)
          throw new ArgumentNullException(nameof (title));
        this.m_parent = parent;
        this.Dictionary.SetProperty(nameof (Parent), (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) parent));
        this.Previous = previous;
        this.Next = next;
        this.Title = title;
      }

      internal PdfBookmark(
        string title,
        PdfBookmarkBase parent,
        PdfBookmark previous,
        PdfBookmark next,
        PdfDestination dest)
        : this(title, parent, previous, next)
      {
        this.Destination = dest != null ? dest : throw new ArgumentNullException(nameof (dest));
      }

      internal void SetParent(PdfBookmarkBase parent) => this.m_parent = parent;

      private void UpdateColor()
      {
        PdfDictionary dictionary = this.Dictionary;
        if (dictionary["C"] is PdfArray && this.m_color.IsEmpty)
          dictionary.Remove("C");
        else
          dictionary["C"] = (IPdfPrimitive) this.m_color.ToArray();
      }

      private void UpdateTextStyle()
      {
        if (this.m_textStyle == PdfTextStyle.Regular)
          this.Dictionary.Remove("F");
        else
          this.Dictionary.SetNumber("F", (int) this.m_textStyle);
      }

      public PdfAction Action
      {
        get => this.m_action;
        set
        {
          if (this.m_action == value)
            return;
          this.m_action = value;
          this.Dictionary.SetProperty("A", (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_action.Dictionary));
        }
      }

      public virtual PdfColor Color
      {
        get => this.m_color;
        set
        {
          if (!(this.m_color != value))
            return;
          this.m_color = value;
          this.UpdateColor();
        }
      }

      public virtual PdfDestination Destination
      {
        get => this.m_destination;
        set
        {
          this.m_destination = value != null ? value : throw new ArgumentNullException(nameof (Destination));
          this.Dictionary.SetProperty("Dest", (IPdfWrapper) value);
        }
      }

      public new bool IsExpanded
      {
        get => base.IsExpanded;
        set => base.IsExpanded = value;
      }

      internal virtual PdfBookmark Next
      {
        get => this.m_next;
        set
        {
          if (this.m_next == value)
            return;
          this.m_next = value;
          this.Dictionary.SetProperty(nameof (Next), (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) value));
        }
      }

      internal virtual PdfBookmarkBase Parent => this.m_parent;

      internal virtual PdfBookmark Previous
      {
        get => this.m_previous;
        set
        {
          if (this.m_previous == value)
            return;
          this.m_previous = value;
          this.Dictionary.SetProperty("Prev", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) value));
        }
      }

      public virtual PdfTextStyle TextStyle
      {
        get => this.m_textStyle;
        set
        {
          if (this.m_textStyle == value)
            return;
          this.m_textStyle = value;
          this.UpdateTextStyle();
        }
      }

      public virtual string Title
      {
        get
        {
          PdfString pdfString = this.Dictionary[nameof (Title)] as PdfString;
          string title = (string) null;
          if (pdfString != null)
            title = pdfString.Value;
          return title;
        }
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Title));
          this.Dictionary.SetString(nameof (Title), value);
        }
      }
    }
}
