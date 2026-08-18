
// Type: Intermech.Diagnostics.ArgumentValueEmptyException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Аргумент не может быть пуст.</summary>
    [Serializable]
    public class ArgumentValueEmptyException : ArgumentException, ISerializable
    {
      public ArgumentValueEmptyException()
      {
      }

      public ArgumentValueEmptyException([CanBeNull, CanBeEmpty] string argumentName, [CanBeNull, CanBeEmpty] string message = null)
        : base(message, argumentName)
      {
      }

      protected ArgumentValueEmptyException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      [CanBeNull]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          return string.IsNullOrWhiteSpace(this.ParamName) ? "Argument cannot be empty." : $"Argument {this.ParamName} cannot be empty.";
        }
      }
    }
}
