// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedFormFieldCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedFormFieldCollection : PdfFieldCollection
{
  private System.Collections.Generic.List<string> m_actualFieldNames;
  private System.Collections.Generic.List<string> m_addedFieldNames;
  private System.Collections.Generic.List<string> m_fieldNames;
  private PdfLoadedForm m_form;
  private System.Collections.Generic.List<string> m_indexedActualFieldNames;
  private System.Collections.Generic.List<string> m_indexedFieldNames;

  public PdfLoadedFormFieldCollection() => this.m_addedFieldNames = new System.Collections.Generic.List<string>();

  public PdfLoadedFormFieldCollection(PdfLoadedForm form)
  {
    this.m_addedFieldNames = new System.Collections.Generic.List<string>();
    this.m_form = form != null ? form : throw new ArgumentException(nameof (form));
    int index = 0;
    for (int count = this.m_form.TerminalFields.Count; index < count; ++index)
    {
      PdfField field = this.GetField(index);
      if (field != null)
        this.DoAdd(field);
    }
  }

  internal void AddFieldDictionary(PdfDictionary field)
  {
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    this.List.Add((object) field);
    this.Items.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) field));
  }

  private PdfField CreateCheckBox(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedCheckBoxField checkBox = new PdfLoadedCheckBoxField(dictionary, crossTable);
    checkBox.SetForm((PdfForm) this.Form);
    return (PdfField) checkBox;
  }

  private PdfField CreateComboBox(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedComboBoxField comboBox = new PdfLoadedComboBoxField(dictionary, crossTable);
    comboBox.SetForm((PdfForm) this.Form);
    return (PdfField) comboBox;
  }

  private PdfField CreateListBox(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedListBoxField listBox = new PdfLoadedListBoxField(dictionary, crossTable);
    listBox.SetForm((PdfForm) this.Form);
    return (PdfField) listBox;
  }

  private PdfField CreatePushButton(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedButtonField pushButton = new PdfLoadedButtonField(dictionary, crossTable);
    pushButton.SetForm((PdfForm) this.Form);
    return (PdfField) pushButton;
  }

  private PdfField CreateRadioButton(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedRadioButtonListField radioButton = new PdfLoadedRadioButtonListField(dictionary, crossTable);
    radioButton.SetForm((PdfForm) this.Form);
    return (PdfField) radioButton;
  }

  private PdfField CreateSignatureField(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedSignatureField signatureField = new PdfLoadedSignatureField(dictionary, crossTable);
    signatureField.SetForm((PdfForm) this.Form);
    return (PdfField) signatureField;
  }

  private PdfField CreateTextField(PdfDictionary dictionary, PdfCrossTable crossTable)
  {
    PdfLoadedTextBoxField textField = new PdfLoadedTextBoxField(dictionary, crossTable);
    textField.SetForm((PdfForm) this.Form);
    return (PdfField) textField;
  }

  protected override int DoAdd(PdfField field)
  {
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    field.SetForm((PdfForm) this.m_form);
    PdfArray primitive = !this.m_form.Dictionary.ContainsKey("Fields") ? new PdfArray() : this.m_form.CrossTable.GetObject(this.m_form.Dictionary["Fields"]) as PdfArray;
    PdfReferenceHolder element = new PdfReferenceHolder((IPdfWrapper) field);
    if (!primitive.Contains((IPdfPrimitive) element))
    {
      if (!this.IsValidName(field.Name))
      {
        if (!this.m_form.FieldAutoNaming)
          throw new PdfDocumentException(string.Format(this.c_exisingFieldException, (object) field.Name));
        string correctName = this.GetCorrectName(field.Name);
        field.ApplyName(correctName);
        primitive.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field));
        this.m_form.Dictionary.SetProperty("Fields", (IPdfPrimitive) primitive);
      }
      else
      {
        primitive.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field));
        this.m_form.Dictionary.SetProperty("Fields", (IPdfPrimitive) primitive);
      }
    }
    this.m_addedFieldNames.Add(field.Name);
    return base.DoAdd(field);
  }

  protected override void DoClear()
  {
    int index = 0;
    for (int count = this.List.Count; index < count; ++index)
    {
      if (this.List[index] is PdfLoadedField field)
        this.m_form.RemoveFromDictionaries((PdfField) field);
    }
    this.m_addedFieldNames.Clear();
    this.m_form.TerminalFields.Clear();
    base.DoClear();
  }

  protected override void DoInsert(int index, PdfField field)
  {
    if (index < 0 || index > this.List.Count)
      throw new IndexOutOfRangeException();
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    field.SetForm((PdfForm) this.m_form);
    if (!(field is PdfLoadedField))
    {
      PdfArray primitive = !this.m_form.Dictionary.ContainsKey("Fields") ? new PdfArray() : this.m_form.CrossTable.GetObject(this.m_form.Dictionary["Fields"]) as PdfArray;
      primitive.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) field));
      this.m_form.Dictionary.SetProperty("Fields", (IPdfPrimitive) primitive);
    }
    base.DoInsert(index, field);
  }

  protected override void DoRemove(PdfField field)
  {
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    this.m_form.RemoveFromDictionaries(field);
    base.DoRemove(field);
  }

  protected override void DoRemoveAt(int index)
  {
    if (index < 0 || index > this.List.Count)
      throw new IndexOutOfRangeException();
    PdfField field = this.List[index] as PdfField;
    if (field is PdfLoadedField)
      this.m_form.RemoveFromDictionaries(field);
    base.DoRemoveAt(index);
  }

  internal string GetCorrectName(string name)
  {
    System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
    foreach (PdfField pdfField in (IEnumerable) this.List)
      stringList.Add(pdfField.Name);
    string correctName = name;
    int num = 0;
    while (stringList.IndexOf(correctName) != -1)
    {
      correctName = name + (object) num;
      ++num;
    }
    return correctName;
  }

  private PdfField GetField(int index)
  {
    PdfDictionary terminalField = this.m_form.TerminalFields[index];
    PdfCrossTable crossTable = this.m_form.CrossTable;
    PdfField field = (PdfField) null;
    PdfName name = PdfLoadedField.GetValue(terminalField, crossTable, "FT", true) as PdfName;
    PdfLoadedFieldTypes loadedFieldTypes = PdfLoadedFieldTypes.Null;
    if (name != (PdfName) null)
      loadedFieldTypes = this.GetFieldType(name, terminalField, crossTable);
    switch (loadedFieldTypes)
    {
      case PdfLoadedFieldTypes.PushButton:
        field = this.CreatePushButton(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.CheckBox:
        field = this.CreateCheckBox(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.RadioButton:
        field = this.CreateRadioButton(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.TextField:
        field = this.CreateTextField(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.ListBox:
        field = this.CreateListBox(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.ComboBox:
        field = this.CreateComboBox(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.SignatureField:
        field = this.CreateSignatureField(terminalField, crossTable);
        break;
      case PdfLoadedFieldTypes.Null:
        field = (PdfField) new PdfLoadedStyledField(terminalField, crossTable);
        field.SetForm((PdfForm) this.Form);
        break;
    }
    if (field is PdfLoadedField pdfLoadedField)
    {
      pdfLoadedField.SetForm((PdfForm) this.Form);
      pdfLoadedField.BeforeNameChanges += new PdfLoadedField.BeforeNameChangesEventHandler(this.ldField_NameChanded);
    }
    return field;
  }

  private int GetFieldIndex(string name)
  {
    int fieldIndex = -1;
    if (this.m_fieldNames == null)
    {
      this.m_fieldNames = new System.Collections.Generic.List<string>();
      this.m_indexedFieldNames = new System.Collections.Generic.List<string>();
      foreach (PdfField pdfField in (IEnumerable) this.List)
      {
        this.m_fieldNames.Add(pdfField.Name);
        this.m_indexedFieldNames.Add(pdfField.Name.Split('[')[0]);
      }
    }
    if (this.m_fieldNames.Contains(name))
      fieldIndex = this.m_fieldNames.IndexOf(name);
    else if (this.m_indexedFieldNames.Contains(name))
      fieldIndex = this.m_indexedFieldNames.IndexOf(name);
    if (fieldIndex < 0)
    {
      if (this.m_actualFieldNames == null)
      {
        this.m_actualFieldNames = new System.Collections.Generic.List<string>();
        this.m_indexedActualFieldNames = new System.Collections.Generic.List<string>();
        foreach (PdfLoadedField pdfLoadedField in (IEnumerable) this.List)
        {
          this.m_actualFieldNames.Add(pdfLoadedField.ActualFieldName);
          this.m_indexedActualFieldNames.Add(pdfLoadedField.ActualFieldName.Split('[')[0]);
        }
      }
      if (this.m_actualFieldNames.Contains(name))
        return this.m_actualFieldNames.IndexOf(name);
      if (this.m_indexedActualFieldNames.Contains(name))
        fieldIndex = this.m_indexedActualFieldNames.IndexOf(name);
    }
    return fieldIndex;
  }

  private PdfLoadedFieldTypes GetFieldType(
    PdfName name,
    PdfDictionary dictionary,
    PdfCrossTable crossTable)
  {
    string str = name.Value;
    PdfLoadedFieldTypes fieldType = PdfLoadedFieldTypes.Null;
    PdfNumber pdfNumber = PdfLoadedField.GetValue(dictionary, crossTable, "Ff", true) as PdfNumber;
    int num = 0;
    if (pdfNumber != null)
      num = pdfNumber.IntValue;
    string lower = str.ToLower();
    switch (lower)
    {
      case null:
        return fieldType;
      case "btn":
        if ((num & 65536 /*0x010000*/) != 0)
          return PdfLoadedFieldTypes.PushButton;
        return (num & 32768 /*0x8000*/) != 0 ? PdfLoadedFieldTypes.RadioButton : PdfLoadedFieldTypes.CheckBox;
      default:
        if (!(lower != "tx"))
          return PdfLoadedFieldTypes.TextField;
        return lower == "ch" ? ((num & 131072 /*0x020000*/) != 0 ? PdfLoadedFieldTypes.ComboBox : PdfLoadedFieldTypes.ListBox) : (lower != "sig" ? fieldType : PdfLoadedFieldTypes.SignatureField);
    }
  }

  private PdfField GetNamedField(string name)
  {
    PdfField namedField = (PdfField) null;
    foreach (PdfField pdfField in (IEnumerable) this.List)
    {
      if (pdfField.Name == name)
        namedField = pdfField;
    }
    return namedField;
  }

  internal bool IsValidName(string name) => !this.m_addedFieldNames.Contains(name);

  private void ldField_NameChanded(string name)
  {
    if (!this.IsValidName(name))
      throw new ArgumentException("Field with the same name already exist");
  }

  public bool TryGetField(string fieldName, out PdfLoadedField field)
  {
    field = (PdfLoadedField) null;
    int fieldIndex = this.GetFieldIndex(fieldName);
    if (fieldIndex <= -1)
      return false;
    field = this.List[fieldIndex] as PdfLoadedField;
    return true;
  }

  public bool TryGetValue(string fieldName, out string fieldValue)
  {
    fieldValue = string.Empty;
    int fieldIndex = this.GetFieldIndex(fieldName);
    if (fieldIndex <= -1)
      return false;
    fieldValue = (this.List[fieldIndex] as PdfLoadedTextBoxField).Text;
    return true;
  }

  public PdfLoadedForm Form
  {
    get => this.m_form;
    set => this.m_form = value;
  }

  public override PdfField this[int index]
  {
    get
    {
      int count = this.List.Count;
      return count >= 0 && index < count ? this.List[index] as PdfField : throw new IndexOutOfRangeException(nameof (index));
    }
  }

  public new PdfField this[string name]
  {
    get
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      int index = !(name == string.Empty) ? this.GetFieldIndex(name) : throw new ArgumentException("Field name can't be empty");
      return index != -1 ? this[index] : throw new ArgumentException("Incorrect field name");
    }
  }
}
