
// Type: Intermech.Client.Core.AssemblyLateInitializerModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Client.Core.CompositionCopying;


namespace Intermech.Client.Core;

/// <summary>
/// Модуль инициализации, обеспечивающий поздний запуск сервисов клиента IPS,
/// зависящих от серисов навигатора.
/// </summary>
public sealed class AssemblyLateInitializerModule : LazyInitializerModuleGroup
{
  public AssemblyLateInitializerModule(IInitializerModuleFactory moduleFactory)
    : base(moduleFactory)
  {
    this.Add<CompositionCopyingInitializerModule>();
  }
}
