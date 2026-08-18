
// Type: Intermech.Client.Core.Organizer.OrganizerChildCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class OrganizerChildCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    NodeIDPath parentPath = items.GetParentPath(0);
    if (parentPath == null)
      return new CommandsInfo();
    INodeID nodeId = parentPath[0];
    if (nodeId == null)
      return new CommandsInfo();
    if (!(ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service))
      return new CommandsInfo();
    CommandsInfo mergedCommands = new CommandsInfo();
    Dictionary<string, CommandInfo> requiredCommands = service.GetRequiredCommands(nodeId.CategoryID);
    if (requiredCommands != null)
    {
      foreach (KeyValuePair<string, CommandInfo> keyValuePair in requiredCommands)
        mergedCommands.Add(keyValuePair.Key, keyValuePair.Value);
    }
    List<string> superfluousCommands = service.GetSuperfluousCommands(nodeId.CategoryID);
    if (superfluousCommands != null)
    {
      foreach (string commandName in superfluousCommands)
        mergedCommands.Add(commandName, new CommandInfo(0));
    }
    return mergedCommands;
  }
}
