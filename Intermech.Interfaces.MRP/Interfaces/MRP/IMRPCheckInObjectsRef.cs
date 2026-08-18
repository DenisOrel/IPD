// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPCheckInObjectsRef
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, ссылающийся на контейнер описаний версий объектов, для которых требуется завершить изменения
/// </summary>
public interface IMRPCheckInObjectsRef : IMRPContext
{
  /// <summary>
  /// Список описаний версий объектов в контейнере (копия списка)
  /// </summary>
  List<IMRPObjectRef> Items { get; }

  /// <summary>Добавить описание версии объекта в контейнер</summary>
  /// <param name="objRef">Описание версии объекта</param>
  void Add(IMRPObjectRef objRef);

  /// <summary>Добавить описания версий объектов в контейнер</summary>
  /// <param name="objRefs">Список описаний версий объектов</param>
  void Add(IList<IMRPObjectRef> objRefs);

  /// <summary>Проверить наличие версии объекта в контейнере</summary>
  /// <param name="objectID">Искомая версия объекта (знак не имеет значения)</param>
  /// <returns>true - версия найдена в контейнере</returns>
  bool Exists(long objectID);
}
