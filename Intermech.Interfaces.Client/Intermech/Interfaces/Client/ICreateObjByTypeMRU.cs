// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICreateObjByTypeMRU
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, помогающий управлять списком команд по созданию типов объектов в подменю "Файл\Создать"
/// </summary>
public interface ICreateObjByTypeMRU
{
  /// <summary>Максимальная ёмкость списка элементов</summary>
  int MaxCapacity { get; set; }

  /// <summary>Элемент коллекции</summary>
  /// <param name="index">Индекс элемента</param>
  /// <returns>Элемент с указанным индексом</returns>
  IMRUItem this[int index] { get; }

  /// <summary>Количество элементов в коллекции</summary>
  int Count { get; }

  /// <summary>
  /// Добавить элемент в коллекцию (с учётом значения MaxCapacity)
  /// </summary>
  /// <param name="Caption">Текстовое пояснение элемента</param>
  /// <param name="Value">Основное значение элемента</param>
  /// <param name="Tag">Дополнительное значение элемента</param>
  /// <returns>Вновь добавленный элемент</returns>
  IMRUItem Add(string Caption, object Value, object Tag);

  /// <summary>
  /// Добавить элемент в коллекцию (с учётом значения MaxCapacity)
  /// </summary>
  /// <param name="value">Элемент, который требуется добавить</param>
  /// <returns>Вновь добавленный элемент</returns>
  IMRUItem Add(IMRUItem value);

  /// <summary>Очистить всю коллекцию элементов</summary>
  void Clear();

  /// <summary>Проверить наличие в списке указанного элемента</summary>
  /// <param name="value">Элемент</param>
  /// <returns>true, если элемент найден</returns>
  bool Contains(IMRUItem value);

  /// <summary>
  /// Отыскать порядковый номер указанного элемента в коллекции
  /// </summary>
  /// <param name="value">Элемент</param>
  /// <returns>-1, если элемент не найден</returns>
  int IndexOf(IMRUItem value);

  /// <summary>
  /// Отыскать порядковый номер указанного значения (IMRUItem.Value, не равно null!!!) в коллекции
  /// </summary>
  /// <param name="value">Значение (IMRUItem.Value) (не равно null!!!)</param>
  /// <returns>-1, если элемент не найден</returns>
  int IndexOf(object value);

  /// <summary>Вставить в коллекцию элемент</summary>
  /// <param name="index">Индекс для добавляемого элемента</param>
  /// <param name="item">Добавляемый элемент</param>
  void Insert(int index, IMRUItem item);

  /// <summary>Удалить элемент из коллекции</summary>
  /// <param name="value">Элемент, который требуется удалить</param>
  /// <returns>Позиция удалённого элемента</returns>
  bool Remove(IMRUItem value);

  /// <summary>Удалить элемент с указанным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  void RemoveAt(int index);

  /// <summary>Выполнить сортировку</summary>
  void Sort();

  /// <summary>
  /// Выполнить сортировку с указанным интерфейсом сравнения
  /// </summary>
  /// <param name="comparer">Интерфейс для сравнения элементов IMRUItem</param>
  void Sort(IComparer<IMRUItem> comparer);

  /// <summary>Вернуть массив элементов IMRUItem из коллекции</summary>
  /// <returns></returns>
  IMRUItem[] ToArray();

  /// <summary>Загрузить список MRU из настроек пользователя</summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  void LoadMRU(long UserID);

  /// <summary>
  /// Сохранить список MRU в настройки указанного пользователя
  /// </summary>
  /// <param name="UserID">Идентификатор пользователя</param>
  void SaveMRU(long UserID);
}
