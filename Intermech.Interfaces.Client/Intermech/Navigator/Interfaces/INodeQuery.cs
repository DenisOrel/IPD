// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeQuery
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс объекта-запроса к источнику данных, используемого
/// для чтения содержимого элементов из пространства навигации
/// </summary>
public interface INodeQuery
{
  /// <summary>
  /// Добавляет колонку, значение которой должно быть получено в
  /// результате выполнения запроса. Дополнительно может быть указано
  /// преобразование, которое должно быть применено к содержимому колонки.
  /// Если преобразовывать содержимое колонки не требуется, то в качестве
  /// преобразования следует указать null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <param name="transform">Преобразование содержимого колонки</param>
  void AddColumn(NodeColumn column, INodeColumnTransform transform);

  /// <summary>
  /// Выполняет запрос на чтение порции дочерних элементов. Позиция для
  /// чтения определяется закладкой (bookmark). Если закладка = null,
  /// то будет прочитана первая порция, иначе будет прочитана порция с
  /// позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  void Execute(object bookmark, int count);

  /// <summary>
  /// Выполняет запрос на чтение значений колонок для указанных
  /// дочерних элементов. Этот метод используется навигатором при
  /// операциях обновления содержимого дерева и других элементов
  /// визуального интерфейса.
  /// </summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов</param>
  void Execute(NodeIDCollection nodeIDs);

  /// <summary>
  /// Возвращает закладку, определяющую позицию для чтения следующей
  /// порции дочерних элементов или null, если была прочитана
  /// последняя порция.
  /// </summary>
  object Bookmark { get; }

  /// <summary>
  /// Возвращает количество прочитанных в результате выполнения запроса дочерних элементов.
  /// </summary>
  int RecordCount { get; }

  /// <summary>Условия выполнения запросов</summary>
  NodeQueryOptions Options { get; set; }

  /// <summary>
  /// Возвращает количество всех элементов, которые могут быть получены с помощью данного запроса.
  /// Значение свойства будет определено только после первого пакетного чтения, при условии, что
  /// в опциях задан флажок ReceiveTotalRecordsCount. Иначе свойство будет равно значению RecordCount.
  /// </summary>
  long TotalRecordCount { get; }

  /// <summary>
  /// Возвращает идентификатор дочернего элемента по его порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  INodeID GetRecordNodeID(int index);

  /// <summary>
  /// Возвращает значения колонок дочернего элемента по его порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив значений колонок</returns>
  object[] GetRecordValues(int index);

  /// <summary>
  /// Возвращает исходные значения колонок дочернего элемента по его порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив исходных значений колонок</returns>
  object[] GetRawRecordValues(int index);
}
