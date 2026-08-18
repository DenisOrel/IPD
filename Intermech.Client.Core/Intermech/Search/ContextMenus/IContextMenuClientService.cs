
// Type: Intermech.Search.ContextMenus.IContextMenuClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Search.ContextMenus;

public interface IContextMenuClientService
{
  ContextMenu FindContextMenu(long contextMenuVersionID);

  ContextMenu FindContextMenuForObjectType(int objectTypeID);

  ContextMenu FindContextMenuForObjectTypes(int[] objectTypeIds);

  void SaveContextMenu(long contextMenuVersionID, ContextMenu contextMenu);

  long[] AddContextMenusToObjectComposition(long objectVersionID);

  void RemoveContextMenuFromObjectComposition(long сontextMenuVersionID, long objectVersionID);

  void ReloadCache();
}
