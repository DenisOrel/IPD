// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfLoadedPage
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfLoadedPage : PdfPageBase
{
  internal static bool m_annotChanged;
  private PdfLoadedAnnotationCollection m_annots;
  private bool m_bCheckResources;
  private PdfCrossTable m_crossTable;
  private PdfDocumentBase m_document;
  private List<PdfDictionary> m_terminalannots;
  private List<long> m_widgetReferences;

  public event EventHandler BeginSave;

  internal PdfLoadedPage(PdfDocumentBase document, PdfCrossTable cTable, PdfDictionary dictionary)
    : base(dictionary)
  {
    this.m_terminalannots = new List<PdfDictionary>();
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (cTable == null)
      throw new ArgumentNullException(nameof (cTable));
    this.m_document = document;
    this.m_crossTable = cTable;
    cTable.PageCorrespondance.Add((IPdfPrimitive) this.Dictionary, (object) null);
    if (!this.m_document.IsPdfViewerDocumentDisable)
      return;
    this.CreateAnnotations();
    this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.PageBeginSave);
    this.Dictionary.EndSave += new SavePdfPrimitiveEventHandler(this.PageEndSave);
  }

  private bool CheckFormField(IPdfPrimitive iPdfPrimitive)
  {
    PdfLoadedDocument document = this.Document as PdfLoadedDocument;
    if (this.m_widgetReferences == null && document.Form != null)
    {
      this.m_widgetReferences = new List<long>();
      foreach (object field in (PdfCollection) document.Form.Fields)
      {
        if (field is PdfLoadedField pdfLoadedField)
        {
          IPdfPrimitive widgetAnnotation = (IPdfPrimitive) pdfLoadedField.GetWidgetAnnotation(pdfLoadedField.Dictionary, pdfLoadedField.CrossTable);
          bool isNew;
          PdfReference reference = this.Document.PdfObjects.GetReference(widgetAnnotation, out isNew);
          if (isNew)
            reference = this.CrossTable.GetReference(widgetAnnotation);
          this.m_widgetReferences.Add(reference.ObjNum);
        }
      }
    }
    PdfReferenceHolder pdfReferenceHolder = iPdfPrimitive as PdfReferenceHolder;
    return document.Form != null && this.m_widgetReferences.Count > 0 && pdfReferenceHolder.Reference != (PdfReference) null && this.m_widgetReferences.Contains(pdfReferenceHolder.Reference.ObjNum);
  }

  internal override void Clear()
  {
    if (this.m_annots != null)
      this.m_annots.Clear();
    base.Clear();
    if (this.m_terminalannots != null)
      this.m_terminalannots.Clear();
    if (this.m_widgetReferences != null)
      this.m_widgetReferences.Clear();
    if (this.m_fontReference == null)
      return;
    this.m_fontReference.Clear();
  }

  internal void CreateAnnotations()
  {
    PdfLoadedPage.m_annotChanged = true;
    if (!this.Dictionary.ContainsKey("Annots"))
      return;
    PdfArray pdfArray = this.m_crossTable.GetObject(this.Dictionary["Annots"]) as PdfArray;
    PdfLoadedDocument document = this.Document as PdfLoadedDocument;
    for (int index = 0; index < pdfArray.Count; ++index)
    {
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(pdfArray[index]) as PdfDictionary;
      if (!document.IsXFAForm && pdfDictionary != null && !this.CheckFormField(pdfArray[index]))
      {
        if (pdfDictionary.ContainsKey("FT"))
          pdfDictionary.Remove("FT");
        if (pdfDictionary.ContainsKey("V"))
          pdfDictionary.Remove("V");
        this.m_terminalannots.Add(pdfDictionary);
      }
    }
    this.m_annots = new PdfLoadedAnnotationCollection(this);
    this.Annotations = this.m_annots;
    this.Annotations = this.m_annots;
  }

  private PdfFontMetrics CreateFont(PdfDictionary fontDictionary, float height, PdfName baseFont)
  {
    PdfFontMetrics font1 = new PdfFontMetrics();
    if (fontDictionary.ContainsKey("FontDescriptor"))
    {
      PdfDictionary pdfDictionary = (fontDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary;
      font1.Ascent = (float) (pdfDictionary["Ascent"] as PdfNumber).IntValue;
      font1.Descent = (float) (pdfDictionary["Descent"] as PdfNumber).IntValue;
      font1.Size = height;
      font1.Height = font1.Ascent - font1.Descent;
      font1.PostScriptName = baseFont.Value;
      if (fontDictionary.ContainsKey("Widths"))
      {
        if (!(fontDictionary["Widths"] is PdfArray font2))
          font2 = (fontDictionary["Widths"] as PdfReferenceHolder).Object as PdfArray;
        int[] widths = new int[font2.Count];
        for (int index = 0; index < font2.Count; ++index)
          widths[index] = (font2[index] as PdfNumber).IntValue;
        font1.WidthTable = (WidthTable) new StandardWidthTable(widths);
      }
      font1.Name = baseFont.Value;
    }
    return font1;
  }

  internal PdfFont[] ExtractFonts()
  {
    List<PdfFont> pdfFontList = new List<PdfFont>();
    this.GetFontStream();
    if (this.m_fontReference != null)
    {
      foreach (PdfReferenceHolder pdfReferenceHolder in this.m_fontReference)
      {
        PdfDictionary fontDictionary1 = pdfReferenceHolder.Object as PdfDictionary;
        float size1 = 12f;
        PdfName pdfName1 = this.CrossTable.GetObject(fontDictionary1["BaseFont"]) as PdfName;
        PdfFont pdfFont = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, size1);
        float contentHeight = this.GetContentHeight(this.GetKey(pdfName1));
        if (fontDictionary1.ContainsKey("Subtype"))
        {
          PdfName pdfName2 = this.CrossTable.GetObject(fontDictionary1["Subtype"]) as PdfName;
          if (pdfName2.Value == "Type1")
          {
            try
            {
              int fontFamily = (int) this.GetFontFamily(pdfName1.Value);
              PdfFontStyle fontStyle = this.GetFontStyle(pdfName1.Value);
              double size2 = (double) contentHeight;
              int style = (int) fontStyle;
              pdfFont = (PdfFont) new PdfStandardFont((PdfFontFamily) fontFamily, (float) size2, (PdfFontStyle) style);
            }
            catch (ArgumentException ex)
            {
              PdfFontMetrics font = this.CreateFont(fontDictionary1, contentHeight, pdfName1);
              string str = pdfName1.Value.Substring(pdfName1.Value.IndexOf('+') + 1);
              PdfFontStyle style = PdfFontStyle.Regular;
              if (str.Contains("PSMT"))
                str = str.Remove(str.IndexOf("PSMT"));
              else if (str.Contains("Bold"))
                style = PdfFontStyle.Bold;
              else if (str.Contains("Italic"))
                style = PdfFontStyle.Italic;
              if (str.Contains("BoldItalic"))
                style = PdfFontStyle.Bold | PdfFontStyle.Italic;
              if (str.Contains("PS"))
                str = str.Remove(str.IndexOf("PS"));
              if (str.Contains("-"))
                str = str.Remove(str.IndexOf("-"));
              pdfFont = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, contentHeight, style);
              WidthTable widthTable = pdfFont.Metrics.WidthTable;
              pdfFont.Metrics = font;
              pdfFont.Metrics.Name = str;
              pdfFont.Metrics.WidthTable = widthTable;
            }
          }
          else if (pdfName2.Value == "TrueType")
          {
            PdfFontMetrics font = this.CreateFont(fontDictionary1, contentHeight, pdfName1);
            string familyName = pdfName1.Value.Substring(pdfName1.Value.IndexOf('+') + 1);
            FontStyle style = FontStyle.Regular;
            if (familyName.Contains("PSMT"))
              familyName = familyName.Remove(familyName.IndexOf("PSMT"));
            else if (familyName.Contains("Bold"))
              style = FontStyle.Bold;
            else if (familyName.Contains("Italic"))
              style = FontStyle.Italic;
            if (familyName.Contains("BoldItalic"))
              style = FontStyle.Bold | FontStyle.Italic;
            if (familyName.Contains("PS"))
              familyName = familyName.Remove(familyName.IndexOf("PS"));
            if (familyName.Contains("-"))
              familyName = familyName.Remove(familyName.IndexOf("-"));
            foreach (FontFamily family in FontFamily.Families)
            {
              string str = family.Name.Replace(" ", string.Empty);
              if (familyName.Contains(str))
              {
                familyName = family.Name;
                break;
              }
            }
            bool unicode = false;
            if (fontDictionary1.ContainsKey("ToUnicode"))
              unicode = true;
            pdfFont = (PdfFont) new PdfTrueTypeFont(new Font(familyName, contentHeight, style), unicode);
            WidthTable widthTable = pdfFont.Metrics.WidthTable;
            pdfFont.Metrics = font;
            pdfFont.Metrics.Name = familyName;
            pdfFont.Metrics.WidthTable = widthTable;
          }
          else if (pdfName2.Value == "Type0" && fontDictionary1.ContainsKey("ToUnicode"))
          {
            PdfDictionary fontDictionary2 = ((fontDictionary1["DescendantFonts"] as PdfArray)[0] as PdfReferenceHolder).Object as PdfDictionary;
            PdfName baseFont = ((fontDictionary2["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary)["FontName"] as PdfName;
            PdfFontMetrics font = this.CreateFont(fontDictionary2, contentHeight, baseFont);
            string familyName = baseFont.Value.Substring(baseFont.Value.IndexOf('+') + 1);
            FontStyle style = FontStyle.Regular;
            if (familyName.Contains("PSMT"))
              familyName = familyName.Remove(familyName.IndexOf("PSMT"));
            else if (familyName.Contains("Bold"))
              style = FontStyle.Bold;
            else if (familyName.Contains("Italic"))
              style = FontStyle.Italic;
            if (familyName.Contains("BoldItalic"))
              style = FontStyle.Bold | FontStyle.Italic;
            if (familyName.Contains("PS"))
              familyName = familyName.Remove(familyName.IndexOf("PS"));
            if (familyName.Contains("-"))
              familyName = familyName.Remove(familyName.IndexOf("-"));
            foreach (FontFamily family in FontFamily.Families)
            {
              string str = family.Name.Replace(" ", string.Empty);
              if (familyName.Contains(str))
              {
                familyName = family.Name;
                break;
              }
            }
            pdfFont = (PdfFont) new PdfTrueTypeFont(new Font(familyName, contentHeight, style), true);
            WidthTable widthTable = pdfFont.Metrics.WidthTable;
            pdfFont.Metrics = font;
            pdfFont.Metrics.Name = familyName;
            pdfFont.Metrics.WidthTable = widthTable;
          }
        }
        if (pdfFont != null)
        {
          pdfFont.InternalFontName = pdfName1.Value;
          pdfFontList.Add(pdfFont);
        }
      }
    }
    return pdfFontList.ToArray();
  }

  internal string FontName(string fontString, out float height)
  {
    PdfReader pdfReader = new PdfReader((Stream) new MemoryStream(Encoding.ASCII.GetBytes(fontString)));
    pdfReader.Position = 0L;
    string s = pdfReader.GetNextToken();
    string nextToken = pdfReader.GetNextToken();
    string str = (string) null;
    height = 0.0f;
    while (nextToken != null && nextToken != string.Empty)
    {
      str = s;
      s = nextToken;
      nextToken = pdfReader.GetNextToken();
      if (nextToken == "Tf")
      {
        height = (float) double.Parse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
        return str;
      }
    }
    return str;
  }

  private float GetContentHeight(string key)
  {
    MemoryStream memoryStream = new MemoryStream();
    this.Layers.CombineContent((Stream) memoryStream);
    float contentHeight = 0.0f;
    string text = PdfString.ByteToString(memoryStream.ToArray());
    StringTokenizer stringTokenizer = new StringTokenizer(text);
    if (!text.Contains(key))
      return contentHeight;
    int num = text.IndexOf(key);
    stringTokenizer.Position = num;
    string[] strArray = stringTokenizer.ReadLine().Split(' ');
    return strArray.Length == 3 ? float.Parse(strArray[1]) : 12f;
  }

  private PdfFontFamily GetFontFamily(string fontFamilyString)
  {
    int length = fontFamilyString.IndexOf("-");
    string str = fontFamilyString;
    if (length >= 0)
      str = fontFamilyString.Substring(0, length);
    return str == "Times" ? PdfFontFamily.TimesRoman : (PdfFontFamily) Enum.Parse(typeof (PdfFontFamily), str, true);
  }

  private PdfFontStyle GetFontStyle(string fontFamilyString)
  {
    int num = fontFamilyString.IndexOf("-");
    PdfFontStyle fontStyle = PdfFontStyle.Regular;
    if (num >= 0)
    {
      string str;
      switch (str = fontFamilyString.Substring(num + 1, fontFamilyString.Length - num - 1))
      {
        case null:
          break;
        case "Italic":
        case "Oblique":
          return PdfFontStyle.Italic;
        default:
          if (!(str != "Bold"))
            return PdfFontStyle.Bold;
          return str != "BoldItalic" && str != "BoldOblique" ? fontStyle : PdfFontStyle.Bold | PdfFontStyle.Italic;
      }
    }
    return fontStyle;
  }

  private string GetKey(PdfName fontName)
  {
    PdfResources resources = this.GetResources();
    if (resources.ContainsKey("Font") && resources["Font"] is PdfDictionary)
    {
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in (resources["Font"] as PdfDictionary).Items)
      {
        if ((this.CrossTable.GetObject(((keyValuePair.Value as PdfReferenceHolder).Object as PdfDictionary)["BaseFont"]) as PdfName).Value == fontName.Value)
          return keyValuePair.Key.Value;
      }
    }
    return (string) null;
  }

  internal override PdfResources GetResources()
  {
    PdfResources resources;
    if (!this.Dictionary.ContainsKey("Resources") || this.m_bCheckResources)
    {
      resources = base.GetResources();
      if ((resources.GetNames().Count == 0 || resources.Items.Count == 0) && this.Dictionary.ContainsKey("Parent"))
      {
        IPdfPrimitive pdfPrimitive = this.Dictionary["Parent"];
        PdfDictionary pdfDictionary = (object) (pdfPrimitive as PdfReferenceHolder) == null ? pdfPrimitive as PdfDictionary : (pdfPrimitive as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary.ContainsKey("Resources"))
        {
          IPdfPrimitive baseDictionary = pdfDictionary["Resources"];
          if (baseDictionary is PdfDictionary && (baseDictionary as PdfDictionary).Items.Count > 0)
          {
            this.Dictionary["Resources"] = baseDictionary;
            resources = new PdfResources((PdfDictionary) baseDictionary);
          }
          else if ((object) (baseDictionary as PdfReferenceHolder) != null)
          {
            this.Dictionary["Resources"] = baseDictionary;
            resources = new PdfResources((PdfDictionary) (baseDictionary as PdfReferenceHolder).Object);
          }
        }
      }
    }
    else
    {
      IPdfPrimitive pointer = this.Dictionary["Resources"];
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(pointer) as PdfDictionary;
      resources = new PdfResources(pdfDictionary);
      if (pdfDictionary != pointer)
      {
        this.m_crossTable.Document.PdfObjects.ReregisterReference((IPdfPrimitive) pdfDictionary, (IPdfPrimitive) resources);
        if (!this.m_crossTable.IsMerging)
          resources.Position = -1;
      }
      else
        this.Dictionary["Resources"] = (IPdfPrimitive) resources;
      this.SetResources(resources);
    }
    this.m_bCheckResources = true;
    return resources;
  }

  protected virtual void OnBeginSave(EventArgs e)
  {
    if (this.BeginSave == null)
      return;
    this.BeginSave((object) this, e);
  }

  private void PageBeginSave(object sender, SavePdfPrimitiveEventArgs args)
  {
    this.OnBeginSave(new EventArgs());
  }

  private void PageEndSave(object sender, SavePdfPrimitiveEventArgs args)
  {
  }

  internal void RemoveFromDictionaries(PdfAnnotation annot)
  {
    if (!this.Dictionary.ContainsKey("Annots"))
      return;
    PdfArray primitive = this.m_crossTable.GetObject(this.Dictionary["Annots"]) as PdfArray;
    PdfReferenceHolder element = new PdfReferenceHolder((IPdfPrimitive) annot.Dictionary);
    primitive.Remove((IPdfPrimitive) element);
    primitive.MarkChanged();
    this.Dictionary.SetProperty("Annots", (IPdfPrimitive) primitive);
  }

  public new PdfLoadedAnnotationCollection Annotations
  {
    get
    {
      if (this.m_annots == null)
        this.m_annots = new PdfLoadedAnnotationCollection(this);
      return this.m_annots;
    }
    set => this.m_annots = value;
  }

  public RectangleF ArtBox
  {
    get
    {
      RectangleF artBox = RectangleF.Empty;
      if (this.Dictionary.ContainsKey(nameof (ArtBox)))
      {
        PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, nameof (ArtBox), "Parent") as PdfArray;
        float floatValue = (pdfArray[2] as PdfNumber).FloatValue;
        float height = (double) (pdfArray[3] as PdfNumber).FloatValue != 0.0 ? (pdfArray[3] as PdfNumber).FloatValue : (pdfArray[1] as PdfNumber).FloatValue;
        artBox = new RectangleF(new PointF((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue), new SizeF(floatValue, height));
      }
      return artBox;
    }
  }

  public RectangleF BleedBox
  {
    get
    {
      RectangleF bleedBox = RectangleF.Empty;
      if (this.Dictionary.ContainsKey(nameof (BleedBox)))
      {
        PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, nameof (BleedBox), "Parent") as PdfArray;
        float floatValue = (pdfArray[2] as PdfNumber).FloatValue;
        float height = (double) (pdfArray[3] as PdfNumber).FloatValue != 0.0 ? (pdfArray[3] as PdfNumber).FloatValue : (pdfArray[1] as PdfNumber).FloatValue;
        bleedBox = new RectangleF(new PointF((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue), new SizeF(floatValue, height));
      }
      return bleedBox;
    }
  }

  public RectangleF CropBox
  {
    get
    {
      RectangleF cropBox = RectangleF.Empty;
      if (this.Dictionary.ContainsKey(nameof (CropBox)))
      {
        PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, nameof (CropBox), "Parent") as PdfArray;
        float floatValue = (pdfArray[2] as PdfNumber).FloatValue;
        float height = (double) (pdfArray[3] as PdfNumber).FloatValue != 0.0 ? (pdfArray[3] as PdfNumber).FloatValue : (pdfArray[1] as PdfNumber).FloatValue;
        cropBox = new RectangleF(new PointF((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue), new SizeF(floatValue, height));
      }
      return cropBox;
    }
  }

  internal PdfCrossTable CrossTable => this.m_crossTable;

  public PdfDocumentBase Document => this.m_document;

  internal override PointF Origin
  {
    get
    {
      PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, "MediaBox", "Parent") as PdfArray;
      return new PointF((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue);
    }
  }

  public override SizeF Size
  {
    get
    {
      PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, "MediaBox", "Parent") as PdfArray;
      return new SizeF((pdfArray[2] as PdfNumber).FloatValue, (double) (pdfArray[3] as PdfNumber).FloatValue != 0.0 ? (pdfArray[3] as PdfNumber).FloatValue : (pdfArray[1] as PdfNumber).FloatValue);
    }
  }

  internal List<PdfDictionary> TerminalAnnotation
  {
    get => this.m_terminalannots;
    set => this.m_terminalannots = value;
  }

  public RectangleF TrimBox
  {
    get
    {
      RectangleF trimBox = RectangleF.Empty;
      if (this.Dictionary.ContainsKey(nameof (TrimBox)))
      {
        PdfArray pdfArray = this.Dictionary.GetValue(this.CrossTable, nameof (TrimBox), "Parent") as PdfArray;
        float floatValue = (pdfArray[2] as PdfNumber).FloatValue;
        float height = (double) (pdfArray[3] as PdfNumber).FloatValue != 0.0 ? (pdfArray[3] as PdfNumber).FloatValue : (pdfArray[1] as PdfNumber).FloatValue;
        trimBox = new RectangleF(new PointF((pdfArray[0] as PdfNumber).FloatValue, (pdfArray[1] as PdfNumber).FloatValue), new SizeF(floatValue, height));
      }
      return trimBox;
    }
  }
}
