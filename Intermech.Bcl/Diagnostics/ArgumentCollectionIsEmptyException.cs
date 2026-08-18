
// Type: Intermech.Diagnostics.ArgumentCollectionIsEmptyException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;


namespace Intermech.Diagnostics
{
    /// <summary>Значение аргумента не может быть пустым</summary>
    [Serializable]
    public class ArgumentCollectionIsEmptyException : ArgumentException, ISerializable
    {
      [CanBeNull]
      [CanBeEmpty]
      public string CollectionName
      {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ParamName;
      }

      public ArgumentCollectionIsEmptyException()
      {
      }

      public ArgumentCollectionIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull, CanBeEmpty] string message = null)
        : base(message, collectionName)
      {
      }

      protected ArgumentCollectionIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      [CanBeNull]
      [CanBeEmpty]
      protected string OriginalMessage => base.Message;

      [NotNull]
      public override string Message
      {
        get
        {
          if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
            return this.OriginalMessage;
          return string.IsNullOrWhiteSpace(this.CollectionName) ? "Argument collection cannot be empty." : $"Argument {this.CollectionName} collection cannot be empty.";
        }
      }
    }
}
