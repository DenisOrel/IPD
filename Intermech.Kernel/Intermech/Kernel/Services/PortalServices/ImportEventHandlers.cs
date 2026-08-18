// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportEventHandlers
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Workflow;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal static class ImportEventHandlers
{
  public static void OnImportError(object sender, ImportTaskErrorEventArgs e)
  {
    IImportRulesService service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, true);
    if (service.ImportErrorTemplate == 0L)
      return;
    new ImportErrorMessage(e.Session, service.ImportErrorTemplate, e.TaskID).CreateProcess();
  }

  public static void OnImportCompleted(object sender, ImportTaskCompletedEventArgs e)
  {
    if (e.ObjectIDs == null || e.ObjectIDs.Count == 0)
      return;
    IImportRulesService service = ServiceUtils.GetService<IImportRulesService>((object) ServerServices.ServiceContainer, true);
    if (service.ImportCompleteTemplate == 0L)
      return;
    List<long> objectIDs = new List<long>();
    IDBRelationsApplicabilityCollection applicabilityCollection = e.Session.GetRelationsApplicabilityCollection();
    foreach (Tuple<long, int> objectId in e.ObjectIDs)
    {
      IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(MetaDataHelper.GetRelationTypeID(SystemGUIDs.relationTypeAttachments), objectId.Item2, wfConsts.StartTypeID);
      if (applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled)
        objectIDs.Add(objectId.Item1);
    }
    new ImportCompleteMessage(e.Session, service.ImportCompleteTemplate, objectIDs).CreateProcess();
  }
}
