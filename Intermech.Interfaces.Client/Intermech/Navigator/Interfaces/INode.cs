// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Основной интерфейс элемента из пространства навигации.
/// </summary>
public interface INode : INodeItems
{
  /// <summary>
  /// Набор дополнительных свойств, которые присущи указанному элементу пространства навигации
  /// </summary>
  NodeOptions Options { get; set; }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого можно
  /// прочитать список дочерних элементов. Если у данного элемента нет
  /// дочерних, то метод вернет null.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип читаемых дочерних элементов</param>
  /// <returns>Интерфейс запроса</returns>
  INodeQuery GetQuery(ContentType content);

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  NodeColumnCollection GetDefaultColumns(ContentType content);

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  NodeColumnCollection GetSupportedColumns(ContentType content, string ColumnSetName);

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (Intermech.Navigator.Consts.NavigatorDefaultColumnSetName)
  /// </summary>
  /// <returns>Список поддерживаемых названий наборов колонок</returns>
  List<string> GetSupportedColumnSetNames();

  /// <summary>Обновляет внутренние структуры элемента навигации.</summary>
  void Refresh();
}
