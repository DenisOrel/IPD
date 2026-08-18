
// Type: Intermech.Diagnostics.ItemWhitespaceNotAllowedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Все строки коллекции должны содержать хотя бы один отличный от пробела символ".</summary>
    [Serializable]
    public class ItemWhitespaceNotAllowedException : ItemEmptyStringNotAllowedException, ISerializable
    {
      public ItemWhitespaceNotAllowedException()
      {
      }

      public ItemWhitespaceNotAllowedException(
        [CanBeNull, NoEnumeration] IEnumerable<string> strings,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] string message = null)
        : base(strings, collectionName, message)
      {
      }

      public ItemWhitespaceNotAllowedException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string customMessage = null)
        : base(collectionName, customMessage)
      {
      }

      protected ItemWhitespaceNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
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
          if (string.IsNullOrWhiteSpace(this.CollectionName))
            return "Strings collection cannot contains a whitespace strings.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Strings collection {this.CollectionName} cannot contains a whitespace strings.";
          return $"Strings collection {this.CollectionName} of type {this.CollectionClassName} cannot contains a whitespace strings.";
        }
      }
    }
}
