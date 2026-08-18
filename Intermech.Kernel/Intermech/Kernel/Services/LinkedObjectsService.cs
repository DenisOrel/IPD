// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.LinkedObjectsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services.PortalServices;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Kernel.Services;

internal sealed class LinkedObjectsService : LongLifeObject, ILinkedObjectsService
{
  private bool _sorted;
  private List<ILinkedObjectsHandler> _handlers = new List<ILinkedObjectsHandler>();

  public void RegisterHandler(ILinkedObjectsHandler handler)
  {
    this._sorted = false;
    this._handlers.Add(handler);
  }

  public Dictionary<string, List<LinkedObject>> GetLinkedObjectsEx(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID)
  {
    if (this._handlers.Count == 0)
      return (Dictionary<string, List<LinkedObject>>) null;
    List<ILinkedObjectsHandler> all = this._handlers.FindAll((Predicate<ILinkedObjectsHandler>) (x => x.IsTypesChanged(session)));
    if (all != null && all.Count > 0)
    {
      this._sorted = false;
      foreach (ILinkedObjectsHandler linkedObjectsHandler in all)
        linkedObjectsHandler.UpdateHandleAndOutputTypes(session, false);
    }
    if (!this._sorted)
    {
      this._handlers.Sort((Comparison<ILinkedObjectsHandler>) ((handler1, handler2) =>
      {
        if (handler2.OutputTypes.Intersect<int>((IEnumerable<int>) handler1.HandleTypes).Count<int>() > 0)
          return 1;
        return handler1.OutputTypes.Intersect<int>((IEnumerable<int>) handler2.HandleTypes).Count<int>() > 0 ? -1 : 0;
      }));
      this._sorted = true;
    }
    string sessionName = $"LinkedObjectsService_{Guid.NewGuid()}";
    IUserSession cloneSession = PortalServicesSessionHelper.GetCloneSession(session, sessionName, "LinkedObjectsService.GetLinkedObjects");
    try
    {
      Dictionary<string, List<LinkedObject>> linkedObjectsEx = new Dictionary<string, List<LinkedObject>>(this._handlers.Count);
      foreach (ILinkedObjectsHandler handler in this._handlers)
      {
        if (handler.HandleTypes.Contains(objectType))
        {
          List<LinkedObject> linkedObjectList = handler.Handle(cloneSession, objectID, objectType, filtrationOwnerID);
          if (linkedObjectList != null && linkedObjectList.Count > 0)
            linkedObjectsEx.Add(handler.Name, linkedObjectList);
        }
      }
      return linkedObjectsEx;
    }
    finally
    {
      PortalServicesSessionHelper.LogoutSession(cloneSession, sessionName, "LinkedObjectsService.GetLinkedObjects");
    }
  }

  public void ForceReloadTypes(IUserSession session)
  {
    if (this._handlers.Count == 0)
      return;
    foreach (ILinkedObjectsHandler handler in this._handlers)
      handler.UpdateHandleAndOutputTypes(session, true);
  }

  public Dictionary<string, List<long>> GetLinkedObjects(
    IUserSession session,
    long objectID,
    int objectType,
    string filtrationOwnerID)
  {
    Dictionary<string, List<LinkedObject>> linkedObjectsEx = this.GetLinkedObjectsEx(session, objectID, objectType, filtrationOwnerID);
    Dictionary<string, List<long>> linkedObjects = new Dictionary<string, List<long>>();
    foreach (KeyValuePair<string, List<LinkedObject>> keyValuePair in linkedObjectsEx)
      linkedObjects.Add(keyValuePair.Key, keyValuePair.Value.ConvertAll<long>((Converter<LinkedObject, long>) (x => x.ObjectID)));
    return linkedObjects;
  }
}
