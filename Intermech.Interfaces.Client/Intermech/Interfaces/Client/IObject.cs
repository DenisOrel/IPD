// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс закэшированного объекта базы данных</summary>
public interface IObject : IEntity<long, IDBObject>
{
  /// <summary>Уникальный идентификатор версии объекта</summary>
  long VersionID { get; }

  /// <summary>Уникальный идентификатор объекта (НЕ ВЕРСИИ!!!)</summary>
  long ObjectID { get; }

  /// <summary>GUID Версии</summary>
  Guid VersionGUID { get; }

  /// <summary>GUID объекта (НЕ ВЕРСИИ!!!)</summary>
  Guid ObjectGUID { get; }

  /// <summary>Cтроковое представление объекта</summary>
  string Caption { get; }

  /// <summary>Дата создания</summary>
  DateTime CreateDate { get; }

  /// <summary>Дата последней модификации объекта</summary>
  DateTime ModifyDate { get; }

  /// <summary>Этап жизненного цикла</summary>
  int LCStep { get; }

  /// <summary>Тип объекта</summary>
  int ObjectType { get; }

  /// <summary>Узлы информационной системы</summary>
  string SiteID { get; }

  /// <summary>
  /// Идентификатор версии объекта, на основе которой была создана данная версия объекта. Если это самая первая версия (или родительская
  /// версия былу удалена), то возвращает -1.
  /// </summary>
  long ParentVersionID { get; }

  /// <summary>Идентификатор проекта, к которому принадлежит объект. Если == 0, то объект создан вне контекста проекта.</summary>
  long ProjectID { get; }
}
