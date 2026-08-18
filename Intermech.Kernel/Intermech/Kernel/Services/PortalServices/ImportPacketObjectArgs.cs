// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportPacketObjectArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportPacketObjectArgs : ImportArgs
{
  public ImportReceipt Receipt;
  public ImportVersionsModes ImportVersionsMode;

  public ImportPacketObjectArgs(
    IUserSession session,
    ITransferedObject unit,
    string path,
    Dictionary<Guid, ImportedInfo> links,
    long userID,
    Guid userGuid,
    IEventLogHelper eventHelper,
    List<long> updateFolderKeyObjects,
    List<Tuple<long, Guid, long>> changesGroupNums,
    List<Tuple<Guid, Guid, long, List<Guid>>> contexts,
    List<Tuple<Guid, List<Guid>>> importedCompositions,
    ImportReceipt receipt,
    ImportVersionsModes importVersionsMode,
    Dictionary<long, Guid> parentVersions)
    : base(session, unit, path, links, userID, userGuid, eventHelper, updateFolderKeyObjects, changesGroupNums, contexts, importedCompositions, parentVersions)
  {
    this.Receipt = receipt;
    this.ImportVersionsMode = importVersionsMode;
  }
}
