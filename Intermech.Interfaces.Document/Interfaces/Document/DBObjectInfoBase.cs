// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DBObjectInfoBase
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Базовый класс. Информация об идентификаторе объекта и/или связи БД</summary>
[Serializable]
public class DBObjectInfoBase : IWriteReadXml, ICloneable
{
  /// <summary>Глобальный идентификатор версии объекта</summary>
  public virtual Guid ObjectGuid
  {
    [DebuggerStepThrough] get => Guid.Empty;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectGuid</summary>
  public virtual void AssignObjectGuid(Guid objectGuid)
  {
  }

  /// <summary>Идентификатор версии объекта</summary>
  public virtual long ObjectID
  {
    [DebuggerStepThrough] get => -1;
    set
    {
    }
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectID</summary>
  public virtual void AssignObjectID(long objectID)
  {
  }

  /// <summary>Тип объекта</summary>
  public virtual int ObjectType
  {
    [DebuggerStepThrough] get => -1;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectType</summary>
  public virtual void AssignObjectType(int objectType)
  {
  }

  /// <summary>Заголовок объекта</summary>
  public virtual string ObjectCaption
  {
    [DebuggerStepThrough] get => (string) null;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ObjectCaption</summary>
  public virtual void AssignObjectCaption(string objectCaption)
  {
  }

  /// <summary>Глобальный идентификатор связи</summary>
  public virtual Guid RelationGuid
  {
    [DebuggerStepThrough] get => Guid.Empty;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство RelationGuid</summary>
  public virtual void AssignRelationGuid(Guid relationGuid)
  {
  }

  /// <summary>Идентификатор связи</summary>
  public virtual long RelationID
  {
    [DebuggerStepThrough] get => -1;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство RelationID</summary>
  public virtual void AssignRelationID(long relationID)
  {
  }

  /// <summary>Тип связи</summary>
  public virtual int RelationType
  {
    [DebuggerStepThrough] get => -1;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство RelationType</summary>
  public virtual void AssignRelationType(int relationType)
  {
  }

  /// <summary>Идентификатор родительского объекта для связи</summary>
  public virtual long ProjID
  {
    [DebuggerStepThrough] get => -1;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ProjID</summary>
  public virtual void AssignProjID(long projID)
  {
  }

  /// <summary>Guid родительского объекта для связи</summary>
  public virtual Guid ProjGuid
  {
    [DebuggerStepThrough] get => Guid.Empty;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ProjGuid</summary>
  public virtual void AssignProjGuid(Guid projGuid)
  {
  }

  /// <summary>Назначить информацию об объекте БД</summary>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public virtual void SetDBObjectInfo(
    Guid objectGuid,
    long objectID,
    int objectType,
    string objectCaption)
  {
  }

  /// <summary>Назначить информацию об объекте БД</summary>
  /// <param name="relationGuid">Глобальный идентификатор связи</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="projGuid">Глобальный идентификатор версии объекта проекта</param>
  /// <param name="projID">Идентификатор версии объекта проекта</param>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public virtual void SetDBRelationInfo(
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
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected virtual void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
  }

  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    xw.WriteStartElement(elementName);
    this.WriteXmlAttributes(xw, objectRefId);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить ссылку из XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public virtual bool ReadFieldFromXml(XmlReadArgs readArgs) => false;

  /// <summary>Клонировать экземпляр объекта</summary>
  /// <returns></returns>
  public virtual DBObjectInfoBase Clone() => (DBObjectInfoBase) null;

  /// <summary>Клонировать экземпляр объекта</summary>
  object ICloneable.Clone() => (object) this.Clone();
}
