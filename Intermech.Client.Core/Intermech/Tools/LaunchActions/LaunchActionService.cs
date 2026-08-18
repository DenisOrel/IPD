
// Type: Intermech.Tools.LaunchActions.LaunchActionService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Redline;
using Intermech.Tools.CommonTasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

internal sealed class LaunchActionService : ILaunchActionService
{
  private readonly IFileVault fileVault;
  private readonly IExternalRedliningEditorService redliningEditorService;
  private readonly DocumentFilesTaskFactory documentFilesTaskFactory;
  private List<ILaunchHandler> handlers;
  private static readonly XmlDocument EmptyHandlerData = new XmlDocument();

  public LaunchActionService(
    IFileVault fileVault,
    IExternalRedliningEditorService redliningEditorService,
    DocumentFilesTaskFactory documentFilesTaskFactory)
  {
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    if (redliningEditorService == null)
      throw new ArgumentNullException(nameof (redliningEditorService));
    if (documentFilesTaskFactory == null)
      throw new ArgumentNullException(nameof (documentFilesTaskFactory));
    this.fileVault = fileVault;
    this.redliningEditorService = redliningEditorService;
    this.documentFilesTaskFactory = documentFilesTaskFactory;
    this.handlers = new List<ILaunchHandler>(32 /*0x20*/);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void Launch(LaunchParams launchParams)
  {
    LaunchActionService.DynamicLaunchInfo dynamicLaunchInfo = launchParams != null ? new LaunchActionService.DynamicLaunchInfo(launchParams, LaunchActionService.EmptyHandlerData) : throw new ArgumentNullException(nameof (launchParams));
    foreach (ILaunchHandler handler in this.handlers)
    {
      if (handler is IDynamicLaunchHandler dynamicLaunchHandler)
        dynamicLaunchHandler.Lookup((IDynamicLaunchInfo) dynamicLaunchInfo);
    }
    if (dynamicLaunchInfo.Handler != null)
    {
      if (!this.handlers.Contains(dynamicLaunchInfo.Handler))
        throw new InvalidOperationException("Указан незарегистрированный обработчик для команды запуска приложения.");
      this.Launch(launchParams, dynamicLaunchInfo.Handler, dynamicLaunchInfo.HandlerData);
    }
    else
    {
      LaunchActionInfo definedLaunchAction = this.GetUserDefinedLaunchAction(launchParams);
      if (definedLaunchAction != null)
        this.Launch(launchParams, definedLaunchAction);
      else
        this.LaunchByShell(launchParams);
    }
  }

  private LaunchActionInfo GetUserDefinedLaunchAction(LaunchParams launchParams)
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Guid guid = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(launchParams.ObjectTypeId).GUID;
      ITarget target = (ITarget) new UserTarget(service.UserID, service.UserGuid);
      return ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).LookupDefaultAction(guid, target, launchParams.LaunchType);
    }
  }

  public void Launch(LaunchParams launchParams, LaunchActionInfo actionInfo)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    ILaunchHandler handler = actionInfo != null ? this.GetLaunchHandler(actionInfo) : throw new ArgumentNullException(nameof (actionInfo));
    XmlDocument handlerData = this.ReadLaunchActionData(actionInfo);
    this.Launch(launchParams, handler, handlerData);
  }

  private void Launch(LaunchParams launchParams, ILaunchHandler handler, XmlDocument handlerData)
  {
    handler.BeforeLaunch(launchParams, handlerData);
    this.PrepareObjectForLaunchAction(launchParams);
    handler.Launch(launchParams, handlerData);
    this.redliningEditorService.ReportFileOpenAction(launchParams.ObjectId, launchParams.LaunchType == LaunchType.View);
  }

  private void PrepareObjectForLaunchAction(LaunchParams launchParams)
  {
    ObjectModifyModes? nullable = new ObjectModifyModes?();
    if (launchParams.ObjectId > 0L && launchParams.NeedCheckout)
    {
      nullable = new ObjectModifyModes?(this.GetObjectModifyMode(launchParams.ObjectId));
      if (nullable.Value != ObjectModifyModes.InBase)
      {
        ObjectCopyCommand checkoutCommand = ObjectCommandFactory.CreateCheckoutCommand(true);
        checkoutCommand.ObjectId = launchParams.ObjectId;
        checkoutCommand.Execute();
        launchParams.ChangeObject(checkoutCommand.NewObjectId, launchParams.OriginalObjectTypeId);
        nullable = new ObjectModifyModes?(ObjectModifyModes.InBase);
      }
    }
    if (launchParams.LaunchType != LaunchType.Edit || (launchParams.ObjectId < 0L || nullable.HasValue && nullable.Value == ObjectModifyModes.InBase ? 1 : (this.GetObjectModifyMode(launchParams.ObjectId) == ObjectModifyModes.InBase ? 1 : 0)) == 0)
      return;
    this.CheckAccessForLaunchEditor(launchParams.ObjectId);
  }

  private ObjectModifyModes GetObjectModifyMode(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectId, true).ObjectModifyMode;
  }

  private void CheckAccessForLaunchEditor(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectId, true).Edit();
  }

  private ILaunchHandler GetLaunchHandler(LaunchActionInfo actionInfo)
  {
    return this.handlers.Find((Predicate<ILaunchHandler>) (handler => handler.Id == actionInfo.HandlerId)) ?? throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1594"), (object) actionInfo.DisplayName));
  }

  private XmlDocument ReadLaunchActionData(LaunchActionInfo actionInfo)
  {
    try
    {
      XmlDocument xmlDocument = new XmlDocument();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ILaunchActionServer service = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true);
        xmlDocument.LoadXml(service.GetActionData(actionInfo.ActionId));
      }
      return xmlDocument;
    }
    catch (XmlException ex)
    {
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1595"), (object) actionInfo.DisplayName), (Exception) ex);
    }
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void LaunchByShell(LaunchParams launchParams)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    this.PrepareObjectForLaunchAction(launchParams);
    if (string.IsNullOrEmpty(launchParams.ObjectFileName))
      launchParams.ObjectFileName = this.fileVault.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true);
    if (launchParams.FileArea == null)
      launchParams.FileArea = launchParams.LaunchType == LaunchType.Edit ? (IFileArea) this.fileVault.WorkArea : (IFileArea) this.fileVault.ViewArea;
    bool rootObjectOnly = LaunchActionServiceVars.RootObjectMode.Value;
    launchParams.ResultFilePath = this.PublishFiles(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.VersionsRule, launchParams.FileArea, rootObjectOnly);
    this.AfterPublishFileHandler((object) this, new LaunchHandlerEventArgs(launchParams));
    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.UseShellExecute = true;
    startInfo.FileName = launchParams.ResultFilePath;
    startInfo.Verb = "open";
    try
    {
      Process.Start(startInfo)?.Dispose();
    }
    catch (Win32Exception ex)
    {
      throw new FaultException(ex.Message, (Exception) ex);
    }
    this.redliningEditorService.ReportFileOpenAction(launchParams.ObjectId, launchParams.LaunchType == LaunchType.View);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public ILaunchHandler GetHandler(Guid handlerId, bool throwIfNotFound)
  {
    if (handlerId == Guid.Empty)
      throw new ArgumentException();
    ILaunchHandler launchHandler = this.handlers.Find((Predicate<ILaunchHandler>) (handler => handler.Id == handlerId));
    return !(launchHandler == null & throwIfNotFound) ? launchHandler : throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1315"), (object) handlerId));
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<ILaunchHandler> GetHandlers() => this.handlers.GetRange(0, this.handlers.Count);

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void RegisterHandler(ILaunchHandler handler)
  {
    if (handler == null)
      throw new ArgumentNullException();
    this.handlers.Add(handler);
    if (!(handler is ILaunchHandlerFileEvents handlerFileEvents))
      return;
    handlerFileEvents.AfterPublishFile += new EventHandler<LaunchHandlerEventArgs>(this.AfterPublishFileHandler);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void UnregisterHandler(ILaunchHandler handler)
  {
    if (handler == null)
      throw new ArgumentNullException();
    this.handlers.Remove(handler);
    if (!(handler is ILaunchHandlerFileEvents handlerFileEvents))
      return;
    handlerFileEvents.AfterPublishFile -= new EventHandler<LaunchHandlerEventArgs>(this.AfterPublishFileHandler);
  }

  private void AfterPublishFileHandler(object sender, LaunchHandlerEventArgs e)
  {
    this.AlterPublishedFile(e.LaunchParams);
  }

  private void AlterPublishedFile(LaunchParams launchParams)
  {
    if (launchParams.LaunchType != LaunchType.View && launchParams.LaunchType != LaunchType.Print)
      return;
    string resultFilePath = launchParams.ResultFilePath;
    if (string.IsNullOrEmpty(resultFilePath) || !Path.IsPathRooted(resultFilePath))
      return;
    InjectStandaloneViewDataTask standaloneViewDataTask = this.documentFilesTaskFactory.InjectStandaloneViewData();
    standaloneViewDataTask.Initialize(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.ResultFilePath);
    if (!standaloneViewDataTask.CanPerform)
      return;
    standaloneViewDataTask.Perform();
  }

  private string PublishFiles(
    long objectId,
    string fileName,
    VersionsRulePackage versionsRule,
    IFileArea fileArea,
    bool rootObjectOnly)
  {
    List<DBObjectState> objectList = rootObjectOnly ? this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(objectId) : this.fileVault.DBObjectsInfo.CreateStateListForObjectTree(objectId, versionsRule);
    string path;
    if (fileArea == this.fileVault.WorkArea)
    {
      this.fileVault.WorkArea.Publish((IList<DBObjectState>) objectList, (IReplaceFilePolicy) new PreserveAnyChanges());
      path = Path.Combine(this.fileVault.WorkArea.AreaPath, fileName);
    }
    else
      path = this.fileVault.ViewArea.Publish((IList<DBObjectState>) objectList).ObjectFiles.Find((Predicate<PublishedFile>) (file => PathUtils.IsSamePath(file.FileState.FileName, fileName)))?.FullName;
    return path != null && File.Exists(path) ? path : throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1292"), (object) fileName, (object) objectId));
  }

  private sealed class DynamicLaunchInfo : IDynamicLaunchInfo
  {
    public DynamicLaunchInfo(LaunchParams launchParams, XmlDocument handlerData)
    {
      this.LaunchParams = launchParams;
      this.HandlerData = handlerData;
    }

    /// <summary>Возвращает описание параметров запуска приложения</summary>
    public LaunchParams LaunchParams { get; private set; }

    /// <summary>
    /// Возвращает конфигурацию для динамически подключаемого обработчика.
    /// Значение свойства содержит пустой xml-документ, так как у таких обработчиков не может быть
    /// декларативно заданной конфигурации.
    /// </summary>
    public XmlDocument HandlerData { get; private set; }

    /// <summary>
    /// Возвращает или задает объект динамически подключаемого обработчика.
    /// Исходное значение свойства равно null.
    /// </summary>
    public ILaunchHandler Handler { get; set; }
  }
}
