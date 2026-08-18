// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AdditionalChapterSettings
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Interfaces.Document;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Настройки дополнительных частей</summary>
public class AdditionalChapterSettings : IWriteReadXml, ICloneable
{
  /// <summary>Идентификатор части</summary>
  public Guid ChapterGuid;
  /// <summary>Идентификатор части</summary>
  public long ChapterID;
  public long SortIndex;
  /// <summary>Заголовок части</summary>
  public string Caption;

  /// <summary>Конструктор</summary>
  public AdditionalChapterSettings()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="chapterGuid">Идентификатор части</param>
  /// <param name="caption">Заголовок части</param>
  public AdditionalChapterSettings(
    Guid chapterGuid,
    long chapterID,
    string caption,
    long sortIndex)
  {
    this.ChapterGuid = chapterGuid;
    this.Caption = caption;
    this.ChapterID = chapterID;
    this.SortIndex = sortIndex;
  }

  /// <summary>Преобразовать класс в строковое представление</summary>
  /// <returns></returns>
  public override string ToString() => this.Caption;

  object ICloneable.Clone() => (object) this.Clone();

  public AdditionalChapterSettings Clone()
  {
    return new AdditionalChapterSettings(this.ChapterGuid, this.ChapterID, this.Caption, this.SortIndex);
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("chapterID", this.ChapterGuid.ToString());
    xw.WriteAttributeString("caption", this.Caption);
    xw.WriteAttributeString("sortIndex", this.SortIndex.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    xw.WriteEndElement();
  }

  /// <summary>Прочитать поле из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле было прочитано</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "chapterID":
        this.ChapterGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "caption":
        this.Caption = readArgs.Reader.Value;
        return true;
      case "sortIndex":
        long.TryParse(readArgs.Reader.Value, out this.SortIndex);
        return true;
      default:
        return false;
    }
  }

  /// <summary>Загрузить из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public virtual void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }
}
