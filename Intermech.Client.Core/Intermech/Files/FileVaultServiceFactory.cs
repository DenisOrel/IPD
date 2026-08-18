
// Type: Intermech.Files.FileVaultServiceFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Configuration;
using System.IO;


namespace Intermech.Files;

internal sealed class FileVaultServiceFactory
{
  private const string VaultPathAppKey = "FileVaultPath";
  private const string VaultPathAppKey2 = "FssVaultPath";
  private const string FssEnabledAppKey = "FssEnabled";
  private IFileAttributeEditorService fileAttributeEditorService;
  private IFileVaultSettingsService fileVaultSettingsService;
  private IOpenFilesService openFilesService;
  private IApplicationEventLogService eventLogService;

  public FileVaultServiceFactory(
    IFileAttributeEditorService fileAttributeEditorService,
    IFileVaultSettingsService fileVaultSettingsService,
    IOpenFilesService openFilesService,
    IApplicationEventLogService eventLogService)
  {
    if (fileAttributeEditorService == null)
      throw new ArgumentNullException(nameof (fileAttributeEditorService));
    if (fileVaultSettingsService == null)
      throw new ArgumentNullException(nameof (fileVaultSettingsService));
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (eventLogService == null)
      throw new ArgumentNullException(nameof (eventLogService));
    this.fileAttributeEditorService = fileAttributeEditorService;
    this.fileVaultSettingsService = fileVaultSettingsService;
    this.openFilesService = openFilesService;
    this.eventLogService = eventLogService;
  }

  public FileVaultService Create()
  {
    bool fssEnabled = this.ReadFssEnabled();
    string homePath = this.ReadHomePath();
    this.ValidateHomePath(homePath);
    long userId = this.ReadCurrentUserId();
    FileVaultService fileVault = new FileVaultService(homePath, userId, fssEnabled, this.fileAttributeEditorService, this.fileVaultSettingsService, this.openFilesService, this.eventLogService);
    try
    {
      this.ClearTempArea(fileVault);
      this.ClearViewArea((IFileVault) fileVault);
      this.ClearWorkArea((IFileVault) fileVault);
    }
    catch
    {
      fileVault.Dispose();
      throw;
    }
    return fileVault;
  }

  private bool ReadFssEnabled()
  {
    string str = ConfigurationManager.AppSettings.Get("FssEnabled");
    if (string.IsNullOrEmpty(str))
      return false;
    string lower = str.ToLower();
    return lower == "true" || lower == "yes" || lower == "on" || lower == "1";
  }

  private string ReadHomePath()
  {
    string name = ConfigurationManager.AppSettings.Get("FileVaultPath") ?? ConfigurationManager.AppSettings.Get("FssVaultPath");
    if (name != null)
      name = name.Trim();
    if (string.IsNullOrEmpty(name))
      name = "FileVault";
    string str = Environment.ExpandEnvironmentVariables(name);
    if (!Path.IsPathRooted(str))
      str = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, str));
    return str;
  }

  private void ValidateHomePath(string homePath)
  {
    if (string.IsNullOrEmpty(homePath))
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1300"), (object) "FileVaultPath", (object) "FssVaultPath"));
    try
    {
      if (new DriveInfo(Path.GetPathRoot(homePath)).DriveType != DriveType.Fixed)
        throw new ArgumentException("Bad drive type.");
    }
    catch (ArgumentException ex)
    {
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Client.Core_1301"), (object) homePath, (object) "FileVaultPath", (object) "FssVaultPath"), (Exception) ex);
    }
  }

  private long ReadCurrentUserId()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.UserID;
  }

  private void ClearTempArea(FileVaultService fileVault)
  {
    try
    {
      fileVault.TempArea.Cleanup();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ClearViewArea(IFileVault fileVault)
  {
    try
    {
      fileVault.ViewArea.Cleanup();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ClearWorkArea(IFileVault fileVault)
  {
    try
    {
      long num1 = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) is IDBConfigurations service ? service.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateCount", 92L, DBConfigMode.UserOnly) : 92L;
      int num2 = service != null ? (int) service.ReadInteger("CLIENT", "WORKCLEANER", "CleaningPendingDateMode", 0L, DBConfigMode.UserOnly) : 0;
      TimeSpan timeSpan = TimeSpan.FromDays(92.0);
      switch (num2)
      {
        case 0:
          timeSpan = TimeSpan.FromDays((double) num1);
          break;
        case 1:
          timeSpan = TimeSpan.FromDays((double) (num1 * 7L));
          break;
        case 2:
          timeSpan = TimeSpan.FromDays((double) (num1 * 31L /*0x1F*/));
          break;
        case 3:
          timeSpan = TimeSpan.FromDays((double) (num1 * 365L));
          break;
      }
      DateTime noUseSinceDate = DateTime.UtcNow.Date - timeSpan;
      foreach (DBObjectState dbObjectState in fileVault.WorkArea.GetPublishedObjects(noUseSinceDate).FindAll((Predicate<DBObjectState>) (workObj => !workObj.IsEditableState)))
        fileVault.WorkArea.Unpublish(dbObjectState.ObjectId);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
