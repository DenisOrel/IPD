// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ObjectAttributes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Список полей, для которых доступно авто-кэширование</summary>
[Flags]
public enum ObjectAttributes
{
  /// <summary>Empty</summary>
  None = 0,
  /// <summary>Уникальный идентификатор объекта (НЕ ВЕРСИИ!!!)</summary>
  ObjectID = 1,
  /// <summary>GUID Версии</summary>
  VersionGUID = 2,
  /// <summary>GUID объекта (НЕ ВЕРСИИ!!!)</summary>
  ObjectGUID = 4,
  /// <summary>Cтроковое представление объекта</summary>
  Caption = 8,
  /// <summary>Дата создания</summary>
  CreateDate = 16, // 0x00000010
  /// <summary>Дата последней модификации объекта</summary>
  ModifyDate = 32, // 0x00000020
  /// <summary>Этап жизненного цикла</summary>
  LCStep = 64, // 0x00000040
  /// <summary>Тип объекта</summary>
  ObjectType = 128, // 0x00000080
  /// <summary>Узлы информационной системы</summary>
  SiteID = 256, // 0x00000100
  /// <summary>Идентификатор версии объекта, на основе которой была создана данная версия объекта.
  /// Если это самая первая версия (или родительская версия былу удалена), то возвращает -1.</summary>
  ParentVersionID = 512, // 0x00000200
  /// <summary>Идентификатор проекта, к которому принадлежит объект. Если == 0, то объект создан вне контекста проекта.</summary>
  ProjectID = 1024, // 0x00000400
  /// <summary>Список флагов по-умолчанию</summary>
  Default = ObjectType | ModifyDate | Caption | ObjectID, // 0x000000A9
}
