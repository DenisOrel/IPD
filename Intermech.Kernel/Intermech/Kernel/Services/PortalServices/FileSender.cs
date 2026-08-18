// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.FileSender
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.WebPortal;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class FileSender
{
  protected string unitGuid;

  public FileSender(string unitGuid) => this.unitGuid = unitGuid;

  public void TransferFile(
    Guid connectionGuid,
    IPortalConnector connector,
    string fileName,
    int size)
  {
    int packetSize;
    if (size == 0)
    {
      connector.TransferPublishUnitFile(connectionGuid, this.unitGuid, fileName, (byte[]) null, false);
    }
    else
    {
      for (int startPosition = 0; startPosition < size; startPosition += packetSize)
      {
        int num = size - startPosition;
        packetSize = num < PortalConsts.DefaultFileTransferBufferLength ? num : PortalConsts.DefaultFileTransferBufferLength;
        byte[] bytes = this.ReadData(startPosition, packetSize);
        if (startPosition == 0)
          connector.TransferPublishUnitFile(connectionGuid, this.unitGuid, fileName, bytes, false);
        else
          connector.TransferPublishUnitFile(connectionGuid, this.unitGuid, fileName, bytes, true);
      }
    }
  }

  protected abstract byte[] ReadData(int startPosition, int packetSize);
}
