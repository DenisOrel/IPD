// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ClipboardObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект, помещаемый в буфер обмена IPS при операциях Копировать и Вырезать
/// </summary>
public class ClipboardObject : IDBTypedObjectID, IDBObjectID, IDBRelationID
{
  private IDBTypedObjectID _iDBTypedObjectID;
  private IDBRelationID _iDBRelationID;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="iDBTypedObjectID"> Интерфейс, описывающий пару тип объекта + его идентификатор </param>
  /// <param name="iDBRelationID"> Интерфейс, описывающий связь, входящей в объект </param>
  public ClipboardObject(IDBTypedObjectID iDBTypedObjectID, IDBRelationID iDBRelationID)
  {
    this._iDBTypedObjectID = iDBTypedObjectID;
    this._iDBRelationID = iDBRelationID;
  }

  /// <summary> Интерфейс, описывающий пару тип объекта + его идентификатор </summary>
  public IDBTypedObjectID IDBTypedObjectID => this._iDBTypedObjectID;

  /// <summary> Интерфейс, описывающий связь, входящей в объект </summary>
  public IDBRelationID IDBRelationID => this._iDBRelationID;

  /// <summary> Идентификтаор типа объекта </summary>
  public int ObjectType => this._iDBTypedObjectID == null ? -1 : this._iDBTypedObjectID.ObjectType;

  /// <summary> Идентификатор версии объекта </summary>
  public long ObjectID => this._iDBTypedObjectID == null ? -1L : this._iDBTypedObjectID.ObjectID;

  /// <summary> Идентификатор объекта </summary>
  public long ID => this._iDBTypedObjectID == null ? -1L : this._iDBTypedObjectID.ID;

  /// <summary>Заголовок объекта</summary>
  public string Caption
  {
    get => this._iDBTypedObjectID == null ? string.Empty : this._iDBTypedObjectID.Caption;
  }

  /// <summary>Владелец объекта</summary>
  public long Owner => this._iDBTypedObjectID == null ? 0L : this._iDBTypedObjectID.Owner;

  /// <summary>Номер версии объекта</summary>
  public long Version => this._iDBTypedObjectID == null ? 0L : this._iDBTypedObjectID.Version;

  /// <summary>Признак базовой версии объекта</summary>
  public long BaseVersion
  {
    get => this._iDBTypedObjectID == null ? 0L : this._iDBTypedObjectID.BaseVersion;
  }

  /// <summary>Узел информационной системы</summary>
  public string SiteID
  {
    get => this._iDBTypedObjectID == null ? string.Empty : this._iDBTypedObjectID.SiteID;
  }

  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  public long ModificationID
  {
    get => this._iDBTypedObjectID == null ? 0L : this._iDBTypedObjectID.ModificationID;
  }

  /// <summary> Идентификатор связи </summary>
  public long Value => this._iDBRelationID == null ? -1L : this._iDBRelationID.Value;

  /// <summary> Идентификатор дочернего объекта </summary>
  public long PartID => this._iDBRelationID == null ? -1L : this._iDBRelationID.PartID;

  /// <summary>Идентификатор типа связи</summary>
  public int RelationType => this._iDBRelationID == null ? -1 : this._iDBRelationID.RelationType;

  /// <summary>Значение атрибута "Сортировка"</summary>
  public long Sorting => this._iDBRelationID == null ? 0L : this._iDBRelationID.Sorting;

  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjID => this._iDBRelationID == null ? 0L : this._iDBRelationID.ProjID;

  /// <summary>Guid связи</summary>
  public Guid RelGuid => this._iDBRelationID == null ? Guid.Empty : this._iDBRelationID.RelGuid;

  /// <summary>Вернуть строковое описание объекта из буфера обмена</summary>
  /// <returns>Cтроковое описание объекта из буфера обмена</returns>
  public override string ToString()
  {
    return !string.IsNullOrEmpty(this.Caption) ? this.Caption : LocalizationHolder.rm.GetString("Interfaces.Client_58");
  }
}
