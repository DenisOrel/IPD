
// Type: Intermech.Globalization.UICultureHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Configuration;
using System.Globalization;


namespace Intermech.Globalization
{
    /// <summary>
    /// Позволяет назначить приложению DefaultThreadCurrentUICulture.
    /// </summary>
    public static class UICultureHelper
    {
      /// <summary>
      /// Назначает приложению DefaultThreadCurrentUICulture. Имя локали берется из App.config из ключа 'UICulture' в секции 'AppSettings'.
      /// Метод должен быть вызван при старте приложения как можно раньше.
      /// </summary>
      public static void ApplySettingsFromConfigurationFile()
      {
        string uiCultureName = ConfigurationManager.AppSettings["UICulture"];
        if (uiCultureName != null)
          uiCultureName = uiCultureName.Trim();
        if (string.IsNullOrEmpty(uiCultureName))
          return;
        CultureInfo cultureInfo = UICultureHelper.TryCreateCultureInfo(uiCultureName);
        if (cultureInfo == null)
          return;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
      }

      private static CultureInfo TryCreateCultureInfo(string uiCultureName)
      {
        try
        {
          return CultureInfo.GetCultureInfo(uiCultureName);
        }
        catch (ArgumentException ex)
        {
          return (CultureInfo) null;
        }
      }
    }
}
