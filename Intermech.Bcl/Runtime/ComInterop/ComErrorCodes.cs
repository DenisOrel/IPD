
// Type: Intermech.Runtime.ComInterop.ComErrorCodes
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop
{
    /// <summary>
    /// Содержит часто встречающиеся коды ошибок, возвращаемых функциями WinAPI для работы с COM-объектами, а также самими COM-объектами.
    /// </summary>
    public static class ComErrorCodes
    {
      /// <summary>Вызов прошел без ошибок</summary>
      public const int S_OK = 0;
      /// <summary>
      /// Общая ошибка. Часто используется как аналог System.InvalidOperationException, т.е. вызов не применим к объекту в текущем состоянии
      /// </summary>
      public const int E_FAIL = -2147467259 /*0x80004005*/;
      /// <summary>Операция прервана</summary>
      public const int E_ABORT = -2147467260 /*0x80004004*/;
      /// <summary>
      /// Приложение завершает работу и не может обработать запрос
      /// </summary>
      public const int E_APPLICATION_EXITING = -2147483622 /*0x8000001A*/;
      /// <summary>
      /// Приложение занято и не может обработать входящий вызов
      /// </summary>
      public const int RPC_E_CALL_REJECTED = -2147418111 /*0x80010001*/;
      /// <summary>Библиотека типов не зарегистрирована</summary>
      public const int TYPE_E_LIBNOTREGISTERED = -2147319779;
      /// <summary>Класс не зарегистрирован.</summary>
      public const int REGDB_E_CLASSNOTREG = -2147221164;
      /// <summary>Ошибка при обращении к реестру OLE</summary>
      internal const int TYPE_E_REGISTRYACCESS = -2147319780;
      /// <summary>
      /// Файл существует, но не является структурированным хранилищем
      /// </summary>
      internal const int STG_E_FILEALREADYEXISTS = -2147286960 /*0x80030050*/;
      /// <summary>Не удается выполнить требуемую операцию.</summary>
      internal const int STG_E_INVALIDFUNCTION = -2147287039 /*0x80030001*/;
    }
}
