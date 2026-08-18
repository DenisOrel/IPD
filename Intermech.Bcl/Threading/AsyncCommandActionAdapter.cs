
// Type: Intermech.Threading.AsyncCommandActionAdapter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Threading
{
    /// <summary>
    /// Класс-обертка, позволяющий представить произвольный метод в виде асинхронной команды с поддержкой прерывания выполнения.
    /// </summary>
    public sealed class AsyncCommandActionAdapter : AsyncCommand
    {
      private Action action;

      /// <summary>Создает объект.</summary>
      /// <param name="action">Метод, который должен быть представлен как команда</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="action" /> не должен быть равен null</exception>
      public AsyncCommandActionAdapter(Action action)
      {
        this.action = action != null ? action : throw new ArgumentNullException(nameof (action));
      }

      /// <summary>Реализует выполенение команды.</summary>
      /// <param name="commandContext">Контекст управления выполнением команды. Используется для прерывания выполнения команды.</param>
      /// <exception cref="T:Exception">В процессе выполнения команды произошло необработанное исключение</exception>
      /// <remarks>
      /// <para>
      /// Команда, получив сигнал прерывания, должна немедленно прекратить выполнение. При этом любые результаты работы команды,
      /// как полные, так и частичные, должны быть отброшены.</para>
      /// <para>Если команда самостоятельно не прекратит выполнение в течение определенного интервала времени,
      /// то она может быть принудительно остановлена с помощью асинхронного исключения.</para>
      /// <see cref="T:System.Threading.ThreadAbortException" />.
      /// </remarks>
      protected override void DoExecute(IAsyncCommandContext commandContext) => this.action();
    }
}
