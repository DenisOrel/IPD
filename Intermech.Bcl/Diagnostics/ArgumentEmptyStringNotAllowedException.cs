
// Type: Intermech.Diagnostics.ArgumentEmptyStringNotAllowedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "строка не должна быть пустой".</summary>
    [Serializable]
    public class ArgumentEmptyStringNotAllowedException : ArgumentValueEmptyException, ISerializable
    {
      public ArgumentEmptyStringNotAllowedException()
      {
      }

      public ArgumentEmptyStringNotAllowedException([CanBeNull, InvokerParameterName] string argumentName, [CanBeNull] string message = null)
        : base(argumentName, message)
      {
      }

      protected ArgumentEmptyStringNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          return string.IsNullOrWhiteSpace(this.ParamName) ? "Argument string cannot be empty." : $"Argument {this.ParamName} string cannot be empty.";
        }
      }
    }
}
