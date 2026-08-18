// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IModelConfigurationsContainer
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Абстракция для контейнера конфигураций.</summary>
public interface IModelConfigurationsContainer
{
  /// <summary>Получить описание конфигурации с указанным именем</summary>
  /// <param name="name">Имя конфигурации</param>
  /// <param name="openVisible">Признак, что конфигурация должна быть открыта в видимом режиме</param>
  /// <returns>Описание конфигурации с указанным именем</returns>
  ModelConfigurationProxy GetConfiguration(string name, bool openVisible = false);

  /// <summary>Получить список существующих конфигураций</summary>
  /// <returns>Список существующих конфигураций</returns>
  List<ModelConfigurationProxy> GetConfigurations();

  /// <summary>Получить список имен существующих конфигураций.</summary>
  /// <returns>Список имен существующих конфигураций</returns>
  List<string> GetConfigurationNames();
}
