// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CopyFilesPageVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class CopyFilesPageVM : WizardPageVM, IBackgroundOperationOwner
{
  private readonly CopyingSession session;
  private BackgroundOperationVM<CopyFilesPageVM.CopyCADFilesContext> copyCADFilesOperation;
  private readonly WizardPageOperationErrorsVM pageErrors;
  private ObservableCollection<string> copyLog;
  private ICollection<DBObjectGraphVertex> documentsToCopy;
  private CopyingSessionProcessingStep newProcessingStep;

  public CopyFilesPageVM()
    : base("Копирование файлов документов")
  {
    BackgroundOperationDescriptor<CopyFilesPageVM.CopyCADFilesContext> descriptor = new BackgroundOperationDescriptor<CopyFilesPageVM.CopyCADFilesContext>()
    {
      OnCreateOperationContext = new Func<CopyFilesPageVM.CopyCADFilesContext>(this.CreateCopyCADFilesContext),
      OnRunInBackground = new RunBackgroundOperation<CopyFilesPageVM.CopyCADFilesContext>(CopyFilesPageVM.RunCopyCADFilesInBackground),
      OnResult = new ProcessBackgroundOperationResult<CopyFilesPageVM.CopyCADFilesContext>(this.ApplyCopyCADFilesResult)
    };
    descriptor.Freeze();
    this.copyCADFilesOperation = new BackgroundOperationVM<CopyFilesPageVM.CopyCADFilesContext>(descriptor);
    this.copyCADFilesOperation.Starting += new EventHandler(this.OnStartingCopyCADFiles);
    this.copyLog = new ObservableCollection<string>();
    this.pageErrors = new WizardPageOperationErrorsVM();
  }

  public CopyFilesPageVM(CopyingSession session)
    : this()
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
    this.pageErrors.SetCopyingSession(session);
  }

  public WizardPageOperationErrorsVM PageErrors
  {
    [DebuggerStepThrough] get => this.pageErrors;
  }

  public ObservableCollection<string> CopyLog
  {
    [DebuggerStepThrough] get => this.copyLog;
  }

  public IBackgroundOperation CopyCADFilesOperation
  {
    [DebuggerStepThrough] get => (IBackgroundOperation) this.copyCADFilesOperation;
  }

  bool IBackgroundOperationOwner.HasRunningOperation() => this.copyCADFilesOperation.IsRunning;

  protected override void DoActivate(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage)
  {
    base.DoActivate(navigationType, previousPage);
    if (this.session == null)
      return;
    DocumentTrait trait;
    this.documentsToCopy = this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.CopyingSelector.IsSelected && x.IsScanned && x.TryGetTrait<DocumentTrait>(out trait) && !trait.IsLocalFilesCopied));
    this.UpdateOperationControlState();
  }

  protected override void DoDeactivate(
    WizardPageNavigationType navigationType,
    WizardPageVM nextPage)
  {
    base.DoDeactivate(navigationType, nextPage);
    if (this.session == null)
      return;
    if (this.copyCADFilesOperation.IsRunning)
      this.copyCADFilesOperation.CancelNoWait();
    this.copyCADFilesOperation.SwitchCommands(false);
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
      this.copyLog.Clear();
    }
    this.IsCompleted = flag;
    this.copyCADFilesOperation.SwitchCommands(!this.IsCompleted);
  }

  private bool ValidateIsCompleted()
  {
    DocumentTrait trait;
    return this.documentsToCopy.All<DBObjectGraphVertex>((Func<DBObjectGraphVertex, bool>) (x => x.TryGetTrait<DocumentTrait>(out trait) && trait.IsLocalFilesCopied));
  }

  private void OnStartingCopyCADFiles(object sender, EventArgs e) => this.CopyLog.Clear();

  private CopyFilesPageVM.CopyCADFilesContext CreateCopyCADFilesContext()
  {
    return new CopyFilesPageVM.CopyCADFilesContext(this, this.session, (ICollection<DBObjectGraphVertex>) new List<DBObjectGraphVertex>((IEnumerable<DBObjectGraphVertex>) this.documentsToCopy));
  }

  private static void RunCopyCADFilesInBackground(
    CopyFilesPageVM.CopyCADFilesContext operationContext)
  {
    Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyCADFilesOperation cadFilesOperation1 = new Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyCADFilesOperation();
    cadFilesOperation1.CancellationPredicate = (Func<bool>) (() => operationContext.CancellationPending);
    Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyCADFilesOperation cadFilesOperation2 = cadFilesOperation1;
    cadFilesOperation2.ProgressAction = cadFilesOperation2.ProgressAction + new Action<int>(((BackgroundOperationContext) operationContext).ReportProgress);
    Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyCADFilesOperation cadFilesOperation3 = cadFilesOperation1;
    cadFilesOperation3.LogAction = cadFilesOperation3.LogAction + new Action<string>(operationContext.AddToCopyDocumentLog);
    cadFilesOperation1.Invoke(operationContext.Session, operationContext.DocumentsToCopy);
    if (cadFilesOperation1.Errors.Count == 0)
      return;
    operationContext.Errors.AddRange((IEnumerable<OperationError>) cadFilesOperation1.Errors);
  }

  private void ApplyCopyCADFilesResult(
    CopyFilesPageVM.CopyCADFilesContext operationContext,
    bool isCancelled,
    Exception error)
  {
    if (!isCancelled && error == null && (operationContext.Errors.Count == 0 || operationContext.Errors.All<OperationError>((Func<OperationError, bool>) (x => x.IsWarning))))
    {
      foreach (IDBObjectGraphTraitOwner vertex in (IEnumerable<DBObjectGraphVertex>) operationContext.DocumentsToCopy)
        vertex.GetTrait<DocumentTrait>().SetLocalFilesCopied();
      this.session.DeferredEventDispatcher.RaiseAll();
    }
    this.pageErrors.Items.Clear();
    if (isCancelled)
      this.copyLog.Add("Процесс копирования прерван...");
    if (operationContext.Errors.Count != 0)
      this.pageErrors.Items.AddRange<OperationError>((IEnumerable<OperationError>) operationContext.Errors);
    if (error != null)
      this.pageErrors.Items.Add(new OperationError(error.Message));
    this.newProcessingStep = new CopyingSessionProcessingStep("CopyLocalFiles");
    this.UpdateOperationControlState();
  }

  private class CopyCADFilesContext : BackgroundOperationContext
  {
    private CopyFilesPageVM pageVM;

    public CopyCADFilesContext(
      CopyFilesPageVM pageVM,
      CopyingSession session,
      ICollection<DBObjectGraphVertex> documentsToCopy)
    {
      this.pageVM = pageVM;
      this.Session = session;
      this.DocumentsToCopy = documentsToCopy;
      this.Errors = new List<OperationError>();
    }

    public CopyingSession Session { get; }

    public ICollection<DBObjectGraphVertex> DocumentsToCopy { get; }

    public List<OperationError> Errors { get; }

    public void AddToCopyDocumentLog(string message)
    {
      if (message == null)
        return;
      this.PostToUIThread((Action) (() => this.pageVM.CopyLog.Add(message)));
    }
  }
}
