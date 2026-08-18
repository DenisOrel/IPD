// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedUriAnnotation
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

public class PdfLoadedUriAnnotation : PdfLoadedStyledAnnotation
{
  private PdfCrossTable m_crossTable;
  private string m_uri;

  internal PdfLoadedUriAnnotation(
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

  private string GetUriText()
  {
    string empty = string.Empty;
    if (this.Dictionary.ContainsKey("A"))
      empty = ((this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["URI"] as PdfString).Value.ToString();
    return empty;
  }

  public string Uri
  {
    get => this.GetUriText();
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException("uri");
        case "":
          throw new ArgumentException("Uri can not be an empty string");
        default:
          if (!(this.m_uri != value))
            break;
          this.m_uri = value;
          PdfDictionary dictionary = this.Dictionary;
          if (!this.Dictionary.ContainsKey("A"))
            break;
          (this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary).SetString("URI", this.m_uri);
          this.Dictionary.Modify();
          break;
      }
    }
  }
}
