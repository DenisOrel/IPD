// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileImportService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileImportService : FileImportService
{
  private readonly SingleFileDataExchangeFactory dataExchangeFactory;
  private ICaptureChangesDriver captureDriver;

  public SingleFileImportService(
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
    this.AllowTransferFileToWorkspace = TransferFileToWorkspaceMode.SourceFileOnly;
    this.captureDriver = this.dataExchangeFactory.CreateCaptureChangesDriver();
  }

  protected sealed override ICaptureChangesDriver GetCaptureChangesDriver() => this.captureDriver;
}
