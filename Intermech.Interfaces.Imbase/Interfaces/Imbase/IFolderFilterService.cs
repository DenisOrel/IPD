// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IFolderFilterService
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces.Imbase.Filters;
using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Сервис поддержки фильтрации папок Каталогов</summary>
public interface IFolderFilterService : ICommonFilterService
{
  /// <summary>
  /// Получает верхнюю часть иерархии каталога, применительно к указанной папке
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="folderId">Идентификатор папки</param>
  /// <param name="catalogId">Идентификатор Каталога или NOOBJECT для всех</param>
  /// <param name="ownerGuid">Идентификатор роли или null</param>
  /// <returns>Массив идентификаторов</returns>
  string[] GetFilter(Guid sessionGuid, Guid folderId, long catalogId, string ownerGuid);

  /// <summary>Получение строк фильтра согласно заданному критерию</summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="filterCond">Критерии получения строк фильтра</param>
  /// <returns></returns>
  DataTable GetFilter(Guid sessionGuid, string filterCond);

  /// <summary>Задает фильтр для указанной папки</summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="folderId">Идентификатор папки</param>
  /// <param name="ownerGuid">Идентификатор роли или null</param>
  /// <param name="addValues">Массив идентификаторов папок для добавления или null</param>
  /// <param name="delValues">Массив идентификаторов папок для удаления или null</param>
  /// <returns>true если папка содержит данные для фильтра</returns>
  bool SetFilter(
    Guid sessionGuid,
    Guid folderId,
    string ownerGuid,
    string[] addValues,
    string[] delValues);

  /// <summary>
  /// Получает таблицу с содержимым Каталога для построения списков настройки фильтрации
  /// (Только папки без записей Imbase)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="catalogId">Идентификатор Каталога</param>
  /// <param name="checkBlobs">Получать данные о назначенных фильтрах</param>
  /// <returns>Таблица с иерархией</returns>
  DataTable LoadCatalogTable(Guid sessionGuid, long catalogId, bool checkBlobs);

  /// <summary>
  /// Получает таблицу с содержимым Каталога для построения списков настройки фильтрации
  /// (Папки + записи Imbase)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="catalogId">Идентификатор Каталога или Папки, состав которых надо получить</param>
  /// <param name="checkBlobs">Получать данные о назначенных фильтрах</param>
  /// <returns>Таблица с иерархией</returns>
  DataTable LoadAllCatalogTable(Guid sessionGuid, long catalogId, bool checkBlobs);

  /// <summary>
  /// Возвращает таблицу с иерархией для указанной папки из каталога
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <param name="folderId">Идентификатор папки с условиями фильтрации</param>
  /// <param name="ownerGuid">Идентификатор роли или null</param>
  /// <param name="catalogId">Каталог</param>
  /// <returns>Таблица с иерархией</returns>
  DataTable LoadFoldersFor(Guid sessionGuid, long folderId, string ownerGuid, long catalogId);
}
