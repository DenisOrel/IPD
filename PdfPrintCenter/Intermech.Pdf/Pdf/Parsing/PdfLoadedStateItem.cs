// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedStateItem
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedStateItem : PdfLoadedFieldItem
{
  internal PdfLoadedStateItem(PdfLoadedStyledField field, int index, PdfDictionary dictionary)
    : base(field, index, dictionary)
  {
  }

  private void SetCheckedStatus(bool value)
  {
    int num = value ? 1 : 0;
    string name = PdfLoadedStateField.GetItemValue(this.Dictionary, this.CrossTable);
    (this.Parent as PdfLoadedStateField).UncheckOthers(this, name, value);
    if (num != 0)
    {
      if (name == null || name != null && name.Length == 0)
        name = "Yes";
      this.Parent.Dictionary.SetName("V", name);
      this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName(name));
      this.Dictionary.SetProperty("V", (IPdfPrimitive) new PdfName(name));
    }
    else
    {
      PdfName pdfName = PdfCrossTable.Dereference(this.Parent.Dictionary["V"]) as PdfName;
      if (pdfName != (PdfName) null && name == pdfName.Value)
        this.Parent.Dictionary.Remove("V");
      this.Dictionary.SetProperty("AS", (IPdfPrimitive) new PdfName("Off"));
    }
    this.Parent.Changed = true;
  }

  public bool Checked
  {
    get
    {
      bool flag = false;
      PdfName pdfName1 = PdfCrossTable.Dereference(this.Dictionary["AS"]) as PdfName;
      if (!(pdfName1 == (PdfName) null))
        return pdfName1.Value != "Off";
      PdfName pdfName2 = PdfLoadedField.GetValue(this.Parent.Dictionary, this.Parent.CrossTable, "V", false) as PdfName;
      if (pdfName2 != (PdfName) null)
        flag = pdfName2.Value == PdfLoadedStateField.GetItemValue(this.Dictionary, this.CrossTable);
      return flag;
    }
    set
    {
      if ((FieldFlags.ReadOnly & this.Field.Flags) != FieldFlags.Default || value == this.Checked)
        return;
      this.SetCheckedStatus(value);
      this.Field.Form.SetAppearanceDictionary = true;
    }
  }
}
