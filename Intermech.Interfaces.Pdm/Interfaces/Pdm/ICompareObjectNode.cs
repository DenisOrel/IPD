// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ICompareObjectNode
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Нод объекта для сравнения составов</summary>
public interface ICompareObjectNode
{
  /// <summary>Список объектов у которых сравнивают состав</summary>
  List<Tuple<long, int>> CompareObjects { get; }

  /// <summary>Текущий объект для сравнения</summary>
  long ObjectID { get; }

  /// <summary>Флаг выставляется во вьющке</summary>
  bool FromCompareView { get; set; }

  /// <summary>
  /// 
  /// </summary>
  int ObjectType { get; }

  /// <summary>Доп. информация для сравнения объектов</summary>
  CompareObjectsInfo Info { get; set; }

  /// <summary>Поток выполняющий запрос состава</summary>
  ICompareBackgroundReader Reader { get; set; }

  /// <summary>Изменения в составе для текущего нода</summary>
  CompareDifferences CurrentDifferences { get; set; }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  NodeColumnCollection GetDefaultColumns(ContentType content);

  /// <summary>
  /// Словарь с флагами необходимости перечитать колонки для сравниваемых объектов
  /// </summary>
  Dictionary<long, bool> RefreshColumns { get; set; }

  /// <summary>Очистить результаты</summary>
  void ClearResult();
}
