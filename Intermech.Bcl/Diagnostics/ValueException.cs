
// Type: Intermech.Diagnostics.ValueException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация, относящаяся к именованному значению.</summary>
    [Serializable]
    public class ValueException : Exception, ISerializable
    {
      [CanBeNull]
      public string ValueName { get; }

      public ValueException()
      {
      }

      public ValueException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull, CanBeEmpty] string message = null)
        : base(message)
      {
        this.ValueName = valueName;
      }

      protected ValueException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.ValueName = info.GetString(nameof (ValueName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ValueName", (object) (this.ValueName ?? string.Empty));
      }

      [CanBeNull]
      protected string OriginalMessage => this.Message;
    }
}
