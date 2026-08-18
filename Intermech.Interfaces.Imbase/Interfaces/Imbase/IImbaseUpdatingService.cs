// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseUpdatingService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public interface IImbaseUpdatingService
{
  /// <summary>
  /// Из списка глобальных идентификаторов получить список GUIDов существующих записей таблиц IMBASE.
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="guids">Список глобальных идентификаторов</param>
  /// <param name="notExistingRecordsGuids">Список глобальных идентификаторов не принадлежащих записям таблицы IMBASE</param>
  /// <returns>Список глобальных идентификаторов записей таблиц IMBASE</returns>
  List<Guid> GetExistingRecordsGuids(
    Guid sessionGuid,
    List<Guid> guids,
    out List<Guid> notExistingRecordsGuids);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="dt">Таблица данных</param>
  /// <returns></returns>
  object UpdateRecordsValue(Guid sessionGuid, DataTable dt);

  /// <summary>Обновить данные в указанной таблицк IMBASE.</summary>
  /// <remarks>Если строка отсутствует в таблице, то она добавляется</remarks>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="tableGuid">Глобальный идентификатор объекта IPS "Таблица IMBASE"</param>
  /// <param name="dt">Таблица данных</param>
  /// <returns>Возвращается список исключений, которые возникли при обновлении.
  /// В большинстве случаев это одно исключение, после которого невозможно продолжать обновление.
  /// Список исключений может быть в случаях:
  /// в качестве глобального идентификатора указан не GUID (columnName входной таблицы не GUID),
  /// атрибут с указанным глобальным идентификатором отсутствует в IPS,
  /// по каким либо причинам не удалось добавить новый атрибут в таблицу IMBASE</returns>
  object UpdateImbaseTable(Guid sessionGuid, Guid tableGuid, DataTable dt);

  /// <summary>Поиск данных атрибута по значению.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор сессии</param>
  /// <param name="catalogGuid">Глобальный идентификатор каталога IMBASE</param>
  /// <param name="data">Список иденификаторов атрибутов со значениями</param>
  /// <returns>Идентификатор ссылки на таблицу и номер записи</returns>
  Tuple<long, long> SearchData(Guid sessionGuid, Guid catalogGuid, List<Tuple<int, object>> data);
}
