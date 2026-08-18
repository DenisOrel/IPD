// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckerInitializerModule
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Workflow;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptCheckerInitializerModule : InitializerModule
{
  private ICurrentUserAndRole currentUserService;
  private IFactory navigatorFactory;
  private ScriptCheckerIDCache idCache;
  private NavigatorCommandProvider navigatorCommandProvider;
  private AdminUtilsCommandProvider adminUtilsCommandProvider;
  private IStartupService startupService;
  private WorkflowMessagesService workflowService;

  public ScriptCheckerInitializerModule(
    ICurrentUserAndRole currentUserService,
    IFactory navigatorFactory,
    ScriptCheckerIDCache idCache,
    NavigatorCommandProvider navigatorCommandProvider,
    AdminUtilsCommandProvider adminUtilsCommandProvider,
    IStartupService startupService,
    WorkflowMessagesService workflowService)
  {
    if (currentUserService == null)
      throw new ArgumentNullException(nameof (currentUserService));
    if (navigatorFactory == null)
      throw new ArgumentNullException(nameof (navigatorFactory));
    if (idCache == null)
      throw new ArgumentNullException(nameof (idCache));
    if (navigatorCommandProvider == null)
      throw new ArgumentNullException(nameof (navigatorCommandProvider));
    if (adminUtilsCommandProvider == null)
      throw new ArgumentNullException(nameof (adminUtilsCommandProvider));
    if (startupService == null)
      throw new ArgumentNullException(nameof (startupService));
    if (workflowService == null)
      throw new ArgumentNullException(nameof (workflowService));
    this.currentUserService = currentUserService;
    this.navigatorFactory = navigatorFactory;
    this.idCache = idCache;
    this.navigatorCommandProvider = navigatorCommandProvider;
    this.adminUtilsCommandProvider = adminUtilsCommandProvider;
    this.startupService = startupService;
    this.workflowService = workflowService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (!this.currentUserService.IsAdmin)
      return;
    this.InitializeAdminCommands();
    this.startupService.StartupComplete += (EventHandler) ((s, e) => this.SendSystemMessageAboutScriptConversion());
  }

  private void InitializeAdminCommands()
  {
    this.navigatorCommandProvider.AddCommandsToMenuTemplate(this.navigatorFactory.ContextMenuTemplate);
    this.navigatorFactory.AddCommandsProvider(1, this.idCache.ScriptsBaseType.Id, (ICommandsProvider) this.navigatorCommandProvider);
    this.adminUtilsCommandProvider.Initialize();
  }

  private void SendSystemMessageAboutScriptConversion()
  {
    string subject = "Важные изменения в работе сценариев C#";
    if (this.workflowService.FindMessageBySubject(subject) != 0L)
      return;
    string text = ScriptCheckerConsts.BreakingChangesWarning + Environment.NewLine + Environment.NewLine + ScriptCheckerConsts.ConversionWarning;
    this.workflowService.SendSystemMessage(subject, text, ProcessPriority.High);
  }

  protected override void DoShutdown()
  {
    if (this.currentUserService.IsAdmin)
      this.ShutdownAdminCommands();
    base.DoShutdown();
  }

  private void ShutdownAdminCommands()
  {
    this.adminUtilsCommandProvider.Shutdown();
    this.navigatorFactory.RemoveCommandsProvider(1, this.idCache.ScriptsBaseType.Id, (ICommandsProvider) this.navigatorCommandProvider);
    this.navigatorCommandProvider.RemoveCommandsFromMenuTemplate(this.navigatorFactory.ContextMenuTemplate);
  }
}
