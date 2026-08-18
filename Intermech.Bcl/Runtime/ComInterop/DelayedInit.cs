
// Type: Intermech.Runtime.ComInterop.DelayedInit
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Некоторым приложениям требуется значительное время на инициализацию после запуска. До завершения инициализации API этих приложений недоступно.
    /// Этот класс предоставляет утилиты, позволяющие дождаться завершения инициалиации таких приложений.
    /// </summary>
    public static class DelayedInit
    {
      private const int waitQuantum = 100;

      /// <summary>Позволяет дождаться полной загрузки приложения.</summary>
      /// <param name="isReady">Функция для проверки состояния приложения</param>
      /// <param name="timeout">Таймаут ожидания в миллисекундах. Значение меньшее или равное 0 может быть использовано для задания бесконечного таймаута</param>
      /// <returns>Возвращает true, если загрузка приложения завершена. Возвращает false, если приложение не завершило загрузку за указанное время.</returns>
      public static bool WaitReady(Func<bool> isReady, int timeout)
      {
        if (isReady == null)
          throw new ArgumentNullException(nameof (isReady));
        bool flag = timeout > 0;
        while (!isReady())
        {
          Thread.Sleep(100);
          if (flag)
          {
            timeout -= 100;
            if (timeout <= 0 && !isReady())
              return false;
          }
        }
        return true;
      }

      /// <summary>
      /// Позволяет дождаться полной загрузки приложения. Если приложение не успело завершить загрузку за указанное время, то метод сбрасывает исключение.
      /// </summary>
      /// <param name="isReady">Функция для проверки состояния приложения</param>
      /// <param name="timeout">Таймаут ожидания в миллисекундах. Значение меньшее или равное 0 может быть использовано для задания бесконечного таймаута</param>
      /// <param name="applicationName">Имя приложения, используемое в сообщении об ошибке</param>
      /// <exception cref="T:Intermech.FaultException">Приложение не успело завершить загрузку за указанное время</exception>
      public static void WaitReadyOrFail(Func<bool> isReady, int timeout, string applicationName)
      {
        if (string.IsNullOrEmpty(applicationName))
          throw new ArgumentException("Не задано имя приложения.", nameof (applicationName));
        if (!DelayedInit.WaitReady(isReady, timeout))
          throw new FaultException($"Не удалось подключиться к {applicationName}! Таймаут ожидания инициализации приложения.");
      }
    }
}
