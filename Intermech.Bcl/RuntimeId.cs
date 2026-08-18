
// Type: Intermech.RuntimeId
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Threading;


namespace Intermech
{
    /// <summary>
    /// Реализует генератор уникальных идентификаторов, которые могут использоваться только во время работы приложения.
    /// При разных запусках приложения значения генерируемых идентификаторов могут отличаться. Полученные от этого сервиса
    /// идентификаторы нельзя сохранять в базу или в файл.
    /// </summary>
    public static class RuntimeId
    {
      private static int lastId;

      /// <summary>Создает и возвращает новый идентификатор.</summary>
      /// <returns>Значение идентификатора</returns>
      public static int Create() => Interlocked.Increment(ref RuntimeId.lastId);
    }
}
