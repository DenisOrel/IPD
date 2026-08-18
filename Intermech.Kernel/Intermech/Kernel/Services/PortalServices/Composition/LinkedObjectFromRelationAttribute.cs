// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.LinkedObjectFromRelationAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal sealed class LinkedObjectFromRelationAttribute(
  ExtendedPublishOptions options,
  ICustomObjectAnalyzer analyzer,
  List<PublishCompositionObject> objects) : 
  LinkedObjectFromAttributableAttribute<PublishCompositionRelation, ObjectLinkFromRelationHandler>(options, analyzer, objects)
{
  protected override IDBAttributable GetAttributable(
    IUserSession session,
    PublishCompositionRelation attributable)
  {
    return (IDBAttributable) session.GetRelation(attributable.PrjLinkID);
  }

  protected override IDBAttributableType GetAttributableType(
    IUserSession session,
    PublishCompositionRelation attributable)
  {
    return (IDBAttributableType) session.GetRelationType(attributable.RelationType);
  }

  protected override void HandleObject(
    IUserSession session,
    ObjectLinkFromRelationHandler handler,
    List<PublishCompositionObject> result,
    PublishCompositionObject pco,
    PublishCompositionRelation parent)
  {
    handler.HandleObject(pco, result, parent);
  }
}
