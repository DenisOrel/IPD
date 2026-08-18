
// Type: Intermech.Diagnostics.ErrorMessageException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    public class ErrorMessageException : InvalidOperationException
    {
      [CanBeNull]
      [NotWhitespace]
      public string Caption { get; }

      public ErrorMessageException()
      {
      }

      public ErrorMessageException([CanBeNull] string message)
        : base(message)
      {
      }

      public ErrorMessageException([CanBeNull] string message, [NotNull] Exception innerException)
        : base(message, innerException)
      {
      }

      public ErrorMessageException([CanBeNull] string message, [CanBeNull] string caption)
        : base(message)
      {
        if (string.IsNullOrWhiteSpace(caption))
          return;
        this.Caption = caption;
      }

      public ErrorMessageException([CanBeNull] string message, [CanBeNull] string caption, [NotNull] Exception innerException)
        : base(message, innerException)
      {
        if (string.IsNullOrWhiteSpace(caption))
          return;
        this.Caption = caption;
      }

      protected ErrorMessageException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.Caption = info.GetString(nameof (Caption));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("Caption", (object) this.Caption);
      }
    }
}
