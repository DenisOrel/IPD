// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPContext
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Класс, содержащий контейнер сервисов (контекст MRP)</summary>
[Serializable]
public class MRPContext : IMRPContext, IMRPAdvancedContext, IDisposable
{
  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого осуществляется некоторое действие)
  /// </summary>
  [NonSerialized]
  protected AdvancedServiceContainer services = new AdvancedServiceContainer();

  /// <summary>Создать заполненный контекст MRP</summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  public MRPContext(IServiceProvider services) => this.services.AdvancedProvider = services;

  /// <summary>Создать частично заполненный контекст MRP</summary>
  /// <param name="session">Добавить сессию в контейнер сервисов, если она задана</param>
  public MRPContext(IUserSession session)
  {
    if (session == null)
      return;
    this.services.AddService(typeof (IUserSession), (object) session);
  }

  /// <summary>Создать заполненный контекст MRP</summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="session">Добавить сессию в контейнер сервисов, если она задана</param>
  public MRPContext(IServiceProvider services, IUserSession session)
  {
    if (session != null)
      this.services.AddService(typeof (IUserSession), (object) session);
    this.services.AdvancedProvider = services;
  }

  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого осуществляется некоторое действие)
  /// </summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this.services;
    set
    {
      if (this.services.AdvancedProvider == value || this.services == value)
        return;
      this.services.AdvancedProvider = value;
    }
  }

  /// <summary>
  /// Контейнер сервисов (контекст, в рамках которого осуществляется некоторое действие)
  /// </summary>
  AdvancedServiceContainer IMRPAdvancedContext.Services
  {
    [DebuggerStepThrough] get => this.services;
  }

  /// <summary>Освободить ресурсы из контекста</summary>
  public void Dispose()
  {
    if (this.services == null)
      return;
    if (this.services.GetService(typeof (IUserSession)) is IUserSession)
      this.services.RemoveService(typeof (IUserSession));
    this.services = (AdvancedServiceContainer) null;
  }
}
