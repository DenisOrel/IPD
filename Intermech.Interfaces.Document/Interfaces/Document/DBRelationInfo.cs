// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DBRelationInfo
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Информация о связи и объекте БД</summary>
[Serializable]
public class DBRelationInfo : DBObjectInfo
{
  private Guid relationGuid = Guid.Empty;
  private long relationID = -1;
  private int relationType = -1;
  private Guid projGuid = Guid.Empty;
  private long projID = -1;

  /// <summary>Глобальный идентификатор связи</summary>
  public override Guid RelationGuid => this.relationGuid;

  /// <summary>Только для внутреннего использования. Назначить свойство RelationGuid</summary>
  public override void AssignRelationGuid(Guid relationGuid) => this.relationGuid = relationGuid;

  /// <summary>Идентификатор связи</summary>
  public override long RelationID => this.relationID;

  /// <summary>Только для внутреннего использования. Назначить свойство RelationID</summary>
  public override void AssignRelationID(long relationID) => this.relationID = relationID;

  /// <summary>Тип связи</summary>
  public override int RelationType => this.relationType;

  /// <summary>Только для внутреннего пользования. Установить новое значение типа. Обязательно проследить за остальными полями!</summary>
  public void SetRelationType(int value) => this.relationType = value;

  /// <summary>Только для внутреннего использования. Назначить свойство RelationType</summary>
  public override void AssignRelationType(int relationType) => this.relationType = relationType;

  /// <summary>Идентификатор родительского объекта для связи</summary>
  public override long ProjID
  {
    [DebuggerStepThrough] get => this.projID;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ProjID</summary>
  public override void AssignProjID(long projID) => this.projID = projID;

  /// <summary>Guid родительского объекта для связи</summary>
  public override Guid ProjGuid
  {
    [DebuggerStepThrough] get => this.projGuid;
  }

  /// <summary>Только для внутреннего использования. Назначить свойство ProjGuid</summary>
  public override void AssignProjGuid(Guid projGuid) => this.projGuid = projGuid;

  /// <summary>Конструктор</summary>
  public DBRelationInfo()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="relationGuid">Guid связи</param>
  /// <param name="objectVersionGuid">Guid версии объекта.
  /// Заполняется, когда нужно хранить для какой версии объекта по связи были сохранены данные в документе</param>
  public DBRelationInfo(Guid relationGuid, Guid objectVersionGuid)
  {
    this.SetDBRelationInfo(relationGuid, -1L, -1, Guid.Empty, -1L, objectVersionGuid, -1L, -1, (string) null);
  }

  /// <summary>Конструктор</summary>
  /// <param name="relationGuid">Глобальный идентификатор связи</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="projGuid">Глобальный идентификатор версии объекта проекта</param>
  /// <param name="projID">Идентификатор версии объекта проекта</param>
  public DBRelationInfo(Guid relationGuid, long relationID, Guid projGuid, long projID)
  {
    this.SetDBRelationInfo(relationGuid, relationID, -1, projGuid, projID, Guid.Empty, -1L, -1, (string) null);
  }

  /// <summary>Конструктор</summary>
  /// <param name="relationGuid">Глобальный идентификатор связи</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="relationType">Тип связи</param>
  /// <param name="projGuid">Глобальный идентификатор версии объекта проекта</param>
  /// <param name="projID">Идентификатор версии объекта проекта</param>
  /// <param name="objectGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectCaption">Заголовок объекта</param>
  public DBRelationInfo(
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
    this.SetDBRelationInfo(relationGuid, relationID, relationType, projGuid, projID, objectGuid, objectID, objectType, objectCaption);
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
    this.relationGuid = relationGuid;
    this.relationID = relationID;
    this.relationType = relationType;
    this.projGuid = projGuid;
    this.projID = projID;
    this.SetDBObjectInfo(objectGuid, objectID, objectType, objectCaption);
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  protected override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    if (this.relationGuid != Guid.Empty)
      xw.WriteAttributeString("relGuid", this.relationGuid.ToString());
    if (!(this.projGuid != Guid.Empty))
      return;
    xw.WriteAttributeString("projGuid", this.projGuid.ToString());
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "relGuid":
        this.relationGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "projGuid":
        this.projGuid = new Guid(readArgs.Reader.Value);
        return true;
      case "relID":
        this.relationID = long.Parse(readArgs.Reader.Value);
        return true;
      case "relType":
        this.relationType = int.Parse(readArgs.Reader.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      default:
        return base.ReadFieldFromXml(readArgs);
    }
  }

  /// <summary>Клонировать экземпляр объекта</summary>
  public override DBObjectInfoBase Clone()
  {
    return (DBObjectInfoBase) new DBRelationInfo(this.relationGuid, this.relationID, this.relationType, this.projGuid, this.projID, this.ObjectGuid, this.ObjectID, this.ObjectType, this.ObjectCaption);
  }
}
