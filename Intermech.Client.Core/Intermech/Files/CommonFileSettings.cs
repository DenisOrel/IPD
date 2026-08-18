
// Type: Intermech.Files.CommonFileSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Configuration;
using Intermech.Interfaces;
using Intermech.Settings;
using System.Collections.Generic;


namespace Intermech.Files;

public sealed class CommonFileSettings : DBPersistentSettingsObject
{
  private const char DriveLetterIsNotSet = '\0';
  private const string DriveLetterParameter = "DriveLetter";
  private const string SymlinkFolderParameter = "SymlinkFolder";
  private const string LeaveSourcesOfImportedFilesParameter = "LeaveSourcesOfImportedFiles";
  private SettingsCell<char> driveLetter;
  private SettingsCell<string> symlinkFolder;
  private SettingsCell<bool> leaveSourcesOfImportedFiles;

  public CommonFileSettings()
    : base("FileVault", "Globals")
  {
  }

  protected override void CreateCells(ICollection<ISettingsCell> cells)
  {
    base.CreateCells(cells);
    this.driveLetter = new SettingsCell<char>((object) this, "Буква диска для подключения файлового хранилища пользователя", char.MinValue);
    cells.Add((ISettingsCell) this.driveLetter);
    this.symlinkFolder = new SettingsCell<string>((object) this, "Имя папки в \"Моих документах\" для подключения файлового хранилища пользователя", string.Empty);
    cells.Add((ISettingsCell) this.symlinkFolder);
    this.leaveSourcesOfImportedFiles = new SettingsCell<bool>((object) this, "При перемещении импортируемых файлов в рабочую область оставлять исходные файлы", false);
    cells.Add((ISettingsCell) this.leaveSourcesOfImportedFiles);
  }

  protected override void CreateValidators(ICollection<object> validators)
  {
    base.CreateValidators(validators);
    validators.Add((object) new DriveLetterValitator(this.driveLetter));
    validators.Add((object) new SymlinkFolderValidator(this.symlinkFolder));
  }

  protected override void DoAssign(SettingsObject source)
  {
    base.DoAssign(source);
    if (!(source is CommonFileSettings commonFileSettings))
      return;
    this.driveLetter.RawValue = commonFileSettings.driveLetter.RawValue;
    this.symlinkFolder.RawValue = commonFileSettings.symlinkFolder.RawValue;
    this.leaveSourcesOfImportedFiles.RawValue = commonFileSettings.leaveSourcesOfImportedFiles.RawValue;
  }

  public SettingsCell<char> DriveLetter => this.driveLetter;

  public SettingsCell<string> SymlinkFolder => this.symlinkFolder;

  public SettingsCell<bool> LeaveSourcesOfImportedFiles => this.leaveSourcesOfImportedFiles;

  public CommonFileSettings Clone() => (CommonFileSettings) this.DoClone();

  protected override void DoLoad(IUserSession session)
  {
    base.DoLoad(session);
    string str1 = this.ReadGlobalString(session, "DriveLetter");
    this.driveLetter.RawValue = str1.Length == 0 || str1[0] == char.MinValue ? char.MinValue : str1[0];
    this.symlinkFolder.RawValue = this.ReadGlobalString(session, "SymlinkFolder");
    string str2 = this.ReadGlobalString(session, "LeaveSourcesOfImportedFiles");
    if (string.IsNullOrEmpty(str2))
      return;
    this.leaveSourcesOfImportedFiles.RawValue = AppSettingsHelper.ParseBoolean(str2, false);
  }

  protected override void DoSave(IUserSession session)
  {
    base.DoSave(session);
    this.WriteGlobalString(session, "DriveLetter", this.driveLetter.RawValue != char.MinValue ? this.driveLetter.RawValue.ToString() : string.Empty);
    this.WriteGlobalString(session, "SymlinkFolder", this.symlinkFolder.RawValue);
    this.WriteGlobalString(session, "LeaveSourcesOfImportedFiles", this.leaveSourcesOfImportedFiles.RawValue.ToString());
  }
}
