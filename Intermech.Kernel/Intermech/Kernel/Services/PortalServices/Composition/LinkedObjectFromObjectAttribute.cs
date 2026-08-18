// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.LinkedObjectFromObjectAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal sealed class LinkedObjectFromObjectAttribute(
  ExtendedPublishOptions options,
  ICustomObjectAnalyzer analyzer,
  List<PublishCompositionObject> objects) : 
  LinkedObjectFromAttributableAttribute<PublishCompositionObject, ObjectLinkFromObjectHandler>(options, analyzer, objects)
{
  protected override IDBAttributable GetAttributable(
    IUserSession session,
    PublishCompositionObject attributable)
  {
    return (IDBAttributable) session.GetObject(attributable.ObjectID);
  }

  protected override IDBAttributableType GetAttributableType(
    IUserSession session,
    PublishCompositionObject attributable)
  {
    return (IDBAttributableType) session.GetObjectType(attributable.ObjectType);
  }

  protected override void HandleObject(
    IUserSession session,
    ObjectLinkFromObjectHandler handler,
    List<PublishCompositionObject> result,
    PublishCompositionObject pco,
    PublishCompositionObject parent)
  {
    handler.HandleObject(pco, result, parent);
  }
}
