
// Type: Intermech.Diagnostics.InvalidUriException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    [Serializable]
    public class InvalidUriException : ValueException, ISerializable
    {
      [CanBeNull]
      public string Uri { get; }

      public InvalidUriException()
      {
      }

      public InvalidUriException([NotNull, NotWhitespace] string uri, [CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull, CanBeEmpty] string message = null)
        : base(valueName, message)
      {
        this.Uri = uri;
      }

      protected InvalidUriException([NotNull] SerializationInfo info, StreamingContext context)
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
          if (string.IsNullOrWhiteSpace(this.ValueName))
            return this.Uri == null ? "The format of the URI = null could not be determined." : $"The format of the URI \"{this.Uri}\" could not be determined.";
          if (this.Uri == null)
            return $"The format of the URI {this.ValueName} = null could not be determined.";
          return $"The format of the URI {this.ValueName} \"{this.Uri}\" could not be determined.";
        }
      }
    }
}
