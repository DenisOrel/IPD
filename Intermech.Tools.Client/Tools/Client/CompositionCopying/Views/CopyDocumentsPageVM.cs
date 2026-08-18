// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CopyDocumentsPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
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

internal sealed class CopyDocumentsPageVM : WizardPageVM, IBackgroundOperationOwner
{
  private readonly WizardPageOperationErrorsVM pageErrors;
  private readonly CopyingSession session;
  private readonly BackgroundOperationVM<CopyDocumentsPageVM.CopyDocumentsContext> copyDocumentsOperation;
  private ObservableCollection<string> copyDocumentLog;
  private string pageDescription;
  private ICollection<DBObjectGraphVertex> documentsToCopy;
  private CopyingSessionProcessingStep newProcessingStep;

  public CopyDocumentsPageVM()
    : base("Копирование атрибутов документов")
  {
    BackgroundOperationDescriptor<CopyDocumentsPageVM.CopyDocumentsContext> descriptor = new BackgroundOperationDescriptor<CopyDocumentsPageVM.CopyDocumentsContext>()
    {
      OnCreateOperationContext = new Func<CopyDocumentsPageVM.CopyDocumentsContext>(this.CreateCopyDocumentsContext),
      OnRunInBackground = new RunBackgroundOperation<CopyDocumentsPageVM.CopyDocumentsContext>(CopyDocumentsPageVM.RunCopyDocumentsInBackground),
      OnResult = new ProcessBackgroundOperationResult<CopyDocumentsPageVM.CopyDocumentsContext>(this.ApplyCopyDocumentsResult)
    };
    descriptor.Freeze();
    this.copyDocumentsOperation = new BackgroundOperationVM<CopyDocumentsPageVM.CopyDocumentsContext>(descriptor);
    this.copyDocumentsOperation.Starting += new EventHandler(this.OnStartingCopyDocuments);
    this.copyDocumentLog = new ObservableCollection<string>();
    this.pageErrors = new WizardPageOperationErrorsVM();
  }

  public CopyDocumentsPageVM(CopyingSession session)
    : this()
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this.pageErrors.SetCopyingSession(session);
  }

  public WizardPageOperationErrorsVM PageErrors
  {
    [DebuggerStepThrough] get => this.pageErrors;
  }

  public IBackgroundOperation CopyDocumentsOperation
  {
    [DebuggerStepThrough] get => (IBackgroundOperation) this.copyDocumentsOperation;
  }

  public ObservableCollection<string> CopyDocumentLog
  {
    [DebuggerStepThrough] get => this.copyDocumentLog;
  }

  public string PageDescription
  {
    [DebuggerStepThrough] get => this.pageDescription;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!(this.pageDescription != value))
        return;
      this.pageDescription = value;
      this.RaisePropertyChanged("pageDescription");
    }
  }

  bool IBackgroundOperationOwner.HasRunningOperation() => this.copyDocumentsOperation.IsRunning;

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this.session == null)
      return;
    DocumentTrait trait;
    this.documentsToCopy = this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.CopyingSelector.IsSelected && x.IsScanned && x.TryGetTrait<DocumentTrait>(out trait) && trait.IsLocalFilesCopied && !trait.IsDBCopied));
    this.UpdateOperationControlState();
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    if (this.session == null)
      return;
    if (this.copyDocumentsOperation.IsRunning)
      this.copyDocumentsOperation.CancelNoWait();
    this.copyDocumentsOperation.SwitchCommands(false);
    if (this.newProcessingStep != null)
    {
      this.session.ProcessingHistory.Update(this.newProcessingStep);
      this.newProcessingStep = (CopyingSessionProcessingStep) null;
    }
    this.documentsToCopy = (ICollection<DBObjectGraphVertex>) null;
  }

  private void UpdateOperationControlState()
  {
    bool flag = this.ValidateIsCompleted();
    if (!flag && this.IsCompleted)
    {
      this.pageErrors.Items.Clear();
      this.copyDocumentLog.Clear();
    }
    this.IsCompleted = flag;
    this.PageDescription = !this.IsCompleted ? $"Сейчас мастер выполнит копирование атрибутов для {this.documentsToCopy.Count} документов в базе данных, а также загрузит в базу данных скопированные файлы документов с диска. За ходом копирования вы можете наблюдать в журнале операции" : "Документы скопированы.";
    this.copyDocumentsOperation.SwitchCommands(!this.IsCompleted);
  }

  private bool ValidateIsCompleted()
  {
    DocumentTrait trait;
    return this.documentsToCopy.All<DBObjectGraphVertex>((Func<DBObjectGraphVertex, bool>) (x => x.TryGetTrait<DocumentTrait>(out trait) && trait.IsDBCopied));
  }

  private void OnStartingCopyDocuments(object sender, EventArgs e) => this.CopyDocumentLog.Clear();

  private CopyDocumentsPageVM.CopyDocumentsContext CreateCopyDocumentsContext()
  {
    return new CopyDocumentsPageVM.CopyDocumentsContext(this, this.session, (ICollection<DBObjectGraphVertex>) new List<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) this.documentsToCopy));
  }

  private static void RunCopyDocumentsInBackground(
    CopyDocumentsPageVM.CopyDocumentsContext operationContext)
  {
    CopyDBObjectsOperation objectsOperation1 = new CopyDBObjectsOperation();
    objectsOperation1.CancellationPredicate = (Func<bool>) (() => operationContext.CancellationPending);
    CopyDBObjectsOperation objectsOperation2 = objectsOperation1;
    objectsOperation2.ProgressAction = objectsOperation2.ProgressAction + new Action<int>(((BackgroundOperationContext) operationContext).ReportProgress);
    CopyDBObjectsOperation objectsOperation3 = objectsOperation1;
    objectsOperation3.LogAction = objectsOperation3.LogAction + new Action<string>(operationContext.AddToCopyDocumentLog);
    objectsOperation1.Invoke(operationContext.Session, operationContext.DocumentsToCopy);
    if (objectsOperation1.Result.Count != 0)
      operationContext.DocumentCopies.AddRange<KeyValuePair<DBObjectGraphVertex, DBObjectRecord>>((IEnumerable<KeyValuePair<DBObjectGraphVertex, DBObjectRecord>>) objectsOperation1.Result);
    if (objectsOperation1.Errors.Count != 0)
      operationContext.Errors.AddRange((IEnumerable<OperationError>) objectsOperation1.Errors);
    if (objectsOperation1.CopyDocumentsUserWork.Count == 0)
      return;
    operationContext.CopyDocumentsUserWork.AddRange((IEnumerable<UserWorkItem>) objectsOperation1.CopyDocumentsUserWork);
  }

  private void ApplyCopyDocumentsResult(
    CopyDocumentsPageVM.CopyDocumentsContext operationContext,
    bool isCancelled,
    Exception error)
  {
    if (!isCancelled && error == null && (operationContext.Errors.Count == 0 || operationContext.Errors.All<OperationError>((Func<OperationError, bool>) (x => x.IsWarning))))
    {
      foreach (KeyValuePair<DBObjectGraphVertex, DBObjectRecord> documentCopy in operationContext.DocumentCopies)
      {
        DBObjectGraphVertex key = documentCopy.Key;
        DBObjectRecord dbCopyInfo = documentCopy.Value;
        key.GetTrait<DocumentTrait>().SetDBCopyInfo(dbCopyInfo);
      }
      if (operationContext.CopyDocumentsUserWork.Count != 0)
        this.session.UserWorkItems.AddRange((IEnumerable<UserWorkItem>) operationContext.CopyDocumentsUserWork);
      this.session.DeferredEventDispatcher.RaiseAll();
    }
    this.pageErrors.Items.Clear();
    if (isCancelled)
      this.copyDocumentLog.Add("Процесс копирования прерван...");
    if (operationContext.Errors.Count != 0)
      this.pageErrors.Items.AddRange<OperationError>((IEnumerable<OperationError>) operationContext.Errors);
    if (error != null)
      this.pageErrors.Items.Add(new OperationError(error.Message));
    this.newProcessingStep = new CopyingSessionProcessingStep("CopyDocuments");
    this.UpdateOperationControlState();
  }

  private sealed class CopyDocumentsContext : BackgroundOperationContext
  {
    private readonly CopyDocumentsPageVM pageVM;

    public CopyDocumentsContext(
      CopyDocumentsPageVM pageVM,
      CopyingSession session,
      ICollection<DBObjectGraphVertex> documentsToCopy)
    {
      this.pageVM = pageVM;
      this.Session = session;
      this.DocumentsToCopy = documentsToCopy;
      this.DocumentCopies = new Dictionary<DBObjectGraphVertex, DBObjectRecord>();
      this.Errors = new List<OperationError>();
      this.CopyDocumentsUserWork = new List<UserWorkItem>(0);
    }

    public CopyingSession Session { get; }

    public ICollection<DBObjectGraphVertex> DocumentsToCopy { get; }

    public Dictionary<DBObjectGraphVertex, DBObjectRecord> DocumentCopies { get; }

    public List<OperationError> Errors { get; }

    public List<UserWorkItem> CopyDocumentsUserWork { get; }

    public void AddToCopyDocumentLog(string message)
    {
      if (message == null)
        return;
      this.PostToUIThread((Action) (() => this.pageVM.CopyDocumentLog.Add(message)));
    }
  }
}
