// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadIntegratorSettings
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using Intermech.Tools.Settings;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Реализует контейнер для настроек интегратора с AutoCAD.
/// </summary>
public sealed class AcadIntegratorSettings : ISettingsObject
{
  private readonly List<AcadStartupConfiguration> startupConfigs;
  private readonly MechanicalSettings mSettings;
  private readonly ConstructionalSettings cSettings;

  /// <summary>Создает объект.</summary>
  public AcadIntegratorSettings()
  {
    this.startupConfigs = new List<AcadStartupConfiguration>();
    this.mSettings = new MechanicalSettings();
    this.cSettings = new ConstructionalSettings();
  }

  /// <summary>
  /// Возвращает список, содержащий настройки подключения к AutoCAD для разный ролей пользователей.
  /// </summary>
  public List<AcadStartupConfiguration> StartupConfigurations => this.startupConfigs;

  /// <summary>
  /// Возвращает настройки интегратора, относящиеся к конструкторским чертежам AutoCAD.
  /// </summary>
  public MechanicalSettings MechanicalSettings => this.mSettings;

  /// <summary>
  /// Возвращает настройки интегратора, относящиеся к СПДС-чертежам AutoCAD.
  /// </summary>
  public ConstructionalSettings ConstructionalSettings => this.cSettings;
}
