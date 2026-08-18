
// Type: Intermech.Threading.RunOnceGuard
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Threading;


namespace Intermech.Threading
{
    /// <summary>
    /// Класс для выполнения методов только один раз.
    /// Реализация является thread safe.
    /// </summary>
    public sealed class RunOnceGuard
    {
      private const int NOT_RUN = 0;
      private const int RUN_PENDING = 1;
      private const int ALREADY_RUN = 2;
      private int runState;

      /// <summary>Выполняет метод, если он еще не был выполнен.</summary>
      /// <param name="action">Выполняемый метод</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="action" /> содержит null</exception>
      public void RunOnce(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        if (Interlocked.CompareExchange(ref this.runState, 1, 0) != 0)
          return;
        try
        {
          action();
          Interlocked.Exchange(ref this.runState, 2);
        }
        catch
        {
          Interlocked.Exchange(ref this.runState, 0);
          throw;
        }
      }
    }
}
