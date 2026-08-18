
// Type: Intermech.Files.FileSettingEditorModel
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;
using Intermech.UI.PropertyPages;
using System;
using System.Diagnostics;


namespace Intermech.Files;

internal sealed class FileSettingEditorModel : IPropertyPageMvpModel
{
  private IFileVaultSettingsService fileVaultSettingsService;
  private bool isInitialized;
  private CommonFileSettings editableState;
  private CommonFileSettings originalState;

  public FileSettingEditorModel(IFileVaultSettingsService fileVaultSettingsService)
  {
    this.fileVaultSettingsService = fileVaultSettingsService != null ? fileVaultSettingsService : throw new ArgumentNullException(nameof (fileVaultSettingsService));
    this.isInitialized = false;
  }

  /// <summary>
  /// Возвращает текущее состояние настроек, в котором накапливаются изменения,
  /// сделанные в редакторах настроек.
  /// </summary>
  public CommonFileSettings EditableState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.editableState;
    }
  }

  /// <summary>
  /// Возвращает исходное состояние настроек, полученное от сервера приложений.
  /// </summary>
  public CommonFileSettings OriginalState
  {
    [DebuggerStepThrough] get
    {
      this.LazyInitialize();
      return this.originalState;
    }
  }

  private void LazyInitialize()
  {
    if (this.isInitialized)
      return;
    this.LoadAllFromDatabase();
    this.isInitialized = true;
  }

  /// <summary>
  /// Возвращает MVP-модель к исходному состоянию, отбрасывая все сделанные изменения.
  /// </summary>
  public void Reset()
  {
    if (!this.isInitialized)
      return;
    this.LoadAllFromDatabase();
  }

  /// <summary>Сохраняет все сделанные изменения, если они есть.</summary>
  public void SaveChanges()
  {
    if (!this.isInitialized || !this.HasAnyChanges())
      return;
    this.SaveAllToDatabase();
  }

  private void LoadAllFromDatabase()
  {
    this.editableState = this.fileVaultSettingsService.CommonSettings.Clone();
    this.originalState = this.editableState.Clone();
  }

  private void SaveAllToDatabase()
  {
    this.editableState.Validate();
    string firstError = this.editableState.GetFirstError();
    if (!string.IsNullOrEmpty(firstError))
      throw new FaultException(firstError);
    this.fileVaultSettingsService.CommonSettings.Assign((SettingsObject) this.editableState);
    this.originalState = this.editableState.Clone();
  }

  private bool HasAnyChanges()
  {
    return this.HasDriveLetterChanged() || this.HasSymlinkFolderChanged() || this.HasLeaveSourcesFlagChanged();
  }

  private bool HasDriveLetterChanged()
  {
    return (int) this.editableState.DriveLetter.RawValue != (int) this.originalState.DriveLetter.RawValue;
  }

  private bool HasSymlinkFolderChanged()
  {
    return this.editableState.SymlinkFolder.RawValue != this.originalState.SymlinkFolder.RawValue;
  }

  private bool HasLeaveSourcesFlagChanged()
  {
    return this.editableState.LeaveSourcesOfImportedFiles.RawValue != this.originalState.LeaveSourcesOfImportedFiles.RawValue;
  }
}
