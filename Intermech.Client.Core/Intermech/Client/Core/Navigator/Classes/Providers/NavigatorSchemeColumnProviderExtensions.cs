
// Type: Intermech.Client.Core.Navigator.Classes.Providers.NavigatorSchemeColumnProviderExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Client.Core.Navigator.Classes.Providers;

/// <summary>
/// Класс для методов расширения поддержки провайдера, для базовых классов навигатора
/// </summary>
public static class NavigatorSchemeColumnProviderExtensions
{
  /// <summary>Коллекция колонок по умолчанию</summary>
  /// <returns>Коллекция колонок по умолчанию</returns>
  public static NodeColumnCollection GetSchemeDefaultColumns(
    this INodePart nodepart,
    NodeColumnCollection columnCollection)
  {
    if (!(nodepart is IContextAware contextAware))
      return columnCollection;
    INavigatorSchemeColumnProvider service = ServiceUtils.GetService<INavigatorSchemeColumnProvider>((object) contextAware.Services, false);
    return service == null ? columnCollection : service.GetDefaultColumns(nodepart, columnCollection);
  }

  /// <summary>
  /// Коллекция всех поддерживаемых данным элементом  виртуальных колонок навигатора.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// String.Empty - набор колонок по умолчанию</param>
  /// <returns>Коллекция всех поддерживаемых виртуальных колонок навигатора</returns>
  public static NodeColumnCollection GetSchemeSupportedColumns(
    this INodePart nodepart,
    NodeColumnCollection columnCollection,
    string columnSetName)
  {
    if (!(nodepart is IContextAware contextAware))
      return columnCollection;
    INavigatorSchemeColumnProvider service = ServiceUtils.GetService<INavigatorSchemeColumnProvider>((object) contextAware.Services, false);
    return service == null ? columnCollection : service.GetSupportedColumns(nodepart, columnCollection, columnSetName);
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (String.Empty)
  /// </summary>
  /// <returns>СписокScheme поддерживаемых названий наборов колонок</returns>
  public static List<string> GetSchemeSupportedColumnSetNames(
    this INodePart nodepart,
    List<string> setNameCollection)
  {
    if (!(nodepart is IContextAware contextAware))
      return setNameCollection;
    INavigatorSchemeColumnProvider service = ServiceUtils.GetService<INavigatorSchemeColumnProvider>((object) contextAware.Services, false);
    return service == null ? setNameCollection : service.GetSupportedColumnSetNames(nodepart, setNameCollection);
  }
}
