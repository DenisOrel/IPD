// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileSettingsService`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Settings;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

/// <summary>
/// Внутренний сервис интегратора, который позволяет получить доступ к настройкам интегратора.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class SingleFileSettingsService<TSettings>(IIntegrator owner) : 
  IntegratorSettingsService<TSettings>(owner),
  IDocumentAttributesSettingsService,
  IIntegratorSettingsService,
  IIntegratorService
  where TSettings : class, ISettingsObject
{
  private ISynchronizedObjectAttributes docAttributes;

  /// <summary>
  /// Обработчик события, который вызывается сразу после успешной инициализации сервиса.
  /// Может использоваться для выполнения действий, требующих предварительной полной инициализации сервиса.
  /// </summary>
  protected override void DoAfterInitialize()
  {
    base.DoAfterInitialize();
    this.docAttributes = this.CreateDocumentAttributesProvider();
  }

  protected override IntegratorSettingsCodec CreateSettingsCodec()
  {
    return (IntegratorSettingsCodec) new SingleFileSettingsCodec(this.Integrator.DisplayName);
  }

  protected override IntegratorSettingsValidator CreateSettingsValidator()
  {
    return (IntegratorSettingsValidator) new SingleFileSettingsValidator(this.Integrator.DisplayName);
  }

  protected virtual ISynchronizedObjectAttributes CreateDocumentAttributesProvider()
  {
    return (ISynchronizedObjectAttributes) new SingleFileSynchronizedAttributes((IIntegratorSettingsService) this);
  }

  /// <summary>
  /// Возвращает объект, позволяющий получить коллекцию синхронизируемых атрибутов документов.
  /// </summary>
  public ISynchronizedObjectAttributes SynchronizedDocumentAttributes
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this.docAttributes;
    }
  }
}
