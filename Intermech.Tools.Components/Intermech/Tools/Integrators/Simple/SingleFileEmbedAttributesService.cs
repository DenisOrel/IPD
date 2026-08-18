// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileEmbedAttributesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.DataExchange;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Сервис интегратора, отвечающий за передачу изменений из карточки объекта в его файлы.
/// </summary>
public class SingleFileEmbedAttributesService : EmbedAttributesService
{
  private readonly SingleFileDataExchangeFactory dataExchangeFactory;
  private IEmbedAttributesDriver driver;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <param name="dataExchangeFactory">Фабрика драйверов</param>
  /// <exception cref="T:System.ArgumentNullException">owner or dataExchangeFactory</exception>
  public SingleFileEmbedAttributesService(
    IIntegrator owner,
    SingleFileDataExchangeFactory dataExchangeFactory)
    : base(owner)
  {
    this.dataExchangeFactory = dataExchangeFactory != null ? dataExchangeFactory : throw new ArgumentNullException(nameof (dataExchangeFactory));
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = this.dataExchangeFactory.CreateEmbedAttributesDriver();
  }

  /// <summary>
  /// Возвращает драйвер для работы с атрибутами документов.
  /// </summary>
  protected sealed override IEmbedAttributesDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
  }
}
