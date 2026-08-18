// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IIMHSelector
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// 
/// </summary>
public interface IIMHSelector
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IDescriptor GetMaterialsHandbookDescriptor();

  /// <summary>Проверка на принадлежность узла марочнику материалов.</summary>
  /// <param name="selectedItems">Проверяемый узел в дереве</param>
  /// <param name="selected">Выделен или нет</param>
  /// <returns>Результат проверки</returns>
  bool IsMaterialsHandbookItem(ISelectedItems selectedItems, out bool selected);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="description"></param>
  /// <param name="descriptorCollection"></param>
  /// <param name="needType"></param>
  /// <param name="contextObjsID"></param>
  /// <returns></returns>
  long SelectMaterial(
    string caption,
    string description,
    object descriptorCollection,
    int needType,
    long contextObjsID);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="useGuid"></param>
  /// <param name="multiSelect"></param>
  /// <returns></returns>
  List<string> SelectMaterial(bool useGuid, bool multiSelect);

  /// <summary>Выбирает запись IMBASE.</summary>
  /// <param name="caption">Заголовок окна при выборе</param>
  /// <param name="description"></param>
  /// <param name="descriptorCollection">Коллекция узлов, которые нужно отобразить в дереве</param>
  /// <param name="contextObjsID">Идентификатор объекта</param>
  /// <returns>Идентификатор ссылки а таблицу IMBASE и номер записи или null при отмене выбора</returns>
  /// <remarks>Если передается contextObjsID, то у объекта получаем объект IMBASE, на который он ссылается, и позиционируемся на этом объекте</remarks>
  Tuple<long, long> SelectMaterial(
    string caption,
    string description,
    object descriptorCollection,
    long contextObjsID);

  /// <summary>Выбор наименования покрытия.</summary>
  /// <returns></returns>
  string SelectCoatingDesignation();

  /// <summary>Выбор наименования клея.</summary>
  /// <returns></returns>
  string SelectGlueDesignation();

  /// <summary>
  /// Поиск и выделение в марочнике материалов указанного материала.
  /// </summary>
  /// <param name="node">Узел в дереве, от котоого идет поиск узла материала</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу IMBASE, которой принадлежит материал</param>
  /// <param name="recID">Номер записи в таблице</param>
  /// <returns>Результат поиска</returns>
  bool SearchAndSelectMaterial(object node, long tableRefID, long recID);

  /// <summary>Поиск в марочнике материалов указанного материала.</summary>
  /// <param name="node">Узел в дереве, от котоого идет поиск узла материала</param>
  /// <param name="tableRefID">Идентификатор ссылки на таблицу IMBASE, которой принадлежит материал</param>
  /// <param name="recID">Номер записи в таблице</param>
  /// <returns>Результат поиска</returns>
  object SearchMaterial(object node, long tableRefID, long recID);
}
