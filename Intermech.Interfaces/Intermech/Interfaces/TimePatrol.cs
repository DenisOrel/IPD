
// Type: Intermech.Interfaces.TimePatrol
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Net;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Содержит утилиты для контроля за расхождением времени клиента и сервера IPS.
    /// </summary>
    public static class TimePatrol
    {
      /// <summary>
      /// Возвращает предельно допустимую разницу между клиентским и серверным временем в самом общем случае.
      /// </summary>
      public static readonly TimeSpan GeneralLimit = TimeSpan.FromMinutes(1.0);
      /// <summary>
      /// Возвращает допустимую разницу между клиентским и серверным временем, при превышении которой выполняется
      /// автоматическая синхронизация системного времени клиента с сервером.
      /// </summary>
      public static readonly TimeSpan MinimalLimit = TimeSpan.FromSeconds(1.0);
      private static readonly BooleanSwitch checkFlag = new BooleanSwitch("TimePatrol.Enable", "", "1");
      private static readonly BooleanSwitch accurateModeFlag = new BooleanSwitch("TimePatrol.UseAccurateMeasurement", "", "1");

      /// <summary>
      /// Возвращает мгновенную задержку клиентского времени относительно серверного. Если эту задержку добавить к системному времени клиента, то время клиента и сервера будет
      /// синхронизировано.
      /// </summary>
      /// <param name="session">Сессия подключения к серверу приложений</param>
      /// <returns>Результы измерения задержки клиентского времени относительно серверного</returns>
      public static ClientTimeDelay GetInstantClientTimeDelay(IUserSession session)
      {
        ISimplePtpServer server = session != null ? ServiceUtils.GetService<ISimplePtpServer>((object) session, true) : throw new ArgumentNullException(nameof (session));
        SimplePtpClient simplePtpClient = new SimplePtpClient();
        simplePtpClient.CalculateInstantDelay(server);
        return simplePtpClient.Result;
      }

      /// <summary>
      /// Возвращает усредненную задержку клиентского времени относительно серверного. Если эту задержку добавить к системному времени клиента, то время клиента и сервера будет
      /// синхронизировано.
      /// </summary>
      /// <param name="session">Сессия подключения к серверу приложений</param>
      /// <returns>Результы измерения задержки клиентского времени относительно серверного</returns>
      public static ClientTimeDelay GetMeanClientTimeDelay(IUserSession session)
      {
        ISimplePtpServer service = ServiceUtils.GetService<ISimplePtpServer>((object) session, true);
        SimplePtpClient simplePtpClient = new SimplePtpClient();
        simplePtpClient.CalculateMeanDelay(service);
        return simplePtpClient.Result;
      }

      /// <summary>
      /// Проверяет соответствие клиентского и серверного времени. Если время расходится более, чем на допустимую величину, то метод сбрасывает FaultException.
      /// </summary>
      /// <param name="offsetLimit">Величина предельно допустимого расхождения времени клиента и сервера</param>
      public static void CheckClientTime(TimeSpan offsetLimit)
      {
        if (!TimePatrol.checkFlag.Enabled)
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ClientTimeDelay clientTimeDelay = TimePatrol.GetInstantClientTimeDelay(sessionKeeper.Session);
          if (!(clientTimeDelay.Value.Duration() <= offsetLimit))
          {
            if (TimePatrol.accurateModeFlag.Enabled)
            {
              clientTimeDelay = TimePatrol.GetMeanClientTimeDelay(sessionKeeper.Session);
              if (clientTimeDelay.Value.Duration() <= offsetLimit)
                return;
            }
            throw new FaultException($"Операция прервана, так как разница между клиентским и серверным временем превышает допустимый предел в {offsetLimit.TotalMilliseconds:0.0}мс. Результаты замера: {clientTimeDelay.ToMillisecondsText()}");
          }
        }
      }

      /// <summary>
      /// Устанавливает системное время. Метод требует наличия у пользователя прав администратора (SE_SYSTEMTIME_NAME privilege).
      /// </summary>
      /// <param name="utcTime">Новое системное время в формате UTC</param>
      public static void SetSystemTime(DateTime utcTime)
      {
        if (!TimePatrol.NativeMethods.SetSystemTime(ref new TimePatrol.NativeMethods.SYSTEMTIME()
        {
          wYear = (short) utcTime.Year,
          wMonth = (short) utcTime.Month,
          wDay = (short) utcTime.Day,
          wHour = (short) utcTime.Hour,
          wMinute = (short) utcTime.Minute,
          wSecond = (short) utcTime.Second,
          wMilliseconds = (short) utcTime.Millisecond
        }))
          throw new Win32Exception(Marshal.GetLastWin32Error());
      }

      private static class NativeMethods
      {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime([In] ref TimePatrol.NativeMethods.SYSTEMTIME st);

        public struct SYSTEMTIME
        {
          public short wYear;
          public short wMonth;
          public short wDayOfWeek;
          public short wDay;
          public short wHour;
          public short wMinute;
          public short wSecond;
          public short wMilliseconds;
        }
      }
    }
}
