// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.IArchiveColumnsSettingsCacheService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Сервис для работы с кэшем настроек колонок по умолчанию архивов для ролей
/// </summary>
internal interface IArchiveColumnsSettingsCacheService
{
  /// <summary>
  /// Поиск настройки происходит только для переданного в метод архива
  /// Это связано с тем, что метод используется в условно-рекурсивном методе FindParentCategoryType в DocumsObject
  /// Наличие сохраненных значений для архива там проверяется на каждом уровне дерева архивов
  /// </summary>
  /// <param name="archiveId"></param>
  /// <param name="userRoleId"></param>
  /// <returns></returns>
  [CanBeNull]
  NodeColumnCollection GetArchiveColumnsSettingsForRole(long archiveId, long userRoleId);

  /// <summary>Зачитываем настройки колонок архива из базы</summary>
  /// <param name="archiveId">ИД архива</param>
  /// <returns>Настройки колонок архива по умолчанию</returns>
  /// 
  ///             Пустой список внутри, если для архива нет настроек
  [NotNull]
  ArchiveColumnsSettings LoadSettingsFromBase(long archiveId);

  /// <summary>Сохранение настроек колонок по умолчанию в базу</summary>
  /// <param name="archiveColumnsSettings"></param>
  void SaveSettingsToBase(ArchiveColumnsSettings archiveColumnsSettings);

  /// <summary>Получаем настройки на архив</summary>
  /// <param name="archiveId">ИД архива</param>
  /// <returns></returns>
  [NotNull]
  ArchiveColumnsSettings GetArchiveColumnsSettings(long archiveId);

  /// <summary>Сохранить настройки в кэше и базе</summary>
  /// <param name="archiveColumnsSettings">Настройки колонок на архив</param>
  void SaveSettingsToCacheAndBase(ArchiveColumnsSettings archiveColumnsSettings);
}
