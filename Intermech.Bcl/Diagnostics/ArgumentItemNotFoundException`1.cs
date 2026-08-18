
// Type: Intermech.Diagnostics.ArgumentItemNotFoundException`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Ошибка поиска элемента по ключу.</summary>
    [Serializable]
    public class ArgumentItemNotFoundException<TKey> : ArgumentItemNotFoundException, ISerializable
    {
      [CanBeNull]
      public TKey KeyValue { get; }

      private ArgumentItemNotFoundException()
      {
      }

      public ArgumentItemNotFoundException([CanBeNull] TKey keyValue, [NotNull, NotWhitespace] string paramName)
        : base(paramName, (string) null)
      {
        this.KeyValue = keyValue;
      }

      public ArgumentItemNotFoundException([CanBeNull] TKey keyValue, [NotNull, NotWhitespace] string paramName, [CanBeNull] string message)
        : base(paramName, message)
      {
        this.KeyValue = keyValue;
      }

      public ArgumentItemNotFoundException(
        [CanBeNull] TKey keyValue,
        [NotNull, NotWhitespace] string paramName,
        [CanBeNull] string message,
        [CanBeNull] Exception innerException)
        : base(paramName, message, innerException)
      {
        this.KeyValue = keyValue;
      }

      protected ArgumentItemNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.KeyValue = (TKey) info.GetValue(nameof (KeyValue), typeof (TKey));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("KeyValue", (object) this.KeyValue);
      }

      public override string Message
      {
        get
        {
          return !string.IsNullOrWhiteSpace(this.OriginalMessage) ? this.OriginalMessage : $"Item with key \"{this.KeyValue}\" not found";
        }
      }
    }
}
