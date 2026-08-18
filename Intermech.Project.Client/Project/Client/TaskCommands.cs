// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.TaskCommands
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Metadata;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

public class TaskCommands : ICommandsProvider
{
  public TaskCommands([CanBeNull] ProjectClientPlugin plugin)
  {
  }

  [NotNull]
  public CommandsInfo GetMergedCommands([NotNull] ISelectedItems items, [CanBeNull] System.IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(index);
      if (!flag2 && itemData.ObjectType == (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task)
        flag2 = true;
      if (!flag1 && itemData.ObjectType == (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Dependency)
        flag1 = true;
    }
    if (!flag1)
      mergedCommands.Add("ViewDocument", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.ViewCommand)));
    mergedCommands.Add("View", new Intermech.Navigator.ContextMenu.CommandInfo(0, new ClickEventHandler(this.ViewCommand)));
    if (flag2 | flag1 && !ControlFuncs.IsKeyPressed(Keys.ControlKey) && !ControlFuncs.IsKeyPressed(Keys.ShiftKey))
    {
      mergedCommands.Add("CheckOut", new Intermech.Navigator.ContextMenu.CommandInfo(0));
      mergedCommands.Add("CheckIn", new Intermech.Navigator.ContextMenu.CommandInfo(0));
      mergedCommands.Add("SaveChanges", new Intermech.Navigator.ContextMenu.CommandInfo(0));
      mergedCommands.Add("CancelChanges", new Intermech.Navigator.ContextMenu.CommandInfo(0));
      mergedCommands.Add("Delete", new Intermech.Navigator.ContextMenu.CommandInfo(0));
    }
    mergedCommands.Add("Add", new Intermech.Navigator.ContextMenu.CommandInfo(0));
    mergedCommands.Add("Exclude", new Intermech.Navigator.ContextMenu.CommandInfo(0));
    mergedCommands.Add("CreateInclude2", new Intermech.Navigator.ContextMenu.CommandInfo(1000000));
    mergedCommands.Add("BasedOnTemplate", new Intermech.Navigator.ContextMenu.CommandInfo(0));
    mergedCommands.Suppress("OpenDocument", 0);
    mergedCommands.Suppress("OpenWith", 0);
    mergedCommands.Suppress("ViewWithOptions", 0);
    mergedCommands.Suppress("PrintDocument", 0);
    mergedCommands.Suppress("Edit", 0);
    mergedCommands.Suppress("EditDocument", 0);
    return mergedCommands;
  }

  [NotNull]
  public CommandsInfo GetGroupCommands([CanBeNull] ISelectedItems items, [CanBeNull] System.IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  public void ViewCommand(
    [NotNull] ISelectedItems items,
    [CanBeNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      Task task = StandaloneTask.Get(ClientSessionProvider2.Provider, items.GetItemData<IDBTypedObjectID>(index).ObjectID);
      if (task != null)
      {
        using (EditTaskForm editTaskForm = new EditTaskForm())
          editTaskForm.EditTask(task, true);
      }
    }
  }
}
