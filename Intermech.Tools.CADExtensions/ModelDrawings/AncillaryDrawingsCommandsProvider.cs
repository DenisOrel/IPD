// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.CADExtensions.ModelDrawings.AncillaryDrawingsCommandsProvider
// Assembly: Intermech.Tools.CADExtensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35CC158B-C7AB-4543-B377-24CF4B98BDA2
// Assembly location: D:\IPS\Client\Intermech.Tools.CADExtensions.dll

using Intermech.CADInterface.Proxies;
using Intermech.Commands;
using Intermech.ControlFlow;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Redline;
using Intermech.Tools.CADExtensions.Properties;
using Intermech.Tools.CommonTasks;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.CADExtensions.ModelDrawings;

internal sealed class AncillaryDrawingsCommandsProvider : ICommandsProvider
{
  private ICurrentUserAndRole currentUserService;
  private IFileVault fileVaultService;
  private IIntegratorRegistry integratorRegistry;
  private IExternalRedliningEditorService externalRedliningEditorService;
  private DocumentFilesTaskFactory documentFilesTaskFactory;

  public AncillaryDrawingsCommandsProvider(
    ICurrentUserAndRole currentUserService,
    IFileVault fileVaultService,
    IIntegratorRegistry integratorRegistry,
    IExternalRedliningEditorService externalRedliningEditorService,
    DocumentFilesTaskFactory documentFilesTaskFactory)
  {
    this.currentUserService = currentUserService;
    this.fileVaultService = fileVaultService;
    this.integratorRegistry = integratorRegistry;
    this.externalRedliningEditorService = externalRedliningEditorService;
    this.documentFilesTaskFactory = documentFilesTaskFactory;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (!(viewServices.GetService(typeof (IViewState)) is IViewState service) || (service.ViewState & ViewStateFlags.ReadOnly) != ViewStateFlags.None || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    if (items.GetItemData(0, typeof (IDBCheckedOutByID)) is IDBCheckedOutByID itemData && (itemData.CheckedOutBy == this.currentUserService.UserID || itemData.CheckedOutBy == 0L))
      groupCommands.Add("EditModelDrawing", new CommandInfo(0, new ClickEventHandler(this.EditModelDrawing)));
    groupCommands.Add("ShowModelDrawing", new CommandInfo(0, new ClickEventHandler(this.ShowModelDrawing)));
    groupCommands.Add("ShowModelDrawingWithOptions", new CommandInfo(0, new ClickEventHandler(this.ShowModelDrawingWithOptions)));
    groupCommands.Add("CreateModelDrawingAuthenticFile", new CommandInfo(0, new ClickEventHandler(this.CreateModelDrawingAuthenticFile)));
    return groupCommands;
  }

  private void CreateModelDrawingAuthenticFile(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    Tuple<IDBTypedObjectID, IntegratorObject, string> tuple = this.TrySelectModelDrawing(items, "Выберите файл чертежа для создания аутентичного файла");
    if (tuple == null)
      return;
    IDBTypedObjectID dbTypedObjectId = tuple.Item1;
    string objectFileName = tuple.Item3;
    MakeAuthenticFileTask authenticFileTask = this.documentFilesTaskFactory.MakeAuthenticFile();
    authenticFileTask.Initialize(dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType, (string) null, objectFileName);
    if (!authenticFileTask.CanPerform)
      return;
    authenticFileTask.Perform();
  }

  private void ShowModelDrawing(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.OpenModelDrawing(items, false);
  }

  private void ShowModelDrawingWithOptions(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    using (new DynamicScope())
    {
      StandaloneViewVars.IsActive.Declare(true);
      StandaloneViewVars.AdjustSettingsInDialogMode.Declare(true);
      this.OpenModelDrawing(items, false);
    }
  }

  private void EditModelDrawing(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.OpenModelDrawing(items, true);
  }

  private void OpenModelDrawing(ISelectedItems items, bool isEdit)
  {
    Tuple<IDBTypedObjectID, IntegratorObject, string> tuple = this.TrySelectModelDrawing(items, "Выберите файл чертежа для открытия");
    if (tuple == null)
      return;
    IDBTypedObjectID dbTypedObjectId = tuple.Item1;
    IntegratorObject integratorObj = tuple.Item2;
    string modelDrawingFile = tuple.Item3;
    this.OpenFile(isEdit, dbTypedObjectId.ObjectID, modelDrawingFile, integratorObj);
  }

  private void OpenFile(
    bool isEdit,
    long objectID,
    string modelDrawingFile,
    IntegratorObject integratorObj)
  {
    if (isEdit)
    {
      ObjectCopyCommand checkoutCommand = ObjectCommandFactory.CreateCheckoutCommand(true);
      checkoutCommand.ObjectId = objectID;
      checkoutCommand.Execute();
      objectID = checkoutCommand.NewObjectId;
    }
    string str = this.fileVaultService.PublishTree(objectID, modelDrawingFile, VersionsRuleSources.GetEditorRule(), (IFileArea) this.fileVaultService.WorkArea);
    if (!isEdit)
    {
      InjectStandaloneViewDataTask standaloneViewDataTask = this.documentFilesTaskFactory.InjectStandaloneViewData();
      standaloneViewDataTask.Initialize(objectID, modelDrawingFile, str);
      if (standaloneViewDataTask.CanPerform)
        standaloneViewDataTask.Perform();
    }
    using (new DynamicScope())
    {
      IntegratorVars.ConserveAppResources.Declare(false);
      using (CADApiSession cadApiSession = new CADApiSession(this.integratorRegistry.GetIntegrator(integratorObj, true)))
      {
        CADSystemProxy application = cadApiSession.Application;
        CADDocumentProxy cadDocumentProxy = application.OpenDocument(str, true);
        application.SwitchToApp();
        cadDocumentProxy.Activate();
      }
    }
    this.externalRedliningEditorService.ReportFileOpenAction(objectID, !isEdit);
  }

  private Tuple<IDBTypedObjectID, IntegratorObject, string> TrySelectModelDrawing(
    ISelectedItems items,
    string dialogCaption)
  {
    if (!(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return (Tuple<IDBTypedObjectID, IntegratorObject, string>) null;
    IntegratorObject integrator = IntegratorServices.Find(itemData.ObjectType);
    List<string> fileNames = this.fileVaultService.DBFilesInfo.GetFileNames(itemData.ObjectID);
    fileNames.Remove(this.fileVaultService.DBFilesInfo.GetMasterFileName(itemData.ObjectID, true));
    if (fileNames.Count == 0)
      throw new FaultException(Resources.ModelHasNoDrawingFile);
    IModelDrawingsService modelDrawingsService = IntegratorServices.GetService<IModelDrawingsService>(integrator, true);
    List<string> list = fileNames.Where<string>((Func<string, bool>) (t => modelDrawingsService.IsDrawingFileName(t))).ToList<string>();
    if (list.Count == 0)
      throw new FaultException(Resources.ModelHasNoDrawingFile);
    string str;
    if (list.Count == 1)
    {
      str = list[0];
    }
    else
    {
      using (SelectModelDrawingDialog modelDrawingDialog = new SelectModelDrawingDialog(list))
      {
        modelDrawingDialog.Text = dialogCaption;
        int num = (int) modelDrawingDialog.ShowDialog();
        str = !string.IsNullOrEmpty(modelDrawingDialog.SelectedModelDrawingFile) ? modelDrawingDialog.SelectedModelDrawingFile : throw new AbortException();
      }
    }
    return Tuple.Create<IDBTypedObjectID, IntegratorObject, string>(itemData, integrator, str);
  }
}
