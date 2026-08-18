// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.ObjectLinkFromRelationHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal sealed class ObjectLinkFromRelationHandler(bool infoRequired) : 
  ObjectLinkFromAttributableHandler<PublishCompositionRelation>(infoRequired)
{
  protected override void OnAddReasonInfo(
    PublishCompositionObject pco,
    List<PublishCompositionObject> resultListObjects,
    PublishCompositionRelation parent)
  {
    PublishCompositionObject compositionObject = resultListObjects.Find((Predicate<PublishCompositionObject>) (x => x.ObjectGuid.Equals(parent.PartGuid)));
    this.AddReasonInfo(pco, $"По ссылочному атрибуту связи c {MetaDataHelper.GetObjectName(compositionObject.ObjectType)} {compositionObject.Caption} ({MetaDataHelper.GetRelationTypeName(parent.RelationType)})");
  }
}
