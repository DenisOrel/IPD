// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IAdditionalCompositionFiltrationService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Сервис, содержит команды дополнительной фильтрации состава от модуля PDM
/// </summary>
public interface IAdditionalCompositionFiltrationService
{
  /// <summary>
  /// Событие возникает при сборке тулбара, подписчикам вернуть свои команды
  /// </summary>
  event GetCompositionFiltrationCommandEventHandler GetCompositionFiltrationCommand;

  Guid CreateCommands(IFiltrationService filtration);

  Guid CreateCommands(
    IFiltrationService filtration,
    AdditionalFiltrationToolBarOptions options,
    Guid registerGuid);

  void OnToolBarClosed(Guid guid);

  IClientPluginsDataTransfer GetToolBar(Guid guid);
}
