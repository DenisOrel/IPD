
// Type: Intermech.Diagnostics.ValueOutOfRangeException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    [Serializable]
    public class ValueOutOfRangeException : ValueException, ISerializable
    {
      public ValueOutOfRangeException()
      {
      }

      public ValueOutOfRangeException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull, CanBeEmpty] string message = null)
        : base(valueName, message)
      {
      }

      protected ValueOutOfRangeException([NotNull] SerializationInfo info, StreamingContext context)
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
          return string.IsNullOrWhiteSpace(this.ValueName) ? "Value is out of the range of valid values." : this.ValueName + " value is out of the range of valid values.";
        }
      }
    }
}
