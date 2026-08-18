// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADEmbedAttributesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис интегратора, отвечающий за передачу изменений из карточки объекта в его файлы.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец сервиса</param>
public class CADEmbedAttributesService(IIntegrator owner) : EmbedAttributesService(owner)
{
  private CIEmbedAttributesDriver driver;
  private bool enableIMViewerExtension;

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = this.CreateDriver();
  }

  /// <summary>
  /// Включает или выключает расширение для интеграции с IMViewer.
  /// Свойство должно быть заполнено до начала использования текущего сервиса.
  /// По умолчанию значение свойства равно false.
  /// </summary>
  public bool EnableIMViewerExtension
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.enableIMViewerExtension;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.enableIMViewerExtension = value;
      }
    }
  }

  /// <summary>Создает драйвер для работы с атрибутами документов.</summary>
  /// <returns>Объект драйвера</returns>
  protected virtual CIEmbedAttributesDriver CreateDriver()
  {
    CIEmbedAttributesDriver driver = new CIEmbedAttributesDriver(this.Integrator);
    if (this.EnableIMViewerExtension)
      this.AddIMViewerExtension(driver);
    return driver;
  }

  private void AddIMViewerExtension(CIEmbedAttributesDriver driver)
  {
    IMViewerObjectsEmbedAttributesExtension attributesExtension = new IMViewerObjectsEmbedAttributesExtension(driver, new IMViewerObjectsIDCache(MetadataResolvers.Factory), ServiceUtils.GetService<ICADSettingsService>((object) this.Integrator, true));
    driver.SidecarObjectsExtensions.Add((ISidecarObjectsEmbedAttributesExtension) attributesExtension);
  }

  /// <summary>
  /// Возвращает драйвер для работы с атрибутами документов.
  /// </summary>
  protected override IEmbedAttributesDriver Driver
  {
    [DebuggerStepThrough] get => (IEmbedAttributesDriver) this.driver;
  }
}
