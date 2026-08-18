// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPSettings
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Интерфейс настроек MRP-системы</summary>
public interface IMRPSettings
{
  /// <summary>
  /// Учитывать контекст составов - позволяет включать в состав производственного
  /// заказа записи с указанными значениями контекстов состава. По умолчанию используются
  /// записи Общего и Производственного контекстов состава.
  /// </summary>
  bool UseCompositionContext { get; set; }

  /// <summary>
  /// Выбрать заменители - значение True требует указать для каждого случая допустимых замен
  /// определённый заменитель. По умолчанию значение False, и состав производственного
  /// заказа формируется по актуальным заменителям.
  /// </summary>
  bool UseSubstitutes { get; set; }

  /// <summary>
  /// Включать в состав производственных заказов документацию - значение True указывает
  /// на необходимость включения в состав экземпляров и партий связей с версиями
  /// документации, которая выпущена на соответствующие изделия/комплектации.
  /// </summary>
  bool UseDocumentation { get; set; }

  /// <summary>
  /// Включать в состав производственных заказов составы покупных изделий - значение True указывает
  /// на необходимость включения в состав экземпляров и партий составы покупных партий и изделий.
  /// </summary>
  bool UseBoughtArticles { get; set; }

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  bool LoadSettings(IUserSession session);

  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// (серверная реализация)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>true, если настройки успешно загружены</returns>
  bool LoadSettings(Guid sessionGuid);

  /// <summary>Внести изменения в глобальную конфигурацию системы</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  bool SaveSettings(IUserSession session);

  /// <summary>
  /// Внести изменения в глобальную конфигурацию системы
  /// (серверная реализация)
  /// </summary>
  /// <param name="sessionGuid">Guid сессии</param>
  /// <returns>true, если изменения успешно внесены</returns>
  bool SaveSettings(Guid sessionGuid);
}
