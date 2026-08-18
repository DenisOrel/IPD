// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.ObjectLinkFromObjectHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal sealed class ObjectLinkFromObjectHandler(bool infoRequired) : 
  ObjectLinkFromAttributableHandler<PublishCompositionObject>(infoRequired)
{
  protected override void OnAddReasonInfo(
    PublishCompositionObject pco,
    List<PublishCompositionObject> resultListObjects,
    PublishCompositionObject parent)
  {
    this.AddReasonInfo(pco, $"По ссылочному атрибуту у {MetaDataHelper.GetObjectName(parent.ObjectType)} {parent.Caption}");
  }
}
