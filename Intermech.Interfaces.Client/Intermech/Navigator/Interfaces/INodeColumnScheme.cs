// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeColumnScheme
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Описывает схему виртуальных колонок, используемых в навигаторе. Схема
/// отвечает за создание, сериализацию и десериализацию колонок, а также
/// предоставляет для колонок преобразование по умолчанию, которое может
/// использоваться для вывода значений колонок в гридах.
/// Схема виртуальных колонок Навигатора INodeColumnScheme позволяет выполнять следующие действия:
/// - создаёт колонки;
/// - выполняет их сериализацию/десериализацию;
/// - отыскивает для колонки интерфейс преобразования значений ячеек INodeColumnTransform.
/// ВНИМАНИЕ! Класс, реализующий данный интерфейс, должен быть потокобезопасным.
/// </summary>
public interface INodeColumnScheme
{
  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки отображения колонок.
  /// </summary>
  string Name { get; }

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  string ColumnIDToPersistName(object columnID);

  /// <summary>
  /// Восстанавливает идентификатор виртуальной колонки по ее
  /// постоянному имени, которое действительно только на текущий сеанс
  /// работы универсального клиента. Если восстанавливаемая колонка не
  /// существует, то метод должен вернуть null.
  /// </summary>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns>Идентификатор виртуальной колонки</returns>
  object PersistNameToColumnID(string persistName);

  /// <summary>
  /// Создает виртуальную колонку без сортировки по указанному
  /// идентификатору. Если колонки с заданным идентификатором в схеме нет -
  /// то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Виртуальная колонка</returns>
  NodeColumn CreateColumn(Guid schemeGuid, object columnID);

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex);

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  INodeColumnTransform GetDefaultTransform(object columnID);
}
