// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DefaultCADSettingsFactory
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Создает объект.</summary>
/// <param name="integrator">Интегратор, с которым связана фабрика</param>
internal sealed class DefaultCADSettingsFactory(CADIntegrator integrator) : CADSettingsFactory(integrator)
{
  /// <summary>Создает пустой объект настроек интегратора.</summary>
  /// <returns>Объект с настройками интегратора</returns>
  protected override CADSettings DoCreateSettingsObject() => new CADSettings();

  /// <summary>
  /// Создает пустую модель представления для объекта настроек интегратора. Модель представления используется редактором
  /// настроек интегратора на основе PropertyGrid. На модель навешиваются все необхоимые атрибуты, управляющие поведение PropertyGrid.
  /// </summary>
  /// <returns>Модель представления</returns>
  protected override CADSettingsViewModel DoCreateSettingsViewModel()
  {
    return new CADSettingsViewModel((CADSettingsFactory) this);
  }

  /// <summary>Создает кодек настроек интегратора.</summary>
  /// <param name="integratorName">Название интегратора</param>
  /// <param name="factory">Фабрика объектов настроек</param>
  /// <returns>Объект кодека</returns>
  protected override CADSettingsCodec DoCreateCodec(
    string integratorName,
    ISettingsObjectFactory factory)
  {
    return new CADSettingsCodec(integratorName, factory);
  }
}
