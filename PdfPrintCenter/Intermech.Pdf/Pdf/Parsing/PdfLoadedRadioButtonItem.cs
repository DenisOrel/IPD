// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedRadioButtonItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedRadioButtonItem : PdfLoadedStateItem
{
  internal PdfLoadedRadioButtonItem(
    PdfLoadedStyledField field,
    int index,
    PdfDictionary dictionary)
    : base(field, index, dictionary)
  {
  }

  private string GetItemValue()
  {
    string empty = string.Empty;
    if (this.Dictionary.ContainsKey("AS"))
    {
      PdfName pdfName = this.CrossTable.GetObject(this.Dictionary["AS"]) as PdfName;
      if (pdfName != (PdfName) null && pdfName.Value != "Off")
        empty = pdfName.Value;
    }
    if (empty == string.Empty && this.Dictionary.ContainsKey("AP"))
    {
      PdfDictionary pdfDictionary1 = this.CrossTable.GetObject(this.Dictionary["AP"]) as PdfDictionary;
      if (!pdfDictionary1.ContainsKey("N"))
        return empty;
      PdfDictionary pdfDictionary2 = this.CrossTable.GetObject((IPdfPrimitive) this.CrossTable.GetReference(pdfDictionary1["N"])) as PdfDictionary;
      List<object> objectList = new List<object>();
      foreach (PdfName key in (IEnumerable) pdfDictionary2.Keys)
        objectList.Add((object) key);
      int index = 0;
      for (int count = objectList.Count; index < count; ++index)
      {
        PdfName pdfName = objectList[index] as PdfName;
        if (pdfName.Value != "Off")
          return pdfName.Value;
      }
    }
    return empty;
  }

  private void SetItemValue(string value)
  {
    string str = value;
    if (this.Dictionary.ContainsKey("AP"))
    {
      PdfDictionary pdfDictionary1 = this.CrossTable.GetObject(this.Dictionary["AP"]) as PdfDictionary;
      if (pdfDictionary1.ContainsKey("N"))
      {
        PdfDictionary pdfDictionary2 = this.CrossTable.GetObject((IPdfPrimitive) this.CrossTable.GetReference(pdfDictionary1["N"])) as PdfDictionary;
        string itemValue = this.GetItemValue();
        if (pdfDictionary2.ContainsKey(itemValue))
        {
          PdfReference reference = this.CrossTable.GetReference(pdfDictionary2[itemValue]);
          pdfDictionary2.Remove(this.Value);
          pdfDictionary2.SetProperty(str, (IPdfPrimitive) new PdfReferenceHolder(reference, this.CrossTable));
        }
      }
    }
    if (str == this.Parent.SelectedValue)
      this.Dictionary.SetName("AS", str);
    else
      this.Dictionary.SetName("AS", "Off");
  }

  internal PdfLoadedRadioButtonListField Parent => base.Parent as PdfLoadedRadioButtonListField;

  public bool Selected
  {
    get => this.Parent.Items.IndexOf(this) == this.Parent.SelectedIndex;
    set
    {
      if (!value)
        return;
      this.Parent.SelectedIndex = this.Parent.Items.IndexOf(this);
    }
  }

  public string Value
  {
    get => this.GetItemValue();
    set => this.SetItemValue(value);
  }
}
