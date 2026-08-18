// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Exporting;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;


namespace Syncfusion.Pdf
{
    public abstract class PdfPageBase : IPdfWrapper
    {
      private PdfLoadedAnnotationCollection m_annotations;
      private int m_annotCount;
      private PdfTemplate m_contentTemplate;
      private string m_currentFont;
      private int m_defLayerIndex = -1;
      private List<RectangleF> m_extractedImagesBounds;
      private List<PdfName> m_fontNames;
      internal List<IPdfPrimitive> m_fontReference;
      private Stack<GraphicsState> m_graphicsState = new Stack<GraphicsState>();
      private PdfImageInfo[] m_imageinfo;
      private bool m_imported;
      private PdfPageLayerCollection m_layers;
      private int m_layersCount;
      private bool m_modified;
      private long m_pageContentLength;
      private PdfDictionary m_pageDictionary;
      private PdfPageResources m_pageResources;
      private PdfRecordCollection m_recordCollection;
      private PdfResources m_resources;
      private char[] m_symbolChars = new char[6]
      {
        '(',
        ')',
        '[',
        ']',
        '<',
        '>'
      };
      private PageResourceLoader resourceLoader = new PageResourceLoader();
      private string resultantText;

      internal PdfPageBase(PdfDictionary dic)
      {
        this.m_pageDictionary = dic != null ? dic : throw new ArgumentNullException(nameof (dic));
      }

      internal virtual void Clear()
      {
        if (this.m_pageResources != null)
        {
          this.m_pageResources.Resources.Clear();
          this.m_pageResources = (PdfPageResources) null;
        }
        this.m_graphicsState = (Stack<GraphicsState>) null;
        if (this.m_layers != null)
          this.m_layers.Clear();
        this.m_layers = (PdfPageLayerCollection) null;
        this.m_resources = (PdfResources) null;
        this.m_pageDictionary = (PdfDictionary) null;
        this.m_annotations = (PdfLoadedAnnotationCollection) null;
        this.m_fontNames = (List<PdfName>) null;
        this.m_fontReference = (List<IPdfPrimitive>) null;
        if (this.m_contentTemplate == null)
          return;
        this.m_contentTemplate = (PdfTemplate) null;
      }

      public PdfTemplate CreateTemplate() => this.GetContent();

      private void DrawAnnotationTemplates(PdfGraphics g)
      {
        PdfArray annots = this.GetAnnots();
        if (annots == null)
          return;
        foreach (IPdfPrimitive pdfPrimitive in annots)
        {
          PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
          PdfDictionary annotation = !(pdfReferenceHolder != (PdfReferenceHolder) null) ? pdfPrimitive as PdfDictionary : pdfReferenceHolder.Object as PdfDictionary;
          PdfTemplate annotTemplate = this.GetAnnotTemplate(annotation);
          if (annotTemplate != null)
          {
            PointF location = this.NormalizeAnnotationLocation(this.GetAnnotationLocation(annotation), g, annotTemplate);
            g.DrawPdfTemplate(annotTemplate, location);
          }
        }
      }

      public Image[] ExtractImages()
      {
        PdfDictionary pdfDictionary = new PdfDictionary();
        ArrayList arrayList = new ArrayList();
        this.m_extractedImagesBounds = new List<RectangleF>();
        System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive> items = this.GetResources().Items;
        this.m_pageResources = PageResourceLoader.Instance.GetPageResources(this);
        List<PdfImageInfo> pdfImageInfoList = new List<PdfImageInfo>();
        foreach (KeyValuePair<string, object> resource in this.m_pageResources.Resources)
        {
          if (resource.Value is ImageStructure)
          {
            try
            {
              if ((resource.Value as ImageStructure).ImageFilter[0] != "JPXDecode")
              {
                PdfDictionary imageDictionary1 = (resource.Value as ImageStructure).ImageDictionary;
                if (imageDictionary1 != null)
                {
                  PdfArray pdfArray1 = (PdfArray) null;
                  PdfArray pdfArray2 = (PdfArray) null;
                  if (imageDictionary1["ColorSpace"] != null)
                  {
                    if (imageDictionary1["ColorSpace"] is PdfArray)
                      pdfArray1 = imageDictionary1["ColorSpace"] as PdfArray;
                    if ((object) (imageDictionary1["ColorSpace"] as PdfReferenceHolder) != null)
                      pdfArray1 = (imageDictionary1["ColorSpace"] as PdfReferenceHolder).Object as PdfArray;
                    if (pdfArray1 != null && (object) (pdfArray1[1] as PdfReferenceHolder) != null)
                      pdfArray2 = (pdfArray1[1] as PdfReferenceHolder).Object as PdfArray;
                    if (pdfArray2 != null && (object) (pdfArray2[1] as PdfReferenceHolder) != null)
                    {
                      if (!((pdfArray2[1] as PdfReferenceHolder).Object as PdfDictionary).ContainsKey("Alternate"))
                        continue;
                    }
                  }
                  else
                    continue;
                }
                PdfStream imageDictionary2 = (resource.Value as ImageStructure).ImageDictionary as PdfStream;
                int width = 0;
                int height = 0;
                if (imageDictionary2.ContainsKey("Width"))
                {
                  if (imageDictionary2["Width"] is PdfNumber)
                    width = (imageDictionary2["Width"] as PdfNumber).IntValue;
                  if ((object) (imageDictionary2["Width"] as PdfReferenceHolder) != null)
                    width = ((imageDictionary2["Width"] as PdfReferenceHolder).Object as PdfNumber).IntValue;
                  if (imageDictionary2["Height"] is PdfNumber)
                    height = (imageDictionary2["Height"] as PdfNumber).IntValue;
                  if ((object) (imageDictionary2["Height"] as PdfReferenceHolder) != null)
                    height = ((imageDictionary2["Height"] as PdfReferenceHolder).Object as PdfNumber).IntValue;
                }
                else if (imageDictionary2.ContainsKey("BBox"))
                {
                  PdfArray pdfArray = imageDictionary2["BBox"] as PdfArray;
                  width = (pdfArray[2] as PdfNumber).IntValue;
                  height = (pdfArray[3] as PdfNumber).IntValue;
                }
                RectangleF rectangleF = new RectangleF(0.0f, 0.0f, (float) width, (float) height);
                PdfImageInfo pdfImageInfo = new PdfImageInfo();
                pdfImageInfo.Name = resource.Key.ToString();
                pdfImageInfo.IsImageExtracted = true;
                Image embeddedImage = (resource.Value as ImageStructure).EmbeddedImage;
                if (embeddedImage != null)
                {
                  arrayList.Add((object) embeddedImage);
                  this.m_extractedImagesBounds.Add(rectangleF);
                  pdfImageInfoList.Add(pdfImageInfo);
                }
              }
            }
            catch (Exception ex)
            {
            }
          }
        }
        this.m_imageinfo = pdfImageInfoList.ToArray();
        Image[] images = new Image[arrayList.Count];
        arrayList.CopyTo((Array) images);
        int index1 = 0;
        int index2 = 0;
        IEnumerator enumerator = (IEnumerator) this.m_extractedImagesBounds.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (!this.m_imageinfo[index1].IsImageExtracted)
          {
            this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
            this.m_imageinfo[index1].Image = (Image) null;
            this.m_imageinfo[index1].Index = index1;
            ++index1;
          }
          else if (this.m_imageinfo[index1].IsImageExtracted)
          {
            Image image = images[index2];
            this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
            this.m_imageinfo[index1].Image = image;
            this.m_imageinfo[index1].Index = index1;
            ++index1;
            ++index2;
          }
        }
        return images;
      }

      internal Image[] ExtractImages(bool imageExtraction)
      {
        PdfDictionary pdfDictionary = new PdfDictionary();
        ArrayList arrayList = new ArrayList();
        List<PdfMatrix> pdfMatrixList = new List<PdfMatrix>();
        List<bool> boolList = new List<bool>();
        this.m_extractedImagesBounds = new List<RectangleF>();
        PdfArray contents = this.Contents;
        System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive> items = this.GetResources().Items;
        System.Collections.Generic.Dictionary<string, PdfMatrix> dictionary = new System.Collections.Generic.Dictionary<string, PdfMatrix>();
        PdfStream pdfStream1 = (PdfStream) null;
        string empty = string.Empty;
        this.m_pageResources = PageResourceLoader.Instance.GetPageResources(this);
        List<PdfImageInfo> pdfImageInfoList = new List<PdfImageInfo>();
        foreach (KeyValuePair<string, object> resource1 in this.m_pageResources.Resources)
        {
          if (resource1.Value is ImageStructure || resource1.Value is XObjectElement)
          {
            try
            {
              if (resource1.Value is XObjectElement)
              {
                string key1 = resource1.Key.ToString();
                for (int index = 0; index < contents.Count; ++index)
                {
                  PdfStream pdfStream2 = pdfStream1 == null ? (contents[index] as PdfReferenceHolder).Object as PdfStream : pdfStream1;
                  pdfStream2.Decompress();
                  MemoryStream internalStream1 = pdfStream2.InternalStream;
                  internalStream1.Position = 0L;
                  PdfReader ContentStream = new PdfReader((Stream) internalStream1);
                  ContentStream.Position = 0L;
                  if (ContentStream.ReadStream().Contains(key1))
                  {
                    MemoryStream internalStream2 = ((resource1.Value as XObjectElement).XObjectDictionary as PdfStream).InternalStream;
                    internalStream2.Position = 0L;
                    PdfReader pdfReader = new PdfReader((Stream) internalStream2);
                    pdfReader.Position = 0L;
                    bool flag = false;
                    foreach (KeyValuePair<string, object> resource2 in this.m_pageResources.Resources)
                    {
                      if (resource2.Value is ImageStructure || resource2.Value is FontStructure)
                      {
                        string key2 = resource2.Key.ToString();
                        if (resource2.Value is ImageStructure)
                        {
                          if (pdfReader.ReadStream().Contains(key2) && !flag)
                            dictionary.Add(key2, new PdfMatrix(ContentStream, key1, this.Size));
                        }
                        else if (resource2.Value is FontStructure)
                        {
                          if (pdfReader.ReadStream().Contains(key2))
                            flag = true;
                          pdfReader.Position = 0L;
                        }
                      }
                    }
                    break;
                  }
                }
              }
              else
              {
                PdfStream imageDictionary = (resource1.Value as ImageStructure).ImageDictionary as PdfStream;
                string key = resource1.Key.ToString();
                PdfMatrix pdfMatrix = (resource1.Value as ImageStructure).ImageInfo;
                for (int index = 0; index < contents.Count; ++index)
                {
                  PdfStream pdfStream3 = pdfStream1 == null ? (contents[index] as PdfReferenceHolder).Object as PdfStream : pdfStream1;
                  pdfStream3.Decompress();
                  MemoryStream internalStream = pdfStream3.InternalStream;
                  internalStream.Position = 0L;
                  PdfReader ContentStream = new PdfReader((Stream) internalStream);
                  ContentStream.Position = 0L;
                  if (ContentStream.ReadStream().Contains(key))
                  {
                    pdfMatrix = new PdfMatrix(ContentStream, key, this.Size);
                    break;
                  }
                }
                bool flag = false;
                if (resource1.Value is ImageStructure && (resource1.Value as ImageStructure).ImageDictionary.ContainsKey("Mask"))
                  flag = true;
                int width = 0;
                int height = 0;
                if (imageDictionary.ContainsKey("Width"))
                {
                  if (imageDictionary["Width"] is PdfNumber)
                    width = (imageDictionary["Width"] as PdfNumber).IntValue;
                  if ((object) (imageDictionary["Width"] as PdfReferenceHolder) != null)
                    width = ((imageDictionary["Width"] as PdfReferenceHolder).Object as PdfNumber).IntValue;
                  if (imageDictionary["Height"] is PdfNumber)
                    height = (imageDictionary["Height"] as PdfNumber).IntValue;
                  if ((object) (imageDictionary["Height"] as PdfReferenceHolder) != null)
                    height = ((imageDictionary["Height"] as PdfReferenceHolder).Object as PdfNumber).IntValue;
                }
                else if (imageDictionary.ContainsKey("BBox"))
                {
                  PdfArray pdfArray = imageDictionary["BBox"] as PdfArray;
                  width = (pdfArray[2] as PdfNumber).IntValue;
                  height = (pdfArray[3] as PdfNumber).IntValue;
                }
                RectangleF rectangleF = pdfMatrix == null ? new RectangleF(0.0f, 0.0f, (float) width, (float) height) : ((double) pdfMatrix.GetWidth != -1.0 || (double) pdfMatrix.GetHeight != -1.0 ? new RectangleF(new PointF(Math.Abs(pdfMatrix.GetScaleX), Math.Abs(pdfMatrix.GetScaleY)), new SizeF(Math.Abs(pdfMatrix.GetWidth), Math.Abs(pdfMatrix.GetHeight))) : new RectangleF(new PointF(Math.Abs(pdfMatrix.GetScaleX), Math.Abs(pdfMatrix.GetScaleY)), new SizeF((float) Math.Abs(width), (float) Math.Abs(height))));
                if (!pdfMatrix.m_scaledBounds.IsEmpty)
                  rectangleF = pdfMatrix.m_scaledBounds;
                PdfImageInfo pdfImageInfo = new PdfImageInfo();
                pdfImageInfo.Name = resource1.Key.ToString();
                pdfImageInfo.IsImageExtracted = true;
                Image embeddedImage = (resource1.Value as ImageStructure).EmbeddedImage;
                if (embeddedImage != null)
                {
                  arrayList.Add((object) embeddedImage);
                  this.m_extractedImagesBounds.Add(rectangleF);
                  pdfImageInfoList.Add(pdfImageInfo);
                  pdfMatrixList.Add(pdfMatrix);
                  boolList.Add(flag);
                }
              }
            }
            catch (Exception ex)
            {
            }
          }
        }
        if (dictionary.Count > 0)
        {
          for (int index = 0; index < pdfImageInfoList.Count; ++index)
          {
            if (dictionary.ContainsKey(pdfImageInfoList[index].Name))
            {
              RectangleF rectangleF = this.m_extractedImagesBounds[index];
              PdfMatrix pdfMatrix = dictionary[pdfImageInfoList[index].Name];
              rectangleF = (double) pdfMatrix.GetWidth >= 1.0 || (double) pdfMatrix.GetHeight >= 1.0 ? new RectangleF(pdfMatrix.GetScaleX + rectangleF.X, pdfMatrix.GetScaleX + rectangleF.Y, pdfMatrix.GetWidth, pdfMatrix.GetHeight) : new RectangleF(pdfMatrix.GetScaleX + rectangleF.X * pdfMatrix.GetWidth, pdfMatrix.TopMargin + rectangleF.Y * pdfMatrix.GetHeight, rectangleF.Width * pdfMatrix.GetWidth, rectangleF.Height * pdfMatrix.GetHeight);
              this.m_extractedImagesBounds[index] = rectangleF;
            }
          }
        }
        this.m_imageinfo = pdfImageInfoList.ToArray();
        Image[] images = new Image[arrayList.Count];
        arrayList.CopyTo((Array) images);
        int index1 = 0;
        int index2 = 0;
        IEnumerator enumerator = (IEnumerator) this.m_extractedImagesBounds.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (!this.m_imageinfo[index1].IsImageExtracted)
          {
            this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
            this.m_imageinfo[index1].Image = (Image) null;
            this.m_imageinfo[index1].Index = index1;
            this.m_imageinfo[index1].Matrix = pdfMatrixList[index1];
            this.m_imageinfo[index1].MaskImage = boolList[index1];
            ++index1;
          }
          else if (this.m_imageinfo[index1].IsImageExtracted)
          {
            Image image = images[index2];
            this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
            this.m_imageinfo[index1].Image = image;
            this.m_imageinfo[index1].Index = index1;
            this.m_imageinfo[index1].Matrix = pdfMatrixList[index1];
            this.m_imageinfo[index1].MaskImage = boolList[index1];
            ++index1;
            ++index2;
          }
        }
        return images;
      }

      public string ExtractText()
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          this.Layers.CombineContent((Stream) memoryStream);
          memoryStream.Position = 0L;
          this.m_recordCollection = new ContentParser(memoryStream.ToArray()).ReadContent();
        }
        this.m_pageResources = PageResourceLoader.Instance.GetPageResources(this);
        this.RenderText(this.m_recordCollection, this.m_pageResources);
        if (this.resultantText != null)
          this.resultantText = this.SkipEscapeSequence(this.resultantText);
        return this.resultantText;
      }

      private int GetAnnotationCount() => this.m_annotations != null ? this.m_annotations.Count : 0;

      private PointF GetAnnotationLocation(PdfDictionary annotation)
      {
        if (!(PdfCrossTable.Dereference(annotation["Rect"]) is PdfArray pdfArray))
          throw new PdfDocumentException("Invalid format: annotation dictionary doesn't contain rectangle array.");
        PdfNumber pdfNumber1 = pdfArray.Count >= 4 ? pdfArray[0] as PdfNumber : throw new PdfDocumentException("Invalid format: annotation rectangle has less then four elements.");
        PdfNumber pdfNumber2 = pdfArray[1] as PdfNumber;
        PdfNumber pdfNumber3 = pdfArray[2] as PdfNumber;
        PdfNumber pdfNumber4 = pdfArray[3] as PdfNumber;
        return new PointF(Math.Min(pdfNumber1.FloatValue, pdfNumber3.FloatValue), Math.Min(pdfNumber2.FloatValue, pdfNumber4.FloatValue));
      }

      private SizeF GetAnnotationSize(PdfDictionary annotation)
      {
        return this.GetElementSize(annotation, "Rect");
      }

      internal PdfArray GetAnnots()
      {
        IPdfPrimitive pdfPrimitive = this.Dictionary.GetValue("Annots", "Parent");
        PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
        return pdfReferenceHolder != (PdfReferenceHolder) null ? pdfReferenceHolder.Object as PdfArray : pdfPrimitive as PdfArray;
      }

      private PdfTemplate GetAnnotTemplate(PdfDictionary annotation)
      {
        PdfDictionary pdfDictionary1 = PdfCrossTable.Dereference(annotation["AP"]) as PdfDictionary;
        PdfTemplate annotTemplate = (PdfTemplate) null;
        if (pdfDictionary1 != null && PdfCrossTable.Dereference(pdfDictionary1["N"]) is PdfDictionary pdfDictionary2)
        {
          if (!(pdfDictionary2 is PdfStream dictionary))
          {
            PdfName key = (PdfName) null;
            if (annotation.ContainsKey("AS"))
            {
              key = PdfCrossTable.Dereference(annotation["AS"]) as PdfName;
            }
            else
            {
              IEnumerator enumerator = pdfDictionary2.Keys.GetEnumerator();
              if (enumerator.MoveNext())
                key = enumerator.Current as PdfName;
            }
            if (key != (PdfName) null)
              dictionary = PdfCrossTable.Dereference(pdfDictionary2[key]) as PdfStream;
          }
          if (dictionary != null)
          {
            PdfDictionary resources = PdfCrossTable.Dereference(dictionary["Resources"]) as PdfDictionary;
            annotTemplate = new PdfTemplate(this.GetElementSize((PdfDictionary) dictionary, "BBox"), dictionary.InternalStream, resources);
          }
        }
        return annotTemplate;
      }

      internal PdfTemplate GetContent()
      {
        this.m_modified = false;
        this.m_layersCount = this.m_layers == null ? 0 : this.m_layers.Count;
        this.m_annotCount = this.GetAnnotationCount();
        MemoryStream stream = new MemoryStream();
        this.Layers.CombineContent((Stream) stream);
        this.m_pageContentLength = stream.Length;
        return new PdfTemplate(this.Origin, this.Size, stream, PdfCrossTable.Dereference(this.Dictionary["Resources"]) as PdfDictionary);
      }

      private PdfArray GetDestination(PdfLoadedDocument ldDoc, PdfDictionary annotation)
      {
        IPdfPrimitive pdfPrimitive = PdfCrossTable.Dereference(annotation["Dest"]);
        PdfName name1 = pdfPrimitive as PdfName;
        PdfString name2 = pdfPrimitive as PdfString;
        PdfArray array = pdfPrimitive as PdfArray;
        if (name1 != (PdfName) null)
          array = ldDoc.GetNamedDestination(name1);
        else if (name2 != null)
          array = ldDoc.GetNamedDestination(name2);
        if (array != null)
          array = new PdfArray(array);
        return array;
      }

      private SizeF GetElementSize(PdfDictionary dictionary, string propertyName)
      {
        if (!(PdfCrossTable.Dereference(dictionary[propertyName]) is PdfArray pdfArray))
          throw new PdfDocumentException("Invalid format: dictionary doesn't contain rectangle array.");
        PdfNumber pdfNumber1 = pdfArray.Count >= 4 ? pdfArray[0] as PdfNumber : throw new PdfDocumentException("Invalid format: rectangle array has less then four elements.");
        PdfNumber pdfNumber2 = pdfArray[1] as PdfNumber;
        PdfNumber pdfNumber3 = pdfArray[2] as PdfNumber;
        PdfNumber pdfNumber4 = pdfArray[3] as PdfNumber;
        return new SizeF(Math.Abs(pdfNumber1.FloatValue - pdfNumber3.FloatValue), Math.Abs(pdfNumber2.FloatValue - pdfNumber4.FloatValue));
      }

      internal System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive> GetFontDictionary(
        PdfDictionary xobjectStream)
      {
        if (xobjectStream == null || !xobjectStream.ContainsKey("Font") || !(xobjectStream["Font"] is PdfDictionary))
          return (System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive>) null;
        PdfDictionary pdfDictionary = xobjectStream["Font"] as PdfDictionary;
        List<IPdfPrimitive> pdfPrimitiveList = new List<IPdfPrimitive>();
        List<PdfName> pdfNameList = new List<PdfName>();
        System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive> items = pdfDictionary.Items;
        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in items)
        {
          pdfNameList.Add(keyValuePair.Key);
          pdfPrimitiveList.Add(keyValuePair.Value);
        }
        return items;
      }

      internal void GetFontStream()
      {
        PdfDictionary dictionary = this.Dictionary;
        if (!this.Dictionary.ContainsKey("Resources"))
          return;
        if (!(this.Dictionary["Resources"] is PdfDictionary pdfDictionary))
        {
          if (pdfDictionary != null)
            return;
          PdfReferenceHolder pdfReferenceHolder = this.Dictionary["Resources"] as PdfReferenceHolder;
          if (!(pdfReferenceHolder != (PdfReferenceHolder) null) || !(pdfReferenceHolder.Object is PdfDictionary))
            return;
          PdfDictionary pdfDictionary1 = pdfReferenceHolder.Object as PdfDictionary;
          if (pdfDictionary1["Font"] is PdfDictionary)
          {
            if (!(pdfDictionary1["Font"] is PdfDictionary pdfDictionary2))
              return;
            this.m_fontNames = new List<PdfName>();
            this.m_fontReference = new List<IPdfPrimitive>();
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary2.Items)
            {
              this.m_fontNames.Add(keyValuePair.Key);
              this.m_fontReference.Add(keyValuePair.Value);
            }
          }
          else
          {
            if ((object) (pdfDictionary1["Font"] as PdfReferenceHolder) == null || !((pdfDictionary1["Font"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary3))
              return;
            this.m_fontNames = new List<PdfName>();
            this.m_fontReference = new List<IPdfPrimitive>();
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary3.Items)
            {
              this.m_fontNames.Add(keyValuePair.Key);
              this.m_fontReference.Add(keyValuePair.Value);
            }
          }
        }
        else
        {
          pdfDictionary6 = (PdfDictionary) null;
          if (pdfDictionary.ContainsKey("Font"))
          {
            pdfDictionary6 = pdfDictionary["Font"] as PdfDictionary;
          }
          else
          {
            PdfDictionary pdfDictionary4 = (PdfDictionary) null;
            IPdfPrimitive xobject = this.GetXObject(this.GetResources());
            while (true)
            {
              if (xobject != null && xobject is PdfDictionary)
              {
                foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in ((PdfDictionary) xobject).Items)
                {
                  if ((object) (keyValuePair.Value as PdfReferenceHolder) != null)
                  {
                    PdfDictionary pdfDictionary5 = (PdfDictionary) (((PdfReferenceHolder) keyValuePair.Value).Object as PdfStream);
                    if (pdfDictionary5.ContainsKey("Resources"))
                      pdfDictionary4 = pdfDictionary5["Resources"] as PdfDictionary;
                  }
                }
              }
              if (pdfDictionary4 != null)
              {
                if (!pdfDictionary4.ContainsKey("Font"))
                {
                  if (xobject != null)
                    xobject = this.GetXObject(new PdfResources(PdfCrossTable.Dereference((IPdfPrimitive) (pdfDictionary4["XObject"] as PdfDictionary)) as PdfDictionary));
                  else
                    goto label_37;
                }
                else
                  break;
              }
              else
                goto label_37;
            }
            pdfDictionary6 = pdfDictionary4["Font"] as PdfDictionary;
          }
    label_37:
          if (pdfDictionary6 != null)
          {
            this.m_fontNames = new List<PdfName>();
            this.m_fontReference = new List<IPdfPrimitive>();
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary6.Items)
            {
              this.m_fontNames.Add(keyValuePair.Key);
              this.m_fontReference.Add(keyValuePair.Value);
            }
          }
          else if ((object) (pdfDictionary["Font"] as PdfReferenceHolder) != null && (pdfDictionary["Font"] as PdfReferenceHolder).Object is PdfDictionary pdfDictionary6)
          {
            this.m_fontNames = new List<PdfName>();
            this.m_fontReference = new List<IPdfPrimitive>();
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary6.Items)
            {
              this.m_fontNames.Add(keyValuePair.Key);
              this.m_fontReference.Add(keyValuePair.Value);
            }
          }
          if (pdfDictionary6 != null || !pdfDictionary.ContainsKey("XObject") || !(pdfDictionary["XObject"] is PdfDictionary pdfDictionary7))
            return;
          foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair1 in pdfDictionary7.Items)
          {
            if ((object) (keyValuePair1.Value as PdfReferenceHolder) != null)
            {
              PdfDictionary pdfDictionary8 = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary8.ContainsKey("Resources"))
              {
                PdfDictionary pdfDictionary9 = pdfDictionary8["Resources"] as PdfDictionary;
                if (pdfDictionary9.ContainsKey("Font"))
                {
                  this.m_fontNames = new List<PdfName>();
                  this.m_fontReference = new List<IPdfPrimitive>();
                  if (!(pdfDictionary9["Font"] is PdfDictionary pdfDictionary10))
                  {
                    PdfReferenceHolder pdfReferenceHolder = pdfDictionary9["Font"] as PdfReferenceHolder;
                    if (pdfReferenceHolder != (PdfReferenceHolder) null)
                      pdfDictionary10 = pdfReferenceHolder.Object as PdfDictionary;
                  }
                  foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair2 in pdfDictionary10.Items)
                  {
                    this.m_fontNames.Add(keyValuePair2.Key);
                    this.m_fontReference.Add(keyValuePair2.Value);
                  }
                }
              }
            }
          }
        }
      }

      private PdfPageOrientation GetOrientation()
      {
        SizeF size = this.Size;
        double width = (double) size.Width;
        size = this.Size;
        double height = (double) size.Height;
        return width <= height ? PdfPageOrientation.Portrait : PdfPageOrientation.Landscape;
      }

      internal virtual PdfResources GetResources()
      {
        if (this.m_resources == null)
        {
          this.m_resources = new PdfResources();
          this.Dictionary["Resources"] = (IPdfPrimitive) this.m_resources;
        }
        return this.m_resources;
      }

      private PdfPageRotateAngle GetRotation()
      {
        int num = 90;
        PdfDictionary pdfDictionary = this.Dictionary;
        PdfNumber pdfNumber;
        for (pdfNumber = (PdfNumber) null; pdfDictionary != null && pdfNumber == null; pdfDictionary = PdfCrossTable.Dereference(pdfDictionary["Parent"]) as PdfDictionary)
          pdfNumber = (object) (pdfDictionary["Rotate"] as PdfReferenceHolder) == null ? (PdfNumber) pdfDictionary["Rotate"] : (PdfNumber) (pdfDictionary["Rotate"] as PdfReferenceHolder).Object;
        if (pdfNumber == null)
          pdfNumber = new PdfNumber(0);
        return (PdfPageRotateAngle) (pdfNumber.IntValue / num);
      }

      private IPdfPrimitive GetXObject(PdfResources resources)
      {
        IPdfPrimitive xobject = (IPdfPrimitive) null;
        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in resources.Items)
        {
          if (keyValuePair.Key.ToString() == "/XObject")
            xobject = PdfCrossTable.Dereference(keyValuePair.Value);
        }
        return xobject;
      }

      private void GetXObject(string[] xobjectElement, PdfPageResources m_pageResources)
      {
        if (!m_pageResources.ContainsKey(this.StripSlashes(xobjectElement[0])) || m_pageResources[this.StripSlashes(xobjectElement[0])] is ImageStructure)
          return;
        PdfRecordCollection recordCollection = (m_pageResources[this.StripSlashes(xobjectElement[0])] as XObjectElement).Render(m_pageResources, this.m_graphicsState);
        PdfDictionary xobjectDictionary = (m_pageResources[this.StripSlashes(xobjectElement[0])] as XObjectElement).XObjectDictionary;
        PdfPageResources pdfPageResources = new PdfPageResources();
        System.Collections.Generic.Dictionary<string, PdfMatrix> commonMatrix = new System.Collections.Generic.Dictionary<string, PdfMatrix>();
        if (xobjectDictionary.ContainsKey("Resources"))
        {
          PdfDictionary pdfDictionary = new PdfDictionary();
          PdfDictionary resourceDictionary = (object) (xobjectDictionary["Resources"] as PdfReferenceHolder) == null ? xobjectDictionary["Resources"] as PdfDictionary : (xobjectDictionary["Resources"] as PdfReferenceHolder).Object as PdfDictionary;
          pdfPageResources = this.resourceLoader.UpdatePageResources(this.resourceLoader.UpdatePageResources(pdfPageResources, this.resourceLoader.GetImageResources(resourceDictionary, this, ref commonMatrix)), this.resourceLoader.GetFontResources(resourceDictionary));
        }
        this.RenderText(recordCollection, pdfPageResources);
      }

      internal void ImportAnnotations(PdfLoadedDocument ldDoc, PdfPageBase page)
      {
        PdfArray annots = page.GetAnnots();
        if (annots == null)
          return;
        PdfArray pdfArray = new PdfArray();
        PdfReferenceHolder primitive = new PdfReferenceHolder((IPdfWrapper) this);
        this.Dictionary["Annots"] = (IPdfPrimitive) pdfArray;
        this.m_modified = true;
        foreach (IPdfPrimitive pdfPrimitive in annots)
        {
          PdfDictionary pdfDictionary = new PdfDictionary(PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary);
          pdfDictionary.SetProperty("P", (IPdfPrimitive) primitive);
          pdfArray.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
        }
      }

      internal void ImportAnnotations(
        PdfLoadedDocument ldDoc,
        PdfPageBase page,
        List<PdfArray> destinations)
      {
        PdfArray annots = page.GetAnnots();
        if (annots == null)
          return;
        PdfArray pdfArray1 = new PdfArray();
        if (this.Dictionary.ContainsKey("Annots"))
          pdfArray1 = this.Dictionary["Annots"] as PdfArray;
        else
          this.Dictionary["Annots"] = (IPdfPrimitive) pdfArray1;
        foreach (IPdfPrimitive pdfPrimitive in annots)
        {
          PdfDictionary dictionary = PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary;
          if (!(this is PdfPage) ? (this as PdfLoadedPage).Document.EnableMemoryOptimization : (this as PdfPage).Section.ParentDocument.EnableMemoryOptimization)
          {
            if (!dictionary.ContainsKey("Subtype") || (dictionary["Subtype"] as PdfName).Value != "Widget")
            {
              this.m_modified = true;
              PdfDictionary annotation = new PdfDictionary(dictionary);
              PdfArray pdfArray2 = (PdfArray) null;
              if (annotation.ContainsKey("Dest"))
                pdfArray2 = this.GetDestination(ldDoc, annotation);
              annotation.Remove("Dest");
              if (annotation.ContainsKey("A") && (object) (annotation["A"] as PdfReferenceHolder) != null)
                ((annotation["A"] as PdfReferenceHolder).Object as PdfDictionary).Remove("AN");
              annotation.Remove("Popup");
              annotation.Remove("P");
              annotation.Remove("Parent");
              PdfCrossTable crossTable = !(this is PdfPage) ? (this as PdfLoadedPage).Document.CrossTable : (this as PdfPage).Section.ParentDocument.CrossTable;
              PdfDictionary pdfDictionary = annotation.Clone(crossTable) as PdfDictionary;
              PdfReferenceHolder primitive = new PdfReferenceHolder((IPdfWrapper) this);
              pdfDictionary.SetProperty("P", (IPdfPrimitive) primitive);
              pdfArray1.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfDictionary));
              if (pdfArray2 != null)
                pdfDictionary["Dest"] = pdfArray2.Clone(crossTable);
            }
          }
          else
          {
            PdfDictionary annotation = new PdfDictionary(dictionary);
            annotation.SetProperty("P", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this));
            pdfArray1.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) annotation));
            if (annotation.ContainsKey("Dest"))
            {
              PdfArray destination = this.GetDestination(ldDoc, annotation);
              if (destination != null)
              {
                destinations.Add(destination);
                annotation["Dest"] = (IPdfPrimitive) destination;
              }
            }
          }
        }
      }

      private PointF NormalizeAnnotationLocation(
        PointF location,
        PdfGraphics graphics,
        PdfTemplate template)
      {
        location.Y = graphics.Size.Height - location.Y - template.Height;
        return location;
      }

      private void RenderFont(string[] fontElements)
      {
        for (int index = 0; index < fontElements.Length; ++index)
        {
          if (fontElements[index].Contains("/"))
          {
            this.m_currentFont = fontElements[index].Replace("/", "");
            break;
          }
        }
      }

      private void RenderText(PdfRecordCollection recordCollection, PdfPageResources m_pageResources)
      {
        if (recordCollection == null)
          return;
        foreach (PdfRecord record in recordCollection)
        {
          string tokenType = record.OperatorName;
          string[] operands = record.Operands;
          foreach (char symbolChar in this.m_symbolChars)
          {
            if (tokenType.Contains(symbolChar.ToString()))
              tokenType = tokenType.Replace(symbolChar.ToString(), "");
          }
          switch (tokenType.Trim())
          {
            case "'":
            case "TJ":
            case "Tj":
              this.resultantText += this.RenderTextElement(operands, tokenType, m_pageResources);
              if (tokenType == "'")
              {
                this.resultantText += "\r\n";
                continue;
              }
              continue;
            case "Do":
              this.GetXObject(operands, m_pageResources);
              continue;
            case "ET":
              this.resultantText += "\r\n";
              continue;
            case "T*":
              this.resultantText += "\r\n";
              continue;
            case "Tf":
              this.RenderFont(operands);
              continue;
            default:
              continue;
          }
        }
      }

      private string RenderTextElement(
        string[] textElements,
        string tokenType,
        PdfPageResources m_pageResources)
      {
        try
        {
          string textToDecode = string.Join("", textElements);
          if (m_pageResources.ContainsKey(this.m_currentFont))
          {
            FontStructure mPageResource = m_pageResources[this.m_currentFont] as FontStructure;
            mPageResource.IsTextExtraction = true;
            textToDecode = mPageResource.DecodeTextExtraction(textToDecode, true);
          }
          return textToDecode;
        }
        catch
        {
          return (string) null;
        }
      }

      public void ReplaceImage(int imageIndex, PdfImage image)
      {
        if (image is PdfMetafile)
          throw new NotSupportedException("Meta file image can't replaced");
        if (imageIndex < 0)
          throw new ArgumentException("Image index is not valid");
        if (image == null)
          throw new NullReferenceException(nameof (image));
        this.m_modified = true;
        try
        {
          PdfImageInfo[] imagesInfo = this.ImagesInfo;
          image.Save();
          PdfReferenceHolder imageReference = new PdfReferenceHolder((IPdfWrapper) image);
          PdfResources resources = this.GetResources();
          if (!resources.ContainsKey("XObject") || !(resources["XObject"] is PdfDictionary))
            return;
          int num1 = 0;
          PdfDictionary primitive = resources["XObject"] as PdfDictionary;
          PdfDictionary dictionary = new PdfDictionary();
          while (primitive != null && primitive != null)
          {
            System.Collections.Generic.Dictionary<PdfName, IPdfPrimitive> items = primitive.Items;
            if (imageIndex >= items.Count)
              throw new ArgumentException("Image Index is not valid");
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair1 in items)
            {
              bool flag = false;
              PdfDictionary pdfDictionary1 = (object) (keyValuePair1.Value as PdfReferenceHolder) == null ? keyValuePair1.Value as PdfDictionary : (keyValuePair1.Value as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary1.ContainsKey("Subtype") && (pdfDictionary1["Subtype"] as PdfName).Value == "Image")
              {
                IPdfPrimitive pdfPrimitive = resources["XObject"];
                if (num1 == imageIndex)
                {
                  string str = keyValuePair1.Key.ToString();
                  foreach (PdfImageInfo pdfImageInfo in imagesInfo)
                  {
                    str = this.StripSlashes(str);
                    if (pdfImageInfo.Name == str)
                    {
                      flag = true;
                      if (pdfImageInfo.Image != null)
                      {
                        long objNum = (primitive[str] as PdfReferenceHolder).Reference.ObjNum;
                        int num2 = 0;
                        foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair2 in dictionary.Items)
                        {
                          if (num2 == dictionary.Count - 1)
                            primitive = dictionary[keyValuePair2.Key.Value] as PdfDictionary;
                          ++num2;
                        }
                        dictionary.Clear();
                        if ((keyValuePair1.Value as PdfReferenceHolder).Object is PdfStream)
                        {
                          PdfStream pdfStream = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfStream;
                          switch (this)
                          {
                            case PdfPage _:
                              if ((this as PdfPage).Document.FileStructure.IncrementalUpdate)
                              {
                                pdfStream.Modify();
                                pdfStream.Clear();
                                break;
                              }
                              pdfStream.Clear();
                              break;
                            case PdfLoadedPage _:
                              if ((this as PdfLoadedPage).Document.FileStructure.IncrementalUpdate)
                              {
                                pdfStream.Modify();
                                pdfStream.Clear();
                                break;
                              }
                              pdfStream.Clear();
                              break;
                          }
                        }
                        primitive.Items.Remove(keyValuePair1.Key);
                        float height = (float) pdfImageInfo.Image.Height;
                        if ((double) this.Size.Height - ((double) pdfImageInfo.Bounds.Top + (double) height) >= 0.0)
                        {
                          primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                          primitive.Modify();
                          break;
                        }
                        primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                        primitive.Modify();
                        if (this is PdfLoadedPage)
                        {
                          int num3 = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.IndexOf(this);
                          int count = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count;
                          if (num3 < ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count - 1)
                          {
                            if (!this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num3 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum) && num3 > 0)
                            {
                              this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num3 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum);
                              break;
                            }
                            break;
                          }
                          if (num3 > 0 && !this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num3 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum) && num3 < count - 1)
                          {
                            this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num3 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum);
                            break;
                          }
                          break;
                        }
                        break;
                      }
                      if ((keyValuePair1.Value as PdfReferenceHolder).Object is PdfStream)
                      {
                        PdfStream pdfStream = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfStream;
                        switch (this)
                        {
                          case PdfPage _:
                            if ((this as PdfPage).Document.FileStructure.IncrementalUpdate)
                            {
                              pdfStream.Modify();
                              pdfStream.Clear();
                              break;
                            }
                            pdfStream.Clear();
                            break;
                          case PdfLoadedPage _:
                            if ((this as PdfLoadedPage).Document.FileStructure.IncrementalUpdate)
                            {
                              pdfStream.Modify();
                              pdfStream.Clear();
                              break;
                            }
                            pdfStream.Clear();
                            break;
                        }
                      }
                      long objNum1 = (keyValuePair1.Value as PdfReferenceHolder).Reference.ObjNum;
                      primitive.Items.Remove(keyValuePair1.Key);
                      float height1 = pdfImageInfo.Bounds.Height;
                      if ((double) this.Size.Height - ((double) pdfImageInfo.Bounds.Top + (double) height1) >= 0.0)
                      {
                        primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                        primitive.Modify();
                        break;
                      }
                      primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                      primitive.Modify();
                      if (this is PdfLoadedPage)
                      {
                        int num4 = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.IndexOf(this);
                        int count = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count;
                        if (num4 < ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count - 1)
                        {
                          if (!this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num4 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1) && num4 > 0)
                          {
                            this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num4 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1);
                            break;
                          }
                          break;
                        }
                        if (num4 > 0 && !this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num4 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1) && num4 < count - 1)
                        {
                          this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num4 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1);
                          break;
                        }
                        break;
                      }
                      break;
                    }
                  }
                }
                else
                  ++num1;
              }
              if (pdfDictionary1.ContainsKey("Resources"))
              {
                PdfDictionary pdfDictionary2 = (object) (pdfDictionary1["Resources"] as PdfReferenceHolder) == null ? pdfDictionary1["Resources"] as PdfDictionary : (pdfDictionary1["Resources"] as PdfReferenceHolder).Object as PdfDictionary;
                if (pdfDictionary2.ContainsKey("XObject"))
                {
                  primitive = pdfDictionary2["XObject"] as PdfDictionary;
                  PdfDictionary.SetProperty(dictionary, keyValuePair1.Key.Value, (IPdfPrimitive) primitive);
                }
              }
              else if (flag)
              {
                primitive = (PdfDictionary) null;
                break;
              }
            }
          }
        }
        catch (Exception ex)
        {
          if (ex is ArgumentException)
            throw ex;
        }
      }

      internal void ReplaceImageByName(string imgName, PdfImage image)
      {
        if (image is PdfMetafile)
          throw new NotSupportedException("Meta file image can't replaced");
        if (image == null)
          throw new NullReferenceException(nameof (image));
        this.m_modified = true;
        try
        {
          PdfImageInfo[] imagesInfo = this.ImagesInfo;
          image.Save();
          PdfReferenceHolder imageReference = new PdfReferenceHolder((IPdfWrapper) image);
          PdfResources resources = this.GetResources();
          if (!resources.ContainsKey("XObject"))
            return;
          PdfDictionary pdfDictionary1 = (PdfDictionary) null;
          if (resources["XObject"] is PdfDictionary)
            pdfDictionary1 = resources["XObject"] as PdfDictionary;
          else if ((object) (resources["XObject"] as PdfReferenceHolder) != null)
            pdfDictionary1 = (resources["XObject"] as PdfReferenceHolder).Object as PdfDictionary;
          if (pdfDictionary1 == null)
            return;
          PdfDictionary primitive = pdfDictionary1;
          PdfDictionary dictionary = new PdfDictionary();
          while (primitive != null && primitive != null)
          {
            foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair1 in primitive.Items)
            {
              bool flag = false;
              PdfDictionary pdfDictionary2 = (object) (keyValuePair1.Value as PdfReferenceHolder) == null ? keyValuePair1.Value as PdfDictionary : (keyValuePair1.Value as PdfReferenceHolder).Object as PdfDictionary;
              if (pdfDictionary2.ContainsKey("Subtype") && (pdfDictionary2["Subtype"] as PdfName).Value == "Image")
              {
                IPdfPrimitive pdfPrimitive = resources["XObject"];
                string str = keyValuePair1.Key.ToString();
                foreach (PdfImageInfo pdfImageInfo in imagesInfo)
                {
                  str = this.StripSlashes(str);
                  if (pdfImageInfo.Name == str && str == imgName)
                  {
                    flag = true;
                    if (pdfImageInfo.Image != null)
                    {
                      long objNum = (primitive[str] as PdfReferenceHolder).Reference.ObjNum;
                      int num1 = 0;
                      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair2 in dictionary.Items)
                      {
                        if (num1 == dictionary.Count - 1)
                          primitive = dictionary[keyValuePair2.Key.Value] as PdfDictionary;
                        ++num1;
                      }
                      dictionary.Clear();
                      if ((keyValuePair1.Value as PdfReferenceHolder).Object is PdfStream)
                      {
                        PdfStream pdfStream = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfStream;
                        switch (this)
                        {
                          case PdfPage _:
                            if ((this as PdfPage).Document.FileStructure.IncrementalUpdate)
                            {
                              pdfStream.Modify();
                              pdfStream.Clear();
                              break;
                            }
                            pdfStream.Clear();
                            break;
                          case PdfLoadedPage _:
                            if ((this as PdfLoadedPage).Document.FileStructure.IncrementalUpdate)
                            {
                              pdfStream.Modify();
                              pdfStream.Clear();
                              break;
                            }
                            pdfStream.Clear();
                            break;
                        }
                      }
                      primitive.Items.Remove(keyValuePair1.Key);
                      float height = (float) pdfImageInfo.Image.Height;
                      if ((double) this.Size.Height - ((double) pdfImageInfo.Bounds.Top + (double) height) >= 0.0)
                      {
                        primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                        primitive.Modify();
                        break;
                      }
                      primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                      primitive.Modify();
                      if (this is PdfLoadedPage)
                      {
                        int num2 = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.IndexOf(this);
                        int count = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count;
                        if (num2 < ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count - 1)
                        {
                          if (!this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num2 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum) && num2 > 0)
                          {
                            this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num2 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum);
                            break;
                          }
                          break;
                        }
                        if (num2 > 0 && !this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num2 - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum) && num2 < count - 1)
                        {
                          this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num2 + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum);
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    if ((keyValuePair1.Value as PdfReferenceHolder).Object is PdfStream)
                    {
                      PdfStream pdfStream = (keyValuePair1.Value as PdfReferenceHolder).Object as PdfStream;
                      switch (this)
                      {
                        case PdfPage _:
                          if ((this as PdfPage).Document.FileStructure.IncrementalUpdate)
                          {
                            pdfStream.Modify();
                            pdfStream.Clear();
                            break;
                          }
                          pdfStream.Clear();
                          break;
                        case PdfLoadedPage _:
                          if ((this as PdfLoadedPage).Document.FileStructure.IncrementalUpdate)
                          {
                            pdfStream.Modify();
                            pdfStream.Clear();
                            break;
                          }
                          pdfStream.Clear();
                          break;
                      }
                    }
                    long objNum1 = (keyValuePair1.Value as PdfReferenceHolder).Reference.ObjNum;
                    primitive.Items.Remove(keyValuePair1.Key);
                    float height1 = pdfImageInfo.Bounds.Height;
                    if ((double) this.Size.Height - ((double) pdfImageInfo.Bounds.Top + (double) height1) >= 0.0)
                    {
                      primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                      primitive.Modify();
                      break;
                    }
                    primitive.Items.Add(keyValuePair1.Key, (IPdfPrimitive) imageReference);
                    primitive.Modify();
                    if (this is PdfLoadedPage)
                    {
                      int num = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.IndexOf(this);
                      int count = ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count;
                      if (num < ((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages.Count - 1)
                      {
                        if (!this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1) && num > 0)
                        {
                          this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1);
                          break;
                        }
                        break;
                      }
                      if (num > 0 && !this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num - 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1) && num < count - 1)
                      {
                        this.ReplacePaginatedImage(((this as PdfLoadedPage).Document as PdfLoadedDocument).Pages[num + 1] as PdfLoadedPage, keyValuePair1.Key.ToString(), imageReference, objNum1);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                }
              }
              if (pdfDictionary2.ContainsKey("Resources"))
              {
                PdfDictionary pdfDictionary3 = (object) (pdfDictionary2["Resources"] as PdfReferenceHolder) == null ? pdfDictionary2["Resources"] as PdfDictionary : (pdfDictionary2["Resources"] as PdfReferenceHolder).Object as PdfDictionary;
                if (pdfDictionary3.ContainsKey("XObject"))
                {
                  primitive = pdfDictionary3["XObject"] as PdfDictionary;
                  PdfDictionary.SetProperty(dictionary, keyValuePair1.Key.Value, (IPdfPrimitive) primitive);
                }
              }
              else if (flag)
              {
                primitive = (PdfDictionary) null;
                break;
              }
            }
          }
        }
        catch (Exception ex)
        {
          if (ex is ArgumentException)
            throw ex;
        }
      }

      private bool ReplacePaginatedImage(
        PdfLoadedPage page,
        string name,
        PdfReferenceHolder imageReference,
        long objIndex)
      {
        this.m_modified = true;
        PdfResources resources = page.GetResources();
        if (resources.ContainsKey("XObject") && resources["XObject"] is PdfDictionary)
        {
          PdfDictionary pdfDictionary = resources["XObject"] as PdfDictionary;
          foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary.Items)
          {
            if ((keyValuePair.Value as PdfReferenceHolder).Reference.ObjNum == objIndex)
            {
              pdfDictionary.Items.Remove(keyValuePair.Key);
              pdfDictionary.Items.Add(keyValuePair.Key, (IPdfPrimitive) imageReference);
              pdfDictionary.Modify();
              return true;
            }
          }
        }
        return false;
      }

      internal void SetResources(PdfResources res)
      {
        this.m_resources = res;
        this.m_modified = true;
      }

      private string SkipEscapeSequence(string text)
      {
        int startIndex = -1;
        do
        {
          startIndex = text.IndexOf("\\", startIndex + 1);
          if (text.Length > startIndex + 1)
          {
            string str = text[startIndex + 1].ToString();
            if (startIndex >= 0 && (str == "\\" || str == "(" || str == ")"))
              text = text.Remove(startIndex, 1);
          }
          else
          {
            text = text.Remove(startIndex, 1);
            startIndex = -1;
          }
        }
        while (startIndex >= 0);
        return text;
      }

      private string StripSlashes(string text) => text.Replace("/", "");

      public PdfLoadedAnnotationCollection Annotations
      {
        get
        {
          if (this.m_annotations == null || this.m_annotations.Annotations.Count == 0)
            this.m_annotations = new PdfLoadedAnnotationCollection(this as PdfLoadedPage);
          return this.m_annotations;
        }
      }

      internal PdfArray Contents
      {
        get
        {
          IPdfPrimitive page = this.m_pageDictionary[nameof (Contents)];
          contents = page as PdfArray;
          PdfReferenceHolder pdfReferenceHolder = page as PdfReferenceHolder;
          if (pdfReferenceHolder != (PdfReferenceHolder) null && !(pdfReferenceHolder.Object is PdfArray contents) && pdfReferenceHolder.Object is PdfStream pdfStream)
          {
            contents = new PdfArray();
            contents.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream));
            this.m_pageDictionary[nameof (Contents)] = (IPdfPrimitive) contents;
          }
          if (contents == null)
          {
            contents = new PdfArray();
            this.m_pageDictionary[nameof (Contents)] = (IPdfPrimitive) contents;
          }
          return contents;
        }
      }

      internal PdfTemplate ContentTemplate
      {
        get
        {
          bool flag = false;
          using (MemoryStream memoryStream = new MemoryStream())
          {
            this.Layers.CombineContent((Stream) memoryStream);
            if (this.m_pageContentLength != memoryStream.Length)
              flag = true;
          }
          if (this.m_contentTemplate == null || this.m_contentTemplate.m_content.Data.Length == 0 || this.m_layersCount != (this.m_layers == null ? 0 : this.m_layers.Count) || this.m_annotCount != this.GetAnnotationCount())
            flag = true;
          if (this.m_modified | flag)
            this.m_contentTemplate = this.GetContent();
          return this.m_contentTemplate;
        }
      }

      public PdfPageLayer DefaultLayer => this.Layers[this.DefaultLayerIndex];

      public int DefaultLayerIndex
      {
        get
        {
          if (this.Layers.Count == 0 || this.m_defLayerIndex == -1)
            this.m_defLayerIndex = this.Layers.IndexOf(this.Layers.Add());
          return this.m_defLayerIndex;
        }
        set
        {
          if (value < 0 || value > this.Layers.Count - 1)
            throw new ArgumentOutOfRangeException(nameof (value), "Index can not be less 0 and greater Layers.Count - 1");
          this.m_defLayerIndex = value;
          this.m_modified = true;
        }
      }

      internal PdfDictionary Dictionary => this.m_pageDictionary;

      public PdfGraphics Graphics => this.DefaultLayer.Graphics;

      public PdfImageInfo[] ImagesInfo
      {
        get
        {
          if (this.m_imageinfo == null)
          {
            try
            {
              Image[] images = this.ExtractImages(true);
              int index1 = 0;
              int index2 = 0;
              IEnumerator enumerator = (IEnumerator) this.m_extractedImagesBounds.GetEnumerator();
              while (enumerator.MoveNext())
              {
                if (!this.m_imageinfo[index1].IsImageExtracted)
                {
                  this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
                  this.m_imageinfo[index1].Image = (Image) null;
                  this.m_imageinfo[index1].Index = index1;
                  ++index1;
                }
                else if (this.m_imageinfo[index1].IsImageExtracted)
                {
                  Image image = images[index2];
                  this.m_imageinfo[index1].Bounds = (RectangleF) enumerator.Current;
                  this.m_imageinfo[index1].Image = image;
                  this.m_imageinfo[index1].Index = index1;
                  ++index1;
                  ++index2;
                }
              }
            }
            catch (Exception ex)
            {
            }
          }
          return this.m_imageinfo;
        }
      }

      internal bool Imported
      {
        get => this.m_imported;
        set => this.m_imported = value;
      }

      public PdfPageLayerCollection Layers
      {
        get
        {
          if (this.m_layers == null)
            this.m_layers = new PdfPageLayerCollection(this);
          return this.m_layers;
        }
      }

      internal PdfPageOrientation Orientation => this.GetOrientation();

      internal abstract PointF Origin { get; }

      public PdfPageRotateAngle Rotation => this.GetRotation();

      public abstract SizeF Size { get; }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_pageDictionary;
    }
}
