// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportPacketThreadArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportPacketThreadArgs : ImportThreadArgs
{
  public ImportVersionsModes ImportVersionsMode;

  public ImportPacketThreadArgs(
    IUserSession session,
    string updateGuid,
    long taskID,
    long[] objectsIDs,
    ImportVersionsModes importVersionsMode,
    bool startImmediately)
    : base(session, updateGuid, taskID, objectsIDs, startImmediately)
  {
    this.ImportVersionsMode = importVersionsMode;
  }
}
