
// Type: Intermech.Windows.Forms.BaseUserControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Bars;
using Intermech.Common;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Windows.Forms;

/// <summary>Базовый User Control</summary>
public class BaseUserControl : 
  SimpleBaseUserControl,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IDesignModeControlsContainer,
  IArrowKeysNavigationSupported,
  ILastFocusedControlTracker,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Объект для асинхронной инициализации статического кэша</summary>
  [NotNull]
  private static readonly object _staticSyncObj = new object();
  [CanBeNull]
  private static IHotKeysManager _hotKeysManager;
  [CanBeNull]
  private static INamedImageList _namedImageList;
  [CanBeNull]
  private static ICommandManager _commandManager;
  /// <summary>Объект, имплементирующий интерфейсы IContextAware и IControlServiceContainer</summary>
  [NotNull]
  private readonly ControlServiceContainer _controlServiceContainer;
  /// <summary>Признак того, что контрол читает настройки после загрузки</summary>
  protected bool _LoadPropsFromStorageOnLoadControl;
  /// <summary>Признак того, что содержимое контрола недоступно для редактирования</summary>
  private bool _isReadOnly;
  /// <summary>Принудительная блокировка редактирования. Включение этого флага исключает возможность разблокировать возможность редактирования перекрывая IsReadOnlyCanBeChanged в потомках и вызывая UpdateReadOnly</summary>
  private bool _forceIsReadOnly;
  /// <summary>Счётчик блокировок обновления доступности команд</summary>
  private int _lockUpdateCommandsCounter;
  /// <summary>Флаг необходимости обновления доступности команд после обнуления счётчика блокировок</summary>
  private bool _needUpdateAfterUnlock;
  /// <summary>Счётчик блокировок установки флага необходимости обновления доступности команд после обнуления
  /// счётчика блокировок в том случае, если во время блокировки будет вызван UpdateCommands()</summary>
  private int _forceLockUpdateCommandsCounter;
  /// <summary>объект для синхронизации операций блокировки синхронизации</summary>
  [NotNull]
  private readonly object _syncObjLockUpdateCommands = new object();

  /// <summary>Сервис службы "горячих клавиш" и связанных с ними команд</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected IHotKeysManager HotKeysManager
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (BaseUserControl._hotKeysManager == null)
      {
        lock (BaseUserControl._staticSyncObj)
        {
          if (BaseUserControl._hotKeysManager == null)
          {
            BaseUserControl._hotKeysManager = ServicesManager.GetService<IHotKeysManager>(true, "IHotKeysManager not found in ServicesManager");
            Thread.MemoryBarrier();
          }
        }
      }
      return BaseUserControl._hotKeysManager;
    }
  }

  /// <summary>Сервис для работы с именованными иконками</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected INamedImageList NamedImageList
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (BaseUserControl._namedImageList == null)
      {
        lock (BaseUserControl._staticSyncObj)
        {
          if (BaseUserControl._namedImageList == null)
          {
            BaseUserControl._namedImageList = ServicesManager.GetService<INamedImageList>(true, "INamedImageList not found in ServicesManager");
            Thread.MemoryBarrier();
          }
        }
      }
      return BaseUserControl._namedImageList;
    }
  }

  /// <summary>Менеджер команд</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected ICommandManager CommandManager
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (BaseUserControl._commandManager == null)
      {
        lock (BaseUserControl._staticSyncObj)
        {
          if (BaseUserControl._commandManager == null)
          {
            BaseUserControl._commandManager = ServicesManager.GetService<ICommandManager>(true, "ICommandManager not found in ServicesManager");
            Thread.MemoryBarrier();
          }
        }
      }
      return BaseUserControl._commandManager;
    }
  }

  /// <summary>Default constructor</summary>
  public BaseUserControl()
  {
    this._controlServiceContainer = new ControlServiceContainer((Control) this);
    this.AddService<INamedContext>((INamedContext) this);
    this.InitSaveLockService();
  }

  /// <summary>Releases the unmanaged resources used by the Intermech.Windows.Forms.BaseUserControl and optionally releases the managed
  /// resources.</summary>
  /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
  protected override void Dispose(bool disposing)
  {
    this.LockUpdateCommands();
    if (disposing)
    {
      this.SaveLocker = (LocksManager) null;
      this.ReadOnlyCanBeChangedEvent = (CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler) null;
      this.RemoveService<INamedContext>();
      this._controlServiceContainer.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnLoad([CanBeNull] EventArgs e)
  {
    base.OnLoad(e);
    if (!this._LoadPropsFromStorageOnLoadControl || this.InDesignMode)
      return;
    this.LoadPropertiesFromStorage();
  }

  /// <summary>Вызов события AfterShown - после первого отображения контрола (первого WM_PAINT)</summary>
  protected override void FireFirstPaint()
  {
    base.FireFirstPaint();
    this.UpdateCommands();
  }

  /// <summary>Контекст (контейнер сервисов) для элемента пространства навигации</summary>
  [NotNull]
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
  /// <returns>Извлекаемый сервис</returns>
  [ContractAnnotation("throwExceptionIfNotFound:false => CanBeNull; => NotNull")]
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
  /// <returns>Извлекаемый сервис</returns>
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

  /// <summary>Контроль того, что сервис был извлечён из провайдера в переданную переменную. Если нет (она null), то извлечь в неё сервис</summary>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => (Control) this;
  }

  /// <summary>Ближайший по иерархии контролов-владельцев вверх контрол, являющийся провайдером сервисов</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  System.IServiceProvider IControlServiceContainer.ParentControlServices
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
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
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] ServiceCreatorCallback callback, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, callback, promote);
  }

  /// <summary>Adds the specified service to the service container</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but
  /// delays the creation of the object until the service is requested</param>
  [Pure]
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
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance, bool promote)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance, promote);
  }

  /// <summary>Adds the specified service to the service container</summary>
  /// <param name="serviceType">The type of service to add</param>
  /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by
  /// the serviceType parameter</param>
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService([NotNull] System.Type serviceType, [NotNull] object serviceInstance)
  {
    this._controlServiceContainer.AddService(serviceType, serviceInstance);
  }

  /// <summary>Добавить сервис в контейнер сервисов</summary>
  /// <typeparam name="T">Тип помещаемого в контейнер сервиса</typeparam>
  /// <param name="service">Помещаемый в контейнер сервиса</param>
  /// <param name="promote">Добавлять ли сервис так же во все родительские контейнеры</param>
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void AddService<T>([NotNull] T service, bool promote = false)
  {
    this._controlServiceContainer.AddService(typeof (T), (object) service, promote);
  }

  /// <summary>Removes the specified service type from the service container, and optionally promotes the service to parent service
  /// containers</summary>
  /// <param name="serviceType">The type of service to remove</param>
  /// <param name="promote">true to promote this request to any parent service containers; otherwise, false</param>
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void RemoveService([NotNull] System.Type serviceType, bool promote)
  {
    this._controlServiceContainer.RemoveService(serviceType, promote);
  }

  /// <summary>Removes the specified service type from the service container</summary>
  /// <param name="serviceType">The type of service to remove</param>
  [Pure]
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
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public IServiceContainer RemoveService<T>(bool promote = false)
  {
    return this._controlServiceContainer.RemoveService<T>(promote);
  }

  /// <summary>Инициализация сервиса блокировки возможности сохранения</summary>
  protected void InitSaveLockService()
  {
    if (this.InDesignMode)
      return;
    this.SaveLocker = this.CreateSaveLocksCounter();
  }

  /// <summary>Счётчик блокировок возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public LocksManager SaveLocker { get; private set; }

  /// <summary>Виртуальный конструктор счётчика блокировок возможности сохранения результата</summary>
  [CanBeNull]
  protected virtual LocksManager CreateSaveLocksCounter()
  {
    return this.InDesignMode ? (LocksManager) null : new LocksManager((object) this, "SaveLock", (LockStatusChangedHandler) ((sender, lockerOwner, isSaveLocked) => this.LockSaveStatusChanged(isSaveLocked)), (IsExternalLockedHandler) ((sender, lockerOwner) => this.CanBeSaved()), this.GetChildSaveLocksCounters().NullOrSelect<ISupportSaveLocks, LocksManager>((Func<ISupportSaveLocks, LocksManager>) (saveLockManagerSupport => saveLockManagerSupport.SaveLocker)));
  }

  /// <summary>Виртуальный метод сбора всех дочерних счётчиков блокировок возможности сохранения результата</summary>
  [CanBeNull]
  protected virtual IEnumerable<ISupportSaveLocks> GetChildSaveLocksCounters()
  {
    return (IEnumerable<ISupportSaveLocks>) null;
  }

  /// <summary>Дополнительная проверка (дополнительная к счётчику блокировок, событию проверки и дочерним счётчикам блокировки) того,
  /// что данные UserControl-а могут быть сохранены</summary>
  protected virtual bool CanBeSaved() => true;

  /// <summary>Проверка статуса блокировки возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол).
  /// True если возможность сохранения в данный момент заблокирована (напр. кнопка Ok в диалоге, где используется контрол должна быть задизэйблена)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool SaveIsLocked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      LocksManager saveLocker = this.SaveLocker;
      return saveLocker != null && saveLocker.IsLocked;
    }
  }

  /// <summary>Значение счётчика блокировки возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int LocksSaveCount
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      LocksManager saveLocker = this.SaveLocker;
      return saveLocker == null ? 0 : saveLocker.LocksCount;
    }
  }

  /// <summary>Вызывается при изменении статуса блокировки возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  protected virtual void LockSaveStatusChanged(bool isSaveLocked) => this.UpdateCommands();

  /// <summary>Событие, инициируемое в случае изменения статуса блокировки возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  public event LockStatusChangedHandler OnLockSaveStatusChanged
  {
    [DebuggerStepThrough] add
    {
      if (this.SaveLocker == null)
        return;
      this.SaveLocker.OnLockStatusChanged += value;
    }
    [DebuggerStepThrough] remove
    {
      if (this.SaveLocker == null)
        return;
      this.SaveLocker.OnLockStatusChanged -= value;
    }
  }

  /// <summary>Заблокировать возможность сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  public void LockSave([CanBeNull] string contextName = null)
  {
    if (this.InDesignMode || this.SaveLocker == null)
      return;
    this.SaveLocker.Lock(this.ConfigName);
  }

  /// <summary>Разблокировать возможность сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  public void UnlockSave([CanBeNull] string contextName = null)
  {
    if (this.InDesignMode || this.SaveLocker == null)
      return;
    this.SaveLocker.Unlock(this.ConfigName);
  }

  /// <summary>Проверить, не изменился ли статус блокировки возможности сохранения и если изменился - проинформировать всех подписчиков</summary>
  public void CheckSaveLockedStatusChanged()
  {
    if (this.InDesignMode || this.SaveLocker == null)
      return;
    this.SaveLocker.CheckStatusChanged();
  }

  /// <summary>Массив наименований текущих операций блокировок возможности возможности сохранения результата (напр. для проверки статуса кнопки Ok в диалогах, в которых используется данный контрол)</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string[] LockSaveOperations
  {
    [DebuggerStepThrough] get => this.SaveLocker?.LockOperations;
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public virtual void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public virtual void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
  }

  /// <summary>Чтение данных из FormStorage</summary>
  protected virtual bool LoadPropertiesFromStorage() => true;

  /// <summary>Сохранение данных в FormStorage</summary>
  protected virtual void SavePropertiesToStorage()
  {
  }

  /// <summary>Имя конфигурации в которую сериализуются свойства контрола</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected string ConfigName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetControlContextName('_', true, true).LimitLength_DeleteRedundantAtStart(32 /*0x20*/);
    }
  }

  /// <summary>Если true, то настройки Layout и прочие сериализуемые в SavePropertiesToStorage/LoadPropertiesFromStorage сохраняются/восстанавливаются
  /// глобально, вне зависимости от контекста. То есть вне зависимости где используется контрол - настройки будут у них все одинаковые</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_313")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_302")]
  public bool IsPropertiesGlobal { get; set; }

  /// <summary>Имя операции в рамках которой используется UserControl. Служит для идентификации настроек контрола</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_312")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_303")]
  public string ContextName { get; set; }

  /// <summary>Имя операции по-умолчанию</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual string DefaultContextName => (string) null;

  /// <summary>Determine if we should serialize operation name</summary>
  /// <returns>true if it succeeds, false if it fails</returns>
  public bool ShouldSerializeContextName() => this.ContextName != this.DefaultContextName;

  public void ResetContextName() => this.ContextName = this.DefaultContextName;

  /// <summary>Имя контекста в который вложен данный</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INamedContext OwnerNamedContext
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._controlServiceContainer.GetService(typeof (INamedContext), false) as INamedContext;
    }
  }

  /// <summary>
  /// Полное имя контекста контрола, включая имена контекстов, в которые он входит, разделённые разделителя.
  /// Например "Создание объекта/Форма создания объекта/Контрол выбора типа"
  /// </summary>
  /// <param name="delimiter">Разделитель в формируемом пути</param>
  [CanBeNull]
  public string GetFullContextName(char delimiter = '/')
  {
    return this.GetControlContextName(delimiter, true, true);
  }

  /// <summary>Признак того, что содержимое контрола недоступно для редактирования</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_311")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_304")]
  public virtual bool IsReadOnly
  {
    get => this._forceIsReadOnly || this._isReadOnly || this.Disposing || this.IsDisposed;
    set
    {
      if (this._isReadOnly == value)
        return;
      if (this.InDesignMode)
      {
        this._isReadOnly = value;
        if (this._forceIsReadOnly)
          return;
        this.FireReadOnlyWasChanged();
      }
      else
      {
        if (this._forceIsReadOnly || !this.IsReadOnlyCanBeChanged(value))
          return;
        this._isReadOnly = value;
        this.FireReadOnlyWasChanged();
      }
    }
  }

  /// <summary>Принудительная блокировка редактирования. Включение этого флага исключает возможность разблокировать возможность редактирования перекрывая IsReadOnlyCanBeChanged в потомках и вызывая UpdateReadOnly</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_311")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_305")]
  public virtual bool ForceIsReadOnly
  {
    get => this._forceIsReadOnly;
    set
    {
      if (this._forceIsReadOnly == value)
        return;
      this._forceIsReadOnly = value;
      if (this._isReadOnly)
        return;
      this.FireReadOnlyWasChanged();
    }
  }

  /// <summary>Вызывается перед сменой значения свойства IsReadOnly. Если возвращает false значит менять значение IsReadOnly нельзя</summary>
  /// <param name="newReadOnlyValue">Новое значение</param>
  protected virtual bool IsReadOnlyCanBeChanged(bool newReadOnlyValue)
  {
    if (this._forceIsReadOnly || newReadOnlyValue && this.GetParentControls().OfType<ICanBeReadOnly>().Any<ICanBeReadOnly>((Func<ICanBeReadOnly, bool>) (parentCanBeReadOnly => parentCanBeReadOnly.IsReadOnly)))
      return false;
    CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler canBeChangedEvent = this.ReadOnlyCanBeChangedEvent;
    return canBeChangedEvent == null || canBeChangedEvent.GetInvocationList().Cast<CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler>().All<CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler>((Func<CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler, bool>) (isReadOnlyCanBeChangedEventHandler => isReadOnlyCanBeChangedEventHandler((object) this, newReadOnlyValue)));
  }

  /// <summary>Проверяет нет ли необходимости изменить статус ReadOnly и если да, то изменяет</summary>
  /// <returns>true если статус IsReadOnly был изменён</returns>
  public bool UpdateReadOnly()
  {
    if (this._forceIsReadOnly || !this.IsReadOnlyCanBeChanged(!this.IsReadOnly))
      return false;
    this._isReadOnly = !this.IsReadOnly;
    this.FireReadOnlyWasChanged();
    return true;
  }

  /// <summary>Вызывается после изменения статуса IsReadOnly, рассылает событие ReadOnlyWasChanged</summary>
  protected virtual void FireReadOnlyWasChanged()
  {
    Action<object> readOnlyWasChanged = this.ReadOnlyWasChanged;
    if (readOnlyWasChanged != null)
      readOnlyWasChanged((object) this);
    this.UpdateCommands();
  }

  /// <summary>Событие которое вызывается перед сменой значения свойства IsReadOnly. Если любой из обработчиков возвращает false, то изменение значения IsReadOnly будет заблокировано</summary>
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_311")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_310")]
  public event CanBeReadOnlyCommon.IsReadOnlyCanBeChangedEventHandler ReadOnlyCanBeChangedEvent;

  /// <summary>Событие вызывается после изменения статуса IsReadOnly</summary>
  [Intermech.Localization.CustomCategory("Attribute.Client.Core_311")]
  [Intermech.Localization.CustomDescription("Attribute.Client.Core_306")]
  public event Action<object> ReadOnlyWasChanged;

  /// <summary>Заблокировать обновление доступности команд</summary>
  /// <param name="forceLock">Если true, то блокируется и установка флаг необходимости обновления доступности команд после обнуления
  /// счётчика блокировок в том случае, если во время блокировки будет вызван UpdateCommands()</param>
  public void LockUpdateCommands(bool forceLock = false)
  {
    lock (this._syncObjLockUpdateCommands)
    {
      ++this._lockUpdateCommandsCounter;
      if (!forceLock && this._forceLockUpdateCommandsCounter <= 0)
        return;
      ++this._forceLockUpdateCommandsCounter;
    }
  }

  /// <summary>Разблокировать обновление доступности команд</summary>
  public void UnlockUpdateCommands()
  {
    lock (this._syncObjLockUpdateCommands)
    {
      if (--this._lockUpdateCommandsCounter == 0 && this._needUpdateAfterUnlock)
        this.UpdateCommands();
      if (this._forceLockUpdateCommandsCounter <= 0)
        return;
      --this._forceLockUpdateCommandsCounter;
    }
  }

  /// <summary>Флаг блокировки обновления команд</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UpdateCommandsIsLocked
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncObjLockUpdateCommands)
        return this._lockUpdateCommandsCounter > 0;
    }
    [DebuggerStepThrough] set
    {
      lock (this._syncObjLockUpdateCommands)
      {
        if (value)
          this.LockUpdateCommands();
        else
          this.UnlockUpdateCommands();
      }
    }
  }

  /// <summary>Обновить статус доступности команд</summary>
  /// <returns>true если обновление прошло успешно, если обновление команд заблокировано, то false</returns>
  protected virtual bool UpdateCommands()
  {
    lock (this._syncObjLockUpdateCommands)
    {
      Intermech.Diagnostics.Check.ObjectState(!this.Disposing && !this.IsDisposed, "Control is disposed or disposing, UpdateCommands can`t be called");
      if (this._lockUpdateCommandsCounter > 0 && this._forceLockUpdateCommandsCounter == 0)
        this._needUpdateAfterUnlock = true;
      else if (this._lockUpdateCommandsCounter == 0)
        this._needUpdateAfterUnlock = false;
      return this._lockUpdateCommandsCounter == 0;
    }
  }
}
