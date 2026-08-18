
// Type: Intermech.Redline.RedliningSettings
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Redline;

/// <summary>
/// Предоставляет доступ к клиентским настройкам красного карандаша.
/// </summary>
public static class RedliningSettings
{
  private static RedliningCommonSettings commonSettings;

  /// <summary>Возвращает общие настройки сервиса.</summary>
  public static RedliningCommonSettings CommonSettings
  {
    [MethodImpl(MethodImplOptions.Synchronized)] get
    {
      if (RedliningSettings.commonSettings == null)
      {
        RedliningSettings.commonSettings = new RedliningCommonSettings();
        RedliningSettings.commonSettings.GetErrorText += new EventHandler<ErrorTextArgs>(RedliningSettings.CommonFileSettingsErrorHandler);
        RedliningSettings.commonSettings.Load();
        RedliningSettings.commonSettings.Changed += new EventHandler(RedliningSettings.SaveHandler);
      }
      return RedliningSettings.commonSettings;
    }
  }

  private static void CommonFileSettingsErrorHandler(object sender, ErrorTextArgs e)
  {
    e.Text = $"{e.Text} Изменить параметры можно в окне 'Параметры IPS' на вкладке 'Система\\Красный карандаш\\Общие настройки'.";
  }

  private static void SaveHandler(object sender, EventArgs e)
  {
    ((PersistentSettingsObject) sender).SaveInBackground();
  }
}
