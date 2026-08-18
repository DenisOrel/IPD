// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBRelationID
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
/// Объект-формат для передачи сведений об идентификаторах связей
/// между различными частями системы. Доступ к передаваемой
/// информации осуществляется через интерфейс IDBRelationID.
/// </summary>
public class DBRelationID : IDBRelationID
{
  /// <summary>Идентификатор связи между объектами</summary>
  private long _relationID;
  /// <summary>
  /// Идентификатор версии объекта, входящего по этой связи в другой объект
  /// </summary>
  private long _partID;
  /// <summary>Идентификатор типа связи</summary>
  private int _relationType;
  /// <summary>Значение атрибута "Сортировка"</summary>
  protected long _sorting;
  /// <summary>Идентификатор версии родительского объекта</summary>
  protected long _projID;
  /// <summary>Guid связи</summary>
  protected Guid _relGuid;

  /// <summary>Создать экземпляр объекта</summary>
  /// <param name="relationID">Идентификатор связи между объектами</param>
  /// <param name="partID">Идентификатор версии объекта, входящего по этой связи в другой объект</param>
  /// <param name="relTypeID">Идентификатор типа связи или -1, если требуется определять её автоматически</param>
  /// <param name="sorting">Значение атрибута "Сортировка"</param>
  /// <param name="relGuid">Guid связи</param>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  public DBRelationID(
    long relationID,
    long partID,
    int relTypeID,
    long sorting,
    Guid relGuid,
    long projID)
  {
    this._relationID = relationID;
    this._partID = partID;
    this._relationType = relTypeID;
    this._sorting = sorting;
    this._relGuid = relGuid;
    this._projID = projID;
    if (relationID == 0L || this._relationType != -1)
      return;
    this._relationType = MetaDataHelper.GetRelationType4PrjLinkID((IUserSession) null, relationID);
    if (this._relationType != -1 || relationID < 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._relationType = MetaDataHelper.GetRelationType4PrjLinkID(sessionKeeper.Session, relationID);
  }

  /// <summary>Идентификатор связи между объектами</summary>
  public long Value
  {
    [DebuggerStepThrough] get => this._relationID;
  }

  /// <summary>
  /// Идентификатор версии объекта, входящего по этой связи в другой объект
  /// </summary>
  public long PartID
  {
    [DebuggerStepThrough] get => this._partID;
  }

  /// <summary>Идентификатор типа связи</summary>
  public int RelationType
  {
    [DebuggerStepThrough] get => this._relationType;
  }

  /// <summary>Значение атрибута "Сортировка"</summary>
  public long Sorting
  {
    [DebuggerStepThrough] get => this._sorting;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjID
  {
    [DebuggerStepThrough] get => this._projID;
  }

  /// <summary>Guid связи</summary>
  public Guid RelGuid
  {
    [DebuggerStepThrough] get => this._relGuid;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is DBRelationID))
      return false;
    DBRelationID dbRelationId = (DBRelationID) obj;
    return this._relationID == dbRelationId._relationID && this._partID == dbRelationId._partID && dbRelationId._relationType == this._relationType;
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  [DebuggerStepThrough]
  public override int GetHashCode()
  {
    return this._relationType.GetHashCode() << 28 ^ this._relationID.GetHashCode() << 16 /*0x10*/ ^ this._partID.GetHashCode();
  }
}
