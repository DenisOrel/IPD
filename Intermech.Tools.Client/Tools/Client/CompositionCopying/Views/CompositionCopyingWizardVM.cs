// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CompositionCopyingWizardVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Model.Operations;
using Intermech.UI.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class CompositionCopyingWizardVM : WizardVM
{
  private CopyingSession session;
  private ICompositionCopyingWizardServices wizardServices;

  public CompositionCopyingWizardVM(
    CopyingSession session,
    ICompositionCopyingWizardServices wizardServices)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (wizardServices == null)
      throw new ArgumentNullException(nameof (wizardServices));
    this.session = session;
    this.wizardServices = wizardServices;
    SelectDocumentsPageVM selectDocumentsPageVm = new SelectDocumentsPageVM(session);
    PrescanDocumentsPageVM prescanDocumentsPageVm = new PrescanDocumentsPageVM(session);
    CopyFilesPageVM copyFilesPageVm = new CopyFilesPageVM(session);
    CopyDocumentsPageVM copyDocumentsPageVm = new CopyDocumentsPageVM(session);
    AttributesEditPageVM attributesEditPageVm = new AttributesEditPageVM(session);
    UserWorkPageVM userWorkPageVm = new UserWorkPageVM(session);
    this.Pages = (IReadOnlyList<WizardPageVM>) new WizardPageVM[6]
    {
      (WizardPageVM) selectDocumentsPageVm,
      (WizardPageVM) prescanDocumentsPageVm,
      (WizardPageVM) attributesEditPageVm,
      (WizardPageVM) copyFilesPageVm,
      (WizardPageVM) copyDocumentsPageVm,
      (WizardPageVM) userWorkPageVm
    };
  }

  [Conditional("DEBUG")]
  private void EnableSanityChecks(CopyingSession session)
  {
    DBObjectGraph sessionGraph = session.Graph;
    FullModelConsistencyValidator consistencyValidator = new FullModelConsistencyValidator();
    this.CurrentPageChanging += (EventHandler<WizardPageChangingEventArgs>) ((sender, e) =>
    {
      if (e.NextPage == null)
        return;
      ValidationServices.ValidateObject<DBObjectGraph>(sessionGraph, (IObjectValidator<DBObjectGraph>) consistencyValidator);
    });
  }

  protected override void DoValidateNavigation(WizardPageNavigationEventArgs e)
  {
    base.DoValidateNavigation(e);
    if (e.NavigationType == WizardPageNavigationType.Cancel && !this.wizardServices.Dialogs.AskYesNo("Работа мастера не завершена. Вы действительно хотите прервать работу мастера?"))
    {
      e.Cancel = true;
    }
    else
    {
      if (!(this.CurrentPage is IBackgroundOperationOwner currentPage) || !currentPage.HasRunningOperation())
        return;
      this.wizardServices.Dialogs.DisplayWarning("На текущей странице мастера есть работающая фоновая операция. Необходимо сначала прервать эту операцию.");
      e.Cancel = true;
    }
  }

  protected override void DoCurrentPageChanged(
    WizardPageNavigationType navigationType,
    WizardPageVM previousPage,
    WizardPageVM nextPage)
  {
    base.DoCurrentPageChanged(navigationType, previousPage, nextPage);
    if (navigationType == WizardPageNavigationType.Finish)
    {
      this.UpdateUIAfterCompletion();
    }
    else
    {
      if (navigationType != WizardPageNavigationType.Cancel)
        return;
      this.CleanupAfterCancellation();
    }
  }

  private void UpdateUIAfterCompletion()
  {
    DocumentTrait trait;
    ICollection<DBObjectGraphVertex> allVertices = this.session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.TryGetTrait<DocumentTrait>(out trait) && trait.IsDBCopied));
    if (allVertices.Count == 0)
      return;
    this.session.Services.NotificationService.FireEvent((object) null, (NotificationEventArgs) new CreatedExternallyEventArgs("ObjectsCreated", (IList<long>) allVertices.Select<DBObjectGraphVertex, long>((Func<DBObjectGraphVertex, long>) (x => x.GetTrait<DocumentTrait>().DBCopyInfo.ObjectId)).ToArray<long>(), (IList<int>) allVertices.Select<DBObjectGraphVertex, int>((Func<DBObjectGraphVertex, int>) (x => x.GetTrait<DocumentTrait>().DBCopyInfo.ObjectTypeId)).ToArray<int>()));
  }

  private void CleanupAfterCancellation() => new CleanupFullStateOperation().Invoke(this.session);
}
