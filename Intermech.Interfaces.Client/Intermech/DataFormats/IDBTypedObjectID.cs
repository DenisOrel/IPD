// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBTypedObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс-формат для передачи данных о типах и идентификаторах объектов
/// базы данных через clipboard, а также между различными частями
/// универсального клиента.
/// </summary>
public interface IDBTypedObjectID : IDBObjectID
{
  /// <summary>Тип объекта</summary>
  int ObjectType { get; }

  /// <summary>Идентификатор версии объекта</summary>
  long ObjectID { get; }

  /// <summary>Номер версии объекта</summary>
  long Version { get; }

  /// <summary>
  /// Признак базовой версии объекта (1). В дальнейшем
  /// может содержать дополнительные признаки (битовые флажки)
  /// </summary>
  long BaseVersion { get; }

  /// <summary>Узлы информационной системы</summary>
  string SiteID { get; }

  /// <summary>
  /// Номер группы изменений (не равна 0 - объект принадлежит контексту редактирования)
  /// </summary>
  long ModificationID { get; }
}
