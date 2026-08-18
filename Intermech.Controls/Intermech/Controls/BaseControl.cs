
// Type: Intermech.Controls.BaseControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>Базовый класс для создания собственных контролов в IPS, слегка расширяющий функциональность</summary>
public class BaseControl : 
  SimpleBaseControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider
{
  [NotNull]
  private readonly ControlServiceContainer _controlServiceContainer;

  /// <summary>Default constructor</summary>
  public BaseControl()
  {
    this._controlServiceContainer = new ControlServiceContainer((Control) this);
  }

  /// <summary>Releases the unmanaged resources used by the Intermech.Windows.Forms.BaseUserControl and optionally releases the managed
  /// resources.</summary>
  /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this._controlServiceContainer.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Контекст (контейнер сервисов) для элемента пространства навигации</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider Services
  {
    [NotNull, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (System.IServiceProvider) this._controlServiceContainer;
    }
    [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._controlServiceContainer.AdvancedProvider = value;
    }
  }

  /// <summary>"Местный" контейнер сервисов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IControlServiceContainer ServiceContainer
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IControlServiceContainer) this._controlServiceContainer;
    }
  }

  /// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its
  /// <see cref="T:System.ComponentModel.Container" />.</summary>
  /// <param name="serviceType"></param>
  /// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />,
  /// or null if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
  protected override object GetService(System.Type serviceType)
  {
    return base.GetService(serviceType) ?? this._controlServiceContainer.GetService(serviceType);
  }

  /// <summary>Gets the service object of the specified type.</summary>
  /// <param name="serviceType">An object that specifies the type of service object to get.</param>
  /// <returns>A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type
  /// <paramref name="serviceType" />.</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  object System.IServiceProvider.GetService([NotNull] System.Type serviceType)
  {
    return this.GetService(serviceType);
  }

  /// <summary>Попытка извлечения сервиса из провайдера сервисов</summary>
  /// <exception cref="T:System.InvalidOperationException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
  /// сервиса (при аргументе throwExceptionIfNotFound == true)</exception>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="throwExceptionIfNotFound">Выбрасывать ли исключительную ситуацию в случае отсутствия в провайдере запрашиваемого
  /// сервиса</param>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
  /// <returns>Извекаемый сервис</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetService<T>(bool throwExceptionIfNotFound = true, [CanBeNull] string exceptionMessageIfFail = null)
  {
    return this._controlServiceContainer.GetService<T>(throwExceptionIfNotFound, exceptionMessageIfFail);
  }

  /// <summary>Извлечение сервиса из провайдера сервисов</summary>
  /// <exception cref="T:System.InvalidOperationException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
  /// сервиса</exception>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
  /// <returns>Извекаемый сервис</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T GetService<T>([CanBeNull] string exceptionMessageIfFail)
  {
    return this._controlServiceContainer.GetService<T>(exceptionMessageIfFail);
  }

  /// <summary>Извлечение сервиса из провайдера</summary>
  /// <exception cref="T:System.InvalidOperationException">Исключительная ситуация выбрасывается в случае отсутствия у провайдера запрашиваемого
  /// сервиса</exception>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="service">[out] извлекаемый сервис</param>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
  /// <returns>Провайдер сервисов (для построения цепочки вызовов)</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public System.IServiceProvider GetService<T>([NotNull] out T service, [CanBeNull] string exceptionMessageIfFail = null)
  {
    return this._controlServiceContainer.GetService<T>(out service, exceptionMessageIfFail);
  }

  /// <summary>Попытка извлечения сервиса из провайдера сервисов</summary>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="service">[out] извлекаемый сервис</param>
  /// <returns>true если сервис был получен, иначе false</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool TryGetService<T>([CanBeNull] out T service)
  {
    return this._controlServiceContainer.TryGetService<T>(out service);
  }

  /// <summary>Контоль того, что сервис был извлечён из провайдера в переданную переменную. Если нет (она null), то извлечь в неё сервис</summary>
  /// <typeparam name="T">Тип извлекаемого сервиса</typeparam>
  /// <param name="service">[in, out] извлекаемый сервис</param>
  /// <param name="exceptionMessageIfFail">Текст сообщения об ошибке, в выбрасываемой исключительной ситуации в случае отсутствия
  /// запрашиваемого сервиса в провайдере. Если null, то используется стандартное сообщение</param>
  /// <returns>извлекаемый сервис</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public T EnsureInitialized<T>([CanBeNull] ref T service, [CanBeNull] string exceptionMessageIfFail = null) where T : class
  {
    return this._controlServiceContainer.EnsureInitialized<T>(ref service, exceptionMessageIfFail);
  }

  /// <summary>Ассоциированный с контейнером сервисов контрол</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  Control IControlServiceContainer.Control
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Control) this;
  }

  /// <summary>Ближайший по иерархии контролов-владельцев вверх контрол, являющийся провайдером сервисов</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  System.IServiceProvider IControlServiceContainer.ParentControlServices
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.ParentControlServices;
    }
  }

  /// <summary>Дополнительный контейнер сервисов</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  System.IServiceProvider IAdvancedServiceContainer.AdvancedProvider
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.AdvancedProvider;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._controlServiceContainer.AdvancedProvider = value;
    }
  }

  /// <summary>Adds the specified service to control, and optionally promotes the service to parent service containers</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but
  /// delays the creation of the object until the service is requested</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, callback, promote);
  }

  /// <summary>Adds the specified service to the service container</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but
  /// delays the creation of the object until the service is requested</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback)
  {
    this._controlServiceContainer.AddService(serviceType, callback);
  }

  /// <summary>Adds the specified service to the service container, and optionally promotes the service to parent service containers</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by
  /// the serviceType parameter</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance, promote);
  }

  /// <summary>Adds the specified service to the service container</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by
  /// the serviceType parameter</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance);
  }

  /// <summary>Добавить сервис в контейнер сервисов</summary>
  /// <typeparam name="T">Тип помещаемого в контейнер сервиса</typeparam>
  /// <param name="service">Помещаемый в контейнер сервиса</param>
  /// <param name="promote">Добавлять ли сервис так же во все родительские контейнеры</param>
  public void AddService<T>([NotNull] T service, bool promote = false)
  {
    this._controlServiceContainer.AddService(typeof (T), (object) service, promote);
  }

  /// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service
  /// containers</summary>
  /// <param name="serviceType">The type of service to remove</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void RemoveService([NotNull] System.Type serviceType, bool promote)
  {
    this._controlServiceContainer.RemoveService(serviceType, promote);
  }

  /// <summary>Removes the specified service type from the service container</summary>
  /// <param name="serviceType">The type of service to remove</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void RemoveService([NotNull] System.Type serviceType)
  {
    this._controlServiceContainer.RemoveService(serviceType);
  }

  /// <summary>Изъять сервис из контейнера сервисов</summary>
  /// <typeparam name="T">Тип изымаемого из контейнера сервиса</typeparam>
  /// <param name="promote">Изымать ли сервис так же из всех родительских контейнеров</param>
  /// <returns>Исходный контейнер сервисов</returns>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IServiceContainer RemoveService<T>(bool promote = false)
  {
    return this._controlServiceContainer.RemoveService<T>(promote);
  }
}
