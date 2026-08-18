
// Type: Intermech.Interfaces.Client.ControlServiceContainer
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


namespace Intermech.Interfaces.Client;

/// <summary>
/// Контейнер сервисов, привязанный к контролу и связанной с ней иерархией вложенности контролов
///   родительским провайдером для данного контейнера сервисов всегда выступает ближайший по иерархии вверх владелец связанного с интерфейсом контрола, поддерживающий IContextAware или IServiceProvider
/// наследуется от IAdvancedServiceContainer, то есть может иметь дополнительный список сервисов, связанным с логическим контекстом
/// </summary>
public class ControlServiceContainer : 
  AdvancedServiceContainer,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider
{
  /// <summary>Связанный с контейнером сервисов провайдер</summary>
  [NotNull]
  private readonly Control _control;

  /// <summary>Получить контейнер сервисов контрола</summary>
  /// <param name="control">Ассоциированный с провайдером контрол</param>
  /// <returns>Контейнер сервисов контрола</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IServiceContainer GetControlServiceContainer([NotNull] Control control)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!control.IsDisposed, "!IsDisposed");
    return (control is IContextAware contextAware ? contextAware.Services : (System.IServiceProvider) null) is IServiceContainer services ? services : control as IServiceContainer;
  }

  /// <summary>Ищет контейнер сервисов из контролов - владельцев переданного</summary>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IServiceContainer FindParentControlServiceContainer([NotNull] Control control)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!control.IsDisposed, "!IsDisposed");
    return control.GetParentsEnumeration().SelectFirstNotNull<Control, IServiceContainer>(new Func<Control, IServiceContainer>(ControlServiceContainer.GetControlServiceContainer));
  }

  /// <summary>Constructor</summary>
  /// <exception cref="T:System.ArgumentNullException">Thrown when control parameter is null.</exception>
  /// <param name="control">Ассоциированный с провайдером контрол</param>
  /// <param name="logicContextServices">Провайдер сервисов логического контекста</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ControlServiceContainer([NotNull] Control control, [CanBeNull] System.IServiceProvider logicContextServices = null)
    : base((System.IServiceProvider) null, logicContextServices)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!control.IsDisposed, "!IsDisposed");
    this._control = control;
  }

  /// <summary>Получить ссылку на сервис указанного типа</summary>
  /// <param name="serviceType">Тип запрашиваемого сервиса</param>
  /// <returns>Сервис запрошенного типа или null, если сервис не найден</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override object GetService(System.Type serviceType) => this.GetService(serviceType, true);

  /// <summary>Получить ссылку на сервис указанного типа</summary>
  /// <param name="serviceType">Тип запрашиваемого сервиса</param>
  /// <returns>Сервис запрошенного типа или null, если сервис не найден</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public object GetService([NotNull] System.Type serviceType, bool includeSelfServices)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
    if (includeSelfServices)
    {
      System.Type type = this._control.GetType();
      if (serviceType.IsAssignableFrom(type))
        return (object) this._control;
      object service = base.GetService(serviceType);
      if (service != null)
        return service;
    }
    else
    {
      object service = this.AdvancedProvider?.GetService(serviceType);
      if (service != null)
        return service;
    }
    bool flag = false;
    foreach (Control service1 in this._control.GetParentsEnumeration())
    {
      System.Type type = service1.GetType();
      if (serviceType.IsAssignableFrom(type))
        return (object) service1;
      if (!flag)
      {
        if (!(service1 is System.IServiceProvider serviceProvider) && service1 is IContextAware contextAware)
          serviceProvider = contextAware.Services;
        if (serviceProvider != null && serviceProvider != this.AdvancedProvider)
        {
          flag = true;
          object service2 = serviceProvider.GetService(serviceType);
          if (service2 != null)
            return service2;
        }
      }
    }
    return (object) null;
  }

  /// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but
  /// delays the creation of the object until the service is requested</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override void AddService(System.Type serviceType, [NotNull] ServiceCreatorCallback callback, bool promote)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
    base.AddService(serviceType, callback, false);
    if (!promote)
      return;
    ControlServiceContainer.FindParentControlServiceContainer(this._control)?.AddService(serviceType, callback, true);
  }

  /// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by
  /// the serviceType parameter</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override void AddService(System.Type serviceType, [NotNull] object serviceInstance, bool promote)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
    base.AddService(serviceType, serviceInstance, false);
    if (!promote)
      return;
    ControlServiceContainer.FindParentControlServiceContainer(this._control)?.AddService(serviceType, serviceInstance, true);
  }

  /// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service
  /// containers</summary>
  /// <param name="serviceType">The type of service to remove</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public override void RemoveService(System.Type serviceType, bool promote)
  {
    Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
    base.RemoveService(serviceType, false);
    if (!promote)
      return;
    ControlServiceContainer.FindParentControlServiceContainer(this._control)?.RemoveService(serviceType, true);
  }

  /// <summary>Ассоциированный с контейнером сервисов контрол</summary>
  [NotNull]
  public Control Control
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
      return this._control;
    }
  }

  /// <summary>Родительский сервис контейнеров</summary>
  [CanBeNull]
  public System.IServiceProvider ParentControlServices
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Diagnostics.Check.Assert<ObjectDisposedException>(!this._control.IsDisposed, "!IsDisposed");
      foreach (Control control in this._control.GetParentsEnumeration())
      {
        if (!(control is System.IServiceProvider parentControlServices) && control is IContextAware contextAware)
          parentControlServices = contextAware.Services;
        if (parentControlServices != null && parentControlServices != this.AdvancedProvider)
          return parentControlServices;
      }
      return (System.IServiceProvider) null;
    }
  }
}
