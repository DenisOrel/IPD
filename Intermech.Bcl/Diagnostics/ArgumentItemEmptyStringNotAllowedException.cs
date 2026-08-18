
// Type: Intermech.Diagnostics.ArgumentItemEmptyStringNotAllowedException
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
    public class ArgumentItemEmptyStringNotAllowedException : 
      ArgumentEmptyStringNotAllowedException,
      ISerializable
    {
      [CanBeNull]
      public string CollectionClassName { get; }

      public ArgumentItemEmptyStringNotAllowedException()
        : this((IEnumerable<string>) null, (string) null)
      {
      }

      public ArgumentItemEmptyStringNotAllowedException(
        [CanBeNull, NoEnumeration] IEnumerable<string> strings,
        [CanBeNull, InvokerParameterName] string collectionName,
        [CanBeNull] string message = null)
        : base(collectionName, message)
      {
        this.CollectionClassName = strings?.GetType().FullName;
      }

      public ArgumentItemEmptyStringNotAllowedException([CanBeNull, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
        : this((IEnumerable<string>) null, collectionName, message)
      {
      }

      protected ArgumentItemEmptyStringNotAllowedException(
        [NotNull] SerializationInfo info,
        StreamingContext context)
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
          if (string.IsNullOrWhiteSpace(this.ParamName))
            return "Argument strings collection cannot contains empty strings.";
          if (string.IsNullOrWhiteSpace(this.CollectionClassName))
            return $"Argument strings collection {this.ParamName} cannot contains empty strings.";
          return $"Argument strings collection {this.ParamName} of type {this.CollectionClassName} cannot contains empty strings.";
        }
      }
    }
}
