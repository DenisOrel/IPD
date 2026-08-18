
// Type: Intermech.Navigator.Parts.INodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

/// <summary>
/// Интерфейс для работы с дочерними элементами пространства навигации. Дополнительно
/// интерфейс позволяет работать с источниками данных, а также управлять коллекциями
/// допустимых колонок и колонок по умолчанию
/// </summary>
public interface INodePart : INodeItems
{
  /// <summary>
  /// Устанавливает или возвращает объект, в состав которого входит эта часть.
  /// </summary>
  object Owner { get; set; }

  /// <summary>
  /// Получить интерфейс объекта-запроса к источнику данных, используемого
  /// для чтения содержимого элементов из пространства навигации
  /// </summary>
  /// <returns>Интерфейс объекта-запроса к источнику данных или null</returns>
  INodeQuery GetQuery();

  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  NodeColumnCollection GetDefaultColumns();

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  NodeColumnCollection GetSupportedColumns(string ColumnSetName);

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (String.Empty)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  List<string> GetSupportedColumnSetNames();
}
