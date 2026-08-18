// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportObjectsThread
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Portal.Connector;
using System;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportObjectsThread(IPortalProxy proxy) : ImportThread(proxy)
{
  private bool _importMethodFinished;

  protected override ImportTask GetImportTask(ImportThreadArgs args)
  {
    string str = string.Format(args.ObjectsIDs.Length == 1 ? "объекта {0}" : "объектов {0},...", (object) args.ObjectsIDs[0]);
    return new ImportTask(args.Session.UserID, (args.Session as UserSession).UserGUID, "Запрос на портал для импорта " + str, args.UpdateGuid);
  }

  protected override void OnImport(Guid connectGuid, DBTask dbTask, ImportThreadArgs ita)
  {
    ImportObjectsThreadArgs objectsThreadArgs = ita as ImportObjectsThreadArgs;
    string[] objectTypeGuidsList = Helper.GetObjectTypeGuidsList(objectsThreadArgs.FilteredTypes);
    int countLevels = 0;
    switch (objectsThreadArgs.CompositionType)
    {
      case SelectCompositionType.FirstLevel:
        countLevels = 1;
        break;
      case SelectCompositionType.RecursiveComposition:
        countLevels = -1;
        break;
    }
    if (this.proxy.AsyncSupported)
    {
      this.proxy.CreateImportTask(connectGuid.ToString(), objectsThreadArgs.UpdateGuid.ToString(), objectsThreadArgs.ObjectsIDs, objectTypeGuidsList, objectsThreadArgs.SetOwner, objectsThreadArgs.AutoUpdate, countLevels);
      for (ImportInfo importInfo = this.proxy.GetImportInfo(connectGuid.ToString(), objectsThreadArgs.UpdateGuid.ToString()); importInfo != null && importInfo.ImportTaskStatus != ImportTaskStatuses.Completed; importInfo = this.proxy.GetImportInfo(connectGuid.ToString(), objectsThreadArgs.UpdateGuid.ToString()))
      {
        if (importInfo.ImportTaskStatus == ImportTaskStatuses.Error)
          throw new Exception(importInfo.ErrorMessage);
        dbTask.SetPercent(Convert.ToDouble(importInfo.Persent));
        Thread.Sleep(300);
      }
    }
    else
      this.proxy.ImportObjects(connectGuid.ToString(), objectsThreadArgs.UpdateGuid.ToString(), objectsThreadArgs.ObjectsIDs, objectTypeGuidsList, objectsThreadArgs.SetOwner, objectsThreadArgs.AutoUpdate, countLevels);
  }
}
