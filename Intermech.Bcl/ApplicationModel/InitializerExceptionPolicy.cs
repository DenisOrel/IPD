
// Type: Intermech.ApplicationModel.InitializerExceptionPolicy
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.ApplicationModel
{
    /// <summary>Политики обработки исключений инициализации модулей</summary>
    public enum InitializerExceptionPolicy
    {
      /// <summary>
      /// При возникновении исключения инициализации модуля оно будет брошено дальше.
      /// </summary>
      Normal,
      /// <summary>
      /// При возникновении исключения инициализации модуля оно будет подавлено.
      /// </summary>
      Suppress,
    }
}
