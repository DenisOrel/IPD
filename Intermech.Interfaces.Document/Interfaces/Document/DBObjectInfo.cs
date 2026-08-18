// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DBObjectInfo
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Информация об идентификаторе объекта БД</summary>
[Serializable]
public class DBObjectInfo : DBObjectInfoBase
{
  private Guid objectGuid = Guid.Empty;
  private long objectID = -1;
  private int objectType = -1;
  private string objectCaption;

  /// <summary>Глобальный идентификатор объекта</summary>
  public override Guid ObjectGuid => this.objectGuid;

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectGuid</summary>
  public override void AssignObjectGuid(Guid objectGuid) => this.objectGuid = objectGuid;

  /// <summary>Идентификатор объекта</summary>
  public override long ObjectID
  {
    get => this.objectID;
    set => this.objectID = value;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectID</summary>
  public override void AssignObjectID(long objectID) => this.objectID = objectID;

  /// <summary>Тип объекта</summary>
  public override int ObjectType => this.objectType;

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectType</summary>
  public override void AssignObjectType(int objectType) => this.objectType = objectType;

  /// <summary>Заголовок объекта</summary>
  public override string ObjectCaption => this.objectCaption;

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectCaption</summary>
  public override void AssignObjectCaption(string objectCaption)
  {
    this.objectCaption = objectCaption;
  }

  /// <summary>Конструктор</summary>
  public DBObjectInfo()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public DBObjectInfo(Guid objectGuid, long objectID = -1, int objectType = -1, string objectCaption = null)
  {
    this.SetDBObjectInfo(objectGuid, objectID, objectType, objectCaption);
  }

  /// <summary>Назначить информацию о связи и объекте БД</summary>
  /// <param name="relationGuid">Глобальный идентификатор связи</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="projGuid">Глобальный идентификатор версии объекта проекта</param>
  /// <param name="projID">Идентификатор версии объекта проекта</param>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public override void SetDBRelationInfo(
    Guid relationGuid,
    long relationID,
    int relationType,
    Guid projGuid,
    long projID,
    Guid objectGuid,
    long objectID,
    int objectType,
    string objectCaption)
  {
    this.SetDBObjectInfo(objectGuid, objectID, objectType, objectCaption);
  }

  /// <summary>Назначить информацию об объекте БД</summary>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public override void SetDBObjectInfo(
    Guid objectGuid,
    long objectID,
    int objectType,
    string objectCaption)
  {
    this.objectGuid = objectGuid;
    this.objectID = objectID;
    this.objectType = objectType;
    this.objectCaption = objectCaption;
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (!(this.objectGuid != Guid.Empty))
      return;
    xw.WriteAttributeString("objGuid", this.objectGuid.ToString());
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "objGuid":
        this.objectGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "caption":
        this.objectCaption = readArgs.Reader.Value;
        return true;
      case "objID":
        this.objectID = long.Parse(readArgs.Reader.Value);
        return true;
      case "objType":
        this.objectType = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  /// <summary>Клонировать экземпляр объекта</summary>
  public override DBObjectInfoBase Clone()
  {
    return (DBObjectInfoBase) new DBObjectInfo(this.objectGuid, this.objectID, this.objectType, this.objectCaption);
  }
}
