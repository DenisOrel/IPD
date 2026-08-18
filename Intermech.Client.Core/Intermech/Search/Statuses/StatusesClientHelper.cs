
// Type: Intermech.Search.Statuses.StatusesClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Statuses;

public static class StatusesClientHelper
{
  public static Status[] ConvertBytesToStatuses(object item)
  {
    List<Status> statusList = new List<Status>();
    IElementStatusesClientService statusesClientService = ServiceLocator.Get<IElementStatusesClientService>();
    byte[] bytes = new byte[0];
    switch (item)
    {
      case byte[] _:
        bytes = (byte[]) item;
        break;
      case _Object _:
        bytes = ((_Object) item).Statuses;
        break;
      case Relation _:
        bytes = ((Relation) item).Statuses;
        break;
      case CompositionPart _:
        bytes = ((RelationObjectBase) item).Object.Statuses;
        break;
    }
    if (statusesClientService.Plugins != null)
    {
      foreach (KeyValuePair<string, ElementStatusesPluginDescription> plugin in statusesClientService.Plugins)
      {
        if (statusesClientService.DisabledPlugins != null && !statusesClientService.DisabledPlugins.Contains(plugin.Key))
        {
          Guid result = Guid.Empty;
          if (Guid.TryParse(plugin.Key, out result))
          {
            foreach (int statuse in statusesClientService.GetStatuses(plugin.Key, bytes))
              statusesClientService.GetStatusIcon(result, statuse);
          }
        }
      }
    }
    return statusList.ToArray();
  }
}
