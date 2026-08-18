// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PageResourceLoader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal sealed class PageResourceLoader
{
  private static PageResourceLoader s_instance;
  private static object s_lock = new object();

  public string DecodeTest(PdfPageBase page, string fontName, string textToDecode)
  {
    PdfPageResources pageResources = this.GetPageResources(page);
    Encoding.Default.GetBytes(textToDecode);
    return (pageResources[fontName] as FontStructure).Decode(textToDecode, pageResources.isSameFont());
  }

  internal Dictionary<string, object> GetExtendedGraphicResources(PdfDictionary resourceDictionary)
  {
    Dictionary<string, object> graphicResources = new Dictionary<string, object>();
    if (resourceDictionary != null && resourceDictionary.ContainsKey("ExtGState"))
    {
      IPdfPrimitive pdfPrimitive = !(resourceDictionary["ExtGState"] is PdfDictionary) ? (resourceDictionary["ExtGState"] as PdfReferenceHolder).Object : resourceDictionary["ExtGState"];
      if (!(pdfPrimitive is PdfDictionary))
        return graphicResources;
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in ((PdfDictionary) pdfPrimitive).Items)
      {
        if ((object) (keyValuePair.Value as PdfReferenceHolder) != null)
        {
          PdfDictionary xobjectDictionary = (keyValuePair.Value as PdfReferenceHolder).Object as PdfDictionary;
          graphicResources.Add(keyValuePair.Key.Value, (object) new XObjectElement(xobjectDictionary, keyValuePair.Key.Value));
        }
        else
        {
          PdfDictionary xobjectDictionary = keyValuePair.Value as PdfDictionary;
          graphicResources.Add(keyValuePair.Key.Value, (object) new XObjectElement(xobjectDictionary, keyValuePair.Key.Value));
        }
      }
    }
    return graphicResources;
  }

  internal Dictionary<string, object> GetFontResources(PdfDictionary resourceDictionary)
  {
    Dictionary<string, object> fontResources = new Dictionary<string, object>();
    if (resourceDictionary != null)
    {
      IPdfPrimitive resource = resourceDictionary["Font"];
      if (resource == null)
        return fontResources;
      PdfDictionary pdfDictionary = (object) (resource as PdfReferenceHolder) == null ? resource as PdfDictionary : (resource as PdfReferenceHolder).Object as PdfDictionary;
      if (pdfDictionary == null)
        return fontResources;
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary.Items)
      {
        if ((object) (keyValuePair.Value as PdfReferenceHolder) != null)
          fontResources.Add(keyValuePair.Key.Value, (object) new FontStructure((keyValuePair.Value as PdfReferenceHolder).Object));
        else
          fontResources.Add(keyValuePair.Key.Value, (object) new FontStructure(keyValuePair.Value));
      }
    }
    return fontResources;
  }

  internal Dictionary<string, object> GetFontResources(
    PdfDictionary resourceDictionary,
    PdfPageBase page)
  {
    Dictionary<string, object> fontResources = new Dictionary<string, object>();
    if (resourceDictionary != null)
    {
      IPdfPrimitive resource = resourceDictionary["Font"];
      if (resource != null)
      {
        PdfDictionary pdfDictionary = (object) (resource as PdfReferenceHolder) == null ? resource as PdfDictionary : (resource as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary != null)
        {
          foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary.Items)
          {
            if ((object) (keyValuePair.Value as PdfReferenceHolder) != null)
              fontResources.Add(keyValuePair.Key.Value, (object) new FontStructure((keyValuePair.Value as PdfReferenceHolder).Object));
            else
              fontResources.Add(keyValuePair.Key.Value, (object) new FontStructure(keyValuePair.Value));
          }
        }
      }
      IPdfPrimitive pdfPrimitive1 = page.Dictionary["Parent"];
      if (pdfPrimitive1 != null)
      {
        IPdfPrimitive pdfPrimitive2 = new PdfResources((pdfPrimitive1 as PdfReferenceHolder).Object as PdfDictionary)["Font"];
        if (pdfPrimitive2 == null || !(pdfPrimitive2 is PdfDictionary pdfDictionary))
          return fontResources;
        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary.Items)
        {
          if (keyValuePair.Value is PdfDictionary)
            fontResources.Add(keyValuePair.Key.Value, (object) (keyValuePair.Value as PdfReferenceHolder).Object);
          fontResources.Add(keyValuePair.Key.Value, (object) new FontStructure(keyValuePair.Value));
        }
      }
    }
    return fontResources;
  }

  internal Dictionary<string, object> GetImageResources(
    PdfDictionary resourceDictionary,
    PdfPageBase page,
    ref Dictionary<string, PdfMatrix> commonMatrix)
  {
    Dictionary<string, object> imageResources = new Dictionary<string, object>();
    if (resourceDictionary != null && resourceDictionary.ContainsKey("XObject"))
    {
      IPdfPrimitive pdfPrimitive = !(resourceDictionary["XObject"] is PdfDictionary) ? (resourceDictionary["XObject"] as PdfReferenceHolder).Object : resourceDictionary["XObject"];
      if (!(pdfPrimitive is PdfDictionary))
        return imageResources;
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair1 in ((PdfDictionary) pdfPrimitive).Items)
      {
        if ((object) (keyValuePair1.Value as PdfReferenceHolder) != null)
        {
          PdfDictionary pdfDictionary = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfDictionary;
          if (pdfDictionary.ContainsKey("Subtype"))
          {
            if ((pdfDictionary["Subtype"] as PdfName).Value == "Image")
            {
              ImageStructure imageStructure = new ImageStructure((IPdfPrimitive) pdfDictionary, new PdfMatrix());
              if (commonMatrix.ContainsKey(keyValuePair1.Key.Value))
                imageStructure = new ImageStructure((IPdfPrimitive) pdfDictionary, commonMatrix[keyValuePair1.Key.Value]);
              imageResources.Add(keyValuePair1.Key.Value, (object) imageStructure);
            }
            else if ((pdfDictionary["Subtype"] as PdfName).Value == "Form" && page != null)
            {
              if (pdfDictionary.ContainsKey("Resources") && pdfDictionary["Resources"] is PdfDictionary)
              {
                foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair2 in (pdfDictionary["Resources"] as PdfDictionary).Items)
                {
                  if (keyValuePair2.Key.Value == "XObject" && keyValuePair2.Value is PdfDictionary)
                  {
                    foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair3 in (keyValuePair2.Value as PdfDictionary).Items)
                    {
                      PdfStream pdfStream = pdfDictionary as PdfStream;
                      pdfStream.Decompress();
                      MemoryStream internalStream = pdfStream.InternalStream;
                      internalStream.Position = 0L;
                      PdfMatrix pdfMatrix = new PdfMatrix(new PdfReader((Stream) internalStream)
                      {
                        Position = 0L
                      }, keyValuePair3.Key.Value, page.Size);
                      if (!commonMatrix.ContainsKey(keyValuePair3.Key.Value))
                        commonMatrix.Add(keyValuePair3.Key.Value, pdfMatrix);
                    }
                  }
                }
              }
              imageResources.Add(keyValuePair1.Key.Value, (object) new XObjectElement(pdfDictionary, keyValuePair1.Key.Value));
            }
            if (!imageResources.ContainsKey(keyValuePair1.Key.Value))
              imageResources.Add(keyValuePair1.Key.Value, (object) new XObjectElement(pdfDictionary, keyValuePair1.Key.Value));
          }
        }
        else
        {
          PdfDictionary xobjectDictionary = keyValuePair1.Value as PdfDictionary;
          imageResources.Add(keyValuePair1.Key.Value, (object) new XObjectElement(xobjectDictionary, keyValuePair1.Key.Value));
        }
      }
    }
    return imageResources;
  }

  public PdfPageResources GetPageResources(PdfPageBase page)
  {
    PdfPageResources pageResources1 = new PdfPageResources();
    float num = 0.0f;
    PdfDictionary resources = (PdfDictionary) page.GetResources();
    PdfArray annots = page.GetAnnots();
    Dictionary<string, PdfMatrix> commonMatrix = new Dictionary<string, PdfMatrix>();
    PdfPageResources pageResources2 = this.UpdatePageResources(this.UpdatePageResources(this.UpdatePageResources(pageResources1, this.GetFontResources(resources, page)), this.GetImageResources(resources, page, ref commonMatrix)), this.GetExtendedGraphicResources(resources));
    if (annots != null)
      pageResources2.Add("Annotations", (object) annots);
    while (resources != null && resources.ContainsKey("XObject"))
    {
      PdfDictionary pdfDictionary1 = (object) (resources["XObject"] as PdfReferenceHolder) == null ? resources["XObject"] as PdfDictionary : (resources["XObject"] as PdfReferenceHolder).Object as PdfDictionary;
      resources = pdfDictionary1["Resources"] as PdfDictionary;
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary1.Items)
      {
        PdfDictionary pdfDictionary2 = (object) (keyValuePair.Value as PdfReferenceHolder) == null ? keyValuePair.Value as PdfDictionary : (keyValuePair.Value as PdfReferenceHolder).Object as PdfDictionary;
        if (pdfDictionary2.ContainsKey("Resources"))
        {
          if ((object) (pdfDictionary2["Resources"] as PdfReferenceHolder) != null)
          {
            PdfReferenceHolder pdfReferenceHolder = pdfDictionary2["Resources"] as PdfReferenceHolder;
            if ((double) num != (double) pdfReferenceHolder.Reference.ObjNum)
            {
              resources = pdfReferenceHolder.Object as PdfDictionary;
              num = (float) pdfReferenceHolder.Reference.ObjNum;
            }
            else
              continue;
          }
          else
            resources = pdfDictionary2["Resources"] as PdfDictionary;
          pageResources2 = this.UpdatePageResources(pageResources2, this.GetFontResources(resources, page));
          pageResources2 = this.UpdatePageResources(pageResources2, this.GetImageResources(resources, page, ref commonMatrix));
        }
      }
    }
    if (page.Rotation == PdfPageRotateAngle.RotateAngle90)
    {
      pageResources2.Add("Rotate", (object) 90f);
      return pageResources2;
    }
    if (page.Rotation == PdfPageRotateAngle.RotateAngle180)
    {
      pageResources2.Add("Rotate", (object) 180f);
      return pageResources2;
    }
    if (page.Rotation == PdfPageRotateAngle.RotateAngle270)
      pageResources2.Add("Rotate", (object) 270f);
    return pageResources2;
  }

  internal PdfPageResources UpdatePageResources(
    PdfPageResources pageResources,
    Dictionary<string, object> objects)
  {
    foreach (KeyValuePair<string, object> keyValuePair in objects)
      pageResources.Add(keyValuePair.Key, keyValuePair.Value);
    return pageResources;
  }

  public static PageResourceLoader Instance
  {
    get
    {
      if (PageResourceLoader.s_instance == null)
      {
        lock (PageResourceLoader.s_lock)
          PageResourceLoader.s_instance = new PageResourceLoader();
      }
      return PageResourceLoader.s_instance;
    }
  }
}
