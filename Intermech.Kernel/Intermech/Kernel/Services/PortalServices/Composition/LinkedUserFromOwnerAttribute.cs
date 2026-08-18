// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.LinkedUserFromOwnerAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal sealed class LinkedUserFromOwnerAttribute(
  ICustomObjectAnalyzer analyzer,
  List<PublishCompositionObject> objects) : 
  LinkedObjectFromAttribute<PublishCompositionObject, OwnerHandler>(analyzer, objects)
{
  protected override List<IDBObject> GetLinkedObjects(
    IUserSession session,
    PublishCompositionObject attributable)
  {
    List<IDBObject> linkedObjects = new List<IDBObject>();
    if (!this._objects.Exists((Predicate<PublishCompositionObject>) (_ => _.ObjectID == attributable.OwnerID)))
    {
      IDBObject dbObject = session.GetObject(attributable.OwnerID, false);
      if (dbObject != null)
        linkedObjects.Add(dbObject);
    }
    return linkedObjects;
  }

  protected override void HandleObject(
    IUserSession session,
    OwnerHandler handler,
    List<PublishCompositionObject> result,
    PublishCompositionObject pco,
    PublishCompositionObject parent)
  {
    handler.HandleObject(pco, result, parent);
  }
}
