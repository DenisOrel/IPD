// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedInkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfLoadedInkAnnotation : PdfLoadedStyledAnnotation
{
  private PdfDictionary m_borderDic;
  private PdfLineBorderStyle m_borderStyle;
  private int m_borderWidth;
  private PdfCrossTable m_crossTable;
  private int[] m_dashArray;
  private List<float> m_inkList;

  internal PdfLoadedInkAnnotation(
    PdfDictionary dictionary,
    PdfCrossTable crossTable,
    RectangleF rectangle)
    : base(dictionary, crossTable)
  {
    this.m_borderWidth = 1;
    this.m_borderDic = new PdfDictionary();
    this.Dictionary = dictionary;
    this.m_crossTable = crossTable;
    if (!this.Dictionary.ContainsKey("BS"))
      return;
    this.m_borderDic = this.m_crossTable.GetObject(this.Dictionary["BS"]) as PdfDictionary;
  }

  private PdfLineBorderStyle GetBorderStyle(string bstyle)
  {
    PdfLineBorderStyle borderStyle = PdfLineBorderStyle.Solid;
    string str = bstyle;
    switch (str)
    {
      case null:
        return borderStyle;
      case "S":
        return PdfLineBorderStyle.Solid;
      default:
        if (!(str != "D"))
          return PdfLineBorderStyle.Dashed;
        switch (str)
        {
          case "B":
            return PdfLineBorderStyle.Beveled;
          case "I":
            return PdfLineBorderStyle.Inset;
          default:
            return str != "U" ? borderStyle : PdfLineBorderStyle.Underline;
        }
    }
  }

  private int GetBorderWidth()
  {
    int borderWidth = 1;
    if (this.Dictionary.ContainsKey("BS"))
    {
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["BS"]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("W"))
        borderWidth = (pdfDictionary["W"] as PdfNumber).IntValue;
    }
    return borderWidth;
  }

  private int[] GetDashArray()
  {
    List<int> intList = new List<int>();
    if (this.Dictionary.ContainsKey("BS"))
    {
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["BS"]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("D"))
      {
        PdfArray pdfArray = this.m_crossTable.GetObject(pdfDictionary["D"]) as PdfArray;
        for (int index = 0; index < pdfArray.Count; ++index)
          intList.Add((pdfArray[index] as PdfNumber).IntValue);
      }
    }
    return intList.ToArray();
  }

  private List<float> GetInkList()
  {
    List<float> inkList = new List<float>();
    if (this.Dictionary.ContainsKey("InkList") && this.m_crossTable.GetObject((this.m_crossTable.GetObject(this.Dictionary["InkList"]) as PdfArray)[0]) is PdfArray pdfArray)
    {
      foreach (PdfNumber pdfNumber in pdfArray)
        inkList.Add(pdfNumber.FloatValue);
    }
    return inkList;
  }

  private PdfLineBorderStyle GetLineBorder()
  {
    PdfLineBorderStyle lineBorder = PdfLineBorderStyle.Solid;
    if (this.Dictionary.ContainsKey("BS"))
    {
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["BS"]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("S"))
        lineBorder = this.GetBorderStyle((pdfDictionary["S"] as PdfName).Value.ToString());
    }
    return lineBorder;
  }

  public PdfLineBorderStyle BorderStyle
  {
    get => this.GetLineBorder();
    set
    {
      this.m_borderStyle = value;
      if (this.m_borderStyle == PdfLineBorderStyle.Solid)
        this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("S"));
      else if (this.m_borderStyle == PdfLineBorderStyle.Inset)
        this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("I"));
      else if (this.m_borderStyle == PdfLineBorderStyle.Dashed)
        this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("D"));
      else if (this.m_borderStyle == PdfLineBorderStyle.Beveled)
      {
        this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("B"));
      }
      else
      {
        if (this.m_borderStyle != PdfLineBorderStyle.Underline)
          return;
        this.m_borderDic.SetProperty("S", (IPdfPrimitive) new PdfName("U"));
      }
    }
  }

  public int BorderWidth
  {
    get => this.GetBorderWidth();
    set
    {
      this.m_borderWidth = value;
      this.m_borderDic.SetProperty("W", (IPdfPrimitive) new PdfNumber(this.m_borderWidth));
    }
  }

  public int[] DashArray
  {
    get => this.GetDashArray();
    set
    {
      this.m_dashArray = value;
      this.m_borderDic.SetProperty("D", (IPdfPrimitive) new PdfArray(this.m_dashArray));
    }
  }

  public List<float> InkList
  {
    get => this.GetInkList();
    set
    {
      this.m_inkList = value;
      this.Dictionary.SetProperty(nameof (InkList), (IPdfPrimitive) new PdfArray(new PdfArray()
      {
        (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) new PdfArray(this.m_inkList.ToArray()))
      }));
    }
  }
}
