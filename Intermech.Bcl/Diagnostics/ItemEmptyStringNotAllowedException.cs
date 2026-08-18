
// Type: Intermech.Diagnostics.ItemEmptyStringNotAllowedException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Исключительная ситуация вида "Коллекция строк не должна содержать пустые строки".</summary>
    [Serializable]
    public class ItemEmptyStringNotAllowedException : EmptyStringNotAllowedException, ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      [CanBeNull]
      public string CollectionName => this.ValueName;

      public ItemEmptyStringNotAllowedException()
        : this((IEnumerable<string>) null, (string) null)
      {
      }

      public ItemEmptyStringNotAllowedException(
        [CanBeNull, NoEnumeration] IEnumerable<string> strings,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] string message = null)
        : base(collectionName, message)
      {
        this.CollectionClassName = strings?.GetType().FullName;
      }

      public ItemEmptyStringNotAllowedException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((IEnumerable<string>) null, collectionName, message)
      {
      }

      protected ItemEmptyStringNotAllowedException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.CollectionClassName = info.GetString(nameof (CollectionClassName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("CollectionClassName", (object) this.CollectionClassName);
      }

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          if (string.IsNullOrWhiteSpace(this.CollectionName))
            return "String collection cannot contains empty strings.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Strings collection {this.CollectionName} cannot contains empty strings.";
          return $"Strings collection {this.CollectionName} of type {this.CollectionClassName} cannot contains empty strings.";
        }
      }
    }
}
