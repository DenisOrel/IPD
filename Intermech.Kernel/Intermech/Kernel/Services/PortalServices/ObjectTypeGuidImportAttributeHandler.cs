// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectTypeGuidImportAttributeHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ObjectTypeGuidImportAttributeHandler : ImportAttributeHandler
{
  public override void Handle(SpecHandleAttributeEventArgs e, Dictionary<string, object> tag)
  {
    string str = Convert.ToString(e.Value.StringValue);
    if (!GuidHelper.IsGuid(str) || e.Session.GetObjectType(new Guid(str), false) != null)
      return;
    e.Value.StringValue = (object) null;
    tag["error"] = (object) new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_951"), (object) str, (object) e.AttributeID, (object) e.AttributableID));
    e.Handled = true;
  }
}
