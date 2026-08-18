// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAttachmentCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfAttachmentCollection : PdfCollection, IPdfWrapper
{
  private int count;
  private Dictionary<string, PdfReferenceHolder> dic;
  private PdfArray m_array;
  internal PdfCrossTable m_CrossTable;
  private PdfDictionary m_dictionary;
  private System.Collections.Generic.List<string> orderList;

  public PdfAttachmentCollection()
  {
    this.m_array = new PdfArray();
    this.m_dictionary = new PdfDictionary();
    this.dic = new Dictionary<string, PdfReferenceHolder>();
    this.m_dictionary.SetProperty("Names", (IPdfPrimitive) this.m_array);
  }

  internal PdfAttachmentCollection(PdfDictionary attachmentDictionary, PdfCrossTable table)
  {
    this.m_array = new PdfArray();
    this.m_dictionary = new PdfDictionary();
    this.dic = new Dictionary<string, PdfReferenceHolder>();
    this.m_dictionary = attachmentDictionary;
    this.m_CrossTable = table;
    PdfReferenceHolder pdfReferenceHolder1 = this.m_dictionary["EmbeddedFiles"] as PdfReferenceHolder;
    if (!(pdfReferenceHolder1 != (PdfReferenceHolder) null))
      return;
    this.m_array = (pdfReferenceHolder1.Object as PdfDictionary)["Names"] as PdfArray;
    if (this.m_array == null || this.m_array.Count == 0)
      return;
    int index1 = 1;
    for (int index2 = 0; index2 < this.m_array.Count / 2; ++index2)
    {
      if ((object) (this.m_array[index1] as PdfReferenceHolder) != null)
      {
        PdfDictionary pdfDictionary = (this.m_array[index1] as PdfReferenceHolder).Object as PdfDictionary;
        PdfStream pdfStream = new PdfStream();
        if (pdfDictionary.ContainsKey("EF"))
        {
          PdfReferenceHolder pdfReferenceHolder2 = (pdfDictionary["EF"] as PdfDictionary)["F"] as PdfReferenceHolder;
          if (pdfReferenceHolder2 != (PdfReferenceHolder) null)
            pdfStream = pdfReferenceHolder2.Object as PdfStream;
        }
        PdfAttachment pdfAttachment;
        if (pdfStream != null)
        {
          pdfStream.Decompress();
          if (pdfDictionary.ContainsKey("F"))
          {
            pdfAttachment = new PdfAttachment((pdfDictionary["F"] as PdfString).Value, pdfStream.Data);
            if (pdfDictionary.ContainsKey("Desc"))
              pdfAttachment.Description = (pdfDictionary["Desc"] as PdfString).Value;
            if (pdfDictionary.ContainsKey("CI") && pdfDictionary["CI"] is PdfDictionary dictionary)
            {
              PdfPortfolioAttributes portfolioAttributes = new PdfPortfolioAttributes(dictionary);
              pdfAttachment.PortfolioAttributes = portfolioAttributes;
            }
          }
          else
            pdfAttachment = new PdfAttachment((pdfDictionary["Desc"] as PdfString).Value, pdfStream.Data);
        }
        else
          pdfAttachment = !pdfDictionary.ContainsKey("Desc") ? new PdfAttachment((pdfDictionary["F"] as PdfString).Value) : new PdfAttachment((pdfDictionary["Desc"] as PdfString).Value);
        this.List.Add((object) pdfAttachment);
      }
      index1 += 2;
    }
  }

  public int Add(PdfAttachment attachment)
  {
    int num = attachment != null ? this.DoAdd(attachment) : throw new ArgumentNullException(nameof (attachment));
    this.m_dictionary.Modify();
    return num;
  }

  public void Clear() => this.DoClear();

  public bool Contains(PdfAttachment attachment)
  {
    return attachment != null ? this.List.Contains((object) attachment) : throw new ArgumentNullException(nameof (attachment));
  }

  private int DoAdd(PdfAttachment attachment)
  {
    string fileName = attachment.FileName;
    string key = !PdfString.IsUnicode(fileName) ? fileName : Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(fileName)));
    System.StringComparer ordinal = System.StringComparer.Ordinal;
    if (this.dic.Count == 0 && this.m_array.Count > 0)
    {
      for (int index = 0; index < this.m_array.Count; index += 2)
      {
        if (!this.dic.ContainsKey((this.m_array[index] as PdfString).Value))
          this.dic.Add((this.m_array[index] as PdfString).Value, this.m_array[index + 1] as PdfReferenceHolder);
        else
          this.dic.Add((this.m_array[index] as PdfString).Value + "_copy", this.m_array[index + 1] as PdfReferenceHolder);
      }
    }
    if (!this.dic.ContainsKey(key))
      this.dic.Add(key, new PdfReferenceHolder((IPdfWrapper) attachment));
    else
      this.dic.Add(key + "_copy", new PdfReferenceHolder((IPdfWrapper) attachment));
    this.orderList = new System.Collections.Generic.List<string>((IEnumerable<string>) this.dic.Keys);
    this.orderList.Sort((IComparer<string>) ordinal);
    this.m_array.Clear();
    foreach (string order in this.orderList)
    {
      this.m_array.Add((IPdfPrimitive) new PdfString(order));
      this.m_array.Add((IPdfPrimitive) this.dic[order]);
    }
    return this.List.Add((object) attachment);
  }

  private void DoClear()
  {
    PdfMainObjectCollection pdfObjects = this.m_CrossTable.Document.PdfObjects;
    this.List.Clear();
    for (int index = 1; index < this.m_array.Count; index += 2)
    {
      if ((this.m_array[index] as PdfReferenceHolder).Object is PdfDictionary element1)
      {
        if (element1.ContainsKey("EF") && element1["EF"] is PdfDictionary pdfDictionary && pdfDictionary.ContainsKey("F") && (object) (pdfDictionary["F"] as PdfReferenceHolder) != null && (pdfDictionary["F"] as PdfReferenceHolder).Object is PdfStream element)
          pdfObjects.Remove(pdfObjects.IndexOf((IPdfPrimitive) element));
        pdfObjects.Remove(pdfObjects.IndexOf((IPdfPrimitive) element1));
      }
    }
    this.m_array.Clear();
  }

  private void DoInsert(int index, PdfAttachment attachment)
  {
    this.m_array.Insert(2 * index, (IPdfPrimitive) new PdfString(attachment.FileName));
    this.m_array.Insert(2 * index + 1, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) attachment));
    this.List.Insert(index, (object) attachment);
  }

  private void DoRemove(PdfAttachment attachment)
  {
    int num = this.List.IndexOf((object) attachment);
    this.m_array.RemoveAt(2 * num);
    this.m_array.RemoveAt(2 * num);
    this.List.Remove((object) attachment);
  }

  private void DoRemoveAt(int index)
  {
    this.m_array.RemoveAt(2 * index);
    this.m_array.RemoveAt(2 * index);
    this.List.RemoveAt(index);
  }

  public int IndexOf(PdfAttachment attachment)
  {
    return attachment != null ? this.List.IndexOf((object) attachment) : throw new ArgumentNullException(nameof (attachment));
  }

  public void Insert(int index, PdfAttachment attachment)
  {
    if (attachment == null)
      throw new ArgumentNullException(nameof (attachment));
    this.DoInsert(index, attachment);
  }

  public void Remove(PdfAttachment attachment)
  {
    if (attachment == null)
      throw new ArgumentNullException(nameof (attachment));
    this.DoRemove(attachment);
  }

  public void RemoveAt(int index) => this.DoRemoveAt(index);

  public PdfAttachment this[int index] => (PdfAttachment) this.List[index];

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
}
