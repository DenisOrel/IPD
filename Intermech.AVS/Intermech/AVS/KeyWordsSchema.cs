// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.KeyWordsSchema
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

/// <summary>Схема ключевых слов для материалов в спецификации </summary>
public class KeyWordsSchema : SettingsSchemeBase, ICloneable, IWriteReadXml
{
  public static Dictionary<long, KeyWordsSchema> MaterialKeyWordsCache = new Dictionary<long, KeyWordsSchema>(100);
  private long _ownerObjectID = -1;
  private KeyWordsSchema _parent;
  private SettingsLevel _level;
  private KeyWordsList words;
  private bool _readOnly;

  public KeyWordsSchema(KeyWordsSchema parent, long ownerObjectID, SettingsLevel level)
  {
    this._parent = parent;
    this._level = level;
    this._ownerObjectID = ownerObjectID;
    this.words = new KeyWordsList(this);
    this.LoadParams();
  }

  /// <summary> Идентификатор объекта, в атрибутах которого хранится схема </summary>
  public long OwnerObjectID
  {
    get => this._ownerObjectID;
    set
    {
      this._ownerObjectID = value;
      this.LoadParams();
    }
  }

  /// <summary> Ссылка на вышестоящий уровень настроек </summary>
  public KeyWordsSchema Parent => this._parent;

  /// <summary> Ссылка на дескриптор уровня настроек </summary>
  public SettingsLevel Level => this._level;

  /// <summary> Признак того, что схема доступна только для чтения </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set => this._readOnly = value;
  }

  /// <summary> Ключевые слова </summary>
  public KeyWordsList KeyWords
  {
    get
    {
      if (this.words == null)
        this.words = new KeyWordsList();
      KeyWordsList keyWords = this.words.Clone();
      KeyWordsList keyWordsList = (KeyWordsList) null;
      if (this._parent != null)
        keyWordsList = this._parent.KeyWords;
      if (this._parent == null || keyWordsList == null)
      {
        keyWords = this.words.Clone();
      }
      else
      {
        keyWords.Clear();
        for (int index = 0; index < keyWordsList.Count; ++index)
        {
          string str = keyWordsList[index];
          if (!this.words.Contains(str + "~d"))
            keyWords.Add(str);
        }
        for (int index = 0; index < this.words.Count; ++index)
        {
          string word = this.words[index];
          if (!word.EndsWith("~d") && !keyWords.Contains(word))
            keyWords.Add(word);
        }
      }
      keyWords.Sort();
      return keyWords;
    }
  }

  public bool SetKeyWord(string oldValue, string newValue)
  {
    if (newValue == null || newValue.Trim() == "")
      return false;
    if (this.words != null && this.words.Contains(oldValue))
    {
      this.words[this.words.IndexOf(oldValue)] = newValue;
      return true;
    }
    if (!this.Parent.KeyWords.Contains(oldValue))
      return false;
    this.AddKeyWord(oldValue + "~d", true);
    this.AddKeyWord(newValue, true);
    return true;
  }

  public bool RemoveKeyWord(string word)
  {
    if (!this.KeyWords.Contains(word))
      return false;
    if (this._parent != null && this._parent.KeyWords.Contains(word))
    {
      if (this.words == null)
        this.words = new KeyWordsList();
      if (!this.words.Contains(word))
        this.words.Add(word + "~d");
      else
        this.words[this.words.IndexOf(word)] = word + "~d";
    }
    else
      this.words.Remove(word);
    return true;
  }

  public bool AddKeyWord(string word, bool check)
  {
    if (this.words == null)
      this.words = new KeyWordsList(this);
    bool flag = true;
    if (check && (word == null || word.Trim() == ""))
      flag = false;
    if (flag)
      this.words.Add(word);
    return true;
  }

  /// <summary> Признак того, что параметр имеет собственное значение, что он не унаследован </summary>
  public bool KeyWordsChanged => this._parent != null && this.words != null;

  /// <summary> Загрузка схемы по-умолчанию </summary>
  public void LoadDefaultParams()
  {
    if (this._parent == null)
    {
      this.words = new KeyWordsList(this);
      this.words.Add("Двутавр");
      this.words.Add("Квадрат");
      this.words.Add("Круг");
      this.words.Add("Лента");
      this.words.Add("Лист");
      this.words.Add("Полоса");
      this.words.Add("Профиль");
      this.words.Add("Рулон");
      this.words.Add("Тавр");
      this.words.Add("Труба");
      this.words.Add("Уголок");
      this.words.Add("Швеллер");
      this.words.Add("Шестигранник");
    }
    else
      this.words = new KeyWordsList(this);
  }

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Сделать полную копию схемы</summary>
  /// <returns>Копия схемы</returns>
  public KeyWordsSchema Clone()
  {
    KeyWordsSchema keyWordsSchema = new KeyWordsSchema(this._parent, this._ownerObjectID, this._level);
    keyWordsSchema.CopyParamsFrom(this);
    return keyWordsSchema;
  }

  public bool IsOwnWord(string word) => this.words.Contains(word);

  /// <summary> Скопировать параметры из другого объекта того же типа </summary>
  /// <param name="copy"> Объект, чьи параметры нужно копировать </param>
  public void CopyParamsFrom(KeyWordsSchema copy) => this.words = copy.words.Clone();

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (readArgs.Reader.LocalName.Contains("KeyWord"))
    {
      if (readArgs.Reader.LocalName == "KeyWord0")
        this.words.Clear();
      if (readArgs.Reader.HasValue)
        this.AddKeyWord(readArgs.Reader.Value, true);
    }
    return false;
  }

  /// <summary> Записать поля в XML </summary>
  /// <param name="elementName"> Имя элемента XML </param>
  /// <param name="xw"> XmlWriter </param>
  /// <param name="objectRefId"> Генератор идентификаторов </param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    try
    {
      int num = 0;
      for (int index = 0; index < this.words.Count; ++index)
      {
        string word = this.words[index];
        if (word != null && word.Trim() != "")
        {
          xw.WriteAttributeString("KeyWord" + num.ToString(), word);
          ++num;
        }
      }
    }
    finally
    {
      xw.WriteEndElement();
    }
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  protected override void SaveToXmlDocument(MemoryStream stream)
  {
    WriteReadXmlHelper.WriteXmlDocument((Stream) stream, (IWriteReadXml) this, nameof (KeyWordsSchema));
  }

  /// <summary>Загрузка параметров из объекта с guid-ом = OwnerGuid</summary>
  public void LoadParams()
  {
    if (this.OwnerObjectID.IsUndefinedId())
      return;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this.OwnerObjectID, true);
        IDBAttribute attributeById = objectActual.GetAttributeByID(AvsIDCache.Attr_MaterialKeyWordsSchema);
        if (attributeById != null)
        {
          new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          aDestStream.Position = 0L;
          if (aDestStream.Length != 0L)
            WriteReadXmlHelper.LoadFromXmlDocument(sessionKeeper.Session, (Stream) aDestStream, (IWriteReadXml) this, nameof (KeyWordsSchema));
          this._readOnly = attributeById.ReadOnly && objectActual.ObjectID > 0L && objectActual.CheckoutBy != 0L;
        }
        else
          this._readOnly = AvsIDCache.Attr_MaterialKeyWordsSchema == -1;
        if (this._readOnly || objectActual.ObjectModifyMode != ObjectModifyModes.CantModify && objectActual.ObjectModifyMode != ObjectModifyModes.CreateVersion)
          return;
        this._readOnly = true;
      }
    }
    finally
    {
      aDestStream.Close();
    }
  }

  /// <summary> Сохранение параметров в объект с guid-ом = OwnerGuid </summary>
  public void SaveParams()
  {
    if (this.ReadOnly || AvsIDCache.Attr_MaterialKeyWordsSchema == -1)
      return;
    this.SaveParamsDataToObjectAttribute(this.OwnerObjectID, AvsIDCache.Attr_MaterialKeyWordsSchema);
  }

  /// <summary> Получить схему сортировки по уровню настроек </summary>
  /// <param name="level"> Уровень настроек </param>
  /// <returns> Схема сортировки </returns>
  public KeyWordsSchema GetSchemaByLevel(SettingsLevel level)
  {
    if (this._level == level)
      return this;
    return this._parent != null ? this._parent.GetSchemaByLevel(level) : (KeyWordsSchema) null;
  }
}
