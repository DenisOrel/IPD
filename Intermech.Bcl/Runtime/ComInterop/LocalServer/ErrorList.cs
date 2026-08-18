
// Type: Intermech.Runtime.ComInterop.LocalServer.ErrorList
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Результат выполнения команды регистрации/дерегистрации COM-классов.
    /// </summary>
    internal sealed class ErrorList : IErrorList
    {
      private readonly LinkedList<string> errors = new LinkedList<string>();
      private readonly LinkedList<string> warnings = new LinkedList<string>();

      /// <summary>Возвращает список ошибок.</summary>
      public LinkedList<string> Errors => this.errors;

      /// <summary>Возвращает список предупреждений.</summary>
      public LinkedList<string> Warnings => this.warnings;

      /// <summary>Возвращает true, если команды выполнилась успешно.</summary>
      public bool Successful => this.errors.Count == 0 && this.warnings.Count == 0;

      void IErrorList.AddError(string message) => this.errors.AddLast(message);

      void IErrorList.AddWarning(string message) => this.warnings.AddLast(message);
    }
}
