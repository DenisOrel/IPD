// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedRadioButtonListField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedRadioButtonListField : PdfLoadedStateField
    {
      private const string CHECK_SYMBOL = "l";

      internal PdfLoadedRadioButtonListField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable, (PdfLoadedStateItemCollection) new PdfLoadedRadioButtonItemCollection())
      {
      }

      internal new PdfField Clone(PdfDictionary dictionary, PdfPage page)
      {
        PdfCrossTable crossTable = page.Section.ParentDocument.CrossTable;
        PdfLoadedRadioButtonListField radioButtonListField = new PdfLoadedRadioButtonListField(dictionary, crossTable);
        radioButtonListField.Page = (PdfPageBase) page;
        radioButtonListField.SetName(this.GetFieldName());
        radioButtonListField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) radioButtonListField;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        base.CreateLoadedItem(dictionary);
        PdfLoadedRadioButtonItem loadedItem = (PdfLoadedRadioButtonItem) null;
        if (this.Items != null)
        {
          loadedItem = new PdfLoadedRadioButtonItem((PdfLoadedStyledField) this, this.Items.Count, dictionary);
          this.Items.Add(loadedItem);
        }
        if (this.Kids == null)
          this.Dictionary["Kids"] = (IPdfPrimitive) new PdfArray();
        this.Kids.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
        return (PdfLoadedFieldItem) loadedItem;
      }

      internal override void Draw()
      {
        base.Draw();
        PdfArray kids = this.Kids;
        if (kids != null)
        {
          for (int index = 0; index < kids.Count; ++index)
          {
            PdfLoadedRadioButtonItem loadedRadioButtonItem = this.Items[index];
            PdfCheckFieldState state = loadedRadioButtonItem.Selected ? PdfCheckFieldState.Checked : PdfCheckFieldState.Unchecked;
            this.DrawStateItem(loadedRadioButtonItem.Page.Graphics, state, (PdfLoadedStateItem) loadedRadioButtonItem);
          }
        }
        else
          this.DrawStateItem(this.Page.Graphics, this.SelectedIndex == this.DefaultIndex ? PdfCheckFieldState.Checked : PdfCheckFieldState.Unchecked, (PdfLoadedStateItem) null);
      }

      internal override PdfLoadedStateItem GetItem(int index, PdfDictionary itemDictionary)
      {
        return (PdfLoadedStateItem) new PdfLoadedRadioButtonItem((PdfLoadedStyledField) this, index, itemDictionary);
      }

      private int GetSelectedIndex()
      {
        PdfLoadedRadioButtonItemCollection items = this.Items;
        int index = 0;
        for (int count = items.Count; index < count; ++index)
        {
          PdfDictionary dictionary = items[index].Dictionary;
          PdfName pdfName = PdfLoadedField.SearchInParents(dictionary, this.CrossTable, "V") as PdfName;
          if (dictionary.ContainsKey("AS") && pdfName != (PdfName) null && (this.CrossTable.GetObject(dictionary["AS"]) as PdfName).Value == pdfName.Value)
            return index;
        }
        return -1;
      }

      private void SetSelectedIndex(int value)
      {
        if (this.SelectedIndex == value)
          return;
        PdfLoadedRadioButtonItem child = this.Items[value];
        this.UncheckOthers((PdfLoadedStateItem) child, PdfLoadedStateField.GetItemValue(child.Dictionary, this.CrossTable), true);
        child.Checked = true;
        this.Dictionary.SetName("V", child.Value);
        this.Dictionary.SetName("DV", child.Value);
      }

      private void SetSelectedValue(string value)
      {
        if (value == null)
          throw new ArgumentNullException("SelectedValue");
        this.UncheckOthers((PdfLoadedStateItem) null, value, true);
        this.Dictionary.SetName("V", value);
        this.Dictionary.SetName("DV", value);
      }

      public PdfLoadedRadioButtonItemCollection Items
      {
        get => base.Items as PdfLoadedRadioButtonItemCollection;
      }

      public int SelectedIndex
      {
        get => this.GetSelectedIndex();
        set => this.SetSelectedIndex(value);
      }

      public PdfLoadedRadioButtonItem SelectedItem
      {
        get
        {
          int selectedIndex = this.SelectedIndex;
          PdfLoadedRadioButtonItem selectedItem = (PdfLoadedRadioButtonItem) null;
          if (selectedIndex > -1)
            selectedItem = this.Items[selectedIndex];
          return selectedItem;
        }
      }

      public string SelectedValue
      {
        get
        {
          int selectedIndex = this.SelectedIndex;
          return selectedIndex <= -1 ? (string) null : this.Items[selectedIndex].Value;
        }
        set => this.SetSelectedValue(value);
      }

      public string Value
      {
        get => this.Items[this.DefaultIndex].Value;
        set => this.Items[this.DefaultIndex].Value = value;
      }
    }
}
