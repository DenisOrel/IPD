// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADSettingsFactory
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует фабрику для объектов с настройками интегратора, а также всех вспомогательных объектов.
/// </summary>
public abstract class CADSettingsFactory : ICADSettingsFactory, ISettingsObjectFactory
{
  private readonly CADIntegrator integrator;

  /// <summary>Создает объект.</summary>
  /// <param name="integrator">Интегратор, с которым связана фабрика</param>
  protected CADSettingsFactory(CADIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  /// <summary>Создает пустой объект настроек интегратора.</summary>
  /// <returns>Объект с настройками интегратора</returns>
  public CADSettings CreateSettingsObject()
  {
    CADSettings settingsObject = this.DoCreateSettingsObject();
    IMultiCADSupport service = ServiceUtils.GetService<IMultiCADSupport>((object) this.integrator, false);
    if (service != null)
      settingsObject.JTDerivedDocumentType = service.JTDerivedDocumentType;
    return settingsObject;
  }

  /// <summary>Создает пустой объект настроек интегратора.</summary>
  /// <returns>Объект с настройками интегратора</returns>
  protected abstract CADSettings DoCreateSettingsObject();

  /// <summary>
  /// Создает пустую модель представления для объекта настроек интегратора. Модель представления используется редактором
  /// настроек интегратора на основе PropertyGrid. На модель навешиваются все необхоимые атрибуты, управляющие поведение PropertyGrid.
  /// </summary>
  /// <returns>Модель представления</returns>
  public CADSettingsViewModel CreateSettingsViewModel() => this.DoCreateSettingsViewModel();

  /// <summary>
  /// Создает пустую модель представления для объекта настроек интегратора. Модель представления используется редактором
  /// настроек интегратора на основе PropertyGrid. На модель навешиваются все необхоимые атрибуты, управляющие поведение PropertyGrid.
  /// </summary>
  /// <returns>Модель представления</returns>
  protected abstract CADSettingsViewModel DoCreateSettingsViewModel();

  /// <summary>Создает валидатор настроек интегратора.</summary>
  /// <returns>Объект валидатора</returns>
  public IntegratorSettingsValidator CreateValidator()
  {
    List<ISettingsValidatorCheck> settingsValidatorCheckList = new List<ISettingsValidatorCheck>(16 /*0x10*/);
    this.DoCreateValidatorChecks(this.integrator, settingsValidatorCheckList);
    IntegratorSettingsValidator validator = new IntegratorSettingsValidator(this.integrator.DisplayName);
    validator.AddChecks((IEnumerable<ISettingsValidatorCheck>) settingsValidatorCheckList);
    return validator;
  }

  /// <summary>
  /// Заполняет список тестов, которые будут использованы валидатором настроек интегратора для проверки корректности настроек.
  /// </summary>
  /// <param name="integrator">Интегратор</param>
  /// <param name="checkList">Список тестов для проверки настроек интегратора</param>
  protected virtual void DoCreateValidatorChecks(
    CADIntegrator integrator,
    List<ISettingsValidatorCheck> checkList)
  {
    checkList.Add((ISettingsValidatorCheck) new DocumentGroupsCheck((IEnumerable<string>) CADSettings.CommonGroups.All));
    checkList.Add((ISettingsValidatorCheck) new DocumentRootsCheck());
    checkList.Add((ISettingsValidatorCheck) new StandardPartTypeCheck());
    checkList.Add((ISettingsValidatorCheck) new DrawingSuffixesCheck());
    checkList.Add((ISettingsValidatorCheck) new ObjectTypesRootCheck());
    checkList.Add((ISettingsValidatorCheck) new MultiCADSupportCheck((IIntegrator) integrator));
    checkList.Add((ISettingsValidatorCheck) new UnpairedDocumentTypesCheck());
  }

  /// <summary>Создает кодек настроек интегратора.</summary>
  /// <returns>Объект кодека</returns>
  public CADSettingsCodec CreateCodec()
  {
    return this.DoCreateCodec(this.integrator.DisplayName, (ISettingsObjectFactory) this);
  }

  /// <summary>Создает кодек настроек интегратора.</summary>
  /// <param name="integratorName">Название интегратора</param>
  /// <param name="factory">Фабрика объектов настроек</param>
  /// <returns>Объект кодека</returns>
  protected abstract CADSettingsCodec DoCreateCodec(
    string integratorName,
    ISettingsObjectFactory factory);

  /// <summary>Создает сервис для доступа к настройкам интегратора.</summary>
  /// <param name="sharedModelAttributes">Признак, что атрибуты документа и атрибуты конфигураций хранятся в одном контейнере</param>
  /// <returns>Созданный объект сервиса</returns>
  public ICADSettingsService CreateSettingsService(bool sharedModelAttributes)
  {
    return (ICADSettingsService) this.DoCreateSettingsService(this.integrator, sharedModelAttributes);
  }

  /// <summary>Создает сервис для доступа к настройкам интегратора.</summary>
  /// <param name="integrator">Интегратор</param>
  /// <param name="sharedModelAttributes">Признак, что атрибуты документа и атрибуты конфигураций хранятся в одном контейнере</param>
  /// <returns>Созданный объект сервиса</returns>
  protected virtual CADSettingsService DoCreateSettingsService(
    CADIntegrator integrator,
    bool sharedModelAttributes)
  {
    return new CADSettingsService((IIntegrator) integrator, this, sharedModelAttributes);
  }

  /// <summary>Создает пустой объект настроек.</summary>
  /// <returns>Пусто объект настроек</returns>
  ISettingsObject ISettingsObjectFactory.CreateSettingsObject()
  {
    return (ISettingsObject) this.CreateSettingsObject();
  }
}
