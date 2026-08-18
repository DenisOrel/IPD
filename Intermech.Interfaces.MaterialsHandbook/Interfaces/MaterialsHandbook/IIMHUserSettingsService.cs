// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IIMHUserSettingsService
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>Интерфейс для работы с настройками пользователей.</summary>
public interface IIMHUserSettingsService
{
  /// <summary>
  /// Получение списка элементов, добавленных в избранное "Типоразмеры сортамента".
  /// </summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <returns>Список элементов</returns>
  List<FavouriteData> GetAssortmentFavourites(Guid userGuid, Guid categoryGuid);

  /// <summary>
  /// Получение списка элементов, добавленных в избранное "Покрытия".
  /// </summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <returns>Список элементов</returns>
  List<CoatingsFavouriteData> GetCoatingFavourites(Guid userGuid, Guid categoryGuid);

  /// <summary>
  /// Получение списка элементов, добавленных в избранное "Материалы".
  /// </summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <returns>Список элементов</returns>
  List<FavouriteData> GetMaterialFavourites(Guid userGuid, Guid categoryGuid);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="categoryGuid"></param>
  /// <param name="assortmentFavourites"></param>
  void RemoveAssortmentFavourites(Guid categoryGuid, List<FavouriteData> assortmentFavourites);

  /// <summary>Сохранение списка элементов, добавленных в избранное.</summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <param name="assortmentFavourites">Список элементов</param>
  void SaveAssortmentFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<FavouriteData> assortmentFavourites);

  /// <summary>Сохранение списка элементов, добавленных в избранное.</summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <param name="coatingFavourites">Список элементов</param>
  void SaveCoatingFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<CoatingsFavouriteData> coatingFavourites);

  /// <summary>Сохранение списка элементов, добавленных в избранное.</summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <param name="categoryGuid"></param>
  /// <param name="materialFavourites">Список элементов</param>
  void SaveMaterialFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<FavouriteData> materialFavourites);

  /// <summary>
  /// Сохранение всех настроек пользователя в конфигурационный файл.
  /// </summary>
  void SaveUserSettings();
}
