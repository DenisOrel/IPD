// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DraftDocumentOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class DraftDocumentOperations
{
  private CaptureChangesDriverContext driverContext;
  private IDraftDocumentsService draftDocumentsService;

  public DraftDocumentOperations(
    CaptureChangesDriverContext driverContext,
    IDraftDocumentsService draftDocumentsService)
  {
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    if (draftDocumentsService == null)
      throw new ArgumentNullException(nameof (draftDocumentsService));
    this.driverContext = driverContext;
    this.draftDocumentsService = draftDocumentsService;
  }

  public IDraftDocumentsService Service
  {
    [DebuggerStepThrough] get => this.draftDocumentsService;
  }

  public SectionEntity CreateDraftDocument(
    string externalFilePath,
    int draftDocumentType,
    long? draftDocumentId)
  {
    if (string.IsNullOrEmpty(externalFilePath))
      throw new ArgumentException();
    if (draftDocumentType == -1)
      throw new ArgumentException();
    DraftDocumentSection sectionObject1 = new DraftDocumentSection(externalFilePath);
    ObjectSection sectionObject2 = new ObjectSection();
    if (draftDocumentId.HasValue)
      sectionObject2.ObjectId = draftDocumentId.Value;
    sectionObject2.ObjectType = draftDocumentType;
    sectionObject2.ExistenceStatus = sectionObject2.ObjectId == 0L ? ObjectExistenceStatus.NewObject : ObjectExistenceStatus.ExistingObject;
    FilesSection sectionObject3 = new FilesSection();
    sectionObject3.MasterFile = externalFilePath;
    DisplaySection sectionObject4 = new DisplaySection();
    sectionObject4.DisplayName = externalFilePath;
    sectionObject4.QualifiedName = externalFilePath;
    ObjectActionsSection sectionObject5 = new ObjectActionsSection();
    SectionEntity draftDocument = new SectionEntity();
    draftDocument.Sections.Set((object) sectionObject1);
    draftDocument.Sections.Set((object) sectionObject2);
    draftDocument.Sections.Set((object) sectionObject3);
    draftDocument.Sections.Set((object) sectionObject4);
    draftDocument.Sections.Set((object) sectionObject5);
    draftDocument.Sections.Set((object) new ProxyDocumentSection());
    return draftDocument;
  }

  public SectionEntity AddDraftDocumentToOperationDatabase(
    string externalFilePath,
    int draftDocumentType,
    long? draftDocumentId = null)
  {
    SectionEntity draftDocument = this.CreateDraftDocument(externalFilePath, draftDocumentType, draftDocumentId);
    this.driverContext.Database.Add((IEntity) draftDocument);
    return draftDocument;
  }

  public void AttachDraftDocumentInfo(SectionEntity documentEntity, long draftDocumentId)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (draftDocumentId == 0L)
      throw new ArgumentException();
    documentEntity.Sections.Set((object) new DraftDocumentConvertationSection(draftDocumentId));
  }

  public bool TryCreateBlankDocumentFromDraftDocument(SectionEntity documentEntity)
  {
    DraftDocumentConvertationSection convertationSection = documentEntity != null ? documentEntity.Sections.Get<DraftDocumentConvertationSection>((DraftDocumentConvertationSection) null) : throw new ArgumentNullException(nameof (documentEntity));
    if (convertationSection != null)
    {
      this.CreateBlankDocumentFromDraftDocument(documentEntity, convertationSection.DraftDocumentId);
      return true;
    }
    if (this.driverContext.Database.IsEntryPointDocument(documentEntity))
    {
      string draftFilename = PathUtils.GetRelativePath(FilesSection.GetMasterFile(documentEntity), ClientContext.FileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
      Tuple<long, string> tuple = CollectionUtils.Find<Tuple<long, string>>((IEnumerable<Tuple<long, string>>) this.GetCurrentUserDraftDocumentsCached(), (Predicate<Tuple<long, string>>) (item => PathUtils.IsSamePath(draftFilename, item.Item2)));
      if (tuple != null)
      {
        this.AttachDraftDocumentInfo(documentEntity, tuple.Item1);
        this.CreateBlankDocumentFromDraftDocument(documentEntity, tuple.Item1);
        return true;
      }
    }
    return false;
  }

  private void CreateBlankDocumentFromDraftDocument(
    SectionEntity documentEntity,
    long draftDocumentId)
  {
    string relativePath = PathUtils.GetRelativePath(FilesSection.GetMasterFile(documentEntity), ClientContext.FileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    ObjectSection objectSection = documentEntity.Sections.Get<ObjectSection>();
    long objectId = draftDocumentId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(draftDocumentId, true);
      dbObject.ObjectType = objectSection.ObjectType;
      this.driverContext.ServerCleanupActions.Add((IAction) new WriteObjectAttributesAction((IDBObjectRef) new DirectDBObjectRef(draftDocumentId), new AttributeValues[1]
      {
        new AttributeValues(this.draftDocumentsService.IdCache.ExternalFilePath.Id, (object) relativePath)
      }));
      this.driverContext.ServerCleanupActions.Add((IAction) new ChangeObjectTypeAction((IDBObjectRef) new DirectDBObjectRef(draftDocumentId), this.draftDocumentsService.IdCache.DraftDocuments.Id));
      draftDocumentId = dbObject.CheckOut().ObjectID;
      this.driverContext.ServerCleanupActions.Add((IAction) new CancelChangesAction((IDBObjectRef) new DirectDBObjectRef(draftDocumentId)));
    }
    ClientContext.FileVault.WorkArea.Unpublish(objectId);
    ClientContext.FileVault.WorkArea.Attach(draftDocumentId);
    this.driverContext.ServerCleanupActions.Add((IAction) new DraftDocumentOperations.DetachFromFileVaultAction(draftDocumentId));
    objectSection.ObjectId = draftDocumentId;
    objectSection.ExistenceStatus = ObjectExistenceStatus.ConvertedObject;
    documentEntity.Sections.Get<ObjectActionsSection>().ObjectActions.ClientActions.Add((IAction) new FireObjectCheckedOutAction((IDBObjectRef) new DBObjectEntityRef(documentEntity), this.driverContext.UINotifications));
  }

  /// <summary>
  /// Возвращает из базы данных IPS все черновики документов, владельцем которых является текущий пользователь.
  /// Результат выполнения метода кэшируется в контексте анализатора изменений.
  /// </summary>
  /// <returns>Список пар вида (идентификатор версии черновика, имя файла черновика)</returns>
  public List<Tuple<long, string>> GetCurrentUserDraftDocumentsCached()
  {
    return CaptureChangesDatabaseGlobals<List<Tuple<long, string>>>.GetOrCreate(this.driverContext.Database, "GetCurrentUserDraftDocuments", new Func<List<Tuple<long, string>>>(this.draftDocumentsService.GetCurrentUserDraftDocuments));
  }

  private sealed class DetachFromFileVaultAction : IAction
  {
    private long objectId;

    public DetachFromFileVaultAction(long objectId) => this.objectId = objectId;

    public void Perform() => ClientContext.FileVault.WorkArea.Unpublish(this.objectId);
  }
}
