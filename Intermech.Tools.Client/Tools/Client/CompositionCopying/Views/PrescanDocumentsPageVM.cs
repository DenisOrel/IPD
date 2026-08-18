// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.PrescanDocumentsPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Files;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Model.Operations;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class PrescanDocumentsPageVM : WizardPageVM, IBackgroundOperationOwner
{
  private readonly BackgroundOperationVM<PrescanDocumentsPageVM.ScanContext> scanOperation;
  private readonly ObservableCollection<string> scanLog;
  private readonly WizardPageOperationErrorsVM pageErrors;
  private readonly CopyingSession session;
  private bool scanRequired;
  private string scanRequiredDescription;
  private ICollection<DBObjectGraphVertex> verticesToCopy;
  private ICollection<DBObjectGraphVertex> verticesToScan;
  private bool isFilesPublished;
  private CopyingSessionProcessingStep newProcessingStep;

  public PrescanDocumentsPageVM()
    : base("Предварительный анализ")
  {
    BackgroundOperationDescriptor<PrescanDocumentsPageVM.ScanContext> descriptor = new BackgroundOperationDescriptor<PrescanDocumentsPageVM.ScanContext>()
    {
      OnCreateOperationContext = new Func<PrescanDocumentsPageVM.ScanContext>(this.CreateScanContext),
      OnRunInBackground = new RunBackgroundOperation<PrescanDocumentsPageVM.ScanContext>(PrescanDocumentsPageVM.RunScanInBackground),
      OnResult = new ProcessBackgroundOperationResult<PrescanDocumentsPageVM.ScanContext>(this.ApplyScanResult)
    };
    descriptor.Freeze();
    this.scanOperation = new BackgroundOperationVM<PrescanDocumentsPageVM.ScanContext>(descriptor);
    this.scanOperation.Starting += new EventHandler(this.OnScanStarting);
    this.scanRequired = false;
    this.scanRequiredDescription = string.Empty;
    this.scanLog = new ObservableCollection<string>();
    this.pageErrors = new WizardPageOperationErrorsVM();
  }

  public PrescanDocumentsPageVM(CopyingSession session)
    : this()
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this.pageErrors.SetCopyingSession(session);
  }

  public bool ScanRequired
  {
    [DebuggerStepThrough] get => this.scanRequired;
    set
    {
      if (this.scanRequired == value)
        return;
      this.scanRequired = value;
      this.RaisePropertyChanged(nameof (ScanRequired));
    }
  }

  public string ScanRequiredDescription
  {
    [DebuggerStepThrough] get => this.scanRequiredDescription;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!(this.scanRequiredDescription != value))
        return;
      this.scanRequiredDescription = value;
      this.RaisePropertyChanged(nameof (ScanRequiredDescription));
    }
  }

  public IBackgroundOperation ScanOperation
  {
    [DebuggerStepThrough] get => (IBackgroundOperation) this.scanOperation;
  }

  public ObservableCollection<string> ScanLog
  {
    [DebuggerStepThrough] get => this.scanLog;
  }

  public WizardPageOperationErrorsVM PageErrors
  {
    [DebuggerStepThrough] get => this.pageErrors;
  }

  bool IBackgroundOperationOwner.HasRunningOperation() => this.scanOperation.IsRunning;

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this.session == null)
      return;
    this.verticesToCopy = this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument() && x.CopyingSelector.IsSelected));
    this.UpdateScanRequiredState();
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    if (this.session == null)
      return;
    if (this.scanOperation.IsRunning)
      this.scanOperation.CancelNoWait();
    this.scanOperation.SwitchCommands(false);
    if (this.newProcessingStep != null)
    {
      this.session.ProcessingHistory.Update(this.newProcessingStep);
      this.newProcessingStep = (CopyingSessionProcessingStep) null;
    }
    if (this.verticesToScan != null)
      this.verticesToScan = (ICollection<DBObjectGraphVertex>) null;
    if (this.verticesToCopy == null)
      return;
    this.verticesToCopy = (ICollection<DBObjectGraphVertex>) null;
  }

  private void UpdateScanRequiredState()
  {
    this.verticesToScan = (ICollection<DBObjectGraphVertex>) this.verticesToCopy.Where<DBObjectGraphVertex>((Func<DBObjectGraphVertex, bool>) (x => !x.IsScanned)).ToHashSet<DBObjectGraphVertex>();
    this.ScanRequired = this.verticesToScan.Count != 0;
    this.ScanRequiredDescription = this.ScanRequired ? $"Для дальнейшей работы мастера требуется выполнить сканирование {this.verticesToScan.Count} документов из {this.verticesToCopy.Count} выбранных для копирования. Для этого мастер выполнит обращения к серверу приложений и к CAD-системе. За ходом сканирования вы можете наблюдать в журнале операции" : "Все необходимые сведения о документах уже собраны. Можно переходить на следующую страницу мастера";
    this.IsCompleted = this.ValidateIsCompleted();
    this.scanOperation.SwitchCommands(!this.IsCompleted);
  }

  private bool ValidateIsCompleted()
  {
    return this.verticesToCopy.All<DBObjectGraphVertex>((Func<DBObjectGraphVertex, bool>) (x => x.IsScanned)) && this.pageErrors.IsEmpty;
  }

  private void OnScanStarting(object sender, EventArgs e) => this.ScanLog.Clear();

  private PrescanDocumentsPageVM.ScanContext CreateScanContext()
  {
    return new PrescanDocumentsPageVM.ScanContext(this, new PrescanDocumentsPageVM.ScanParameters(this.session, this.verticesToScan))
    {
      IsFilesPublished = this.isFilesPublished
    };
  }

  private static void RunScanInBackground(
    PrescanDocumentsPageVM.ScanContext operationContext)
  {
    PrescanDocumentsPageVM.ScanParameters parameters = operationContext.Parameters;
    IFileVault fileVaultService = parameters.Session.Services.FileVaultService;
    if (!operationContext.IsFilesPublished)
    {
      ICollection<DBObjectGraphVertex> allVertices = parameters.Session.Graph.GetAllVertices();
      List<DBObjectState> objectList = new List<DBObjectState>();
      foreach (DBObjectGraphVertex objectGraphVertex in (IEnumerable<DBObjectGraphVertex>) allVertices)
      {
        DBObjectState objectState = fileVaultService.DBObjectsInfo.GetObjectState(objectGraphVertex.ObjectId, false);
        if (objectState != null)
          objectList.Add(objectState);
      }
      fileVaultService.WorkArea.Publish((IList<DBObjectState>) objectList, (IReplaceFilePolicy) new PreserveAnyChanges());
      operationContext.IsFilesPublished = true;
    }
    PrescanDBObjectsOperation objectsOperation1 = new PrescanDBObjectsOperation();
    objectsOperation1.CancellationPredicate = (Func<bool>) (() => operationContext.CancellationPending);
    PrescanDBObjectsOperation objectsOperation2 = objectsOperation1;
    objectsOperation2.ProgressAction = objectsOperation2.ProgressAction + (Action<int>) (value => operationContext.ReportProgress(value / 2));
    PrescanDBObjectsOperation objectsOperation3 = objectsOperation1;
    objectsOperation3.LogAction = objectsOperation3.LogAction + (Action<string>) (value => operationContext.AddToScanLog(value));
    objectsOperation1.Invoke(parameters.Session, parameters.Vertices);
    PrescanFilesOperation prescanFilesOperation1 = new PrescanFilesOperation();
    prescanFilesOperation1.CancellationPredicate = (Func<bool>) (() => operationContext.CancellationPending);
    PrescanFilesOperation prescanFilesOperation2 = prescanFilesOperation1;
    prescanFilesOperation2.ProgressAction = prescanFilesOperation2.ProgressAction + (Action<int>) (value => operationContext.ReportProgress(50 + value / 2));
    PrescanFilesOperation prescanFilesOperation3 = prescanFilesOperation1;
    prescanFilesOperation3.LogAction = prescanFilesOperation3.LogAction + (Action<string>) (value => operationContext.AddToScanLog(value));
    prescanFilesOperation1.Invoke(parameters.Session, (ICollection<PrescanDBObjectRecord>) objectsOperation1.Result);
    foreach (PrescanDBObjectRecord prescanDbObjectRecord in objectsOperation1.Result)
    {
      if (prescanFilesOperation1.Result.Contains(prescanDbObjectRecord.DBObjectVertex))
        operationContext.Result.Add(prescanDbObjectRecord);
    }
    if (objectsOperation1.Errors.Count != 0)
      operationContext.Errors.AddRange((IEnumerable<OperationError>) objectsOperation1.Errors);
    if (prescanFilesOperation1.Errors.Count == 0)
      return;
    operationContext.Errors.AddRange((IEnumerable<OperationError>) prescanFilesOperation1.Errors);
  }

  private void ApplyScanResult(
    PrescanDocumentsPageVM.ScanContext operationContext,
    bool isCancelled,
    Exception error)
  {
    if (!this.isFilesPublished && operationContext.IsFilesPublished)
      this.isFilesPublished = true;
    if (!isCancelled && operationContext.Result.Count != 0)
    {
      foreach (PrescanDBObjectRecord prescanDbObjectRecord in operationContext.Result)
      {
        DBObjectGraphVertex dbObjectVertex = prescanDbObjectRecord.DBObjectVertex;
        dbObjectVertex.Attributes.Clear();
        dbObjectVertex.Attributes.AddRange<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) prescanDbObjectRecord.Attributes);
        dbObjectVertex.Files.Clear();
        dbObjectVertex.Files.AddRange<DBObjectFileEntry>((IEnumerable<DBObjectFileEntry>) prescanDbObjectRecord.Files);
        dbObjectVertex.Content = prescanDbObjectRecord.Content;
        dbObjectVertex.IsScanned = true;
      }
      this.session.DeferredEventDispatcher.RaiseAll();
      ValidationServices.ValidateObject<DBObjectGraph>(this.session.Graph, (IObjectValidator<DBObjectGraph>) new ScannedVerticesValidator());
    }
    this.pageErrors.Items.Clear();
    if (operationContext.Errors.Count != 0)
      this.pageErrors.Items.AddRange<OperationError>((IEnumerable<OperationError>) operationContext.Errors);
    if (error != null)
      this.pageErrors.Items.Add(new OperationError(error.Message));
    this.newProcessingStep = new CopyingSessionProcessingStep("PrescanDocuments");
    this.UpdateScanRequiredState();
  }

  private sealed class ScanParameters
  {
    public ScanParameters(CopyingSession session, ICollection<DBObjectGraphVertex> vertices)
    {
      this.Session = session;
      this.Vertices = new List<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) vertices);
    }

    public CopyingSession Session { get; }

    public List<DBObjectGraphVertex> Vertices { get; }
  }

  private sealed class ScanContext : BackgroundOperationContext
  {
    public ScanContext(
      PrescanDocumentsPageVM page,
      PrescanDocumentsPageVM.ScanParameters parameters)
    {
      this.Page = page;
      this.Parameters = parameters;
      this.Result = new List<PrescanDBObjectRecord>();
      this.Errors = new List<OperationError>();
    }

    public PrescanDocumentsPageVM Page { get; }

    public PrescanDocumentsPageVM.ScanParameters Parameters { get; }

    public List<PrescanDBObjectRecord> Result { get; }

    public List<OperationError> Errors { get; }

    public bool IsFilesPublished { get; set; }

    public void AddToScanLog(string message)
    {
      if (message == null)
        return;
      this.PostToUIThread((Action) (() => this.Page.ScanLog.Add(message)));
    }
  }
}
