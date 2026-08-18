// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageLayerCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPageLayerCollection : PdfCollection
{
  internal PdfDictionary m_OptionalContent;
  private PdfPageBase m_page;
  internal bool m_sublayer;
  private int parentLayerCount;

  public PdfPageLayerCollection() => this.m_OptionalContent = new PdfDictionary();

  public PdfPageLayerCollection(PdfPageBase page)
  {
    this.m_OptionalContent = new PdfDictionary();
    this.m_page = page != null ? page : throw new ArgumentNullException(nameof (page));
    PdfPageBase loadedPage = page;
    if (loadedPage == null)
      return;
    this.ParseLayers(loadedPage);
  }

  public PdfPageLayer Add()
  {
    PdfPageLayer layer = new PdfPageLayer(this.m_page);
    layer.Name = string.Empty;
    this.Add(layer);
    return layer;
  }

  public int Add(PdfPageLayer layer)
  {
    if (layer == null)
      throw new ArgumentNullException(nameof (layer));
    if (layer.Page != this.m_page)
      throw new ArgumentException("The layer belongs to another page");
    int index = this.List.Add((object) layer);
    this.AddLayer(index, layer);
    return index;
  }

  public PdfPageLayer Add(string LayerName)
  {
    PdfPageLayer layer = new PdfPageLayer(this.m_page);
    layer.Name = LayerName;
    layer.LayerId = "OCG_" + Guid.NewGuid().ToString();
    this.Add(layer);
    if (this.m_page is PdfPage)
    {
      this.CreateLayer(layer);
      return layer;
    }
    this.CreateLayerLoadedPage(layer);
    return layer;
  }

  public PdfPageLayer Add(string LayerName, bool Visible)
  {
    PdfPageLayer layer = new PdfPageLayer(this.m_page);
    layer.Name = LayerName;
    layer.Visible = Visible;
    layer.LayerId = "OCG_" + Guid.NewGuid().ToString();
    this.Add(layer);
    this.CreateLayer(layer);
    return layer;
  }

  private void AddLayer(int index, PdfPageLayer layer)
  {
    if (layer == null)
      throw new ArgumentNullException(nameof (layer));
    this.m_page.Contents.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) layer));
  }

  public void Clear()
  {
    int index = 0;
    for (int count = this.List.Count; index < count; ++index)
    {
      this.RemoveLayer(this[index]);
      this.m_page = (PdfPageBase) null;
    }
    this.List.Clear();
  }

  internal void CombineContent(Stream stream)
  {
    bool flag = this.m_page is PdfLoadedPage;
    byte[] buffer = PdfString.StringToByte("\r\n");
    int index = 0;
    for (int count = this.Count; index < count; ++index)
    {
      PdfStream element = ((IPdfWrapper) this[index]).Element as PdfStream;
      if (flag)
        element.Decompress();
      element.InternalStream.WriteTo(stream);
      stream.Write(buffer, 0, buffer.Length);
    }
  }

  public bool Contains(PdfPageLayer layer)
  {
    return layer != null ? this.List.Contains((object) layer) : throw new ArgumentNullException(nameof (layer));
  }

  private void CreateLayer(PdfPageLayer layer)
  {
    PdfPage page = this.m_page as PdfPage;
    PdfDictionary primitive = new PdfDictionary();
    IPdfPrimitive contentDictionary = this.CreateOptionalContentDictionary(layer);
    primitive["OCGs"] = contentDictionary;
    primitive["D"] = this.CreateOptionalContentViews(layer);
    page.Document.Catalog.SetProperty("OCProperties", (IPdfPrimitive) primitive);
  }

  private void CreateLayerLoadedPage(PdfPageLayer layer)
  {
    PdfLoadedPage page = this.m_page as PdfLoadedPage;
    PdfDictionary primitive = new PdfDictionary();
    IPdfPrimitive contentDictionary = this.CreateOptionalContentDictionary(layer, true);
    primitive["OCGs"] = contentDictionary;
    primitive["D"] = this.CreateOptionalContentViews(layer, true);
    page.Document.Catalog.SetProperty("OCProperties", (IPdfPrimitive) primitive);
  }

  private IPdfPrimitive CreateOptionalContentDictionary(PdfPageLayer layer)
  {
    PdfPage page = this.m_page as PdfPage;
    PdfDictionary pdfDictionary = new PdfDictionary();
    pdfDictionary["Name"] = (IPdfPrimitive) new PdfString(layer.Name);
    pdfDictionary["Type"] = (IPdfPrimitive) new PdfName("OCG");
    pdfDictionary["LayerID"] = (IPdfPrimitive) new PdfName(layer.LayerId);
    pdfDictionary["Visible"] = (IPdfPrimitive) new PdfBoolean(layer.Visible);
    if (!layer.PrintState.Equals((object) PdfPrintState.AlwaysPrint))
    {
      PdfPrintState printState = layer.PrintState;
      if (!printState.Equals((object) PdfPrintState.NeverPrint))
      {
        printState = layer.PrintState;
        if (!printState.Equals((object) PdfPrintState.PrintWhenVisible))
          goto label_4;
      }
    }
    layer.m_usage = this.setPrintOption(layer);
    pdfDictionary["Usage"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) layer.m_usage);
    page.Document.m_printLayer.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
label_4:
    PdfReferenceHolder pdfReferenceHolder = new PdfReferenceHolder((IPdfPrimitive) pdfDictionary);
    page.Document.primitive.Insert(page.Document.m_positon, (IPdfPrimitive) pdfReferenceHolder);
    if (!this.m_sublayer)
    {
      layer.m_sublayer = false;
      if (page.Document.m_sublayerposition > 0)
      {
        int count = this.m_page.Contents.Count;
        this.m_page.Contents.RemoveAt(this.parentLayerCount - 1);
        PdfStream pdfStream = new PdfStream();
        byte[] bytes = Encoding.ASCII.GetBytes("EMC\n");
        pdfStream.Write(bytes);
        this.m_page.Contents.Insert(count - 2, (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream));
      }
      page.Document.m_sublayerposition = 0;
      page.Document.m_sublayer = new PdfArray();
      page.Document.m_order.Insert(page.Document.m_orderposition, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_orderposition;
      this.WriteEndMark();
      this.parentLayerCount = this.m_page.Contents.Count;
    }
    else
    {
      layer.m_sublayer = true;
      page.Document.m_sublayer.Insert(page.Document.m_sublayerposition, (IPdfPrimitive) pdfReferenceHolder);
      if (page.Document.m_sublayerposition != 0)
      {
        page.Document.m_order.RemoveAt(page.Document.m_orderposition - 1);
        --page.Document.m_orderposition;
      }
      page.Document.m_order.Insert(page.Document.m_orderposition, (IPdfPrimitive) page.Document.m_sublayer);
      ++page.Document.m_sublayerposition;
      ++page.Document.m_orderposition;
      this.WriteEndMark();
    }
    if (layer.Visible)
    {
      page.Document.m_on.Insert(page.Document.m_onpositon, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_onpositon;
    }
    else
    {
      page.Document.m_off.Insert(page.Document.m_offpositon, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_offpositon;
    }
    ++page.Document.m_positon;
    page.GetResources().AddProperties(layer.LayerId, pdfReferenceHolder);
    return (IPdfPrimitive) page.Document.primitive;
  }

  private IPdfPrimitive CreateOptionalContentDictionary(PdfPageLayer layer, bool isLoadedPage)
  {
    PdfLoadedPage page = this.m_page as PdfLoadedPage;
    PdfDictionary pdfDictionary = new PdfDictionary();
    pdfDictionary["Name"] = (IPdfPrimitive) new PdfString(layer.Name);
    pdfDictionary["Type"] = (IPdfPrimitive) new PdfName("OCG");
    pdfDictionary["LayerID"] = (IPdfPrimitive) new PdfName(layer.LayerId);
    pdfDictionary["Visible"] = (IPdfPrimitive) new PdfBoolean(layer.Visible);
    if (!layer.PrintState.Equals((object) PdfPrintState.AlwaysPrint))
    {
      PdfPrintState printState = layer.PrintState;
      if (!printState.Equals((object) PdfPrintState.NeverPrint))
      {
        printState = layer.PrintState;
        if (!printState.Equals((object) PdfPrintState.PrintWhenVisible))
          goto label_4;
      }
    }
    layer.m_usage = this.setPrintOption(layer);
    pdfDictionary["Usage"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) layer.m_usage);
    page.Document.m_printLayer.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
label_4:
    PdfReferenceHolder pdfReferenceHolder = new PdfReferenceHolder((IPdfPrimitive) pdfDictionary);
    page.Document.primitive.Insert(page.Document.m_positon, (IPdfPrimitive) pdfReferenceHolder);
    if (!this.m_sublayer)
    {
      layer.m_sublayer = false;
      if (page.Document.m_sublayerposition > 0)
      {
        int count = this.m_page.Contents.Count;
        this.m_page.Contents.RemoveAt(this.parentLayerCount - 1);
        PdfStream pdfStream = new PdfStream();
        byte[] bytes = Encoding.ASCII.GetBytes("EMC\n");
        pdfStream.Write(bytes);
        this.m_page.Contents.Insert(count - 2, (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream));
      }
      page.Document.m_sublayerposition = 0;
      page.Document.m_sublayer = new PdfArray();
      page.Document.m_order.Insert(page.Document.m_orderposition, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_orderposition;
      this.WriteEndMark();
      this.parentLayerCount = this.m_page.Contents.Count;
    }
    else
    {
      layer.m_sublayer = true;
      page.Document.m_sublayer.Insert(page.Document.m_sublayerposition, (IPdfPrimitive) pdfReferenceHolder);
      if (page.Document.m_sublayerposition != 0)
      {
        page.Document.m_order.RemoveAt(page.Document.m_orderposition - 1);
        --page.Document.m_orderposition;
      }
      page.Document.m_order.Insert(page.Document.m_orderposition, (IPdfPrimitive) page.Document.m_sublayer);
      ++page.Document.m_sublayerposition;
      ++page.Document.m_orderposition;
      this.WriteEndMark();
    }
    if (layer.Visible)
    {
      page.Document.m_on.Insert(page.Document.m_onpositon, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_onpositon;
    }
    else
    {
      page.Document.m_off.Insert(page.Document.m_offpositon, (IPdfPrimitive) pdfReferenceHolder);
      ++page.Document.m_offpositon;
    }
    ++page.Document.m_positon;
    page.GetResources().AddProperties(layer.LayerId, pdfReferenceHolder);
    return (IPdfPrimitive) page.Document.primitive;
  }

  private IPdfPrimitive CreateOptionalContentViews(PdfPageLayer layer)
  {
    PdfPage page = this.m_page as PdfPage;
    PdfArray pdfArray = new PdfArray();
    this.m_OptionalContent["Name"] = (IPdfPrimitive) new PdfString("Layers");
    this.m_OptionalContent["Order"] = (IPdfPrimitive) page.Document.m_order;
    this.m_OptionalContent["ON"] = (IPdfPrimitive) page.Document.m_on;
    this.m_OptionalContent["OFF"] = (IPdfPrimitive) page.Document.m_off;
    PdfArray primitive = new PdfArray();
    primitive.Add((IPdfPrimitive) new PdfName("Print"));
    PdfDictionary pdfDictionary = new PdfDictionary();
    pdfDictionary.SetProperty("Category", (IPdfPrimitive) primitive);
    pdfDictionary.SetProperty("OCGs", (IPdfPrimitive) page.Document.m_printLayer);
    pdfDictionary.SetProperty("Event", (IPdfPrimitive) new PdfName("Print"));
    pdfArray.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
    this.m_OptionalContent["AS"] = (IPdfPrimitive) pdfArray;
    return (IPdfPrimitive) this.m_OptionalContent;
  }

  private IPdfPrimitive CreateOptionalContentViews(PdfPageLayer layer, bool isLoadedPage)
  {
    PdfLoadedPage page = this.m_page as PdfLoadedPage;
    PdfArray pdfArray = new PdfArray();
    this.m_OptionalContent["Name"] = (IPdfPrimitive) new PdfString("Layers");
    this.m_OptionalContent["Order"] = (IPdfPrimitive) page.Document.m_order;
    this.m_OptionalContent["ON"] = (IPdfPrimitive) page.Document.m_on;
    this.m_OptionalContent["OFF"] = (IPdfPrimitive) page.Document.m_off;
    PdfArray primitive = new PdfArray();
    primitive.Add((IPdfPrimitive) new PdfName("Print"));
    PdfDictionary pdfDictionary = new PdfDictionary();
    pdfDictionary.SetProperty("Category", (IPdfPrimitive) primitive);
    pdfDictionary.SetProperty("OCGs", (IPdfPrimitive) page.Document.m_printLayer);
    pdfDictionary.SetProperty("Event", (IPdfPrimitive) new PdfName("Print"));
    pdfArray.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
    this.m_OptionalContent["AS"] = (IPdfPrimitive) pdfArray;
    return (IPdfPrimitive) this.m_OptionalContent;
  }

  public int IndexOf(PdfPageLayer layer)
  {
    return layer != null ? this.List.IndexOf((object) layer) : throw new ArgumentNullException(nameof (layer));
  }

  public void Insert(int index, PdfPageLayer layer)
  {
    if (index < 0)
      throw new ArgumentOutOfRangeException(nameof (index), "Value can not be less 0");
    if (layer == null)
      throw new ArgumentNullException(nameof (layer));
    if (layer.Page != this.m_page)
      throw new ArgumentException("The layer belongs to another page");
    this.List.Insert(index, (object) layer);
    this.InsertLayer(index, layer);
  }

  private void InsertLayer(int index, PdfPageLayer layer)
  {
    PdfReferenceHolder element = layer != null ? new PdfReferenceHolder((IPdfWrapper) layer) : throw new ArgumentNullException(nameof (layer));
    this.m_page.Contents.Insert(index, (IPdfPrimitive) element);
  }

  private void ParseLayers(PdfPageBase loadedPage)
  {
    if (loadedPage == null)
      throw new ArgumentNullException(nameof (loadedPage));
    PdfArray contents = this.m_page.Contents;
    PdfDictionary resources = (PdfDictionary) this.m_page.GetResources();
    PdfDictionary pdfDictionary1 = (PdfDictionary) null;
    PdfDictionary pdfDictionary2 = (PdfDictionary) null;
    bool flag = false;
    PdfCrossTable crossTable;
    if (loadedPage is PdfPage)
    {
      crossTable = (loadedPage as PdfPage).CrossTable;
    }
    else
    {
      crossTable = (loadedPage as PdfLoadedPage).CrossTable;
      pdfDictionary2 = PdfCrossTable.Dereference(resources["Properties"]) as PdfDictionary;
      pdfDictionary1 = PdfCrossTable.Dereference((loadedPage as PdfLoadedPage).Document.Catalog["OCProperties"]) as PdfDictionary;
    }
    PdfStream pdfStream1 = new PdfStream();
    PdfStream pdfStream2 = new PdfStream();
    byte num1 = 113;
    byte num2 = 10;
    byte num3 = 81;
    byte[] numArray1 = new byte[1]{ num1 };
    pdfStream1.Data = numArray1;
    contents.Insert(0, (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream1));
    numArray1[0] = num3;
    pdfStream2.Data = numArray1;
    contents.Insert(contents.Count, (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream2));
    foreach (IPdfPrimitive pointer in contents)
    {
      try
      {
        if (!(crossTable.GetObject(pointer) is PdfStream stream))
          throw new PdfDocumentException("Invalid contents array.");
        if (!loadedPage.Imported)
          stream.Decompress();
        string str1 = PdfString.ByteToString(stream.Data);
        if (!loadedPage.Imported && contents.Count == 1 && ((int) stream.Data[stream.Data.Length - 2] == (int) num3 || (int) stream.Data[stream.Data.Length - 1] == (int) num3))
        {
          byte[] data = stream.Data;
          byte[] numArray2 = new byte[data.Length + 4];
          numArray2[0] = num1;
          numArray2[1] = num2;
          data.CopyTo((Array) numArray2, 2);
          numArray2[numArray2.Length - 2] = num2;
          numArray2[numArray2.Length - 1] = num3;
          stream.Data = numArray2;
        }
        if (pdfDictionary1 != null && pdfDictionary2 != null)
        {
          foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair1 in pdfDictionary2.Items)
          {
            string str2 = keyValuePair1.Key.ToString();
            PdfDictionary pdfDictionary3 = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfDictionary;
            if (PdfCrossTable.Dereference(pdfDictionary3["Usage"]) is PdfDictionary pdfDictionary5)
            {
              if (str1.Contains(str2))
              {
                PdfPageLayer pdfPageLayer = new PdfPageLayer(loadedPage, stream);
                if (PdfCrossTable.Dereference(pdfDictionary5["Print"]) is PdfDictionary pdfDictionary4)
                {
                  pdfPageLayer.m_printOption = pdfDictionary4;
                  foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair2 in pdfDictionary4.Items)
                  {
                    if (keyValuePair2.Key.Value.Equals("PrintState"))
                    {
                      pdfPageLayer.PrintState = !(keyValuePair2.Value as PdfName).Value.Equals("ON") ? PdfPrintState.NeverPrint : PdfPrintState.AlwaysPrint;
                      break;
                    }
                  }
                }
                PdfString pdfString = PdfCrossTable.Dereference(pdfDictionary3["Name"]) as PdfString;
                pdfPageLayer.Name = pdfString.Value;
                this.List.Add((object) pdfPageLayer);
                flag = true;
                break;
              }
            }
            else if (str1.Contains(str2))
            {
              PdfPageLayer pdfPageLayer = new PdfPageLayer(loadedPage, stream);
              this.List.Add((object) pdfPageLayer);
              if (pdfDictionary3.ContainsKey("Name"))
              {
                PdfString pdfString = PdfCrossTable.Dereference(pdfDictionary3["Name"]) as PdfString;
                pdfPageLayer.Name = pdfString.Value;
              }
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          this.List.Add((object) new PdfPageLayer(loadedPage, stream));
        else
          flag = false;
      }
      catch (Exception ex)
      {
        if (ex is PdfDocumentException)
          throw new PdfDocumentException("Invalid contents array.");
      }
    }
  }

  public void Remove(PdfPageLayer layer)
  {
    if (layer == null)
      throw new ArgumentNullException(nameof (layer));
    this.List.Remove((object) layer);
    this.RemoveLayer(layer);
  }

  public void Remove(string name)
  {
    for (int index = 0; index < this.List.Count; ++index)
    {
      PdfPageLayer layer = this.List[index] as PdfPageLayer;
      if (layer.Name == name)
      {
        this.RemoveLayer(layer);
        this.List.Remove((object) layer);
        break;
      }
    }
  }

  public void RemoveAt(int index)
  {
    if (index < 0 || index > this.List.Count - 1)
      throw new ArgumentOutOfRangeException(nameof (index), "Value can not be less 0 and greater List.Count - 1");
    PdfPageLayer layer = this[index];
    this.List.RemoveAt(index);
    if (layer == null)
      return;
    this.RemoveLayer(layer);
  }

  private void RemoveLayer(PdfPageLayer layer)
  {
    PdfReferenceHolder element = layer != null ? new PdfReferenceHolder((IPdfWrapper) layer) : throw new ArgumentNullException(nameof (layer));
    if (this.m_page == null)
      return;
    this.m_page.Contents.Remove((IPdfPrimitive) element);
  }

  private PdfDictionary setPrintOption(PdfPageLayer layer)
  {
    PdfDictionary pdfDictionary = new PdfDictionary();
    layer.m_printOption = new PdfDictionary();
    layer.m_printOption.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Print"));
    if (layer.PrintState.Equals((object) PdfPrintState.NeverPrint))
      layer.m_printOption.SetProperty("PrintState", (IPdfPrimitive) new PdfName("OFF"));
    else if (layer.PrintState.Equals((object) PdfPrintState.AlwaysPrint))
      layer.m_printOption.SetProperty("PrintState", (IPdfPrimitive) new PdfName("ON"));
    pdfDictionary.SetProperty("Print", (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) layer.m_printOption));
    return pdfDictionary;
  }

  private void WriteEndMark()
  {
    PdfStream pdfStream = new PdfStream();
    byte[] bytes = Encoding.ASCII.GetBytes("EMC\n");
    pdfStream.Write(bytes);
    this.m_page.Contents.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream));
  }

  public PdfPageLayer this[int index]
  {
    get => this.List[index] as PdfPageLayer;
    set
    {
      if (value == null)
        throw new ArgumentNullException("layer");
      if (value.Page != this.m_page)
        throw new ArgumentException("The layer belongs to another page");
      PdfPageLayer layer = this[index];
      if (layer != null)
        this.RemoveLayer(layer);
      this.List[index] = (object) value;
      this.InsertLayer(index, value);
    }
  }
}
