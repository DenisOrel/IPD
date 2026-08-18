// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ContextServicesStack`1
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>База для стека однотипных сервисов в локальном контексте.
/// Позволяет организовывать работу однотивных сервисов, находящихся во вложенных друг в
/// друга контейнерах. Например "фильтрация команд контекстного меню должна осуществляться контролом, а так же всеми
/// контролами, в которые он вложен (поддерживающих сервис фильтрации команд)"</summary>
/// <typeparam name="ServiceType">Тип сервиса</typeparam>
public class ContextServicesStack<ServiceType> : IContextServicesStack<ServiceType> where ServiceType : class
{
  public List<ServiceType> _commandsProviders;

  /// <summary>Конструктор</summary>
  /// <param name="localContext">Локальный контекст (контейнер сервисов)</param>
  /// <param name="localService">Локальный сервис, которые будут доступен только в данном контексте</param>
  public ContextServicesStack(IServiceContainer localContext, ServiceType localService)
  {
    IContextServicesStack<ServiceType> service = localContext.GetService<IContextServicesStack<ServiceType>>(false);
    this._commandsProviders = service == null ? new List<ServiceType>(1) : new List<ServiceType>(service.Enumeration);
    if ((object) localService == null)
      return;
    this._commandsProviders.Add(localService);
  }

  /// <summary>Перечисление сервисов, сложенных в стек в данном контексте</summary>
  public IEnumerable<ServiceType> Enumeration
  {
    [DebuggerStepThrough] get => (IEnumerable<ServiceType>) this._commandsProviders;
  }
}
