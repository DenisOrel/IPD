
// Type: Intermech.Threading.IAsyncCommandContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Threading
{
    /// <summary>Интерфейс объекта для управления выполнением команды.</summary>
    public interface IAsyncCommandContext
    {
      /// <summary>
      /// Признак, что было затребовано прерывание выполнения команды.
      /// Значение свойства может быть изменено в любой момент, так как прерывание выполнения команды может быть выполнено асинхронно из любого потока приложения.
      /// </summary>
      bool CommandAborted { get; }
    }
}
