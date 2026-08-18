// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IOfficeDocumentTypeService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Сервис канцелярских документов</summary>
public interface IOfficeDocumentTypeService
{
  /// <summary>Вернуть настройки типа документов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <param name="documentType">тип документа.</param>
  /// <returns>The settings.</returns>
  [NotNull]
  OfficeDocumentTypeSettings GetSettings(Guid sessionGuid, int documentType);

  /// <summary>Установить настройки типа документов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <param name="documentType">тип документа.</param>
  /// <param name="settings">настройки.</param>
  void SetSettings(Guid sessionGuid, int documentType, [NotNull] OfficeDocumentTypeSettings settings);

  /// <summary>Вернуть режимы обнуления счетчиков для всех документов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <returns>The own reset modes.</returns>
  [NotNull]
  Dictionary<OfficeDocumentTypes, CountResetTypes> GetOwnResetModes(Guid sessionGuid);

  /// <summary>Сохранить режимы обнуления счетчиков для всех документов.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <param name="resetTypes">Режимы обнуления счетчиков для всех документов.</param>
  void SetOwnResetModes(
    Guid sessionGuid,
    [NotNull] Dictionary<OfficeDocumentTypes, CountResetTypes> resetTypes);

  /// <summary>Вернуть настройки канцелярских типов документов для канцелярии подразделении.</summary>
  /// <param name="unitID">Идентификатор подразделения.</param>
  /// <returns>При отсутствии настроек вернется null.</returns>
  [CanBeNull]
  Dictionary<int, OfficeDocumentTypeSettingsForUnit> GetTypeSettingsForUnit(long unitID);

  /// <summary>Установить настройки канцелярских типов документов для канцелярии подразделении.</summary>
  /// <param name="unitID">Идентификатор подразделения.</param>
  /// <param name="settings">настройки.</param>
  void SetTypeSettingsForUnit(
    long unitID,
    [NotNull] Dictionary<int, OfficeDocumentTypeSettingsForUnit> settings);

  /// <summary>Вернуть настройки типа документов для подразделения.</summary>
  /// <param name="unitID">Идентификатор подразделения.</param>
  /// <param name="documentType">Тип документа.</param>
  /// <returns>The settings.</returns>
  [CanBeNull]
  OfficeDocumentTypeSettingsForUnit GetSettings(long unitID, int documentType);
}
