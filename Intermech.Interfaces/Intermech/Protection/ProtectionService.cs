
// Type: Intermech.Protection.ProtectionService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using System;
using System.ComponentModel.Design;


namespace Intermech.Protection
{
    /// <summary>
    /// Класс-helper для обеспечения работы защиты на клиенте и сервере
    /// </summary>
    public static class ProtectionService
    {
      private static IServiceProvider _serviceProvider = (IServiceProvider) new ServiceContainer();
      private static bool _ui = false;

      public static event je sw;

      public static event nq f6;

      public static IServiceProvider Provider
      {
        set => ProtectionService._serviceProvider = value;
      }

      public static IAlertMessageService AlertService
      {
        get
        {
          return ProtectionService._serviceProvider.GetService(typeof (IAlertMessageService)) as IAlertMessageService;
        }
      }

      public static IProtectionKey Key
      {
        get => ProtectionService._serviceProvider.GetService(typeof (IProtectionKey)) as IProtectionKey;
      }

      public static bool AskYesNo(string message, string caption)
      {
        return ProtectionService.sw == null || ProtectionService.sw(message, caption);
      }

      public static object GetService(Type type) => ProtectionService._serviceProvider.GetService(type);

      public static bool HasUI
      {
        get => ProtectionService._ui;
        set => ProtectionService._ui = value;
      }

      internal static string OnAuthorize(int daysLeft, string licenseText, ref bool cancel)
      {
        if (ProtectionService.f6 != null)
          return ProtectionService.f6(daysLeft, licenseText, ref cancel);
        if (daysLeft <= 0)
          cancel = true;
        return string.Empty;
      }

      public static bool CanAuthorize => ProtectionService.f6 != null;

      public static void Stop() => ProtectionService.Key?.Dispose();
    }
}
