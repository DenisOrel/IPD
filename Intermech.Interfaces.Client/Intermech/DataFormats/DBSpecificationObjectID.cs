// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBSpecificationObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений о дочерних объектах спецификации
/// между различными частями системы
/// </summary>
public class DBSpecificationObjectID : 
  DBTypedObjectID,
  IDBSpecificationObjectID,
  IDBTypedObjectID,
  IDBObjectID
{
  /// <summary>Идентификатор связи</summary>
  private long relationID = -1;
  /// <summary>Идентификатор типа связи</summary>
  private int relationTypeID = -1;
  /// <summary>Идентификатор версии родительского объекта</summary>
  private long projID;
  /// <summary>Обозначение объекта (атрибут объекта)</summary>
  private string designation;
  /// <summary>Наименование объекта (атрибут объекта)</summary>
  private string name;
  /// <summary>Зона (атрибут связи)</summary>
  private string zone;
  /// <summary>Позиция (атрибут связи)</summary>
  private string position;
  /// <summary>Формат (атрибут объекта)</summary>
  private string format;
  /// <summary>Количество (атрибут связи)</summary>
  private string quantity;
  /// <summary>Примечание (атрибут связи)</summary>
  private string remark;
  /// <summary>
  /// Идентификатор раздела спецификации, в котором находится объект (атрибут связи)
  /// </summary>
  private long sectionID;

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="caption">Заголовок</param>
  public DBSpecificationObjectID(int objTypeID, long objID, long id, string caption)
    : base(objTypeID, objID, id, caption, 0L, 0L, 0L, string.Empty, 0L)
  {
    this.relationID = -1L;
    this.relationTypeID = -1;
    this.projID = 0L;
    this.designation = string.Empty;
    this.name = string.Empty;
    this.zone = string.Empty;
    this.position = string.Empty;
    this.format = string.Empty;
    this.quantity = string.Empty;
    this.remark = string.Empty;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="relationID">Идентификатор связи</param>
  /// <param name="relationTypeID">Идентификатор типа связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="designation">Обозначение объекта (атрибут объекта)</param>
  /// <param name="name">Наименование объекта (атрибут объекта)</param>
  /// <param name="zone">Зона (атрибут связи)</param>
  /// <param name="position">Позиция (атрибут связи)</param>
  /// <param name="format">Формат (атрибут объекта)</param>
  /// <param name="quantity">Количество (атрибут связи)</param>
  /// <param name="remark">Примечание (атрибут связи)</param>
  /// <param name="sectionID">Идентификатор раздела спецификации, в котором находится объект (атрибут связи)</param>
  /// <param name="version">Номер версии объекта</param>
  /// <param name="baseVersion">Признак базовой версии</param>
  public DBSpecificationObjectID(
    int objTypeID,
    long objID,
    long id,
    string caption,
    long relationID,
    int relationTypeID,
    long projID,
    string designation,
    string name,
    string zone,
    string position,
    string format,
    string quantity,
    string remark,
    long sectionID,
    long version,
    long baseVersion)
    : base(objTypeID, objID, id, caption, 0L, version, baseVersion, string.Empty, 0L)
  {
    this.relationID = relationID;
    this.relationTypeID = relationTypeID;
    this.projID = projID;
    this.designation = designation;
    this.name = name;
    this.zone = zone;
    this.position = position;
    this.format = format;
    this.quantity = quantity;
    this.remark = remark;
    this.sectionID = sectionID;
  }

  /// <summary>Конструктор</summary>
  /// <param name="session">Сессия</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="relationID">Идентификатор связи</param>
  public DBSpecificationObjectID(IUserSession session, long objID, long relationID)
    : base(-1, objID, -1L, string.Empty, 0L, 0L, 0L, string.Empty, 0L)
  {
    this.Load(session, objID, relationID);
  }

  /// <summary>
  /// Загрузить информацию из базы данных в экземпляр класса
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="relationID">Идентификатор связи</param>
  public void Load(IUserSession session, long objID, long relationID)
  {
    if (session == null)
      return;
    this.relationID = relationID;
    IDBObject dbObject = session.GetObject(objID);
    this._id = dbObject.ID;
    this._caption = dbObject.Caption;
    this._objID = dbObject.ObjectID;
    this._objTypeID = dbObject.ObjectType;
    this._owner = dbObject.OwnerID;
    this._version = (long) dbObject.VersionID;
    this._baseVersion = Convert.ToInt64(dbObject.IsBaseVersion);
    this.designation = this.GetAttrAsString(dbObject, "cad0001f-306c-11d8-b4e9-00304f19f545");
    this.name = this.GetAttrAsString(dbObject, "cad00020-306c-11d8-b4e9-00304f19f545");
    this.format = this.GetAttrAsString(dbObject, "cad00255-306c-11d8-b4e9-00304f19f545");
    if (relationID != -1L)
    {
      IDBRelation relation = session.GetRelation(relationID);
      this.projID = relation.ProjID;
      this.relationTypeID = relation.RelationType;
      this.zone = this.GetAttrAsString(relation, "cad0027a-306c-11d8-b4e9-00304f19f545");
      this.position = this.GetAttrAsString(relation, "cad00270-306c-11d8-b4e9-00304f19f545");
      this.quantity = this.GetAttrAsString(relation, "cad00267-306c-11d8-b4e9-00304f19f545");
      this.remark = this.GetAttrAsString(relation, "cad00021-306c-11d8-b4e9-00304f19f545");
      this.sectionID = this.GetAttrAsInt64(relation, "cad00266-306c-11d8-b4e9-00304f19f545");
    }
    else
    {
      this.projID = -1L;
      this.relationTypeID = -1;
      this.zone = (string) null;
      this.position = (string) null;
      this.quantity = (string) null;
      this.remark = (string) null;
      this.sectionID = -1L;
    }
  }

  /// <summary>
  /// Получить значение указанного атрибута объекта в виде строки
  /// </summary>
  /// <param name="obj">Объект</param>
  /// <param name="attrGuid">Идентификатор атрибута</param>
  /// <returns>Значение атрибута в виде строки</returns>
  protected virtual string GetAttrAsString(IDBObject obj, string attrGuid)
  {
    string attrAsString = string.Empty;
    IDBAttribute attributeById = obj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(attrGuid));
    if (attributeById != null)
      attrAsString = attributeById.AsString;
    return attrAsString;
  }

  /// <summary>
  /// Получить значение указанного атрибута объекта в виде Int64
  /// </summary>
  /// <param name="obj">Объект</param>
  /// <param name="attrGuid">Идентификатор атрибута</param>
  /// <returns>Значение атрибута в виде Int64</returns>
  protected virtual long GetAttrAsInt64(IDBObject obj, string attrGuid)
  {
    object empty = (object) string.Empty;
    IDBAttribute attributeById = obj.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(attrGuid));
    if (attributeById != null)
      empty = attributeById.Value;
    long result = 0;
    if (empty != null && empty != DBNull.Value)
      long.TryParse(empty.ToString(), out result);
    return result;
  }

  /// <summary>
  /// Получить значение указанного атрибута связи в виде строки
  /// </summary>
  /// <param name="rel">Связь</param>
  /// <param name="attrGuid">Идентификатор атрибута</param>
  /// <returns>Значение атрибута в виде строки</returns>
  protected virtual string GetAttrAsString(IDBRelation rel, string attrGuid)
  {
    string attrAsString = string.Empty;
    IDBAttribute attributeById = rel.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(attrGuid));
    if (attributeById != null)
      attrAsString = attributeById.AsString;
    return attrAsString;
  }

  /// <summary>
  /// Получить значение указанного атрибута связи в виде Int64
  /// </summary>
  /// <param name="rel">Связь</param>
  /// <param name="attrGuid">Идентификатор атрибута</param>
  /// <returns>Значение атрибута в виде Int64</returns>
  protected virtual long GetAttrAsInt64(IDBRelation rel, string attrGuid)
  {
    object empty = (object) string.Empty;
    IDBAttribute attributeById = rel.GetAttributeByID(MetaDataHelper.GetAttributeTypeID(attrGuid));
    if (attributeById != null)
      empty = attributeById.Value;
    long result = 0;
    if (empty != null && empty != DBNull.Value)
      long.TryParse(empty.ToString(), out result);
    return result;
  }

  /// <summary>Идентификатор связи</summary>
  public long RelationID
  {
    [DebuggerStepThrough] get => this.relationID;
  }

  /// <summary>Идентификатор типа связи</summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough] get => this.relationTypeID;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjID
  {
    [DebuggerStepThrough] get => this.projID;
  }

  /// <summary>Обозначение объекта (атрибут объекта)</summary>
  public string Designation
  {
    [DebuggerStepThrough] get => this.designation;
    set => this.designation = value;
  }

  /// <summary>Наименование объекта (атрибут объекта)</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    set => this.name = value;
  }

  /// <summary>Зона (атрибут связи)</summary>
  public string Zone
  {
    [DebuggerStepThrough] get => this.zone;
    set => this.zone = value;
  }

  /// <summary>Позиция (атрибут связи)</summary>
  public string Position
  {
    [DebuggerStepThrough] get => this.position;
    set => this.position = value;
  }

  /// <summary>Формат (атрибут объекта)</summary>
  public string Format
  {
    [DebuggerStepThrough] get => this.format;
    set => this.format = value;
  }

  /// <summary>Количество (атрибут связи)</summary>
  public string Quantity
  {
    [DebuggerStepThrough] get => this.quantity;
    set => this.quantity = value;
  }

  /// <summary>Примечание (атрибут связи)</summary>
  public string Remark
  {
    [DebuggerStepThrough] get => this.remark;
    set => this.remark = value;
  }

  /// <summary>
  /// Идентификатор раздела спецификации, в котором находится объект (атрибут связи)
  /// </summary>
  public long SectionID
  {
    [DebuggerStepThrough] get => this.sectionID;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is DBSpecificationObjectID specificationObjectId && this.ObjectID == specificationObjectId.ObjectID && this.RelationID == specificationObjectId.RelationID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  [DebuggerStepThrough]
  public override int GetHashCode()
  {
    long num1 = this.ObjectID;
    int num2 = num1.GetHashCode() << 16 /*0x10*/;
    num1 = this.RelationID;
    int hashCode = num1.GetHashCode();
    return num2 | hashCode;
  }
}
