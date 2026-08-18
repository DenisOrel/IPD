
// Type: Intermech.Threading.LockObjects
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Threading
{
    public static class LockObjects
    {
      private static TimeSpan lockTimeout = TimeSpan.FromMinutes(1.0);

      /// <summary>
      /// Возвращает или задает таймаут получения блокировки на объекте. Используется для автоматического разрушения deadlock'ов.
      /// </summary>
      public static TimeSpan LockTimeout
      {
        get => LockObjects.lockTimeout;
        set => LockObjects.lockTimeout = value;
      }

      /// <summary>Восстанавливает значение таймаута по умолчанию.</summary>
      public static void RestoreLockTimeout() => LockObjects.LockTimeout = TimeSpan.FromMinutes(1.0);
    }
}
