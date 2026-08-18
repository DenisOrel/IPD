// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedChoiceField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedChoiceField : PdfLoadedStyledField
    {
      internal PdfLoadedChoiceField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable)
      {
      }

      internal PdfLoadedListItemCollection GetListItemCollection()
      {
        PdfLoadedListItemCollection listItemCollection = new PdfLoadedListItemCollection(this);
        PdfArray pdfArray1 = PdfLoadedField.GetValue(this.Dictionary, this.CrossTable, "Opt", true) as PdfArray;
        int index = 0;
        for (int count = pdfArray1.Count; index < count; ++index)
        {
          IPdfPrimitive pdfPrimitive = this.CrossTable.GetObject(pdfArray1[index]);
          PdfLoadedListItem pdfLoadedListItem;
          if (pdfPrimitive is PdfString)
          {
            pdfLoadedListItem = new PdfLoadedListItem((pdfPrimitive as PdfString).Value, (string) null, this, this.CrossTable);
          }
          else
          {
            PdfArray pdfArray2 = pdfPrimitive as PdfArray;
            PdfString pdfString = this.CrossTable.GetObject(pdfArray2[0]) as PdfString;
            pdfLoadedListItem = new PdfLoadedListItem((this.CrossTable.GetObject(pdfArray2[1]) as PdfString).Value, pdfString.Value, this, this.CrossTable);
          }
          listItemCollection.AddItem(pdfLoadedListItem);
        }
        return listItemCollection;
      }

      protected int[] GetSelectedIndex()
      {
        List<int> intList = new List<int>();
        if (this.Dictionary.ContainsKey("I"))
        {
          if (this.CrossTable.GetObject(this.Dictionary["I"]) is PdfArray pdfArray)
          {
            if (pdfArray.Count > 0)
            {
              for (int index = 0; index < pdfArray.Count; ++index)
              {
                PdfNumber pdfNumber = this.CrossTable.GetObject(pdfArray[index]) as PdfNumber;
                intList.Add(pdfNumber.IntValue);
              }
            }
          }
          else if (this.CrossTable.GetObject(this.Dictionary["I"]) is PdfNumber pdfNumber1)
            intList.Add(pdfNumber1.IntValue);
        }
        if (intList.Count == 0)
          intList.Add(-1);
        return intList.ToArray();
      }

      protected string[] GetSelectedValue()
      {
        List<string> stringList = new List<string>();
        if (this.Dictionary.ContainsKey("V"))
        {
          IPdfPrimitive pdfPrimitive = this.CrossTable.GetObject(this.Dictionary["V"]);
          if (pdfPrimitive is PdfString)
          {
            stringList.Add((pdfPrimitive as PdfString).Value);
          }
          else
          {
            PdfArray pdfArray = pdfPrimitive as PdfArray;
            for (int index = 0; index < pdfArray.Count; ++index)
            {
              PdfString pdfString = pdfArray[index] as PdfString;
              stringList.Add(pdfString.Value);
            }
          }
        }
        else
        {
          foreach (int index in this.SelectedIndex)
          {
            if (index > -1)
              stringList.Add(this.Values[index].Value);
          }
        }
        return stringList.ToArray();
      }

      protected void SetSelectedIndex(int[] value)
      {
        if (value.Length == 0 || value.Length > this.Values.Count)
          throw new ArgumentOutOfRangeException("SelectedIndex");
        foreach (int num in value)
        {
          if (num < 0 || num >= this.Values.Count)
            throw new ArgumentOutOfRangeException("SelectedIndex");
        }
        if (this.ReadOnly)
          return;
        this.Dictionary.SetProperty("I", (IPdfPrimitive) new PdfArray(value));
        List<string> stringList = new List<string>();
        foreach (int index in value)
          stringList.Add(this.Values[index].Value);
        this.SetSelectedValue(stringList.ToArray());
        this.Changed = true;
      }

      protected void SetSelectedValue(string[] values)
      {
        List<int> intList = new List<int>();
        List<string> stringList = new List<string>();
        PdfLoadedListItemCollection values1 = this.Values;
        foreach (string str in values)
        {
          int num = 0;
          foreach (PdfLoadedListItem pdfLoadedListItem in (PdfCollection) values1)
          {
            stringList.Add(pdfLoadedListItem.Value);
            if (pdfLoadedListItem.Value == str)
            {
              intList.Add(num);
              break;
            }
            ++num;
          }
          if (!stringList.Contains(str))
            throw new ArgumentOutOfRangeException("index");
        }
        int[] selectedIndex = this.GetSelectedIndex();
        bool flag = false;
        if (selectedIndex.Length == intList.Count)
        {
          for (int index = 0; index < selectedIndex.Length; ++index)
          {
            if (selectedIndex[index] == intList.ToArray()[index])
            {
              flag = true;
            }
            else
            {
              flag = false;
              break;
            }
          }
          if (!flag)
            this.SetSelectedIndex(intList.ToArray());
        }
        if (this.Dictionary.ContainsKey("V"))
        {
          IPdfPrimitive pdfPrimitive = this.CrossTable.GetObject(this.Dictionary["V"]);
          switch (pdfPrimitive)
          {
            case null:
            case PdfString _:
              this.Dictionary.SetString("V", values[0]);
              break;
            default:
              PdfArray primitive = pdfPrimitive as PdfArray;
              primitive.Clear();
              foreach (string str in values)
                primitive.Add((IPdfPrimitive) new PdfString(str));
              this.Dictionary.SetProperty("V", (IPdfPrimitive) primitive);
              break;
          }
        }
        else
        {
          PdfArray primitive = new PdfArray();
          foreach (string str in values)
            primitive.Add((IPdfPrimitive) new PdfString(str));
          this.Dictionary.SetProperty("V", (IPdfPrimitive) primitive);
        }
        this.Changed = true;
      }

      public int[] SelectedIndex
      {
        get => this.GetSelectedIndex();
        set => this.SetSelectedIndex(value);
      }

      public PdfLoadedListItemCollection SelectedItem
      {
        get
        {
          PdfLoadedListItemCollection selectedItem = new PdfLoadedListItemCollection(this);
          foreach (int index in this.SelectedIndex)
          {
            if (index > -1)
              selectedItem.Add(this.Values[index]);
          }
          return selectedItem;
        }
      }

      public string[] SelectedValue
      {
        get => this.GetSelectedValue();
        set => this.SetSelectedValue(value);
      }

      public PdfLoadedListItemCollection Values => this.GetListItemCollection();
    }
}
