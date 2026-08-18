// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Primitives.PdfDictionary
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Syncfusion.Pdf.Primitives;

internal class PdfDictionary : IPdfPrimitive, IPdfChangable
{
  private bool m_archive;
  private bool m_bChanged;
  private PdfDictionary m_clonedObject;
  private PdfCrossTable m_crossTable;
  private bool m_encrypt;
  private int m_index;
  private bool m_isDecrypted;
  private bool m_isSaving;
  private Dictionary<PdfName, IPdfPrimitive> m_items;
  private int m_position;
  private ObjectStatus m_status;
  private const string Prefix = "<<";
  private static object s_syncLock = new object();
  private const string Suffix = ">>";

  internal event SavePdfPrimitiveEventHandler BeginSave;

  internal event SavePdfPrimitiveEventHandler EndSave;

  internal PdfDictionary()
  {
    this.m_archive = true;
    this.m_position = -1;
    this.m_items = new Dictionary<PdfName, IPdfPrimitive>();
    this.m_encrypt = true;
  }

  internal PdfDictionary(PdfDictionary dictionary)
  {
    this.m_archive = true;
    this.m_position = -1;
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    this.m_items = new Dictionary<PdfName, IPdfPrimitive>();
    foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in dictionary.m_items)
      this.m_items[keyValuePair.Key] = keyValuePair.Value;
    this.Status = dictionary.Status;
    this.FreezeChanges((object) this);
    this.m_encrypt = true;
  }

  private bool CheckChanges()
  {
    foreach (IPdfPrimitive pdfPrimitive in (IEnumerable) this.Values)
    {
      if (pdfPrimitive is IPdfChangable pdfChangable && pdfChangable.Changed)
        return true;
    }
    return false;
  }

  public void Clear()
  {
    this.m_items.Clear();
    this.Modify();
  }

  public virtual IPdfPrimitive Clone(PdfCrossTable crossTable)
  {
    if (!(this is PdfStream))
    {
      if (this.m_clonedObject != null && this.m_clonedObject.CrossTable == crossTable)
        return (IPdfPrimitive) this.m_clonedObject;
      this.m_clonedObject = (PdfDictionary) null;
    }
    PdfDictionary pdfDictionary = new PdfDictionary();
    foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in this.m_items)
    {
      PdfName key = keyValuePair.Key;
      IPdfPrimitive pdfPrimitive = keyValuePair.Value.Clone(crossTable);
      if (!(pdfPrimitive is PdfNull))
        pdfDictionary[key] = pdfPrimitive;
    }
    pdfDictionary.Archive = this.m_archive;
    pdfDictionary.IsDecrypted = this.m_isDecrypted;
    pdfDictionary.Status = this.m_status;
    pdfDictionary.Encrypt = this.m_encrypt;
    pdfDictionary.FreezeChanges((object) this);
    pdfDictionary.m_crossTable = crossTable;
    if (!(this is PdfStream))
      this.m_clonedObject = pdfDictionary;
    return (IPdfPrimitive) pdfDictionary;
  }

  public bool ContainsKey(PdfName key) => this.m_items.ContainsKey(key);

  public bool ContainsKey(string key) => this.ContainsKey(new PdfName(key));

  public void FreezeChanges(object freezer)
  {
    switch (freezer)
    {
      case PdfParser _:
      case PdfDictionary _:
        this.m_bChanged = false;
        break;
    }
  }

  internal DateTime GetDateTime(PdfString dateTimeString)
  {
    if (dateTimeString == null)
      throw new ArgumentNullException(nameof (dateTimeString));
    string format = "yyyyMMddHHmmss";
    string str1 = "D:";
    dateTimeString.Value = dateTimeString.Value.Trim('(', ')', 'D', ':');
    if (dateTimeString.Value.StartsWith("191"))
      dateTimeString.Value = dateTimeString.Value.Remove(0, 3).Insert(0, "20");
    bool flag = dateTimeString.Value.Contains(str1);
    string str2 = string.Empty.PadRight(format.Length);
    if (dateTimeString.Value.Length == 0)
      return DateTime.Now;
    string s = flag ? dateTimeString.Value.Substring(str1.Length, str2.Length) : dateTimeString.Value.Substring(0, str2.Length);
    DateTime result = DateTime.Now;
    DateTime.TryParseExact(s, format, (IFormatProvider) DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite, out result);
    return result;
  }

  internal int GetInt(string propertyName)
  {
    PdfNumber pdfNumber = PdfCrossTable.Dereference(this[propertyName]) as PdfNumber;
    int num = 0;
    if (pdfNumber != null)
      num = pdfNumber.IntValue;
    return num;
  }

  protected internal PdfName GetName(string name)
  {
    return name != null ? new PdfName(name) : throw new ArgumentNullException(nameof (name));
  }

  internal PdfString GetString(string propertyName)
  {
    return PdfCrossTable.Dereference(this[propertyName]) as PdfString;
  }

  public IPdfPrimitive GetValue(string key, string parentKey)
  {
    pdfDictionary = this;
    IPdfPrimitive pdfPrimitive = PdfCrossTable.Dereference(pdfDictionary[key]);
    while (pdfPrimitive == null && PdfCrossTable.Dereference(pdfDictionary[parentKey]) is PdfDictionary pdfDictionary)
      pdfPrimitive = PdfCrossTable.Dereference(pdfDictionary[key]);
    return pdfPrimitive;
  }

  public IPdfPrimitive GetValue(PdfCrossTable crossTable, string key, string parentKey)
  {
    PdfDictionary pdfDictionary = this;
    IPdfPrimitive pdfPrimitive;
    for (pdfPrimitive = PdfCrossTable.Dereference(pdfDictionary[key]); pdfPrimitive == null; pdfPrimitive = PdfCrossTable.Dereference(pdfDictionary[key]))
      pdfDictionary = PdfCrossTable.Dereference(pdfDictionary[parentKey]) as PdfDictionary;
    return pdfPrimitive;
  }

  internal void Modify() => this.m_bChanged = true;

  protected virtual void OnBeginSave(SavePdfPrimitiveEventArgs args)
  {
    lock (PdfDictionary.s_syncLock)
    {
      if (this.BeginSave == null)
        return;
      this.BeginSave((object) this, args);
    }
  }

  protected virtual void OnEndSave(SavePdfPrimitiveEventArgs args)
  {
    lock (PdfDictionary.s_syncLock)
    {
      if (this.EndSave == null)
        return;
      this.EndSave((object) this, args);
    }
  }

  public void Remove(PdfName key)
  {
    if (key == (PdfName) null)
      throw new ArgumentNullException(nameof (key));
    this.m_items.Remove(key);
    this.Modify();
  }

  public void Remove(string key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    this.Remove(new PdfName(key));
  }

  public virtual void Save(IPdfWriter writer)
  {
    lock (new object())
    {
      if (writer == null)
        throw new ArgumentNullException(nameof (writer));
      this.Save(writer, true);
    }
  }

  internal void Save(IPdfWriter writer, bool bRaiseEvent)
  {
    writer.Write("<<");
    if (bRaiseEvent)
      this.OnBeginSave(new SavePdfPrimitiveEventArgs(writer));
    if (this.Count > 0)
    {
      PdfSecurity security = writer.Document.Security;
      bool enabled = security.Enabled;
      if (!this.m_encrypt)
        security.Enabled = false;
      this.SaveItems(writer);
      if (!this.m_encrypt)
        security.Enabled = enabled;
    }
    writer.Write(">>");
    writer.Write("\r\n");
    if (!bRaiseEvent)
      return;
    this.OnEndSave(new SavePdfPrimitiveEventArgs(writer));
  }

  internal virtual void SaveItems(IPdfWriter writer)
  {
    lock (PdfDictionary.s_syncLock)
    {
      writer.Write("\r\n");
      foreach (KeyValuePair<PdfName, IPdfPrimitive> keyValuePair in this.m_items)
      {
        keyValuePair.Key.Save(writer);
        writer.Write(" ");
        keyValuePair.Value.Save(writer);
        writer.Write("\r\n");
      }
    }
  }

  internal void SetArray(string key, params IPdfPrimitive[] list)
  {
    if (this[key] is PdfArray pdfArray)
    {
      pdfArray.Clear();
      this.Modify();
    }
    else
    {
      pdfArray = new PdfArray();
      this[key] = (IPdfPrimitive) pdfArray;
    }
    foreach (IPdfPrimitive element in list)
      pdfArray.Add(element);
  }

  internal void SetBoolean(string key, bool value)
  {
    if (this[key] is PdfBoolean pdfBoolean)
    {
      pdfBoolean.Value = value;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfBoolean(value);
  }

  internal void SetDateTime(string key, DateTime dateTime)
  {
    if (this[key] is PdfString pdfString)
    {
      pdfString.Value = PdfString.FromDate(dateTime);
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfString(PdfString.FromDate(dateTime));
  }

  internal void SetName(string key, string name)
  {
    PdfName pdfName = this[key] as PdfName;
    if (pdfName != (PdfName) null)
    {
      pdfName.Value = name;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfName(name);
  }

  internal static void SetName(PdfDictionary dictionary, string key, string name)
  {
    PdfName pdfName = dictionary[key] as PdfName;
    if (pdfName != (PdfName) null)
    {
      pdfName.Value = name;
      dictionary.Modify();
    }
    else
      dictionary[key] = (IPdfPrimitive) new PdfName(name);
  }

  internal void SetName(string key, string name, bool processSpecialCharacters)
  {
    PdfName pdfName = this[key] as PdfName;
    string str = name.Replace("#", "#23").Replace(" ", "#20").Replace("/", "#2F");
    if (pdfName != (PdfName) null)
    {
      pdfName.Value = str;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfName(str);
  }

  internal void SetNumber(string key, int value)
  {
    if (this[key] is PdfNumber pdfNumber)
    {
      pdfNumber.IntValue = value;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfNumber(value);
  }

  internal void SetNumber(string key, float value)
  {
    if (this[key] is PdfNumber pdfNumber)
    {
      pdfNumber.FloatValue = value;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfNumber(value);
  }

  internal void SetProperty(PdfName key, IPdfPrimitive primitive)
  {
    if (primitive == null)
      this.m_items.Remove(key);
    else
      this[key] = primitive;
  }

  internal void SetProperty(string key, IPdfWrapper wrapper)
  {
    if (wrapper == null)
      this.m_items.Remove(new PdfName(key));
    else
      this.SetProperty(key, wrapper.Element);
  }

  internal void SetProperty(string key, IPdfPrimitive primitive)
  {
    if (primitive == null)
      this.m_items.Remove(new PdfName(key));
    else
      this[key] = primitive;
  }

  internal static void SetProperty(PdfDictionary dictionary, string key, IPdfWrapper wrapper)
  {
    if (wrapper == null)
      dictionary.Remove(new PdfName(key));
    else
      PdfDictionary.SetProperty(dictionary, key, wrapper.Element);
  }

  internal static void SetProperty(PdfDictionary dictionary, string key, IPdfPrimitive primitive)
  {
    if (primitive == null)
      dictionary.Remove(new PdfName(key));
    else
      dictionary[key] = primitive;
  }

  internal void SetString(string key, string str)
  {
    if (this[key] is PdfString pdfString)
    {
      pdfString.Value = str;
      this.Modify();
    }
    else
      this[key] = (IPdfPrimitive) new PdfString(str);
  }

  internal bool Archive
  {
    get => this.m_archive;
    set => this.m_archive = value;
  }

  public bool Changed
  {
    get
    {
      if (!this.m_bChanged)
        this.m_bChanged = this.CheckChanges();
      return this.m_bChanged;
    }
  }

  public virtual IPdfPrimitive ClonedObject => (IPdfPrimitive) this.m_clonedObject;

  public int Count => this.m_items.Count;

  internal PdfCrossTable CrossTable => this.m_crossTable;

  internal bool Encrypt
  {
    get => this.m_encrypt;
    set
    {
      this.m_encrypt = value;
      this.Modify();
    }
  }

  internal bool IsDecrypted
  {
    get => this.m_isDecrypted;
    set => this.m_isDecrypted = value;
  }

  public bool IsSaving
  {
    get => this.m_isSaving;
    set => this.m_isSaving = value;
  }

  public IPdfPrimitive this[PdfName key]
  {
    get
    {
      if (key == (PdfName) null)
        throw new ArgumentNullException(nameof (key));
      return this.m_items.ContainsKey(key) ? this.m_items[key] : (IPdfPrimitive) null;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (key == (PdfName) null)
        throw new ArgumentNullException(nameof (key));
      this.m_items[key] = value;
      this.Modify();
    }
  }

  public IPdfPrimitive this[string key]
  {
    get
    {
      return key != null && !(key == string.Empty) ? this[new PdfName(key)] : throw new ArgumentNullException(nameof (key));
    }
    set
    {
      if (key == null || key == string.Empty)
        throw new ArgumentNullException(nameof (key));
      this[this.GetName(key)] = value;
      this.Modify();
    }
  }

  internal Dictionary<PdfName, IPdfPrimitive> Items => this.m_items;

  internal ICollection Keys => (ICollection) this.m_items.Keys;

  public int ObjectCollectionIndex
  {
    get => this.m_index;
    set => this.m_index = value;
  }

  public int Position
  {
    get => this.m_position;
    set => this.m_position = value;
  }

  public ObjectStatus Status
  {
    get => this.m_status;
    set => this.m_status = value;
  }

  public ICollection Values => (ICollection) this.m_items.Values;
}
