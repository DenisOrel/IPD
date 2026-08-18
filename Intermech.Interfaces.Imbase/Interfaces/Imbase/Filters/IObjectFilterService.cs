// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.IObjectFilterService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>
/// Сервис поддержки фильтрации каталогов / справочников Imbase для объектов
/// </summary>
public interface IObjectFilterService : ICommonFilterService
{
  /// <summary>Получение списка фильтров для типа объекта</summary>
  /// <remarks>Для получения полного списка фильтров, в качестве типа объекта указать "-2"</remarks>
  /// <param name="sessionGuid">Guid сесси</param>
  /// <param name="refObjTypeId">Идентификатор типа объекта, для которго назначен фильтр</param>
  /// <returns></returns>
  List<ImbaseObjFilterInfo> GetFilterList(Guid sessionGuid, int refObjTypeId);

  /// <summary>Получение строк фильтра согласно заданному критерию</summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="filterObjId">Идентификатор объекта - фильтра Imbase</param>
  /// <param name="filterData">Данные фильтра</param>
  /// <returns>true если загрузка прошло успешно</returns>
  bool GetFilterData(Guid sessionGuid, long filterObjId, out ImbaseObjFilterData filterData);

  /// <summary>Задает фильтр для указанной папки</summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="filterObjId">Идентификатор объекта - фильтра Imbase</param>
  /// <param name="filterData">Данные фильтра</param>
  /// <returns>true если сохранение прошло успешно</returns>
  bool SetFilterData(Guid sessionGuid, long filterObjId, ImbaseObjFilterData filterData);

  /// <summary>
  /// Получает таблицу с содержимым Каталога для построения списков настройки фильтрации
  /// (Только папки без записей Imbase)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="catalogId">Идентификатор Каталога</param>
  /// <returns>Таблица с иерархией</returns>
  DataTable LoadCatalogTable(Guid sessionGuid, long catalogId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dt"></param>
  /// <param name="recObjTypeId"></param>
  /// <param name="classifKeyColumnIndex"></param>
  /// <returns></returns>
  DataTable RemoveWithMissingParents(DataTable dt, int recObjTypeId, int classifKeyColumnIndex);
}
