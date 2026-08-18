// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.MainServicesContainer
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.ComponentModel.Design;
using System.Diagnostics;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Ссылка на основной контейнер сервисов</summary>
public static class MainServicesContainer
{
  /// <summary>Контейнер сервисов</summary>
  private static IServiceContainer _services = (IServiceContainer) new System.ComponentModel.Design.ServiceContainer();

  /// <summary>Контейнер сервисов</summary>
  public static IServiceContainer ServiceContainer
  {
    [DebuggerStepThrough] get
    {
      MainServicesContainer._services = MainServicesContainer._services ?? (IServiceContainer) new System.ComponentModel.Design.ServiceContainer();
      return MainServicesContainer._services;
    }
    set => MainServicesContainer._services = value ?? MainServicesContainer._services;
  }

  /// <summary>Получить сервис указанного типа</summary>
  /// <param name="serviceType">Запрашиваемый тип</param>
  /// <returns>Результат или null</returns>
  public static object GetService(Type serviceType)
  {
    return MainServicesContainer.ServiceContainer.GetService(serviceType);
  }

  /// <summary>Добавить сервис указанного типа</summary>
  /// <param name="serviceType">Добавляемый тип</param>
  /// <param name="service">Экземпляр добавляемого типа</param>
  public static void AddService(Type serviceType, object service)
  {
    MainServicesContainer.ServiceContainer.AddService(serviceType, service);
  }

  /// <summary>Удалить сервис указанного типа</summary>
  /// <param name="serviceType">Удаляемый тип</param>
  public static void RemoveService(Type serviceType)
  {
    MainServicesContainer.ServiceContainer.RemoveService(serviceType);
  }
}
