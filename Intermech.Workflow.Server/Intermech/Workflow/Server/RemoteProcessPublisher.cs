// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.RemoteProcessPublisher
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Services.PortalServices;

#nullable disable
namespace Intermech.Workflow.Server;

public static class RemoteProcessPublisher
{
  public static Publisher Create(
    IUserSession session,
    CustomPublishDataInfo processInfo,
    Packet4Publish packet,
    bool createReceipt,
    long localProcessID,
    long localActivityID)
  {
    if (processInfo.Attachments == null || processInfo.Attachments.Count <= 0)
      return (Publisher) new EmptyRemoteProcessPublisher(processInfo, packet);
    processInfo.Options.CompositionOptions |= PublishCompositionOptions.IncludeObjectsAlways | PublishCompositionOptions.ForcedPublication;
    processInfo.Options.EnableSites = processInfo.SiteRecipient.ToString();
    return (Publisher) new RemoteProcessWithAttachmentsPublisher(processInfo, Publisher.Composition(session, processInfo.Attachments, processInfo.Options, PublishType.Simple), packet, createReceipt, localProcessID, localActivityID);
  }
}
