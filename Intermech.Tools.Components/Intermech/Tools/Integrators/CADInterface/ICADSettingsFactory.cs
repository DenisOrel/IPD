// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ICADSettingsFactory
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Интерфейс фабрики объектов для подсистемы настроек интегратора.
/// </summary>
public interface ICADSettingsFactory
{
  /// <summary>Создает сервис для доступа к настройкам интегратора.</summary>
  /// <param name="sharedModelAttributes">Признак, что атрибуты документа и атрибуты конфигураций хранятся в одном контейнере</param>
  /// <returns>Созданный объект сервиса</returns>
  ICADSettingsService CreateSettingsService(bool sharedModelAttributes);
}
