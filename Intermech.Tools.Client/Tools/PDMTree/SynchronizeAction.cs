// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.SynchronizeAction
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class SynchronizeAction : PDMSystemAction
{
  private readonly IFileVault fileVault;
  private readonly SynchronizeOperations synchronizeOperations;
  private readonly PDMDocument rootDocument;
  private bool reloadRequired;
  private List<DBObjectState> unpublishedObjects;
  private List<DBObjectState> outdatedObjects;
  private List<DBObjectState> unsavedWorkObjects;
  private List<DBObjectState> savedWorkObjects;

  public SynchronizeAction(PDMDocument rootDocument)
    : base(rootDocument.PDMSystem, LocalizationHolder.rm.GetString("SR_307"))
  {
    if (rootDocument == null)
      throw new ArgumentNullException(nameof (rootDocument));
    this.rootDocument = rootDocument.ObjectId != 0L ? rootDocument : throw new ArgumentException();
    this.fileVault = rootDocument.PDMSystem.PDMSystemContext.FileVaultService;
    this.synchronizeOperations = (SynchronizeOperations) new SynchronizeAction.InternalSynchronizeOperations(this.fileVault, rootDocument.PDMSystem);
    this.synchronizeOperations.EnableUIReport = UIReport.Enabled;
  }

  protected override void DoPerform()
  {
    using (new DynamicScope())
    {
      VersionsRuleSources.AllowCache.Declare(true);
      base.DoPerform();
      this.reloadRequired = false;
      PDMDocumentVersionInfo actualDocumentVersion = this.synchronizeOperations.GetActualDocumentVersion(this.rootDocument.ID);
      List<DBObjectState> documentStructure = this.synchronizeOperations.GetActualDocumentStructure(actualDocumentVersion.DBObjectState.ObjectId, true);
      if (UIReport.Enabled)
        this.ReportRootDocumentInfo(actualDocumentVersion, documentStructure);
      PDMDocumentSynchronizationInfo synchronizationInfo = this.synchronizeOperations.AnalyzeDocumentStructure(actualDocumentVersion, documentStructure);
      this.unpublishedObjects = synchronizationInfo.UnpublishedObjects;
      this.outdatedObjects = synchronizationInfo.OutdatedObjects;
      this.unsavedWorkObjects = synchronizationInfo.UnsavedWorkObjects;
      this.savedWorkObjects = synchronizationInfo.SavedWorkObjects;
      this.ProcessDocuments();
    }
  }

  protected override void DoCleanup()
  {
    base.DoCleanup();
    this.unpublishedObjects = (List<DBObjectState>) null;
    this.outdatedObjects = (List<DBObjectState>) null;
    this.unsavedWorkObjects = (List<DBObjectState>) null;
    this.savedWorkObjects = (List<DBObjectState>) null;
  }

  private void ReportRootDocumentInfo(
    PDMDocumentVersionInfo rootDocumentVersion,
    List<DBObjectState> rootDocumentStructure)
  {
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Client_122"), (object) rootDocumentVersion.DBObjectState.ObjectId, (object) rootDocumentVersion.DBObjectState.Caption));
    UIReport.Indent();
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Client_123"), (object) rootDocumentStructure.Count));
    UIReport.Unindent();
  }

  private void ProcessDocuments()
  {
    if (this.unsavedWorkObjects.Count != 0 || this.savedWorkObjects.Count != 0)
      this.PushChangesToIPS();
    if (this.outdatedObjects.Count == 0 && this.unpublishedObjects.Count == 0)
      return;
    this.PullChangesFromIPS();
  }

  private void PushChangesToIPS()
  {
    List<DBObjectState> objects = (List<DBObjectState>) null;
    if (UIReport.Enabled)
      objects = new List<DBObjectState>(this.unsavedWorkObjects.Count + this.savedWorkObjects.Count);
    try
    {
      if (this.unsavedWorkObjects.Count != 0)
      {
        ICaptureChangesService service = ServiceUtils.GetService<ICaptureChangesService>((object) this.pdmSystem.Integrator, true);
        foreach (DBObjectState unsavedWorkObject in this.unsavedWorkObjects)
        {
          using (UIReport.CreateIsolatedScope())
            service.CaptureChanges(unsavedWorkObject.ObjectId);
          this.SaveToArchCopyIfUpdated(unsavedWorkObject);
          if (UIReport.Enabled)
            objects.Add(this.fileVault.DBObjectsInfo.GetObjectState(unsavedWorkObject.ObjectId, true));
        }
      }
      if (this.savedWorkObjects.Count == 0)
        return;
      foreach (DBObjectState savedWorkObject in this.savedWorkObjects)
      {
        if (this.SaveToArchCopyIfUpdated(savedWorkObject) && UIReport.Enabled)
          objects.Add(this.fileVault.DBObjectsInfo.GetObjectState(savedWorkObject.ObjectId, true));
      }
    }
    finally
    {
      if (objects.Count != 0 && UIReport.Enabled)
        SynchronizeAction.ReportProcessedObjects((ICollection<DBObjectState>) objects, LocalizationHolder.rm.GetString("Tools.Client_126"));
    }
  }

  private bool SaveToArchCopyIfUpdated(DBObjectState objectState)
  {
    bool archCopyIfUpdated = objectState.ModifyMode == ObjectModifyModes.Checkout && this.IsWorkCopyUpdated(objectState.ObjectId);
    if (archCopyIfUpdated)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(objectState.ObjectId, true).SaveToArcCopy();
    }
    return archCopyIfUpdated;
  }

  private bool IsWorkCopyUpdated(long workCopyObjectId)
  {
    if (workCopyObjectId < 0L)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(workCopyObjectId, true);
        IDBObject dbObject2 = sessionKeeper.Session.GetObject(-workCopyObjectId, true);
        int id = IDCache.Default.ContentModifyDate.Id;
        IDBAttribute attributeById1 = dbObject1.GetAttributeByID(id);
        IDBAttribute attributeById2 = dbObject2.GetAttributeByID(IDCache.Default.ContentModifyDate.Id);
        if (attributeById1 != null)
        {
          if (attributeById2 != null)
          {
            if (attributeById1.AsDateTime != attributeById2.AsDateTime)
              return true;
          }
        }
      }
    }
    return false;
  }

  private void PullChangesFromIPS()
  {
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>(this.outdatedObjects.Count + this.unpublishedObjects.Count);
    if (this.unpublishedObjects.Count != 0)
      dbObjectStateList.AddRange((IEnumerable<DBObjectState>) this.unpublishedObjects);
    if (this.outdatedObjects.Count != 0)
      dbObjectStateList.AddRange((IEnumerable<DBObjectState>) this.outdatedObjects);
    ISynchronizeActionReloadStrategy reloadStrategy = this.CreateReloadStrategy();
    reloadStrategy.BeginOperation(dbObjectStateList);
    try
    {
      if (reloadStrategy.TryUnlockFiles())
      {
        if (UIReport.Enabled)
          SynchronizeAction.ReportProcessedObjects((ICollection<DBObjectState>) dbObjectStateList, LocalizationHolder.rm.GetString("Tools.Client_125"));
        this.reloadRequired = this.fileVault.WorkArea.Publish((IList<DBObjectState>) dbObjectStateList, (IReplaceFilePolicy) new ForceRefresh()).ReloadedFiles != 0;
      }
      else
      {
        if (UIReport.Enabled)
          UIReport.ReportEvent("Не удалось выгрузить файлы документов из памяти CAD-системы", TraceLevel.Warning);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Невозможно заменить файлы документов на диске более свежими файлами из базы данных, так как не удалось выгрузить эти файлы из памяти CAD-системы.");
        stringBuilder.AppendLine("Чтобы обновить заблокированные файлы вручную, закройте все документы в CAD-системе, а затем переоткройте их из IPS.");
        int num = (int) MessageBox.Show(stringBuilder.ToString(), this.actionName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        this.reloadRequired = false;
      }
    }
    finally
    {
      reloadStrategy.EndOperation();
    }
  }

  private ISynchronizeActionReloadStrategy CreateReloadStrategy()
  {
    return ServiceUtils.GetService<IPDMBrowserService>((object) this.pdmSystem.Integrator, true).CreateSynchronizeActionReloadStrategy();
  }

  private static void ReportProcessedObjects(
    ICollection<DBObjectState> objects,
    string eventCaption)
  {
    UIReport.ReportEvent(eventCaption);
    UIReport.Indent();
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_310"), (object) objects.Count));
    foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) objects)
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_311"), (object) dbObjectState.ObjectId, (object) dbObjectState.Caption));
    UIReport.Unindent();
  }

  public PDMDocument RootDocument => this.rootDocument;

  public bool ReloadRequired => this.reloadRequired;

  private sealed class InternalSynchronizeOperations : SynchronizeOperations
  {
    private PDMSystem pdmSystem;

    public InternalSynchronizeOperations(IFileVault fileVaultService, PDMSystem pdmSystem)
      : base(fileVaultService)
    {
      this.pdmSystem = pdmSystem != null ? pdmSystem : throw new ArgumentNullException(nameof (pdmSystem));
    }

    protected override void SaveDocumentsToDisk(
      PDMDocumentVersionInfo rootDocument,
      ICollection<DBObjectState> documents)
    {
      base.SaveDocumentsToDisk(rootDocument, documents);
      if (rootDocument.DBObjectState.IsEditableState)
        this.SaveDocumentFileToDisk(rootDocument.DBObjectState.ObjectId, rootDocument.MasterFileName);
      foreach (DBObjectStateWithFiles fileState in this.FileVaultService.DBFilesInfo.GetFileStates((IList<DBObjectState>) CollectionUtils.FindAllAsList<DBObjectState>(documents, (Predicate<DBObjectState>) (doc => doc.IsEditableState))))
      {
        foreach (FileState file in fileState.Files)
          this.SaveDocumentFileToDisk(fileState.Owner.ObjectId, file.FileName);
      }
    }

    private void SaveDocumentFileToDisk(long objectId, string masterFileName)
    {
      string fullPath = Path.GetFullPath(Path.Combine(this.FileVaultService.WorkArea.AreaPath, masterFileName));
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.pdmSystem.CADService))
      {
        CADDocumentProxy openDocument = cadApiSession.Application.FindOpenDocument(fullPath);
        if (openDocument == null || !openDocument.Modified || openDocument.ReadOnly)
          return;
        openDocument.Save();
      }
    }
  }
}
