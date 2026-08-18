// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedCheckBoxField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedCheckBoxField : PdfLoadedStateField
    {
      private const string CHECK_SYMBOL = "4";

      internal PdfLoadedCheckBoxField(PdfDictionary dictionary, PdfCrossTable crossTable)
        : base(dictionary, crossTable, (PdfLoadedStateItemCollection) new PdfLoadedCheckBoxItemCollection())
      {
      }

      internal override void BeginSave()
      {
        base.BeginSave();
        PdfArray kids = this.Kids;
        if (kids != null)
        {
          for (int index = 0; index < kids.Count; ++index)
            this.ApplyAppearance(this.CrossTable.GetObject(kids[index]) as PdfDictionary, this.Items[index]);
        }
        else
          this.ApplyAppearance(this.GetWidgetAnnotation(this.Dictionary, this.CrossTable), (PdfLoadedCheckBoxItem) null);
      }

      internal new PdfField Clone(PdfDictionary dictionary, PdfPage page)
      {
        PdfCrossTable crossTable = page.Section.ParentDocument.CrossTable;
        PdfLoadedCheckBoxField loadedCheckBoxField = new PdfLoadedCheckBoxField(dictionary, crossTable);
        loadedCheckBoxField.Page = (PdfPageBase) page;
        loadedCheckBoxField.SetName(this.GetFieldName());
        loadedCheckBoxField.Widget.Dictionary = this.Widget.Dictionary.Clone(crossTable) as PdfDictionary;
        return (PdfField) loadedCheckBoxField;
      }

      internal override PdfLoadedFieldItem CreateLoadedItem(PdfDictionary dictionary)
      {
        base.CreateLoadedItem(dictionary);
        PdfLoadedCheckBoxItem loadedItem = (PdfLoadedCheckBoxItem) null;
        if (this.Items != null)
        {
          loadedItem = new PdfLoadedCheckBoxItem((PdfLoadedStyledField) this, this.Items.Count, dictionary);
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
            PdfLoadedCheckBoxItem loadedCheckBoxItem = this.Items[index];
            PdfCheckFieldState state = loadedCheckBoxItem.Checked ? PdfCheckFieldState.Checked : PdfCheckFieldState.Unchecked;
            this.DrawStateItem(loadedCheckBoxItem.Page.Graphics, state, (PdfLoadedStateItem) loadedCheckBoxItem);
          }
        }
        else
          this.DrawStateItem(this.Page.Graphics, this.Checked ? PdfCheckFieldState.Checked : PdfCheckFieldState.Unchecked, (PdfLoadedStateItem) null);
      }

      internal override PdfLoadedStateItem GetItem(int index, PdfDictionary itemDictionary)
      {
        return (PdfLoadedStateItem) new PdfLoadedCheckBoxItem((PdfLoadedStyledField) this, index, itemDictionary);
      }

      public bool Checked
      {
        get
        {
          bool flag = false;
          if (this.Items.Count > 0)
            flag = this.Items[this.DefaultIndex].Checked;
          return flag;
        }
        set
        {
          if ((FieldFlags.ReadOnly & this.Flags) != FieldFlags.Default)
            return;
          if (this.Items.Count > 0)
            this.Items[this.DefaultIndex].Checked = value;
          else
            this.SetCheckedStatus(value);
          this.Form.SetAppearanceDictionary = true;
        }
      }

      public PdfLoadedCheckBoxItemCollection Items => base.Items as PdfLoadedCheckBoxItemCollection;
    }
}
