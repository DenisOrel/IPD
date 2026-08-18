// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedCheckBoxItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Parsing
{
    public class PdfLoadedCheckBoxItem : PdfLoadedStateItem
    {
      internal PdfLoadedCheckBoxItem(PdfLoadedStyledField field, int index, PdfDictionary dictionary)
        : base(field, index, dictionary)
      {
      }

      private void SetCheckedStatus(bool value)
      {
        int num = value ? 1 : 0;
        string itemValue = PdfLoadedStateField.GetItemValue(this.Dictionary, this.CrossTable);
        if (num != 0)
        {
          (this.Parent as PdfLoadedCheckBoxField).UncheckOthers((PdfLoadedStateItem) this, itemValue, value);
          this.Parent.Dictionary.SetName("V", itemValue);
          this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName(itemValue));
        }
        else
        {
          PdfName pdfName = PdfCrossTable.Dereference(this.Parent.Dictionary["V"]) as PdfName;
          if (pdfName != (PdfName) null && itemValue == pdfName.Value)
            this.Parent.Dictionary.Remove("V");
          this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName("Off"));
        }
        this.Parent.Changed = true;
      }
    }
}
