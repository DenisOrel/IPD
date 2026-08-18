
// Type: Intermech.Navigator.Queries.INodeQuerySupport
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Интерфейс, помогающий подготовить запрос к выполнению и обработать
/// его результаты.
/// </summary>
public interface INodeQuerySupport
{
  /// <summary>
  /// Возвращает идентификатор поля источника данных для указанной
  /// виртуальной колонки. Если данная колонка не поддерживается, то
  /// метод должен вернуть null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <returns>Идентификатор поля источника данных</returns>
  object MapColumnToField(NodeColumn column);

  /// <summary>
  /// Возвращает список идентификаторов полей источника данных, значения
  /// которых обязательно должны быть получены в результате выполнения
  /// запроса.
  /// </summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  List<object> GetSpecialFields();

  /// <summary>
  /// Создает и возвращает унифицированный идентификатор элемента навигации.
  /// </summary>
  /// <param name="fieldValues">Значения полей, полученных от источника данных</param>
  /// <param name="adapter">Адаптер полей источника данных</param>
  /// <returns>Унифицированный идентификатор элемента навигации</returns>
  INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter);

  /// <summary>
  /// Создает и возвращает идентификатор элемента в источнике данных по
  /// его унифицированному идентификатору.
  /// </summary>
  /// <param name="nodeId">Унифицированный идентификатор элемента навигации</param>
  /// <returns>Идентификатор соответствующего элемента в источнике данных</returns>
  object CreateRecordId(INodeID nodeId);
}
