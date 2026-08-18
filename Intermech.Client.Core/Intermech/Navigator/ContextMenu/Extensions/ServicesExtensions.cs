
// Type: Intermech.Navigator.ContextMenu.Extensions.ServicesExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Extensions;
using System;
using System.ComponentModel.Design;


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>Класс для расширения над контейнером серсисов, упрощающего регистрацию провайдера локальных команд не удаляя приэтом команды вышестоящего
/// контекста</summary>
/// <summary>Класс для расширения над контейнером серсисов, упрощающего регистрацию провайдера локальных команд не удаляя приэтом команды вышестоящего
/// контекста</summary>
public static class ServicesExtensions
{
  /// <summary>Класс для расширение над контейнером серсисов, упрощающего регистрацию сервиса фильтрации локальных команд сохраняя при этом фильтры
  /// вышестоящего контекста (например формы и userControl-ы могут быть вложены друг в друга, каждый добавляет свои фильтры команд и
  /// внутри каждого работают как его фильтры команд, так и фильтры контролов, в которые он вложен)</summary>
  public static void StackLocalContextCommandsFilter(
    this IServiceContainer localContext,
    ICommandsFilter localCommandsFilter)
  {
    localContext.AddService(typeof (ICommandFiltersStack), (object) new CommandFiltersStack(localContext, localCommandsFilter));
  }

  /// <summary>Засуспендить в контексте провайдера команд
  /// Может потребоваться например для того, чтобы во вложенном в диалог (который является провайдером комманд) пользовательском контроле,
  /// запретить работу фильтра комманд, создаваемого диалогом</summary>
  public static void SuspendLocalContextCommandsFilter(
    this IServiceContainer localContext,
    ICommandsFilter localCommandsFilter)
  {
    ContextServicesStackFuncs.SuspendLocalService<ICommandsFilter>(localContext, (Func<ContextServicesStack<ICommandsFilter>>) (() => (ContextServicesStack<ICommandsFilter>) new CommandFiltersStack(localContext, (ICommandsFilter) null)), localCommandsFilter);
  }

  /// <summary>Расширение над контейнером серсисов, упрощающего регистрацию провайдера локальных команд сохраняя при этом команды вышестоящего
  /// контекста (например формы и userControl-ы могут быть вложены друг в друга, каждый добавляет свои команды и внутри каждого доступны как
  /// его команды, так и команды контролов, в которые он вложен)</summary>
  public static void StackLocalContextCommandsProvider(
    this IServiceContainer localContext,
    ILocalCommandsProvider localCommandsProvider)
  {
    ContextServicesStackFuncs.AddLocalService<ILocalCommandsProvider>(localContext, localCommandsProvider);
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      localCommandsProvider.InitCommandTemplates(contextMenuTemplate);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Расширение над контейнером серсисов, упрощающего dispose заготовок локальных команд</summary>
  public static void DisposeLocalContextCommandsTemplates(
    this IServiceContainer localContext,
    ILocalCommandsProvider localCommandsProvider)
  {
    ContextServicesStackFuncs.RemoveLocalService<ILocalCommandsProvider>(localContext, localCommandsProvider);
    MenuTemplate contextMenuTemplate = Holder.Factory.ContextMenuTemplate;
    contextMenuTemplate.BeginUpdate();
    try
    {
      localCommandsProvider.DisposeCommandTemplates(contextMenuTemplate);
    }
    finally
    {
      contextMenuTemplate.EndUpdate();
    }
  }

  /// <summary>Засуспендить в контексте провайдера команд
  /// Может потребоваться например для того, чтобы во вложенном в диалог (который является провайдером комманд) пользовательском контроле,
  /// запретить отображаться дополнительным командам, создаваемым диалогом</summary>
  public static void SuspendLocalContextCommandsProvider(
    this IServiceContainer localContext,
    ILocalCommandsProvider localCommandsProvider)
  {
    ContextServicesStackFuncs.SuspendLocalService<ILocalCommandsProvider>(localContext, (Func<ContextServicesStack<ILocalCommandsProvider>>) (() => new ContextServicesStack<ILocalCommandsProvider>(localContext, (ILocalCommandsProvider) null)), localCommandsProvider);
  }
}
