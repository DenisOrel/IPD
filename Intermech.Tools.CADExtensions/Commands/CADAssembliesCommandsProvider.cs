// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.Commands.CADAssembliesCommandsProvider
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.CADExtensions.Commands;

internal sealed class CADAssembliesCommandsProvider : ICommandsProvider
{
  private Lazy<IWorkCopyCommandOptions> workCopyCommandOptions;
  private Lazy<IFileVault> fileVaultService;
  private Lazy<IOutputView> outputViewService;

  public CADAssembliesCommandsProvider(
    Lazy<IOutputView> outputViewService,
    Lazy<IFileVault> fileVaultService,
    Lazy<IWorkCopyCommandOptions> workCopyCommandOptions)
  {
    if (outputViewService == null)
      throw new ArgumentNullException(nameof (outputViewService));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (workCopyCommandOptions == null)
      throw new ArgumentNullException(nameof (workCopyCommandOptions));
    this.outputViewService = outputViewService;
    this.fileVaultService = fileVaultService;
    this.workCopyCommandOptions = workCopyCommandOptions;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    CommandsInfo result = new CommandsInfo();
    this.TryAddSaveChangesAndCheckInSubtree(items, viewServices, result);
    return result;
  }

  private void TryAddSaveChangesAndCheckInSubtree(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    CommandsInfo result)
  {
    ViewStateFlags viewStateFlags = viewServices.GetService(typeof (IViewState)) is IViewState service ? service.ViewState : ViewStateFlags.None;
    if (items.Count != 1 || (viewStateFlags & ViewStateFlags.ReadOnly) != ViewStateFlags.None || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData) || itemData.Value > 0L)
      return;
    result.Add(CADAssembliesCommandsConsts.SaveChangesSubtreeCommandName, new CommandInfo(4, new ClickEventHandler(this.SaveChangesSubtreeHandler)));
    result.Add(CADAssembliesCommandsConsts.CheckInSubtreeCommandName, new CommandInfo(4, new ClickEventHandler(this.CheckInSubtreeHandler)));
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (viewServices == null)
      throw new ArgumentNullException(nameof (viewServices));
    return CommandsInfo.Empty;
  }

  private void SaveChangesSubtreeHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ServiceContainer contextServices = new ServiceContainer();
    contextServices.AddService(typeof (ObjectCommandsOptionsHolder), (object) new ObjectCommandsOptionsHolder(ObjectCommandsOptions.NonInteractive));
    if (MessageBox.Show("Сохранить изменения в ветке, начиная с указанного объекта?", CADAssembliesCommandsConsts.SaveChangesSubtreeDisplayName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    List<DBObjectState> objectWithChildren = this.GetObjectWithChildrenStates((items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value);
    List<ErrorInfo> errorList = new List<ErrorInfo>(objectWithChildren.Count);
    ProgressSinks.DialogService.Invoke(CADAssembliesCommandsConsts.SaveChangesSubtreeDisplayName, ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
    {
      IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, objectWithChildren.Count);
      for (int index = 0; index < objectWithChildren.Count; ++index)
      {
        DBObjectState dbObjectState = objectWithChildren[index];
        progressUpdater.ProgressSink.SetState(dbObjectState.Caption);
        if (DBHelper.IsObjectAlive(dbObjectState.ObjectId) && dbObjectState.IsEditableState)
        {
          ObjectCommand saveChangesCommand = ObjectCommandFactory.CreateSaveChangesCommand(true);
          saveChangesCommand.ObjectId = dbObjectState.ObjectId;
          saveChangesCommand.ContextServices = (System.IServiceProvider) contextServices;
          try
          {
            saveChangesCommand.Execute();
          }
          catch (Exception ex)
          {
            errorList.Add(this.ErrorInfoFromObjectCommandException(ex, dbObjectState));
          }
        }
        progressUpdater.AddCompletedTasks(1);
        if (progressSink.IsCancelled)
          break;
      }
    }));
    if (errorList.Count == 0)
      return;
    this.ShowErrorList(errorList, CADAssembliesCommandsConsts.SaveChangesSubtreeDisplayName);
  }

  private void CheckInSubtreeHandler(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    ServiceContainer contextServices = new ServiceContainer();
    contextServices.AddService(typeof (ObjectCommandsOptionsHolder), (object) new ObjectCommandsOptionsHolder(ObjectCommandsOptions.NonInteractive));
    List<WorkCopyCommandOptionsEditor> commandOptionsEditorList = new List<WorkCopyCommandOptionsEditor>();
    this.workCopyCommandOptions.Value.GetCheckinOptions(items, (IServiceContainer) contextServices, commandOptionsEditorList);
    if (new CheckinObjectsForm((IServiceContainer) contextServices, CADAssembliesCommandsConsts.CheckInSubtreeDisplayName, "Завершить редактирование в ветке, начиная с указанного объекта?", "Объекты", (ICollection<WorkCopyCommandOptionsEditor>) commandOptionsEditorList)
    {
      ShowPreserveWorkingCopiesBox = true
    }.ShowDialog() != DialogResult.Yes)
      return;
    List<DBObjectState> objectWithChildren = this.GetObjectWithChildrenStates((items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value);
    List<ErrorInfo> errorList = new List<ErrorInfo>(objectWithChildren.Count);
    ProgressSinks.DialogService.Invoke(CADAssembliesCommandsConsts.CheckInSubtreeDisplayName, ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink =>
    {
      IProgressUpdater progressUpdater = ProgressSinks.CreateProgressUpdater(progressSink, objectWithChildren.Count);
      for (int index = 0; index < objectWithChildren.Count; ++index)
      {
        DBObjectState dbObjectState = objectWithChildren[index];
        progressUpdater.ProgressSink.SetState(dbObjectState.Caption);
        if (DBHelper.IsObjectAlive(dbObjectState.ObjectId) && dbObjectState.IsEditableState)
        {
          ObjectCopyCommand checkinCommand = ObjectCommandFactory.CreateCheckinCommand(true);
          checkinCommand.ObjectId = dbObjectState.ObjectId;
          checkinCommand.ContextServices = (System.IServiceProvider) contextServices;
          try
          {
            checkinCommand.Execute();
          }
          catch (Exception ex)
          {
            errorList.Add(this.ErrorInfoFromObjectCommandException(ex, dbObjectState));
          }
        }
        progressUpdater.AddCompletedTasks(1);
        if (progressSink.IsCancelled)
          break;
      }
    }));
    if (errorList.Count == 0)
      return;
    this.ShowErrorList(errorList, CADAssembliesCommandsConsts.CheckInSubtreeDisplayName);
  }

  private List<DBObjectState> GetObjectWithChildrenStates(long rootObjectId)
  {
    return this.fileVaultService.Value.DBObjectsInfo.CreateStateListForObjectTree(rootObjectId, VersionsRuleSources.GetEditorRule());
  }

  private ErrorInfo ErrorInfoFromObjectCommandException(
    Exception exception,
    DBObjectState dbObjectState)
  {
    return ErrorInfo.FromException(exception, $"Команде не удалось обработать объект IPS '{dbObjectState.Caption}' (ид. версии = {dbObjectState.ObjectId})");
  }

  private void ShowErrorList(List<ErrorInfo> errorList, string commandDisplayName)
  {
    new ErrorReporterAdapter((IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(this.outputViewService.Value, "Ошибки")))
    {
      CaptionGenerator = ((Func<ICollection<ErrorInfo>, string>) (errors => $"При выполнении команды '{commandDisplayName}' произошли ошибки."))
    }.ReportErrors((ICollection<ErrorInfo>) errorList);
    int num = (int) MessageBox.Show($"Команда завершена с ошибками. Подробные сведения о них вы можете найти в окне 'Вывод' на странице '{"Ошибки"}'.", commandDisplayName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }
}
