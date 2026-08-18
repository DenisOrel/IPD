
// Type: Intermech.Client.Core.Navigator.Classes.Providers.INavigatorSchemeColumnProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Client.Core.Navigator.Classes.Providers;

public interface INavigatorSchemeColumnProvider
{
  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  NodeColumnCollection GetDefaultColumns(INodePart nodepart, NodeColumnCollection columnCollection);

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  NodeColumnCollection GetSupportedColumns(
    INodePart nodepart,
    NodeColumnCollection columnCollection,
    string columnSetName);

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (String.Empty)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  List<string> GetSupportedColumnSetNames(INodePart nodepart, List<string> setNameCollection);
}
