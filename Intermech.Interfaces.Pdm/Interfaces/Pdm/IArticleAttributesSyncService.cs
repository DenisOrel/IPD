// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IArticleAttributesSyncService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Интерфейс службы синхронизации атрибутов конструкторских документов и изделий
/// </summary>
public interface IArticleAttributesSyncService
{
  /// <summary>
  /// Записывает настройки синхронизации атрибутов. Требует прав админа.
  /// </summary>
  /// <param name="settings">Настройки синхронизации.</param>
  /// <param name="sessionGuid">Гуид сессии, производящей запись настроек.</param>
  void WriteSyncSettings(ArticleAttributesSyncSettings settings, Guid sessionGuid);

  /// <summary>
  /// Возвращает объект с настройками синхронизации атрибутов.
  /// </summary>
  /// <param name="sessionGuid">Гуид сесси пользователя.</param>
  /// <returns>Объект с настройками синхронизации.</returns>
  ArticleAttributesSyncSettings ReadSyncSett(Guid sessionGuid);
}
