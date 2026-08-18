// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.MenuCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.CADInterface.Proxies;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Services.IMViewer;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class MenuCommandsProvider : ICommandsProvider
{
  private IIntegratorRegistry integratorRegistry;
  private IIMViewerObjectCreatorService imviewerService;
  private MenuCommandsFlags imviewerControlFlags;
  private IExceptionDisplayService exceptionService;
  private IOutputView outputViewService;

  public MenuCommandsProvider(
    IIntegratorRegistry integratorRegistry,
    IIMViewerObjectCreatorService imviewerService,
    MenuCommandsFlags imviewerControlFlags,
    IExceptionDisplayService exceptionService,
    IOutputView outputViewService)
  {
    this.integratorRegistry = integratorRegistry;
    this.imviewerService = imviewerService;
    this.imviewerControlFlags = imviewerControlFlags;
    this.exceptionService = exceptionService;
    this.outputViewService = outputViewService;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    if (!this.imviewerService.Settings.EnableIntegration || items.Count == 0 || !this.CanUpdateIMFiles(items, viewServices))
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(MenuConsts.UpdateIMVFilesCommandName, new CommandInfo(0, new ClickEventHandler(this.UpdateIMVFilesCommandHandler)));
    groupCommands.Add(MenuConsts.UpdateIMVFilesRecursiveCommandName, new CommandInfo(0, new ClickEventHandler(this.UpdateIMVFilesRecursiveCommandHandler)));
    return groupCommands;
  }

  private bool CanUpdateIMFiles(ISelectedItems items, System.IServiceProvider viewServices)
  {
    IntegratorObject integratorObject1 = (IntegratorObject) null;
    int num = 0;
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      if (itemData != null && this.imviewerService.CanHaveViewerObject(itemData.ObjectType))
      {
        IntegratorObject integratorObject2 = IntegratorServices.Find(itemData.ObjectType);
        if (integratorObject2 != null)
        {
          if (integratorObject1 == null)
            integratorObject1 = integratorObject2;
          if (!(integratorObject2.Id != integratorObject1.Id))
            ++num;
        }
      }
    }
    return items.Count == num;
  }

  private void UpdateIMVFilesCommandHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IntegratorObject integrator = IntegratorServices.Find(((IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID))).ObjectType);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) IntegratorServices.GetService<ICADInterfaceService>(integrator, true)))
    {
      CADSystemProxy cadSystem = cadApiSession.Application;
      ApplicationVisualState<CADSystemProxy> savedVisualState = cadSystem.SaveVisualState(CADSystemVisualStateFlags.ActiveDocument);
      ProgressSinks.DialogService.Invoke(MenuConsts.UpdateIMVFilesDisplayName, ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
      {
        IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, items.Count);
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
          this.UpdateIMVFilesWithExceptionHandling(itemData.ObjectID, itemData.ObjectType, editorRule, cadSystem);
          progressUpdater.AddCompletedTasks(1);
          if (progressSink.IsCancelled)
            break;
        }
      }));
      if (savedVisualState == null)
        return;
      cadSystem.RestoreVisualState(savedVisualState);
    }
  }

  private void UpdateIMVFilesWithExceptionHandling(
    long documentId,
    int documentTypeId,
    VersionsRulePackage editorRule,
    CADSystemProxy cadSystem)
  {
    IList<ErrorInfo> updateViewerObject = this.imviewerService.CreateOrUpdateViewerObject(documentId, documentTypeId, editorRule, cadSystem, this.imviewerControlFlags.PreOpenDocumentsMode);
    if (updateViewerObject.Count == 0 || updateViewerObject[0].Exception == null)
      return;
    this.exceptionService.ShowException(updateViewerObject[0].Exception);
  }

  private void UpdateIMVFilesRecursiveCommandHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    IntegratorObject integrator = IntegratorServices.Find(((IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID))).ObjectType);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) IntegratorServices.GetService<ICADInterfaceService>(integrator, true)))
    {
      CADSystemProxy cadSystem = cadApiSession.Application;
      ApplicationVisualState<CADSystemProxy> savedVisualState = cadSystem.SaveVisualState(CADSystemVisualStateFlags.ActiveDocument);
      List<ErrorInfo> errors = new List<ErrorInfo>();
      ProgressSinks.DialogService.Invoke(MenuConsts.UpdateIMVFilesRecursiveDisplayName, ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
      {
        IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, items.Count);
        for (int index = 0; index < items.Count; ++index)
        {
          IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
          errors.AddRange((IEnumerable<ErrorInfo>) this.imviewerService.CreateOrUpdateViewerObjectsRecursive(itemData.ObjectID, itemData.ObjectType, editorRule, cadSystem, this.imviewerControlFlags.PreOpenDocumentsMode));
          progressUpdater.AddCompletedTasks(1);
          if (progressSink.IsCancelled)
            break;
        }
      }));
      if (savedVisualState != null)
        cadSystem.RestoreVisualState(savedVisualState);
      if (errors.Count == 0)
        return;
      new ErrorReporterAdapter((IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(this.outputViewService, "Вывод")))
      {
        CaptionGenerator = ((Func<ICollection<ErrorInfo>, string>) (errorList => $"Отчет о выполнении команды \"{MenuConsts.UpdateIMVFilesRecursiveDisplayName}\""))
      }.ReportErrors((ICollection<ErrorInfo>) errors);
      int num = (int) MessageBox.Show("При обновлении файлов IMViewer возникли ошибки. Подробности смотрите в окне \"Вывод\".", MenuConsts.UpdateIMVFilesRecursiveDisplayName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }
}
