
// Type: Intermech.ApplicationModel.InitializerModule
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;
using System.Reflection;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Позволяет реализовать инициализацию и завершение работы сервиса или подсистемы приложения в виде объекта.
    /// </summary>
    public class InitializerModule
    {
      private InitializerExceptionPolicy exceptionPolicy;
      private Action<Exception> exceptionHandler;
      private MethodInfo assemblyInitializer;
      private bool isInitialized;
      private InitializerModuleGroup group;

      /// <summary>Создает объект.</summary>
      public InitializerModule() => this.exceptionPolicy = InitializerExceptionPolicy.Normal;

      /// <summary>
      /// Возвращает или задает политику обработки исключений, возникающих при инициализации модуля.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Нельзя изменить значение свойства, так как модуль уже был инициализирован</exception>
      public InitializerExceptionPolicy ExceptionPolicy
      {
        [DebuggerStepThrough] get => this.exceptionPolicy;
        [DebuggerStepThrough] set
        {
          this.RequireNotInitializedForPropertyChange(nameof (ExceptionPolicy));
          this.exceptionPolicy = value;
        }
      }

      /// <summary>
      /// Возвращает или задает обработчик для исключений инициализации модуля. Обработчик вызывается всегда, независимо от заданной политики обработки исключений.
      /// Он может использоваться для вывода исключения в журнал приложения, либо для показа исключения пользователю.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Нельзя изменить значение свойства, так как модуль уже был инициализирован</exception>
      public Action<Exception> ExceptionHandler
      {
        [DebuggerStepThrough] get => this.exceptionHandler;
        [DebuggerStepThrough] set
        {
          this.RequireNotInitializedForPropertyChange(nameof (ExceptionHandler));
          this.exceptionHandler = value;
        }
      }

      /// <summary>
      /// Возвращает или задает имя открытого статического метода, который в процессе инициализации сборки инициализирует этот модуль.
      /// Имя этого метода используется в сообщении об ошибке в методе RequireInitialized(), если оказалось, что модуль не был не инициализирован.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Нельзя изменить значение свойства, так как модуль уже был инициализирован</exception>
      public MethodInfo AssemblyInitializer
      {
        [DebuggerStepThrough] get => this.assemblyInitializer;
        [DebuggerStepThrough] set
        {
          this.RequireNotInitializedForPropertyChange(nameof (AssemblyInitializer));
          this.assemblyInitializer = value;
        }
      }

      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем. Если в процессе выполнения этого метода будет сброшено исключение,
      /// то будет вызвано аварийное завершение работы модуля с помощью метода DoShutdown().
      /// </summary>
      public void Initialize()
      {
        if (this.isInitialized)
          return;
        try
        {
          this.DoInitialize();
          this.isInitialized = true;
        }
        catch (Exception ex)
        {
          this.DoShutdown();
          if (this.exceptionHandler != null)
            this.exceptionHandler(ex);
          if (this.exceptionPolicy != InitializerExceptionPolicy.Normal)
            return;
          throw;
        }
      }

      /// <summary>
      /// Возвращает признак, что модуля был успешно инициализирован.
      /// </summary>
      public bool IsInitialized
      {
        [DebuggerStepThrough] get => this.isInitialized;
      }

      /// <summary>
      /// Возвращает группу модулей, если этот модуль является частью группы.
      /// </summary>
      public InitializerModuleGroup Group
      {
        [DebuggerStepThrough] get => this.group;
        [DebuggerStepThrough] internal set => this.group = value;
      }

      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
      /// </summary>
      protected virtual void DoInitialize()
      {
      }

      /// <summary>
      /// Завершает работу объектов и сервисов, предоставленных модулем.
      /// </summary>
      public void Shutdown()
      {
        if (!this.isInitialized)
          return;
        this.DoShutdown();
        this.isInitialized = false;
      }

      /// <summary>
      /// Завершает работу объектов и сервисов, предоставленных модулем.
      /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
      /// </summary>
      protected virtual void DoShutdown()
      {
      }

      /// <summary>
      /// Позволяет убедиться, что инициализация модуля еще не была выполнена.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Инициализация модуля уже была выполнена</exception>
      protected void RequireNotInitialized()
      {
        if (this.isInitialized)
          throw new InvalidOperationException($"Неприменимо, так как модуль '{this.GetType()}' уже был инициализирован.");
      }

      private void RequireNotInitializedForPropertyChange(string propertyName)
      {
        if (this.isInitialized)
          throw new InvalidOperationException($"Нельзя изменить значение свойства '{propertyName}', так как модуль '{this.GetType()}' уже был инициализирован.");
      }

      /// <summary>
      /// Позволяет убедиться, что модуля был успешно инициализирован.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Инициализация модуля не была выполнена</exception>
      public void RequireInitialized()
      {
        if (!this.isInitialized)
          throw new InvalidOperationException(this.GetRequireInitializedMessage());
      }

      private string GetRequireInitializedMessage()
      {
        InitializerModuleGroup topmostGroup = this.TryGetTopmostGroup();
        return topmostGroup != null ? topmostGroup.GetModuleInitializerMessage() : this.GetModuleInitializerMessage();
      }

      private InitializerModuleGroup TryGetTopmostGroup()
      {
        InitializerModuleGroup group = this.Group;
        if (group != null)
        {
          while (group.Group != null)
            group = group.Group;
        }
        return group;
      }

      private string GetModuleInitializerMessage()
      {
        return !(this.assemblyInitializer != (MethodInfo) null) ? $"Модуль '{this.GetType()}' не был инициализирован должным образом." : $"Сборка не была инициализирована должным образом. Воспользуйтесь методом '{this.assemblyInitializer.DeclaringType.FullName}.{this.assemblyInitializer.Name}'.";
      }
    }
}
