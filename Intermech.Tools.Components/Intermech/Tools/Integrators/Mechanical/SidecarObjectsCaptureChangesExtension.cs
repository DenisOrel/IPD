// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarObjectsCaptureChangesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public abstract class SidecarObjectsCaptureChangesExtension : ISidecarObjectsCaptureChangesExtension
{
  private readonly MechanicalDriver driver;
  private readonly SidecarObjectsIDCache sidecarIDCache;
  private readonly SidecarObjectsOperations sidecarOperations;
  private bool enableSanityChecks;
  private ManualResetEvent sidecarStartStage;
  private ManualResetEvent sidecarRelationsStage;
  private ManualResetEvent sidecarDiskWritesStage;
  private ManualResetEvent sidecarUploadFilesStage;

  public SidecarObjectsCaptureChangesExtension(
    MechanicalDriver driver,
    SidecarObjectsIDCache sidecarIDCache)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (sidecarIDCache == null)
      throw new ArgumentNullException(nameof (sidecarIDCache));
    this.driver = driver;
    this.sidecarIDCache = sidecarIDCache;
    this.sidecarOperations = new SidecarObjectsOperations(sidecarIDCache);
    this.enableSanityChecks = true;
  }

  public SidecarObjectsIDCache SidecarIDCache => this.sidecarIDCache;

  public bool EnableSanityChecks
  {
    get => this.enableSanityChecks;
    set => this.enableSanityChecks = value;
  }

  public virtual void Initialize() => this.UpdateScheduler();

  public virtual void Cleanup()
  {
    this.sidecarStartStage = (ManualResetEvent) null;
    this.sidecarRelationsStage = (ManualResetEvent) null;
    this.sidecarDiskWritesStage = (ManualResetEvent) null;
    this.sidecarUploadFilesStage = (ManualResetEvent) null;
  }

  private void UpdateScheduler()
  {
    CooperativeScheduler scheduler = this.driver.SchedulerStages.Scheduler;
    this.sidecarStartStage = new ManualResetEvent(scheduler);
    this.sidecarRelationsStage = new ManualResetEvent(scheduler);
    this.sidecarDiskWritesStage = new ManualResetEvent(scheduler);
    this.sidecarUploadFilesStage = new ManualResetEvent(scheduler);
    scheduler.AppendCheckpointAfter(this.driver.SchedulerStages.UploadFilesStage, this.sidecarStartStage);
    scheduler.AppendCheckpointAfter(this.sidecarStartStage, this.sidecarRelationsStage);
    scheduler.AppendCheckpointAfter(this.sidecarRelationsStage, this.sidecarDiskWritesStage);
    scheduler.AppendCheckpointAfter(this.sidecarDiskWritesStage, this.sidecarUploadFilesStage);
  }

  public virtual bool IsSourceDocument(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    return true;
  }

  private void CheckIfSourceDocument(SectionEntity documentEntity)
  {
    if (!this.IsSourceDocument(documentEntity))
      throw new InvalidOperationException($"The {DisplaySection.GetDisplayName(documentEntity)} is not a source document for the {this.GetType()} extension.");
  }

  private void CheckIfSourceDocuments(IEnumerable<SectionEntity> documentEntities)
  {
    foreach (SectionEntity documentEntity in documentEntities)
      this.CheckIfSourceDocument(documentEntity);
  }

  public ICollection<Tuple<SectionEntity, long>> FindExisting(IList<SectionEntity> documentEntities)
  {
    if (documentEntities == null)
      throw new ArgumentNullException(nameof (documentEntities));
    if (this.EnableSanityChecks && documentEntities.Count != 0)
      this.CheckIfSourceDocuments((IEnumerable<SectionEntity>) documentEntities);
    return (ICollection<Tuple<SectionEntity, long>>) this.sidecarOperations.FindMany<SectionEntity>(documentEntities, new Func<SectionEntity, long>(ObjectSection.GetObjectId));
  }

  public bool CanCreate(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (this.EnableSanityChecks)
      this.CheckIfSourceDocument(documentEntity);
    return this.CanCreateNewSidecarObject(documentEntity);
  }

  protected virtual bool CanCreateNewSidecarObject(SectionEntity documentEntity) => true;

  public void Create(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (this.EnableSanityChecks)
      this.CheckIfSourceDocument(documentEntity);
    SectionEntity sidecarEntity = this.CreateSidecarEntity(documentEntity, objectType: new int?(this.sidecarIDCache.SidecarObjectType.Id));
    documentEntity.Database.Add((IEntity) sidecarEntity);
    this.ScheduleSidecarObjectHandler(documentEntity, sidecarEntity);
  }

  public void Update(SectionEntity documentEntity, long sidecarObjectId)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (Consts.IsUndefinedObjectId(sidecarObjectId))
      throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (sidecarObjectId));
    if (this.EnableSanityChecks)
      this.CheckIfSourceDocument(documentEntity);
    SectionEntity sidecarEntity = this.CreateSidecarEntity(documentEntity, new long?(sidecarObjectId), new int?(this.sidecarIDCache.SidecarObjectType.Id));
    documentEntity.Database.Add((IEntity) sidecarEntity);
    this.ScheduleSidecarObjectHandler(documentEntity, sidecarEntity);
  }

  internal SectionEntity CreateSidecarEntity(
    SectionEntity documentEntity,
    long? objectId = null,
    int? objectType = null)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    if (this.EnableSanityChecks)
      this.CheckIfSourceDocument(documentEntity);
    if (!objectType.HasValue && objectId.HasValue)
      objectType = new int?(DBHelper.GetObjectType(objectId.Value));
    long objectId1 = ObjectSection.GetObjectId(documentEntity);
    ObjectSection sectionObject1 = new ObjectSection();
    if (objectId.HasValue)
      sectionObject1.ObjectId = objectId.Value;
    if (objectType.HasValue)
      sectionObject1.ObjectType = objectType.Value;
    sectionObject1.ExistenceStatus = objectId.HasValue ? ObjectExistenceStatus.ExistingObject : ObjectExistenceStatus.NewObject;
    DisplaySection sectionObject2 = new DisplaySection()
    {
      DisplayName = $"{this.sidecarIDCache.SidecarInstanceName} для документа #{objectId1}"
    };
    sectionObject2.QualifiedName = sectionObject2.DisplayName;
    FilesSection sectionObject3 = new FilesSection();
    sectionObject3.MasterFile = string.Empty;
    FilesProcessingOptionsSection sectionObject4 = new FilesProcessingOptionsSection();
    sectionObject4.EnableFilesProcessing = false;
    sectionObject4.EnableDependenciesProcessing = false;
    ProxyDocumentSection sectionObject5 = new ProxyDocumentSection();
    SidecarObjectSection sectionObject6 = new SidecarObjectSection();
    sectionObject6.SourceDocumentId = Math.Abs(objectId1);
    ObjectActionsSection sectionObject7 = new ObjectActionsSection();
    SectionEntity sidecarEntity = new SectionEntity();
    sidecarEntity.Sections.Set((object) sectionObject1);
    sidecarEntity.Sections.Set((object) sectionObject2);
    sidecarEntity.Sections.Set((object) sectionObject3);
    sidecarEntity.Sections.Set((object) sectionObject4);
    sidecarEntity.Sections.Set((object) sectionObject5);
    sidecarEntity.Sections.Set((object) sectionObject6);
    sidecarEntity.Sections.Set((object) sectionObject7);
    return sidecarEntity;
  }

  private void ScheduleSidecarObjectHandler(
    SectionEntity documentEntity,
    SectionEntity sidecarEntity)
  {
    SidecarObjectHandler waitTarget = new SidecarObjectHandler(this.driver, this.driver.Operations.DriverContext, sidecarEntity, documentEntity, this);
    waitTarget.ScheduleAdapter = DocumentScheduleAdapter.FromStandardScheduler(this.driver.SchedulerStages);
    waitTarget.ScheduleAdapter.RelationsStage = this.sidecarRelationsStage;
    waitTarget.ScheduleAdapter.DiskWritesStage = this.sidecarDiskWritesStage;
    waitTarget.ScheduleAdapter.UploadFilesStage = this.sidecarUploadFilesStage;
    this.sidecarStartStage.Wait((IAction) waitTarget);
  }

  /// <summary>
  /// Возвращает путь к выделенной папке для генерации ассоциированных файлов.
  /// </summary>
  /// <returns>Абсолютный путь к папке</returns>
  protected internal abstract string GetSidecarFilesBaseDirectory();

  protected internal abstract SidecarObjectUpdateMode GetSidecarObjectUpdateMode(
    SectionEntity documentEntity);

  protected internal abstract IAction TryCreateBlankSidecarObjectAction(
    SectionEntity documentEntity,
    SectionEntity sidecarEntity);

  protected internal abstract SidecarFileResult TryCreateOrUpdateSidecarFile(
    SectionEntity documentEntity,
    string documentBaseDirectory);

  protected internal abstract string TryCreateSidecarObjectCaption(
    long documentId,
    int documentType,
    ValueBag documentAttributeBag,
    IEnumerable<StringKey> identityAttributeNames);

  protected internal abstract string CreateErrorWhenSidecarFileUpdateFailed(
    SectionEntity documentEntity);
}
