
// Type: Intermech.Diagnostics.IErrorReporter
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Позволяет реализовать вывод списков ошибок в логи, журналы и пр.
    /// </summary>
    public interface IErrorReporter
    {
      /// <summary>
      /// Выводит список ошибок в журнал в форме, пригодной для чтения пользователем.
      /// </summary>
      /// <param name="errors">Коллекция ошибок</param>
      /// <exception cref="T:System.ArgumentNullException">errors</exception>
      void ReportErrors(ICollection<ErrorInfo> errors);
    }
}
