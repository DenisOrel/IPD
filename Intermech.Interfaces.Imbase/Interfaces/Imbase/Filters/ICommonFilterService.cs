// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Filters.ICommonFilterService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase.Filters;

/// <summary>Базовый интерфейс сервисов фильтрации папок Каталогов</summary>
public interface ICommonFilterService
{
  /// <summary>Применить фильтр для заданного DataTable</summary>
  /// <remarks>Список каталогов получаем из DataTable</remarks>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="filterObjId">Идентификатор объекта содержащего имформацию о фильтре</param>
  /// <param name="ownerGuid">Идентификатор роли или null</param>
  /// <param name="dataTable">Таблица с фильтруемыми данными каталога / справочника Imbase</param>
  /// <param name="extArgs">Доп. параметры фильтрации </param>
  /// <returns></returns>
  DataTable ApplyFilter(
    Guid sessionGuid,
    long filterObjId,
    string ownerGuid,
    DataTable dataTable,
    HybridDictionary extArgs = null);
}
