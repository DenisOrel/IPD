// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.AssemblyInitializerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ApplicationModel;
using Intermech.Kernel.Services.StandaloneView;


namespace Intermech.Kernel;

public sealed class AssemblyInitializerModule : LazyInitializerModuleGroup
{
  public AssemblyInitializerModule(IInitializerModuleFactory moduleFactory)
    : base(moduleFactory)
  {
    this.Add<Module>();
  }
}
