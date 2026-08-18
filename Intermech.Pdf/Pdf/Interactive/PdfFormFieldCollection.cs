// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFormFieldCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Parsing;
using System;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfFormFieldCollection : PdfFieldCollection
    {
      private PdfForm m_form;

      protected override int DoAdd(PdfField field)
      {
        field.SetForm(this.Form);
        string empty = string.Empty;
        string name1 = (!(field is PdfLoadedField) ? field.Name : (field as PdfLoadedField).ActualFieldName) ?? Guid.NewGuid().ToString();
        this.m_form.FieldNames.Add(name1);
        string name2 = this.m_form.FieldAutoNaming ? this.m_form.GetCorrectName(name1) : throw new PdfDocumentException(string.Format(this.c_exisingFieldException, (object) name1));
        field.ApplyName(name2);
        return base.DoAdd(field);
      }

      protected override void DoClear()
      {
        foreach (PdfField field in (PdfCollection) this)
        {
          this.m_form.DeleteFromPages(field);
          this.m_form.DeleteAnnotation(field);
          field.Page = (PdfPageBase) null;
          field.Dictionary.Clear();
          field.SetForm((PdfForm) null);
        }
        base.DoClear();
      }

      protected override void DoInsert(int index, PdfField field)
      {
        if (!this.IsValidName(field.Name))
          throw new PdfDocumentException(string.Format(this.c_exisingFieldException, (object) field.Name));
        field.SetForm(this.Form);
        base.DoInsert(index, field);
      }

      protected override void DoRemove(PdfField field)
      {
        field.SetForm((PdfForm) null);
        base.DoRemove(field);
      }

      protected override void DoRemoveAt(int index)
      {
        ((PdfField) this.Items[index]).SetForm((PdfForm) null);
        base.DoRemoveAt(index);
      }

      private bool IsValidName(string name) => this.m_form.FieldNames.Contains(name);

      internal PdfForm Form
      {
        get => this.m_form;
        set => this.m_form = value != null ? value : throw new ArgumentNullException("form");
      }
    }
}
