// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Scripting.CSharpScriptExecutor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Scripting;
using Intermech.Scripting.Common;
using Intermech.Scripting.Common.Debugging;
using Intermech.Scripting.Common.Hosting;
using Intermech.Scripting.CSharp;
using Intermech.Scripting.CSharp.Debugging;
using Intermech.Scripting.CSharp.Hosting;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel.Scripting;

[ServerService(ClientVisible = true)]
internal sealed class CSharpScriptExecutor : 
  LongLifeObject,
  ICSharpScriptExecutor,
  ICSharpDebugExecutor,
  IDebugExecutor
{
  private CSharpScriptExecutorOptionsProvider scriptExecutorOptionsProvider;
  private IDBTimedEvents dBTimedEvents;
  private ScriptExecutor<ICSharpScriptServerContext> internalExecutor;
  private ScriptInvocationLogger internalExecutorLogger;
  private CSharpScriptCodeAnalyzer internalScriptCodeAnalyzer;
  private CSharpDebugOperations internalDebugOperations;

  public CSharpScriptExecutor(
    ICSharpScriptServerContext scriptContext,
    CSharpScriptExecutorOptionsProvider scriptExecutorOptionsProvider,
    IApplicationStateEventsService applicationStateService,
    IServerEventLogService serverEventLog,
    IDBTimedEvents dBTimedEvents)
  {
    if (scriptContext == null)
      throw new ArgumentNullException(nameof (scriptContext));
    if (scriptExecutorOptionsProvider == null)
      throw new ArgumentNullException(nameof (scriptExecutorOptionsProvider));
    if (applicationStateService == null)
      throw new ArgumentNullException(nameof (applicationStateService));
    if (serverEventLog == null)
      throw new ArgumentNullException(nameof (serverEventLog));
    if (dBTimedEvents == null)
      throw new ArgumentNullException(nameof (dBTimedEvents));
    this.scriptExecutorOptionsProvider = scriptExecutorOptionsProvider;
    this.dBTimedEvents = dBTimedEvents;
    this.internalExecutor = new ScriptExecutor<ICSharpScriptServerContext>(scriptContext);
    this.internalExecutor.SearchPathListProvider = (SearchPathListProvider) new AppDomainSearchPathListProvider();
    this.internalExecutor.AutoReferencedAssemblies = (ICollection<string>) new string[5]
    {
      "System.Core.dll",
      "Intermech.Bcl.dll",
      "Intermech.Scripting.dll",
      "Intermech.Interfaces.dll",
      "Intermech.Interfaces.Server.dll"
    };
    this.internalExecutor.DependencyInjectionService = (ScriptDependencyInjectionService) new ApplicationServicesInjectionService((IServiceProvider) ApplicationServices.Container);
    this.internalExecutorLogger = new ScriptInvocationLogger((IScriptExecutorEvents) this.internalExecutor, serverEventLog);
    this.internalExecutorLogger.LogAll = this.scriptExecutorOptionsProvider.LogAllInvocations;
    this.internalExecutorLogger.Enabled = true;
    applicationStateService.Exit += new EventHandler(this.OnApplicationExit);
    this.internalScriptCodeAnalyzer = new CSharpScriptCodeAnalyzer();
    this.internalDebugOperations = new CSharpDebugOperations();
  }

  private void OnApplicationExit(object sender, EventArgs e)
  {
    this.internalExecutorLogger.Enabled = false;
    this.internalExecutor.Shutdown();
  }

  public CSharpScriptRuntimeInfo GetRuntimeInfo()
  {
    return new CSharpScriptRuntimeInfo()
    {
      AutoReferencesAssemblies = this.internalExecutor.AutoReferencedAssemblies,
      SearchPathList = this.internalExecutor.SearchPathListProvider.GetSearchPathList()
    };
  }

  public bool CanExecuteInSandbox(string scriptCode)
  {
    return this.internalScriptCodeAnalyzer.CanExecuteInSandbox(scriptCode);
  }

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

  Tuple<IUserSession, string> ICSharpDebugExecutor.CreateDebugSystemSession(int clientToken)
  {
    this.CheckClientTokenIsValid(clientToken);
    this.CheckUserIsAdmin();
    string sessionName = "DebugSession";
    return Tuple.Create<IUserSession, string>(this.dBTimedEvents.GetSystemSessionTemporaryClone(sessionName), sessionName);
  }

  ICollection<string> ICSharpDebugExecutor.GetAssembliesForAutocompletion(int clientToken)
  {
    this.CheckClientTokenIsValid(clientToken);
    this.CheckUserIsAdmin();
    return this.internalDebugOperations.GetAssembliesForAutocompletion();
  }

  public bool CanDebug(int clientToken)
  {
    return this.IsValidClientToken(clientToken) && this.IsAdminUser();
  }

  DebugExecuteResult IDebugExecutor.DebugExecute(
    int clientToken,
    string scriptCode,
    object options,
    params object[] arguments)
  {
    if (scriptCode == null)
      throw new ArgumentNullException(nameof (scriptCode));
    if (arguments == null)
      throw new ArgumentNullException(nameof (arguments));
    this.CheckClientTokenIsValid(clientToken);
    this.CheckUserIsAdmin();
    return this.internalDebugOperations.DebugExecute((ICSharpScriptExecutor) this, scriptCode, arguments);
  }

  private void CheckClientTokenIsValid(int clientToken)
  {
    if (!this.IsValidClientToken(clientToken))
      throw new ScriptExecutorException("Для отладки серверных C#-сценариев требуется, чтобы сервер приложений IPS был запущен на одном компьютере с клиентом IPS.");
  }

  private bool IsValidClientToken(int clientToken)
  {
    try
    {
      return string.Equals(Process.GetProcessById(clientToken).ProcessName, "IMClient", StringComparison.InvariantCultureIgnoreCase);
    }
    catch
    {
      return false;
    }
  }

  private void CheckUserIsAdmin()
  {
    if (!this.IsAdminUser())
      throw new ScriptExecutorException("Для отладки серверных C#-сценариев требуются права администратора IPS.");
  }

  private bool IsAdminUser()
  {
    return Thread.CurrentPrincipal is IPSPrincipal currentPrincipal && currentPrincipal.IsInRole(IPSBuiltInRole.Administrator);
  }
}
