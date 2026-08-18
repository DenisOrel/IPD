// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLoadedFileLinkAnnotation
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

public class PdfLoadedFileLinkAnnotation : PdfLoadedStyledAnnotation
{
  private int[] destinationArray;
  private PdfLaunchAction m_action;
  private PdfCrossTable m_crossTable;
  private PdfArray m_destination;

  internal PdfLoadedFileLinkAnnotation(
    PdfDictionary dictionary,
    PdfCrossTable crossTable,
    RectangleF rectangle,
    string filename)
    : base(dictionary, crossTable)
  {
    if (filename == null)
      throw new ArgumentNullException(nameof (filename));
    this.Dictionary = dictionary;
    this.m_crossTable = crossTable;
    this.m_action = new PdfLaunchAction(filename, true);
  }

  internal PdfLoadedFileLinkAnnotation(
    PdfDictionary dictionary,
    PdfCrossTable crossTable,
    PdfArray destination,
    RectangleF rectangle,
    string filename)
    : base(dictionary, crossTable)
  {
    if (filename == null)
      throw new ArgumentNullException(nameof (filename));
    this.Dictionary = dictionary;
    this.m_crossTable = crossTable;
    this.Destination = destination;
  }

  private int[] GetDestination()
  {
    int[] destination = (int[]) null;
    if (this.Dictionary.ContainsKey("A"))
    {
      PdfArray pdfArray = PdfCrossTable.Dereference((this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["D"]) as PdfArray;
      int index = 0;
      destination = new int[pdfArray.Count - 1];
      foreach (object obj in pdfArray)
      {
        if (obj is PdfNumber)
        {
          if (index == 0)
          {
            destination[index] = (obj as PdfNumber).IntValue + 1;
            ++index;
          }
          else
          {
            destination[index] = (obj as PdfNumber).IntValue;
            ++index;
          }
        }
        else if (obj is PdfNull)
        {
          destination[index] = 0;
          ++index;
        }
      }
    }
    return destination;
  }

  private string GetFileName()
  {
    string empty = string.Empty;
    if (this.Dictionary.ContainsKey("A"))
      empty = ((this.m_crossTable.GetObject((this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["F"]) as PdfDictionary)["F"] as PdfString).Value.ToString();
    return empty;
  }

  private PdfArray Destination
  {
    get => this.m_destination;
    set => this.m_destination = value;
  }

  public int[] DestinationArray
  {
    get => this.GetDestination();
    set
    {
      if (value == null)
        throw new ArgumentNullException("DestinationPageNumber");
      if (value == this.destinationArray)
        return;
      this.destinationArray = value;
      this.Destination.Clear();
      this.Destination.Add((IPdfPrimitive) new PdfNumber(value[0] - 1));
      this.Destination.Add((IPdfPrimitive) new PdfName("XYZ"));
      this.Destination.Add((IPdfPrimitive) new PdfNull());
      this.Destination.Add((IPdfPrimitive) new PdfNumber(value[1]));
      this.Destination.Add((IPdfPrimitive) new PdfNumber(value[2]));
      PdfDictionary dictionary = this.Dictionary;
      if (!this.Dictionary.ContainsKey("A"))
        return;
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary;
      pdfDictionary.Remove("D");
      pdfDictionary.SetProperty("D", (IPdfPrimitive) this.Destination);
      this.Dictionary.Modify();
    }
  }

  public string FileName
  {
    get => this.GetFileName();
    set
    {
      PdfDictionary dictionary = this.Dictionary;
      if (!this.Dictionary.ContainsKey("A"))
        return;
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject((this.m_crossTable.GetObject(this.Dictionary["A"]) as PdfDictionary)["F"]) as PdfDictionary;
      pdfDictionary.SetString("F", value);
      if (pdfDictionary.ContainsKey("UF"))
        pdfDictionary.SetString("UF", value);
      this.Dictionary.Modify();
    }
  }
}
