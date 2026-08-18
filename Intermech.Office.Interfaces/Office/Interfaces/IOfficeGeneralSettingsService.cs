// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.IOfficeGeneralSettingsService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Интерфейс на серверную службу с общими настройками канцелярии.</summary>
public interface IOfficeGeneralSettingsService
{
  /// <summary>Общие настройки канцелярии.</summary>
  [NotNull]
  OfficeGeneralSettings Settings { get; }

  /// <summary>Список идентификаторов супервизоров канцелярии</summary>
  [NotNull]
  long[] SupervisorObjVerIDs { get; }

  /// <summary>Перечитать настройки.</summary>
  /// <param name="sessionGuid">
  /// Глобальный идентификатор сессии администратора. Если сессия не администраторская выбрасывается исключительная ситуация
  /// </param>
  void Reload(Guid sessionGuid);

  /// <summary>Сохранить настройки.</summary>
  /// <param name="sessionGuid">
  /// Глобальный идентификатор сессии администратора. Если сессия не администраторская выбрасывается исключительная ситуация
  /// </param>
  /// <param name="settings">Новые настройки.</param>
  void Save(Guid sessionGuid, [NotNull] OfficeGeneralSettings settings);

  /// <summary>Сохранить в настройки список супервизоров канцелярии</summary>
  void WriteSupervisorsList(Guid sessionGuid, [NotNull] long[] supervisorsList);
}
