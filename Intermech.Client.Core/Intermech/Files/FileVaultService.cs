
// Type: Intermech.Files.FileVaultService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Settings;
using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Files;

/// <summary>
/// Реализует файловый сервис IPS.  Все методы этого класса являются thread-safe.
/// </summary>
internal sealed class FileVaultService : 
  IFileVault,
  IFileAreas,
  IEnumerable<IFileArea>,
  IEnumerable,
  IDisposable
{
  private bool _deleteLinks = true;
  private const string WorkspaceLinkFileName = "IPS Workspace.lnk";
  private IFileVaultSettingsService fileVaultSettingsService;
  private IOpenFilesService openFilesService;
  private IApplicationEventLogService eventLogService;
  private readonly AlteredFilesService alteredFilesService;
  private IReadOnlyLocalFilesManager readOnlyLocalFilesManager;
  private DBObjectsInformationService dbObjectsInformation;
  private DBFilesInformationService dbFilesInformation;
  private readonly IFileVaultGuardian vaultGuardian;
  private readonly string unmappedPath;
  private string vaultPath;
  private char mappedDriveLetter;
  private WindowsJunctionPointsManager fsLinkManager;
  private string vaultSymlinkPath;
  private readonly SystemAreaService systemArea;
  private readonly TempAreaService tempArea;
  private readonly CacheAreaService cacheArea;
  private readonly WorkAreaService workArea;
  private readonly ViewAreaService viewArea;
  private readonly AreaBase[] areas;

  public FileVaultService(
    string homePath,
    long userId,
    bool fssEnabled,
    IFileAttributeEditorService fileAttributeEditorService,
    IFileVaultSettingsService fileVaultSettingsService,
    IOpenFilesService openFilesService,
    IApplicationEventLogService eventLogService)
  {
    if (string.IsNullOrEmpty(homePath))
      throw new ArgumentException();
    if (userId == 0L)
      throw new ArgumentException();
    if (fileAttributeEditorService == null)
      throw new ArgumentNullException(nameof (fileAttributeEditorService));
    if (fileVaultSettingsService == null)
      throw new ArgumentNullException(nameof (fileVaultSettingsService));
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (eventLogService == null)
      throw new ArgumentNullException(nameof (eventLogService));
    this.fileVaultSettingsService = fileVaultSettingsService;
    this.openFilesService = openFilesService;
    this.eventLogService = eventLogService;
    this.alteredFilesService = AlteredFilesService.Default;
    this.dbObjectsInformation = new DBObjectsInformationService(fileAttributeEditorService);
    this.dbFilesInformation = new DBFilesInformationService();
    this.readOnlyLocalFilesManager = (IReadOnlyLocalFilesManager) new ReadOnlyLocalFilesManager();
    if (fssEnabled)
    {
      DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(homePath));
      if (driveInfo.DriveFormat != "NTFS")
        throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1286"), (object) homePath, (object) driveInfo.DriveFormat));
      this.vaultGuardian = (IFileVaultGuardian) new FSSGuardian();
    }
    else
      this.vaultGuardian = (IFileVaultGuardian) new DumbGuardian();
    string str = userId.ToString();
    this.vaultGuardian.Initialize(homePath, str);
    this.vaultPath = Path.Combine(homePath, str);
    this.unmappedPath = this.vaultPath;
    this.CreateSubstDrive(homePath);
    this.CreateSymlinkFolder();
    this.systemArea = new SystemAreaService(this, "System", LocalizationHolder.rm.GetString("Client.Core_1287"));
    this.tempArea = new TempAreaService(this, "Temp", "Область временных файлов");
    this.cacheArea = new CacheAreaService(this, "Cache", "Область кэшируемых файлов");
    this.workArea = new WorkAreaService(this, "Workspace", LocalizationHolder.rm.GetString("Client.Core_1288"));
    this.viewArea = new ViewAreaService(this, "View", LocalizationHolder.rm.GetString("Client.Core_1289"));
    this.areas = new AreaBase[4]
    {
      (AreaBase) this.systemArea,
      (AreaBase) this.tempArea,
      (AreaBase) this.workArea,
      (AreaBase) this.viewArea
    };
    foreach (AreaBase area in this.areas)
      area.Initialize();
    if (RegistryHelper.GetValue<int>(RegistryHive.CurrentUser, "Software\\Intermech\\IPS", "CreateIpsWorkspaceShortcut", 1) == 0)
      return;
    this.CreateShellLinkFavs();
  }

  private void CreateShellLinkFavs()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._deleteLinks = sessionKeeper.Session.ActingUserID == 0L;
      if (!this._deleteLinks)
        return;
    }
    string desktopFolderPath = FileVaultService.GetDesktopFolderPath();
    string linksFolderPath = FileVaultService.GetLinksFolderPath();
    if (string.IsNullOrEmpty(desktopFolderPath) && string.IsNullOrEmpty(linksFolderPath))
      return;
    using (Intermech.ShellLink.ShellLink shellLink = new Intermech.ShellLink.ShellLink())
    {
      shellLink.Target = this.workArea.AreaPath;
      shellLink.WorkingDirectory = this.workArea.AreaPath;
      shellLink.Description = "IPS Workspace Shorcut";
      shellLink.DisplayMode = Intermech.ShellLink.ShellLink.LinkDisplayMode.edmNormal;
      shellLink.IconPath = Application.ExecutablePath;
      shellLink.IconIndex = 0;
      try
      {
        if (!string.IsNullOrEmpty(desktopFolderPath))
          shellLink.Save(Path.Combine(desktopFolderPath, "IPS Workspace.lnk"));
      }
      catch
      {
      }
      try
      {
        if (string.IsNullOrEmpty(linksFolderPath))
          return;
        shellLink.Save(Path.Combine(linksFolderPath, "IPS Workspace.lnk"));
      }
      catch
      {
      }
    }
  }

  private void DeleteShellLinkFavs()
  {
    if (!this._deleteLinks)
      return;
    string desktopFolderPath = FileVaultService.GetDesktopFolderPath();
    if (!string.IsNullOrEmpty(desktopFolderPath))
      FileUtils.DeleteFileSilently(Path.Combine(desktopFolderPath, "IPS Workspace.lnk"));
    string linksFolderPath = FileVaultService.GetLinksFolderPath();
    if (string.IsNullOrEmpty(linksFolderPath))
      return;
    FileUtils.DeleteFileSilently(Path.Combine(linksFolderPath, "IPS Workspace.lnk"));
  }

  private static string GetDesktopFolderPath()
  {
    return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
  }

  private static string GetLinksFolderPath()
  {
    return ShellKnownFolders.GetFolderPath(ShellKnownFolders.LinksFolderId);
  }

  private void CreateSubstDrive(string homePath)
  {
    char driveLetter = char.MinValue;
    try
    {
      driveLetter = (char) (ValueCell<char>) this.fileVaultSettingsService.CommonSettings.DriveLetter;
      if (driveLetter == char.MinValue)
        return;
      if (FileVaultService.CreateSubstDrive(driveLetter, homePath, this.vaultPath))
        this.mappedDriveLetter = driveLetter;
      this.vaultPath = $"{driveLetter}:\\";
    }
    catch (FaultException ex)
    {
      this.ShowSubstDriveError(driveLetter, (Exception) ex);
    }
    catch (Win32Exception ex)
    {
      this.ShowSubstDriveError(driveLetter, (Exception) ex);
    }
  }

  private void ShowSubstDriveError(char driveLetter, Exception x)
  {
    int num = (int) MessageBox.Show($"{$"Не удалось подключить файловое хранилище как диск {driveLetter}."} {x.Message}{Environment.NewLine}{Environment.NewLine}{$"Файловое хранилище будет доступно по обычному пути '{this.vaultPath}'."}", "IPS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  private static bool CreateSubstDrive(char driveLetter, string homePath, string vaultPath)
  {
    bool substDrive = true;
    string str = DriveUtils.GetMappedPath(driveLetter);
    if (!string.IsNullOrEmpty(str) && PathUtils.IsPlacedIn(str, homePath))
    {
      DriveUtils.UnmapDrive(driveLetter, str);
      str = (string) null;
      substDrive = false;
    }
    if (str != null)
      throw new FaultException("Указанная буква диска занята.");
    DriveUtils.MapDrive(driveLetter, vaultPath);
    return substDrive;
  }

  private void DeleteSubstDrive()
  {
    if (this.mappedDriveLetter == char.MinValue)
      return;
    try
    {
      DriveUtils.UnmapDrive(this.mappedDriveLetter, this.unmappedPath);
    }
    catch
    {
    }
  }

  private void CreateSymlinkFolder()
  {
    string str1 = (string) null;
    try
    {
      str1 = (string) (ValueCell<string>) this.fileVaultSettingsService.CommonSettings.SymlinkFolder;
      if (string.IsNullOrEmpty(str1))
        return;
      this.fsLinkManager = new WindowsJunctionPointsManager();
      if (!this.fsLinkManager.IsSupported)
        return;
      string str2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), str1);
      if (Directory.Exists(str2) && this.fsLinkManager.GetLinkTarget(str2) != null)
        this.fsLinkManager.BreakLink(str2);
      this.fsLinkManager.CreateLink(str2, this.unmappedPath);
      this.vaultSymlinkPath = str2;
      this.vaultPath = str2;
    }
    catch (FaultException ex)
    {
      this.ShowSymlinkFolderError(str1, (Exception) ex);
    }
    catch (IOException ex)
    {
      this.ShowSymlinkFolderError(str1, (Exception) ex);
    }
  }

  private void ShowSymlinkFolderError(string symlinkFolder, Exception x)
  {
    int num = (int) MessageBox.Show($"{$"Не удалось подключить файловое хранилище как папку '{symlinkFolder}'."} {x.Message}{Environment.NewLine}{Environment.NewLine}{$"Файловое хранилище будет доступно по обычному пути '{this.vaultPath}'."}", "IPS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  private void DeleteSymlinkFolder()
  {
    try
    {
      if (string.IsNullOrEmpty(this.vaultSymlinkPath) || !Directory.Exists(this.vaultSymlinkPath))
        return;
      if (this.fsLinkManager.GetLinkTarget(this.vaultSymlinkPath) != null)
        this.fsLinkManager.BreakLink(this.vaultSymlinkPath);
      Directory.Delete(this.vaultSymlinkPath);
    }
    catch
    {
    }
  }

  public void Dispose()
  {
    this.DeleteSubstDrive();
    this.DeleteSymlinkFolder();
    this.DeleteShellLinkFavs();
    this.vaultGuardian.Dispose();
  }

  /// <summary>
  /// Удаляет из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public void RemoveUnpublishedObjects(List<DBObjectState> list, IFileAreaPublishedObjects area)
  {
    this.DBObjectsInfo.RemoveUnpublishedObjects(list, area);
  }

  /// <summary>
  /// Извлекает из списка все объекты, которые не были ранее опубликованы в указанной файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <param name="area">Файловая область</param>
  /// <returns>Список с извлеченными неопубликованными объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на список объектов и файловую область не должны быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectState> ExtractUnpublishedObjects(
    List<DBObjectState> list,
    IFileAreaPublishedObjects area)
  {
    return this.DBObjectsInfo.ExtractUnpublishedObjects(list, area);
  }

  /// <summary>
  /// Удаляет из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public void RemoveDeadObjects(List<DBObjectState> list)
  {
    this.DBObjectsInfo.RemoveDeadObjects(list);
  }

  /// <summary>
  /// Извлекает из списка все мертвые объекты (отсутствующие в базе IPS). Этот метод используется в случаях, когда список
  /// построен на основе локальных данных о публикации объектов в файловой области.
  /// </summary>
  /// <param name="list">Список объектов</param>
  /// <returns>Список с извлеченными мертвыми объектами</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список объектов не должна быть null</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectState> ExtractDeadObjects(List<DBObjectState> list)
  {
    return this.DBObjectsInfo.ExtractDeadObjects(list);
  }

  /// <summary>Возвращает состояние объекта в базе IPS.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Признак, нужно ли сбрасывать исключение при отсутствии объекта</param>
  /// <returns>Состояние объекта в базе или null, если указанного объекта нет в базе IPS</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public DBObjectState GetObjectState(long objectId, bool throwIfNotFound)
  {
    return this.DBObjectsInfo.GetObjectState(objectId, throwIfNotFound);
  }

  /// <summary>
  /// Возвращает список имен файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список имен файлов объекта</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [Obsolete("Use the IDBFilesInformationService instead of this", true)]
  public List<string> GetObjectFileNames(long objectId) => this.DBFilesInfo.GetFileNames(objectId);

  /// <summary>
  /// Возвращает состояния файлов в атрибуте 'Файл' указанного объекта. Значения атрибута, равные DBNULL,
  /// игнорируются и в результате выполнения метода не присутствуют.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Список состояний файлов</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  [Obsolete("Use the IDBFilesInformationService instead of this", true)]
  public List<FileState> GetObjectFileStates(long objectId)
  {
    return this.DBFilesInfo.GetFileStates(objectId);
  }

  /// <summary>Определяет мастер-файл для указанного объекта.</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNotFound">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <returns>Имя файла в относительной форме (так, как оно записано в базе IPS)</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор версии объекта</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  [Obsolete("Use the IDBFilesInformationService instead of this", true)]
  public string GetObjectMasterFile(long objectId, bool throwIfNotFound)
  {
    return this.DBFilesInfo.GetMasterFileName(objectId, throwIfNotFound);
  }

  /// <summary>
  /// Создает список, содержащий один указанный объект. Этот метод используется в случаях, когда требуется опубликовать
  /// объект без учета его связей с другими объектами.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectState> CreateStateListForSingleObject(long objectId)
  {
    return this.DBObjectsInfo.CreateStateListForSingleObject(objectId);
  }

  /// <summary>
  /// Создает список, содержащий указанный объект и все связанные с ним объекты по всем типам связей, для которых настроено
  /// извлечение файлов.
  /// </summary>
  /// <param name="rootObjectId">Идентификатор версии корневого объекта</param>
  /// <param name="versionsRule">Правило подбора версий объектов</param>
  /// <returns>Построенный список состояний объектов</returns>
  /// <exception cref="T:Intermech.KernelException">Указанный объект отсутствует в базе IPS</exception>
  [Obsolete("Use the IDBObjectsInformationService instead of this", true)]
  public List<DBObjectState> CreateStateListForDocumentTree(
    long rootObjectId,
    VersionsRulePackage versionsRule)
  {
    return this.DBObjectsInfo.CreateStateListForObjectTree(rootObjectId, versionsRule);
  }

  /// <summary>
  /// Публикует заданный объект и все объекты, связанные с ним, в указанной области файлового хранилища.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="fileName">Имя файла в относительной форме (так, как оно записано в базе IPS)</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="fileArea">Область файлового хранилища, в которой следует опубликовать объект</param>
  /// <returns>Абсолютный путь к файлу объекта после публикации</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта или имя файла</exception>
  /// <exception cref="T:System.ArgumentNullException">Не задано правило подбора версий или область файлового хранилища</exception>
  public string PublishTree(
    long objectId,
    string fileName,
    VersionsRulePackage versionsRule,
    IFileArea fileArea)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    if (fileArea == null)
      throw new ArgumentNullException();
    if (fileArea != this.workArea && fileArea != this.viewArea)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_1291"));
    string path;
    if (fileArea == this.workArea)
    {
      this.workArea.Publish((IList<DBObjectState>) this.DBObjectsInfo.CreateStateListForObjectTree(objectId, versionsRule), (IReplaceFilePolicy) new PreserveAnyChanges());
      path = Path.Combine(this.workArea.AreaPath, fileName);
    }
    else
      path = this.viewArea.Publish((IList<DBObjectState>) this.DBObjectsInfo.CreateStateListForObjectTree(objectId, versionsRule)).ObjectFiles.Find((Predicate<PublishedFile>) (file => PathUtils.IsSamePath(file.FileState.FileName, fileName)))?.FullName;
    return path != null && File.Exists(path) ? path : throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1292"), (object) fileName, (object) objectId));
  }

  /// <summary>
  /// Публикует заданный объект и все объекты, связанные с ним, в указанной области файлового хранилища.
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="throwIfNoMasterFile">Следует ли сбрасывать исключение при отсутствии мастер-файла у объекта</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="fileArea">Область файлового хранилища, в которой следует опубликовать объект</param>
  /// <returns>Абсолютный путь к мастер-файлу объекта после публикации. Может быть null, если у объекта нет мастер-файла или атрибута "Файл"</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор объекта или имя файла</exception>
  /// <exception cref="T:System.ArgumentNullException">Не задано правило подбора версий или область файлового хранилища</exception>
  /// <exception cref="T:Intermech.FaultException">У объекта отсутствует мастер-файл или нет атрибута "Файл"</exception>
  public string PublishTree(
    long objectId,
    bool throwIfNoMasterFile,
    VersionsRulePackage versionsRule,
    IFileArea fileArea)
  {
    string masterFileName = this.DBFilesInfo.GetMasterFileName(objectId, throwIfNoMasterFile);
    return !string.IsNullOrEmpty(masterFileName) ? this.PublishTree(objectId, masterFileName, versionsRule, fileArea) : (string) null;
  }

  /// <summary>
  /// Возвращает сервис для получения информации о состоянии объектов IPS в базе данных.
  /// </summary>
  public IDBObjectsInformationService DBObjectsInfo
  {
    [DebuggerStepThrough] get => (IDBObjectsInformationService) this.dbObjectsInformation;
  }

  /// <summary>
  /// Возвращает сервис для получения информации о состоянии файлов объектов IPS в базе данных.
  /// </summary>
  public IDBFilesInformationService DBFilesInfo
  {
    [DebuggerStepThrough] get => (IDBFilesInformationService) this.dbFilesInformation;
  }

  /// <summary>
  /// Возвращает менеджер операций с атрибутом read-only для локальных файлов объектов IPS.
  /// </summary>
  public IReadOnlyLocalFilesManager ReadOnlyLocalFiles
  {
    [DebuggerStepThrough] get => this.readOnlyLocalFilesManager;
  }

  /// <summary>Возвращает перечислитель файловых областей.</summary>
  /// <returns>Перечислитель файловых областей</returns>
  public IEnumerator<IFileArea> GetEnumerator()
  {
    return (IEnumerator<IFileArea>) new FileVaultService.WrapAreaEnumerator(this.areas.GetEnumerator());
  }

  /// <summary>Возвращает перечислитель файловых областей.</summary>
  /// <returns>Перечислитель файловых областей</returns>
  IEnumerator IEnumerable.GetEnumerator() => this.areas.GetEnumerator();

  /// <summary>
  /// Позволяет определить область файлового хранилища, в которой находится указанный файл.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к файлу</param>
  /// <returns>Объект файловой области. Может быть null,если файл находится вне файлового хранилища</returns>
  /// <exception cref="T:System.ArgumentException">Не задан путь к файлу</exception>
  /// <exception cref="T:System.InvalidOperationException">Путь к файлу указан не в абсолютной форме</exception>
  public IFileArea FindArea(string fullPath)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(fullPath))
      throw new InvalidOperationException();
    for (int index = 0; index < this.areas.Length; ++index)
    {
      if (PathUtils.IsPlacedIn(fullPath, this.areas[index].AreaPath))
        return (IFileArea) this.areas[index];
    }
    return (IFileArea) null;
  }

  /// <summary>Возвращает объект области для временных файлов.</summary>
  ITempArea IFileAreas.TempArea => (ITempArea) this.tempArea;

  /// <summary>Возвращает объект области для кэшируемых файлов.</summary>
  IFileArea IFileAreas.CacheArea => (IFileArea) this.cacheArea;

  /// <summary>
  /// Возвращает объект рабочей области файлового хранилища.
  /// </summary>
  IWorkArea IFileAreas.WorkArea => (IWorkArea) this.workArea;

  /// <summary>
  /// Возвращает объект области просмотра файлового хранилища.
  /// </summary>
  IViewArea IFileAreas.ViewArea => (IViewArea) this.viewArea;

  internal string VaultPath => this.vaultPath;

  internal SystemAreaService SystemArea => this.systemArea;

  internal TempAreaService TempArea => this.tempArea;

  internal WorkAreaService WorkArea => this.workArea;

  internal ViewAreaService ViewArea => this.viewArea;

  internal AlteredFilesService AlteredFilesService => this.alteredFilesService;

  internal IOpenFilesService OpenFilesService => this.openFilesService;

  internal IApplicationEventLogService EventLogService => this.eventLogService;

  private sealed class WrapAreaEnumerator : IEnumerator<IFileArea>, IDisposable, IEnumerator
  {
    private readonly IEnumerator arrayEnumerator;

    public WrapAreaEnumerator(IEnumerator arrayEnumerator)
    {
      this.arrayEnumerator = arrayEnumerator;
    }

    public IFileArea Current => (IFileArea) this.arrayEnumerator.Current;

    public void Dispose()
    {
    }

    object IEnumerator.Current => this.arrayEnumerator.Current;

    public bool MoveNext() => this.arrayEnumerator.MoveNext();

    public void Reset() => this.arrayEnumerator.Reset();
  }
}
