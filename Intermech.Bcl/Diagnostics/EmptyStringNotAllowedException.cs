
// Type: Intermech.Diagnostics.EmptyStringNotAllowedException
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
    public class EmptyStringNotAllowedException : ValueEmptyException, ISerializable
    {
      public EmptyStringNotAllowedException()
      {
      }

      public EmptyStringNotAllowedException([CanBeNull, InvokerParameterName] string valueName, [CanBeNull] string message = null)
        : base(valueName, message)
      {
      }

      protected EmptyStringNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
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
          return string.IsNullOrWhiteSpace(this.ValueName) ? "String cannot be empty." : this.ValueName + " string cannot be empty.";
        }
      }
    }
}
