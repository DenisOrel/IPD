// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.ClientRxmlService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Redline;

internal sealed class ClientRxmlService : IClientRxmlService
{
  private readonly IServiceContainer emptyViewServices;

  public ClientRxmlService()
  {
    this.emptyViewServices = (IServiceContainer) new ServiceContainer();
    this.emptyViewServices.AddService(typeof (IViewState), (object) new ViewStateService());
  }

  public bool TryOpenRxmlViewer(long documentId)
  {
    CommandsTable commandsTable = !Consts.IsUndefinedObjectId(documentId) ? Intermech.Navigator.ContextMenu.Services.GetCommandsTable(Intermech.Navigator.ContextMenu.Services.GetItems(documentId), (IServiceProvider) this.emptyViewServices) : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
    if (!commandsTable.Contains(RxmlCommandsProvider.ViewRxmlCommandName))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(RxmlCommandsProvider.ViewRxmlCommandName, commandsTable, (IServiceProvider) this.emptyViewServices);
    return true;
  }
}
