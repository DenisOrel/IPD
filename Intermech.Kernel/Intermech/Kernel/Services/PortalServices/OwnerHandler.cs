// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.OwnerHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class OwnerHandler(bool infoRequired) : InfoRequiredService(infoRequired)
{
  public void HandleObject(
    PublishCompositionObject pco,
    List<PublishCompositionObject> resultListObjects,
    PublishCompositionObject obj)
  {
    if (this.HandleFilterIncludes(pco, false) || !PublishOptionsHelper.NormalPublish(pco.Include))
      return;
    this.AddReasonInfo(pco, $"Владелец {MetaDataHelper.GetObjectName(obj.ObjectType)} {obj.Caption}");
    resultListObjects.Add(pco);
  }
}
