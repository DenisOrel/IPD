
// Type: Intermech.Search.ContextMenus.ContextMenuClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuClientService : IContextMenuClientService
{
  private Dictionary<int, Tuple<long, ContextMenu>> _contextMenuByObjectTypeDictionary;

  public ContextMenuClientService()
  {
    ServiceLocator.Get<INotificationService>().Subscribe(new NotificationEventHandler(this.NotificationService_EventFired));
  }

  public ContextMenu FindContextMenu(long contextMenuVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID))
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IContextMenuServerService) sessionKeeper.Session.GetCustomService(typeof (IContextMenuServerService))).FindContextMenu(sessionKeeper.Session.SessionGUID, contextMenuVersionID);
  }

  public ContextMenu FindContextMenuForObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    if (this._contextMenuByObjectTypeDictionary == null)
      this.ReloadContextMenuByObjectTypeDictionary();
    Tuple<long, ContextMenu> tuple = (Tuple<long, ContextMenu>) null;
    this._contextMenuByObjectTypeDictionary.TryGetValue(objectTypeID, out tuple);
    return tuple?.Item2;
  }

  public ContextMenu FindContextMenuForObjectTypes(int[] objectTypeIds)
  {
    if (objectTypeIds == null || objectTypeIds.Length == 0 || ObjectTypeHelper.IsAnyUnknownObjectTypeID((IEnumerable<int>) objectTypeIds))
      throw new ArgumentException();
    IGrouping<ContextMenu, ContextMenu>[] array = ((IEnumerable<int>) objectTypeIds).Select<int, ContextMenu>((Func<int, ContextMenu>) (o => this.FindContextMenuForObjectType(o))).Where<ContextMenu>((Func<ContextMenu, bool>) (o => o != null)).GroupBy<ContextMenu, ContextMenu>((Func<ContextMenu, ContextMenu>) (o => o)).ToArray<IGrouping<ContextMenu, ContextMenu>>();
    return array.Length != 1 ? (ContextMenu) null : array[0].Key;
  }

  public void SaveContextMenu(long contextMenuVersionID, ContextMenu contextMenu)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID))
      throw new ArgumentException();
    if (contextMenu == null)
      throw new ArgumentNullException(nameof (contextMenu));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((IContextMenuServerService) sessionKeeper.Session.GetCustomService(typeof (IContextMenuServerService))).SaveContextMenu(sessionKeeper.Session.SessionGUID, contextMenuVersionID, contextMenu);
    }
  }

  public long[] AddContextMenusToObjectComposition(long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    object[] source = SelectionWindow.Select("Выберите контекстные меню.", (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(ContextMenuConstants.ContextMenuObjectTypeID), typeof (IDBTypedObjectID), SelectionOptions.Default);
    if (source == null || source.Length == 0)
      return new long[0];
    long[] array = source.Cast<IDBTypedObjectID>().Select<IDBTypedObjectID, long>((Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((IContextMenuServerService) sessionKeeper.Session.GetCustomService(typeof (IContextMenuServerService))).AddContextMenusToObjectComposition(sessionKeeper.Session.SessionGUID, array, objectVersionID);
    }
    return array;
  }

  public void RemoveContextMenuFromObjectComposition(
    long contextMenuVersionID,
    long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(contextMenuVersionID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (NotificationContext.Create(sessionKeeper.Session))
        ((IContextMenuServerService) sessionKeeper.Session.GetCustomService(typeof (IContextMenuServerService))).RemoveContextMenuFromObjectComposition(sessionKeeper.Session.SessionGUID, contextMenuVersionID, objectVersionID);
    }
  }

  public void ReloadCache() => this.ReloadContextMenuByObjectTypeDictionary();

  private void NotificationService_EventFired(object sender, NotificationEventArgs e)
  {
    if ((e.EventName == "RelationsCreated" || e.EventName == "RelationsRemoved") && e is DBRelationsEventArgs && ((DBRelationsEventArgs) e).KnownRelationTypes != null && ((DBRelationsEventArgs) e).KnownRelationTypes.Contains(ContextMenuConstants.ContextMenusRelationTypeID))
    {
      this.ReloadContextMenuByObjectTypeDictionary();
    }
    else
    {
      if (!(e.EventName == "ObjectsChanged") || !(e is DBObjectsEventArgs) || !this.NeedReloadContextMenuByObjectTypeDictionary((DBObjectsEventArgs) e))
        return;
      this.ReloadContextMenuByObjectTypeDictionary();
    }
  }

  private bool NeedReloadContextMenuByObjectTypeDictionary(DBObjectsEventArgs dbObjectsEventArgs)
  {
    if (dbObjectsEventArgs.ObjectTypeIDs != null && dbObjectsEventArgs.ObjectTypeIDs.Contains(ContextMenuConstants.ContextMenuObjectTypeID))
      return true;
    return dbObjectsEventArgs.ObjectIDs != null && ((IEnumerable<long>) this.GetContextMenuVersionIdsFromContextMenuByObjectTypeDictionary()).Any<long>((Func<long, bool>) (o => dbObjectsEventArgs.ObjectIDs.Contains(o)));
  }

  private long[] GetContextMenuVersionIdsFromContextMenuByObjectTypeDictionary()
  {
    return this._contextMenuByObjectTypeDictionary != null ? this._contextMenuByObjectTypeDictionary.Values.Select<Tuple<long, ContextMenu>, long>((Func<Tuple<long, ContextMenu>, long>) (o => o.Item1)).Distinct<long>().ToArray<long>() : new long[0];
  }

  private void ReloadContextMenuByObjectTypeDictionary()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._contextMenuByObjectTypeDictionary = ((IContextMenuServerService) sessionKeeper.Session.GetCustomService(typeof (IContextMenuServerService))).GetContextMenuByObjectTypeDictionary(sessionKeeper.Session.SessionGUID);
  }
}
