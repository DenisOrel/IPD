
// Type: Intermech.Remoting.Optimized.AppSettings
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Configuration;


namespace Intermech.Remoting.Optimized
{
    internal static class AppSettings
    {
      internal static readonly string AllowTransparentProxyMessageKeyName = "microsoft:Remoting:AllowTransparentProxyMessage";
      internal static readonly bool AllowTransparentProxyMessageDefaultValue = false;
      internal static readonly string AllowTransparentProxyMessageFwLink = "http://go.microsoft.com/fwlink/?LinkId=390633";
      internal static readonly string AllowUnsanitizedWSDLUrlsKeyName = "microsoft:Remoting:AllowUnsanitizedWSDLUrls";
      internal static readonly bool AllowUnsanitizedWSDLUrlsDefaultValue = false;
      private static bool allowTransparentProxyMessageValue = AppSettings.AllowTransparentProxyMessageDefaultValue;
      private static bool allowUnsanitizedWSDLUrlsValue = AppSettings.AllowUnsanitizedWSDLUrlsDefaultValue;
      private static volatile bool settingsInitialized = false;
      private static object appSettingsLock = new object();

      internal static bool AllowUnsanitizedWSDLUrls
      {
        get
        {
          AppSettings.EnsureSettingsLoaded();
          return AppSettings.allowUnsanitizedWSDLUrlsValue;
        }
      }

      internal static bool AllowTransparentProxyMessage
      {
        get
        {
          AppSettings.EnsureSettingsLoaded();
          return AppSettings.allowTransparentProxyMessageValue;
        }
      }

      private static void EnsureSettingsLoaded()
      {
        if (AppSettings.settingsInitialized)
          return;
        lock (AppSettings.appSettingsLock)
        {
          if (AppSettings.settingsInitialized)
            return;
          try
          {
            AppSettingsReader appSettingsReader = new AppSettingsReader();
            object obj = (object) null;
            AppSettings.allowTransparentProxyMessageValue = !AppSettings.TryGetValue(appSettingsReader, AppSettings.AllowTransparentProxyMessageKeyName, typeof (bool), out obj) ? AppSettings.AllowTransparentProxyMessageDefaultValue : (bool) obj;
            if (AppSettings.TryGetValue(appSettingsReader, AppSettings.AllowUnsanitizedWSDLUrlsKeyName, typeof (bool), out obj))
              AppSettings.allowUnsanitizedWSDLUrlsValue = (bool) obj;
            else
              AppSettings.allowUnsanitizedWSDLUrlsValue = AppSettings.AllowUnsanitizedWSDLUrlsDefaultValue;
          }
          catch
          {
          }
          finally
          {
            AppSettings.settingsInitialized = true;
          }
        }
      }

      private static bool TryGetValue(
        AppSettingsReader appSettingsReader,
        string key,
        Type type,
        out object value)
      {
        try
        {
          value = appSettingsReader.GetValue(key, type);
          return true;
        }
        catch
        {
          value = (object) null;
          return false;
        }
      }
    }
}
