
// Type: Intermech.Runtime.ComInterop.RetryRejectedCallsFilter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime.ComInterop.ComTypes;
using System;


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Фильтр для очереди сообщений COM, позволяющий повторить последнее обращение к любому COM-объекту в случае ошибки RPC_E_CALL_REJECTED.
    /// </summary>
    public sealed class RetryRejectedCallsFilter : MessageFilter
    {
      private int retryDelay;
      private int retryTimeout;

      /// <summary>Создает объект.</summary>
      public RetryRejectedCallsFilter()
        : this(TimeSpan.FromSeconds(1.0))
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="retryDelay">Задержка перед повторным обращением к COM-объекту</param>
      /// <exception cref="T:ArgumentOutOfRangeException">Параметр <paramref name="retryDelay" /> должен быть положительным числом или равен 0</exception>
      public RetryRejectedCallsFilter(TimeSpan retryDelay)
      {
        int totalMilliseconds = (int) retryDelay.TotalMilliseconds;
        this.retryDelay = totalMilliseconds >= 0 ? totalMilliseconds : throw new ArgumentOutOfRangeException("retryDelayMs");
        this.retryTimeout = (int) TimeSpan.FromMinutes(1.0).TotalMilliseconds;
      }

      /// <summary>
      /// Provides applications with an opportunity to display a dialog box offering retry, cancel, or task-switching options.
      /// </summary>
      /// <param name="hTaskCallee">The thread id of the called application</param>
      /// <param name="dwTickCount">The number of elapsed ticks since the call was made</param>
      /// <param name="dwRejectType">Specifies either SERVERCALL_REJECTED or SERVERCALL_RETRYLATER, as returned by the object application</param>
      /// <returns>The number of tick before retry or -1 to cancel a call</returns>
      public override int RetryRejectedCall(
        IntPtr hTaskCallee,
        int dwTickCount,
        SERVERCALL dwRejectType)
      {
        return dwTickCount < this.retryTimeout ? this.retryDelay : base.RetryRejectedCall(hTaskCallee, dwTickCount, dwRejectType);
      }
    }
}
