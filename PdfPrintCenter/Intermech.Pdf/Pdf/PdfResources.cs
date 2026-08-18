// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfResources
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.ColorSpace;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class PdfResources : PdfDictionary
{
  private Dictionary<IPdfPrimitive, PdfName> m_names;
  private PdfDictionary m_properties;

  internal PdfResources() => this.m_properties = new PdfDictionary();

  internal PdfResources(PdfDictionary baseDictionary)
    : base(baseDictionary)
  {
    this.m_properties = new PdfDictionary();
  }

  internal void Add(PdfColorSpaces color, PdfName name)
  {
    IPdfPrimitive pdfPrimitive = this["ColorSpace"];
    PdfDictionary pdfDictionary;
    if (pdfPrimitive != null)
    {
      PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
      pdfDictionary = pdfPrimitive as PdfDictionary;
      if (pdfReferenceHolder != (PdfReferenceHolder) null)
        pdfDictionary = PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary;
    }
    else
    {
      pdfDictionary = new PdfDictionary();
      this["ColorSpace"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(((IPdfWrapper) color).Element);
  }

  private void Add(PdfBrush brush, PdfName name)
  {
    IPdfPrimitive element = (brush as IPdfWrapper).Element;
    if (element == null)
      return;
    if (!(this["Pattern"] is PdfDictionary pdfDictionary))
    {
      pdfDictionary = new PdfDictionary();
      this["Pattern"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(element);
  }

  internal void Add(PdfFont font, PdfName name)
  {
    IPdfPrimitive pdfPrimitive = this["Font"];
    PdfDictionary pdfDictionary;
    if (pdfPrimitive != null)
    {
      PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
      pdfDictionary = pdfPrimitive as PdfDictionary;
      if (pdfReferenceHolder != (PdfReferenceHolder) null)
        pdfDictionary = PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary;
    }
    else
    {
      pdfDictionary = new PdfDictionary();
      this["Font"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(((IPdfWrapper) font).Element);
  }

  private void Add(PdfImage image, PdfName name)
  {
    PdfDictionary pdfDictionary1 = this["XObject"] as PdfDictionary;
    PdfReferenceHolder pdfReferenceHolder = this["XObject"] as PdfReferenceHolder;
    PdfDictionary pdfDictionary2 = new PdfDictionary();
    if (pdfDictionary1 == null && pdfReferenceHolder != (PdfReferenceHolder) null)
      pdfDictionary2 = pdfReferenceHolder.Object as PdfDictionary;
    if (pdfDictionary1 == null)
    {
      pdfDictionary1 = new PdfDictionary();
      this["XObject"] = (IPdfPrimitive) pdfDictionary1;
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary2.Items)
        pdfDictionary1[keyValuePair.Key] = (IPdfPrimitive) (keyValuePair.Value as PdfReferenceHolder);
    }
    pdfDictionary1[name] = (IPdfPrimitive) new PdfReferenceHolder(((IPdfWrapper) image).Element);
  }

  private void Add(PdfTemplate template, PdfName name)
  {
    PdfDictionary pdfDictionary = (object) (this["XObject"] as PdfReferenceHolder) == null ? this["XObject"] as PdfDictionary : (this["XObject"] as PdfReferenceHolder).Object as PdfDictionary;
    if (pdfDictionary == null)
    {
      pdfDictionary = new PdfDictionary();
      this["XObject"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(((IPdfWrapper) template).Element);
  }

  private void Add(PdfTransparency transparancy, PdfName name)
  {
    IPdfPrimitive element = ((IPdfWrapper) transparancy).Element;
    if (element == null)
      return;
    PdfDictionary pdfDictionary = (PdfDictionary) null;
    if (this["ExtGState"] is PdfDictionary)
      pdfDictionary = this["ExtGState"] as PdfDictionary;
    else if ((object) (this["ExtGState"] as PdfReferenceHolder) != null)
      pdfDictionary = (this["ExtGState"] as PdfReferenceHolder).Object as PdfDictionary;
    if (pdfDictionary == null)
    {
      pdfDictionary = new PdfDictionary();
      this["ExtGState"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(element);
  }

  private void Add(IPdfWrapper obj, PdfName name)
  {
    switch (obj)
    {
      case PdfFont font:
        this.Add(font, name);
        break;
      case PdfTemplate template:
        this.Add(template, name);
        break;
      case PdfImage image:
        this.Add(image, name);
        break;
      case PdfBrush brush:
        this.Add(brush, name);
        break;
      case PdfTransparency transparancy:
        this.Add(transparancy, name);
        break;
      case PdfColorSpaces color:
        this.Add(color, name);
        break;
      case PdfDictionary _:
        this.Add(color, name);
        break;
    }
  }

  internal void Add(PdfDictionary color, PdfName name)
  {
    IPdfPrimitive pdfPrimitive = this["ColorSpace"];
    PdfDictionary pdfDictionary;
    if (pdfPrimitive != null)
    {
      PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
      pdfDictionary = pdfPrimitive as PdfDictionary;
      if (pdfReferenceHolder != (PdfReferenceHolder) null)
        pdfDictionary = PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary;
    }
    else
    {
      pdfDictionary = new PdfDictionary();
      this["ColorSpace"] = (IPdfPrimitive) pdfDictionary;
    }
    pdfDictionary[name] = (IPdfPrimitive) new PdfReferenceHolder(((IPdfWrapper) color).Element);
  }

  internal void AddProperties(string layerid, PdfReferenceHolder reff)
  {
    this.m_properties[layerid] = (IPdfPrimitive) reff;
    this["Properties"] = (IPdfPrimitive) this.m_properties;
  }

  private string GenerateName() => Guid.NewGuid().ToString();

  internal PdfName GetName(IPdfWrapper obj)
  {
    IPdfPrimitive key = obj != null ? obj.Element : throw new ArgumentNullException(nameof (obj));
    PdfName name = (PdfName) null;
    if (this.Names.ContainsKey(key))
      name = this.Names[key];
    if (name == (PdfName) null)
    {
      name = new PdfName(this.GenerateName());
      this.Names[key] = name;
      this.Add(obj, name);
    }
    return name;
  }

  internal Dictionary<IPdfPrimitive, PdfName> GetNames()
  {
    if (this.m_names == null)
    {
      this.m_names = new Dictionary<IPdfPrimitive, PdfName>();
      IPdfPrimitive pdfPrimitive = this["Font"];
      if (pdfPrimitive != null)
      {
        PdfReferenceHolder pdfReferenceHolder = pdfPrimitive as PdfReferenceHolder;
        PdfDictionary pdfDictionary = pdfPrimitive as PdfDictionary;
        if (pdfReferenceHolder != (PdfReferenceHolder) null)
          pdfDictionary = PdfCrossTable.Dereference(pdfPrimitive) as PdfDictionary;
        if (pdfDictionary != null)
        {
          foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in pdfDictionary.Items)
            this.m_names[PdfCrossTable.Dereference(keyValuePair.Value)] = keyValuePair.Key;
        }
      }
    }
    return this.m_names;
  }

  internal void RemoveFont(string name)
  {
    IPdfPrimitive key = (IPdfPrimitive) null;
    foreach (KeyValuePair<IPdfPrimitive, PdfName> name1 in this.m_names)
    {
      if (name1.Value == new PdfName(name))
      {
        key = name1.Key;
        break;
      }
    }
    if (key == null)
      return;
    this.m_names.Remove(key);
  }

  internal void RequireProcSet(string procSetName)
  {
    if (procSetName == null)
      throw new ArgumentNullException(nameof (procSetName));
    if (!(this["ProcSet"] is PdfArray pdfArray))
    {
      pdfArray = new PdfArray();
      this["ProcSet"] = (IPdfPrimitive) pdfArray;
    }
    PdfName element = new PdfName(procSetName);
    if (pdfArray.Contains((IPdfPrimitive) element))
      return;
    pdfArray.Add((IPdfPrimitive) element);
  }

  private Dictionary<IPdfPrimitive, PdfName> Names => this.GetNames();
}
