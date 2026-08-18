// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ICompositionByObjectTypesFilter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс фильтра составов по типам родительских и дочерних типов объектов
/// </summary>
public interface ICompositionByObjectTypesFilter : IMetaDataSync, IXMLFilterStorageLoadSave
{
  /// <summary>
  /// Название фильтра составов по типам родительских и дочерних типов объектов
  /// </summary>
  string Name { get; set; }

  /// <summary>
  /// Уникальный глобальный идентификатор фильтра составов по типам родительских и дочерних типов объектов
  /// </summary>
  Guid GUID { get; set; }

  /// <summary>Количество родительских типов в коллекции</summary>
  int ParentTypesCount { get; }

  /// <summary>
  /// Список уникальных глобальных идентификаторов родительских типов объектов, составы которых фильтруются
  /// (возвращается КОПИЯ внутренней коллекции)
  /// </summary>
  List<Guid> ParentObjectTypes { get; }

  /// <summary>
  /// Словарь всех допустимых дочерних типов (верхнего уровня), которые не должны отображаться
  /// (возвращается КОПИЯ внутренней коллекции)
  /// </summary>
  Dictionary<Guid, List<Guid>> ChildObjectTypes { get; }

  /// <summary>Полностью очистить содержимое фильтра</summary>
  void Clear();

  /// <summary>Скопировать содержимое указанного фильтра в свои поля</summary>
  /// <param name="source">Фильтр-источник</param>
  void Assign(ICompositionByObjectTypesFilter source);

  /// <summary>Добавить указанный родительский тип в фильтр</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <returns>true, если тип был успешно добавлен</returns>
  bool Add(Guid parentType);

  /// <summary>Добавить скрытый дочерний тип объектов в фильтр</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Скрываемый дочерний тип объектов</param>
  /// <returns>true, если тип был успешно добавлен</returns>
  bool Add(Guid parentType, Guid childrenType);

  /// <summary>Удалить указанный родительский тип объекта из фильтра</summary>
  /// <param name="parentType">Guid удаляемого родительского типа объекта</param>
  /// <returns>true, если тип был успешно удалён</returns>
  bool Remove(Guid parentType);

  /// <summary>
  /// Удалить указанный скрываемый дочерний тип объекта из фильтра
  /// </summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid удаляемого дочернего типа объекта</param>
  /// <returns>true, если тип был успешно удалён</returns>
  bool Remove(Guid parentType, Guid childrenType);

  /// <summary>
  /// Проверить наличие указанного родительского типа в коллекции
  /// </summary>
  /// <param name="parentType">Guid искомого родительского типа объекта</param>
  /// <returns>true, если указанный родительский тип найден в коллекции</returns>
  bool Exists(Guid parentType);

  /// <summary>
  /// Проверить наличие указанного скрытого дочернего типа объекта у родительского типа объекта
  /// </summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid искомого скрытого дочернего типа объекта</param>
  /// <returns>true, если указанный скрытый дочерний тип найден в коллекции</returns>
  bool Exists(Guid parentType, Guid childrenType);

  /// <summary>
  /// Получить индекс указанного родительского типа в коллекции
  /// </summary>
  /// <param name="parentType">Guid искомого родительского типа объекта</param>
  /// <returns>-1, если указанный родительский тип не найден в коллекции</returns>
  int IndexOf(Guid parentType);

  /// <summary>Получить индекс указанного дочернего типа в коллекции</summary>
  /// <param name="parentType">Guid родительского типа объекта</param>
  /// <param name="childrenType">Guid искомого дочернего типа объекта</param>
  /// <returns>-1, если указанный дочерний тип не найден в коллекции</returns>
  int IndexOf(Guid parentType, Guid childrenType);

  /// <summary>Обменять местами указанные родительские типы объектов</summary>
  /// <param name="idx1">Индекс первого родительского типа объектов</param>
  /// <param name="idx2">Индекс второго родительского типа объектов</param>
  /// <returns>true, если обмен успешно выполнен</returns>
  bool Swap(int idx1, int idx2);

  /// <summary>Обменять местами указанные дочерние типы объектов</summary>
  /// <param name="parentType">Guid родительского типа объектов</param>
  /// <param name="idx1">Индекс первого дочернего типа объектов</param>
  /// <param name="idx2">Индекс второго дочернего типа объектов</param>
  /// <returns>true, если обмен успешно выполнен</returns>
  bool Swap(Guid parentType, int idx1, int idx2);
}
