// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.CaptureChangesManager
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Реализует менеждер по захвату и передаче в IPS изменений, сделанных в файлах объекта с помощью приложения-редактора.
/// </summary>
public sealed class CaptureChangesManager
{
  private ICaptureChangesDriver driver;
  private IReplaceFilePolicy workAreaPolicy;
  private bool? keepCheckedOut;
  private const string commandScopeId = "CaptureChanges";

  /// <summary>
  /// Возвращает или задает стратегию захвата изменений.
  /// Значение свойства должно быть задано до начала использования менеджера.
  /// </summary>
  public ICaptureChangesDriver Driver
  {
    get => this.driver;
    set => this.driver = value;
  }

  /// <summary>
  /// Позволяет задать политику замены файлов в рабочей области при извлечении файлов обрабатываемого объекта IPS.
  /// Если значение этого свойства установлено в null, то используется стандартная политика.
  /// </summary>
  public IReplaceFilePolicy WorkAreaPolicy
  {
    get => this.workAreaPolicy;
    set => this.workAreaPolicy = value;
  }

  /// <summary>
  /// Возвращает или устанавливает флаг, управляющий взятием на изменение импортируемых объектов.
  /// Если значение флага равно true, то все импортированные объекты будут взяты на изменение.
  /// </summary>
  public bool KeepCheckedOut
  {
    get
    {
      return !this.keepCheckedOut.HasValue ? UISettings.AutoCheckOutNewObjects : this.keepCheckedOut.Value;
    }
    set => this.keepCheckedOut = new bool?(value);
  }

  /// <summary>
  /// Выполняет захват и передачу в IPS изменений, сделанных в файлах существующего объекта IPS.
  /// </summary>
  /// <param name="parameters">Параметры выполнения операции</param>
  /// <returns>Объект, содержащий результат выполнения. Может быть null, если у объекта нет файлов</returns>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="parameters" /> не должен быть равен null</exception>
  public CaptureChangesResult CaptureChanges(CaptureChangesActionParameters parameters)
  {
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    parameters.ValidateProperties();
    this.ValidateProperties();
    this.PrepareCaptureChangesParameters(parameters);
    using (UIReportScope childScope = UIReport.CreateChildScope())
    {
      using (UIReport.CreateLogicalOperation((object) nameof (CaptureChanges)))
      {
        CaptureChangesReportHelper.TrySetFormattingHandler(childScope);
        IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
        string masterFileName = service.DBFilesInfo.GetMasterFileName(parameters.ObjectId, false);
        if (string.IsNullOrEmpty(masterFileName))
          return (CaptureChangesResult) null;
        using (new DynamicScope())
        {
          VersionsRuleSources.AllowCache.Declare(true);
          service.WorkArea.Publish((IList<Intermech.Files.DBObjectState>) service.DBObjectsInfo.CreateStateListForObjectTree(parameters.ObjectId, VersionsRuleSources.GetEditorRule()), this.workAreaPolicy != null ? this.workAreaPolicy : (IReplaceFilePolicy) new PreserveAnyChanges());
          string str = Path.Combine(service.WorkArea.AreaPath, masterFileName);
          if (!File.Exists(str))
            throw new FileNotFoundException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_407"), (object) str, (object) DBHelper.GetObjectCaption(parameters.ObjectId)), str);
          CaptureChangesDatabase database = new CaptureChangesDatabase();
          this.ConvertToEntryPointDocument(database.AddDocument(str, parameters.ObjectId));
          this.CaptureChangesCore(database, parameters.ProgressSink);
          return new CaptureChangesResult(parameters.ObjectId, str, database);
        }
      }
    }
  }

  private void PrepareCaptureChangesParameters(CaptureChangesActionParameters parameters)
  {
    if (parameters.ProgressSink != null)
      return;
    parameters.ProgressSink = ProgressSinks.NullPercentageSink;
  }

  /// <summary>
  /// Выполняет импорт объекта из файла в рабочей области файлового хранилища.
  /// </summary>
  /// <param name="parameters">Параметры выполнения операции</param>
  /// <returns>Объект, содержащий результат выполнения</returns>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="parameters" /> не должен быть равен null</exception>
  public CaptureChangesResult ImportFile(ImportFileActionParameters parameters)
  {
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    parameters.ValidateProperties();
    this.ValidateProperties();
    this.PrepareImportFileParameters(parameters);
    using (UIReportScope childScope = UIReport.CreateChildScope())
    {
      using (UIReport.CreateLogicalOperation((object) "CaptureChanges"))
      {
        CaptureChangesReportHelper.TrySetFormattingHandler(childScope);
        if (!File.Exists(parameters.FullPath))
          throw new FileNotFoundException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_408"), (object) parameters.FullPath), parameters.FullPath);
        IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
        if (service.WorkArea != service.FindArea(parameters.FullPath))
          throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_419"), (object) parameters.FullPath)).WithRecoveryActions((ErrorRecoveryAction) new OpenFileRecoveryAction(parameters.FullPath));
        CaptureChangesDatabase database = new CaptureChangesDatabase();
        SectionEntity sectionEntity = database.AddDocument(parameters.FullPath);
        this.ConvertToEntryPointDocument(sectionEntity);
        this.CaptureChangesCore(database, parameters.ProgressSink);
        return new CaptureChangesResult(ObjectSection.GetObjectId(sectionEntity), parameters.FullPath, database);
      }
    }
  }

  private void PrepareImportFileParameters(ImportFileActionParameters parameters)
  {
    if (parameters.ProgressSink != null)
      return;
    parameters.ProgressSink = ProgressSinks.NullPercentageSink;
  }

  private void ConvertToEntryPointDocument(SectionEntity documentEntity)
  {
    documentEntity.Sections.Set((object) new RootItemSection(true));
  }

  private UINotificationsBuilder GetOrCreateUINotificationsBuilder()
  {
    UICommandInfo uiCommandInfo = UIVars.UICommand.Value;
    if (uiCommandInfo == null)
    {
      uiCommandInfo = new UICommandInfo("Capture changes");
      UIVars.UICommand.Declare(uiCommandInfo);
    }
    object obj;
    UINotificationsBuilder uiNotifications;
    if (uiCommandInfo.Tags.TryGetValue((object) "UINotifications", out obj))
    {
      uiNotifications = (UINotificationsBuilder) obj;
    }
    else
    {
      uiNotifications = new UINotificationsBuilder();
      uiCommandInfo.Tags.Add((object) "UINotifications", (object) uiNotifications);
      uiCommandInfo.RegisterDisposeAction((Action) (() => this.UpdateUI(uiNotifications)));
    }
    return uiNotifications;
  }

  private void UpdateUI(UINotificationsBuilder builder)
  {
    List<NotificationEventArgs> notificationList = builder.ToNotificationList();
    if (notificationList.Count == 0)
      return;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, true);
    foreach (NotificationEventArgs e in notificationList)
      service.FireEvent((object) null, e);
  }

  private void CaptureChangesCore(
    CaptureChangesDatabase database,
    IPercentageProgressSink progressSink)
  {
    using (new DynamicScope())
    {
      VersionsRuleSources.AllowCache.Declare(true);
      CaptureChangesContext ctx = new CaptureChangesContext(database, this.GetOrCreateUINotificationsBuilder());
      this.Driver.BeginAction();
      try
      {
        try
        {
          CaptureChangesLists driverResults;
          using (UIReport.CreateLogicalOperation((object) "Analysis"))
          {
            progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_421"));
            this.Driver.Invoke(ctx, progressSink.CreateNestedSink(70.0));
            driverResults = this.GetDriverResults(ctx);
            this.ValidateDriverResults(driverResults);
          }
          using (UIReport.CreateLogicalOperation((object) "Applying"))
          {
            progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_422"));
            this.ApplyDriverResults(driverResults, progressSink.CreateNestedSink(30.0));
          }
          progressSink.SetState(string.Empty);
          progressSink.SetProgress(100.0);
        }
        catch
        {
          if (ctx.ServerCleanupActions.Count != 0)
            this.RunServerCleanupSafely((IEnumerable<IAction>) ctx.ServerCleanupActions);
          throw;
        }
        if (UIReport.Enabled)
          CaptureChangesReportHelper.ReportFileUploadSummary();
        this.Driver.Postprocess();
      }
      finally
      {
        if (this.Driver.Active)
          this.Driver.DetachDatabase(ctx.Database);
        this.Driver.EndAction();
      }
    }
  }

  private void RunServerCleanupSafely(IEnumerable<IAction> serverCleanupActions)
  {
    List<IAction> actionList = new List<IAction>(serverCleanupActions);
    actionList.Reverse();
    foreach (IAction action in actionList)
    {
      try
      {
        action.Perform();
      }
      catch (Exception ex)
      {
        SuppressedExceptions.TraceException(ex, "CaptureChangesManager.RunServerCleanupSafely()");
      }
    }
  }

  private CaptureChangesLists GetDriverResults(CaptureChangesContext ctx)
  {
    CaptureChangesLists driverResults = new CaptureChangesLists();
    this.MakeSpecialObjectLists(ctx, driverResults);
    this.MakeServerActions(ctx, driverResults);
    this.MakeClientActions(ctx, driverResults);
    return driverResults;
  }

  private void MakeSpecialObjectLists(CaptureChangesContext ctx, CaptureChangesLists driverResults)
  {
    EntitySet entitySet1 = ctx.Database.Query((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Complement, new IQueryCondition[2]
    {
      (IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
      {
        (IQueryCondition) new BinaryCondition((object) ObjectSection.ExistenceStatusRef, BinaryOperator.Equal, (object) ObjectExistenceStatus.NewObject),
        (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (FilesSection))
      }),
      (IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ProxyDocumentSection))
    }));
    driverResults.ImportedObjects.AddRange<SectionEntity>((IEnumerable<SectionEntity>) new SectionEntityEnumAdapter(entitySet1));
    EntitySet entitySet2 = ctx.Database.Query((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
    {
      (IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Intersection, new IQueryCondition[2]
      {
        (IQueryCondition) new BinaryCondition((object) ObjectActionsSection.RequireCheckoutRef, BinaryOperator.Equal, (object) true),
        (IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectIdRef, BinaryOperator.Greater, (object) 0L)
      }),
      (IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Union, new IQueryCondition[2]
      {
        (IQueryCondition) new BinaryCondition((object) ObjectSection.ExistenceStatusRef, BinaryOperator.Equal, (object) ObjectExistenceStatus.ConvertedObject),
        (IQueryCondition) new BinaryCondition((object) ObjectSection.ExistenceStatusRef, BinaryOperator.Equal, (object) ObjectExistenceStatus.ExistingObject)
      })
    }));
    driverResults.RequireWriteObjects.AddRange<SectionEntity>((IEnumerable<SectionEntity>) new SectionEntityEnumAdapter(entitySet2));
    if (driverResults.RequireWriteObjects.Count <= 0 || !UIReport.Enabled)
      return;
    this.ReportRequireWriteObjects(driverResults);
  }

  private void ReportRequireWriteObjects(CaptureChangesLists driverResults)
  {
    UIReport.ReportEvent($"{LocalizationHolder.rm.GetString("Tools.Components_467")}:");
    UIReport.Indent();
    foreach (SectionEntity requireWriteObject in (IEnumerable<SectionEntity>) driverResults.RequireWriteObjects)
    {
      long objectId = ObjectSection.GetObjectId(requireWriteObject);
      string objectCaption = DBHelper.GetObjectCaption(objectId);
      UIReport.ReportEvent($"#{objectId}, {objectCaption}");
    }
    UIReport.Unindent();
  }

  private void MakeServerActions(CaptureChangesContext ctx, CaptureChangesLists driverResults)
  {
    EntitySet entitySet = ctx.Database.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ObjectActionsSection)));
    foreach (SectionEntity workItem in (HashSet<IEntity>) entitySet)
    {
      foreach (IAction serverAction in workItem.Sections.Get<ObjectActionsSection>().ObjectActions.ServerActions)
        driverResults.ServerActions.Add((IAction) new ExplainErrorAction((IAction) new UIReportActionDecorator(serverAction), workItem));
    }
    foreach (SectionEntity workItem in (HashSet<IEntity>) entitySet)
    {
      foreach (IAction serverAction in workItem.Sections.Get<ObjectActionsSection>().RelationActions.ServerActions)
        driverResults.ServerActions.Add((IAction) new ExplainErrorAction((IAction) new UIReportActionDecorator(serverAction), workItem));
    }
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) ctx.Database.Query((IQueryCondition) new BinaryCondition((object) ObjectSection.ExistenceStatusRef, BinaryOperator.Equal, (object) ObjectExistenceStatus.NewObject)))
    {
      ObjectKeepCheckedOutSection checkedOutSection = sectionEntity.Sections.Get<ObjectKeepCheckedOutSection>((ObjectKeepCheckedOutSection) null);
      CommitBlankObjectAction blankObjectAction = new CommitBlankObjectAction((IUpdateableDBObjectRef) new DBObjectEntityRef(sectionEntity), checkedOutSection != null ? checkedOutSection.KeepCheckedOut : this.KeepCheckedOut);
      driverResults.ServerActions.Add((IAction) new ExplainErrorAction((IAction) new UIReportActionDecorator((IAction) blankObjectAction), sectionEntity));
    }
  }

  private void MakeClientActions(CaptureChangesContext ctx, CaptureChangesLists driverResults)
  {
    EntitySet entitySet = ctx.Database.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (ObjectActionsSection)));
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      ActionQueuePair objectActions = sectionEntity.Sections.Get<ObjectActionsSection>().ObjectActions;
      driverResults.ClientActions.AddRange<IAction>((IEnumerable<IAction>) objectActions.ClientActions);
    }
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      ActionQueuePair relationActions = sectionEntity.Sections.Get<ObjectActionsSection>().RelationActions;
      driverResults.ClientActions.AddRange<IAction>((IEnumerable<IAction>) relationActions.ClientActions);
    }
  }

  private void ValidateDriverResults(CaptureChangesLists driverResults)
  {
    if (driverResults == null)
      throw this.ValidateDriverResultsError(LocalizationHolder.rm.GetString("Tools.Components_423"));
    this.ValidateEntityList(driverResults.RequireWriteObjects, LocalizationHolder.rm.GetString("Tools.Components_425"));
    this.ValidateEntityList(driverResults.ImportedObjects, LocalizationHolder.rm.GetString("Tools.Components_426"));
  }

  private void ValidateEntityList(ICollection<SectionEntity> workItems, string errorText)
  {
    if (workItems.Count <= 0)
      return;
    Dictionary<SectionEntity, object> dictionary = new Dictionary<SectionEntity, object>(workItems.Count);
    foreach (SectionEntity workItem in (IEnumerable<SectionEntity>) workItems)
    {
      if (dictionary.ContainsKey(workItem))
        throw this.ValidateDriverResultsError(errorText);
      dictionary.Add(workItem, (object) null);
    }
  }

  private Exception ValidateDriverResultsError(string errorText)
  {
    throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_427"), (object) errorText));
  }

  private void ApplyDriverResults(
    CaptureChangesLists driverResults,
    IPercentageProgressSink progressSink)
  {
    this.CheckCancelled(progressSink);
    progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_428"));
    if (driverResults.RequireWriteObjects.Count > 0)
      this.CheckoutWriteableObjects(driverResults.RequireWriteObjects);
    progressSink.SetProgress(5.0);
    this.CheckCancelled(progressSink);
    progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_429"));
    this.RunServerActions(driverResults, progressSink.CreateNestedSink(90.0));
    progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_430"));
    this.RunClientActions(driverResults);
    progressSink.SetState(string.Empty);
    progressSink.SetProgress(100.0);
  }

  private void CheckoutWriteableObjects(ICollection<SectionEntity> objItems)
  {
    List<long> objectList = new List<long>(objItems.Count);
    foreach (SectionEntity objItem in (IEnumerable<SectionEntity>) objItems)
      objectList.Add(ObjectSection.GetObjectId(objItem));
    ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true);
    IList<long> longList = DBDocumentHelper.Checkout((IList<long>) objectList, (DBDocumentHelper.CheckoutErrorHandler) null);
    int num1 = 0;
    foreach (SectionEntity objItem in (IEnumerable<SectionEntity>) objItems)
    {
      ObjectSection objectSection = objItem.Sections.Get<ObjectSection>();
      long num2 = longList[num1++];
      if (objectSection.ObjectId != num2)
        objectSection.ObjectId = num2;
    }
  }

  private void RunServerActions(
    CaptureChangesLists driverResults,
    IPercentageProgressSink progressSink)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_434"));
      IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, true);
      service.StartTransaction();
      try
      {
        progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_435"));
        if (driverResults.ServerActions.Count > 0)
        {
          IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink.CreateNestedSink(90.0), driverResults.ServerActions.Count);
          foreach (IAction serverAction in (IEnumerable<IAction>) driverResults.ServerActions)
          {
            this.CheckCancelled(progressSink);
            serverAction.Perform();
            progressUpdater.AddCompletedTasks(1);
          }
        }
        this.CheckCancelled(progressSink);
        progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_436"));
        if (driverResults.ImportedObjects.Count > 0)
          this.PublishImportedObjectsInVault(driverResults.ImportedObjects);
        progressSink.SetProgress(95.0);
        this.CheckCancelled(progressSink);
        progressSink.SetState(LocalizationHolder.rm.GetString("Tools.Components_437"));
        service.Commit();
        progressSink.SetState(string.Empty);
        progressSink.SetProgress(100.0);
      }
      catch
      {
        this.RollbackSafely(service);
        throw;
      }
    }
  }

  private void RollbackSafely(IDBTransactions tx)
  {
    try
    {
      tx.Rollback();
    }
    catch (Exception ex)
    {
      SuppressedExceptions.TraceException(ex, "CaptureChangesManager.RollbackSafely()");
    }
  }

  private void PublishImportedObjectsInVault(ICollection<SectionEntity> importedObjects)
  {
    List<long> objectList = CollectionUtils.ConvertAsList<SectionEntity, long>(importedObjects, (Converter<SectionEntity, long>) (importedItem => ObjectSection.GetObjectId(importedItem)));
    ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).WorkArea.Attach((IList<long>) objectList);
  }

  private void RunClientActions(CaptureChangesLists driverResults)
  {
    IInvokeService service = ServiceUtils.GetService<IInvokeService>((object) ServicesManager.ServiceContainer, true);
    foreach (IAction clientAction in (IEnumerable<IAction>) driverResults.ClientActions)
      service.InvokeAction(-1, new Action(clientAction.Perform));
  }

  private void ValidateProperties()
  {
    if (this.Driver == null)
      throw new InvalidOperationException("Property 'Driver' must not be null.");
  }

  private void CheckCancelled(IPercentageProgressSink progressSink)
  {
    if (progressSink.IsCancelled)
      throw new AbortException("Пользователь прервал выполнение операции.");
  }
}
