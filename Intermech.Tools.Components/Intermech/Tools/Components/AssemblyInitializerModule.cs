// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Components.AssemblyInitializerModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ApplicationModel;
using Intermech.Services.IMViewer;
using Intermech.Services.WeldingJoints;

#nullable disable
namespace Intermech.Tools.Components;

/// <summary>
/// Модуль инициализации, обеспечивающий запуск сервисов клиента IPS, реализованных в этой сборке.
/// </summary>
public sealed class AssemblyInitializerModule : LazyInitializerModuleGroup
{
  public AssemblyInitializerModule(IInitializerModuleFactory moduleFactory)
    : base(moduleFactory)
  {
    this.Add<WeldingJointsModule>();
    this.Add<IMViewerInitializerModule>();
  }
}
