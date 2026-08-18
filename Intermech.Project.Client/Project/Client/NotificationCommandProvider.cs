// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.NotificationCommandProvider
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Project.Client;

internal class NotificationCommandProvider : ICommandsProvider
{
  [NotNull]
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ViewProject", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.OpenProject), (object) false));
    mergedCommands.Add("EditProject", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.OpenProject), (object) true));
    return mergedCommands;
  }

  [NotNull]
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void OpenProject(
    [NotNull] ISelectedItems items,
    [NotNull] IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < items.Count; ++index)
      {
        long asInteger = sessionKeeper.Session.GetObject(items.GetItemData<IDBTypedObjectID>(index).ObjectID).AttributeByID(Intermech.Metadata.Attributes.Process.ID).AsInteger;
        if (asInteger != 0L)
          IMProject.OpenProject(asInteger, Convert.ToBoolean(additionalInfo));
      }
    }
  }
}
