// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifRazdelNumbering
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary> Коллекция настроек нумерации позиций для разделов </summary>
public class SpecifRazdelNumbering : ICloneable, IWriteReadXml
{
  private const string _XmlNodeName = "SpecifRazdelNumbering";
  private SpecifNumberingFull _SpecifNumberingFull;
  private Dictionary<long, SpecifNumbering> _RazdelIDKeySpecifNumberingValueHash = new Dictionary<long, SpecifNumbering>();
  private bool _Changed;

  public SpecifRazdelNumbering()
  {
  }

  public SpecifRazdelNumbering(SpecifNumberingFull specifNumberingFull)
  {
    this._SpecifNumberingFull = specifNumberingFull;
  }

  /// <summary> Ссылка на полную схему настроек нумерации </summary>
  public SpecifNumberingFull SpecifNumberingFull => this._SpecifNumberingFull;

  /// <summary> Хэш таблица. Ключ - тип объекта, значение - схема нумерации позиций (неполная) </summary>
  public Dictionary<long, SpecifNumbering> RazdelIDKeySpecifNumberingValueHash
  {
    get => this._RazdelIDKeySpecifNumberingValueHash;
  }

  public SpecifNumbering GetSpecifNumbering(SpecificationSection section)
  {
    return this.RazdelIDKeySpecifNumberingValueHash != null && this.RazdelIDKeySpecifNumberingValueHash.ContainsKey(section.SectionID) ? this.RazdelIDKeySpecifNumberingValueHash[section.SectionID] : (SpecifNumbering) null;
  }

  /// <summary> Признак того, что список был изменён </summary>
  public bool Changed
  {
    get => this._Changed;
    set => this._Changed = value;
  }

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultSchema()
  {
    if (this._SpecifNumberingFull == null)
      return;
    if (this._SpecifNumberingFull.ParentLevel == null)
      this._RazdelIDKeySpecifNumberingValueHash.Clear();
    else
      this.CopyParamsFrom(this._SpecifNumberingFull.ParentLevel.SpecifRazdelNumbering);
    this._Changed = this._SpecifNumberingFull != null && this._SpecifNumberingFull.ParentLevel == null;
  }

  /// <summary> Очистка схемы нумерации </summary>
  public void Clear()
  {
    this.LoadDefaultSchema();
    this._Changed = false;
  }

  /// <summary> Получить Guid раздела спецификации по его идентификатору </summary>
  /// <param name="razdelID"> Идентификатор раздела спецификации </param>
  /// <returns> Guid раздела спецификации </returns>
  public static Guid GetRazdelGuidByID(long razdelID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(razdelID);
        return dbObject != null && dbObject.ObjectType == AvsIDCache.ObjType_SpecificationSection ? (dbObject as IDBGuid).GUID : Guid.Empty;
      }
      catch
      {
        return Guid.Empty;
      }
    }
  }

  /// <summary> Получить идентификатор раздела спецификации по его Guid-у </summary>
  /// <param name="razdelGuid"> Guid раздела спецификации </param>
  /// <returns> Идентификатор раздела спецификации </returns>
  public static long GetRazdelIDByGuid(Guid razdelGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(razdelGuid);
        return dbObject != null && dbObject.ObjectType == AvsIDCache.ObjType_SpecificationSection ? dbObject.ObjectID : 0L;
      }
      catch
      {
        return 0;
      }
    }
  }

  /// <summary> Сделать полную копию схемы </summary>
  /// <returns> Копия схемы </returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary> Сделать полную копию схемы </summary>
  /// <returns> Копия схемы </returns>
  public SpecifRazdelNumbering Clone()
  {
    SpecifRazdelNumbering specifRazdelNumbering = new SpecifRazdelNumbering(this._SpecifNumberingFull);
    specifRazdelNumbering.CopyParamsFrom(this);
    return specifRazdelNumbering;
  }

  /// <summary> Копировать параметры из другой схемы </summary>
  /// <returns> Копия схемы </returns>
  public void CopyParamsFrom(SpecifRazdelNumbering copy)
  {
    this.RazdelIDKeySpecifNumberingValueHash.Clear();
    foreach (KeyValuePair<long, SpecifNumbering> keyValuePair in copy.RazdelIDKeySpecifNumberingValueHash)
      this.RazdelIDKeySpecifNumberingValueHash[keyValuePair.Key] = keyValuePair.Value.Clone();
    this._Changed = copy.Changed;
  }

  /// <summary>Прочитать одно поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  bool IWriteReadXml.ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (!(readArgs.Reader.LocalName == nameof (SpecifRazdelNumbering)))
      return false;
    this._RazdelIDKeySpecifNumberingValueHash = new Dictionary<long, SpecifNumbering>();
    HybridDictionary hybridDictionary = new HybridDictionary();
    WriteReadXmlHelper.ReadDictionaryFromXml((IDictionary) hybridDictionary, typeof (Guid), typeof (SpecifNumbering), readArgs);
    foreach (DictionaryEntry dictionaryEntry in hybridDictionary)
    {
      long razdelIdByGuid = SpecifRazdelNumbering.GetRazdelIDByGuid((Guid) dictionaryEntry.Key);
      if (this._RazdelIDKeySpecifNumberingValueHash.ContainsKey(razdelIdByGuid))
        this._RazdelIDKeySpecifNumberingValueHash[razdelIdByGuid] = (SpecifNumbering) dictionaryEntry.Value;
      else
        this._RazdelIDKeySpecifNumberingValueHash.Add(razdelIdByGuid, (SpecifNumbering) dictionaryEntry.Value);
    }
    foreach (SpecifNumbering specifNumbering in this._RazdelIDKeySpecifNumberingValueHash.Values)
      specifNumbering.ParentLevel = (SpecifNumbering) this._SpecifNumberingFull;
    this.Changed = true;
    return true;
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  void IWriteReadXml.WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    if (!this.Changed)
      return;
    xw.WriteStartElement(elementName);
    try
    {
      HybridDictionary hybridDictionary = new HybridDictionary();
      foreach (KeyValuePair<long, SpecifNumbering> keyValuePair in this._RazdelIDKeySpecifNumberingValueHash)
        hybridDictionary[(object) SpecifRazdelNumbering.GetRazdelGuidByID(keyValuePair.Key)] = (object) keyValuePair.Value;
      WriteReadXmlHelper.WriteDictionaryToXml(nameof (SpecifRazdelNumbering), (IDictionary) hybridDictionary, "Razdel", "SpecifNumbering", typeof (Guid), typeof (SpecifNumbering), xw, objectRefId);
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  void IWriteReadXml.ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
    if (!this.Changed)
      this.LoadDefaultSchema();
    object numberingValueHash = (object) this._RazdelIDKeySpecifNumberingValueHash;
    DictionaryHelper.ValidateDictionary(ref numberingValueHash, new DictionaryHelper.ValidateItemDelegate(this.ValidateSection));
    this._RazdelIDKeySpecifNumberingValueHash = (Dictionary<long, SpecifNumbering>) numberingValueHash;
  }

  /// <summary> Процедура специальных настроек нумерации для некоторого раздела спецификации </summary>
  /// <param name="dictionaryEntry"> Проверяемый элемент словаря </param>
  /// <returns> true если валидация прошла успешно, иначе - false </returns>
  public bool ValidateSection(DictionaryEntry dictionaryEntry)
  {
    return dictionaryEntry.Key != null && dictionaryEntry.Value != null && (long) dictionaryEntry.Key != 0L;
  }
}
