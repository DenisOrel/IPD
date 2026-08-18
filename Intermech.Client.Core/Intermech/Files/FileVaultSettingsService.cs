
// Type: Intermech.Files.FileVaultSettingsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;
using System;
using System.Diagnostics;


namespace Intermech.Files;

internal sealed class FileVaultSettingsService : IFileVaultSettingsService
{
  private CommonFileSettings commonSettings;

  public FileVaultSettingsService()
  {
    this.commonSettings = new CommonFileSettings();
    this.commonSettings.GetErrorText += new EventHandler<ErrorTextArgs>(this.CommonFileSettingsErrorHandler);
    this.commonSettings.Load();
    this.commonSettings.Changed += new EventHandler(this.SaveHandler);
  }

  private void CommonFileSettingsErrorHandler(object sender, ErrorTextArgs e)
  {
    e.Text = $"{e.Text} Изменить параметры можно в окне 'Параметры IPS' на вкладке 'Файловое хранилище\\Общие настройки'.";
  }

  private void SaveHandler(object sender, EventArgs e)
  {
    ((PersistentSettingsObject) sender).SaveInBackground();
  }

  public CommonFileSettings CommonSettings
  {
    [DebuggerStepThrough] get => this.commonSettings;
  }
}
