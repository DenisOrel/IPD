// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.WeldingJointsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class WeldingJointsService : IWeldingJointsService
{
  private IMainFormUpdate mainFormService;
  private INotificationService notificationService;
  private IFileVault fileVaultService;
  private IIntegratorRegistry integratorRegistry;
  private Func<IWeldingSeamsModelRoot> modelRootFactory;

  public WeldingJointsService(
    IMainFormUpdate mainFormService,
    INotificationService notificationService,
    IFileVault fileVaultService,
    IIntegratorRegistry integratorRegistry,
    Func<IWeldingSeamsModelRoot> modelRootFactory)
  {
    this.mainFormService = mainFormService;
    this.notificationService = notificationService;
    this.fileVaultService = fileVaultService;
    this.integratorRegistry = integratorRegistry;
    this.modelRootFactory = modelRootFactory;
  }

  public bool CanUpdateWeldingSeams(int documentTypeId)
  {
    if (documentTypeId == -1)
      throw new ArgumentException("Не задан идентификатор типа документа IPS.", nameof (documentTypeId));
    return this.Is3DModelDocumentType(documentTypeId) && PDMHelper.IsDocumentWithArticles(documentTypeId);
  }

  private bool Is3DModelDocumentType(int documentTypeId)
  {
    IntegratorObject integrator = IntegratorServices.Find(documentTypeId);
    if (integrator == null)
      return false;
    ICADSettingsService service = IntegratorServices.GetService<ICADSettingsService>(integrator, false);
    if (service == null)
      return false;
    CADSettings cadSettings;
    try
    {
      cadSettings = service.GetCADSettings();
    }
    catch
    {
      return false;
    }
    DocumentGroup byDocumentType = cadSettings.FileDocumentGroups.FindByDocumentType(documentTypeId, false);
    if (byDocumentType == null)
      return false;
    return byDocumentType.Name == "Assembly" || byDocumentType.Name == "Part";
  }

  public UpdateWeldingSeamsResult UpdateWeldingSeams(long documentId)
  {
    return documentId != 0L ? this.UpdateWeldingSeamsInternal(documentId, DBHelper.GetObjectType(documentId)) : throw new ArgumentException("Не задан идентификатор версии документа IPS.", nameof (documentId));
  }

  public UpdateWeldingSeamsResult UpdateWeldingSeams(IDBTypedObjectID documentInfo)
  {
    if (documentInfo == null)
      throw new ArgumentNullException(nameof (documentInfo));
    return this.UpdateWeldingSeamsInternal(documentInfo.ObjectID, documentInfo.ObjectType);
  }

  public UpdateWeldingSeamsResult UpdateWeldingSeams(QuickObjectInfo documentInfo)
  {
    if (documentInfo.Empty)
      throw new ArgumentException("Не задан идентификатор версии документа IPS.", nameof (documentInfo));
    return this.UpdateWeldingSeamsInternal(documentInfo.ObjectID, documentInfo.ObjectTypeID);
  }

  private UpdateWeldingSeamsResult UpdateWeldingSeamsInternal(long documentId, int documentTypeId)
  {
    IIntegrator integrator = this.integratorRegistry.GetIntegrator(IntegratorServices.Find(documentTypeId), true);
    UpdateWeldingSeamsAction weldingSeamsAction = new UpdateWeldingSeamsAction(documentId, integrator, this.mainFormService, this.notificationService, this.fileVaultService, this.modelRootFactory());
    weldingSeamsAction.Perform();
    return weldingSeamsAction.Result;
  }
}
