
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerRunMode
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Указывает режим работы COM-сервера.</summary>
    public enum ComServerRunMode
    {
      /// <summary>
      /// Обычный режим работы приложения. Пользователь решает, когда приложение должно быть закрыто.
      /// </summary>
      Normal,
      /// <summary>
      /// Режим работы COM-сервера по запросу от COM-клиента. Приложение должно быть автоматически закрыто после отключения всех COM-клиентов.
      /// </summary>
      Embedding,
    }
}
