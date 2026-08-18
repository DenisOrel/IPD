
// Type: Intermech.Tools.CommonTasks.MakeAuthenticFileTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Integrators;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Tools.CommonTasks;

public sealed class MakeAuthenticFileTask : IAction
{
  private IFileVault fileVaultService;
  private IOpenFilesService openFilesService;
  private INotificationService notificationService;
  private Func<InjectStandaloneViewDataTask> injectViewDataTaskFactory;
  private bool isInitialized;
  private long objectId;
  private int objectType;
  private string authenticFileTypeFilter;
  private bool canPerform;
  private IntegratorObject integratorObject;
  private IAuthenticFilesService authMakerService;
  private string objectFileName;
  private ICollection<string> possibleFileTypes;
  private string authFileType;

  public MakeAuthenticFileTask(
    IFileVault fileVaultService,
    IOpenFilesService openFilesService,
    INotificationService notificationService,
    Func<InjectStandaloneViewDataTask> injectViewDataTaskFactory)
  {
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (injectViewDataTaskFactory == null)
      throw new ArgumentNullException(nameof (injectViewDataTaskFactory));
    this.fileVaultService = fileVaultService;
    this.openFilesService = openFilesService;
    this.notificationService = notificationService;
    this.injectViewDataTaskFactory = injectViewDataTaskFactory;
    this.DoClear();
  }

  public void Initialize(
    long objectId,
    int objectType,
    string authenticFileTypeFilter,
    string objectFileName)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (objectId));
    if (objectType == -1)
      throw new ArgumentException("Не задан идентификатор типа объекта.", nameof (objectType));
    if (!string.IsNullOrEmpty(authenticFileTypeFilter) && !authenticFileTypeFilter.StartsWith("."))
      throw new ArgumentException("Расширение файла должно начинаться с точки.", nameof (authenticFileTypeFilter));
    if (this.isInitialized)
      this.Clear();
    try
    {
      this.objectId = objectId;
      this.objectType = objectType;
      this.authenticFileTypeFilter = authenticFileTypeFilter;
      this.objectFileName = objectFileName;
      this.canPerform = this.DoInitialize();
      this.isInitialized = true;
    }
    catch
    {
      this.DoClear();
      throw;
    }
  }

  private bool DoInitialize()
  {
    this.integratorObject = this.FindIntegratorObject();
    if (this.integratorObject == null)
      return false;
    this.authMakerService = IntegratorServices.GetService<IAuthenticFilesService>(this.integratorObject, false);
    if (this.authMakerService == null)
      return false;
    if (string.IsNullOrEmpty(this.objectFileName))
    {
      this.objectFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(this.objectId, false);
      if (this.objectFileName == null)
        return false;
    }
    IApplicationFileTypes service = IntegratorServices.GetService<IApplicationFileTypes>(this.integratorObject, false);
    if (service == null || !service.IsApplicationFile(this.objectFileName))
      return false;
    this.possibleFileTypes = this.authMakerService.GetPossibleFileTypes(this.objectType);
    if (this.possibleFileTypes.Count != 0 && !string.IsNullOrEmpty(this.authenticFileTypeFilter))
      this.ApplyAuthenticFileTypeFilter(this.possibleFileTypes);
    return this.possibleFileTypes.Count != 0;
  }

  private void ApplyAuthenticFileTypeFilter(ICollection<string> possibleFileTypes)
  {
    if (!CollectionUtils.Exists<string>((IEnumerable<string>) this.possibleFileTypes, (Predicate<string>) (item => PathUtils.IsSamePath(item, this.authenticFileTypeFilter))))
      return;
    this.possibleFileTypes.Clear();
    this.possibleFileTypes.Add(this.authenticFileTypeFilter);
  }

  private IntegratorObject FindIntegratorObject()
  {
    DBObjectTypeFileHandlingRules fileHandlingRules = IntegratorServices.GetFileHandlingRules(this.objectType);
    return fileHandlingRules.IntegratorRef != null && fileHandlingRules.RequireNormalEditMode ? fileHandlingRules.IntegratorRef : (IntegratorObject) null;
  }

  public void Clear()
  {
    if (!this.isInitialized)
      return;
    this.DoClear();
    this.isInitialized = false;
  }

  /// <summary>
  /// Выполняет полную очистку внутреннего состояния. Может вызываться для очистки частично заполненного состояния. Метод не должен бросать исключений.
  /// </summary>
  private void DoClear()
  {
    this.objectId = 0L;
    this.objectType = -1;
    this.authenticFileTypeFilter = (string) null;
    this.canPerform = false;
    this.integratorObject = (IntegratorObject) null;
    this.authMakerService = (IAuthenticFilesService) null;
    this.objectFileName = (string) null;
    this.possibleFileTypes = (ICollection<string>) null;
    this.authFileType = (string) null;
  }

  public bool IsInitialized => this.isInitialized;

  private void RequireInitialized()
  {
    if (!this.isInitialized)
      throw new InvalidOperationException("Object must be initialized first.");
  }

  public long ObjectId => this.objectId;

  public int ObjectType => this.objectType;

  public string ObjectFileName => this.objectFileName;

  public bool CanPerform => this.canPerform;

  public void Perform()
  {
    this.RequireInitialized();
    if (!this.CanPerform)
      return;
    this.DoMakeAuthenticFile();
  }

  private void DoMakeAuthenticFile()
  {
    if (this.possibleFileTypes.Count == 1)
    {
      this.authFileType = CollectionUtils.GetFirstItem<string>((IEnumerable<string>) this.possibleFileTypes);
    }
    else
    {
      using (SelectItemForm selectItemForm = new SelectItemForm())
      {
        selectItemForm.Text = "Выбор типа аутентичного файла";
        selectItemForm.Description = $"Из представленного ниже списка выберите тип аутентичного файла, который требуется создать для документа '{DBHelper.GetObjectCaption(this.objectId)}'";
        selectItemForm.Items = (IEnumerable) this.possibleFileTypes;
        if (selectItemForm.ShowDialog() != DialogResult.OK)
          return;
        this.authFileType = (string) selectItemForm.SelectedItem;
      }
    }
    if (string.IsNullOrEmpty(this.authFileType) || this.authFileType[0] != '.')
      throw new InvalidOperationException("Расширение аутентичного файла должно начинаться с точки и не может быть пустым.");
    string str1 = this.fileVaultService.PublishTree(this.objectId, this.objectFileName, VersionsRuleSources.GetEditorRule(), (IFileArea) this.fileVaultService.WorkArea);
    InjectStandaloneViewDataTask standaloneViewDataTask = this.injectViewDataTaskFactory();
    standaloneViewDataTask.Initialize(this.objectId, this.objectFileName, str1);
    if (standaloneViewDataTask.CanPerform)
      standaloneViewDataTask.Perform();
    string str2 = this.authMakerService.MakeFilePath(str1, this.authFileType);
    if (File.Exists(str2))
    {
      this.openFilesService.SetReadOnlyFlag(str2, false);
      File.SetAttributes(str2, FileAttributes.Normal);
    }
    this.authMakerService.MakeFile(str1, str2);
    this.UploadAuthenticFile(str2);
    this.NotifyUI();
  }

  private void UploadAuthenticFile(string localFilePath)
  {
    string relativePath = PathUtils.GetRelativePath(localFilePath, this.fileVaultService.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    UploadFileAction uploadResult = new UploadFileAction(FileState.FromFile(localFilePath, relativePath), localFilePath);
    uploadResult.AllowNewFiles = true;
    uploadResult.FileType = FileTypes.ftAuthentical;
    FileOperations.BatchUpdateFiles(this.objectId, (IList<IFileAttributeAction>) new IFileAttributeAction[1]
    {
      (IFileAttributeAction) uploadResult
    });
    new TrackUploadedFileAction(this.fileVaultService.WorkArea.FileTracker, this.objectId, (IObjectFilesUploadResult) uploadResult).Perform();
  }

  private void NotifyUI()
  {
    this.notificationService.FireEvent((object) this, (NotificationEventArgs) new FileAttribute4ObjectChangedEventArgs((ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).FileAttributeID, this.objectId));
  }
}
