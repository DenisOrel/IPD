
// Type: Intermech.Client.Scripting.CSharpScriptExecutor
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Scripting.Common;
using Intermech.Scripting.Common.Hosting;
using Intermech.Scripting.CSharp;
using Intermech.Scripting.CSharp.Hosting;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Scripting
{
    /// <summary>
    /// Объект сервиса, позволяющего выполнять C#-сценарии в изолированном окружении.
    /// Реализация является thread safe.
    /// </summary>
    internal sealed class CSharpScriptExecutor : LongLifeObject, ICSharpScriptExecutor
    {
      private CSharpScriptExecutorOptionsProvider scriptExecutorOptionsProvider;
      private ScriptExecutor<ICSharpScriptClientContext> internalExecutor;
      private ScriptInvocationLogger internalExecutorLogger;
      private CSharpScriptCodeAnalyzer internalScriptCodeAnalyzer;

      public CSharpScriptExecutor(
        ICSharpScriptClientContext scriptContext,
        CSharpScriptExecutorOptionsProvider scriptExecutorOptionsProvider,
        IApplicationStateEventsService applicationStateService,
        IServerEventLogService serverEventLog)
      {
        if (scriptContext == null)
          throw new ArgumentNullException(nameof (scriptContext));
        if (scriptExecutorOptionsProvider == null)
          throw new ArgumentNullException(nameof (scriptExecutorOptionsProvider));
        if (applicationStateService == null)
          throw new ArgumentNullException(nameof (applicationStateService));
        if (serverEventLog == null)
          throw new ArgumentNullException(nameof (serverEventLog));
        this.scriptExecutorOptionsProvider = scriptExecutorOptionsProvider;
        this.internalExecutor = new ScriptExecutor<ICSharpScriptClientContext>(scriptContext);
        this.internalExecutor.SearchPathListProvider = (SearchPathListProvider) new AppDomainSearchPathListProvider();
        this.internalExecutor.AutoReferencedAssemblies = (ICollection<string>) new string[5]
        {
          "System.Core.dll",
          "Intermech.Bcl.dll",
          "Intermech.Scripting.dll",
          "Intermech.Interfaces.dll",
          "Intermech.Interfaces.Client.dll"
        };
        this.internalExecutor.DependencyInjectionService = (ScriptDependencyInjectionService) new ApplicationServicesInjectionService((IServiceProvider) ApplicationServices.Container);
        this.internalExecutorLogger = new ScriptInvocationLogger((IScriptExecutorEvents) this.internalExecutor, serverEventLog);
        this.internalExecutorLogger.LogAll = this.scriptExecutorOptionsProvider.LogAllInvocations;
        this.internalExecutorLogger.Enabled = true;
        applicationStateService.Exit += new EventHandler(this.OnApplicationExit);
        this.internalScriptCodeAnalyzer = new CSharpScriptCodeAnalyzer();
      }

      private void OnApplicationExit(object sender, EventArgs e)
      {
        this.internalExecutorLogger.Enabled = false;
        this.internalExecutor.Shutdown();
      }

      /// <summary>Возвращает инфомарцию о среде выполнения сценариев.</summary>
      /// <returns>Информация о среде выполнения сценариев</returns>
      public CSharpScriptRuntimeInfo GetRuntimeInfo()
      {
        return new CSharpScriptRuntimeInfo()
        {
          AutoReferencesAssemblies = this.internalExecutor.AutoReferencedAssemblies,
          SearchPathList = this.internalExecutor.SearchPathListProvider.GetSearchPathList()
        };
      }

      /// <summary>
      /// Проверяет, может ли сценарий быть выполнен в изолированном окружении.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <returns>true - код сценария содержит свойство ScriptContext и может быть выполнен в изолированном окружении</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null</exception>
      public bool CanExecuteInSandbox(string scriptCode)
      {
        return this.internalScriptCodeAnalyzer.CanExecuteInSandbox(scriptCode);
      }

      /// <summary>
      /// Выполняет код сценария. Код должен содержать класс Script с экземплярным свойством
      /// ScriptContext типа ICSharpScriptContext и экземплярный метод Execute,
      /// параметры которого должны соответствовать аргументам вызова сценария.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="options">Опции выполнения сценария</param>
      /// <param name="arguments">Аргументы вызова сценария</param>
      /// <returns>Результат выполнения сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="options" /> не должен быть равен null; параметр <paramref name="arguments" /> не должен быть равен null</exception>
      /// <exception cref="T:System.Exception">Код сценария не содержит необходимых элементов, либо произошла ошибка при выполнении сценария</exception>
      public object Execute(
        string scriptCode,
        CSharpScriptInvocationOptions options,
        params object[] arguments)
      {
        if (scriptCode == null)
          throw new ArgumentNullException(nameof (scriptCode));
        if (options == null)
          throw new ArgumentNullException(nameof (options));
        if (arguments == null)
          throw new ArgumentNullException(nameof (arguments));
        return this.internalExecutor.Execute(scriptCode, (IScriptInvocationOptions) options, arguments);
      }

      /// <summary>
      /// Создает и возвращает объект сценария, завернутый в объект-хранитель.
      /// Метод применяется в тех случаях, когда обращение к сценарию C# не может быть
      /// сведено к единственному вызову метода Execute. Код сценария должен содержать
      /// класс Script с экземплярным свойством ScriptContext типа ICSharpScriptContext.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="options">Опции выполнения сценария</param>
      /// <returns>Объект-хранитель, содержащий объект сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="options" /> не должен быть равен null</exception>
      /// <exception cref="T:System.Exception">Код сценария не содержит необходимых элементов, либо произошла ошибка при компиляции сценария</exception>
      public CSharpScriptObjectKeeper CreateScriptObject(
        string scriptCode,
        CSharpScriptInvocationOptions options)
      {
        if (scriptCode == null)
          throw new ArgumentNullException(nameof (scriptCode));
        if (options == null)
          throw new ArgumentNullException(nameof (options));
        return new CSharpScriptObjectKeeper(this.internalExecutor.CreateScriptObject(scriptCode, (IScriptInvocationOptions) options));
      }
    }
}
