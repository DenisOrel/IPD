// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavigatorDataHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Служба для получения информации из ячеек элементов Навигатора, содержащих списки строк
/// </summary>
public interface INavigatorDataHelper
{
  /// <summary>
  /// Получить значение атрибута указанного типа из текущей (или первой выделенной) строки в элементе
  /// Навигатора. Источник атрибута (объект, связь, т.п.) не играет роли, значение будет получено
  /// у первой подходящей колонки
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <returns>Значение, либо DBNull.Value (для пустого значения), либо null,
  /// если колонка с указанным атрибутом не найдена</returns>
  object GetAttributeValue(int attrID);

  /// <summary>
  /// Получить значение атрибута указанного типа из текущей (или первой выделенной) строки в элементе
  /// Навигатора
  /// </summary>
  /// <param name="attrID">Идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  /// <returns>Значение, либо DBNull.Value (для пустого значения), либо null,
  /// если колонка с указанным атрибутом не найдена</returns>
  object GetAttributeValue(int attrID, AttributeSourceTypes attrSource);

  /// <summary>
  /// Получить значение атрибута указанного типа из текущей (или первой выделенной) строки в элементе
  /// Навигатора. Источник атрибута (объект, связь, т.п.) не играет роли, значение будет получено
  /// у первой подходящей колонки
  /// </summary>
  /// <param name="attrGuid">Глобальный идентификатор типа атрибута</param>
  /// <returns>Значение, либо DBNull.Value (для пустого значения), либо null,
  /// если колонка с указанным атрибутом не найдена</returns>
  object GetAttributeValue(Guid attrGuid);

  /// <summary>
  /// Получить значение атрибута указанного типа из текущей (или первой выделенной) строки в элементе
  /// Навигатора
  /// </summary>
  /// <param name="attrGuid">Глобальный идентификатор типа атрибута</param>
  /// <param name="attrSource">Источник атрибута</param>
  /// <returns>Значение, либо DBNull.Value (для пустого значения), либо null,
  /// если колонка с указанным атрибутом не найдена</returns>
  object GetAttributeValue(Guid attrGuid, AttributeSourceTypes attrSource);

  /// <summary>
  /// Получить данные указанного формата (типа) из элемента, на основании которого сформирована текущая
  /// (первая выделенная) строка
  /// </summary>
  /// <param name="dataFormat">Тип запрашиваемых данных</param>
  /// <returns>Данные запрошенного типа или null</returns>
  object GetRowData(Type dataFormat);
}
