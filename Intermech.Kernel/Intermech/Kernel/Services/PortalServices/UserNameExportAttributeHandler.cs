// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.UserNameExportAttributeHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal class UserNameExportAttributeHandler : ExportAttributeHandler
{
  public override void Handle(IUserSession session, IDBAttribute attr)
  {
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    this.Value = attr.AsString.IndexOf('\\') < 0 ? (object) PortalConsts.GlobalUserName(customService.Info.Caption, attr.AsString) : (object) attr.AsString;
  }
}
