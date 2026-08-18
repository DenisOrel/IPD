// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.FolderKeyImportAttributeHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class FolderKeyImportAttributeHandler : ImportAttributeHandler
{
  public override void Handle(SpecHandleAttributeEventArgs e, Dictionary<string, object> tag)
  {
    e.Value.StringValue = (object) string.Empty;
    tag["needRefreshFolderKey"] = (object) e.AttributableID;
    e.Handled = true;
  }
}
