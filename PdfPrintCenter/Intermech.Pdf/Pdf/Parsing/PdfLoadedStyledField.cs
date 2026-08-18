// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedStyledField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedStyledField : PdfLoadedField
{
  private PdfFieldActions m_actions;
  private PdfPen m_borderPen;
  internal PdfFont m_font;
  private PdfAction m_gotFocus;
  private PdfAction m_lostFocus;
  private PdfAction m_mouseDown;
  private PdfAction m_mouseEnter;
  private PdfAction m_mouseLeave;
  private PdfAction m_mouseUp;
  private WidgetAnnotation m_widget;
  private const byte ShadowShift = 64 /*0x40*/;

  internal PdfLoadedStyledField(PdfDictionary dictionary, PdfCrossTable crossTable)
    : base(dictionary, crossTable)
  {
    this.m_widget = new WidgetAnnotation();
  }

  internal override void BeginSave()
  {
    base.BeginSave();
    if (!(this.BackBrush is PdfSolidBrush backBrush) || !backBrush.Color.IsEmpty)
      return;
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfDictionary primitive1 = new PdfDictionary();
    PdfArray primitive2 = new PdfArray(new float[3]
    {
      1f,
      1f,
      1f
    });
    primitive1.SetProperty("BG", (IPdfPrimitive) primitive2);
    widgetAnnotation.SetProperty("MK", (IPdfPrimitive) primitive1);
  }

  internal PdfField Clone(PdfDictionary dictionary, PdfPage page)
  {
    PdfCrossTable crossTable = page.Section.ParentDocument.CrossTable;
    PdfLoadedStyledField loadedStyledField = new PdfLoadedStyledField(dictionary, crossTable);
    loadedStyledField.Page = (PdfPageBase) page;
    loadedStyledField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
    return (PdfField) loadedStyledField;
  }

  private void CreateBorderPen()
  {
    float width = (float) this.m_widget.WidgetBorder.Width;
    this.m_borderPen = new PdfPen(this.m_widget.WidgetAppearance.BorderColor, width);
    if (this.Widget.WidgetBorder.Style != PdfBorderStyle.Dashed)
      return;
    this.m_borderPen.DashStyle = PdfDashStyle.Custom;
    this.m_borderPen.DashPattern = new float[1]
    {
      3f / width
    };
  }

  private PdfBorderStyle CreateBorderStyle(PdfDictionary bs)
  {
    PdfBorderStyle borderStyle = PdfBorderStyle.Solid;
    if (!bs.ContainsKey("S"))
      return borderStyle;
    PdfName pdfName = this.CrossTable.GetObject(bs["S"]) as PdfName;
    if (!(pdfName == (PdfName) null))
    {
      string lower;
      switch (lower = pdfName.Value.ToLower())
      {
        case null:
          break;
        case "d":
          return PdfBorderStyle.Dashed;
        default:
          if (!(lower != "b"))
            return PdfBorderStyle.Beveled;
          if (lower == "i")
            return PdfBorderStyle.Inset;
          return lower != "u" ? borderStyle : PdfBorderStyle.Underline;
      }
    }
    return borderStyle;
  }

  private PdfColor CreateColor(PdfArray array)
  {
    int count1 = array.Count;
    PdfColor empty = PdfColor.Empty;
    float[] numArray = new float[array.Count];
    int index = 0;
    for (int count2 = array.Count; index < count2; ++index)
    {
      PdfNumber pdfNumber = this.CrossTable.GetObject(array[index]) as PdfNumber;
      numArray[index] = pdfNumber.FloatValue;
    }
    switch (count1)
    {
      case 1:
        return new PdfColor(numArray[0]);
      case 2:
        return empty;
      case 3:
        return new PdfColor(numArray[0], numArray[1], numArray[2]);
      case 4:
        return new PdfColor(numArray[0], numArray[1], numArray[2], numArray[3]);
      default:
        return empty;
    }
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
        if ((object) (fontDictionary["Widths"] as PdfReferenceHolder) != null)
        {
          PdfArray pdfArray = (new PdfReferenceHolder(fontDictionary["Widths"]).Object as PdfReferenceHolder).Object as PdfArray;
          int[] widths = new int[pdfArray.Count];
          for (int index = 0; index < pdfArray.Count; ++index)
            widths[index] = (pdfArray[index] as PdfNumber).IntValue;
          font1.WidthTable = (WidthTable) new StandardWidthTable(widths);
        }
        else
        {
          PdfArray font2 = fontDictionary["Widths"] as PdfArray;
          int[] widths = new int[font2.Count];
          for (int index = 0; index < font2.Count; ++index)
            widths[index] = (font2[index] as PdfNumber).IntValue;
          font1.WidthTable = (WidthTable) new StandardWidthTable(widths);
        }
      }
      font1.Name = baseFont.Value;
    }
    return font1;
  }

  internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
  {
    return (PdfLoadedFieldItem) null;
  }

  protected override void DefineDefaultAppearance()
  {
    if (this.Form == null || this.m_font == null)
      return;
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfName name = this.Form.Resources.GetName((IPdfWrapper) this.m_font);
    this.Form.Resources.Add(this.m_font, name);
    this.Form.NeedAppearances = true;
    PdfString pdfString = new PdfString(new PdfDefaultAppearance()
    {
      FontName = name.Value,
      FontSize = this.m_font.Size,
      ForeColor = this.ForeColor
    }.ToString());
    widgetAnnotation["DA"] = (IPdfPrimitive) pdfString;
  }

  internal override void Draw()
  {
  }

  internal string FontName(string fontString, out float height)
  {
    if (fontString.Contains("#2C"))
    {
      StringBuilder stringBuilder = new StringBuilder(fontString);
      stringBuilder.Replace("#2C", ",");
      fontString = stringBuilder.ToString();
    }
    PdfReader pdfReader = new PdfReader((Stream) new MemoryStream(Encoding.UTF8.GetBytes(fontString)));
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

  private PdfBrush GetBackBrush()
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfBrush backBrush = (PdfBrush) null;
    if (widgetAnnotation != null && widgetAnnotation.ContainsKey("MK"))
    {
      PdfDictionary pdfDictionary = this.CrossTable.GetObject(widgetAnnotation["MK"]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("BG"))
        backBrush = (PdfBrush) new PdfSolidBrush(this.CreateColor(this.CrossTable.GetObject(pdfDictionary["BG"]) as PdfArray));
    }
    return backBrush;
  }

  internal PdfColor GetBackColor()
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfColor backColor = new PdfColor((byte) 0, (byte) 20, (byte) 200);
    if (widgetAnnotation.ContainsKey("MK"))
    {
      PdfDictionary pdfDictionary = this.CrossTable.GetObject(widgetAnnotation["MK"]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("BG"))
        backColor = this.CreateColor(pdfDictionary["BG"] as PdfArray);
    }
    return backColor;
  }

  internal PdfPen GetBorderPen()
  {
    PdfDictionary pdfDictionary1 = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfPen borderPen = (PdfPen) null;
    if (pdfDictionary1 == null)
      pdfDictionary1 = this.Dictionary;
    if (pdfDictionary1 != null && pdfDictionary1.ContainsKey("MK"))
    {
      PdfDictionary pdfDictionary2 = this.CrossTable.GetObject(pdfDictionary1["MK"]) as PdfDictionary;
      if (pdfDictionary2.ContainsKey("BC"))
        borderPen = new PdfPen(this.CreateColor(this.CrossTable.GetObject(pdfDictionary2["BC"]) as PdfArray));
    }
    PdfBorderStyle borderStyle = this.BorderStyle;
    int borderWidth = this.BorderWidth;
    if (borderPen != null)
    {
      borderPen.Width = (float) borderWidth;
      if (borderStyle != PdfBorderStyle.Dashed)
        return borderPen;
      float[] dashPatern = this.DashPatern;
      borderPen.DashStyle = PdfDashStyle.Custom;
      if (dashPatern != null)
      {
        borderPen.DashPattern = dashPatern;
        return borderPen;
      }
      borderPen.DashPattern = new float[1]
      {
        (float) (3 / borderWidth)
      };
    }
    return borderPen;
  }

  private PdfBorderStyle GetBorderStyle()
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfBorderStyle borderStyle = PdfBorderStyle.Solid;
    if (widgetAnnotation.ContainsKey("BS"))
      borderStyle = this.CreateBorderStyle(this.CrossTable.GetObject(widgetAnnotation["BS"]) as PdfDictionary);
    return borderStyle;
  }

  private int GetBorderWidth()
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    int borderWidth = 0;
    if (widgetAnnotation.ContainsKey("BS"))
    {
      borderWidth = 1;
      if (this.CrossTable.GetObject((this.CrossTable.GetObject(widgetAnnotation["BS"]) as PdfDictionary)["W"]) is PdfNumber pdfNumber)
        borderWidth = pdfNumber.IntValue;
    }
    return borderWidth;
  }

  private RectangleF GetBounds(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfArray pdfArray = (PdfArray) null;
    if (dictionary.ContainsKey("Kids"))
    {
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(dictionary, crossTable);
      if (widgetAnnotation.ContainsKey("Rect"))
        pdfArray = crossTable.GetObject(widgetAnnotation["Rect"]) as PdfArray;
    }
    else if (dictionary.ContainsKey("Rect"))
      pdfArray = crossTable.GetObject(dictionary["Rect"]) as PdfArray;
    if (pdfArray == null)
      return new RectangleF();
    RectangleF rectangle = pdfArray.ToRectangle();
    if ((double) (pdfArray[1] as PdfNumber).FloatValue < 0.0)
    {
      rectangle.Y = (pdfArray[1] as PdfNumber).FloatValue;
      if ((double) (pdfArray[1] as PdfNumber).FloatValue > (double) (pdfArray[3] as PdfNumber).FloatValue)
        rectangle.Y -= rectangle.Height;
    }
    return rectangle;
  }

  private float[] GetDashPatern()
  {
    float[] dashPatern1 = (float[]) null;
    if (this.BorderStyle == PdfBorderStyle.Dashed)
    {
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      if (!widgetAnnotation.ContainsKey("D"))
        return dashPatern1;
      PdfArray pdfArray = this.CrossTable.GetObject(widgetAnnotation["D"]) as PdfArray;
      if (dashPatern1.Length == 2)
      {
        float[] dashPatern2 = new float[2];
        PdfNumber pdfNumber1 = pdfArray[0] as PdfNumber;
        dashPatern2[0] = (float) pdfNumber1.IntValue;
        PdfNumber pdfNumber2 = pdfArray[1] as PdfNumber;
        dashPatern2[1] = (float) pdfNumber2.IntValue;
        return dashPatern2;
      }
      dashPatern1 = new float[1];
      PdfNumber pdfNumber = pdfArray[0] as PdfNumber;
      dashPatern1[0] = (float) pdfNumber.IntValue;
    }
    return dashPatern1;
  }

  private PdfFont GetFont(string fontString, out bool isCorrectFont)
  {
    float height = 0.0f;
    isCorrectFont = true;
    string str1 = this.FontName(fontString, out height);
    PdfFont font1 = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, height);
    if (!(this.CrossTable.GetObject(this.Form.Resources["Font"]) is PdfDictionary pdfDictionary) || str1 == null || !pdfDictionary.ContainsKey(str1))
    {
      PdfFont fontByName = this.GetFontByName(str1, height);
      if (fontByName != null)
        font1 = fontByName;
      else
        isCorrectFont = false;
    }
    else if (this.CrossTable.GetObject(pdfDictionary[str1]) is PdfDictionary fontDictionary1 && fontDictionary1.ContainsKey("Subtype"))
    {
      PdfName pdfName1 = this.CrossTable.GetObject(fontDictionary1["Subtype"]) as PdfName;
      if (pdfName1.Value == "Type1")
      {
        PdfName baseFont = this.CrossTable.GetObject(fontDictionary1["BaseFont"]) as PdfName;
        PdfFontStyle fontStyle = this.GetFontStyle(baseFont.Value);
        string standardName;
        PdfFontFamily fontFamily = this.GetFontFamily(baseFont.Value, out standardName);
        if (standardName == null)
        {
          font1 = (PdfFont) new PdfStandardFont(fontFamily, height, fontStyle);
        }
        else
        {
          if (fontStyle != PdfFontStyle.Regular)
            font1 = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, height, fontStyle);
          if (baseFont.Value.Contains("MyriadPro"))
            font1 = (PdfFont) new PdfTrueTypeFont(new System.Drawing.Font(baseFont.Value, height, (FontStyle) fontStyle));
          else
            font1.Metrics = this.CreateFont(fontDictionary1, height, baseFont) != null ? this.CreateFont(fontDictionary1, height, baseFont) : font1.Metrics;
        }
      }
      else if (!(pdfName1.Value == "TrueType"))
      {
        if (pdfName1.Value == "Type0" && fontDictionary1.ContainsKey("ToUnicode"))
        {
          PdfDictionary fontDictionary = ((fontDictionary1["DescendantFonts"] as PdfArray)[0] as PdfReferenceHolder).Object as PdfDictionary;
          PdfName baseFont = ((fontDictionary["FontDescriptor"] as PdfReferenceHolder).Object as PdfDictionary)["FontName"] as PdfName;
          PdfFontMetrics font2 = this.CreateFont(fontDictionary, height, baseFont);
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
            string str2 = family.Name.Replace(" ", string.Empty);
            if (familyName.Contains(str2))
            {
              familyName = family.Name;
              break;
            }
          }
          font1 = (PdfFont) new PdfTrueTypeFont(new System.Drawing.Font(familyName, height, style), true);
          WidthTable widthTable = font1.Metrics.WidthTable;
          font1.Metrics = font2;
          font1.Metrics.WidthTable = widthTable;
        }
      }
      else
      {
        PdfName baseFont = this.CrossTable.GetObject(fontDictionary1["BaseFont"]) as PdfName;
        string fontName = this.GetFontName(baseFont.Value);
        PdfFontStyle fontStyle = this.GetFontStyle(baseFont.Value);
        if (fontName != null || fontName != string.Empty)
        {
          bool flag = false;
          foreach (string name in Enum.GetNames(typeof (PdfFontFamily)))
          {
            if (fontName.Contains(name))
            {
              font1 = (PdfFont) new PdfStandardFont((PdfFontFamily) Enum.Parse(typeof (PdfFontFamily), name, true), height, fontStyle);
              flag = true;
            }
          }
          if (!flag)
          {
            try
            {
              string str3 = fontName;
              string[] sourceArray = new string[1]{ "" };
              int length = 0;
              for (int startIndex = 0; startIndex < str3.Length; ++startIndex)
              {
                string str4 = str3.Substring(startIndex, 1);
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".Contains(str4) && startIndex > 0 && !"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".Contains(str3[startIndex - 1].ToString()))
                {
                  ++length;
                  string[] destinationArray = new string[length + 1];
                  Array.Copy((Array) sourceArray, 0, (Array) destinationArray, 0, length);
                  sourceArray = destinationArray;
                }
                string[] strArray;
                IntPtr index;
                (strArray = sourceArray)[(int) (index = (IntPtr) length)] = strArray[(int) index] + str4;
              }
              string str5 = string.Empty;
              foreach (string str6 in sourceArray)
                str5 = $"{str5}{str6} ";
              System.Drawing.Font font3 = new System.Drawing.Font(str5.Trim(), height, (FontStyle) fontStyle);
              if (this.Dictionary.ContainsKey("V"))
              {
                PdfString pdfString = this.Dictionary.GetString("V");
                if ((this.Dictionary["FT"] as PdfName).Value.Equals("Ch") && this.Dictionary.ContainsKey("Opt"))
                {
                  PdfArray pdfArray1 = this.Dictionary["Opt"] as PdfArray;
                  if (pdfArray1[0] is PdfArray)
                  {
                    foreach (PdfArray pdfArray2 in pdfArray1)
                    {
                      if ((pdfArray2[0] as PdfString).Value.Equals(pdfString.Value))
                      {
                        pdfString = pdfArray2[1] as PdfString;
                        break;
                      }
                    }
                  }
                }
                if (pdfString != null)
                  font1 = !PdfString.IsUnicode(pdfString.Value) ? (PdfFont) new PdfTrueTypeFont(font3) : (PdfFont) new PdfTrueTypeFont(font3, true);
              }
              else
                font1 = (PdfFont) new PdfTrueTypeFont(font3);
            }
            catch (ArgumentException ex)
            {
              font1 = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, height, fontStyle);
            }
          }
        }
        else if (fontStyle != PdfFontStyle.Regular)
          font1 = (PdfFont) new PdfStandardFont((PdfStandardFont) PdfDocument.DefaultFont, height, fontStyle);
        PdfName pdfName2 = fontDictionary1["Name"] as PdfName;
        if (pdfName2 != (PdfName) null && font1.Name != pdfName2.Value)
          font1.Metrics = this.CreateFont(fontDictionary1, height, baseFont) != null ? this.CreateFont(fontDictionary1, height, baseFont) : font1.Metrics;
      }
    }
    if ((double) height == 0.0)
    {
      PdfStandardFont prototype = font1 as PdfStandardFont;
      float fontHeight = this.GetFontHeight(prototype.FontFamily);
      font1 = (PdfFont) new PdfStandardFont(prototype, fontHeight);
    }
    return font1;
  }

  private PdfFont GetFontByName(string name, float height)
  {
    switch (name)
    {
      case "CoBO":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Courier, height, PdfFontStyle.Bold | PdfFontStyle.Italic);
      case "CoBo":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Courier, height, PdfFontStyle.Bold);
      case "CoOb":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Courier, height, PdfFontStyle.Italic);
      case "Cour":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Courier, height, PdfFontStyle.Regular);
      case "HeBO":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, height, PdfFontStyle.Bold | PdfFontStyle.Italic);
      case "HeBo":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, height, PdfFontStyle.Bold);
      case "HeOb":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, height, PdfFontStyle.Italic);
      case "Helv":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Helvetica, height, PdfFontStyle.Regular);
      case "Symb":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.Symbol, height);
      case "TiBI":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.TimesRoman, height, PdfFontStyle.Bold | PdfFontStyle.Italic);
      case "TiBo":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.TimesRoman, height, PdfFontStyle.Bold);
      case "TiIt":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.TimesRoman, height, PdfFontStyle.Italic);
      case "TiRo":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.TimesRoman, height, PdfFontStyle.Regular);
      case "ZaDb":
        return (PdfFont) new PdfStandardFont(PdfFontFamily.ZapfDingbats, height);
      default:
        return (PdfFont) null;
    }
  }

  private PdfFontFamily GetFontFamily(string fontFamilyString, out string standardName)
  {
    int length = fontFamilyString.IndexOf("-");
    PdfFontFamily fontFamily1 = PdfFontFamily.Helvetica;
    standardName = fontFamilyString;
    if (length >= 0)
      standardName = fontFamilyString.Substring(0, length);
    if (standardName == "Times")
    {
      PdfFontFamily fontFamily2 = PdfFontFamily.TimesRoman;
      standardName = (string) null;
      return fontFamily2;
    }
    foreach (string name in Enum.GetNames(typeof (PdfFontFamily)))
    {
      if (name.Contains(standardName))
      {
        PdfFontFamily fontFamily3 = (PdfFontFamily) Enum.Parse(typeof (PdfFontFamily), standardName, true);
        standardName = (string) null;
        return fontFamily3;
      }
    }
    return fontFamily1;
  }

  internal virtual float GetFontHeight(PdfFontFamily family) => 0.0f;

  internal string GetFontName(string fontFamilyString)
  {
    if (fontFamilyString.Contains("-") || fontFamilyString.Contains("PSMT") || fontFamilyString.Contains("MT") || fontFamilyString.Contains(","))
    {
      string str = fontFamilyString;
      if (fontFamilyString.Contains("-"))
        str = fontFamilyString.Replace("-", " ");
      if (fontFamilyString.Contains(","))
        return fontFamilyString.Split(',')[0];
      foreach (FontFamily family in FontFamily.Families)
      {
        string name = family.Name;
        if (str == name)
          return name;
      }
      if (fontFamilyString.Contains("PSMT"))
        return fontFamilyString.Replace("PSMT", "");
      if (fontFamilyString.Contains("MT") && !fontFamilyString.Contains("-"))
        return fontFamilyString.Replace("MT", "");
    }
    return fontFamilyString.Split('-')[0];
  }

  private PdfFontStyle GetFontStyle(string fontFamilyString)
  {
    int num = fontFamilyString.IndexOf("-");
    if (num < 0)
      num = fontFamilyString.IndexOf(",");
    PdfFontStyle fontStyle = PdfFontStyle.Regular;
    if (num >= 0)
    {
      switch (fontFamilyString.Substring(num + 1, fontFamilyString.Length - num - 1))
      {
        case "Bold":
        case "BoldMT":
          return PdfFontStyle.Bold;
        case "BoldItalic":
        case "BoldItalicMT":
        case "BoldOblique":
          return PdfFontStyle.Bold | PdfFontStyle.Italic;
        case "It":
        case "Italic":
        case "ItalicMT":
        case "Oblique":
          return PdfFontStyle.Italic;
      }
    }
    return fontStyle;
  }

  private PdfBrush GetForeBrush()
  {
    PdfBrush foreBrush = PdfBrushes.Black;
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    if (widgetAnnotation != null && widgetAnnotation.ContainsKey("DA"))
      foreBrush = (PdfBrush) new PdfSolidBrush(this.GetForeColour((this.CrossTable.GetObject(widgetAnnotation["DA"]) as PdfString).Value));
    return foreBrush;
  }

  internal PdfColor GetForeColour(string defaultAppearance)
  {
    PdfColor foreColour = new PdfColor((byte) 0, (byte) 0, (byte) 0);
    if (defaultAppearance == null || defaultAppearance == string.Empty)
      return new PdfColor((byte) 0, (byte) 0, (byte) 0);
    PdfReader pdfReader = new PdfReader((Stream) new MemoryStream(Encoding.UTF8.GetBytes(defaultAppearance)));
    pdfReader.Position = 0L;
    bool flag = false;
    Stack<string> stringStack = new Stack<string>();
    string nextToken = pdfReader.GetNextToken();
    if (nextToken == "/")
      flag = true;
    while (nextToken != null && nextToken != string.Empty)
    {
      if (flag)
        nextToken = pdfReader.GetNextToken();
      flag = true;
      switch (nextToken)
      {
        case "g":
          foreColour = new PdfColor(this.ParseFloatColour(stringStack.Pop()));
          continue;
        case "rg":
          byte blue = (byte) ((double) this.ParseFloatColour(stringStack.Pop()) * (double) byte.MaxValue);
          byte green = (byte) ((double) this.ParseFloatColour(stringStack.Pop()) * (double) byte.MaxValue);
          foreColour = new PdfColor((byte) ((double) this.ParseFloatColour(stringStack.Pop()) * (double) byte.MaxValue), green, blue);
          continue;
        case "k":
          float floatColour1 = this.ParseFloatColour(stringStack.Pop());
          float floatColour2 = this.ParseFloatColour(stringStack.Pop());
          float floatColour3 = this.ParseFloatColour(stringStack.Pop());
          foreColour = new PdfColor(this.ParseFloatColour(stringStack.Pop()), floatColour3, floatColour2, floatColour1);
          continue;
        default:
          stringStack.Push(nextToken);
          continue;
      }
    }
    return foreColour;
  }

  protected void GetGraphicsProperties(
    out PdfLoadedStyledField.GraphicsProperties graphicsProperties,
    PdfLoadedFieldItem item)
  {
    if (item != null)
      graphicsProperties = new PdfLoadedStyledField.GraphicsProperties(item);
    else
      graphicsProperties = new PdfLoadedStyledField.GraphicsProperties(this);
  }

  private string GetHighLightString(PdfHighlightMode mode)
  {
    switch (mode)
    {
      case PdfHighlightMode.NoHighlighting:
        return "N";
      case PdfHighlightMode.Invert:
        return "I";
      case PdfHighlightMode.Outline:
        return "O";
      case PdfHighlightMode.Push:
        return "P";
      default:
        return (string) null;
    }
  }

  private PdfArray GetKids()
  {
    PdfArray kids = (PdfArray) null;
    if (this.Dictionary.ContainsKey("Kids"))
      kids = this.CrossTable.GetObject(this.Dictionary["Kids"]) as PdfArray;
    return kids;
  }

  private int GetRotationAngle()
  {
    int rotationAngle = 0;
    if (this.Dictionary != null && this.Dictionary.ContainsKey("MK") && this.Dictionary["MK"] is PdfDictionary pdfDictionary)
      rotationAngle = pdfDictionary.ContainsKey("R") ? (pdfDictionary["R"] as PdfNumber).IntValue : 0;
    return rotationAngle;
  }

  private PdfBrush GetShadowBrush()
  {
    PdfBrush white = PdfBrushes.White;
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    if (widgetAnnotation == null || !widgetAnnotation.ContainsKey("DA"))
      return white;
    this.CrossTable.GetObject(widgetAnnotation["DA"]);
    PdfColor color = new PdfColor(byte.MaxValue, byte.MaxValue, byte.MaxValue);
    if (this.BackBrush is PdfSolidBrush backBrush)
      color = backBrush.Color;
    color.R = (int) color.R - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.R - 64U /*0x40*/) : (byte) 0;
    color.G = (int) color.G - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.G - 64U /*0x40*/) : (byte) 0;
    color.B = (int) color.B - 64 /*0x40*/ >= 0 ? (byte) ((uint) color.B - 64U /*0x40*/) : (byte) 0;
    return (PdfBrush) new PdfSolidBrush(color);
  }

  private PdfStringFormat GetStringFormat()
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfStringFormat stringFormat = new PdfStringFormat()
    {
      LineAlignment = PdfVerticalAlignment.Middle
    };
    stringFormat.LineAlignment = (this.Flags & FieldFlags.Multiline) > FieldFlags.Default ? PdfVerticalAlignment.Top : PdfVerticalAlignment.Middle;
    PdfNumber pdfNumber = (PdfNumber) null;
    if (widgetAnnotation != null && widgetAnnotation.ContainsKey("Q"))
      pdfNumber = this.CrossTable.GetObject(widgetAnnotation["Q"]) as PdfNumber;
    else if (this.Dictionary.ContainsKey("Q"))
      pdfNumber = this.CrossTable.GetObject(this.Dictionary["Q"]) as PdfNumber;
    if (pdfNumber != null && pdfNumber.IsInteger)
      stringFormat.Alignment = (PdfTextAlignment) pdfNumber.IntValue;
    return stringFormat;
  }

  private bool GetVisible()
  {
    PdfDictionary pdfDictionary = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable) ?? this.Dictionary;
    return pdfDictionary == null || !pdfDictionary.ContainsKey("F") || (this.CrossTable.GetObject(pdfDictionary["F"]) as PdfNumber).IntValue != 2;
  }

  private float ParseFloatColour(string text)
  {
    return (float) double.Parse(text, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  private void SetBackBrush(PdfBrush brush)
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    if (widgetAnnotation == null || !(brush is PdfSolidBrush))
      return;
    PdfDictionary pdfDictionary;
    if (widgetAnnotation.ContainsKey("MK"))
    {
      pdfDictionary = this.CrossTable.GetObject(widgetAnnotation["MK"]) as PdfDictionary;
    }
    else
    {
      pdfDictionary = new PdfDictionary();
      widgetAnnotation["MK"] = (IPdfPrimitive) pdfDictionary;
    }
    PdfArray array = (brush as PdfSolidBrush).Color.ToArray();
    pdfDictionary["BG"] = (IPdfPrimitive) array;
  }

  internal PdfPen SetBorderColor(PdfColor borderColor)
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    PdfPen pdfPen = (PdfPen) null;
    if (widgetAnnotation != null)
    {
      if (widgetAnnotation.ContainsKey("MK"))
      {
        PdfDictionary pdfDictionary = this.CrossTable.GetObject(widgetAnnotation["MK"]) as PdfDictionary;
        PdfArray array = borderColor.ToArray();
        PdfArray pdfArray = array;
        pdfDictionary["BC"] = (IPdfPrimitive) pdfArray;
        pdfPen = new PdfPen(this.CreateColor(array));
      }
      else
      {
        PdfDictionary pdfDictionary = new PdfDictionary();
        PdfArray array = borderColor.ToArray();
        pdfDictionary["BC"] = (IPdfPrimitive) array;
        pdfPen = new PdfPen(this.CreateColor(array));
        widgetAnnotation["MK"] = (IPdfPrimitive) pdfDictionary;
      }
    }
    PdfBorderStyle borderStyle = this.BorderStyle;
    int borderWidth = this.BorderWidth;
    if (pdfPen != null)
    {
      pdfPen.Width = (float) borderWidth;
      if (borderStyle != PdfBorderStyle.Dashed)
        return pdfPen;
      float[] dashPatern = this.DashPatern;
      pdfPen.DashStyle = PdfDashStyle.Custom;
      if (dashPatern != null)
      {
        pdfPen.DashPattern = dashPatern;
        return pdfPen;
      }
      pdfPen.DashPattern = new float[1]
      {
        (float) (3 / borderWidth)
      };
    }
    return pdfPen;
  }

  private void SetBorderStyle(PdfBorderStyle borderStyle)
  {
    string str = "";
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    if (!widgetAnnotation.ContainsKey("BS"))
      return;
    this.CrossTable.GetObject(widgetAnnotation["BS"]);
    switch (borderStyle)
    {
      case PdfBorderStyle.Solid:
        str = "S";
        break;
      case PdfBorderStyle.Dashed:
        str = "D";
        break;
      case PdfBorderStyle.Beveled:
        str = "B";
        break;
      case PdfBorderStyle.Inset:
        str = "I";
        break;
      case PdfBorderStyle.Underline:
        str = "U";
        break;
    }
    (widgetAnnotation["BS"] as PdfDictionary)["S"] = (IPdfPrimitive) new PdfName(str);
    this.Widget.WidgetBorder.Style = borderStyle;
  }

  private void SetBorderWidth(int width)
  {
    PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
    if (!widgetAnnotation.ContainsKey("BS"))
      return;
    (widgetAnnotation["BS"] as PdfDictionary)["W"] = (IPdfPrimitive) new PdfNumber(width);
    this.CreateBorderPen();
  }

  internal PdfBrush BackBrush
  {
    get => this.GetBackBrush();
    set => this.SetBackBrush(value);
  }

  public PdfColor BorderColor
  {
    get => this.m_widget.WidgetAppearance.BorderColor;
    set
    {
      this.Form.SetAppearanceDictionary = true;
      this.m_widget.WidgetAppearance.BorderColor = value;
      this.SetBorderColor(value);
    }
  }

  internal PdfPen BorderPen => this.GetBorderPen();

  public PdfBorderStyle BorderStyle
  {
    get => this.GetBorderStyle();
    set
    {
      this.SetBorderStyle(value);
      this.CreateBorderPen();
    }
  }

  public int BorderWidth
  {
    get => this.GetBorderWidth();
    set
    {
      this.m_widget.WidgetBorder.Width = value;
      this.SetBorderWidth(value);
    }
  }

  public RectangleF Bounds
  {
    get
    {
      RectangleF bounds = this.GetBounds(this.Dictionary, this.CrossTable);
      if ((double) bounds.Y > 0.0)
      {
        if (this.Page != null)
        {
          bounds.Y = this.Page.Size.Height - (bounds.Y + bounds.Height);
          return bounds;
        }
        bounds.Y += bounds.Height;
        return bounds;
      }
      bounds.Y = (float) -((double) bounds.Y + (double) bounds.Height);
      return bounds;
    }
    set
    {
      RectangleF rectangleF = value;
      if (rectangleF == RectangleF.Empty)
        throw new ArgumentNullException("rectangle");
      float height = this.Page.Size.Height;
      PdfNumber[] pdfNumberArray = new PdfNumber[4]
      {
        new PdfNumber(rectangleF.X),
        new PdfNumber(height - (rectangleF.Y + rectangleF.Height)),
        new PdfNumber(rectangleF.X + rectangleF.Width),
        new PdfNumber(height - rectangleF.Y)
      };
      PdfDictionary pdfDictionary = this.Dictionary;
      if (!pdfDictionary.ContainsKey("Rect"))
        pdfDictionary = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      pdfDictionary.SetArray("Rect", (IPdfPrimitive[]) pdfNumberArray);
      this.Changed = true;
    }
  }

  internal float[] DashPatern => this.GetDashPatern();

  public new int DefaultIndex
  {
    get => base.DefaultIndex;
    set => base.DefaultIndex = value >= 0 ? value : throw new IndexOutOfRangeException("index");
  }

  public PdfFont Font
  {
    get
    {
      if (this.m_font != null)
        return this.m_font;
      PdfFont font = PdfDocument.DefaultFont;
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      if (widgetAnnotation != null && (widgetAnnotation.ContainsKey("DA") || this.Dictionary.ContainsKey("DA")))
      {
        if (!(this.CrossTable.GetObject(widgetAnnotation["DA"]) is PdfString pdfString))
          pdfString = this.CrossTable.GetObject(this.Dictionary["DA"]) as PdfString;
        string[] strArray = pdfString.Value.Split(new char[1]
        {
          ' '
        }, StringSplitOptions.RemoveEmptyEntries);
        bool isCorrectFont;
        font = this.GetFont(pdfString.Value, out isCorrectFont);
        if (!isCorrectFont)
        {
          string newValue = "/Helv";
          widgetAnnotation.SetProperty("DA", (IPdfPrimitive) new PdfString(pdfString.Value.Replace(strArray[0], newValue)));
        }
      }
      return font;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Font));
      if (this.m_font == value)
        return;
      this.m_font = value;
      if (this.Form == null)
        return;
      this.Form.SetAppearanceDictionary = true;
    }
  }

  internal PdfBrush ForeBrush => this.GetForeBrush();

  private PdfColor ForeColor
  {
    get
    {
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      PdfColor foreColor = new PdfColor((byte) 0, (byte) 0, (byte) 0);
      if (widgetAnnotation != null && widgetAnnotation.ContainsKey("DA"))
        foreColor = this.GetForeColour((this.CrossTable.GetObject(widgetAnnotation["DA"]) as PdfString).Value);
      return foreColor;
    }
  }

  public PdfAction GotFocus
  {
    get => this.m_gotFocus;
    set
    {
      if (value == null)
        return;
      this.m_gotFocus = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("Fo", (IPdfWrapper) this.m_gotFocus);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  internal PdfArray Kids => this.GetKids();

  public PointF Location
  {
    get => this.Bounds.Location;
    set => this.Bounds = new RectangleF(value, this.Bounds.Size);
  }

  public PdfAction LostFocus
  {
    get => this.m_lostFocus;
    set
    {
      if (value == null)
        return;
      this.m_lostFocus = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("Bl", (IPdfWrapper) this.m_lostFocus);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  public PdfAction MouseDown
  {
    get => this.m_mouseDown;
    set
    {
      if (value == null)
        return;
      this.m_mouseDown = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("D", (IPdfWrapper) this.m_mouseDown);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  public PdfAction MouseEnter
  {
    get => this.m_mouseEnter;
    set
    {
      if (value == null)
        return;
      this.m_mouseEnter = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("E", (IPdfWrapper) this.m_mouseEnter);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  public PdfAction MouseLeave
  {
    get => this.m_mouseLeave;
    set
    {
      if (value == null)
        return;
      this.m_mouseLeave = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("X", (IPdfWrapper) this.m_mouseLeave);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  public PdfAction MouseUp
  {
    get => this.m_mouseUp;
    set
    {
      if (value == null)
        return;
      this.m_mouseUp = value;
      this.m_actions = new PdfFieldActions(this.Widget.Actions);
      PdfDictionary widgetAnnotation = this.GetWidgetAnnotation(this.Dictionary, this.CrossTable);
      widgetAnnotation.SetProperty("AA", (IPdfWrapper) this.m_actions);
      PdfDictionary primitive = this.CrossTable.GetObject(widgetAnnotation["AA"]) as PdfDictionary;
      primitive.SetProperty("U", (IPdfWrapper) this.m_mouseUp);
      widgetAnnotation.SetProperty("AA", (IPdfPrimitive) primitive);
      this.Changed = true;
    }
  }

  internal new int RotationAngle => this.GetRotationAngle();

  internal PdfBrush ShadowBrush => this.GetShadowBrush();

  public SizeF Size
  {
    get => this.Bounds.Size;
    set => this.Bounds = new RectangleF(this.Bounds.Location, value);
  }

  internal PdfStringFormat StringFormat => this.GetStringFormat();

  public bool Visible => this.GetVisible();

  internal WidgetAnnotation Widget => this.m_widget;

  protected struct GraphicsProperties
  {
    public RectangleF Rect;
    public PdfPen Pen;
    public PdfBorderStyle Style;
    public int BorderWidth;
    public PdfBrush BackBrush;
    public PdfBrush ForeBrush;
    public PdfBrush ShadowBrush;
    public PdfFont Font;
    public PdfStringFormat StringFormat;
    public int RotationAngle;

    public GraphicsProperties(PdfLoadedStyledField field)
    {
      this.Rect = field != null ? field.Bounds : throw new ArgumentNullException(nameof (field));
      this.Pen = field.BorderPen;
      this.Style = field.BorderStyle;
      this.BorderWidth = field.BorderWidth;
      this.BackBrush = field.BackBrush;
      this.ForeBrush = field.ForeBrush;
      this.ShadowBrush = field.ShadowBrush;
      this.Font = field.Font;
      this.StringFormat = field.StringFormat;
      this.RotationAngle = field.RotationAngle;
    }

    public GraphicsProperties(PdfLoadedFieldItem item)
    {
      this.Rect = item != null ? item.Bounds : throw new ArgumentNullException(nameof (item));
      this.Pen = item.BorderPen;
      this.Style = item.BorderStyle;
      this.BorderWidth = item.BorderWidth;
      this.BackBrush = item.BackBrush;
      this.ForeBrush = item.ForeBrush;
      this.ShadowBrush = item.ShadowBrush;
      this.Font = item.Font;
      this.StringFormat = item.StringFormat;
      this.RotationAngle = 0;
    }
  }
}
