// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RedliningComObject
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Signs.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Redline;

[ComVisible(true)]
[Guid("EF6C73FA-A71B-493C-9105-8726EF0EC566")]
[ProgId("IPS.RedliningAPI")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IRedliningAPI))]
[TypeLibGuid("82D46FB7-95B0-429A-A1E6-EA3255C860D5", 3, 0)]
public sealed class RedliningComObject : FreeThreadedObject, IRedliningAPI
{
  private const int UIOperationTimeout = 60000;
  private FileDifferenceCalculator fileDiffCalculator;
  private RedliningWorkflowHelper workflowHelper;
  private object syncRoot;
  private bool isInitialized;
  private string redliningExtension;
  private IStartupService startupService;
  private ICurrentUserAndRole currentUserService;
  private IFileVault fileVaultService;
  private IInvokeService invokerService;
  private IExternalRedliningEditorService redliningEditorService;
  private RedliningIdCache redliningIdCache;
  private Lazy<ISignsClientService> signsService;
  private Lazy<LastViewedDocumentsService> lastViewedDocumentsService;
  private long openDocumentId;

  public RedliningComObject()
  {
    this.syncRoot = new object();
    this.redliningExtension = ".rxml";
    this.openDocumentId = 0L;
    this.fileDiffCalculator = new FileDifferenceCalculator();
    this.workflowHelper = new RedliningWorkflowHelper();
  }

  internal void Initialize(RedliningComObjectServiceLink serviceLink)
  {
    if (serviceLink == null)
      throw new ArgumentNullException(nameof (serviceLink));
    lock (this.syncRoot)
    {
      if (this.isInitialized)
        throw new InvalidOperationException($"Объект '{this.GetType()}' уже был инициализирован.");
      this.startupService = serviceLink.StartupService;
      this.currentUserService = serviceLink.CurrentUserService;
      this.fileVaultService = serviceLink.FileVaultService;
      this.invokerService = serviceLink.InvokerService;
      this.redliningEditorService = serviceLink.RedliningEditorService;
      this.redliningIdCache = serviceLink.RedliningIdCache;
      this.signsService = serviceLink.SignsService;
      this.lastViewedDocumentsService = serviceLink.LastViewedDocumentsService;
      this.isInitialized = true;
    }
  }

  public bool IsInitialized
  {
    get
    {
      lock (this.syncRoot)
        return this.isInitialized;
    }
  }

  private void CheckInitialzied()
  {
    if (!this.isInitialized)
      throw new InvalidOperationException($"Объект '{this.GetType()}' не был инициализирован.");
  }

  public string FileExtension
  {
    get
    {
      lock (this.syncRoot)
      {
        this.CheckInitialzied();
        return this.redliningExtension;
      }
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        throw new ArgumentException("Значение свойства не может быть пустым.", nameof (FileExtension));
      if (value.Length < 2 || !value.StartsWith("."))
        throw new ArgumentException("Значение свойства должно начинаться с точки и содержать как минимум один символ.", nameof (FileExtension));
      lock (this.syncRoot)
      {
        this.CheckInitialzied();
        this.redliningExtension = value;
      }
    }
  }

  public bool IsReady => this.startupService.IsStartupCompleted;

  public long CurrentUserID
  {
    get
    {
      lock (this.syncRoot)
      {
        this.CheckInitialzied();
        return this.IsReady ? this.currentUserService.UserID : 0L;
      }
    }
  }

  public string CurrentUserGuid
  {
    get
    {
      lock (this.syncRoot)
      {
        this.CheckInitialzied();
        return RedliningComObject.GuidToString(this.IsReady ? this.currentUserService.UserGuid : Guid.Empty);
      }
    }
  }

  private static string GuidToString(Guid guid) => guid.ToString("B");

  private static Guid StringToGuid(string guidString) => Guid.Parse(guidString);

  public bool WaitReady(int timeout)
  {
    return DelayedInit.WaitReady((Func<bool>) (() => this.IsReady), timeout);
  }

  private void CheckReady()
  {
    if (!this.IsReady)
      throw new Exception("Инициализация IPS не завершена.");
  }

  public string GetCurrentUserRanks()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      return string.Join(Environment.NewLine, (IEnumerable<string>) CollectionUtils.ConvertAsList<Tuple<string, string>, string>((ICollection<Tuple<string, string>>) this.signsService.Value.GetUserGraphs(0L), (Converter<Tuple<string, string>, string>) (pair => pair.Item2)));
    }
  }

  private void CheckIfUser(IDBObject dbObj)
  {
    if (!DBHelper.IsBasedOnType(dbObj.ObjectType, this.redliningIdCache.UsersType.Id))
      throw new Exception($"Указанный глобальный идентификатор должен соответствовать объекту типа '{this.redliningIdCache.UsersType.Text}'.");
  }

  public long GetUserIDFromGuid(string userGuidAsString)
  {
    Guid objectGUID = !string.IsNullOrEmpty(userGuidAsString) ? RedliningComObject.StringToGuid(userGuidAsString) : throw new ArgumentException("Глобальный идентификатор пользователя не может быть пустым.", "userGuid");
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObj = sessionKeeper.Session.GetObject(objectGUID, false);
        if (dbObj != null)
        {
          this.CheckIfUser(dbObj);
          return dbObj.ObjectID;
        }
      }
      return 0;
    }
  }

  public string GetUserFullName(long userId)
  {
    if (userId == 0L)
      throw new ArgumentException("Идентификатор пользователя не может быть пустым.", nameof (userId));
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObj = sessionKeeper.Session.GetObject(userId, true);
        this.CheckIfUser(dbObj);
        return (dbObj.GetAttributeByID(this.redliningIdCache.UserVisibleName.Id) ?? throw new Exception("У пользователя нет видимого имени.")).AsString;
      }
    }
  }

  public long SelectLastViewedDocument()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      List<long> fileOpenHistory = this.redliningEditorService.GetFileOpenHistory();
      List<long> liveObjectsOnly = DBHelper.GetLiveObjectsOnly((ICollection<long>) fileOpenHistory);
      foreach (long num in fileOpenHistory)
      {
        if (num < 0L && !liveObjectsOnly.Contains(num))
        {
          long objectId = -num;
          if (!liveObjectsOnly.Contains(objectId) && DBHelper.IsObjectAlive(objectId))
            liveObjectsOnly.Add(objectId);
        }
      }
      return liveObjectsOnly.Count == 0 ? -1L : this.SelectDocumentCore((IDescriptor) new ListDescriptor(this.lastViewedDocumentsService.Value.CategoryId, 0, "Недавно открытые документы", (IList) liveObjectsOnly), SelectionOptions.HideTree | SelectionOptions.DisableSelectFromTree, true);
    }
  }

  public long SelectDocument()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      return this.SelectDocumentCore((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(IDCache.Default.AllDocuments.Id), SelectionOptions.DisableSelectFromTree);
    }
  }

  private long SelectDocumentCore(
    IDescriptor selectionRoot,
    SelectionOptions selectionOptions,
    bool showAllModifications = false)
  {
    return this.invokerService.InvokeFunc<long>(60000, (Func<long>) (() =>
    {
      AdvancedServiceContainer nodesContext = new AdvancedServiceContainer();
      if (showAllModifications)
      {
        ObjectsSelectionOptionsHolder serviceInstance = new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications);
        nodesContext.AddService(typeof (ObjectsSelectionOptionsHolder), (object) serviceInstance);
      }
      object[] objArray = SelectionWindow.Select("Выберите документ для прикрепления замечаний", selectionRoot, typeof (IDBTypedObjectID), (IServiceProvider) nodesContext, SelectionOptions.SelectObjects | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect | selectionOptions);
      return objArray == null || objArray.Length == 0 ? 0L : ((IDBTypedObjectID) objArray[0]).ObjectID;
    }));
  }

  public long FindDocumentByFilePath(string filePath)
  {
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException("Путь к файлу не может быть пустым", nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException("Требуется абсолютный путь к файлу.", nameof (filePath));
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      FileOrigin fileOrigin = this.fileVaultService.WorkArea.GetFileOrigin(filePath, false);
      switch (fileOrigin.OriginType)
      {
        case FileOriginType.WorkFile:
          return fileOrigin.WorkObject.ObjectId;
        case FileOriginType.DetachedFile:
          VersionsRulePackage versionsRule = this.GetVersionsRule();
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            return sessionKeeper.Session.GetObjectByVersionsRule(fileOrigin.Id, versionsRule.OwnerId, true).ObjectID;
        default:
          return 0;
      }
    }
  }

  public void OpenDocument(long documentId)
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.openDocumentId = documentId;
    }
  }

  private void CheckDocumentIsOpen()
  {
    if (this.openDocumentId == 0L)
      throw new Exception("Не задан текущий документ. Воспользуйтесь методом OpenDocument(documentId).");
  }

  public object GetDocumentAttribute(string attributeName)
  {
    if (string.IsNullOrEmpty(attributeName))
      throw new ArgumentException("Имя атрибута документа не может быть пустым.", nameof (attributeName));
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        object[] valuesByName = sessionKeeper.Session.GetObject(this.openDocumentId).GetValuesByName(attributeName, false);
        return valuesByName == null || valuesByName.Length == 0 ? (object) null : valuesByName[0];
      }
    }
  }

  public string GetDocumentFilePath()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      return Path.Combine(this.fileVaultService.WorkArea.AreaPath, this.fileVaultService.DBFilesInfo.GetMasterFileName(this.openDocumentId, true));
    }
  }

  public string GetDocumentRanks()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      return string.Join(Environment.NewLine, (IEnumerable<string>) CollectionUtils.ConvertAsList<Tuple<string, string>, string>((ICollection<Tuple<string, string>>) this.signsService.Value.GetUserGraphs(this.openDocumentId), (Converter<Tuple<string, string>, string>) (tuple => tuple.Item2)));
    }
  }

  public string SelectDocumentRank()
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      RankGraphsInfo[] rankGraphsInfoArray = this.invokerService.InvokeFunc<RankGraphsInfo[]>(60000, (Func<RankGraphsInfo[]>) (() => this.signsService.Value.ShowUserGraphsDialog(this.openDocumentId)));
      if (rankGraphsInfoArray == null || rankGraphsInfoArray.Length == 0)
        return string.Empty;
      OrderedList<string> values = new OrderedList<string>();
      foreach (RankGraphsInfo rankGraphsInfo in rankGraphsInfoArray)
      {
        foreach (Tuple<string, string> graph in rankGraphsInfo.Graphs)
          values.Add(graph.Item2);
      }
      return string.Join(Environment.NewLine, (IEnumerable<string>) values);
    }
  }

  public bool GetDocumentWorkflowInfo(out string processName, out string activityName)
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      Tuple<long, int, string, string> anyActiveProcess = this.workflowHelper.FindAnyActiveProcess(this.openDocumentId);
      if (anyActiveProcess != null)
      {
        activityName = anyActiveProcess.Item3;
        processName = anyActiveProcess.Item4;
        return true;
      }
      activityName = string.Empty;
      processName = string.Empty;
      return false;
    }
  }

  public string ViewDocument(bool copyToDiskOnly)
  {
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      return copyToDiskOnly ? this.fileVaultService.PublishTree(this.openDocumentId, true, this.GetVersionsRule(), (IFileArea) this.fileVaultService.WorkArea) : this.invokerService.InvokeFunc<string>(60000, (Func<string>) (() =>
      {
        LaunchParams launchParams = new LaunchParams(LaunchType.View, this.openDocumentId, DBHelper.GetObjectType(this.openDocumentId), this.GetVersionsRule());
        launchParams.FileArea = (IFileArea) this.fileVaultService.WorkArea;
        ClientContext.LaunchActions.Launch(launchParams);
        return launchParams.ResultFilePath;
      }));
    }
  }

  public void UpdateRedliningFile(string redliningFilePath)
  {
    if (string.IsNullOrEmpty(redliningFilePath))
      throw new ArgumentException("Путь к файлу не может быть пустым", nameof (redliningFilePath));
    if (!Path.IsPathRooted(redliningFilePath))
      throw new ArgumentException("Требуется абсолютный путь к файлу.", nameof (redliningFilePath));
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      if (!File.Exists(redliningFilePath))
        throw new FileNotFoundException("Файл замечаний не найден на диске.", redliningFilePath);
      string masterFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(this.openDocumentId, true);
      string str1 = this.MakeRedliningFileName(masterFileName);
      if (!PathUtils.IsSamePath(Path.GetFileName(redliningFilePath), Path.GetFileName(str1)))
        throw new Exception($"Имя файла замечаний '{Path.GetFileName(redliningFilePath)}' должно соответствовать имени основного файла документа '{Path.GetFileName(masterFileName)}'.");
      string str2 = Path.Combine(this.fileVaultService.WorkArea.AreaPath, str1);
      if (!PathUtils.IsSamePath(str2, redliningFilePath) && File.Exists(str2))
      {
        FileAttributes attributes = File.GetAttributes(str2);
        File.SetAttributes(str2, FileAttributes.Normal);
        File.Copy(redliningFilePath, str2, true);
        File.SetAttributes(str2, attributes);
      }
      UploadFileAction uploadResult = new UploadFileAction(FileState.FromFile(redliningFilePath, str1), redliningFilePath);
      uploadResult.AllowNewFiles = true;
      uploadResult.FileType = FileTypes.ftRedlining;
      FileOperations.BatchUpdateFiles(this.openDocumentId, (IList<IFileAttributeAction>) new IFileAttributeAction[1]
      {
        (IFileAttributeAction) uploadResult
      });
      new TrackUploadedFileAction(this.fileVaultService.WorkArea.FileTracker, this.openDocumentId, (IObjectFilesUploadResult) uploadResult).Perform();
    }
  }

  public string GetRedliningFile(string dirPath)
  {
    if (string.IsNullOrEmpty(dirPath))
      throw new ArgumentException("Путь к папке не может быть пустым", nameof (dirPath));
    if (!Path.IsPathRooted(dirPath))
      throw new ArgumentException("Требуется абсолютный путь к папке.", nameof (dirPath));
    lock (this.syncRoot)
    {
      this.CheckInitialzied();
      this.CheckReady();
      this.CheckDocumentIsOpen();
      string desiredRedliningFileName = this.MakeRedliningFileName(this.fileVaultService.DBFilesInfo.GetMasterFileName(this.openDocumentId, true));
      FileState fileState1 = CollectionUtils.Find<FileState>((IEnumerable<FileState>) this.fileVaultService.DBFilesInfo.GetFileStates(this.openDocumentId), (Predicate<FileState>) (item => PathUtils.IsSamePath(item.FileName, desiredRedliningFileName)));
      if (fileState1 == null)
        return (string) null;
      string redliningFile = Path.Combine(dirPath, Path.GetFileName(desiredRedliningFileName));
      FileState fileState2 = File.Exists(redliningFile) ? FileState.FromFile(redliningFile, desiredRedliningFileName) : (FileState) null;
      List<IFileAttributeAction> actions = new List<IFileAttributeAction>();
      switch (this.fileDiffCalculator.Calculate(fileState2, fileState1).DifferenceType)
      {
        case FileDifferenceType.MissingFile:
          actions.Add((IFileAttributeAction) new DownloadFileAction(fileState1, redliningFile));
          break;
        case FileDifferenceType.OutdatedFile:
          actions.Add((IFileAttributeAction) new DeleteLocalFileAction(fileState2, redliningFile));
          actions.Add((IFileAttributeAction) new DownloadFileAction(fileState1, redliningFile));
          break;
      }
      if (actions.Count != 0)
        FileOperations.BatchReadFiles(this.openDocumentId, (ICollection<IFileAttributeAction>) actions);
      return redliningFile;
    }
  }

  private string MakeRedliningFileName(string masterFileName)
  {
    return masterFileName + this.redliningExtension;
  }

  private VersionsRulePackage GetVersionsRule() => VersionsRuleSources.GetEditorRule();
}
