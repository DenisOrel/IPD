// Decompiled with JetBrains decompiler
// Type: Intermech.Search.IRoleConfigurationManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Search.Navigator;

#nullable disable
namespace Intermech.Search;

/// <summary>Менеджер конфигурации роли</summary>
public interface IRoleConfigurationManager
{
  /// <summary>Загрузить пакет колонок по-умолчанию навигатора</summary>
  /// <param name="roleConfigurationVersionID">Идентификатор версии конфигурации роли</param>
  /// <returns>Пакет колонок</returns>
  ColumnPack LoadNavigatorDefaultColumnPack(long roleConfigurationVersionID);

  /// <summary>Сохранить пакет колонок по-умолчанию навигатора</summary>
  /// <param name="roleConfigurationVersionID">Идентификатор версии конфигурации роли</param>
  /// <param name="columnPack">Пакет колонок</param>
  void SaveNavigatorDefaultColumnPack(long roleConfigurationVersionID, ColumnPack columnPack);
}
