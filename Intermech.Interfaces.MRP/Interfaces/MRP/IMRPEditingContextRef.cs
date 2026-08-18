// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPEditingContextRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, ссылающийся на контейнер версий объектов, которые требуется добавить в какой-либо контекст редактирования
/// </summary>
public interface IMRPEditingContextRef : IMRPContext
{
  /// <summary>Список версий объектов в контейнере (копия списка)</summary>
  List<long> Items { get; }

  /// <summary>Список объектов в контейнере (копия списка)</summary>
  List<long> ItemsF_ID { get; }

  /// <summary>Список типов объектов в контейнере (копия списка)</summary>
  List<int> ItemTypes { get; }

  /// <summary>Добавить версию объекта в контейнер</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <param name="fID">Идентификатор объекта</param>
  /// <param name="typeID">Идентификатор типа объекта</param>
  void Add(long objectID, long fID, int typeID);

  /// <summary>Добавить версии объектов в контейнер</summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="fIDs">Список идентификаторов объектов</param>
  /// <param name="typeIDs">Список типов объектов</param>
  void Add(IList<long> objectIDs, IList<long> fIDs, IList<int> typeIDs);

  /// <summary>Проверить наличие версии объекта в контейнере</summary>
  /// <param name="objectID">Искомая версия объекта</param>
  /// <returns>true - версия найдена в контейнере</returns>
  bool Exists(long objectID);
}
