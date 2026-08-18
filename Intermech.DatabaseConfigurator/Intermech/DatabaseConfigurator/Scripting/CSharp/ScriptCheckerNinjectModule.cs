// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckerNinjectModule
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Ninject.Modules;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptCheckerNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Kernel.Bind<ScriptCheckerIDCache>().ToSelf().InSingletonScope();
    this.Kernel.Bind<ScriptCheckerService>().ToSelf().InSingletonScope();
    this.Kernel.Bind<WorkflowMessagesService>().ToSelf().InSingletonScope();
    this.Kernel.Bind<HtmlReportGenerator>().ToSelf();
    this.Kernel.Bind<HtmlReportWriter>().ToSelf();
    this.Kernel.Bind<CheckScriptStructureUIAction>().ToSelf();
    this.Kernel.Bind<NavigatorCommandProvider>().ToSelf();
    this.Kernel.Bind<AdminUtilsCommandProvider>().ToSelf();
    this.Kernel.Bind<ScriptCheckerInitializerModule>().ToSelf();
  }
}
