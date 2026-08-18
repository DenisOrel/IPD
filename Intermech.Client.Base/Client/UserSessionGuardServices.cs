using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.Diagnostics;


namespace Intermech.Client
{
    /// <summary>
    /// Содержит сервисы, относящиеся к защите методов и свойств объектов сервера приложений от использования вне SessionKeeper.
    /// </summary>
    public static class UserSessionGuardServices
    {
      private static readonly Lazy<bool> isEnabled = new Lazy<bool>(new Func<bool>(UserSessionGuardServices.DetectEnabled), true);

      /// <summary>
      /// Возвращает true, если защита методов и свойств объектов сервера приложений от использования вне SessionKeeper включена.
      /// </summary>
      public static bool IsEnabled
      {
        [DebuggerStepThrough] get => UserSessionGuardServices.isEnabled.Value;
      }

      private static bool DetectEnabled()
      {
        return RegistryHelper.GetValue<int>(RegistryHive.CurrentUser, "Software\\Intermech\\IPS", "SessionGuardEnabled", 1) != 0;
      }
    }
}
