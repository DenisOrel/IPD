// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Scripting.CSharpScriptsNinjectModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Ninject.Modules;


namespace Intermech.Kernel.Scripting;

internal sealed class CSharpScriptsNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<ICSharpScriptContext, ICSharpScriptServerContext>().To<CSharpScriptServerContext>();
    this.Bind<CSharpScriptExecutorOptionsProvider>().ToSelf().WhenInjectedInto<CSharpScriptExecutor>();
    this.Bind<ICSharpScriptExecutor>().To<CSharpScriptExecutor>().InSingletonScope();
  }
}
