// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.MessagePublisher
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal class MessagePublisher(RemoteData data, string enableSites) : 
  CustomDataPublisher<RemoteData>(data, TransferedObjectCategory.AutoTransfer, enableSites, PublishType.Simple)
{
  protected override CustomXMLFileFormer<RemoteData> GetXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer)
  {
    return (CustomXMLFileFormer<RemoteData>) new MessageXMLFileFormer(session, unit, writer, this.data);
  }
}
