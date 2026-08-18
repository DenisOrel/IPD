// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MaterialsHandbook.IIMHSystemSettingsService
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MaterialsHandbook;

/// <summary>
/// Интерфейс для работы с настройками "Марочника материалов".
/// </summary>
public interface IIMHSystemSettingsService
{
  /// <summary>
  /// Получить глобальный идентификатор каталога, по наименованию поля, с которым он связал.
  /// </summary>
  /// <param name="name">Наименование поля</param>
  /// <returns>Глобальный идентификатор каталога</returns>
  Guid GetObjectGuidByName(string name);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="names"></param>
  /// <returns></returns>
  Dictionary<string, Guid> GetObjectGuidsByNames(List<string> names);

  /// <summary>Получить значение поля по наименованию.</summary>
  /// <param name="name">Наименование поля</param>
  /// <returns>Значение</returns>
  object GetValueByName(string name);

  /// <summary>Получение системных настроек.</summary>
  /// <returns></returns>
  IMHSystemSettings GetSystemSettings();

  /// <summary>Сохранение системных настроек.</summary>
  /// <param name="settings"></param>
  void SaveSistemSettings(IMHSystemSettings settings);
}
