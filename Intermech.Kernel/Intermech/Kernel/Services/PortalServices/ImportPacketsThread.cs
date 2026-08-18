// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportPacketsThread
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Portal.Connector;
using System;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportPacketsThread(IPortalProxy proxy) : ImportThread(proxy)
{
  protected override ImportTask GetImportTask(ImportThreadArgs args)
  {
    string str = string.Format(args.ObjectsIDs.Length == 1 ? "пакета {0}" : "пакетов {0},...", (object) args.ObjectsIDs[0]);
    return new ImportTask(args.Session.UserID, (args.Session as UserSession).UserGUID, "Запрос на портал для импорта " + str, args.UpdateGuid);
  }

  protected override void OnImport(Guid connectGuid, DBTask dbTask, ImportThreadArgs ita)
  {
    ImportPacketThreadArgs packetThreadArgs = ita as ImportPacketThreadArgs;
    this.proxy.ImportPackets(connectGuid.ToString(), packetThreadArgs.UpdateGuid.ToString(), packetThreadArgs.ObjectsIDs);
  }

  protected override void AfterCreateTask(IDBObject dbTask, ImportThreadArgs ita)
  {
    ImportPacketThreadArgs packetThreadArgs = ita as ImportPacketThreadArgs;
    dbTask.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeImportVersionsModes), false, new object[1]
    {
      (object) (int) packetThreadArgs.ImportVersionsMode
    });
  }
}
