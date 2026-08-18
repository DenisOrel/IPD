// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedPopupAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfLoadedPopupAnnotation : PdfLoadedStyledAnnotation
{
  private PdfCrossTable m_crossTable;
  private PdfPopupIcon m_name;
  private bool m_open;

  internal PdfLoadedPopupAnnotation(
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

  private PdfPopupIcon GetIcon()
  {
    PdfPopupIcon icon = PdfPopupIcon.NewParagraph;
    if (this.Dictionary.ContainsKey("Name"))
      icon = this.GetIconName((this.Dictionary["Name"] as PdfName).Value.ToString());
    return icon;
  }

  private PdfPopupIcon GetIconName(string name)
  {
    PdfPopupIcon iconName = PdfPopupIcon.NewParagraph;
    switch (name)
    {
      case "Comment":
        return PdfPopupIcon.Comment;
      case "Help":
        return PdfPopupIcon.Help;
      case "Insert":
        return PdfPopupIcon.Insert;
      case "Key":
        return PdfPopupIcon.Key;
      case "NewParagraph":
        return PdfPopupIcon.NewParagraph;
      case "Note":
        return PdfPopupIcon.Note;
      case "Paragraph":
        return PdfPopupIcon.Paragraph;
      default:
        return iconName;
    }
  }

  private bool GetOpen()
  {
    bool open = false;
    if (this.Dictionary.ContainsKey("Open"))
      open = (this.Dictionary["Open"] as PdfBoolean).Value;
    return open;
  }

  public PdfPopupIcon Icon
  {
    get => this.GetIcon();
    set
    {
      this.m_name = value;
      this.Dictionary.SetName("Name", this.m_name.ToString());
    }
  }

  public bool Open
  {
    get => this.GetOpen();
    set
    {
      this.m_open = value;
      this.Dictionary.SetBoolean(nameof (Open), this.m_open);
    }
  }
}
