// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.LinkedObjectFromAttribute`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal abstract class LinkedObjectFromAttribute<TPublishCompositionAttributable, TObjectHandler> where TPublishCompositionAttributable : IIncludeTyped
{
  private readonly ICustomObjectAnalyzer _analyzer;
  protected readonly List<PublishCompositionObject> _objects;

  public LinkedObjectFromAttribute(
    ICustomObjectAnalyzer analyzer,
    List<PublishCompositionObject> objects)
  {
    this._analyzer = analyzer;
    this._objects = objects;
  }

  public List<PublishCompositionObject> GetObjects(
    IUserSession session,
    List<TPublishCompositionAttributable> attributableCollection,
    TObjectHandler handler)
  {
    List<PublishCompositionObject> result = new List<PublishCompositionObject>();
    foreach (TPublishCompositionAttributable attributable in attributableCollection)
    {
      if (attributable.Include == IncludeTypes.Include)
      {
        List<IDBObject> linkedObjects = this.GetLinkedObjects(session, attributable);
        if (linkedObjects != null)
        {
          foreach (IDBObject dbObject in linkedObjects)
          {
            IDBObject obj = dbObject;
            if (!result.Exists((Predicate<PublishCompositionObject>) (x => x.ObjectID == obj.ObjectID)))
            {
              PublishCompositionObject objectInfo = this._analyzer.GetObjectInfo(session, obj);
              if (PublishOptionsHelper.NormalPublish(objectInfo.Include))
                objectInfo.Include = IncludeTypes.ObjectLink;
              this.HandleObject(session, handler, result, objectInfo, attributable);
            }
          }
        }
      }
    }
    return result;
  }

  protected abstract void HandleObject(
    IUserSession session,
    TObjectHandler handler,
    List<PublishCompositionObject> result,
    PublishCompositionObject pco,
    TPublishCompositionAttributable parent);

  protected abstract List<IDBObject> GetLinkedObjects(
    IUserSession session,
    TPublishCompositionAttributable attributable);
}
