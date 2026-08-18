// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.IDBSpecificationObjectID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс объекта-формат для передачи сведений о дочерних объектах спецификации
/// между различными частями системы
/// </summary>
public interface IDBSpecificationObjectID : IDBTypedObjectID, IDBObjectID
{
  /// <summary>Идентификатор связи</summary>
  long RelationID { get; }

  /// <summary>Идентификатор типа связи</summary>
  int RelationTypeID { get; }

  /// <summary>Идентификатор версии родительского объекта</summary>
  long ProjID { get; }

  /// <summary>Обозначение объекта (атрибут объекта)</summary>
  string Designation { get; }

  /// <summary>Наименование объекта (атрибут объекта)</summary>
  string Name { get; }

  /// <summary>Зона (атрибут связи)</summary>
  string Zone { get; }

  /// <summary>Позиция (атрибут связи)</summary>
  string Position { get; }

  /// <summary>Формат (атрибут объекта)</summary>
  string Format { get; set; }

  /// <summary>Количество (атрибут связи)</summary>
  string Quantity { get; }

  /// <summary>Примечание (атрибут связи)</summary>
  string Remark { get; }

  /// <summary>
  /// Идентификатор раздела спецификации, в котором находится объект (атрибут связи)
  /// </summary>
  long SectionID { get; }
}
