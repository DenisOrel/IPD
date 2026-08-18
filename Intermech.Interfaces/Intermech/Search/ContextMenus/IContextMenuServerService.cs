
// Type: Intermech.Search.ContextMenus.IContextMenuServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.ContextMenus
{
    public interface IContextMenuServerService
    {
      ContextMenu FindContextMenu(Guid userSessionGuid, long contextMenuVersionID);

      Dictionary<long, ContextMenu> FindContextMenus(Guid userSessionGuid, long[] contextMenuVersionIds);

      void SaveContextMenu(Guid userSessionGuid, long contextMenuVersionID, ContextMenu contextMenu);

      void AddContextMenusToObjectComposition(
        Guid userSessionGuid,
        long[] contextMenuVersionIds,
        long objectVersionID);

      void RemoveContextMenuFromObjectComposition(
        Guid userSessionGuid,
        long contextMenuVersionID,
        long objectVersionID);

      Dictionary<int, Tuple<long, ContextMenu>> GetContextMenuByObjectTypeDictionary(
        Guid userSessionGUID);
    }
}
