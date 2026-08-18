// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ExtendedTransferedObjectExporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ExtendedTransferedObjectExporter : 
  TransferedObjectExporter<ExtendedTransferedObject>,
  ITransferedObjectExporter
{
  private readonly IBlobReader _reader;

  public ExtendedTransferedObjectExporter(
    long portalTaskID,
    IBlobReader reader,
    ExtendedTransferedObject unit)
    : base(portalTaskID, unit)
  {
    this._reader = reader;
  }

  public void Publish(IUserSession session, Guid connectionGuid, IPortalConnector connector)
  {
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"ExtendedTransferedObjectExporter start publish unit={this.unit.GUID} connectionGuid={connectionGuid}");
    connector.PublishUnit(connectionGuid, this.portalTaskID, this.unit.ToTransferedObject);
    FromBlobReaderFileSender readerFileSender = new FromBlobReaderFileSender(this._reader, this.unit.GUID);
    if (this.unit.DataFiles == null)
      return;
    for (int index = 0; index < this.unit.DataFiles.Length; ++index)
    {
      if (SiteTraceLog.Enabled)
        SiteTraceLog.Write($"transfer file={this.unit.DataFiles[index]} size={this.unit.FileSizes[index]} unit={this.unit.GUID} connectionGuid={connectionGuid}");
      readerFileSender.TransferFile(connectionGuid, connector, this.unit.DataFiles[index], Convert.ToInt32(this.unit.FileSizes[index]));
    }
  }
}
