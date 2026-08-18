using Intermech.ComparisonPlugins.PDFComparison.Common;
using Intermech.DataFormats;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.ComparisonPlugins.PDFComparison
{
    internal class CommandProvider : ICommandsProvider
    {
      CommandsInfo ICommandsProvider.GetGroupCommands(
        ISelectedItems items,
        IServiceProvider viewServices)
      {
        if (items == null || items.Count > 2)
          return CommandsInfo.Empty;
        CommandsInfo groupCommands = new CommandsInfo();
        bool flag = true;
        for (int index = 0; index < items.Count; ++index)
        {
          if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !HelperConsts.ComparedObjectTypes.Contains(itemData.ObjectType))
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          groupCommands.Add("OpenForComparison", new CommandInfo(0, new ClickEventHandler(this.OpenForComparisonClick)));
          if (items.Count == 1 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && itemData.BaseVersion != 1L)
            groupCommands.Add("СompareWithBaseVersion", new CommandInfo(0, new ClickEventHandler(this.СompareWithBaseVersionClick)));
        }
        return groupCommands;
      }

      CommandsInfo ICommandsProvider.GetMergedCommands(
        ISelectedItems items,
        IServiceProvider viewServices)
      {
        return CommandsInfo.Empty;
      }

      private void OpenForComparisonClick(
        ISelectedItems items,
        IServiceProvider viewServices,
        object additionalInfo)
      {
        this.RunComparison(items, (ComparisonProvider) new OpenForComparisonProvider());
      }

      private void СompareWithBaseVersionClick(
        ISelectedItems items,
        IServiceProvider viewServices,
        object additionalInfo)
      {
        this.RunComparison(items, (ComparisonProvider) new СompareWithBaseVersionProvider());
      }

      private void RunComparison(ISelectedItems items, ComparisonProvider provider)
      {
        long[] objectIDs = new long[items.Count];
        for (int index = 0; index < items.Count; ++index)
        {
          if (items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
            objectIDs[index] = itemData.ObjectID;
        }
        provider.ShowСomparisonWindow((IEnumerable<long>) objectIDs);
      }
    }
}
