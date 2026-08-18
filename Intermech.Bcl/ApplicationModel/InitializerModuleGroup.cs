
// Type: Intermech.ApplicationModel.InitializerModuleGroup
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech.ApplicationModel
{
    /// <summary>
    /// Позволяет реализовать группу модулей, инициализация которых выполняется в определенном порядке.
    /// </summary>
    public class InitializerModuleGroup : InitializerModule
    {
      private ICollection<InitializerModule> modules;
      private List<InitializerModule> initializedModules;

      /// <summary>Создает объект.</summary>
      public InitializerModuleGroup()
      {
        this.modules = (ICollection<InitializerModule>) new LinkedList<InitializerModule>();
      }

      /// <summary>
      /// Добавляет модуль в группу в конец последовательности инициализации модулей.
      /// </summary>
      /// <param name="module">Объект модуля</param>
      /// <exception cref="T:System.ArgumentNullException">module</exception>
      /// <exception cref="T:System.InvalidOperationException">Группа уже была инициализирована; добавляемый модуль уже был инициализирован; добавляемый модуль уже входит в другую группу</exception>
      public void Add(InitializerModule module)
      {
        if (module == null)
          throw new ArgumentNullException(nameof (module));
        this.RequireNotInitialized();
        this.AddInternal(module);
      }

      private void AddInternal(InitializerModule module)
      {
        if (module.Group != null)
        {
          if (module.Group != this)
            throw new InvalidOperationException("Модуль уже включен в группу.");
        }
        else
        {
          if (module.IsInitialized)
            throw new InvalidOperationException("Модуль уже инициализирован.");
          this.modules.Add(module);
          module.Group = this;
        }
      }

      /// <summary>
      /// Добавляет указанную коллекцию модулей инициализации в группу в конец последовательности инициализации модулей.
      /// </summary>
      /// <param name="modules">Коллекция модулей инициализации</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="modules" /> не должен быть равен null</exception>
      public void AddRange(IEnumerable<InitializerModule> modules)
      {
        if (modules == null)
          throw new ArgumentNullException(nameof (modules));
        this.RequireNotInitialized();
        foreach (InitializerModule module in modules)
        {
          if (module != null)
            this.Add(module);
        }
      }

      /// <summary>
      /// Выполняет инициализацию модулей в порядке их добавления в группу.
      /// </summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        this.initializedModules = new List<InitializerModule>(this.modules.Count);
        foreach (InitializerModule module in (IEnumerable<InitializerModule>) this.modules)
        {
          module.Initialize();
          this.initializedModules.Add(module);
        }
      }

      /// <summary>
      /// Завершает работу модулей в порядке, обратном порядку их добавления в группу.
      /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
      /// </summary>
      protected override void DoShutdown()
      {
        base.DoShutdown();
        if (this.initializedModules.Count == 0)
          return;
        this.initializedModules.Reverse();
        foreach (InitializerModule initializedModule in this.initializedModules)
          initializedModule.Shutdown();
      }
    }
}
