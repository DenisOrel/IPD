// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ISMDOSettingsService
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Office.Interfaces;

public interface ISMDOSettingsService
{
  /// <summary>Общие настройки канцелярии</summary>
  SMDOSettings Settings { get; }

  /// <summary>Перечитать настройки</summary>
  /// <param name="sessionGuid">Глобильный идентификатор сессии администратора. Если сессия не администраторская генерится ошибка.</param>
  void Reload(Guid sessionGuid);

  /// <summary>Сохранить настройки</summary>
  /// <param name="sessionGuid">Глобильный идентификатор сессии администратора. Если сессия не администраторская генерится ошибка.</param>
  /// <param name="settings">Новые настройки</param>
  void Save(Guid sessionGuid, SMDOSettings settings);

  /// <summary>Отправляем посылочку</summary>
  string SendEmail(
    SMDOSettings settings,
    string subject,
    Dictionary<FileStream, string> attachments,
    string body = null);
}
