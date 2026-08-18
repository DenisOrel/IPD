// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.SnapshotAttributes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Список полей итерации, для которых доступно авто-кэширование</summary>
[Flags]
public enum SnapshotAttributes
{
  /// <summary>Пустое значение</summary>
  None = 0,
  /// <summary>Имя итерации</summary>
  Name = 1,
  /// <summary>Дата и время последней модификации итерации</summary>
  ModifyDate = 2,
  /// <summary>Владелец итерации</summary>
  Owner = 4,
  /// <summary>Идентификатор версии объекта, по которому была создана итерация</summary>
  RootObject = 8,
  /// <summary>Таблица дополнительных атрибутов корневого объекта итерации</summary>
  RootObjectAttributes = 16, // 0x00000010
  /// <summary>Идентификаторы версий всех объектов, которые входят в данный снимок</summary>
  ObjectsInSnapshot = 32, // 0x00000020
  /// <summary>Список флагов по-умолчанию</summary>
  Default = RootObject | ModifyDate | Name, // 0x0000000B
}
