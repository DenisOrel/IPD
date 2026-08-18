// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Composition.ObjectLinkFromAttributableHandler`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Composition;

internal abstract class ObjectLinkFromAttributableHandler<TParent> : InfoRequiredService
{
  public ObjectLinkFromAttributableHandler(bool infoRequired)
    : base(infoRequired)
  {
  }

  public void HandleObject(
    PublishCompositionObject pco,
    List<PublishCompositionObject> resultListObjects,
    TParent parent)
  {
    if (this.HandleFilterIncludes(pco, false) || !PublishOptionsHelper.NormalPublish(pco.Include))
      return;
    this.OnAddReasonInfo(pco, resultListObjects, parent);
    resultListObjects.Add(pco);
  }

  protected abstract void OnAddReasonInfo(
    PublishCompositionObject pco,
    List<PublishCompositionObject> resultListObjects,
    TParent parent);
}
