// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Plugin.ConfigEditorCommandProvider
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.XmlExchange.ConfigEditor.Editor;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Plugin;

internal class ConfigEditorCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items != null && items.Count == 1 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && MetaDataHelper.IsObjectTypeChildOf(itemData.ObjectType, MetaDataHelper.GetObjectTypeID("cadd9457-306c-11d8-b4e9-00304f19f545")))
    {
      mergedCommands.Add("EditDocument", new CommandInfo(3, new ClickEventHandler(this.EditExportConfig)));
      mergedCommands.Add("OpenDocument", new CommandInfo(3, new ClickEventHandler(this.ViewExportConfig)));
      mergedCommands.Add("ViewDocument", new CommandInfo(3, new ClickEventHandler(this.ViewExportConfig)));
    }
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  internal void EditExportConfig(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    ConfigEditorWindow.ShowEditorWindow(items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID, false);
  }

  internal void ViewExportConfig(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    ConfigEditorWindow.ShowEditorWindow(items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID, true);
  }
}
