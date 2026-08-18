// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SettingsSurrogate
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.PropertyEditors;
using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class SettingsSurrogate : ICloneable
{
  private ChangeTrackingListAdapter<StartupConfigurationSurrogate> startupConfigs;
  private bool msEnableSupport;
  private ChangeTrackingListAdapter<DrawingTypeSettings> msAssemblyDrawings;
  private ChangeTrackingListAdapter<DrawingTypeSettings> msPartDrawings;
  private bool csEnableSupport;
  private ChangeTrackingListAdapter<DrawingTypeSettings> csDrawings;

  public SettingsSurrogate(AcadIntegratorSettings settings)
  {
    this.startupConfigs = new ChangeTrackingListAdapter<StartupConfigurationSurrogate>((IEnumerable<StartupConfigurationSurrogate>) settings.StartupConfigurations.ConvertAll<StartupConfigurationSurrogate>((Converter<AcadStartupConfiguration, StartupConfigurationSurrogate>) (item => new StartupConfigurationSurrogate()
    {
      UserRole = item.UserRole,
      UseSpecificProfile = item.UseSpecificProfile,
      ProfileName = item.ProfileName
    })));
    this.msEnableSupport = settings.MechanicalSettings.IsEnabled;
    this.msAssemblyDrawings = new ChangeTrackingListAdapter<DrawingTypeSettings>((IEnumerable<DrawingTypeSettings>) settings.MechanicalSettings.AssemblyDrawings);
    this.msPartDrawings = new ChangeTrackingListAdapter<DrawingTypeSettings>((IEnumerable<DrawingTypeSettings>) settings.MechanicalSettings.PartDrawings);
    this.csEnableSupport = settings.ConstructionalSettings.IsEnabled;
    this.csDrawings = new ChangeTrackingListAdapter<DrawingTypeSettings>((IEnumerable<DrawingTypeSettings>) settings.ConstructionalSettings.Drawings);
  }

  public AcadIntegratorSettings ToSettings()
  {
    AcadIntegratorSettings settings = new AcadIntegratorSettings();
    foreach (StartupConfigurationSurrogate startupConfig in this.startupConfigs)
      settings.StartupConfigurations.Add(new AcadStartupConfiguration()
      {
        UserRole = startupConfig.UserRole,
        UseSpecificProfile = startupConfig.UseSpecificProfile,
        ProfileName = SettingsUtils.TrimStringValue(startupConfig.ProfileName)
      });
    settings.MechanicalSettings.IsEnabled = this.msEnableSupport;
    settings.MechanicalSettings.AssemblyDrawings.AddRange((IEnumerable<DrawingTypeSettings>) this.msAssemblyDrawings);
    settings.MechanicalSettings.PartDrawings.AddRange((IEnumerable<DrawingTypeSettings>) this.msPartDrawings);
    settings.ConstructionalSettings.IsEnabled = this.csEnableSupport;
    settings.ConstructionalSettings.Drawings.AddRange((IEnumerable<DrawingTypeSettings>) this.csDrawings);
    return settings;
  }

  public SettingsSurrogate Clone() => new SettingsSurrogate(this.ToSettings());

  object ICloneable.Clone() => (object) this.Clone();

  [Category("1. Общие настройки")]
  [DisplayName("Параметры подключения к приложению")]
  [Description("Это свойство позволяет задать нужные параметры подключения к приложению в зависимости от роли пользователя в IPS")]
  [Editor(typeof (StartupConfigurationListUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<StartupConfigurationSurrogate> StartupConfigurations
  {
    get => this.startupConfigs;
    set => this.startupConfigs = value;
  }

  [Category("2. Конструкторская документация")]
  [DisplayName("Включить поддержку?")]
  [Description("Включает и выключает поддержку конструкторских чертежей.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool EnableMechanicalSupport
  {
    get => this.msEnableSupport;
    set => this.msEnableSupport = value;
  }

  [Category("2. Конструкторская документация")]
  [DisplayName("Сборочные чертежи")]
  [Description("Список типов документов IPS, соответствующих сборочным чертежам. Также содержит дополнительные настройки сканирования и обработки сборочных чертежей.")]
  [Editor(typeof (DrawingTypeListUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<DrawingTypeSettings> MechanicalAssemblies
  {
    get => this.msAssemblyDrawings;
    set => this.msAssemblyDrawings = value;
  }

  [Category("2. Конструкторская документация")]
  [DisplayName("Чертежи деталей")]
  [Description("Список типов документов IPS, соответствующих чертежам деталей. Также содержит дополнительные настройки сканирования и обработки чертежей деталей.")]
  [Editor(typeof (DrawingTypeListUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<DrawingTypeSettings> MechanicalParts
  {
    get => this.msPartDrawings;
    set => this.msPartDrawings = value;
  }

  [Category("3. Проектная документация")]
  [DisplayName("Включить поддержку?")]
  [Description("Включает и выключает поддержку СПДС-чертежей.")]
  [TypeConverter(typeof (YesNoConverter))]
  public bool EnableConstructionalSupport
  {
    get => this.csEnableSupport;
    set => this.csEnableSupport = value;
  }

  [Category("3. Проектная документация")]
  [DisplayName("СПДС-Чертежи")]
  [Description("Список типов документов IPS, соответствующих СПДС-чертежам. Также содержит дополнительные настройки сканирования и обработки чертежей.")]
  [Editor(typeof (DrawingTypeListUIEditor), typeof (UITypeEditor))]
  public ChangeTrackingListAdapter<DrawingTypeSettings> ConstructionalDrawings
  {
    get => this.csDrawings;
    set => this.csDrawings = value;
  }
}
