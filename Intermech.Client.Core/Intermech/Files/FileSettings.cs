
// Type: Intermech.Files.FileSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Files;

[Obsolete("Use the service IFileVaultSettingsService instead of this.", true)]
public static class FileSettings
{
  private static CommonFileSettings common;

  public static CommonFileSettings Common
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get
    {
      if (FileSettings.common == null)
      {
        FileSettings.common = new CommonFileSettings();
        FileSettings.common.GetErrorText += new EventHandler<ErrorTextArgs>(FileSettings.CommonFileSettingsErrorHandler);
        FileSettings.common.Load();
        FileSettings.common.Changed += new EventHandler(FileSettings.SaveHandler);
      }
      return FileSettings.common;
    }
  }

  private static void CommonFileSettingsErrorHandler(object sender, ErrorTextArgs e)
  {
    e.Text = $"{e.Text} Изменить параметры можно в окне 'Параметры IPS' на вкладке 'Файловое хранилище\\Общие настройки'.";
  }

  private static void SaveHandler(object sender, EventArgs e)
  {
    ((PersistentSettingsObject) sender).SaveInBackground();
  }
}
