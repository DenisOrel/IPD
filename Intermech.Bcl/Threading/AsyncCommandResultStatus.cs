
// Type: Intermech.Threading.AsyncCommandResultStatus
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Threading
{
    /// <summary>
    /// Описывает возможные результаты выполнения асинхронной команды.
    /// </summary>
    public enum AsyncCommandResultStatus
    {
      /// <summary>Команда еще не была выполнена</summary>
      Undefined,
      /// <summary>Команда успешно выполнена</summary>
      Completed,
      /// <summary>
      /// В процессе выполнения команды возникло необработанное исключение
      /// </summary>
      Failed,
      /// <summary>Выполнение команды было прервано</summary>
      Aborted,
    }
}
