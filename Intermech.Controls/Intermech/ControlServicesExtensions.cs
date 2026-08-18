
// Type: Intermech.ControlServicesExtensions
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech;

/// <summary>Методы расширения для класса Control для работы с сервисами (IContextAware, IServiceProvider, IAdvancedServiceContainer)</summary>
public static class ControlServicesExtensions
{
  /// <summary>Получить реализацию интерфейса IContextAware у контрола</summary>
  /// <param name="control">Контрол, у которого ищется IContextAware</param>
  /// <param name="onlyLocal">если true то ищется только локальный IContextAware контрола, иначе IContextAware ищется вверх по структуре вложенности контролов</param>
  /// <returns>Провайдер сервисов</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IContextAware GetContextAware([NotNull] this Control control, bool onlyLocal = false)
  {
    if (control is IContextAware contextAware)
      return contextAware;
    return !onlyLocal ? control.GetParentsEnumeration().SelectFirstNotNull<Control, IContextAware>((Func<Control, IContextAware>) (ctrl => ctrl.GetContextAware())) : (IContextAware) null;
  }

  /// <summary>Получить у контрола провайдер сервисов</summary>
  /// <param name="control">Контрол, провайдер сервисов которого ищется</param>
  /// <param name="onlyLocal">если true то ищется только локальный провайдер сервисов, иначе провайдер ищется вверх по структуре вложенности контролов</param>
  /// <returns>Провайдер сервисов</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static System.IServiceProvider GetServiceProvider([NotNull] this Control control, bool onlyLocal = false)
  {
    System.IServiceProvider services = control is IContextAware contextAware ? contextAware.Services : (System.IServiceProvider) null;
    if (services != null)
      return services;
    if (control is System.IServiceProvider serviceProvider)
      return serviceProvider;
    return !onlyLocal ? control.GetParentsEnumeration().SelectFirstNotNull<Control, System.IServiceProvider>((Func<Control, System.IServiceProvider>) (ctrl => ctrl.GetServiceProvider())) : (System.IServiceProvider) null;
  }

  /// <summary>Получить у контрола контейнер сервисов</summary>
  /// <param name="control">Контрол, контейнер сервисов которого ищется</param>
  /// <param name="onlyLocal">если true то ищется только локальный контейнер сервисов, иначе провайдер ищется вверх по структуре вложенности контролов</param>
  /// <returns>Контейнер сервисов</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IServiceContainer GetServiceContainer([NotNull] this Control control, bool onlyLocal = false)
  {
    if (!((control is IContextAware contextAware ? contextAware.Services : (System.IServiceProvider) null) is IServiceContainer serviceContainer))
      serviceContainer = control as IServiceContainer;
    if (serviceContainer != null)
      return serviceContainer;
    return !onlyLocal ? control.GetParentsEnumeration().SelectFirstNotNull<Control, IServiceContainer>((Func<Control, IServiceContainer>) (ctrl => ctrl.GetServiceContainer())) : (IServiceContainer) null;
  }

  /// <summary>Gets the service object of the specified type.</summary>
  /// <param name="control"></param>
  /// <param name="serviceType">An object that specifies the type of service object to get.</param>
  /// <returns>A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type
  /// <paramref name="serviceType" />.</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static object GetService([NotNull] this Control control, [NotNull] System.Type serviceType)
  {
    return control.GetServiceProvider()?.GetService(serviceType);
  }
}
