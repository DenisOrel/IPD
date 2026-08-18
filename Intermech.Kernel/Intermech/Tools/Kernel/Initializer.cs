// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.Initializer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Tools.Kernel;

public static class Initializer
{
  private static InitializerModuleGroup modules;

  [MethodImpl(MethodImplOptions.Synchronized)]
  public static void InitializeAll()
  {
    Initializer.modules = new InitializerModuleGroup();
    Initializer.modules.AssemblyInitializer = typeof (Initializer).GetMethod(nameof (InitializeAll));
    Initializer.modules.ExceptionPolicy = InitializerExceptionPolicy.Suppress;
    Initializer.modules.ExceptionHandler = new Action<Exception>(Initializer.LogStartupExceptions);
    Initializer.modules.Add((InitializerModule) new ToolServicesModule());
    Initializer.modules.Initialize();
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public static void ShutdownAll()
  {
    if (Initializer.modules == null)
      return;
    Initializer.modules.Shutdown();
    Initializer.modules = (InitializerModuleGroup) null;
  }

  private static void LogStartupExceptions(Exception x)
  {
    ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, false)?.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1124"), (object) x.Message), Intermech.Consts.traceAlways, string.Empty);
  }
}
