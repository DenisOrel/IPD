
// Type: Intermech.Remoting.Ipc.IpcConnectorContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Diagnostics;


namespace Intermech.Remoting.Ipc
{
    /// <summary>
    /// Общий контекст разделяемый всеми экземплярами <see cref="T:Intermech.Remoting.Ipc.IpcConnector`1" />.
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    internal static class IpcConnectorContext
    {
      private static readonly IpcConnectorProcessTable processTable = new IpcConnectorProcessTable();

      /// <summary>
      /// Таблица процессов, запущенных на выполнение с помощью <see cref="T:Intermech.Remoting.Ipc.IpcConnector`1" />.
      /// Используется для хранение и последующего получения информации о процессах, которую нельзя получить
      /// стандартными средствами .NET
      /// </summary>
      public static IpcConnectorProcessTable ProcessTable
      {
        [DebuggerStepThrough] get => IpcConnectorContext.processTable;
      }
    }
}
