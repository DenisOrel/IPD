// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ICompositionByObjectTypesFilters
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс списка фильтров составов по типам родительских и дочерних типов объектов
/// </summary>
public interface ICompositionByObjectTypesFilters : 
  IMetaDataSync,
  IXMLStorageLoadSave,
  IDatabaseLoadSave
{
  /// <summary>Количество фильтров в коллекции</summary>
  int Count { get; }

  /// <summary>
  /// Получить фильтр по его уникальному глобальному идентификатору
  /// </summary>
  /// <param name="filterGuid">Guid фильтра</param>
  /// <returns>Фильтр по его Guid, или null, если такой фильтр не найден</returns>
  ICompositionByObjectTypesFilter this[Guid filterGuid] { get; }

  /// <summary>Получить фильтр по его индексу в коллекции</summary>
  /// <param name="index">Индекс фильтра в коллекции</param>
  /// <returns>Фильтр</returns>
  ICompositionByObjectTypesFilter this[int index] { get; set; }

  /// <summary>
  /// Добавить в список новый фильтр. Имя и Guid для фильтра генерируются автоматически
  /// </summary>
  /// <returns>Ссылка на интерфейс нового фильтра</returns>
  ICompositionByObjectTypesFilter Add();

  /// <summary>Добавить в список новый фильтр</summary>
  /// <param name="name">Название нового фильтра</param>
  /// <param name="guid">Guid нового фильтра</param>
  /// <returns>Ссылка на интерфейс нового фильтра</returns>
  ICompositionByObjectTypesFilter Add(string name, Guid guid);

  /// <summary>
  /// Удалить из коллекции фильтр с указанным уникальным глобальным идентификатором
  /// </summary>
  /// <param name="guid">Guid удаляемого фильтра</param>
  /// <returns>true, если фильтр был найден и удалён</returns>
  bool Remove(Guid guid);

  /// <summary>Скопировать содержимое коллекции в свои поля</summary>
  /// <param name="source">Коллекция-источник</param>
  void Assign(ICompositionByObjectTypesFilters source);

  /// <summary>Отыскать индекс указанного фильтра</summary>
  /// <param name="filter">Искомый фильтр</param>
  /// <returns>Индекс указанного фильтра или -1</returns>
  int IndexOf(ICompositionByObjectTypesFilter filter);

  /// <summary>Отыскать индекс фильтра по его Guid</summary>
  /// <param name="guid">Guid фильтра</param>
  /// <returns>Индекс фильтра или -1</returns>
  int IndexOf(Guid guid);
}
