
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerRegistrationException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Тип исключений для ошибок регистрации COM-классов.</summary>
    [Serializable]
    public class ComServerRegistrationException : ComServerException
    {
      private const string problemsField = "_problems";
      private List<string> problems = new List<string>();

      /// <summary>Создает объект исключения.</summary>
      public ComServerRegistrationException()
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="message">Текст сообщения</param>
      public ComServerRegistrationException(string message)
        : base(message)
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="message">Текст сообщения</param>
      /// <param name="innerException">Вложенное исключение</param>
      public ComServerRegistrationException(string message, Exception innerException)
        : base(message, innerException)
      {
      }

      /// <summary>Создает объект исключения.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="ctx">Контекст сериализации</param>
      private ComServerRegistrationException(SerializationInfo info, StreamingContext ctx)
        : base(info, ctx)
      {
        this.problems = (List<string>) info.GetValue("_problems", typeof (List<string>));
      }

      /// <summary>Выполняет сериализацию объекта.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="context">Контекст сериализации</param>
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_problems", (object) this.problems);
      }

      /// <summary>Возвращает список ошибок и предупреждений.</summary>
      public List<string> Problems
      {
        [DebuggerStepThrough] get => this.problems;
      }
    }
}
