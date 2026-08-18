// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.PdfLoadedForm
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class PdfLoadedForm : PdfForm
{
  private PdfCrossTable m_crossTable;
  private PdfLoadedFormFieldCollection m_fields;
  private bool m_isModified;
  private bool m_isXFAForm;
  private List<PdfDictionary> m_terminalFields;

  internal PdfLoadedForm(PdfCrossTable crossTable)
  {
    this.m_terminalFields = new List<PdfDictionary>();
    this.m_crossTable = crossTable;
    this.Dictionary.SetBoolean(nameof (NeedAppearances), this.NeedAppearances);
    this.CrossTable.Document.Catalog.BeginSave += new SavePdfPrimitiveEventHandler(((PdfForm) this).Dictionary_BeginSave);
    this.CrossTable.Document.Catalog.Modify();
  }

  internal PdfLoadedForm(PdfDictionary formDictionary, PdfCrossTable crossTable)
    : this(crossTable)
  {
    this.Initialize(formDictionary, crossTable);
  }

  internal override void Clear()
  {
    if (this.m_fields != null)
      this.m_fields.Clear();
    if (this.m_pageMap != null)
      this.m_pageMap.Clear();
    if (this.m_terminalFields != null)
      this.m_terminalFields.Clear();
    this.Dictionary.Clear();
  }

  private void CreateFields()
  {
    PdfArray fields = (PdfArray) null;
    if (this.Dictionary.ContainsKey("Fields"))
      fields = this.m_crossTable.GetObject(this.Dictionary["Fields"]) as PdfArray;
    int num = 0;
    Stack<PdfLoadedForm.NodeInfo> nodeInfoStack = new Stack<PdfLoadedForm.NodeInfo>();
    while (fields != null)
    {
      for (; num < fields.Count; ++num)
      {
        PdfDictionary pdfDictionary = this.m_crossTable.GetObject(fields[num]) as PdfDictionary;
        PdfArray kids = (PdfArray) null;
        if (pdfDictionary != null && pdfDictionary.ContainsKey("Kids"))
          kids = this.m_crossTable.GetObject(pdfDictionary["Kids"]) as PdfArray;
        if (kids == null)
        {
          if (pdfDictionary != null && !this.m_terminalFields.Contains(pdfDictionary))
            this.m_terminalFields.Add(pdfDictionary);
        }
        else if (!pdfDictionary.ContainsKey("FT") || this.IsNode(kids))
        {
          PdfLoadedForm.NodeInfo nodeInfo = new PdfLoadedForm.NodeInfo(fields, num);
          nodeInfoStack.Push(nodeInfo);
          num = -1;
          fields = kids;
        }
        else
          this.m_terminalFields.Add(pdfDictionary);
      }
      if (nodeInfoStack.Count == 0)
        break;
      PdfLoadedForm.NodeInfo nodeInfo1 = nodeInfoStack.Pop();
      fields = nodeInfo1.Fields;
      num = nodeInfo1.Count + 1;
    }
  }

  internal void DeleteAnnottation(PdfField field)
  {
    PdfDictionary dictionary = field.Dictionary;
    PdfName key = new PdfName("Kids");
    if (!dictionary.ContainsKey(key))
      return;
    PdfArray primitive = this.m_crossTable.GetObject(dictionary[key]) as PdfArray;
    primitive.Clear();
    dictionary.SetProperty(key, (IPdfPrimitive) primitive);
  }

  internal new void DeleteFromPages(PdfField field)
  {
    PdfDictionary dictionary = field.Dictionary;
    PdfName key1 = new PdfName("Kids");
    PdfName key2 = new PdfName("Annots");
    PdfName key3 = new PdfName("P");
    if (dictionary.ContainsKey(key1))
    {
      PdfArray pdfArray = this.CrossTable.GetObject(dictionary[key1]) as PdfArray;
      int index = 0;
      for (int count = pdfArray.Count; index < count; ++index)
      {
        PdfReferenceHolder pdfReferenceHolder = pdfArray[index] as PdfReferenceHolder;
        PdfDictionary pdfDictionary1 = this.CrossTable.GetObject((IPdfPrimitive) pdfReferenceHolder) as PdfDictionary;
        PdfReference pointer = (PdfReference) null;
        if (pdfDictionary1.ContainsKey(key3))
          pointer = this.CrossTable.GetReference(pdfDictionary1[key3]);
        else if (dictionary.ContainsKey(key3))
          pointer = this.CrossTable.GetReference(dictionary[key3]);
        else if (field.Page != null)
          pointer = this.CrossTable.GetReference((IPdfPrimitive) field.Page.Dictionary);
        if (this.CrossTable.GetObject((IPdfPrimitive) pointer) is PdfDictionary pdfDictionary2 && pdfDictionary2.ContainsKey(key2))
        {
          PdfArray primitive = this.CrossTable.GetObject(pdfDictionary2[key2]) as PdfArray;
          primitive.Remove((IPdfPrimitive) pdfReferenceHolder);
          primitive.MarkChanged();
          pdfDictionary2.SetProperty(key2, (IPdfPrimitive) primitive);
        }
      }
    }
    else
    {
      PdfReference pointer = (PdfReference) null;
      if (dictionary.ContainsKey(key3))
        pointer = this.CrossTable.GetReference(dictionary[key3]);
      else if (field.Page != null)
        pointer = this.CrossTable.GetReference((IPdfPrimitive) field.Page.Dictionary);
      if (!(this.CrossTable.GetObject((IPdfPrimitive) pointer) is PdfDictionary pdfDictionary) || !pdfDictionary.ContainsKey(key2))
        return;
      PdfArray primitive = this.CrossTable.GetObject(pdfDictionary[key2]) as PdfArray;
      primitive.Remove((IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) dictionary));
      primitive.MarkChanged();
      pdfDictionary.SetProperty(key2, (IPdfPrimitive) primitive);
    }
  }

  internal override void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    int index = 0;
    if (base.SignatureFlags != SignatureFlags.None)
    {
      this.NeedAppearances = false;
      if (this.Dictionary.ContainsKey("NeedAppearances"))
        this.Dictionary.SetBoolean("NeedAppearances", this.NeedAppearances);
    }
    for (; index < this.Fields.Count; ++index)
    {
      if (this.Fields[index] is PdfLoadedField field1 && field1.DisableAutoFormat && field1.Dictionary.ContainsKey("AA"))
      {
        field1.Dictionary.Remove("AA");
        field1.BeginSave();
      }
      if (field1 != null)
      {
        int num = 0;
        PdfDictionary dictionary = field1.Dictionary;
        if (dictionary.ContainsKey("F"))
          num = (dictionary["F"] as PdfNumber).IntValue;
        if (field1.Flatten && num != 6)
        {
          field1.Draw();
          this.Fields.Remove((PdfField) field1);
          --index;
        }
        else if (field1.Changed)
          field1.BeginSave();
      }
      else
      {
        PdfField field2 = this.Fields[index];
        if (field2.Flatten)
        {
          this.Fields.Remove(field2);
          field2.Draw();
          --index;
        }
        else
          field2.Save();
      }
    }
    if (this.m_fields.Count == 0)
    {
      this.Dictionary.Clear();
    }
    else
    {
      if (!this.SetAppearanceDictionary)
        return;
      this.Dictionary.SetBoolean("NeedAppearances", this.NeedAppearances);
    }
  }

  public void ExportData(Stream stream, DataFormat dataFormat, string formName)
  {
    if (dataFormat == DataFormat.Xml)
      this.ExportDataXML(stream);
    if (dataFormat == DataFormat.Fdf)
      this.ExportDataFDF(stream, formName);
    if (dataFormat != DataFormat.XFdf)
      return;
    this.ExportDataXFDF(stream, formName);
  }

  public void ExportData(string fileName, DataFormat dataFormat, string formName)
  {
    FileStream fileStream = (FileStream) null;
    try
    {
      fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
      this.ExportData((Stream) fileStream, dataFormat, formName);
    }
    catch
    {
      throw;
    }
    finally
    {
      fileStream?.Close();
    }
  }

  private void ExportDataFDF(Stream stream, string formName)
  {
    BinaryWriter binaryWriter = new BinaryWriter(stream);
    byte[] bytes1 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString("%FDF-1.2\n")
    {
      Encode = PdfString.ForceEncoding.ASCII
    }.Value);
    stream.Write(bytes1, 0, bytes1.Length);
    int objectid = 1;
    for (int index = 0; index < this.Fields.Count; ++index)
    {
      PdfLoadedField field = (PdfLoadedField) this.Fields[index];
      if (field.Export)
        field.ExportField(stream, ref objectid);
    }
    StringBuilder stringBuilder = new StringBuilder();
    byte[] bytes2 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(formName)
    {
      Encode = PdfString.ForceEncoding.ASCII
    }.Value);
    stringBuilder.AppendFormat("{0} 0 obj<</F <{1}>  /Fields [", (object) objectid, (object) PdfString.BytesToHex(bytes2));
    for (int index = 0; index < this.Fields.Count; ++index)
    {
      PdfLoadedField field = (PdfLoadedField) this.Fields[index];
      if (field.Export && field.ObjectID != 0)
        stringBuilder.AppendFormat("{0} 0 R ", (object) field.ObjectID);
    }
    stringBuilder.Append("]>>endobj\n");
    stringBuilder.AppendFormat("{0} 0 obj<</Version /1.4 /FDF {1} 0 R>>endobj\n", (object) (objectid + 1), (object) objectid);
    stringBuilder.AppendFormat("trailer\n<</Root {0} 0 R>>\n", (object) (objectid + 1));
    byte[] bytes3 = Encoding.GetEncoding("windows-1252").GetBytes(new PdfString(stringBuilder.ToString())
    {
      Encode = PdfString.ForceEncoding.ASCII
    }.Value);
    stream.Write(bytes3, 0, bytes3.Length);
    stream.Flush();
  }

  private void ExportDataXFDF(Stream stream, string formName)
  {
    XFdfDocument xfdfDocument = new XFdfDocument(formName);
    for (int index = 0; index < this.Fields.Count; ++index)
    {
      PdfLoadedField field = (PdfLoadedField) this.Fields[index];
      if (field.Export)
      {
        switch ((PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "FT", true) as PdfName).Value)
        {
          case "Tx":
            if (PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) is PdfString pdfString1)
            {
              xfdfDocument.SetFields((object) field.Name, (object) pdfString1.Value);
              continue;
            }
            continue;
          case "Ch":
            if (field.GetType().Name == "PdfLoadedListBoxField")
            {
              if (PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) is PdfArray Fieldvalue)
              {
                xfdfDocument.SetFields((object) field.Name, (object) Fieldvalue);
                continue;
              }
              if (PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) is PdfString pdfString2)
              {
                xfdfDocument.SetFields((object) field.Name, (object) pdfString2.Value);
                continue;
              }
              continue;
            }
            PdfName pdfName1 = PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) as PdfName;
            if (pdfName1 != (PdfName) null)
            {
              xfdfDocument.SetFields((object) field.Name, (object) pdfName1.Value);
              continue;
            }
            if (PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) is PdfString pdfString3)
            {
              xfdfDocument.SetFields((object) field.Name, (object) pdfString3.Value);
              continue;
            }
            continue;
          case "Btn":
            PdfName pdfName2 = PdfLoadedField.GetValue(field.Dictionary, field.CrossTable, "V", true) as PdfName;
            if (pdfName2 != (PdfName) null)
            {
              xfdfDocument.SetFields((object) field.Name, (object) pdfName2.Value);
              continue;
            }
            PdfDictionary widgetAnnotation = field.GetWidgetAnnotation(field.Dictionary, field.CrossTable);
            if ((object) (widgetAnnotation["AS"] as PdfName) != null)
            {
              xfdfDocument.SetFields((object) field.Name, (object) (widgetAnnotation["AS"] as PdfName).Value);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    xfdfDocument.Save(stream);
  }

  internal void ExportDataXML(Stream stream)
  {
    XmlTextWriter textWriter = new XmlTextWriter(stream, (Encoding) new UTF8Encoding());
    textWriter.Formatting = Formatting.Indented;
    textWriter.WriteStartDocument();
    textWriter.WriteStartElement("Fields", "");
    for (int index = 0; index < this.Fields.Count; ++index)
    {
      PdfLoadedField field = (PdfLoadedField) this.Fields[index];
      if (field.Export)
        field.ExportField(textWriter);
    }
    textWriter.WriteEndElement();
    textWriter.Flush();
  }

  internal override string GetCorrectName(string name)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.Fields.Count; ++index)
      stringList.Add(this.Fields[index].Name);
    string correctName = name;
    int num = 0;
    while (stringList.IndexOf(correctName) != -1)
    {
      correctName = name + (object) num;
      ++num;
    }
    return correctName;
  }

  public void HighlightFields(bool highlight)
  {
    this.CrossTable.Document.Catalog["OpenAction"] = (IPdfPrimitive) new PdfDictionary()
    {
      ["Type"] = (IPdfPrimitive) new PdfName("Action"),
      ["S"] = (IPdfPrimitive) new PdfName("JavaScript"),
      ["JS"] = (!highlight ? (IPdfPrimitive) new PdfString("app.runtimeHighlight = false;") : (IPdfPrimitive) new PdfString("app.runtimeHighlight = true;"))
    };
    this.CrossTable.Document.Catalog.Modify();
  }

  private PdfLoadedFieldImportError[] ImportData(Stream stream, bool continueImportOnError)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(stream);
    if (xmlDocument.DocumentElement.LocalName.ToUpper() != "fields".ToUpper())
      throw new ArgumentException("The XML form data stream is not valid");
    ArrayList list = new ArrayList();
    this.ImportXMLData(xmlDocument.DocumentElement.ChildNodes, continueImportOnError, list);
    return list.Count == 0 ? (PdfLoadedFieldImportError[]) null : (PdfLoadedFieldImportError[]) list.ToArray(typeof (PdfLoadedFieldImportError));
  }

  public void ImportData(string fileName, DataFormat dataFormat)
  {
    this.ImportDataField(fileName, dataFormat, false);
  }

  private PdfLoadedFieldImportError[] ImportData(
    Stream fileName,
    DataFormat dataFormat,
    bool continueImportOnError)
  {
    if (dataFormat == DataFormat.Xml)
      return this.ImportData(fileName, continueImportOnError);
    return dataFormat == DataFormat.Fdf ? this.ImportDataFDF(fileName, continueImportOnError) : (PdfLoadedFieldImportError[]) null;
  }

  public PdfLoadedFieldImportError[] ImportData(
    string fileName,
    DataFormat dataFormat,
    bool errorFlag)
  {
    return this.ImportDataField(fileName, dataFormat, errorFlag);
  }

  public PdfLoadedFieldImportError[] ImportDataFDF(Stream stream, bool continueImportOnError)
  {
    PdfReader pdfReader = new PdfReader(stream);
    pdfReader.Position = 0L;
    if (pdfReader.GetNextToken().StartsWith("%") && !pdfReader.GetNextToken().StartsWith("FDF-"))
      throw new Exception("The source is not a valid FDF file because it does not start with\"%FDF-\"");
    Hashtable hashtable = new Hashtable();
    string key = "";
    for (string nextToken1 = pdfReader.GetNextToken(); nextToken1 != null && nextToken1 != string.Empty; nextToken1 = pdfReader.GetNextToken())
    {
      if (nextToken1.ToUpper() == "T")
      {
        nextToken1 = pdfReader.GetNextToken();
        while (nextToken1 != ">" && nextToken1 != ")")
        {
          nextToken1 = pdfReader.GetNextToken();
          if (nextToken1 != ">" && nextToken1 != ")")
          {
            if (this.OnlyHexInString(nextToken1))
            {
              key = PdfString.ByteToString(new PdfString().HexToBytes(nextToken1));
              break;
            }
            key = nextToken1;
          }
        }
      }
      if (nextToken1.ToUpper() == "V")
      {
        for (nextToken1 = pdfReader.GetNextToken(); nextToken1 != ">" && nextToken1 != ")"; nextToken1 = pdfReader.GetNextToken())
        {
          if (nextToken1 == "/" || nextToken1 != ")")
          {
            string nextToken2 = pdfReader.GetNextToken();
            if (nextToken2 != ">" && nextToken2 != ")")
            {
              if (this.OnlyHexInString(nextToken2))
              {
                nextToken1 = PdfString.ByteToString(new PdfString().HexToBytes(nextToken2));
                hashtable.Add((object) key, (object) nextToken1);
                break;
              }
              hashtable.Add((object) key, (object) nextToken2);
            }
          }
        }
      }
      if (nextToken1.ToUpper() == "F")
      {
        nextToken1 = pdfReader.GetNextToken();
        while (nextToken1 != ">")
        {
          nextToken1 = pdfReader.GetNextToken();
          if (nextToken1 != ">")
            PdfString.ByteToString(new PdfString().HexToBytes(nextToken1));
        }
      }
      Console.WriteLine(nextToken1.ToString() + "\n");
    }
    foreach (DictionaryEntry dictionaryEntry in hashtable)
    {
      try
      {
        ((PdfLoadedField) this.Fields[dictionaryEntry.Key.ToString()])?.ImportFieldValue(dictionaryEntry.Value.ToString());
      }
      catch
      {
        if (!continueImportOnError)
          throw;
      }
    }
    return (PdfLoadedFieldImportError[]) null;
  }

  private PdfLoadedFieldImportError[] ImportDataField(
    string fileName,
    DataFormat dataFormat,
    bool continueImportOnError)
  {
    FileStream fileName1 = (FileStream) null;
    try
    {
      fileName1 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
      return this.ImportData((Stream) fileName1, dataFormat, continueImportOnError);
    }
    catch
    {
      throw;
    }
    finally
    {
      fileName1?.Close();
    }
  }

  public void ImportDataXFDF(Stream stream)
  {
    if (stream == null)
      return;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(stream);
    XmlNodeList elementsByTagName1 = xmlDocument.GetElementsByTagName("field");
    XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("value");
    string[] strArray1 = new string[elementsByTagName1.Count];
    string[] strArray2 = new string[elementsByTagName2.Count];
    for (int i = 0; i < elementsByTagName1.Count; ++i)
    {
      strArray1[i] = elementsByTagName1[i].Attributes["name"].Value;
      PdfLoadedField field = (PdfLoadedField) this.Fields[strArray1[i]];
      strArray2[i] = elementsByTagName2[i].InnerText;
      string FieldValue = strArray2[i];
      field.ImportFieldValue(FieldValue);
    }
    stream.Dispose();
  }

  public void ImportDataXFDF(string fileName)
  {
    Stream inStream = (Stream) new FileStream(fileName, FileMode.Open);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(inStream);
    XmlNodeList elementsByTagName1 = xmlDocument.GetElementsByTagName("field");
    XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("value");
    string[] strArray1 = new string[elementsByTagName1.Count];
    string[] strArray2 = new string[elementsByTagName2.Count];
    for (int i = 0; i < elementsByTagName1.Count; ++i)
    {
      strArray1[i] = elementsByTagName1[i].Attributes["name"].Value;
      PdfLoadedField field = (PdfLoadedField) this.Fields[strArray1[i]];
      strArray2[i] = elementsByTagName2[i].InnerText;
      string FieldValue = strArray2[i];
      field.ImportFieldValue(FieldValue);
    }
    inStream.Dispose();
  }

  private void ImportXMLData(XmlNodeList xmlnode, bool continueImportOnError, ArrayList list)
  {
    for (int i = 0; i < xmlnode.Count; ++i)
    {
      if (xmlnode[i] is XmlText xmlText)
      {
        string data = xmlText.Data;
        XmlNode parentNode = xmlText.ParentNode;
        string name = "";
        for (; parentNode.LocalName.ToUpper() != "fields".ToUpper(); parentNode = parentNode.ParentNode)
        {
          if (name.Length > 0)
            name = "." + name;
          name = parentNode.LocalName + name;
        }
        PdfLoadedField field = (PdfLoadedField) null;
        try
        {
          field = (PdfLoadedField) this.Fields[name];
          field?.ImportFieldValue(data);
        }
        catch (Exception ex)
        {
          if (!continueImportOnError)
            throw;
          PdfLoadedFieldImportError fieldImportError = new PdfLoadedFieldImportError(field, ex);
          list.Add((object) fieldImportError);
        }
      }
      if (xmlnode[i].ChildNodes != null)
        this.ImportXMLData(xmlnode[i].ChildNodes, continueImportOnError, list);
    }
  }

  private void Initialize(PdfDictionary formDictionary, PdfCrossTable crossTable)
  {
    if (formDictionary == null)
      throw new ArgumentNullException("dictionary");
    if (crossTable == null)
      throw new ArgumentNullException(nameof (crossTable));
    this.Dictionary = formDictionary;
    if (this.Dictionary.ContainsKey("XFA"))
    {
      this.m_isXFAForm = true;
      this.Dictionary.Remove("XFA");
    }
    this.CreateFields();
    if (this.Dictionary.ContainsKey("NeedAppearances"))
    {
      base.NeedAppearances = (this.m_crossTable.GetObject(this.Dictionary["NeedAppearances"]) as PdfBoolean).Value;
      this.SetAppearanceDictionary = true;
    }
    else
      this.SetAppearanceDictionary = false;
    if (this.Dictionary.ContainsKey("SigFlags"))
      base.SignatureFlags = (SignatureFlags) (this.m_crossTable.GetObject(this.Dictionary["SigFlags"]) as PdfNumber).IntValue;
    if (!this.Dictionary.ContainsKey("DR"))
      return;
    PdfResources pdfResources = new PdfResources(this.m_crossTable.GetObject(this.Dictionary["DR"]) as PdfDictionary);
    this.Resources = pdfResources;
    base.Resources = pdfResources;
  }

  private bool IsNode(PdfArray kids)
  {
    bool flag = false;
    if (kids.Count >= 1)
    {
      PdfDictionary pdfDictionary = this.m_crossTable.GetObject(kids[0]) as PdfDictionary;
      if (pdfDictionary.ContainsKey("Subtype") && (this.m_crossTable.GetObject(pdfDictionary["Subtype"]) as PdfName).Value != "Widget")
        flag = true;
    }
    return flag;
  }

  public bool OnlyHexInString(string test) => Regex.IsMatch(test, "\\A\\b[0-9a-fA-F]+\\b\\Z");

  internal void OnValidate(string nodeName)
  {
    if (nodeName.StartsWith("XML"))
      throw new Exception("Element type names may not start with XML");
    if (nodeName.StartsWith("_"))
      throw new Exception("Element type names must start with a letter or underscore");
    if (!char.IsLetter(nodeName[0]) && !char.IsNumber(nodeName[0]))
      throw new Exception("Element type names must start with a letter or underscore");
  }

  internal void RemoveFromDictionaries(PdfField field)
  {
    if (this.m_fields != null && this.m_fields.Count > 0)
    {
      PdfName key = new PdfName("Fields");
      PdfArray primitive = this.m_crossTable.GetObject(this.Dictionary[key]) as PdfArray;
      PdfReferenceHolder element = new PdfReferenceHolder((IPdfPrimitive) field.Dictionary);
      primitive.Remove((IPdfPrimitive) element);
      primitive.MarkChanged();
      this.Dictionary.SetProperty(key, (IPdfPrimitive) primitive);
    }
    if (!(field is PdfLoadedField))
      return;
    this.DeleteFromPages(field);
    this.DeleteAnnottation(field);
  }

  internal PdfCrossTable CrossTable => this.m_crossTable;

  public PdfLoadedFormFieldCollection Fields
  {
    get
    {
      if (this.m_fields == null)
        this.m_fields = new PdfLoadedFormFieldCollection(this);
      return this.m_fields;
    }
  }

  internal bool IsModified
  {
    get => this.m_isModified;
    set => this.m_isModified = value;
  }

  internal bool IsXFAForm
  {
    get => this.m_isXFAForm;
    set => this.m_isXFAForm = value;
  }

  internal override bool NeedAppearances
  {
    get => base.NeedAppearances;
    set
    {
      base.NeedAppearances = value;
      this.IsModified = true;
    }
  }

  public override bool ReadOnly
  {
    get => base.ReadOnly;
    set
    {
      base.ReadOnly = value;
      foreach (PdfField field in (PdfCollection) this.Fields)
        field.ReadOnly = value;
    }
  }

  internal override PdfResources Resources
  {
    get => base.Resources;
    set
    {
      base.Resources = value;
      this.IsModified = true;
      this.Dictionary.SetProperty("DR", (IPdfPrimitive) value);
    }
  }

  internal override SignatureFlags SignatureFlags
  {
    get => base.SignatureFlags;
    set
    {
      base.SignatureFlags = value;
      this.IsModified = true;
      this.Dictionary.SetNumber("SigFlags", (int) value);
    }
  }

  internal List<PdfDictionary> TerminalFields
  {
    get => this.m_terminalFields;
    set => this.m_terminalFields = value;
  }

  private class NodeInfo
  {
    private int m_count;
    private PdfArray m_fields;

    internal NodeInfo(PdfArray fields, int count)
    {
      this.m_fields = fields;
      this.m_count = count;
    }

    internal int Count
    {
      get => this.m_count;
      set => this.m_count = value;
    }

    internal PdfArray Fields
    {
      get => this.m_fields;
      set => this.m_fields = value;
    }
  }
}
